﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TCMigrator.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.1.0.0")]
    public sealed partial class LogSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static LogSettings defaultInstance = ((LogSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new LogSettings())));
        
        public static LogSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("VERBOSE")]
        public string LOG_LEVEL {
            get {
                return ((string)(this["LOG_LEVEL"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\EngineeringUSA\\Logs\\")]
        public string LOG_PATH_BASE {
            get {
                return ((string)(this["LOG_PATH_BASE"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ExecutionLog.txt")]
        public string EXECUTION_LOG {
            get {
                return ((string)(this["EXECUTION_LOG"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ErrorLog.txt")]
        public string ERROR_LOG {
            get {
                return ((string)(this["ERROR_LOG"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("DataLog.txt")]
        public string DATA_LOG {
            get {
                return ((string)(this["DATA_LOG"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("LogLog.txt")]
        public string LOG_LOG {
            get {
                return ((string)(this["LOG_LOG"]));
            }
        }
    }
}
