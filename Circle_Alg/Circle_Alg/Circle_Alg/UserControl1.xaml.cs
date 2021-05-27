using System.Windows.Controls;

namespace Circle_Alg
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
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
        public UserControl1()
        {
            InitializeComponent();
        }
    }
}
