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
using System.Windows.Shapes;

namespace FinalProject.View
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : Window
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        private void btnMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            btnMinimize.Background = (Brush)bc.ConvertFrom("#7F8487");
            //btnMinimize.Foreground = Brushes.Black;
        }

        private void btnMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            btnMinimize.Background = Brushes.Transparent;
            btnMinimize.Foreground = Brushes.White;
        }

        private void btnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            btnClose.Background = Brushes.Red;
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.Background = Brushes.Transparent;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            var account = DataProvider.Ins.DB.NGUOIDUNGs.Where(x=>x.TAIKHOAN == userName.Text).FirstOrDefault();
            if (account == null)
            {
                if (againtPassword.Password != passwordBox.Password)
                {
                    notify.Text = "Password does not match";
                }
                else
                {
                    var newacc = new NGUOIDUNG() { TAIKHOAN = userName.Text, MATKHAU = passwordBox.Password, LOAIND = 1 };
                    DataProvider.Ins.DB.NGUOIDUNGs.Add(newacc);
                    string makh;
                    int v = DataProvider.Ins.DB.KHACHHANGs.Count();
                    if (v == 0)
                    {
                        makh = "KH1";
                    }
                    else
                    {
                        List<KHACHHANG> listKH = DataProvider.Ins.DB.KHACHHANGs.ToList();
                        KHACHHANG KH = listKH[v - 1];
                        string ma = KH.MAKH.Substring(2);
                        makh = "KH" + (Int32.Parse(ma) + 1);
                    }

                    
                    var kh = new KHACHHANG() { MAKH = makh, TAIKHOANKH = userName.Text, DOANHSO = 0, SOLUONGSANPHAM = 0 };
                    DataProvider.Ins.DB.KHACHHANGs.Add(kh);

                    DataProvider.Ins.DB.SaveChanges();
                    MessageBox.Show("Success!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoginView login = new LoginView();
                    login.Show();
                    this.Close();
                }
            }
            else notify.Text = "Username was already existed";
        }

        private void signInBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginView login = new LoginView();
            login.Show();
            this.Close();
        }

    }
}
