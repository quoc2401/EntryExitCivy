using System;
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
            exitData.DataSource = Utils.SelectColumnExit();

            //đổi tên cột
            exitData.Columns["civy_id"].HeaderText = "Số hộ chiếu";
            exitData.Columns["fullname"].HeaderText = "Họ tên";
            exitData.Columns["depart_date"].HeaderText = "Ngày đi";
            exitData.Columns["destination"].HeaderText = "Nơi đến";
            exitData.Columns["visa_expiration"].HeaderText = "Hạn visa";
            exitData.Columns["passport_expiration"].HeaderText = "Hạn hộ chiếu";
            exitData.Columns["purpose"].HeaderText = "Mục đích";

            //ẩn những cột không hiển thị nhưng vẫn dùng
            exitData.Columns["nationality"].Visible = false;
            exitData.Columns["gender"].Visible = false;
            exitData.Columns["birthday"].Visible = false;
            exitData.Columns["phone"].Visible = false;
            exitData.Columns["home_address"].Visible = false;
            exitData.Columns["occupation"].Visible = false;
            exitData.Columns["destination_id"].Visible = false;

            //điều chỉnh chiều rộng cột
            exitData.Columns["civy_id"].Width = 100;
            exitData.Columns["fullname"].Width = 200;
            exitData.Columns["depart_date"].Width = 80;
            exitData.Columns["destination"].Width = 200;
            exitData.Columns["visa_expiration"].Width = 80;
            exitData.Columns["passport_expiration"].Width = 100;
            exitData.Columns["purpose"].Width = 100;

            try
            {
                DataTable data1 = MySqlUtils.GetNationsItems();
                Utils.AddComboBoxItems(cbNationality, data1);
                DataTable data2 = MySqlUtils.GetNationsItems();
                Utils.AddComboBoxItems(cbDestination, data2);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(text: ex.Message, caption: "Error");
            }

            cbPurpose.DataSource = Enum.GetValues(typeof(Purpose));
            exitData.RowValidated += exitData_RowValidated;
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
                DateTime depart_date = dtpDepartDate.Value;
                string destination = cbDestination.SelectedValue.ToString();
                DateTime visa_expriration = dtpVisaExpire.Value;
                DateTime passport_expriration = dtpPassportExpire.Value;
                Purpose purpose = (Purpose)Enum.Parse(typeof(Purpose), cbPurpose.Text, true);

                Civy c = new Civy(id: passport_no, fullname: name, gender: gender, birthday: birthday
                                 , nationality: nationality, phone: phone, home_address: address, occupation: occupation);

                Exit ex = new Exit(civy_id: passport_no, depart_date: depart_date, visa_expiration: visa_expriration
                                    , passport_expiration: passport_expriration, purpose: purpose, destination: destination);

                string exist = MySqlUtils.CivyExist(passport_no);
                DataTable result;

                if (exist == passport_no)
                {

                    result = MySqlUtils.AddExit(ex);
                }
                else
                {
                    MySqlUtils.AddNewCivy(c);
                    result = MySqlUtils.AddExit(ex);
                }
                if (result != null)
                    exitData.DataSource = result;
                else
                    exitData.DataSource = Utils.SelectColumnExit();

                MessageBox.Show(text: "Thêm thành công!", caption: "Inform");
                exitData.ClearSelection();
                Utils.Clear(groupBox1);
          
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
                DataTable result = new DataTable();

                for (int i = 0; i < exitData.SelectedRows.Count; i++)
                {
                    int selectedIndex = exitData.SelectedRows[i].Index;
                    string id = exitData[0, selectedIndex].Value.ToString();

                    DateTime depart_date = DateTime.Parse(exitData[2, selectedIndex].Value.ToString());
                    string date = string.Format("{0:yyyy/MM/dd}", depart_date);
                    result = MySqlUtils.DeleteExit(id, date);
                }
                if (result != null)
                    exitData.DataSource = result;
                else
                    exitData.DataSource = Utils.SelectColumnExit();

                MessageBox.Show(text: "Thêm thành công!", caption: "Inform");

                exitData.ClearSelection();
                Utils.Clear(groupBox1);
                btnDelete.Hide();
                btnEdit.Hide();
                btnUnselect.Hide();

                btnAdd.Show();
                btnReset.Show();
                rdbMale.Select();
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
            rdbMale.Select();
            exitData.ClearSelection();
            Utils.Clear(groupBox1);
        }


        //phần datagridview
        private void exitData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            exitData.ClearSelection();
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            if (exitData.Rows.Count > 0)
            {
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
                            PdfPTable pdfTable = new PdfPTable(exitData.Columns.Count);
                            pdfTable.DefaultCell.Padding = 3;
                            pdfTable.WidthPercentage = 100;
                            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in exitData.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in exitData.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(!string.IsNullOrEmpty(Convert.ToString(cell.Value)) ? Convert.ToString(cell.Value) : "");
                                }
                            }

                            //exporting to PDF  
                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {
                                Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);
                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
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


        private void exitData_CellClick(object sender, DataGridViewCellEventArgs e)
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
                    DataGridViewRow row = exitData.Rows[e.RowIndex];
                    txtPassport.Text = row.Cells[0].Value.ToString();
                    txtName.Text = row.Cells[1].Value.ToString();
                    dtpDepartDate.Value = DateTime.Parse(row.Cells[2].Value.ToString());
                    cbNationality.SelectedValue = row.Cells[7].Value;
                    if (row.Cells[8].Value.ToString() == "1")
                        rdbMale.Select();
                    else
                        rdbFemale.Select();
                    dtpBirthday.Value = DateTime.Parse(row.Cells[9].Value.ToString());
                    txtPhone.Text = row.Cells[10].Value.ToString();
                    txtAddress.Text = row.Cells[11].Value.ToString();
                    txtOccupation.Text = row.Cells[12].Value.ToString();
                    dtpVisaExpire.Value = DateTime.Parse(row.Cells[4].Value.ToString());
                    dtpPassportExpire.Value = DateTime.Parse(row.Cells[5].Value.ToString());
                    for (int i = 0; i < cbPurpose.Items.Count; i++)
                        if (cbPurpose.Items[i].ToString() == row.Cells[6].Value.ToString())
                            cbPurpose.SelectedIndex = i;
                    cbDestination.SelectedValue = row.Cells[13].Value;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            } 
        }

        private void exitData_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable changes = ((DataTable)exitData.DataSource).GetChanges();
                if (changes != null)
                {
                    //MySqlUtils.UpdateExits(changes);
                    ((DataTable)exitData.DataSource).AcceptChanges();
                    //MessageBox.Show("Cập nhật thành công", "Inform");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thất bại, chi tiết lỗi:\n" + ex.Message, "Error");
            }
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


        //đổi màu viền panel
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.WhiteSmoke, ButtonBorderStyle.Solid);
        }


        //phần searchbox
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
    }
}
