using LifeinFile.Core.Cage;
using LifeinFile.Windows;
using System.Runtime.CompilerServices;

namespace LifeinFile.Core.Facade
{
    public static class CageManager
    {
        static CageFactory _cageFactory = new CageFactory();

        static CageExternal _desktopCage;
        static List<CageExternal> _cages = new List<CageExternal>();
        
        public static CageExternal DesktopCage => _desktopCage;
        public static IReadOnlyList<CageExternal> Cages => _cages;
        
        public static void ChangeDesktopCage(CageExternal cage) => _desktopCage = cage; 

        public static CageExternal CreateDesktopCage()
        {
            CageInitData data = new CageInitData("DesktopCage", false);
            var cage = _cageFactory.Create(data);
            _desktopCage = cage;
            return cage;
        }
        
        public static CageExternal CreateCage(CageInitData data)
        {
            var cage = _cageFactory.Create(data);
            _cages.Add(cage);
            return cage;
        }
        
        public static void RemoveCage(CageExternal cage)
        {
            _cages.Remove(cage);
            PetCageConnector.RemoveCage(cage);
        }
    }
}