using LifeinFile.Controller.CageSystem;
using LifeinFile.Models.Cages;
using CageWindow = LifeinFile.Windows.CageWindow;

namespace LifeinFile.Core.Cage
{
    static class CageFactory
    {
        public static CageExternal Create(CageInitData initData)
        {
            CageExternal external = new CageExternal();
            CageModel model = new CageModel(initData.Name, external);
            CageWindow window = new CageWindow(external);
            window.Show();
            CageMover mover = new CageMover(window, window, model);
            PetInCageMover petInCageMover = new PetInCageMover(model, window, window, external);
            CageCollider collider = new CageCollider(model, window);
            external.Construct(model, window);


            return external;
        }
    }
}
