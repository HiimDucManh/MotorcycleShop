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
using Clipboard = System.Windows.Forms.Clipboard;
using DataFormats = System.Windows.Forms.DataFormats;
using MessageBox = System.Windows.Forms.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace FinalProject.View.Staffs.Pages
{
    /// <summary>
    /// Interaction logic for Staff_Products.xaml
    /// </summary>
    public partial class Staff_Products : Page
    {
        NHANVIEN nhanVien;
        public Staff_Products(NHANVIEN nv)
        {
            InitializeComponent();
            nhanVien = nv;
        }

        private void clearSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            searchBox.Clear();
        }

        private void productList_Loaded(object sender, RoutedEventArgs e)
        {
            var prod = DataProvider.Ins.DB.SANPHAMs;
            productList.ItemsSource = CreateList(prod);
        }
        public ObservableCollection<ProductModel> CreateList(IQueryable<SANPHAM> prodChild)
        {
            ObservableCollection<ProductModel> prodList = new ObservableCollection<ProductModel>();
            int i = 0;
            foreach (var item in prodChild)
            {
                i++;
                ProductModel productModel = new ProductModel();

                productModel.Id = item.MASP;
                productModel.Name = item.TENSP;
                productModel.No = i.ToString();
                productModel.Cost = item.GIAGOC.ToString();
                productModel.Price = item.GIABAN.ToString();
                productModel.Type = item.MALOAISP;
                
                prodList.Add(productModel);
            }

            return prodList;
        }

        private void filterList_Click(object sender, RoutedEventArgs e)
        {
            var prod = DataProvider.Ins.DB.SANPHAMs.OrderBy(x=>x.GIABAN);
            productList.ItemsSource = null;
            productList.ItemsSource = CreateList(prod);
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txb = sender as TextBox;
            if (txb.Text != "")
            {
                var filteredList = DataProvider.Ins.DB.SANPHAMs.Where(x => x.TENSP.ToLower().Contains(txb.Text.ToLower()));
                productList.ItemsSource = null;
                productList.ItemsSource = CreateList(filteredList);
            }
            else
            {
                var prod = DataProvider.Ins.DB.SANPHAMs;
                productList.ItemsSource = null;
                productList.ItemsSource = CreateList(prod);
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

        private void exportList_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".xls";
            saveFileDialog.Filter = "Excel File|*.xlsx;*.xls";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    productList.SelectAllCells();
                    productList.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                    ApplicationCommands.Copy.Execute(null, productList);

                    String result = (string)Clipboard.GetData(DataFormats.Text);
                    productList.UnselectAllCells();

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
    }
}
