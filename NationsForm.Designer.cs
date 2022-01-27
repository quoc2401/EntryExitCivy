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
            this.label2 = new System.Windows.Forms.Label();
            this.dgridNation = new System.Windows.Forms.DataGridView();
            this.miEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();
            this.miNation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnFuncs = new System.Windows.Forms.MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.dgridNation)).BeginInit();
            this.mnFuncs.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(119, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(752, 47);
            this.label2.TabIndex = 2;
            this.label2.Text = "DANH SÁCH QUỐC GIA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            this.label2.Paint += new System.Windows.Forms.PaintEventHandler(this.label2_Paint);
            // 
            // dgridNation
            // 
            this.dgridNation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgridNation.BackgroundColor = System.Drawing.Color.DarkCyan;
            this.dgridNation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridNation.Location = new System.Drawing.Point(11, 101);
            this.dgridNation.Margin = new System.Windows.Forms.Padding(50);
            this.dgridNation.Name = "dgridNation";
            this.dgridNation.RowHeadersWidth = 51;
            this.dgridNation.RowTemplate.Height = 24;
            this.dgridNation.Size = new System.Drawing.Size(960, 476);
            this.dgridNation.TabIndex = 4;
            this.dgridNation.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridNation_CellContentClick);
            this.dgridNation.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgridNation_DataBindingComplete);
            this.dgridNation.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgridNation_RowValidated);
            // 
            // miEntry
            // 
            this.miEntry.Name = "miEntry";
            this.miEntry.Size = new System.Drawing.Size(94, 24);
            this.miEntry.Text = "Nhập cảnh";
            this.miEntry.Click += new System.EventHandler(this.miEntry_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(88, 24);
            this.miExit.Text = "Xuất cảnh";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miNation
            // 
            this.miNation.AutoToolTip = true;
            this.miNation.Name = "miNation";
            this.miNation.Size = new System.Drawing.Size(153, 24);
            this.miNation.Text = "Danh sách quốc gia";
            this.miNation.Click += new System.EventHandler(this.miNation_Click);
            // 
            // mnFuncs
            // 
            this.mnFuncs.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnFuncs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEntry,
            this.miExit,
            this.miNation});
            this.mnFuncs.Location = new System.Drawing.Point(0, 0);
            this.mnFuncs.Name = "mnFuncs";
            this.mnFuncs.Size = new System.Drawing.Size(995, 28);
            this.mnFuncs.TabIndex = 0;
            this.mnFuncs.Text = "Các chức năng";
            // 
            // NationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(995, 583);
            this.Controls.Add(this.dgridNation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mnFuncs);
            this.MainMenuStrip = this.mnFuncs;
            this.Name = "NationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NationsForm";
            this.Load += new System.EventHandler(this.NationsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridNation)).EndInit();
            this.mnFuncs.ResumeLayout(false);
            this.mnFuncs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgridNation;
        private System.Windows.Forms.ToolStripMenuItem miEntry;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miNation;
        private System.Windows.Forms.MenuStrip mnFuncs;
    }
}