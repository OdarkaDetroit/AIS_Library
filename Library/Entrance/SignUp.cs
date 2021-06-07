using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Library.Entrance;

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
            db.openConnection();


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
                new MySqlCommand("INSERT INTO reader (sec_name, fir_name, third_name, " +
                "city, street, house, flat, workplace, birth_date, email, password, accessibility) VALUES " +
                "(@sname, @fname, @tname, @cit, @str, @hos, @flt, @wrk, @brthd, @eml, @pssw, 'User')", db.getConnection());

            //MySqlCommand command =
            //    new MySqlCommand("INSERT INTO reader (sec_name, fir_name, third_name, " +
            //    "city, street, house, flat, workplace, birth_date, email, password, accessibility) VALUES " +
            //    "(@sec_name, @fir_name, @third_name, @city, @street, @house, @flat, @workplace, @birth_date, @email, @password, 'User')", db.getConnection());


            MySqlDataReader reader = command.ExecuteReader();

            command.Prepare();

            command.Parameters.AddWithValue("@sname", secname);
            command.Parameters.AddWithValue("@fname", firname);

            if (thirname != "")
                command.Parameters.AddWithValue("@tname", thirname);
            else
                command.Parameters.AddWithValue("@tname", DBNull.Value);

            command.Parameters.AddWithValue("@cit", city);
            command.Parameters.AddWithValue("@str", street);
            command.Parameters.AddWithValue("@hos", house);

            if (flat != "")
                command.Parameters.AddWithValue("@flt", flat);
            else
                command.Parameters.AddWithValue("@flt", DBNull.Value);

            if (workplace != "")
                command.Parameters.AddWithValue("@wrk", workplace);
            else
                command.Parameters.AddWithValue("@wrk", DBNull.Value);

            command.Parameters.AddWithValue("@brthd", bdate);
            command.Parameters.AddWithValue("@eml", email);
            command.Parameters.AddWithValue("@pssw", pass);


        
            //command.Parameters.Add("@sname", MySqlDbType.VarChar).Value = secname;
            //command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = firname;

            //if (thirname != "")
            //    command.Parameters.Add("@tname", MySqlDbType.VarChar).Value = thirname;
            //else
            //    command.Parameters.AddWithValue("@tname", DBNull.Value);

            //command.Parameters.Add("@cit", MySqlDbType.VarChar).Value = city;
            //command.Parameters.Add("@str", MySqlDbType.VarChar).Value = street;
            //command.Parameters.Add("@hos", MySqlDbType.VarChar).Value = house;

            //if (flat != "")
            //    command.Parameters.Add("@flt", MySqlDbType.Int32).Value = flat;
            //else
            //    command.Parameters.AddWithValue("@flt", DBNull.Value);

            //if (workplace != "")
            //    command.Parameters.Add("@wrk", MySqlDbType.VarChar).Value = workplace;
            //else
            //    command.Parameters.AddWithValue("@wrk", DBNull.Value);

            //command.Parameters.Add("@brthd", MySqlDbType.Date).Value = bdate;
            //command.Parameters.Add("@eml", MySqlDbType.VarChar).Value = email;
            //command.Parameters.Add("@pssw", MySqlDbType.VarChar).Value = pass;
        


            if (CheckEmail())
            {
                MessageBox.Show("Ця електронна скринька вже використовується");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text)
                    || string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text)
                    || string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(dateTimePicker1.Text)
                    || string.IsNullOrWhiteSpace(textBox9.Text) || string.IsNullOrWhiteSpace(textBox10.Text))
                {
                    MessageBox.Show("Акаунт не створено - не всі необхідні дані надані");
                }
                else if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Створено акаунт!");
                    while(reader.Read())
                    {
                        _ = new UserMain { Visible = true };
                        Visible = false;
                    }

                }
                //else
                //{
                //    MessageBox.Show("Акаунт не створено");
                //}
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

            db1.openConnection();

            String email = textBox9.Text;

            //DataTable table = new DataTable();
            //MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand sqlCom2 = new MySqlCommand($"SELECT * FROM reader WHERE email = '{email}'", db1.getConnection());

            //sqlCom2.Parameters.Add("@eml", MySqlDbType.VarChar).Value = email;

            //adapter.Fill(table);
            MySqlDataReader reader = sqlCom2.ExecuteReader();


            //check if the email already exists in the db
            if (reader.HasRows)
            {
                MessageBox.Show("Така адреса електронної скриньки вже існує");
                return true;

            }
            else
            {
                return false;
            }

            db1.closeConnection();

        }

    }
}
