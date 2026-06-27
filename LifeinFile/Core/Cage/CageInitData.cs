using System;
using System.Collections.Generic;
using System.Text;

namespace LifeinFile.Core.Cage
{
    public class CageInitData
    {
        public string Name { get; }
        public bool IsShowOnCreate { get; }

        public CageInitData(string name , bool isShowOnCreate = true)
        {
            Name = name;
            IsShowOnCreate = isShowOnCreate;
        }
    }
}
