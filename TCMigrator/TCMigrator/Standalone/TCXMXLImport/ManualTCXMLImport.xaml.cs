using System;
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
using TCMigrator.Teamcenter;
using TCMigrator.VisualUtilities;

namespace TCMigrator.Standalone.TCXMXLImport
{
    /// <summary>
    /// Interaction logic for ManualTCXMLImport.xaml
    /// </summary>
    public partial class ManualTCXMLImport : Page
    {
        private string user;
        private string password;
        private string group;
        Thread t1;
        string xmlLocation;
        private SynchronizationContext _context = SynchronizationContext.Current;
        public ManualTCXMLImport()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var TcXmlLocation = XmlLocation.Text;
            xmlLocation = TcXmlLocation;
            var dir = getDirectory(TcXmlLocation);
            user = TcUser.Text;
            password = TcPass.Password;
            group = TcGroup.Text;
            startThreads(dir);
        }
        private void startThreads(string dir)
        {
            t1 = new Thread(import);
            t1.Start(dir);
        }
        private string getDirectory(string filePath)
        {
            var parts = filePath.Split('\\');
            var dir = "";
            for(var x = 0; x < parts.Length - 1; x++)
            {
                dir += parts[x] + @"\";
            }
            return dir;
        }
        private void import(object dir)
        {
<<<<<<< Updated upstream
            var csv = new Converter();
            csv.Import(xmlLocation,dir.ToString(),user,password,group);
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
                        _context.Post(setComplete, null);
                    }
                }
                else if (textualData.Contains("Import Error"))
                {
                    success = false;
                    if (!String.IsNullOrWhiteSpace(textualData))
                    {
                        
                        _context.Post(AppendError, textualData.ToString());
                        _context.Post(setError, null);
                        var data = csv.TCCommandPrompt.Prompt.StandardOutput.ReadLine();
                        while (!String.IsNullOrWhiteSpace(data)){
                            _context.Post(AppendError, data.ToString());
                            data = csv.TCCommandPrompt.Prompt.StandardOutput.ReadLine();
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

=======
            var csv = new Converter(callback);
            if (csv.Import(xmlLocation, dir.ToString(), user, password, group)) { _context.Post(setComplete,new object()); }
            else { _context.Post(setError, new object()); }
>>>>>>> Stashed changes
        }
        public void setComplete(object o)
        {
            btn.Content = "Home";
            btn.Click -= Button_Click;
            btn.Click += GoHome;
            btn.Style = FindResource("EngBlueBtn") as Style;
        }
        public void setError(object o)
        {
            btn.Content = "Retry";
            btn.Style = FindResource("EngRedBtn") as Style;
        }
<<<<<<< Updated upstream
        public void GoHome(object sender, RoutedEventArgs e) { }
        public void AppendOutput(object o)
=======
        public void GoHome(object sender, RoutedEventArgs e) {
            mw.NavigateHome();
        }
        private void callback(UIMessage m)
>>>>>>> Stashed changes
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
    }
}
