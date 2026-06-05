using LifeinFile.Controller.PetSystem;
using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets;
using LifeinFile.Views.Pets.Animation.DirectionAnimation;
using PetWindow = LifeinFile.Windows.PetWindow;

namespace LifeinFile.Core.Pets
{
    static class PetFactory
    {
        static bool _isFirst = true;

        public static PetExternal Create(PetInitData initData)
        {
            if(_isFirst)
            {
                ProvideUpdate.Start();
                _isFirst = false;
            }

            PetExternal external = new PetExternal();
            PetWindow window = new PetWindow();
            window.Show();

            PetModel model = new PetModel(initData.Name, initData.Position, external);

            DragController dragController = new DragController(model,  window, window, window, external);

            DropFileController filesController = new DropFileController(model, window);
            Interacted interacted = new Interacted(model, window);
            GaugeConsumer gaugeConsumer = new GaugeConsumer(model);
            
            PetMoveBrain moveBrain = new PetMoveBrain(model);
            
            GaugeView gauge = new GaugeView(model, window);
            
            PetAnimationsCreator creator = new PetAnimationsCreator(model, window);
            AnimationContext animationContext = new AnimationContext();
            
            AnimateStateMachine squishStateMachine =
                new AnimateStateMachine(model.Disposables, creator.Breath, animationContext);
            SquishAnimationController squishAnimationController = new SquishAnimationController(model, squishStateMachine, creator);
            
            AnimateStateMachine directionStateMachine =
                new AnimateStateMachine(model.Disposables, animationContext);
            DirectionAnimationController directionAnimationController = new DirectionAnimationController(model, directionStateMachine, creator);
            
            
            PetCollision collision = new PetCollision(model, window);
            PetScreenCollider screenCollider = new PetScreenCollider(model, window, collision);
            PetMoveDrive mover = new PetMoveDrive(model, window);
            
            external.Construct(model, collision, window);

            return external;
        }
    }
}
