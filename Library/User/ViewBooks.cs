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
    public partial class ViewBooks : Form
    {
        public ViewBooks()
        {
            InitializeComponent();
            DBConnection d = new DBConnection();

            string query = "SELECT * FROM book;;";
            MySqlCommand sqlCommand = new MySqlCommand(query, d.getConnection());
            d.openConnection();
            MySqlDataAdapter sdr = new MySqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sdr.Fill(dt);
            comboBox1.DataSource = dt;
            //comboBox1.DisplayMember = "catalogue_name";
            comboBox1.ValueMember = "book_name";
            d.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = new UserMain { Visible = true };
            Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String book = comboBox1.SelectedValue.ToString();
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(
               $"SELECT id_exemplar FROM exemplar WHERE id_exemplar not in ( Select ppk_exemplar From borrowing Where real_return is null) and fk_book in (Select id_book FROM book WHERE book_name = '{book}')"
               , db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }
    }
}
