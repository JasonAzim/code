using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Common.Utility;

namespace Catalogue
{
    public partial class frmConfig : Form
    {
        public frmConfig()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs EA)
        {
            // Remember to call base implementation
            base.OnLoad(EA);

            this.txtCatalogueDirectory.Text = Settings.ReadConfigValue("CatalogueDirectory");
            this.txtOutputDirectory.Text = Settings.ReadConfigValue("OutputDirectory");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            this.CatalogBrowser.Description = "Please select a folder for the download.";
            this.CatalogBrowser.ShowNewFolderButton = false;

            if (this.CatalogBrowser.ShowDialog() == DialogResult.OK)
            {
                this.txtCatalogueDirectory.Text = this.CatalogBrowser.SelectedPath;
            }
        }

        private void btnOutputDirectory_Click(object sender, EventArgs e)
        {
            this.CatalogBrowser.Description = "Please select a folder for the download.";
            this.CatalogBrowser.ShowNewFolderButton = false;

            if (this.CatalogBrowser.ShowDialog() == DialogResult.OK)
            {
                this.txtOutputDirectory.Text = this.CatalogBrowser.SelectedPath;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings SaveSettings = new Settings();
            SaveSettings.AppConfig.CatalogueDirectory = this.txtCatalogueDirectory.Text;
            SaveSettings.AppConfig.OutputDirectory = this.txtOutputDirectory.Text;
            SaveSettings.SaveConfig();
            MessageBox.Show("Please restart the application for the changes to take effect.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
            Application.Exit();

            // ConfigurationManipulator.Test(); 
            // Settings.WriteConfigValue("CatalogueDirectory",this.txtCatalogueDirectory.Text);
            // Settings.WriteConfigValue("OutputDirectory",this.txtOutputDirectory.Text);
        }
    }
}
