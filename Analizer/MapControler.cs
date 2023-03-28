using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;

using System.Xml.Serialization;
using System.Xml;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;


namespace Analizer
{
    class MapControler
    {
        GMapControl Map { get; set; }
        states states { get; set; }
        List<GMapPolygon> polygons { get; set; }
        Dictionary<string, GMapPolygon> xmlPolygons { get; set; }
        string[] colors { get; set; }

        public MapControler(GMapControl map)
        {
            this.Map = map;
        }

        public void LoadMap()
        {
            Map.Bearing = 0;

            Map.CanDragMap = true;

            Map.DragButton = MouseButtons.Left;

            Map.GrayScaleMode = true;

            Map.MaxZoom = 6;

            Map.MinZoom = 3;

            Map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            
            Map.NegativeMode = false;

            Map.PolygonsEnabled = true;
            
            Map.ShowTileGridLines = false;

            Map.Zoom = 3;
            
            Map.MapProvider =
                GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode =
                GMap.NET.AccessMode.ServerOnly;

            //Если вы используете интернет через прокси сервер,
            //указываем свои учетные данные.
            GMap.NET.MapProviders.GMapProvider.WebProxy =
                System.Net.WebRequest.GetSystemWebProxy();
            GMap.NET.MapProviders.GMapProvider.WebProxy.Credentials =
                System.Net.CredentialCache.DefaultCredentials;

            Map.Position = new PointLatLng(35.0041, -88.1955);
        }

        public void GetColors()
        {
            colors = File.ReadAllLines(@"C:\Codes\C-sharp\Analizer\Analizer\Analizer\data\colors.txt");
        }

        public void DrowPolygons(Dictionary<string,List<PointLatLng>> states_point)
        {
            polygons = new List<GMapPolygon>();

            foreach(var state in states_point)
            {
                polygons.Add(MakePolygon(state.Value, state.Key));
            }
            GMapOverlay polyOverlay = new GMapOverlay("mypolygon");

            foreach(var poly in polygons)
            {
                polyOverlay.Polygons.Add(poly);
            }

            Map.Overlays.Add(polyOverlay);
        }

        public Dictionary<string,List<PointLatLng>> PointsToList()
        {
            Dictionary<string, List<PointLatLng>> dict = new Dictionary<string, List<PointLatLng>>();
            List<PointLatLng> c;
            int i = 0;

            foreach(var a in states.state)
            {
                c = new List<PointLatLng>();
                foreach(var d in a.point)
                {
                    c.Add(new PointLatLng((double)d.lat,(double)d.lng));
                }

                dict.Add(colors.ElementAt(i),c);
                i++;
            }

            return dict;
        }

        public void GetStatesPoint()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(states));
            XmlReader reader = XmlReader.Create(@"states.xml");
            this.states = (states)serializer.Deserialize(reader);
        }

        public GMapPolygon MakePolygon(List<PointLatLng> points, string hex)
        {
            GMapPolygon polygon = new GMapPolygon(points, "polygon");
            Color col = ColorTranslator.FromHtml(hex);
            polygon.Fill = new SolidBrush(Color.FromArgb(200, col));
            polygon.Stroke = new Pen(col, 1);

            return polygon;
        }

        public GMapPolygon MakePolygon(List<PointLatLng> points)
        {
            GMapPolygon polygon = new GMapPolygon(points, "polygon");
            

            return polygon;
        }


        public Dictionary<string, GMapPolygon> GetStatesPolygons()
        {
            Dictionary<string, GMapPolygon> sPolygon = new Dictionary<string, GMapPolygon>();
            List<PointLatLng> c;
            foreach(var a in states.state)
            {
                c = new List<PointLatLng>();
                foreach(var d in a.point)
                {
                    c.Add(new PointLatLng((double)d.lat, (double)d.lng));
                }

                sPolygon.Add(a.name, MakePolygon(c));
            }

            return sPolygon;
        }

        public IEnumerable<Tweet> AddStateToTweet(IEnumerable<Tweet> tweets)
        {
            xmlPolygons = GetStatesPolygons();

            return tweets.Select(a => CheckTweet(a));
        }

        public Tweet CheckTweet(Tweet tweet)
        {

            foreach(var a in xmlPolygons)
            {
                if(a.Value.IsInside(tweet.Point))
                {
                    tweet.State = a.Key;
                    break;
                }
            }
            return tweet;
        }
        List<StateEmotion> emotionalStates;
        Dictionary<string, IEnumerable<Tweet>> stateTweet = new Dictionary<string, IEnumerable<Tweet>>();
        public List<StateEmotion> stateEmotionality(IEnumerable<Tweet> tweets)
        {
            emotionalStates = new List<StateEmotion>();
          //  emotionalStates.Add(new StateEmotion("", 0.0));
            foreach(string nameState in xmlPolygons.Keys)
            {
                stateTweet.Add(nameState, tweets.Where(a => a.State == nameState));
                emotionalStates.Add(new StateEmotion(nameState, 0.0));
            }

            //var g  = emotionalStates.Select(a => a.Emotionality += stateTweet["Texas"].Sum(b => b.Emotianality));

            var g = tweets.Select(a => a = SelectPolygon(a, emotionalStates.Single(b => b.State == a.State)));

            MessageBox.Show(emotionalStates.ElementAt(0).State + emotionalStates.ElementAt(0).Emotionality.ToString());


            return emotionalStates;
        }

       public Tweet SelectPolygon(Tweet tweet, StateEmotion em)
        {
            if (tweet.State != "" && em.State == tweet.State)
            {
                em.Emotionality += tweet.Emotianality;
            }

            
            return tweet;
        }
    }
}