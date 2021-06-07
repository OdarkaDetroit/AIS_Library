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

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(
                "SELECT catalogue_name FROM system_catalogue", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }



    }


}
