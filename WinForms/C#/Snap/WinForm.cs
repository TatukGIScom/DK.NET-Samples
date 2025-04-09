using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Snap
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnWithoutSnapping;
        private System.Windows.Forms.Button btnWithSnapping;
        private System.Windows.Forms.Timer tmrWithSnapping;
        private System.Windows.Forms.Timer tmrWithoutSnapping;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TGIS_Shape shpPolice;                   // police shape
        private int cntPoint;                                   // number of evaluated points

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnWithSnapping = new System.Windows.Forms.Button();
            this.btnWithoutSnapping = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tmrWithSnapping = new System.Windows.Forms.Timer(this.components);
            this.tmrWithoutSnapping = new System.Windows.Forms.Timer(this.components);
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnWithSnapping);
            this.panel1.Controls.Add(this.btnWithoutSnapping);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 29);
            this.panel1.TabIndex = 0;
            // 
            // btnWithSnapping
            // 
            this.btnWithSnapping.Location = new System.Drawing.Point(129, 2);
            this.btnWithSnapping.Name = "btnWithSnapping";
            this.btnWithSnapping.Size = new System.Drawing.Size(144, 25);
            this.btnWithSnapping.TabIndex = 2;
            this.btnWithSnapping.Text = "Start (with snapping)";
            this.btnWithSnapping.Click += new System.EventHandler(this.btnWithSnapping_Click);
            // 
            // btnWithoutSnapping
            // 
            this.btnWithoutSnapping.Location = new System.Drawing.Point(0, 2);
            this.btnWithoutSnapping.Name = "btnWithoutSnapping";
            this.btnWithoutSnapping.Size = new System.Drawing.Size(129, 25);
            this.btnWithoutSnapping.TabIndex = 1;
            this.btnWithoutSnapping.Text = "Start w/o snapping";
            this.btnWithoutSnapping.Click += new System.EventHandler(this.btnWithoutSnapping_Click);
            // 
            // toolBar1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolBar1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 29);
            this.toolStrip1.TabIndex = 0;
            // 
            // tmrWithSnapping
            // 
            this.tmrWithSnapping.Interval = 50;
            this.tmrWithSnapping.Tick += new System.EventHandler(this.tmrWithSnapping_Tick);
            // 
            // tmrWithoutSnapping
            // 
            this.tmrWithoutSnapping.Interval = 50;
            this.tmrWithoutSnapping.Tick += new System.EventHandler(this.tmrWithoutSnapping_Tick);
            // 
            // GIS
            // 
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.IncrementalPaint = false;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(592, 437);
            this.GIS.TabIndex = 1;
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
            this.Text = "TatukGIS Samples - Snapping";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Leave += new System.EventHandler(this.WinForm_Leave);
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

            // lets open streets
            GIS.Lock();
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\San Bernardino\TIGER\tl_2008_06071_edges_trunc.SHP");
            GIS.Zoom = GIS.Zoom * 5;
            GIS.CenterViewport(TGIS_Utils.GisPoint(-117.0208, 34.0629));

            // now create a points layer
            ll = new TGIS_LayerVector();
            ll.Path = "trackingpoints";
            ll.CS = GIS.CS;
            GIS.Add(ll);
            ll.Params.Labels.Allocator = false;

            // and attach to it our test police-car point
            shpPolice = ll.CreateShape(TGIS_ShapeType.Point);
            shpPolice.Params.Marker.Symbol =
                TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\police.bmp?TRUE");
            shpPolice.Params.Marker.Size = -13;
            shpPolice.Params.Labels.OutlineWidth = 0;
            shpPolice.Params.Labels.Pattern = TGIS_BrushStyle.Clear;
            shpPolice.Params.Labels.Position = TGIS_LabelPosition.DownCenter;
            shpPolice.Params.Labels.Value = "112";
            GIS.Unlock();
        }

        private void WinForm_Leave(object sender, System.EventArgs e)
        {
            // close viewer - all layers and shapes will be free
            GIS.Close();
        }

        private void btnWithoutSnapping_Click(object sender, System.EventArgs e)
        {
            btnWithoutSnapping.Enabled = false;
            btnWithSnapping.Enabled = false;

            // Let's travel from center of the screen
            shpPolice.SetPosition(GIS.Center, null, 0);

            cntPoint = 0;
            tmrWithoutSnapping.Enabled = true;
        }

        private void btnWithSnapping_Click(object sender, System.EventArgs e)
        {
            btnWithoutSnapping.Enabled = false;
            btnWithSnapping.Enabled = false;

            // Let's travel from Left-dwon to Upper-right
            shpPolice.SetPosition(GIS.Center, null, 0);

            cntPoint = 0;
            tmrWithSnapping.Enabled = true;
        }

        private void tmrWithSnapping_Tick(object sender, System.EventArgs e)
        {
            TGIS_Point ptg;

            // to protect against circular calling
            tmrWithSnapping.Enabled = false;

            // let's move in some aribtrary direction
            // normally you can read here a GPS position etc.
            ptg.X = shpPolice.Centroid().X - 0.00020;
            ptg.Y = shpPolice.Centroid().Y + 0.00010;

            // move icon over the map
            // is not elegant to access Items[0] but its only sample :>
            shpPolice.Lock(TGIS_Lock.Projection);
            shpPolice.SetPosition(ptg,
                                                         (TGIS_LayerVector)GIS.Items[0],
                                                         0.05
                                                     );
            shpPolice.Unlock();
            ++cntPoint;

            // not end? - reenable the timer
            if (cntPoint < 120) tmrWithSnapping.Enabled = true;
            else
            {
                btnWithoutSnapping.Enabled = true;
                btnWithSnapping.Enabled = true;
            };
        }

        private void tmrWithoutSnapping_Tick(object sender, System.EventArgs e)
        {
            TGIS_Point ptg;

            // to protect against circular calling
            tmrWithoutSnapping.Enabled = false;

            // let's move in some aribtrary direction
            // normally you can read here a GPS position etc.
            ptg.X = shpPolice.Centroid().X - 0.00020;
            ptg.Y = shpPolice.Centroid().Y + 0.00010;

            // move icon over the map
            shpPolice.Lock(TGIS_Lock.Projection);
            shpPolice.SetPosition(ptg, null, 0);
            shpPolice.Unlock();
            ++cntPoint;

            // not end? - reenable the timer
            if (cntPoint < 120) tmrWithoutSnapping.Enabled = true;
            else
            {
                btnWithoutSnapping.Enabled = true;
                btnWithSnapping.Enabled = true;
            }
        }
    }
}
