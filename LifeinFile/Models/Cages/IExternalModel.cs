using LifeinFile.Core.Pets;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeinFile.Models.Cages
{
    public interface IExternalModel
    {
        public string Name { get;}
        IReadOnlyList<PetExternal> Pets { get; }
        public void AddPet(PetExternal pet);
        public void RemovePet(PetExternal pet);
    }
}
