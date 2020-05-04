using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TechnicSolderHelper.SQL.liteloader
{
    public class LiteloaderJson
    {
        public Meta Meta { get; set; }

        public Dictionary<string, Versions> Versions { get; set; }
    }

    public class Meta
    {
        public string Description { get; set; }

        public string Authors { get; set; }

        public string Url { get; set; }
    }

    public class Versions
    {
        public LiteloaderReleases Artefacts { get; set; }

        public LiteloaderReleases Snapshots { get; set; }
    }

    public class LiteloaderReleases
    {
        [JsonProperty("com.mumfrey:liteloader")]
        public Dictionary<string, LiteloaderVersionInfo> LiteloaderVersions;
    }

    public class LiteloaderVersionInfo
    {
        public string File { get; set; }

        public string Version { get; set; }

        public string Md5 { get; set; }

        public string McVersion { get; set; }

        [OnError]
        internal void OnError(StreamingContext context, ErrorContext errorContext)
        {
            errorContext.Handled = true;
        }
    }
}
