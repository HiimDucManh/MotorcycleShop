using FinalProject.Model;
using FinalProject.View.Customer;
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
        NHANVIEN nhanVien;
        public StaffDashboard(NHANVIEN nv)
        {
            InitializeComponent();
            staffPageNavigate.Navigate(new Dashboard(nv));
            nhanVien = nv;
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
            staffPageNavigate.Navigate(new Staff_CustomerPage(nhanVien));
        }

        private void dashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            staffPageNavigate.Navigate(new Dashboard(nhanVien));
        }

        private void productsBtn_Click(object sender, RoutedEventArgs e)
        {
            staffPageNavigate.Navigate(new Staff_Products(nhanVien));
        }

        private void maintenanceBtn_Click(object sender, RoutedEventArgs e)
        {
            staffPageNavigate.Navigate(new Staff_Maintenance(nhanVien));
        }

        private void website_Click(object sender, RoutedEventArgs e)
        {
            KHACHHANG khachhang = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.TAIKHOANKH == "vuducmanh").First();
            CustomerWindow wd = new CustomerWindow(khachhang);
            wd.Show();
            this.Close();
        }

        private void ordersBtn_Click(object sender, RoutedEventArgs e)
        {
            staffPageNavigate.Navigate(new Staff_Orders(nhanVien));
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
            this.Close();
        }
    }
}
