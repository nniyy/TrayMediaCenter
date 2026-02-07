using Microsoft.Win32;

namespace TrayMediaCenter.Actions;

public class StartupControl
{
    private const string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";

    public static bool IsEnabled(string appName)
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKey, false);
        return key?.GetValue(appName) is string s && !string.IsNullOrWhiteSpace(s);
    }

    public static void Enable(string appName, string exePath)
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKey, true)
                        ?? Registry.CurrentUser.CreateSubKey(RunKey, true);
        
        key.SetValue(appName, $"\"{exePath}\"");
    }

    public static void Disable(string appName)
    {
        using var key = Registry.CurrentUser.OpenSubKey(RunKey, true);
        key?.DeleteValue(appName, false);
    }
}