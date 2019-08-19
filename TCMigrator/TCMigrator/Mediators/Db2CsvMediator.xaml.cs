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
using TCMigrator.DBImpot;
using TCMigrator.Interfaces;
using TCMigrator.Standalone.CSV2TCXML;
using TCMigrator.Teamcenter;

namespace TCMigrator.Mediators
{
    /// <summary>
    /// Interaction logic for Db2CsvMediator.xaml
    /// </summary>
    public partial class Db2CsvMediator : Page,IPageMediator
    {
        private List<Page> pages;
        private ImportData data;
        private int step;
        private MainWindow mw;
        public Db2CsvMediator(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
            pages = new List<Page>();
            pages.Add(new DataSelect(this));
            step = 0;
            ContentWindow.Content = new DataSelect(this);
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        private void LazyLoadClass()
        {
            if (step == 1 && data.AreHeadersSet) { step++; }
            switch (step)
            {
                case 1:
                    ContentWindow.Content = new ManualHeaders(this);
                    break;
                case 2:
                    ContentWindow.Content = new DBImpot.Transform(this);
                    break;
                case 3:
                    ContentWindow.Content = new Complete(this);
                    break;
            }
        }
    }
}
