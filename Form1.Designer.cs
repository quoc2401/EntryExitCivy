namespace EntryExitCivy
{
    partial class Form1
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
            this.mnFuncs.SuspendLayout();
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
            this.mnFuncs.Size = new System.Drawing.Size(624, 33);
            this.mnFuncs.TabIndex = 3;
            this.mnFuncs.Text = "Các chức năng";
            // 
            // miEntry
            // 
            this.miEntry.Name = "miEntry";
            this.miEntry.Size = new System.Drawing.Size(109, 29);
            this.miEntry.Text = "Nhập cảnh";
            this.miEntry.Click += new System.EventHandler(this.miEntry_Click);
            // 
            // miExit
            // 
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(102, 29);
            this.miExit.Text = "Xuất cảnh";
            this.miExit.Click += new System.EventHandler(this.miExit_Click);
            // 
            // miNation
            // 
            this.miNation.AutoToolTip = true;
            this.miNation.Name = "miNation";
            this.miNation.Size = new System.Drawing.Size(180, 29);
            this.miNation.Text = "Danh sách quốc gia";
            this.miNation.Click += new System.EventHandler(this.miNation_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 253);
            this.Controls.Add(this.mnFuncs);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Chương trình quản lý xuất nhập cảnh";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mnFuncs.ResumeLayout(false);
            this.mnFuncs.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnFuncs;
        private System.Windows.Forms.ToolStripMenuItem miEntry;
        private System.Windows.Forms.ToolStripMenuItem miExit;
        private System.Windows.Forms.ToolStripMenuItem miNation;
    }
}

