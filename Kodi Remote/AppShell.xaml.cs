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
    public sealed partial class AppShell : Page
    {
        private List<SidebarNavItem> navlist = new List<SidebarNavItem>(
            new[]
            {
                new SidebarNavItem()
                {
                    Label = "Home",
                    Symbol = Symbol.Home,
                    DestPage = typeof(MainPage)
                },
                new SidebarNavItem()
                {
                    Label = "Hosts",
                    Symbol = Symbol.Setting,
                    DestPage = typeof(HostListing)
                }
            });

        private Page current = null;

        public AppShell()
        {
            this.InitializeComponent();

            this.Navigation.ItemsSource = this.navlist;
        }

        public Frame AppFrame { get { return this.content; } }

    }
}
