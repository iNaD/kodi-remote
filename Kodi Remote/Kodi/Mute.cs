using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote.Kodi
{
    class Mute : Command
    {
        private readonly Host host;

        public Mute(Host host)
        {
            this.host = host;
        }

        override public async Task<JsonValue> fire()
        {
            string request = API.Build("Application.SetMute", API.Parameters(API.Parameter("mute", "toggle")));

            return await this.host.request(request);
        }

    }
}
