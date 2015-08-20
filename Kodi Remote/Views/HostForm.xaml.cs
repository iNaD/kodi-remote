using System;
using System.Diagnostics;
using Windows.Data.Json;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Kodi_Remote.Views
{
    public sealed partial class HostForm : Page
    {

        public HostForm()
        {
            Debug.WriteLine("Initializing HostForm");
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Host host = e.Parameter as Host;

            if (host != null)
            {
                this.hostname.Text = host.hostname;
                this.port.Text = host.port;
                this.username.Text = host.username;
                this.password.Password = host.password;
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
            string password = this.password.Password;

            var host = new Host(hostname, port, username, password);

            try {
                var command = new Kodi.TestConnection(host);
                await command.Fire();
                if(command.Ok())
                {
                    ShowMessage("Connection successfull\nVersion: " + command.Result().ToString());
                    return;
                }

                ShowMessage("Target seems not to be an instance of Kodi/XBMC.");

            } catch(Exception)
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
            Host host = new Host(this.hostname.Text, this.port.Text, this.username.Text, this.password.Password);

            Settings.AddHost(host);

            Settings.Save();
        }
    }
}
