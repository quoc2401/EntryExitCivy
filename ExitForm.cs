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
        Button btnEdit, btnUnselect, btnDelete;
        DataTable table = new DataTable();
        public ExitForm()
        {
            InitializeComponent();
            MySqlUtils my = new MySqlUtils();

            btnEdit = new Button();
            btnEdit.Text = "Chỉnh sửa";
            btnEdit.Location = new Point(btnAdd.Location.X - 150, btnReset.Location.Y);
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
            btnDelete.Location = new Point(btnEdit.Location.X - 150, btnAdd.Location.Y);
            btnDelete.Size = btnAdd.Size;
            btnDelete.Click += btnDelete_Click;

            this.Controls.Add(btnEdit);
            this.Controls.Add(btnUnselect);
            this.Controls.Add(btnDelete);

            Utils.FormatButtons(this);

            btnEdit.Hide();
            btnUnselect.Hide();
            btnDelete.Hide();
        }

        private void ExitForm_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Passport", typeof(string));
            table.Columns.Add("Name", typeof(string));

            table.Rows.Add(1234, "A1234", "Ho Nguyen Cong Sang");
            table.Rows.Add(1235, "A1235", "Ho Cong Hoang");
            table.Rows.Add(1237, "A1237", "Haha");

            exitData.DataSource = table;

            try
            {
                DataTable data = MySqlUtils.GetNationsItems();
                Utils.AddComboBoxItems(cbNationality, data);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(text: ex.Message, caption: "Error");
            }

            cbPurpose.DataSource = Enum.GetValues(typeof(Purpose));
        }


        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.LetterOnly(e);
        }

        private void txtPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.LetterAndNumber(e);
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.NumberOnly(e);
        }

        private void txtOccupation_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.LetterOnly(e);
        }

        private void txtDestination_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.LetterOnly(e);
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            Utils.Clear(groupBox1);
        }

        private void btnUnselect_Click(object sender, EventArgs e)
        {
            btnDelete.Hide();
            btnEdit.Hide();
            btnUnselect.Hide();

            btnAdd.Show();
            btnReset.Show();

            foreach (var t in this.Controls.OfType<TextBox>())
                t.Text = "";
            foreach (var c in this.Controls.OfType<ComboBox>())
                c.SelectedIndex = 0;

            exitData.ClearSelection();
        }


        private void exitData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdd.Hide();
            btnReset.Hide();
            btnEdit.Show();
            btnDelete.Show();
            btnUnselect.Show();
        }


        private void miEntry_Click(object sender, EventArgs e)
        {
            Utils.menuClick(this, miEntry);
        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Utils.menuClick(this, miExit);
        }

        private void miNation_Click(object sender, EventArgs e)
        {
            Utils.menuClick(this, miNation);
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.WhiteSmoke, ButtonBorderStyle.Solid);
        }


        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.LetterAndNumber(e);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            Utils.RemovePlaceholder((TextBox)sender);
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            Utils.AddPlaceholder((TextBox)sender);
        }


        private void exitData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            exitData.ClearSelection();
        }

    }
}
