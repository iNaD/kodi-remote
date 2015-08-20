using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote.Kodi
{
    class TestConnection : ICommand
    {
        private readonly Host host;
        private IJsonValue response;
 
        public TestConnection(Host host)
        {
            this.host = host;
        }

        public async Task<IJsonValue> Fire()
        {
            var properties = new List<String>
            {
                "version"
            };
            var parameters = API.Parameters(API.Parameter("properties", properties));
            var json = API.Init("Application.GetProperties", parameters);

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
                    IJsonValue version;
                    if (result.GetObject().TryGetValue("version", out version))
                    {
                        return version;
                    }
                }
            }

            return null;
        }
    }
}
