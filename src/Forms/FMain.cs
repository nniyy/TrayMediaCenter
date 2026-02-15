using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using TrayMediaCenter.Actions;
using Windows.Media.Control;
using TrayMediaCenter.Extensions;

namespace TrayMediaCenter.Forms;

public partial class FMain : Form
{
    private static string _appName = "Media tray center";
    private CancellationTokenSource? _flashCts;
    private Image? _thumb;
    private DateTime _lastHoverRefreshUtc = DateTime.MinValue;
    private ToolStripLabel _sourceLabel;
    private Icon? _overrideIcon;
    private readonly MediaControl _media = new();
    private bool _isDarkTheme;
    private bool _hoverRefreshing;


    public FMain()
    {
        InitializeComponent();

        _isDarkTheme = ThemeControl.IsSystemDarkThemeEnabled();

        _sourceLabel = new ToolStripLabel
        {
            AutoSize = true
        };
        contextMenuStrip.Items.Insert(0, _sourceLabel);
        contextMenuStrip.Items.Insert(1, new ToolStripSeparator());

        ApplyTrayState(MediaState.Empty);

        _media.StateChanged += (_, state) =>
        {
            if (IsDisposed) return;
            if (InvokeRequired) BeginInvoke(new Action(() => ApplyTrayState(state)));
            else ApplyTrayState(state);
        };

        timer.Start();
    }

    private async void FMain_Load(object sender, EventArgs e)
    {
        startOnToolStripMenuItem.Checked = StartupControl.IsEnabled(_appName);

        HideForm();

        try
        {
            await _media.InitializeAsync();
            ApplyTrayState(_media.Current);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void FMain_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            HideForm();
        }
    }

    private async void timer_Tick(object sender, EventArgs e)
    {
        await _media.RefreshAsync();
    }

    private async void trayIcon_MouseMove(object sender, MouseEventArgs e)
    {
        await RefreshOnHoverAsync();
    }

    private async void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            await FlashTrayIconAsync(GetSwitchIcon(forward: true));
            await _media.NextAsync();
            await _media.RefreshAsync();
        }
    }

    private async void trayIcon_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left) return;

        if ((ModifierKeys & Keys.Shift) == Keys.Shift)
        {
            await FlashTrayIconAsync(GetSwitchIcon(forward: false));
            await _media.PrevAsync();
        }
        else
            await _media.TogglePlayPauseAsync();

        await _media.RefreshAsync();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FormClosing -= FMain_FormClosing;

        timer.Stop();

        _flashCts?.Cancel();
        _flashCts?.Dispose();
        _flashCts = null;

        _thumb?.Dispose();
        _thumb = null;

        _media.Dispose();

        trayIcon.Visible = false;
        trayIcon.Dispose();

        Close();
    }

    private async void playPauseToolStripMenuItem_Click(object sender, EventArgs e)
    {
        await _media.TogglePlayPauseAsync();
        await _media.RefreshAsync();
    }

    private async void nextToolStripMenuItem_Click(object sender, EventArgs e)
    {
        await FlashTrayIconAsync(GetSwitchIcon(forward: true), 500);
        await _media.NextAsync();
        await _media.RefreshAsync();
    }

    private async void previousToolStripMenuItem_Click(object sender, EventArgs e)
    {
        await FlashTrayIconAsync(GetSwitchIcon(forward: false), 500);
        await _media.PrevAsync();
        await _media.RefreshAsync();
    }

    private void startOnToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (startOnToolStripMenuItem.Checked)
            StartupControl.Enable(_appName, Application.ExecutablePath);
        else
            StartupControl.Disable(_appName);
    }

    private async Task RefreshOnHoverAsync()
    {
        if (_hoverRefreshing) return;

        var now = DateTime.UtcNow;
        if ((now - _lastHoverRefreshUtc).TotalSeconds < 2) return;

        _hoverRefreshing = true;
        _lastHoverRefreshUtc = now;

        try
        {
            await _media.RefreshAsync();
        }
        catch
        {
        }
        finally
        {
            _hoverRefreshing = false;
        }
    }

    private void ApplyTrayState(MediaState state)
    {
        trayIcon.Icon = _overrideIcon ?? GetIconForState(state);
        trayIcon.Text = Formatter.BuildTooltip(state);

        _sourceLabel.Text = Formatter.BuildMenuTrackLine(state);
        SetSourceThumbnail(state.ThumbnailBytes);
        playPauseToolStripMenuItem.Text = state.IsPlaying ? "Pause" : "Play";

        bool hasSession = state.Status != GlobalSystemMediaTransportControlsSessionPlaybackStatus.Closed;
        playPauseToolStripMenuItem.Enabled = hasSession;
        nextToolStripMenuItem.Enabled = hasSession;
        previousToolStripMenuItem.Enabled = hasSession;
    }

    private Icon GetSwitchIcon(bool forward)
    {
        if (_isDarkTheme)
            return forward ? TrayIcons.NextLight : TrayIcons.PrevLight;

        return forward ? TrayIcons.NextDark : TrayIcons.PrevDark;
    }

    private Icon GetIconForState(MediaState state)
    {
        bool playing = state.IsPlaying;

        if (_isDarkTheme)
            return playing ? TrayIcons.PauseLight : TrayIcons.PlayLight;

        return playing ? TrayIcons.PauseDark : TrayIcons.PlayDark;
    }

    private async Task FlashTrayIconAsync(Icon icon, int ms = 500)
    {
        _flashCts?.Cancel();
        _flashCts?.Dispose();

        _flashCts = new CancellationTokenSource();
        var token = _flashCts.Token;

        _overrideIcon = icon;
        trayIcon.Icon = icon;

        try
        {
            await Task.Delay(ms, token);
        }
        catch (TaskCanceledException)
        {
            return;
        }
        finally
        {
            if (!token.IsCancellationRequested)
                _overrideIcon = null;
        }

        ApplyTrayState(_media.Current);
    }

    private void SetSourceThumbnail(byte[]? bytes)
    {
        _thumb?.Dispose();
        _thumb = null;

        if (bytes is null)
        {
            _sourceLabel.Image = null;
            return;
        }

        using var ms = new MemoryStream(bytes);
        using var img = Image.FromStream(ms);

        _thumb = new Bitmap(img, new Size(24, 24));
        _sourceLabel.Image = _thumb;
    }

    private void HideForm()
    {
        WindowState = FormWindowState.Minimized;
        ShowInTaskbar = false;
        Hide();
        trayIcon.Visible = true;
    }

    protected override void WndProc(ref Message m)
    {
        const int WM_SETTINGCHANGE = 0x001A;
        if (m.Msg == WM_SETTINGCHANGE)
        {
            bool current = ThemeControl.IsSystemDarkThemeEnabled();
            if (_isDarkTheme != current)
            {
                _isDarkTheme = current;
                ApplyTrayState(_media.Current);
            }
        }

        base.WndProc(ref m);
    }
}