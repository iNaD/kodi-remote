using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Kodi_Remote.Views
{
    public sealed partial class HostListing : Page
    {
        private List<HostListItem> hosts;

        public HostListing()
        {
            this.InitializeComponent();

            this.hosts = new List<HostListItem>();

            foreach(Host host in Settings.hosts)
            {
                var item = new HostListItem()
                {
                    Host = host
                };
                    
                hosts.Add(item);
            }

            this.HostsList.ItemsSource = this.hosts;
        }

        private void HostsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            HostListItem item = (HostListItem) e.ClickedItem;
            AppShell.Current.AppFrame.Navigate(typeof(HostForm), item.Host);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AppShell.Current.AppFrame.Navigate(typeof(HostForm));
        }
    }
}
