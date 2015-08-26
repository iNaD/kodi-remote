using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Kodi_Remote
{
    public class HostListItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate Template { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item != null && item is Host)
            {
                Host host = item as Host;

                if(host.isDefault == true)
                {
                    return DefaultTemplate;
                } else
                {
                    return Template;
                }
            }

            return null;
        }
    }
}
