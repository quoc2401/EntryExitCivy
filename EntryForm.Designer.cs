namespace EntryExitCivy
{
    partial class EntryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgridEntry = new System.Windows.Forms.DataGridView();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtNation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgridEntry)).BeginInit();
            this.SuspendLayout();
            // 
            // dgridEntry
            // 
            this.dgridEntry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridEntry.Location = new System.Drawing.Point(265, 43);
            this.dgridEntry.Name = "dgridEntry";
            this.dgridEntry.RowTemplate.Height = 24;
            this.dgridEntry.Size = new System.Drawing.Size(536, 366);
            this.dgridEntry.TabIndex = 0;
            this.dgridEntry.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridEntry_CellClick);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(116, 61);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(68, 22);
            this.txtId.TabIndex = 1;
            // 
            // txtNation
            // 
            this.txtNation.Location = new System.Drawing.Point(116, 138);
            this.txtNation.Name = "txtNation";
            this.txtNation.Size = new System.Drawing.Size(118, 22);
            this.txtNation.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mã quốc gia";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tên quốc gia";
            // 
            // EntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 421);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNation);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.dgridEntry);
            this.Name = "EntryForm";
            this.Text = "EntryForm";
            this.Load += new System.EventHandler(this.EntryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridEntry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgridEntry;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtNation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}