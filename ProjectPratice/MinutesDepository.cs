using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace ProjectPratice
{
    public partial class MinutesDepository : Form
    {
        private SQLiteCommand DBcommand;
        private SQLiteConnection DBconnect;
        private SQLiteDataReader DBreader;
        Minute_Editor editor;
        private string minuteName;

        public MinutesDepository()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db;Compress= true;");
            editor = new Minute_Editor();
            minuteName = null;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;

            if (listView1.SelectedItems.Count > 0)
            {
                this.Size = new Size(879, 534);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                getMinutesFromDB(listView1.SelectedItems[0].Text);
                Minute_Editor.minuteName = listView1.SelectedItems[0].Text;
                this.Text = "Minutes Repository"+" |"+minuteName;
                
            }
            
            
        }

        private void MinutesDepository_Load(object sender, EventArgs e)
        {
            LoadDataIntoListView();

            //Hid some features of editor
            {
                //editor.openToolStripMenuItem1.Visible = false;
                editor.toolStripSeparator2.Visible = false;
            }

            editor.TopLevel = false;
            editor.FormBorderStyle = FormBorderStyle.None;
            editor.Dock = DockStyle.Fill;
            editor.Show();
            panel1.Controls.Add(editor);
            editor.openToolStripMenuItem1.Click += OpenToolStripMenuItem1_Click;
        }

        private void OpenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            chkShowList.Checked = true;
        }

        private void chkShowList_CheckedChanged(object sender, EventArgs e)
        {
            listView1.Visible = chkShowList.Checked;
            btnOpen.Visible =   chkShowList.Checked;
        }

        //Method that loads Data from DataBase into ListView
        private void LoadDataIntoListView()
        {
            listView1.Items.Clear();
            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();

                DBcommand = new SQLiteCommand("SELECT * FROM MeetingTable ORDER BY DATE", DBconnect);
                DBreader = DBcommand.ExecuteReader();

                while (DBreader.Read())
                {
                    ListViewItem item = new ListViewItem(DBreader["MeetingName"].ToString());
                    item.SubItems.Add(DBreader["Date"].ToString());
                    item.SubItems.Add(DBreader["Venue"].ToString());
                    item.SubItems.Add(DBreader["MinuteTaker"].ToString());
                    listView1.Items.Add(item);
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DBconnect.Close();
        }

        private void getMinutesFromDB( string name)
        {
            byte[] file = null;

            //if(Minute_Editor.saved== false)
            //{
            //    if (MessageBox.Show("Did you want to  skip saving?",
            //        "Saving Notice",MessageBoxButtons.YesNo, MessageBoxIcon.Information)== DialogResult.Yes)
            //    {
            //        return;
            //    }
            //}

            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();
                DBcommand = new SQLiteCommand("SELECT * FROM MeetingTable WHERE MeetingName='" +
                    name + "'", DBconnect);
                DBreader = DBcommand.ExecuteReader();

                while (DBreader.Read())
                {
                    file = (byte[])DBreader["Minute"];
                }
                editor.richTextBoxPrintCtrl1.Rtf = new UTF8Encoding(true).GetString(file);
                minuteName = name;
                Minute_Editor.minuteName = listView1.SelectedItems[0].Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Minute not found",
                   "Error Opening...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Size = new Size(400, 534);
                //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                //minuteName = null;
            }
            DBconnect.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.Size = new Size(400, 534);
            //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            editor.richTextBoxPrintCtrl1.Rtf = null;
            this.Text = "Minutes Repository";
            panel1.Visible = false;
        }

        
    }
}
