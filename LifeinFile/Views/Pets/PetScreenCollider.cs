using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using System.Numerics;
using System.Windows;

namespace LifeinFile.Views.Pets
{
    public class PetScreenCollider : IUpdateLate
    {
        private PetModel _model;
        private Window _window;

        private double _screenWidth;
        private double _screenHeight;

        public PetScreenCollider(PetModel model, Window window)
        {
            _model = model;
            _window = window;
            ProvideUpdate.AddUpdateLate(this);

            _screenWidth = SystemParameters.WorkArea.Width;
            _screenHeight = SystemParameters.WorkArea.Height;
        }

        public void OnUpdateLate()
        {
            bool hitX = false;
            bool hitY = false;

            if (_window.Left <= 0)
            {
                _window.Left = 0;
                hitX = true;
            }
            else if (_window.Left + _window.Width >= _screenWidth)
            {
                _window.Left = _screenWidth - _window.Width;
                hitX = true;
            }

            if (_window.Top <= 0)
            {
                _window.Top = 0;
                hitY = true;
            }
            else if (_window.Top + _window.Height >= _screenHeight)
            {
                _window.Top = _screenHeight - _window.Height;
                hitY = true;
            }

            if (hitX || hitY)
            {
                float newVelX = hitX ? -_model.Velocity.X : _model.Velocity.X;
                float newVelY = hitY ? -_model.Velocity.Y : _model.Velocity.Y;

                _model.Velocity = new Vector2(newVelX, newVelY);
            }
        }
    }
}