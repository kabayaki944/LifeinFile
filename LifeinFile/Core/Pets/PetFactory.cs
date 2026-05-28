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

            PetRoot root = new PetRoot();
            PetModel model = new PetModel(initData.Name, initData.Position);
            PetMoveBrain moveBrain = new PetMoveBrain(model);
            ProvideUpdate.AddUpdate(moveBrain);
            PetWindow window = new PetWindow(root);
            window.Show();
            PetMoveDrive mover = new PetMoveDrive(model, window);

            return root;
        }
    }
}
