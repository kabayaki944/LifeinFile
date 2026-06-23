using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System.Numerics;
using System.Reactive.Linq;

namespace LifeinFile.Controller.PetSystem
{ 
    public struct MoveStep
    {
        public Vector2 move;
        public int duration;
    }

    public class PetMoveBrain
    {
        const int MAX_IDLE_DURATION = 60;
        const int MIN_IDLE_DURATION = 20;
        const int MAX_MOVE_DURATION = 80;
        const int MIN_MOVE_DURATION = 40;
        const int MAX_SPEED = 5;
        const int MIN_SPEED = -5;

        private Random _random = new Random();

        public MoveStep CurrentStep{get; private set;}
        public int ActionTimer { get; private set; } = 0;

        readonly PetModel _model;
        public PetMoveBrain(PetModel model)
        {
            _model = model;
            ProvideUpdate.UpdateAsObservable
                .Where(_ => model.AbleToMove)
                .Subscribe(_ => OnUpdate())
                .AddTo(model.Disposables);
        }

        
        void OnUpdate()
        { 
            ActionTimer++;

            //最初に起動したとき
            if(CurrentStep.duration == 0) DecideNextMoveData();
            
            if (ActionTimer >= CurrentStep.duration)
            {
                DecideNextMoveData();
                _model.Velocity.Value = CurrentStep.move;
            }
        }

        // 次の行動をランダムに決める処理
        void DecideNextMoveData()
        {
            int nextAction = _random.Next(2);

            MoveStep step;

            if (nextAction == 0)
            {
                step = new MoveStep
                {
                    move = Vector2.Zero,
                    duration = _random.Next(MIN_IDLE_DURATION, MAX_IDLE_DURATION)
                };
            }
            else
            {
               step = new MoveStep
               {
                   move = new Vector2(
                        _random.Next(MIN_SPEED, MAX_SPEED),
                        _random.Next(MIN_SPEED, MAX_SPEED)
                    ),
                   duration = _random.Next(MIN_MOVE_DURATION, MAX_MOVE_DURATION)
               };
            }
            CurrentStep = step;
            ActionTimer = 0;
        }
    }
}