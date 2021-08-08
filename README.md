# Drizzle, a Rain World level editor

Drizzle is a port and gradual rewrite of the official Rain World level editor. Primary goals are to **make renders faster** and to **have a better interface**.

## Compiling and running

To run drizzle, you currently need to:
1. `git submodule update --init` to initialize the `Data/` submodule.
2. run `Drizzle.Transpiler` to transpile the Lingo code to C#.
3. run `Drizzle.Editor`, off you go!