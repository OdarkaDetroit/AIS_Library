using Library.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Library
{
    public partial class UserMain : Form
    {
        public UserMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = new ViewGenre { Visible = true };
            Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _ = new ViewBooks { Visible = true };
            Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // рядочок *клік* виділення значення
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = new ViewAuthors { Visible = true };
            Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _ = new MyBooks { Visible = true };
            Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _ = new MyBookHistory { Visible = true };
            Visible = false;
        }
    }
}
