namespace Catalogue
{
    partial class frmConfig
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
            this.txtCatalogueDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.CatalogBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnOutputDirectory = new System.Windows.Forms.Button();
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.lblCatalogDirectory = new System.Windows.Forms.Label();
            this.lblOutputDirectory = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCatalogueDirectory
            // 
            this.txtCatalogueDirectory.Location = new System.Drawing.Point(101, 12);
            this.txtCatalogueDirectory.Name = "txtCatalogueDirectory";
            this.txtCatalogueDirectory.Size = new System.Drawing.Size(254, 20);
            this.txtCatalogueDirectory.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(366, 12);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(24, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnOutputDirectory
            // 
            this.btnOutputDirectory.Location = new System.Drawing.Point(366, 50);
            this.btnOutputDirectory.Name = "btnOutputDirectory";
            this.btnOutputDirectory.Size = new System.Drawing.Size(24, 23);
            this.btnOutputDirectory.TabIndex = 2;
            this.btnOutputDirectory.Text = "...";
            this.btnOutputDirectory.UseVisualStyleBackColor = true;
            this.btnOutputDirectory.Click += new System.EventHandler(this.btnOutputDirectory_Click);
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Location = new System.Drawing.Point(101, 50);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(254, 20);
            this.txtOutputDirectory.TabIndex = 3;
            // 
            // lblCatalogDirectory
            // 
            this.lblCatalogDirectory.AutoSize = true;
            this.lblCatalogDirectory.Location = new System.Drawing.Point(4, 15);
            this.lblCatalogDirectory.Name = "lblCatalogDirectory";
            this.lblCatalogDirectory.Size = new System.Drawing.Size(91, 13);
            this.lblCatalogDirectory.TabIndex = 4;
            this.lblCatalogDirectory.Text = "Catalog Directory:";
            // 
            // lblOutputDirectory
            // 
            this.lblOutputDirectory.AutoSize = true;
            this.lblOutputDirectory.Location = new System.Drawing.Point(4, 50);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.Size = new System.Drawing.Size(87, 13);
            this.lblOutputDirectory.TabIndex = 5;
            this.lblOutputDirectory.Text = "Output Directory:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(101, 88);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(7, 88);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 125);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblOutputDirectory);
            this.Controls.Add(this.lblCatalogDirectory);
            this.Controls.Add(this.txtOutputDirectory);
            this.Controls.Add(this.btnOutputDirectory);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtCatalogueDirectory);
            this.Name = "frmConfig";
            this.Text = "Settings Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCatalogueDirectory;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog CatalogBrowser;
        private System.Windows.Forms.Button btnOutputDirectory;
        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Label lblCatalogDirectory;
        private System.Windows.Forms.Label lblOutputDirectory;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
    }
}

