using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRageMath;
using VRage.Game.GUI.TextPanel;

namespace IngameScript
{
    public static class LogManager
    {
        private static List<LogEntry> history = new List<LogEntry>();
        static IMyTextSurface debugLCD;
        static Severity severity = Severity.LOG;
        public static int offsetSpaces = 2;
        private static string lcdTextStore;
        private static bool prependMessages = false;

        //TODO: code to print history to lcd
        public static string add(LogEntry logEntry)
        {
            history.Add(logEntry);
            return InnerAdd(logEntry);
        }

        private static string InnerAdd(LogEntry logEntry)
        {
            if (debugLCD != null && logEntry.severity <= severity)
            {
                string line = logEntry.ToString(offsetSpaces);
                if (prependMessages)
                {
                    lcdTextStore = line + "\n" + lcdTextStore;
                    debugLCD.WriteText(lcdTextStore, false);
                } else
                {
                    debugLCD.WriteText(line + "\n", true);
                }
                return line;
            }
            return "";
        }

        private static void refreshLCD()
        {
            debugLCD.ContentType = ContentType.TEXT_AND_IMAGE;
            debugLCD.Font = "Monospace";

            StringBuilder stringBuilder = new StringBuilder();
            int i = history.Count() - 1;
            int lines = 0;
            List<string> lineStrings = new List<string>();
            while (lines < 40 && i >= 0)
            {
                LogEntry logEntry = history[i];
                if (logEntry.severity <= severity)
                {
                    lineStrings.Add(logEntry.ToString(offsetSpaces));
                    lines++;
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


        public static void setDebugPanel(IMyTextSurface _debugLCD)
        {
            debugLCD = _debugLCD;
            refreshLCD();
        }
        public static void reverseLogDirection()
        {
            prependMessages = !prependMessages;
            refreshLCD();
        }

        public static void setSeverity(Severity _severity)
        {
            severity = _severity;
            refreshLCD();
        }

        public static void setOffsetSpaces(int _offset)
        {
            offsetSpaces = _offset;
            refreshLCD();
        }
    }
}
