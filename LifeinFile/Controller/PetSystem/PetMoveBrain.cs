using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System.Numerics;

namespace LifeinFile.Controller.PetSystem
{ 
    public struct MoveData
    {
        public Vector2 move;
        public double duration;
    }

    public class PetMoveBrain: IUpdate
    {
        const int MAX_IDLE_DURATION = 60;
        const int MIN_IDLE_DURATION = 20;
        const int MAX_MOVE_DURATION = 80;
        const int MIN_MOVE_DURATION = 40;
        const int MAX_SPEED = 5;
        const int MIN_SPEED = -5;

        private Random _random = new Random();

        MoveData _currentData;
        private int _actionTimer = 0;

        readonly PetModel _model;
        public PetMoveBrain(PetModel model)
        {
            _model = model;
            ProvideUpdate.UpdateAsObservable
                .Subscribe(_ => OnUpdate())
                .AddTo(model.Disposables);

            Start();
        }

        void Start()
        {
            UpdateMoveData();
        }

        public void OnUpdate()
        { 
            _actionTimer++;

            if (_actionTimer >= _currentData.duration)
            {
                UpdateMoveData();
                _model.Velocity = _currentData.move;
            }
        }

        // 次の行動をランダムに決める処理
        private void UpdateMoveData()
        {
            int nextAction = _random.Next(2);

            MoveData data;

            if (nextAction == 0)
            {
                data = new MoveData
                {
                    move = Vector2.Zero,
                    duration = _random.Next(MIN_IDLE_DURATION, MAX_IDLE_DURATION)
                };
            }
            else
            {
               data = new MoveData
               {
                   move = new Vector2(
                        _random.Next(MIN_SPEED, MAX_SPEED),
                        _random.Next(MIN_SPEED, MAX_SPEED)
                    ),
                   duration = _random.Next(MIN_MOVE_DURATION, MAX_MOVE_DURATION)
               };
            }
            _currentData = data;
            _actionTimer = 0;
        }
    }
}