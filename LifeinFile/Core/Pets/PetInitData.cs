using LifeinFile.Views.Pets;
using System.Numerics;
using System.Windows;

namespace LifeinFile.Core.Pets
{
    public class PetInitData
    {
        public string Name { get;}
        public Vector2 Position { get; }
        public int SpritesId { get; }
        public bool IsShowOnCreate { get; }

        public PetInitData(string name, Vector2 position, int spritesId)
        {
            Name = name;
            Position = position;
            SpritesId = spritesId;
        }
        public PetInitData(string name, int spritesId,  bool isShowOnCreate = true)
        {
            Name = name;
            float centerX = (float)SystemParameters.WorkArea.Width / 2;
            float centerY = (float)SystemParameters.WorkArea.Height / 2;
            Position = new Vector2(centerX, centerY);
            SpritesId = spritesId;
            IsShowOnCreate = isShowOnCreate;
        }
    }
}
