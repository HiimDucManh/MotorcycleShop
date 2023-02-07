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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace FinalProject.View.Customer
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        KHACHHANG khachhang;
        public ProfilePage(KHACHHANG kh)
        {
            InitializeComponent();
            khachhang= kh;
            setInformationCus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            setInformationCus();
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            khachhang.TENKH = tbFirstName.Text;
            khachhang.HOKH = tbLastName.Text;
            khachhang.NGAYSINH = dtBirthday.SelectedDate;
            if (cbbGender.SelectedIndex == 0)
                khachhang.GIOITINH = "Nam";
            else
                khachhang.GIOITINH = "Nữ";
            khachhang.SDT = tbPhone.Text;
            khachhang.DIACHI = tbAddress.Text;
            khachhang.EMAIL = tbEmail.Text;
            khachhang.NGAYDANGKY = dtStart.SelectedDate;
            DataProvider.Ins.DB.SaveChanges();
            notifier.ShowSuccess("Save Successfully");
        }

        private void setInformationCus()
        {
            tbFirstName.Text = khachhang.TENKH;
            tbLastName.Text = khachhang.HOKH;
            dtBirthday.Text = khachhang.NGAYSINH.ToString();
            if (khachhang.GIOITINH == "Nam")
                cbbGender.SelectedIndex = 0;
            else
                cbbGender.SelectedIndex = 1;
            dtStart.Text = khachhang.NGAYDANGKY.ToString();
            tbPhone.Text = khachhang.SDT;
            tbAddress.Text = khachhang.DIACHI;
            tbEmail.Text = khachhang.EMAIL;
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 120);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(1),
                maximumNotificationCount: MaximumNotificationCount.FromCount(1));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
    }
}
