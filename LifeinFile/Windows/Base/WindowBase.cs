using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using LifeinFile.Windows;

namespace LifeinFile.Windows
{
    public class WindowBase : Window, IProvideWindowInput, IReadOnlyWindowModel, IWindowDriver
    {
        public WindowBase()
        {
            AllowDrop = true; // D&Dを有効化
        }

        // ==========================================
        // ① マウス関連の実装
        // ==========================================
        public IObservable<Point> OnMouseLeftDownAsObservable =>
            Observable.FromEventPattern<MouseButtonEventArgs>(this, nameof(MouseLeftButtonDown))
                .Select(e => e.EventArgs.GetPosition(this)); // WPFのイベントから座標(Point)だけを抜き出して流す

        public IObservable<Point> OnMouseLeftUpAsObservable =>
            Observable.FromEventPattern<MouseButtonEventArgs>(this, nameof(MouseLeftButtonUp))
                .Select(e => e.EventArgs.GetPosition(this));

        public IObservable<Point> OnMouseRightDownAsObservable =>
            Observable.FromEventPattern<MouseButtonEventArgs>(this, nameof(MouseRightButtonDown))
                .Select(e => e.EventArgs.GetPosition(this));

        public IObservable<Point> OnMouseMoveAsObservable =>
            Observable.FromEventPattern<MouseEventArgs>(this, nameof(MouseMove))
                .Select(e => e.EventArgs.GetPosition(this));

        public IObservable<int> OnMouseWheelAsObservable =>
            Observable.FromEventPattern<MouseWheelEventArgs>(this, nameof(MouseWheel))
                .Select(e => e.EventArgs.Delta); // ホイールの回転量（上なら+, 下なら-）を流す

        public IObservable<Unit> OnMouseEnterAsObservable =>
            Observable.FromEventPattern<MouseEventArgs>(this, nameof(MouseEnter))
                .Select(_ => Unit.Default); // 値は不要なので Unit(空) を流す

        public IObservable<Unit> OnMouseLeaveAsObservable =>
            Observable.FromEventPattern<MouseEventArgs>(this, nameof(MouseLeave))
                .Select(_ => Unit.Default);

        // ==========================================
        // ② キーボード関連の実装
        // ==========================================
        public IObservable<Key> OnKeyDownAsObservable =>
            Observable.FromEventPattern<KeyEventArgs>(this, nameof(KeyDown))
                .Select(e => e.EventArgs.Key); // 押されたキーの種類を流す

        public IObservable<Key> OnKeyUpAsObservable =>
            Observable.FromEventPattern<KeyEventArgs>(this, nameof(KeyUp))
                .Select(e => e.EventArgs.Key);

        // ==========================================
        // ③ ドラッグ＆ドロップの実装
        // ==========================================
        public IObservable<string[]> OnFileDroppedAsObservable =>
            Observable.FromEventPattern<DragEventArgs>(this, nameof(Drop))
                .Where(e => e.EventArgs.Data.GetDataPresent(DataFormats.FileDrop)) // ファイルドロップの時だけ通す（IF文の代わり）
                .Select(e => (string[])e.EventArgs.Data.GetData(DataFormats.FileDrop)); // パス配列を流す

        public IObservable<IDataObject> OnDataDroppedAsObservable =>
            Observable.FromEventPattern<DragEventArgs>(this, nameof(Drop))
                .Select(e => e.EventArgs.Data);

        // ==========================================
        // ④ ウィンドウ状態の実装
        // ==========================================
        public IObservable<System.Reactive.Unit> OnLocationChangedAsObservable =>
            Observable.FromEventPattern<EventArgs>(this, nameof(LocationChanged))
                .Select(_ => System.Reactive.Unit.Default);

        public IObservable<Size> OnSizeChangedAsObservable =>
            Observable.FromEventPattern<SizeChangedEventArgs>(this, nameof(SizeChanged))
                .Select(e => e.EventArgs.NewSize); // 変更後のサイズを流す

        public IObservable<Unit> OnDeactivatedAsObservable =>
            Observable.FromEventPattern<EventArgs>(this, nameof(Deactivated))
                .Select(_ => Unit.Default);


        // ==========================================
        // ① Transform（座標・サイズ）の実装
        // ==========================================


        public void UpdatePosition(double left, double top)
        {
            Dispatcher.Invoke(() =>
            {
                this.Left = left;
                this.Top = top;
            });
        }

        public void UpdateSize(double width, double height)
        {
            Dispatcher.Invoke(() =>
            {
                this.Width = width;
                this.Height = height;
            });
        }

        // 取得系はInvoke不要（値を見るだけなら安全なことが多いですが、厳密にはInvokeした方がより安全です）
        public double CurrentLeft => Dispatcher.Invoke(() => this.Left);
        public double CurrentTop => Dispatcher.Invoke(() => this.Top);
        public double CurrentWidth => Dispatcher.Invoke(() => this.ActualWidth); // ActualWidthの方が正確

        public double CurrentHeight => Dispatcher.Invoke(() => this.ActualHeight);

        // SystemParameters.WorkArea はタスクバー部分を除外した安全な画面サイズを返してくれます
        public double ScreenWorkAreaWidth => SystemParameters.WorkArea.Width;
        public double ScreenWorkAreaHeight => SystemParameters.WorkArea.Height;
        public bool IsVisible => Dispatcher.Invoke(() => this.IsVisible);
        public bool IsActive => Dispatcher.Invoke(() => this.IsActive);

        // ==========================================
        // ② State（状態）の実装
        // ==========================================
        public void CloseWindow() => Dispatcher.Invoke(() => this.Close());

        public void HideWindow() => Dispatcher.Invoke(() => this.Hide());

        public void ShowWindow() => Dispatcher.Invoke(() => this.Show());

        public void SetOpacity(double opacity)
        {
            Dispatcher.Invoke(() => this.Opacity = opacity);
        }

        public void SetTopmost(bool isTopmost)
        {
            Dispatcher.Invoke(() => this.Topmost = isTopmost);
        }

        // ==========================================
        // ③ Action（アクション）の実装
        // ==========================================
        public void StartDragMove()
        {
            Dispatcher.Invoke(() =>
            {
                // マウスの左ボタンが押されている時だけ DragMove を呼ぶ（エラー回避のため）
                if (System.Windows.Input.Mouse.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            });
        }
    }
}