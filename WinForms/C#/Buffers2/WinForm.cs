using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Buffers2
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnMinus;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip stripBar1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStrip toolBar2;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStrip toolBar3;
        private System.Windows.Forms.ToolStripButton btnPlus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;

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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnMinus = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.toolBar3 = new System.Windows.Forms.ToolStrip();
            this.btnPlus = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.toolBar2 = new System.Windows.Forms.ToolStrip();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnMinus});
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(23, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnMinus
            // 
            this.btnMinus.ImageIndex = 0;
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Click += toolStrip1_ButtonClick;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Width = 575;
            // 
            // GIS
            // 
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraPosition = null;
            this.GIS.CursorForCameraRotation = null;
            this.GIS.CursorForCameraXY = null;
            this.GIS.CursorForCameraXYZ = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.CursorForSunPosition = null;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 25);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(477, 422);
            this.GIS.TabIndex = 3;
            this.GIS.UseRTree = false;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBox1.Location = new System.Drawing.Point(477, 25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(115, 422);
            this.textBox1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 25);
            this.panel1.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.toolBar3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(264, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(328, 25);
            this.panel5.TabIndex = 2;
            // 
            // toolBar3
            // 
            this.toolBar3.AutoSize = false;
            this.toolBar3.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnPlus});
            this.toolBar3.ImageList = this.imageList1;
            this.toolBar3.Location = new System.Drawing.Point(0, 0);
            this.toolBar3.Name = "toolBar3";
            this.toolBar3.ShowItemToolTips = true;
            this.toolBar3.Size = new System.Drawing.Size(328, 25);
            this.toolBar3.TabIndex = 0;
            // 
            // btnPlus
            // 
            this.btnPlus.ImageIndex = 1;
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Click += toolBar3_ButtonClick;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(23, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(241, 25);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.trackBar1);
            this.panel4.Controls.Add(this.toolBar2);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(241, 25);
            this.panel4.TabIndex = 0;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(0, 2);
            this.trackBar1.Maximum = 200;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(241, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // toolBar2
            // 
            this.toolBar2.Location = new System.Drawing.Point(0, 0);
            this.toolBar2.Name = "toolBar2";
            this.toolBar2.ShowItemToolTips = true;
            this.toolBar2.Size = new System.Drawing.Size(241, 42);
            this.toolBar2.TabIndex = 0;
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
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Buffers2";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
            TGIS_LayerAbstract la;
            TGIS_LayerVector lb;

            la = TGIS_Utils.GisCreateLayer("counties",
                   TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\Counties.SHP"
                 );
            GIS.Lock();
            GIS.Add(la);

            lb = new TGIS_LayerVector();
            lb.Name = "buffer";
            lb.Transparency = 70;
            lb.Params.Area.Color = TGIS_Color.Yellow;
            lb.CS = GIS.CS;
            GIS.Add(lb);

            GIS.Unlock();
            GIS.FullExtent();
            timer1_Tick(sender, e);
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (trackBar1.Value > trackBar1.Minimum + 25)
            {
                trackBar1.Value -= 25;
                timer1_Tick(this, e);
            }
            else if (trackBar1.Value > trackBar1.Minimum)
            {
                trackBar1.Value = trackBar1.Minimum;
                timer1_Tick(this, e);
            }
            //switch (toolStrip1.Buttons.IndexOf(e.Button))
            //{
            //    case 0:
            //        if (trackBar1.Value > trackBar1.Minimum + 25)
            //        {
            //            trackBar1.Value -= 25;
            //            timer1_Tick(this, e);
            //        }
            //        else if (trackBar1.Value > trackBar1.Minimum)
            //        {
            //            trackBar1.Value = trackBar1.Minimum;
            //            timer1_Tick(this, e);
            //        }
            //        break;
            //}
        }

        private void toolBar3_ButtonClick(object sender, System.EventArgs e)
        {
            if (trackBar1.Value < trackBar1.Maximum - 25)
            {
                trackBar1.Value += 25;
                timer1_Tick(this, e);
            }
            else if (trackBar1.Value < trackBar1.Maximum)
            {
                trackBar1.Value = trackBar1.Maximum;
                timer1_Tick(this, e);
            }
            //switch (toolBar3.Buttons.IndexOf(e.Button))
            //{
            //    case 0:
            //        if (trackBar1.Value < trackBar1.Maximum - 25)
            //        {
            //            trackBar1.Value += 25;
            //            timer1_Tick(this, e);
            //        }
            //        else if (trackBar1.Value < trackBar1.Maximum)
            //        {
            //            trackBar1.Value = trackBar1.Maximum;
            //            timer1_Tick(this, e);
            //        }
            //        break;
            //}
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_LayerVector lb;
            TGIS_Shape shp;
            TGIS_Shape tmp;
            TGIS_Shape buf;
            TGIS_Topology tpl;

            timer1.Enabled = false;

            try
            {
                // find buffer for vistual river
                ll = (TGIS_LayerVector)GIS.Get("counties");
                if (ll == null) return;

                lb = (TGIS_LayerVector)GIS.Get("buffer");
                if (lb == null) return;

                shp = ll.FindFirst(TGIS_Utils.GisWholeWorld(), "NAME='Merced'");
                if (shp == null) return;

                tpl = new TGIS_Topology();
                try
                {
                    lb.RevertShapes();
                    tmp = tpl.MakeBuffer(shp, (1.0 * trackBar1.Value) / 100.0);
                    if (tmp != null)
                    {
                        buf = lb.AddShape(tmp);
                        tmp = null;
                    }
                    else
                        buf = null;
                }
                finally
                {
                    tpl = null;
                }

                // find all states crossing by buffer of Vistula river
                if (buf == null) return;

                ll = (TGIS_LayerVector)GIS.Get("counties");
                ll.IgnoreShapeParams = false;
                if (ll == null) return;
                ll.RevertShapes();
                textBox1.Clear();

                // check all shapes
                tmp = ll.FindFirst(buf.Extent);
                while (tmp != null)
                {
                    // if any has a common point with buffer mark it as blue
                    if (buf.IsCommonPoint(tmp))
                    {
                        tmp = tmp.MakeEditable();
                        textBox1.AppendText(tmp.GetField("name").ToString() + "\r\n");
                        tmp.Params.Area.Color = TGIS_Color.Blue;
                    }
                    tmp = ll.FindNext();
                }
            }
            finally
            {
                GIS.InvalidateWholeMap();
            }
        }

        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            timer1.Enabled = false;
            stripBar1.Items[0].Text = trackBar1.Value.ToString() + " km";
            timer1.Enabled = true;
        }
    }
}
