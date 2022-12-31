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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalProject.View.Customer
{
    /// <summary>
    /// Interaction logic for ImagePage.xaml
    /// </summary>
    public partial class ImagePage : Page
    {
        public ImagePage(byte[] image)
        {
            InitializeComponent();

            if (image != null)
            {
                Stream StreamObj = new MemoryStream(image);
                BitmapImage BitObj = new BitmapImage();
                BitObj.BeginInit();
                BitObj.StreamSource = StreamObj;
                BitObj.EndInit();
                ImgSP.ImageSource = BitObj;
            }
        }
    }
}
