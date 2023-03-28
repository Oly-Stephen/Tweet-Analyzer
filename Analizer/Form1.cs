using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analizer;



namespace Analizer
{
    public partial class Form1 : Form
    {
        static string tweetsPath = @"all_tweets.txt";
        static string emotianalityPath = @"sentiments.csv";
        IReader tweetsReader = new TweetsReader();
        IParser<IEnumerable<Tweet>> tweetsParser = new TweetsParser();
        TweetsLoader tweetsLoader = new TweetsLoader();

        IReader emotianalityReader = new EmotianalityReader();
        IParser<Dictionary<char, Dictionary<string, double>>> emotianalityParser = new EmotianalityParser();
        EmotianalityLoader emotianalityLoader = new EmotianalityLoader();
        TweetsAnalyzer tweetsAnalyzer = new TweetsAnalyzer();
        IEnumerable<Tweet> completeTweets;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            MapControler map = new MapControler(gMapControl1);

            timer1.Start();

            /*Generating Regeons and Map*/
            Info.Text = "Generating points.";

            map.GetStatesPoint();

            Info.Text = "Geting colors.";

            map.GetColors();

            Info.Text = "Drowing polygons.";

            map.DrowPolygons(map.PointsToList());

            Info.Text = "Load map.";

            map.LoadMap();

            progressBar1.Increment(timer1.Interval);

            timer1.Stop();

            Info.Text = "Done.";

            progressBar1.Value = 0;
            timer1.Start();

            Parallel.Invoke(() =>
            {
                tweetsLoader.Load(tweetsReader, tweetsParser, tweetsPath);

            },
                            () =>
            {
                emotianalityLoader.Load(emotianalityReader, emotianalityParser, emotianalityPath);
            }
            );

            TweetsAnalyzer tweetsAnalyzer = new TweetsAnalyzer();
            tweetsAnalyzer.alphabet = emotianalityLoader.emotianality;
            completeTweets = tweetsAnalyzer.Analyze(tweetsLoader.tweets);
            
            progressBar1.Increment(timer1.Interval);
            timer1.Stop();

            completeTweets = map.AddStateToTweet(completeTweets);

          //  MessageBox.Show(completeTweets.Count().ToString());
            map.stateEmotionality(completeTweets);



            //for(int i = 0; i < 20; i++)
            //{
            //    MessageBox.Show(completeTweets.ElementAt(i).State);
            //}

        }
    }

}