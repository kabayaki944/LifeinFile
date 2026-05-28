using LifeinFile.Core.Pets;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeinFile.Models.Cages
{
    public class CageModel: IExternalModel
    {
        public string Name { get; set; }
        public List<PetExternal> Pets { get; } = new List<PetExternal>();

        public CageModel(string name)
        {
            Name = name;
        }

        //---ExternalModel---//
        IReadOnlyList<PetExternal> IExternalModel.Pets => Pets.AsReadOnly();
        public void AddPet(PetExternal pet)=>  Pets.Add(pet);
        public void RemovePet(PetExternal pet) => Pets.Remove(pet);

    }
}
