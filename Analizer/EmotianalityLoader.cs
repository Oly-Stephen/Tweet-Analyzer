using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizer
{
    class EmotianalityLoader : ILoader<Dictionary<char, Dictionary<string, double>>>
    {
        public Dictionary<char, Dictionary<string, double>> emotianality { get; set; }

        public void Load(IReader reader, IParser<Dictionary<char, Dictionary<string, double>>> parser, string path)
        {
            string[] lines = reader.ReadLines(path);
            emotianality = parser.Parse(lines);
        }
    }
}
