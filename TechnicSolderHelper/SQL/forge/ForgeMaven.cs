using System.Collections.Generic;

namespace TechnicSolderHelper.SQL.Forge
{
    public class ForgeMaven
    {

        public Dictionary<int, Number> Number { get; set; }

        public string WebPath { get; set; }
    }

    public class Number
    {

        public int Build { get; set; }

        public string JobVersion { get; set; }

        public string McVersion { get; set; }

        public string Version { get; set; }

        public string DownloadUrl { get; set; }

        public string Branch { get; set; }
    }

}
