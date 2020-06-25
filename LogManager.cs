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
    public static partial class LogManager
    {
        private static string blockNameLabel = "[Advanced Logger]";
        private static readonly List<LogEntry> history = new List<LogEntry>();
        private static List<OutputBlock> outputs = new List<OutputBlock>();

        //TODO: code to print history to lcd
        public static string Add(LogEntry logEntry)
        {
            history.Add(logEntry);
            InnerAdd(logEntry);
            return logEntry.ToString(2);
        }

        private static void InnerAdd(LogEntry logEntry)
        {
            foreach (OutputBlock output in outputs)
            {
                output.WriteLine(logEntry);
            }
        }

        public static void Refresh(Program program)
        {
            ReScan(program);
            foreach (OutputBlock output in outputs)
            {
                output.ReloadSettings(program);
                output.ReDraw(history);
            }
        }

        public static void SetBlockNameLabel(string label)
        {
            blockNameLabel = label;
        }
    }
}
