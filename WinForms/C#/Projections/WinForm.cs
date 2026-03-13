using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Projections
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.ComboBox cbxSrcProjection;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Panel panel1;

        public WinForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.cbxSrcProjection = new System.Windows.Forms.ComboBox();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxSrcProjection
            // 
            this.cbxSrcProjection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSrcProjection.Location = new System.Drawing.Point(0, 4);
            this.cbxSrcProjection.Name = "cbxSrcProjection";
            this.cbxSrcProjection.Size = new System.Drawing.Size(193, 21);
            this.cbxSrcProjection.TabIndex = 0;
            this.cbxSrcProjection.SelectedIndexChanged += new System.EventHandler(this.cbxSrcProjection_SelectedIndexChanged);
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 437);
            this.GIS.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxSrcProjection);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 29);
            this.panel1.TabIndex = 2;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Projections";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinForm());
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            int i;
            System.Collections.SortedList lst;

            lst = new System.Collections.SortedList();
            lst.Clear();

            for (i = 0; i < TGIS_Utils.CSProjList.Count(); i++)
            {
                if (TGIS_Utils.CSProjList[i].IsStandard)
                {
                    lst.Add(TGIS_Utils.CSProjList[i].WKT, TGIS_Utils.CSProjList[i].WKT);
                }
            }
            for (i = 0; i < lst.Count; i++)
            {
                cbxSrcProjection.Items.Add(lst.GetByIndex(i));
            };

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\Projects\world.ttkproject", true);

            cbxSrcProjection.SelectedIndex = 0;
        }

        private void cbxSrcProjection_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            String sproj = (String)cbxSrcProjection.Items[cbxSrcProjection.SelectedIndex];

            TGIS_CSGeographicCoordinateSystem ogcs = TGIS_Utils.CSGeographicCoordinateSystemList.ByEPSG(4030);
            TGIS_CSUnits ounit = TGIS_Utils.CSUnitsList.ByWKT("Meter");
            TGIS_CSProjAbstract oproj = TGIS_Utils.CSProjList.ByWKT(sproj);


            TGIS_CSCoordinateSystem ocs = new TGIS_CSProjectedCoordinateSystem(
                     -1, "Test",
                     ogcs.EPSG, ounit.EPSG, oproj.EPSG,
                     TGIS_Utils.CSProjectedCoordinateSystemList.DefaultParams(oproj.EPSG)
                   );

            GIS.Lock();
            try
            {
                try
                {
                    GIS.CS = ocs;
                    GIS.FullExtent();
                }
                catch
                {
                    // we are aware of problems upon switching
                    // between two incompatible systems
                }
            }
            finally
            {
                GIS.Unlock();
            }
        }
    }
}
