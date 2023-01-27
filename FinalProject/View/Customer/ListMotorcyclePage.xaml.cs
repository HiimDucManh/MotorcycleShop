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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalProject.View.Customer
{
    public partial class ListMotorcyclePage : Page
    {
        KHACHHANG khachhang;

        public ListMotorcyclePage(KHACHHANG kh)
        {
            InitializeComponent();
            khachhang= kh;

            KHUYENMAI khuyenmai = DataProvider.Ins.DB.KHUYENMAIs.Where(x => x.NGAYBATDAU <= DateTime.Now && x.NGAYKETTHUC >= DateTime.Now).First();
            tblTenKM.Text = khuyenmai.TENKM;
            tblPhanTram.Text = khuyenmai.PHANTRAMKM.ToString();
            CultureInfo viVn = new CultureInfo("vi-VN");
            DateTime bd = khuyenmai.NGAYBATDAU.GetValueOrDefault();
            tblNgayBD.Text = bd.ToString("d", viVn);
            DateTime kt = khuyenmai.NGAYKETTHUC.GetValueOrDefault();
            tblNgayKT.Text = kt.ToString("d", viVn);


            List<SANPHAM> listProductDIA = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MALOAISP == "DIA").ToList();
            List<CHITIETSANPHAM> listDIA = getProductInModel(listProductDIA);           
            ListDiavel.ItemsSource= listDIA;

            List<SANPHAM> listProductXDI = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MALOAISP == "XDI").ToList();
            List<CHITIETSANPHAM> listXDI = getProductInModel(listProductXDI);
            ListXDiavel.ItemsSource = listXDI;

            List<SANPHAM> listProductHYP = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MALOAISP == "HYP").ToList();
            List<CHITIETSANPHAM> listHYP = getProductInModel(listProductHYP);
            ListHyperMotard.ItemsSource = listHYP;

            List<SANPHAM> listProductMON = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MALOAISP == "MON").ToList();
            List<CHITIETSANPHAM> listMON = getProductInModel(listProductMON);
            ListMonster.ItemsSource = listMON;

            List<SANPHAM> listProductSTR = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MALOAISP == "STR").ToList();
            List<CHITIETSANPHAM> listSTR = getProductInModel(listProductSTR);
            ListStreetFighter.ItemsSource = listSTR;

            List<SANPHAM> listProductMUL = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MALOAISP == "MUL").ToList();
            List<CHITIETSANPHAM> listMUL = getProductInModel(listProductMUL);
            ListMultistrada.ItemsSource = listMUL;

            List<SANPHAM> listProductPAN = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MALOAISP == "PAN").ToList();
            List<CHITIETSANPHAM> listPAN = getProductInModel(listProductPAN);
            ListPanigale.ItemsSource = listPAN;

            List<SANPHAM> listProductSUP = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MALOAISP == "SUP").ToList();
            List<CHITIETSANPHAM> listSUP = getProductInModel(listProductSUP);
            ListSuperSport.ItemsSource = listSUP;

            //List<SANPHAM> listProductDES = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MALOAISP == "DES").ToList();
            //List<CHITIETSANPHAM> listDES = getProductInModel(listProductDES);
            //ListDesertX.ItemsSource = listDES;
        }

        public List<CHITIETSANPHAM> getProductInModel(List<SANPHAM> listproduct)
        {
            int i = 0;
            List<CHITIETSANPHAM> list = new List<CHITIETSANPHAM>();
            foreach (var ctsp in listproduct)
            {
                if (i < 3)
                {
                    list.Add(DataProvider.Ins.DB.CHITIETSANPHAMs.Where(x => x.MASP == ctsp.MASP).First());
                    i++;
                }                   
            }
            return list;
        }

        private void btnDiavel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DetailMotorcyclePage(khachhang, "DIA"));
        }

        private void btnXDiavel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DetailMotorcyclePage(khachhang, "XDI"));
        }

        private void btnHyper_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DetailMotorcyclePage(khachhang, "HYP"));
        }

        private void btnMonster_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DetailMotorcyclePage(khachhang, "MON"));
        }

        private void btnStreet_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DetailMotorcyclePage(khachhang, "STR"));
        }

        private void btnMultistrada_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DetailMotorcyclePage(khachhang, "MUL"));
        }

        private void btnPanigale_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DetailMotorcyclePage(khachhang, "PAN"));
        }

        private void btnSupersprot_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DetailMotorcyclePage(khachhang, "SUP"));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DetailEventWindow wd = new DetailEventWindow();
            wd.ShowDialog();
        }
    }
}
