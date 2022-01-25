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
    public partial class EntryForm : Form
    {
        //Button btnEdit, btnUnselect, btnDelete;
        DataTable table = new DataTable();

        public EntryForm()
        {
            InitializeComponent();
            MySqlUtils mysql = new MySqlUtils();
            

            //btnEdit = new Button();
            //btnEdit.Text = "Chỉnh sửa";
            //btnEdit.Location = new Point(btnReset.Location.X - 20, btnReset.Location.Y);
            //btnEdit.Size = btnReset.Size;
            //btnEdit.Padding = new Padding(3);
            //btnEdit.Size = new Size(100, btnReset.Size.Height);

            //btnUnselect = new Button();
            //btnUnselect.Text = "Bỏ chọn";
            //btnUnselect.Location = btnAdd.Location;
            //btnUnselect.Size = btnAdd.Size;
            //btnUnselect.Click += btnUnselect_Click;

            //btnDelete = new Button();
            //btnDelete.Text = "Xóa";
            //btnDelete.Location = new Point(btnReset.Location.X - 110, btnAdd.Location.Y);
            //btnDelete.Size = btnAdd.Size;

            //this.Controls.Add(btnEdit);
            //this.Controls.Add(btnUnselect);
            //this.Controls.Add(btnDelete);

            //btnEdit.Hide();
            //btnUnselect.Hide();
            //btnDelete.Hide();

            foreach (Button b in this.Controls.OfType<Button>())
            {
                b.Cursor = Cursors.Hand;
            }
        }


        private void EntryForm_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Passport", typeof(string));
            table.Columns.Add("Name", typeof(string));

            table.Rows.Add(1234, "A1234", "Ho Nguyen Cong Sang");
            table.Rows.Add(1235, "A1235", "Ho Cong Hoang");

            entryData.DataSource = table;

            try
            {
                DataTable data = MySqlUtils.GetNationsItems();
                //Utils.AddComboBoxItems(cbNationality, data);
                cbNationality.ValueMember = "id";
                cbNationality.DisplayMember = "name";
                cbNationality.DataSource = data;
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
                DateTime arrival_day = dtpArrivalDate.Value;
                string expected_destination = txtExpectedDestination.Text;
                DateTime visa_expriration = dtpVisaExpire.Value;
                DateTime passport_expriration = dtpPassportExpire.Value;
                Purpose purpose = (Purpose)Enum.Parse(typeof(Purpose),cbPurpose.Text, true);

                Civy c = new Civy(id: passport_no, fullname: name, gender: gender, birthday: birthday
                                 , nationality: nationality, phone: phone, home_address: address, occupation: occupation);

                Entry en = new Entry(civy_id: passport_no, arrival_date: arrival_day, visa_expiration: visa_expriration
                                    , passport_expiration: passport_expriration, purpose: purpose
                                    , expected_destination:expected_destination);

                string exist = MySqlUtils.CivyExist(passport_no);
          
                if (exist == passport_no)
                {

                    MySqlUtils.AddEntry(en);
                }
                else
                {
                    MySqlUtils.AddNewCivy(c);
                    MySqlUtils.AddEntry(en);
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
                for (int i = 0; i < entryData.SelectedRows.Count; i++)
                {
                    int selectedIndex = entryData.SelectedRows[i].Index;
                    int id = int.Parse(entryData[0, selectedIndex].Value.ToString());
                    MySqlUtils.DeleteEntry(id);
                }
                MessageBox.Show(text: "Xóa thành công!", caption: "Inform");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(text: ex.Message, caption: "Error");
            } 
        }


        private void btnUnselect_Click(object sender, EventArgs e)
        {
            //btnDelete.Hide();
            //btnEdit.Hide();
            //btnUnselect.Hide();

            //btnAdd.Show();
            //btnReset.Show();

            //txtId.Text = "";
            //txtNation.Text = "";

            //dgridEntry.ClearSelection();
        }


        private void dgridEntry_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //dgridEntry.ClearSelection();        
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.WhiteSmoke, ButtonBorderStyle.Solid);
        }


        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    DateTime d = DateTime.ParseExact("2022-01-24", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        //    Console.WriteLine(d.ToString("yyyy-mm-dd"));
        //}
    }
}
