using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
    public class Logger
        {
            private string tag;
            private int offset = 0;

            public Logger(string tag)
            {
                this.tag = tag;
            }

            private string logEntry(Severity severity, string message)
            {
                return LogManager.add(new LogEntry(DateTime.UtcNow, this.offset, this.tag, severity, message));
            }

            public string error(string msg)
            {
            return logEntry(Severity.ERROR, msg);
            }
            public string log(string msg)
            {
            return logEntry(Severity.LOG, msg);
            }
            public string debug(string msg)
            {
            return logEntry(Severity.DEBUG, msg);
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
