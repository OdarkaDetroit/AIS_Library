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
    public partial class AddBook : Form
    {
        public AddBook()
        {
            InitializeComponent();
        }

        private void allAuthors_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
                @"select second_name as 'Прізвище', initials as 'Ініціали', count(ppk_book) as 'Кількість книг'
                       from authors au left
                       join author_book_connect abc on au.second_name = abc.ppk_author
                       where ppk_book in
                            (select id_book
                            from book)
                group by second_name, initials; ", db.getConnection()
                );
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            authorGrid.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }

        private void allGenres_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
                "select id_catalogue, catalogue_name as 'Назва жанру' from system_catalogue;", db.getConnection()
                );
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            genreGrid.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }

        private void allBooks_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
                @"select id_book, book_name as 'Назва книги',
                    publishing_city as 'Місто', 
                    publiser_name as 'Видавництво',
                    publishing_date as 'Дата публікації',
                    pages_num as 'К-сть сторінок',
                    price as 'Ціна', 
                    count(id_exemplar) as 'К-сть примірників'
	                    from book s inner join exemplar ex on s.id_book = ex.fk_book
                 group by id_book;", db.getConnection()
                );
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            bookGrid.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }

        private void allItems_Click(object sender, EventArgs e)
        {
            DBConnection db = new DBConnection();
            db.openConnection();

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter
                (
                @"select id_exemplar,
                         shelf as 'Полиця', 
                         fk_book as 'ID книги',
                         book_name as 'Назва книги'
                      from exemplar ex inner join book b on b.id_book = ex.fk_book
                  group by id_exemplar;", db.getConnection()
                );
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            itemGrid.DataSource = dataSet.Tables[0];

            db.closeConnection();
        }
    }
}
