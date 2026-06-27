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
        private PetModel  _model;
        public IExternalModel Model => _model;
        public Window Window { get; private set; }
        LifeSystem _lifeSystem;
        PetCollision _collision;
        public void Construct(PetModel model, PetCollision collision, PetWindow window, LifeSystem lifeSystem)
        {
            _model = model;
            _collision = collision;
            Window = window;
            _lifeSystem = lifeSystem;
        }

        
        public void OnCollision(CollisionResult result) => _collision.OnCollision(result);
        public void Kill() => _lifeSystem.Kill();
        public PetFile InstanceFile() => new PetFile(_model, Window);
        
        ~PetExternal() => System.Diagnostics.Debug.WriteLine("PetExternal is clear");
    }
}
