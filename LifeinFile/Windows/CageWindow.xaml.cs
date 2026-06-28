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
        CageModel _model;
        public CageWindow(CageExternal external, CageModel model)
        {
            InitializeComponent();
            External = external;
            _model = model;
            
            model.Name.Subscribe(newName => Title = newName)
                .AddTo(model.Disposables);
            model.State.Subscribe(newState =>
            {
                if(newState == CageState.Desktop) Hide();
                else Show();
            }).AddTo(model.Disposables);
        }
        
        
        public void EnableWindow(bool enable)
        {
            if(_model.State.Value == CageState.Desktop) return;
            if(enable) Show();
            else Hide();
        }

        public void ToggleEnableWindow()
        {
            if(_model.State.Value == CageState.Desktop) return;
            if(Visibility == Visibility.Visible) Hide();
            else Show();
        }
        
        private void MenuItem_DebugSave_Click(object sender, RoutedEventArgs e) => SaveManager.TrySave(External);
    }
}