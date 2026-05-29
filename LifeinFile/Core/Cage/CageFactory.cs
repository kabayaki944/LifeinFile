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
            CageExternal external = new CageExternal();
            CageModel model = new CageModel(initData.Name, external);
            CageWindow window = new CageWindow(model, external);
            window.Show();
            CageCollider collider = new CageCollider(model, window);
            external.Construct(model, window);


            return external;
        }
    }
}
