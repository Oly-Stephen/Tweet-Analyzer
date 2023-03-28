using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Analizer
{
    class EmotianalityReader : IReader
    {
        public string[] ReadLines (string filePath)
        {
            string[] sentiments;

            sentiments = File.ReadAllLines(filePath);

            return sentiments;
        }
    }
}
