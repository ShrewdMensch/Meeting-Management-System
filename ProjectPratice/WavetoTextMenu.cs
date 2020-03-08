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
using NAudio.Wave;
using System.IO;


namespace ProjectPratice
{
    public partial class WavetoTextMenu : Form
    {
        private SQLiteConnection DBconnect;
        private SQLiteCommand DBcommand;
        private SQLiteDataReader DBreader;
        private static MemoryStream waveMemoryStream = null;

        public WavetoTextMenu()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db;Compress= true;");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string audioDate = null;
            if (audioGridView.SelectedRows == null)
            {
                return;

            }

            byte[] audioFileAsBytes = null;

            try
            {
                audioDate = audioGridView.SelectedCells[0].OwningRow.Cells[0].Value.ToString();
                if (DBconnect.State.Equals(ConnectionState.Closed))
                    DBconnect.Open();
                DBcommand = new SQLiteCommand(@"SELECT Audio FROM MeetingTable WHERE Date='" + audioDate + "'", DBconnect);

                DBreader = DBcommand.ExecuteReader();

                while (DBreader.Read())
                {
                    audioFileAsBytes = (byte[])DBreader[0];
                }

                waveMemoryStream = new MemoryStream(audioFileAsBytes, 0, audioFileAsBytes.Length);
                this.Close();
            }

            catch (Exception ex)
            {

            }
        }

        public static MemoryStream getWaveMemoryStream()
        {
            return waveMemoryStream;
        }

        private void loadDataFromDBIntoDataGridView()
        {
            //Load Data from MeetingTable into DataGridView audioGridView 
            try
            {
                if (DBconnect.State.Equals(ConnectionState.Closed))
                    DBconnect.Open();
                DBcommand = new SQLiteCommand(@"SELECT Date,MeetingGroup AS 'Meeting Group',MeetingKind AS 'Meeting Kind',
                MinuteTaker AS 'Meeting Taker',Time,Venue FROM MeetingTable", DBconnect);
                SQLiteDataAdapter da = new SQLiteDataAdapter(DBcommand);
                DataSet ds = new DataSet();
                da.Fill(ds);
                audioGridView.DataSource = ds.Tables[0];
                audioGridView.DefaultCellStyle.BackColor = Color.SkyBlue;
                //audioGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.RoyalBlue;
                //audioGridView.DefaultCellStyle.SelectionBackColor = Color.LightGreen;
                audioGridView.RowHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
                audioGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
                audioGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                DBconnect.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WavetoTextMenu_Load(object sender, EventArgs e)
        {
            loadDataFromDBIntoDataGridView();
        }
    }
}
