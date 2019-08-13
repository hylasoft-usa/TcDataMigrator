using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.Logging
{
    public class Logger
    {
        private readonly string _singleMessageFormat = "{0}|Level: {1}|Caller: {2}|Line: {3}|File: {4}|\t\t\t{5}";
        private readonly string _multiLineMessageFormat = "{0}|Level: {1}|Caller: {2}|Line: {3}|File: {4}|";
        private LogSettings _LoggingSettings;
        private static Logger _instance = new Logger();
        static Logger()
        {

        }
        private Logger()
        {
            _LoggingSettings = LogSettings.Instance;
        }
        public void LogVerbose(LogType lt, string message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string path = null)
        {
            if (_shouldLog(LogLevel.VERBOSE))
            {
                Log(LogLevel.VERBOSE, lt, message, lineNumber, caller, path);
            }
        }
        public void LogInfo(LogType lt, string message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string path = null)
        {
            if (_shouldLog(LogLevel.INFO))
            {
                Log(LogLevel.INFO, lt, message, lineNumber, caller, path);
            }
        }
        public void LogError(LogType lt, string message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string path = null)
        {
            if (_shouldLog(LogLevel.ERROR))
            {
                Log(LogLevel.ERROR, lt, message, lineNumber, caller, path);
            }
        }
        public void LogVerbose(LogType lt, string[] message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string path = null)
        {
            if (_shouldLog(LogLevel.VERBOSE))
            {
                Log(LogLevel.VERBOSE, lt, message, lineNumber, caller, path);
            }
        }
        public void LogInfo(LogType lt, string[] message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string path = null)
        {
            if (_shouldLog(LogLevel.INFO))
            {
                Log(LogLevel.INFO, lt, message, lineNumber, caller, path);
            }
        }
        public void LogError(LogType lt, string[] message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string path = null)
        {
            if (_shouldLog(LogLevel.ERROR))
            {
                Log(LogLevel.ERROR, lt, message, lineNumber, caller, path);
            }
        }
        public void Log(LogLevel ll, LogType lt, string message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string path = null)
        {
            string formattedMessage = String.Format(_singleMessageFormat, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture), ll.ToString(), caller, lineNumber, path, message);
            WriteLog(lt, formattedMessage);
        }
        public void Log(LogLevel ll, LogType lt, string[] message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string path = null)
        {
            string formattedMessage = String.Format(_multiLineMessageFormat, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture), ll.ToString(), caller, lineNumber, path);
            int length = formattedMessage.Length;
            foreach (string s in message)
            {
                formattedMessage += "\t\t\t" + s.PadLeft(length);
                formattedMessage += Environment.NewLine;
            }
            WriteLog(lt, formattedMessage);

        }
        private void WriteLog(LogType lt, string formattedMessage)
        {
            createDirs();
            if (!File.Exists(_locateCorrectLog(lt)))
            {
                using (StreamWriter sw = File.CreateText(_locateCorrectLog(lt)))
                {
                    sw.WriteLine(formattedMessage);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(_locateCorrectLog(lt)))
                {
                    sw.WriteLine(formattedMessage);
                }
            }
        }
        private string _locateCorrectLog(LogType lt)
        {
            switch (lt)
            {
                case LogType.DATA:
                    return _LoggingSettings.DataLogFullPath;
                case LogType.ERROR:
                    return _LoggingSettings.ErrorLogFullPath;
                case LogType.EXECUTION:
                    return _LoggingSettings.ExecutionLogFullPath;
                default:
                    return _LoggingSettings.LogLogFullPath;
            }
        }
        private bool _shouldLog(LogLevel ll)
        {
            LogLevel minLevel = _LoggingSettings.LogLevel;
            if (ll >= minLevel)
            {
                return true;
            }
            return false;
        }
        private void createDirs()
        {
            Directory.CreateDirectory(_LoggingSettings.LogBasePath);
        }


    }
}
