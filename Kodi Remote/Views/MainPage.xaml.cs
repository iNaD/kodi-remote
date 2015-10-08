using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            if(Helpers.DefaultHostRequired() == false)
            {
                return;
            }

            var command = new Kodi.Mute(Settings.DefaultHost());
            await command.Fire();

            if(command.Ok())
            {
                if((bool) command.Result() == true)
                {
                    Helpers.ShowMessage("Muted");
                } else if((bool)command.Result() == false)
                {
                    Helpers.ShowMessage("Unmuted");
                } else
                {
                    Helpers.ShowMessage("Unknown response");
                }
            } else
            {
                Helpers.ShowMessage("Toggle Mute failed");
            }
        }

        private async void sendToKodi_Click(object sender, RoutedEventArgs e)
        {
            if (Helpers.DefaultHostRequired() == false)
            {
                return;
            }

            var command = new Kodi.YouTube(Settings.DefaultHost(), this.link.Text);
            await command.Fire();

            if (command.Ok() && (bool)command.Result())
            {
                Helpers.ShowMessage("Playing video");
            }

            Helpers.ShowMessage("Couldn't play video");
        }

    }
}