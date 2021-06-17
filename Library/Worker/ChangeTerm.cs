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
    public partial class ChangeTerm : Form
    {
        public ChangeTerm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            if (userName == "")
            {
                MessageBox.Show("Введіть прізвище читача!");
            }
            else
            {
                DBConnection db = new DBConnection();
                db.openConnection();

                MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                    (" Select id_exemplar, book_name,id_reader, sec_name, first_name, third_name, " +
                    " exodused,expected_return" +
                    " From (((reader as r inner join borrowing as b on r.id_reader=b.ppk_reader)" +
                    " inner join exemplar as e on e.id_exemplar=b.ppk_exemplar) " +
                    " inner join book on fk_book=id_book)" +
                    $" Where sec_name='{userName}' and real_return is null", db.getConnection());

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                dataGridView1.DataSource = dataSet.Tables[0];

                MySqlCommand authorCom = new MySqlCommand("select sec_name " +
                    " From (((reader as r inner join borrowing as b on r.id_reader=b.ppk_reader)" +
                    " inner join exemplar as e on e.id_exemplar=b.ppk_exemplar) " +
                    " inner join book on fk_book=id_book)" +
                    $" Where sec_name='{userName}' and real_return is null", db.getConnection());
                string authorcheck = (string)authorCom.ExecuteScalar();

                if (authorcheck == null)
                {

                    MessageBox.Show("Немає книг у цього читача!");
                }
                db.closeConnection();
            }
        }
        public int numExemp = 0;
        public int id_read = 0;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
       
                try
                {

                    textBox2.Text = row.Cells["id_exemplar"].Value.ToString();
                    numExemp = int.Parse(row.Cells["id_exemplar"].Value.ToString());
                    textBox3.Text = row.Cells["id_reader"].Value.ToString();
                    id_read = int.Parse(row.Cells["id_reader"].Value.ToString());

                }
                catch { };


            }
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
                  "update borrowing "+
                    "set expected_return = @exodused " +
                    " Where ppk_exemplar = @id_exemp and ppk_reader = @idRead ", db.getConnection());
                //SignIn.userId
                //command.Prepare();
                command.Parameters.AddWithValue("@idRead", id_read);
                command.Parameters.AddWithValue("@id_exemp", numExemp);
                command.Parameters.AddWithValue("@exodused", dateTimePicker1.Value.Date.ToString("yyyy/MM/dd"));
               
                MySqlDataReader reader = command.ExecuteReader();

                MessageBox.Show("Дату змінено! ");

                db.closeConnection();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _ = new WorkerMain { Visible = true };
            Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (numExemp == 0)
            {
                MessageBox.Show("Оберіть екземпляр!");
            }
            else if (id_read == 0)
            {
                MessageBox.Show("Оберіть читача!");
            }
            else
            {
                DBConnection db = new DBConnection();
                db.openConnection();

                MySqlCommand command =
              new MySqlCommand(
                  "update borrowing " +
                    "set real_return =CURDATE()" +
                    " Where ppk_exemplar = @id_exemp and ppk_reader = @idRead ", db.getConnection()) ;
                //SignIn.userId
                //command.Prepare();
                command.Parameters.AddWithValue("@idRead", id_read);
                command.Parameters.AddWithValue("@id_exemp", numExemp);
                //command.Parameters.AddWithValue("@exodused", dateTimePicker1.Value.Date.ToString("yyyy/MM/dd"));

                MySqlDataReader reader = command.ExecuteReader();

                MessageBox.Show("Книгу повернуто! ");

                db.closeConnection();
            }
        }
    }
}
