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
using System.Speech.Recognition;
using ClassLibrary1.RichTextBoxPrintCtrl;
using System.Speech.AudioFormat;
using SautinSoft;

namespace ProjectPratice
{
    public partial class Minute_Editor : Form
    {
        Font currentFont = null;
        FontStyle currentFontStyle;
        string minuteDate = null;
        private int checkPrint; //variable that tells if there are more printable pages 

        //Declaration of variables for DB accessing
        private SQLiteCommand DBcommand;
        private SQLiteConnection DBconnect;
        private SQLiteDataReader DBreader;

        internal static string minuteName;
        internal static bool saved = true; //Variable that tells if changes av been saved or not

        public Minute_Editor()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db;Compress= true;");
            minuteName= "Minute_" + DateTime.Now.ToShortDateString();
            saved = true;
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //byte[] file = null;
            //MinuteArchive minuteArchive = new MinuteArchive();
            //minuteArchive.retrievingMinuteDetails += MinuteArchive_retrievingMinuteDetails;
            //minuteArchive.ShowDialog(this);
            //if ( minuteArchive.DialogResult==DialogResult.OK)
            //{
            //    try
            //    {
            //        if (DBconnect.State == ConnectionState.Closed)
            //            DBconnect.Open();
            //        DBcommand = new SQLiteCommand("SELECT * FROM MeetingTable WHERE MeetingName='" +
            //            minuteName + "'", DBconnect);
            //        DBreader = DBcommand.ExecuteReader();

            //        while (DBreader.Read())
            //        {
            //            file = (byte[])DBreader["Minute"];
            //        }
            //        richTextBoxPrintCtrl1.Rtf = new UTF8Encoding(true).GetString(file);
            //    }

            //    catch(Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //    DBconnect.Close();
            //}
        }

        private void MinuteArchive_retrievingMinuteDetails(object sender, fetchDataEvent e)
        {
            this.Text = e.MinuteName;
            minuteDate = e.MinuteDate;
            getMinutesFromDB(e.MinuteName);
        }


        private void getMinutesFromDB(string name)
        {
            byte[] file = null;

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
                richTextBoxPrintCtrl1.Rtf = new UTF8Encoding(true).GetString(file);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Minute not found",
                   "Error Opening...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            DBconnect.Close();
        }


        private void boldToolStrip_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBoxPrintCtrl1.SelectionLength > 0)
                {
                    int startPos = richTextBoxPrintCtrl1.SelectionStart;
                    int selectionLength = richTextBoxPrintCtrl1.SelectionLength;

                    using (RichTextBox txtTemp = new RichTextBox())
                    {
                        txtTemp.Rtf = richTextBoxPrintCtrl1.SelectedRtf;
                        for(int i= 0; i< selectionLength; i++)
                        {
                            txtTemp.Select(i, 1);
                            txtTemp.SelectionFont = new Font(txtTemp.SelectionFont,
                            txtTemp.SelectionFont.Style ^ FontStyle.Bold);
                        }
                        txtTemp.Select(0, selectionLength);
                        richTextBoxPrintCtrl1.SelectedRtf = txtTemp.SelectedRtf;
                        richTextBoxPrintCtrl1.Select(startPos, selectionLength);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBoxPrintCtrl1.SelectionLength > 0)
                {
                    int startPos = richTextBoxPrintCtrl1.SelectionStart;
                    int selectionLength = richTextBoxPrintCtrl1.SelectionLength;

                    using (RichTextBox txtTemp = new RichTextBox())
                    {
                        txtTemp.Rtf = richTextBoxPrintCtrl1.SelectedRtf;
                        for (int i = 0; i < selectionLength; i++)
                        {
                            txtTemp.Select(i, 1);
                            txtTemp.SelectionFont = new Font(txtTemp.SelectionFont,
                            txtTemp.SelectionFont.Style ^ FontStyle.Italic);
                        }
                        txtTemp.Select(0, selectionLength);
                        richTextBoxPrintCtrl1.SelectedRtf = txtTemp.SelectedRtf;
                        richTextBoxPrintCtrl1.Select(startPos, selectionLength);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBoxPrintCtrl1.SelectionLength > 0)
                {
                    int startPos = richTextBoxPrintCtrl1.SelectionStart;
                    int selectionLength = richTextBoxPrintCtrl1.SelectionLength;

                    using (RichTextBox txtTemp = new RichTextBox())
                    {
                        txtTemp.Rtf = richTextBoxPrintCtrl1.SelectedRtf;
                        for (int i = 0; i < selectionLength; i++)
                        {
                            txtTemp.Select(i, 1);
                            txtTemp.SelectionFont = new Font(txtTemp.SelectionFont,
                            txtTemp.SelectionFont.Style ^ FontStyle.Underline);
                        }
                        txtTemp.Select(0, selectionLength);
                        richTextBoxPrintCtrl1.SelectedRtf = txtTemp.SelectedRtf;
                        richTextBoxPrintCtrl1.Select(startPos, selectionLength);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBoxPrintCtrl1.SelectionLength > 0)
                {
                    int startPos = richTextBoxPrintCtrl1.SelectionStart;
                    int selectionLength = richTextBoxPrintCtrl1.SelectionLength;

                    using (RichTextBox txtTemp = new RichTextBox())
                    {
                        txtTemp.Rtf = richTextBoxPrintCtrl1.SelectedRtf;
                        for (int i = 0; i < selectionLength; i++)
                        {
                            txtTemp.Select(i, 1);
                            txtTemp.SelectionFont = new Font(txtTemp.SelectionFont,
                            txtTemp.SelectionFont.Style ^ FontStyle.Strikeout);
                        }
                        txtTemp.Select(0, selectionLength);
                        richTextBoxPrintCtrl1.SelectedRtf = txtTemp.SelectedRtf;
                        richTextBoxPrintCtrl1.Select(startPos, selectionLength);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void richTextBoxPrintCtrl1_TextChanged(object sender, EventArgs e)
        {
            saved = false;
        }

       

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = null;
            MemoryStream ms = new MemoryStream();
            Cursor.Current = Cursors.WaitCursor;
            richTextBoxPrintCtrl1.SaveFile(ms, RichTextBoxStreamType.RichText);
            byte[] file = ms.ToArray();
                query = @"UPDATE MeetingTable SET  Minute= @file WHERE MeetingName='" +minuteName + "' ";

            //Save minute into temp folder on disk
            String destination = Path.Combine(@"temp\", minuteName+".rtf");


            if (!Directory.Exists(@"temp\"))            Directory.CreateDirectory(@"temp\");
               
            //Save .rtf to the created dir
            richTextBoxPrintCtrl1.SaveFile(destination);

            //Convert to pdf
            PdfMetamorphosis p = new PdfMetamorphosis();
            string pdfPath = Path.ChangeExtension(destination, ".pdf");
            int i = p.RtfToPdfConvertFile(destination, pdfPath);

            if (i != 0) MessageBox.Show("An error occured during converting RTF to PDF!", "Error");

            //File.Delete(destination);
            try
            {
                if (DBconnect.State.Equals(ConnectionState.Closed))
                    DBconnect.Open();
                DBcommand = new SQLiteCommand(query, DBconnect);
                DBcommand.Parameters.Add("@file", DbType.Binary, file.Length).Value = file;
                DBcommand.ExecuteNonQuery();

                Cursor.Current = Cursors.Arrow;

                MessageBox.Show(this, "Minute updated succesfully", "Saving...", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                vacuumDatabase();
                saved = true;
            }
                
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DBconnect.Close();
        }
    


        //Event for Zooming in 
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            if (richTextBoxPrintCtrl1.ZoomFactor<= 10)
            richTextBoxPrintCtrl1.ZoomFactor+= 0.1F;
        }

        //Event for Zooming out
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            if (richTextBoxPrintCtrl1.ZoomFactor > 1.1)
                richTextBoxPrintCtrl1.ZoomFactor-=0.1F;
        }

        //Event that triggers printing
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog(this) == DialogResult.OK)
                printDocument1.Print();
        }

        //Event that triggers printPreviewing
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                printPreviewDialog1.ClientSize = this.Size;
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Event that triggers when currently printing
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Store the last character printed
            checkPrint = richTextBoxPrintCtrl1.Print(checkPrint, richTextBoxPrintCtrl1.TextLength, e);

            //check if there are more pages to be printed
            if (checkPrint < richTextBoxPrintCtrl1.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        //Event that triggers when printing just started
        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            checkPrint = 0;
        }

        //Event that triggers printing page setup
        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Text = null;
        }

        private void Minute_Editor_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        ///     Change the richtextbox style for the current selection
        /// </summary>
        public void ChangeFontStyle(FontStyle style, bool add)
        {
            //This method should handle cases that occur when multiple fonts/styles are selected
            // Parameters:-
            //  style - eg FontStyle.Bold
            //  add - IF true then add else remove

            // throw error if style isn't: bold, italic, strikeout or underline
            if (style != FontStyle.Bold
                && style != FontStyle.Italic
                && style != FontStyle.Strikeout
                && style != FontStyle.Underline)
                throw new System.InvalidProgramException("Invalid style parameter to ChangeFontStyle");

            int rtb1start = richTextBoxPrintCtrl1.SelectionStart;
            int len = richTextBoxPrintCtrl1.SelectionLength;
            int rtbTempStart = 0;

            //if len <= 1 and there is a selection font then just handle and return
            if (len <= 1 && richTextBoxPrintCtrl1.SelectionFont != null)
            {
                //add or remove style 
                if (add)
                    richTextBoxPrintCtrl1.SelectionFont = new Font(richTextBoxPrintCtrl1.SelectionFont,
                        richTextBoxPrintCtrl1.SelectionFont.Style | style);
                else
                    richTextBoxPrintCtrl1.SelectionFont = new Font(richTextBoxPrintCtrl1.SelectionFont,
                        richTextBoxPrintCtrl1.SelectionFont.Style & ~style);

                return;
            }

            using (RichTextBoxPrintCtrl temp = new RichTextBoxPrintCtrl())
            {
                //Step through the selected text on char ar a time
                temp.Rtf = richTextBoxPrintCtrl1.SelectedRtf;
                for (int i = 0; i < len; ++i)
                {
                    temp.Select(rtbTempStart + i, 1);

                    //add or remove style
                    if (add)
                        temp.SelectionFont = new Font(temp.SelectionFont, temp.SelectionFont.Style | style);
                    else
                        temp.SelectionFont = new Font(temp.SelectionFont, temp.SelectionFont.Style & ~style);
                }

                //replace & select 
                temp.Select(rtbTempStart, len);
                richTextBoxPrintCtrl1.SelectedRtf = temp.SelectedRtf;
            }
            return;

        }

        //Methods that Resize (Vacuum) Database i.e remove excessive allocated space
        private void vacuumDatabase()
        {
            if (DBconnect.State.Equals(ConnectionState.Closed))
                DBconnect.Open();

            DBcommand = new SQLiteCommand("VACUUM;", DBconnect);
            DBcommand.ExecuteNonQuery();

            DBconnect.Close();
        }

        private void addListOfAttendeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string attendees = null;
            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();
                string query = @"SELECT Attendance FROM MeetingTable WHERE Date='"+
                    DateTime.Now.ToShortDateString() + "' ";
                DBcommand = new SQLiteCommand(query, DBconnect);
                DBreader = DBcommand.ExecuteReader();

                while(DBreader.Read())
                {
                    attendees = DBreader[0].ToString();
                }
                richTextBoxPrintCtrl1.SelectedText = "Attendees:\n"+attendees;
                richTextBoxPrintCtrl1.SelectionAlignment = HorizontalAlignment.Left;
            }

            catch(Exception ex)
            {

            }

            DBconnect.Close();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Copy();
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Paste();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if(richTextBoxPrintCtrl1.CanUndo)
            richTextBoxPrintCtrl1.Undo();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (richTextBoxPrintCtrl1.CanRedo)
            richTextBoxPrintCtrl1.Redo();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            ColorDialog fd = new ColorDialog();
            if (fd.ShowDialog() == DialogResult.OK)
                richTextBoxPrintCtrl1.SelectionColor = fd.Color;

        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            ColorDialog fd = new ColorDialog();
            fd.Color = richTextBoxPrintCtrl1.ForeColor;
            if (fd.ShowDialog() == DialogResult.OK)
                richTextBoxPrintCtrl1.SelectionBackColor = fd.Color;
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Cut();
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem.PerformClick();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Paste();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Cut();
        }

        private void textSizeComboBox_TextChanged(object sender, EventArgs e)
        {
        //    if ( textSizeComboBox.Text != null ) 
        //    richTextBoxPrintCtrl1.SelectionFont = new Font(richTextBoxPrintCtrl1.SelectionFont.FontFamily, Convert.ToInt16(textSizeComboBox.Text),richTextBoxPrintCtrl1.Font.Style);
        }

        private void Minute_Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saved)
            {
                if (MessageBox.Show("Are you sure you want to close without saving changes made so far",
                    "Notice!", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    e.Cancel = true;
                else return;
           }

            return;
        }

        private void addAddendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string attendees = null;
            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();
                string query = @"SELECT Attendance FROM MeetingTable WHERE Date='" +
                    DateTime.Now.ToShortDateString() + "' ";
                DBcommand = new SQLiteCommand(query, DBconnect);
                DBreader = DBcommand.ExecuteReader();

                while (DBreader.Read())
                {
                    attendees = DBreader[0].ToString();
                }
                richTextBoxPrintCtrl1.SelectedText = "Attendees:\n" + attendees;
                richTextBoxPrintCtrl1.SelectionAlignment = HorizontalAlignment.Left;
            }

            catch (Exception ex)
            {

            }

            DBconnect.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton8.PerformClick();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton9.PerformClick();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (richTextBoxPrintCtrl1.SelectionFont != null)
                {
                    fontDialog1.Font = richTextBoxPrintCtrl1.SelectionFont;
                }

                else
                    fontDialog1.Font = null;
                fontDialog1.ShowApply = true;
                if(fontDialog1.ShowDialog()==DialogResult.OK)
                {
                    richTextBoxPrintCtrl1.SelectionFont = fontDialog1.Font;
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void addBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.BulletIndent = 10;
            richTextBoxPrintCtrl1.SelectionBullet = true;
        }

        private void removeBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionBullet = false;
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionIndent = 0;
        }

        private void ptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionIndent = 5;
        }

        private void ptsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionIndent = 10;
        }

        private void ptsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionIndent = 15;
        }

        private void ptsToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.SelectionIndent = 20;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Focus();
            richTextBoxPrintCtrl1.SelectAll();
        }

        private void insertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofg = new OpenFileDialog();
            ofg.Title = "Insert Image";
            ofg.DefaultExt = "rtf";
            ofg.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif|PNG Files|*.png";
            ofg.FilterIndex = 1;
            ofg.ShowDialog();

            if (ofg.FileName == "")
                return;

            try
            {
                string imagePath = ofg.FileName;
                Image img;
                img = Image.FromFile(imagePath);
                Clipboard.SetDataObject(img);
                DataFormats.Format df= DataFormats.GetFormat(DataFormats.Bitmap);

                if(this.richTextBoxPrintCtrl1.CanPaste(df))
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this.richTextBoxPrintCtrl1.Paste(df);
                }
            }

            catch
            {
                MessageBox.Show("Unable to insert image format selected.","RTE-Paste", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor.Current = Cursors.WaitCursor;
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxPrintCtrl1.Focus();
            findDialog fd = new findDialog(this);
            fd.ShowDialog();
        }

        private void speechtotextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            WavetoTextMenu waveMenuToTxt = new WavetoTextMenu();
            waveMenuToTxt.ShowDialog();
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            //create and load a dictionary
            recognizer.LoadGrammar(new DictationGrammar());

            if (WavetoTextMenu.getWaveMemoryStream() != null)
            {
                Cursor.Current = Cursors.WaitCursor;

                recognizer.SetInputToWaveStream(WavetoTextMenu.getWaveMemoryStream());
                recognizer.BabbleTimeout = new TimeSpan(Int32.MaxValue);
                recognizer.InitialSilenceTimeout = new TimeSpan(Int32.MaxValue);
                recognizer.EndSilenceTimeout = new TimeSpan(100000000);
                recognizer.EndSilenceTimeoutAmbiguous = new TimeSpan(100000000);

                StringBuilder sb = new StringBuilder();
                while (true)
                {
                    try
                    {
                        var recText = recognizer.Recognize();
                        if (recText == null)
                        {
                            break;
                        }

                        sb.Append(recText.Text);
                    }

                    catch (Exception ex)
                    {
                        //handle exception      
                        //...

                        break;
                    }
                }
                richTextBoxPrintCtrl1.Text += sb.ToString();
            }

            
                Cursor.Current = Cursors.Arrow;

        }
        

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

