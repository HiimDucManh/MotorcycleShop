using FinalProject.Model;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FinalProject.View.Customer
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        int i = 2;
        KHACHHANG khachhang;

        public HomePage(KHACHHANG kh)
        {
            InitializeComponent();
            khachhang = kh;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                imagerotator();
            }));
        }

        private void imagerotator()
        {
            Storyboard myStoryboard2 = new Storyboard();

            myStoryboard2.SpeedRatio = 5;

            var fadein = new DoubleAnimation()
            {
                From = 1,
                To = 1,
                Duration = TimeSpan.FromSeconds(2),
            };

            Storyboard.SetTarget(fadein, imgframe);
            Storyboard.SetTargetProperty(fadein, new PropertyPath(ImageBrush.OpacityProperty));

            var sb = new Storyboard();
            sb.Children.Add(fadein);
            sb.Completed += new EventHandler(sb_Completed0);
            sb.Begin();
        }

        private void sb_Completed0(object sender, EventArgs e)
        {
            Storyboard myStoryboard2 = new Storyboard();
            myStoryboard2.SpeedRatio = 5;

            var fadein = new DoubleAnimation()
            {
                From = 1,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5),
            };

            Storyboard.SetTarget(fadein, imgframe);
            Storyboard.SetTargetProperty(fadein, new PropertyPath(ImageBrush.OpacityProperty));
            var sb = new Storyboard();
            sb.Children.Add(fadein);
            sb.Completed += new EventHandler(sb_Completed);
            sb.Begin();

        }

        private void sb_Completed(object sender, EventArgs e)
        {
            string strUri2 = String.Format(@"pack://application:,,,/FinalProject;component/Assets/Layout/StoryBoard/{0}.png", i.ToString());
            i++;
            if (i > 8)//number of pictures
            {
                i = 1;
            }
            imgframe.Source = new BitmapImage(new Uri(strUri2));
            Storyboard myStoryboard2 = new Storyboard();

            myStoryboard2.SpeedRatio = 5;
            var fadein = new DoubleAnimation()
            {
                From = 1,
                To = 1,
                Duration = TimeSpan.FromSeconds(.5),
            };
            Storyboard.SetTarget(fadein, imgframe);
            Storyboard.SetTargetProperty(fadein, new PropertyPath(ImageBrush.OpacityProperty));
            var sb = new Storyboard();
            sb.Children.Add(fadein);
            sb.Begin();
            imagerotator();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ListMotorcyclePage(khachhang));
        }
    }
}
