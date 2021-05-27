using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_Alg
{
    public class Model : IModel
    {
        private float threshold;
        private Line line;
        private float min;
        private float max;


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
            Dictionary<int, Tuple<float, float>> anomalys = new Dictionary<int,Tuple<float,float>>();
            anomaly_scatter = new ScatterSeries();
            anomaly_scatter.MarkerType = MarkerType.Circle;
            anomaly_scatter.MarkerFill = OxyColor.FromAColor(140, OxyColors.Red);
            for(int i = 0; i < size; i++)
            {
                if(Math.Abs(test2[i] - line.f(test1[i])) > threshold)
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
            learn_scatter.MarkerFill = OxyColor.FromAColor(120, OxyColors.Gray);
            learn_scatter.MarkerType = MarkerType.Circle;
            Point[] points = new Point[size];
            min = learn1[0];
            max=learn1[0];
            for(int i = 0; i < size; i++)
            {
                learn_scatter.Points.Add(new ScatterPoint(learn1[i], learn2[i], 2, 0));
                points[i] = new Point(learn1[i], learn2[i]);
                if (learn1[i] < min)
                {
                    min = learn1[i];
                }
                if (learn1[i] > max)
                {
                    max = learn1[i];
                }
            }
            line = Pearson.linear_reg(points, size);
            line_dllGraph.Points.Add(new DataPoint(min, line.f(min)));
            line_dllGraph.Points.Add(new DataPoint(max, line.f(max)));
            line_dllGraph.Color = OxyColors.BlueViolet;

            float maxx = 0;
            for (int i = 0; i < size; i++)
            {
                float d = Math.Abs(points[i].getY() - line.f(points[i].getX()));
                if (d > maxx)
                    maxx = d;
            }
            threshold = maxx*(float)1.1;
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        public Dictionary<int, Tuple<float, float>> Start(List<float> learn1, List<float> learn2, List<float> test1, List<float> test2)
        {
            this.size = learn1.Count;
            learnormal(learn1, learn2);
            Dictionary<int, Tuple<float, float>>  d =detect(test1, test2);
            CreateGraph();
            return d;
        }
        
        public void CreateGraph()
        {

            PlotModel pm = new PlotModel();

            pm.Series.Add(line_dllGraph);
            pm.Series.Add(learn_scatter);
            pm.Series.Add(anomaly_scatter);
            pm.Title = "LinearReg Anomaly";
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
