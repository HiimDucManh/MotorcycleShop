using FinalProject.View.Staffs.Pages;
using LiveCharts;
using LiveCharts.Wpf;
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
using System.Windows.Shapes;

namespace FinalProject.View.Staffs
{
    /// <summary>
    /// Interaction logic for StaffDashboard.xaml
    /// </summary>
    public partial class StaffDashboard : Window
    {
        public StaffDashboard()
        {
            InitializeComponent();
            staffPageNavigate.Navigate(new Dashboard());
        }

        //Cartesian
        //public void Cartesian()
        //{
        //    SeriesCollection = new SeriesCollection
        //    {
        //        new LineSeries
        //        {
        //            Values=new ChartValues<double>{ 50, 120, 110, 160, 150, 180, 120 }
        //        },
        //    };
        //    DataContext = this;
        //}
        //public SeriesCollection SeriesCollection { get; set; }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void shutdownBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void customerBtn_Click(object sender, RoutedEventArgs e)
        {
            staffPageNavigate.Navigate(new Staff_CustomerPage());
        }

        private void dashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            staffPageNavigate.Navigate(new Dashboard());
        }
    }
}
