using LifeinFile.Models.Cages;
using LifeinFile.Windows;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

public partial class WindowCloseView
{
    // --- Win32 APIの魔法の呪文 ---
    [DllImport("user32.dll")]
    private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
    [DllImport("user32.dll")]
    private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

    private const uint SC_CLOSE = 0xF060;
    private const uint MF_BYCOMMAND = 0x00000000;
    private const uint MF_ENABLED = 0x00000000;
    private const uint MF_GRAYED = 0x00000001; // 灰色にして無効化

    CageModel _model;
    CageWindow  _window;
    public WindowCloseView(CageModel model, CageWindow window)
    {
        _model = model;
        _window = window;

        model.State.Subscribe(_ => SetCloseButtonEnabled(model.AbleToViewClose));
    }

    // ★ 閉じるボタンの有効・無効を切り替える関数
    void SetCloseButtonEnabled(bool isEnabled)
    {
        // Windowのハンドル（識別番号）を取得
        IntPtr hwnd = new WindowInteropHelper(_window).Handle;
        IntPtr hMenu = GetSystemMenu(hwnd, false);

        if (hMenu != IntPtr.Zero)
        {
            uint flag = isEnabled ? MF_ENABLED : MF_GRAYED;
            EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | flag);
        }
    }
}