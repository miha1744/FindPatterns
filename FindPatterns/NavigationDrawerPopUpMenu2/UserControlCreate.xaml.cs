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

namespace NavigationDrawerPopUpMenu2
{
   
    public partial class UserControlCreate : UserControl
    {
        public List<Base> Bases;
        public UserControlCreate()
        {
            
            InitializeComponent();
            Data_Show();
        }

        private void Data_Show()
        {

            Bases = Base.ParseCsv(MainWindow.filename);
            Datagrid1.ItemsSource = Bases;
        }
    }
}
