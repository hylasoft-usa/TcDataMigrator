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
using TCDataUtilities.Filter;
using TCMigration.Filter;

namespace TCMigrator.DBImpot
{
    /// <summary>
    /// Interaction logic for SplitCsv.xaml
    /// </summary>
    public partial class SplitCsv : Page
    {
        List<ColumnFilter> singles;
        List<CompoundFilter> compounds;
        List<ComparisonFilter> comparisons;
        CompoundFilter current;
        SplitWindow w;
        int groupNum = 1;
        
        public SplitCsv(SplitWindow w)
        {
            this.w = w;
            InitializeComponent();
            singles = new List<ColumnFilter>();
            compounds=new List<CompoundFilter>();
            comparisons = new List<ComparisonFilter>();
            initSelectLists();
            checkIfGroupCanBeClosed();
            isSingleFilterSubmittable();
        }
        private void initSelectLists()
        {
            var colnames = w.getCurrentData().ColumnNames;
            var filterTypes = Enum.GetNames(typeof(Enums.FilterType));
            var compoundTypes = Enum.GetNames(typeof(Enums.CompoundFilterType));
            foreach(string c in colnames)
            {
                columnName.Items.Add(c);
                compoundColumnName.Items.Add(c);
                SingleFilterCol2.Items.Add(c);
            }
            foreach(string f in filterTypes)
            {
                compoundFilterType.Items.Add(f);
                FilterType.Items.Add(f);
            }
            foreach(string ct in compoundTypes)
            {
                compoundJoinType.Items.Add(ct);
            }
            singleFilterType.Items.Add("Filter By Comparison");
            singleFilterType.Items.Add("Filter By Column Value");
            singleFilterType.SelectedIndex = 0;
        }
        #region single filter type select
        public void OnSingleFilterTypeChanged(object sender, RoutedEventArgs e)
        {
            if (singleFilterType.SelectedIndex == 0)
            {
                //filter by comparison
                SingleFilterValue.Visibility = Visibility.Hidden;
                SingleFilterTypeLabel.Text = "Column 2";
                SingleFilterCol2.Visibility = Visibility.Visible;
                FilterType.Items.Clear();
                FilterType.Items.Add(Enums.FilterType.AFTER);
                FilterType.Items.Add(Enums.FilterType.BEFORE);
                FilterType.Items.Add(Enums.FilterType.EQUAL_TO);
                FilterType.Items.Add(Enums.FilterType.LESS_THAN);
                FilterType.Items.Add(Enums.FilterType.GREATER_THAN);
                FilterType.Items.Add(Enums.FilterType.LESS_THAN_OR_EQUAL_TO);
                FilterType.Items.Add(Enums.FilterType.GREATER_THAN_OR_EQUAL_TO);
                
            }
            else
            {
                //filter by value
                SingleFilterValue.Visibility = Visibility.Visible;
                SingleFilterTypeLabel.Text = "Value";
                SingleFilterCol2.Visibility = Visibility.Hidden;
                FilterType.Items.Clear();
                var vals = Enum.GetNames(typeof(Enums.FilterType));
                foreach(string s in vals)
                {
                    FilterType.Items.Add(s);
                }
            }
        }
        #endregion
        #region SingleFilter
        private void AddSingleFilter(object sender,RoutedEventArgs e) {
            var col = columnName.Text;
            var type = (FilterType)Enum.Parse(typeof(FilterType), FilterType.Text);
            if (singleFilterType.SelectedIndex == 0)
            {
                //add Comparison Column
                var i1 = columnName.SelectedIndex;
                var i2 = SingleFilterCol2.SelectedIndex;
                var n1 = columnName.Text;
                var n2 = SingleFilterCol2.Text;
                var filtertype = (FilterType)Enum.Parse(typeof(FilterType), FilterType.Text);
                comparisons.Add(new ComparisonFilter(i1, i2, n1, n2, filtertype));
                SingleFilterDisplay.Items.Add(new SingleFilterDisplay(n1, filtertype.ToString(), n2));
            }
            else
            {
                var val = SingleFilterValue.Text;
                singles.Add(new ColumnFilter(columnName.SelectedIndex, col, type, val));
                SingleFilterDisplay.Items.Add(new SingleFilterDisplay(col, type.ToString(), val));
            }

            _resetSingleFilter();
        }

        private void isSingleFilterSubmittable()
        {

            if(columnName.SelectedIndex>-1 && FilterType.SelectedIndex > -1)
            {
                if (singleFilterType.SelectedIndex == 0)
                {
                    if (SingleFilterCol2.SelectedIndex > -1)
                    {
                        btnAddSingle.IsEnabled = true;
                    }
                    else
                    {
                        btnAddSingle.IsEnabled = false;
                    }
                }
                else
                {
                    btnAddSingle.IsEnabled = true;
                }
            }
            else
            {
                btnAddSingle.IsEnabled = false;
            }
        }
        private void _resetSingleFilter()
        {
            columnName.SelectedIndex = -1;
            FilterType.SelectedIndex = -1;
            if (singleFilterType.SelectedIndex == 0)
            {
                SingleFilterCol2.SelectedIndex = -1;
            }
            else
            {
                SingleFilterValue.Text = "";
            }
            isSingleFilterSubmittable();
        }
        private void OnSingleSelectChanged(object sender, RoutedEventArgs e)
        {
            isSingleFilterSubmittable();
        }
        #endregion
        #region compound filter
        private void isCompoundFilterAddable()
        {
            if (current != null)
            {
                if(compoundColumnName.SelectedIndex>-1 && compoundFilterType.SelectedIndex > -1)
                {
                    AddGroupFilter.IsEnabled = true;
                }
                else
                {
                    AddGroupFilter.IsEnabled = false;
                }
            }
            else
            {
                if(compoundJoinType.SelectedIndex>-1 &&compoundColumnName.SelectedIndex>-1 && compoundFilterType.SelectedIndex > -1)
                {
                    AddGroupFilter.IsEnabled = true;
                }
                else
                {
                    AddGroupFilter.IsEnabled = false;
                }
            }
        }
        private void AddToGroup(object sender, RoutedEventArgs e)
        {
            if (current == null)
            {
                var curFilterType = (CompoundFilterType)Enum.Parse(typeof(Enums.CompoundFilterType), compoundJoinType.Text);
                current = new CompoundFilter(curFilterType);
            }
            addFilterToGroup();
            resetCompoundFilter();
            checkIfGroupCanBeClosed();
        }
        private void resetCompoundFilter()
        {
            compoundColumnName.SelectedIndex = -1;
            compoundFilterType.SelectedIndex = -1;
            compoundFilterText.Text = "";
        }
        private void addFilterToGroup() {
            var filter = getCompoundFilter();
            current.addFilter(filter);
            compoundFilters.Items.Add(new CompoundFilterDisplay(groupNum.ToString(), filter.Name, filter.FilterValue, filter.Type.ToString(), current.JoinType.ToString()));
        }
        private void CloseGroup(object sender,RoutedEventArgs e) {
            compounds.Add(current);
            compoundJoinType.IsEnabled = true;
            current = null;
            groupNum++;
            checkIfGroupCanBeClosed();
        }

        private void checkIfGroupCanBeClosed()
        {
            if (current!=null && current.FilterCount > 1)
            {
                btnCloseGroup.IsEnabled = true;
                
            }
            else
            {
                btnCloseGroup.IsEnabled = false;
            }
            compoundJoinType.IsEnabled = (current==null || current.FilterCount < 1);
        }
        private ColumnFilter getCompoundFilter()
        {
            var col = compoundColumnName.Text;
            var index = compoundColumnName.SelectedIndex;
            var type = (FilterType)Enum.Parse(typeof(FilterType), compoundFilterType.Text);
            var val = compoundFilterText.Text;
            return new ColumnFilter(index, col, type, val);
        }

        private void OnCompoundSelectChanged(object sender, RoutedEventArgs e)
        {
            isCompoundFilterAddable();
            checkIfGroupCanBeClosed();
        }
        #endregion

        #region window functions
        private void Close(object sender, RoutedEventArgs e)
        {
            w.Close();
        }
        private void CloseAndApply(object sender, RoutedEventArgs e)
        {
            w.setFilters(singles, compounds,comparisons);
        }
        #endregion
    }
    public class SingleFilterDisplay
    {
        public String toFilter { get; }
        public String type { get; }
        public string filterAgainst { get; }
        public SingleFilterDisplay(string toFilter, string type, string filterAgainst)
        {
            this.toFilter = toFilter;
            this.type = type;
            this.filterAgainst = filterAgainst;
        }
    }
    public class CompoundFilterDisplay
    {
        public string Group { get; }
        public string toFilter { get; }
        public string filterValue { get; }
        public string JoinType { get; }
        public string FilterType { get; }

        public CompoundFilterDisplay(string g, string colToFilter, string filterValue, string fType,string jType)
        {
            this.Group = g;
            this.toFilter = colToFilter;
            this.filterValue = filterValue;
            this.JoinType = jType;
            this.FilterType = fType;
        }
    }
}
