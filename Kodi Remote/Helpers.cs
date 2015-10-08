using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Kodi_Remote
{
    static class Helpers
    {
        public static async void ShowMessage(string message)
        {
            MessageDialog messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }

        public static async void ShowMessage(string message, string title)
        {
            MessageDialog messageDialog = new MessageDialog(message, title);
            await messageDialog.ShowAsync();
        }

        public static bool DefaultHostRequired()
        {
            if(Settings.HasDefaultHost() == false)
            {
                ShowMessage("A default host is required. Please specify one in the Hosts section.");
                return false;
            }

            return true;
        }

    }
}
