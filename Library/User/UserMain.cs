using Library.Entrance;
using Library.User;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ = new ViewGenre { Visible = true };
            Visible = false;
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
            _ = new MyBooks { Visible = true };
            Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _ = new MyBookHistory { Visible = true };
            Visible = false;
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

            db.closeConnection();
        }
    }
}
