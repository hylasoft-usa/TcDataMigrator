using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.Transform
{
    public class TransformOptions
    {
        private Dictionary<String, String> replacement;
        private List<String> remove;
        private bool trim;
        private bool skipFirstRow;
        public Dictionary<String, String> ReplacementDictionary { get { return this.replacement; } }
        public List<String> RemovalList { get { return this.remove; } }
        public bool Trim { get { return this.trim; } set { this.trim = value; } }
        public bool SkipFirstRow { get { return this.skipFirstRow; } set { this.skipFirstRow = value; } }

        public TransformOptions()
        {
            replacement = new Dictionary<string, string>();
            remove = new List<String>();
            trim = true;
        }
        public void addReplacement(String replace, String replaceWith)
        {
            if (!replacement.ContainsKey(replace))
            {
                replacement.Add(replace, replaceWith);
            }
        }
        public void removeReplacement(String key)
        {
            if (replacement.ContainsKey(key))
            {
                replacement.Remove(key);
            }
        }
        public void addReplacements(Dictionary<String, String> toAdd)
        {
            foreach(KeyValuePair<String,String> kvp in toAdd)
            {
                addReplacement(kvp.Key, kvp.Value);
            }
        }
        public void removeReplacements(Dictionary<String, String> toRemove)
        {
            foreach(KeyValuePair<String,String> kvp in toRemove)
            {
                removeReplacement(kvp.Key);
            }
        }
        public void addRemoval(String toRemove)
        {
            if (!remove.Contains(toRemove))
            {
                remove.Add(toRemove); 
            }
        }
        public void addRemovals(List<String> toRemove)
        {
            foreach(String s in toRemove)
            {
                addRemoval(s);
            }
        }
        public void removeRemoval(String toDelete)
        {
            if (remove.Contains(toDelete))
            {
                remove.Remove(toDelete);
            }
        }
        public void removalRemovals(List<String> toRemoveList)
        {
            foreach(string s in toRemoveList)
            {
                removeRemoval(s);
            }
        }
    }
}
