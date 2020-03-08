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
    public partial class Members : Form
    {
        private SQLiteConnection DBConnect;
        private SQLiteCommand DBcommand;
        private SQLiteDataReader DBReader;

        public Members()
        {
            InitializeComponent();
            DBConnect = new SQLiteConnection("Data Source= MeetingMgtDB.db;");
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

      

        private void Members_Load(object sender, EventArgs e)
        {
            loadDataFromDBIntoDataGridView();

                newMemberToolStripMenuItem.Enabled = btnDelete.Enabled= btnUpdate.Enabled=
                    LogIn.accessLevel == "Full";
                
        }

        private void loadDataFromDBIntoDataGridView()
        {
            try
            {
                //Check the state of the Database connection and Open when appropriate
                if (DBConnect.State.Equals(ConnectionState.Closed))
                    DBConnect.Open();
                //string query = @"SELECT StaffID AS 'Staff ID',Title,FirstName AS 'First Name',
                //LastName AS 'Last Name',Department,Sex,Email,PhoneNumber FROM MembersTable 
                //ORDER BY FullName ASC";

                string query = @"SELECT StaffID AS 'Staff ID',FullName As 'Full Name',Department,Sex,Email,
                PhoneNumber FROM MembersTable ORDER BY FullName ASC";
                DBcommand = new SQLiteCommand(query, DBConnect);
                SQLiteDataAdapter da = new SQLiteDataAdapter(DBcommand);
                DataSet dt = new DataSet();
                da.Fill(dt);
                dataGridView1.DataSource = dt.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DBConnect.Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //Hide and unhide Controls based on checkbox1 state
            comboTitle.Visible = txtFirstName.Visible = txtLastName.Visible = txtStaffID.Visible =
                comboDprt.Visible = txtPhone.Visible = txtEmail.Visible = comboSex.Visible =
            label1.Visible = label2.Visible = label3.Visible = label4.Visible = label5.Visible =
                label6.Visible = label7.Visible = label8.Visible = btnUpdate.Visible = btnDelete.Visible = checkBox1.Checked;
            tableLayoutPanel1.Visible = checkBox1.Checked;
            groupBox1.Visible = checkBox1.Checked;
        }

        private void newMemberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            New_Member mn = new New_Member();
            //mn.MdiParent = this;
            mn.ShowDialog(this);
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                if (DBConnect.State == ConnectionState.Closed)
                    DBConnect.Open();

                DataGridViewCell cell = null;
                DataGridViewRow row = null;
                foreach (DataGridViewCell selectedCell in dataGridView1.SelectedCells)
                {
                    cell = selectedCell;
                    break;
                }
                if (cell != null)
                {
                    row = cell.OwningRow;
                }
                
                string query = @"SELECT * FROM MembersTable WHERE StaffID ='" +
                    row.Cells[0].Value.ToString()+ "' ";

                DBcommand = new SQLiteCommand(query, DBConnect);
                DBReader = DBcommand.ExecuteReader();

                txtEmail.Text = txtFirstName.Text = txtLastName.Text= comboTitle.Text = txtPhone.Text = 
                    txtStaffID.Text = comboDprt.Text = comboSex.Text = null;
               
                    while (DBReader.Read())
                {
                    txtStaffID.Text = DBReader["StaffID"].ToString();
                    comboTitle.Text = DBReader["Title"].ToString();
                    txtFirstName.Text = DBReader["FirstName"].ToString();
                    txtLastName.Text = DBReader["LastName"].ToString();
                    comboDprt.Text = DBReader["Department"].ToString();
                    comboSex.Text = DBReader["Sex"].ToString();
                    txtEmail.Text = DBReader["Email"].ToString();
                    txtPhone.Text = DBReader["PhoneNumber"].ToString();
                }

                this.Text = "Members |    " + txtFirstName.Text + " " + txtLastName.Text;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadDataFromDBIntoDataGridView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string fullName = comboTitle.Text + " " + txtFirstName.Text + " " + txtLastName.Text;
            try
            {
                if (DBConnect.State == ConnectionState.Closed)
                    DBConnect.Open();
                string query = @"UPDATE MembersTable SET Title= '"+comboTitle.Text+"', FirstName= '"+txtFirstName.Text+
                    "', LastName='"+txtLastName.Text+"', StaffID= '"+txtStaffID.Text+"',Department='"+comboDprt.Text+
                    "' ,Sex= '"+comboSex.Text+"',PhoneNumber= '"+txtPhone.Text+"',Email='"+txtEmail.Text+
                    "',FullName='"+fullName+"' WHERE StaffID='"+
                    dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value.ToString() +"'";

                DBcommand = new SQLiteCommand(query, DBConnect);
                DBcommand.ExecuteNonQuery();
                MessageBox.Show(this, "Data updated successfully!", "Updating...", MessageBoxButtons.OK,
                   MessageBoxIcon.Information);

                loadDataFromDBIntoDataGridView(); //Refresh dataGridView1 to reflect editted data
            }

            catch(Exception ex )
            {
                MessageBox.Show("Conflicting Data");
            }
            DBConnect.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string fullName = comboTitle.Text + " " + txtFirstName.Text + " " + txtLastName.Text;
            try
            {
                if (DBConnect.State == ConnectionState.Closed)
                    DBConnect.Open();
                string query = @"DELETE FROM MembersTable WHERE FullName='" + fullName + "'";

                DialogResult dlg = MessageBox.Show("Did you really want to delete this profile?",
                    "Warning...",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                if (dlg == DialogResult.Yes)
                {
                    DBcommand = new SQLiteCommand(query, DBConnect);
                    DBcommand.ExecuteNonQuery();
                    MessageBox.Show(this, "Profile deleted successfully!", "Notice!", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                    loadDataFromDBIntoDataGridView(); //Refresh dataGridView1 to reflect editted data
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error deleting member's profile");
            }
            DBConnect.Close();
        }
    }
}
