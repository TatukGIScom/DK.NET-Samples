using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Reproject
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxSrcProjection;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxSrcProjection = new System.Windows.Forms.ComboBox();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(217, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 0;
            this.button1.Text = "Reproject";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxSrcProjection);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 25);
            this.panel1.TabIndex = 1;
            // 
            // cbxSrcProjection
            // 
            this.cbxSrcProjection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSrcProjection.Location = new System.Drawing.Point(8, 2);
            this.cbxSrcProjection.Name = "cbxSrcProjection";
            this.cbxSrcProjection.Size = new System.Drawing.Size(201, 21);
            this.cbxSrcProjection.TabIndex = 1;
            this.cbxSrcProjection.SelectedIndexChanged += new System.EventHandler(this.cbxSrcProjection_SelectedIndexChanged);
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "shp";
            this.dlgSave.Filter = "SHP File|*.shp";
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 25);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(592, 441);
            this.GIS.TabIndex = 0;
            this.GIS.UseRTree = false;
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
            this.Text = "TatukGIS Samples - Reproject";
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
            for (i = 0; i < TGIS_Utils.CSProjList.Count(); i++)
            {// UTM is a bit to restrictive to show whole country
                if (lst.ContainsKey(TGIS_Utils.CSProjList[i].WKT) == false)
                    lst.Add(TGIS_Utils.CSProjList[i].WKT, TGIS_Utils.CSProjList[i].WKT);
            }
            for (i = 0; i < lst.Count; i++)
                cbxSrcProjection.Items.Add(lst.GetByIndex(i));

            cbxSrcProjection.SelectedIndex = 0;
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\country.shp");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_LayerSHP lo;

            if (GIS.IsEmpty) return;

            if (dlgSave.ShowDialog() != DialogResult.OK) return;

            ll = (TGIS_LayerVector)GIS.Items[0];

            lo = new TGIS_LayerSHP();
            lo.Path = dlgSave.FileName;
            lo.CS = GIS.CS;
            try
            {
                lo.ImportLayer(ll, TGIS_Utils.GisWholeWorld(),
                                TGIS_ShapeType.Unknown, "", false

                              );
            }
            finally
            {
                lo = null;
            }
        }

        private void cbxSrcProjection_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            String sproj = (String)cbxSrcProjection.Items[cbxSrcProjection.SelectedIndex];

            TGIS_CSGeographicCoordinateSystem ogcs = TGIS_Utils.CSGeographicCoordinateSystemList.ByEPSG(4030);
            TGIS_CSUnits ounit = TGIS_Utils.CSUnitsList.ByWKT("METER");
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
                    GIS.CS = null;
                }
            }
            finally
            {
                GIS.Unlock();
            }

        }
    }
}
