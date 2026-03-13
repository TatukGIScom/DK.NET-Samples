using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;

namespace Viewer
{
    /// <summary>
    /// Summary description for WinForm1.
    /// </summary>
    public class InfoForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private TatukGIS.NDK.WinForms.TGIS_ControlAttributes GIS_ControlAttributes;
        public WinForm mainForm;

        public InfoForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.GIS_ControlAttributes = new TatukGIS.NDK.WinForms.TGIS_ControlAttributes();
            this.SuspendLayout();
            // 
            // GIS_ControlAttributes
            // 
            this.GIS_ControlAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS_ControlAttributes.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.GIS_ControlAttributes.Location = new System.Drawing.Point(0, 0);
            this.GIS_ControlAttributes.Name = "GIS_ControlAttributes";
            this.GIS_ControlAttributes.ReadOnly = true;
            this.GIS_ControlAttributes.Size = new System.Drawing.Size(242, 196);
            this.GIS_ControlAttributes.TabIndex = 0;
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(242, 196);
            this.Controls.Add(this.GIS_ControlAttributes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(250, 236);
            this.Name = "InfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Information";
            this.Closed += new System.EventHandler(this.InfoForm_Closed);
            this.ResumeLayout(false);

        }
        #endregion

        public void ShowInfo(TGIS_Shape _shp)
        {
            // if not found, show nothing
            if (_shp == null)
            {
                Text = "Shape: null";
            }
            else
            {
                Text = String.Format("Shape: {0}", _shp.Uid);
                // display all attributes for selected shape
                GIS_ControlAttributes.ShowShape(_shp);
            }
        }

        private void InfoForm_Closed(object sender, System.EventArgs e)
        {
            mainForm.infForm = null;
        }
    }
}
