using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var command = new Kodi.Mute(new Host("osmc"));
            var result = await command.fire();

            ShowMessage(result.ToString());
        }
        private async void ShowMessage(string message)
        {
            MessageDialog messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }

        private async void sendToKodi_Click(object sender, RoutedEventArgs e)
        {
            var command = new Kodi.YouTube(new Host("osmc"), this.link.Text);
            var result = await command.fire();

            Debug.WriteLine(result.ToString());

            IJsonValue resultStatus;
            if (result.GetObject().TryGetValue("result", out resultStatus))
            {
                if (resultStatus.GetString() == "OK")
                {
                    ShowMessage("Video wird abgespielt.");
                } else
                {
                    ShowMessage("Video konnte nicht abgespielt werden.");
                }
            }
            else
            {
                ShowMessage("Video konnte nicht abgespielt werden.");
            }

        }
    }
}
