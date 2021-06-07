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

            String email = textBox1.Text;
            String password = textBox2.Text;

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand sqlCom1 = new MySqlCommand("SELECT * FROM 'reader' WHERE 'email' = @eml AND 'password' = @pass", db.getConnection());

            sqlCom1.Parameters.Add("@eml", MySqlDbType.VarChar).Value = email;
            sqlCom1.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Успіх!");
            }
            else
            {
                MessageBox.Show("Такий користувач не існує");
            }
        }
    }
}
