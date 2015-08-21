using System;
using System.Diagnostics;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Kodi_Remote.Views
{
    public sealed partial class HostForm : Page
    {

        private Host host;

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
                this.host = host;
                this.label.Text = host.label;
                this.hostname.Text = host.hostname;
                this.port.Text = host.port;
                this.username.Text = host.username;
                this.password.Password = host.password;
                this.delete.Visibility = Visibility.Visible;
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
            if(this.host != null)
            {
                Settings.hosts.Remove(this.host);
            }

            this.host = new Host(this.label.Text, this.hostname.Text, this.port.Text, this.username.Text, this.password.Password);

            Settings.AddHost(this.host);

            Settings.Save();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if(this.host != null)
            {
                Settings.hosts.Remove(this.host);

                Settings.Save();
            }

            AppShell.Current.AppFrame.Navigate(typeof(HostListing));
        }
    }
}
