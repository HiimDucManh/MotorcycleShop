using ControlzEx.Standard;
using FinalProject.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Lifetime.Clear;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace FinalProject.View.Customer
{
    public partial class DetailMotorcyclePage : Page
    {
        KHACHHANG khachhang;
        CHITIETSANPHAM CTSP;
        decimal? km;
        public DetailMotorcyclePage(KHACHHANG kh, string model)
        {
            InitializeComponent();
            khachhang = kh;
            tblCheckPayment.Visibility = Visibility.Hidden;

            if (model == "DIA")
            {
                backgroundModel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Layout/NewYork.jpg"));
            }
            else if(model == "XDI")
            {
                backgroundModel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Layout/Nera.jpg"));
            }
            else if (model == "HYP")
            {
                backgroundModel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Layout/Hyper.jpg"));
            }
            else if (model == "MON")
            {
                backgroundModel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Layout/RaceTrack.jpg"));
            }
            else if (model == "STR")
            {
                backgroundModel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Layout/Street.jpg"));
            }
            else if (model == "MUL")
            {
                backgroundModel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Layout/Mountain.jpg"));
            }
            else if (model == "PAN")
            {
                backgroundModel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Layout/Pan.jpg"));
            }
            else if (model == "SUP")
            {
                backgroundModel.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Layout/Seaside.jpg"));
            }

            List<SANPHAM> listProduct = DataProvider.Ins.DB.SANPHAMs.Where(x => x.MALOAISP == model).ToList();
            listviewProduct.ItemsSource = listProduct;

            LOAISP lsp = DataProvider.Ins.DB.LOAISPs.Where(x => x.MALOAI == model).FirstOrDefault();
            tblNameModel.Text = lsp.TENLOAI;

            getDetailMoto(listProduct[0]);
            loadBill(listProduct[0].CHITIETSANPHAMs.First());
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 120);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(1),
                maximumNotificationCount: MaximumNotificationCount.FromCount(1));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMotorcyclePage(khachhang));
        }

        private void listviewProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listviewProduct.SelectedIndex == -1)
                return;

            for (int i = 0; i < listviewProduct.Items.Count; i++)
            {
                if (listviewProduct.SelectedIndex == i)
                {
                    ListBoxItem currentSelectedListBoxItem = listviewProduct.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (currentSelectedListBoxItem == null)
                        return;

                    // Iterate whole listbox tree and search for this items
                    TextBlock nameBlock = FindDescendant<TextBlock>(currentSelectedListBoxItem);

                    var bc = new BrushConverter();
                    nameBlock.Foreground = (Brush)bc.ConvertFrom("#E32431");

                    SANPHAM sp = (SANPHAM)listviewProduct.SelectedItem;
                    getDetailMoto(sp);
                    loadBill(sp.CHITIETSANPHAMs.First());
                }
                else
                {

                    ListBoxItem currentListBoxItem = listviewProduct.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                    // Iterate whole listbox tree and search for this items
                    TextBlock nameBlock = FindDescendant<TextBlock>(currentListBoxItem);

                    var bc = new BrushConverter();
                    nameBlock.Foreground = (Brush)bc.ConvertFrom("#BFBFC4");
                }
            }
        }

        public T FindDescendant<T>(DependencyObject obj) where T : DependencyObject
        {
            // Check if this object is the specified type
            if (obj is T)
                return obj as T;

            // Check for children
            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            if (childrenCount < 1)
                return null;

            // First check all the children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                    return child as T;
            }

            // Then check the childrens children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = FindDescendant<T>(VisualTreeHelper.GetChild(obj, i));
                if (child != null && child is T)
                    return child as T;
            }

            return null;
        }

        private void listviewColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listviewColor.SelectedIndex == -1)
                return;

            for (int i = 0; i < listviewColor.Items.Count; i++)
            {
                if (listviewColor.SelectedIndex == i)
                {
                    ListBoxItem currentSelectedListBoxItem = listviewColor.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (currentSelectedListBoxItem == null)
                        return;

                    Thickness margin = currentSelectedListBoxItem.Margin;
                    margin.Bottom = 1;
                    margin.Top = -1;
                    currentSelectedListBoxItem.Margin = margin;

                    MAU m = (MAU)listviewColor.SelectedItem;
                    getDetailColorMoto(m);

                    CHITIETSANPHAM ctsp = DataProvider.Ins.DB.CHITIETSANPHAMs.Where(x => x.MAMAUSP == m.MAMAU && x.SANPHAM.LOAISP.TENLOAI == tblNameModel.Text).First();
                    CTSP = ctsp;
                    loadBill(ctsp);
                }
                else
                {

                    ListBoxItem currentListBoxItem = listviewColor.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                    Thickness margin = currentListBoxItem.Margin;
                    margin.Bottom = 0;
                    margin.Top = 0;
                    currentListBoxItem.Margin = margin;
                }
            }
        }

        private void getDetailColorMoto(MAU mau)
        {
            CHITIETSANPHAM ctsp = DataProvider.Ins.DB.CHITIETSANPHAMs.Where(x => x.MAMAUSP == mau.MAMAU && x.SANPHAM.LOAISP.TENLOAI == tblNameModel.Text).First();
            ImagePagesNavigation.Navigate(new ImagePage(ctsp.IMG));
            tblDescriptionCL.Text = ctsp.GHICHU;

            CTSP = ctsp;
        }

        private void getDetailMoto(SANPHAM sanpham)
        {
            tblNameProduct.Text = sanpham.TENSP;
            tblPriceProduct.Text = String.Format("{0:0,0}", sanpham.GIABAN);
            tblDescription.Text = sanpham.GHICHU;

            tblDT.Text = sanpham.DUNGTICH;
            tblCS.Text = sanpham.CONGSUAT;
            tblMM.Text = sanpham.MOMEN;
            tblTL.Text = sanpham.TRONGLUONG;

            List<CHITIETSANPHAM> listCTSP = sanpham.CHITIETSANPHAMs.ToList();
            ImagePagesNavigation.Navigate(new ImagePage(listCTSP[0].IMG));

            List<MAU> listMau = new List<MAU>();
            foreach (var item in listCTSP)
            {
                MAU m = DataProvider.Ins.DB.MAUs.Where(x => x.MAMAU == item.MAMAUSP).First();
                listMau.Add(m);
            }
            listviewColor.ItemsSource = listMau;
            tblDescriptionCL.Text = listCTSP[0].GHICHU;

            CTSP = listCTSP[0];
        }

        private void loadBill(CHITIETSANPHAM chitietsp)
        {
            if (chitietsp.IMG != null)
            {
                Stream StreamObj = new MemoryStream(chitietsp.IMG);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();
                imgSP.ImageSource = BitObj;
            }

            tblTenSP.Text = chitietsp.SANPHAM.TENSP;
            tblMauSP.Text = chitietsp.MAU.TENMAU;
            tbPrice.Text = String.Format("{0:0,0}", chitietsp.SANPHAM.GIABAN);
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0.5;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            //da.AutoReverse = true;
            //da.RepeatBehavior = RepeatBehavior.Forever;
            //da.RepeatBehavior=new RepeatBehavior(3);
            gridDetail.BeginAnimation(OpacityProperty, da);
            gridDetail.IsEnabled = false;

            gridCreateBill.RenderTransform = new TranslateTransform(500, 0);
            DoubleAnimation da1 = new DoubleAnimation();
            da1.From = 500;
            da1.To = 0;
            da1.Duration = new Duration(TimeSpan.FromSeconds(1));
            da1.DecelerationRatio = 0.6;
            gridCreateBill.RenderTransform.BeginAnimation(TranslateTransform.XProperty, da1);

            gridCreateBill.Visibility = Visibility.Visible;
            tbName.Text = khachhang.HOKH + " " + khachhang.TENKH;
            tbPhone.Text = khachhang.SDT;
            tbAddress.Text = khachhang.DIACHI;
            tbEmail.Text = khachhang.EMAIL;

            int v = DataProvider.Ins.DB.HOADONMHs.Count();
            if(v == 0)
            {
                tblMaHD.Text = "HDMH1";
            }    
            else
            {
                List<HOADONMH> listMH = DataProvider.Ins.DB.HOADONMHs.ToList();
                HOADONMH hdmh = listMH[v - 1];
                string mahd = hdmh.MAHDMH.Substring(4);
                tblMaHD.Text = "HDMH" + (Int32.Parse(mahd) + 1);
            }    
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            gridCreateBill.RenderTransform = new TranslateTransform(0, 0);
            DoubleAnimation da1 = new DoubleAnimation();
            da1.From = 0;
            da1.To = 500;
            da1.Duration = new Duration(TimeSpan.FromSeconds(1));
            da1.DecelerationRatio = 0.6;
            gridCreateBill.RenderTransform.BeginAnimation(TranslateTransform.XProperty, da1);

            DoubleAnimation da = new DoubleAnimation();
            da.From = 0.5;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            gridDetail.BeginAnimation(OpacityProperty, da);
            gridDetail.IsEnabled = true;
        }

        private void btnCreateBill_Click(object sender, RoutedEventArgs e)
        {
            if (cbbPayment.SelectedItem == null)
            {
                tblCheckPayment.Visibility = Visibility.Visible;
                tblCheckPayment.Text = "*Select a payment method";
                return;
            }

            gridCreateBill.RenderTransform = new TranslateTransform(0, 0);
            DoubleAnimation da1 = new DoubleAnimation();
            da1.From = 0;
            da1.To = 500;
            da1.Duration = new Duration(TimeSpan.FromSeconds(1));
            da1.DecelerationRatio = 0.6;
            gridCreateBill.RenderTransform.BeginAnimation(TranslateTransform.XProperty, da1);

            DoubleAnimation da = new DoubleAnimation();
            da.From = 0.5;
            da.To = 1;
            da.Duration = new Duration(TimeSpan.FromSeconds(1));
            gridDetail.BeginAnimation(OpacityProperty, da);
            gridDetail.IsEnabled = true;
           
            if(decimal.Parse(tbPrice.Text) == CTSP.SANPHAM.GIABAN)
            {
                HOADONMH hdmh = new HOADONMH()
                {
                    MAHDMH = tblMaHD.Text,
                    NGAYHDMH = DateTime.Now,
                    MASPMH = CTSP.MASP,
                    MAMAUSPMH = CTSP.MAMAUSP,
                    IMGSPMH = CTSP.IMG,
                    MAKHMH = khachhang.MAKH,
                    MANVMH = null,
                    LOAITHANHTOAN = convertPayment(cbbPayment.SelectedIndex),
                    TONGTIEN = CTSP.SANPHAM.GIABAN,
                    MAKM = null,
                    SOTIENKM = null,
                    SOTIENPHAITRA = decimal.Parse(tbPrice.Text),
                    TINHTRANG = "Unpaid"
                };
                DataProvider.Ins.DB.HOADONMHs.Add(hdmh);
            }
            else
            {
                HOADONMH hdmh = new HOADONMH()
                {
                    MAHDMH = tblMaHD.Text,
                    NGAYHDMH = DateTime.Now,
                    MASPMH = CTSP.MASP,
                    MAMAUSPMH = CTSP.MAMAUSP,
                    IMGSPMH = CTSP.IMG,
                    MAKHMH = khachhang.MAKH,
                    MANVMH = null,
                    LOAITHANHTOAN = convertPayment(cbbPayment.SelectedIndex),
                    TONGTIEN = CTSP.SANPHAM.GIABAN,
                    MAKM = tbCode.Text,
                    SOTIENKM = km,
                    SOTIENPHAITRA = decimal.Parse(tbPrice.Text),
                    TINHTRANG = "Unpaid"
                };
                DataProvider.Ins.DB.HOADONMHs.Add(hdmh);

                
            }
            if (CTSP.SLTON > 0)
            {
                CTSP.SLTON = CTSP.SLTON - 1;
                CTSP.SLBAN = CTSP.SLBAN + 1;
                khachhang.DOANHSO = khachhang.DOANHSO + decimal.Parse(tbPrice.Text);
                khachhang.SOLUONGSANPHAM = khachhang.SOLUONGSANPHAM + 1;

                DataProvider.Ins.DB.SaveChanges();
                notifier.ShowSuccess("Order Success");
            }
            else
            {
                notifier.ShowError("The product is out of stock");
            }
        }

        private void tbCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            CHITIETKHUYENMAI ctkm = DataProvider.Ins.DB.CHITIETKHUYENMAIs.Where(x => x.MAKM == tbCode.Text && x.MALOAISPKM == CTSP.SANPHAM.MALOAISP && x.KHUYENMAI.NGAYBATDAU <= DateTime.Now && x.KHUYENMAI.NGAYKETTHUC >= DateTime.Now).FirstOrDefault();
            if(ctkm != null)
            {
                decimal? giagoc = CTSP.SANPHAM.GIABAN;
                decimal? giaban = giagoc * (100 - (int)ctkm.KHUYENMAI.PHANTRAMKM) / 100;
                km = giagoc * (int)ctkm.KHUYENMAI.PHANTRAMKM / 100;
                tbPrice.Text = String.Format("{0:0,0}", giaban);
            }
            else
            {
                tbPrice.Text = String.Format("{0:0,0}", CTSP.SANPHAM.GIABAN);
            }    
        }

        private bool convertPayment(int i)
        {
            if(i == 0)
                return false;
            return true;
        }
    }
}
