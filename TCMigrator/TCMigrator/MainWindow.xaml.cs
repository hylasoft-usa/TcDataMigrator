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
using TCMigrator.Mediators;
using TCMigrator.Settings;
using TCMigrator.Standalone.TCXMXLImport;

namespace TCMigrator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new Home(this);
        }
        private void ShowLoggingSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new LoggerSettings(this);
        }
        private void ShowTCSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new TCSettings(this);
        }

        private void NavigateSetup(object sender, RoutedEventArgs e)
        {
            //Main.Content = new InstallCSV2TCXML();
        }

        private void ShowDBSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new DBSettings(this);
        }
        private void ShowCSVSettings(object sender,RoutedEventArgs e)
        {
            Main.Content = new CSVSettings(this);
        }
        private void ShowImportSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new ImportSettings(this);
        }
        public void NavigateHome()
        {
            Main.Content = new Home(this);
        }
        public void Db2Csv(object sender, RoutedEventArgs e)
        {
            Main.Content = new Db2CsvMediator(this);
        }
        public void TCXML(object sender, RoutedEventArgs e)
        {
            Main.Content = new CSV2TCXMLMediator(this);
        }
        public void Import(object sender, RoutedEventArgs e)
        {
            Main.Content = new ManualTCXMLImport(this);
        }

        private void DBImport(object sender, RoutedEventArgs e)
        {
            Main.Content = new DBMediator(this);
        }
        public void Navigate(Page p)
        {
            Main.Content = p;
        }
        public void GoHome(object sender, RoutedEventArgs e)
        {
            NavigateHome();
        }
        public void ShowTest(object sender, RoutedEventArgs e)
        {
            Main.Content = new SettingsDisplay(Properties.ImportSettings.Default, "Import Settigns");
        }
        public void ShowSettings(object sender, RoutedEventArgs e)
        {
            Main.Content = new TabbedSettings();
        }
    }
}
