using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Kodi_Remote
{
    static class Settings
    {
        private static StorageFolder localFolder;
        private static ApplicationDataContainer localSettings;

        public static readonly string endpoint = "jsonrpc";

        public static void Init()
        {
            localSettings = ApplicationData.Current.LocalSettings;
            localFolder = ApplicationData.Current.LocalFolder;
            hosts = new HashSet<Host>();
            LoadHosts();
        }

        public static HashSet<Host> hosts { get; private set; }

        public static bool AddHost(Host host)
        {
            return hosts.Add(host);
        }

        private static void LoadHosts()
        {
            object json;

            if(localSettings.Values.TryGetValue("hosts", out json))
            {
                MemoryStream stream = new MemoryStream();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HashSet<Host>));
                StreamWriter writer = new StreamWriter(stream);

                writer.AutoFlush = true;

                writer.Write((string)json);

                stream.Position = 0;

                hosts = (HashSet<Host>)serializer.ReadObject(stream);
            }

        }
        
        private static string HostsToJson()
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HashSet<Host>));
            serializer.WriteObject(stream, hosts);

            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        private static void SaveHosts()
        {
            localSettings.Values["hosts"] = HostsToJson();
        }

        public static void Save()
        {
            SaveHosts();
        }
    }
}
