using LifeinFile.Controller.CageSystem;
using LifeinFile.Models.Cages;
using CageWindow = LifeinFile.Windows.CageWindow;

namespace LifeinFile.Core.Cage
{
    public class CageExternal
    {
        private CageModel _model;
        public IExternalModel Model => _model;
        public CageWindow Window { get; private set;}

        LifeSystem _lifeSystem;
        public void Construct(CageModel model, CageWindow window, LifeSystem lifeSystem)
        {
            _model = model;
            Window = window;
            _lifeSystem = lifeSystem;
        }
        
        public void Kill() => _lifeSystem.Kill();
        public CageFile InstanceFile() => new CageFile(_model, Window);
    }
}
