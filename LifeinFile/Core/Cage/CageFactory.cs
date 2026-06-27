using LifeinFile.Controller.CageSystem;
using LifeinFile.Models.Cages;
using CageWindow = LifeinFile.Windows.CageWindow;

namespace LifeinFile.Core.Cage
{
    class CageFactory
    {
        public CageExternal Create(CageInitData initData)
        {
            CageExternal external = new CageExternal();
            CageModel model = new CageModel(initData.Name, external);
            CageWindow window = new CageWindow(external, model);
            LifeSystem lifeSystem = new LifeSystem(model, external, window);
            
            WindowCloseView windowCloseView = new WindowCloseView(model, window);
            CageMover mover = new CageMover(window, window, model);
            PetInCageMover petInCageMover = new PetInCageMover(model, window, window, external);
            CageCollider collider = new CageCollider(model, window);

            CageFileController fileController = new CageFileController(model, window);
            
            external.Construct(model, window, lifeSystem, fileController);
            if(initData.IsShowOnCreate)window.Show();

            return external;
        }
    }
}
