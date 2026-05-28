using System.Numerics;

namespace LifeinFile.Core.Pets
{
    class PetInitData
    {
        public string Name { get;}
        public Vector2 Position { get; }

        public PetInitData(string name, Vector2 position)
        {
            Name = name;
            Position = position;
        }
    }
}
