using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Models.Cages;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;
using System.Diagnostics;

namespace LifeinFile.Controller.CageSystem
{
    public class LifeSystem
    {
        CageModel _model;
        CageExternal _external;
        CageWindow _window;
        public LifeSystem(CageModel model, CageExternal external, CageWindow window)
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

        void InnerKill()
        {
            Debug.WriteLine($"cage Kill {_model.Name}");
            _model.Disposables.Dispose();
            CageManager.RemoveCage(_external);
            
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}