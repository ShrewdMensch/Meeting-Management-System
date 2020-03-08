using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using System.Diagnostics;
using System.IO;
using System.Data.SQLite;

namespace ProjectPratice
{
    public partial class Recording : Form
    {
        WaveIn sourceStream = null;
        WaveFileWriter waveWriter = null;
        Stopwatch elapsedTimer;
        private SQLiteConnection DBconnect;
        private SQLiteCommand sqlite_cmd;
        private SQLiteDataReader sqlite_reader;


        public Recording()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db;Compress= true;");
            elapsedTimer = new Stopwatch();
        }

        //Event that starts recording 
        private void SourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            //check if an audio is currently written by the waveWriter 
            if (waveWriter == null) return;
            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }

        private void Recording_Load(object sender, EventArgs e)
        {
            btnStopAndSave.Enabled = false;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            DisposeWave();
            sourceStream = new WaveIn();
            sourceStream.WaveFormat = new WaveFormat(44100, 1);
            waveWriter = new WaveFileWriter("temp.wav", sourceStream.WaveFormat);
            sourceStream.DataAvailable += new EventHandler<WaveInEventArgs>(SourceStream_DataAvailable);
            btnStopAndSave.Enabled = true;
            timer1.Start();
            elapsedTimer = new Stopwatch();
            elapsedTimer.Start();
            btnRecord.Enabled = false;
            sourceStream.StartRecording();
        }

        private void DisposeWave()
        {
            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;
            }

            if (waveWriter != null)
            {
                waveWriter.Dispose();
                waveWriter = null;
            }
        }

        private void btnStopAndSave_Click(object sender, EventArgs e)
        {
            DisposeWave(); //Dispose wave related instance in order to start afresh
            elapsedTimer.Stop();
            timer1.Stop();

            insertAudioFileIntoDB("temp.wav"); //save file into Database
            btnRecord.Enabled = true; //Disable btnRecord
            btnStopAndSave.Enabled = false;

            //reset the recording time to 00:00:00
            TimeSpan ts = TimeSpan.Zero;
            label1.Text = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        }

        //Method that inserts recorded audio into DB
        public void insertAudioFileIntoDB(string path)
        {
            var currentDate = DateTime.Today.ToShortDateString();
            int recordCount = 0;
            string query = null;
            DialogResult overWriteStatus = DialogResult.No;
            byte[] file = null;

            try
            {

                if (DBconnect.State.Equals(ConnectionState.Closed))
                    DBconnect.Open();

                //Read recorded audio as Binary 
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        file = reader.ReadBytes((int)stream.Length);
                    }

                    stream.Close();
                }


                query = @"UPDATE MeetingTable SET Audio=@file WHERE Date='"
                + currentDate + "'";
                sqlite_cmd = new SQLiteCommand(query, DBconnect);
                sqlite_cmd.Parameters.Add("@file", DbType.Binary, file.Length).Value = file;
                sqlite_cmd.ExecuteNonQuery();
                MessageBox.Show("Data Successfully Saved", "Saving Status", MessageBoxButtons.OK);

            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            File.Delete("temp.wav");
            DBconnect.Close(); //Close Database connection

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = elapsedTimer.Elapsed;
            label1.Text = string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        }


    }
}
