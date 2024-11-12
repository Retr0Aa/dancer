using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dancer.Framework
{
    public class Sample
    {
        public string filePath;
        public string title;
        public int id;

        public bool[] loadedPoints;

        public Sample(string filePath, string title, int id)
        {
            loadedPoints = new bool[11] {
            false, false, false, false, false, false, false, false, false, false, false
        };
            this.filePath = filePath;
            this.title = title;
            this.id = id;
        }
    }
}
