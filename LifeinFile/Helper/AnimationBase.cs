using LifeinFile.Helper;
using System;

namespace LifeinFile.Views.Pets.Animations
{
    public abstract class AnimationBase
    {
        protected abstract int Count{get; set;}
        
        protected double Timer { get; private set; }
        protected virtual double EasingDuration => 0.3; 
        protected virtual double Frequency { get; set; } = 1.0;

        double[] _startValues;
        double[] _targetValues;
        double[] _easingValues;
        
        public void OnEnter(AnimationContext context)
        {
            Timer = 0.0;
            _startValues = new double[Count];
            _targetValues = new double[Count];
            _easingValues = new double[Count];
            
            OnStart(context);
            _startValues = GetStartValues(); // 派生クラスで StartValues に初期値を詰めてもらう
        }

        public void OnUpdate(double deltaTime)
        {
            Timer += deltaTime;
            double easing = Math.Min(Timer / EasingDuration, 1.0);
            double phase = Timer * Frequency;

            // ① 派生クラスに「目標値(TargetValues)」を計算してもらう
            _targetValues = CalculateTargets(phase);

            // ② 🌟 親が責任を持って、すべての変数を一括でLerpする！
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

        // --- 派生クラスに実装させるメソッド ---
        protected abstract void OnStart(AnimationContext context);
        protected abstract double[] GetStartValues();
        protected abstract double[] CalculateTargets(double phase);
        protected abstract void ApplyToView(double[] currentValues);
    }
}