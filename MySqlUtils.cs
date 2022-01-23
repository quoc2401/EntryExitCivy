using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace EntryExitCivy
{
    class MySqlUtils
    {
        private static MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;


        public MySqlUtils()
        {
            Initialize();
        }


        private void Initialize()
        {
            server = "127.0.0.1";
            int port = 3306;
            database = "eedata";
            uid = "root";
            password = "quoc2401";

            // Connection String.
            string connString = "Server=" + server + ";Database=" + database
                + ";port=" + port + ";UID=" + uid + ";password=" + password;

            conn = new MySqlConnection(connString);
        }


        private static bool OpenConn()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }


        private static bool CloseConn()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public static void AddTest(string test)
        {
            string query = "Insert into test(name, name2, cate) values('" + test + "', '" + test + "', '" + purpose.TRAVEL.ToString() + "')";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            CloseConn();
        }


        public static DataTable GetNations()
        {
            string query = "Select * from nation";
            var nations = new DataTable();

            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //cmd.ExecuteNonQuery();
                //MySqlDataReader reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    var n = new Nation(reader.GetInt32(0), reader["name"].ToString());
                //    

                //    
                //} 
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(nations);
                mda.Dispose();
            }
            CloseConn();
            return nations;
        }

        public static void UpdateNation(DataTable changes)
        {
            MySqlCommand cmd = new MySqlCommand("Select * from nation;", conn);
            MySqlDataAdapter mda = new MySqlDataAdapter();
            mda.SelectCommand = cmd;
            MySqlCommandBuilder mcb = new MySqlCommandBuilder(mda);
            mda.UpdateCommand = mcb.GetUpdateCommand();
            mda.Update(changes);
        }
    }
}