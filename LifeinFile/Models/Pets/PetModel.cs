using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using System.Numerics;

namespace LifeinFile.Models.Pets
{
    public class PetModel:IExternalModel
    {
        public string Name { get; set; }
        public Vector2 Position { get; set;}
        public Vector2 Velocity { get; set; }

        private PetExternal _external;
        public CageExternal belongCage => PetCageConnecter.GetCageOfPet(_external);

        public PetModel(string name, Vector2 position, PetExternal external)
        {
            Name = name;
            Position = position;
            Velocity = Vector2.Zero;
            _external = external;
        }
    }
}
