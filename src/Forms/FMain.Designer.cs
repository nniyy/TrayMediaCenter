namespace TrayMediaCenter.Forms;

partial class FMain
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FMain));
        trayIcon = new System.Windows.Forms.NotifyIcon(components);
        contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
        playPauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        previousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        startOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
        toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
        toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
        timer = new System.Windows.Forms.Timer(components);
        toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
        toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
        contextMenuStrip.SuspendLayout();
        SuspendLayout();
        // 
        // trayIcon
        // 
        trayIcon.ContextMenuStrip = contextMenuStrip;
        trayIcon.Icon = ((System.Drawing.Icon)resources.GetObject("trayIcon.Icon"));
        trayIcon.Visible = true;
        trayIcon.MouseClick += trayIcon_MouseClick;
        trayIcon.MouseDoubleClick += trayIcon_MouseDoubleClick;
        trayIcon.MouseMove += trayIcon_MouseMove;
        // 
        // contextMenuStrip
        // 
        contextMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
        contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { playPauseToolStripMenuItem, nextToolStripMenuItem, previousToolStripMenuItem, settingsToolStripMenuItem, exitToolStripMenuItem });
        contextMenuStrip.Name = "contextMenuStrip";
        contextMenuStrip.ShowImageMargin = false;
        contextMenuStrip.Size = new System.Drawing.Size(156, 136);
        // 
        // playPauseToolStripMenuItem
        // 
        playPauseToolStripMenuItem.Name = "playPauseToolStripMenuItem";
        playPauseToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
        playPauseToolStripMenuItem.Text = "Play/Pause";
        playPauseToolStripMenuItem.Click += playPauseToolStripMenuItem_Click;
        // 
        // nextToolStripMenuItem
        // 
        nextToolStripMenuItem.Name = "nextToolStripMenuItem";
        nextToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
        nextToolStripMenuItem.Text = "Next";
        nextToolStripMenuItem.Click += nextToolStripMenuItem_Click;
        // 
        // previousToolStripMenuItem
        // 
        previousToolStripMenuItem.Name = "previousToolStripMenuItem";
        previousToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
        previousToolStripMenuItem.Text = "Previous";
        previousToolStripMenuItem.Click += previousToolStripMenuItem_Click;
        // 
        // settingsToolStripMenuItem
        // 
        settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { startOnToolStripMenuItem });
        settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
        settingsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
        settingsToolStripMenuItem.Text = "Settings";
        // 
        // startOnToolStripMenuItem
        // 
        startOnToolStripMenuItem.CheckOnClick = true;
        startOnToolStripMenuItem.Name = "startOnToolStripMenuItem";
        startOnToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        startOnToolStripMenuItem.Text = "Autostart";
        startOnToolStripMenuItem.Click += startOnToolStripMenuItem_Click;
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
        exitToolStripMenuItem.Text = "Exit";
        exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
        // 
        // toolStripMenuItem1
        // 
        toolStripMenuItem1.Name = "toolStripMenuItem1";
        toolStripMenuItem1.Size = new System.Drawing.Size(32, 19);
        // 
        // toolStripMenuItem2
        // 
        toolStripMenuItem2.Name = "toolStripMenuItem2";
        toolStripMenuItem2.Size = new System.Drawing.Size(32, 19);
        // 
        // toolStripMenuItem3
        // 
        toolStripMenuItem3.Name = "toolStripMenuItem3";
        toolStripMenuItem3.Size = new System.Drawing.Size(32, 19);
        // 
        // toolStripMenuItem4
        // 
        toolStripMenuItem4.Name = "toolStripMenuItem4";
        toolStripMenuItem4.Size = new System.Drawing.Size(32, 19);
        // 
        // timer
        // 
        timer.Interval = 3000;
        timer.Tick += timer_Tick;
        // 
        // toolStripMenuItem5
        // 
        toolStripMenuItem5.Name = "toolStripMenuItem5";
        toolStripMenuItem5.Size = new System.Drawing.Size(32, 19);
        // 
        // toolStripMenuItem6
        // 
        toolStripMenuItem6.Name = "toolStripMenuItem6";
        toolStripMenuItem6.Size = new System.Drawing.Size(32, 19);
        // 
        // FMain
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(284, 261);
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        FormClosing += FMain_FormClosing;
        Load += FMain_Load;
        contextMenuStrip.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;

    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;

    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;

    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;

    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

    private System.Windows.Forms.NotifyIcon trayIcon;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

    #endregion

    private ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.Timer timer;
    private System.Windows.Forms.ToolStripMenuItem playPauseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem previousToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem startOnToolStripMenuItem;
}