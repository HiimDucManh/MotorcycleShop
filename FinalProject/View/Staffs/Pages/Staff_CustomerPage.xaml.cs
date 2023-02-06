using FinalProject.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
using Clipboard = System.Windows.Forms.Clipboard;
using DataFormats = System.Windows.Forms.DataFormats;
using DataGrid = System.Windows.Controls.DataGrid;
using MessageBox = System.Windows.Forms.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace FinalProject.View.Staffs.Pages
{
    /// <summary>
    /// Interaction logic for Staff_CustomerPage.xaml
    /// </summary>
    public partial class Staff_CustomerPage : Page
    {
        NHANVIEN nhanVien;
        public Staff_CustomerPage(NHANVIEN nv)
        {
            InitializeComponent();
            nhanVien = nv;
        }

        private void clearSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            searchBox.Clear();
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

        public ObservableCollection<CustomerModel> CreateList(IQueryable<KHACHHANG> cuslist)
        {
            ObservableCollection<CustomerModel> cusList = new ObservableCollection<CustomerModel>();
            int i = 0;
            foreach (var item in cuslist)
            {
                i++;
                CustomerModel cusModel = new CustomerModel();
                string cusName = item.HOKH + " " + item.TENKH;

                cusModel.CustomerId = item.MAKH;
                cusModel.CustomerName = cusName;
                cusModel.No = i.ToString();
                cusModel.CustomerEmail = item.EMAIL;
                cusModel.CustomerGender = item.GIOITINH;
                cusModel.CustomerPhone = item.SDT;
                cusModel.CustomerAddress = item.DIACHI;
                cusModel.CustomerBirth = (item.NGAYSINH.Value.Day + "/" + item.NGAYSINH.Value.Month + "/" + item.NGAYSINH.Value.Year).ToString();
                cusModel.CustomerDateRegister = (item.NGAYDANGKY.Value.Day + "/" + item.NGAYDANGKY.Value.Month + "/" + item.NGAYDANGKY.Value.Year).ToString();
                cusList.Add(cusModel);
            }

            return cusList;
        }

        public void customerList_Loaded(object sender, RoutedEventArgs e)
        {
            int i = 0;
            var cuslist = DataProvider.Ins.DB.KHACHHANGs;
            customerList.ItemsSource = CreateList(cuslist);
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txb = sender as TextBox;
            if (txb.Text != "")
            {
                var filteredList = DataProvider.Ins.DB.KHACHHANGs.Where(x => (x.TENKH.ToLower().Contains(txb.Text.ToLower())) || (x.HOKH.ToLower().Contains(txb.Text.ToLower())));
                customerList.ItemsSource = null;
                customerList.ItemsSource = CreateList(filteredList);
            }
            else
            {
                var cuslist = DataProvider.Ins.DB.KHACHHANGs;
                customerList.ItemsSource = null;
                customerList.ItemsSource = CreateList(cuslist);
            }
        }

        private void filterList_Click(object sender, RoutedEventArgs e)
        {
            var cuslist = DataProvider.Ins.DB.KHACHHANGs.OrderByDescending(x => x.NGAYDANGKY);
            customerList.ItemsSource = null;
            customerList.ItemsSource = CreateList(cuslist);
        }

        
        private void exportList_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".xls";
            saveFileDialog.Filter = "Excel File|*.xlsx;*.xls";
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try 
                {
                    customerList.SelectAllCells();
                    customerList.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                    ApplicationCommands.Copy.Execute(null, customerList);
                    
                    String result = (string)Clipboard.GetData(DataFormats.Text);
                    customerList.UnselectAllCells();

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

        public static DataTable DataGridtoDataTable(DataGrid dg)
        {
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);
            DataTable dt = new DataTable();
            //1st row must be column names; force lower case to ensure matching later on.
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    Row[f] = Fields[f];
                }
                dt.Rows.Add(Row);
            }
            return dt;
        }

        private void more_Click(object sender, RoutedEventArgs e)
        {
            CustomerModel select = customerList.SelectedItem as CustomerModel;
            KHACHHANG kh = DataProvider.Ins.DB.KHACHHANGs.Where(x => x.MAKH == select.CustomerId).First();
            CustomerDetail customer = new CustomerDetail(kh);
            customer.Show();
        }
    }
}
