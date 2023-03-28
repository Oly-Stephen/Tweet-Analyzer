using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;

namespace Analizer
{
    class Tweet
    {
        public PointLatLng Point { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string[] Words { get; set; }
        public double Emotianality { get; set; }

        public string State { get; set; }

        public Tweet() { }

        public Tweet(PointLatLng point, DateTime date, string message, string[] words)
        {
            this.Point = point;
            this.Date = date;
            this.Message = message;
            this.Words = words;
        }
    }
}
