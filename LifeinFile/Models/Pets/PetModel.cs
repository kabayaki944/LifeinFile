using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Helper;
using Reactive.Bindings;
using Reactive.Bindings.Disposables;
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


        private PetExternal _external;
        public CageExternal BelongCage => PetCageConnector.GetCageOfPet(_external);
        
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

        public PetModel(string name, Vector2 position, PetExternal external)
        {
            Name.Value = name;
            Position = position;
            Velocity.Value = Vector2.Zero;
            _external = external;
        }
        
        //IReadOnly
        IReadOnlyReactiveProperty<string> IExternalModel.Name { get;}
        
        ~PetModel() => System.Diagnostics.Debug.WriteLine("PetModel is clear");
    }
}
