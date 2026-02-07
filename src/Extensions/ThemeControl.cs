using Microsoft.Win32;

namespace TrayMediaCenter.Extensions;

public class ThemeControl
{
    public static bool IsSystemDarkThemeEnabled()
    {
        const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        const string valueName = "SystemUsesLightTheme";

        using var key = Registry.CurrentUser.OpenSubKey(keyPath);
        var v = key?.GetValue(valueName);

        return v is int i ? i == 0 : true;
    }
}