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
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CommandLineText {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CommandLineText() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TCMigrator.Properties.CommandLineText", typeof(CommandLineText).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to call {0} csv2tcxml.perl install.
        /// </summary>
        public static string CALL_TC_PERL {
            get {
                return ResourceManager.GetString("CALL_TC_PERL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to cd {0}.
        /// </summary>
        public static string CHANGE_DIRECTORY {
            get {
                return ResourceManager.GetString("CHANGE_DIRECTORY", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to tcperl csv2tcxml.perl {0}.
        /// </summary>
        public static string CONVERT_CSV2TCXML {
            get {
                return ResourceManager.GetString("CONVERT_CSV2TCXML", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -p {0}={1}.
        /// </summary>
        public static string CONVERT_CSV2TCXML_PARAM_SPECIFICATION {
            get {
                return ResourceManager.GetString("CONVERT_CSV2TCXML_PARAM_SPECIFICATION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to EXIT.
        /// </summary>
        public static string EXIT {
            get {
                return ResourceManager.GetString("EXIT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to tcperl csv2tcxml.perl install.
        /// </summary>
        public static string INSTALL_CSV2TCXML {
            get {
                return ResourceManager.GetString("INSTALL_CSV2TCXML", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -t = {0}.
        /// </summary>
        public static string INSTALL_CSV2TCXML_TEMPLATE_FLAG {
            get {
                return ResourceManager.GetString("INSTALL_CSV2TCXML_TEMPLATE_FLAG", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SET TC_DATA={0}.
        /// </summary>
        public static string SET_TC_DATA {
            get {
                return ResourceManager.GetString("SET_TC_DATA", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SET TC_ROOT={0}.
        /// </summary>
        public static string SET_TC_ROOT {
            get {
                return ResourceManager.GetString("SET_TC_ROOT", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to tcxml_import –file={0} –bulk_load –bypass_inferdelete.
        /// </summary>
        public static string TC_XML_IMPORT {
            get {
                return ResourceManager.GetString("TC_XML_IMPORT", resourceCulture);
            }
        }
    }
}
