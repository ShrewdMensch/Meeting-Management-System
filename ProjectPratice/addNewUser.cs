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
    public partial class addNewUser : Form
    {
        private SQLiteConnection DBconnect;
        private SQLiteCommand DBcommand;
           
        public addNewUser()
        {
            InitializeComponent();
            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db");
        }

        //Method that clear all input fields
        private void clearAllFields()
        {
            txtUserName.Text = txtPassword.Text = txtConfirmPass.Text = comboAccess.Text = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
                try
                {
                    if (DBconnect.State == ConnectionState.Closed)
                        DBconnect.Open();

                if (txtPassword.Text == txtConfirmPass.Text && noEmptyField())
                {
                    string query = "INSERT INTO UsersTable(UserName,Password,Access) VALUES('" +
                        txtUserName.Text + "','" + txtPassword.Text + "','" + comboAccess.Text + "')";

                    DBcommand = new SQLiteCommand(query, DBconnect);
                    DBcommand.ExecuteNonQuery();
                    MessageBox.Show("User " + txtUserName.Text + " Added Successfully!");

                    clearAllFields();
                }

                else
                    MessageBox.Show("Password Mismatch/Blank Field(s)!", "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error...");
                }
            
        }

        //Method that checks if any field is blank and return a boolean value appropriately
        private bool noEmptyField()
        {
            if (!String.IsNullOrWhiteSpace(txtConfirmPass.Text) && !String.IsNullOrWhiteSpace(txtPassword.Text) &&
                 !String.IsNullOrWhiteSpace(txtUserName.Text) && !String.IsNullOrWhiteSpace(comboAccess.Text) )
            {
                return true;
            }
            return false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAllFields();
        }
    }
}
