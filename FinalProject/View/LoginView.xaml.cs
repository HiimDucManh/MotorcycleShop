using FinalProject.Model;
using FinalProject.View.Customer;
using FinalProject.View.Staffs;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace FinalProject.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            tblLogin.Text = string.Empty;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
            //Uri uri = new Uri("Images/CloseBtnRed.png", UriKind.Relative);
            //StreamResourceInfo streamInfo = Application.GetResourceStream(uri);

            //BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            //var brush = new ImageBrush();
            //brush.ImageSource = temp;

            btnClose.Background = Brushes.Red;
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            //Uri uri = new Uri("Images/CloseBtn.png", UriKind.Relative);
            //StreamResourceInfo streamInfo = Application.GetResourceStream(uri);

            //BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            //var brush = new ImageBrush();
            //brush.ImageSource = temp;

            btnClose.Background = Brushes.Transparent;
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            NGUOIDUNG nd = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TAIKHOAN == txbUsername.Text).FirstOrDefault();
            if (nd != null)
            {
                if(nd.MATKHAU == txbPass.Password && nd.LOAIND == 1)
                {
                    KHACHHANG khachhang = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.TAIKHOANKH == txbUsername.Text).First();
                    CustomerWindow wd = new CustomerWindow(khachhang);
                    wd.Show();
                    this.Close();
                }
                if (nd.MATKHAU == txbPass.Password && nd.LOAIND == 0)
                {
                    NHANVIEN nhanVien = DataProvider.Ins.DB.NHANVIENs.Where(x => x.TAIKHOANNV == txbUsername.Text).First();
                    StaffDashboard staff = new StaffDashboard(nhanVien);
                    staff.Show();
                    this.Close();
                }
                if (nd.MATKHAU == txbPass.Password && nd.LOAIND == 2)
                {
                    
                }
                else
                {                    
                    tblLogin.Text = "*Wrong password!";
                }
            }
            else
            {
                tblLogin.Text = "*Account does not exist!";
            }
        }

        private void signUpBtn_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            SignUpView signUpView = new SignUpView();
            signUpView.Show();
            this.Close();
        }
    }
}
