using FinalProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Clipboard = System.Windows.Forms.Clipboard;
using DataFormats = System.Windows.Forms.DataFormats;
using MessageBox = System.Windows.Forms.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace FinalProject.View.Staffs.Pages
{
    /// <summary>
    /// Interaction logic for Staff_Orders.xaml
    /// </summary>
    public partial class Staff_Orders : Page
    {
        NHANVIEN nhanVien;
        public Staff_Orders(NHANVIEN nv)
        {
            InitializeComponent();
            nhanVien = nv;
        }

        private void clearSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            searchBox.Clear();
        }

        private void orderList_Loaded(object sender, RoutedEventArgs e)
        {
            var order = DataProvider.Ins.DB.HOADONMHs;
            orderList.ItemsSource = CreateList(order);
        }

        public ObservableCollection<OrderModel> CreateList(IQueryable<HOADONMH> ordlist)
        {
            ObservableCollection<OrderModel> cusList = new ObservableCollection<OrderModel>();
            int i = 0;
            foreach (var item in ordlist)
            {
                i++;
                OrderModel ordModel = new OrderModel();

                ordModel.Id = item.MAHDMH;
                ordModel.Date = item.NGAYHDMH.Value.Date;
                ordModel.No = i.ToString();
                ordModel.CustomerId = item.MAKHMH;
                ordModel.ProductId = item.MASPMH;
                ordModel.StaffId = item.MANVMH;
                ordModel.Price = item.SOTIENPHAITRA.ToString();
                ordModel.Status = item.TINHTRANG;
                cusList.Add(ordModel);
            }

            return cusList;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txb = sender as TextBox;
            if (txb.Text != "")
            {
                var filteredList = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.MAKHMH.ToLower().Contains(txb.Text.ToLower())));
                orderList.ItemsSource = null;
                orderList.ItemsSource = CreateList(filteredList);
            }
            else
            {
                var ordlist = DataProvider.Ins.DB.HOADONMHs;
                orderList.ItemsSource = null;
                orderList.ItemsSource = CreateList(ordlist);
            }
        }

        private void filterList_Click(object sender, RoutedEventArgs e)
        {
            var ordlist = DataProvider.Ins.DB.HOADONMHs.OrderByDescending(x => x.NGAYHDMH);
            orderList.ItemsSource = null;
            orderList.ItemsSource = CreateList(ordlist);
        }

        private void exportList_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".xls";
            saveFileDialog.Filter = "Excel File|*.xlsx;*.xls";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    orderList.SelectAllCells();
                    orderList.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                    ApplicationCommands.Copy.Execute(null, orderList);

                    String result = (string)Clipboard.GetData(DataFormats.Text);
                    orderList.UnselectAllCells();

                    System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName);
                    file.WriteLine(result.Replace(",", " "));
                    file.Close();

                    MessageBox.Show("Success");
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            userName.Content = nhanVien.HONV + " " + nhanVien.TENNV;

            var avt = DataProvider.Ins.DB.NHANVIENs.Where(x => x.TAIKHOANNV == nhanVien.TAIKHOANNV).First();
            Stream StreamObj = new MemoryStream(avt.IMG);
            BitmapImage BitObj = new BitmapImage();
            BitObj.BeginInit();
            BitObj.StreamSource = StreamObj;
            BitObj.EndInit();
            avatar.Source = BitObj;
        }

        private void allBtn_Click(object sender, RoutedEventArgs e)
        {
            var order = DataProvider.Ins.DB.HOADONMHs;
            orderList.ItemsSource = CreateList(order);
        }

        private void onDeliveryBtn_Click(object sender, RoutedEventArgs e)
        {
            var order = DataProvider.Ins.DB.HOADONMHs.Where(x => x.TINHTRANG == "On Delivery");
            orderList.ItemsSource = CreateList(order);
        }

        private void DeliveredBtn_Click(object sender, RoutedEventArgs e)
        {
            var order = DataProvider.Ins.DB.HOADONMHs.Where(x => x.TINHTRANG == "Delivered");
            orderList.ItemsSource = CreateList(order);
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            var order = DataProvider.Ins.DB.HOADONMHs.Where(x => x.TINHTRANG == "Cancel");
            orderList.ItemsSource = CreateList(order);
        }
    }
}
