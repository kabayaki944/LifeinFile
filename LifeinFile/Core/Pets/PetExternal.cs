using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets;
using System.Windows;
using Views.Windows;

namespace LifeinFile.Core.Pets
{
    public class PetExternal
    {
        public IExternalModel Model { get; private set; }
        public Window Window { get; private set; }
        PetCollision _collision;
        public void Construct(IExternalModel model, PetCollision collision, PetWindow window)
        {
            Model = model;
            _collision = collision;
            Window = window;
        }

        public void OnCollision(CollisionResult result) => _collision.OnCollision(result);
    }
}
