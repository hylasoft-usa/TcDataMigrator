using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TCMigrator.VisualUtilities
{
    public static class GridGenerator
    {
        public static Grid GenerateDefaultGrid()
        {
            Grid g = new Grid();
            g.HorizontalAlignment = HorizontalAlignment.Stretch;
            g.VerticalAlignment = VerticalAlignment.Stretch;
            g.Margin = new Thickness(5);
            return g;
        }
        public static void AddRelativeGridColumns(Grid g, int[] relativeWidths)
        {
            foreach (int i in relativeWidths)
            {
                ColumnDefinition c = new ColumnDefinition();
                c.Width = new GridLength(i, GridUnitType.Star);
                g.ColumnDefinitions.Add(c);
            }
        }
        public static void AddRelativeGridRows(Grid g, int[] relativeHeights)
        {
            foreach (int i in relativeHeights)
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(i, GridUnitType.Star);
                g.RowDefinitions.Add(r);
            }
        }
        public static void AddUniformRows(Grid g, int num)
        {
            for (var x = 0; x < num; x++)
            {
                RowDefinition r = new RowDefinition();
                r.MinHeight = 25;
                g.RowDefinitions.Add(r);
            }
        }
    }
}
