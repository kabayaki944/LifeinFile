using LifeinFile.Core.Cage;
using LifeinFile.Windows;
using System.Runtime.CompilerServices;

namespace LifeinFile.Core.Facade
{
    public static class CageManager
    {
        static List<CageExternal> _cages = new List<CageExternal>();
        public static IReadOnlyList<CageExternal> Cages => _cages;

        public static void CreateCage(CageInitData data)
        {
            var cage = CageFactory.Create(data);
            _cages.Add(cage);
        }
        
        public static void RemoveCage(CageExternal cage)
        {
            _cages.Remove(cage);
        }
    }
}