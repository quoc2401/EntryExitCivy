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

        private void ExitForm_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Id", typeof(string));
            table.Columns.Add("Name", typeof(string));

            table.Rows.Add("A1234", "Ho Nguyen Cong Sang");
            table.Rows.Add("A1235", "Ho Cong Hoang");
            table.Rows.Add("A1237", "Haha");

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
            try
            {
                string passport_no = txtPassport.Text;
                string name = txtName.Text;
                bool gender = rdbMale.Checked ? true : false;
                DateTime birthday = dtpBirthday.Value;
                string nationality = cbNationality.SelectedValue.ToString();
                string phone = txtPhone.Text;
                string address = txtAddress.Text;
                string occupation = txtOccupation.Text;
                DateTime depart_date = dtpDepartDate.Value;
                string destination = txtDestination.Text;
                DateTime visa_expriration = dtpVisaExpire.Value;
                DateTime passport_expriration = dtpPassportExpire.Value;
                Purpose purpose = (Purpose)Enum.Parse(typeof(Purpose), cbPurpose.Text, true);

                Civy c = new Civy(id: passport_no, fullname: name, gender: gender, birthday: birthday
                                 , nationality: nationality, phone: phone, home_address: address, occupation: occupation);

                Exit ex = new Exit(civy_id: passport_no, depart_date: depart_date, visa_expiration: visa_expriration
                                    , passport_expiration: passport_expriration, purpose: purpose, destination: destination);

                string exist = MySqlUtils.CivyExist(passport_no);

                if (exist == passport_no)
                {

                    MySqlUtils.AddExit(ex);
                }
                else
                {
                    MySqlUtils.AddNewCivy(c);
                    MySqlUtils.AddExit(ex);
                }
                MessageBox.Show(text: "Thêm thành công!", caption: "Inform");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(text: ex.Message, caption: "Error");
                MySqlUtils.CloseConn();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < exitData.SelectedRows.Count; i++)
                {
                    int selectedIndex = exitData.SelectedRows[i].Index;
                    string id = exitData[0, selectedIndex].Value.ToString();
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

    }
}
