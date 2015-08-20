using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote
{
    [DataContract]
    class Host
    {
        private JSONRPCClient rpcClient;
        private string url;

        public Host(string hostname, string port, string username, string password)
        {
            this.hostname = hostname;
            this.SetPort(port);
            this.username = username;
            this.password = password;
            this.Init();
        }

        public Host(string hostname, string port)
        {
            this.hostname = hostname;
            this.username = "";
            this.password = "";
            this.SetPort(port);
            this.Init();
        }

        public Host(string hostname)
        {
            this.hostname = hostname;
            this.username = "";
            this.password = "";
            this.SetPort("80");
            this.Init();
        }

        private void Init()
        {
            this.rpcClient = new JSONRPCClient();

            this.GenerateUrl();
        }

        private void GenerateUrl()
        {
            if (this.hostname.Length == 0)
            {
                throw new Exception("Hostname is required.");
            }

            this.url = "http://";

            if (this.username.Length > 0 && this.password.Length > 0)
            {
                this.url += this.username + ":" + this.password + "@";
            }

            this.url += this.hostname + ":" + this.port;
            this.url += "/" + Settings.endpoint;
        }

        private void SetPort(string port)
        {
            if (port.Length == 0)
            {
                this.port = "80";
            }
            else
            {
                this.port = port;
            }
        }

        [DataMember]
        public string hostname { get; private set; }

        [DataMember]
        public string port { get; private set; }

        [DataMember]
        public string username { get; private set; }

        [DataMember]
        public string password { get; private set; }

        public string ToJson()
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Host));

            serializer.WriteObject(stream, this);

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        public async Task<JsonValue> request(String request)
        {
            return await this.rpcClient.POST(this.url, request);
        }

        public static Host FromJson(string json)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Host));
            StreamWriter writer = new StreamWriter(stream);

            writer.Write(json);

            stream.Position = 0;

            return (Host)serializer.ReadObject(stream);
        }

        public override string ToString()
        {
            return base.ToString() + ": " + this.ToJson();
        }
    }
}
