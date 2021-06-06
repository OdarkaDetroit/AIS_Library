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
