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
using TCDataUtilities.DataModel;
using TCMigration.DataModel;
using TCMigrator.Interfaces;

namespace TCMigrator.DatasetImport
{
    /// <summary>
    /// Interaction logic for DataSelect.xaml
    /// </summary>
    public partial class DataSelect : Page
    {
        private List<string> tables;
        private List<String> columnList;
        private TCDataUtilities.Database.IDbConnection con;
        List<ComboBox> boxes;
        IPageMediator med;
        public DataSelect(IPageMediator med)
        {
            InitializeComponent();
            con = DB.DBProvider.GetDBConnection();
            boxes = new List<ComboBox>() { ParentIdColumn, ParentRevIdColumn, ParentTypeColumn, ParentRevTypeColumn, VolumeTagColumn, FileNameColumn,OriginalFileNameColumn,SDPathColumn, DatasetTypeColumn, DatasetObjectNameColumn, RelationType };
            tables = con.getTables();

            this.med = med;
        }
        private void initTables()
        {
            tables = con.getTables();
            foreach (String tn in tables)
            {
                Tables.Items.Add(tn);
            }
        }
        private void GetColumns(object sender, SelectionChangedEventArgs e)
        {
            foreach (ComboBox cb in boxes)
            {
                cb.Items.Clear();
            }
            var tn = Tables.Items[Tables.SelectedIndex].ToString();
            columnList = con.getTableColumns(tn);
            foreach (string col in columnList)
            {
                foreach (ComboBox cb in boxes)
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
            var map = new DatasetDataMap(
                columnList,
                ParentIdColumn.SelectedIndex,
                ParentTypeColumn.SelectedIndex,
                DatasetTypeColumn.SelectedIndex,
                FileNameColumn.SelectedIndex,
                OriginalFileNameColumn.SelectedIndex,
                VolumeTagColumn.SelectedIndex,
                SDPathColumn.SelectedIndex,
                DatasetObjectNameColumn.SelectedIndex,
                RelationType.SelectedIndex,
                ParentRevIdColumn.SelectedIndex,
                ParentRevTypeColumn.SelectedIndex
                );
            var entries = con.getEntries(table);
            List<DatasetData> relations = new List<DatasetData>();
            foreach (string[] entry in entries)
            {
                relations.Add(new DatasetData(
                    entry[map.ParentIdIndex],
                    entry[map.ParentTypeIndex],
                    entry[map.DatasetTypeIndex],
                    entry[map.NewFileNameIndex],
                    entry[map.OriginalFileNameIndex],
                    entry[map.VolumeTagIndex],
                    entry[map.SdPathIndex],
                    entry[map.DatasetObjectNameIndex],
                    entry[map.RelationTypeIndex],
                    entry[map.ParentRevIdIndex],
                    entry[map.ParentRevTypeIndex])
                 );
            }
            ImportData id = new ImportData(table);
            id.Entries = entries;
            id.AddSplitEntrySets(ts.SplitForDatasets(relations));
            med.updateData(id);
            med.advance();
        }
    }
}

