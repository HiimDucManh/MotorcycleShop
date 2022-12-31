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

namespace FinalProject.View.Customer
{
    /// <summary>
    /// Interaction logic for PriceServicePage.xaml
    /// </summary>
    public partial class PriceServicePage : Page
    {
        string name = String.Empty;
        public PriceServicePage()
        {
            InitializeComponent();

            ListViewModels.ItemsSource = DataProvider.Ins.DB.LOAISPs.ToList();
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

        private void ListViewModels_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ListViewModels.SelectedIndex == -1)
                return;

            for (int i = 0; i < ListViewModels.Items.Count; i++)
            {
                if (ListViewModels.SelectedIndex == i)
                {
                    ListBoxItem currentSelectedListBoxItem = ListViewModels.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                    if (currentSelectedListBoxItem == null)
                        return;

                    TextBlock nameBlock = FindDescendant<TextBlock>(currentSelectedListBoxItem);           
                    LOAISP lsp = (LOAISP)ListViewModels.SelectedItem;

                    if (name != nameBlock.Text)
                    {
                        var bc = new BrushConverter();
                        nameBlock.Foreground = (Brush)bc.ConvertFrom("#E32431");

                        gridService.ColumnDefinitions[1].Width = new GridLength(1.0, GridUnitType.Star);
                        ImagePagesNavigation.Navigate(new ImagePriceService(lsp.IMGDV1));
                        name = nameBlock.Text;
                    }
                    else
                    {
                        var bc = new BrushConverter();
                        nameBlock.Foreground = (Brush)bc.ConvertFrom("#434344");

                        gridService.ColumnDefinitions[1].Width = new GridLength(0.0, GridUnitType.Star);
                        name = String.Empty;
                    }
                }
                else
                {

                    ListBoxItem currentListBoxItem = ListViewModels.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                    // Iterate whole listbox tree and search for this items
                    TextBlock nameBlock = FindDescendant<TextBlock>(currentListBoxItem);

                    var bc = new BrushConverter();
                    nameBlock.Foreground = (Brush)bc.ConvertFrom("#434344");
                }
            }
        }
    }
}
