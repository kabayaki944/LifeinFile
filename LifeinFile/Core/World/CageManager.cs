using LifeinFile.Core.Cage;
using LifeinFile.Windows;
using System.Runtime.CompilerServices;

namespace LifeinFile.Core.Facade
{
    public static class CageManager
    {
        static CageFactory _cageFactory = new CageFactory();
        static List<CageExternal> _cages = new List<CageExternal>();
        public static IReadOnlyList<CageExternal> Cages => _cages;

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