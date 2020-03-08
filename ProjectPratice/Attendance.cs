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
using System.IO;

namespace ProjectPratice
{
    public partial class Attendance : Form
    {
        private SQLiteConnection DBconnect;
        private SQLiteCommand sqlite_cmd;
        private SQLiteDataReader sqlite_reader;
        private StringBuilder buildText;

        public Attendance()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db;");
            listView1.ItemChecked += listView1_ItemChecked;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (DBconnect.State.Equals(ConnectionState.Closed))

                DBconnect.Open();
            listView1.Items.Clear();
            try
            {

                sqlite_cmd = new SQLiteCommand("SELECT * FROM MembersTable ORDER BY FullName", DBconnect);

                SQLiteDataAdapter da = new SQLiteDataAdapter(sqlite_cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListViewItem item = new ListViewItem(dr["StaffID"].ToString());
                    item.SubItems.Add(dr["FullName"].ToString());
                    item.SubItems.Add(dr["Department"].ToString());
                    item.SubItems.Add(dr["Email"].ToString());
                    listView1.Items.Add(item);
                }
           
                DBconnect.Close();
            btnDeselect.Enabled = false;
            btnSelect.Enabled = true;

        }
            catch(Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            populateSelectedItemsToText();
            
            Byte[] info = new ASCIIEncoding().GetBytes(buildText.ToString());
            FileStream fs = new FileStream("new.txt", FileMode.Create, FileAccess.Write);
            fs.Write(info, 0, info.Length);
            fs.Close();
            //Insert Attendance into Database
            insertAttendanceIntoDB(buildText.ToString());
            vacuumDatabase();
            MessageBox.Show(this, "Today's attendance successfully added!", "Saving...", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


        }

        private void vacuumDatabase()
        {
            if (DBconnect.State.Equals(ConnectionState.Closed))
                DBconnect.Open();
            
           sqlite_cmd = new SQLiteCommand("VACUUM;", DBconnect);
            sqlite_cmd.ExecuteNonQuery();

            DBconnect.Close();
        }

        public void populateSelectedItemsToText()
        {
            buildText = new StringBuilder();
            foreach (ListViewItem item in this.listView1.CheckedItems)
            {
                try
                {
                    if (DBconnect.State.Equals(ConnectionState.Closed))
                        DBconnect.Open();
                    string query = "SELECT * FROM MembersTable WHERE FullName= '" + item.SubItems[1].Text + "'";
                    sqlite_cmd = new SQLiteCommand(query, DBconnect);
                    sqlite_reader = sqlite_cmd.ExecuteReader();
                    string id = null;
                    string name = null;
                    string department = null;
                    string email = null;

                    while (sqlite_reader.Read())
                    {
                        id = sqlite_reader["StaffID"].ToString();
                        name = sqlite_reader["FullName"].ToString();
                        department = sqlite_reader["Department"].ToString();
                        email = sqlite_reader["Email"].ToString();
                    }

                    buildText.AppendFormat("{0} ({1})",  name,department);
                    buildText.AppendLine();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void insertAttendanceIntoDB(string path)
        {
            try
            {
                if (DBconnect.State.Equals(ConnectionState.Closed))
                    DBconnect.Open();
                
                
                string query = "UPDATE MeetingTable SET Attendance='"+
                    path+"' WHERE Date='"+DateTime.Now.ToShortDateString()+"'";
                sqlite_cmd = new SQLiteCommand(query, DBconnect);
                sqlite_cmd.ExecuteNonQuery();
                DBconnect.Close();
                

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (listView1.SelectedItems[0].Checked == false)
               
                    listView1.SelectedItems[0].Checked = true;
                else
                listView1.SelectedItems[0].Checked = false;
                 
            }
        }
        

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            label2.Text = listView1.CheckedItems.Count.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
            foreach(ListViewItem item in listView1.Items)
            {
                item.Checked = true;
                
            }
            btnDeselect.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = false;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongDateString().ToString();
            btnDeselect.Enabled = false;
            btnSelect.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    richTextBox1.UseWaitCursor = true;
        //   // System.Threading.Thread.Sleep(5000);
        //    retrieveArtfFileFromDB("SELECT * FROM AttendanceRecord WHERE ID= '3'");
        //    richTextBox1.UseWaitCursor = false;
        //}

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

      
    }
}
