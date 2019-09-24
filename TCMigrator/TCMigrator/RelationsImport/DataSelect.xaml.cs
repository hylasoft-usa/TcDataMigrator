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
using TCDataUtilities.CSV;
using TCDataUtilities.Database;
using TCDataUtilities.DataModel;
using TCMigration.DataModel;

namespace TCMigrator.RelationsImport
{
    /// <summary>
    /// Interaction logic for DataSelect.xaml
    /// </summary>
    public partial class DataSelect : Page
    {
        private List<string> tables;
        private List<String> columnList;
        private IDbConnection con;
        List<ComboBox> boxes;
        public DataSelect()
        {
            InitializeComponent();
            con= DB.DBProvider.GetDBConnection();
            tables = con.getTables();
            List<ComboBox> boxes = new List<ComboBox>() { ParentIdColumn, ParentRevIdColumn, ParentTypeColumn, ParentRevTypeColumn, ChildIdColumn, ChildRevIdColumn, ChildTypeColumn, ChildRevTypeColumn, RelationType };  
        }
        private void initTables()
        {
            tables = con.getTables();
            foreach(String tn in tables)
            {
                Tables.Items.Add(tn);
            }
        }
        private void GetColumns(object sender, SelectionChangedEventArgs e)
        {
            var tn = Tables.Items[Tables.SelectedIndex].ToString();
            columnList = con.getTableColumns(tn);
            foreach(string col in columnList)
            {
                foreach(ComboBox cb in boxes)
                {
                    cb.Items.Add(col);
                }
            }
        }
        public void checkTablesLoaded(object sender, EventArgs e)
        {
            if (Tables.Items.Count < 1)
            {
                initTables();
            }
        }


        private void FormatImportData(object sender, RoutedEventArgs e)
        {
            TypeSplitter ts = new TypeSplitter();
            var table = Tables.Text;
            var map = new RelationDataMap(columnList,ParentIdColumn.SelectedIndex, ParentTypeColumn.SelectedIndex, ChildIdColumn.SelectedIndex, ChildTypeColumn.SelectedIndex, RelationType.SelectedIndex, ParentRevIdColumn.SelectedIndex, ParentRevTypeColumn.SelectedIndex, ChildRevIdColumn.SelectedIndex, ChildRevTypeColumn.SelectedIndex);
            var entries = con.getEntries(table);
            List<RelationData> relations = new List<RelationData>();
            foreach(string[] entry in entries)
            {
                relations.Add(new RelationData(entry[map.ParentTypeIndex], entry[map.ParentIdIndex], entry[map.ParentRevTypeIndex], entry[map.ParentRevIdIndex], entry[map.ChildTypeIndex], entry[map.ChildIdIndex], entry[map.ChildRevTypeIndex], entry[map.ChildRevIdIndex], entry[map.RelationTypeIndex]));
            }
            ImportData id = new ImportData(table);
            id.Entries = entries;
            id.AddSplitEntrySets(ts.SplitForRelations(relations.Where(o => o.UsesParentRevision).Where(o => o.UsesChildRevision).ToList()));
            id.AddSplitEntrySets(ts.SplitForRelations(relations.Where(o => o.UsesParentRevision).Where(o => !o.UsesChildRevision).ToList()));
            id.AddSplitEntrySets(ts.SplitForRelations(relations.Where(o => !o.UsesParentRevision).Where(o => !o.UsesChildRevision).ToList()));
            id.AddSplitEntrySets(ts.SplitForRelations(relations.Where(o => !o.UsesParentRevision).Where(o => o.UsesChildRevision).ToList()));
        }
    }
}
