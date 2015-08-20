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

namespace Kodi_Remote.Views
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
            await command.Fire();

            if(command.Ok())
            {
                if((bool) command.Result() == true)
                {
                    ShowMessage("Muted");
                } else if((bool)command.Result() == false)
                {
                    ShowMessage("Unmuted");
                } else
                {
                    ShowMessage("Unknown response");
                }
            } else
            {
                ShowMessage("Toggle Mute failed");
            }
        }
        private async void ShowMessage(string message)
        {
            MessageDialog messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }

        private async void sendToKodi_Click(object sender, RoutedEventArgs e)
        {
            var command = new Kodi.YouTube(new Host("osmc"), this.link.Text);
            await command.Fire();

            if (command.Ok() && (bool)command.Result())
            {
                ShowMessage("Playing video");
            }

             ShowMessage("Couldn't play video");
        }
    }
}
