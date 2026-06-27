using LifeinFile.Core.Pets;
using LifeinFile.Models.Pets;
using LifeinFile.Windows;
using Reactive.Bindings.Extensions;

namespace LifeinFile.Views.Pets
{
    public class SpriteController
    {
        IPetWindow _window;
        PetModel _model;
        public SpriteController(PetModel model, PetWindow petWindow)
        {
            _model = model;
            _window = petWindow;
            
            model.SpritesId.Subscribe(_ =>ChangeToNormal())
                .AddTo(model.Disposables);
        }

        PetSprites Sprites
        {
            get
            {
                Console.WriteLine(_model.SpritesId.Value);
                PetSpritesDictionary.TryGet(_model.SpritesId.Value,out var sprites);
                return sprites;
            }
        }

        public void Change(SpriteType type)
        {
            string spritePath = Sprites.Normal;
            switch (type)
            {
                case SpriteType.Normal:
                    spritePath = Sprites.Normal;
                    break;
                case SpriteType.Smile:
                    spritePath = Sprites.Smile;
                    break;
                case SpriteType.Trouble:
                    spritePath = Sprites.Trouble;
                    break;
                case SpriteType.Dizzy:
                    spritePath = Sprites.Dizzy;
                    break;
            }
            _window.SetSprite(spritePath);
        }
        
        public void ChangeToNormal()
        {
            _window.SetSprite(Sprites.Normal);
        }
    }
}