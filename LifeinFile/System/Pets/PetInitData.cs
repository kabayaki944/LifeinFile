using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace LifeinFile.System.Pets
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
