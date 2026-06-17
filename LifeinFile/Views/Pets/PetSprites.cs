namespace LifeinFile.Views.Pets
{
    public class PetSprites
    {
        public int Id{get; set; }
        public string Name{get; set; }
        
        public string Normal{get; set; }
        public string Smile{get; set; }
        public string Trouble{get; set; }
        public string Dizzy{get; set; }
    }

    public enum SpriteType
    {
        Normal,
        Smile,
        Trouble,
        Dizzy
    }
}