A simple tray icon app to swiftly change between two network cards.
Only configuration that is requred is to put you interfaces' names into lines 21 and 22 of TrayIcon.cs
You can get these names by running this command in a command prompt: netsh interface show interface
The exact same names are required for the program to work properly.
It needs to be run on admin privilages as the commands (to turn on/off cards) won't work on lower level privilages 
