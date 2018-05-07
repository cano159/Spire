using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spire.Events
{
    public class CommandsTraceEventArgs : EventArgs
    {
        public object Object { get; }

        public CommandsTraceEventArgs(object obj)
        {
            Object = obj;
        }
    }
}
