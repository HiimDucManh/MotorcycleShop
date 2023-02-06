using FinalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for CustomerDetail.xaml
    /// </summary>
    public partial class CustomerDetail : Window
    {
        KHACHHANG khachHang;
        public CustomerDetail(KHACHHANG kh)
        {
            InitializeComponent();
            khachHang = kh;
        }

        private void shutdownBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            Stream StreamObj = new MemoryStream(khachHang.IMG);
            BitmapImage BitObj = new BitmapImage();
            BitObj.BeginInit();
            BitObj.StreamSource = StreamObj;
            BitObj.EndInit();
            avatar.ImageSource = BitObj;

            firstName.Text = khachHang.HOKH;
            lastName.Text = khachHang.TENKH;
            birth.Text = khachHang.NGAYSINH.ToString();
            gender.Text = khachHang.GIOITINH;

            address.Text = khachHang.DIACHI;
            email.Text = khachHang.EMAIL;
            phone.Text = khachHang.SDT;

            registrationDate.Text = khachHang.NGAYDANGKY.Value.Date.ToString();
        }
    }
}
