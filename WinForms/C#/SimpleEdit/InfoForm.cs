using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace SimpleEdit
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
        private TGIS_ControlAttributes GISAttributes;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
            this.GISAttributes = new TatukGIS.NDK.WinForms.TGIS_ControlAttributes();
            this.SuspendLayout();
            // 
            // GISAttributes
            // 
            this.GISAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GISAttributes.Location = new System.Drawing.Point(0, 0);
            this.GISAttributes.Name = "GISAttributes";
            this.GISAttributes.Size = new System.Drawing.Size(242, 303);
            this.GISAttributes.TabIndex = 0;
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(242, 303);
            this.Controls.Add(this.GISAttributes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(262, 231);
            this.Name = "InfoForm";
            this.Text = "Information";

            this.Closing += new System.ComponentModel.CancelEventHandler(this.InfoForm_Closing);
            this.ResumeLayout(false);

        }
        #endregion

        public void ShowInfo(TGIS_Shape _shp)
        {
            GISAttributes.ShowShape(_shp);
        }

        private void InfoForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
