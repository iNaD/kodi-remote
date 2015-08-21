using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote.Kodi
{
    static class API
    {

        private static readonly string jsonrpcVersion = "2.0";
        private static readonly string defaultId = "1";

        public static Dictionary<string, object> Init(string method)
        {
            return Init(method, new Dictionary<string, object> { });
        }

        public static Dictionary<string, object> Init(string method, Dictionary<string, object> parameters)
        {
            return Init(method, parameters, defaultId);
        }

        public static Dictionary<string, object> Init(string method, Dictionary<string, object> parameters, string id)
        {
            return new Dictionary<string, object>
            {
                { "id", id },
                { "jsonrpc", jsonrpcVersion },
                { "method", method },
                { "params", parameters }
            };
        }

        public static Dictionary<string, object> Parameters(params KeyValuePair<string, object>[] parameters)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            foreach(KeyValuePair<string, object> parameter in parameters)
            {
                dict.Add(parameter.Key, parameter.Value);
            }

            return dict;
        }

        public static KeyValuePair<string, object> Parameter(string key, object value)
        {
            return new KeyValuePair<string, object>(key, value);
        }

        public static string ToJson(object dict)
        {
            return JsonConvert.SerializeObject(dict);
        }

        public static async Task<IJsonValue> Request(Host host, object json)
        {
            string request = API.ToJson(json);

            Debug.WriteLine(request);

            return await host.request(request);
        }

    }
}
