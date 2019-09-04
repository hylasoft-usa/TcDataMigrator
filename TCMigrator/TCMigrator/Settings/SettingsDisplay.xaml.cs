using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TCMigrator.VisualUtilities;

namespace TCMigrator.Settings
{
    /// <summary>
    /// Interaction logic for SettingsDisplay.xaml
    /// </summary>
    public partial class SettingsDisplay : Page
    {
        List<SettingsListItem> props;
        PageableGrid<SettingsListItem> pgd;
        ApplicationSettingsBase ba;
        private int recordsPerPage = 8;
        private SynchronizationContext _context = SynchronizationContext.Current;
        System.Timers.Timer timer;
        public SettingsDisplay(ApplicationSettingsBase ba, String label)
        {
            this.ba = ba;
            props = new List<SettingsListItem>();
            InitializeComponent();
            List<SettingsProperty> properties = new List<SettingsProperty>();
            foreach(SettingsProperty s in ba.Properties)
            {
                properties.Add(s);
            }

            foreach (SettingsProperty sp in properties.OrderBy(x => x.Name)) { 
                props.Add(new SettingsListItem(sp.Name, sp.DefaultValue.ToString()));
            }
            pgd = new PageableGrid<SettingsListItem>(props, 8);
            Disp.ItemsSource = pgd.CurrentPageRecords;
            SettingType.Content = label;
            PageNo.Content = "1";
            SetButtonStatus();
        }
        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            var props = pgd.AllRecords;
            foreach (SettingsListItem sli in props)
            {
                foreach (SettingsProperty sp in ba.Properties)
                {
                    if (sli.Name == sp.Name)
                    {
                        sp.DefaultValue = Convert.ChangeType(sli.Value, ba[sp.Name].GetType());
                    }
                }
            }
            ba.Save();
            SaveSuccess();
        }
        private void SaveSuccess()
        {
            Overlay.Visibility = Visibility.Visible;
            Saved.Visibility = Visibility.Visible;
            timer = new System.Timers.Timer(2000);
            timer.Elapsed += CloseSuccess;
            timer.AutoReset = false;
            timer.Enabled = true;
        }
        public void CloseSuccess(Object source,ElapsedEventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            _context.Post(CloseOverlay, new Object());
        }
        public void CloseOverlay(Object status)
        {
            Overlay.Visibility = Visibility.Hidden;
            Saved.Visibility = Visibility.Hidden;
        }
        private void SetPageCount()
        {
            if (pgd.PageCount > 1)
            {
                PageNo.Visibility = Visibility.Visible;
                BackArrow.Visibility = Visibility.Visible;
                ForwardArrow.Visibility = Visibility.Visible;
            }

        }
        private void goForward(object sender,RoutedEventArgs e)
        {
            pgd.Next();
            Disp.ItemsSource = pgd.CurrentPageRecords;
            PageNo.Content = pgd.PageNumber.ToString();
            SetButtonStatus();
        }
        private void goBack(object sender, RoutedEventArgs e)
        {
            pgd.Previous();
            Disp.ItemsSource = pgd.CurrentPageRecords;
            PageNo.Content = pgd.PageNumber.ToString();
            SetButtonStatus();
        }
        private void SetButtonStatus()
        {
            if (pgd.PageNumber - 1 > 0)
            {
                BackArrow.IsEnabled = true;
            }
            else
            {
                BackArrow.IsEnabled = false;
            }
            if (pgd.PageNumber + 1 <= pgd.PageCount)
            {
                ForwardArrow.IsEnabled = true;
            }
            else
            {
                ForwardArrow.IsEnabled = false;
            }
        }
        private void Reset(object sender, RoutedEventArgs e)
        {
            List<SettingsProperty> properties = new List<SettingsProperty>();
            foreach (SettingsProperty s in ba.Properties)
            {
                properties.Add(s);
            }
            props = new List<SettingsListItem>();
            foreach (SettingsProperty sp in properties.OrderBy(x => x.Name))
            {
                props.Add(new SettingsListItem(sp.Name, sp.DefaultValue.ToString()));
            }
            pgd = new PageableGrid<SettingsListItem>(props, 8);
            Disp.ItemsSource = pgd.CurrentPageRecords;
        }
    }
}
