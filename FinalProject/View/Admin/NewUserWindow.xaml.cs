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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace FinalProject.View.Admin
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        NGUOIDUNG nGUOIDUNG;
        public NewUserWindow(NGUOIDUNG ng)
        {
            InitializeComponent();
            nGUOIDUNG = ng;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            username.Text = nGUOIDUNG.TAIKHOAN;
            password.Text = nGUOIDUNG.MATKHAU;
            userRole.Text = nGUOIDUNG.LOAIND.ToString();
            userMode.Text = nGUOIDUNG.CHEDOND.ToString();

            if(userRole.Text == "0")
            {
                name.Text = DataProvider.Ins.DB.NHANVIENs.Where(x => x.TAIKHOANNV == username.Text).First().TENNV;
                var avt = DataProvider.Ins.DB.NHANVIENs.Where(x => x.TAIKHOANNV == username.Text).First();
                Stream StreamObj = new MemoryStream(avt.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();
                avatar.ImageSource = BitObj;

                role.Text = "Staff";
            }
            else if(userRole.Text == "1")
            {
                name.Text = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.TAIKHOANKH == username.Text).First().TENKH;
                var avt = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.TAIKHOANKH == username.Text).First();
                Stream StreamObj = new MemoryStream(avt.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();
                avatar.ImageSource = BitObj;
                role.Text = "Customer";
            }
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

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            NGUOIDUNG ngd = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TAIKHOAN == username.Text).FirstOrDefault();
            if (ngd != null)
            {
                DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.MATKHAU == nGUOIDUNG.MATKHAU).First().MATKHAU = password.Text;
                DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.LOAIND == nGUOIDUNG.LOAIND).First().LOAIND = Convert.ToInt32(userRole.Text);
                DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.CHEDOND == nGUOIDUNG.CHEDOND).First().CHEDOND = false;
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Successfully Update!", "Notification", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                NGUOIDUNG ngdu = new NGUOIDUNG();
                ngdu.TAIKHOAN = username.Text;
                ngdu.MATKHAU = password.Text;
                ngdu.LOAIND = Convert.ToInt32(userRole.Text);
                ngdu.CHEDOND = false;
                DataProvider.Ins.DB.NGUOIDUNGs.Add(ngdu);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Successfully added new!", "Notification", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Information);
                this.Close();
            }

        }
    }
}
