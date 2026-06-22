using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;
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
                .Where(_ => model.AbleToMove)
                .Subscribe(_ => OnUpdateLate())
                .AddTo(_model.Disposables);
            model.Velocity
                .Where(_ => model.AbleToMove)
                .Subscribe(_ =>OnChnage())
                .AddTo(_model.Disposables);
        }

        void OnChnage() => _model.UpdateDirectionByVelocity();

        void OnUpdateLate()
        {
            _window.Left += _model.Velocity.Value.X;
            _window.Top += _model.Velocity.Value.Y;
        }
    }
}
