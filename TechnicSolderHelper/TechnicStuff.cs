using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TechnicSolderHelper.SQL;

namespace TechnicSolderHelper
{
    public partial class SolderHelper
    {
        public StringBuilder AddedModStringBuilder = new StringBuilder();
        private readonly Dictionary<string, int> _processesUsingModID = new Dictionary<string, int>();

        private void CreateOwnPermissionInfo(string modName, string modid, string modAuthor, string linkToPermission, string modLink)
        {
            string output = string.Format("{0}({1}) by {2} {3}Permission: {4} {3}Link to mod: {5}{3}{3}", modName, modid, modAuthor, Environment.NewLine, linkToPermission, modLink);
            File.AppendAllText(_ftbOwnPermissionList, output);
        }

        private void CreateTableRow(string firstColumn, string secondColumn, string thirdColumn)
        {
            string addedMod = "<tr>";
            addedMod += string.Format("<td><input readonly class=\"containsInfo\" value=\"{0}\"></td>", firstColumn);
            addedMod += string.Format("<td><input readonly class=\"containsInfo\" value=\"{0}\"></td>", secondColumn);
            addedMod += string.Format("<td><input readonly class=\"containsInfo\" value=\"{0}\"></td>", thirdColumn);
            addedMod += "<td><button class=\"Hide\" type=\"button\">Hide</button></td></tr>" + Environment.NewLine;
            AddedModStringBuilder.Append(addedMod);
            //File.AppendAllText(_path, addedMod + Environment.NewLine);
        }

        private void CreateConfigZip()
        {
            if (solderPackRadioButton.Checked)
            {
                string inputDirectory = this.inputDirectoryTextBox.Text;
                inputDirectory = inputDirectory.Replace(Globalfunctions.PathSeperator + "mods", "");

                _outputDirectory = outputDirectoryTextBox.Text;
                string configFileName;
                if (_modpackName == null)
                    configFileName =
                        MakeUrlFriendly(
                        Prompt.ShowDialog(
                            "What do you want the file name of the config " + Environment.NewLine + "folder to be?",
                            "Config FileInfo Name"));
                else
                    configFileName = MakeUrlFriendly(_modpackName) + "-configs";
                var configVersion = _modpackVersion ?? Prompt.ShowDialog("What is the config version?", "Config Version");
                string configFileZipName = configFileName + "-" + configVersion;
                if (!(configFileZipName.EndsWith(".zip")))
                {
                    configFileZipName = configFileZipName.ToLower().Replace(" ", "-") + ".zip";
                }
                if (Globalfunctions.IsUnix())
                {
                    _startInfo.FileName = "zip";
                    Directory.CreateDirectory(_outputDirectory + "/mods/" + configFileName.ToLower());
                    Environment.CurrentDirectory = inputDirectory;
                    _startInfo.Arguments = string.Format("-r \"{0}/mods/{1}/{2}\" \"config\" -x config/YAMPST.nbt", _outputDirectory, configFileName.ToLower(), configFileZipName.ToLower());
                }
                else
                {
                    _startInfo.Arguments = string.Format("a -x!config\\YAMPST.nbt -y \"{0}\\mods\\{1}\\{2}\" \"{3}\\config" + "\"", _outputDirectory, configFileName.ToLower(), configFileZipName.ToLower(), inputDirectory);
                }
                _process.StartInfo = _startInfo;
                _process.Start();

                CreateTableRow(configFileName, configFileName.ToLower(), configVersion.ToLower());

                _process.WaitForExit();

                if (!useSolderCheckBox.Checked)
                    return;
                int id = _solderSqlHandler.GetModId(configFileName.ToLower());
                if (id == -1)
                {
                    _solderSqlHandler.AddModToSolder(configFileName.ToLower(), null, null, null, configFileName);
                    id = _solderSqlHandler.GetModId(configFileName.ToLower());
                }
                string outputFile = Path.Combine(_outputDirectory, "mods", configFileName.ToLower(), configFileZipName.ToLower());
                _solderSqlHandler.AddNewModVersionToSolder(id, _modpackVersion, SqlHelper.CalculateMd5(outputFile).ToLower());

                int modVersionId = _solderSqlHandler.GetModVersionId(_solderSqlHandler.GetModId(configFileName.ToLower()), _modpackVersion);
                _solderSqlHandler.AddModVersionToBuild(_buildId, modVersionId);
            }
            else
            {
                if (Globalfunctions.IsUnix())
                {
                    Environment.CurrentDirectory = _inputDirectory.Remove(_inputDirectory.LastIndexOf(Globalfunctions.PathSeperator));
                    _startInfo.FileName = "zip";
                    _startInfo.Arguments = string.Format("-r \"{0}\" \"config\" -x config/YAMPST.nbt", _modpackArchive);
                }
                else
                {
                    var input = _inputDirectory.Replace("\\mods", "\\config");
                    _startInfo.Arguments = "a -x!config\\YAMPST.nbt -y \"" + _modpackArchive + "\" \"" + input + "\"";
                }
                _process.StartInfo = _startInfo;
                _process.Start();
                _process.WaitForExit();
            }


        }

        private void CreateTechnicPermissionInfo(McMod mod, PermissionPolicy pl, string customPermissionText = null)
        {
            string modlink = _ftbPermsSqLhelper.GetPermissionFromModId(mod.modId).modAuthors;
            while (string.IsNullOrWhiteSpace(modlink) || !Uri.IsWellFormedUriString(modlink, UriKind.Absolute))
            {
                modlink = Prompt.ShowDialog("What is the link to " + mod.Name + "?", "Mod link", false, Prompt.ModsLeftString(_totalMods, _currentMod));
                if (!Uri.IsWellFormedUriString(modlink, UriKind.Absolute))
                {
                    MessageBox.Show("Not a proper URL");
                }
            }
            CreateTechnicPermissionInfo(mod, pl, customPermissionText, modlink);
        }

        private void CreateTechnicPermissionInfo(McMod mod, PermissionPolicy pl, string customPermissionText, string modLink)
        {
            string ps = string.Format("{0}({1}) by {2}{3}At {4}{3}Permissions are {5}{3}", mod.Name, mod.modId, GetAuthors(mod), Environment.NewLine, modLink, pl);
            if (!string.IsNullOrWhiteSpace(customPermissionText))
            {
                ps += customPermissionText + Environment.NewLine;
            }
            File.AppendAllText(_technicPermissionList, ps + Environment.NewLine);
        }

        public void CreateTechnicModZip(McMod mod, string modFilePath)
        {
            if (mod.IsSkipping)
            {
                return;
            }
            string fileName = modFilePath.Substring(modFilePath.LastIndexOf(Globalfunctions.PathSeperator) + 1);
            string modMd5 = SqlHelper.CalculateMd5(modFilePath);
            _modsSqLhelper.AddMod(mod.Name, mod.modId, mod.Version, mod.McVersion, fileName, modMd5, false);
            # region permissions
            if (checkTechnicPermissionsCheckBox.Checked)
            {
                PermissionPolicy permissionPolicy = _ftbPermsSqLhelper.FindPermissionPolicy(mod.modId, technicPublicPackRadioButton.Checked);
                string overwriteLink;
                OwnPermissions ownPermissions;
                string customPermissionText;
                switch (permissionPolicy)
                {
                    case PermissionPolicy.Open:
                        CreateTechnicPermissionInfo(mod, permissionPolicy);
                        break;
                    case PermissionPolicy.Notify:
                        ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                        if (!ownPermissions.HasPermission)
                        {
                            overwriteLink = Prompt.ShowDialog(mod.Name + " requires that you notify the author of inclusion." + Environment.NewLine + "Please provide proof (an imgur link) that you have done this:" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name, true, Prompt.ModsLeftString(_totalMods, _currentMod));
                            while (true)
                            {
                                if (overwriteLink.ToLower().Equals("skip".ToLower()))
                                {
                                    mod.IsSkipping = true;
                                    return;
                                }
                                if (Uri.IsWellFormedUriString(overwriteLink, UriKind.Absolute))
                                {
                                    if (overwriteLink.ToLower().Contains("imgur"))
                                    {
                                        _ownPermsSqLhelper.AddOwnModPerm(mod.Name, mod.modId, overwriteLink);
                                        customPermissionText = "Proof of notification: " + overwriteLink;
                                        CreateTechnicPermissionInfo(mod, permissionPolicy, customPermissionText, _ftbPermsSqLhelper.GetPermissionFromModId(mod.modId).modLink);
                                        break;
                                    }
                                }
                                overwriteLink = Prompt.ShowDialog(mod.Name + " requires that you notify the author of inclusion." + Environment.NewLine + "Please provide proof (an imgur link) that you have done this:" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name, true, Prompt.ModsLeftString(_totalMods, _currentMod));
                            }
                        }
                        else
                        {
                            customPermissionText = "Proof of notification: " + ownPermissions.PermissionLink;
                            CreateTechnicPermissionInfo(mod, permissionPolicy, customPermissionText);
                        }
                        break;
                    case PermissionPolicy.FTB:
                        ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                        if (!ownPermissions.HasPermission)
                        {
                            overwriteLink = Prompt.ShowDialog("Permissions for " + mod.Name + " are FTB exclusive" + Environment.NewLine + "Please provide proof (an imgur link) that you have permission to include this mod:" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name, true, Prompt.ModsLeftString(_totalMods, _currentMod));
                            while (true)
                            {
                                if (overwriteLink.ToLower().Equals("skip".ToLower()))
                                {
                                    mod.IsSkipping = true;
                                    return;
                                }
                                if (Uri.IsWellFormedUriString(overwriteLink, UriKind.Absolute))
                                {
                                    if (overwriteLink.ToLower().Contains("imgur"))
                                    {
                                        _ownPermsSqLhelper.AddOwnModPerm(mod.Name, mod.modId, overwriteLink, _ftbPermsSqLhelper.GetPermissionFromModId(mod.modId).modLink);
                                        break;
                                    }
                                }
                                overwriteLink = Prompt.ShowDialog("Permissions for " + mod.Name + " are FTB exclusive" + Environment.NewLine + "Please provide proof (an imgur link) that you have permission to include this mod:" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name, true, Prompt.ModsLeftString(_totalMods, _currentMod));
                            }
                        }
                        ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                        customPermissionText = "Proof of permission outside of FTB: " + ownPermissions.PermissionLink;
                        CreateTechnicPermissionInfo(mod, permissionPolicy, customPermissionText, ownPermissions.ModLink);
                        break;
                    case PermissionPolicy.Request:
                        ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                        if (!ownPermissions.HasPermission)
                        {
                            overwriteLink = Prompt.ShowDialog("This mod requires that you request permissions from the mod author of " + mod.Name + Environment.NewLine + "Please provide proof (an imgur link) that you have this permission:" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name);
                            while (true)
                            {
                                if (overwriteLink.ToLower().Equals("skip".ToLower()))
                                {
                                    mod.IsSkipping = true;
                                    return;
                                }
                                if (Uri.IsWellFormedUriString(overwriteLink, UriKind.Absolute))
                                {
                                    if (overwriteLink.ToLower().Contains("imgur"))
                                    {
                                        _ownPermsSqLhelper.AddOwnModPerm(mod.Name, mod.modId, overwriteLink, _ftbPermsSqLhelper.GetPermissionFromModId(mod.modId).modLink);
                                        break;
                                    }
                                }
                                overwriteLink = Prompt.ShowDialog("This mod requires that you request permissions from the mod author of " + mod.Name + Environment.NewLine + "Please provide proof (an imgur link) that you have this permission:" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name, true, Prompt.ModsLeftString(_totalMods, _currentMod));
                            }
                        }
                        ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                        customPermissionText = GetAuthors(mod) + " has given permission as seen here: " + ownPermissions.PermissionLink;
                        CreateTechnicPermissionInfo(mod, permissionPolicy, customPermissionText, ownPermissions.ModLink);
                        break;
                    case PermissionPolicy.Closed:
                        ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                        if (!ownPermissions.HasPermission)
                        {
                            overwriteLink = Prompt.ShowDialog("The FTB permissionsheet states that permissions for " + mod.Name + " are closed." + Environment.NewLine + "Please provide proof (an imgur link) that you have permission to include this mod:" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name, true, Prompt.ModsLeftString(_totalMods, _currentMod));
                            while (true)
                            {
                                if (overwriteLink.ToLower().Equals("skip".ToLower()))
                                {
                                    mod.IsSkipping = true;
                                    return;
                                }
                                if (Uri.IsWellFormedUriString(overwriteLink, UriKind.Absolute))
                                {
                                    if (overwriteLink.ToLower().Contains("imgur"))
                                    {
                                        _ownPermsSqLhelper.AddOwnModPerm(mod.Name, mod.modId, overwriteLink, _ftbPermsSqLhelper.GetPermissionFromModId(mod.modId).modLink);
                                        break;
                                    }
                                }
                                overwriteLink = Prompt.ShowDialog("The FTB permissionsheet states that permissions for " + mod.Name + " is closed." + Environment.NewLine + "Please provide proof (an imgur link) that you have permission to include this mod:" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name);
                            }
                        }
                        ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                        customPermissionText = GetAuthors(mod) + " has given permission as seen here: " + ownPermissions.PermissionLink;
                        CreateTechnicPermissionInfo(mod, permissionPolicy, customPermissionText, ownPermissions.ModLink);
                        break;
                    case PermissionPolicy.Unknown:
                        ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                        var modLink = ownPermissions.ModLink;
                        if (!ownPermissions.HasPermission)
                        {
                            overwriteLink = Prompt.ShowDialog("Permissions for " + mod.Name + " are unknown" + Environment.NewLine + "Please provide proof (an imgur link) of permissions:" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name, true, Prompt.ModsLeftString(_totalMods, _currentMod));
                            while (true)
                            {
                                if (overwriteLink.ToLower().Equals("skip".ToLower()))
                                {
                                    mod.IsSkipping = true;
                                    return;
                                }
                                if (Uri.IsWellFormedUriString(overwriteLink, UriKind.Absolute))
                                {
                                    if (overwriteLink.ToLower().Contains("imgur"))
                                    {
                                        break;
                                    }
                                }
                                overwriteLink = Prompt.ShowDialog("Permissions for " + mod.Name + " are unknown" + Environment.NewLine + "Please provide proof (an imgur link) of permissions:" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name, true, Prompt.ModsLeftString(_totalMods, _currentMod));
                            }
                            while (string.IsNullOrWhiteSpace(modLink))
                            {
                                if (modLink != null && modLink.ToLower().Equals("skip".ToLower()))
                                {
                                    mod.IsSkipping = true;
                                    return;
                                }
                                if (modLink != null && Uri.IsWellFormedUriString(modLink, UriKind.Absolute))
                                {
                                    _ownPermsSqLhelper.AddOwnModPerm(mod.Name, mod.modId, overwriteLink, modLink);
                                    break;

                                }
                                modLink = Prompt.ShowDialog("Please provide a link to " + mod.Name + ":" + Environment.NewLine + "Enter \"skip\" to skip the mod.", mod.Name, true, Prompt.ModsLeftString(_totalMods, _currentMod));
                            }
                            string a = GetAuthors(mod);
                            _ownPermsSqLhelper.AddOwnModPerm(mod.Name, mod.modId, overwriteLink, modLink);
                            CreateOwnPermissionInfo(mod.Name, mod.modId, a, overwriteLink, modLink);

                        }
                        ownPermissions = _ownPermsSqLhelper.DoUserHavePermission(mod.modId);
                        customPermissionText = GetAuthors(mod) + " has given permission as seen here: " + ownPermissions.PermissionLink;
                        CreateTechnicPermissionInfo(mod, permissionPolicy, customPermissionText, ownPermissions.ModLink);
                        break;
                }
            }
            # endregion 
            if (solderPackRadioButton.Checked)
            {
                bool force = forceSolderUpdateCheckBox.Checked;
                bool useSolder = useSolderCheckBox.Checked;
                BackgroundWorker bw = new BackgroundWorker();
                _runningProcess++;
                bw.DoWork += (o, arg) =>
                {

                    var modid = mod.GetSafeModId();
                    string modVersion = mod.McVersion.ToLower() + "-" + mod.Version.ToLower();
                    if (useSolder && !force)
                    {
                        if (_solderSqlHandler.IsModVersionOnline(modid, modVersion))
                        {
                            Debug.WriteLine(modid + " is already online with version " + modVersion);
                            int id = _solderSqlHandler.GetModId(modid);
                            int modVersionId = _solderSqlHandler.GetModVersionId(id, modVersion);
                            _solderSqlHandler.AddModVersionToBuild(_buildId, modVersionId);
                            _runningProcess--;
                            return;
                        }
                    }

                    while (true)
                    {
                        if (_processesUsingModID.ContainsKey(mod.modId))
                        {
                            Debug.WriteLine("Sleeping with id: " + mod.modId);
                            Thread.Sleep(100);
                        }
                        else
                        {
                            _processesUsingModID.Add(mod.modId, 1);
                            break;
                        }
                    }

                    if (!_modsSqLhelper.IsFileInSolder(modFilePath) || force)
                    {
                        var modDir = Path.Combine(_outputDirectory, "mods",
                            mod.GetSafeModId(), "mods");
                        Directory.CreateDirectory(modDir);
                        if (_processesUsingFolder.ContainsKey(modDir))
                        {
                            _processesUsingFolder[modDir]++;
                        }
                        else
                        {
                            _processesUsingFolder.Add(modDir, 1);
                        }
                        string tempModFile = Path.Combine(modDir, fileName);

                        string tempFileDirectory =
                            tempModFile.Remove(tempModFile.LastIndexOf(Globalfunctions.PathSeperator));

                        Directory.CreateDirectory(tempFileDirectory);
                        File.Copy(modFilePath, tempModFile, true);

                        var modArchive = Path.Combine(_outputDirectory, "mods",
                                mod.GetSafeModId(),
                                mod.GetSafeModId() + "-" + mod.McVersion.ToLower() + "-" + mod.Version.ToLower() + ".zip");
                        if (Globalfunctions.IsUnix())
                        {
                            Environment.CurrentDirectory = Path.Combine(_outputDirectory, "mods",
                                mod.GetSafeModId());
                            _startInfo.FileName = "zip";
                            _startInfo.Arguments = "-r \"" + modArchive + "\" \"mods\" ";
                        }
                        else
                        {
                            _startInfo.Arguments = "a -y \"" + modArchive + "\" \"" + modDir + "\" ";
                        }
                        Process process = new Process { StartInfo = _startInfo };

                        process.Start();

                        //Save mod to database
                        _modsSqLhelper.AddMod(mod.Name, mod.modId, mod.Version, mod.McVersion, fileName, modMd5, true);

                        // Add mod info to a html file
                        CreateTableRow(mod.Name.Replace("|", string.Empty), mod.GetSafeModId(), modVersion);

                        process.WaitForExit();

                        if (useSolder)
                        {
                            int id = -1;
                            string archive = Path.Combine(_outputDirectory, "mods", modArchive);
                            SolderSqlHandler sqh = new SolderSqlHandler();
                            string md5Value = SqlHelper.CalculateMd5(archive).ToLower();
                            if (sqh.IsModVersionOnline(mod.GetSafeModId(), modVersion))
                            {
                                Debug.WriteLine(string.Format("Updating mod on solder with Modid: {0} modversion: {1} md5value: {2}", modid, modVersion, md5Value), doDebugCheckBox.Checked);
                                sqh.UpdateModVersionMd5(mod.GetSafeModId(), modVersion, md5Value);
                                Debug.WriteLine(string.Format("Done updating mod on solder with modid: {0}", modid));
                            }
                            else
                            {
                                id = sqh.GetModId(modid);
                                if (id == -1)
                                {
                                    sqh.AddModToSolder(mod.GetSafeModId(), mod.Description, GetAuthors(mod), mod.Url,
                                        mod.Name);
                                }
                                Debug.WriteLine(string.Format("Adding mod to solder with Modid: {0} modversion: {1} md5value: {2}", modid, modVersion, md5Value), doDebugCheckBox.Checked);
                                sqh.AddNewModVersionToSolder(mod.GetSafeModId(), modVersion, md5Value);
                            }
                            id = sqh.GetModId(mod.GetSafeModId());
                            int modVersionId = sqh.GetModVersionId(id, modVersion);
                            sqh.AddModVersionToBuild(_buildId, modVersionId);
                            Debug.WriteLine(string.Format("Done adding mod {0} to build", modid));
                        }
                        Debug.WriteLine("Decrementing " + modDir);
                        _processesUsingFolder[modDir]--;
                        Debug.WriteLine("Decremented " + modDir);
                        if (Directory.Exists(modDir) && _processesUsingFolder[modDir] == 0)
                        {
                            while (true)
                            {
                                try
                                {
                                    Debug.WriteLine("Attempting to delete directory " + modDir);
                                    Directory.Delete(modDir, true);
                                    Debug.WriteLine("Done deleting directory " + modDir);
                                    break;
                                }
                                catch (IOException e)
                                {
                                    Debug.WriteLine(e.Message);
                                    Thread.Sleep(100);
                                }
                            }
                            _processesUsingFolder.Remove(modDir);
                        }
                    }
                    if (_processesUsingModID.ContainsKey(mod.modId))
                        if (_processesUsingModID[mod.modId] > 1)
                        {
                            _processesUsingModID[mod.modId]--;
                        }
                        else
                        {
                            _processesUsingModID.Remove(mod.modId);
                        }
                    _runningProcess--;
                    Debug.WriteLine("Decremented _runningProcess");
                };
                bw.RunWorkerAsync();
            }
            else
            {
                _modsSqLhelper.AddMod(mod.Name, mod.modId, mod.Version, mod.McVersion, fileName, modMd5, false);
                while (string.IsNullOrWhiteSpace(_modpackName))
                {
                    _modpackName = Prompt.ShowDialog("What is the modpack name?", "Modpack Name");
                }
                while (string.IsNullOrWhiteSpace(_modpackVersion))
                {
                    _modpackVersion = Prompt.ShowDialog("What version is the modpack?", "Modpack Version");
                }

                string tempDirectory = Path.Combine(_outputDirectory, "tmp");
                string tempModDirectory = Path.Combine(tempDirectory, "mods");
                Directory.CreateDirectory(tempModDirectory);
                string tempFile = Path.Combine(tempModDirectory, fileName);

                int index = tempFile.LastIndexOf(Globalfunctions.PathSeperator);

                string tempFileDirectory = tempFile.Remove(index);
                Directory.CreateDirectory(tempFileDirectory);
                File.Copy(modFilePath, tempFile, true);

                _modpackArchive = Path.Combine(_outputDirectory, string.Format("{0}-{1}.zip", _modpackName, _modpackVersion));

                if (Globalfunctions.IsUnix())
                {
                    Environment.CurrentDirectory = tempDirectory;
                    _startInfo.Arguments = string.Format("-r \"{0}\" \"{1}\"", _modpackArchive, "mods");
                }
                else
                {
                    _startInfo.Arguments = string.Format("a -y \"{0}\" \"{1}\"", _modpackArchive, tempModDirectory);
                }
                if (Globalfunctions.IsUnix())
                {
                    _startInfo.FileName = "zip";
                }
                _process.StartInfo = _startInfo;
                _process.Start();
                _process.WaitForExit();
                Directory.Delete(tempDirectory, true);
            }

            if (mod.HasBeenWritenToModlist)
                return;
            File.AppendAllText(_modlistTextFile, mod.Name + Environment.NewLine);
            mod.HasBeenWritenToModlist = true;
        }
    }
}
