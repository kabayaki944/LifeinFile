using Path = System.IO.Path;

namespace LifeinFile.Helper
{
    public static class PathHelper
    {
        public static string ExeDir { get; }= System.AppDomain.CurrentDomain.BaseDirectory;
        public static string GamePath { get; } = Path.Combine(ExeDir, "Game");
        public static string DefaultCagesPath { get; } = Path.Combine(GamePath, "Cages");
        public static string SettingsPath { get; } = Path.Combine(GamePath,  "Settings");
        
        static string AssetPath { get; } = Path.Combine(GamePath, "Assets");
        static string GraphicPath { get; } = Path.Combine(AssetPath, "Graphic");

        public static string PetSpritesPath { get; } = Path.Combine(GraphicPath,  "Pets");


    }
}