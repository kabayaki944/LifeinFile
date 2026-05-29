using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Views.Windows;

namespace LifeinFile.Views.Pets
{
    public class HungerView: IUpdateLate
    {
        PetModel _model;
        PetWindow _window;
        public HungerView(PetModel model, PetWindow window)
        {
            _model = model;
            _window = window;
            ProvideUpdate.AddUpdateLate(this);
        }

        public void OnUpdateLate()
        {
            _window.HungerGauge.Value = _model.CurrentHunger;
        }
    }
}
