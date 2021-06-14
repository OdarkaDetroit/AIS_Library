using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Library
{
    class DBConnection
    {
   //     private MySqlConnection connection;

        static String server = "localhost";
        static String username = "root";
        static String port = "3306";

        //for DASHA
        static String password = "u8s8jDddds2_0";
        static String database = "lib";

        //for OLHA
        static String password1 = "Oliakma2020";
        static String database1 = "lib";

        //      string connectionString;

        //      connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
        //database + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";";

        //      connection = new MySqlConnection(connectionString);

       /* private MySqlConnection connection = new MySqlConnection("server=" + server
                               + ";port=" + port
                               + ";user id=" + username + ";"
                               + "password=" + password + ";"
                               + "database=" + database);*/

        private MySqlConnection connection = new MySqlConnection("server=" + server
                       + ";port=" + port
                       + ";user id=" + username + ";"
                       + "password=" + password1 + ";"
                       + "database=" + database1);

        //private MySqlConnection connection = new MySqlConnection("server=localhost;username=root;password=u8s8jDddds2_0;database=lib");

        //connection.ConnectionString = "server=" + server + ";"
        //                               + "user id=" + username + ";"
        //                               + "password=" + password + ";"
        //                               + "database=" + database;

        public MySqlConnection getConnection()
        {
            return connection;
        }


        //openinig connection function
        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        //closing connection function
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
