using LifeinFile.Helper;
using LifeinFile.Windows;
using System;

namespace LifeinFile.Views.Pets.Animations
{
    public abstract class AnimationBase
    {
        protected abstract int IDX_SIZE { get; set; }
        
        protected double Timer { get; private set; }
        protected virtual double EasingDuration => 0.3; 
        protected virtual double Frequency { get; set; } = 1.0;

        protected double[] _startValues;
        protected double[] _targetValues;
        protected double[] _easingValues;
        
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

        protected abstract void SetStartValues();
        protected abstract void SetInitialValues();
        
        protected abstract void UpdateTargets(double[] targets, double phase);

        protected abstract void ApplyToView(double[] currentValues);

        protected abstract void OnStart(AnimationContext context);
    }
}