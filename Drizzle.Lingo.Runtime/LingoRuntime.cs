using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog;

namespace Drizzle.Lingo.Runtime
{
    /// <summary>
    ///     Lingo runtime main class, wraps everything else.
    /// </summary>
    public partial class LingoRuntime
    {
        private readonly Assembly _assembly;
        public LingoGlobal Global { get; }
        public LingoScriptRuntimeBase MovieScriptInstance { get; private set; } = default!;

        private readonly Dictionary<string, Type> _behaviorScripts = new(StringComparer.InvariantCultureIgnoreCase);
        private readonly Dictionary<string, Type> _parentScripts = new(StringComparer.InvariantCultureIgnoreCase);

        public LingoRuntime(Assembly assembly)
        {
            _assembly = assembly;
            Global = new LingoGlobal(this);
        }

        public void Init()
        {
            Log.Debug("Initializing Lingo, pebbles save us all...");

            Global.Init();
            LoadCast();
            InitScript();
        }

        private void InitScript()
        {
            Type? movieScriptType = null;
            var parentScripts = new List<Type>();
            var behaviorScripts = new List<Type>();

            foreach (var type in _assembly.GetTypes())
            {
                if (Attribute.IsDefined(type, typeof(MovieScriptAttribute)))
                    movieScriptType = type;
                if (Attribute.IsDefined(type, typeof(ParentScriptAttribute)))
                    parentScripts.Add(type);
                if (Attribute.IsDefined(type, typeof(BehaviorScriptAttribute)))
                    behaviorScripts.Add(type);
            }

            if (movieScriptType == null)
                throw new Exception("Unable to find movie script type!");

            MovieScriptInstance = (LingoScriptRuntimeBase) Activator.CreateInstance(movieScriptType)!;
            MovieScriptInstance.Init(MovieScriptInstance, Global);

            foreach (var scriptType in behaviorScripts)
            {
                _behaviorScripts.Add(scriptType.Name, scriptType);
            }

            foreach (var scriptType in parentScripts)
            {
                _parentScripts.Add(scriptType.Name, scriptType);
            }

            Log.Debug("Instantiated movie script {MovieScriptType}", movieScriptType);
            Log.Debug("Found {CountParentScripts} parent and {CountBehaviorScripts} behavior scripts",
                parentScripts.Count, behaviorScripts.Count);
        }

        public void Tick()
        {
            try
            {
                TickAdvanceScore();
            }
            catch (Exception e)
            {
                Log.Error(e, "Exception in Lingo tick!");
                Environment.Exit(1);
            }
        }

        private LingoScriptRuntimeBase InstantiateBehaviorScript(string name) =>
            InstantiateScriptType(_behaviorScripts[name]);

        private LingoScriptRuntimeBase InstantiateParentScript(string name) =>
            InstantiateScriptType(_parentScripts[name]);

        private LingoScriptRuntimeBase InstantiateScriptType(Type type)
        {
            var instance = (LingoScriptRuntimeBase) Activator.CreateInstance(type)!;
            instance.Init(MovieScriptInstance, Global);

            return instance;
        }

        public LingoScriptRuntimeBase CreateScript(string type, LingoList list)
        {
            var instance = InstantiateScriptType(_parentScripts[type])!;

            var newMethod = instance.GetType().GetMethod("new");
            newMethod?.Invoke(instance, list.List.ToArray());

            return instance;
        }
    }
}
