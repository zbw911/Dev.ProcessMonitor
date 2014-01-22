using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.ProcessMonitor
{
    public class OutArg : EventArgs
    {
        #region Instance Properties

        public string OutPut { get; set; }

        public int ProcessId { get; set; }

        #endregion
    }

    public class StandardOutArg : OutArg
    {
    }

    public class StandardErrorArg : OutArg
    {
    }
}
