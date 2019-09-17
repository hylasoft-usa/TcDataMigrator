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
using TCMigrator.Enums;
using TCMigrator.Interfaces;
using TCMigrator.Teamcenter;
using IM = TCMigrator.Properties.ImportSettings;

namespace TCMigrator.Standalone.DB2CSV
{
    /// <summary>
    /// Interaction logic for Csv2Tcxml.xaml
    /// </summary>
    public partial class Csv2Tcxml : Page
    {
        IPageMediator main;
        public Csv2Tcxml(IPageMediator main)
        {
            this.main = main;
            InitializeComponent();
            LoadDefaults();
        }
        private void LoadDefaults()
        {
            SourceSite.Text = IM.Default.SOURCE_SITE;
            BomViewRevisionType.Text = IM.Default.BOMVEIW_REVISION_TYPE;
            BomViewType.Text = IM.Default.BOMVIEW_TYPE;
            CsvSep.Text = IM.Default.CSV_SEPARATOR.ToString();
            FileEncoding.Text = IM.Default.ENCODING;
            CsvEscape.Text = IM.Default.CSV_ESCAPE.ToString();
            TCXMLSep.Text = IM.Default.GMS_TCXML_STRING_SEPERATOR.ToString();
            GroupBy.IsChecked = IM.Default.DEFAULT_GROUP_ITEMS;
            GroupByType.IsEnabled = GroupBy.IsChecked.Value;
            GroupByType.Text = IM.Default.DEFAULT_GROUP_ITEMS_TYPE;
            IslandSize.Text = IM.Default.DEFAULT_ISLAND_SIZE.ToString();
            LocalTimeOffset.Text = IM.Default.LOCAL_TIMEZONE_OFFSET_HOURS.ToString();
            UseLocal.IsChecked = IM.Default.DEFAULT_USE_LOCAL_TIME;
            LocalTimeOffset.IsEnabled = UseLocal.IsChecked.Value;
            ValidateLovs.IsChecked = IM.Default.LOV_VALIDATE;
            CsvQuote.Text = IM.Default.CSV_QUOTATION.ToString(); ;
            CsvEscape.Text = IM.Default.CSV_ESCAPE.ToString();
            SaveGSID.IsChecked = IM.Default.SAVE_GSID_OUT;
            SkipExists.IsChecked = IM.Default.SKIP_EXISTING;
            SkipExistingType.IsEnabled = SkipExists.IsChecked.Value;
            SkipExistingType.Text = IM.Default.DEFAULT_SKIP_EXISTING_TYPE;
            UseBVRPercise.IsChecked = IM.Default.BVR_PERCISE;
            //CsvPath.Text = Properties.CSVSettings.Default.CSVDirectory + main.getCurrentData().InputTitle + @"\" + Properties.CSVSettings.Default.DefaultCSVName;
            var encodingTypes = Enum.GetValues(typeof(EncodingType));
            foreach (EncodingType et in Enum.GetValues(typeof(EncodingType)))
            {
                FileEncoding.Items.Add(et.ToString());
                if (et.ToString() == IM.Default.ENCODING)
                {
                    FileEncoding.SelectedItem = et.ToString();
                }
            }
            checkSubmittable();

        }

        private void SaveImportOptions(object sender, RoutedEventArgs e)
        {
            CSVConverterOptions co = new CSVConverterOptions();
            co.SourceSite = SourceSite.Text;
            co.bvr_type = BomViewRevisionType.Text;
            co.bv_type = BomViewType.Text;
            co.csvSeperator = CsvSep.Text[0];
            co.Encoding = FileEncoding.Text;
            if (co.Encoding.Contains('_'))
            {
                co.Encoding = co.Encoding.Replace('_', '-');
            }
            co.escapeChar = CsvEscape.Text[0];
            co.gmsTcxmlStringSeperator = TCXMLSep.Text[0];
            co.groupDataItems = GroupBy.IsChecked.HasValue ? GroupBy.IsChecked.Value : false;
            co.groupDataItemsType = GroupByType.Text;
            co.islandSize = Int32.Parse(IslandSize.Text);
            co.localTimeOffsetHours = Int32.Parse(LocalTimeOffset.Text);
            co.lovValidate = ValidateLovs.IsChecked.HasValue ? ValidateLovs.IsChecked.Value : false;
            co.useLocalTime = UseLocal.IsChecked.HasValue ? UseLocal.IsChecked.Value : false;
            co.quotationMarkIdentifier = CsvQuote.Text[0];
            co.saveGsidOut = SaveGSID.IsChecked.HasValue ? SaveGSID.IsChecked.Value : false;
            co.skipExisting = SkipExists.IsChecked.HasValue ? SkipExists.IsChecked.Value : false;
            co.skipExistingType = SkipExistingType.Text;
            co.useBvrPercise = UseBVRPercise.IsChecked.HasValue ? UseBVRPercise.IsChecked.Value : false;
            main.updateImportOptions(co);
            var data = main.getCurrentData();
            data.csvLocation = CsvPath.Text;
            main.updateData(data);
            main.advance();
        }
        private void GoBack(object sender, RoutedEventArgs e) { }
        private void OpenFileBrowser(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = Properties.CSVSettings.Default.CSVDirectory;
            dlg.Filter = "CSV Files (*.csv)|*.csv";
            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                CsvPath.Text = dlg.FileName;
            }
        }
        private void textChanged(object sender, TextChangedEventArgs e)
        {
            checkSubmittable();
        }
        private void checkSubmittable()
        {
            if (!string.IsNullOrWhiteSpace(CsvPath.Text))
            {
                advance.IsEnabled = true;
            }
            else
            {
                advance.IsEnabled = false;
            }
        }
    }
}
