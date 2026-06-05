using LifeinFile.Helper;
using LifeinFile.Views.Pets.Animations;
using LifeinFile.Windows;

namespace LifeinFile.Views.Pets
{
    public class TurnAnimation: DirectionAnimationBase
    {
        Direction _direction;
        // 振り向きは「サッ」と素早く行いたいので、少し短めに上書き
        protected override double EasingDuration => 0.3; 

        // コンストラクタで「どちらを向くか」を受け取る
        public TurnAnimation(PetWindow window, Direction direction) : base(window)
        {
            _direction  = direction;
        }

        protected override void OnStart(AnimationContext context)
        {
            // タイマー起動や購読(Subscribe)は不要。
            // Baseクラスの SetStartValues() が現在値を記録してくれます。
        }

        protected override void UpdateTargets(double[] targets, double phase)
        {
            // 目標の向き（ScaleX）を設定するだけ！
            double targetScaleX = (_direction == Direction.Left) ? -1 : 1;
            targets[IDX_SCALE_X] = targetScaleX;

            // ※ TransX, TransY, ScaleY は Base側の SetInitialValues() で
            // 0 や 1.0（立ちポーズの基本値）にリセットされているため、
            // どんな体勢からでも、綺麗に立ちポーズに戻りながら振り向きます。
        }
    }
}