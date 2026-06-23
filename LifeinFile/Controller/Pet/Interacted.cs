using LifeinFile.Models.Pets;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;

namespace LifeinFile.Controller.PetSystem
{
    public class Interacted
    {
        const double AMOUNT_AFFECTION = 20f;
        
        PetModel _model;
        public Interacted(PetModel model, IProvideWindowInput input)
        {
            _model = model;
            input.OnMouseLeftDownAsObservable
                .Subscribe(_ =>Interact())
                .AddTo(model.Disposables);
        }

        void Interact() => _model.AddAffection(AMOUNT_AFFECTION);
    }
}