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
            string[] kq = test.Split(new char[] { ' ', '\n', '\t', '.' }, StringSplitOptions.RemoveEmptyEntries);
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


        public static void Clear(GroupBox g)
        {
            foreach(var t in g.Controls.OfType<TextBox>())
                t.Clear();
            foreach (var c in g.Controls.OfType<ComboBox>())
                c.SelectedIndex = 0;
            foreach (var d in g.Controls.OfType<DateTimePicker>())
                d.Value = DateTime.Now;        
        }

        public static void FormatButtons(Form f)
        {
            foreach (var b in f.Controls.OfType<Button>())
            {
                b.BackColor = Color.DarkCyan;
                b.Size = new Size(110, 40);
                b.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                b.Cursor = Cursors.Hand;
            }
        }

        public static void menuClick(Form f, ToolStripMenuItem mi)
        {
            var nextForm = new Form();
            switch (mi.Name.ToString())
            { 
                case "miNation":
                    nextForm = new NationsForm();
                    break;

                case "miExit":
                    nextForm = new ExitForm();
                    break;

                case "miEntry":
                    nextForm = new EntryForm();
                    break;

                default:
                    break;
            }
            
            nextForm.WindowState = f.WindowState;
            f.Hide();
            nextForm.ShowDialog();
            f.Close();  
        }

        public static void NumberOnly(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsNumber(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        public static void LetterAndNumber(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        public static void LetterOnly(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        public static void RemovePlaceholder(TextBox t)
        {
            if (t.Text == " Số hộ chiếu...")
            {
                t.ForeColor = Color.Black;
                t.Clear(); 
            }        
        }

        public static void AddPlaceholder(TextBox t)
        {
            if (t.Text == "")
            {
                t.ForeColor = Color.DarkGray;
                t.Text = " Số hộ chiếu...";
            }
        }

        public static DataTable SelectColumnExit()
        {
            string[] selectedColumns = new[] { "civy_id", "fullname", "depart_date", "destination", "visa_expiration",
                                               "passport_expiration", "purpose", "nationality", "gender", "birthday",
                                               "phone", "home_address", "occupation", "destination_id"};
            var exits = new DataView(MySqlUtils.GetExits()).ToTable(false, selectedColumns);

            return exits;
        }

        public static DataTable SelectColumnEntry()
        {
            string[] selectedColumns = new[] { "civy_id", "fullname", "arrival_date", "expected_destination", "visa_expiration",
                                               "passport_expiration", "purpose", "nationality", "gender", "birthday",
                                               "phone", "home_address", "occupation"};
            var entrys = new DataView(MySqlUtils.GetEntrys()).ToTable(false, selectedColumns);

            return entrys;
        }


    }
}

