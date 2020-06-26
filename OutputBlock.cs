using Sandbox.ModAPI.Ingame;
using System.Collections.Generic;

namespace IngameScript
{
    class OutputBlock
    {
        public readonly IMyTerminalBlock myTerminalBlock;
        private List<OutputConfiguration> surfaces = new List<OutputConfiguration>();

        public OutputBlock(IMyTerminalBlock myTerminalBlock)
        {
            this.myTerminalBlock = myTerminalBlock;
            ReloadSettings(null);
        }

        public void ReloadSettings(Program program)
        {
            List<OutputConfiguration> newSurfaces = new List<OutputConfiguration>();
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(myTerminalBlock.CustomData, "@(\\d) (ERROR|LOG|DEBUG) *(spaces:(\\d))? *(reverse:true)? *(.+)?");
            if (program!=null) { program.Echo(myTerminalBlock.CustomData); }
            while (match.Success)
            {
                System.Text.RegularExpressions.GroupCollection groups = match.Groups;
                int textSurfaceIndex = int.Parse(groups[1].Value);

                OutputConfiguration outputConfiguration = surfaces.Find(element => element.surfaceIndex == textSurfaceIndex);

                if(outputConfiguration == null)
                {
                    outputConfiguration = new OutputConfiguration(textSurfaceIndex, ((IMyTextSurfaceProvider)myTerminalBlock).GetSurface(textSurfaceIndex));
                }
                outputConfiguration.severity = SeverityMethods.FromString(groups[2].Value);

                if (groups[4].Success)
                {
                    outputConfiguration.offsetSpaces = int.Parse(groups[4].Value);
                }
                if (groups[5].Success)
                {
                    outputConfiguration.prependMessages = true;
                }
                if (groups[6].Success)
                {
                    outputConfiguration.pattern = groups[6].Value;
                }
                newSurfaces.Add(outputConfiguration);
                match = match.NextMatch();
            }
            surfaces.Clear();
            surfaces = newSurfaces;
        }

        public void WriteLine(LogEntry logEntry)
        {
            foreach (OutputConfiguration outputConfiguration in surfaces)
            {
                outputConfiguration.WriteLine(logEntry);
            }
        }

        public void ReDraw(List<LogEntry> history)
        {
            foreach (OutputConfiguration outputConfiguration in surfaces)
            {
                outputConfiguration.ReDraw(history);
            }
        }
    }
}
