using Library.Entrance;
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
        public int id_exemp = 0;
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
                try
                {
                    textBox2.Text = row.Cells["id_exemplar"].Value.ToString();
                    id_exemp = int.Parse(row.Cells["id_exemplar"].Value.ToString());

                }
                catch { };
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _ = new WorkerMain { Visible = true };
            Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            if (userName == "")
            {
                MessageBox.Show("Оберіть книгу !");
            }
            else
            {
                int idB = int.Parse(userName);
                DBConnection db = new DBConnection();
                db.openConnection();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                    (" Select id_exemplar, shelf ,book_name, publishing_city, " +
                    " publiser_name, publishing_date, pages_num, price" +
                    " From exemplar inner join book" +
                    " on fk_book=id_book" +
                    $" where id_book='{idB}' and id_exemplar not in( select old_exemp from changes)", db.getConnection());

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];
                dataGridView1.Columns["id_exemplar"].DisplayIndex = 0;

                MySqlCommand authorCom = new MySqlCommand(" Select book_name " +
                    " From exemplar inner join book" +
                    " on fk_book=id_book" +
                    $" where id_book='{idB}' and id_exemplar not in( select old_exemp from changes)", db.getConnection());
                string authorcheck = (string)authorCom.ExecuteScalar();

                if (authorcheck == null)
                {

                    MessageBox.Show("Немає примірників!");
                }
                db.closeConnection();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (id_exemp == 0)
            {
                MessageBox.Show("Оберіть екземпляр!");
            }
            else if (id_book == 0)
            {
                MessageBox.Show("Оберіть книгу!");
            }
            else
            {
                DBConnection db = new DBConnection();
                db.openConnection();
                    MySqlCommand command =
               new MySqlCommand(
                   "INSERT INTO changes (id_worker, date_change, old_exemp) VALUES  " +
               "(@id_work, @data_change,@old_exemp )", db.getConnection());
                    //SignIn.userId
                    //command.Prepare();
                   // command.Parameters.AddWithValue("@id_reader", id_read);
                    command.Parameters.AddWithValue("@old_exemp",id_exemp);
                    command.Parameters.AddWithValue("@data_change", DateTime.Now.ToString("yyyy/MM/dd"));
                    command.Parameters.AddWithValue("@id_work", WorkSignIn.workerId);
                   // command.Parameters.AddWithValue("@new_exemp", int.Parse(sNewExemp));
                    MySqlDataReader reader = command.ExecuteReader();

                    MessageBox.Show("Книга списана! ");
                    //  textBox3.Clear();

                    db.closeConnection();
                }
            }
    }
}
