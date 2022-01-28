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
            Utils.menuClick(this, miNation);
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Utils.menuClick(this, miExit);
        }

        private void miEntry_Click(object sender, EventArgs e)
        {
            Utils.menuClick(this, miEntry);
        }



        private void dgridNation_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgridNation.ClearSelection();
        }

        private void label2_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.label2.ClientRectangle, Color.WhiteSmoke, ButtonBorderStyle.Solid);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void miExit_Click(object sender, EventArgs e)
        {
            label2.Text = "DANH SÁCH XUẤT CẢNH";
            var exits = MySqlUtils.GetExits();
            dgridNation.DataSource = exits;
            dgridNation.Columns["civy_id"].HeaderText = "Số hộ chiếu";
            dgridNation.Columns["depart_date"].HeaderText = "Ngày đi";
            dgridNation.Columns["destination"].HeaderText = "Nơi đi kiến đến";
            dgridNation.Columns["visa_expiration"].HeaderText = "Hạn visa";
            dgridNation.Columns["passport_expiration"].HeaderText = "Hạn hộ chiếu";
            dgridNation.Columns["purpose"].HeaderText = "Mục đích";
            dgridNation.Columns["civy_id"].Width = 100;
            dgridNation.Columns["depart_date"].Width = 80;
            dgridNation.Columns["destination"].Width = 250;
            dgridNation.Columns["visa_expiration"].Width = 80;
            dgridNation.Columns["passport_expiration"].Width = 100;
            dgridNation.Columns["purpose"].Width = 100;
        }

        private void dgridNation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void miEntry_Click(object sender, EventArgs e)
        {
            label2.Text = "DANH SÁCH NHẬP CẢNH";
            var entrys = MySqlUtils.GetEntrys();
            dgridNation.DataSource = entrys;
            dgridNation.Columns["civy_id"].HeaderText = "Số hộ chiếu";
            dgridNation.Columns["arrival_date"].HeaderText = "Ngày đến";
            dgridNation.Columns["expected_destination"].HeaderText = "Nơi đến dự kiến ";
            dgridNation.Columns["visa_expiration"].HeaderText = "Hạn visa";
            dgridNation.Columns["passport_expiration"].HeaderText = "Hạn hộ chiếu";
            dgridNation.Columns["purpose"].HeaderText = "Mục đích";
            dgridNation.Columns["civy_id"].Width = 100;
            dgridNation.Columns["arrival_date"].Width = 80;
            dgridNation.Columns["expected_destination"].Width = 250;
            dgridNation.Columns["visa_expiration"].Width = 80;
            dgridNation.Columns["passport_expiration"].Width = 100;
            dgridNation.Columns["purpose"].Width = 100;
        }
    }
}