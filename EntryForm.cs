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
    public partial class EntryForm : Form
    {
        float firstWidth;
        float firstHeight;


        public EntryForm()
        {
            InitializeComponent();
            MySqlUtils mysql = new MySqlUtils();
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            var nations = MySqlUtils.GetNations();
            dgridEntry.DataSource = nations;
            dgridEntry.Columns["id"].HeaderText = "Mã quốc gia";
            dgridEntry.Columns["name"].HeaderText = "Tên quốc gia";
            dgridEntry.Columns["id"].Width = 110;
            dgridEntry.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dgridEntry_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                DataGridViewRow row = this.dgridEntry.Rows[e.RowIndex];
                txtId.Text = row.Cells["id"].Value.ToString();
                txtNation.Text = row.Cells["name"].Value.ToString();
            }
        }
    }
}
