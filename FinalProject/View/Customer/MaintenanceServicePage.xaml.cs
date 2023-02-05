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
            tblCheckmodel.Visibility = Visibility.Hidden;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string MaintID = string.Empty;
            int v = DataProvider.Ins.DB.HOADONBTs.Count();
            if (v == 0)
            {
                MaintID = "HDBT1";
            }
            else
            {
                List<HOADONBT> listBT = DataProvider.Ins.DB.HOADONBTs.ToList();
                HOADONBT hdbt = listBT[v - 1];
                string mahd = MaintID.Substring(4);
                MaintID = "HDBT" + (Int32.Parse(mahd) + 1);
            }

            if (cbbModel.SelectedItem != null)
            {
                tblCheckmodel.Visibility = Visibility.Hidden;
                LOAISP lsp = DataProvider.Ins.DB.LOAISPs.Where(x => x.TENLOAI == cbbModel.Text).First();
                HOADONBT hdbt = new HOADONBT()
                {
                    MAHDBT = MaintID,
                    NGAYHDBT = DateTime.Now,
                    MALOAISPBT = lsp.MALOAI,
                    MAKHBT = khachhang.MAKH,
                    MANVBT = null,
                    SOTIENPHAITRA = null,
                    TINHTRANG = "Unpaid",
                    GHICHU = tbDetail.Text
                };

                DataProvider.Ins.DB.HOADONBTs.Add(hdbt);
                DataProvider.Ins.DB.SaveChanges();
                notifier.ShowSuccess("Successfully sent information");
            }
            else
            {
                tblCheckmodel.Visibility = Visibility.Visible;
                tblCheckmodel.Text = "*There are no selected models yet";
            }
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
