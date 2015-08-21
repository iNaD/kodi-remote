using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote.Kodi
{
    class YouTube : ICommand
    {
        private readonly Host host;
        private IJsonValue response;

        private String videoId;

        public YouTube(Host host, String link)
        {
            this.host = host;
            this.videoId = ParseLink(link);
            Debug.WriteLine("Video ID: " + this.videoId);
        }

        private String ParseLink(String link)
        {
            var regex = Regex.Match(link, @"(?:https?:\/\/)?(?:www\.)?youtu(?:.be\/|be\.com\/watch\?v=|be\.com\/v\/)(.{8,})");

            return regex.Groups[1].Value;
        }

        public async Task<IJsonValue> Fire()
        {
            var file = "plugin://plugin.video.youtube/?action=play_video&videoid=" + this.videoId;
            var parameters = API.Parameters(API.Parameter("item", API.Parameter("file", file)));
            var json = API.Init("Player.Open", parameters);

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
                    if (result.GetString() == "OK")
                    {
                        return true;
                    }
                }
            }

            return null;
        }

    }
}
