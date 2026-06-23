using LifeinFile.Controller.PetSystem;
using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets;
using LifeinFile.Views.Pets.Animation.DirectionAnimation;
using System.Diagnostics;
using PetWindow = LifeinFile.Windows.PetWindow;

namespace LifeinFile.Core.Pets
{
    class PetFactory
    {
        bool _isFirst = true;

        public PetExternal Create(PetInitData initData)
        {
            if(_isFirst)
            {
                ProvideUpdate.Start();
                _isFirst = false;
            }

            PetExternal external = new PetExternal();
            
            PetModel model = new PetModel(initData.Name, initData.Position, initData.Sprites, external);
            
            PetWindow window = new PetWindow(model);
            window.Show();
            
            DragController dragController = new DragController(model,  window, window, window, external);

            LifeSystem lifeSystem = new LifeSystem(model, window, external);
            
            DropFileController filesController = new DropFileController(model, window);
            Interacted interacted = new Interacted(model, window);
            GaugeConsumer gaugeConsumer = new GaugeConsumer(model);
            
            PetMoveBrain moveBrain = new PetMoveBrain(model);
            
            GaugeView gauge = new GaugeView(model, window);
            
            Debug.Write(initData.Sprites);
            SpriteController spriteController = new SpriteController(model, window);
            
            PetAnimationsCreator creator = new PetAnimationsCreator(model, window);
            AnimationContext animationContext = new AnimationContext();
            
            AnimateStateMachine squishStateMachine =
                new AnimateStateMachine(model.Disposables, creator.Breath, animationContext);
            SquishAnimationController squishAnimationController = new SquishAnimationController(model, squishStateMachine, creator);
            
            AnimateStateMachine directionStateMachine =
                new AnimateStateMachine(model.Disposables, animationContext);
            DirectionAnimationController directionAnimationController = new DirectionAnimationController(model, directionStateMachine, creator);

            ShowDebugWindow showDebugWindow = new ShowDebugWindow(model, moveBrain, window);
            
            PetCollision collision = new PetCollision(model, window);
            PetScreenCollider screenCollider = new PetScreenCollider(model, window, collision);
            PetMoveDrive mover = new PetMoveDrive(model, window);
            
            external.Construct(model, collision, window, lifeSystem);

            return external;
        }
    }
}
