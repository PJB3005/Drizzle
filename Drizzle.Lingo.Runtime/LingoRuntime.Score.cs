using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Serilog;

namespace Drizzle.Lingo.Runtime
{
    public partial class LingoRuntime
    {
        private static readonly Dictionary<int, string> ScoreFrameScripts = new()
        {
            [1] = "startUp",
            [2] = "loadLevelStart",
            [3] = "loadLevel",
            [4] = "massRenderStart",
            [5] = "massRenderMenu",
            [9] = "LOstart",
            [10] = "levelOverview",
            [13] = "saveProject",
            [16] = "levelEditStart",
            [17] = "levelEditor",
            [20] = "changeSize",
            [23] = "propEditorStart",
            [24] = "propEditor",
            [26] = "tileEditorStart",
            [28] = "tileEditor",
            [30] = "envEditorStart",
            [31] = "envEditorLoop",
            [32] = "cameraEditorStart",
            [33] = "cameraEditor",
            [34] = "effectsEditorStart",
            [35] = "effectsEditor",
            [39] = "lightEditorStart",
            [40] = "lightEditor",
            [43] = "renderStart",
            [44] = "cameraRepeatPoint",
            [45] = "renderLayers",
            [51] = "renderPropsStart",
            [52] = "renderProps",
            [54] = "renderEffectsStart",
            [55] = "renderEffects",
            [57] = "renderPropsStart",
            [58] = "renderProps",
            [64] = "renderLightStart",
            [65] = "renderLight",
            [69] = "finalize",
            [72] = "unify",
            [74] = "renderColors",
            [75] = "applyBlur",
            [76] = "finished",
            [80] = "saveFile",
            [81] = "goback",
            [85] = "testDrawLevel",
            [90] = "massRenderLoop",
        };

        private bool _doIncrementFrame = true;
        public int CurrentFrame { get; private set; } = 0;
        private int _lastFrame;

        public LingoScriptRuntimeBase? CurrentFrameBehavior { get; private set; }
        public string? LastFrameBehaviorName { get; private set; }

        private void TickAdvanceScore()
        {
            Debug.Assert(CurrentFrameBehavior == null);

            if (_doIncrementFrame)
                CurrentFrame += 1;

            _doIncrementFrame = true;

            var changedFrame = false;
            if (CurrentFrame != _lastFrame)
            {
                _lastFrame = CurrentFrame;
                changedFrame = true;
                Log.Debug("Advancing to frame {CurrentFrame}", CurrentFrame);
            }

            if (!ScoreFrameScripts.TryGetValue(CurrentFrame, out var frameScript))
            {
                LastFrameBehaviorName = null;
                return;
            }

            if (changedFrame)
            {
                Log.Debug("Current frame behavior script is {FrameBehaviorScript}", frameScript);
            }

            CurrentFrameBehavior = InstantiateBehaviorScript(frameScript);
            LastFrameBehaviorName = frameScript;

            var method = CurrentFrameBehavior.GetType().GetMethod("enterFrame",
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                // Log.Debug("Invoking enterFrame handler");
                method.Invoke(CurrentFrameBehavior, Array.Empty<object?>());
            }

            method = CurrentFrameBehavior.GetType().GetMethod("exitFrame",
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                // Log.Debug("Invoking exitFrame handler");
                method.Invoke(CurrentFrameBehavior, Array.Empty<object?>());
            }

            CurrentFrameBehavior = null;
        }

        public void ScoreGo(int newFrame)
        {
            CurrentFrame = newFrame;
            _doIncrementFrame = false;
        }
    }
}
