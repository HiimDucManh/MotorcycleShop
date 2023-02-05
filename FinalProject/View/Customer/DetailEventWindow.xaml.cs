using FinalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FinalProject.View.Customer
{
    /// <summary>
    /// Interaction logic for DetailEventWindow.xaml
    /// </summary>
    public partial class DetailEventWindow : Window
    {
        public DetailEventWindow()
        {
            InitializeComponent();
            datagridKM.ItemsSource = DataProvider.Ins.DB.KHUYENMAIs.Where(x => x.NGAYBATDAU <= DateTime.Now && x.NGAYKETTHUC >= DateTime.Now).ToList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void datagridKM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datagridKM.SelectedItem != null)
            {
                stpnModel.Visibility= Visibility.Visible;
                KHUYENMAI km = (KHUYENMAI)datagridKM.SelectedItem;

                ListViewModels.ItemsSource = DataProvider.Ins.DB.CHITIETKHUYENMAIs.Where(x => x.MAKM == km.MAKM && x.TRANGTHAI == true).ToList();

            }
        }
    }
}
