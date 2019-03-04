
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Forms.DataVisualization;
using System.IO;
using System.Threading;

namespace NavigationDrawerPopUpMenu2
{
    
    public partial class MainWindow : Window
    {

        public static string filename;
        public static List<Base> Bases;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();
            try {
                switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
                {

                    case "Download":
                        OpenFileDialog dlg = new OpenFileDialog();
                        dlg.DefaultExt = ".csv";
                        dlg.Filter = "CSV Files (*.csv)|*.csv";
                        if (dlg.ShowDialog() == true)
                        {
                            filename = dlg.FileName;
                        }
                        break;
                    case "DrawGraph":
                        usc = new UserControlChart();
                        GridMain.Children.Add(usc);
                        break;
                    case "FindPatterns":
                        usc = new UserControlHome();
                        GridMain.Children.Add(usc);
                        break;
                    case "ShowLikeTable":
                        if (filename != null)
                        {
                            usc = new UserControlCreate();
                            GridMain.Children.Add(usc);
                        }
                        break;
                    case "Statistic":


                        usc = new UserControlStat();
                        GridMain.Children.Add(usc);

                        break;

                    default:
                        break;
                }
            }
            catch
            {
                
            }

          }
        private void FindPatterns_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Information_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
