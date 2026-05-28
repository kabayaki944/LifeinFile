using System.Windows;
using System.Windows.Input;


namespace Views.Windows
{
    public partial class StartUpWindow : Window
    {
        public StartUpWindow()
        {
            InitializeComponent();

            PetWindow petWindow = new PetWindow();
            petWindow.Show();

            Close();
        }
    }
}
