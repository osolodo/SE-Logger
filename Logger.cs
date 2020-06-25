using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
    public class Logger
        {
            private readonly string tag;
            private int offset = 0;

            public Logger(string tag)
            {
                this.tag = tag;
            }

            private string LogEntry(Severity severity, string message)
            {
                return LogManager.Add(new LogEntry(DateTime.UtcNow, this.offset, this.tag, severity, message));
            }

            public string Error(string msg)
            {
            return LogEntry(Severity.ERROR, msg);
            }
            public string Log(string msg)
            {
            return LogEntry(Severity.LOG, msg);
            }
            public string Debug(string msg)
            {
            return LogEntry(Severity.DEBUG, msg);
            }
            public void IncLvl()
            {
                offset++;
            }
            public void DecLvl()
            {
                offset--;
            }
        }
}
