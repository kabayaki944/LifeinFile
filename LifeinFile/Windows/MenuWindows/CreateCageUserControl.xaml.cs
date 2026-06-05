using LifeinFile.Core.Cage;
using System.Windows;
using System.Windows.Controls;

namespace LifeinFile.Windows
{
    public partial class CreateCageUserControl : UserControl
    {
        public CreateCageUserControl()
        {
            InitializeComponent();
        }

        // 作成ボタンが押されたとき
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string cageName = CageNameInput.Text;
            if (string.IsNullOrWhiteSpace(cageName))
            {
                MessageBox.Show("Cageの名前を入力してください！", "エラー");
                return;
            }

            // 【ここに実際のCageWindowを生成・表示するロジックを走らせる】
            //MessageBox.Show($"名前「{cageName}」のCageを配置します！", "成功");
            CageInitData data = new CageInitData(cageName);
            CageFactory.Create(data);

            // 親ウィンドウを取得して、最初のメニュー画面に戻す
            var mainWindow = Window.GetWindow(this) as MenuWindow;
            mainWindow?.ResetMenu();
        }

        // 戻るボタンが押されたとき
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MenuWindow;
            mainWindow?.ResetMenu();
        }
    }
}