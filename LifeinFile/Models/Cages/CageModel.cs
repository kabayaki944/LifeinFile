using LifeinFile.Core.Cage;
using LifeinFile.Core.Facade;
using LifeinFile.Core.Pets;
using System;
using System.Collections.Generic;
using System.Text;

namespace LifeinFile.Models.Cages
{
    public class CageModel: IExternalModel
    {
        public string Name { get; set; }

        private CageExternal _external;
        public IReadOnlyList<PetExternal> Pets => PetCageConnecter.GetPetsInCage(_external);
        public CageModel(string name, CageExternal external)
        {
            Name = name;
            _external = external;
        }
    }
}
