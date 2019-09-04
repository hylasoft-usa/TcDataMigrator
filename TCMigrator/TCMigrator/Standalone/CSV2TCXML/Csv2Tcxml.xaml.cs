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
            var options = main.getCurrentImportOptions();
            SourceSite.Text = options.SourceSite;
            BomViewRevisionType.Text = options.bvr_type;
            BomViewType.Text = options.bv_type;
            CsvSep.Text = options.csvSeperator.ToString();
            FileEncoding.Text = options.Encoding;
            CsvEscape.Text = options.escapeChar.ToString();
            TCXMLSep.Text = options.gmsTcxmlStringSeperator.ToString();
            GroupBy.IsChecked = options.groupDataItems;
            GroupByType.IsEnabled = GroupBy.IsChecked.Value;
            GroupByType.Text = options.groupDataItemsType;
            IslandSize.Text = options.islandSize.ToString();
            LocalTimeOffset.Text = options.localTimeOffsetHours.ToString();
            UseLocal.IsChecked = options.useLocalTime;
            LocalTimeOffset.IsEnabled = UseLocal.IsChecked.Value;
            ValidateLovs.IsChecked = options.lovValidate;
            CsvQuote.Text = options.quotationMarkIdentifier.ToString();
            SaveGSID.IsChecked = options.saveGsidOut;
            SkipExists.IsChecked = options.skipExisting;
            SkipExistingType.IsEnabled = SkipExists.IsChecked.Value;
            SkipExistingType.Text = options.skipExistingType;
            UseBVRPercise.IsChecked = options.useBvrPercise;
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
        private void EnableExisting(object sender, RoutedEventArgs e)
        {
            SkipExistingType.IsEnabled = true;
        }
        private void DisableExisting(object sender, RoutedEventArgs e)
        {
            SkipExistingType.IsEnabled = false;
        }
        private void EnableGroupBy(object sender, RoutedEventArgs e)
        {
            GroupByType.IsEnabled = true;
        }
        private void DisableGroupBy(object sender, RoutedEventArgs e)
        {
            GroupByType.IsEnabled = false;
        }
        private void EnableLocalTime(object sender, RoutedEventArgs e)
        {
            LocalTimeOffset.IsEnabled = true;
        }
        private void DisableLocalTime(object sender, RoutedEventArgs e)
        {
            LocalTimeOffset.IsEnabled = false;
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
