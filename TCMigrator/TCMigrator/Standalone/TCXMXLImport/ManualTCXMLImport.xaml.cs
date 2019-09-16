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
        MainWindow mw;
        public ManualTCXMLImport(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
            checkIsSubmittable();
            loadDefaults();
        }
        public void loadDefaults()
        {
            this.TcGroup.Text = Properties.TeamcenterSettings.Default.TC_GROUP;
            this.TcPass.Password = Properties.TeamcenterSettings.Default.TC_Password;
            this.TcUser.Text = Properties.TeamcenterSettings.Default.TC_USER;
        }

        private void DoImport(object sender, RoutedEventArgs e)
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
            var csv = new Converter(callback);
            if (csv.ImportAll(xmlLocation, dir.ToString(), user, password, group)) { _context.Post(setComplete,new object()); }
            else { _context.Post(setError, new object()); }
        }
        public void setComplete(object o)
        {
            btn.Content = "Home";
            btn.Click -= DoImport;
            btn.Click += GoHome;
            btn.Style = FindResource("EngBlueBtn") as Style;
        }
        public void setError(object o)
        {
            btn.Content = "Retry";
            btn.Style = FindResource("EngRedBtn") as Style;
        }
        public void GoHome(object sender, RoutedEventArgs e) {
            mw.NavigateHome();
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
            if(!String.IsNullOrWhiteSpace(XmlLocation.Text) &&!String.IsNullOrEmpty(TcUser.Text) && !String.IsNullOrWhiteSpace(TcPass.Password) && !String.IsNullOrEmpty(TcGroup.Text))
            {
                btn.IsEnabled = true;
            }
            else
            {
                btn.IsEnabled = false;
            }
        }
    }
}
