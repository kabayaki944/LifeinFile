using LifeinFile.System.Pets;
using System.Windows;
using System.Windows.Input;


namespace Views.Windows
{
    public partial class StartUpWindow : Window
    {
        public StartUpWindow()
        {
            InitializeComponent();

            PetInitData initData = new PetInitData("Test", new System.Numerics.Vector2(100, 100));
            PetFactory.Create(initData);

            Close();
        }
    }
}
