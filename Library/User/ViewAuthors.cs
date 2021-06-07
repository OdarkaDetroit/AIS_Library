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
    }
}
