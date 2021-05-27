using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Line_Alg
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        private ViewModel d_vm;
        public ViewModel D_Vm
        {
            set
            {
                d_vm = value;
                DataContext = value;

            }
        }
        public View()
        {
            InitializeComponent();
            //this.d_vm = vm;
            //DataContext = vm;

        }

    }
}
