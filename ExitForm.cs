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
    public partial class ExitForm : Form
    {
        DataTable table = new DataTable();
        public ExitForm()
        {
            InitializeComponent();
            MySqlUtils my = new MySqlUtils();
        }
        private void dgridNation_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            exitData.ClearSelection();
        }
        private void dgridNation_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable changes = ((DataTable)exitData.DataSource).GetChanges();
                if (changes != null)
                {
                    MySqlUtils.UpdateNation(changes);
                    ((DataTable)exitData.DataSource).AcceptChanges();
                    MessageBox.Show("Cập nhật thành công", "Inform");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại, chi tiết lỗi:\n" + ex.Message, "Error");
            }
        }
        private void ExitForm_Load(object sender, EventArgs e)
        {
            var exits = MySqlUtils.GetExits();
            exitData.DataSource = exits;
            exitData.Columns["civy_id"].HeaderText = "Số hộ chiếu";
            exitData.Columns["depart_date"].HeaderText = "Ngày đi";
            exitData.Columns["destination"].HeaderText = "Nơi đi kiến đến";
            exitData.Columns["visa_expiration"].HeaderText = "Hạn visa";
            exitData.Columns["passport_expiration"].HeaderText = "Hạn hộ chiếu";
            exitData.Columns["purpose"].HeaderText = "Mục đích";
            exitData.Columns["civy_id"].Width = 100;
            exitData.Columns["depart_date"].Width = 80;
            exitData.Columns["destination"].Width = 250;
            exitData.Columns["visa_expiration"].Width = 80;
            exitData.Columns["passport_expiration"].Width = 100;
            exitData.Columns["purpose"].Width = 100;
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void txtPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsNumber(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void txtOccupation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void txtDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                return;
            }
            e.Handled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string passport_no = txtPassport.Text;
            string name = Utils.ChuanHoa(txtName.Text);
            string gender = rdbMale.Checked ? "Nam" : "Nữ";
            string birthday = dtpBirthday.Value.ToString("yyyy-MM-dd");
            string nationality = cbNationality.Text;
            string phone = txtPhone.Text;
            string address = Utils.ChuanHoa(txtAddress.Text);
            string occupation = Utils.ChuanHoa(txtOccupation.Text);
            string departure_day = dtpDepartDate.Value.ToString("yyyy-MM-dd");
            string destination = Utils.ChuanHoa(txtDestination.Text);
            string visa_expriration = dtpVisaExpire.Value.ToString("yyyy-MM-dd");
            string passport_expriration = dtpPassportExpire.Value.ToString("yyyy-MM-dd");
            string purpose = cbPurpose.Text;

            string exist = MySqlUtils.CivyExist(passport_no);
            try
            {
                if (exist == passport_no)
                {
                    
                    //MySqlUtils.AddExit(passport_no, departure_day, destination, visa_expriration, passport_expriration, purpose);
                }
                else
                {
                    //MySqlUtils.AddNewCivy(passport_no, name, gender, birthday, "VN", phone, address, occupation);
                    //MySqlUtils.AddExit(passport_no, departure_day, destination, visa_expriration, passport_expriration, purpose);
                }
                MessageBox.Show(text: "Thêm thành công!", caption: "Inform");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(text: ex.Message, caption: "Error");
            } 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < exitData.SelectedRows.Count; i++)
                {
                    int selectedIndex = exitData.SelectedRows[i].Index;
                    int id = int.Parse(exitData[0, selectedIndex].Value.ToString());
                    MySqlUtils.DeleteExit(id);
                }
                MessageBox.Show(text: "Xóa thành công!", caption: "Inform");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(text: ex.Message, caption: "Error");
            } 
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.WhiteSmoke, ButtonBorderStyle.Solid);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void exitData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
