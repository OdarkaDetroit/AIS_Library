using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Library
{
    class DBConnection
    {
        static String server = "localhost";
        static String username = "root";
        static String port = "3306";
        static String password = "u8s8jDddds2_0";
        static String database = "lib";

        private MySqlConnection connection = new MySqlConnection("server=" + server 
                               + ";port=" + port
                               + ";user id=" + username + ";"
                               + "password=" + password + ";"
                               + "database=" + database);


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
