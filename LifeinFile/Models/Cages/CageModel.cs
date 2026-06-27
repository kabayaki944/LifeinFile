using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Core.Setting;
using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;

namespace LifeinFile.Models.Cages
{
    public class CageModel: IExternalModel
    {
        public ReactiveProperty<string> Name { get; set; } = new ReactiveProperty<string>("");
        public string InstanceId{get;set;}
        public CompositeDisposable Disposables { get;} = new CompositeDisposable();
        public string Path { get; set; }
        
        //--- AbleTo---//
        public ReactiveProperty<CageState> State { get; set; } = new ReactiveProperty<CageState>(CageState.Normal);
        public bool AbleToViewClose { get; private set; }

        void StateSubscribes()
        {
            State.Subscribe(state =>
                {
                    switch (state)
                    {
                        case CageState.Normal:
                            AbleToViewClose = true;
                            break;
                        case CageState.PetPreview:
                            AbleToViewClose = false;
                            break;
                    }
                })
                .AddTo(Disposables);
        }
        
        private CageExternal _external;
        public IReadOnlyList<PetExternal> Pets => PetCageConnector.GetPetsInCage(_external);
        
        public CageModel(string name, CageExternal external)
        {
            Name.Value = name;
            InstanceId = Guid.NewGuid().ToString();
            Path = UserSetting.Data.DefaultCreateCagePath;
            _external = external;
            
            StateSubscribes();
        }
        
        IReadOnlyReactiveProperty<string> IExternalModel.Name => Name;
        
        ~CageModel() => System.Diagnostics.Debug.WriteLine($"Cage clear: {Name}");
    }
}
