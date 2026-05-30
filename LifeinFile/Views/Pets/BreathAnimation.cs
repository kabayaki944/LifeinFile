using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;
using System.Diagnostics;
using System.Numerics;
using Vector = System.Windows.Vector;

namespace LifeinFile.Views.Pets
{
    public class BreathAnimation
    {
        PetModel _model;
        PetWindow _window;
        public BreathAnimation(PetModel model, PetWindow window)
        {
            _model = model;
            _window = window;
            ProvideUpdate.LateUpdateAsObservable
                .Subscribe(_ => OnUpdateLate())
                .AddTo(model.Disposables);
        }

        const double SPEED = 0.2;
        const double SENSIBILITY = 0.03;
        Vector2 _oldVelocity =  Vector2.Zero;

        double _currentX = 0;
        void OnUpdateLate()
        {
            if (_model.Velocity == Vector2.Zero )
            {
                if(_oldVelocity != Vector2.Zero)
                    _currentX = 0d;
                
                _currentX += SPEED;
                var result = ScaleHelper.GetSlimeScale(_currentX, SENSIBILITY);
                Debug.WriteLine($"Breath Set {result.x}");
                _window.SetScale(result.x, result.y);
            }
            _oldVelocity = _model.Velocity;
        }
    }
}