using LifeinFile.Controller.PetSystem;
using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets;
using System.Windows;
using PetWindow = LifeinFile.Windows.PetWindow;

namespace LifeinFile.Core.Pets
{
    public class PetExternal
    {
        public IExternalModel Model { get; private set; }
        public Window Window { get; private set; }
        LifeSystem _lifeSystem;
        PetCollision _collision;
        public void Construct(IExternalModel model, PetCollision collision, PetWindow window, LifeSystem lifeSystem)
        {
            Model = model;
            _collision = collision;
            Window = window;
        }

        
        public void OnCollision(CollisionResult result) => _collision.OnCollision(result);
        public void Kill() => _lifeSystem.Kill();
        
        ~PetExternal() => System.Diagnostics.Debug.WriteLine("PetExternal is clear");
    }
}
