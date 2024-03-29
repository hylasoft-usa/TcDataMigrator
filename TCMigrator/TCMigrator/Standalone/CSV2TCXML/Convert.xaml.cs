﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using TCMigrator.Teamcenter;
using TCMigrator.VisualUtilities;
using static TCMigrator.DBImpot.ConvertAndImport;

namespace TCMigrator.Standalone.CSV2TCXML
{
    /// <summary>
    /// Interaction logic for Convert.xaml
    /// </summary>
    public partial class Convert : Page
    {
        IPageMediator main;
        private SynchronizationContext _context = SynchronizationContext.Current;
        private ConcurrentQueue<String> outputData = new ConcurrentQueue<string>();
        private ConcurrentQueue<String> errorData = new ConcurrentQueue<string>();
        private readonly AutoResetEvent _signal = new AutoResetEvent(false);
        private readonly AutoResetEvent _errorSignal = new AutoResetEvent(false);
        private bool readersShouldRun = false;
        public Convert(IPageMediator main)
        {
            InitializeComponent();
            this.main = main;
            loadDefaults();
        }
        private void loadDefaults()
        {
            User.Text = Properties.TeamcenterSettings.Default.TC_USER;
            Group.Text = Properties.TeamcenterSettings.Default.TC_GROUP;
            checkIsSubmittable();
        }

        private void Import(object sender, RoutedEventArgs e)
        {
            var importLocation = main.getCurrentData().csvLocation;
            var OutputTCXMLLocation = getOutputLocation(importLocation);
            var conversionLogFileLocation = importLocation + ".log";
            ConvertThreadData ctd = new ConvertThreadData() { importLocation = importLocation, outTCXML = OutputTCXMLLocation, logLocation = conversionLogFileLocation };
            startThreads(ctd);
            Btn.Content = "Home";
            Btn.Style = FindResource("EngBlueBtn") as Style;
            Btn.Click -= Import;
            Btn.Click += GoHome;
        }
        public void startThreads(ConvertThreadData ctd)
        {
            Thread t1 = new Thread(performCmdCalls);

            t1.Start(ctd);
        }
        public void performCmdCalls(object data)
        {
            ConvertThreadData ctd = (ConvertThreadData)data;
            Converter csv = new Converter(callback);
            if (convert(ctd.importLocation, csv))
            {
                csv.archive(ctd.outTCXML);
            }
        }
        private bool convert(String csvPath, Converter csv)
        {
            var convertOptions = main.getCurrentImportOptions();
            List<String> Params = new List<String>();
            Params.Add(String.Format(Properties.ConvertConfig.source_site, convertOptions.SourceSite));
            Params.Add(String.Format(Properties.ConvertConfig.save_gsid, convertOptions.saveGsidOut ? 1 : 0));
            Params.Add(String.Format(Properties.ConvertConfig.lov_validate, convertOptions.lovValidate ? 1 : 0));

            Params.Add(String.Format(Properties.ConvertConfig.island_batch_size, convertOptions.islandSize));

            Params.Add(String.Format(Properties.ConvertConfig.gms_sep, convertOptions.gmsTcxmlStringSeperator));
            Params.Add(String.Format(Properties.ConvertConfig.file_encoding, convertOptions.Encoding));
            Params.Add(String.Format(Properties.ConvertConfig.csv_seperator, convertOptions.csvSeperator));
            Params.Add(String.Format(Properties.ConvertConfig.csv_quote, convertOptions.quotationMarkIdentifier));
            Params.Add(String.Format(Properties.ConvertConfig.csv_escape, convertOptions.escapeChar));
            Params.Add(String.Format(Properties.ConvertConfig.bv_type, convertOptions.bv_type));
            Params.Add(String.Format(Properties.ConvertConfig.bvr_type, convertOptions.bvr_type));
            Params.Add(String.Format(Properties.ConvertConfig.bvr_percise, convertOptions.useBvrPercise ? 1 : 0));
            if (convertOptions.useLocalTime)
            {
                Params.Add(String.Format(Properties.ConvertConfig.local_time, convertOptions.localTimeOffsetHours));
            }
            if (convertOptions.groupDataItems)
            {
                Params.Add(String.Format(Properties.ConvertConfig.group_items, convertOptions.groupDataItemsType));
            }
            var xmlPath = "";
            return csv.ConvertWithParameters(csvPath, Params,out xmlPath,true);
        }

        private void GoHome(Object sender, RoutedEventArgs e)
        {

        }
        private string getOutputLocation(string importLocation)
        {
            var parts = importLocation.Split('\\');
            string output = "";
            for(var x = 0; x < parts.Length; x++)
            {
                output += parts[x] + @"\";
            }
            return output;
        }
        private void callback(UIMessage m)
        {
            switch (m.MessageType)
            {
                case UIMessageType.SUCCESS:
                    _context.Post(AppendSuccess, m);
                    break;
                case UIMessageType.ERROR:
                    _context.Post(AppendError, m);
                    break;
                case UIMessageType.DATA:
                    _context.Post(AppendData, m);
                    break;
            }
        }
        private void AppendError(object o)
        {
            Output.Inlines.Add(new Run(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + ": " + o + System.Environment.NewLine) { Foreground = Brushes.DarkRed, FontWeight = FontWeights.Bold });
            Output.Inlines.Add(Environment.NewLine);
            Viewer.ScrollToBottom();
        }
        private void AppendSuccess(object o)
        {
            Output.Inlines.Add(new Run(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + ": " + o + System.Environment.NewLine) { Foreground = Brushes.DarkGreen, FontWeight = FontWeights.Bold });
            Output.Inlines.Add(Environment.NewLine);
            Viewer.ScrollToBottom();
        }
        private void AppendData(object o)
        {
            if (!String.IsNullOrWhiteSpace(o.ToString()))
            {
                Output.Inlines.Add(o.ToString());
                Output.Inlines.Add(Environment.NewLine);
                Viewer.ScrollToBottom();
            }
        }

        private void onTextChanged(object sender, TextChangedEventArgs e)
        {
            checkIsSubmittable();
        }

        private void onPasswordChanged(object sender, RoutedEventArgs e)
        {
            checkIsSubmittable();
        }
        private void checkIsSubmittable()
        {
            if(!String.IsNullOrWhiteSpace(User.Text) && !String.IsNullOrWhiteSpace(Group.Text) && !String.IsNullOrWhiteSpace(Password.Password))
            {
                Btn.IsEnabled = true;
            }
            else
            {
                Btn.IsEnabled = false;
            }
        }
    }
}
