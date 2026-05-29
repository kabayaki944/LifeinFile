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

        public static PetFilesController Create(PetInitData initData)
        {
            if(_isFirst)
            {
                ProvideUpdate.Start();
                _isFirst = false;
            }

            PetExternal external = new PetExternal();
            PetModel model = new PetModel(initData.Name, initData.Position, external);

            PetFilesController filesController = new PetFilesController(model);
            HungerConsumer hungerConsumer = new HungerConsumer(model);
            
            PetMoveBrain moveBrain = new PetMoveBrain(model);

            PetWindow window = new PetWindow(filesController, external, model);
            window.Show();
            
            HungerView hunger = new HungerView(model, window);

            PetCollision collision = new PetCollision(model, window);
            PetScreenCollider screenCollider = new PetScreenCollider(model, window, collision);
            PetMoveDrive mover = new PetMoveDrive(model, window);
            
            external.Construct(model, collision, window);

            return filesController;
        }
    }
}
