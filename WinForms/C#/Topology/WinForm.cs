using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Topology
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
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Button btnAplusB;
        private System.Windows.Forms.Button btnAmultB;
        private System.Windows.Forms.Button btnAminusB;
        private System.Windows.Forms.Button btnBminusA;
        private System.Windows.Forms.Button btnAxorB;
        private System.Windows.Forms.StatusStrip stripBar1;
        private TGIS_Topology topologyObj;
        private TGIS_LayerVector layerObj;
        private TGIS_ShapePolygon shpA;
        private TGIS_ShapePolygon shpB;
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
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnAplusB = new System.Windows.Forms.Button();
            this.btnAmultB = new System.Windows.Forms.Button();
            this.btnAminusB = new System.Windows.Forms.Button();
            this.btnBminusA = new System.Windows.Forms.Button();
            this.btnAxorB = new System.Windows.Forms.Button();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // GIS
            // 
            this.GIS.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 25);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(592, 422);
            this.GIS.TabIndex = 0;
            this.GIS.UseRTree = false;
            // 
            // btnAplusB
            // 
            this.btnAplusB.Location = new System.Drawing.Point(0, 1);
            this.btnAplusB.Name = "btnAplusB";
            this.btnAplusB.Size = new System.Drawing.Size(75, 23);
            this.btnAplusB.TabIndex = 1;
            this.btnAplusB.Text = "A + B";
            this.btnAplusB.Click += new System.EventHandler(this.btnAplusB_Click);
            // 
            // btnAmultB
            // 
            this.btnAmultB.Location = new System.Drawing.Point(75, 1);
            this.btnAmultB.Name = "btnAmultB";
            this.btnAmultB.Size = new System.Drawing.Size(75, 23);
            this.btnAmultB.TabIndex = 2;
            this.btnAmultB.Text = "A * B";
            this.btnAmultB.Click += new System.EventHandler(this.btnAmultB_Click);
            // 
            // btnAminusB
            // 
            this.btnAminusB.Location = new System.Drawing.Point(150, 1);
            this.btnAminusB.Name = "btnAminusB";
            this.btnAminusB.Size = new System.Drawing.Size(75, 23);
            this.btnAminusB.TabIndex = 3;
            this.btnAminusB.Text = "A - B";
            this.btnAminusB.Click += new System.EventHandler(this.btnAminusB_Click);
            // 
            // btnBminusA
            // 
            this.btnBminusA.Location = new System.Drawing.Point(225, 1);
            this.btnBminusA.Name = "btnBminusA";
            this.btnBminusA.Size = new System.Drawing.Size(75, 23);
            this.btnBminusA.TabIndex = 4;
            this.btnBminusA.Text = "B - A";
            this.btnBminusA.Click += new System.EventHandler(this.btnBminusA_Click);
            // 
            // btnAxorB
            // 
            this.btnAxorB.Location = new System.Drawing.Point(300, 1);
            this.btnAxorB.Name = "btnAxorB";
            this.btnAxorB.Size = new System.Drawing.Size(75, 23);
            this.btnAxorB.TabIndex = 5;
            this.btnAxorB.Text = "A xor B";
            this.btnAxorB.Click += new System.EventHandler(this.btnAxorB_Click);
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 25);
            this.panel1.TabIndex = 7;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.btnAxorB);
            this.Controls.Add(this.btnBminusA);
            this.Controls.Add(this.btnAminusB);
            this.Controls.Add(this.btnAmultB);
            this.Controls.Add(this.btnAplusB);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Topology";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Leave += new System.EventHandler(this.WinForm_Leave);
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

            topologyObj = new TGIS_Topology();

            GIS.Lock();
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\Topology\topology.shp");

            ll = (TGIS_LayerVector)GIS.Items[0];
            if (ll == null) return;

            shpA = (TGIS_ShapePolygon)ll.GetShape(1).MakeEditable();
            if (shpA == null) return;

            shpB = (TGIS_ShapePolygon)ll.GetShape(2).MakeEditable();
            if (shpB == null) return;

            layerObj = new TGIS_LayerVector();
            layerObj.Name = "output";
            layerObj.Transparency = 50;
            layerObj.Params.Area.Color = TGIS_Color.Red;

            GIS.Add(layerObj);

            GIS.Unlock();
            GIS.FullExtent();
        }

        private void WinForm_Leave(object sender, System.EventArgs e)
        {
            if (topologyObj != null)
                topologyObj = null;
        }

        private void btnAplusB_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape tmp;

            layerObj.RevertShapes();
            tmp = topologyObj.Combine(shpA, shpB,
                                       TGIS_TopologyCombineType.Union
                                     );
            if (tmp != null)
            {
                layerObj.AddShape(tmp);
                tmp = null;
            }
            GIS.InvalidateWholeMap();
        }

        private void btnAmultB_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape tmp;

            layerObj.RevertShapes();
            tmp = topologyObj.Combine(shpA, shpB,
                                       TGIS_TopologyCombineType.Intersection
                                     );
            if (tmp != null)
            {
                layerObj.AddShape(tmp);
                tmp = null;
            }
            GIS.InvalidateWholeMap();
        }

        private void btnAminusB_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape tmp;

            layerObj.RevertShapes();
            tmp = topologyObj.Combine(shpA, shpB,
                                       TGIS_TopologyCombineType.Difference
                                     );
            if (tmp != null)
            {
                layerObj.AddShape(tmp);
                tmp = null;
            }
            GIS.InvalidateWholeMap();
        }

        private void btnBminusA_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape tmp;

            layerObj.RevertShapes();
            tmp = topologyObj.Combine(shpB, shpA,
                                       TGIS_TopologyCombineType.Difference
                                     );
            if (tmp != null)
            {
                layerObj.AddShape(tmp);
                tmp = null;
            }
            GIS.InvalidateWholeMap();
        }

        private void btnAxorB_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape tmp;

            layerObj.RevertShapes();
            tmp = topologyObj.Combine(shpA, shpB,
                                       TGIS_TopologyCombineType.SymmetricalDifference
                                     );
            if (tmp != null)
            {
                layerObj.AddShape(tmp);
                tmp = null;
            }
            GIS.InvalidateWholeMap();
        }
    }
}
