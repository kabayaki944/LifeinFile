using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using System.Windows;
using System.Windows.Controls;

namespace LifeinFile.Windows
{
    public partial class CreatePetUserControl : UserControl
    {
        public CreatePetUserControl()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string petName = PetNameInput.Text;
            if (string.IsNullOrWhiteSpace(petName))
            {
                MessageBox.Show("名前を入力してください！", "エラー");
                return;
            }

            //MessageBox.Show($"名前「{petName}」のPetを召喚します！", "成功");
            PetInitData data = new PetInitData(petName);
            PetManager.CreatePet(data);

            // 親ウィンドウを取得して、最初のメニュー画面に戻す
            var mainWindow = Window.GetWindow(this) as MenuWindow;
            mainWindow?.ResetMenu();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MenuWindow;
            mainWindow?.ResetMenu();
        }
    }
}