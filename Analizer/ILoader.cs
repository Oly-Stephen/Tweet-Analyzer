using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizer
{
    interface ILoader<T>
    {
        void Load(IReader reader, IParser<T> parser, string path);
    }
}
