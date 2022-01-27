namespace EntryExitCivy
{
    partial class NationsForm
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
            this.mnFuncs = new System.Windows.Forms.MenuStrip();
            this.miEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miNation = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.dgridNation = new System.Windows.Forms.DataGridView();
            this.mnFuncs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridNation)).BeginInit();
            this.SuspendLayout();
            // 
            // mnFuncs
            // 
            this.mnFuncs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEntry,
            this.miExit,
            this.miNation});
            this.mnFuncs.Location = new System.Drawing.Point(0, 0);
            this.mnFuncs.Name = "mnFuncs";
            this.mnFuncs.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mnFuncs.Size = new System.Drawing.Size(867, 33);
            this.mnFuncs.TabIndex = 0;
            this.mnFuncs.Text = "Các chức năng";
            // 
            // miEntry
            // 
            this.miEntry.Name = "miEntry";
            this.miEntry.Size = new System.Drawing.Size(109, 29);
            this.miEntry.Text = "Nhập cảnh";
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(102, 29);
            this.miExit.Text = "Xuất cảnh";
            // 
            // miNation
            // 
            this.miNation.AutoToolTip = true;
            this.miNation.Name = "miNation";
            this.miNation.Size = new System.Drawing.Size(180, 29);
            this.miNation.Text = "Danh sách quốc gia";
            this.miNation.Click += new System.EventHandler(this.miNation_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(846, 58);
            this.label2.TabIndex = 2;
            this.label2.Text = "DANH SÁCH QUỐC GIA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Paint += new System.Windows.Forms.PaintEventHandler(this.label2_Paint);
            // 
            // dgridNation
            // 
            this.dgridNation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgridNation.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.dgridNation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridNation.Location = new System.Drawing.Point(12, 126);
            this.dgridNation.Margin = new System.Windows.Forms.Padding(56, 62, 56, 62);
            this.dgridNation.Name = "dgridNation";
            this.dgridNation.RowTemplate.Height = 24;
            this.dgridNation.Size = new System.Drawing.Size(842, 435);
            this.dgridNation.TabIndex = 4;
            this.dgridNation.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgridNation_DataBindingComplete);
            this.dgridNation.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridNation_RowValidated);
            // 
            // NationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(867, 578);
            this.Controls.Add(this.dgridNation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mnFuncs);
            this.MainMenuStrip = this.mnFuncs;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NationsForm";
            this.Load += new System.EventHandler(this.NationsForm_Load);
            this.mnFuncs.ResumeLayout(false);
            this.mnFuncs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgridNation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnFuncs;
        private System.Windows.Forms.ToolStripMenuItem miEntry;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miNation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgridNation;
    }
}