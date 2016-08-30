using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatVillagers.WolfLab.Logic.Factories
{
    public class NextList<T> : List<T>
    {
        private int NextIndex = 0;

        public NextList(IEnumerable<T> list) : base(list)
        {
            
        }

        public T GetNext()
        {
            var result =this[NextIndex]; 
            NextIndex++;

            return result;
        }
    }
}
