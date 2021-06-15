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

using static Library.PasswordManipulations;

namespace Library
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        // перевірка на вже існуючий email у БД
        public Boolean CheckEmail()
        {
            DBConnection db1 = new DBConnection();

            db1.openConnection();

            String email = textBox9.Text;

            MySqlCommand sqlCom2 = new MySqlCommand($"SELECT * FROM reader WHERE email = '{email}'", db1.getConnection());

            MySqlDataReader reader = sqlCom2.ExecuteReader();

            if (reader.HasRows)
            {
                //MessageBox.Show("Така адреса електронної скриньки вже існує");
                return true;
            }
            else { return false; }

            db1.closeConnection();

        }


        //registration (adding new user)
        private void button1_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();


            string secname = textBox1.Text;
            string firname = textBox2.Text;
            string thirname = textBox3.Text;

            string city = textBox4.Text;
            string street = textBox6.Text;
            string house = textBox5.Text;
            string flat = textBox7.Text;
            string workplace = textBox8.Text;

            DateTime bdate = dateTimePicker1.Value;
            string email = textBox9.Text;
            string pass = textBox10.Text;

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
                else
                {
                    MySqlCommand command =
                new MySqlCommand(
                    "INSERT INTO reader (sec_name, first_name, third_name, " +
                "city, street, house, flat, workplace, birth_date, email, password) VALUES " +
                "(@sname, @fname, @tname, @cit, @str, @hos, @flt, @wrk, @brthd, @eml, @pssw)", db.getConnection());

                    //command.Prepare();

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

                    // encoding gotten from the current user password
                    string encodePass = EncodePasswordToBase64(pass);
                    command.Parameters.AddWithValue("@pssw", encodePass);


                    MySqlDataReader reader = command.ExecuteReader();

                        MessageBox.Show("Створено акаунт!");

                            _ = new UserMain { Visible = true };
                            Visible = false;

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



        }
    }
}
