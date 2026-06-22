using LifeinFile.Views.Pets;
using System.Numerics;
using System.Windows;

namespace LifeinFile.Core.Pets
{
    public class PetInitData
    {
        public string Name { get;}
        public Vector2 Position { get; }
        public PetSprites Sprites { get; }

        public PetInitData(string name, Vector2 position, PetSprites sprites)
        {
            Name = name;
            Position = position;
            Sprites = sprites;
        }
        public PetInitData(string name, PetSprites sprites)
        {
            Name = name;
            float centerX = (float)SystemParameters.WorkArea.Width / 2;
            float centerY = (float)SystemParameters.WorkArea.Height / 2;
            Position = new Vector2(centerX, centerY);
            Sprites = sprites;
        }
    }
}
