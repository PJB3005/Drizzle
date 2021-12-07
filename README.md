# Drizzle, a Rain World level editor

Drizzle is a port and gradual rewrite of the official Rain World level editor (RWLE). Primary goals are to **make renders faster** and to **have a better interface**.

## Compiling and running

To run drizzle, you currently need to:
1. `git submodule update --init` to initialize the `Data/` submodule.
2. run `Drizzle.Transpiler` to transpile the Lingo code to C#.
3. run `Drizzle.Editor` or `Drizzle.ConsoleApp`, off you go!

## Project structure

The project is organized as such:
* `Drizzle.Lingo.Runtime`: Includes core logic necessary to run Lingo code required by RWLE.
* `Drizzle.Transpiler`: Transpiles Lingo into extremely messy, `dynamic` heavy C#. Requires `Drizzle.Lingo.Runtime` to parse Lingo.
* `Drizzle.Ported`: Contains transpiled C# code output by `Drizzle.Transpiler`.
* `Drizzle.Logic`: Contains C# logic shared between console app and GUI renderer, interfacing with the transpiled code.
* `Drizzle.ConsoleApp`: Console application for headless renders.
* `Drizzle.Editor`: GUI editor using Avalonia.