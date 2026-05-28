using LifeinFile.Controller.PetSystem;
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
        PetRoot _root;
        PetExternal _external;
        PetModel _model;
        public PetWindow(PetRoot root, PetExternal external, PetModel model)
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
                        UpdateCage(cage);
                        return;
                    }
                }
            }
            // ケージの外にドロップ
            OutOfCage();
        }

        private void UpdateCage(CageWindow cage)
        {
            //新しいケージに追加
            cage.External.Model.AddPet(_external);
            //古いケージから削除
            _model.BelongCage?.Model.RemovePet(_external);
            //Petの所属ケージを更新
            _model.BelongCage = cage.External;
            Owner = cage;
        }

        private void OutOfCage()
        {
            Debug.WriteLine("ケージの外にドロップされました");
            //古いケージから削除
            _model.BelongCage?.Model.RemovePet(_external);
            //Petの所属ケージを更新
            _model.BelongCage = null;
            Owner = null;
        }
    }
}