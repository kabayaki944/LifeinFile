using LifeinFile.Controller.PetSystem;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System.Net.Mime;
using System.Windows;
using System.Windows.Input;

namespace LifeinFile.Windows
{
    public partial class PetWindow : WindowBase, IPetWindow
    {
        public PetWindow(PetModel model)
        {
            InitializeComponent();
            HungerGauge.Visibility = Visibility.Hidden;
            ComGauge.Visibility = Visibility.Hidden;
            
            model.Name.Subscribe(newName => Title =  newName)
                .AddTo(model.Disposables);
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

        public (double x, double y) GetSquishScale() => (SquishScale.ScaleX, SquishScale.ScaleY);
        public (double x, double y) GetDirectionScale() => (DirectionScale.ScaleX, DirectionScale.ScaleY);
        public (double x, double y) GetTrans() => (PetTranslate.X, PetTranslate.Y);

        public void SetTrans(double x, double y)
        {
            PetTranslate.X = x;
            PetTranslate.Y = y;
        }
        public void SetSquishScale(double x, double y)
        {
            SquishScale.ScaleX = x;
            SquishScale.ScaleY = y;
        }

        public void SetDirectionScale(double x, double y)
        {
            DirectionScale.ScaleX = x;
            DirectionScale.ScaleY = y;
        }

        public void AddScale(double amountX, double amountY)
        {
            SquishScale.ScaleX += amountX;
            SquishScale.ScaleY += amountY;
        }

        public void SetSprite(string spriteName)
        {
            Console.WriteLine(spriteName);
            Uri uri = new Uri(spriteName, UriKind.RelativeOrAbsolute);
            Image.Source = new System.Windows.Media.Imaging.BitmapImage(uri);
        }
        
        ~PetWindow() => System.Diagnostics.Debug.WriteLine("PetWindow is clear");
    }
}