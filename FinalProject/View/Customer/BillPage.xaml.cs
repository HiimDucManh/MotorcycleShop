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
    /// <summary>
    /// Interaction logic for BillPage.xaml
    /// </summary>
    public partial class BillPage : Page
    {
        KHACHHANG khachhang;
        public BillPage(KHACHHANG kh)
        {
            InitializeComponent();
            khachhang= kh;
            
            if (DataProvider.Ins.DB.HOADONMHs.Where(x => x.MAKHMH == kh.MAKH).FirstOrDefault() == null)
            {
                ListPurchase.Visibility = Visibility.Collapsed;
                tblListPurchase.Visibility = Visibility.Visible;
            }
            else
            {
                ListPurchase.Visibility = Visibility.Visible;
                tblListPurchase.Visibility = Visibility.Collapsed;
                ListPurchase.ItemsSource = DataProvider.Ins.DB.HOADONMHs.Where(x => x.MAKHMH == kh.MAKH).ToList();
            }

            if (DataProvider.Ins.DB.HOADONBTs.Where(x => x.MAKHBT == kh.MAKH).FirstOrDefault() == null)
            {
                datagridMaint.Visibility = Visibility.Collapsed;
                tblListMaint.Visibility = Visibility.Visible;
            }
            else
            {
                datagridMaint.Visibility = Visibility.Visible;
                tblListMaint.Visibility = Visibility.Collapsed;
                datagridMaint.ItemsSource = DataProvider.Ins.DB.HOADONBTs.Where(x => x.MAKHBT == kh.MAKH).ToList();
            }
        }

        private void ListPurchase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListPurchase.SelectedItem != null)
            {
                PrintBillWindow wd = new PrintBillWindow(khachhang, (HOADONMH)ListPurchase.SelectedItem);
                wd.ShowDialog();
            }    
        }
    }
}
