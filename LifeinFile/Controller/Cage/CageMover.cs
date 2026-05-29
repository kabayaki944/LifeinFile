using LifeinFile.Models.Cages;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;

namespace LifeinFile.Controller.CageSystem
{
    public class CageMover
    {
        public CageMover(IProvideWindowInput input, IWindowDriver driver, CageModel model)
        {
            input.OnMouseLeftDownAsObservable
                .Subscribe(_ => driver.StartDragMove())
                .AddTo(model.Disposables);
        }
    }
}