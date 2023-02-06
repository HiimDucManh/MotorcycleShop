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
    /// Interaction logic for Staff_Maintenance.xaml
    /// </summary>
    public partial class Staff_Maintenance : Page
    {
        NHANVIEN nhanVien;
        public Staff_Maintenance(NHANVIEN nv)
        {
            InitializeComponent();
            nhanVien = nv;
        }

        private void clearSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            searchBox.Clear();
        }

        private void maintenanceList_Loaded(object sender, RoutedEventArgs e)
        {
            var maintenance = DataProvider.Ins.DB.HOADONBTs;
            maintenanceList.ItemsSource = CreateList(maintenance);
        }
        public ObservableCollection<MaintenanceModel> CreateList(IQueryable<HOADONBT> cuslist)
        {
            ObservableCollection<MaintenanceModel> maintenance = new ObservableCollection<MaintenanceModel>();
            int i = 0;
            foreach (var item in cuslist)
            {
                i++;
                MaintenanceModel mainteModel = new MaintenanceModel();

                mainteModel.Id = item.MAHDBT;
                mainteModel.Date = item.NGAYHDBT.Value.Date;
                mainteModel.No = i.ToString();
                mainteModel.CustomerId = item.MAKHBT;
                mainteModel.StaffId = item.MANVBT;
                mainteModel.ProductId = item.MALOAISPBT;
                if(item.TINHTRANG == "Done")
                    mainteModel.Status = true;
                else mainteModel.Status = false;
               
                maintenance.Add(mainteModel);
            }

            return maintenance;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txb = sender as TextBox;
            if (txb.Text != "")
            {
                var filteredList = DataProvider.Ins.DB.HOADONBTs.Where(x => x.MALOAISPBT.ToLower().Contains(txb.Text.ToLower()));
                maintenanceList.ItemsSource = null;
                maintenanceList.ItemsSource = CreateList(filteredList);
            }
            else
            {
                var mainte = DataProvider.Ins.DB.HOADONBTs;
                maintenanceList.ItemsSource = null;
                maintenanceList.ItemsSource = CreateList(mainte);
            }
        }

        private void filterList_Click(object sender, RoutedEventArgs e)
        {
            var mainte = DataProvider.Ins.DB.HOADONBTs.OrderByDescending(x => x.NGAYHDBT);
            maintenanceList.ItemsSource = null;
            maintenanceList.ItemsSource = CreateList(mainte);
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
                    maintenanceList.SelectAllCells();
                    maintenanceList.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                    ApplicationCommands.Copy.Execute(null, maintenanceList);

                    String result = (string)Clipboard.GetData(DataFormats.Text);
                    maintenanceList.UnselectAllCells();

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
