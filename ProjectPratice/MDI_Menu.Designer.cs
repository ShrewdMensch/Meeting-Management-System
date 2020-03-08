using System.Drawing;
using System.Drawing.Drawing2D;

namespace ProjectPratice
{
    partial class MDI_Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            LinearGradientBrush br = new LinearGradientBrush(this.ClientRectangle, Color.Black, Color.Black, 0, false);
            ColorBlend cb = new ColorBlend();
            cb.Positions = new[] { 0,0.5f, 0f, 0.5f, 1 };
            cb.Colors = new[] { Color.Brown, Color.Orange, Color.Brown,Color.Orange,Color.Brown };
            br.InterpolationColors = cb;

            // rotate
            br.RotateTransform(0);
            // paint
            e.Graphics.FillRectangle(br, this.ClientRectangle);

            base.OnPaint(e);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDI_Menu));
            this.attendanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.goToMeetingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioArchivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meetingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minuteTakersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // attendanceToolStripMenuItem
            // 
            this.attendanceToolStripMenuItem.Name = "attendanceToolStripMenuItem";
            this.attendanceToolStripMenuItem.Size = new System.Drawing.Size(85, 21);
            this.attendanceToolStripMenuItem.Text = "&Attendance";
            this.attendanceToolStripMenuItem.Visible = false;
            this.attendanceToolStripMenuItem.Click += new System.EventHandler(this.attendanceToolStripMenuItem_Click);
            // 
            // minutesToolStripMenuItem
            // 
            this.minutesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("minutesToolStripMenuItem.Image")));
            this.minutesToolStripMenuItem.Name = "minutesToolStripMenuItem";
            this.minutesToolStripMenuItem.Size = new System.Drawing.Size(128, 21);
            this.minutesToolStripMenuItem.Text = "&Minutes Archive";
            this.minutesToolStripMenuItem.Click += new System.EventHandler(this.minutesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 21);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.SkyBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToMeetingToolStripMenuItem,
            this.attendanceToolStripMenuItem,
            this.audioArchivesToolStripMenuItem,
            this.audioRecordingToolStripMenuItem,
            this.notificationToolStripMenuItem,
            this.meetingToolStripMenuItem,
            this.minutesToolStripMenuItem,
            this.minuteTakersToolStripMenuItem,
            this.toolStripMenuItem1,
            this.logOutToolStripMenuItem,
            this.toolStripTextBox1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1244, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // goToMeetingToolStripMenuItem
            // 
            this.goToMeetingToolStripMenuItem.Image = global::ProjectPratice.Properties.Resources.appointment_new;
            this.goToMeetingToolStripMenuItem.Name = "goToMeetingToolStripMenuItem";
            this.goToMeetingToolStripMenuItem.Size = new System.Drawing.Size(160, 21);
            this.goToMeetingToolStripMenuItem.Text = "Add To&day\'s Meeting";
            this.goToMeetingToolStripMenuItem.Click += new System.EventHandler(this.goToMeetingToolStripMenuItem_Click);
            // 
            // audioArchivesToolStripMenuItem
            // 
            this.audioArchivesToolStripMenuItem.Image = global::ProjectPratice.Properties.Resources._1476810804_Music;
            this.audioArchivesToolStripMenuItem.Name = "audioArchivesToolStripMenuItem";
            this.audioArchivesToolStripMenuItem.Size = new System.Drawing.Size(122, 21);
            this.audioArchivesToolStripMenuItem.Text = "Audio &Archives";
            this.audioArchivesToolStripMenuItem.Click += new System.EventHandler(this.audioArchivesToolStripMenuItem_Click);
            // 
            // audioRecordingToolStripMenuItem
            // 
            this.audioRecordingToolStripMenuItem.Image = global::ProjectPratice.Properties.Resources._1476810773_mic;
            this.audioRecordingToolStripMenuItem.Name = "audioRecordingToolStripMenuItem";
            this.audioRecordingToolStripMenuItem.Size = new System.Drawing.Size(134, 21);
            this.audioRecordingToolStripMenuItem.Text = "&Audio Recording";
            this.audioRecordingToolStripMenuItem.Click += new System.EventHandler(this.audioRecordingToolStripMenuItem_Click);
            // 
            // notificationToolStripMenuItem
            // 
            this.notificationToolStripMenuItem.Image = global::ProjectPratice.Properties.Resources._1476811257_notification5;
            this.notificationToolStripMenuItem.Name = "notificationToolStripMenuItem";
            this.notificationToolStripMenuItem.Size = new System.Drawing.Size(103, 21);
            this.notificationToolStripMenuItem.Text = "Notifi&cation";
            this.notificationToolStripMenuItem.Click += new System.EventHandler(this.notificationToolStripMenuItem_Click);
            // 
            // meetingToolStripMenuItem
            // 
            this.meetingToolStripMenuItem.AutoSize = false;
            this.meetingToolStripMenuItem.Image = global::ProjectPratice.Properties.Resources._1470549563_userconfig;
            this.meetingToolStripMenuItem.Name = "meetingToolStripMenuItem";
            this.meetingToolStripMenuItem.Size = new System.Drawing.Size(94, 21);
            this.meetingToolStripMenuItem.Text = "M&embers";
            this.meetingToolStripMenuItem.Click += new System.EventHandler(this.meetingToolStripMenuItem_Click);
            // 
            // minuteTakersToolStripMenuItem
            // 
            this.minuteTakersToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("minuteTakersToolStripMenuItem.Image")));
            this.minuteTakersToolStripMenuItem.Name = "minuteTakersToolStripMenuItem";
            this.minuteTakersToolStripMenuItem.Size = new System.Drawing.Size(117, 21);
            this.minuteTakersToolStripMenuItem.Text = "Mi&nute Takers";
            this.minuteTakersToolStripMenuItem.Click += new System.EventHandler(this.minuteTakersToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(67, 21);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.Color.SkyBlue;
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolStripTextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 21);
            // 
            // MDI_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1244, 556);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MDI_Menu";
            this.Text = "Meeting Manager";
            this.TransparencyKey = System.Drawing.Color.DodgerBlue;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MDI_Menu_FormClosing);
            this.Load += new System.EventHandler(this.MDI_Menu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem goToMeetingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attendanceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioArchivesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioRecordingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meetingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem minuteTakersToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
    }
}