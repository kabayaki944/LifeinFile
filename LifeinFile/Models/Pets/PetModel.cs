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
        public Vector2 Position { get; set;}
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
        private const double MAX_HUNGER = 100.0;
        private double _currentHunger = MAX_HUNGER / 2f;
        public double CurrentHunger => _currentHunger;
        
        //---Communication---//
        private const double MAX_COM = 100.0;
        private double _currentCom = MAX_COM / 2f;
        public double CurrentCom => _currentCom;

        public void AddHunger(double amount)
        {
            _currentHunger += amount;
            if (CurrentHunger > MAX_HUNGER)
                _currentHunger = MAX_HUNGER;
            
        }
        public void ConsumeHunger(double amount)
        {
            _currentHunger -= amount;
            if (CurrentHunger < 0)
                _currentHunger = 0;
        }
        public void AddCom(double amount)
        {
            _currentCom += amount;
            if (CurrentCom > MAX_COM)
                _currentCom = MAX_COM;
        }
        public void ConsumeCom(double amount)
        {
            _currentCom -= amount;
            if (CurrentCom < 0)
                _currentCom = 0;
        }

        public PetModel(string name, Vector2 position, PetSprites sprites, PetExternal external)
        {
            Name.Value = name;
            Position = position;
            Velocity.Value = Vector2.Zero;
            Sprites.Value = sprites;
            
            _external = external;
            
            StateSubscribe();
        }
        
        //IReadOnly
        //IReadOnlyReactiveProperty<string> IExternalModel.Name { get;}
        
        ~PetModel() => System.Diagnostics.Debug.WriteLine("PetModel is clear");
    }
}
