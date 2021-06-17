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
    public partial class WorkSignIn : Form
    {
        public static string emailParam;
        public static int userId;
        public static string userSecN;
        private string accessParam;

        public WorkSignIn()
        {
            InitializeComponent();
        }

        public static int workerId;
        private void button1_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();

            db.openConnection();

            emailParam = textBox1.Text;
            string password = EncodePasswordToBase64(textBox2.Text);

            MySqlCommand sqlCom1 = new MySqlCommand($"SELECT * FROM worker WHERE email = '{emailParam}' AND password = '{password}'", db.getConnection());

            MySqlDataReader reader = sqlCom1.ExecuteReader();

            if (!reader.HasRows)
            {
                MessageBox.Show("Не існує такого користувача!");
            }
            else
            {

                DBConnection ndb = new DBConnection();
                ndb.openConnection();
                MySqlCommand userCom = new MySqlCommand($"SELECT id_worker FROM worker WHERE email = '{emailParam}'", ndb.getConnection());
                workerId = (int)userCom.ExecuteScalar();

                MySqlCommand userC = new MySqlCommand($"SELECT sec_name FROM worker WHERE email = '{emailParam}'", ndb.getConnection());
                userSecN = (string)userC.ExecuteScalar();
                ndb.closeConnection();

                MessageBox.Show("Успіх!");
                while (reader.Read())
                {
                    emailParam = reader.GetString(7);
                    accessParam = reader.GetString(4);

                    if (accessParam == "Librarian")
                    {
                        _ = new WorkerMain { Visible = true };
                        Visible = false;
                    }
                    else if (accessParam == "Admin")
                    {
                        _ = new AdminMain { Visible = true };
                        Visible = false;
                    }
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = new SignIn { Visible = true };
            Visible = false;
        }
    }
}
