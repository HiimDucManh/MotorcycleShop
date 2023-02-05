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
using System.Windows.Shapes;

namespace FinalProject.View
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : Window
    {
        public SignUpView()
        {
            InitializeComponent();
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
            btnClose.Background = Brushes.Red;
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.Background = Brushes.Transparent;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
