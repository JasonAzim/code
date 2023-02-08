namespace Catalogue
{
    partial class frmMain
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
            this.btnProcess = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.TopMenu = new System.Windows.Forms.MenuStrip();
            this.miConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.completeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFileSizesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDLLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDistinctFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showVBProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCSProjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFileTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Worker = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.QueryInputBox = new System.Windows.Forms.RichTextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.gvCatalogue = new System.Windows.Forms.DataGridView();
            this.dLLCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dLLCopyUniqueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TopMenu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCatalogue)).BeginInit();
            this.SuspendLayout();
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(3, 2);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(53, 23);
            this.btnProcess.TabIndex = 0;
            this.btnProcess.Text = "View";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(193, 6);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(114, 17);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Process is Idle";
            // 
            // TopMenu
            // 
            this.TopMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miConfig,
            this.reportsToolStripMenuItem});
            this.TopMenu.Location = new System.Drawing.Point(0, 0);
            this.TopMenu.Name = "TopMenu";
            this.TopMenu.Size = new System.Drawing.Size(828, 24);
            this.TopMenu.TabIndex = 2;
            this.TopMenu.Text = "Main switchboard";
            // 
            // miConfig
            // 
            this.miConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simpleToolStripMenuItem,
            this.completeToolStripMenuItem});
            this.miConfig.Name = "miConfig";
            this.miConfig.Size = new System.Drawing.Size(50, 20);
            this.miConfig.Text = "Config";
            // 
            // simpleToolStripMenuItem
            // 
            this.simpleToolStripMenuItem.Name = "simpleToolStripMenuItem";
            this.simpleToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.simpleToolStripMenuItem.Text = "Simple";
            this.simpleToolStripMenuItem.Click += new System.EventHandler(this.simpleToolStripMenuItem_Click);
            // 
            // completeToolStripMenuItem
            // 
            this.completeToolStripMenuItem.Name = "completeToolStripMenuItem";
            this.completeToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.completeToolStripMenuItem.Text = "Detail";
            this.completeToolStripMenuItem.Click += new System.EventHandler(this.DetailToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAllFilesToolStripMenuItem,
            this.showFileSizesToolStripMenuItem,
            this.showDLLToolStripMenuItem,
            this.showDistinctFilesToolStripMenuItem,
            this.showMSIToolStripMenuItem,
            this.showVBProjectsToolStripMenuItem,
            this.showCSProjectsToolStripMenuItem,
            this.showFileTypesToolStripMenuItem,
            this.dLLCopyToolStripMenuItem,
            this.dLLCopyUniqueToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // showAllFilesToolStripMenuItem
            // 
            this.showAllFilesToolStripMenuItem.Name = "showAllFilesToolStripMenuItem";
            this.showAllFilesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showAllFilesToolStripMenuItem.Text = "Show All Files";
            this.showAllFilesToolStripMenuItem.Click += new System.EventHandler(this.showAllFilesToolStripMenuItem_Click);
            // 
            // showFileSizesToolStripMenuItem
            // 
            this.showFileSizesToolStripMenuItem.Name = "showFileSizesToolStripMenuItem";
            this.showFileSizesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showFileSizesToolStripMenuItem.Text = "Show File Sizes";
            this.showFileSizesToolStripMenuItem.Click += new System.EventHandler(this.showFileSizesToolStripMenuItem_Click);
            // 
            // showDLLToolStripMenuItem
            // 
            this.showDLLToolStripMenuItem.Name = "showDLLToolStripMenuItem";
            this.showDLLToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showDLLToolStripMenuItem.Text = "Show DLL";
            this.showDLLToolStripMenuItem.Click += new System.EventHandler(this.showDLLToolStripMenuItem_Click);
            // 
            // showDistinctFilesToolStripMenuItem
            // 
            this.showDistinctFilesToolStripMenuItem.Name = "showDistinctFilesToolStripMenuItem";
            this.showDistinctFilesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showDistinctFilesToolStripMenuItem.Text = "Show distinct Files";
            this.showDistinctFilesToolStripMenuItem.Click += new System.EventHandler(this.showDistinctFilesToolStripMenuItem_Click);
            // 
            // showMSIToolStripMenuItem
            // 
            this.showMSIToolStripMenuItem.Name = "showMSIToolStripMenuItem";
            this.showMSIToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showMSIToolStripMenuItem.Text = "Show MSI";
            this.showMSIToolStripMenuItem.Click += new System.EventHandler(this.showMSIToolStripMenuItem_Click);
            // 
            // showVBProjectsToolStripMenuItem
            // 
            this.showVBProjectsToolStripMenuItem.Name = "showVBProjectsToolStripMenuItem";
            this.showVBProjectsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showVBProjectsToolStripMenuItem.Text = "Show VB Projects";
            this.showVBProjectsToolStripMenuItem.Click += new System.EventHandler(this.showVBProjectsToolStripMenuItem_Click);
            // 
            // showCSProjectsToolStripMenuItem
            // 
            this.showCSProjectsToolStripMenuItem.Name = "showCSProjectsToolStripMenuItem";
            this.showCSProjectsToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showCSProjectsToolStripMenuItem.Text = "Show CS Projects";
            this.showCSProjectsToolStripMenuItem.Click += new System.EventHandler(this.showCSProjectsToolStripMenuItem_Click);
            // 
            // showFileTypesToolStripMenuItem
            // 
            this.showFileTypesToolStripMenuItem.Name = "showFileTypesToolStripMenuItem";
            this.showFileTypesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showFileTypesToolStripMenuItem.Text = "Show File Types";
            this.showFileTypesToolStripMenuItem.Click += new System.EventHandler(this.showFileTypesToolStripMenuItem_Click);
            // 
            // Worker
            // 
            this.Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Worker_DoWork);
            this.Worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Worker_RunWorkerCompleted);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gvCatalogue);
            this.splitContainer1.Size = new System.Drawing.Size(828, 577);
            this.splitContainer1.SplitterDistance = 146;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.QueryInputBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnExport);
            this.splitContainer2.Panel2.Controls.Add(this.btnCopy);
            this.splitContainer2.Panel2.Controls.Add(this.btnProcess);
            this.splitContainer2.Panel2.Controls.Add(this.lblMessage);
            this.splitContainer2.Size = new System.Drawing.Size(828, 146);
            this.splitContainer2.SplitterDistance = 100;
            this.splitContainer2.TabIndex = 0;
            // 
            // QueryInputBox
            // 
            this.QueryInputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QueryInputBox.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QueryInputBox.Location = new System.Drawing.Point(0, 0);
            this.QueryInputBox.Name = "QueryInputBox";
            this.QueryInputBox.Size = new System.Drawing.Size(828, 100);
            this.QueryInputBox.TabIndex = 0;
            this.QueryInputBox.Text = "select Name,ApplicationName,Size,Path from [CurrentCatalogue] where FILE_FILTER =" +
                " \'*.msi\' & DIR_FILTER = \'*\'";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(64, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(53, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(125, 3);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(53, 23);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // gvCatalogue
            // 
            this.gvCatalogue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvCatalogue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvCatalogue.Location = new System.Drawing.Point(0, 0);
            this.gvCatalogue.Name = "gvCatalogue";
            this.gvCatalogue.Size = new System.Drawing.Size(828, 427);
            this.gvCatalogue.TabIndex = 0;
            // 
            // dLLCopyToolStripMenuItem
            // 
            this.dLLCopyToolStripMenuItem.Name = "dLLCopyToolStripMenuItem";
            this.dLLCopyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.dLLCopyToolStripMenuItem.Text = "DLL Copy";
            this.dLLCopyToolStripMenuItem.Click += new System.EventHandler(this.dLLCopyToolStripMenuItem_Click);
            // 
            // dLLCopyUniqueToolStripMenuItem
            // 
            this.dLLCopyUniqueToolStripMenuItem.Name = "dLLCopyUniqueToolStripMenuItem";
            this.dLLCopyUniqueToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.dLLCopyUniqueToolStripMenuItem.Text = "DLL Copy Unique";
            this.dLLCopyUniqueToolStripMenuItem.Click += new System.EventHandler(this.dLLCopyUniqueToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 601);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.TopMenu);
            this.MainMenuStrip = this.TopMenu;
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.TopMenu.ResumeLayout(false);
            this.TopMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvCatalogue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.MenuStrip TopMenu;
        private System.Windows.Forms.ToolStripMenuItem miConfig;
        private System.ComponentModel.BackgroundWorker Worker;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox QueryInputBox;
        private System.Windows.Forms.DataGridView gvCatalogue;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ToolStripMenuItem simpleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem completeToolStripMenuItem;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAllFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFileSizesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDLLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDistinctFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMSIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showVBProjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCSProjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showFileTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dLLCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dLLCopyUniqueToolStripMenuItem;
    }
}