using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Models.Pets;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;
using System.Diagnostics;

namespace LifeinFile.Controller.PetSystem
{
    public class LifeSystem
    {
        PetModel _model;
        PetExternal _external;
        PetWindow _window;
        
        public LifeSystem(PetModel model, PetWindow window, PetExternal external)
        {
            _model = model;
            _external = external;
            _window = window;
            window.OnClosedAsObservable.Subscribe(_ => InnerKill())
                .AddTo(model.Disposables);
        }

        public void Kill()
        {
            _window.Close();
            InnerKill();
        }

        private void InnerKill()
        {
            Debug.WriteLine($"pet Kill: {_model.Name}");
            _model.Disposables.Dispose();
            PetManager.RemovePet(_external);
    
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}