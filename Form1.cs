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

        private void miEntry_Click(object sender, EventArgs e)
        {
            EntryForm nextForm = new EntryForm();
            nextForm.Size = this.Size;
            nextForm.Top = this.Top;
            nextForm.Left = this.Left;
            nextForm.WindowState = this.WindowState;
            this.Hide();
            nextForm.ShowDialog();
            this.Close();
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            ExitForm nextForm = new ExitForm();
            nextForm.Size = this.Size;
            nextForm.Top = this.Top;
            nextForm.Left = this.Left;
            nextForm.WindowState = this.WindowState;
            this.Hide();
            nextForm.ShowDialog();
            this.Close();
        }

        private void miNation_Click(object sender, EventArgs e)
        {

        }

    }
}
