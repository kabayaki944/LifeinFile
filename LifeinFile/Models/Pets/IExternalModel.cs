using LifeinFile.Views.Pets;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace LifeinFile.Models.Pets
{
    public interface IExternalModel
    {
        public IReadOnlyReactiveProperty<string> Name { get;}
        public Vector2 Position { get;}
        public ReactiveProperty<Vector2> Velocity { get; }
        public ReactiveProperty<PetSprites> Sprites { get; }
    }
}
