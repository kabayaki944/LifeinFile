using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using System.Windows;

namespace LifeinFile.Windows
{
    public partial class StartUpWindow : Window
    {
        public StartUpWindow()
        {
            InitializeComponent();
            
            MenuWindow menuWindow = new MenuWindow();
            menuWindow.Show();

            Close();
        }
    }
}
