using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LifeinFile.Controller.PetSystem
{
    public class HungerConsumer: IUpdate
    {
        const double HUNGER_CONSUME_RATE = 0.05;

        PetModel _model;
        public HungerConsumer(PetModel model)
        {
            _model = model;
            ProvideUpdate.AddUpdate(this);
        }

        public void OnUpdate()
        {
            Debug.WriteLine($"Hunger before consuming: {_model.CurrentHunger}");
            _model.ConsumeHunger(HUNGER_CONSUME_RATE);
        }
    }
}
