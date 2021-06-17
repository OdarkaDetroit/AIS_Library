using Library.Entrance;
using Library.User;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    
            DBConnection d = new DBConnection();

            string query = " SELECT catalogue_name " +
                " FROM system_catalogue" +
                " where id_catalogue IN(" +
                "     select id_catalogue" +
                "         from book_catalogue_connet " +
                "         where id_book in(" +
                "         select fk_book " +
                "                 from exemplar " +
                "                 where id_exemplar not in" +
                "             (select old_exemp" +
                "                         from changes)))";

            MySqlCommand sqlCommand = new MySqlCommand(query, d.getConnection());

            string qAuthor = " SELECT* FROM authors" +
                            " where second_name in(" +
                            " 		select ppk_author" +
                            "         from author_book_connect" +
                            "         where ppk_book in(" +
                            " 				select fk_book " +
                            "                 from exemplar " +
                            "                 where id_exemplar not in" +
                            " 						(select old_exemp" +
                            "                         from changes)));";
            MySqlCommand sqlAuthor = new MySqlCommand(qAuthor, d.getConnection());

            d.openConnection();

            MySqlDataAdapter sdr = new MySqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            sdr.Fill(dt);

            comboBox1.DataSource = dt;
            //comboBox1.DisplayMember = "catalogue_name";
            comboBox1.ValueMember = "catalogue_name";

            //////////////////////////////////////////////////

            string queryAuthor = " SELECT* FROM authors" +
                            " where second_name in(" +
                            " 		select ppk_author" +
                            "         from author_book_connect" +
                            "         where ppk_book in(" +
                            " 				select fk_book " +
                            "                 from exemplar " +
                            "                 where id_exemplar not in" +
                            " 						(select old_exemp" +
                            "                         from changes)));";

            MySqlCommand sqlCommandAuthor = new MySqlCommand(queryAuthor, d.getConnection());

            //d.openConnection();

            MySqlDataAdapter sdrAuthor = new MySqlDataAdapter(sqlCommandAuthor);
            DataTable dtAuthor = new DataTable();
            sdrAuthor.Fill(dtAuthor);

            comboBox2.DataSource = dtAuthor;
            //comboBox1.DisplayMember = "catalogue_name";
            comboBox2.ValueMember = "second_name";

            /////////////////////////
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
            comboBox3.DataSource = dtBook;
            //comboBox1.DisplayMember = "catalogue_name";
            comboBox3.ValueMember = "book_name";


            d.closeConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (" SELECT catalogue_name as назва_жанру" +
                " FROM system_catalogue" +
                " where id_catalogue IN(" +
                "     select id_catalogue" +
                "         from book_catalogue_connet " +
                "         where id_book in(" +
                "         select fk_book " +
                "                 from exemplar " +
                "                 where id_exemplar not in" +
                "             (select old_exemp" +
                "                         from changes)))", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            db.closeConnection();
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
            int userId = SignIn.userId;
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (" select expected_return, book_name, publishing_city, " +
                " publiser_name, publishing_date, pages_num, price" +
                " from (book inner join exemplar on fk_book=id_book) " +
                " inner join borrowing on id_exemplar=ppk_exemplar" +
                $" where ppk_reader='{userId}' and real_return is null", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            MySqlCommand authorCom = new MySqlCommand(" select id_exemplar,expected_return, book_name, publishing_city, " +
                " publiser_name, publishing_date, pages_num, price" +
                " from (book inner join exemplar on fk_book=id_book) " +
                " inner join borrowing on id_exemplar=ppk_exemplar" +
                $" where ppk_reader='{userId}' and real_return is null", db.getConnection());
            int authorcheck = (int)authorCom.ExecuteScalar();

            if (authorcheck == null)
            {

                MessageBox.Show("Зараз у вас немає ніяких книг!");
            }

            db.closeConnection();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int userId = SignIn.userId;
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (" select exodused,real_return, book_name, publishing_city, " +
                " publiser_name, publishing_date, pages_num, price" +
                " from (book inner join exemplar on fk_book=id_book) " +
                " inner join borrowing on id_exemplar=ppk_exemplar" +
                $" where ppk_reader='{userId}' and real_return is not null", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            MySqlCommand authorCom = new MySqlCommand(" select id_exemplar,exodused,real_return, book_name, publishing_city, " +
                " publiser_name, publishing_date, pages_num, price" +
                " from (book inner join exemplar on fk_book=id_book) " +
                " inner join borrowing on id_exemplar=ppk_exemplar" +
                $" where ppk_reader='{userId}' and real_return is not null", db.getConnection());
            int authorcheck = (int)authorCom.ExecuteScalar();

            if (authorcheck == null)
            {

                MessageBox.Show("Зараз у вас немає ніяких книг!");
            }

            db.closeConnection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            int s=SignIn.userId;

            db.openConnection();
            string name = SignIn.userSecN;

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(
            
                "select distinct id_book, book_name, second_name, initials \n" +
                "from ((exemplar inner join book on fk_book=id_book)\n" +
                "left join author_book_connect on ppk_book=id_book)\n" +
                "left join authors on ppk_author=second_name\n" +
                "where id_exemplar not in(select ppk_exemplar\n" +
                "                         from borrowing\n" +
                "                         where real_return is null)\n" +
                "and id_exemplar not in(select old_exemp from changes)\n" +
                "and fk_book in\n" +
                "  (select id_book \n" +
                "  from book_catalogue_connet\n" +
                "  where id_catalogue in\n" +
                "    (select id_catalogue\n" +
                "    from book_catalogue_connet\n" +
                "    where id_book in\n" +
                "      (select fk_book\n" +
                "      from (reader inner join borrowing on id_reader=ppk_reader) \n" +
                "      inner join exemplar ON id_exemplar=ppk_exemplar\n" +
                $"      where sec_name='{name}' and real_return IS Null\n" +
                "       )\n" +
                "       )\n" +
                "    );", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            MySqlCommand authorCom = new MySqlCommand("select distinct id_book, book_name, second_name, initials \n" +
                "from ((exemplar inner join book on fk_book=id_book)\n" +
                "left join author_book_connect on ppk_book=id_book)\n" +
                "left join authors on ppk_author=second_name\n" +
                "where id_exemplar not in(select ppk_exemplar\n" +
                "                         from borrowing\n" +
                "                         where real_return is null)\n" +
                "and id_exemplar not in(select old_exemp from changes)\n" +
                "and fk_book in\n" +
                "  (select id_book \n" +
                "  from book_catalogue_connet\n" +
                "  where id_catalogue in\n" +
                "    (select id_catalogue\n" +
                "    from book_catalogue_connet\n" +
                "    where id_book in\n" +
                "      (select fk_book\n" +
                "      from (reader inner join borrowing on id_reader=ppk_reader) \n" +
                "      inner join exemplar ON id_exemplar=ppk_exemplar\n" +
                $"      where sec_name='{name}' and real_return IS Null\n" +
                "       )\n" +
                "       )\n" +
                "    );", db.getConnection());
            int authorcheck = (int)authorCom.ExecuteScalar();

            if (authorcheck == null )
            {

                MessageBox.Show("Зараз немає вільних книг цього жанру!");
            }

            db.closeConnection();
        }
        public int count1 = 0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String genre = comboBox1.SelectedValue.ToString();
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
               " SELECT book_name, publishing_city, publiser_name, publishing_date, pages_num, price, ppk_author " +
                " FROM((book inner join book_catalogue_connet on book.id_book = book_catalogue_connet.id_book)" +
                " inner join system_catalogue on system_catalogue.id_catalogue = book_catalogue_connet.id_catalogue )" +
                " inner join author_book_connect on book.id_book=ppk_book" +
                $" WHERE catalogue_name = '{genre}' and book.id_book in " +
                " (Select fk_book " +
                " From exemplar " +
                " Where id_exemplar not in " +
                " (Select ppk_exemplar " +
                " From borrowing " +
                " Where real_return is null)" +
                " and id_exemplar not in (" +
                " select old_exemp" +
                " from changes))" , db.getConnection()
                );

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            MySqlCommand authorCom = new MySqlCommand(" SELECT book_name as назва_книги " +
                "  FROM(book inner join book_catalogue_connet on book.id_book = book_catalogue_connet.id_book)" +
                "  inner join system_catalogue on system_catalogue.id_catalogue = book_catalogue_connet.id_catalogue " +
                $"  WHERE catalogue_name = '{genre}' and book.id_book in " +
                "  (Select fk_book " +
                "  From exemplar " +
                "  Where id_exemplar not in " +
                "  (Select ppk_exemplar " +
                "  From borrowing " +
                "  Where real_return is null)" +
                "  and id_exemplar not in (" +
                "  select old_exemp" +
                "  from changes))", db.getConnection());
            string authorcheck = (string)authorCom.ExecuteScalar();

            if (authorcheck == null && count1 > 0)
            {

               MessageBox.Show("Зараз немає вільних книг цього жанру!");
            }
            if (authorcheck == null) { count1++; }

            db.closeConnection();
        }
        public int count = 0;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            String author = comboBox2.SelectedValue.ToString();
            DBConnection db = new DBConnection();

            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(
       "                  SELECT book_name, publishing_city, publiser_name, publishing_date, pages_num, price" +
        "                 FROM book" +
        "                 WHERE id_book IN " +
        "                 (SELECT ppk_book " +
        "                 FROM author_book_connect " +
        $"                 WHERE ppk_author = '{author}') " +
        "                 and id_book in" +
        "                 (Select fk_book " +
        "                 From exemplar " +
        "                 Where id_exemplar not in" +
        "                 ( Select ppk_exemplar " +
        "                 From borrowing " +
        "                 Where real_return is null)" +
        "                 and id_exemplar not in (" +
        " 							select old_exemp" +
        "                             from changes))", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            MySqlCommand authorCom = new MySqlCommand(" SELECT book_name " +
        "                 FROM book" +
        "                 WHERE id_book IN " +
        "                 (SELECT ppk_book " +
        "                 FROM author_book_connect " +
        $"                 WHERE ppk_author = '{author}') " +
        "                 and id_book in" +
        "                 (Select fk_book " +
        "                 From exemplar " +
        "                 Where id_exemplar not in" +
        "                 ( Select ppk_exemplar " +
        "                 From borrowing " +
        "                 Where real_return is null)" +
        "                 and id_exemplar not in (" +
        " 							select old_exemp" +
        "                             from changes))", db.getConnection());
            string authorcheck = (string)authorCom.ExecuteScalar();
         
                if (authorcheck == null && count >0) {
              
                    MessageBox.Show("Зараз у цього автора немає вільних книг!");
            }
            if (authorcheck == null) { count++; }

            db.closeConnection();
        }
        public int cBook=0;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            String book = comboBox3.SelectedValue.ToString();
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
               " SELECT id_exemplar ,book_name, publishing_city, publiser_name, publishing_date, pages_num, price, ppk_author" +
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
        public int numExemp=0;
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                try
                {
                    textBox1.Text = row.Cells["id_exemplar"].Value.ToString();
                    numExemp = int.Parse(row.Cells["id_exemplar"].Value.ToString());
                }
                catch { };

                
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (numExemp ==0) {
                MessageBox.Show("Оберіть екземпляр!");
            }
            else{
                DBConnection db = new DBConnection();
                db.openConnection();

                MySqlCommand command =
              new MySqlCommand(
                  "INSERT INTO borrowing (ppk_reader, ppk_exemplar, exodused, expected_return, real_return) VALUES " +
              "(@idRead, @id_exemp,@exodused ,@expected_return, @real_return)", db.getConnection());
                //SignIn.userId
                //command.Prepare();
                command.Parameters.AddWithValue("@idRead", SignIn.userId);
                command.Parameters.AddWithValue("@id_exemp", numExemp);
                command.Parameters.AddWithValue("@exodused", DateTime.Now.ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@expected_return", DateTime.Today.AddDays(14).ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@real_return", DBNull.Value);
                MySqlDataReader reader = command.ExecuteReader();

                MessageBox.Show("Ви взяли книгу на 2 тижні! ");
                  
                db.closeConnection();
            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (" Select count(*) as кількість, id_book,book_name, publishing_city, publiser_name, publishing_date, pages_num, price, ppk_author" +
                " from ((book inner join exemplar on fk_book=id_book) " +
                " inner join borrowing on ppk_exemplar=id_exemplar)" +
                " inner join author_book_connect on ppk_book=id_book" +
                " where id_exemplar not in(" +
                " select old_exemp" +
                " from changes)" +
                " group by id_book,book_name, publishing_city, publiser_name, publishing_date, pages_num, price, ppk_author" +
                " order by count(*) desc;", db.getConnection());

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
            MySqlCommand authorCom = new MySqlCommand(" Select count(*) as кількість, id_book,book_name, publishing_city, publiser_name, publishing_date, pages_num, price, ppk_author" +
                    " from ((book inner join exemplar on fk_book=id_book) " +
                    " inner join borrowing on ppk_exemplar=id_exemplar)" +
                    " inner join author_book_connect on ppk_book=id_book" +
                    " where id_exemplar not in(" +
                    " select old_exemp" +
                    " from changes)" +
                    " group by id_book,book_name, publishing_city, publiser_name, publishing_date, pages_num, price, ppk_author" +
                    " order by count(*) desc;", db.getConnection());
            Int64 authorcheck = (Int64)authorCom.ExecuteScalar();

            /*if (authorcheck == null)
            {

                MessageBox.Show("Зараз у вас немає ніяких книг!");
            }*/

            db.closeConnection();
        }

       
    }
}
