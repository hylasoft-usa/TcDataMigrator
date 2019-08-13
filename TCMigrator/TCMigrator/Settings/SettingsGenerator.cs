using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TCMigrator.VisualUtilities;

namespace TCMigrator.Settings
{
    public class SettingsGenerator
    {
        private Grid _parent;
        private Grid _child;
        private Style _goodStyle;
        private Style _badStyle;
        private Dictionary<String, TextBox> _inputs;
        private SettingsPropertyCollection _settings;
        public SettingsGenerator(SettingsPropertyCollection c)
        {
            this._settings = c;
            this._inputs = new Dictionary<string, TextBox>();
        }
        public Grid GetSettingsGrid(Button save,Button cancel)
        {
            generateParent();
            generateChild();
            populateRows();
            addButtons(save, cancel);
            _parent.Children.Add(_child);
            return _parent;
        }
        private void generateParent()
        {
            Grid g = GridGenerator.GenerateDefaultGrid();
            GridGenerator.AddRelativeGridColumns(g,new int[3] { 1, 12, 1 });
            GridGenerator.AddRelativeGridRows(g, new int[2] { 1, 8 });
            _parent = g;

        }
        private void generateChild()
        {
            var count = _settings.Count;
            Grid g = GridGenerator.GenerateDefaultGrid();
            GridGenerator.AddRelativeGridColumns(g, new int[2] { 1, 5 });
            GridGenerator.AddUniformRows(g, count + 1);
            Grid.SetColumn(g, 1);
            Grid.SetRow(g, 1);
            _child = g;
        }
        private void populateRows()
        {
            var props = new List<SettingsProperty>();
            foreach(SettingsProperty sp in _settings)
            {
                props.Add(sp);
            }
            props.OrderByDescending(z => z.Name.Length);
            for(var x = 0; x < props.Count; x++)
            {
                var prop = props[x];
                TextBlock block = TextBlockGenerator.GenerateDefaultLabelBlock(prop.Name);
                TextBox box = TextBoxGenerator.GenerateDefaultInput(prop.Name);
                box.Text = prop.DefaultValue.ToString();
                _inputs.Add(prop.Name, box);
                Grid.SetRow(block, x);
                Grid.SetRow(box, x);
                Grid.SetColumn(box, 1);
                _child.Children.Add(block);
                _child.Children.Add(box);
            }            
        }
        private void addButtons(Button save, Button cancel)
        {
            Grid gr = GridGenerator.GenerateDefaultGrid();
            GridGenerator.AddRelativeGridColumns(gr, new int[5] { 1, 3, 1, 3, 1 });
            save.Style = _goodStyle;
            cancel.Style = _badStyle;
            Grid.SetColumn(save, 1);
            Grid.SetColumn(cancel, 3);
            gr.Children.Add(save);
            gr.Children.Add(cancel);
            Grid.SetRow(gr, _settings.Count);
            Grid.SetColumn(gr, 1);
            _child.Children.Add(gr);
        }

    }
}
