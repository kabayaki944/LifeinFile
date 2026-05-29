using System.Windows;
using System.Windows.Input;

namespace LifeinFile.Windows
{
    public partial class PetWindow : WindowBase
    {
        public PetWindow()
        {
            InitializeComponent();
            HungerGauge.Visibility = Visibility.Hidden;
            ComGauge.Visibility = Visibility.Hidden;
        }
        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            // ゲージを表示する
            HungerGauge.Visibility = Visibility.Visible;
            ComGauge.Visibility = Visibility.Visible;
        }

        // カーソルがPet（Grid）から外れた時
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            // ゲージを隠す
            HungerGauge.Visibility = Visibility.Hidden;
            ComGauge.Visibility = Visibility.Hidden;
        }

        public (double scaleX, double scaleY) GetScale() => (PetScale.ScaleX, PetScale.ScaleY);
        public (double x, double y) GetTrans() => (PetTranslate.X, PetTranslate.Y);

        public void SetTrans(double x, double y)
        {
            PetTranslate.X = x;
            PetTranslate.Y = y;
        }
        public void SetScale(double x, double y)
        {
            PetScale.ScaleX = x;
            PetScale.ScaleY = y;
        }

        public void ChangeScale(double amountX, double amountY)
        {
            PetScale.ScaleX += amountX;
            PetScale.ScaleY += amountY;
        }
    }
}