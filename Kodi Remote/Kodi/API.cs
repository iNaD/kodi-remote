using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote.Kodi
{
    static class API
    {

        private static readonly string jsonrpcVersion = "2.0";
        
        public static string Build(string method, JsonObject parameters, string id)
        {
            return _Build(method, parameters, id);
        }

        public static string Build(string method, JsonObject parameters)
        {
            return _Build(method, parameters, "1");
        }

        private static string _Build(string method, JsonObject parameters, string id)
        {
            JsonObject json = new JsonObject();
            json.Add("jsonrpc", JsonValue.CreateStringValue(jsonrpcVersion));
            json.Add("method", JsonValue.CreateStringValue(method));
            json.Add("params", parameters);
            json.Add("id", JsonValue.CreateStringValue(id));

            return json.ToString();
        }

        public static KeyValuePair<string, IJsonValue> Parameter(string key, IJsonValue value)
        {
            return new KeyValuePair<string, IJsonValue>(key, value);
        }

        public static KeyValuePair<string, IJsonValue> Parameter(string key, string value)
        {
            return new KeyValuePair<string, IJsonValue>(key, JsonValue.CreateStringValue(value));
        }

        public static JsonObject Parameters(params KeyValuePair<string,IJsonValue>[] parameters)
        {
            JsonObject json = new JsonObject();

            foreach(KeyValuePair<string, IJsonValue> parameter in parameters)
            {
                json.Add(parameter.Key, parameter.Value);
            }

            return json;
        }

    }
}
