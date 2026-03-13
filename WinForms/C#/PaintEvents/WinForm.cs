using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;

namespace PaintEvents
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
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox chkDrag;
        private System.Windows.Forms.ToolTip toolTip1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private Panel panel2;
        private CheckBox chkPrintBmpWithEvents;
        private Button btnTestPrintBmp;
        private CheckBox chkAfterPaintRendererEvent;
        private CheckBox chkAfterPaintEvent;
        private CheckBox chkPaintExtraEvent;
        private CheckBox chkBeforePaintEvent;
        private CheckBox chkBeforePaintRendererEvent;
        private System.Windows.Forms.Panel panel1;
        private SaveFileDialog saveFileDialog1;
        private ComboBox cbRenderer;
        private Label lblRenderer;
        private TGIS_Point center_ptg;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.toolTip1.SetToolTip(this.chkDrag, "Drag mode ON/OFF");
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
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.chkDrag = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbRenderer = new System.Windows.Forms.ComboBox();
            this.chkPrintBmpWithEvents = new System.Windows.Forms.CheckBox();
            this.btnTestPrintBmp = new System.Windows.Forms.Button();
            this.chkAfterPaintRendererEvent = new System.Windows.Forms.CheckBox();
            this.chkAfterPaintEvent = new System.Windows.Forms.CheckBox();
            this.chkPaintExtraEvent = new System.Windows.Forms.CheckBox();
            this.chkBeforePaintEvent = new System.Windows.Forms.CheckBox();
            this.chkBeforePaintRendererEvent = new System.Windows.Forms.CheckBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lblRenderer = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 466);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(621, 19);
            this.stripBar1.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut,
            this.toolStripSeparator1});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(621, 24);
            this.toolStrip1.TabIndex = 2;
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += toolStrip1_ButtonClick;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageIndex = 1;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.ToolTipText = "ZoomIn";
            this.btnZoomIn.Click += toolStrip1_ButtonClick;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "ZoomOut";
            this.btnZoomOut.Click += toolStrip1_ButtonClick;
            // 
            // toolStripButton1
            // 
            this.toolStripSeparator1.Name = "toolStripButton1";
            // 
            // chkDrag
            // 
            this.chkDrag.Location = new System.Drawing.Point(77, 2);
            this.chkDrag.Name = "chkDrag";
            this.chkDrag.Size = new System.Drawing.Size(97, 22);
            this.chkDrag.TabIndex = 5;
            this.chkDrag.Text = "Dragging";
            this.chkDrag.Click += new System.EventHandler(this.chkDrag_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkDrag);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(621, 24);
            this.panel1.TabIndex = 2;
            // 
            // GIS
            // 
            this.GIS.AutoStyle = true;
            this.GIS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Level = 28.140189979287609D;
            this.GIS.Location = new System.Drawing.Point(0, 24);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(442, 442);
            this.GIS.TabIndex = 3;
            this.GIS.BeforePaintEvent += new TatukGIS.NDK.TGIS_PaintEvent(this.GIS_BeforePaintEvent);
            this.GIS.BeforePaintRendererEvent += new TatukGIS.NDK.TGIS_RendererEvent(this.GIS_BeforePaintRendererEvent);
            this.GIS.AfterPaintEvent += new TatukGIS.NDK.TGIS_PaintEvent(this.GIS_AfterPaintEvent);
            this.GIS.AfterPaintRendererEvent += new TatukGIS.NDK.TGIS_RendererEvent(this.GIS_AfterPaintRendererEvent);
            this.GIS.PaintExtraEvent += new TatukGIS.NDK.TGIS_RendererEvent(this.GIS_PaintExtraEvent);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblRenderer);
            this.panel2.Controls.Add(this.cbRenderer);
            this.panel2.Controls.Add(this.chkPrintBmpWithEvents);
            this.panel2.Controls.Add(this.btnTestPrintBmp);
            this.panel2.Controls.Add(this.chkAfterPaintRendererEvent);
            this.panel2.Controls.Add(this.chkAfterPaintEvent);
            this.panel2.Controls.Add(this.chkPaintExtraEvent);
            this.panel2.Controls.Add(this.chkBeforePaintEvent);
            this.panel2.Controls.Add(this.chkBeforePaintRendererEvent);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(442, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(179, 442);
            this.panel2.TabIndex = 4;
            // 
            // cbRenderer
            // 
            this.cbRenderer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRenderer.FormattingEnabled = true;
            this.cbRenderer.Location = new System.Drawing.Point(13, 278);
            this.cbRenderer.Name = "cbRenderer";
            this.cbRenderer.Size = new System.Drawing.Size(150, 21);
            this.cbRenderer.TabIndex = 8;
            this.cbRenderer.SelectedIndexChanged += new System.EventHandler(this.cbRenderer_SelectedIndexChanged);
            // 
            // chkPrintBmpWithEvents
            // 
            this.chkPrintBmpWithEvents.AutoSize = true;
            this.chkPrintBmpWithEvents.Checked = true;
            this.chkPrintBmpWithEvents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintBmpWithEvents.Location = new System.Drawing.Point(13, 200);
            this.chkPrintBmpWithEvents.Margin = new System.Windows.Forms.Padding(2);
            this.chkPrintBmpWithEvents.Name = "chkPrintBmpWithEvents";
            this.chkPrintBmpWithEvents.Size = new System.Drawing.Size(125, 17);
            this.chkPrintBmpWithEvents.TabIndex = 6;
            this.chkPrintBmpWithEvents.Text = "PrintBmp with events";
            this.chkPrintBmpWithEvents.UseVisualStyleBackColor = true;
            // 
            // btnTestPrintBmp
            // 
            this.btnTestPrintBmp.Location = new System.Drawing.Point(13, 170);
            this.btnTestPrintBmp.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestPrintBmp.Name = "btnTestPrintBmp";
            this.btnTestPrintBmp.Size = new System.Drawing.Size(150, 22);
            this.btnTestPrintBmp.TabIndex = 5;
            this.btnTestPrintBmp.Text = "Test PrintBmp";
            this.btnTestPrintBmp.UseVisualStyleBackColor = true;
            this.btnTestPrintBmp.Click += new System.EventHandler(this.btnTestPrintBmp_Click);
            // 
            // chkAfterPaintRendererEvent
            // 
            this.chkAfterPaintRendererEvent.AutoSize = true;
            this.chkAfterPaintRendererEvent.Checked = true;
            this.chkAfterPaintRendererEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAfterPaintRendererEvent.Location = new System.Drawing.Point(13, 106);
            this.chkAfterPaintRendererEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkAfterPaintRendererEvent.Name = "chkAfterPaintRendererEvent";
            this.chkAfterPaintRendererEvent.Size = new System.Drawing.Size(144, 17);
            this.chkAfterPaintRendererEvent.TabIndex = 4;
            this.chkAfterPaintRendererEvent.Text = "AfterPaintRendererEvent";
            this.chkAfterPaintRendererEvent.UseVisualStyleBackColor = true;
            this.chkAfterPaintRendererEvent.CheckedChanged += new System.EventHandler(this.chkAfterPaintRendererEvent_CheckedChanged);
            // 
            // chkAfterPaintEvent
            // 
            this.chkAfterPaintEvent.AutoSize = true;
            this.chkAfterPaintEvent.Checked = true;
            this.chkAfterPaintEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAfterPaintEvent.Location = new System.Drawing.Point(13, 84);
            this.chkAfterPaintEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkAfterPaintEvent.Name = "chkAfterPaintEvent";
            this.chkAfterPaintEvent.Size = new System.Drawing.Size(100, 17);
            this.chkAfterPaintEvent.TabIndex = 3;
            this.chkAfterPaintEvent.Text = "AfterPaintEvent";
            this.chkAfterPaintEvent.UseVisualStyleBackColor = true;
            this.chkAfterPaintEvent.CheckedChanged += new System.EventHandler(this.chkAfterPaintEvent_CheckedChanged);
            // 
            // chkPaintExtraEvent
            // 
            this.chkPaintExtraEvent.AutoSize = true;
            this.chkPaintExtraEvent.Checked = true;
            this.chkPaintExtraEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPaintExtraEvent.Location = new System.Drawing.Point(13, 62);
            this.chkPaintExtraEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkPaintExtraEvent.Name = "chkPaintExtraEvent";
            this.chkPaintExtraEvent.Size = new System.Drawing.Size(102, 17);
            this.chkPaintExtraEvent.TabIndex = 2;
            this.chkPaintExtraEvent.Text = "PaintExtraEvent";
            this.chkPaintExtraEvent.UseVisualStyleBackColor = true;
            this.chkPaintExtraEvent.CheckedChanged += new System.EventHandler(this.chkPaintExtraEvent_CheckedChanged);
            // 
            // chkBeforePaintEvent
            // 
            this.chkBeforePaintEvent.AutoSize = true;
            this.chkBeforePaintEvent.Checked = true;
            this.chkBeforePaintEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBeforePaintEvent.Location = new System.Drawing.Point(13, 39);
            this.chkBeforePaintEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkBeforePaintEvent.Name = "chkBeforePaintEvent";
            this.chkBeforePaintEvent.Size = new System.Drawing.Size(109, 17);
            this.chkBeforePaintEvent.TabIndex = 1;
            this.chkBeforePaintEvent.Text = "BeforePaintEvent";
            this.chkBeforePaintEvent.UseVisualStyleBackColor = true;
            this.chkBeforePaintEvent.CheckedChanged += new System.EventHandler(this.chkBeforePaintEvent_CheckedChanged);
            // 
            // chkBeforePaintRendererEvent
            // 
            this.chkBeforePaintRendererEvent.AutoSize = true;
            this.chkBeforePaintRendererEvent.Checked = true;
            this.chkBeforePaintRendererEvent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBeforePaintRendererEvent.Location = new System.Drawing.Point(13, 17);
            this.chkBeforePaintRendererEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkBeforePaintRendererEvent.Name = "chkBeforePaintRendererEvent";
            this.chkBeforePaintRendererEvent.Size = new System.Drawing.Size(153, 17);
            this.chkBeforePaintRendererEvent.TabIndex = 0;
            this.chkBeforePaintRendererEvent.Text = "BeforePaintRendererEvent";
            this.chkBeforePaintRendererEvent.UseVisualStyleBackColor = true;
            this.chkBeforePaintRendererEvent.CheckedChanged += new System.EventHandler(this.chkBeforePaintRendererEvent_CheckedChanged);
            // 
            // lblRenderer
            // 
            this.lblRenderer.AutoSize = true;
            this.lblRenderer.Location = new System.Drawing.Point(13, 261);
            this.lblRenderer.Name = "lblRenderer";
            this.lblRenderer.Size = new System.Drawing.Size(54, 13);
            this.lblRenderer.TabIndex = 9;
            this.lblRenderer.Text = "Renderer:";
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(621, 485);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - PaintEvents";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
            TGIS_LayerSHP ll;

            // add layer
            ll = new TGIS_LayerSHP();
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\Counties.shp" ;
            ll.Params.Area.Color = TGIS_Color.LightGray;
            GIS.Add(ll);
            GIS.FullExtent();
            center_ptg = GIS.CenterPtg;

            cbRenderer.Items.Clear();
            for (int i = 0; i < TGIS_Utils.RendererManager.Names.Count; i++)
            {
                cbRenderer.Items.Add(TGIS_Utils.RendererManager.Names[i]);
            }
            cbRenderer.SelectedIndex = TGIS_Utils.RendererManager.Names.IndexOf(GIS.Renderer.GetType().Name);
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent)
            {
                GIS.FullExtent();
            }
            else if (sender == btnZoomIn) 
            {
                GIS.Zoom = GIS.Zoom * 2;
            }
            else if (sender == btnZoomOut) 
            {
                GIS.Zoom = GIS.Zoom / 2;
            }
        }

        private void chkDrag_Click(object sender, System.EventArgs e)
        {
            // change viewer mode
            if (chkDrag.Checked)
                GIS.Mode = TGIS_ViewerMode.Drag;
            else
                GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void toolStrip1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            if (toolStrip1.Items[0].Bounds.Contains(p) ||
                toolStrip1.Items[1].Bounds.Contains(p) ||
                toolStrip1.Items[2].Bounds.Contains(p))
                toolStrip1.Cursor = Cursors.Hand;
            else
                toolStrip1.Cursor = Cursors.Default;
        }

        private void chkBeforePaintRendererEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBeforePaintRendererEvent.Checked)
                GIS.BeforePaintRendererEvent += GIS_BeforePaintRendererEvent;
            else
                GIS.BeforePaintRendererEvent -= GIS_BeforePaintRendererEvent;
            GIS.Invalidate();
        }

        private void chkBeforePaintEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBeforePaintEvent.Checked)
                GIS.BeforePaintEvent += GIS_BeforePaintEvent;
            else
                GIS.BeforePaintEvent -= GIS_BeforePaintEvent;
            GIS.Invalidate();
        }

        private void chkPaintExtraEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPaintExtraEvent.Checked)
                GIS.PaintExtraEvent += GIS_PaintExtraEvent;
            else
                GIS.PaintExtraEvent -= GIS_PaintExtraEvent;
            GIS.Invalidate();
        }

        private void chkAfterPaintEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAfterPaintEvent.Checked)
                GIS.AfterPaintEvent += GIS_AfterPaintEvent;
            else
                GIS.AfterPaintEvent -= GIS_AfterPaintEvent;
            GIS.Invalidate();
        }

        private void chkAfterPaintRendererEvent_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAfterPaintRendererEvent.Checked)
                GIS.AfterPaintRendererEvent += GIS_AfterPaintRendererEvent;
            else
                GIS.AfterPaintRendererEvent -= GIS_AfterPaintRendererEvent;
            GIS.Invalidate();
        }

        private void btnTestPrintBmp_Click(object sender, EventArgs e)
        {
            Bitmap bitmap;

            saveFileDialog1.DefaultExt = "BMP";
            saveFileDialog1.Filter = "Window Bitmap (*.bmp)|*.BMP";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            bitmap = null;
            try
            {
                GIS.PrintBmp(ref bitmap, chkPrintBmpWithEvents.Checked);
                bitmap.Save(saveFileDialog1.FileName);
            }
            finally
            {
                bitmap.Dispose();
            }
        }

        private void GIS_BeforePaintRendererEvent(object _sender, TGIS_RendererEventArgs _e)
        {
            Object cn;
            TGIS_RendererAbstract rdr;
            TGIS_Color bcl;

            rdr = _e.Renderer;
            cn = rdr.CanvasNative();
            bcl = TGIS_Color.FromRGB(0xEE, 0xE8, 0xAA);
            if (cn is System.Drawing.Graphics)
                ((System.Drawing.Graphics)cn).Clear(System.Drawing.Color.FromArgb(bcl.A, bcl.R, bcl.G, bcl.B));
            else
            {   // the same with renderer method
                rdr.CanvasPen.Style = TGIS_PenStyle.Clear;
                rdr.CanvasBrush.Color = bcl;
                rdr.CanvasBrush.Style = TGIS_BrushStyle.Solid;
                rdr.CanvasDrawRectangle(new Rectangle(0, 0, GIS.Width, GIS.Height));
            }
            rdr.CanvasPen.Color = TGIS_Color.Blue;
            rdr.CanvasPen.Width = 1;
            rdr.CanvasBrush.Style = TGIS_BrushStyle.Clear;
            rdr.CanvasDrawRectangle(new Rectangle(10, 10, GIS.Width - 2*10, GIS.Height - 2*10));
        }

        private void GIS_BeforePaintEvent(object _sender, TGIS_PaintEventArgs _e)
        {
            TGIS_Color bcl;

            bcl = TGIS_Color.Blue;
            ((System.Drawing.Graphics)_e.Graphics).DrawRectangle(
                new System.Drawing.Pen(System.Drawing.Color.FromArgb(bcl.A, bcl.R, bcl.G, bcl.B), 1),
                new System.Drawing.Rectangle(40, 40, GIS.Width - 2 * 40, GIS.Height - 2 * 40));
        }

        private void GIS_PaintExtraEvent(object _sender, TGIS_RendererEventArgs _e)
        {
            string txt;
            Point pt;
            Point ptc;

            txt = "PaintExtra";
            _e.Renderer.CanvasFont.Name = "Courier New";
            _e.Renderer.CanvasFont.Size = 24;
            _e.Renderer.CanvasFont.Color = TGIS_Color.Blue;
            pt = _e.Renderer.CanvasTextExtent(txt);
            ptc = GIS.MapToScreen(center_ptg);
            _e.Renderer.CanvasDrawText(new Rectangle(ptc.X - pt.X / 2,
                                                     ptc.Y - pt.Y / 2,
                                                     pt.X, pt.Y),
                                       txt);
        }

        private void GIS_AfterPaintEvent(object _sender, TGIS_PaintEventArgs _e)
        {
            TGIS_Color bcl;

            bcl = TGIS_Color.Blue;
            ((System.Drawing.Graphics)_e.Graphics).DrawRectangle(
                new System.Drawing.Pen(System.Drawing.Color.FromArgb(bcl.A, bcl.R, bcl.G, bcl.B), 1),
                new System.Drawing.Rectangle(70, 70, GIS.Width - 2 * 70, GIS.Height - 2 * 70));
        }

        private void GIS_AfterPaintRendererEvent(object _sender, TGIS_RendererEventArgs _e)
        {
            TGIS_RendererAbstract rdr;

            rdr = _e.Renderer;
            rdr.CanvasPen.Color = TGIS_Color.Blue;
            rdr.CanvasPen.Width = 1;
            rdr.CanvasBrush.Style = TGIS_BrushStyle.Clear;
            rdr.CanvasDrawRectangle(new Rectangle(100, 100, GIS.Width - 2*100, GIS.Height - 2*100));
        }

        private void cbRenderer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRenderer.SelectedIndex >= 0 )
                GIS.Renderer = TGIS_Utils.RendererManager.CreateInstance(TGIS_Utils.RendererManager.Names[cbRenderer.SelectedIndex]);

            GIS.ControlUpdateWholeMap();
        }
    }
}
