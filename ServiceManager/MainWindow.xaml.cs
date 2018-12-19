using ServiceClassLibrary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace ServiceManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ObservableCollection<Service> servicesData;

        public static ObservableCollection<Service> ServicesData { get => servicesData; set => servicesData = value; }

        public MainWindow()
        {
            InitializeComponent();

            //Place Bottom Right of Screen
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;

            //Set data
            //ServicesData = Service.DeserializeFromFile();
            ServicesData = Service.GetAllServices();
            Service.UpdateStatus(ref servicesData);
            servicesList.ItemsSource = ServicesData;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Service selected = (Service)((ContentControl)sender).Content;
            selected.Toggle();
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
            selected.Toggle();
            RefreshServiceList();
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            RefreshServiceList();
        }

        public void RefreshServiceList()
        {
            Service.UpdateStatus(ref servicesData);
            servicesList.ItemsSource = ServicesData;
            servicesList.Items.Refresh();
        }
    }
}
