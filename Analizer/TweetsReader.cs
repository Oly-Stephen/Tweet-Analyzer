using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizer
{
    class TweetsReader : IReader
    {
        public string[] ReadLines(string filePath)
        {
            string[] allFile;

            allFile = File.ReadAllLines(filePath);

            return allFile;
        }
    }
}
