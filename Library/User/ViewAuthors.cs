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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = new UserMain { Visible = true };
            Visible = false;
        }
    }
}
