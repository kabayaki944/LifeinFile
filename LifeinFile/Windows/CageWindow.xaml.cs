using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Models.Cages;
using System.Windows;
using System.Windows.Input;

namespace LifeinFile.Windows
{
    public partial class CageWindow : WindowBase
    {
        public CageExternal External { get; }
        public CageWindow(CageExternal external)
        {
            InitializeComponent();
            External = external;
        }
    }
}