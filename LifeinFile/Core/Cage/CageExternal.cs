using LifeinFile.Controller.CageSystem;
using LifeinFile.Models.Cages;
using CageWindow = LifeinFile.Windows.CageWindow;

namespace LifeinFile.Core.Cage
{
    public class CageExternal
    {
        public IExternalModel Model { get; private set; }
        public CageWindow Window { get; private set;}

        LifeSystem _lifeSystem;
        public void Construct(IExternalModel model, CageWindow window, LifeSystem lifeSystem)
        {
            Model = model;
            Window = window;
            _lifeSystem = lifeSystem;
        }
        
        public void Kill() => _lifeSystem.Kill();
    }
}
