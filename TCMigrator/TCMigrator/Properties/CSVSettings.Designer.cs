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
    public sealed partial class CSVSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static CSVSettings defaultInstance = ((CSVSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new CSVSettings())));
        
        public static CSVSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\EngineeringUSA\\CSV\\")]
        public string CSVDirectory {
            get {
                return ((string)(this["CSVDirectory"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("import")]
        public string DefaultCSVName {
            get {
                return ((string)(this["DefaultCSVName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("|")]
        public char CSVSeparator {
            get {
                return ((char)(this["CSVSeparator"]));
            }
        }
    }
}
