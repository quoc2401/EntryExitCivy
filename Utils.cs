using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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
    }
}
