using FinalProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace FinalProject.View.Customer
{
    public partial class SecurityPage : Page
    {
        KHACHHANG khachhang;
        int otp;
        Random random= new Random();
        public SecurityPage(KHACHHANG kh)
        {
            InitializeComponent();
            khachhang = kh;
            ResetPage();
            btnSendCode.Visibility = Visibility.Visible;
            btnResendCode.Visibility = Visibility.Collapsed;
            tblCheckCurrentPass.Visibility = Visibility.Hidden;
            tblCheckConfirmPass.Visibility = Visibility.Hidden;
            tblCheckCode.Visibility = Visibility.Hidden;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ResetPage();
        }

        private void btnSendCode_Click(object sender, RoutedEventArgs e)
        {
            string to, from, pass, messageBody;
            MailMessage message = new MailMessage();
            to = khachhang.EMAIL;
            from = "20520628@gm.uit.edu.vn";
            pass = "1911115717";

            otp = random.Next(100000, 1000000);
            messageBody = otp.ToString();
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = "Verification code: " + messageBody;
            message.Subject = "Verification code";
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(message);
                notifier.ShowSuccess("Code Sent Successfully");
                btnSendCode.Visibility = Visibility.Collapsed;
                btnResendCode.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
            }           
        }

        private void btnResendCode_Click(object sender, RoutedEventArgs e)
        {
            string to, from, pass, messageBody;
            MailMessage message = new MailMessage();
            to = khachhang.EMAIL;
            from = "20520628@gm.uit.edu.vn";
            pass = "1911115717";

            otp = random.Next(100000, 1000000);
            messageBody = otp.ToString();
            message.To.Add(to);
            message.From = new MailAddress(from);
            message.Body = "Verification code: " + messageBody;
            message.Subject = "Verification code";
            message.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(from, pass);

            try
            {
                smtp.Send(message);
                notifier.ShowSuccess("Code Sent Successfully");
                btnSendCode.Visibility = Visibility.Collapsed;
                btnResendCode.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(tbCurrentPass.Password != khachhang.NGUOIDUNG.MATKHAU)
            {
                tblCheckCurrentPass.Visibility= Visibility.Visible;
                tblCheckConfirmPass.Visibility = Visibility.Hidden;
                tblCheckCode.Visibility = Visibility.Hidden;
                tblCheckCurrentPass.Text = "*Wrong password";
            }    
            else if(tbNewPass.Password != tbConfirmPass.Password)
            {
                tblCheckConfirmPass.Visibility = Visibility.Visible;
                tblCheckCurrentPass.Visibility = Visibility.Hidden;
                tblCheckCode.Visibility = Visibility.Hidden;
                tblCheckConfirmPass.Text = "*Confirm password does not match";
            }
            else if(otp != Int32.Parse(tbCode.Text))
            {
                tblCheckCode.Visibility = Visibility.Visible;
                tblCheckCurrentPass.Visibility = Visibility.Hidden;
                tblCheckConfirmPass.Visibility = Visibility.Hidden;
                tblCheckCode.Text = "*Wrong verification code";
            }    
            else
            {
                tblCheckCurrentPass.Visibility = Visibility.Hidden;
                tblCheckConfirmPass.Visibility = Visibility.Hidden;
                tblCheckCode.Visibility= Visibility.Hidden;
                khachhang.NGUOIDUNG.MATKHAU = tbNewPass.Password;
                DataProvider.Ins.DB.SaveChanges();

                notifier.ShowSuccess("Save Successfully");
            }
        }

        private void ResetPage()
        {
            tbCurrentPass.Password = string.Empty;
            tbNewPass.Password = string.Empty;
            tbConfirmPass.Password = string.Empty;
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

        private void tbCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }  
    }
}
