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
    public partial class New_Meeting_Details : Form
    {
        private SQLiteConnection DBconnect;
        private SQLiteCommand DBcommand;
        //private SQLiteDataReader DBreader;
        public static DialogResult dlgResult;
        ErrorProvider errorProvider1; //Error Indicator
        public static List<string> meetingDetails;



        public New_Meeting_Details()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source = MeetingMgtDB.db; Compress= true;");
            btnAdd.DialogResult = DialogResult.OK;
            dlgResult = DialogResult.No;
            btnCancel.DialogResult = DialogResult.Cancel;

            //Setting properties for the error specifier i.e errorProvider1
            errorProvider1 = new ErrorProvider();
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            errorProvider1.SetIconAlignment(label9, ErrorIconAlignment.MiddleLeft);
            meetingDetails = new List<String>(8);

            var textboxes = new[] { txtAgenda,txtMeetingGrp,txtMinuteTaker,txtVenue };
            for (int i = 0; i <= 3; i++)
            {
                //textboxes[i].Validated += Tx_Validated;
                //textboxes[i].Validating += New_Meeting_Details_Validating;
                textboxes[i].TextChanged += New_Meeting_Details_TextChanged;
            }

            var comboBoxes = new[] {comboMeetingKind,comboMeetingType };
            for (int i = 0; i < 1; i++)
                comboBoxes[i].TextChanged += comboBox_TextChanged;
        }

        private void comboBox_TextChanged(object sender, EventArgs e)
        {
            ComboBox txtBox = (ComboBox)sender;
            if (noEmptyField())
            {
                errorProvider1.SetError(label9, string.Empty);
                label9.Text = "";
                btnAdd.Enabled = true;
            }
            else
            {
                errorProvider1.SetError(label9, "Field required!");
                label9.Text = "* -Fill Required Fields";
                btnAdd.Enabled = false;
            }
        }

        private void New_Meeting_Details_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            if (noEmptyField())
            {
                errorProvider1.SetError(label9, string.Empty);
                label9.Text = "";
                btnAdd.Enabled = true;
            }
            else
            {
                errorProvider1.SetError(label9, "Field required!");
                label9.Text = "* -Fill Required Fields";
                btnAdd.Enabled = false;
            }
        }

        //Method that checks if any field is blank and return a boolean value appropriately
        public bool noEmptyField()
        {
            if (!String.IsNullOrWhiteSpace(txtAgenda.Text) && !String.IsNullOrWhiteSpace(comboMeetingKind.Text) &&
                 !String.IsNullOrWhiteSpace(txtMeetingGrp.Text) && !String.IsNullOrWhiteSpace(txtMinuteTaker.Text) &&
                 !String.IsNullOrWhiteSpace(txtVenue.Text) && !String.IsNullOrWhiteSpace(comboMeetingType.Text) )
            {
                return true;
            }
            return false;
        }

        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (noEmptyField())
            {
                meetingDetails.Add(txtMeetingGrp.Text);
                meetingDetails.Add(comboMeetingKind.Text);
                meetingDetails.Add(dateTimePicker1.Text);
                meetingDetails.Add(txtMinuteTaker.Text);
                meetingDetails.Add(txtAgenda.Text);
                meetingDetails.Add(comboMeetingType.Text);
                meetingDetails.Add(txtVenue.Text);
                meetingDetails.Add(dateTimePicker2.Text);

                try
                {
                    if (DBconnect.State.Equals(ConnectionState.Closed))
                        DBconnect.Open();
                    string query = @"INSERT INTO MeetingTable(Date,Time,MeetingGroup,MeetingKind,MinuteTaker,
                MeetingAgenda,Venue,MeetingType,MeetingName) VALUES('" + dateTimePicker1.Value.ToShortDateString() + "','" +
                        dateTimePicker2.Value.ToShortTimeString() + "','" + txtMeetingGrp.Text + "','" + comboMeetingKind.Text
                        + "','" + txtMinuteTaker.Text + "','" + txtAgenda.Text + "','" + txtVenue.Text + "','" +
                        comboMeetingType.Text + "','" + "Minute_" + DateTime.Now.ToShortDateString() + "')";
                    DBcommand = new SQLiteCommand(query, DBconnect);
                    DBcommand.ExecuteNonQuery();
                    DBconnect.Close();

                    dlgResult = DialogResult.No;
                }

                catch (Exception ex)
                {
                    dlgResult = MessageBox.Show("Today\'s meeting has been already added!",
                        "Duplicated Meeting Notice...", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                this.Close();
            }
            //Minute_Editor editor = new Minute_Editor();
            //editor.Show();
        }

        private void New_Meeting_Details_Load(object sender, EventArgs e)
        {
            comboMeetingKind.SelectedIndex = 0;
            comboMeetingType.SelectedIndex = 0;
        }
    }
}
