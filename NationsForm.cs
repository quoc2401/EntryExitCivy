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
    public partial class NationsForm : Form
    {
        float firstWidth;
        float firstHeight;

        public NationsForm()
        {
            InitializeComponent();
            MySqlUtils mysql = new MySqlUtils();
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;
        }


        private void NationsForm_Load(object sender, EventArgs e)
        {
            var nations = MySqlUtils.GetNations();
            dgridNation.DataSource = nations;
            dgridNation.Columns["id"].HeaderText = "Mã quốc gia";
            dgridNation.Columns["name"].HeaderText = "Tên quốc gia";
            dgridNation.Columns["id"].Width = 110;
            dgridNation.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }


        private void dgridNation_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable changes = ((DataTable)dgridNation.DataSource).GetChanges();
                if (changes != null)
                {
                    MySqlUtils.UpdateNation(changes);
                    ((DataTable)dgridNation.DataSource).AcceptChanges();
                    MessageBox.Show("Cập nhật thành công");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại, chi tiết lỗi:\n" + ex.Message);
            }
        }


        private void miNation_Click(object sender, EventArgs e)
        {
            NationsForm nextForm = new NationsForm();
            nextForm.Size = this.Size;
            nextForm.Top = this.Top;
            nextForm.Left = this.Left;
            nextForm.WindowState = this.WindowState;
            this.Hide();
            nextForm.ShowDialog();
            this.Close();
        }


        private void NationsForm_Resize(object sender, EventArgs e)
        {
            float size1 = this.Size.Width / firstWidth;
            float size2 = this.Size.Height / firstHeight;

            SizeF scale = new SizeF(size1, size2);
            firstWidth = this.Size.Width;
            firstHeight = this.Size.Height;

            dgridNation.Scale(scale);
        }


    }
}
