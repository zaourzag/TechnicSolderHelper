using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TechnicSolderHelper.SQL;

namespace TechnicSolderHelper
{
    public class McMod
    {
        public string modId { get; set; }

        public string Name { get; set; }

        private string version;

        public string Version
        {
            get { return version; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = "";
                }
                this.version = Regex.Replace(value, "_| ", "");
            }
        }

        public string McVersion { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public bool HasBeenWritenToModlist { get; set; }

        public bool IsSkipping { get; set; }

        public List<string> AuthorList { get; set; }

        public List<string> Authors { get; set; }

        public PermissionPolicy PublicPerms { get; set; }

        public PermissionPolicy PrivatePerms { get; set; }

        public string PermissionLink { get; set; }

        public string LicenseLink { get; set; }

        public bool IsIgnore { get; set; }

        public bool UseShortName { get; set; }

        public bool FromSuggestion { get; set; }

        public bool FromUserInput { get; set; }

        public string Filename { get; set; }

        public string Path { get; set; }

        public bool IsDone { get; set; }

        private static string reg = null;
        public virtual string GetSafeModId()
        {
            if (string.IsNullOrWhiteSpace(reg))
            {
                reg = @"\\|\/|\||:|\*|" + "\"" + @"|<|>|'|\?|&|\$|@|=|;|\+|\s|,|{|}|\^|%|`|\]|\[|~|#";
                for (int i = 0; i < 32; i++)
                {
                    char c = (char)i;
                    reg += "|" + c;
                }
                for (int i = 127; i < 256; i++)
                {
                    char c = (char)i;
                    reg += "|" + c;
                }
                Debug.WriteLine(reg);
            }
            // Regex get rids of any illigal windows explorer characters
            // And some characters that break url navigation
            // And anything amazon recommends removing
            return Regex.Replace(Regex.Replace(modId, "_| ", "-"), reg, string.Empty).ToLower();
        }

    }

    public class OwnPermissions
    {
        public bool HasPermission { get; set; }

        public string PermissionLink { get; set; }

        public string ModLink { get; set; }

        public string LicenseLink { get; set; }
    }

    public class ModHelper
    {

        public static McMod GoodVersioning(string fileName)
        {
            fileName = fileName.Remove(fileName.LastIndexOf("."));
            McMod mod = new McMod();

            //Figure out modname
            string modname = "";
            foreach (char c in fileName)
            {
                if (!(c.Equals('-')))
                {
                    modname = modname + c;
                }
                else
                {
                    break;
                }
            }
            mod.Name = modname;
            fileName = fileName.Replace(modname + "-", "");

            //Figure out minecraft version
            string mcversion = "";
            foreach (char c in fileName)
            {
                if (!(c.Equals('-')))
                {
                    mcversion = mcversion + c;
                }
                else
                {
                    break;
                }
            }
            mod.McVersion = mcversion;

            //Figure out modversion
            fileName = fileName.Replace(mcversion + "-", "");
            mod.Version = fileName;


            return mod;
        }

        public static McMod WailaPattern(string fileName) // waila-1.5.5_1.7.10.jar
        {
            fileName = fileName.Remove(fileName.LastIndexOf(".", StringComparison.Ordinal));
            McMod mod = new McMod();

            string name = "";
            foreach (char c in fileName)
            {
                if (!(c.Equals('-')))
                {
                    name = name + c;
                }
                else
                {
                    break;
                }
            }
            mod.Name = name;

            fileName = fileName.Replace(name, "");

            string version = "";
            foreach (char c in fileName)
            {
                if (!(c.Equals('_')))
                {
                    version = version + c;
                }
                else
                {
                    break;
                }
            }
            mod.Version = version;

            fileName = fileName.Replace("_", "").Replace(version, "");
            mod.McVersion = fileName;

            return mod;
        }

        public static McMod ReikasMods(string fileName)
        {
            McMod mod = new McMod();

            fileName = fileName.Remove(fileName.LastIndexOf(".", StringComparison.Ordinal));

            //Figure out mod name
            string[] reikas = fileName.Split(' ');

            mod.Name = reikas[0].Replace(" ", string.Empty);
            mod.McVersion = reikas[1].Replace(" ", string.Empty);
            mod.Version = reikas[2].Replace(" ", string.Empty);

            return mod;
        }

    }

    public class Mcmod2
    {
        public int Modinfoversion { get; set; }

        public int ModListVersion { get; set; }

        public List<ModList> ModList { get; set; }
    }
}
