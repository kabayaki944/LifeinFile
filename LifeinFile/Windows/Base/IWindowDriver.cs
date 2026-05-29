using System;

namespace LifeinFile.Windows
{
    // ① 座標・サイズに関する操作（Transform）
    public interface IWindowTransformDrive
    {
        // 操作
        void UpdatePosition(double left, double top);
        void UpdateSize(double width, double height);
    }

    // ② ウィンドウの状態に関する操作（State）
    public interface IWindowStateDrive
    {
        void CloseWindow(); // アプリ終了や消滅時
        void HideWindow();  // 一時的に隠す（タスクトレイに引っ込む時など）
        void ShowWindow();  // 再び表示する
        void SetOpacity(double opacity); // 透明度を変更（フェードイン・アウト演出用）
        void SetTopmost(bool isTopmost); // 最前面表示をON/OFFする
    }

    // ③ WPF特有のアクション（Action）
    public interface IWindowActionDrive
    {
        // WPF標準の「ドラッグ移動」を強制的に開始する
        void StartDragMove(); 
    }

    // 上記すべてを束ねた「最強の出力インターフェース」
    public interface IWindowDriver : 
        IWindowTransformDrive, 
        IWindowStateDrive, 
        IWindowActionDrive
    {
    }
}