using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Models.Pets;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;
using System.Windows;

namespace LifeinFile.Controller.PetSystem
{
    public class DragController
    {
        private readonly IWindowDriver _driver;
        private readonly IReadOnlyWindowModel _windowModel;
        private readonly PetExternal _external; 

        public DragController(
            PetModel model,
            IProvideMouseInput input, 
            IWindowDriver driver, 
            IReadOnlyWindowModel windowModel,
            PetExternal external)
        {
            _driver = driver;
            _windowModel = windowModel;
            _external = external;

            // ① マウスを「押し込んだ」時：ドラッグ移動を開始する
            input.OnMouseLeftDownAsObservable
                .Where(_ => model.AbleToInteract)
                .Subscribe(_ =>
                {
                    _driver.StartDragMove();
                    model.State.Value = PetState.Draged;
                })
                .AddTo(model.Disposables);

            // ② マウスを「離した」時：ドラッグ終了なので、ケージに入ったか判定する
            input.OnMouseLeftUpAsObservable
                .Where(_ => model.AbleToInteract)
                .Subscribe(_ =>
                {
                    CheckDropInCage();
                    model.State.Value = PetState.Active;
                })
                .AddTo(model.Disposables);
        }

        private void CheckDropInCage()
        {
            // this.Left ではなく、読み取り専用モデル(_windowModel)から取得する！
            Point petCenter = new Point(
                _windowModel.CurrentLeft + (_windowModel.CurrentWidth / 2), 
                _windowModel.CurrentTop + (_windowModel.CurrentHeight / 2)
            );

            //TOdo：Application.Current.Windows への直接アクセスは、
            // 今後「ICageManager」のような管理クラスを作って、そこから Rect をもらうようにすると更に完璧です！
            foreach (Window window in Application.Current.Windows)
            {
                if (window is LifeinFile.Windows.CageWindow cage)
                {
                    Rect cageRect = new Rect(cage.Left, cage.Top, cage.Width, cage.Height);

                    // ケージの中に中心点が入っているか判定
                    if (cageRect.Contains(petCenter))
                    {
                        // ※PetCageConnecter や cage.External は既存のシステムに合わせています
                        PetCageConnector.MovePetToCage(_external, cage.External);
                        return; // 入っていたらここで終了
                    }
                }
            }

            // どのケージにも入っていなかった場合（外にドロップ）
            PetCageConnector.MovePetToOut(_external);
        }
    }
}