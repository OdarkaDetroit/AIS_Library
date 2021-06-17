using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Library.Worker
{
    public partial class AddBook : Form
    {
        string value;
        NumberStyles style;
        CultureInfo culture;
        decimal number;


        public AddBook()
        {
            InitializeComponent();
        }


        private void AddBook_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = new BookRegistration { Visible = true };
            Visible = false;
        }


        public Boolean CheckGenreExistence()
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            String name = textBox1.Text;
            String city = textBox2.Text;
            String publisher = textBox3.Text;
            decimal price = decimal.Parse(textBox5.Text, CultureInfo.InvariantCulture);
            int year = dateTimePicker1.Value.Year;

            MySqlCommand sqlCom2 = new MySqlCommand
                (
                $"SELECT * FROM book WHERE book_name = '{name}' AND " +
                $"publishing_city = '{city}' AND publiser_name = '{publisher}' " +
                $"AND price = {price} AND publishing_date = {year};", db.getConnection()
                );
            MySqlDataReader reader = sqlCom2.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            else { return false; }

            db.closeConnection();
        }



        // add button
        private void button2_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            int id = int.Parse(textBox6.Text);
            String name = textBox1.Text;
            String city = textBox2.Text;
            String publisher = textBox3.Text;
            double pages = int.Parse(textBox4.Text);
            decimal price = decimal.Parse(textBox5.Text, CultureInfo.InvariantCulture);
            int year = dateTimePicker1.Value.Year;

            if (CheckGenreExistence())
            {
                MessageBox.Show("Така книга вже існує!");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text)
                    || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text)
                    || string.IsNullOrWhiteSpace(textBox5.Text) || dateTimePicker1.Value == default(DateTime)
                    || string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    MessageBox.Show("Книгу не додано - не всі необхідні дані надані");
                }
                else
                {
                    MySqlCommand command =
                            new MySqlCommand(
                                @"insert into book (id_book, book_name, publishing_city, publiser_name,
                                                    publishing_date, pages_num, price)
                                        values (@id, @name, @city, @pname, @pdate, @pnum, @price);", db.getConnection()
                                );

                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@city", city);
                    command.Parameters.AddWithValue("@pname", publisher);
                    command.Parameters.AddWithValue("@pdate", year);
                    command.Parameters.AddWithValue("@pnum", pages);
                    command.Parameters.AddWithValue("@price", price);

                    MySqlDataReader reader = command.ExecuteReader();


                    MessageBox.Show("Book додано!");
                db.closeConnection();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox6.Text);

            DBConnection db = new DBConnection();
            db.openConnection();

            var authors = authorsComboBox.Text.Replace(" ", "").Split(',');


            //check if author in database
            foreach (string author in authors)
            {
                //string l_n = "";
                //MySqlCommand command1 =
                //            new MySqlCommand(@"SELECT second_name FROM authors WHERE second_name = '" + author + "'", db.getConnection());
                //MySqlDataReader reader1 = command1.ExecuteReader();

                //while (reader1.Read())
                //    l_n += reader1.GetString(0);
                //if (l_n != "")
                //{
                MySqlCommand command1 = new MySqlCommand(
                        "INSERT INTO author_book_connect (ppk_book, ppk_author) " +
                        "VALUES ('" + id + "','" + author + "')", db.getConnection());
                    command1.ExecuteNonQuery();
                //}
            }

            db.closeConnection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox6.Text);

            DBConnection db = new DBConnection();
            db.openConnection();

            var genres = genresComboBox.Text.Replace(" ", "").Split(',');

            foreach (string genre in genres)
            {
                MySqlCommand command1 = new MySqlCommand(
                        "INSERT INTO book_catalogue_connet (id_book, id_catalogue) " +
                        "VALUES ('" + id + "','" + genre + "')", db.getConnection());
                command1.ExecuteNonQuery();
                //}
            }
            db.closeConnection();
        }
    }
}

