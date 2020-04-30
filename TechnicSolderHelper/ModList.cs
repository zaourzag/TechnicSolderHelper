using System.Collections.Generic;

namespace TechnicSolderHelper
{
    public class ModList
    {
        public string ModId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }

        public string McVersion { get; set; }

        public string Url { get; set; }

        public List<string> Authors { get; set; }
    }
}