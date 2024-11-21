using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dancer.Framework
{
    public class Pattern
    {
        public string name;
        public List<Sample> samples;

        public Pattern(string name, List<Sample> samples)
        {
            this.name = name;
            this.samples = samples;
        }
    }
}
