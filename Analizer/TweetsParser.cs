using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using GMap.NET;

namespace Analizer
{
    class TweetsParser : IParser<IEnumerable<Tweet>>
    {
        Regex clearTweet = new Regex(@"([@|#][0-9A-z]*)|(https?://[^ ]+)|([,|.|\)|\(|:|;|\!|\?]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public IEnumerable<Tweet> Parse(string[] allTwits)
        {
            Regex splitTweets = new Regex(@"\W*([-|+]?[0-9]*[.]?[0-9]*)\W*\s+([-|+]?[0-9]*[.]?[0-9]*)\W*.\W*(\d*\-\d*\-\d*.\d*\:\d*\:\d*)\s+(.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            var tweets = allTwits.Select(a => splitTweets.Split(a.Trim()));

            tweets = tweets.Where(a => a.Length != 1);

            var t = tweets.Select(c => new Tweet(new PointLatLng(Convert.ToDouble(c.ElementAtOrDefault(1).Replace('.', ',')), Convert.ToDouble(c.ElementAtOrDefault(2).Replace('.', ','))), Convert.ToDateTime(c.ElementAtOrDefault(3)), c.ElementAtOrDefault(4).ToLower(), intoWords(c.ElementAtOrDefault(4).ToLower())));


            return t;
        }

        public string[] intoWords(string tweet)
        {
            return clearTweet.Replace(tweet, "").Trim().Split(' ');
        }
    }
}