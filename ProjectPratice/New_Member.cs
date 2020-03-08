using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace ProjectPratice
{
    public partial class New_Member : Form
    {
        ErrorProvider errorProvider1;
        bool fieldIsBlank = false;

        //DataBase access Variables
        private SQLiteConnection DBconnect;
        private SQLiteCommand DBcommand;
        

        public New_Member()
        {
            InitializeComponent();

            //Initialize basic properties of errorProvider1
            errorProvider1 = new ErrorProvider();
            errorProvider1.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
            errorProvider1.SetIconPadding(label9, 3);

            //Initialize DataBase connection i.e DBconnect
            DBconnect = new SQLiteConnection("Data Source= MeetingMgtDB.db;");
            txt_email.Validating += Txt_email_Validating;

        }

        private void Txt_email_Validating(object sender, CancelEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;

            
            if (isValidEmail(txtBox.Text))
            {
                errorProvider1.SetError(labelError, string.Empty);
                labelError.Text = "";

            }
            else
            {
                errorProvider1.SetError(labelError, "Invalid email entered");
                labelError.Text = "*-Email Field not valid!";
                e.Cancel = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label10.Visible = label11.Visible = txt_userName.Visible = txt_passWord.Visible = checkBox1.Checked;
        }

        //Form Load event
        private void TableLayoutForm_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        //btnSave click event
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (noEmptyField() && isValidEmail(txt_email.Text))
            {
                saveToDB();
                emptyEntryFields();
                return;
            }

            MessageBox.Show("Some Fields are Blank thus, Saving halted!");
        }

        //Function that determines if email address is valid or not
        public bool isValidEmail(string emailAddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailAddress);
                return true;
            }

            catch(Exception ex)
            {
                return false;
            }
        }

        private void saveToDB()
        {
            string query = null;
            try
            {
                if (DBconnect.State == ConnectionState.Closed)
                    DBconnect.Open();

                //Concatenate title,firstname and lastname to have fullName
                string fullName = comboTitle.Text+" "+txt_firstName.Text+" "+txt_lastName.Text;

                query = @"INSERT INTO MembersTable(Title,FirstName,LastName,StaffID,
    Department,Sex,PhoneNumber,Email,FullName) VALUES('"+comboTitle.Text+"','"+txt_firstName.Text+"','"
    +txt_lastName.Text+"','"+txtID.Text+"','"+comboDprt.Text+"','"+comboSex.Text+"','"+txt_phoneNo.Text
    +"','"+txt_email.Text+"','"+fullName+"');";

                //Execute query appropriately
                DBcommand = new SQLiteCommand(query, DBconnect);
                DBcommand.ExecuteNonQuery();

                if(checkBox1.Checked && txt_userName.Text!="" && txt_passWord.Text!="")
                {
                    query = @"INSERT INTO UsersTable(UserName,Password,Access) VALUES('"+txt_userName.Text+
                        "','"+txt_passWord.Text+"','Standard');";

                    //Execute query appropriately
                    DBcommand = new SQLiteCommand(query, DBconnect);
                    DBcommand.ExecuteNonQuery();
                }
                MessageBox.Show(this,"New Member added successfully!","Saving...",MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            DBconnect.Close();
        }
       
        //Method that clear All fields
        private void emptyEntryFields()
        {
            txtID.Text = comboTitle.Text = txt_email.Text = txt_firstName.Text = txt_lastName.Text = 
                txt_passWord.Text = txt_phoneNo.Text= txt_userName.Text= comboDprt.Text= comboSex.Text= null;
        }

        //Method that checks if any field is blank and return a boolean value appropriately
       private bool noEmptyField()
        {
           if(!String.IsNullOrWhiteSpace(txtID.Text) && !String.IsNullOrWhiteSpace(comboTitle.Text) && 
                !String.IsNullOrWhiteSpace(txt_email.Text) && !String.IsNullOrWhiteSpace(txt_firstName.Text) &&
                !String.IsNullOrWhiteSpace(txt_lastName.Text) && !String.IsNullOrWhiteSpace(txt_phoneNo.Text) &&
                !String.IsNullOrWhiteSpace(comboDprt.Text) && !String.IsNullOrWhiteSpace(comboSex.Text))
            {
                return true;
            }
            return false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            emptyEntryFields();
        }
    }
}
