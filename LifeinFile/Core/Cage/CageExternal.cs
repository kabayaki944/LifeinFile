using LifeinFile.Models.Cages;
using CageWindow = LifeinFile.Windows.CageWindow;

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
