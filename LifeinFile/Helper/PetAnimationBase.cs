using LifeinFile.Helper;
using LifeinFile.Windows;
using System;

namespace LifeinFile.Views.Pets.Animations
{
    public abstract class PetAnimationBase
    {
        protected const int IDX_SIZE = 4;

        protected const int IDX_TRANS_X = 0;
        protected const int IDX_TRANS_Y = 1;
        protected const int IDX_SCALE_X = 2;
        protected const int IDX_SCALE_Y = 3;
        
        const double INIT_TRANS_X = 0;
        const double INIT_TRANS_Y = 0;
        const double INIT_SCALE_X = 1;
        const double INIT_SCALE_Y = 1;
        
        protected double Timer { get; private set; }
        protected virtual double EasingDuration => 0.3; 
        protected virtual double Frequency { get; set; } = 1.0;

        double[] _startValues;
        double[] _targetValues;
        double[] _easingValues;
        
        protected readonly PetWindow Window;
        protected PetAnimationBase(PetWindow window)
        {
            Window = window;
        }
        
        public void OnEnter(AnimationContext context)
        {
            Timer = 0.0;
            _startValues = new double[IDX_SIZE];
            _targetValues = new double[IDX_SIZE];
            _easingValues = new double[IDX_SIZE];
            
            OnStart(context);
            SetStartValues(); // 派生クラスで StartValues に初期値を詰めてもらう
        }

        public void OnUpdate(double deltaTime)
        {
            Timer += deltaTime;
            double easing = Math.Min(Timer / EasingDuration, 1.0);
            double phase = Timer * Frequency;

            //Target
            SetInitialValues();
            UpdateTargets(_targetValues, phase);
            
            //Leap
            if (_startValues != null)
            {
                for (int i = 0; i < _startValues.Length; i++)
                {
                    _easingValues[i] = EasingHelper.Lerp(_startValues[i], _targetValues[i], easing);
                }
            }

            // ③ Lerpが終わった配列を派生クラスに渡し、画面に反映してもらう
            ApplyToView(_easingValues);
        }

        void SetStartValues()
        {
            var trans = Window.GetTrans();
            var scale = Window.GetScale();
            _startValues[IDX_TRANS_X] = trans.x;
            _startValues[IDX_TRANS_Y] = trans.y;
            _startValues[IDX_SCALE_X] = scale.x;
            _startValues[IDX_SCALE_Y]  = scale.y;
        }

        void SetInitialValues()
        {
            _targetValues[IDX_TRANS_X] = INIT_TRANS_X;
            _targetValues[IDX_TRANS_Y] = INIT_TRANS_Y;
            _targetValues[IDX_SCALE_X] = INIT_SCALE_X;
            _targetValues[IDX_SCALE_Y] = INIT_SCALE_Y;
        }
        
        protected abstract void UpdateTargets(double[] targets, double phase);
        
        void ApplyToView(double[] currentValues)
        {
            Window.SetTrans(_easingValues[IDX_TRANS_X], _easingValues[IDX_TRANS_Y]);
            Window.SetScale(_easingValues[IDX_SCALE_X], _easingValues[IDX_SCALE_Y]);
        }

        protected abstract void OnStart(AnimationContext context);
    }
}