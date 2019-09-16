
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
    /// DataSelect
    /// </summary>
    public partial class DataSelect : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\DBImpot\DataSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Tables;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\DBImpot\DataSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SelectAllCols;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\DBImpot\DataSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox AutoHeaders;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\DBImpot\DataSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SelectCol;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\DBImpot\DataSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeselectCol;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\DBImpot\DataSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox AvailableCols;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\DBImpot\DataSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox SelectedCols;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\DBImpot\DataSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button advance;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\DBImpot\DataSelect.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox Loading;
        
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
            System.Uri resourceLocater = new System.Uri("/TCMigrator;component/dbimpot/dataselect.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DBImpot\DataSelect.xaml"
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
            this.Tables = ((System.Windows.Controls.ComboBox)(target));
            
            #line 26 "..\..\..\DBImpot\DataSelect.xaml"
            this.Tables.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GetColumns);
            
            #line default
            #line hidden
            
            #line 26 "..\..\..\DBImpot\DataSelect.xaml"
            this.Tables.DropDownOpened += new System.EventHandler(this.checkTablesLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SelectAllCols = ((System.Windows.Controls.CheckBox)(target));
            
            #line 34 "..\..\..\DBImpot\DataSelect.xaml"
            this.SelectAllCols.Checked += new System.Windows.RoutedEventHandler(this.LockColumnSelection);
            
            #line default
            #line hidden
            
            #line 34 "..\..\..\DBImpot\DataSelect.xaml"
            this.SelectAllCols.Unchecked += new System.Windows.RoutedEventHandler(this.unlockColumnSelection);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AutoHeaders = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 4:
            this.SelectCol = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\DBImpot\DataSelect.xaml"
            this.SelectCol.Click += new System.Windows.RoutedEventHandler(this.AddColumnToSelected);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DeselectCol = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\DBImpot\DataSelect.xaml"
            this.DeselectCol.Click += new System.Windows.RoutedEventHandler(this.RemoveSelectedColumn);
            
            #line default
            #line hidden
            return;
            case 6:
            this.AvailableCols = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.SelectedCols = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.advance = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\DBImpot\DataSelect.xaml"
            this.advance.Click += new System.Windows.RoutedEventHandler(this.FormatImportData);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Loading = ((System.Windows.Controls.GroupBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

