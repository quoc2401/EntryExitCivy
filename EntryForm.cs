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
        Button btnEdit, btnUnselect, btnDelete;


        public EntryForm()
        {
            InitializeComponent();
            MySqlUtils mysql = new MySqlUtils();
            

            btnEdit = new Button();
            btnEdit.Text = "Chỉnh sửa";
            btnEdit.Location = new Point(btnReset.Location.X - 20, btnReset.Location.Y);
            btnEdit.Size = btnReset.Size;
            btnEdit.Padding = new Padding(3);
            btnEdit.Size = new Size(100, btnReset.Size.Height);

            btnUnselect = new Button();
            btnUnselect.Text = "Bỏ chọn";
            btnUnselect.Location = btnAdd.Location;
            btnUnselect.Size = btnAdd.Size;
            btnUnselect.Click += btnUnselect_Click;

            btnDelete = new Button();
            btnDelete.Text = "Xóa";
            btnDelete.Location = new Point(btnReset.Location.X - 110, btnAdd.Location.Y);
            btnDelete.Size = btnAdd.Size;

            this.Controls.Add(btnEdit);
            this.Controls.Add(btnUnselect);
            this.Controls.Add(btnDelete);

            btnEdit.Hide();
            btnUnselect.Hide();
            btnDelete.Hide();

            foreach (Button b in this.Controls.OfType<Button>())
            {
                b.Cursor = Cursors.Hand;
            }
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgridEntry.Rows[e.RowIndex];
                txtId.Text = row.Cells["id"].Value.ToString();
                txtNation.Text = row.Cells["name"].Value.ToString();

                btnAdd.Hide();
                btnReset.Hide();

                btnDelete.Show();
                btnEdit.Show();
                btnUnselect.Show();
            }
        }


        private void btnUnselect_Click(object sender, EventArgs e)
        {
            btnDelete.Hide();
            btnEdit.Hide();
            btnUnselect.Hide();

            btnAdd.Show();
            btnReset.Show();

            txtId.Text = "";
            txtNation.Text = "";

            dgridEntry.ClearSelection();
        }


        private void dgridEntry_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgridEntry.ClearSelection();        
        }

        private void EntryForm_Resize(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
        
        }
    }
}
