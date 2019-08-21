using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC = TCMigrator.Properties.TeamcenterSettings;
using CMD = TCMigrator.Properties.CommandLineText;
using System.IO;
using TCMigrator.VisualUtilities;
using System.Threading;

namespace TCMigrator.Teamcenter
{
    public class Converter
    {
        private TCCmd cmd;
        public TCCmd TCCommandPrompt { get { return cmd; } }
        Action<UIMessage> callback = null;
        private bool? csvCompleteSuccess;
        private bool? importCompleteSuccess;

        public Converter(Action<UIMessage> action = null)
            {
                callback = action;
                cmd = new TCCmd();
                cmd.Prompt.OutputDataReceived += (s, e) => handleData(e.Data);
                cmd.Prompt.ErrorDataReceived += (s, e) => handleData(e.Data);
            cmd.Prompt.BeginOutputReadLine();
            cmd.Prompt.BeginErrorReadLine();
            }
        public void Install()
        {
            cmd.SendCommand(TC.Default.TC_DRIVE);
            cmd.SendCommand(String.Format(CMD.CHANGE_DIRECTORY, TC.Default.TC_DATA + "csv2tcxml\\"));
            cmd.SendCommand(CMD.INSTALL_CSV2TCXML);
        }
        public void InstallWithTemplates(List<String> templates)
        {
            string command = CMD.INSTALL_CSV2TCXML;
            foreach (String s in templates)
            {
                command += " " + String.Format(CMD.INSTALL_CSV2TCXML_TEMPLATE_FLAG, s);
            }
            cmd.SendCommand(command);
        }
        public bool Convert(String pathToCsv,out String xmlPath,bool shouldArchive=false)
        {
            cmd.SendCommand(Properties.TeamcenterSettings.Default.TC_DRIVE);
            cmd.SendCommand(String.Format(Properties.CommandLineText.CHANGE_DIRECTORY, Properties.TeamcenterSettings.Default.TC_DATA + "csv2tcxml\\"));
            cmd.SendCommand(String.Format(CMD.CONVERT_CSV2TCXML, pathToCsv));
            xmlPath= pathToCsv + ".xml";
            var success=awaitConvertCompletion();
            if (shouldArchive)
            {
                archive(getParentDirectory(pathToCsv));
            }
            return success;
        }
        public bool ConvertWithParameters(String path, List<String> parameters,out String xmlPath, bool shouldArchive=false)
        {
            cmd.SendCommand(Properties.TeamcenterSettings.Default.TC_DRIVE);
            cmd.SendCommand(String.Format(Properties.CommandLineText.CHANGE_DIRECTORY, Properties.TeamcenterSettings.Default.TC_DATA + "csv2tcxml\\"));
            string command = String.Format(CMD.CONVERT_CSV2TCXML,path);
            if (parameters.Count > 0) { command += " --param "; }
        foreach (String s in parameters)
            {
                command += " " + s;
            }
            cmd.SendCommand(command);
            xmlPath=path + ".xml";
            var success = awaitConvertCompletion();
            if (shouldArchive)
            {
                archive(getParentDirectory(path));
            }
            return success;
        }
        public bool Import(String directory)
        {
            cmd.SendCommand(String.Format(Properties.CommandLineText.CHANGE_DIRECTORY, directory));
            var command = String.Format(@"tcxml_import -file=import.csv -bulk_load -bypass_inferdelete");
            cmd.SendCommand(command);
            var success= awaitImportCompletion();
            archive(directory);
            return success;
        }
        public bool Import(String directory,string user, string password, string group)
        {
            cmd.SendCommand(getDriveLetter(directory));
            cmd.SendCommand(String.Format(Properties.CommandLineText.CHANGE_DIRECTORY, directory));
            var command = String.Format(@"tcxml_import -file=import.csv.xml -u={0} -p={1} -g={2} -bulk_load -bypass_inferdelete",user,password, group);
            cmd.SendCommand(command);
            var success = awaitImportCompletion();
            archive(directory);
            return success;

    }
        public bool Import(String path,String directory, string user, string password, string group)
        {
            cmd.SendCommand(getDriveLetter(directory));
            cmd.SendCommand(String.Format(Properties.CommandLineText.CHANGE_DIRECTORY, directory));
            var command = String.Format(@"tcxml_import -file={0} -u={1} -p={2} -g={3} -bulk_load -bypass_inferdelete", path,user, password, group);
            cmd.SendCommand(command);
            var success = awaitImportCompletion();
            archive(directory);
            return success;

        }
        public void archive(string path)
        {
            var achriveExtensions = new string[] { ".txt", ".csv", ".xml", ".log" };
            var now = DateTime.Now;
            var filename = String.Format("Import_{0}_{1}_{2} {3}-{4}-{5}\\", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            string dirname = "";
            if (path.EndsWith(@"\"))
            {
                dirname = path + filename;
            }
            else
            {
                dirname = path + @"\" + filename;
            }
            if (!Directory.Exists(dirname))
            {
                Directory.CreateDirectory(dirname);
            }
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] f = d.GetFiles();
            foreach(FileInfo fi in f)
            {
                File.Move(fi.FullName, dirname + fi.Name);
            }

        }
        private string getDriveLetter(string path)
        {
            return path[0]+":";
        }
        private string getParentDirectory(string path)
        {
            var outPath = "";
            var parts = path.Split('\\');
            for(var x = 0; x < parts.Length; x++)
            {
                outPath += parts[x] + @"\";
            }
            return outPath;

        }
        private void checkCompletionStatus(object d)
        {
            var data = (string)d;
            if (data.Contains(Properties.CommandLineText.CSV_SUCCESS))
            {
                csvCompleteSuccess = true;
            }
            if (data.Contains(Properties.CommandLineText.CSV_FAILURE))
            {
                csvCompleteSuccess = false;
            }
            if (data.Contains(Properties.CommandLineText.TCXML_IMPORT_FAILURE))
            {
                importCompleteSuccess = false;
            }
            if (data.Contains(Properties.CommandLineText.TCXML_IMPORT_SUCCESS))
            {
                importCompleteSuccess = true;
            }
        }
        private void handleData(object d)
        {
            checkCompletionStatus(d);
            var data = d.ToString();
            if (data.Contains(Properties.CommandLineText.CSV_SUCCESS) || data.Contains(Properties.CommandLineText.TCXML_IMPORT_SUCCESS))
            {
                UIMessage m = new UIMessage(UIMessageType.SUCCESS, d.ToString());
                if (callback != null) { callback(m); }
            }
            else if (data.Contains(Properties.CommandLineText.CSV_FAILURE) || data.Contains(Properties.CommandLineText.TCXML_IMPORT_FAILURE))
            {
                UIMessage m = new UIMessage(UIMessageType.ERROR, d.ToString());
                if (callback != null) { callback(m); }
            }
            else
            {
                UIMessage m = new UIMessage(UIMessageType.DATA, d.ToString());
                if (callback != null) { callback(m); }
            }
        }
        private bool awaitImportCompletion() {
            while (!importCompleteSuccess.HasValue)
            {
                Thread.Sleep(50);
            }
            return importCompleteSuccess.Value;
        }
        private bool awaitConvertCompletion()
        {
            while (!csvCompleteSuccess.HasValue)
            {
                Thread.Sleep(50);
            }
            return csvCompleteSuccess.Value;
        }

    }
}
