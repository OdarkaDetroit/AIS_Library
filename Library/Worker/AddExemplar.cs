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
    public partial class AddExemplar : Form
    {
        public AddExemplar()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = new BookRegistration { Visible = true };
            Visible = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            int shelf = int.Parse(textBox1.Text);
            int fk_book = int.Parse(textBox2.Text);

            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Не додано примірник - не всі дані надані");
            }
            else
            {
                MySqlCommand command =
                        new MySqlCommand(
            @"insert into exemplar (shelf, fk_book) values (@shelf, @fk_book);", db.getConnection()
            );
                command.Parameters.AddWithValue("@shelf", shelf);
                command.Parameters.AddWithValue("@fk_book", fk_book);

                MySqlDataReader reader = command.ExecuteReader();

                MessageBox.Show("Примірник додано!");
            }
            db.closeConnection();
        }
    }
}
