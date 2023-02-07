using FinalProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FinalProject.View.Admin
{
    /// <summary>
    /// Interaction logic for AllUsers.xaml
    /// </summary>
    public partial class AllUsers : Page
    {
        public AllUsers()
        {
            InitializeComponent();
        }

        private void userList_Loaded(object sender, RoutedEventArgs e)
        {
            var uslist = DataProvider.Ins.DB.NGUOIDUNGs;
            userList.ItemsSource = CreateList(uslist);
        }

        public ObservableCollection<UserModel> CreateList(IQueryable<NGUOIDUNG> uslist)
        {
            ObservableCollection<UserModel> usList = new ObservableCollection<UserModel>();
            int i = 0;
            foreach (var item in uslist)
            {
                i++;
                UserModel usModel = new UserModel();

                usModel.Username = item.TAIKHOAN;
                usModel.Password = item.MATKHAU;
                usModel.No = i.ToString();
                usModel.UserRole = item.LOAIND.ToString();
                usModel.UserMode = item.CHEDOND.ToString();
                
                usList.Add(usModel);
            }

            return usList;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txb = sender as TextBox;
            if (txb.Text != "")
            {
                var filteredList = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => (x.TAIKHOAN.ToLower().Contains(txb.Text.ToLower())));
                userList.ItemsSource = null;
                userList.ItemsSource = CreateList(filteredList);
            }
            else
            {
                var uslist = DataProvider.Ins.DB.NGUOIDUNGs;
                userList.ItemsSource = null;
                userList.ItemsSource = CreateList(uslist);
            }
        }

        private void filterList_Click(object sender, RoutedEventArgs e)
        {
            var uslist = DataProvider.Ins.DB.NGUOIDUNGs.OrderByDescending(x => x.LOAIND);
            userList.ItemsSource = null;
            userList.ItemsSource = CreateList(uslist);
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
                    userList.SelectAllCells();
                    userList.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                    ApplicationCommands.Copy.Execute(null, userList);

                    String result = (string)Clipboard.GetData(DataFormats.Text);
                    userList.UnselectAllCells();

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

        private void allBtn_Click(object sender, RoutedEventArgs e)
        {
            var uslist = DataProvider.Ins.DB.NGUOIDUNGs;
            userList.ItemsSource = CreateList(uslist);
        }

        private void customer_Click(object sender, RoutedEventArgs e)
        {
            var uslist = DataProvider.Ins.DB.NGUOIDUNGs.Where(x=>x.LOAIND == 1);
            userList.ItemsSource = CreateList(uslist);
        }

        private void staff_Click(object sender, RoutedEventArgs e)
        {
            var uslist = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.LOAIND == 0);
            userList.ItemsSource = CreateList(uslist);
        }

        private void clearSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            searchBox.Clear();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            UserModel userModel = userList.SelectedItem as UserModel;
            NGUOIDUNG ng = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TAIKHOAN == userModel.Username).First();
            NewUserWindow newUser = new NewUserWindow(ng);
            newUser.Show();
            userList_Loaded(sender, e);
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            UserModel userModel = userList.SelectedItem as UserModel ;
            NGUOIDUNG ng = DataProvider.Ins.DB.NGUOIDUNGs.Where(x => x.TAIKHOAN == userModel.Username).First();
            if (MessageBox.Show("Do you want to continue?", "Notification", (MessageBoxButtons)MessageBoxButton.YesNo, (MessageBoxIcon)MessageBoxImage.Question) == DialogResult.Yes)
            {
                DataProvider.Ins.DB.NGUOIDUNGs.Remove(ng);
                DataProvider.Ins.DB.SaveChanges();
                MessageBox.Show("Successful delete!", "Notification", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Information);
                userList_Loaded(sender, e);
            }
        }
    }
}
