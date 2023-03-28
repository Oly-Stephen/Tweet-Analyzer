using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizer
{
    interface IReader
    {
        string[] ReadLines(string path);
    }
}
