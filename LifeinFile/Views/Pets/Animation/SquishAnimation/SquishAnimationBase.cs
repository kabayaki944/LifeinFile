using LifeinFile.Helper;
using LifeinFile.Windows;
using System;

namespace LifeinFile.Views.Pets.Animations
{
    public abstract class SquishAnimationBase: AnimationBase
    {
        protected override int IDX_SIZE { get; set; } = 4;

        protected const int IDX_TRANS_X = 0;
        protected const int IDX_TRANS_Y = 1;
        protected const int IDX_SCALE_X = 2;
        protected const int IDX_SCALE_Y = 3;
        
        const double INIT_TRANS_X = 0;
        const double INIT_TRANS_Y = 0;
        const double INIT_SCALE_X = 1;
        const double INIT_SCALE_Y = 1;
        
        
        protected readonly IPetWindow Window;
        protected SquishAnimationBase(IPetWindow window)
        {
            Window = window;
        }
        
        
        protected override void SetStartValues()
        {
            var trans = Window.GetTrans();
            var scale = Window.GetSquishScale();
            _startValues[IDX_TRANS_X] = trans.x;
            _startValues[IDX_TRANS_Y] = trans.y;
            _startValues[IDX_SCALE_X] = scale.x;
            _startValues[IDX_SCALE_Y]  = scale.y;
        }

        protected override void SetInitialValues()
        {
            _targetValues[IDX_TRANS_X] = INIT_TRANS_X;
            _targetValues[IDX_TRANS_Y] = INIT_TRANS_Y;
            _targetValues[IDX_SCALE_X] = INIT_SCALE_X;
            _targetValues[IDX_SCALE_Y] = INIT_SCALE_Y;
        }
        
        protected override void ApplyToView(double[] currentValues)
        {
            Window.SetTrans(_easingValues[IDX_TRANS_X], _easingValues[IDX_TRANS_Y]);
            Window.SetSquishScale(_easingValues[IDX_SCALE_X], _easingValues[IDX_SCALE_Y]);
        }
    }
}