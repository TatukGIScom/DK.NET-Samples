using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace TrackingTest
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.CheckBox chkUseLock;
        private System.Windows.Forms.Button btnAnimate;
        private System.Windows.Forms.StatusStrip stripBar1;
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAnimate = new System.Windows.Forms.Button();
            this.chkUseLock = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAnimate);
            this.panel1.Controls.Add(this.chkUseLock);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 29);
            this.panel1.TabIndex = 0;
            // 
            // btnAnimate
            // 
            this.btnAnimate.Location = new System.Drawing.Point(82, 4);
            this.btnAnimate.Name = "btnAnimate";
            this.btnAnimate.Size = new System.Drawing.Size(75, 22);
            this.btnAnimate.TabIndex = 3;
            this.btnAnimate.Text = "Animate";
            this.btnAnimate.Click += new System.EventHandler(this.btnAnimate_Click);
            // 
            // chkUseLock
            // 
            this.chkUseLock.Location = new System.Drawing.Point(8, 4);
            this.chkUseLock.Name = "chkUseLock";
            this.chkUseLock.Size = new System.Drawing.Size(89, 22);
            this.chkUseLock.TabIndex = 1;
            this.chkUseLock.Text = "Use Lock";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 29);
            this.toolStrip1.TabIndex = 0;
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            // 
            // GIS
            // 
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.IncrementalPaint = false;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.MinZoomSize = -5;
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(592, 418);
            this.GIS.TabIndex = 2;
            // 
            // WinForm
            // 
            this.AcceptButton = this.btnAnimate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Tracking test";
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
            TGIS_LayerVector ll;
            int i;
            TGIS_Shape shp;
            Random rnd;

            GIS.Lock();
            try
            {
                GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\VisibleEarth\world_8km.jpg");
                GIS.Zoom = GIS.Zoom * 2;
                GIS.InvalidateWholeMap();

                // create a layer and add a field
                ll = new TGIS_LayerVector();
                ll.Params.Marker.Symbol = TGIS_Utils.SymbolList.Prepare(
                                                                        TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\2267.cgm"
                                                                    );
                ll.Params.Marker.SymbolRotate = Math.PI / 2;
                ll.Params.Marker.Size = -20;
                ll.Params.Line.Symbol = TGIS_Utils.SymbolList.Prepare(
                                                                    TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\1301.cgm"
                                                                );
                ll.Params.Line.Width = -5;
                ll.CachedPaint = false;
                ll.CS = GIS.CS;
                GIS.Add(ll);
                ll.AddField("Name", TGIS_FieldType.String, 255, 0);
                ll.Params.Labels.Field = "Name";

                // add random plains
                rnd = new Random();
                for (i = 0; i <= 100; i++)
                {
                    shp = ((TGIS_LayerVector)GIS.Items[1]).CreateShape(TGIS_ShapeType.Point);
                    shp.SetField("Name", Convert.ToString(i + 1));
                    shp.Params.Marker.SymbolRotate = rnd.Next(360) * (Math.PI / 180);
                    shp.Params.Marker.Color = TGIS_Color.FromRGB((byte)rnd.Next(256),
                                                                  (byte)rnd.Next(256),
                                                                  (byte)rnd.Next(256)
                                                                );
                    shp.Params.Marker.OutlineColor = shp.Params.Marker.Color;
                    shp.Lock(TGIS_Lock.Extent);
                    shp.AddPart();
                    shp.AddPoint(TGIS_Utils.GisPoint(-180 + rnd.Next(360),
                                                       (90 - rnd.Next(180))
                                                     )
                                );
                    shp.Unlock();
                    }
                }
            finally
            {
                GIS.Unlock();
            }
        }

        private void btnAnimate_Click(object sender, System.EventArgs e)
        {
            int i, j;
            TGIS_Shape shp;
            TGIS_Point pt;
            int delta;

            btnAnimate.Enabled = false;
            for (i = 0; i <= 90; i++)
            {
                if (chkUseLock.Checked)
                    GIS.Lock();

                // move plains
                for (j = 1; j <= 90; j++)
                {
                    if (this.IsDisposed)
                        break;
                    shp = ((TGIS_LayerVector)GIS.Items[1]).GetShape(j);
                    pt = shp.Centroid();

                    delta = j % 3 - 1;
                    shp.SetPosition(TGIS_Utils.GisPoint(pt.X + delta, pt.Y), null, 0);
                    Application.DoEvents();
                }

                if (this.IsDisposed)
                    break;
                if (chkUseLock.Checked)
                {
                    GIS.Unlock();
                    Application.DoEvents();
                }
                else
                    GIS.LabelsReg.Reset();
            }
            btnAnimate.Enabled = true;
        }
    }
}
