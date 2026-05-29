using LifeinFile.Models.Cages;
using System;
using System.Collections.Generic;
using System.Text;
using Views.Windows;

namespace LifeinFile.Core.Cage
{
    public class CageExternal
    {
        public IExternalModel Model { get; private set; }
        public CageWindow Window { get; private set;}
        public void Construct(IExternalModel model, CageWindow window)
        {
            Model = model;
            Window = window;
        }
    }
}
