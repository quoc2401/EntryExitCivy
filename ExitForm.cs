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
