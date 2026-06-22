using LifeinFile.Core.Cage;
using LifeinFile.Core.Pets;
using LifeinFile.Windows;
using System.Runtime.CompilerServices;

namespace LifeinFile.Core.Facade
{
    public static class PetManager
    {
        static PetFactory _petFactory = new PetFactory();
        static List<PetExternal> _pets = new List<PetExternal>();
        public static IReadOnlyList<PetExternal> Pets => _pets;

        public static PetExternal CreatePet(PetInitData data)
        {
            var pet = _petFactory.Create(data);
            _pets.Add(pet);
            PetCageConnector.MovePetToOut(pet);
            return pet;
        }
        
        public static void RemovePet(PetExternal pet)
        {
            _pets.Remove(pet);
            PetCageConnector.RemovePet(pet);
        }
    }
}