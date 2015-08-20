using System;
using System.Collections.Generic;
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

namespace Kodi_Remote
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class HostListing : Page
    {
        private List<ListViewItem> hosts;

        public HostListing()
        {
            this.InitializeComponent();

            this.hosts = new List<ListViewItem>();

            foreach(Host host in Settings.hosts)
            {
                var item = new ListViewItem();
                item.Content = host.hostname;
                hosts.Add(item);
            }

            this.HostsList.ItemsSource = this.hosts;
        }
    }
}
