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

        public NationsForm()
        {
            InitializeComponent();
            MySqlUtils mysql = new MySqlUtils();
            
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
                    MessageBox.Show("Cập nhật thành công", "Inform");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại, chi tiết lỗi:\n" + ex.Message, "Error");
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

    }
}