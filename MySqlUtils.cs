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
                if (conn.State == ConnectionState.Closed)
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
                if (conn.State == ConnectionState.Open)
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

        public static DataTable AddExit (Exit e)
        {
            string query = "Insert into eedata.exit(civy_id, depart_date, destination, visa_expiration, passport_expiration, purpose)" +
                          "values('" + e.Civy_id + "','" + e.Depart_date.ToString("yyyy-MM-dd") + "','" + e.Destination + "','" + e.Visa_expiration.ToString("yyyy-MM-dd") + 
                          "','" + e.Passport_expiration.ToString("yyyy-MM-dd") + "','" + e.Purpose + "')";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            DataTable exits = Utils.SelectColumnExit();
            CloseConn();
            return exits;
        }

        public static DataTable AddEntry(Entry en)
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
            DataTable entry = Utils.SelectColumnEntry();
            CloseConn();
            return entry;
        }

        public static DataTable DeleteExit(string id, string depart_date)
        {
            string query = "Delete from eedata.exit where civy_id = '" + id + "' and depart_date = '" + depart_date + "';";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            DataTable exits = Utils.SelectColumnExit();
            CloseConn();
            return exits;
        }

        public static DataTable DeleteEntry(string id, string arrival_date)
        {
            string query = "Delete from eedata.entry where civy_id = '" + id + "' and arrival_date = '" + arrival_date + "';";
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            DataTable entry = Utils.SelectColumnEntry();
            CloseConn();
            return entry;
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

        public static DataTable GetEntrys()
        {
            string query = "SELECT e.*, c.* " +
                           "FROM eedata.entry e, eedata.civy c " +
                           "WHERE civy_id = c.id;";
            var entrys = new DataTable();

            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(entrys);
                mda.Dispose();
            }
            CloseConn();
            return entrys;
        }

        public static void UpdateEntrys(DataTable changes)
        {
            string query = "SELECT e.*, c.* " +
                           "FROM eedata.entry e, eedata.civy c " +
                           "WHERE civy_id = c.id;";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter mda = new MySqlDataAdapter();
            mda.SelectCommand = cmd;
            MySqlCommandBuilder mcb = new MySqlCommandBuilder(mda);
            mda.UpdateCommand = mcb.GetUpdateCommand();
            mda.Update(changes);
        }

        public static DataTable GetEntryItems()
        {
            string query = "Select * from eedata.entry";
            var entrys = new DataTable();

            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.SelectCommand = cmd;
                mda.Fill(entrys);
                mda.Dispose();
                cmd.Dispose();
            }
            CloseConn();
            return entrys;
        }
        public static DataTable GetExits()
        {
            string query = "SELECT civy_id, depart_date, n.name as 'destination', visa_expiration" +
                           ", passport_expiration, purpose, c.*, n.id as 'destination_id' " +
                           "FROM eedata.exit e, eedata.civy c, eedata.nation n " +
                           "WHERE civy_id = c.id and n.id = e.destination;";
            var exits = new DataTable();

            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(exits);
                mda.Dispose();
            }
            CloseConn();
            return exits;
        }

        public static void UpdateExits(DataTable changes)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM eedata.exit", conn);
            MySqlDataAdapter mda = new MySqlDataAdapter();
            mda.SelectCommand = cmd;
            MySqlCommandBuilder mcb = new MySqlCommandBuilder(mda);
            mda.UpdateCommand = mcb.GetUpdateCommand();
            mda.Update(changes);
        }

        public static DataTable GetExitItems()
        {
            string query = "Select * from eedata.exit";
            var exits = new DataTable();

            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.SelectCommand = cmd;
                mda.Fill(exits);
                mda.Dispose();
                cmd.Dispose();
            }
            CloseConn();
            return exits;
        }

        public static DataTable UpdateEntryCivy(Civy c, Entry en)
        {
            string queryCivy = "update eedata.civy " +
                               "set id = '" + c.Id.ToString() + "', fullname = '" + c.Fullname + "', gender = '" + (c.Gender ? 1 : 0).ToString() +
                               "', birthday = '" + c.Birthday.ToString("yyyy-MM-dd") + "', nationality = '" + c.Nationality + "', phone = '" + c.Phone +
                               "', home_address = '" + c.Home_address + "', occupation = '" + c.Occupation + "' " +
                               "where id = '" + c.Id.ToString() + "';";

            string queryEntry = "update eedata.entry " +
                                "set civy_id = '" + en.Civy_id.ToString() + "', arrival_date = '" +
                                en.Arrival_date.ToString("yyyy-MM-dd") + "', expected_destination = '" + en.Expected_destination +
                                "', visa_expiration = '" + en.Visa_expiration.ToString("yyyy-MM-dd") + 
                                "', passport_expiration = '" + en.Passport_expiration.ToString("yyyy-MM-dd") + "', purpose = '" + en.Purpose.ToString() + "' " +
                                "where civy_id = '" + en.Civy_id.ToString() + "';";
            if (OpenConn())
            {
                MySqlCommand cmd1 = new MySqlCommand(queryCivy, conn);
                cmd1.ExecuteNonQuery();
                MySqlCommand cmd2 = new MySqlCommand(queryEntry, conn);
                cmd2.ExecuteNonQuery();
            }
            DataTable entry = Utils.SelectColumnEntry();
            CloseConn();
            return entry;
        }

        public static DataTable UpdateExitCivy(Civy c, Exit e)
        {
            string queryCivy = "update eedata.civy " +
                               "set id = '" + c.Id.ToString() + "', fullname = '" + c.Fullname + "', gender = '" + (c.Gender ? 1 : 0).ToString() +
                               "', birthday = '" + c.Birthday.ToString("yyyy-MM-dd") + "', nationality = '" + c.Nationality + "', phone = '" + c.Phone +
                               "', home_address = '" + c.Home_address + "', occupation = '" + c.Occupation + "' " +
                               "where id = '" + c.Id.ToString() + "';";
            string queryExit = "update eedata.exit " +
                               "set civy_id = '" + e.Civy_id.ToString() + "', depart_date = '" + 
                               e.Depart_date.ToString("yyyy-MM-dd") + "', destination = '" + e.Destination +
                               "', visa_expiration = '" + e.Visa_expiration.ToString("yyyy-MM-dd") +
                               "', passport_expiration = '" + e.Passport_expiration.ToString("yyyy-MM-dd") + "', purpose = '" + e.Purpose.ToString() + "' " +
                               "where civy_id = '" + e.Civy_id.ToString() + "';";

            if (OpenConn())
            {
                MySqlCommand cmd1 = new MySqlCommand(queryCivy, conn);
                cmd1.ExecuteNonQuery();
                MySqlCommand cmd2 = new MySqlCommand(queryExit, conn);
                cmd2.ExecuteNonQuery();
            }
            DataTable exit = Utils.SelectColumnExit();
            CloseConn();
            return exit;
        }

        public static DataTable SearchEntry(string passport)
        {
            string query = "SELECT e.*, c.* " +
                           "FROM eedata.entry e, eedata.civy c " +
                           "WHERE civy_id = id and civy_id like '%%" + passport + "%';";
            var entry = new DataTable();
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(entry);
                mda.Dispose();
            }
            CloseConn();
            return entry;
        }

        public static DataTable SearchExit(string passport)
        {
            string query = "SELECT e.*, c.* " +
                           "FROM eedata.exit e, eedata.civy c " +
                           "WHERE civy_id = id and civy_id like '%%" + passport + "%';";
            var exit = new DataTable();
            if (OpenConn())
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(exit);
                mda.Dispose();
            }
            CloseConn();
            return exit;
        }
    }
}