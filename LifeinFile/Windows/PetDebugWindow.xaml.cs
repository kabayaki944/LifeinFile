using LifeinFile.Controller.PetSystem;
using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using Reactive.Bindings.Extensions;
using System.Numerics;
using System.Windows;
using Vector = System.Windows.Vector;

namespace LifeinFile.Windows
{
    public partial class PetDebugWindow : Window
    {
        public PetDebugWindow(PetModel model, PetMoveBrain moveBrain)
        {
            InitializeComponent();
            Subscribes(model, moveBrain);
        }

        void Subscribes(PetModel model, PetMoveBrain moveBrain)
        {
            model.Name.Subscribe(UpdateName).AddTo(model.Disposables);
            model.Position.Subscribe(UpdatePosition).AddTo(model.Disposables);
            model.Velocity.Subscribe(UpdateVelocity).AddTo(model.Disposables);
            model.Direction.Subscribe(UpdateDirection).AddTo(model.Disposables);
            model.State.Subscribe(UpdateState).AddTo(model.Disposables);
            model.CurrentHunger.Subscribe(hunger => UpdateHunger(hunger, model.MaxHunger)).AddTo(model.Disposables);
            model.CurrentCom.Subscribe(com =>  UpdateCommunication(com, model.MaxCom)).AddTo(model.Disposables);
            ProvideUpdate.LateUpdateAsObservable.Subscribe(_ => UpdateMoveStep(moveBrain.CurrentStep, moveBrain.ActionTimer)).AddTo(model.Disposables);
        }

        void UpdateId(int id) => TextId.Text = $"ID: {id}";
        void UpdateName(string name) => TextName.Text = $"Name: {name}";
        void UpdatePosition(Vector2 position) => TextPosition.Text = $"Position: {position}";
        void UpdateVelocity(Vector2 velocity) => TextVelocity.Text = $"Velocity: {velocity}";
        void UpdateDirection(Direction direction) => TextDirection.Text = $"Direction: {direction}";
        void UpdateState(PetState state) => TextState.Text = $"State: {state}";
        void UpdateHunger(double hunger, double maxHunger) => TextHunger.Text = $"Hunger: {hunger:F1}/{maxHunger}";
        void UpdateCommunication(double communication, double maxCommunication) => TextCommunication.Text = $"Communication: {communication:F1}/ {maxCommunication}";
        void UpdateMoveStep(MoveStep step, int timer) => TextMoveStep.Text =  $"MoveStep: direction({step.move}), duration({timer}/{step.duration})";
    }
}