using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EatVillagers.WolfLab.Logic.GameLogic.Perceptions;
using EatVillagers.WolfLab.Logic.Models.Enums;

namespace EatVillagers.WolfLab.Logic.GameLogic
{
    public class Signal
    {
        public Polarities Polarity { get; set; } 
        public Levels Level { get; set; }
    }
}
