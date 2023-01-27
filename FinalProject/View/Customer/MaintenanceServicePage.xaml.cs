using FinalProject.Model;
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

namespace FinalProject.View.Customer
{
    public partial class MaintenanceServicePage : Page
    {
        KHACHHANG khachhang;
        public MaintenanceServicePage(KHACHHANG kh)
        {
            InitializeComponent();
            khachhang = kh;
            tbName.Text = khachhang.HOKH + " " + khachhang.TENKH;
            tbPhone.Text = khachhang.SDT;
            tbEmail.Text = khachhang.EMAIL;
            tbAddress.Text = khachhang.DIACHI;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
