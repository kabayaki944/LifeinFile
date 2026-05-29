using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
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
            ProvideUpdate.LateUpdateAsObservable
                .Subscribe(_ => OnUpdateLate())
                .AddTo(_model.Disposables);
        }

        public void OnUpdateLate()
        {
            _window.HungerGauge.Value = _model.CurrentHunger;
        }
    }
}
