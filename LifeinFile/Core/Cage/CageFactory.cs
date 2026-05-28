using LifeinFile.Models.Cages;
using System;
using System.Collections.Generic;
using System.Text;
using Views.Windows;

namespace LifeinFile.Core.Cage
{
    static class CageFactory
    {
        public static CageExternal Create(CageInitData initData)
        {
            CageModel model = new CageModel(initData.Name);
            CageExternal external = new CageExternal(model);
            CageWindow window = new CageWindow(external);
            window.Show();
            CageCollider collider = new CageCollider(model, window); ;

            return external;
        }
    }
}
