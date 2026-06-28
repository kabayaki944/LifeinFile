using LifeinFile.Core.Facade;
using System.Windows;

namespace LifeinFile.Windows
{
    public partial class MenuWindow : WindowBase
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void CreatePetButton_Click(object sender, RoutedEventArgs e)
        {
            MenuContent.Content = new CreatePetUserControl();
        }

        private void CreateCageButton_Click(object sender, RoutedEventArgs e)
        {
            MenuContent.Content = new CreateCageUserControl();
        }
        public void ResetMenu()
        {
            MenuContent.Content = MainMenuPanel;
        }

        void LoadCageButton_Click(object sender, RoutedEventArgs e)
        {
            LoadManager.TryLoad();
        }

        void MenuItem_DebugSaveDesktopCage_Click(object sender, RoutedEventArgs e)
        {
            SaveManager.TrySave(CageManager.DesktopCage);
        }
    }
}