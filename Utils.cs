using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace EntryExitCivy
{
    class Utils
    {
        public static string ChuanHoa(string test)
        {
            string[] kq = test.Split(new char[] { ' ', '\n', '\t', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
            string s1 = "";
            for (int i = 0; i < kq.Length; i++)
            {
                string s = kq[i].Substring(0, 1).ToUpper() + kq[i].Substring(1).ToLower();
                
                s1 += s + " ";
            }
            return s1;
        }

        public static void AddComboBoxItems(ComboBox index, DataTable data)
        {          
            index.ValueMember = "id";
            index.DisplayMember = "name";
            index.DataSource = data;
        }
    }
}
