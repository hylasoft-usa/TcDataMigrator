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
using TCMigrator.Standalone.TCXMXLImport;

namespace TCMigrator
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        MainWindow mw;
        public Home(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void StartMigrator(object sender, RoutedEventArgs e)
        {
            mw.Navigate(new DBMediator(mw));
        }

        private void DownloadCSV(object sender, RoutedEventArgs e)
        {
            mw.Navigate(new Db2CsvMediator(mw));
        }

        private void ToTCXML(object sender, RoutedEventArgs e)
        {
            mw.Navigate(new CSV2TCXMLMediator(mw));
        }

        private void ImportTcxml(object sender, RoutedEventArgs e)
        {
            mw.Navigate(new ManualTCXMLImport(mw));
        }

        private void ConfigSettings(object sender, RoutedEventArgs e)
        {
            mw.Navigate(new SettingsSelect(mw));
        }
    }
}
