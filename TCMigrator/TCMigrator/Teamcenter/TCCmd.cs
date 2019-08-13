using System;
using System.Diagnostics;
using TC = TCMigrator.Properties.TeamcenterSettings;
using CMD = TCMigrator.Properties.CommandLineText;

namespace TCMigrator.Teamcenter
{
    public class TCCmd
    {
        private Process _cmd;
        public Process Prompt { get { return _cmd; } }
        public ProcessStartInfo _info = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            RedirectStandardInput = true,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        };

        public TCCmd()
        {
            _cmd = new Process() { StartInfo = _info };
            _cmd.EnableRaisingEvents = true;
            _cmd.Start();
            setEnvironmentVariables();
        }
        private void setEnvironmentVariables()
        {
            _cmd.StandardInput.WriteLine(CMD.SET_TC_ROOT, TC.Default.TC_ROOT);
            _cmd.StandardInput.WriteLine(CMD.SET_TC_DATA, TC.Default.TC_DATA);
            _cmd.StandardInput.WriteLine(@"D:\Siemens\TC_DATA\tc_profilevars");
            _cmd.StandardInput.Flush();
        }
        public void SendCommand(String cmd)
        {
            _cmd.StandardInput.WriteLine(cmd);
            _cmd.StandardInput.Flush();
        }
        public void Exit()
        {
            SendCommand(CMD.EXIT);
        }
    }
}
