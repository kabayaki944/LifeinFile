using LifeinFile.Models.Cages;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeinFile.Core.Cage
{
    public class CageExternal
    {
        public IExternalModel Model { get; }
        public CageExternal(IExternalModel model)
        {
            Model = model;
        }
    }
}
