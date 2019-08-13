using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TCMigrator.Interfaces;

namespace TCMigrator.DBImpot
{
    /// <summary>
    /// Interaction logic for ManualHeaders.xaml
    /// </summary>
    public partial class ManualHeaders : Page
    {
        IPageMediator main;
        List<DisplayObject> columns;
        public ManualHeaders(IPageMediator main)
        {
            this.main = main;
            this.columns = new List<DisplayObject>();
            InitializeComponent();
            this.formatDisplayObjects();
            this.addDisplayObjects();
        }
        private void formatDisplayObjects()
        {
            var data = main.getCurrentData();
            var cols = data.ColumnNames;
            var entries = data.Entries[0];
            
            for(var x= 0; x < cols.Count; x++)
            {
                columns.Add(new DisplayObject(cols[x], entries[x]));
            }
        }
        private void addDisplayObjects()
        {
            foreach (DisplayObject dobj in columns)
            {
                ColumnList.Items.Add(dobj);
            }
        }

        private void SetHeaders(object sender, RoutedEventArgs e)
        {
            List<String> headers = new List<String>();
            foreach(object o in ColumnList.Items)
            {
                var obj = (DisplayObject)o;
                headers.Add(obj.Header);
            }
            var data = main.getCurrentData();
            data.Headers = headers;
            main.updateData(data);
            main.advance();
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {

        }
    }

    public class DisplayObject
    {
        public string ColumnName { get; set; }
        public string ExampleData { get; set; }
        public string Header{ get; set; }
        public DisplayObject(string ColName, String exampleData)
        {
            ColumnName = ColName;
            ExampleData = exampleData;
            Header = "";
        }
    }
}
