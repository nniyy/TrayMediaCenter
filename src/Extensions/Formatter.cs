using Windows.Media.Control;
using TrayMediaCenter.Actions;

namespace TrayMediaCenter.Extensions;

public class Formatter
{
    public static string BuildMenuTrackLine(MediaState state)
    {
        if (state.Status == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Closed)
            return "No media session";

        var track = ComposeTrack(state);
        if (!string.IsNullOrWhiteSpace(track))
            return Truncate(track, 60);

        return $"Source: {FriendlySource(state.SourceAppId)}";
    }
    
    public static string BuildTooltip(MediaState state)
    {
        string text;

        if (state.Status == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Closed)
        {
            text = "No media session";
        }
        else
        {
            var mode = state.IsPlaying ? "Playing" : "Paused";
            var track = ComposeTrack(state);
            text = !string.IsNullOrWhiteSpace(track)
                ? $"{mode}: {track}"
                : $"{mode}: {FriendlySource(state.SourceAppId)}";
        }

        return Truncate(text, 60);
    }

    private static string ComposeTrack(MediaState state)
    {
        var title = state.Title?.Trim();
        var artist = state.Artist?.Trim();

        if (!string.IsNullOrWhiteSpace(artist) && !string.IsNullOrWhiteSpace(title))
            return $"{artist} - {title}";

        return title ?? "";
    }

    private static string FriendlySource(string? appId)
    {
        if (string.IsNullOrWhiteSpace(appId)) return "Unknown";
        var s = appId;

        var excl = s.IndexOf('!');
        if (excl >= 0) s = s[..excl];

        var lastDot = s.LastIndexOf('.');
        if (lastDot >= 0 && lastDot + 1 < s.Length)
            s = s[(lastDot + 1)..];

        return s;
    }

    private static string Truncate(string text, int max)
    {
        if (string.IsNullOrEmpty(text)) return text;
        if (max < 4) return text[..Math.Min(text.Length, max)];
        return text.Length <= max ? text : text[..(max - 3)] + "...";
    }
    
}