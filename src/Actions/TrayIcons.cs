using System.Reflection;
namespace TrayMediaCenter.Actions;

public static class TrayIcons
{
    private const string Base = "TrayMediaCenter.Resources.";

    public static readonly Icon PlayDark = Load(Base + "play_black.ico");
    public static readonly Icon PauseDark = Load(Base + "pause_black.ico");
    public static readonly Icon NextDark = Load(Base + "next_black.ico");
    public static readonly Icon PrevDark = Load(Base + "prev_black.ico");
    public static readonly Icon PlayLight = Load(Base + "play_white.ico");
    public static readonly Icon PauseLight = Load(Base + "pause_white.ico");
    public static readonly Icon NextLight = Load(Base + "next_white.ico");
    public static readonly Icon PrevLight = Load(Base + "prev_white.ico");

    private static Icon Load(string resourceName)
    {
        var asm = Assembly.GetExecutingAssembly();
        using var s = asm.GetManifestResourceStream(resourceName)
                      ?? throw new FileNotFoundException("Embedded resource not found: " + resourceName);

        return new Icon(s);
    }
}