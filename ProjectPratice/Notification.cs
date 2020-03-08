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
using System.Net.Mail;
using System.Net;

namespace ProjectPratice
{
    public partial class Notification : Form
    {
        private SQLiteConnection DBconnect;
        private SQLiteCommand DBcommand;
        private SQLiteDataReader DBReader;

        public Notification()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db");
        }

        

        private void checkAdvance_CheckedChanged(object sender, EventArgs e)
        {
            tableLayoutPanel8.Visible = checkAdvance.Checked;
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            tableLayoutPanel8.Visible = false;
            loadEmailSettingsFromDB();
            getRecipients();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveToDB();
        }

        private void loadEmailSettingsFromDB()
        {
            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();

                string query = "SELECT * FROM EmailSettings";
                DBcommand = new SQLiteCommand(query, DBconnect);
                DBReader= DBcommand.ExecuteReader();

                while (DBReader.Read())
                {
                    txtUsername.Text = DBReader[0].ToString();
                    txtPassword.Text = DBReader.GetString(1);
                    txtSmtp.Text = DBReader.GetString(2);
                    txtPort.Text = DBReader.GetString(3);
                }

                DBReader.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DBconnect.Close();
        }

        private void saveToDB()
        {
            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();

                string query1 = "DELETE FROM EmailSettings";
                string query2 = @"INSERT INTO EmailSettings(Username,Password,Host,Port) VALUES('" +
                    txtUsername.Text + "','"+txtPassword.Text+"','"+txtSmtp.Text+"','"+txtPort.Text+ "')";

                DBcommand = new SQLiteCommand(query1, DBconnect);
                DBcommand.ExecuteNonQuery();

                DBcommand = new SQLiteCommand(query2, DBconnect);
                DBcommand.ExecuteNonQuery();
                MessageBox.Show("Settings Saved Succesfully!");
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DBconnect.Close();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            loadEmailSettingsFromDB();
        }

        private void btnAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            String path = System.IO.Path.GetDirectoryName("temp");
            ofd.InitialDirectory = path;
            ofd.RestoreDirectory = true;
            ofd.ShowDialog();

            if (ofd.FileName == "")
                return;
            txtAttachment.Visible = true;
            foreach(string fn in ofd.FileNames)
            txtAttachment.Text += fn+";";

        }

        private void getRecipients()
        {
            if (DBconnect.State == ConnectionState.Closed)
                DBconnect.Open();
            try
            {
                DBcommand = new SQLiteCommand("SELECT * FROM MembersTable;", DBconnect);
                DBReader = DBcommand.ExecuteReader();

                while (DBReader.Read())
                {
                    txtRecipients.Text += DBReader["Email"].ToString()+";";
                }
                DBReader.Close();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            DBconnect.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            MailMessage message = new MailMessage();
            message.From =new MailAddress (txtUsername.Text);
            message.Subject = txtSubject.Text;
            message.Body = txtBody.Text;
            System.Net.Mail.Attachment attachment;
            

            String[] recipients = txtRecipients.Text.Split(new string[] { ";", " " },
                StringSplitOptions.RemoveEmptyEntries);

            String[] attachedFiles = txtAttachment.Text.Split(new string[] { ";"},
               StringSplitOptions.RemoveEmptyEntries);

            foreach (string sh in recipients)
            {
                message.To.Add(sh);
            }

            foreach (string attached in attachedFiles)
            {
                attachment = new System.Net.Mail.Attachment(attached);
                message.Attachments.Add(attachment);
            }

            try
            {
                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential(txtUsername.Text, txtPassword.Text);
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Send(message);
                Cursor.Current = Cursors.Arrow;
            }

            catch(Exception ex)
            {
                MessageBox.Show("Invalid mail settings or check your internet connection","Error Message...");
                return;
            }
            string[] sk = null;
            string all = null;
            //sk = txtRecipients.Text.Split(new string[] { ";", " " }, StringSplitOptions.RemoveEmptyEntries);

            //foreach (string sh in sk)
            //    all += sh + "\n";
            MessageBox.Show("Mail sent successfully!");
        }
    }
}
