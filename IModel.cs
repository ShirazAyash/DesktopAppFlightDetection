using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Line_Alg
{
    public interface IModel : INotifyPropertyChanged
    {
        void Start(List<float> learn1, List<float> learn2, List<float> test1, List<float> test2);
        void learnormal(List<float> learn1, List<float> learn2);
        void detect(List<float> test1, List<float> test2);
        void CreateGraph();

        PlotModel DllGraph { get; set; }

    }
}
