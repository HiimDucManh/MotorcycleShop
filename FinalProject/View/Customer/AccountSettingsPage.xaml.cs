using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace FinalProject.View.Customer
{
    /// <summary>
    /// Interaction logic for AccountSettingsPage.xaml
    /// </summary>
    public partial class AccountSettingsPage : Page
    {
        public AccountSettingsPage()
        {
            InitializeComponent();
            tgbAccount.IsChecked = true;
        }

        private void btnChangeIMG_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (op.ShowDialog() == DialogResult.OK)
            {
                ImageProfile.ImageSource = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void tgbAccount_Checked(object sender, RoutedEventArgs e)
        {
            if(tgbAccount.IsChecked == true)
            {
                PagesNavigation.Navigate(new ProfilePage());
            }
        }
    }
}
