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
    public partial class AddAuthor : Form
    {
        public AddAuthor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = new BookRegistration { Visible = true };
            Visible = false;
        }


        public Boolean CheckAuthorExistence()
        {
            DBConnection db1 = new DBConnection();
            db1.openConnection();

            String secName = secondNameBox.Text;
            String initi = initialsBox.Text;

            MySqlCommand sqlCom2 = new MySqlCommand
                (
                $"SELECT * FROM authors WHERE second_name = '{secName}' " +
                                             $"and initials = '{initi}';", db1.getConnection()
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

            String secName = secondNameBox.Text;
            String initi = initialsBox.Text;

            if (CheckAuthorExistence())
            {
               MessageBox.Show("Такий автор вже існує");            
            }
            else
            {
                if (string.IsNullOrWhiteSpace(secondNameBox.Text) || string.IsNullOrWhiteSpace(initialsBox.Text) 
                    || initialsBox.Text.Length > 4)
                {
                    MessageBox.Show("Автора не додано - не всі необхідні дані надані");
                }
                else
                {
                    MySqlCommand command =
                            new MySqlCommand(
                                @"insert into authors (second_name, initials)
                                   values (@secname, @init);", db.getConnection()
                                );
                    command.Parameters.AddWithValue("@secname", secName);
                    command.Parameters.AddWithValue("@init", initi);

                    MySqlDataReader reader = command.ExecuteReader();

                    MessageBox.Show("Створено акаунт!");
                }
            }
             db.closeConnection();
        }
    }
}
