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
using TCMigrator.Data;
using TCMigrator.Interfaces;
using TCMigrator.Standalone.DB2CSV;
using TCMigrator.Teamcenter;

namespace TCMigrator.Mediators
{
    /// <summary>
    /// Interaction logic for CSV2TCXMLMediator.xaml
    /// </summary>
    public partial class CSV2TCXMLMediator : Page,IPageMediator
    {
        private List<Page> pages;
        private ImportData data;
        private int step;
        private CSVConverterOptions o;
        public CSV2TCXMLMediator()
        {
            InitializeComponent();
            data = new ImportData(String.Format("ManualImport_{0}", DateTime.Now));
            step = 1;
            ContentWindow.Content = new Csv2Tcxml(this);
        }

        public void advance()
        {
            step++;
            LazyLoadClass();
        }

        public ImportData getCurrentData()
        {
            return this.data;
        }

        public CSVConverterOptions getCurrentImportOptions()
        {
            return this.o;
        }

        public void retreat()
        {
            step--;
            LazyLoadClass();
        }

        public void updateData(ImportData data)
        {
            this.data = data;
        }

        public void updateImportOptions(CSVConverterOptions o)
        {
            this.o = o;
        }
        private void LazyLoadClass()
        {
            switch (step)
            {
                case 1:
                    ContentWindow.Content = new Csv2Tcxml(this);
                    break;
                case 2:
                    ContentWindow.Content = new Standalone.CSV2TCXML.Convert(this);
                    break;
            }
        }
    }
}
