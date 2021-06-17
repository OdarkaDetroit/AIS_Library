//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Library
//{
//    class TESTADDBOOK
//    {
//        private OleDbConnection connection = Login.connection;
//        private int sh = 0;

//        public AddBook()
//        {
//            InitializeComponent();
//        }

//        private void AddBook_Load(object sender, EventArgs e)
//        {
//            if (AddMessage.AddBook == false)
//                textBoxBookID.Text = AddMessage.BookID;
//            updateComboBox();

//        }

//        void updateComboBox()
//        {
//            var cmd = new OleDbCommand("SELECT publish_city, publishing FROM Book", connection);
//            var cmd1 = new OleDbCommand("SELECT sphere_name FROM Sphere", connection);
//            var cmd2 = new OleDbCommand("SELECT last_name FROM Author", connection);
//            try
//            {
//                comboBoxAuthor.Items.Clear();
//                comboBoxPubCity.Items.Clear();
//                comboBoxPubName.Items.Clear();
//                comboBoxSphere.Items.Clear();
//                connection.Open();
//                OleDbDataReader reader = cmd.ExecuteReader();
//                OleDbDataReader reader1 = cmd1.ExecuteReader();
//                OleDbDataReader reader2 = cmd2.ExecuteReader();
//                while (reader.Read())
//                {
//                    if (!comboBoxPubCity.Items.Contains(reader.GetString(0)))
//                        comboBoxPubCity.Items.Add(reader.GetString(0));

//                    if (!comboBoxPubName.Items.Contains(reader.GetString(1)))
//                        comboBoxPubName.Items.Add(reader.GetString(1));

//                }
//                while (reader1.Read())
//                    if (!comboBoxSphere.Items.Contains(reader1.GetString(0)))
//                        comboBoxSphere.Items.Add(reader1.GetString(0));

//                while (reader2.Read())
//                    if (!comboBoxAuthor.Items.Contains(reader2.GetString(0)))
//                        comboBoxAuthor.Items.Add(reader2.GetString(0));
//                connection.Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }


//        private void buttonReturn_Click(object sender, EventArgs e)
//        {
//            this.Close();
//            var log = new AdminMain();
//            log.Show();
//        }

//        private void buttonAdd_Click(object sender, EventArgs e)
//        {
//            if (textBoxBookID.Text == "" || textBoxName.Text == "" || comboBoxPubCity.Text == "" ||
//                comboBoxPubName.Text == "" || textBoxPubYear.Text == "" || textBoxPrice.Text == ""
//                || textBoxPrice.Text == "" || comboBoxAuthor.Text == "" || comboBoxSphere.Text == "")
//                MessageBox.Show("Not all the required info is entered");
//            else
//            {
//                connection.Open();
//                var cmd = new OleDbCommand(
//                    "INSERT INTO Book (book_id, book_name, publish_city, publishing, publish_year, page_num, price) " +
//                    "VALUES ('" + textBoxBookID.Text + "','" + textBoxName.Text + "','"
//                    + comboBoxPubCity.Text + "','" + comboBoxPubName.Text + "'," + textBoxPubYear.Text + ","
//                    + textBoxPageNum.Text + "," + textBoxPrice.Text + ")", connection);
//                cmd.ExecuteNonQuery();
//                var authors = comboBoxAuthor.Text.Replace(" ", "").Split(',');
//                //check if author in database
//                foreach (string author in authors)
//                {
//                    string l_n = "";
//                    var data = new OleDbCommand(@"SELECT last_name
//                                              FROM Author WHERE last_name = '" + author + "'", connection);
//                    OleDbDataReader reader = data.ExecuteReader();
//                    while (reader.Read())
//                        l_n += reader.GetString(0);
//                    if (l_n == "")
//                    {
//                        cmd = new OleDbCommand(
//                            "INSERT INTO Author (last_name) " +
//                            "VALUES ('" + author + "')", connection);
//                        cmd.ExecuteNonQuery();

//                        cmd = new OleDbCommand(
//                            "INSERT INTO Authorship (book_id, last_name) " +
//                            "VALUES ('" + textBoxBookID.Text + "','" + author + "')", connection);
//                        cmd.ExecuteNonQuery();
//                    }
//                    else
//                    {
//                        cmd = new OleDbCommand(
//                            "INSERT INTO Authorship (book_id, last_name) " +
//                            "VALUES ('" + textBoxBookID.Text + "','" + author + "')", connection);
//                        cmd.ExecuteNonQuery();
//                    }
//                }


//                var spheres = comboBoxSphere.Text.Replace(" ", "").Split(',');
//                //check if sphere in database
//                foreach (string sphere in spheres)
//                {
//                    string s_n = "";
//                    var dataS = new OleDbCommand(@"SELECT sphere_name
//                                              FROM Sphere WHERE sphere_name = '" + comboBoxSphere.Text + "'", connection);
//                    OleDbDataReader reader2 = dataS.ExecuteReader();
//                    while (reader2.Read())
//                        s_n += reader2.GetString(0);
//                    if (s_n == "")
//                    {
//                        cmd = new OleDbCommand(
//                            "INSERT INTO Sphere (sphere_name) " +
//                            "VALUES ('" + comboBoxAuthor.Text + "')", connection);
//                        cmd.ExecuteNonQuery();

//                        dataS = new OleDbCommand(@"SELECT sphere_id
//                                              FROM Sphere WHERE sphere_name = '" + comboBoxSphere.Text + "'", connection);
//                        var reader3 = dataS.ExecuteReader();
//                        //string sss = "";
//                        while (reader3.Read())
//                            s_n += reader3.GetInt32(0).ToString();
//                    }
//                    cmd = new OleDbCommand(
//                            "INSERT INTO Belongs (sphere_id, book_id) " +
//                            "VALUES (" + s_n + ",'" + textBoxBookID.Text + "')", connection);
//                    cmd.ExecuteNonQuery();
//                }
//                    else
//                {
//                    dataS = new OleDbCommand(@"SELECT sphere_id
//                                              FROM Sphere WHERE sphere_name = '" + comboBoxSphere.Text + "'", connection);
//                    var reader3 = dataS.ExecuteReader();
//                    var f = "";
//                    while (reader3.Read())
//                        f = reader3.GetInt32(0).ToString();
//                    cmd = new OleDbCommand(
//                        "INSERT INTO Belongs (sphere_id, book_id) " +
//                        "VALUES ('" + f + "','" + textBoxBookID.Text + "')", connection);
//                    cmd.ExecuteNonQuery();
//                }
//            }
//            sh++;
//            cmd = new OleDbCommand(
//                        "INSERT INTO Copy (shelf, book_id) " +
//                        "VALUES (" + sh + ",'" + textBoxBookID.Text + "')", connection);

//            cmd.ExecuteNonQuery();
//            connection.Close();
//            updateComboBox();
//        }
//    }
//}

