using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace LifeinFile.Models.Pets
{
    public class PetModel
    {
        public string Name { get; set; }
        public Vector2 Position { get; set;}
        public Vector2 Velocity { get; set; }

        public PetModel(string name, Vector2 position)
        {
            Name = name;
            Position = position;
            Velocity = Vector2.Zero;
        }
    }
}
