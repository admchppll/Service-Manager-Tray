using ServiceClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ServiceManager
{
    /// <summary>
    /// Interaction logic for ServiceSettingWindow.xaml
    /// </summary>
    public partial class ServiceSettingWindow : Window
    {
        private static ObservableCollection<Service> allServicesData;
        public static ObservableCollection<Service> AllServicesData { get => allServicesData; set => allServicesData = value; }

        public ServiceSettingWindow()
        {
            InitializeComponent();

            AllServicesData = Service.GetAllServices();
            servicesList.ItemsSource = AllServicesData;
        }

        private void serviceFilter_KeyUp(object sender, KeyEventArgs e)
        {
            //Search case insensitive
            string input = ((TextBox)sender).Text.ToLowerInvariant();

            //if there is no text to filter by return all services
            if (string.IsNullOrWhiteSpace(input))
            {
                servicesList.ItemsSource = AllServicesData;
            }
            else
            {
                servicesList.ItemsSource = AllServicesData
                    .Where(s => s.Name.ToLowerInvariant().Contains(input) == true
                        || s.MachineName.ToLowerInvariant().Contains(input) == true);
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
