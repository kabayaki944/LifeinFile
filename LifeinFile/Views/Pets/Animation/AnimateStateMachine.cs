using LifeinFile.Helper;
using LifeinFile.Views.Pets.Animations;
using Reactive.Bindings.Disposables;
using Reactive.Bindings.Extensions;

namespace LifeinFile.Views.Pets
{
    public class AnimateStateMachine
    {
        PetAnimationBase _currentPetAnimation; 
        CompositeDisposable _animationDisposables = new CompositeDisposable();
        public void SetAnimation(PetAnimationBase petAnimation)
        {
            if(petAnimation == _currentPetAnimation) return;
            
            _animationDisposables.Clear();
            UpdateContext();
            
            _currentPetAnimation = petAnimation;
            _currentPetAnimation.OnEnter(_context);
        }

        AnimationContext _context;
        CompositeDisposable _userDisposables;
        public AnimateStateMachine(CompositeDisposable userDisposables, PetAnimationBase petAnimation, AnimationContext animationContext)
        {
            _userDisposables = userDisposables;
            _context = animationContext;
            
            ProvideUpdate.LateUpdateAsObservable
                .Subscribe(dt => _currentPetAnimation?.OnUpdate(dt))
                .AddTo(userDisposables);
            SetAnimation(petAnimation);
        }

        void UpdateContext()
        {
            CompositeDisposable disposables = new CompositeDisposable()
                .AddTo(_userDisposables)
                .AddTo(_animationDisposables);
        }
    }
}