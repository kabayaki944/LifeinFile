using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using Reactive.Bindings.Disposables;
using System.Numerics;

namespace LifeinFile.Models.Pets
{
    public class PetModel:IExternalModel
    {
        //---Basic---//
        public string Name { get; set; }
        public Vector2 Position { get; set;}
        public Vector2 Velocity { get; set; }

        private PetExternal _external;
        public CageExternal BelongCage => PetCageConnecter.GetCageOfPet(_external);
        
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
            Name = name;
            Position = position;
            Velocity = Vector2.Zero;
            _external = external;
        }
    }
}
