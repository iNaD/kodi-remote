using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace Kodi_Remote
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly string endpoint = "jsonrpc";

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            string hostname = this.hostname.Text;
            if(hostname.Length == 0)
            {
                ShowMessage("The hostname is required.");
                return;
            }

            string port = this.port.Text;
            if(port.Length == 0)
            {
                port = "80";
            }

            string username = this.username.Text;
            string password = this.password.Text;

            string url = "http://";

            if(username.Length > 0 && password.Length > 0)
            {
                url += username + ":" + password + "@";
            }

            url += hostname + ":" + port;
            url += "/" + endpoint;

            TryConnection(url);
        }

        private async void TryConnection(string url)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(new Uri(url));
            var result = await response.Content.ReadAsStringAsync();

            ShowMessage(result);
        }

        private async void ShowMessage(string message)
        {
            MessageDialog messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
    }
}
