using LifeinFile.Controller.PetSystem;
using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets;

using Views.Windows;

namespace LifeinFile.Core.Pets
{
    static class PetFactory
    {
        static bool _isFirst = true;

        public static PetRoot Create(PetInitData initData)
        {
            if(_isFirst)
            {
                ProvideUpdate.Start();
                _isFirst = false;
            }

            PetExternal external = new PetExternal();
            PetRoot root = new PetRoot();
            PetModel model = new PetModel(initData.Name, initData.Position, external);
            PetMoveBrain moveBrain = new PetMoveBrain(model);
            ProvideUpdate.AddUpdate(moveBrain);
            PetWindow window = new PetWindow(root, external, model);
            window.Show();
            PetCollision collision = new PetCollision(model, window);
            PetScreenCollider screenCollider = new PetScreenCollider(model, window, collision);
            PetMoveDrive mover = new PetMoveDrive(model, window);
            
            external.Construct(model, collision, window);

            return root;
        }
    }
}
