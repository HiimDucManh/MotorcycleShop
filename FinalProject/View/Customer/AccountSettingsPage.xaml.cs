using FinalProject.Model;
using MaterialDesignThemes.Wpf.Converters;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace FinalProject.View.Customer
{
    public partial class AccountSettingsPage : Page
    {
        KHACHHANG khachhang;
        public AccountSettingsPage(KHACHHANG kh)
        {
            InitializeComponent();
            khachhang= kh;
            tgbAccount.IsChecked = true;

            if (khachhang.IMG != null)
            {
                Stream StreamObj = new MemoryStream(khachhang.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();
                ImageProfile.ImageSource = BitObj;
            }

            if (khachhang.NGUOIDUNG.CHEDOND == false)
            {
                tgbMoon.IsChecked = false;
                tgbSun.IsChecked = true;
            }
            else
            {
                tgbMoon.IsChecked = true;
                tgbSun.IsChecked = false;
            }
        }

        private void btnChangeIMG_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (op.ShowDialog() == DialogResult.OK)
            {
                ImageProfile.ImageSource = new BitmapImage(new Uri(op.FileName));
                khachhang.IMG = File.ReadAllBytes(op.FileName);

                DataProvider.Ins.DB.SaveChanges();
                notifier.ShowSuccess("Avatar update successful");

            }
        }

        private void tgbAccount_Checked(object sender, RoutedEventArgs e)
        {
            if(tgbAccount.IsChecked == true)
            {
                PagesNavigation.Navigate(new ProfilePage(khachhang));
            }
        }

        private void tgbSecurity_Checked(object sender, RoutedEventArgs e)
        {
            if (tgbSecurity.IsChecked == true)
            {
                PagesNavigation.Navigate(new SecurityPage(khachhang));
            }
        }

        private void tgbBill_Checked(object sender, RoutedEventArgs e)
        {
            if (tgbBill.IsChecked == true)
            {
                PagesNavigation.Navigate(new BillPage(khachhang));
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginView wd = new LoginView();
            wd.Show();
            CloseWindow();
        }

        public void CloseWindow()
        {
            var parentWindow = Window.GetWindow(this);

            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void tgbSun_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Resources["PrimaryBackgroundColor"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#f3f3f3");
            System.Windows.Application.Current.Resources["TextSecundaryColor"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#434344");
            khachhang.NGUOIDUNG.CHEDOND = false;
            DataProvider.Ins.DB.SaveChanges();
        }

        private void tgbMoon_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Resources["PrimaryBackgroundColor"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#36393F");
            System.Windows.Application.Current.Resources["TextSecundaryColor"] = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF");
            khachhang.NGUOIDUNG.CHEDOND = true;
            DataProvider.Ins.DB.SaveChanges();
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: System.Windows.Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 120);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(1),
                maximumNotificationCount: MaximumNotificationCount.FromCount(1));

            cfg.Dispatcher = System.Windows.Application.Current.Dispatcher;
        });       
    }
}
