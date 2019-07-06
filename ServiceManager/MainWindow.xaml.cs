using ServiceManagement.Core.Models;
using ServiceManagement.Core.Repositories;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ServiceManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceRepository _serviceRepository;
        private static ObservableCollection<Service> servicesData;

        public static ObservableCollection<Service> ServicesData
        {
            get => servicesData; set => servicesData = value;
        }

        public MainWindow(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;

            ShowInTaskbar = false;

            InitializeComponent();

            //Place Bottom Right of Screen
            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width;
            Top = desktopWorkingArea.Bottom - Height;
            Topmost = true;

            ServicesData = new ObservableCollection<Service>(_serviceRepository.GetAllServices().Result);

            servicesList.ItemsSource = ServicesData;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Service selected = (Service)((ContentControl)sender).Content;
            //selected.Toggle();
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!System.Windows.Application.Current.Windows.OfType<ServiceSettingWindow>().Any())
            {
                ServiceSettingWindow setting = new ServiceSettingWindow();
                setting.Show();
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void toggleBtn_Click(object sender, RoutedEventArgs e)
        {
            Service selected = (Service)((ContentControl)sender).DataContext;
            //selected.Toggle();
            RefreshServiceList();
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshServiceList();
        }

        public void RefreshServiceList()
        {
            //Service.UpdateStatus(ref servicesData);
            servicesList.ItemsSource = ServicesData;
            servicesList.Items.Refresh();
        }
    }
}