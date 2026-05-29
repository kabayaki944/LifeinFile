using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System.Numerics;
using System.Windows;

namespace LifeinFile.Views.Pets
{
    public class PetScreenCollider : IUpdateLate
    {
        private PetModel _model;
        private Window _window;
        private PetCollision _collision;

        private double _screenWidth;
        private double _screenHeight;

        public PetScreenCollider(PetModel model, Window window, PetCollision collision)
        {
            _model = model;
            _window = window;
            _collision = collision;
            
            ProvideUpdate.LateUpdateAsObservable
                .Subscribe(_ => OnUpdateLate())
                .AddTo(_model.Disposables);

            _screenWidth = SystemParameters.WorkArea.Width;
            _screenHeight = SystemParameters.WorkArea.Height;
        }

        public void OnUpdateLate()
        {
            Rect screenRect = new Rect(0, 0, _screenWidth, _screenHeight);

            var result = CollisionHelper.CheckAndClamp(_window, screenRect);

            if (result.hitX || result.hitY)
                _collision.OnCollision(result);
        }
    }
}