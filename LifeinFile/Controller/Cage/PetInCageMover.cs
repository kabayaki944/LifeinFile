using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Models.Cages;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions; // IProvideWindowInput や IReadOnlyWindowModel のある場所
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Windows;

namespace LifeinFile.Controller.CageSystem // 適切なnamespaceに変更してください
{
    public class PetInCageMover
    {
        // 前回のケージの位置を記録する変数
        private double _lastLeft;
        private double _lastTop;
        
        private readonly CageExternal _external;
        public PetInCageMover(
            CageModel model,
            IProvideWindowInput input,
            IReadOnlyWindowModel windowModel,
            CageExternal external)
        {
            _external = external;

            // 初期位置を記憶（Loadedイベントの代わり）
            _lastLeft = windowModel.CurrentLeft;
            _lastTop = windowModel.CurrentTop;

            // ケージが移動した時のイベントを購読
            input.OnLocationChangedAsObservable
                .Subscribe(_ => OnCageLocationChanged(windowModel))
                .AddTo(model.Disposables);
        }

        private void OnCageLocationChanged(IReadOnlyWindowModel windowModel)
        {
            // 現在のケージの位置を取得
            double currentLeft = windowModel.CurrentLeft;
            double currentTop = windowModel.CurrentTop;

            // 今回ケージが動いた「差分（デルタ）」を計算
            double deltaX = currentLeft - _lastLeft;
            double deltaY = currentTop - _lastTop;

            // ケージ内のペットを取得
            IReadOnlyList<PetExternal> pets = PetCageConnector.GetPetsInCage(_external);

            if (pets != null)
            {
                foreach (var pet in pets)
                {
                    //todo
                    // ⚠️ 将来的なアーキテクチャの改善ポイント
                    // 現在は pet.Window (WPFのWindow) を直接操作していますが、
                    // ゆくゆくは pet が持つ IWindowTransformDrive.UpdatePosition() を使って
                    // 移動させるようにすると「完全なUI分離」が達成できます！
                    // 差分だけペットを平行移動させる
                    pet.Window.Left += deltaX;
                    pet.Window.Top += deltaY;
                }
            }

            // 次回の計算のために、現在の位置を「前回の位置」として保存
            _lastLeft = currentLeft;
            _lastTop = currentTop;
        }
    }
}