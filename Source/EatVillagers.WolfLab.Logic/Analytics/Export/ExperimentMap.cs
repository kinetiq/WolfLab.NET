using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Z.Core.Extensions;

namespace EatVillagers.WolfLab.Logic.Analytics.Export
{
    public sealed class ExperimentMap : CsvClassMap<Experiment>
    {
        public ExperimentMap()
        {
            Map(x => x.Name);
            Map(x => x.Village);
            Map(x => x.Wolves);
            Map(x => x.GoodWins);
            Map(x => x.EvilWins);
            Map(x => x.GoodWinPercentage);
        }     
    }
}
