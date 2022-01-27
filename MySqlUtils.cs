using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

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

        public static bool OpenConn()
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


        public static bool CloseConn()
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

        public static string CivyExist(string passport_no)
        {
            string id = "";
            string query = "Select id from civy where id = '" + passport_no + "';";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = reader.GetValue(0).ToString();
                        }
                    }
                    else
                    {
                        id = "";
                    }
                }
            }
            CloseConn();

            return id;
        }

        public static void AddNewCivy(Civy c)
        {
            string query = "Insert into eedata.civy(id, fullname, gender, birthday, nationality, phone, home_address" +
                ", occupation) values('" + c.Id.ToString() + "','" + c.Fullname + "','" + (c.Gender ? 1 : 0).ToString() +
                           "','" + c.Birthday.ToString("yyyy-MM-dd") + "','" + c.Nationality + "','" + c.Phone +
                           "','" + c.Home_address + "','" + c.Occupation + "')";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            CloseConn();
        }

        public static void AddExit (Exit e)
        {
            string query = "Insert into eedata.exit(civy_id, depart_date, destination, visa_expiration, passport_expiration, purpose)" +
                          "values('" + e.Civy_id + "','" + e.Depart_date.ToString("yyyy-MM-dd") + "','" + e.Destination + "','" + e.Visa_expiration.ToString("yyyy-MM-dd") + 
                          "','" + e.Passport_expiration.ToString("yyyy-MM-dd") + "','" + e.Purpose + "')";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            CloseConn();
        }

        public static void AddEntry(Entry en)
        {
            string query = "Insert into eedata.entry(civy_id, arrival_date, expected_destination, visa_expiration," +
                           " passport_expiration, purpose) values('" + en.Civy_id.ToString() + "','" +
                           en.Arrival_date.ToString("yyyy-MM-dd")+ "','" + en.Expected_destination + "','" +
                           en.Visa_expiration.ToString("yyyy-MM-dd") + "','" +
                           en.Passport_expiration.ToString("yyyy-MM-dd") + "','" + en.Purpose.ToString() + "')";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            CloseConn();
        }

        public static void DeleteExit(string id)
        {
            string query = "Delete from eedata.exit where civy_id = '" + id + "';";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            CloseConn();
        }

        public static void DeleteEntry(string id)
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

        public static DataTable GetNationsItems()
        {
            string query = "Select * from eedata.nation";
            var nations = new DataTable();

            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.SelectCommand = cmd;
                mda.Fill(nations);
                mda.Dispose();
                cmd.Dispose();
            }
            CloseConn();
            return nations;
        }
    }
}