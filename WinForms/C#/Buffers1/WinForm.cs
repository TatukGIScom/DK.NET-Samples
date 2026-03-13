using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Buffers1
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private long shp_id;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

        private System.Windows.Forms.ToolStripLabel statusStrip1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton btnPlus;
        private System.Windows.Forms.ToolStripButton btnMinus;

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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.ToolStripLabel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.btnPlus = new System.Windows.Forms.ToolStripButton();
            this.btnMinus = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // statusStrip2
            // 
            this.statusStrip2.Location = new System.Drawing.Point(0, 447);
            this.statusStrip2.Name = "stripBar1";
            this.statusStrip2.Size = new System.Drawing.Size(592, 19);
            this.statusStrip2.TabIndex = 1;
            this.statusStrip2.Items.AddRange(new ToolStripItem[] { 
                this.statusStrip1 
            });
            //
            // toolStripLabel1
            // 
            this.statusStrip1.Name = "toolStripLabel1";
            this.statusStrip1.Text = "Click on shapes to choose one for buffer creation";
            this.statusStrip1.Width = 575;
            // 
            // GIS
            // 
            this.GIS.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 25);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 422);
            this.GIS.TabIndex = 1;
            this.GIS.UseRTree = false;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 25);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.toolStrip3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(264, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(328, 25);
            this.panel4.TabIndex = 2;
            // 
            // toolBar3
            // 
            this.toolStrip3.AutoSize = false;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnPlus});
            this.toolStrip3.ImageList = this.imageList1;
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolBar3";
            this.toolStrip3.ShowItemToolTips = true;
            this.toolStrip3.Size = new System.Drawing.Size(328, 25);
            this.toolStrip3.TabIndex = 0;
            //this.toolStrip3.ButtonClick += new System.Windows.Forms.ToolStripButtonClickEventHandler(this.toolBar3_ButtonClick);
            // 
            // btnPlus
            // 
            this.btnPlus.ImageIndex = 1;
            this.btnPlus.Name = "btnPlus";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.trackBar1);
            this.panel3.Controls.Add(this.toolStrip2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(23, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(241, 25);
            this.panel3.TabIndex = 0;
            this.panel3.TabStop = true;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(0, 2);
            this.trackBar1.Maximum = 50;
            this.trackBar1.Minimum = -50;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(241, 23);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // toolBar2
            // 
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolBar2";
            this.toolStrip2.ShowItemToolTips = true;
            this.toolStrip2.Size = new System.Drawing.Size(241, 42);
            this.toolStrip2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(23, 25);
            this.panel2.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnMinus});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(23, 25);
            this.toolStrip1.TabIndex = 0;
            //this.toolStrip1.ButtonClick += new System.Windows.Forms.ToolStripButtonClickEventHandler(this.toolStrip1_ButtonClick);
            // 
            // btnMinus
            // 
            this.btnMinus.ImageIndex = 0;
            this.btnMinus.Name = "btnMinus";
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Buffers1";
            this.Load += new System.EventHandler(this.WinForm_Load);
            //((System.ComponentModel.ISupportInitialize)(this.statusStrip1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            //((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panel2.ResumeLayout(false);
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
            TGIS_LayerVector lb;

            // open a project
            GIS.Lock();
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\Topology\topology.shp");
            shp_id = 2;
            // create a layer for buffer
            lb = new TGIS_LayerVector();
            lb.Name = "buffer";
            lb.Transparency = 50;
            lb.Params.Area.Color = TGIS_Color.Red;
            GIS.Add(lb);
            GIS.Unlock();
            GIS.FullExtent();
        }

        private void GIS_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_Shape shp;

            if (GIS.IsEmpty) return;
            if (GIS.InPaint) return;

            // locate a shape after click
            ptg = GIS.ScreenToMap(new System.Drawing.Point(e.X, e.Y));
            shp = (TGIS_Shape)GIS.Locate(ptg, 5 / GIS.Zoom); // 5 pixels precision
                                                             // remember id to use buffer on selected shape
            if (shp != null)
            {
                shp_id = shp.Uid;
                shp.Flash();
            }
        }

        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_LayerVector lb;
            TGIS_Shape shp;
            TGIS_Shape tmp;
            TGIS_Topology tpl;

            ll = (TGIS_LayerVector)GIS.Items[0];
            if (ll == null) return;

            lb = (TGIS_LayerVector)GIS.Get("buffer");
            if (lb == null) return;

            shp = ll.GetShape(shp_id);
            if (shp == null) return;

            // create a buffer using topology
            tpl = new TGIS_Topology();
            try
            {
                lb.RevertShapes();
                tmp = tpl.MakeBuffer(shp, trackBar1.Value * 1000);
                if (tmp != null)
                {
                    lb.AddShape(tmp);
                    tmp = null;
                }
                // check extents
                GIS.RecalcExtent();
                GIS.FullExtent();
            }
            finally
            {
                tpl = null;
            }
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            //switch (toolStrip1.Buttons.IndexOf(e.Button))
            //{
            //    case 0:
            //        // change bar position and recalculate buffer
            //        if (trackBar1.Value > trackBar1.Minimum)
            //        {
            //            trackBar1.Value -= 1;
            //            trackBar1_Scroll(this, e);
            //        }
            //        break;
            //}
        }

        private void toolBar3_ButtonClick(object sender, System.EventArgs e)
        {
            //    switch (toolBar3.Buttons.IndexOf(e.Button))
            //    {
            //        case 0:
            //            // change bar position and recalculate buffer
            //            if (trackBar1.Value < trackBar1.Maximum)
            //            {
            //                trackBar1.Value += 1;
            //                trackBar1_Scroll(this, e);
            //            }
            //            break;
            //    }
        }
    }
}
