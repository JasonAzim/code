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
    public partial class frmConfigDetail : Form
    {
        public frmConfigDetail()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs EA)
        {
            // throw new System.NotImplementedException();

            // Remember to call base implementation
            base.OnLoad(EA);

            Settings LoadSettings = new Settings();
            this.ppgSettings.SelectedObject = LoadSettings.AppConfig; 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings SaveSettings = new Settings();

            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(ppgSettings.SelectedObject, true);

            foreach (PropertyDescriptor pd in pdc)
            {
                string PropertyName = pd.Name;
                string PropertyValue = (pd.GetValue(ppgSettings.SelectedObject)).ToString();

                if (PropertyName == "CatalogueDirectory")
                {
                    SaveSettings.AppConfig.CatalogueDirectory = PropertyValue;
                }
                else if (PropertyName == "OutputDirectory")
                {
                    SaveSettings.AppConfig.OutputDirectory = PropertyValue;
                }
                else if (PropertyName == "OutputReport")
                {
                    SaveSettings.AppConfig.OutputReport = PropertyValue; 
                }
                else if (PropertyName == "MaxFileCount")
                {
                    SaveSettings.AppConfig.MaxFileCount = Int32.Parse(PropertyValue);
                }
            }

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
