using FinalProject.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace FinalProject.View.Customer
{
    /// <summary>
    /// Interaction logic for PrintBillWindow.xaml
    /// </summary>
    public partial class PrintBillWindow : Window
    {
        HOADONMH hd;
        public PrintBillWindow(KHACHHANG kh, HOADONMH hdmh)
        {
            InitializeComponent();
            hd = hdmh;

            tblCusName.Text = kh.HOKH + " " + kh.TENKH;
            CultureInfo cul = new CultureInfo("en-US");
            DateTime bd = hdmh.NGAYHDMH.GetValueOrDefault();
            tblDate.Text = bd.ToString("D", cul);
            tblBillID.Text = hdmh.MAHDMH;

            tblProName.Text = hdmh.SANPHAM.TENSP;
            tblPrice.Text = String.Format("{0:0,0}", hdmh.TONGTIEN);
            tblPriceAmount.Text = String.Format("{0:0,0}", hdmh.TONGTIEN);
            tblSubtotal.Text = String.Format("{0:0,0}", hdmh.TONGTIEN);

            if(hdmh.MAKM == null)
            {
                tblDiscount.Text = "0";
                tblPriceDiscount.Text = "0";
            }
            else
            {
                tblDiscount.Text = hdmh.KHUYENMAI.PHANTRAMKM.ToString();
                tblPriceDiscount.Text = String.Format("{0:0,0}", hdmh.SOTIENKM);
            }
            tblTotalAmount.Text = String.Format("{0:0,0}", hdmh.SOTIENPHAITRA);

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice_" + hd.MAHDMH);
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
