using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace EatVillagers.WolfLab.Logic.Analytics
{
    public class Experiment
    {
        public string Name { get; set; }
        public int Villagers { get; set; }
        public int Wolves { get; set; }
        public int GoodWins { get; set; }
        public int EvilWins { get; set; }
        public List<DataTags> Tags = new List<DataTags>();
        public long Duration { get; set; }
    }
}
