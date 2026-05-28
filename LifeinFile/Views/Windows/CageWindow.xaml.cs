using System.Windows;
using System.Windows.Input;

namespace Views.Windows
{
    /// <summary>
    /// CageWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CageWindow : Window
    {
        public CageWindow()
        {
            InitializeComponent();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
