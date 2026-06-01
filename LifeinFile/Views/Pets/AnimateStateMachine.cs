using LifeinFile.Helper;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;

namespace LifeinFile.Views.Pets
{
    public class AnimateStateMachine
    {
        AnimationBase _currentAnimation; 
        CompositeDisposable _animationDisposables = new CompositeDisposable();
        public void SetAnimation(AnimationBase animation)
        {
            _animationDisposables.Clear();
            UpdateContext();
            
            _currentAnimation?.OnExit();
            _currentAnimation = animation;
            _currentAnimation.OnEnter(_context);
        }

        AnimationContext _context;
        CompositeDisposable _userDisposables;
        public AnimateStateMachine(CompositeDisposable userDisposables, AnimationBase animation, AnimationContext animationContext)
        {
            _userDisposables = userDisposables;
            _context = animationContext;
            
            ProvideUpdate.LateUpdateAsObservable
                .Subscribe(dt => _currentAnimation?.OnUpdate(dt))
                .AddTo(userDisposables);
            SetAnimation(animation);
        }

        void UpdateContext()
        {
            CompositeDisposable disposables = new CompositeDisposable()
                .AddTo(_userDisposables)
                .AddTo(_animationDisposables);
            
            _context.Disposables =  disposables;
        }
    }
}