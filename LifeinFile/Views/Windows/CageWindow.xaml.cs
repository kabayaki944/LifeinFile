using LifeinFile.Core.Cage;
using LifeinFile.Models.Cages;
using System.Windows;
using System.Windows.Input;

namespace Views.Windows
{
    /// <summary>
    /// CageWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class CageWindow : Window
    {
        public CageExternal External { get; }
        public CageWindow(CageExternal external)
        {
            External = external;

            InitializeComponent();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
