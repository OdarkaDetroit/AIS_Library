using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Library
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        //registration (adding new user)
        private void button1_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();

            String secname = textBox1.Text;
            String firname = textBox2.Text;
            String thirname = textBox3.Text;

            String city = textBox4.Text;
            String street = textBox5.Text;
            String house = textBox6.Text;
            String flat = textBox7.Text;
            String workplace = textBox8.Text;

            String bdate = dateTimePicker1.Text;
            String email = textBox9.Text;
            String pass = textBox10.Text;

            MySqlCommand command =
                new MySqlCommand("INSERT INTO 'reader' VALUES " +
                "(@sname, @fname, @tname, @cit, @str, @hos, @flt, @wrk, @brthd, @eml, @pssw)");

            command.Parameters.Add("@sname", MySqlDbType.VarChar).Value = secname;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = firname;
            command.Parameters.Add("@tname", MySqlDbType.VarChar).Value = thirname;
            command.Parameters.Add("@cit", MySqlDbType.VarChar).Value = city;
            command.Parameters.Add("@str", MySqlDbType.VarChar).Value = street;
            command.Parameters.Add("@hos", MySqlDbType.VarChar).Value = house;
            command.Parameters.Add("@flt", MySqlDbType.VarChar).Value = flat;
            command.Parameters.Add("@wrk", MySqlDbType.VarChar).Value = workplace;
            command.Parameters.Add("@brthd", MySqlDbType.VarChar).Value = bdate;
            command.Parameters.Add("@eml", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@pssw", MySqlDbType.VarChar).Value = pass;

            db.openConnection();

            if(CheckEmail())
            {
                MessageBox.Show("Ця електронна скринька вже використовується");
            }
            else
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Створено акаунт!");
                }
                else
                {
                    MessageBox.Show("Акаунт не створено");
                }
            }

            db.closeConnection();
        }

        //some visual changes, no value

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{
        //    String secName = textBox1.Text;
        //    if(secName.Equals("second name"))
        //    {
        //        textBox1.Text = "";
        //    }
        //}

        //private void textBox2_TextChanged(object sender, EventArgs e)
        //{
        //    String firName = textBox2.Text;
        //    if (firName.Equals("first name"))
        //    {
        //        textBox2.Text = "";
        //    }
        //}

        public Boolean CheckEmail()
        {
            DBConnection db1 = new DBConnection();

            String email = textBox9.Text;

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand sqlCom1 = new MySqlCommand("SELECT * FROM 'reader' WHERE 'email' = @eml", db1.getConnection());

            sqlCom1.Parameters.Add("@eml", MySqlDbType.VarChar).Value = email;

            adapter.Fill(table);

            //check if the email already exists in the db
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
