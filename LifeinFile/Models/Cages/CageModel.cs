using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeinFile.Models.Cages
{
    public class CageModel: IExternalModel
    {
        public ReactiveProperty<string> Name { get; set; } = new ReactiveProperty<string>("");
        public CompositeDisposable Disposables { get;} = new CompositeDisposable();
        
        private CageExternal _external;
        public IReadOnlyList<PetExternal> Pets => PetCageConnector.GetPetsInCage(_external);
        
        public CageModel(string name, CageExternal external)
        {
            Name.Value = name;
            _external = external;
        }
        
        IReadOnlyReactiveProperty<string> IExternalModel.Name => Name;
        
        ~CageModel() => System.Diagnostics.Debug.WriteLine($"Cage clear: {Name}");
    }
}
