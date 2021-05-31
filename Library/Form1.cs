using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Form1 : Form
    {

        Bitmap bitmap;

        MySqlConnection sqlCon = new MySqlConnection();
        MySqlCommand sqlCom1 = new MySqlCommand();
        MySqlCommand sqlCom2 = new MySqlCommand();

        DataTable sqlDt1 = new DataTable();
        DataTable sqlDt2 = new DataTable();

        MySqlDataReader sqlRd1;
        MySqlDataReader sqlRd2;

        String sqlQuery1;
        String sqlQuery2;
        MySqlDataAdapter DtA = new MySqlDataAdapter();

        DataSet DS = new DataSet();

        String server = "localhost";
        String username = "root";
        String password = "u8s8jDddds2_0";
        String database = "lib";


        public Form1()
        {
            InitializeComponent();
        }

        private void upLoadData2()
        {
            sqlCon.ConnectionString = "server=" + server + ";"
                                       + "user id=" + username + ";"
                                       + "password=" + password + ";"
                                       + "database=" + database;

            sqlCon.Open();
            sqlCom2.Connection = sqlCon;

            sqlCom2.CommandText = @"select distinct ppk_author, initials
                                    from author_book_connect inner join authors on second_name=ppk_author
                                    where not exists(
                                      select *
                                        from reader
                                        where (
                                        (YEAR(CURRENT_DATE) - YEAR(birth_date)) -
                                        (DATE_FORMAT(CURRENT_DATE, '%m%d') < DATE_FORMAT(birth_date, '%m%d')) 
                                       >18 and not exists(
                                        select *
                                            from ((borrowing inner join exemplar on id_exemplar=ppk_exemplar)
                                            inner join book on id_book =fk_book)
                                            inner join author_book_connect as a on id_book=ppk_book
                                        where a.ppk_author=author_book_connect.ppk_author
                                             and reader.id_reader=ppk_reader
                                            )));";

            sqlRd2 = sqlCom2.ExecuteReader();

            sqlDt2.Load(sqlRd2);

            sqlRd2.Close();

            sqlCon.Close();

            dataGridView2.DataSource = sqlDt2;
        }

        private void upLoadData1()
        {
            sqlCon.ConnectionString = "server=" + server + ";"
                           + "user id=" + username + ";"
                           + "password=" + password + ";"
                           + "database=" + database;

            sqlCon.Open();
            sqlCom1.Connection = sqlCon;

            sqlCom1.CommandText = @"select ppk_author, count(id_book)
                                    from author_book_connect inner join book
                                    on id_book = ppk_book
                                    where id_book in
                                    
                                    (select b.id_book
                                     from book_catalogue_connet as b
                                     where b.id_catalogue in 
                                     
                                     (select c.id_catalogue 
                                      from system_catalogue as c
                                      where catalogue_name = 'поезія'))

                                      group by ppk_author; ";

            sqlRd1 = sqlCom1.ExecuteReader();

            sqlDt1.Load(sqlRd1);

            sqlRd1.Close();

            sqlCon.Close();

            dataGridView1.DataSource = sqlDt1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            upLoadData1();
            upLoadData2();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
