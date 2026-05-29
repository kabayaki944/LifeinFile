using LifeinFile.Controller.PetSystem;
using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets;
using PetWindow = LifeinFile.Windows.PetWindow;

namespace LifeinFile.Core.Pets
{
    static class PetFactory
    {
        static bool _isFirst = true;

        public static DropFileController Create(PetInitData initData)
        {
            if(_isFirst)
            {
                ProvideUpdate.Start();
                _isFirst = false;
            }

            PetExternal external = new PetExternal();
            PetWindow window = new PetWindow();
            window.Show();

            PetModel model = new PetModel(initData.Name, initData.Position, external);

            LeftClickController leftClickController = new LeftClickController(model,  window, window, window, external);

            DropFileController filesController = new DropFileController(model, window);
            HungerConsumer hungerConsumer = new HungerConsumer(model);
            
            PetMoveBrain moveBrain = new PetMoveBrain(model);

            
            HungerView hunger = new HungerView(model, window);

            PetCollision collision = new PetCollision(model, window);
            PetScreenCollider screenCollider = new PetScreenCollider(model, window, collision);
            PetMoveDrive mover = new PetMoveDrive(model, window);
            
            external.Construct(model, collision, window);

            return filesController;
        }
    }
}
