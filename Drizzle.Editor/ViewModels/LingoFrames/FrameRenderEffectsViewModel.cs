using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Media;
using Drizzle.Lingo.Runtime;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Drizzle.Editor.ViewModels.LingoFrames
{
    public sealed class FrameRenderEffectsViewModel : LingoFrameViewModel
    {
        public ObservableCollection<RenderEffectViewModel> EffectsToRender { get; } = new();
        [Reactive] public RenderEffectViewModel? CurrentRenderingEffect { get; set; }

        private int _skipToEffect;


        public override void OnLoad(LingoViewModel lingo)
        {
            base.OnLoad(lingo);

            var movie = MovieScript;
            var effectNames = ((LingoList)movie.global_geeprops.effects)
                .Select(e => (string)((dynamic)e).nm)
                .Select(e => new RenderEffectViewModel(this, e))
                .ToArray();

            EffectsToRender.AddRange(effectNames);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            var r = (int) MovieScript.global_r;
            CurrentRenderingEffect = r == 0 ? null : EffectsToRender[r - 1];

            if (_skipToEffect != 0 && r == _skipToEffect)
                Lingo.IsPaused = true;
        }

        public void NextEffect()
        {
            _skipToEffect = (int)MovieScript.global_r + 1;
            Lingo.IsPaused = false;
        }
    }

    public sealed class RenderEffectViewModel : ViewModelBase
    {
        public FrameRenderEffectsViewModel _parent { get; }
        [Reactive] public bool IsCurrent { get; private set; }
        public string Name { get; }
        [Reactive] public Color BackgroundColor { get; private set; }

        public RenderEffectViewModel(FrameRenderEffectsViewModel parent, string name)
        {
            _parent = parent;
            Name = name;

            this.WhenAnyValue(x => x._parent.CurrentRenderingEffect)
                .Select(x => x == this)
                .Subscribe(x =>
                {
                    IsCurrent = x;
                    BackgroundColor = x ? Colors.Blue : Colors.Transparent;
                });
        }
    }
}
