using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Analizer
{
    class EmotianalityParser : IParser<Dictionary<char, Dictionary<string, double>>>
    {
        public Dictionary<char, Dictionary<string, double>> Parse(string[] sentiments)
        {
            Dictionary<char, Dictionary<string, double>> alphabet = new Dictionary<char, Dictionary<string, double>>();

            char previous = sentiments[0][0];

            Dictionary<string, double> newLeter = new Dictionary<string, double>();

            for (int i = 0; i < sentiments.Length; i++)
            {
                string[] buffer = Regex.Split(sentiments[i], ",", RegexOptions.IgnoreCase);

                if (previous == buffer[0][0])
                {
                    newLeter.Add(buffer[0], Convert.ToDouble(buffer[1].Replace('.', ',')));
                }
                else
                {

                    alphabet.Add(previous, newLeter);
                    newLeter = new Dictionary<string, double>();
                    previous = buffer[0][0];
                }
            }

            return alphabet;
        }
    }
}
