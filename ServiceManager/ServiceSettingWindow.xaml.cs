using ServiceClassLibrary;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ServiceManager
{
    /// <summary>
    /// Interaction logic for ServiceSettingWindow.xaml
    /// </summary>
    public partial class ServiceSettingWindow : Window
    {
        private static ObservableCollection<Tuple<Service, bool>> allServicesData;

        public static ObservableCollection<Tuple<Service, bool>> AllServicesData
        {
            get => allServicesData; set => allServicesData = value;
        }

        public ServiceSettingWindow()
        {
            InitializeComponent();
            InitializeServicesList();
            servicesList.ItemsSource = AllServicesData;
        }

        /// <summary>
        /// Initialize list of all services
        /// </summary>
        private void InitializeServicesList()
        {
            AllServicesData = new ObservableCollection<Tuple<Service, bool>>();

            foreach (Service service in Service.GetAllServices())
            {
                AllServicesData.Add(Tuple.Create(service, IsMonitored(service.MachineName)));
            }
        }

        private bool IsMonitored(string machineName)
        {
            return MainWindow.ServicesData.Any(s => s.MachineName == machineName);
        }

        /// <summary>
        /// Search through the service list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serviceFilter_KeyUp(object sender, KeyEventArgs e)
        {
            //Search case insensitive
            string input = ((TextBox) sender).Text.ToLowerInvariant();

            //if there is no text to filter by return all services
            if (string.IsNullOrWhiteSpace(input))
            {
                servicesList.ItemsSource = AllServicesData;
            }
            else
            {
                servicesList.ItemsSource = AllServicesData
                    .Where(s => s.Item1.Name.ToLowerInvariant().Contains(input) == true
                        || s.Item1.MachineName.ToLowerInvariant().Contains(input) == true);
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ServiceChecked(object sender, RoutedEventArgs e)
        {
        }

        private void ServiceUnchecked(object sender, RoutedEventArgs e)
        {
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}