using System;
using System.Collections.Generic;
using System.Configuration;
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
using TCMigrator.VisualUtilities;

namespace TCMigrator.Settings
{
    /// <summary>
    /// Interaction logic for CSVSettings.xaml
    /// </summary>
    public partial class CSVSettings : Page
    {
        private Page _thisPage;
        private Grid _parentGrid;
        private Grid _childGrid;
        private Dictionary<String, TextBox> textboxes = new Dictionary<string, TextBox>();
        private ScrollViewer sv;
        private MainWindow mw;

        public CSVSettings(MainWindow main)
        {
            InitializeComponent();
            _thisPage = (Page)this;
            setupGrids();
            populateRows(_childGrid);
            sv = new ScrollViewer();
            sv.HorizontalAlignment = HorizontalAlignment.Stretch;
            sv.VerticalAlignment = VerticalAlignment.Stretch;
            sv.Content = _parentGrid;
            _thisPage.Content = sv;
            this.mw = main;
        }
        private void setupGrids()
        {
            Grid parent = setupParentGrid();
            Grid child = setupChildGrid();
            parent.Children.Add(child);
        }
        private Grid setupParentGrid()
        {
            Grid parent = GridGenerator.GenerateDefaultGrid();
            GridGenerator.AddRelativeGridColumns(parent, new int[3] { 1, 12, 1 });
            GridGenerator.AddRelativeGridRows(parent, new int[2] { 1, 8 });
            _parentGrid = parent;
            return parent;
        }
        private Grid setupChildGrid()
        {
            var count = Properties.CSVSettings.Default.Properties.Count;
            Grid child = GridGenerator.GenerateDefaultGrid();
            GridGenerator.AddRelativeGridColumns(child, new int[2] { 1, 5 });
            GridGenerator.AddUniformRows(child, count + 1); //+1 for the row containing buttons
            _childGrid = child;
            Grid.SetColumn(child, 1);
            Grid.SetRow(child, 1);
            return child;
        }
        private void populateRows(Grid child)
        {
            var propsCollection = Properties.CSVSettings.Default.Properties;
            var props = new List<SettingsProperty>();
            foreach (SettingsProperty sp in propsCollection)
            {
                props.Add(sp);
            }
            props.OrderBy(z => z.Name.Length);
            var propsCount = props.Count();
            var x = 0;
            foreach (SettingsProperty sp in props.OrderByDescending(z => z.Name.Length))
            {
                TextBlock block = TextBlockGenerator.GenerateDefaultLabelBlock(sp.Name);
                TextBox box = TextBoxGenerator.GenerateDefaultInput(sp.Name);
                box.Text = sp.DefaultValue.ToString();
                textboxes.Add(sp.Name, box);
                Grid.SetRow(block, x);
                Grid.SetRow(box, x);
                Grid.SetColumn(box, 1);
                child.Children.Add(block);
                child.Children.Add(box);
                x++;
            }
            makeButtons(child, propsCount);
        }
        private void makeButtons(Grid g, int row)
        {
            Grid gr = GridGenerator.GenerateDefaultGrid();
            GridGenerator.AddRelativeGridColumns(gr, new int[5] { 1, 3, 1, 3, 1 });
            Button save = ButtonGenerator.GenerateDefaultButton("Save");
            save.Style = Application.Current.FindResource("EngBlueBtn") as Style;
            Button cancel = ButtonGenerator.GenerateDefaultButton("Cancel");
            cancel.Style = Application.Current.FindResource("EngRedBtn") as Style;
            save.Click += saveSettings;
            cancel.Click += navigateAway;

            Grid.SetColumn(cancel, 3);
            Grid.SetColumn(save, 1);

            gr.Children.Add(save);
            gr.Children.Add(cancel);

            Grid.SetRow(gr, row);
            Grid.SetColumn(gr, 1);
            g.Children.Add(gr);
        }
        private void saveSettings(object sender, RoutedEventArgs e)
        {
            foreach (KeyValuePair<String, TextBox> entry in textboxes)
            {
                var val = entry.Value.Text;
                var name = entry.Key.Replace(' ', '_');
                foreach (SettingsProperty p in Properties.CSVSettings.Default.Properties)
                {
                    if (p.Name == name)
                    {
                        p.DefaultValue = val;
                    }
                }
            }
            Properties.CSVSettings.Default.Save();

        }
        private void navigateAway(object sender, RoutedEventArgs e)
        {
            mw.NavigateHome();
        }
    }
}
