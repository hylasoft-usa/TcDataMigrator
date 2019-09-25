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
using TCDataUtilities.DataModel;
using TCMigrator.DBImpot;
using TCMigrator.Interfaces;
using TCMigrator.RelationsImport;
using TCMigrator.Teamcenter;
using TCMigrator.Transform;
using DataSelect = TCMigrator.RelationsImport.DataSelect;

namespace TCMigrator.Mediators
{
    /// <summary>
    /// Interaction logic for RelationsMediator.xaml
    /// </summary>
    public partial class RelationsMediator : IPageMediator
    {
        private List<Page> pages;
        private ImportData data;
        private CSVConverterOptions options;
        private int step;
        MainWindow mw;
        private TransformOptions to;
        public RelationsMediator(MainWindow mw)
        {
            InitializeComponent();
            pages = new List<Page>();
            pages.Add(new DataSelect(this));
            step = 0;
            ContentWindow.Content = new DataSelect(this);
            this.mw = mw;
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
            return this.options;
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
            this.options = o;
        }
        private void LazyLoadClass()
        {
            switch (step)
            {
                case 1:
                    ContentWindow.Content = new DBImpot.Transform(this);
                    break;
                case 2:
                    ContentWindow.Content = new DBImpot.Import(this);
                    break;
                case 3:
                    ContentWindow.Content = new ConvertAndImport(this);
                    break;
            }
        }
        public void setTransformOptions(TransformOptions to)
        {
            this.to = to;
        }
        public TransformOptions getTransformOptions()
        {
            if (this.to == null) { this.to = new TransformOptions(); }
            return this.to;
        }
    }
}

