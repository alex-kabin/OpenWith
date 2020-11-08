# OpenWith
File open helper utility

Starts configured (in OpenWith.yaml) target program (or offers to choose one of some in UI-form) on specified (command line arg) source file.
Especially useful with Total Commander as F4 (Editor) hotkey tool.

For example, start 
```
OpenWith.exe movie.mkv
```
Utility will lookup configuration file (.yaml) for suitable command and executes it with specified args. 
If there are more than one suitable command found, program will ask you to pick one from UI form.
