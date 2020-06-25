# SE Logger
 LCD Logger for ingame scripting in Space Engineers.

This is a logger to be added to scripts that use MDK,
designed to be easy to use when scripting and easy to use in game.

## Features
* Three severity levels: ERROR, LOG & DEBUG.
* Log lines grouped by tag and regex searchable.
* Indentation in log lines.
* Timestamps on each line.
* Per LCD output configured using Custom Data.
  * Severity level: equal or above.
  * Regex selector for tags.
  * Spaces per indentation level.
  * Reversable. New lines at the top or bottom.

## Setup

#### Writing Script
1. Include the mixin as described in the MDK guide.
2. Create a new Logger object where you need it.
This could be static per class or local to a sub-program,
however you want to organise it.
3. Call the Logger with .error .log or .debug
4. Change indentation with IncLvl and DecLvl.
5. Call LogManager.Refresh() with whatever frequency you want to update
the LCDs to output on.

#### Using in Game
1. Load your script into a Programable Block however you intend.
2. Add the block label to any block with a text surface, by default
this is "[Advanced Logger]", but you can set this with a call to LogManager.
3. In the Custom Data of the block add the config string:
"@\{text surface index\} \{Severity\}[ spaces:\{int\}][ reverse:true] \{regex tag matcher\}"
##### Examples:
* @0 DEBUG first
* @1 LOG spaces:4 main|igc
* @3 ERROR reverse:yes .
* @0 LOG spaces:1 reverse:yes Logger.*