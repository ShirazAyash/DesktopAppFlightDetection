using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Circle_Alg
{
    public interface IModel : INotifyPropertyChanged
    {
        Dictionary<int, Tuple<float, float>> Start(List<float> learn1, List<float> learn2, List<float> test1, List<float> test2);
        void learnormal(List<float> learn1, List<float> learn2);
        Dictionary<int, Tuple<float, float>> detect(List<float> test1, List<float> test2);
        void CreateGraph();

        PlotModel DllGraph { get; set; }

    }
}
