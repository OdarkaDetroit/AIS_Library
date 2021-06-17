using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Library.Worker
{
    public partial class WorkerMain : Form
    {
        public WorkerMain()
        {
            InitializeComponent();
        }

        private void lendingBook_Click(object sender, EventArgs e)
        {
            _ = new LendBook { Visible = true };
            Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = new BookRegistration { Visible = true };
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = new ChangeTerm { Visible = true };
            Visible = false;
        }

        private void deletingBook_Click(object sender, EventArgs e)
        {
            _ = new WriteOff { Visible = true };
            Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            _ = new Lost { Visible = true };
            Visible = false;
        }
    }
    
}
