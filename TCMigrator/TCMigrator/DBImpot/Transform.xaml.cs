using System;
using System.Collections.Generic;
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
using TCMigrator.Data;
using TCMigrator.Interfaces;
using TCMigrator.Transform;

namespace TCMigrator.DBImpot
{
    /// <summary>
    /// Interaction logic for Transform.xaml
    /// </summary>
    public partial class Transform : Page
    {
        Dictionary<String, String> replacementDict;
        List<String> removalList;
        List<String> disAllowList;
        List<TransformDescriptor> transforms;
        IPageMediator main;
        public Transform(IPageMediator main)
        {
            InitializeComponent();
            replacementDict = new Dictionary<string, string>();
            removalList = new List<String>();
            this.main = main;
        }
        private void AddReplacementItem(object sender, RoutedEventArgs e)
        {
            TransformList.Items.Add(new TransformDescriptor() { id = TransformList.Items.Count, type = Enums.TransformType.REPLACE, value = Replace.Text, replacement = With.Text });
            replacementDict.Add(Replace.Text, With.Text);
            Replace.Text = "";
            With.Text = "";
        }
        private void RemoveNoReplace(object sender, RoutedEventArgs e)
        {
            TransformList.Items.Add(new TransformDescriptor() { id = TransformList.Items.Count, type = Enums.TransformType.REMOVE, value = Remove.Text, replacement = "" });
            removalList.Add(Remove.Text);
            Remove.Text = "";
        }
        private void DeleteSelected(object sender, RoutedEventArgs e)
        {
            TransformList.Items.Remove(TransformList.SelectedItem);
        }
        private void PerformTransform(object sender, RoutedEventArgs e)
        {
            var transformer = new GenericTransformer();
            TransformOptions to = new TransformOptions();
            to.addReplacements(createReplacementDict());
            to.addRemovals(createRemovalList());
            if(Trim.IsChecked.HasValue && Trim.IsChecked.Value == true)
            {
                to.Trim = true;
            }
            main.updateData(transformer.transform(main.getCurrentData(), to));
            writeCSV(main.getCurrentData());
            main.advance();
        }
        private void GoBack(object sender, RoutedEventArgs e) { }
        private Dictionary<String, String> createReplacementDict()
        {
            Dictionary<String, String> replacement = new Dictionary<string, string>();
            foreach (TransformDescriptor to in TransformList.Items)
            {
                if (to.type == Enums.TransformType.REPLACE)
                {
                    replacement.Add(to.value, to.replacement);
                }
            }
            return replacement;
        }
        private List<String> createRemovalList()
        {
            List<String> remove = new List<String>();
            foreach(TransformDescriptor td in TransformList.Items)
            {
                if (td.type == Enums.TransformType.REMOVE)
                {
                    remove.Add(td.value);
                }
            }
            return remove;
        }
        private void writeCSV(ImportData d)
        {
            ICsv csv = new CSV.GenericCsv();
            csv.Write(d);
        }
    }
}
