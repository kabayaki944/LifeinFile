using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace LifeinFile.Controller.PetSystem
{
    public class HungerConsumer: IUpdate
    {
        const double HUNGER_CONSUME_RATE = 0.05;

        readonly PetModel _model;
        public HungerConsumer(PetModel model)
        {
            _model = model;
            ProvideUpdate.UpdateAsObservable
                .Subscribe(_ =>OnUpdate())
                .AddTo(model.Disposables);
        }

        public void OnUpdate()
        {
            Debug.WriteLine($"Hunger before consuming: {_model.CurrentHunger}");
            _model.ConsumeHunger(HUNGER_CONSUME_RATE);
        }
    }
}
