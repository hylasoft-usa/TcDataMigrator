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

namespace TCMigrator.Settings
{
    /// <summary>
    /// Interaction logic for TabbedSettings.xaml
    /// </summary>
    public partial class TabbedSettings : Page
    {
        public TabbedSettings()
        {
            InitializeComponent();
            DBSettings.Content = new SettingsDisplay(Properties.Database.Default, "Database Settings");
            ImportSettings.Content = new SettingsDisplay(Properties.ImportSettings.Default, "ImportSettings");
            CSVSettings.Content = new SettingsDisplay(Properties.CSVSettings.Default, "CSV Settings");
            TCSettings.Content = new SettingsDisplay(Properties.TeamcenterSettings.Default, "Teamcenter Settings");
            LogSettings.Content = new SettingsDisplay(Properties.LogSettings.Default, "Logger Settings");
        }
    }
}
