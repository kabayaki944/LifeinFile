using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using System.Numerics;
using PetWindow = LifeinFile.Windows.PetWindow;

namespace LifeinFile.Views.Pets
{
    public class PetCollision
    {
        PetModel _model;
        PetWindow _window;

        public PetCollision(PetModel model, PetWindow window)
        {
            _model = model;
            _window = window;
        }

        public void OnCollision(CollisionResult result)
        {
            // 押し戻し
            _window.Left = result.newX;
            _window.Top = result.newY;

            // 速度の反転
            float newVelX = result.hitX ? -_model.Velocity.X : _model.Velocity.X;
            float newVelY = result.hitY ? -_model.Velocity.Y : _model.Velocity.Y;
            _model.Velocity = new Vector2(newVelX, newVelY);
        }
    }
}
