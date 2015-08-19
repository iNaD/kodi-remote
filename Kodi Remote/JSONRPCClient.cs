using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote
{
    class JSONRPCClient
    {
        private readonly HttpClient httpClient;

        public JSONRPCClient()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<JsonValue> GET(String url)
        {
            Debug.WriteLine("Sending GET request to: " + url);

            var response = await httpClient.GetAsync(new Uri(url));
            var data = await response.Content.ReadAsStringAsync();

            Debug.WriteLine("Received response");

            return JsonValue.Parse(data);
        }

    }
}
