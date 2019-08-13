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
        private readonly AutoResetEvent _errorSignal = new AutoResetEvent(false);
        private bool readersShouldRun = false;
        private string user;
        private string password;
        private string group;
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
            if (convert(ctd.importLocation, csv))
            {
                csv.Import(ctd.outTCXML,user,password,group);
                bool? success = null;
                List<String> output = new List<String>();
                var iterations = 0;
                while (!success.HasValue)
                {
                    var textualData = csv.TCCommandPrompt.Prompt.StandardOutput.ReadLine();
                    if (textualData.Contains("The import operation has completed successfully"))
                    {
                        success = true;
                        csv.TCCommandPrompt.Exit();
                        if (!String.IsNullOrWhiteSpace(textualData))
                        {
                            _context.Post(AppendOutput, textualData.ToString());
                        }
                    }
                    else if (textualData.Contains("Import Error"))
                    {
                        success = false;
                        if (!String.IsNullOrWhiteSpace(textualData))
                        {

                            _context.Post(AppendError, textualData.ToString());
                            
                            var tdata = csv.TCCommandPrompt.Prompt.StandardOutput.ReadLine();
                            while (!String.IsNullOrWhiteSpace(tdata))
                            {
                                _context.Post(AppendError, data.ToString());
                                tdata = csv.TCCommandPrompt.Prompt.StandardOutput.ReadLine();
                            }
                            csv.TCCommandPrompt.Exit();
                        }

                    }
                    else
                    {
                        if (!String.IsNullOrWhiteSpace(textualData))
                        {
                            _context.Post(AppendOutput, textualData.ToString());
                        }
                    }
                    iterations++;
                    if (iterations > 400) { success = false; }
                }
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
            csv.ConvertWithParameters(csvPath, Params);
            bool? success = null;
            List<String> output = new List<String>();
            while (!success.HasValue)
            {
                var data = csv.TCCommandPrompt.Prompt.StandardError.ReadLine();
                if (data.Contains("Converting took"))
                {
                    success = true;
                    if (!String.IsNullOrWhiteSpace(data))
                    {
                        _context.Post(AppendOutput, data.ToString());
                    }
                }
                else if (data.Contains("FATAL"))
                {
                    success = false;
                    csv.TCCommandPrompt.Exit();
                    if (!String.IsNullOrWhiteSpace(data))
                    {
                        _context.Post(AppendError, data.ToString());
                    }
                }
                else
                {
                   if (!String.IsNullOrWhiteSpace(data))
                    {
                        _context.Post(AppendOutput, data.ToString());
                    }
                }
            }
            return success.Value;
        }

        public void AppendOutput(object o)
        {
            Output.Inlines.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + ": " +o + System.Environment.NewLine);
            Viewer.ScrollToBottom();

        }
        public void AppendError(object o)
        {
            Output.Inlines.Add(new Run(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) + ": " + o + System.Environment.NewLine) { Foreground = Brushes.DarkRed, FontWeight = FontWeights.Bold });
            Viewer.ScrollToBottom();
        }
        public class ConvertThreadData
        {
            public String importLocation { get; set; }
            public String outTCXML { get; set; }
            public String logLocation { get; set; }
        }

    }
}

