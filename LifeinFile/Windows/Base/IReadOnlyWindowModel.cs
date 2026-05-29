using System.Windows;

namespace LifeinFile.Windows
{
    public interface IReadOnlyWindowModel
    {
        // ==========================================
        // ① 自身の座標とサイズ（移行分）
        // ==========================================
        double CurrentLeft { get; }
        double CurrentTop { get; }
        double CurrentWidth { get; }
        double CurrentHeight { get; }

        // ==========================================
        // ② 画面（モニター）の境界情報（★超おすすめ）
        // ==========================================
        // ペットが画面外に逃げないように壁反射の計算をするには、
        // 「今いるモニターの広さ（タスクバーを除いた純粋な作業領域）」が絶対に必要になります。
        double ScreenWorkAreaWidth { get; }
        double ScreenWorkAreaHeight { get; }

        // ==========================================
        // ③ ウィンドウの現在の状態（★おすすめ）
        // ==========================================
        // 「今画面に表示されているか？（Hide状態じゃないか）」
        bool IsVisible { get; }
        
        // 「今ユーザーがこのウィンドウを選択（フォーカス）しているか？」
        // （クリックされたらこちらを振り向く、などの反応に使えます）
        bool IsActive { get; }
    }
}