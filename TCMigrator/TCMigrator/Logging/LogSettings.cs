using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.Logging
{
    public class LogSettings
    {
        private static LogSettings _instance = new LogSettings();
        static LogSettings()
        {

        }
        public static LogSettings Instance { get { return _instance; } }
        public LogLevel LogLevel
        {
            get
            {
                return GetLogLevelFromString(Properties.LogSettings.Default.LOG_LEVEL);
            }
        }
        public int LogLevelInt
        {
            get
            {
                return (int)LogLevel;
            }
        }
        public string ErrorLogName { get { return Properties.LogSettings.Default.ERROR_LOG; } }
        public string ExecutionLogName { get { return Properties.LogSettings.Default.EXECUTION_LOG; } }
        public string DataLogName { get { return Properties.LogSettings.Default.DATA_LOG; } }
        public string LogLogName { get { return Properties.LogSettings.Default.LOG_LOG; } }
        public string LogBasePath { get { return Properties.LogSettings.Default.LOG_PATH_BASE; } }
        public string ErrorLogFullPath { get { return Path.Combine(LogBasePath, ErrorLogName); } }
        public string ExecutionLogFullPath { get { return Path.Combine(LogBasePath, ExecutionLogName); } }
        public string DataLogFullPath { get { return Path.Combine(LogBasePath, DataLogName); } }
        public string LogLogFullPath { get { return Path.Combine(LogBasePath, LogLogName); } }
        public LogLevel GetLogLevelFromString(string levelString)
        {
            LogLevel level = LogLevel.UNKNOWN;
            string[] levelNames = Enum.GetNames(typeof(LogLevel));
            foreach (string s in levelNames)
            {
                if (s.ToUpper() == levelString.ToUpper())
                {
                    level = (LogLevel)Enum.Parse(typeof(LogLevel), s);
                }
            }
            return level;
        }
    }
}
