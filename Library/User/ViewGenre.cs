using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Library
{

    public partial class ViewGenre : Form
    {

        public ViewGenre()
        {
            InitializeComponent();
            DBConnection d = new DBConnection();

            string query = "SELECT * FROM system_catalogue";

            MySqlCommand sqlCommand = new MySqlCommand(query, d.getConnection());

            d.openConnection();

            MySqlDataAdapter sdr = new MySqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sdr.Fill(dt);

            comboBox1.DataSource = dt;
            //comboBox1.DisplayMember = "catalogue_name";
            comboBox1.ValueMember = "catalogue_name";

            d.closeConnection();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = new UserMain { Visible = true };
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                ("SELECT catalogue_name FROM system_catalogue", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String genre = comboBox1.SelectedValue.ToString();
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
                " SELECT book_name "+
                "FROM(book inner join book_catalogue_connet on book.id_book = book_catalogue_connet.id_book) " +
                "inner join system_catalogue on system_catalogue.id_catalogue = book_catalogue_connet.id_catalogue "+

                $"WHERE catalogue_name = '{genre}' and book.id_book in " +

                "(Select fk_book "+
                "From exemplar "+
                "Where id_exemplar not in " +

                "(Select ppk_exemplar "+
                "From borrowing "+
                "Where real_return is null))", db.getConnection()
                );

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }
    }


}
