using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TCMigrator.Data;
using TCMigrator.DB;
using TCMigrator.Interfaces;

namespace TCMigrator.DBImpot
{
    /// <summary>
    /// Interaction logic for DataSelect.xaml
    /// </summary>
    public partial class DataSelect : Page
    {
        IPageMediator main;
        public DataSelect(IPageMediator main)
        {
            InitializeComponent();
            this.main = main;
            checkCanGoForward();
            checkReorgButtons();
        }

        private void InitTables()
        {
            Loading.Visibility = Visibility.Visible;
            IDbConnection con = DBProvider.GetDBConnection();
            List<String> tableNames = con.getTables();
            foreach (String s in tableNames)
            {
                Tables.Items.Add(s);
            }
            Loading.Visibility = Visibility.Hidden;
            checkCanGoForward();
        }

        private void LockColumnSelection(object sender, RoutedEventArgs e)
        {
            AvailableCols.IsEnabled = false;
            SelectedCols.IsEnabled = false;
            SelectCol.IsEnabled = false;
            DeselectCol.IsEnabled = false;
            checkCanGoForward();
        }
        private void unlockColumnSelection(object sender, RoutedEventArgs e)
        {
            AvailableCols.IsEnabled = true;
            SelectedCols.IsEnabled = true;
            SelectCol.IsEnabled = true;
            DeselectCol.IsEnabled = true;
            checkCanGoForward();
        }

        private void GetColumns(object sender, SelectionChangedEventArgs e)
        {
            AvailableCols.Items.Clear();
            SelectedCols.Items.Clear();
            if (Tables.SelectedIndex > -1)
            {
                var table = (string)Tables.SelectedItem;
                IDbConnection db = DBProvider.GetDBConnection();
                List<String> cols=db.getTableColumns(table);
                foreach(String s in cols)
                {
                    AvailableCols.Items.Add(s);
                }
            }
            checkCanGoForward();
        }
        public void checkTablesLoaded(object sender, EventArgs e)
        {
            if (Tables.Items.Count<1)
            {
                InitTables();
            }
        }
        private void MoveSelectedUp(object sender, RoutedEventArgs e)
        {
            if (SelectedCols.SelectedItem != null)
            {
                var index = SelectedCols.SelectedIndex;
                if (index > 0)
                {
                    var i1 = SelectedCols.Items[index];
                    var it2 = SelectedCols.Items[index - 1];
                    SelectedCols.Items[index] = it2;
                    SelectedCols.Items[index - 1] = i1;
                    SelectedCols.SelectedIndex = index - 1;
                }
            }
            checkReorgButtons();
        }
        private void MoveSelectedDown(object sender, RoutedEventArgs e)
        {
            if (SelectedCols.SelectedItem != null)
            {
                if (SelectedCols.SelectedIndex < SelectedCols.Items.Count - 1)
                {
                    var index = SelectedCols.SelectedIndex;
                    var i1 = SelectedCols.Items[index];
                    var i2 = SelectedCols.Items[index + 1];
                    SelectedCols.Items[index + 1] = i1;
                    SelectedCols.Items[index] = i2;
                    SelectedCols.SelectedIndex = index + 1;
                }
            }
            checkReorgButtons();
        }
        private void checkReorgButtons()
        {
            if (SelectedCols.SelectedItem != null)
            {
                if (SelectedCols.SelectedIndex > 0)
                {
                    MoveItemUp.IsEnabled = true;
                }
                else
                {
                    MoveItemUp.IsEnabled = false;
                }
                if (SelectedCols.SelectedIndex < SelectedCols.Items.Count - 1)
                {
                    MoveItemDown.IsEnabled = true;
                }
                else
                {
                    MoveItemDown.IsEnabled = false;
                }
            }
        }

        private void AddColumnToSelected(object sender, RoutedEventArgs e)
        {
            var selectedIndex = AvailableCols.SelectedIndex;
            if (selectedIndex >= 0)
            {
                SelectedCols.Items.Add((String)AvailableCols.SelectedItem);
                AvailableCols.Items.Remove(AvailableCols.SelectedItem);
            }
            checkCanGoForward();

        }

        private void RemoveSelectedColumn(object sender, RoutedEventArgs e)
        {
            var selectedIndex = SelectedCols.SelectedIndex;
            if (selectedIndex >= 0)
            {
                AvailableCols.Items.Add((String)SelectedCols.SelectedItem);
                SelectedCols.Items.Remove(SelectedCols.SelectedItem);
            }
            checkCanGoForward();
        }

        private void FormatImportData(object sender, RoutedEventArgs e)
        {
            var table = (String)Tables.SelectedValue;
            var cols = getSelectedColList();
            var headers = getHeaders(cols);
            var entries = getEntries(table, cols);
            ImportData id = new ImportData(table);
            id.ColumnNames = cols;
            id.Headers = headers;
            id.Entries = entries;
            main.updateData(id);
            main.advance();
        }
        private List<String> getSelectedColList()
        {
            List<String> cols = new List<String>();
            if(SelectAllCols.IsChecked.HasValue && SelectAllCols.IsChecked == true)
            {
                foreach(Object o in AvailableCols.Items)
                {
                    cols.Add((string)o);
                }
            }
            else
            {
                foreach(Object o in SelectedCols.Items)
                {
                    cols.Add((string)o);
                }
            }
            return cols;
        }
        private void checkCanGoForward()
        {
            if(getSelectedColList().Count>0 && Tables.SelectedIndex > -1)
            {
                advance.IsEnabled = true;
            }
            else
            {
                advance.IsEnabled = false;
            }
        }
        private List<String> getHeaders(List<String> cols)
        {
            List<String> headers = new List<String>();
            if(AutoHeaders.IsChecked.HasValue && AutoHeaders.IsChecked.Value == true)
            {
                IDbConnection db = DBProvider.GetDBConnection();
                headers = db.AutogenerateHeaderRow(cols);
            }
            return headers;
        }
        private List<String[]> getEntries(String tableName,List<String> cols)
        {
            IDbConnection con = DBProvider.GetDBConnection();
            return con.getEntries(tableName, cols);
        }

        private void SelectedCols_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            checkReorgButtons();
        }
    }
}
