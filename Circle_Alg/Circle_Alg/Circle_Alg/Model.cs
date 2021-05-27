using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Circle_Alg
{
    public class Model : IModel
    {
        private float threshold;
        private float Xmin;
        private float Xmax;
        private float Ymin;
        private float Ymax;
        private float X_ymin;
        private float X_ymax;
        private float Y_xmin;
        private float Y_xmax;
        private Circle Min_circle;
        private Point Center;


        private int size;
        public event PropertyChangedEventHandler PropertyChanged;
        //IModel idll;

        public void NotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        public Model()
        {
            this.threshold = 0;
            this.size = 0;
        }
        private ScatterSeries anomaly_scatter;
        public Dictionary<int, Tuple<float, float>> detect(List<float> test1, List<float> test2)
        {
            Dictionary<int, Tuple<float, float>> anomalys = new Dictionary<int, Tuple<float, float>>();
            anomaly_scatter = new ScatterSeries();
            anomaly_scatter.MarkerFill = OxyColors.Red;
            for (int i = 0; i < size; i++)
            {
                if (MinCircle.Dist(this.Center, new Point(test1[i], test2[i])) > threshold)
                {
                    anomalys.Add(i, new Tuple<float, float>(test1[i], test2[i]));
                    anomaly_scatter.Points.Add(new ScatterPoint(test1[i], test2[i], 4, 0));
                }
            }
            return anomalys;

        }
        private ScatterSeries learn_scatter;
        private LineSeries line_dllGraph = new LineSeries();


        public void learnormal(List<float> learn1, List<float> learn2)
        {
            learn_scatter = new ScatterSeries();
            line_dllGraph = new LineSeries();
            learn_scatter.MarkerFill = OxyColor.FromAColor(120, OxyColors.LightGray);
            learn_scatter.MarkerType = MarkerType.Circle;
            Point[] points = new Point[size];
            Xmin = learn1[0];
            Xmax = learn1[0];
            Ymin = learn1[0];
            Ymax = learn1[0];
            X_ymin = learn2[0];
            X_ymax = learn2[0];
            Y_xmin = learn2[0];
            Y_xmax = learn2[0];
            for (int i = 0; i < size; i++)
            {
                learn_scatter.Points.Add(new ScatterPoint(learn1[i], learn2[i], 2, 0));
                points[i] = new Point(learn1[i], learn2[i]);
                if (learn1[i] < Xmin)
                {
                    Xmin = learn1[i];
                    Y_xmin = learn2[i];
                }
                if (learn1[i] > Xmax)
                {
                    Xmax = learn1[i];
                    Y_xmax = learn2[i];
                }
                if (learn2[i] < Ymin)
                {
                    Ymin = learn2[i];
                    X_ymin = learn1[i];
                }
                if (learn2[i] > Ymax)
                {
                    Ymax = learn2[i];
                    X_ymax = learn1[i];
                }
            }
            this.Center = MinCircle.MinCenter(Xmin, Xmax, Ymin, Ymax);
            float Radius = (MinCircle.Dist(new Point(Xmin, Ymin), new Point(Xmax, Ymax))) / 2;
            this.threshold = (float)(Radius * 1.1);

            //line_dllGraph.Points.Add(new DataPoint(Center.getX(), Center.getY()));
            line_dllGraph.Points.AddRange(createCircle(Center.getX(), Center.getY(), Radius));
            //line_dllGraph.MarkerSize = Radius;
            line_dllGraph.MarkerType = MarkerType.Circle;
            line_dllGraph.MarkerSize = 1;
            line_dllGraph.MarkerFill = OxyColors.BlueViolet ;
        }

        public List<DataPoint> createCircle(double a, double b, double r)
        {

            List<DataPoint> list = new List<DataPoint>();

            for (double i = 0; i < Math.PI * 2; i += 0.001)
            {
                double x = a + (r * Math.Cos(i));
                double y = b + (r * Math.Sin(i));
                list.Add(new DataPoint(x, y));
            }

            return list;
        }

        public Dictionary<int, Tuple<float, float>> Start(List<float> learn1, List<float> learn2, List<float> test1, List<float> test2)
        {
            this.size = learn1.Count;
            learnormal(learn1, learn2);
            Dictionary<int, Tuple<float, float>> d = detect(test1, test2);
            CreateGraph();
            return d;
        }

        public void CreateGraph()
        {

            PlotModel pm = new PlotModel();

            pm.Series.Add(line_dllGraph);
            pm.Series.Add(learn_scatter);
            pm.Series.Add(anomaly_scatter);
            pm.Title = "MinCircle Anomalys";
            this.DllGraph = pm;
            this.DllGraph.InvalidatePlot(true);
        }

        private PlotModel dllGraph;
        public PlotModel DllGraph
        {
            get
            {
                return dllGraph;
            }
            set
            {
                dllGraph = value;

                //plotModelTwo.TitleColor = OxyColors.Turquoise;
                NotifyPropertyChanged("DllGraph");
            }
        }
    }
}
