using LifeinFile.Helper;
using LifeinFile.Windows;
using System;

namespace LifeinFile.Views.Pets.Animations
{
    public abstract class DirectionAnimationBase: AnimationBase
    {
        protected override int IDX_SIZE { get; set; } = 2;
        
        protected const int IDX_SCALE_X = 0;
        protected const int IDX_SCALE_Y = 1;
        
        const double INIT_SCALE_X = 1;
        const double INIT_SCALE_Y = 1;
        
        
        protected readonly IPetWindow Window;
        protected DirectionAnimationBase(IPetWindow window)
        {
            Window = window;
        }
        
        
        protected override void SetStartValues()
        {
            var scale = Window.GetDirectionScale();
            _startValues[IDX_SCALE_X] = scale.x;
            _startValues[IDX_SCALE_Y]  = scale.y;
        }

        protected override void SetInitialValues()
        {
            _targetValues[IDX_SCALE_X] = INIT_SCALE_X;
            _targetValues[IDX_SCALE_Y] = INIT_SCALE_Y;
        }
        
        protected override void ApplyToView(double[] currentValues)
        {
            Window.SetDirectionScale(_easingValues[IDX_SCALE_X], _easingValues[IDX_SCALE_Y]);
        }
    }
}