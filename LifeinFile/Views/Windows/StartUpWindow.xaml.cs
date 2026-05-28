using LifeinFile.Core.Cage;
using LifeinFile.Core.Pets;
using System.Windows;


namespace Views.Windows
{
    public partial class StartUpWindow : Window
    {
        public StartUpWindow()
        {
            InitializeComponent();

            PetInitData initData = new PetInitData("Test", new System.Numerics.Vector2(100, 100));
            PetFactory.Create(initData);
            CageInitData cageInitData1 = new CageInitData("Cage1");
            CageInitData cageInitData2 = new CageInitData("Cage2");
            CageFactory.Create(cageInitData1);
            CageFactory.Create(cageInitData2);

            Close();
        }
    }
}
