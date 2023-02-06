
﻿using FinalProject.Model;
using LiveCharts;
using System;
using System.Collections.Generic;
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

using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;


namespace FinalProject.View.Staffs.Pages
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {

        NHANVIEN nhanV;
        public Dashboard(NHANVIEN nv)
        {
            InitializeComponent();
            nhanV = nv;
        }


        private void minimizeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Total
            totalVerhicles.Text = DataProvider.Ins.DB.SANPHAMs.Count().ToString();
            totalOrders.Text = DataProvider.Ins.DB.HOADONMHs.Count().ToString();
            totalCustomers.Text = DataProvider.Ins.DB.KHACHHANGs.Count().ToString();

            //Orders summary
            onDeliveryTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.TINHTRANG == "On Delivery")).Count().ToString();
            deliveredTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => x.TINHTRANG == "Delivered").Count().ToString();
            canceledTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => x.TINHTRANG == "Canceled").Count().ToString();

            newOrdersTbx.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => x.NGAYHDMH == DateTime.Today).Count().ToString();

            //Loaded chart
            ChartValues<double> chartVL = new ChartValues<double>();
            var listVL = DataProvider.Ins.DB.HOADONMHs.Where(x => x.NGAYHDMH.Value.Year == DateTime.Today.Year).ToList();
            double sum = 0;
            chartVL.Add(0);
            for (int i = 1; i <= 12; i++) 
            {
                sum = 0;
                foreach (var item in listVL)
                {
                    if (item.NGAYHDMH.Value.Month == i)
                        sum += Convert.ToDouble(item.SOTIENPHAITRA);
                }
                chartVL.Add(sum);
            }
            lineSeriesChart.Values = chartVL;/*new ChartValues<double> { 5000000000, 1200000000, 11000000000, 16000, 150, 180, 120 };*/

            //Loaded profile
            var avt = DataProvider.Ins.DB.NHANVIENs.Where(x => x.TAIKHOANNV == nhanV.TAIKHOANNV).First();
            Stream StreamObj = new MemoryStream(avt.IMG);
            BitmapImage BitObj = new BitmapImage();
            BitObj.BeginInit();
            BitObj.StreamSource = StreamObj;
            BitObj.EndInit();
            avatar.ImageSource = BitObj;

            userName.Text = nhanV.HONV + " " + nhanV.TENNV.ToString();
            userEmail.Text = nhanV.EMAIL.ToString();

            //Loaded date
            DateTime dt = DateTime.Today;
            dateTime.Text = dt.Day + " " + dt.Month + " " + dt.Year + ", " + dt.DayOfWeek;
        }

        private void manageOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Staff_Orders(nhanV));
        }

        private void ordersSummaryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ordersSummaryCB.SelectedValue.ToString() == "Today")
            {
                onDeliveryTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.TINHTRANG == "On Delivery") && (x.NGAYHDMH.Value.Day == DateTime.Today.Day)).Count().ToString();
                deliveredTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.TINHTRANG == "Delivered") && (x.NGAYHDMH.Value.Day == DateTime.Today.Day)).Count().ToString();
                canceledTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.TINHTRANG == "Canceled") && (x.NGAYHDMH.Value.Day == DateTime.Today.Day)).Count().ToString();
            }
            else if (ordersSummaryCB.SelectedValue.ToString() == "Month")
            {
                onDeliveryTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.TINHTRANG == "On Delivery") && (x.NGAYHDMH.Value.Month == DateTime.Today.Month)).Count().ToString();
                deliveredTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.TINHTRANG == "Delivered") && (x.NGAYHDMH.Value.Month == DateTime.Today.Month)).Count().ToString();
                canceledTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.TINHTRANG == "Canceled") && (x.NGAYHDMH.Value.Month == DateTime.Today.Month)).Count().ToString();
            }
            else if (ordersSummaryCB.SelectedValue.ToString() == "Year")
            {
                onDeliveryTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.TINHTRANG == "On Delivery") && (x.NGAYHDMH.Value.Year == DateTime.Today.Year)).Count().ToString();
                deliveredTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.TINHTRANG == "Delivered") && (x.NGAYHDMH.Value.Year == DateTime.Today.Year)).Count().ToString();
                canceledTxb.Text = DataProvider.Ins.DB.HOADONMHs.Where(x => (x.TINHTRANG == "Canceled") && (x.NGAYHDMH.Value.Year == DateTime.Today.Year)).Count().ToString();
            }
        }

        private void changeAvt_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (op.ShowDialog() == DialogResult.OK)
            {
                avatar.ImageSource = new BitmapImage(new Uri(op.FileName));
                nhanV.IMG = File.ReadAllBytes(op.FileName);

                DataProvider.Ins.DB.SaveChanges();
                //notifier.ShowSuccess("Avatar update successful");

            }
        }


    }
}
