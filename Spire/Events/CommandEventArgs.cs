using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spire.Events
{
    public class CommandsLogEventArgs : EventArgs
    {
        public string Log { get; }

        public CommandsLogEventArgs(string log)
        {
            Log = log;
        }

    }
}
