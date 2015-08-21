using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kodi_Remote.Views
{
    public sealed partial class HostListing : Page
    {

        public HostListing()
        {
            this.InitializeComponent();

            this.HostsList.ItemsSource = Settings.hosts;
        }

        private void HostsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            AppShell.Current.AppFrame.Navigate(typeof(HostForm), (Host)e.ClickedItem);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AppShell.Current.AppFrame.Navigate(typeof(HostForm));
        }
    }
}
