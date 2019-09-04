using System;
using System.Collections.Generic;
using System.Configuration;
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
using TCMigrator.VisualUtilities;

namespace TCMigrator.Settings
{
    /// <summary>
    /// Interaction logic for CSVPagedSettings.xaml
    /// </summary>
    public partial class CSVPagedSettings : Page
    {
        List<SettingsListItem> props;
        PageableGrid<SettingsListItem> pgd;
        ApplicationSettingsBase ba;
        public CSVPagedSettings(ApplicationSettingsBase ba,String label)
        {
            this.ba = ba;
            props = new List<SettingsListItem>();
            InitializeComponent();
            foreach(SettingsProperty sp in ba.Properties)
            {
                props.Add(new SettingsListItem(sp.Name, sp.DefaultValue.ToString()));
            }
            pgd = new PageableGrid<SettingsListItem>(props, 8);
            Disp.ItemsSource = pgd.CurrentPageRecords;
        }
        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            var props = pgd.AllRecords;
            foreach(SettingsListItem sli in props)
            {
               foreach(SettingsProperty sp in ba.Properties)
                {
                    if (sli.Name == sp.Name)
                    {
                        sp.DefaultValue= Convert.ChangeType(sli.Value, ba[sp.Name].GetType());
                    }
                }
            }
            ba.Save();
        }
    }
}
