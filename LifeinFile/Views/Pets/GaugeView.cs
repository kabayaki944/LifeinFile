using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using PetWindow = LifeinFile.Windows.PetWindow;

namespace LifeinFile.Views.Pets
{
    public class GaugeView
    {
        PetModel _model;
        PetWindow _window;
        public GaugeView(PetModel model, PetWindow window)
        {
            _model = model;
            _window = window;
            ProvideUpdate.LateUpdateAsObservable
                .Subscribe(_ => OnUpdateLate())
                .AddTo(_model.Disposables);
        }

        public void OnUpdateLate()
        {
            _window.HungerGauge.Value = _model.CurrentHunger;
            _window.ComGauge.Value = _model.CurrentCom;
        }
    }
}
