﻿#pragma checksum "..\..\..\DBImpot\ConvertAndImport.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C397208C20E3711D888BC0285FFA0F76C036A249D600425B252F2393655589C1"
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
using TCMigrator.DBImpot;


namespace TCMigrator.DBImpot {
    
    
    /// <summary>
    /// ConvertAndImport
    /// </summary>
    public partial class ConvertAndImport : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\DBImpot\ConvertAndImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button importBtn;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\DBImpot\ConvertAndImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox User;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\DBImpot\ConvertAndImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Group;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\DBImpot\ConvertAndImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox Password;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\DBImpot\ConvertAndImport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer Viewer;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\DBImpot\ConvertAndImport.xaml"
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
            System.Uri resourceLocater = new System.Uri("/TCMigrator;component/dbimpot/convertandimport.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DBImpot\ConvertAndImport.xaml"
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
            this.importBtn = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\DBImpot\ConvertAndImport.xaml"
            this.importBtn.Click += new System.Windows.RoutedEventHandler(this.Import);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 38 "..\..\..\DBImpot\ConvertAndImport.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Back);
            
            #line default
            #line hidden
            return;
            case 3:
            this.User = ((System.Windows.Controls.TextBox)(target));
            
            #line 39 "..\..\..\DBImpot\ConvertAndImport.xaml"
            this.User.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onTextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Group = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\DBImpot\ConvertAndImport.xaml"
            this.Group.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.onTextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Password = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 41 "..\..\..\DBImpot\ConvertAndImport.xaml"
            this.Password.PasswordChanged += new System.Windows.RoutedEventHandler(this.onPasswordChanged);
            
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

