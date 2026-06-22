using LifeinFile.Core.Pets;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeinFile.Models.Cages
{
    public interface IExternalModel
    {
        public IReadOnlyReactiveProperty<string> Name { get; }
        public ReactiveProperty<CageState> State { get; }
    }
}
