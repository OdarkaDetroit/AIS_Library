using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Library.Entrance;

using static Library.PasswordManipulations;
using System.Data.OleDb;

namespace Library
{
    public partial class SignUp : Form
    {
        DateTime localDate = DateTime.Now;

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

        // перевірка на вже існуючий telephone у БД
        public Boolean CheckPhone()
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            String telenum = textBox9.Text;

            MySqlCommand sqlCom2 = new MySqlCommand($"SELECT * FROM telephones WHERE telenum = '{telenum}'", db.getConnection());
            MySqlDataReader reader = sqlCom2.ExecuteReader();

            if (reader.HasRows)
            {
                //MessageBox.Show("Такий мобільний номер вже використовується кимось");
                return true;
            }
            else { return false; }

            db.closeConnection();
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
            string telenum = textBox11.Text;

            TimeSpan span = localDate.Subtract(bdate);

            if (CheckEmail() || CheckPhone())
            {
                MessageBox.Show("Ця електронна скринька або/та цей номер телефону вже використовується");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text)
                    || string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox5.Text)
                    || string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(dateTimePicker1.Text)
                    || string.IsNullOrWhiteSpace(textBox9.Text) || string.IsNullOrWhiteSpace(textBox10.Text)
                    || bdate == default(DateTime) || (span.Days < 6205) || string.IsNullOrWhiteSpace(textBox11.Text)
                    || (textBox10.Text.Length < 8))
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


                    //MySqlDataReader reader = command.ExecuteReader();

                    command.ExecuteNonQuery();

                    ///// trlrphone ading 
                    ///
                    var l_n = "";
                    var cmd = new MySqlCommand(@"SELECT id_reader
                                              FROM reader WHERE email = '" + textBox9.Text + "'", db.getConnection());
                    MySqlDataReader reader2 = cmd.ExecuteReader();
                    while (reader2.Read())
                        l_n += reader2.GetInt32(0);
                    reader2.Close();
                    
                        var ph = textBox11.Text.Replace(" ", "").Split(',');
                    foreach (string ph1 in ph)
                    {
                        cmd = new MySqlCommand(
                            "INSERT INTO telephones (telenum, fk_reader) " +
                            "VALUES ('" + ph1 + "'," + l_n + ")", db.getConnection());
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Створено акаунт!");

                            _ = new UserMain { Visible = true };
                            Visible = false;
                }
                db.closeConnection();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked==true)
            {
                textBox10.UseSystemPasswordChar = false;
            }
            else
            {
                textBox10.UseSystemPasswordChar = true;
            }
        }
    }
}
