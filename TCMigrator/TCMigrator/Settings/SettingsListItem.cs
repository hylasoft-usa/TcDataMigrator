using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.Settings
{
    public class SettingsListItem
    {
        private String _value = "";
        public String Name { get; set; }
        public String Value { get { return this._value; } set { this._value = value.ToString(); } }
        public SettingsListItem(String Name, String Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
