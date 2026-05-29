using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using System.ComponentModel;
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
        public CageExternal belongCage => PetCageConnecter.GetCageOfPet(_external);

        //---Hunger---//
        private const double MAX_HUNGER = 100.0;
        private double _currentHunger = MAX_HUNGER / 2f;
        public double CurrentHunger => _currentHunger;

        public void Feed(double amount)
        {
            _currentHunger += amount;
            if (CurrentHunger > MAX_HUNGER)
            {
                _currentHunger = MAX_HUNGER;
            }
        }

        public void ConsumeHunger(double amount)
        {
            _currentHunger -= amount;
            if (CurrentHunger < 0)
            {
                _currentHunger = 0;
            }
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
