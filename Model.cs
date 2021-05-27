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

        private List<Point> anomaly;
        public List<Point> Anomaly
        {
            get
            {
                return anomaly;
            }
            set
            {
                anomaly = value;
                NotifyPropertyChanged("");

            }
        }
        public Model()
        {
            this.threshold = 0;
            this.size = 0;
        }
        private ScatterSeries anomaly_scatter;
        public void detect(List<float> test1, List<float> test2)
        {
            List<Point> temp_point=new List<Point>();
            anomaly_scatter = new ScatterSeries();
            anomaly_scatter.MarkerFill = OxyColors.Red;
            for(int i = 0; i < size; i++)
            {
                if(Math.Abs(test2[i] - line.f(test1[i])) > threshold)
                {
                    anomaly_scatter.Points.Add(new ScatterPoint(test1[i], test2[i], 4, 0));
                    temp_point.Add(new Point(test1[i], test2[i]));
                }
            }
            anomaly = temp_point;
        }
        private ScatterSeries learn_scatter;
        private LineSeries line_dllGraph = new LineSeries();
        
        
        public void learnormal(List<float> learn1, List<float> learn2)
        {
            learn_scatter = new ScatterSeries();
            line_dllGraph = new LineSeries();
            learn_scatter.MarkerFill = OxyColors.Gray;
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

            float maxx = 0;
            for (int i = 0; i < size; i++)
            {
                float d = Math.Abs(points[i].getY() - line.f(points[i].getX()));
                if (d > maxx)
                    maxx = d;
            }
            threshold = maxx*(float)1.1;
        }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
        public void Start(List<float> learn1, List<float> learn2, List<float> test1, List<float> test2)
        {
            this.size = learn1.Count;
            learnormal(learn1, learn2);
            detect(test1, test2);
            CreateGraph();
        }
        
        public void CreateGraph()
        {

            PlotModel pm = new PlotModel();

            pm.Series.Add(line_dllGraph);
            pm.Series.Add(learn_scatter);
            pm.Series.Add(anomaly_scatter);

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
