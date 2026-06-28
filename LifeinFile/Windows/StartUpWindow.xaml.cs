using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Core.Setting;
using LifeinFile.Helper;
using LifeinFile.Models.Cages;
using System.IO;
using System.Net;
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
            
            bool isFirst = CheckFirstStartUp();
            UserSetting.StartUp(isFirst);

            CreateDirectory();
            CreateDeskTopCage();

            Close();
        }

        void CreateDeskTopCage()
        {
            var cage = CageManager.CreateDesktopCage();
            cage.Model.State.Value = CageState.Desktop;
        }

        bool CheckFirstStartUp()
        {
            return !File.Exists(PathHelper.DefaultCagesPath);
        }

        void CreateDirectory()
        {
            Directory.CreateDirectory(PathHelper.DefaultCagesPath);
        }
        
    }
}
