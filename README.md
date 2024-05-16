A simple tray icon app to swiftly change between two network cards.
The only configuration that is required is to put your interfaces' names into lines 21 and 22 of TrayIcon.cs
You can get these names by running this command in a command prompt: netsh interface show interface
The exact same names are required for the program to work properly.
It needs to be run on admin privileges as the commands (to turn cards on/off) won't work on lower level privileges. 
I plan on adding a GUI with checkbox buttons to enable graphically choosing two or more interfaces.
