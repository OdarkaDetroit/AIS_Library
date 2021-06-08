using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Library.User
{
    public partial class ViewAuthors : Form
    {
        public ViewAuthors()
        {
            InitializeComponent();
            DBConnection d = new DBConnection();

            string query = "SELECT * FROM authors;";

            MySqlCommand sqlCommand = new MySqlCommand(query, d.getConnection());

            d.openConnection();

            MySqlDataAdapter sdr = new MySqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sdr.Fill(dt);

            comboBox1.DataSource = dt;
            //comboBox1.DisplayMember = "catalogue_name";
            comboBox1.ValueMember = "second_name";

            d.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = new UserMain { Visible = true };
            Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String author = comboBox1.SelectedValue.ToString();
            DBConnection db = new DBConnection();

            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(
                $"SELECT book_name " +
                $"FROM book " +
                $"WHERE id_book IN " +

                $"(SELECT ppk_book " +
                $"FROM author_book_connect " +
                $"WHERE ppk_author = '{author}') " +
                $"and id_book in" +

                $"(Select fk_book " +
                $"From exemplar " +
                $"Where id_exemplar not in" +

                $"( Select ppk_exemplar " +
                $"From borrowing " +
                $"Where real_return is null))", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();

        }

        /*private void comboBox1_Click(object sender, EventArgs e)
        {

            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(
                "SELECT catalogue_name FROM system_catalogue", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }*/
    }
}
