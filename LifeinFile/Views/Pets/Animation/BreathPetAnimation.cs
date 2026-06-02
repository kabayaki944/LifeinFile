using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Views.Pets.Animations;
using LifeinFile.Windows;

namespace LifeinFile.Views.Pets
{
    public class BreathPetAnimation: PetAnimationBase
    {
        PetModel _model;
        IReadOnlyWindowModel _windowModel;
        public BreathPetAnimation(PetModel model, PetWindow window, IReadOnlyWindowModel windowModel) : base(window)
        {
            _model = model;
            _windowModel = windowModel;
        }

        protected override double Frequency { get; set; } = 2;
        const double SENSIBILITY = 0.03;
        
        protected override void OnStart(AnimationContext context) { }

        protected override void UpdateTargets(double[] targets, double phase)
        {
            var result = ScaleHelper.GetSlimeScale(phase, SENSIBILITY);
            
            targets[IDX_SCALE_X] = result.x;
            targets[IDX_SCALE_Y] = result.y;
        }
    }
}