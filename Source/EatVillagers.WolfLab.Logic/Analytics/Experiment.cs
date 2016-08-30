using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatVillagers.WolfLab.Logic.Analytics
{
    public class Experiment
    {
        public int GoodWins;
        public int EvilWins;
        public List<DataTags> Tags = new List<DataTags>();
        public long Duration;
    }
}
