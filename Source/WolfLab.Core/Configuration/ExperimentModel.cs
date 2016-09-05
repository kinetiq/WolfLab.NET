using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfLab.Core.Configuration
{
    public class ExperimentModel
    {
        public string Name { get; set; }
        public Dictionary<string, string> Variables { get; set; }

        public ExperimentModel()
        {
            Variables = new Dictionary<string, string>();
        }
    }
}
