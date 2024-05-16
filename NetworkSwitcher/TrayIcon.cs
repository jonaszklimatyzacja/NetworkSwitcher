using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Windows.Forms;

public class TrayIconForm : Form
{
    private NotifyIcon trayIcon;
    private ContextMenu trayMenu;
    private MenuItem network1;
    private MenuItem network2; 

    //-------- CONFIGURATION HERE ----------------------------------------------------------------------------------
    //run this in command prompt:
    //netsh interface show interface

    //choose the interface name of two desired network interfaces
    //input them below:
    private string network1Name = "Wi-Fi";
    private string network2Name = "Ethernet";
    //that's all that needed to be done, the program should work now
    //remember that it needs to run with admin privilages, else the commands won't work

    public TrayIconForm()
    {
        // tray menu creation
        trayMenu = new ContextMenu();
        network1 = new MenuItem(network1Name, ChangeNetworks(1));
        network2 = new MenuItem(network2Name, ChangeNetworks(2));
        MenuItem exitItem = new MenuItem("Exit", OnExit);
        
        trayMenu.MenuItems.Add(network1);
        trayMenu.MenuItems.Add(network2);
        trayMenu.MenuItems.Add(exitItem);

        trayIcon = new NotifyIcon();
        trayIcon.Text = "Network Switcher";
        trayIcon.Icon = new Icon(NetworkSwitcher.Properties.Resources.project_icon, 40, 40);

        trayIcon.ContextMenu = trayMenu;
        trayIcon.Visible = true;
        
        CheckInterfaces();
    }

    private void CheckInterfaces() //this will give a checkmark in context menu to any previously given interface that is on
    {
        NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        List<string> names = new List<string>();

        foreach (NetworkInterface a in networkInterfaces)
        {
            names.Add(a.Name);
        }

        if (names.Contains(network1Name))
        {
            network1.Checked = true;
        }
        else
        {
            network1.Checked = false;
        }

        if (names.Contains(network2Name))
        {
            network2.Checked = true;
        }
        else
        {
            network2.Checked = false;
        }
    }

    private void RunCommand(string command, string name)
    {

        if (command == "enable" || command == "disable")
        {
            command = command.ToUpper() + "D";
            command = "netsh interface set interface " + name + " " + command;

            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;
            var process = Process.Start(processInfo);
            process.Close();
        }
    }

    private EventHandler ChangeNetworks(int n)
    {
        return (sender, e) =>
        {
            switch (n)
            {
                case 1: //Enable network 1, disable network2

                    RunCommand("enable", network1Name);
                    RunCommand("disable", network2Name);
                    network1.Checked = true;
                    network2.Checked = false;
                    break;


                case 2: //Enable network 2, disable network1

                    RunCommand("enable", network2Name);
                    RunCommand("disable", network1Name);
                    network2.Checked = true;
                    network1.Checked = false;
                    break;


                default:
                    CheckInterfaces();
                    break;
            }
        };
    }

    protected override void OnLoad(EventArgs e)
    {
        Visible = false; 
        ShowInTaskbar = false; 

        base.OnLoad(e);
    }

    private void OnExit(object sender, EventArgs e)
    {
        Application.Exit();
    }

    protected override void Dispose(bool isDisposing)
    {
        if (isDisposing)
        {
            trayIcon.Dispose();
        }

        base.Dispose(isDisposing);
    }
}
