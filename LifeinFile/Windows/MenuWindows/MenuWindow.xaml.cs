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
    }
}