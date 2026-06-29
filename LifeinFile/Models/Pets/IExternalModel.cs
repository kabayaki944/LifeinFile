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
        public ReactiveProperty<string> Name { get;}
        public string InstanceId { get; }
        public ReactiveProperty<Vector2> Position { get;}
        public ReactiveProperty<Vector2> Velocity { get; }
        public ReactiveProperty<int> SpritesId { get; }
        
        public ReactiveProperty<PetState> State { get; }
    }
}
