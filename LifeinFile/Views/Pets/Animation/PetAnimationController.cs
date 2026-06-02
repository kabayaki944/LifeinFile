using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System.Numerics;

namespace LifeinFile.Views.Pets
{
    public class PetAnimationController
    {
        PetModel _model;
        AnimateStateMachine _stateMachine;
        PetAnimationsCreator _animations;
        public PetAnimationController(PetModel model, AnimateStateMachine stateMachine, PetAnimationsCreator animations)
        {
            _model = model;
            _stateMachine = stateMachine;
            _animations = animations;
            
            ProvideUpdate.UpdateAsObservable
                .Subscribe(_ => OnUpdate())
                .AddTo(model.Disposables);
        }

        void OnUpdate()
        {
            if(_model.Velocity != Vector2.Zero) _stateMachine.SetAnimation(_animations.MovePet);
            else _stateMachine.SetAnimation(_animations.BreathPet);
        }
        
    }
}