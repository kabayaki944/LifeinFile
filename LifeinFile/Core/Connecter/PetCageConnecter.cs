using LifeinFile.Core.Cage;
using LifeinFile.Core.Pets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LifeinFile.Core.Facade
{
    public static class PetCageConnecter
    {
        static Dictionary<PetExternal, CageExternal> _petCageMap = new Dictionary<PetExternal, CageExternal>();

        public static void MovePetToCage(PetExternal pet, CageExternal cage)
        {
            Debug.WriteLine("Moving pet to cage: " + pet.Model.Name + " -> " + cage.Model.Name);
            _petCageMap[pet] = cage;
            pet.Window.Owner = cage.Window;
        }


        public static void MovePetToOut(PetExternal pet)
        { 
            Debug.WriteLine("Moving pet out of cage: " + pet.Model.Name);
            _petCageMap[pet] = null;
            pet.Window.Owner = null;
        }


        public static CageExternal GetCageOfPet(PetExternal pet)
        {
            if (_petCageMap.ContainsKey(pet))
            {
                return _petCageMap[pet];
            }
            return null;
        }
        public static IReadOnlyList<PetExternal> GetPetsInCage(CageExternal cage)
        {
            // LINQを使って「Valueが指定のcageと一致するPet(Key)」だけを抽出してリスト化します
            return _petCageMap.Where(kvp => kvp.Value == cage)
                              .Select(kvp => kvp.Key)
                              .ToList();
        }
    }
}
