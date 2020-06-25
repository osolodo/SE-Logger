using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
    public enum Severity { ERROR, LOG, DEBUG }

    static class SeverityMethods
    {
        public static Severity FromString(string str)
        {
            switch (str)
            {
                case "ERROR":
                    return Severity.ERROR;
                case "LOG":
                    return Severity.LOG;
                case "DEBUG":
                    return Severity.DEBUG;
                default:
                    throw new Exception("Not a valid severity: "+str);
            }
        }
    }
}
