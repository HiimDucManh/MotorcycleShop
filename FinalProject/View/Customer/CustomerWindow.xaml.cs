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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalProject.View.Customer
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
            PagesNavigation.Navigate(new HomePage());
            ListViewModels.ItemsSource = DataProvider.Ins.DB.LOAISPs.ToList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (btnMenu.IsChecked == true)
            {
                ResetMenu();
            }
        }

        private void tgbModels_Click(object sender, RoutedEventArgs e)
        {
            gridService.Visibility = Visibility.Collapsed;
            gridListModels.Visibility = Visibility.Visible;    
        }

        private void tgbServices_Click(object sender, RoutedEventArgs e)
        {
            gridListModels.Visibility = Visibility.Collapsed;
            gridService.Visibility = Visibility.Visible;          
        }

        private void rdbHome_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new HomePage());

            btnMenu.IsChecked = false;
            ResetMenu();

            if (ListViewModels.SelectedIndex == -1)
                return;

            for (int i = 0; i < ListViewModels.Items.Count; i++)
            {
                ListBoxItem currentListBoxItem = ListViewModels.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                // Iterate whole listbox tree and search for this items
                TextBlock nameBlock = FindDescendant<TextBlock>(currentListBoxItem);

                var bc = new BrushConverter();
                nameBlock.Foreground = (Brush)bc.ConvertFrom("#BFBFC4");
            }    
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

                    // Iterate whole listbox tree and search for this items
                    TextBlock nameBlock = FindDescendant<TextBlock>(currentSelectedListBoxItem);

                    var bc = new BrushConverter();
                    nameBlock.Foreground = (Brush)bc.ConvertFrom("#E32431");

                    LOAISP lsp = (LOAISP)ListViewModels.SelectedItem;
                    PagesNavigation.Navigate(new DetailMotorcyclePage(lsp.MALOAI));
                    btnMenu.IsChecked = false;
                    ResetMenu();
                }
                else
                {

                    ListBoxItem currentListBoxItem = ListViewModels.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

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

        private void ResetMenu()
        {
            rdbHome.IsChecked = false;
            rdbModels.IsChecked = false;
            rdbServices.IsChecked = false;
            gridListModels.Visibility = Visibility.Collapsed;
            gridService.Visibility = Visibility.Collapsed;
        }

        private void tgbPriceMaintenance_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new PriceServicePage());

            btnMenu.IsChecked = false;
            ResetMenu();
        }

        private void tgbMaintenance_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new MaintenanceServicePage());

            btnMenu.IsChecked = false;
            ResetMenu();
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PagesNavigation.Navigate(new AccountSettingsPage());

            btnMenu.IsChecked = false;
            ResetMenu();
        }
    }
}
