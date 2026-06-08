using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System.Diagnostics;
using System.Numerics;

namespace LifeinFile.Views.Pets.Animation.DirectionAnimation
{
    public class DirectionAnimationController
    {
        AnimateStateMachine _stateMachine;
        PetAnimationsCreator _animations;
        public DirectionAnimationController(PetModel model, AnimateStateMachine stateMachine, PetAnimationsCreator animations)
        {
            _stateMachine = stateMachine;
            _animations = animations;

            model.Direction
                .Subscribe(OnChange)
                .AddTo(model.Disposables);
        }

        void OnChange(Direction direction)
        {
            //Debug.WriteLine("OnChange: " + direction);
            TurnAnimation turn = (direction == Direction.Left)? _animations.LeftTurn : _animations.RightTurn;
            _stateMachine.SetAnimation(turn);
        }
    }
}