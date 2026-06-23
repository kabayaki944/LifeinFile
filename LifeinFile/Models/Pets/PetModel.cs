using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Helper;
using LifeinFile.Views.Pets;
using Reactive.Bindings;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;
using System.Numerics;

namespace LifeinFile.Models.Pets
{
    public class PetModel:IExternalModel
    {
        //---Basic---//
        public ReactiveProperty<string> Name { get;} = new ReactiveProperty<string>("");
        public ReactiveProperty<Vector2> Position { get; set; } = new ReactiveProperty<Vector2>();
        public ReactiveProperty<Vector2> Velocity { get; } = new ReactiveProperty<Vector2>();
        public ReactiveProperty<Direction> Direction { get;} = new ReactiveProperty<Direction>(Helper.Direction.Right);

        public void UpdateDirectionByVelocity()
        {
            if(Velocity.Value.X ==0) return;
            Direction.Value = (Velocity.Value.X > 0) ? Helper.Direction.Right : Helper.Direction.Left;
        }
        
        public ReactiveProperty<PetSprites>  Sprites { get;} = new ReactiveProperty<PetSprites>();


        private PetExternal _external;
        public CageExternal BelongCage => PetCageConnector.GetCageOfPet(_external);
        
        //---Able to ---//
        public ReactiveProperty<PetState>  State { get;} = new ReactiveProperty<PetState>();
        public bool AbleToMove { get; private set; }
        public bool AbleToInteract { get; private set; }
        public bool AbleToShowGauge { get; private set; }
        public bool AbleToConsumeGauge { get; private set; }

        void StateSubscribe()
        {
            State.Subscribe(state =>
                {
                    switch (state)
                    {
                        case PetState.Preview:
                            AbleToMove = true;
                            AbleToInteract = false;
                            AbleToShowGauge = false;
                            AbleToConsumeGauge = false;
                            break;
                        case PetState.Active:
                            AbleToMove = true;
                            AbleToInteract = true;
                            AbleToShowGauge = true;
                            AbleToConsumeGauge = true;
                            break;
                        case PetState.Draged:
                            AbleToMove = false;
                            AbleToInteract = false;
                            AbleToShowGauge = true;
                            AbleToConsumeGauge = true;
                            break;
                        case PetState.Pose:
                            AbleToMove = false;
                            AbleToInteract = false;
                            AbleToShowGauge = true;
                            AbleToConsumeGauge = false;
                            break;
                    }
                })
                .AddTo(Disposables);
        }
        
        
        //---Reactive---//
        public CompositeDisposable Disposables{ get; }= new CompositeDisposable();

        //---Hunger---//
        public double MaxHunger { get; set; } = 100.0;
        readonly ReactiveProperty<double> _currentHunger;
        public IReadOnlyReactiveProperty<double> CurrentHunger => _currentHunger;
        
        //---Affection---//
        public double MaxAffection { get; set; } = 100.0;
        readonly ReactiveProperty<double> _currentAffection;
        public IReadOnlyReactiveProperty<double> CurrentAffection => _currentAffection;

        public void AddHunger(double amount)
        {
            _currentHunger.Value += amount;
            if (CurrentHunger.Value > MaxHunger)
                _currentHunger.Value = MaxHunger;
            
        }
        public void ConsumeHunger(double amount)
        {
            _currentHunger.Value -= amount;
            if (CurrentHunger.Value < 0)
                _currentHunger.Value = 0;
        }
        public void AddAffection(double amount)
        {
            _currentAffection.Value += amount;
            if (CurrentAffection.Value > MaxAffection)
                _currentAffection.Value = MaxAffection;
        }
        public void ConsumeAffection(double amount)
        {
            _currentAffection.Value -= amount;
            if (CurrentAffection.Value < 0)
                _currentAffection.Value = 0;
        }

        public PetModel(string name, Vector2 position, PetSprites sprites, PetExternal external)
        {
            Name.Value = name;
            Position.Value = position;
            Velocity.Value = Vector2.Zero;
            Sprites.Value = sprites;

            _currentHunger = new ReactiveProperty<double>(MaxHunger / 2);
            _currentAffection = new ReactiveProperty<double>(MaxAffection / 2);
            
            _external = external;
            
            StateSubscribe();
        }
        
        //IReadOnly
        //IReadOnlyReactiveProperty<string> IExternalModel.Name { get;}
        
        ~PetModel() => System.Diagnostics.Debug.WriteLine("PetModel is clear");
    }
}
