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
                _ = new AddBook { Visible = true };
                Visible = false;
            }
        }
    
}
