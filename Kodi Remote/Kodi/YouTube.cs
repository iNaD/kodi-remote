using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Kodi_Remote.Kodi
{
    class YouTube : Command
    {
        private readonly Host host;

        private String videoId;

        public YouTube(Host host, String link)
        {
            this.host = host;
            this.videoId = ParseLink(link);
            Debug.WriteLine("Video ID: " + this.videoId);
        }

        public async override Task<JsonValue> fire()
        {
            string request = API.Build("Player.Open", API.Parameters(API.Parameter("item", API.Parameter("file", "plugin://plugin.video.youtube/?action=play_video&videoid=" + this.videoId))));

            return await this.host.request(request);
        }

        private String ParseLink(String link)
        {
            var regex = Regex.Match(link, @"(?:https?:\/\/)?(?:www\.)?youtu(?:.be\/|be\.com\/watch\?v=|be\.com\/v\/)(.{8,})");

            return regex.Groups[1].Value;
        }

    }
}
