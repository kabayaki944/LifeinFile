using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets.Animations;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;
using System.Diagnostics;
using System.Numerics;
using Vector = System.Windows.Vector;

namespace LifeinFile.Views.Pets
{
    public class BreathAnimation: AnimationBase
    {
        PetModel _model;
        PetWindow _window;
        IReadOnlyWindowModel _windowModel;
        public BreathAnimation(PetModel model, PetWindow window, IReadOnlyWindowModel windowModel)
        {
            _model = model;
            _window = window;
            _windowModel = windowModel;
            /*
            ProvideUpdate.LateUpdateAsObservable
                .Subscribe(_ => OnUpdateLate())
                .AddTo(model.Disposables);
                */
        }

        /*
        const double SPEED = 0.2;
        // const double SENSIBILITY = 0.03;
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
        */

        //新
        protected override int Count { get; set; } = 2;
        protected override double Frequency { get; set; } = 2;
        const double SENSIBILITY = 0.03;

        
        protected override void OnStart(AnimationContext context) { }
        protected override double[] GetStartValues()
        {
            var scale = _window.GetScale();
            return  new double[]
            {
                scale.scaleX,
                scale.scaleY
            };
        }

        protected override double[] CalculateTargets(double phase)
        {
            var result = ScaleHelper.GetSlimeScale(phase, SENSIBILITY);
            return new double[]
            {
                result.x,
                result.y
            };
        }

        protected override void ApplyToView(double[] currentValues)
        {
            Debug.WriteLine($"ApplyToView {currentValues[0]}");
            _window.SetScale(currentValues[0], currentValues[1]);
        }
    }
}