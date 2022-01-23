using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EntryExitCivy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MySqlUtils mysql = new MySqlUtils();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string test = txtTest.Text;
            
            try
            {
                MySqlUtils.AddTest(test);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(text: ex.Message, caption: "Error");
            } 
        }
    }
}
