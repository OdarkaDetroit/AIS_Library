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
    public partial class LendBook : Form
    {
        public LendBook()
        {
            InitializeComponent();
            DBConnection d = new DBConnection();
            d.openConnection();
            string queryBook = " SELECT * FROM book" +
                       " 		Where id_book in(" +
                       " 		select fk_book" +
                       " 		from exemplar" +
                       " 		where id_exemplar not in(" +
                       " 		select old_exemp" +
                       " 		from changes))" +
                       "         and (exists(" +
                       " 				select *" +
                       "                 from borrowing inner join exemplar on id_exemplar=ppk_exemplar" +
                       "                 where expected_return>= SYSDATE() and fk_book =id_book) " +
                       "                 or not exists(" +
                       "                 select *" +
                       "                 from borrowing as b inner join exemplar as e on e.id_exemplar=b.ppk_exemplar" +
                       "                 where b.real_return is null and e.fk_book =id_book) );";
            MySqlCommand sqlCommandBook = new MySqlCommand(queryBook, d.getConnection());

            MySqlDataAdapter sdrBook = new MySqlDataAdapter(sqlCommandBook);
            DataTable dtBook = new DataTable();
            sdrBook.Fill(dtBook);
            comboBox1.DataSource = dtBook;
            //comboBox1.DisplayMember = "catalogue_name";
            comboBox1.ValueMember = "book_name";


            d.closeConnection();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            if (userName == "") {
                MessageBox.Show("Введіть прізвище читача!");
            }
            else
            {
                DBConnection db = new DBConnection();
                db.openConnection();
               
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                    (" Select id_reader, sec_name, first_name, third_name" +
                        " From reader" +
                        $" Where sec_name='{userName}'", db.getConnection());

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];

                MySqlCommand authorCom = new MySqlCommand("select sec_name" +
                        " From reader" +
                        $" Where sec_name='{userName}'", db.getConnection());
                string authorcheck = (string)authorCom.ExecuteScalar();

                if (authorcheck == null)
                {

                    MessageBox.Show("Немає такого читача!");
                }
                db.closeConnection();
            }
        }
        public int id_read=0;
        public int numExemp=0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
               
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                try
                {
                    textBox2.Text = row.Cells["id_reader"].Value.ToString();
                    id_read = int.Parse(row.Cells["id_reader"].Value.ToString());
                    textBox3.Text = row.Cells["id_exemplar"].Value.ToString();
                    numExemp = int.Parse(row.Cells["id_exemplar"].Value.ToString());
                }
                catch { };
                try
                {
   
                    textBox3.Text = row.Cells["id_exemplar"].Value.ToString();
                    numExemp = int.Parse(row.Cells["id_exemplar"].Value.ToString());
                }
                catch { };


            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                try
                {
                    textBox2.Text = row.Cells["id_reader"].Value.ToString();
                    id_read = int.Parse(row.Cells["id_reader"].Value.ToString());
                    textBox3.Text = row.Cells["id_exemplar"].Value.ToString();
                    numExemp = int.Parse(row.Cells["id_exemplar"].Value.ToString());
                }
                catch { };
                try
                {

                    textBox3.Text = row.Cells["id_exemplar"].Value.ToString();
                    numExemp = int.Parse(row.Cells["id_exemplar"].Value.ToString());
                }
                catch { };


            }
        }
        public int cBook=0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String book = comboBox1.SelectedValue.ToString();
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
               " SELECT id_exemplar ,book_name, publishing_city, publiser_name, publishing_date, pages_num, price" +
                " FROM exemplar inner join book on fk_book=id_book " +
                " WHERE id_exemplar not in " +
                " (Select ppk_exemplar " +
                " From borrowing " +
                " Where real_return is null) and fk_book in " +
                " (Select id_book " +
                " FROM book " +
                $" WHERE book_name = '{book}')" +
                " and id_exemplar not in(" +
                " select old_exemp" +
                " from changes)"
               , db.getConnection()
               );

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            //dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = dataSet.Tables[0];
            dataGridView1.Columns["id_exemplar"].DisplayIndex = 0;

            MySqlCommand authorCom = new MySqlCommand(
                " SELECT book_name " +
                " FROM (exemplar inner join book on fk_book=id_book) inner join author_book_connect on id_book=ppk_book" +
                " WHERE id_exemplar not in " +
                " (Select ppk_exemplar " +
                " From borrowing " +
                " Where real_return is null) and fk_book in " +
                " (Select id_book " +
                " FROM book " +
                $" WHERE book_name = '{book}')" +
                " and id_exemplar not in(" +
                " select old_exemp" +
                " from changes)"
                , db.getConnection());
            string authorcheck = (string)authorCom.ExecuteScalar();

            if (authorcheck == null && cBook > 0)
            {

                MessageBox.Show("Зараз у цієї книги немає вільних екземплярів!");
                MySqlDataAdapter dataAdapterIf = new MySqlDataAdapter(
             " Select expected_return" +
            " from borrowing" +
            " where ppk_exemplar in (" +
            " 		select id_exemplar" +
            "         from exemplar" +
            "         where fk_book in (" +
            " 					select id_book" +
            "                     from book " +
           $"                     where book_name ='{book}' ))" +
            " and expected_return>= SYSDATE() and real_return is Null" +
            " ORDER BY expected_return limit 1;"
              , db.getConnection()
              );

                DataSet dataSetIf = new DataSet();
                dataAdapterIf.Fill(dataSetIf);
                //dataGridView1.AutoGenerateColumns = false;

                dataGridView1.DataSource = dataSetIf.Tables[0];

            }
            if (authorcheck == null) { cBook++; }

            db.closeConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (numExemp == 0)
            {
                MessageBox.Show("Оберіть екземпляр!");
            }
            else if (id_read==0) {
                MessageBox.Show("Оберіть читача!");
            }
            else
            {
                DBConnection db = new DBConnection();
                db.openConnection();

                MySqlCommand command =
              new MySqlCommand(
                  "INSERT INTO borrowing (ppk_reader, ppk_exemplar, exodused, expected_return, real_return) VALUES " +
              "(@idRead, @id_exemp,@exodused ,@expected_return, @real_return)", db.getConnection());
                //SignIn.userId
                //command.Prepare();
                command.Parameters.AddWithValue("@idRead", id_read);
                command.Parameters.AddWithValue("@id_exemp", numExemp);
                command.Parameters.AddWithValue("@exodused", DateTime.Now.ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@expected_return", DateTime.Today.AddDays(14).ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@real_return", DBNull.Value);
                MySqlDataReader reader = command.ExecuteReader();

                MessageBox.Show("Книга видана на 2 тижні! ");
                textBox3.Clear();
                
                db.closeConnection();

                String book = comboBox1.SelectedValue.ToString();
                DBConnection d = new DBConnection();
                d.openConnection();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                    (
                   " SELECT id_exemplar ,book_name, publishing_city, publiser_name, publishing_date, pages_num, price" +
                    " FROM exemplar inner join book on fk_book=id_book " +
                    " WHERE id_exemplar not in " +
                    " (Select ppk_exemplar " +
                    " From borrowing " +
                    " Where real_return is null) and fk_book in " +
                    " (Select id_book " +
                    " FROM book " +
                    $" WHERE book_name = '{book}')" +
                    " and id_exemplar not in(" +
                    " select old_exemp" +
                    " from changes)"
                   , d.getConnection()
                   );

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                //dataGridView1.AutoGenerateColumns = false;

                dataGridView1.DataSource = dataSet.Tables[0];
                dataGridView1.Columns["id_exemplar"].DisplayIndex = 0;

                MySqlCommand authorCom = new MySqlCommand(
                    " SELECT book_name " +
                    " FROM (exemplar inner join book on fk_book=id_book) inner join author_book_connect on id_book=ppk_book" +
                    " WHERE id_exemplar not in " +
                    " (Select ppk_exemplar " +
                    " From borrowing " +
                    " Where real_return is null) and fk_book in " +
                    " (Select id_book " +
                    " FROM book " +
                    $" WHERE book_name = '{book}')" +
                    " and id_exemplar not in(" +
                    " select old_exemp" +
                    " from changes)"
                    , d.getConnection());
                string authorcheck = (string)authorCom.ExecuteScalar();

                if (authorcheck == null )
                {

                 //   MessageBox.Show("Зараз у цієї книги немає вільних екземплярів!");
                    MySqlDataAdapter dataAdapterIf = new MySqlDataAdapter(
                 " Select expected_return" +
                " from borrowing" +
                " where ppk_exemplar in (" +
                " 		select id_exemplar" +
                "         from exemplar" +
                "         where fk_book in (" +
                " 					select id_book" +
                "                     from book " +
               $"                     where book_name ='{book}' ))" +
                " and expected_return>= SYSDATE() and real_return is Null" +
                " ORDER BY expected_return limit 1;"
                  , db.getConnection()
                  );

                    DataSet dataSetIf = new DataSet();
                    dataAdapterIf.Fill(dataSetIf);
                    //dataGridView1.AutoGenerateColumns = false;

                    dataGridView1.DataSource = dataSetIf.Tables[0];

                }
               

                d.closeConnection();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _ = new WorkerMain { Visible = true };
            Visible = false;
        }
    }
}
