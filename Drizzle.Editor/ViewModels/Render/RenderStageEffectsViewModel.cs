using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media;
using Drizzle.Logic;
using Drizzle.Logic.Rendering;

namespace Drizzle.Editor.ViewModels.Render
{
    public sealed class RenderStageEffectsViewModel : RenderStageViewModelBase
    {
        public IReadOnlyList<RenderSingleEffectViewModel> Effects { get; }

        public override (int max, int current)? Progress { get; }

        public RenderStageEffectsViewModel(RenderStageStatusEffects status)
        {
            Effects = status.EffectNames
                .Select((x, i) => new RenderSingleEffectViewModel(x, i == status.CurrentEffect - 1))
                .ToArray();

            Progress = (status.TotalEffectsCount * 60, (status.CurrentEffect - 1) * 60 + status.VertRepeater);
        }
    }

    public sealed class RenderSingleEffectViewModel : ViewModelBase
    {
        public string Name { get; }
        public bool Current { get; }

        public RenderSingleEffectViewModel(string name, bool current)
        {
            Name = name;
            Current = current;
        }
    }
}
