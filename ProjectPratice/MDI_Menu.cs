using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ProjectPratice
{
    public partial class MDI_Menu : Form
    {
        Attendance fm2;
        Members mn;
        Minute_Editor editor;
        MinutesDepository minutesRepo;
        AudioArchive audioRepository;
        Notification mailForm;
        Recording audioRecForm;
        MeetingTakerMgt meetingMgt;

        private SQLiteConnection DBConnect;
        private SQLiteCommand DBcommand;

        public MDI_Menu()
        {
            //Thread t = new Thread(new ThreadStart(SplashStart));
            //t.Start();
            //Thread.Sleep(15000);
            //t.Abort();
            InitializeComponent();
            DBConnect = new SQLiteConnection("Data Source= dataBase.db;Compress= true;");
            fm2 = new Attendance();
            mn = new Members();
            editor = new Minute_Editor();
            audioRepository = new AudioArchive();
            mailForm = new Notification();
            audioRecForm = new Recording();
            minutesRepo = new MinutesDepository();
            meetingMgt = new MeetingTakerMgt();

            this.FormClosing += MDI_Menu_FormClosing;
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            audioRecordingToolStripMenuItem.Visible = false;
        }
        

        //public void SplashStart()
        //{
        //    Application.Run(new SplashScreen());
        //}
        private void MDI_Menu_Load(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Control cntrl in this.Controls)
            {
                if (cntrl is MdiClient)
                {
                    cntrl.BackColor = Color.DodgerBlue;
                }
            }
            audioRecordingToolStripMenuItem.Visible = false;
            // minutesToolStripMenuItem.Visible = false;
        }

        private void attendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fm2.IsDisposed)
            {
                fm2 = new Attendance();
                fm2.Show();
                fm2.MdiParent = this;
            }

            else
            {
                fm2.Show();
                fm2.MdiParent = this;
            }
            if (fm2.WindowState == FormWindowState.Minimized)
                fm2.WindowState = FormWindowState.Normal;
        }

        

        private void meetingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (mn.IsDisposed)
            {
                mn = new Members();
                mn.Show();
                mn.MdiParent = this;
            }

            else
            {
                mn.Show();
                mn.MdiParent = this;
            }

            if (mn.WindowState == FormWindowState.Minimized)
                mn.WindowState = FormWindowState.Normal;
        }

        private void goToMeetingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool alreadyEditingMinute = false;
            FormCollection openWindowsForm = Application.OpenForms;

            foreach (Form editor in openWindowsForm)
            {
                if (editor is Minute_Editor)
                    alreadyEditingMinute = true;
            }

            if (alreadyEditingMinute)
            {
                MessageBox.Show("Close Minute Editor First");
                return;
            }
            
            New_Meeting_Details newMeeting = new New_Meeting_Details();

            if (newMeeting.ShowDialog() == DialogResult.OK && New_Meeting_Details.dlgResult == DialogResult.No &&
                newMeeting.noEmptyField())
            {
                audioRecordingToolStripMenuItem.Visible = true;
                editor = new Minute_Editor();

                //Hid some feature of the editor
                {
                    editor.openToolStripMenuItem1.Visible = false;
                    editor.printPreviewToolStripMenuItem.Visible = false;
                    editor.printToolStripMenuItem.Visible = false;
                    editor.pageSetupToolStripMenuItem.Visible = false;
                    editor.toolStripSeparator2.Visible = false;
                    attendanceToolStripMenuItem.Visible = true;
                }

                string header = @"Meeting on "+ New_Meeting_Details.meetingDetails[4]+"\n";
                string ms = @"The " + New_Meeting_Details.meetingDetails[1] + " meeting of " +
                    New_Meeting_Details.meetingDetails[0] + " was held on " +
                    Convert.ToDateTime(New_Meeting_Details.meetingDetails[2]).ToLongDateString() + " at " +
                    Convert.ToDateTime(New_Meeting_Details.meetingDetails[7]).ToShortTimeString().ToLower() + ", at " +
                    New_Meeting_Details.meetingDetails[6]+
                    ", the President being in chair and the secretary being present"+" and minute taken by "+ 
                    New_Meeting_Details.meetingDetails[3]+
                    " .The minutes of last meeting were read and approved.";


                    editor.richTextBoxPrintCtrl1.SelectedText = header;
                    editor.richTextBoxPrintCtrl1.SelectedText=ms;
                    editor.richTextBoxPrintCtrl1.SelectedText=("\n");
                
                editor.Show();
                editor.MdiParent = this;
            }
        }

        private void minutesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (minutesRepo.IsDisposed)
            {
                minutesRepo = new MinutesDepository();
                minutesRepo.Show();
                minutesRepo.MdiParent = this;
            }

            else
            {
                minutesRepo.Show();
                minutesRepo.MdiParent = this;
            }

            if (minutesRepo.WindowState == FormWindowState.Minimized)
                minutesRepo.WindowState = FormWindowState.Normal;
        }

        private void audioRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (audioRecForm.IsDisposed)
            {
                audioRecForm = new Recording();
                audioRecForm.Show();
                audioRecForm.MdiParent = this;
            }

            else
            {
                audioRecForm.Show();
                audioRecForm.MdiParent = this;
            }


            if (audioRecForm.WindowState == FormWindowState.Minimized)
                audioRecForm.WindowState = FormWindowState.Normal;
        }

        private void audioArchivesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (audioRepository.IsDisposed)
            {
                audioRepository = new AudioArchive();
                audioRepository.Show();
                audioRepository.MdiParent = this;
            }

            else
            {
                audioRepository.Show();
                audioRepository.MdiParent = this;
            }

            if (audioRepository.WindowState == FormWindowState.Minimized)
                audioRepository.WindowState = FormWindowState.Normal;
        }

        private void notificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mailForm.IsDisposed)
            {
                mailForm = new Notification();
                mailForm.Show();
                mailForm.MdiParent = this;
            }

            else
            {
                mailForm.Show();
                mailForm.MdiParent = this;
            }

            if (mailForm.WindowState == FormWindowState.Minimized)
                mailForm.WindowState = FormWindowState.Normal;
        }

        private void minuteTakersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (meetingMgt.IsDisposed)
            {
                meetingMgt = new MeetingTakerMgt();
                meetingMgt.Show();
                meetingMgt.MdiParent = this;
            }

            else
            {
                meetingMgt.Show();
                meetingMgt.MdiParent = this;
            }

            if (meetingMgt.WindowState == FormWindowState.Minimized)
                meetingMgt.WindowState = FormWindowState.Normal;
        }

     
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn newLogInPage = new LogIn("Without SplashScreen");
            newLogInPage.Show();
        }


        private void MDI_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
                Application.Exit();
            
        }
    }
}
