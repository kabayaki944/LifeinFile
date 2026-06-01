using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System.Windows;

namespace LifeinFile.Views.Pets
{
    class PetMoveDrive
    {
        PetModel _model;
        Window _window;
        public PetMoveDrive(PetModel model, Window window)
        {
            _model = model;
            _window = window;
            ProvideUpdate.LateUpdateAsObservable
                .Subscribe(_ => OnUpdateLate())
                .AddTo(_model.Disposables);
        }

        public void OnUpdateLate()
        {
            _window.Left += _model.Velocity.X;
            _window.Top += _model.Velocity.Y;
        }
    }
}
