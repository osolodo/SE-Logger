using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;
using System.Text;

namespace IngameScript
{
    public static partial class LogManager
    {
        public static void ReScan(Program program)
        {
            List<IMyTerminalBlock> outputBlocks = new List<IMyTerminalBlock>();
            program.GridTerminalSystem.SearchBlocksOfName(blockNameLabel, outputBlocks);
            List<OutputBlock> foundBlocks = new List<OutputBlock>();
            program.Echo("Output Blocks Found:" + outputBlocks.Count);
            foreach (IMyTerminalBlock terminalBlock in outputBlocks)
            {
                if (terminalBlock is IMyTextSurfaceProvider)
                {
                    OutputBlock outputBlock = outputs.Find(element => element.myTerminalBlock.EntityId == terminalBlock.EntityId);
                    if (outputBlock == null)
                    {
                        outputBlock = new OutputBlock(terminalBlock);
                    }
                    foundBlocks.Add(outputBlock);
                }
                else
                {
                    program.Echo("Could not create log on :"+terminalBlock.CustomName);
                }
            }

            outputs = foundBlocks;
        }
    }
}
