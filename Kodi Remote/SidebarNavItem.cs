using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Kodi_Remote
{
    public class SidebarNavItem
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }
        public char SymbolAsChar
        {
            get
            {
                return (char)this.Symbol;
            }
        }

        public Type DestPage { get; set; }
        public object Arguments { get; set; }

        public SidebarNavItem()
        {

        }
    }
}
