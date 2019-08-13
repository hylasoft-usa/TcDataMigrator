using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TCMigrator.VisualUtilities
{
    public static class TextBoxGenerator
    {
        public static TextBox GenerateDefaultInput(string name)
        {
            TextBox tb = new TextBox();
            tb.Name = name;
            tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            tb.Margin = new System.Windows.Thickness(5);
            tb.TextWrapping = System.Windows.TextWrapping.Wrap;
            return tb;

        }
    }
}
