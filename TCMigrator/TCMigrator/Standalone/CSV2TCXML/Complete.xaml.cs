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

namespace TCMigrator.Standalone.CSV2TCXML
{
    /// <summary>
    /// Interaction logic for Complete.xaml
    /// </summary>
    public partial class Complete : Page
    {
        IPageMediator main;
        public Complete(IPageMediator main)
        {
            InitializeComponent();
            this.main = main;
            csvLocation.Text = String.Format("CSV can be found at"+Environment.NewLine+" {0}", Properties.CSVSettings.Default.CSVDirectory + main.getCurrentData().InputTitle + @"\" + Properties.CSVSettings.Default.DefaultCSVName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Home();
        }
    }
}
