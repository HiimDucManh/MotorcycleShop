using FinalProject.View.Customer;
using System;
using System.Collections.Generic;
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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace FinalProject.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            btnMinimize.Background = (Brush)bc.ConvertFrom("#7F8487");
            //btnMinimize.Foreground = Brushes.Black;
        }

        private void btnMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            btnMinimize.Background = Brushes.Transparent;
            btnMinimize.Foreground = Brushes.White;
        }

        private void btnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            //Uri uri = new Uri("Images/CloseBtnRed.png", UriKind.Relative);
            //StreamResourceInfo streamInfo = Application.GetResourceStream(uri);

            //BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            //var brush = new ImageBrush();
            //brush.ImageSource = temp;

            btnClose.Background = Brushes.Red;
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            //Uri uri = new Uri("Images/CloseBtn.png", UriKind.Relative);
            //StreamResourceInfo streamInfo = Application.GetResourceStream(uri);

            //BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            //var brush = new ImageBrush();
            //brush.ImageSource = temp;

            btnClose.Background = Brushes.Transparent;
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
          
        }

        private void TextBlock_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            SignUpView signUpView = new SignUpView();
            signUpView.Show();
            this.Close();
        }
    }
}
