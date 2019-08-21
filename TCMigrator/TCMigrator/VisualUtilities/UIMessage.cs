using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.VisualUtilities
{
    public class UIMessage
    {
        private UIMessageType _type;
        private String _message;
        public String Message { get; }
        public UIMessageType MessageType { get; }
        public UIMessage(UIMessageType type, String message)
        {
            this._type = type;
            this._message = message;
        }
        public override string ToString() {
            return _message;
        }
    }
}
