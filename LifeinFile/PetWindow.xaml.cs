using LifeinFile.Scripts.CageSystem;
using System.Windows;
using System.Windows.Input;

namespace LifeinFile
{
    public partial class PetWindow : Window
    {
        Pet myPet;

        public PetWindow()
        {
            InitializeComponent();
            CageWindow cage = new CageWindow();
            cage.Show();
            myPet = new Pet(this, cage);
        }

        // マウスの左ボタンを押したままドラッグで移動できるようにする
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            // ドロップされたものが「ファイル」かどうか確認
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // ドロップされたすべてのファイルを順番に処理する
                foreach (string file in files)
                {
                    // PCから完全に消去せず、安全に「ゴミ箱」へ送る
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(
                        file,
                        Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                        Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin
                    );
                }
            }
        }
    }
}