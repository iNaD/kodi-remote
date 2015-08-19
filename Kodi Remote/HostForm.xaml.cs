using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

namespace Kodi_Remote
{
    public sealed partial class HostForm : Page
    {

        public HostForm()
        {
            this.InitializeComponent();
            if (Settings.hosts.Count > 0) {
                Host host = Settings.hosts.ElementAt(0);
                this.hostname.Text = host.hostname;
                this.port.Text = host.port;
                this.username.Text = host.username;
                this.password.Text = host.password;
            }
        }

        private async void connect_Click(object sender, RoutedEventArgs e)
        {
            string hostname = this.hostname.Text;

            if(hostname.Length == 0)
            {
                ShowMessage("The hostname is required.");
                return;
            }            

            string port = this.port.Text;
            string username = this.username.Text;
            string password = this.password.Text;

            var host = new Host(hostname, port, username, password);

            try {
                var result = await host.request("");

                IJsonValue version;
                if (result.GetObject().TryGetValue("version", out version))
                {
                    Debug.WriteLine("Version: " + version.ToString());
                    ShowMessage("Connection successfull\nVersion: " + version.ToString());
                }
                else
                {
                    ShowMessage("Target seems not to be an instance of Kodi/XBMC.");
                }
            } catch(HttpRequestException exception)
            {
                ShowMessage("Failed to connection. Please review your provided data.");
            }
        }

        private async void ShowMessage(string message)
        {
            MessageDialog messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            Host host = new Host(this.hostname.Text, this.port.Text, this.username.Text, this.password.Text);

            Settings.AddHost(host);

            Settings.Save();
        }
    }
}
