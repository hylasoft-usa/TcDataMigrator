﻿#pragma checksum "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E081802F8727C8EC112401B677E053E1C2971ED7C3AA87F478427850F67DFAA4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TCMigrator.Standalone.TCXMXLImport;


namespace TCMigrator.Standalone.TCXMXLImport {
    
    
    /// <summary>
    /// ManualTCXMLImport
    /// </summary>
    public partial class ManualTCXMLImport : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox XmlLocation;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TcUser;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox TcPass;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TcGroup;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer Viewer;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Output;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TCMigrator;component/standalone/tcxmxlimport/manualtcxmlimport.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.XmlLocation = ((System.Windows.Controls.TextBox)(target));
            
            #line 29 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
            this.XmlLocation.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onTextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TcUser = ((System.Windows.Controls.TextBox)(target));
            
            #line 31 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
            this.TcUser.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onTextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TcPass = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 33 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
            this.TcPass.PasswordChanged += new System.Windows.RoutedEventHandler(this.onPasswordChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TcGroup = ((System.Windows.Controls.TextBox)(target));
            
            #line 35 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
            this.TcGroup.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onTextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\Standalone\TCXMXLImport\ManualTCXMLImport.xaml"
            this.btn.Click += new System.Windows.RoutedEventHandler(this.DoImport);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Viewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 7:
            this.Output = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

