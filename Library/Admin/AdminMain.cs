using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Library.Admin
{
    public partial class AdminMain : Form
    {
        public AdminMain()
        {
            InitializeComponent();

            DBConnection db = new DBConnection();

            // отримання значень з таблиці "system_catalogue"
            string query = "SELECT * FROM system_catalogue";
            string queryBook = "SELECT * FROM book";

            MySqlCommand sqlCommand = new MySqlCommand(query, db.getConnection());
            MySqlCommand sqlCommandBook = new MySqlCommand(queryBook, db.getConnection());

            db.openConnection();

            MySqlDataAdapter sdr = new MySqlDataAdapter(sqlCommand);
            MySqlDataAdapter sdrBook = new MySqlDataAdapter(sqlCommandBook);
            DataTable dt = new DataTable();
            DataTable dtBook = new DataTable();
            sdr.Fill(dt);
            sdrBook.Fill(dtBook);

            // обираємо колонку з необхідними даними
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "catalogue_name";
            bookNameBox.DataSource = dtBook;
            bookNameBox.ValueMember = "book_name";

            db.closeConnection();
        }

        // біла панель з випадаючим списком тих значень,
        // які взяли у AdminMain()
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String genre = comboBox1.SelectedValue.ToString();
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
                "select ppk_author, count(id_book) as books_amount " +
                "from author_book_connect inner join book " +
                "on id_book = ppk_book " +
                "where id_book in " +

                "(select b.id_book " +
                "from book_catalogue_connet as b " +
                "where b.id_catalogue in " +

                "(select c.id_catalogue " +
                "from system_catalogue as c " +
                $"where catalogue_name = '{genre}')) " +

                 "group by ppk_author;", db.getConnection()
                );
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }

        private void getPopularBooks_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
                "SELECT b.id_book, b.book_name AS 'Назва книги', COUNT(*) AS Count " +
                "FROM(book b INNER JOIN exemplar ex " +
                "on b.id_book = ex.fk_book) inner join borrowing bo " +
                "ON bo.ppk_exemplar = ex.id_exemplar " +

                $"where (bo.exodused > NOW()-INTERVAL 2 year) and (bo.exodused < NOW()) " +
                "GROUP BY b.book_name " +
                "ORDER BY Count DESC; ", db.getConnection()
                );

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }

        private void getUnpopularBooks_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
                "SELECT b.id_book, b.book_name AS 'Назва книги', COUNT(*) AS Count " +
                "FROM(book b INNER JOIN exemplar ex " +
                "on b.id_book = ex.fk_book) inner join borrowing bo " +
                "ON bo.ppk_exemplar = ex.id_exemplar " +

                $"where (bo.exodused > NOW()-INTERVAL 2 year) and (bo.exodused < NOW()) " +
                "GROUP BY b.book_name " +
                "ORDER BY Count; ", db.getConnection()
                );

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
                "SELECT DISTINCT id_reader, sec_name as 'Прізвище', first_name as 'Ім''я', email as 'Е-mail', " +
                "GROUP_CONCAT(telenum SEPARATOR ',') as 'Номер телефону' " +
                "from(reader left join telephones on telephones.fk_reader = reader.id_reader) " +
                "where id_reader in " +
                "(select distinct ppk_reader " +
                "from borrowing " +
                "where (real_return is null) and(expected_return < current_date())) " +
                "group by id_reader, sec_name, first_name;", db.getConnection()
                );

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void bookNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            String bookName = bookNameBox.SelectedValue.ToString();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
                    "select price as 'Ціна', book_name as 'Назва книги', id_book " +
                    "from book " +
                    $"where book_name = '{bookName}';", db.getConnection()
                );

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }
    }
}
