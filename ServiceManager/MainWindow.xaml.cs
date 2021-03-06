﻿using ServiceManagement.Core.Interfaces;
using ServiceManagement.Core.Models;
using ServiceManager.Startup;
using ServiceManagement.Data.Repositories.Interfaces;
using System;
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
    public partial class MainWindow : Window, IToggleWindow
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IWatchedServiceRepository _watchedServiceRepository;
        private static ObservableCollection<Service> servicesData;

        public static ObservableCollection<Service> ServicesData
        {
            get => servicesData;
            set => servicesData = value;
        }

        public MainWindow(IServiceRepository serviceRepository,
            IWatchedServiceRepository watchedServiceRepository)
        {
            _serviceRepository = serviceRepository;
            _watchedServiceRepository = watchedServiceRepository;

            ShowInTaskbar = false;

            InitializeComponent();

            //Place Bottom Right of Screen
            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - Width;
            Top = desktopWorkingArea.Bottom - Height;
            Topmost = true;

            //ServicesData = new ObservableCollection<Service>(_serviceRepository.GetAllServices().Result);
            ServicesData = new ObservableCollection<Service>(_watchedServiceRepository.GetWatchedServices().Result);

            servicesList.ItemsSource = ServicesData;
        }

        public Action<object, EventArgs> GetWindowToggleAction()
        {
            return delegate (object sender, EventArgs args)
            {
                if (!IsVisible)
                {
                    Show();
                }
                else
                    Hide();

                WindowState = WindowState.Normal;
            };
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Service selected = (Service)((ContentControl)sender).Content;
            //selected.Toggle();
        }

        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Application.Current.Windows.OfType<ServiceSettingWindow>().Any())
            {
                var setting = UnityBootstrapper.Resolve<ServiceSettingWindow>();
                setting.Show();
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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