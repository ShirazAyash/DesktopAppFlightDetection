using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_Alg
{
    
    public class Dll
    {
        private View v;
        private Model d_model;
        private ViewModel d_vm;
        public View Create()
        {
            d_model = new Model();
            d_vm = new ViewModel(d_model);
            v = new View();
            v.D_Vm = d_vm;
            
            return v;
        }
        public Dictionary<int, Tuple<float, float>> Update(List<float> learn1, List<float> learn2, List<float> test1, List<float> test2)
        {
           return d_model.Start(learn1, learn2, test1, test2);

        }
    }
}
