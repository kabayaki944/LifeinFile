using LifeinFile.Controller.PetSystem;
using LifeinFile.Models.Pets;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;

namespace LifeinFile.Views.Pets
{
    public class ShowDebugWindow
    {
        public ShowDebugWindow(PetModel model, PetMoveBrain moveBrain, PetWindow window)
        {
            window.OnDebugMenuClicked.Subscribe(_ =>Show(model, moveBrain)).AddTo(model.Disposables);
        }

        void Show(PetModel model, PetMoveBrain moveBrain)
        {
            var debugWindow = new PetDebugWindow(model, moveBrain);
            
            debugWindow.Show();
        }
    }
}