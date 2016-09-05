using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfLab.Core.Configuration
{
    public class ProjectModel
    {
        public string Name { get; set; }
        public bool CrunchMode { get; set; }
        public int MinVillage { get; set; }
        public int MaxVillage { get; set; }
        public int SampleSize { get; set; }

        public List<ExperimentModel> Experiments { get; set; }

        public ProjectModel()
        {
            Experiments = new List<ExperimentModel>();
        }
    }
}
