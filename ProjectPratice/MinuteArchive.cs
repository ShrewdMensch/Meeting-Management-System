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

namespace ProjectPratice
{
    public partial class MinuteArchive : Form
    {
        public event EventHandler<fetchDataEvent> retrievingMinuteDetails;
        private SQLiteCommand DBcommand;
        private SQLiteConnection DBconnect;
        private SQLiteDataReader DBreader;

        public MinuteArchive()
        {
            InitializeComponent();

            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db;Compress= true;");
            btnOk.Click += BtnOk_Click;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 0) return;
            
            retrievingMinuteDetails.Invoke(this, new fetchDataEvent(
                listView1.SelectedItems[0].SubItems[0].Text, listView1.SelectedItems[0].SubItems[1].Text));
            this.Dispose();
        }

        private void MinuteArchive_Load(object sender, EventArgs e)
        {
            btnOk.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;

            listView1.Items.Clear();
            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();

                DBcommand = new SQLiteCommand("SELECT * FROM MeetingTable", DBconnect);
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
    }

    public class fetchDataEvent : EventArgs
    {
        private string minuteName;
        private string minuteDate;
        
        //MinuteName property definition
            public string MinuteName
        {
            get { return minuteName; }
            set { minuteName = value; }
        }

        //MinuteDate property definition
        public string MinuteDate
        {
            get { return minuteDate; }
            set { minuteDate = value; }
        }

        //Constructor
        public fetchDataEvent(string minName,string minDate)
        {
            MinuteName = minName;
            MinuteDate = minDate;
        }


    }
}
