using System;
using System.Collections.Generic;

namespace Circle_Alg
{
    public class Dll
    {
        private UserControl1 v;
        private Model d_model;
        private ViewModel d_vm;
        public UserControl1 Create()
        {
            d_model = new Model();
            d_vm = new ViewModel(d_model);
            v = new UserControl1();
            v.D_Vm = d_vm;

            return v;
        }
        public Dictionary<int, Tuple<float, float>> Update(List<float> learn1, List<float> learn2, List<float> test1, List<float> test2)
        {
            return d_model.Start(learn1, learn2, test1, test2);

        }
    }
}
