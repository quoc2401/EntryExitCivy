﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

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
                    
        private void EntryForm_Load(object sender, EventArgs e)
        {
            var entrys = MySqlUtils.GetEntrys();
            string[] selectedColumns = new[] { "civy_id", "fullname", "arrival_date", "expected_destination", "visa_expiration",
                                               "passport_expiration", "purpose", "nationality", "gender", "birthday",
                                               "phone", "home_address", "occupation"};
            entrys = new DataView(entrys).ToTable(false, selectedColumns);
            entryData.DataSource = entrys;

            //đổi tên cột
            entryData.Columns["civy_id"].HeaderText = "Số hộ chiếu";
            entryData.Columns["fullname"].HeaderText = "Họ tên";
            entryData.Columns["arrival_date"].HeaderText = "Ngày đến";
            entryData.Columns["expected_destination"].HeaderText = "Nơi đến dự kiến";
            entryData.Columns["visa_expiration"].HeaderText = "Hạn visa";
            entryData.Columns["passport_expiration"].HeaderText = "Hạn hộ chiếu";
            entryData.Columns["purpose"].HeaderText = "Mục đích";

            //ẩn những cột không hiển thị nhưng vẫn dùng
            entryData.Columns["nationality"].HeaderText = "Quốc tịch";
            entryData.Columns["nationality"].Visible = false;
            entryData.Columns["gender"].HeaderText = "Giới tính";
            entryData.Columns["gender"].Visible = false;
            entryData.Columns["birthday"].HeaderText = "Ngày sinh";
            entryData.Columns["birthday"].Visible = false;
            entryData.Columns["phone"].HeaderText = "SĐT";
            entryData.Columns["phone"].Visible = false;
            entryData.Columns["home_address"].HeaderText = "Địa chỉ";
            entryData.Columns["home_address"].Visible = false;
            entryData.Columns["occupation"].HeaderText = "Nghề nghiệp";
            entryData.Columns["occupation"].Visible = false;

            //điều chỉnh chiều rộng cột
            entryData.Columns["civy_id"].Width = 100;
            entryData.Columns["fullname"].Width = 200;
            entryData.Columns["arrival_date"].Width = 80;
            entryData.Columns["expected_destination"].Width = 200;
            entryData.Columns["visa_expiration"].Width = 80;
            entryData.Columns["passport_expiration"].Width = 100;
            entryData.Columns["purpose"].Width = 100;

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


        //phần kiểm soát nhập liệu
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


        //phần buttons
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

                var entrys = MySqlUtils.GetEntrys();
                entryData.DataSource = entrys;
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
                    string id = entryData[0, selectedIndex].Value.ToString();
                    
                    DateTime arrival_date = DateTime.Parse(entryData[2, selectedIndex].Value.ToString());
                    string date = string.Format("{0:yyyy/MM/dd}", arrival_date);
                    MySqlUtils.DeleteEntry(id, date);
                }

                var entrys = MySqlUtils.GetEntrys();
                entryData.DataSource = entrys;
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
            rdbMale.Select();
        }

        private void btnUnselect_Click(object sender, EventArgs e)
        {
            btnDelete.Hide();
            btnEdit.Hide();
            btnUnselect.Hide();

            btnAdd.Show();
            btnReset.Show();

            entryData.ClearSelection();
            Utils.Clear(groupBox1);
            rdbMale.Select();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (entryData.Rows.Count > 0)
            {
                string ArialBold = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "VUARIAL.TTF");
                BaseFont customfont = BaseFont.CreateFont(ArialBold, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                var fTitle = new iTextSharp.text.Font(customfont, 20, iTextSharp.text.Font.BOLD);
                var fHeader = new iTextSharp.text.Font(customfont, 10, iTextSharp.text.Font.BOLD);
                var f = new iTextSharp.text.Font(customfont, 8, iTextSharp.text.Font.NORMAL);

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Entry.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("Không thể tạo file!" + ex.Message, "Error");
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(7);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in entryData.Columns)
                            {
                                if (column.Visible)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, fHeader));
                                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                    pdfTable.AddCell(cell);
                                }
                            }

                            foreach (DataGridViewRow row in entryData.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    if (cell.Visible)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(cell.Value)))
                                            pdfTable.AddCell(new Phrase(Convert.ToString(cell.Value), f));
                                    } 
                                }
                            }

                            //exporting to PDF
                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                pdfDoc.Add(new Phrase("\n"));
                                Paragraph p = new Paragraph("Danh sách xuất cảnh", fTitle);
                                p.Alignment = Element.ALIGN_CENTER;
                                pdfDoc.Add(p);
                                pdfDoc.Add(new Phrase("\n"));
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Xuất PDF thành công!", "Inform");
                        }   
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Inform");
            }  
        }

        //phần datagridview
        private void entryData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            entryData.ClearSelection(); 
        }

        private void entryData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAdd.Hide();
            btnReset.Hide();
            btnEdit.Show();
            btnDelete.Show();
            btnUnselect.Show();

            //hiển thị data lên Groupbox
            if (e.RowIndex >= 0)
            {

                try
                {
                    DataGridViewRow row = entryData.Rows[e.RowIndex];
                    txtPassport.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
                    dtpArrivalDate.Value = DateTime.Parse(row.Cells[2].Value.ToString());
                    txtExpectedDestination.Text = row.Cells[3].Value.ToString();
                    dtpVisaExpire.Value = DateTime.Parse(row.Cells[4].Value.ToString());
                    dtpPassportExpire.Value = DateTime.Parse(row.Cells[5].Value.ToString());
                    for (int i = 0; i < cbPurpose.Items.Count; i++)
                        if (cbPurpose.Items[i].ToString() == row.Cells[6].Value.ToString())
                            cbPurpose.SelectedIndex = i;
                    cbNationality.SelectedValue = row.Cells[7].Value;
                    if (row.Cells[8].Value.ToString() == "1")
                        rdbMale.Select();
                    else
                        rdbFemale.Select();
                    dtpBirthday.Value = DateTime.Parse(row.Cells[9].Value.ToString());
                    txtPhone.Text = row.Cells[10].Value.ToString();
                    txtAddress.Text = row.Cells[11].Value.ToString();
                    txtOccupation.Text = row.Cells[12].Value.ToString();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        private void entryData_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable changes = ((DataTable)entryData.DataSource).GetChanges();
                if (changes != null)
                {
                    //MySqlUtils.UpdateNation(changes);
                    ((DataTable)entryData.DataSource).AcceptChanges();
                    //MessageBox.Show("Cập nhật thành công", "Inform");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại, chi tiết lỗi:\n" + ex.Message, "Error");
            }
        }

        //đổi màu viền panel
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.WhiteSmoke, ButtonBorderStyle.Solid);
        }


        //phần menuitem click
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


        //phần searchbox
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utils.LetterAndNumber(e);
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            Utils.AddPlaceholder((TextBox)sender);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            Utils.RemovePlaceholder((TextBox)sender);
        }

    }

}
