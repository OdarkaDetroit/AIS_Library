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

            emailParam = textBox1.Text;
           

            string password = textBox2.Text;

            //DataTable table = new DataTable();

            MySqlCommand sqlCom1 = new MySqlCommand($"SELECT * FROM reader WHERE email = '{emailParam}' AND password = '{password}'", db.getConnection());

            //sqlCom1.Parameters.Add("@eml", MySqlDbType.VarChar).Value = email;
            //sqlCom1.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;

            MySqlDataReader reader = sqlCom1.ExecuteReader();

            if (!reader.HasRows)
            {
                MessageBox.Show("Не існує такого користувача!");
            }
            else
            {
                DBConnection ndb = new DBConnection();
                ndb.openConnection();
                MySqlCommand userCom = new MySqlCommand($"Select id_reader From reader Where email = '{emailParam}'", ndb.getConnection());
                userId = (int)userCom.ExecuteScalar();

                MySqlCommand userC = new MySqlCommand($"Select sec_name From reader Where email = '{emailParam}'", ndb.getConnection());
                userSecN = (string)userC.ExecuteScalar();
                ndb.closeConnection();

                MessageBox.Show("Успіх!");
                while (reader.Read())
                {
                    emailParam = reader.GetString(10);
                    ///accessParam = reader.GetString(12);
                    //if(accessParam == "User")
                    //{
                        _ = new UserMain { Visible = true };
                        Visible = false;
                    //}
                    //else if (accessParam == "Librarian")
                    //{
                    //    _ = new WorkerMain { Visible = true };
                    //    Visible = false;
                    //}
                    //else if (accessParam == "Administrator")
                    //{
                    //    _ = new AdminMain { Visible = true };
                    //    Visible = false;
                    //}
                }
            };

        
                //MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM 'reader' WHERE 'email' = @eml AND 'password' = @pass", db.getConnection());
                //sqlCom1.Parameters.Add("@eml", MySqlDbType.VarChar).Value = email;
                //sqlCom1.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
                //adapter.Fill(table);

                //if (table.Rows.Count > 0)
                //{
                //    MessageBox.Show("Успіх!");
                //}
                //else
                //{
                //    MessageBox.Show("Такий користувач не існує");
                //}
                db.closeConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = new WorkSignIn { Visible = true };
            Visible = false;
        }
    }
   
}
