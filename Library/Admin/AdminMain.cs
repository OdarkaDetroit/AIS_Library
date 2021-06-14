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

            DBConnection d = new DBConnection();
            // отримання значень з таблиці "system_catalogue"
            string query = "SELECT * FROM system_catalogue";

            MySqlCommand sqlCommand = new MySqlCommand(query, d.getConnection());

            d.openConnection();

            MySqlDataAdapter sdr = new MySqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            // обираємо колонку з необхідними даними
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "catalogue_name";

            d.closeConnection();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
