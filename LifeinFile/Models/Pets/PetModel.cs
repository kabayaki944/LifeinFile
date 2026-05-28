using LifeinFile.Core.Cage;
using System.Numerics;

namespace LifeinFile.Models.Pets
{
    public class PetModel:IExternalModel
    {
        public string Name { get; set; }
        public Vector2 Position { get; set;}
        public Vector2 Velocity { get; set; }
        public CageExternal BelongCage { get; set; } = null;

        public PetModel(string name, Vector2 position)
        {
            Name = name;
            Position = position;
            Velocity = Vector2.Zero;
        }
    }
}
