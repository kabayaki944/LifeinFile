using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets.Animations;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;
using System.Diagnostics;
using System.Numerics;

namespace LifeinFile.Views.Pets
{
    public class MoveAnimation: SquishAnimationBase
    {
        PetModel _model;
        // 独自に位相を管理するための変数（名前を分かりやすく _animX としました）
        double _animX = 0;
        protected override double Frequency { get; set; } = 1.0;

        public MoveAnimation(PetModel model, IPetWindow window) : base(window)
        {
            _model = model;
        }

        protected override void OnStart(AnimationContext context) { }
        protected override void UpdateTargets(double[] targets, double phase)
        {
            // 💡 移動アニメーションは時間(phase)ではなく速度(Velocity)に依存するため、
            // 引数の phase は使わず、独自管理の _animX を更新します。

            _animX %= (Math.PI * 2);

            // 動いている時は速度に応じて加算
            _animX += _model.Velocity.Value.Length() / 10d;

            // 目標値の計算
            double targetTransY = CalcTransY();
            var targetScale = CalcScale();

            targets[IDX_TRANS_Y] = targetTransY;
            targets[IDX_SCALE_X] = targetScale.x;
            targets[IDX_SCALE_Y] = targetScale.y;
        }

        private (double x, double y) CalcScale()
        {
            const double sensibility = 0.07;
            return ScaleHelper.GetSlimeScale(_animX, sensibility);
        } 

        private double CalcTransY()
        { 
            const double offsetY = 0.3;
            const double epsilon = 0.01;
            const double sensibility = 15;
            
            double zeroX = Math.Asin(-offsetY);
            double baseX = _animX + zeroX;
            
            double z = Math.Sin(baseX) + offsetY;
            double result = - (z + Math.Sqrt(z * z + epsilon)) / 2.0f;
            result *= sensibility;
            
            return result;
        }
    
        /*
        PetModel _model;
        PetWindow _window;
        public PetMoveTrans(PetModel model, PetWindow window)
        {
            //Debug.WriteLine("Start");
            _model = model;
            _window = window;
            ProvideUpdate.LateUpdateAsObservable
                .Subscribe(_ => UpdateLate())
                .AddTo(model.Disposables);
        }

        
        double x = 0;

        void UpdateLate()
        {
            //Debug.WriteLine("UpdateLate");

            x %= (Math.PI * 2);

            if (_model.Velocity.Length() == 0) 
            {
                // 2. 止まった時、0 と 2π のうち「今近い方」を目標地点にする（逆再生を防ぐため）
                double target = (x > Math.PI) ? Math.PI * 2 : 0;
                
                // 3. 現在のxから目標地点へ、毎フレーム少しずつ（20%ずつ）近づける
                x += (target - x) * 0.2; 
            }
            else 
            {
                // 動いている時は速度に応じて加算
                x += _model.Velocity.Length() / 10d;
            }
            
            _window.SetTrans(0, CalcTransY());
            if(_model.Velocity != Vector2.Zero)
            {
                var scale = CalcScale();
                Debug.WriteLine($"Move Set {scale.x}");
                _window.SetScale(scale.x, scale.y);
            }
            //_window.SetScale(CalcScaleX(), CalcScaleY());
        }

        (double x, double y) CalcScale()
        {
            const double sensibility = 0.07;
            
            return ScaleHelper.GetSlimeScale(x, sensibility);
        } 

        private double CalcTransY()
        { 
            const double offsetY = 0.3;
            const double epsilon = 0.01;
            const double sensibility = 15;
            
            double zeroX = Math.Asin(-offsetY);
            double baseX = x + zeroX;
            
            double z = Math.Sin(baseX) + offsetY;
            // zがマイナスの時はほぼ0に、プラスの時はほぼzになり、境界が滑らかになる
            double result = - (z + Math.Sqrt(z * z + epsilon)) / 2.0f;
            result *= sensibility;
            
            Debug.WriteLine("TransY" + result);
            return result;
        }
        */
    }
}