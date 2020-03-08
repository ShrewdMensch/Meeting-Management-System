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
    public partial class AudioArchive : Form
    {
        private SQLiteConnection DBconnect;
        private SQLiteCommand DBcommand;
        private SQLiteDataReader DBreader;
        private MemoryStream waveMemoryStream = null;
        private WaveFileReader waveReader = null;
        private DirectSoundOut soundOutput = null;


        public AudioArchive()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db;Compress= true;");
        }

      
        private void AudioArchive_Load(object sender, EventArgs e)
        {
            loadDataFromDBIntoDataGridView();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            

            string audioDate = null;
            DisposeWave();
            if (audioGridView.SelectedRows == null)
            {
                return;
                
            }

            byte[] audioFileAsBytes = null;
            object temp = null;

      
            try
            {
                audioDate = audioGridView.SelectedCells[0].OwningRow.Cells[0].Value.ToString();
                if (DBconnect.State.Equals(ConnectionState.Closed))
                    DBconnect.Open();
                DBcommand = new SQLiteCommand(@"SELECT Audio FROM MeetingTable WHERE Date='"+audioDate+"'", DBconnect);

                DBreader = DBcommand.ExecuteReader();
                
                while (DBreader.Read())
                {
                    audioFileAsBytes = (byte[])DBreader[0];
                    temp = DBreader[0];
                }

                //waveWriter = new WaveFileWriter("temp_audio.wav", new WaveFormat(44100,1));

                
                //waveWriter.Write(audioFileAsBytes, 0, audioFileAsBytes.Length);
                MemoryStream waveMemoryStream = new MemoryStream(audioFileAsBytes, 0, audioFileAsBytes.Length);
                    waveReader = new WaveFileReader(waveMemoryStream);
                    soundOutput = new DirectSoundOut();
                    soundOutput.Init(new WaveChannel32(waveReader));
                    soundOutput.Play();
            }

            catch(Exception ex)
            {

            }

            }

        private void DisposeWave()
        {
            if(soundOutput != null)
            {
                if(soundOutput.PlaybackState== PlaybackState.Playing)
                {
                    soundOutput.Stop();
                    soundOutput = null;
                }
            }
            if (waveReader!=null)
            {
                waveReader.Dispose();
                waveReader = null;
            }

            if (waveMemoryStream != null)
            {
                waveMemoryStream.Dispose();
                waveMemoryStream = null;
            }
        }

        private void AudioArchive_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposeWave();
        }

        private void btnPauseOrResume_Click(object sender, EventArgs e)
        {
            if (soundOutput != null)
            {
                if (soundOutput.PlaybackState == PlaybackState.Playing)
                {
                    soundOutput.Pause();
                    btnPauseOrResume.Text = "Resume";
                    return;
                }


                if (soundOutput.PlaybackState == PlaybackState.Paused)
                {
                    soundOutput.Play();
                    btnPauseOrResume.Text = "Pause";
                    return;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if(soundOutput != null)
            soundOutput.Stop();
            DisposeWave();
            if (btnPauseOrResume.Text == "Resume")
                btnPauseOrResume.Text = "Pause";
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadDataFromDBIntoDataGridView();
        }
    }
}
