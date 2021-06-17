using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Library.Worker
{
    public partial class AddGenre : Form
    {
        public AddGenre()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = new BookRegistration { Visible = true };
            Visible = false;
        }


        public Boolean CheckGenreExistence()
        {
            DBConnection db1 = new DBConnection();
            db1.openConnection();

            String genre = textBox1.Text;

            MySqlCommand sqlCom2 = new MySqlCommand
                (
                $"SELECT * FROM system_catalogue WHERE catalogue_name = '{genre}';", db1.getConnection()
                );
            MySqlDataReader reader = sqlCom2.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
            else { return false; }

            db1.closeConnection();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            String genre = textBox1.Text;

            if (CheckGenreExistence())
            {
                MessageBox.Show("Такий жанр вже існує");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Жанр не додано - не всі необхідні дані надані");
                }
                else
                {
                    MySqlCommand command =
                            new MySqlCommand(
                                @"insert into system_catalogue (catalogue_name)
                                   values (@init);", db.getConnection()
                                );
                    command.Parameters.AddWithValue("@init", genre);

                    MySqlDataReader reader = command.ExecuteReader();

                    MessageBox.Show("Жанр додано!");
                }
            }
            db.closeConnection();
        }
    }
}
