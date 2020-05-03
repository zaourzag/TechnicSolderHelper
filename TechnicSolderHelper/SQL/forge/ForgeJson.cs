using System;
using System.Collections.Generic;

namespace TechnicSolderHelper.SQL.Forge
{
    public class ForgeJson : List<ForgeVersionInfo>
    {
        public string WebPath { get; set; }
    }

    public class ForgeVersionInfo
    {
        public string name { get; set; }

        public string gameVersion { get; set; }

        public bool latest { get; set; }

        public bool recommended { get; set; }

        public int Build { get; set; }

        //public string JobVersion { get; set; }

        public string Version { get; set; }

        public string DownloadUrl { get; set; }

        //public string Branch { get; set; }
    }

}
