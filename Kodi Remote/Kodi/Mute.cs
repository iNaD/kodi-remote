using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote.Kodi
{
    class Mute : ICommand
    {
        private readonly Host host;
        private IJsonValue response;


        public Mute(Host host)
        {
            this.host = host;
        }

        public async Task<IJsonValue> Fire()
        {
            var parameters = API.Parameters(API.Parameter("mute", "toggle"));
            var json = API.Init("Application.SetMute", parameters);

            this.response = await API.Request(this.host, json);

            return this.response;
        }

        public bool Ok()
        {
            if (this.Result() != null)
            {
                return true;
            }

            return false;
        }

        public object Result()
        {
            if (this.response != null)
            {
                IJsonValue result;
                if (this.response.GetObject().TryGetValue("result", out result))
                {
                    return result.GetBoolean();
                }
            }

            return null;
        }

    }
}
