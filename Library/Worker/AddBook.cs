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

            DBConnection db = new DBConnection();

            // отримання значень з таблиці "system_catalogue"
            string query = "SELECT * FROM authors";
            string queryBook = "SELECT * FROM system_catalogue";

            MySqlCommand sqlCommand = new MySqlCommand(query, db.getConnection());
            MySqlCommand sqlCommandGenre = new MySqlCommand(queryBook, db.getConnection());

            db.openConnection();

            MySqlDataAdapter sdr = new MySqlDataAdapter(sqlCommand);
            MySqlDataAdapter sdrBook = new MySqlDataAdapter(sqlCommandGenre);
            DataTable dt = new DataTable();
            DataTable dtBook = new DataTable();
            sdr.Fill(dt);
            sdrBook.Fill(dtBook);

            // обираємо колонку з необхідними даними
            authorsComboBox.DataSource = dt;
            authorsComboBox.ValueMember = "second_name";
            genresComboBox.DataSource = dtBook;
            genresComboBox.ValueMember = "id_catalogue";

            db.closeConnection();
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


        public Boolean CheckAuthConnectionExistence()
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            int id = int.Parse(textBox6.Text);
            String auth = authorsComboBox.SelectedValue.ToString();

            MySqlCommand sqlCom2 = new MySqlCommand
                (
                $"SELECT * FROM author_book_connect WHERE ppk_book = {id} AND " +
                $"ppk_author = '{auth}';", db.getConnection()
                );
            MySqlDataReader reader = sqlCom2.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            else { return false; }

            db.closeConnection();
        }

        public Boolean CheckGenreConnectionExistence()
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            int id = int.Parse(textBox6.Text);
            String auth = genresComboBox.SelectedValue.ToString();

            MySqlCommand sqlCom2 = new MySqlCommand
                (
                $"SELECT * FROM author_book_connect WHERE ppk_book = {id} AND " +
                $"ppk_author = " + int.Parse(auth) + ";", db.getConnection()
                );
            MySqlDataReader reader = sqlCom2.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            else { return false; }

            db.closeConnection();
        }

        public Boolean CheckBookExistence()
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

            if (CheckBookExistence())
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
            String auth = authorsComboBox.SelectedValue.ToString();

            DBConnection db = new DBConnection();
            db.openConnection();

            if (CheckAuthConnectionExistence())
            {
                MessageBox.Show("В цієї книги вже є такий автор!");
            }
            else
            {
                MySqlCommand command1 = new MySqlCommand(
            "INSERT INTO author_book_connect (ppk_book, ppk_author) " +
            "VALUES ('" + id + "','" + int.Parse(auth) + "')", db.getConnection());
                command1.ExecuteNonQuery();

                MessageBox.Show("Автора додано до книги!");
            }

            db.closeConnection();
        }

        //private int GenreToId(string a)
        //{
        //    string s_n = "SELECT id_catalogue FROM system_catalogue WHERE catalogue_name = '" + a + "';";

        //    return int.Parse(s_n);
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox6.Text);
            String genre = genresComboBox.SelectedValue.ToString();

            DBConnection db1 = new DBConnection();
            db1.openConnection();

            if (CheckGenreConnectionExistence())
            { 
                MessageBox.Show("Цей жанр вже відповідає даній книзі!"); 
            }
            else
            {
                MySqlCommand command1 = new MySqlCommand(
            "INSERT INTO book_catalogue_connet (id_book, id_catalogue) " +
            "VALUES ('" + id + "', '" + genre + "')", db1.getConnection());
                command1.ExecuteNonQuery();
                MessageBox.Show("Жанр додано!");
            }

            db1.closeConnection();
        }
    }
}

