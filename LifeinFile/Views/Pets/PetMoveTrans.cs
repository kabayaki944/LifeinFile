using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;
using System.Diagnostics;
using System.Numerics;

namespace LifeinFile.Views.Pets
{
    public class PetMoveTrans
    {
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
    }
}