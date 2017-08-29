using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Framework
{
    public class PCStartHelp
    {
        public static void AddStart(string path, string name)
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            rk2.SetValue(name, path);
            rk2.Close();
            rk.Close();
        }
        public static void RemoveStart(string name)
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            rk2.DeleteValue(name, false);
            rk2.Close();
            rk.Close();
        }
    }
}
