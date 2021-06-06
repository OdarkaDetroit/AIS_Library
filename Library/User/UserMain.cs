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

        }
    }
}
