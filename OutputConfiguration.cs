using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;
using System.Text;
using VRage.Game.GUI.TextPanel;

namespace IngameScript
{
    class OutputConfiguration
    {
        public int surfaceIndex;
        public string pattern;
        public IMyTextSurface debugLCD;
        public Severity severity = Severity.LOG;
        public int offsetSpaces = 2;
        public string lcdTextStore;
        public bool prependMessages = false;

        public OutputConfiguration(int surfaceIndex,IMyTextSurface myTextSurface)
        {
            this.surfaceIndex = surfaceIndex;
            debugLCD = myTextSurface;
        }

        public void WriteLine(LogEntry logEntry)
        {
            if (logEntry.severity <= severity)
            {
                if (System.Text.RegularExpressions.Regex.Match(logEntry.tag, pattern).Success)
                {
                    string line = logEntry.ToString(offsetSpaces);
                    if (prependMessages)
                    {
                        lcdTextStore = line + "\n" + lcdTextStore;
                        debugLCD.WriteText(lcdTextStore, false);
                    }
                    else
                    {
                        debugLCD.WriteText(line + "\n", true);
                    }
                }
            }
        }

        public void ReDraw(List<LogEntry> history)
        {
            debugLCD.ContentType = ContentType.TEXT_AND_IMAGE;
            debugLCD.Font = "Monospace";

            StringBuilder stringBuilder = new StringBuilder();
            int i = history.Count - 1;
            int lines = 0;
            List<string> lineStrings = new List<string>();
            while (lines < 40 && i >= 0)
            {
                LogEntry logEntry = history[i];
                if (logEntry.severity <= severity)
                {
                    if (System.Text.RegularExpressions.Regex.Match(logEntry.tag, pattern).Success)
                    {
                        lineStrings.Add(logEntry.ToString(offsetSpaces));
                        lines++;
                    }
                }
                i--;
            }
            if (!prependMessages)
            {
                lineStrings.Reverse();
            }
            foreach (string lineSting in lineStrings)
            {
                stringBuilder.AppendLine(lineSting);
            }
            lcdTextStore = stringBuilder.ToString();
            debugLCD.WriteText(lcdTextStore, false);
        }
    }
}
