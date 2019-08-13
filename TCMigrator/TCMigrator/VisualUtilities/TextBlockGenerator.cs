using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using TCMigrator.Helpers;

namespace TCMigrator.VisualUtilities
{
    public static class TextBlockGenerator
    {
        public static TextBlock GenerateDefaultLabelBlock(string propertyName)
        {
            TextBlock block = new TextBlock();
            block.Text = PropertyHelper.ConvertPropertyNameToLabel(propertyName + ":");
            block.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            block.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            block.TextAlignment = System.Windows.TextAlignment.Right;
            block.TextWrapping = System.Windows.TextWrapping.Wrap;
            block.Foreground = new SolidColorBrush(Color.FromRgb(4, 28, 44));
            block.FontFamily = new FontFamily("Segoe UI Semilight");
            return block;
        }

    }
}
