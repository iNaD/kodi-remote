using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
            this.InitializeComponent();
        }

        private void setHost(Host host)
        {
            this.host = host;
            this.label.Text = host.label;
            this.hostname.Text = host.hostname;
            this.port.Text = host.port;
            this.username.Text = host.username;
            this.password.Password = host.password;
            this.isDefault.IsOn = host.isDefault;

            this.delete.Visibility = Visibility.Visible;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Host host = e.Parameter as Host;

            if (host != null)
            {
                this.setHost(host);
            }
        }

        private async Task<bool> TestConnection()
        {
            string hostname = this.hostname.Text.Trim();
            string port = this.port.Text.Trim();
            string username = this.username.Text.Trim();
            string password = this.password.Password.Trim();

            var host = new Host(hostname, port, username, password);

            try {
                var command = new Kodi.TestConnection(host);
                await command.Fire();
                if(command.Ok())
                {
                    return true;
                }

                Helpers.ShowMessage("Target seems not to be an instance of Kodi/XBMC.");

            } catch(Exception)
            {
                Helpers.ShowMessage("Failed to connect. Please review your provided data.");
            }

            return false;
        }

        private async void save_Click(object sender, RoutedEventArgs e)
        {
            if (this.Validate() == false)
            {
                return;
            }

            if(await this.TestConnection() == false)
            {
                return;
            }

            if (this.host != null)
            {
                Settings.hosts.Remove(this.host);
            }

            var host = new Host(this.label.Text.Trim(), this.hostname.Text.Trim(), this.port.Text.Trim(), this.username.Text, this.password.Password.Trim());
            host.isDefault = this.isDefault.IsOn;

            Settings.AddHost(host);

            Settings.Save();

            this.setHost(host);

            Helpers.ShowMessage("Host saved.");
        }

        private bool Validate()
        {
            var hostname = this.hostname.Text.Trim();

            if (hostname.Length == 0)
            {
                Helpers.ShowMessage("The hostname is required.");
                return false;
            }

            bool isDefault = this.isDefault.IsOn;

            if(isDefault == true && Settings.DifferentDefaultHostExists(this.host) == true)
            {
                Helpers.ShowMessage("Only one host could be set as default host. If you want to make this host the default host, change \"" + Settings.DefaultHost().label + "\" first.");
                return false;
            }

            return true;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if(this.host != null)
            {
                Settings.hosts.Remove(this.host);

                Settings.Save();

                this.host = null;
            }

            AppShell.Current.AppFrame.Navigate(typeof(HostListing));
        }
    }
}
