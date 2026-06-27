using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using LifeinFile.Models.Cages;
using Reactive.Bindings.Extensions;
using System.Windows;
using System.Windows.Input;

namespace LifeinFile.Windows
{
    public partial class CageWindow : WindowBase
    {
        public CageExternal External { get; }
        public CageWindow(CageExternal external, CageModel model)
        {
            InitializeComponent();
            External = external;
            model.Name.Subscribe(newName => Title = newName)
                .AddTo(model.Disposables);
        }
        
        private void MenuItem_DebugSave_Click(object sender, RoutedEventArgs e) => SaveManager.TrySave(External);
    }
}