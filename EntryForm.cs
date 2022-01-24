﻿using System;
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
        DataTable table = new DataTable();
        public EntryForm()
        {
            InitializeComponent();
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
                DataSet data = MySqlUtils.GetNationsItems();
                Utils.AddComboBoxItems(cbNationality, data);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(text: ex.Message, caption: "Error");
            }

            cbPurpose.DataSource = Enum.GetValues(typeof(purpose));
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
            long passport_no = Convert.ToInt64(txtPassport.Text);
            string name = txtName.Text;
            string gender = rdbMale.Checked ? "Nam" : "Nữ";
            string birthday = dtpBirthday.Value.ToString("yyyy-MM-dd");
            string nationality = cbNationality.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;
            string occupation = txtOccupation.Text;
            string arrival_day = dtpArrivalDate.Value.ToString("yyyy-MM-dd");
            string expected_destination = txtExpectedDestination.Text;
            string visa_expriration = dtpVisaExpire.Value.ToString("yyyy-MM-dd");
            string passport_expriration = dtpPassportExpire.Value.ToString("yyyy-MM-dd");
            string purpose = cbPurpose.Text;

            long exist = MySqlUtils.CivyExist(passport_no);
            try
            {
                if (exist == passport_no)
                {
                    
                    MySqlUtils.AddEntry(passport_no, arrival_day, expected_destination, visa_expriration, passport_expriration, purpose);
                }
                else
                {
                    MySqlUtils.AddNewCivy(passport_no, name, gender, birthday, "VN", phone, address, occupation);
                    MySqlUtils.AddEntry(passport_no, arrival_day, expected_destination, visa_expriration, passport_expriration, purpose);
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
    }
}
