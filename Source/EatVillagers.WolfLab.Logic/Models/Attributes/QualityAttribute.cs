using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.Models.Attributes
{
    public class QualityAttribute : Attribute
    {
        public string Name;
    
        public QualityAttribute(string name)
        {
            Name = name;
        }
    }
}
