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
    public partial class WriteOff : Form
    {
        public WriteOff()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();

            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(

                " Select *" +
" from book" +
" where id_book not in (" +
" 		select e.fk_book" +
"         from borrowing as b inner join exemplar as e" +
"         on b.ppk_exemplar=e.id_exemplar" +
"         where (YEAR(CURRENT_DATE) - YEAR(exodused)) -" +
"     (DATE_FORMAT(CURRENT_DATE, '%m%d') < DATE_FORMAT(exodused, '%m%d')) <2)" +
"     and exists (select * " +
"     from exemplar" +
"     where book.id_book=exemplar.fk_book" +
"     and id_exemplar not in(" +
" 			select old_exemp" +
"             from changes))", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            MySqlCommand authorCom = new MySqlCommand("Select book_name " +
                    " from book" +
                    " where id_book not in (" +
                    " 		select e.fk_book" +
                    "         from borrowing as b inner join exemplar as e" +
                    "         on b.ppk_exemplar=e.id_exemplar" +
                    "         where (YEAR(CURRENT_DATE) - YEAR(exodused)) -" +
                    "     (DATE_FORMAT(CURRENT_DATE, '%m%d') < DATE_FORMAT(exodused, '%m%d')) <2)" +
                    "     and exists (select * " +
                    "     from exemplar" +
                    "     where book.id_book=exemplar.fk_book" +
                    "     and id_exemplar not in(" +
                    " 			select old_exemp" +
                    "             from changes))", db.getConnection());
            string authorcheck = (string)authorCom.ExecuteScalar();

            if (authorcheck == null)
            {

                MessageBox.Show("Зараз немає не списаних книг!");
            }

            db.closeConnection();
        }
        public int id_book = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                try
                {
                    textBox1.Text = row.Cells["id_book"].Value.ToString();
                    id_book = int.Parse(row.Cells["id_book"].Value.ToString());

                }
                catch { };
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = new WorkerMain { Visible = true };
            Visible = false;
        }
    }
}
