using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System.Numerics;
using System.Reactive.Linq;

namespace LifeinFile.Views.Pets
{
    public class SquishAnimationController
    {
        PetModel _model;
        AnimateStateMachine _stateMachine;
        PetAnimationsCreator _animations;
        public SquishAnimationController(PetModel model, AnimateStateMachine stateMachine, PetAnimationsCreator animations)
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
            if(_model.Velocity.Value != Vector2.Zero && _model.State.Value != PetState.Draged) _stateMachine.SetAnimation(_animations.Move);
            else _stateMachine.SetAnimation(_animations.Breath);
        }
        
    }
}