using System;
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

namespace TCMigrator.DBImpot
{
    /// <summary>
    /// Interaction logic for ConvertAndImport.xaml
    /// </summary>
    public partial class ConvertAndImport : Page
    {
        IPageMediator main;
        private SynchronizationContext _context = SynchronizationContext.Current;
        private ConcurrentQueue<String> outputData = new ConcurrentQueue<string>();
        private ConcurrentQueue<String> errorData = new ConcurrentQueue<string>();
        private readonly AutoResetEvent _signal = new AutoResetEvent(false);
        private string user;
        private string password;
        private string group;
        private bool shouldQuit = false;
        private bool? operationSuccess;
        private List<Run> outputRuns = new List<Run>();
        private bool? csvCompleteSuccess;
        private bool? importCompleteSuccess;
        public ConvertAndImport(IPageMediator main)
        {
            InitializeComponent();
            this.main = main;
            loadDefaults();
        }
        private void loadDefaults()
        {
            User.Text = Properties.TeamcenterSettings.Default.TC_USER;
            Group.Text = Properties.TeamcenterSettings.Default.TC_GROUP;
        }

        private void Import(object sender, RoutedEventArgs e)
        {
            user = User.Text;
            password = Password.Password;
            group = Group.Text;
            var importLocation = Properties.CSVSettings.Default.CSVDirectory + main.getCurrentData().InputTitle + @"\" + Properties.CSVSettings.Default.DefaultCSVName;
            var OutputTCXMLLocation = Properties.CSVSettings.Default.CSVDirectory + main.getCurrentData().InputTitle + @"\";
            var conversionLogFileLocation = importLocation + ".log";
            ConvertThreadData ctd = new ConvertThreadData() { importLocation = importLocation, outTCXML = OutputTCXMLLocation, logLocation = conversionLogFileLocation };
            startThreads(ctd);
        }
        public void startThreads(ConvertThreadData ctd)
        {
            Thread t1 = new Thread(performCmdCalls);

            t1.Start(ctd);
        }
        public void performCmdCalls(object data)
        {
            ConvertThreadData ctd = (ConvertThreadData)data;
            Converter csv = new Converter();
            csv.TCCommandPrompt.Prompt.OutputDataReceived += (s, e) => handleData(e.Data);
            csv.TCCommandPrompt.Prompt.ErrorDataReceived += (s, e) => handleData(e.Data);
            csv.TCCommandPrompt.Prompt.BeginOutputReadLine();
            csv.TCCommandPrompt.Prompt.BeginErrorReadLine();
            convert(ctd, csv);
            
                //await completion
            while (!csvCompleteSuccess.HasValue)
            {
                Thread.Sleep(50);
            }
            if (csvCompleteSuccess.HasValue && csvCompleteSuccess.Value)
            {
                csv.Import(ctd.outTCXML, user, password, group);
            }
            else
            {
                csv.archive(ctd.outTCXML);
            }
            //await import complete
            while (!importCompleteSuccess.HasValue)
            {
                Thread.Sleep(50);
            }
            if (importCompleteSuccess.Value)
            {
                csv.archive(ctd.outTCXML);
               
            }
            else
            {
                csv.archive(ctd.outTCXML);
            }
            
        }
        private void handleData(object d)
        {
            checkCompletionStatus(d);
            var data = d.ToString();
            if(data.Contains(Properties.CommandLineText.CSV_SUCCESS) || data.Contains(Properties.CommandLineText.TCXML_IMPORT_SUCCESS))
            {
                _context.Post(AppendSuccess, d);
            }
            else if(data.Contains(Properties.CommandLineText.CSV_FAILURE) || data.Contains(Properties.CommandLineText.TCXML_IMPORT_FAILURE))
            {
                shouldQuit = true;
                operationSuccess = false;
                _context.Post(AppendError, d);
                
            }
            else
            {
                _context.Post(AppendData, d);
            }
            checkDataStreams();
        }
        private void checkCompletionStatus(object d)
        {
            var data = (string)d;
            if (data.Contains(Properties.CommandLineText.CSV_SUCCESS))
            {
                csvCompleteSuccess = true;
            }
            if (data.Contains(Properties.CommandLineText.CSV_FAILURE))
            {
                csvCompleteSuccess = false;
            }
            if (data.Contains(Properties.CommandLineText.TCXML_IMPORT_FAILURE))
            {
                importCompleteSuccess = false;
            }
            if (data.Contains(Properties.CommandLineText.TCXML_IMPORT_SUCCESS))
            {
                importCompleteSuccess = true;
            }
        }
        private void convert(ConvertThreadData ctd, Converter csv)
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
            csv.ConvertWithParameters(ctd.importLocation, Params);
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
            if (!String.IsNullOrWhiteSpace(o.ToString())){
                Output.Inlines.Add(o.ToString());
                Output.Inlines.Add(Environment.NewLine);
                Viewer.ScrollToBottom();
            }
        }

        private void checkDataStreams()
        {
            if (outputRuns.Count > 0)
            {
                foreach(Run r in outputRuns)
                {
                    Output.Inlines.Add(r);
                    Viewer.ScrollToBottom();
                    outputRuns.Remove(r);
                }
            }
        }
        public class ConvertThreadData
        {
            public String importLocation { get; set; }
            public String outTCXML { get; set; }
            public String logLocation { get; set; }
        }

    }
}

