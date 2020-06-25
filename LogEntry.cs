using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
    public struct LogEntry
    {
        public DateTime dateTime;
        public int offset;
        public string tag;
        public Severity severity;
        public string message;

        public LogEntry(DateTime dateTime, int offset, string tag, Severity severity, string message) : this()
        {
            this.dateTime = dateTime;
            this.offset = offset;
            this.tag = tag;
            this.severity = severity;
            this.message = message;
        }

        public string ToString(int offsetSpaces)
        {
            return String.Format("[{0} {1}] {2} {3}{4}", dateTime.ToShortTimeString(), severity.ToString().PadLeft(5), tag, new String(' ', offset * offsetSpaces), message);
        }
    }
}
