using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Library.Entrance
{
    public partial class SignIn : Form
    {
        //MySqlConnection sqlCon = new MySqlConnection();

        //DataTable sqlDt1 = new DataTable();

        //MySqlDataReader sqlRd1;

        //MySqlDataAdapter DtA = new MySqlDataAdapter();

        //DataSet DS = new DataSet();

        //String server = "localhost";
        //String username = "root";
        //String password = "u8s8jDddds2_0";
        //String database = "lib";



        public SignIn()
        {
            InitializeComponent();
        }

        private void registrateButton_Click(object sender, EventArgs e)
        {
            _ = new SignUp { Visible = true };
            Visible = false;
        }
    }
}
