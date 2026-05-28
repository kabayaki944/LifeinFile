using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.System.Pets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace LifeinFile.Views.Pets
{
    class PetMoveDrive: IUpdateLate
    {
        PetModel _model;
        Window _window;
        public PetMoveDrive(PetModel model, Window window)
        {
            _model = model;
            _window = window;
            ProvideUpdate.AddUpdateLate(this);
        }

        public void OnUpdateLate()
        {
            //Todo: 衝突検知

            _window.Left += _model.Velocity.X;
            _window.Top += _model.Velocity.Y;

        }
    }
}
