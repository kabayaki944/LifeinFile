using LifeinFile.Controller.PetSystem;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Models.Pets;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Views.Windows
{
    public partial class PetWindow : Window
    {
        PetFilesController _root;
        PetExternal _external;
        PetModel _model;
        public PetWindow(PetFilesController root, PetExternal external, PetModel model)
        {
            InitializeComponent();
            _root = root;
            _external = external;
            _model = model;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
            CheckDropInCage();
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            // ドロップされたものが「ファイル」かどうか確認
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                _root.EatFiles(files);
            }
        }

        private void CheckDropInCage()
        {
            // ① Petの中心点を、WPFの「Point（点）」構造体として作る
            Point petCenter = new Point(this.Left + (this.Width / 2), this.Top + (this.Height / 2));

            foreach (Window window in Application.Current.Windows)
            {
                if (window is CageWindow cage)
                {
                    // ② ケージの範囲を「Rect（長方形）」として作る
                    Rect cageRect = new Rect(cage.Left, cage.Top, cage.Width, cage.Height);

                    // ③ Rectの必殺技「Contains（含まれるか）」を使って一発で判定！
                    if (cageRect.Contains(petCenter))
                    {
                        PetCageConnecter.MovePetToCage(_external, cage.External);
                        return;
                    }
                }
            }
            // ケージの外にドロップ
            PetCageConnecter.MovePetToOut(_external);
        }
        private void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // ゲージを表示する
            HungerGauge.Visibility = System.Windows.Visibility.Visible;
        }

        // カーソルがPet（Grid）から外れた時
        private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // ゲージを隠す
            HungerGauge.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}