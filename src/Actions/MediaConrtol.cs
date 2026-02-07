using Windows.Media.Control;

namespace TrayMediaCenter.Actions;

public class MediaControl : IDisposable
{
    private GlobalSystemMediaTransportControlsSessionManager? _manager;
    private GlobalSystemMediaTransportControlsSession? _session;

    public event EventHandler<MediaState>? StateChanged;

    public MediaState Current { get; private set; } = MediaState.Empty;

    public async Task InitializeAsync()
    {
        _manager = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();
        _manager.CurrentSessionChanged += Manager_CurrentSessionChanged;

        await AttachSessionAsync(_manager.GetCurrentSession());
    }

    public async Task RefreshAsync() => await RefreshInternalAsync(playbackOnly: false);

    public async Task TogglePlayPauseAsync()
    {
        if (_session is not null)
        {
            await _session.TryTogglePlayPauseAsync();
            await RefreshInternalAsync(playbackOnly: true);
        }
    }

    public async Task NextAsync()
    {
        if (_session is not null)
        {
            await _session.TrySkipNextAsync();
        }
    }

    public async Task PrevAsync()
    {
        if (_session is not null)
        {
            await _session.TrySkipPreviousAsync();
        }
    }

    private async void Manager_CurrentSessionChanged(GlobalSystemMediaTransportControlsSessionManager sender, CurrentSessionChangedEventArgs args)
        => await AttachSessionAsync(sender.GetCurrentSession());

    private async Task AttachSessionAsync(GlobalSystemMediaTransportControlsSession? session)
    {
        if (_session is not null)
        {
            _session.PlaybackInfoChanged -= Session_PlaybackInfoChanged;
            _session.MediaPropertiesChanged -= Session_MediaPropertiesChanged;
        }

        _session = session;

        if (_session is null)
        {
            UpdateState(MediaState.Empty);
            return;
        }

        _session.PlaybackInfoChanged += Session_PlaybackInfoChanged;
        _session.MediaPropertiesChanged += Session_MediaPropertiesChanged;

        await RefreshInternalAsync(playbackOnly: false);
    }

    private async void Session_PlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender, PlaybackInfoChangedEventArgs args)
        => await RefreshInternalAsync(playbackOnly: true);

    private async void Session_MediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, MediaPropertiesChangedEventArgs args)
        => await RefreshInternalAsync(playbackOnly: false);

    private async Task RefreshInternalAsync(bool playbackOnly)
    {
        if (_session is null)
        {
            UpdateState(MediaState.Empty);
            return;
        }

        var pb = _session.GetPlaybackInfo();
        var status = pb.PlaybackStatus;

        string? title = Current.Title;
        string? artist = Current.Artist;
        byte[]? thumbBytes = Current.ThumbnailBytes;

        if (!playbackOnly)
        {
            var props = await _session.TryGetMediaPropertiesAsync();
            title = props?.Title;
            artist = props?.Artist;

            if (props?.Thumbnail is not null)
            {
                try
                {
                    using var ra = await props.Thumbnail.OpenReadAsync();
                    using var s = ra.AsStreamForRead();
                    using var ms = new MemoryStream();
                    await s.CopyToAsync(ms);
                    thumbBytes = ms.ToArray();
                }
                catch
                {
                    thumbBytes = null;
                }
            }
            else
            {
                thumbBytes = null;
            }
        }

        var source = _session.SourceAppUserModelId;
        UpdateState(new MediaState(status, title, artist, source, thumbBytes));
    }

    private void UpdateState(MediaState state)
    {
        Current = state;
        StateChanged?.Invoke(this, state);
    }

    public void Dispose()
    {
        if (_manager is not null)
            _manager.CurrentSessionChanged -= Manager_CurrentSessionChanged;

        if (_session is not null)
        {
            _session.PlaybackInfoChanged -= Session_PlaybackInfoChanged;
            _session.MediaPropertiesChanged -= Session_MediaPropertiesChanged;
        }
    }
}

public readonly record struct MediaState(
    GlobalSystemMediaTransportControlsSessionPlaybackStatus Status,
    string? Title,
    string? Artist,
    string? SourceAppId,
    byte[]? ThumbnailBytes)
{
    public bool IsPlaying => Status == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing;

    public static MediaState Empty =>
        new(GlobalSystemMediaTransportControlsSessionPlaybackStatus.Closed, null, null, null, null);
}