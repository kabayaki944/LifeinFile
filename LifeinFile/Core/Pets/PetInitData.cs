using System.Numerics;
using System.Windows;

namespace LifeinFile.Core.Pets
{
    class PetInitData
    {
        public string Name { get;}
        public Vector2 Position { get; }

        public PetInitData(string name, Vector2 position)
        {
            Name = name;
            Position = position;
        }
        public PetInitData(string name)
        {
            Name = name;
            float centerX = (float)SystemParameters.WorkArea.Width / 2;
            float centerY = (float)SystemParameters.WorkArea.Height / 2;
            Position = new Vector2(centerX, centerY);
        }
    }
}
