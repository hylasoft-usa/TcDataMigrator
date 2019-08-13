using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using W = System.Windows;
namespace TCMigrator.VisualUtilities
{
    public static class ButtonGenerator
    {
        public static Button GenerateDefaultButton(String content)
        {
            Button b = new Button();
            b.Content = content;
            b.HorizontalAlignment = W.HorizontalAlignment.Stretch;
            b.VerticalAlignment = W.VerticalAlignment.Center;
            b.Margin = new W.Thickness(5);
            return b;
        }
    }
}
