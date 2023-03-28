using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizer
{
    class TweetsLoader : ILoader<IEnumerable<Tweet>>
    {
        public IEnumerable<Tweet> tweets { get; set; }

        public void Load(IReader reader, IParser<IEnumerable<Tweet>> parser, string path)
        {
            string[] lines = reader.ReadLines(path);
            tweets = parser.Parse(lines);
        }
    }
}
