using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EntryExitCivy
{
    public partial class HomePage : Form
    {

        public HomePage()
        {
            InitializeComponent();
        }

        private void HomePage_Load(object sender, EventArgs e)
        {

        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            ExitForm exitform = new ExitForm();
            exitform.FormClosing += new FormClosingEventHandler(exitform_FormClosing);
            this.Hide();
            exitform.Show();
        }

        private void exitform_FormClosing(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnEntry_Click(object sender, EventArgs e)
        {
            EntryForm entryform = new EntryForm();
            entryform.FormClosing += new FormClosingEventHandler(entryform_FormClosing);
            this.Hide();
            entryform.Show();
        }

        private void entryform_FormClosing(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnNation_Click(object sender, EventArgs e)
        {
            NationsForm nationsform = new NationsForm();
            nationsform.FormClosing += new FormClosingEventHandler(nationsform_FormClosing);
            this.Hide();
            nationsform.Show();
        }

        private void nationsform_FormClosing(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
