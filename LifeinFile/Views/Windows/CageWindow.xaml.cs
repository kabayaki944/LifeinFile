using LifeinFile.Core.Cage;
using LifeinFile.Models.Cages;
using System;
using System.Windows;
using System.Windows.Input;

namespace Views.Windows
{
    public partial class CageWindow : Window
    {
        // 1. 前回のケージの位置を記録する変数
        private double _lastLeft;
        private double _lastTop;

        public CageExternal External { get; }
        private CageModel _model;
        public CageWindow(CageModel model, CageExternal external)
        {
            InitializeComponent();

            _model = model;
            External = external;

            // イベントの登録（XAML側に書いてもOKです）
            this.Loaded += CageWindow_Loaded;
            this.LocationChanged += CageWindow_LocationChanged;

            this.MouseLeftButtonDown += Window_MouseLeftButtonDown;
        }

        // ケージが画面に表示された瞬間の位置を記憶
        private void CageWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _lastLeft = this.Left;
            _lastTop = this.Top;
        }

        // ケージのドラッグ開始トリガー（ここは今のままでOK）
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // 2. ケージが動いた時にリアルタイムで呼ばれるイベント
        private void CageWindow_LocationChanged(object sender, EventArgs e)
        {
            // 今回ケージが動いた「差分（デルタ）」を計算
            double deltaX = this.Left - _lastLeft;
            double deltaY = this.Top - _lastTop;

            // 3. 管理しているペットたちを同じ分だけ平行移動させる
            // ※Model.Pets へのアクセス方法は現在の設計に合わせて調整してください
            if (_model?.Pets != null)
            {
                foreach (var pet in _model.Pets)
                {
                    // ペットが持っているウィンドウ（PetWindow）の座標を更新
                    if (pet.Window is Window petWindow)
                    {
                        petWindow.Left += deltaX;
                        petWindow.Top += deltaY;
                    }
                }
            }

            // 次回の計算のために、現在の位置を「前回の位置」として保存
            _lastLeft = this.Left;
            _lastTop = this.Top;
        }
    }
}