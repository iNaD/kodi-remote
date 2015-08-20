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

        public async Task<JsonValue> POST(String url, String content)
        {
            Debug.WriteLine("Sending POST request to: " + url);

            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.PostAsync(new Uri(url), new StringContent(content, Encoding.UTF8, "application/json"));
            var data = await response.Content.ReadAsStringAsync();

            Debug.WriteLine("Received response");

            return JsonValue.Parse(data);
        }

    }
}
