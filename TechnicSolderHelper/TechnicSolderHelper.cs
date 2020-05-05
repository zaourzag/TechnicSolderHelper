using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using TechnicSolderHelper.AmazonS3;
using TechnicSolderHelper.Confighandler;
using TechnicSolderHelper.FileUpload;
using TechnicSolderHelper.FileUpload.Sftp;
using TechnicSolderHelper.SmallInterfaces;
using TechnicSolderHelper.SQL;
using TechnicSolderHelper.SQL.Forge;
using TechnicSolderHelper.SQL.liteloader;
using TechnicSolderHelper.SQL.WorkTogether;

namespace TechnicSolderHelper
{
    public partial class SolderHelper
    {
        #region Application Wide Variables

        private string _inputDirectory;
        private string _outputDirectory;
        private readonly ModListSqlHelper _modsSqLHelper = new ModListSqlHelper();
        private readonly FtbPermissionsSqlHelper _ftbPermsSqLhelper = new FtbPermissionsSqlHelper();
        private readonly OwnPermissionsSqlHelper _ownPermsSqLhelper = new OwnPermissionsSqlHelper();
        private readonly ForgeSqlHelper _forgeSqlHelper = new ForgeSqlHelper();
        private readonly LiteloaderSqlHelper _liteloaderSqlHelper = new LiteloaderSqlHelper();
        private SolderSqlHandler _solderSqlHandler = new SolderSqlHandler();
        private readonly string _sevenZipLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolderHelper", "7za.exe");
        private readonly Process _process = new Process();
        private readonly ProcessStartInfo _startInfo = new ProcessStartInfo();
        public string _path, _currentMcVersion, _modpackVersion, _modpackName, _modpackArchive, _ftbModpackArchive;
        private readonly ConfigHandler _confighandler = new ConfigHandler();
        private string _modlistTextFile = "", _technicPermissionList = "", _ftbPermissionList = "", _ftbOwnPermissionList = "";
        private short _totalMods, _currentMod;
        private readonly string _modpacksJsonFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolderHelper", "modpacks.json");
        private ModPacks _modpacks = new ModPacks();
        private readonly Dictionary<string, CheckBox> _additionalDirectories = new Dictionary<string, CheckBox>();
        private Ftp _ftp;
        private readonly List<string> _inputDirectories = new List<string>();
        private int _buildId, _modpackId;
        private bool _updatingForge, _updatingLiteloader;
        private bool _updatingPermissions;
        private bool _uploadingToFtp, _uploadingToS3, _uploadingToSftp;

        private int _runningProcess;
        private readonly Dictionary<string, int> _processesUsingFolder = new Dictionary<string, int>();

        private bool UpdatingForge
        {
            get { return _updatingForge; }
            set
            {
                _updatingForge = value;
                AsyncBlockingProcessUpdated();
            }

        }

        private bool UpdatingLiteloader
        {
            get { return _updatingLiteloader; }
            set
            {
                _updatingLiteloader = value;
                AsyncBlockingProcessUpdated();
            }

        }

        private bool UpdatingPermissions
        {
            get { return _updatingPermissions; }
            set
            {
                _updatingPermissions = value;
                AsyncBlockingProcessUpdated();
            }
        }

        private bool UploadingToFtp
        {
            get { return _uploadingToFtp; }
            set
            {
                _uploadingToFtp = value;
                AsyncLockInterface();
            }

        }
        private bool UploadingToSftp
        {
            get { return _uploadingToSftp; }
            set
            {
                _uploadingToSftp = value;
                AsyncLockInterface();
            }
        }
        private bool UploadingToS3
        {
            get { return _uploadingToS3; }
            set
            {
                _uploadingToS3 = value;
                AsyncLockInterface();
            }
        }

        private void AsyncBlockingProcessUpdated()
        {
            if (goButton.InvokeRequired)
            {
                goButton.Invoke((Action)(AsyncBlockingProcessUpdated));
            }
            else
            {
                goButton.Enabled = !UpdatingForge && !UpdatingLiteloader && !UpdatingPermissions;
            }
        }

        private void AsyncLockInterface()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)(AsyncLockInterface));
            }
            else
            {
                this.Enabled = !UploadingToFtp && !UploadingToS3 && !UploadingToSftp;

            }
        }

        #endregion

        private void form_resize(object sender, EventArgs e)
        {
            if ((Globalfunctions.IsUnix() && Width > 923) || (!Globalfunctions.IsUnix() && Width > 800))
            {
                //Debug.WriteLine(Width - 923 + 159);
                if (Globalfunctions.IsUnix())
                {
                    additionalFoldersGroupBox.Width = Width - 923 + 159;
                }
                else
                {
                    additionalFoldersGroupBox.Width = Width - 800 + 136;
                }
            }
            if (!Globalfunctions.IsUnix() && Height > 522)
            {
                additionalFoldersGroupBox.Height = Height - 522 + 427;
            }
        }

        public string GetAuthors(McMod mod, bool listview = false)
        {
            string authorString = "";
            bool isFirst = true;
            if (mod.Authors != null && mod.Authors.Count != 0)
            {
                foreach (string author in mod.Authors)
                {
                    if (isFirst)
                    {
                        authorString = author;
                        isFirst = false;
                    }
                    else
                    {
                        authorString += ", " + author;
                    }
                }
            }
            else
            {
                if (mod.AuthorList != null && mod.AuthorList.Count != 0)
                {
                    foreach (string author in mod.AuthorList)
                    {
                        if (isFirst)
                        {
                            authorString = author;
                            isFirst = false;
                        }
                        else
                        {
                            authorString += ", " + author;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(mod.modId))
                    {
                        return "";
                    }

                    Permission temp = _ftbPermsSqLhelper.GetPermissionFromModId(mod.modId);

                    if (temp == null)
                    {
                        authorString = "";
                    }
                    else
                    {
                        authorString = temp.modAuthors;
                    }

                    if (string.IsNullOrWhiteSpace(authorString))
                    {
                        authorString = _ownPermsSqLhelper.GetAuthor(mod.modId);
                        if (string.IsNullOrWhiteSpace(authorString))
                        {
                            if (listview)
                            {
                                return null;
                            }
                            authorString = Prompt.ShowDialog("Who is the author of " + mod.Name + "?" + Environment.NewLine + "If you leave this empty the author list in the output will also be empty.", "Mod Author", false, Prompt.ModsLeftString(_totalMods, _currentMod));

                        }
                    }
                }
            }
            _ownPermsSqLhelper.AddAuthor(mod.modId, authorString);
            return authorString;
        }

        private bool Prepare()
        {
            _inputDirectory = inputDirectoryTextBox.Text;
            _outputDirectory = outputDirectoryTextBox.Text;
            _ftbOwnPermissionList = Path.Combine(_outputDirectory, "Own Permission List.txt");
            _ftbPermissionList = Path.Combine(_outputDirectory, "FTB Permission List.txt");
            _technicPermissionList = Path.Combine(_outputDirectory, "Technic Permission List.txt");
            //_sqlCommandPath = Path.Combine(_outputDirectory, "commands.sql");
            _currentMcVersion = string.IsNullOrEmpty(mcVersionDropdown.Text) ? null : mcVersionDropdown.SelectedItem.ToString();

            if (!_inputDirectory.EndsWith(Globalfunctions.PathSeperator + "mods", true, CultureInfo.CurrentCulture))
            {
                MessageBox.Show(string.Format("You need to point Modpack Helper at a {0}mods directory", Globalfunctions.PathSeperator));
                return true;
            }

            Environment.CurrentDirectory = Globalfunctions.IsUnix() ? "/" : "C:\\";
            if (!Directory.Exists(inputDirectoryTextBox.Text))
            {
                MessageBox.Show("Input directory does not exist.");
                return true;
            }

            if (!_inputDirectories.Contains(_inputDirectory))
            {
                _inputDirectories.Add(_inputDirectory);
            }

            if (clearOutputDirectoryCheckBox.Checked)
            {
                if (Directory.Exists(_outputDirectory))
                {
                    try
                    {
                        Directory.Delete(_outputDirectory, true);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to clear output directory." + Environment.NewLine + "Please restart the process when the directory is no longer in use.");
                        return true;
                    }
                }
            }

            if (uploadToFTPServerCheckBox.Checked)
            {
                var tmp = _confighandler.GetConfig("ftpUrl");
                if (string.IsNullOrWhiteSpace(tmp))
                {
                    MessageBox.Show("You do not have a URL set for your FTP server.");
                    return true;
                }
                tmp = _confighandler.GetConfig("ftpUserName");
                if (string.IsNullOrWhiteSpace(tmp))
                {
                    MessageBox.Show("You do not have a username set for your FTP server.");
                    return true;
                }
                tmp = _confighandler.GetConfig("ftpPassword");
                if (string.IsNullOrWhiteSpace(tmp))
                {
                    MessageBox.Show("You do not have a password set for your FTP server.");
                    return true;
                }

            }

            if (createTechnicPackCheckBox.Checked && includeForgeZipCheckBox.Checked)
            {
                if (mcVersionDropdown.SelectedItem == null)
                {
                    MessageBox.Show("You have chosen to include Minecraft Forge, but you haven't selected a Minecraft version.");
                    return true;
                }
                if (forgeVersionDropdown.SelectedItem == null)
                {
                    MessageBox.Show("You have chosen to include Minecraft Forge, but you haven't selected a Forge version to include.");
                    return true;
                }
            }

            if (File.Exists(_ftbOwnPermissionList))
                File.Delete(_ftbOwnPermissionList);
            if (File.Exists(_ftbPermissionList))
                File.Delete(_ftbPermissionList);
            if (File.Exists(_technicPermissionList))
                File.Delete(_technicPermissionList);

            _modpackName = modpackNameInput.Text;
            _modpackVersion = null;
            _modpackVersion = modpackVersionTextBox.Text;

            if (!string.IsNullOrWhiteSpace(_modpackName))
            {
                _modpacks = new ModPacks();
                if (!modpackNameInput.Items.Contains(_modpackName))
                {
                    modpackNameInput.Items.Add(_modpackName);
                }
                foreach (string item in modpackNameInput.Items)
                {
                    if (_modpacks.ModPack == null)
                    {
                        _modpacks.ModPack = new Dictionary<string, List<string>>();
                    }
                    if (!_modpacks.ModPack.ContainsKey(item))
                    {
                        _modpacks.ModPack.Add(item, null);
                    }
                }
                string tmpJson = JsonConvert.SerializeObject(_modpacks, Formatting.Indented);
                File.WriteAllText(_modpacksJsonFile, tmpJson);
            }


            //Download 7zip dependency
            if (!Globalfunctions.IsUnix())
            {
                if (!(Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\TechnicSolderHelper")))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\TechnicSolderHelper");
                }
            }
            if (!(Globalfunctions.IsUnix() || File.Exists(_sevenZipLocation)))
            {
                WebClient wb = new WebClient();
                Uri sevenWeb = new Uri("https://www.dropbox.com/s/oct206mp41bj8vu/7za.exe?dl=1");
                wb.DownloadFile(sevenWeb, _sevenZipLocation);
            }
            _confighandler.SetConfig("InputDirectory", inputDirectoryTextBox.Text);
            _confighandler.SetConfig("OutputDirectory", outputDirectoryTextBox.Text);

            _path = Path.Combine(_outputDirectory, "mods.html");


            Directory.CreateDirectory(_outputDirectory);
            Environment.CurrentDirectory = _outputDirectory;
            _modlistTextFile = Path.Combine(_outputDirectory, "modlist.txt");
            if (File.Exists(_modlistTextFile))
            {
                File.Delete(_modlistTextFile);
            }

            // The start of the output html file for Technic Solder.
            if (solderPackRadioButton.Checked)
            {
                string htmlfile = "<!DOCTYPE html> \n <html> <head>" + Environment.NewLine +
                                  "<title>Mods</title>" + Environment.NewLine +
                                  "<meta charset=\"utf-8\" />" + Environment.NewLine +
                                  "<script src=\"https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js\"></script>" + Environment.NewLine +
                                  "<script src=\"http://cloud.zlepper.dk/technicsolderhelper.js\"></script>" + Environment.NewLine +
                                  "<link type=\"text/css\" rel=\"stylesheet\" href=\"http://cloud.zlepper.dk/technicsolderhelper.css\">" +
                                  "</head>" + Environment.NewLine + "<body><table border='1'><tr><th>Modname</th><th>Modslug</th><th>Version</th></tr>" + Environment.NewLine;
                File.WriteAllText(_path, htmlfile);
            }

            return false;
        }

        private List<string> GetModFiles()
        {
            // Create array with all the mod locations
            List<string> files = new List<string>();
            _totalMods = 0;
            _currentMod = 0;

            if (!Directory.Exists(_inputDirectory))
            {
                MessageBox.Show("Input directory does not exist.");
                return files;
            }

            // Add the different mod files to the files array
            foreach (string file in Directory.GetFiles(_inputDirectory, "*.zip", SearchOption.AllDirectories))
            {
                FileInfo f = new FileInfo(file);
                string name = f.Name;
                if (Regex.IsMatch(name, @"-?[0-9]+,-?[0-9]+.zip"))
                {
                    Debug.WriteLine("Skipped " + name);
                    continue;
                }
                files.Add(file);
                _totalMods++;
            }
            foreach (string file in Directory.GetFiles(_inputDirectory, "*.jar", SearchOption.AllDirectories))
            {
                files.Add(file);
                _totalMods++;
            }
            foreach (string file in Directory.GetFiles(_inputDirectory, "*.litemod", SearchOption.AllDirectories))
            {
                files.Add(file);
                _totalMods++;
            }
            foreach (string file in Directory.GetFiles(_inputDirectory, "*.disabled", SearchOption.AllDirectories))
            {
                files.Add(file);
                _totalMods++;
            }
            return files;
        }

        private void InitializeSolderSqlHandler()
        {
            _solderSqlHandler = new SolderSqlHandler();
            _modpackId = _solderSqlHandler.GetModpackId(_modpackName);
            if (_modpackId == -1)
            {
                _solderSqlHandler.CreateNewModpack(_modpackName, _modpackName.ToLower().Replace(" ", "-"));
                _modpackId = _solderSqlHandler.GetModpackId(_modpackName);
            }
            _buildId = _solderSqlHandler.GetBuildId(_modpackId, _modpackVersion);
            string javaVersion = "";
            switch (minimumJavaVersionDropdown.Text)
            {
                case "Java 1.8":
                    javaVersion = "1.8";
                    break;
                case "Java 1.7":
                    javaVersion = "1.7";
                    break;
                case "Java 1.6":
                    javaVersion = "1.6";
                    break;
            }
            int memory;
            bool parsed = int.TryParse(minimumMemoryTextBox.Text, out memory);
            if (!parsed)
            {
                memory = 0;
            }
            if (_buildId == -1)
            {
                _solderSqlHandler.CreateModpackBuild(_modpackId, _modpackVersion, _currentMcVersion, javaVersion, memory);
                _buildId = _solderSqlHandler.GetBuildId(_modpackId, _modpackVersion);
            }
        }

        private void Start()
        {
            if (Prepare())
            {
                return;
            }
            AddedModStringBuilder.Clear();
            toolStripStatusLabel.Text = "Finding files";
            List<string> files = GetModFiles();
            while (string.IsNullOrWhiteSpace(_modpackName))
            {
                _modpackName = Prompt.ShowDialog("What is the modpack name?", "Modpack Name");
            }
            while (string.IsNullOrWhiteSpace(_modpackVersion))
            {
                _modpackVersion = Prompt.ShowDialog("What version is the modpack?", "Modpack Version");
            }
            if (string.IsNullOrWhiteSpace(_modpackArchive))
            {
                _modpackArchive = Path.Combine(_outputDirectory, string.Format("{0}-{1}.zip", _modpackName, _modpackVersion));
            }
            _ftbModpackArchive = Path.Combine(_outputDirectory, _modpackName + "-" + _modpackVersion + "-FTB" + ".zip");

            if (string.IsNullOrWhiteSpace(_currentMcVersion))
            {
                McSelector selector = new McSelector(this);
                selector.ShowDialog();
            }

            if (useSolderCheckBox.Checked)
            {
                InitializeSolderSqlHandler();
            }

            _processesUsingModID.Clear();

            List<McMod> modsList = new List<McMod>(_totalMods);
            toolStripProgressBar.Value = 0;
            toolStripProgressBar.Maximum = _totalMods;
            //Check if files have already been added
            foreach (string file in files)
            {
                Debug.WriteLine("");
                _currentMod++;
                toolStripProgressBar.Increment(1);
                if (IsWierdMod(file) == 0)
                {
                    continue;
                }
                // ReSharper disable once InconsistentNaming
                var FileName = file.Substring(file.LastIndexOf(Globalfunctions.PathSeperator) + 1);
                //Check for mcmod.info
                toolStripStatusLabel.Text = FileName;
                Directory.CreateDirectory(_outputDirectory);
                McMod tmpMod = _modsSqLHelper.GetModInfo(SqlHelper.CalculateMd5(file));
                if (tmpMod != null)
                {
                    tmpMod.Filename = FileName;
                    tmpMod.Path = file;
                    modsList.Add(tmpMod);
                    continue;
                }
                string arguments;
                if (Globalfunctions.IsUnix())
                {
                    _startInfo.FileName = "unzip";
                    arguments = string.Format("-o \"{0}\" \"*.info\" \"*.json\" -d \"{1}\"", file, _outputDirectory);
                }
                else
                {
                    arguments = string.Format("e -y -o\"{0}\" \"{1}\" *.info litemod.json", _outputDirectory, file);
                }
                _startInfo.Arguments = arguments;

                _process.StartInfo = _startInfo;
                _process.Start();
                _process.WaitForExit();
                string mcmodfile = Path.Combine(_outputDirectory, "mcmod.info");
                string litemodfile = Path.Combine(_outputDirectory, "litemod.json");
                if (File.Exists(litemodfile))
                {
                    if (File.Exists(mcmodfile))
                    {
                        File.Delete(mcmodfile);
                    }
                    File.Move(litemodfile, mcmodfile);
                }
                if (!File.Exists(mcmodfile))
                {
                    foreach (string modinfofile in Directory.GetFiles(_outputDirectory, "*.info"))
                    {
                        if (modinfofile.ToLower().Contains("dependancies") ||
                            modinfofile.ToLower().Contains("dependencies"))
                            File.Delete(modinfofile);
                        else
                        {
                            if (!File.Exists(mcmodfile))
                                File.Move(modinfofile, mcmodfile);
                            else
                            {
                                File.Delete(mcmodfile);
                                File.Move(modinfofile, mcmodfile);
                            }
                        }
                    }
                }

                if (File.Exists(mcmodfile))
                {

                    //If exist, then read info and make zip file
                    string json;
                    using (StreamReader r = new StreamReader(mcmodfile))
                    {
                        json = r.ReadToEnd();
                    }
                    try
                    {
                        try
                        {
                            Mcmod2 modinfo2;
                            try
                            {
                                modinfo2 = JsonConvert.DeserializeObject<Mcmod2>(json);
                            }
                            catch (JsonReaderException)
                            {
                                //MessageBox.Show(string.Format("Something is wrong with the Json in {0}", FileName));
                                throw new JsonSerializationException("Invalid Json in file" + FileName);
                            }

                            McMod mod = new McMod();

                            if (modinfo2.Modinfoversion != 0 && modinfo2.Modinfoversion > 1 || modinfo2.ModListVersion != 0 && modinfo2.ModListVersion > 1)
                            {
                                mod.McVersion = modinfo2.ModList[0].McVersion;
                                mod.modId = modinfo2.ModList[0].ModId;
                                mod.Name = modinfo2.ModList[0].Name;
                                mod.Version = modinfo2.ModList[0].Version;
                                mod.Authors = modinfo2.ModList[0].Authors;
                                mod.Description = modinfo2.ModList[0].Description;
                                mod.Url = modinfo2.ModList[0].Url;
                                mod.Filename = FileName;
                                mod.Path = file;
                                modsList.Add(mod);

                            }
                            else
                            {
                                throw new JsonSerializationException();
                            }
                        }
                        catch (JsonSerializationException)
                        {
                            try
                            {
                                List<McMod> modinfo;
                                try
                                {
                                    modinfo = JsonConvert.DeserializeObject<List<McMod>>(json);
                                }
                                catch (JsonReaderException)
                                {
                                    //MessageBox.Show(string.Format("Something is wrong with the Json in {0}", FileName));
                                    throw new JsonSerializationException("Invalid Json in file" + FileName);
                                }
                                var mod = modinfo[0];
                                if (!string.IsNullOrWhiteSpace(mod.Version))
                                {
                                    mod.Version = mod.Version.Replace(" ", "-");
                                }

                                mod.Filename = FileName;
                                mod.Path = file;
                                modsList.Add(mod);


                            }
                            catch (JsonSerializationException)
                            {
                                LiteMod liteMod;

                                try
                                {
                                    liteMod = JsonConvert.DeserializeObject<LiteMod>(json);
                                }
                                catch (JsonReaderException)
                                {
                                    //MessageBox.Show(string.Format("Something is wrong with the Json in {0}", FileName));
                                    throw new JsonSerializationException("Invalid Json in file" + FileName);
                                }
                                //Convert into mcmod
                                McMod mod = new McMod
                                {
                                    McVersion = liteMod.McVersion,
                                    modId = liteMod.Name.Replace(" ", ""),
                                    Name = liteMod.Name,
                                    Description = liteMod.Description,
                                    Authors = new List<string> { liteMod.Author }
                                };

                                if (string.IsNullOrEmpty(liteMod.Version) || string.IsNullOrEmpty(liteMod.Revision))
                                {
                                    if (!(string.IsNullOrEmpty(liteMod.Version)))
                                    {
                                        mod.Version = liteMod.Version;
                                    }
                                    else
                                    {
                                        if (!(string.IsNullOrEmpty(liteMod.Revision)))
                                        {
                                            mod.Version = liteMod.Revision;
                                        }
                                    }
                                }
                                else
                                {
                                    mod.Version = liteMod.Version + "-" + liteMod.Revision;
                                }
                                mod.Filename = FileName;
                                mod.Path = file;
                                modsList.Add(mod);

                            }
                        }

                    }
                    catch (JsonSerializationException)
                    {
                        McMod mod = new McMod();
                        mod.Filename = FileName;
                        mod.Path = file;
                        modsList.Add(mod);

                    }
                    File.Delete(mcmodfile);
                }
                else
                {
                    //Check the FTB permission sheet for info before doing anything else
                    string shortName = _ftbPermsSqLhelper.GetShortName(SqlHelper.CalculateMd5(file));
                    if (string.IsNullOrWhiteSpace(shortName))
                    {
                        int fixNr = IsWierdMod(FileName);
                        if (fixNr != int.MaxValue)
                        {
                            McMod mod;
                            switch (fixNr)
                            {
                                case 1:
                                    LiteloaderVersionInfo llVersionInfo = _liteloaderSqlHelper.GetInfo(SqlHelper.CalculateMd5(file));
                                    mod = new McMod
                                    {
                                        McVersion = llVersionInfo.McVersion,
                                        Name = "Liteloader",
                                        modId = "liteloader"
                                    };
                                    try
                                    {
                                        mod.Version = llVersionInfo.Version.Substring(llVersionInfo.Version.LastIndexOf("_", StringComparison.Ordinal) + 1);
                                    }
                                    catch (NullReferenceException)
                                    {
                                        mod.Version = 1.ToString();
                                    }
                                    mod.Filename = FileName;
                                    mod.Path = file;
                                    modsList.Add(mod);
                                    break;
                            }
                        }
                        else
                        {

                            modsList.Add(new McMod
                            {
                                Path = file,
                                Filename = FileName
                            });

                        }
                    }
                    else
                    {
                        McMod mod = new McMod();
                        if (shortName.Equals("ignore"))
                        {
                            mod.IsIgnore = true;
                        }
                        else
                        {
                            mod.IsIgnore = false;
                            mod.UseShortName = true;
                            mod.modId = shortName;
                            mod.Name = _ftbPermsSqLhelper.GetPermissionFromModId(shortName).modName;
                            mod.Authors = new List<string>
                            {
                                _ftbPermsSqLhelper.GetPermissionFromModId(shortName).modAuthors
                            };
                            mod.AuthorList = mod.Authors;
                            mod.PrivatePerms = _ftbPermsSqLhelper.FindPermissionPolicy(shortName, false);
                            mod.PublicPerms = _ftbPermsSqLhelper.FindPermissionPolicy(shortName, true);
                        }

                        mod.Filename = FileName;
                        mod.Path = file;
                        modsList.Add(mod);

                    }
                }
            }
            toolStripStatusLabel.Text = "Showing mod list pane.";
            toolStripProgressBar.Value = toolStripProgressBar.Maximum;
            Form modInfoForm = new ModInfo(modsList, this);
            if (!modInfoForm.IsDisposed)
            {
                modInfoForm.ShowDialog();
            }

            Environment.CurrentDirectory = _inputDirectory;
            string[] directories = Directory.GetDirectories(_inputDirectory);
            const string minecraftVersionPattern = @"^[0-9]{1}\.[0-9]{1}\.[0-9]{1,2}$";
            foreach (string dir in directories)
            {
                toolStripStatusLabel.Text = "Packing additional folders";

                string dirName = dir.Substring(dir.LastIndexOf(Globalfunctions.PathSeperator) + 1);
                if (Regex.IsMatch(dirName, minecraftVersionPattern, RegexOptions.Multiline))
                {
                    continue;
                }
                string[] jarFiles = Directory.GetFiles(dir, "*.jar", SearchOption.AllDirectories);
                if (jarFiles.Length != 0)
                {
                    continue;
                }
                string levelOverInputDirectory = _inputDirectory.Remove(_inputDirectory.LastIndexOf(Globalfunctions.PathSeperator));

                DialogResult confirmInclude = MessageBox.Show(string.Format("Do you want to include {0}?", dirName),
                                                  @"Additional folder found", MessageBoxButtons.YesNo);
                if (confirmInclude == DialogResult.Yes)
                {
                    Environment.CurrentDirectory = levelOverInputDirectory;
                    if (createTechnicPackCheckBox.Checked)
                    {
                        //Create Technic Pack
                        if (solderPackRadioButton.Checked)
                        {
                            List<string> md5Values = new List<string>();
                            List<string> oldmd5Values = new List<string>();
                            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolderHelper", "unarchivedFiles"));
                            string md5ValuesFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolderHelper", "unarchivedFiles", dirName + ".txt");
                            if (File.Exists(md5ValuesFile))
                            {
                                using (StreamReader reader = new StreamReader(md5ValuesFile))
                                {
                                    while (true)
                                    {
                                        string tmp = reader.ReadLine();
                                        if (string.IsNullOrWhiteSpace(tmp))
                                            break;
                                        oldmd5Values.Add(tmp);
                                    }
                                }
                            }

                            bool same = true;
                            foreach (string f in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
                            {
                                if (!oldmd5Values.Contains(SqlHelper.CalculateMd5(f)))
                                {
                                    same = false;
                                }
                                md5Values.Add(SqlHelper.CalculateMd5(f));
                            }
                            if (same)
                            {
                                continue;
                            }
                            while (string.IsNullOrWhiteSpace(_modpackVersion))
                            {
                                _modpackVersion = Prompt.ShowDialog("What version is the modpack?", "Modpack Version");
                            }
                            //So we need to include this folder
                            Environment.CurrentDirectory = levelOverInputDirectory;
                            string outputFile = Path.Combine(_outputDirectory, "mods", dirName.ToLower(), dirName.ToLower() + "-" + MakeUrlFriendly(_modpackName + "-" + _modpackVersion) + ".zip");
                            Directory.CreateDirectory(Path.Combine(_outputDirectory, "mods", dirName.ToLower()));
                            if (Globalfunctions.IsUnix())
                            {
                                _startInfo.FileName = "zip";
                                _startInfo.Arguments = string.Format("-r \"{0}\" mods/{1}", outputFile, dirName);
                            }
                            else
                            {
                                _startInfo.FileName = _sevenZipLocation;
                                _startInfo.Arguments = string.Format("a -y \"{0}\" \"mods\\{1}\"", outputFile, dirName);
                            }
                            _process.StartInfo = _startInfo;
                            _process.Start();
                            CreateTableRow(dirName, dirName.ToLower(), MakeUrlFriendly(_modpackName + "-" + _modpackVersion));
                            _process.WaitForExit();

                            if (useSolderCheckBox.Checked)
                            {
                                int id = _solderSqlHandler.GetModId(dirName.ToLower());
                                if (id == -1)
                                {
                                    _solderSqlHandler.AddModToSolder(dirName.ToLower(), null, null, null, dirName);
                                    id = _solderSqlHandler.GetModId(dirName.ToLower());
                                }
                                string modVersion = MakeUrlFriendly(_modpackName + "-" + _modpackVersion);
                                string md5 = SqlHelper.CalculateMd5(outputFile).ToLower();
                                if (_solderSqlHandler.IsModVersionOnline(dirName.ToLower(), modVersion))
                                    _solderSqlHandler.UpdateModVersionMd5(dirName.ToLower(), modVersion, md5);
                                else
                                    _solderSqlHandler.AddNewModVersionToSolder(id, modVersion, md5);

                                id = _solderSqlHandler.GetModId(dirName.ToLower());
                                int modVersionId = _solderSqlHandler.GetModVersionId(id, modVersion);
                                _solderSqlHandler.AddModVersionToBuild(_buildId, modVersionId);
                            }
                            if (File.Exists(md5ValuesFile))
                            {
                                File.Delete(md5ValuesFile);
                            }
                            foreach (string md5Value in md5Values)
                            {
                                File.AppendAllText(md5ValuesFile, md5Value + Environment.NewLine);
                            }
                        }
                        else
                        {
                            if (Globalfunctions.IsUnix())
                            {
                                _startInfo.FileName = "zip";
                                _startInfo.Arguments = string.Format("-r \"{0}\" \"./mods/{1}\"", _modpackArchive, dirName);
                            }
                            else
                            {
                                _startInfo.FileName = _sevenZipLocation;
                                _startInfo.Arguments = string.Format("a -y \"{0}\" \"mods\\{1}\"", _modpackArchive, dirName);
                            }
                            _process.StartInfo = _startInfo;
                            _process.Start();
                            _process.WaitForExit();
                        }
                    }
                    if (createFTBPackCheckBox.Checked)
                    {
                        // Create FTB Pack

                        string tmpDir = Path.Combine(_outputDirectory, "minecraft", "mods");
                        Directory.CreateDirectory(tmpDir);
                        if (Globalfunctions.IsUnix())
                        {
                            _startInfo.FileName = "cp";
                            _startInfo.Arguments = string.Format("-r \"./mods/{0}\" \"{1}\"", dirName, tmpDir);
                            _process.StartInfo = _startInfo;
                            _process.Start();
                            _process.WaitForExit();
                        }
                        else
                        {
                            string input = Path.Combine(_inputDirectory, dirName);
                            DirectoryCopy(input, tmpDir, true);
                        }
                    }
                }

            }

            // Pack additional folders if they are marked
            if (createTechnicPackCheckBox.Checked)
            {

                toolStripStatusLabel.Text = "Packing additional selected folders.";
                Environment.CurrentDirectory = _inputDirectory.Remove(_inputDirectory.LastIndexOf(Globalfunctions.PathSeperator));
                foreach (string folderName in from cb in _additionalDirectories where cb.Value.Checked select cb.Key.Substring(cb.Key.LastIndexOf(Globalfunctions.PathSeperator) + 1).ToLower())
                {
                    if (solderPackRadioButton.Checked)
                    {
                        bool useSolder = useSolderCheckBox.Checked;
                        var worker = new BackgroundWorker();
                        var name = folderName;
                        worker.DoWork += (sender, args) =>
                        {
                            _runningProcess++;
                            string modPackName = _modpackName.Replace(" ", "-").ToLower();
                            string of = Path.Combine(_outputDirectory, "mods", name);
                            Directory.CreateDirectory(of);
                            string outputFile = Path.Combine(of,
                                name.ToLower() + "-" + modPackName + "-" + _modpackVersion + ".zip");
                            if (Globalfunctions.IsUnix())
                            {
                                _startInfo.FileName = "zip";
                                _startInfo.Arguments = string.Format("-r \"{0}\" {1}", outputFile, name);
                            }
                            else
                            {
                                _startInfo.FileName = _sevenZipLocation;
                                _startInfo.Arguments = string.Format("a -y \"{0}\" \"{1}\"", outputFile, name);
                            }
                            _process.StartInfo = _startInfo;
                            _process.Start();
                            _process.WaitForExit();

                            CreateTableRow(name, name.ToLower(), modPackName + "-" + _modpackVersion);

                            if (useSolder)
                            {
                                int id = _solderSqlHandler.GetModId(name.ToLower());
                                if (id == -1)
                                {
                                    _solderSqlHandler.AddModToSolder(name.ToLower(), null, null, null,
                                        name);
                                    id = _solderSqlHandler.GetModId(name.ToLower());
                                }
                                string md5 = SqlHelper.CalculateMd5(outputFile).ToLower();
                                if (_solderSqlHandler.IsModVersionOnline(name.ToLower(),
                                    modPackName + "-" + _modpackVersion))
                                    _solderSqlHandler.UpdateModVersionMd5(name.ToLower(),
                                        modPackName + "-" + _modpackVersion, md5);
                                else
                                    _solderSqlHandler.AddNewModVersionToSolder(id, modPackName + "-" + _modpackVersion,
                                        md5);
                                int modVersionId =
                                    _solderSqlHandler.GetModVersionId(
                                        _solderSqlHandler.GetModId(name.ToLower()),
                                        modPackName + "-" + _modpackVersion);
                                _solderSqlHandler.AddModVersionToBuild(_buildId, modVersionId);
                            }
                            _runningProcess--;
                        };
                        worker.RunWorkerAsync();
                    }
                    else
                    {

                        if (Globalfunctions.IsUnix())
                        {
                            _startInfo.FileName = "zip";
                            _startInfo.Arguments = string.Format("-r \"{0}\" \"{1}\"", _modpackArchive, folderName);
                        }
                        else
                        {
                            _startInfo.FileName = _sevenZipLocation;
                            _startInfo.Arguments = string.Format("a -y \"{0}\" \"{1}\"", _modpackArchive, folderName);
                        }
                        _process.StartInfo = _startInfo;
                        _process.Start();
                        _process.WaitForExit();

                    }
                }
            }
            if (createTechnicPackCheckBox.Checked && includeForgeZipCheckBox.Checked)
            {
                string selectedBuild = forgeVersionDropdown.SelectedItem.ToString();
                PackForge(selectedBuild);
            }


            if (createTechnicPackCheckBox.Checked && includeConfigZipCheckBox.Checked)
                CreateConfigZip();

            //FTB pack configs
            if (createFTBPackCheckBox.Checked)
            {
                foreach (KeyValuePair<string, CheckBox> cb in _additionalDirectories)
                {
                    if (cb.Value.Checked)
                    {
                        string dirName = cb.Key.Substring(cb.Key.LastIndexOf(Globalfunctions.PathSeperator) + 1);
                        string tmpDir = Path.Combine(_outputDirectory, "minecraft");

                        Directory.CreateDirectory(tmpDir);
                        if (Globalfunctions.IsUnix())
                        {
                            _startInfo.FileName = "cp";
                            _startInfo.Arguments = string.Format("-r \"{0}\" \"{1}\"", dirName, tmpDir);
                            _process.StartInfo = _startInfo;
                            _process.Start();
                            _process.WaitForExit();
                        }
                        else
                        {
                            FileAttributes attr = File.GetAttributes(cb.Key);
                            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                            {
                                tmpDir = Path.Combine(tmpDir, dirName);
                                DirectoryCopy(cb.Key, tmpDir, true);
                            }
                            else
                            {
                                string outputFile = Path.Combine(tmpDir, dirName);
                                File.Copy(cb.Key, outputFile);
                            }
                        }
                    }
                }

                string tmpConfigDirectory = Path.Combine(_outputDirectory, Path.Combine("minecraft", "config"));
                Directory.CreateDirectory(tmpConfigDirectory);

                string sourceConfigDirectory = inputDirectoryTextBox.Text.Replace(Globalfunctions.PathSeperator + "mods", Globalfunctions.PathSeperator + "config");
                try
                {
                    DirectoryCopy(sourceConfigDirectory, tmpConfigDirectory, true);
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("I can't seem to find a config directory for the FTB pack.");
                }

                Environment.CurrentDirectory = _outputDirectory;
                if (Globalfunctions.IsUnix())
                {
                    _startInfo.FileName = "zip";
                    _startInfo.Arguments = string.Format("-r \"{0}\" \"minecraft\" -x minecraft/config/YAMPST.nbt",
                        _ftbModpackArchive);
                }
                else
                    _startInfo.Arguments = string.Format("a -x!minecraft\\config\\YAMPST.nbt -y \"{0}\" \"minecraft\" ", _ftbModpackArchive);

                _process.StartInfo = _startInfo;
                _process.Start();
                _process.WaitForExit();
                Directory.Delete(Path.Combine(_outputDirectory, "minecraft"), true);
            }
            while (_runningProcess > 0)
            {
                toolStripStatusLabel.Text = "Waiting for " + _runningProcess + " mod" + (_runningProcess == 1 ? "" : "s") + " to finish packing";
                if (Globalfunctions.IsUnix())
                {
                    statusStrip.Refresh();
                    this.Refresh();
                }
                if (doDebugCheckBox.Checked)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string pros in _processesUsingFolder.Keys)
                    {
                        sb.AppendLine(pros);
                    }
                    Debug.WriteLine(sb.ToString());
                }
                Debug.Flush();
                Thread.Sleep(100);
            }
            toolStripStatusLabel.Text = "Saving mod data";
            _modsSqLHelper.SaveData();
            toolStripStatusLabel.Text = "Done saving mod data";

            if (Directory.Exists(Path.Combine(_outputDirectory, "assets")))
                Directory.Delete(Path.Combine(_outputDirectory, "assets"), true);
            if (Directory.Exists(Path.Combine(_outputDirectory, "example")))
                Directory.Delete(Path.Combine(_outputDirectory, "example"), true);
            foreach (string file in Directory.GetFiles(_outputDirectory, "*.info", SearchOption.TopDirectoryOnly))
            {
                File.Delete(file);
            }

            if (createTechnicPackCheckBox.Checked && solderPackRadioButton.Checked && !useSolderCheckBox.Checked)
            {
                File.AppendAllText(_path, AddedModStringBuilder.ToString());
                File.AppendAllText(_path, @"</table><button id=""Reshow"" type=""button"">Unhide Everything</button><p>List autogenerated by TechnicSolderHelper &copy; 2014 - Rasmus Hansen</p></body></html>");
                if (Globalfunctions.IsUnix())
                {
                    Process.Start(_path);
                }
                else
                {
                    try
                    {
                        Process.Start("chrome.exe", "\"" + _path + "\"");
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Process.Start("iexplore", "\"" + _path + "\"");
                        }
                        catch (Exception)
                        {
                            try
                            {
                                Process.Start("firefox.exe", "\"" + _path + "\"");
                            }
                            catch (Exception)
                            {
                                Process.Start("\"" + _path + "\"");
                            }
                        }
                    }
                }

            }
            if (createTechnicPackCheckBox.Checked && solderPackRadioButton.Checked)
            {
                if (uploadToSFTPCheckBox.Checked)
                {
                    toolStripStatusLabel.Text = "Uploading to SFTP";
                    UploadingToSftp = true;
                    BackgroundWorker sftpBackgroundWorker = new BackgroundWorker();
                    sftpBackgroundWorker.DoWork += (s, a) =>
                    {
                        Sftp sftp = new Sftp();
                        sftp.UploadFolder(Path.Combine(_outputDirectory, "mods"), _confighandler.GetConfig("sftpDest"));
                        sftp.Dispose();
                    };
                    sftpBackgroundWorker.RunWorkerCompleted += (s, a) =>
                    {
                        UploadingToSftp = false;
                        MessageBox.Show("Done uploading to SFTP");
                    };
                    sftpBackgroundWorker.RunWorkerAsync();
                }
                if (uploadToFTPServerCheckBox.Checked)
                {
                    toolStripStatusLabel.Text = "Uploading to FTP";
                    UploadingToFtp = true;
                    BackgroundWorker ftpBackgroundWorker = new BackgroundWorker();
                    ftpBackgroundWorker.DoWork += (s, a) =>
                    {
                        if (_ftp == null)
                        {
                            _ftp = new Ftp();
                        }
                        _ftp.UploadFolder(Path.Combine(_outputDirectory, "mods"));
                    };
                    ftpBackgroundWorker.RunWorkerCompleted += (s, a) =>
                    {
                        UploadingToFtp = false;
                        MessageBox.Show("Done uploading to FTP");
                    };
                    ftpBackgroundWorker.RunWorkerAsync();
                }
                if (useS3CheckBox.Checked)
                {
                    toolStripStatusLabel.Text = "Uploading to S3";
                    UploadingToS3 = true;
                    BackgroundWorker s3BackgroundWorker = new BackgroundWorker();
                    s3BackgroundWorker.DoWork += (s, a) =>
                    {
                        S3 s3Client = new S3();
                        s3Client.UploadFolder(Path.Combine(_outputDirectory, "mods"));
                        UploadingToS3 = false;
                    };
                    s3BackgroundWorker.RunWorkerCompleted += (s, a) =>
                    {
                        UploadingToS3 = false;
                    };
                    s3BackgroundWorker.RunWorkerAsync();
                }
            }



            inputDirectoryTextBox.Items.Clear();
            try
            {
                inputDirectoryTextBox.Items.AddRange(_inputDirectories.ToArray());
            }
            catch
            {
                // ignored
            }
            toolStripProgressBar.Value = 0;
            toolStripStatusLabel.Text = "Waiting...";
        }

        public void PackForge(string forgeVersion)
        {
            BackgroundWorker worker = new BackgroundWorker();
            bool uploadToSolder = useSolderCheckBox.Checked && solderPackRadioButton.Checked;
            bool solderPack = solderPackRadioButton.Checked;
            worker.DoWork += (sender, args) =>
            {
                _runningProcess++;
                ForgeVersionInfo forgeInfo = _forgeSqlHelper.GetForgeInfo(forgeVersion);
                string tempDir = Path.Combine(_outputDirectory, "bin");
                Directory.CreateDirectory(tempDir);
                string tempFile = Path.Combine(tempDir, "modpack.jar");

                WebClient webClient = new WebClient();
                try
                {
                    webClient.DownloadFile(forgeInfo.DownloadUrl, tempFile);
                }
                catch (WebException)
                {
                    ForgeVersionSelector forgeVersionSelector = new ForgeVersionSelector(this);
                    if (!forgeVersionSelector.IsDisposed)
                    {
                        forgeVersionSelector.ShowDialog();
                        return;
                    }
                }

                if (solderPack)
                {
                    Directory.CreateDirectory(Path.Combine(_outputDirectory, "mods", "forge"));
                    string outputFile = Path.Combine(_outputDirectory, "mods", "forge",
                        "forge-" + forgeInfo.Version + ".zip");
                    if (Globalfunctions.IsUnix())
                    {
                        _startInfo.FileName = "zip";
                        Environment.CurrentDirectory = _outputDirectory;
                        _startInfo.Arguments = "-r \"" + outputFile + "\" \"bin\"";
                    }
                    else
                    {
                        _startInfo.Arguments = "a -y \"" + outputFile + "\" \"" + tempDir + "\"";
                    }
                    CreateTableRow("Minecraft Forge", "forge", forgeInfo.Version.ToLower());


                }
                else
                {
                    if (Globalfunctions.IsUnix())
                    {
                        Environment.CurrentDirectory = _outputDirectory;
                        _startInfo.FileName = "zip";
                        _startInfo.Arguments = "-r \"" + _modpackArchive + "\" \"bin\"";
                    }
                    else
                    {
                        _startInfo.Arguments = "a -y \"" + _modpackArchive + "\" \"" + tempDir + "\"";
                    }
                }
                _process.StartInfo = _startInfo;
                _process.Start();
                _process.WaitForExit();

                if (solderPack && uploadToSolder)
                {
                    int id = _solderSqlHandler.GetModId("forge");
                    if (id == -1)
                    {
                        _solderSqlHandler.AddModToSolder("forge",
                            "Minecraft Forge is a common open source API allowing a broad range of mods to work cooperatively together. It allows many mods to be created without them editing the main Minecraft code.",
                            "LexManos, Eloram, Spacetoad", "http://MinecraftForge.net", "Minecraft Forge");
                        id = _solderSqlHandler.GetModId("forge");
                    }
                    string outputFile = Path.Combine(_outputDirectory, "mods", "forge",
                        "forge-" + forgeInfo.Version + ".zip");
                    string md5 = SqlHelper.CalculateMd5(outputFile).ToLower();

                    //update the MD5 in Solder if this version of Forge is already there
                    if (_solderSqlHandler.IsModVersionOnline("forge", forgeInfo.Version))
                    {
                        _solderSqlHandler.UpdateModVersionMd5("forge", forgeInfo.Version, md5);
                    }
                    //if the version isn't there, add it
                    else
                    {
                        _solderSqlHandler.AddNewModVersionToSolder(id, forgeInfo.Version.ToLower(), md5);
                    }

                    int modVersionId = _solderSqlHandler.GetModVersionId(_solderSqlHandler.GetModId("forge"),
                        forgeInfo.Version.ToLower());
                    _solderSqlHandler.AddModVersionToBuild(_buildId, modVersionId);
                }

                Directory.Delete(tempDir, true);
                
                _runningProcess--;
            };
            worker.RunWorkerAsync();
        }

        #region Technic Pack Function



        /// <summary>
        /// Checks if the mod is on the list of mods which has custom support.
        /// </summary>
        /// <param name="modFileName">The mod file name.</param>
        /// <returns>Returns the number of the method to call, if no match is found, returns zero</returns>
        private static int IsWierdMod(string modFileName)
        {
            string[] skipMods =
                {"CarpentersBlocksCachedResources",
                    "CodeChickenLib",
                    "ejml-",
                    "commons-codec",
                    "commons-compress",
                    "Cleanup"
                };
            if (skipMods.Any(t => modFileName.ToLower().Contains(t.ToLower())))
            {
                return 0;
            }
            string[] modPatterns =
                {
                    @"liteloader"
                };
            for (int i = 0; i < modPatterns.Length; i++)
            {
                if (Regex.IsMatch(modFileName, modPatterns[i], RegexOptions.IgnoreCase))
                {
                    return i + 1;
                }
            }

            return int.MaxValue;
        }

        /// <summary>
        /// Check is the mcmod.info file has all the info we need to produce a zip file
        /// </summary>
        /// <param name="mod"></param>
        /// <returns>
        /// Returns true if everything is alright</returns>
        private static bool IsFullyInformed(McMod mod)
        {
            if (string.IsNullOrWhiteSpace(mod.Name) || string.IsNullOrWhiteSpace(mod.Version) ||
                string.IsNullOrWhiteSpace(mod.McVersion) || string.IsNullOrWhiteSpace(mod.modId) || mod.modId.ToLower().Contains("example") || mod.Name.ToLower().Contains("example") || mod.Version.ToLower().Contains("example"))
                return false;
            if (mod.Name.Contains("${") || mod.Version.Contains("${") || mod.McVersion.Contains("${") || mod.modId.Contains("${") || mod.Version.ToLower().Contains("@version@"))
            {
                return false;
            }
            return true;
        }

        private string MakeUrlFriendly(string value)
        {
            value = value.ToLower();
            return Regex.Replace(value, @"[^A-Za-z0-9_\.~]+", "-");
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
                Directory.CreateDirectory(destDirName);

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
                Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (!copySubDirs)
                return;
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }

        #region RequireUserInfo
        /*
         private void RequireUserInfo(string file)
        {
            McMod mod = new McMod { McVersion = null, modId = null, Name = null, Version = null };

            RequireUserInfo(mod, file);
        }

        private void RequireUserInfo(McMod currentData, string file)
        {
            try
            {
                McMod mod;
                string s = SqlHelper.CalculateMd5(file);
                Debug.WriteLine(s);
                DataSuggest suggest = new DataSuggest();

                try
                {
                    mod = _modsSqLHelper.GetModInfo(s);
                    Debug.WriteLine("Got from local database");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    mod = suggest.GetMcmod(s);
                    if (mod == null)
                    {
                        mod = new McMod();
                    }
                    else
                    {
                        mod.FromSuggestion = true;
                        Debug.WriteLine("Got from remove database");
                    }
                }
                if (mod == null)
                {
                    Debug.WriteLine("didn't get anything from local database");
                    mod = suggest.GetMcmod(SqlHelper.CalculateMd5(file));
                    if (mod == null)
                    {
                        mod = new McMod();
                    }
                    else
                    {
                        mod.FromSuggestion = true;
                        Debug.WriteLine("Got from remove database");
                    }
                }

                string fileName = file.Substring(file.LastIndexOf(Globalfunctions.PathSeperator) + 1);
                fileName = fileName.Remove(fileName.LastIndexOf(".", StringComparison.Ordinal));


                if (currentData.Name != null && !currentData.Name.Contains("${") && !currentData.Name.ToLower().Contains("example"))
                {
                    mod.Name = currentData.Name;

                }
                else
                {
                    if (mod.Name == null && (string.IsNullOrWhiteSpace(currentData.Name) || currentData.Name.Contains("${") || currentData.Name.ToLower().Contains("example")))
                    {
                        string a =
                            string.Format("Mod name of {0}{1}Go bug the mod author to include an mcmod.info file!",
                                fileName, Environment.NewLine);
                        mod.Name = Prompt.ShowDialog(a, "Mod Name", false,
                            Prompt.ModsLeftString(_totalMods, _currentMod));
                        currentData.Name = mod.Name;
                        if (mod.Name.Equals(""))
                            return;
                        mod.FromUserInput = true;
                    }

                }
                if (currentData.Version != null && !currentData.Version.Contains("${") && !currentData.Version.ToLower().Contains("@version@") && !currentData.Version.ToLower().Contains("example"))
                    mod.Version = currentData.Version.Replace(" ", "-").ToLower();
                else
                {
                    if (mod.Version == null && (string.IsNullOrWhiteSpace(currentData.Version) || currentData.Version.Contains("${") || currentData.Version.ToLower().Contains("@version@") || currentData.Version.ToLower().Contains("example")))
                    {
                        string a =
                            string.Format(
                                "Mod version of {0}" + Environment.NewLine +
                                "Go bug the mod author to include an mcmod.info file!", fileName);
                        mod.Version = Prompt.ShowDialog(a, "Mod Version", false,
                            Prompt.ModsLeftString(_totalMods, _currentMod));
                        mod.Version = mod.Version.Replace(" ", "-").ToLower();
                        if (mod.Version.Equals(""))
                            return;
                        mod.FromUserInput = true;
                    }
                }
                if (mod.Version != null)
                {
                    mod.Version = mod.Version.Replace(" ", "-");
                }

                if (currentData.McVersion != null && !currentData.McVersion.Contains("${") && !currentData.McVersion.ToLower().Contains("example"))
                    mod.McVersion = currentData.McVersion;
                else if (mod.McVersion == null && (string.IsNullOrWhiteSpace(currentData.McVersion) || currentData.McVersion.Contains("${") || currentData.McVersion.ToLower().Contains("example")))
                    if (_currentMcVersion == null)
                    {
                        McSelector selector = new McSelector(this);
                        selector.ShowDialog();
                        currentData.McVersion = _currentMcVersion;
                    }
                    else
                    {
                        mod.McVersion = _currentMcVersion;
                        currentData.McVersion = _currentMcVersion;
                    }

                if (!string.IsNullOrWhiteSpace(currentData.modId) && !currentData.modId.ToLower().Contains("example") && !currentData.modId.Contains("${"))
                {
                    mod.modId = currentData.modId;
                }
                //mod.Modid = currentData.Modid ?? mod.Name.Replace(" ", "").ToLower();
                if (mod.Name != null && (string.IsNullOrWhiteSpace(mod.modId) || mod.modId.Contains("${") || mod.modId.ToLower().Contains("example")))
                {
                    mod.modId = mod.Name.ToLower();
                }
                mod.modId = mod.Name.Replace(" ", "");

                string md5 = SqlHelper.CalculateMd5(file);
                if (mod.FromUserInput && !mod.FromSuggestion && !suggest.IsModSuggested(md5))
                {
                    string a = GetAuthors(mod);
                    suggest.Suggest(fileName, mod.McVersion, mod.Version, md5, mod.modId, mod.Name, a);
                }

                if (createTechnicPackCheckBox.Checked)
                    CreateTechnicModZip(mod, file);
                if (createFTBPackCheckBox.Checked)
                    CreateFtbPackZip(mod, file);
            }
            catch (NullReferenceException ex)
            {
                string error = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "error.txt");
                File.AppendAllText(error, ex.Message);
                File.AppendAllText(error, ex.StackTrace);
                MessageBox.Show("Please check the error.txt file on your desktop, and send it to the developer.");
                RequireUserInfo(file);
            }
        }
        */
        #endregion

        private void inputDirectoryBrowseButton_Click(object sender, EventArgs e)
        {
            folderBrowser.SelectedPath = inputDirectoryTextBox.SelectedText;
            DialogResult result = folderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                inputDirectoryTextBox.Text = folderBrowser.SelectedPath;
                _confighandler.SetConfig("InputDirectory", inputDirectoryTextBox.Text);
            }
            inputDirectoryTextBox_TextChanged(null, null);

        }

        private void outputDirectoryBrowseButton_Click(object sender, EventArgs e)
        {
            folderBrowser.SelectedPath = outputDirectoryTextBox.Text;
            DialogResult result = folderBrowser.ShowDialog();
            if (result != DialogResult.OK)
                return;
            outputDirectoryTextBox.Text = folderBrowser.SelectedPath;
            _confighandler.SetConfig("OutputDirectory", outputDirectoryTextBox.Text);
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void resetDatabaseButton_Click(object sender, EventArgs e)
        {
            _modsSqLHelper.ResetTable();
            string s = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolderHelper", "unarchievedFiles");
            if (Directory.Exists(s))
            {
                Directory.Delete(s, true);
            }
        }

        private void inputDirectoryTextBox_TextChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("InputDirectory", inputDirectoryTextBox.Text);

            string superDirectory = inputDirectoryTextBox.Text.Remove(inputDirectoryTextBox.Text.LastIndexOf(Globalfunctions.PathSeperator));


            if (!Directory.Exists(superDirectory))
                return;
            List<string> dirs = Directory.GetDirectories(superDirectory).Where(dir => !dir.EndsWith("mods") && !dir.EndsWith("config")).ToList();
            _additionalDirectories.Clear();
            for (int i = 0; i < dirs.Count; i ++)
            {
                if (!_additionalDirectories.ContainsKey(dirs[i]))
                {
                    string dirname = dirs[i].Substring(dirs[i].LastIndexOf(Globalfunctions.PathSeperator) + 1);
                    _additionalDirectories.Add(dirs[i], new CheckBox
                    {
                        Left = 10,
                        Top = i * 22,
                        Height = 20,
                        Text = dirname
                    });
                }
            }


            string serversDat = Path.Combine(superDirectory, "servers.dat");
            if (File.Exists(serversDat))
            {
                _additionalDirectories.Add(serversDat, new CheckBox
                {
                    Left = 10,
                    Top = dirs.Count * 22,
                    Height = 20,
                    Text = @"Servers.dat file"
                });
            }
            additionalFoldersPanel.Controls.Clear();
            foreach (CheckBox checkBox in _additionalDirectories.Values)
            {
                additionalFoldersPanel.Controls.Add(checkBox);
            }
        }

        private void outputDirectoryTextBox_TextChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("OutputDirectory", outputDirectoryTextBox.Text);
        }

        private void updateStoredFTBPermissionsButton_Click(object sender, EventArgs e)
        {
            UpdateFtbPermissions();
        }

        private void UpdateFtbPermissions()
        {
            var bw = new BackgroundWorker();
            bw.DoWork += (s, args) =>
            {
                UpdatingPermissions = true;
                FtbPermissionsSqlHelper ftbPermissionsSqlHelper = new FtbPermissionsSqlHelper();
                ftbPermissionsSqlHelper.LoadOnlinePermissions();
            };
            bw.RunWorkerCompleted += (s, a) =>
            {
                UpdatingPermissions = false;
            };
            bw.RunWorkerAsync();
        }

        private void createFTBPackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("CreateFTBPack", createFTBPackCheckBox.Checked);

            if (createFTBPackCheckBox.Checked)
            {
                ftbDistributionLevelGroupBox.Show();
            }
            else
            {
                ftbDistributionLevelGroupBox.Hide();
            }
        }

        private void ftbPrivatePackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("CreatePrivateFTBPack", ftbPrivatePackRadioButton.Checked);
        }

        private void createTechnicPackCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("CreateTechnicSolderFiles", createTechnicPackCheckBox.Checked);
            if (createTechnicPackCheckBox.Checked)
            {
                packTypeGroupBox.Show();
                ftbDistributionLevelGroupBox.Location = new Point(ftbDistributionLevelGroupBox.Location.X, ftbDistributionLevelGroupBox.Location.Y + packTypeGroupBox.Height);
                createFTBPackCheckBox.Location = new Point(createFTBPackCheckBox.Location.X, createFTBPackCheckBox.Location.Y + packTypeGroupBox.Height);
                if (checkTechnicPermissionsCheckBox.Checked)
                {
                    technicDistributionLevelGroupBox.Show();
                }
                else
                {
                    technicDistributionLevelGroupBox.Hide();
                }
                if (includeForgeZipCheckBox.Checked)
                {
                    forgeVersionLabel.Show();
                    forgeVersionDropdown.Show();
                }
                else
                {
                    forgeVersionLabel.Hide();
                    forgeVersionDropdown.Hide();
                }
            }
            else
            {
                packTypeGroupBox.Hide();
                ftbDistributionLevelGroupBox.Location = new Point(ftbDistributionLevelGroupBox.Location.X, ftbDistributionLevelGroupBox.Location.Y - packTypeGroupBox.Height);
                createFTBPackCheckBox.Location = new Point(createFTBPackCheckBox.Location.X, createFTBPackCheckBox.Location.Y - packTypeGroupBox.Height);
                technicDistributionLevelGroupBox.Hide();
                forgeVersionLabel.Hide();
                forgeVersionDropdown.Hide();
            }
        }

        private void includeConfigZipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("CreateTechnicConfigZip", includeConfigZipCheckBox.Checked);
        }

        private void solderPackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("CreateSolderPack", solderPackRadioButton.Checked);

            if (solderPackRadioButton.Checked)
            {
                includeForgeZipCheckBox.Text = "Create Forge zip";
                includeConfigZipCheckBox.Text = "Create config zip";
            }
            else
            {
                includeForgeZipCheckBox.Text = "Include Forge in zip";
                includeConfigZipCheckBox.Text = "Include configs in zip";
            }
        }

        private void checkTechnicPermissionsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("CheckTechnicPermissions", checkTechnicPermissionsCheckBox.Checked);

            if (checkTechnicPermissionsCheckBox.Checked)
                technicDistributionLevelGroupBox.Show();
            else
                technicDistributionLevelGroupBox.Hide();
        }

        private void technicPrivatePackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("TechnicPrivatePermissionsLevel", technicPrivatePackRadioButton.Checked);
        }

        #endregion

        private void uploadToFTPServerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("UploadToFTPServer", uploadToFTPServerCheckBox.Checked);

            if (uploadToFTPServerCheckBox.Checked)
            {
                bool hasBeenWarned = false;
                try
                {
                    hasBeenWarned = Convert.ToBoolean(_confighandler.GetConfig("HasBeenWarnedAboutLongFTPTimes"));
                }
                catch
                {
                    // ignored
                }
                if (!hasBeenWarned)
                {
                    _confighandler.SetConfig("HasBeenWarnedAboutLongFTPTimes", true);
                    var response = MessageBox.Show(@"Uploading to FTP can take a very long time. Do you still want to upload to FTP?", @"FTP upload", MessageBoxButtons.YesNo);
                    if (response == DialogResult.Yes)
                        configureFtpButton.Show();
                    else
                    {
                        uploadToFTPServerCheckBox.Checked = false;
                        return;
                    }
                }
                configureFtpButton.Show();
            }
            else
            {
                configureFtpButton.Hide();
                return;
            }
            if (_ftp == null)
                _ftp = new Ftp();
        }

        private void MCversion_SelectedIndexChanged(object sender, EventArgs e)
        {
            forgeVersionDropdown.Items.Clear();
            string selectedMcVersion = mcVersionDropdown.SelectedItem.ToString();
            List<string> forgeVersions = _forgeSqlHelper.GetForgeBuilds(selectedMcVersion);

            foreach (string build in forgeVersions)
                forgeVersionDropdown.Items.Add(build);
        }

        private void getForgeAndMcVersionsButton_Click(object sender, EventArgs e)
        {
            UpdateForgeVersions();
        }

        private void UpdateForgeVersions()
        {

            var bw = new BackgroundWorker();
            bw.DoWork += (s, args) =>
            {

                UpdatingForge = true;
                ForgeSqlHelper forgeSqlHelper = new ForgeSqlHelper();
                forgeSqlHelper.FindAllForgeVersions();
            };
            bw.RunWorkerCompleted += (s, args) =>
            {
                UpdatingForge = false;
                List<string> mcVersions = _forgeSqlHelper.GetMcVersions();
                foreach (string mcVersion in mcVersions)
                {
                    mcVersionDropdown.Items.Add(mcVersion);
                }
            };
            mcVersionDropdown.Items.Clear();
            bw.RunWorkerAsync();
        }

        private void includeForgeZipCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("IncludeForgeVersion", includeForgeZipCheckBox.Checked);

            if (includeForgeZipCheckBox.Checked)
            {
                forgeVersionLabel.Show();
                forgeVersionDropdown.Show();
            }
            else
            {
                forgeVersionLabel.Hide();
                forgeVersionDropdown.Hide();
            }
        }

        private void getLiteLoaderVersionsButton_Click(object sender, EventArgs e)
        {
            UpdateLiteloaderVersions();
        }

        private void UpdateLiteloaderVersions()
        {

            var bw = new BackgroundWorker();
            bw.DoWork += (s, args) =>
            {

                UpdatingLiteloader = true;
                LiteloaderSqlHelper liteloaderSqlHelper = new LiteloaderSqlHelper();
                liteloaderSqlHelper.FindAllLiteloaderVersions();
            };
            bw.RunWorkerCompleted += (s, args) =>
            {
                UpdatingLiteloader = false;
            };
            bw.RunWorkerAsync();
        }

        private void configureFtpButton_Click(object sender, EventArgs e)
        {
            Form f = new FtpInfo();
            f.ShowDialog();
            _ftp = new Ftp();
        }

        private void OnApplicationClosing(object sender, EventArgs e)
        {
            //MessageBox.Show("Exiting");
            string json = JsonConvert.SerializeObject(_inputDirectories);
            FileInfo inputDirectoriesFile = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolderHelper", "inputDirectories.json"));
            File.WriteAllText(inputDirectoriesFile.ToString(), json);
        }

        private void uploadToSFTPCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("UploadToSFTPServer", uploadToSFTPCheckBox.Checked);

            if (uploadToSFTPCheckBox.Checked)
            {
                bool hasBeenWarned = false;
                try
                {
                    hasBeenWarned = Convert.ToBoolean(_confighandler.GetConfig("HasBeenWarnedAboutLongSFTPTimes"));
                }
                catch
                {
                    // ignored
                }
                if (!hasBeenWarned)
                {
                    _confighandler.SetConfig("HasBeenWarnedAboutLongSFTPTimes", true);
                    var response = MessageBox.Show("Uploading with SFTP may be slower or faster than FTP, depending on your network. The upload may take some time.\nDo you still want to use SFTP?", @"SFTP upload", MessageBoxButtons.YesNo);
                    if (response == DialogResult.No)

                    {
                        uploadToSFTPCheckBox.Checked = false;
                        return;
                    }
                }
                configureSftpButton.Show();
            }
            else
            {
                configureSftpButton.Hide();
                return;
            }

        }

        private void doDebugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (doDebugCheckBox.Checked)
            {
                Debug.WriteLine("Started debug output.");
            }
            else
            {
                Debug.WriteLine("Stopped debug output.", true);
            }
        }

        private void SolderHelper_Load(object sender, EventArgs e)
        {
            uploadToSFTPCheckBox.Checked = bool.Parse(_confighandler.GetConfig("UploadToSFTPServer"));
            uploadToSFTPCheckBox_CheckedChanged(null, null);
        }

        private void configureSftpButton_Click(object sender, EventArgs e)
        {
            FileUpload.Sftp.SftpInfo sftp = new SftpInfo();
            sftp.ShowDialog();
        }

        private void testmysql_Click(object sender, EventArgs e)
        {
            Form f = new ModInfo(this);
            f.Show();
        }

        private void configureSolder_Click(object sender, EventArgs e)
        {
            Form f = new SqlInfo();
            f.ShowDialog();
            _solderSqlHandler = new SolderSqlHandler();
        }

        private void useSolderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("useSolder", useSolderCheckBox.Checked);
            if (useSolderCheckBox.Checked)
            {
                configureSolderMySQLButton.Show();
                minimumJavaVersionLabel.Show();
                minimumMemoryLabel.Show();
                minimumMemoryTextBox.Show();
                minimumJavaVersionDropdown.Show();
            }
            else
            {
                configureSolderMySQLButton.Hide();
                minimumJavaVersionLabel.Hide();
                minimumMemoryLabel.Hide();
                minimumMemoryTextBox.Hide();
                minimumJavaVersionDropdown.Hide();
            }
        }

        private void useS3CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _confighandler.SetConfig("useS3", useS3CheckBox.Checked);
            configureS3Button.Visible = useS3CheckBox.Checked;
        }

        private void configureS3Button_Click(object sender, EventArgs e)
        {
            Form f = new S3Info();
            f.Show();
        }

        private void editModDataButton_Click(object sender, EventArgs e)
        {
            Form f = new DatabaseEditor();
            f.Show();
        }

        private void mcVersionDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            forgeVersionDropdown.Items.Clear();
            string selectedMcVersion = mcVersionDropdown.SelectedItem.ToString();
            List<string> forgeVersions = _forgeSqlHelper.GetForgeBuilds(selectedMcVersion);
            foreach (string build in forgeVersions)
                forgeVersionDropdown.Items.Add(build);
            forgeVersionDropdown.SelectedIndex = forgeVersionDropdown.Items.Count - 1;
        }

        private void toolStripStatusLabel_TextChanged(object sender, EventArgs e)
        {
            statusStrip.Refresh();
            Debug.WriteLine("changed");
        }

        private void generatePermissionsButton_Click(object sender, EventArgs e)
        {
            _inputDirectory = inputDirectoryTextBox.Text;
            _outputDirectory = outputDirectoryTextBox.Text;
            _ftbOwnPermissionList = Path.Combine(_outputDirectory, "Own Permission List.txt");
            _ftbPermissionList = Path.Combine(_outputDirectory, "FTB Permission List.txt");
            _technicPermissionList = Path.Combine(_outputDirectory, "Technic Permission List.txt");
            string unknownFilesList = Path.Combine(_outputDirectory, "Unknown Files.txt");
            string errorPermissionListTechnic = Path.Combine(_outputDirectory, "Missing Permissions Technic.txt");
            string errorPermissionListFtb = Path.Combine(_outputDirectory, "Missing Permissions FTB.txt");

            if (File.Exists(_ftbOwnPermissionList))
            {
                File.Delete(_ftbOwnPermissionList);
            }
            if (File.Exists(_ftbPermissionList))
            {
                File.Delete(_ftbPermissionList);
            }
            if (File.Exists(_technicPermissionList))
            {
                File.Delete(_technicPermissionList);
            }
            if (File.Exists(unknownFilesList))
            {
                File.Delete(unknownFilesList);
            }
            if (File.Exists(errorPermissionListTechnic))
            {
                File.Delete(errorPermissionListTechnic);
            }
            if (File.Exists(errorPermissionListFtb))
            {
                File.Delete(errorPermissionListFtb);
            }
            if (!Directory.Exists(_outputDirectory))
            {
                Directory.CreateDirectory(_outputDirectory);
            }

            List<string> files = GetModFiles();
            foreach (string file in files)
            {
                McMod mod = _modsSqLHelper.GetModInfo(SqlHelper.CalculateMd5(file));
                if (mod == null)
                {
                    var errorFile = new FileInfo(file);
                    File.AppendAllText(unknownFilesList, errorFile.Name + Environment.NewLine);
                    continue;
                }

                // Output Technic Permissions
                if (createTechnicPackCheckBox.Checked)
                {
                    PermissionPolicy permissionLevel = _ftbPermsSqLhelper.FindPermissionPolicy(mod.modId,
                        ftbPublicPackRadioButton.Checked);
                    OwnPermissions ownPermissions;
                    switch (permissionLevel)
                    {
                        case PermissionPolicy.Open:
                            CreateTechnicPermissionInfo(mod, permissionLevel);
                            break;
                        case PermissionPolicy.Notify:
                            ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                            if (ownPermissions.HasPermission)
                            {
                                string customPermissionText = "Proof of notitification: " +
                                                              ownPermissions.PermissionLink;
                                CreateTechnicPermissionInfo(mod, permissionLevel, customPermissionText);
                            }
                            else
                            {
                                string error = string.Format("{0} with id {1}{2}By {3}{2}PermissionState is {4}{2}{2}",
                                    mod.Name, mod.modId, Environment.NewLine, GetAuthors(mod, true),
                                    permissionLevel.ToString());
                                File.AppendAllText(errorPermissionListTechnic, error);
                            }
                            break;
                        case PermissionPolicy.FTB:
                            ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                            if (ownPermissions.HasPermission)
                            {
                                string customPermissionText = "Proof of permission outside of FTB: " +
                                                              ownPermissions.PermissionLink;
                                CreateTechnicPermissionInfo(mod, permissionLevel, customPermissionText,
                                    ownPermissions.ModLink);
                            }
                            else
                            {
                                string error = string.Format("{0} with id {1}{2}By {3}{2}PermissionState is {4}{2}{2}",
                                    mod.Name, mod.modId, Environment.NewLine, GetAuthors(mod, true),
                                    permissionLevel.ToString());
                                File.AppendAllText(errorPermissionListTechnic, error);
                            }
                            break;
                        case PermissionPolicy.Request:
                        case PermissionPolicy.Closed:
                        case PermissionPolicy.Unknown:
                            ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                            if (ownPermissions.HasPermission)
                            {
                                string customPermissionText = GetAuthors(mod) + " has given permission as seen here: " +
                                                              ownPermissions.PermissionLink;
                                CreateTechnicPermissionInfo(mod, permissionLevel, customPermissionText,
                                    ownPermissions.ModLink);
                            }
                            else
                            {
                                string error = string.Format("{0} with id {1}{2}By {3}{2}PermissionState is {4}{2}{2}",
                                    mod.Name, mod.modId, Environment.NewLine, GetAuthors(mod, true),
                                    permissionLevel.ToString());
                                File.AppendAllText(errorPermissionListTechnic, error);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                // Output FTB permissions
                if (createFTBPackCheckBox.Checked)
                {
                    PermissionPolicy permissionLevel = _ftbPermsSqLhelper.FindPermissionPolicy(mod.modId,
                        ftbPublicPackRadioButton.Checked);
                    switch (permissionLevel)
                    {
                        case PermissionPolicy.Open:
                        case PermissionPolicy.FTB:
                            CreateFtbPermissionInfo(mod, permissionLevel);
                            break;
                        case PermissionPolicy.Notify:
                        case PermissionPolicy.Request:
                        case PermissionPolicy.Closed:
                        case PermissionPolicy.Unknown:
                            var ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                            if (ownPermissions.HasPermission)
                            {
                                //Get Author
                                string a = _ftbPermsSqLhelper.GetPermissionFromModId(mod.modId).modAuthors;
                                CreateFtbPermissionInfo(mod.Name, mod.modId, a, ownPermissions.PermissionLink);
                            }
                            else
                            {
                                string error = string.Format("{0} with id {1}{2}By {3}{2}PermissionState is {4}{2}{2}",
                                    mod.Name, mod.modId, Environment.NewLine, GetAuthors(mod, true),
                                    permissionLevel.ToString());
                                File.AppendAllText(errorPermissionListFtb, error);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
    }
}
