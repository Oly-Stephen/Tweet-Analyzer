using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Analizer
{
    class TweetsAnalyzer : IAnalyzer<IEnumerable<Tweet>>
    {
        public Dictionary<char, Dictionary<string, double>> alphabet {get; set;}
        
        public TweetsAnalyzer() { }
       
        public IEnumerable<Tweet> Analyze(IEnumerable<Tweet> tweets)
        {
            return tweets.Select(a => AnalyzeTweet(a));
        }

        public Tweet AnalyzeTweet(Tweet tweet)
        {
            for (int i = 0; i < tweet.Words.Length; i++)
            {
                if (tweet.Words[i] != "" && (alphabet.ContainsKey(tweet.Words[i][0])))
                {
                    tweet.Emotianality += (alphabet[tweet.Words[i][0]].SingleOrDefault(a => a.Key == tweet.Words[i])).Value;
                }
            }
           
            return tweet;
        }
    }
}