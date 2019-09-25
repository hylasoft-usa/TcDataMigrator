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
using System.Windows.Shapes;
using TCDataUtilities.DataModel;
using TCDataUtilities.Filter;
using TCMigration.Filter;
using TCMigrator.DBImpot;
using TCMigrator.Interfaces;

namespace TCMigrator
{
    /// <summary>
    /// Interaction logic for SplitWindow.xaml
    /// </summary>
    public partial class SplitWindow : Window
    {
        public IPageMediator ipm;
        public SplitWindow(IPageMediator med)
        {
            this.ipm = med;
            InitializeComponent();
            SplitFrame.Content = new SplitCsv(this);
        }
        public void setFilters(List<ColumnFilter> singles, List<CompoundFilter> compounds,List<ComparisonFilter> comparisons) {
            var to=ipm.getTransformOptions();
            to.ColumnFilters = singles;
            to.CompoundFilters = compounds;
            to.ComparisonFilters = comparisons;
            ipm.setTransformOptions(to);
            this.Close();
        }
        public void close()
        {
            this.Close();
        }
        public ImportData getCurrentData()
        {
            return ipm.getCurrentData();
        }
    }
}
