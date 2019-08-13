using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC = TCMigrator.Properties.TeamcenterSettings;
using CMD = TCMigrator.Properties.CommandLineText;

namespace TCMigrator.Teamcenter
{
    public class Converter
    {
            private TCCmd cmd;
            public TCCmd TCCommandPrompt { get { return cmd; } }

            public Converter()
            {
                cmd = new TCCmd();
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
            public string convert(String pathToCsv)
            {
            cmd.SendCommand(Properties.TeamcenterSettings.Default.TC_DRIVE);
            cmd.SendCommand(String.Format(Properties.CommandLineText.CHANGE_DIRECTORY, Properties.TeamcenterSettings.Default.TC_DATA + "csv2tcxml\\"));
            cmd.SendCommand(String.Format(CMD.CONVERT_CSV2TCXML, pathToCsv));
                return pathToCsv + ".xml";
            }
            public string ConvertWithParameters(String path, List<String> parameters)
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
                return path + ".xml";
            }
            public void Import(String directory)
            {
                cmd.SendCommand(String.Format(Properties.CommandLineText.CHANGE_DIRECTORY, directory));
                var command = String.Format(@"tcxml_import -file=import.csv -bulk_load -bypass_inferdelete");
                cmd.SendCommand(command);

            }
            public void Import(String directory,string user, string password, string group)
            {
                cmd.SendCommand(getDriveLetter(directory));
                cmd.SendCommand(String.Format(Properties.CommandLineText.CHANGE_DIRECTORY, directory));
                var command = String.Format(@"tcxml_import -file=import.csv.xml -u={0} -p={1} -g={2} -bulk_load -bypass_inferdelete",user,password, group);
                cmd.SendCommand(command);

            }
        public void Import(String path,String directory, string user, string password, string group)
        {
            cmd.SendCommand(getDriveLetter(directory));
            cmd.SendCommand(String.Format(Properties.CommandLineText.CHANGE_DIRECTORY, directory));
            var command = String.Format(@"tcxml_import -file={0} -u={1} -p={2} -g={3} -bulk_load -bypass_inferdelete", path,user, password, group);
            cmd.SendCommand(command);

        }
        private string getDriveLetter(string path)
        {
            return path[0]+":";
        }

        }
}
