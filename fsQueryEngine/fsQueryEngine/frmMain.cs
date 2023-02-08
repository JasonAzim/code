using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Utility;

namespace Catalogue
{
    public partial class frmMain : Form
    {
        const string PROCESS = "Process";
        const string EXPORT = "Export";
        const string COPY = "Copy";

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "Process running...";

            ThreadMessage message = new ThreadMessage(this.QueryInputBox.Text, PROCESS);
            Worker.RunWorkerAsync(message);

            // Worker.RunWorkerAsync(this.QueryInputBox.Text);
        }

        private void miConfig_Click(object sender, EventArgs e)
        {
            frmConfig FC = new frmConfig();
            FC.ShowDialog(); 
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ThreadMessage message = (ThreadMessage)e.Argument;

            string Query = string.Empty;
            Query = message.Query;
            // Query = (string)e.Argument;
            // Query = "select Name,ApplicationName,Size,Path,Extension from [CurrentCatalogue]";
            // Query = "select distinct Name,Size from [CurrentCatalogue]";
            // Query = "select distinct Name,Size from [CurrentCatalogue] where FILE_FILTER = '*.dll' & DIR_FILTER = '*'";
            // Query = "select distinct Name from [CurrentCatalogue]";
            // Query = "select distinct Name from [CurrentCatalogue] where FILE_FILTER = '*.dll' & DIR_FILTER = '*'";
            // Query = "select Name,ApplicationName,Size,Path from [CurrentCatalogue] where FILE_FILTER = '*.dll' & DIR_FILTER = '*'";
            // Query = "select Name,ApplicationName,Size,Path from [CurrentCatalogue] where FILE_FILTER = '*.msi' & DIR_FILTER = '*'";
            // Query = "select distinct Name,ApplicationName,Path from [CurrentCatalogue] where FILE_FILTER = '*.csproj' & DIR_FILTER = '*'";
            // Query = "select distinct Name,ApplicationName,Path from [CurrentCatalogue] where FILE_FILTER = '*.vbproj' & DIR_FILTER = '*'";
            // Query = "select distinct Extension from [CurrentCatalogue]";
            // Query = "select distinct Name,ApplicationName from [CurrentCatalogue] where FILE_FILTER = '*.msi' & DIR_FILTER = '*'";

            FileAdapter FA = new FileAdapter();

            if (message.Source == PROCESS)
            {
                e.Result = FA.ExecuteQuery(Query, false,false);
            }
            else if (message.Source == EXPORT)
            {
                e.Result = FA.ExecuteQuery(Query, true,false);
            }
            else if (message.Source == COPY)
            {
                e.Result = FA.ExecuteQuery(Query, false, true);
            }

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lblMessage.Text = "Process completed...";
            this.gvCatalogue.DataSource = (DataTable)e.Result;
        }

        private void simpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfig FC = new frmConfig();
            FC.ShowDialog(); 
        }

        private void DetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigDetail FC = new frmConfigDetail();
            FC.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "Process running...";

            ThreadMessage message = new ThreadMessage(this.QueryInputBox.Text, EXPORT);
            Worker.RunWorkerAsync(message);
        }

        private void showAllFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.QueryInputBox.Text = "select Name,ApplicationName,Size,Path,Extension from [CurrentCatalogue]";
        }

        private void showFileSizesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.QueryInputBox.Text = "select distinct Name,Size from [CurrentCatalogue]";
        }

        private void showDLLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.QueryInputBox.Text = "select distinct Name,Size from [CurrentCatalogue] where FILE_FILTER = '*.dll' & DIR_FILTER = '*'";
        }

        private void showDistinctFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.QueryInputBox.Text = "select distinct Name from [CurrentCatalogue]";
        }

        private void showMSIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.QueryInputBox.Text = "select Name,ApplicationName,Size,Path from [CurrentCatalogue] where FILE_FILTER = '*.msi' & DIR_FILTER = '*'";
        }

        private void showVBProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.QueryInputBox.Text = "select distinct Name,ApplicationName,Path from [CurrentCatalogue] where FILE_FILTER = '*.vbproj' & DIR_FILTER = '*'";
        }

        private void showCSProjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.QueryInputBox.Text = "select distinct Name,ApplicationName,Path from [CurrentCatalogue] where FILE_FILTER = '*.csproj' & DIR_FILTER = '*'";
        }

        private void showFileTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.QueryInputBox.Text = "select distinct Extension from [CurrentCatalogue]";
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            this.lblMessage.Text = "Process running...";

            ThreadMessage message = new ThreadMessage(this.QueryInputBox.Text, COPY);
            Worker.RunWorkerAsync(message);
        }

        private void dLLCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.QueryInputBox.Text = "select distinct Name,Size,Path from [CurrentCatalogue] where FILE_FILTER = '*.dll' & DIR_FILTER = '*'";
        }

        private void dLLCopyUniqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.QueryInputBox.Text = "select distinct Name,Path from [CurrentCatalogue] where FILE_FILTER = '*.dll' & DIR_FILTER = '*'";
        }
    }
}
