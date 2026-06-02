using LifeinFile.Helper;
using LifeinFile.Views.Pets.Animations;
using LifeinFile.Windows;

namespace LifeinFile.Views.Pets
{
    public class TurnPetAnimation: PetAnimationBase
    {
        public TurnPetAnimation(PetWindow window) : base(window)
        {
        }

        protected override void UpdateTargets(double[] targets, double phase)
        {
            throw new NotImplementedException();
        }

        protected override void OnStart(AnimationContext context)
        {
            throw new NotImplementedException();
        }
    }
}