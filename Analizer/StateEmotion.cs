using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizer
{
    class StateEmotion
    {
        public string State { get; set; }
        public double Emotionality { get; set; }

        public StateEmotion() { }

        public StateEmotion(string state, double emotionality)
        {
            this.State = state;
            this.Emotionality = Emotionality;
        }
    }
}
