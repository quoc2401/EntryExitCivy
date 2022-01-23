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
            server = "localhost";
            int port = 3306;
            database = "eedata";
            uid = "root";
            password = "15082001";

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

        public static void AddExit (string passport_no, string name, string gender, string birthday, string nationality,
                                    string phone, string address, string occupation, string departure_day,
                                    string destination, string visa_expiration, string passport_expiration, string purpose)
        {
            string query = "Insert into eedata.exit(civy_id, fullname, gender, birthday, nationality, phone, home_address, occupation, departure_day, destination, visa_expiration, passport_expiration, purpose)" +
                          "values('" + passport_no + "','" + name + "','" + gender + "','" + birthday + "','" + nationality + "','" + phone + "','" + address + "','" + occupation + "','" + departure_day + "','" + destination + "','" + visa_expiration + "','" + passport_expiration + "','" + purpose + "')";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            CloseConn();
        }

        public static void AddEntry(string passport_no, string name, string gender, string birthday, string nationality,
                                    string phone, string address, string occupation, string arrival_day,
                                    string expected_destination, string visa_expiration, string passport_expiration, string purpose)
        {
            string query = "Insert into eedata.entry(civy_id, fullname, gender, birthday, nationality, phone, home_address, occupation, arrival_day, expected_destination, visa_expiration, passport_expiration, purpose)" +
                          "values('" + passport_no + "','" + name + "','" + gender + "','" + birthday + "','" + nationality + "','" + phone + "','" + address + "','" + occupation + "','" + arrival_day + "','" + expected_destination + "','" + visa_expiration + "','" + passport_expiration + "','" + purpose + "')";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            CloseConn();
        }

        public static void DeleteExit(int id)
        {
            string query = "Delete from eedata.exit where civy_id = '" + id + "';";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            CloseConn();
        }

        public static void DeleteEntry(int id)
        {
            string query = "Delete from eedata.entry where civy_id = '" + id + "';";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            CloseConn();
        }

        public static DataTable GetNations()
        {
            string query = "Select * from eedata.nation";
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
            MySqlCommand cmd = new MySqlCommand("Select * from eedata.nation;", conn);
            MySqlDataAdapter mda = new MySqlDataAdapter();
            mda.SelectCommand = cmd;
            MySqlCommandBuilder mcb = new MySqlCommandBuilder(mda);
            mda.UpdateCommand = mcb.GetUpdateCommand();
            mda.Update(changes);
        }
    }
}