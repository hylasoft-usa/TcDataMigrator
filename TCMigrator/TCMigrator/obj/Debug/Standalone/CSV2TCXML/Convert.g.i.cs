﻿#pragma checksum "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "535E550069029C393607CB811ABC82C780238621C701EAC7BE60B2013552FA40"
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
using TCMigrator.Standalone.CSV2TCXML;


namespace TCMigrator.Standalone.CSV2TCXML {
    
    
    /// <summary>
    /// Convert
    /// </summary>
    public partial class Convert : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox User;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Group;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Password;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer Viewer;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
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
            System.Uri resourceLocater = new System.Uri("/TCMigrator;component/standalone/csv2tcxml/convert.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
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
            this.Btn = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
            this.Btn.Click += new System.Windows.RoutedEventHandler(this.Import);
            
            #line default
            #line hidden
            return;
            case 2:
            this.User = ((System.Windows.Controls.TextBox)(target));
            
            #line 37 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
            this.User.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onTextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Group = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
            this.Group.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onTextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Password = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 39 "..\..\..\..\Standalone\CSV2TCXML\Convert.xaml"
            this.Password.PasswordChanged += new System.Windows.RoutedEventHandler(this.onPasswordChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Viewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 6:
            this.Output = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

