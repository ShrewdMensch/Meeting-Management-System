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
    public partial class MeetingTakerMgt : Form
    {
        private SQLiteConnection DBconnect;
        private SQLiteCommand DBcommand;
        private SQLiteDataReader DBreader;

        public MeetingTakerMgt()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db;");
        }

        private void MeetingTakerMgt_Load(object sender, EventArgs e)
        {
            loadDataFromDB();
            comboUsername.SelectedIndex = 0;
        }

        private void loadDataFromDB()
        {
            comboUsername.Items.Clear();
            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();

                DBcommand = new SQLiteCommand("SELECT * FROM UsersTable", DBconnect);
                DBreader = DBcommand.ExecuteReader();

                while (DBreader.Read())
                {
                    comboUsername.Items.Add(DBreader["UserName"].ToString());
                }

                DBconnect.Close();
            }
            catch(Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboUsername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();

                DBcommand = new SQLiteCommand(@"SELECT * FROM UsersTable WHERE UserName='" + comboUsername.Text +
                    "'", DBconnect);
                DBreader = DBcommand.ExecuteReader();

                while (DBreader.Read())
                {
                    comboAccess.Text = DBreader["Access"].ToString();
                    txtPassword.Text = DBreader["Password"].ToString();
                    txtConfirmPass.Text = DBreader["Password"].ToString();
                }

                DBconnect.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string query = null;

            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();

                if (txtPassword.Text == txtConfirmPass.Text && noEmptyField() )
                {

                    query = @"UPDATE UsersTable SET Password='"+txtConfirmPass.Text +"', Access='" + 
                        comboAccess.Text + "' WHERE UserName='" + comboUsername.Text + "';";

                    DBcommand = new SQLiteCommand(query, DBconnect);
                    DBcommand.ExecuteNonQuery();
                    MessageBox.Show("Data Update Successful");
                }

                else
                    MessageBox.Show("Password Mismatch/Blank Field(s)!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                DBconnect.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"\n"+query);
            }
        }

        //Method that checks if any field is blank and return a boolean value appropriately
        private bool noEmptyField()
        {
            if (!String.IsNullOrWhiteSpace(txtConfirmPass.Text) && !String.IsNullOrWhiteSpace(txtPassword.Text) &&
                 !String.IsNullOrWhiteSpace(comboUsername.Text) && !String.IsNullOrWhiteSpace(comboAccess.Text))
            {
                return true;
            }
            return false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();

                string query = @"DELETE FROM UsersTable WHERE UserName='" + comboUsername.Text + "' ";

                DBcommand = new SQLiteCommand(query, DBconnect);
                DBcommand.ExecuteNonQuery();
                MessageBox.Show("User "+comboUsername.Text+ "Successfully removed!");
            loadDataFromDB();
            comboUsername.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addNewUser user = new addNewUser();
            user.ShowDialog(this);
            loadDataFromDB();
        }
    }
}
