using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace ProjectPratice
{
    public partial class LogIn : Form
    {
        private SQLiteConnection DBconnect;
        private SQLiteCommand DBcommand;
        private SQLiteDataReader DBreader;
        public static string accessLevel; //Variable for logged on user's access level

        public LogIn()
        {
            Thread t = new Thread(new ThreadStart(SplashStart));
            t.Start();
            Thread.Sleep(15000);
            t.Abort();
            InitializeComponent();

            //Check if the DB already exist, if yes, uses existing DB
            if (File.Exists("MeetingMgtDB.db"))
                DBconnect = new SQLiteConnection("Data Source=MeetingMgtDB.db;Version= 3;");

            //If DB doesn't exist, create a new one to handle DBConnection exceptions
            else
            {
                DBconnect = new SQLiteConnection("Data Source=MeetingMgtDB.db;Version= 3; New= True; Compress= True;");
                
            }
        }

        public LogIn(string comment)
        {
            InitializeComponent();
            //Check if the DB already exist, if yes, uses existing DB
            if (File.Exists("MeetingMgtDB.db"))
                DBconnect = new SQLiteConnection("Data Source=MeetingMgtDB.db;Version= 3;");

            //If DB doesn't exist, create a new one to handle DBConnection exceptions
            else
            {
                DBconnect = new SQLiteConnection("Data Source=MeetingMgtDB.db;Version= 3; New= True; Compress= True;");

            }
        }

        public void SplashStart()
        {
            Application.Run(new SplashScreen());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;

            if (DBconnect.State == ConnectionState.Closed)
                DBconnect.Open();
            DBcommand = new SQLiteCommand("SELECT * FROM UsersTable WHERE UserName= '"+
                txtUserName.Text+"' AND Password='"+txtPassword.Text+"';", DBconnect);
            DBreader = DBcommand.ExecuteReader();

            while (DBreader.Read())
            {
                count++;
                accessLevel = DBreader["Access"].ToString(); //get the access level of a valid authentication

            }

            if (count > 0)
            {
                MDI_Menu menu = new MDI_Menu();
                menu.Show(this);
                //menu.label1.Text = "Welcome: " + txtUserName.Text;
               
                this.Hide();
            }

            else
                MessageBox.Show("Invalid login Credentials!","Error");
        }

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            try
            {
                if (DBconnect.State.Equals(ConnectionState.Closed))
                    DBconnect.Open();
                DBcommand  = DBconnect.CreateCommand();
                DBcommand.CommandType = CommandType.Text;

                //Check if  Tables exists in Database
                DBcommand.CommandText = "SELECT name FROM sqlite_master WHERE name= 'MeetingTable'";
                var name = DBcommand.ExecuteScalar();

                //check if  MeetingTable exists
                if (name != null && name.ToString() == "MeetingTable")
                    return;
                else
                {
                    DBcommand.CommandText = @"CREATE TABLE MeetingTable (
    Date          VARCHAR PRIMARY KEY,
    MeetingName   STRING,
    Time          VARCHAR,
    MeetingGroup  VARCHAR,
    MeetingKind,
    MinuteTaker   VARCHAR,
    MeetingAgenda VARCHAR,
    Venue         VARCHAR,
    MeetingType   VARCHAR,
    Minute        BLOB,
    Audio         BLOB,
    Attendance    TEXT
)";
                    DBcommand.ExecuteNonQuery();
                }

                //Check if  MembersTable exists
                DBcommand.CommandText = "SELECT name FROM sqlite_master WHERE name= 'MembersTable'";
                name = DBcommand.ExecuteScalar();

                if (name != null && name.ToString() == "MembersTable")
                    return;
                else
                {
                    DBcommand.CommandText = @"CREATE TABLE MembersTable (
    Title       VARCHAR,
    FirstName   VARCHAR,
    LastName    VARCHAR,
    StaffID     VARCHAR PRIMARY KEY,
    Department  VARCHAR,
    Sex         VARCHAR,
    PhoneNumber VARCHAR,
    Email       VARCHAR,
    FullName    VARCHAR
)";
                    DBcommand.ExecuteNonQuery();
                }

                //Check if  UsersTable Table exists
                DBcommand.CommandText = "SELECT name FROM sqlite_master WHERE name= 'UsersTable'";
                name = DBcommand.ExecuteScalar();

                if (name != null && name.ToString() == "UsersTable")
                    return;
                else
                {
                    DBcommand.CommandText = @"CREATE TABLE UsersTable (
    UserName VARCHAR PRIMARY KEY,
    Password VARCHAR,
    Access   VARCHAR
)";
                    DBcommand.ExecuteNonQuery();
                    DBcommand.CommandText = @"INSERT INTO UsersTable (UserName, Password, Access)
VALUES('admin','1234','Full')";
                    DBcommand.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\n" + "\n" + ex.Message);
            }
        }
    }
}
