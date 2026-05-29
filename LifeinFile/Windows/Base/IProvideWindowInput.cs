using System;
using System.Reactive;
using System.Windows;
using System.Windows.Input;

namespace LifeinFile.Windows
{
    // ① マウス関連の入力
    public interface IProvideMouseInput
    {
        IObservable<Point> OnMouseLeftDownAsObservable { get; }
        IObservable<Point> OnMouseLeftUpAsObservable { get; }
        IObservable<Point> OnMouseRightDownAsObservable { get; }
        IObservable<Point> OnMouseMoveAsObservable { get; }
        IObservable<int> OnMouseWheelAsObservable { get; } // ホイールの回転量
        IObservable<Unit> OnMouseEnterAsObservable { get; } // カーソルが乗った時
        IObservable<Unit> OnMouseLeaveAsObservable { get; } // カーソルが外れた時
    }

    // ② キーボード関連の入力
    public interface IProvideKeyboardInput
    {
        IObservable<Key> OnKeyDownAsObservable { get; }
        IObservable<Key> OnKeyUpAsObservable { get; }
    }

    // ③ ドラッグ＆ドロップ関連の入力
    public interface IProvideDropInput
    {
        IObservable<string[]> OnFileDroppedAsObservable { get; }
        IObservable<IDataObject> OnDataDroppedAsObservable { get; }
    }

    // ④ ウィンドウ状態関連の入力（リサイズなど）
    public interface IProvideWindowStateInput
    {
        IObservable<System.Reactive.Unit> OnLocationChangedAsObservable { get; }
        IObservable<Size> OnSizeChangedAsObservable { get; }
        IObservable<Unit> OnDeactivatedAsObservable { get; } // 裏画面に行った時（おやすみモード用などに）
    }

    // 上記すべてを束ねた「最強の入力インターフェース」
    public interface IProvideWindowInput : 
        IProvideMouseInput, 
        IProvideKeyboardInput, 
        IProvideDropInput, 
        IProvideWindowStateInput
    {
    }
}