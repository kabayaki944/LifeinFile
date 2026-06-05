using LifeinFile.Core.Cage;
using LifeinFile.Core.Pets;
using LifeinFile.Windows;
using System.Runtime.CompilerServices;

namespace LifeinFile.Core.Facade
{
    public static class PetManager
    {
        static List<PetExternal> _pets = new List<PetExternal>();
        public static IReadOnlyList<PetExternal> Pets => _pets;

        public static void CreatePet(PetInitData data)
        {
            var cage = PetFactory.Create(data);
            _pets.Add(cage);
        }
        
        public static void RemovePet(PetExternal pet)
        {
            _pets.Remove(pet);
        }
    }
}