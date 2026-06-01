using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets.Animations;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;
using System.Diagnostics;
using System.Numerics;

namespace LifeinFile.Views.Pets
{
    public class MoveAnimation: AnimationBase
    {
        PetModel _model;
        PetWindow _window;
        
        // 独自に位相を管理するための変数（名前を分かりやすく _animX としました）
        double _animX = 0;

        protected override int Count { get; set; } = 3; 
        protected override double Frequency { get; set; } = 1.0;

        public MoveAnimation(PetModel model, PetWindow window)
        {
            _model = model;
            _window = window;
        }

        protected override void OnStart(AnimationContext context) { }

        protected override double[] GetStartValues()
        {
            // アニメーション開始時の現在値を取得（Easing補間のため）
            var scale = _window.GetScale();
            var trans = _window.GetTrans(); // ※PetWindow側に GetTrans() が必要です
            
            return new double[]
            {
                trans.y, // [0] TransY
                scale.scaleX, // [1] ScaleX
                scale.scaleY  // [2] ScaleY
            };
        }

        protected override double[] CalculateTargets(double phase)
        {
            // 💡 移動アニメーションは時間(phase)ではなく速度(Velocity)に依存するため、
            // 引数の phase は使わず、独自管理の _animX を更新します。

            _animX %= (Math.PI * 2);

            // 動いている時は速度に応じて加算
            _animX += _model.Velocity.Length() / 10d;

            // 目標値の計算
            double targetTransY = CalcTransY();
            var targetScale = CalcScale();

            return new double[]
            {
                targetTransY,
                targetScale.x,
                targetScale.y
            };
        }

        protected override void ApplyToView(double[] currentValues)
        {
            // Lerp された値を受け取って Window に反映
            // currentValues[0] = TransY, [1] = ScaleX, [2] = ScaleY
            
            _window.SetTrans(0, currentValues[0]);
            
            // 元のコードでは速度0の時に Scale を更新していませんでしたが、
            // AnimationBase の仕様上、停止時も滑らかに元の形に戻るように毎フレーム適用させています。
            _window.SetScale(currentValues[1], currentValues[2]);
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