using LifeinFile.Controller.PetSystem;
using System.Windows;
using System.Windows.Input;

namespace Views.Windows
{
    public partial class PetWindow : Window
    {
        PetRoot _root;
        public PetWindow(PetRoot root)
        {
            InitializeComponent();
            _root = root;
        }

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

                _root.EatFiles(files);
            }
        }
    }
}