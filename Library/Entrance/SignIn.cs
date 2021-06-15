using Library.Admin;
using Library.Worker;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using static Library.PasswordManipulations;

namespace Library.Entrance
{
    public partial class SignIn : Form
    {


        //public enum AccessBD { "User", "Librarian", "Administrator" }
        ///public List<string> Roles { get; set; } = new List<string> { "User", "Librarian", "Administrator" };

        public static string emailParam;

        public static int userId;
        public static string userSecN;

        public SignIn()
        {
            InitializeComponent();
        }

        private void registrateButton_Click(object sender, EventArgs e)
        {
            _ = new SignUp { Visible = true };
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();

            // Сonnection is opened
            db.openConnection();

            // getting encoded to Base64 password from user
            emailParam = textBox1.Text;
            string password = EncodePasswordToBase64(textBox2.Text);


            MySqlCommand sqlCom1 = new MySqlCommand($"SELECT * FROM reader WHERE email = '{emailParam}' AND password = '{password}'", db.getConnection());


            MySqlDataReader reader = sqlCom1.ExecuteReader();

            if (!reader.HasRows)
            {
                MessageBox.Show("Не існує такого користувача!");
            }
            else
            {
                DBConnection ndb = new DBConnection();
                ndb.openConnection();
                MySqlCommand userCom = new MySqlCommand($"SELECT id_reader FROM reader WHERE email = '{emailParam}'", ndb.getConnection());
                userId = (int)userCom.ExecuteScalar();

                MySqlCommand userC = new MySqlCommand($"SELECT sec_name FROM reader WHERE email = '{emailParam}'", ndb.getConnection());
                userSecN = (string)userC.ExecuteScalar();
                ndb.closeConnection();

                MessageBox.Show("Успіх!");
                while (reader.Read())
                {
                    emailParam = reader.GetString(10);

                    _ = new UserMain { Visible = true };
                    Visible = false;

                }
            };
                db.closeConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = new WorkSignIn { Visible = true };
            Visible = false;
        }
    }

}
