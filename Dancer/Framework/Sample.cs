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

        public Sample(string filePath, string title, int id, int length)
        {
            loadedPoints = new bool[length + 1];

            for (int i = 0; i < loadedPoints.Length; i++)
            {
                loadedPoints[i] = false;
            }

            this.filePath = filePath;
            this.title = title;
            this.id = id;
        }
    }
}
