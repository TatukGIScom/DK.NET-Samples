using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace MiniMap
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.Windows.Forms.Panel paLeft;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnZoom;
        private System.Windows.Forms.ToolStripButton btnDrag;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GISm;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Rectcolor1;
        private System.Windows.Forms.ToolStripMenuItem Outlinecolor1;
        private System.Windows.Forms.ToolStripMenuItem colorR;
        private System.Windows.Forms.ToolStripMenuItem color0;
        private System.Windows.Forms.GroupBox gbCanvasInfo;
        private System.Windows.Forms.Label lbP4;
        private System.Windows.Forms.Label lbP3;
        private System.Windows.Forms.Label lbP2;
        private System.Windows.Forms.Label lbP1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.ColorDialog dlgColor;

        private const string MINIMAP_R_NAME = "minimap_rect";
        private const string MINIMAP_O_NAME = "minimap_rect_outline";
        private TGIS_Shape minishp;                             //minimap shape
        private TGIS_Shape minishpo;                            //minimap shape outline
        private bool fminiMove;                                 //flag for move mini rectangle
        private TGIS_Point lP1, lP2, lP3, lP4;
        private bool ctrlPressed;

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
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.paLeft = new System.Windows.Forms.Panel();
            this.GISm = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.Rectcolor1 = new System.Windows.Forms.ToolStripMenuItem();
            this.Outlinecolor1 = new System.Windows.Forms.ToolStripMenuItem();
            this.colorR = new System.Windows.Forms.ToolStripMenuItem();
            this.color0 = new System.Windows.Forms.ToolStripMenuItem();
            this.gbCanvasInfo = new System.Windows.Forms.GroupBox();
            this.lbP4 = new System.Windows.Forms.Label();
            this.lbP3 = new System.Windows.Forms.Label();
            this.lbP2 = new System.Windows.Forms.Label();
            this.lbP1 = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.paLeft.SuspendLayout();
            this.gbCanvasInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut,
            this.toolStripSeparator1,
            this.btnZoom,
            this.btnDrag});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(740, 36);
            this.toolStrip1.TabIndex = 0;
            //this.toolStrip1.ButtonClick += new System.Windows.Forms.ToolStripButtonClickEventHandler(this.toolStrip1_ButtonClick);
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
            this.btnZoomIn.ToolTipText = "Zoom In";
            this.btnZoomIn.Click += toolStrip1_ButtonClick;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "Zoom Out";
            this.btnZoomOut.Click += toolStrip1_ButtonClick;
            // 
            // toolStripButton1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // btnZoom
            // 
            this.btnZoom.ImageIndex = 3;
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Checked = true;
            this.btnZoom.ToolTipText = "Zoom";
            this.btnZoom.Click += toolStrip1_ButtonClick;
            // 
            // btnDrag
            // 
            this.btnDrag.ImageIndex = 4;
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.ToolTipText = "Drag";
            this.btnDrag.Click += toolStrip1_ButtonClick;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 558);
            this.stripBar1.Margin = new System.Windows.Forms.Padding(4);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(740, 24);
            this.stripBar1.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Width = 719;
            // 
            // paLeft
            // 
            this.paLeft.Controls.Add(this.GISm);
            this.paLeft.Controls.Add(this.gbCanvasInfo);
            this.paLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.paLeft.Location = new System.Drawing.Point(0, 36);
            this.paLeft.Margin = new System.Windows.Forms.Padding(4);
            this.paLeft.Name = "paLeft";
            this.paLeft.Size = new System.Drawing.Size(251, 522);
            this.paLeft.TabIndex = 2;
            // 
            // GISm
            // 
            this.GISm.ContextMenuStrip = this.contextMenuStrip1;
            this.GISm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GISm.Location = new System.Drawing.Point(0, 304);
            this.GISm.Margin = new System.Windows.Forms.Padding(4);
            this.GISm.Name = "GISm";
            this.GISm.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GISm.SelectionTransparency = 100;
            this.GISm.Size = new System.Drawing.Size(251, 218);
            this.GISm.TabIndex = 1;
            this.GISm.UseRTree = false;
            this.GISm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GISm_MouseDown);
            this.GISm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GISm_MouseMove);
            this.GISm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GISm_MouseUp);
            // 
            // contextMenu1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.Rectcolor1,
            this.Outlinecolor1});
            // 
            // Rectcolor1
            // 
            //this.Rectcolor1.Index = 0;
            this.Rectcolor1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.colorR});
            this.Rectcolor1.Text = "Rectangle";
            // 
            // Outlinecolor1
            // 
            //this.Outlinecolor1.Index = 1;
            this.Outlinecolor1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.color0});
            this.Outlinecolor1.Text = "Outline";
            // 
            // colorR
            // 
            //this.colorR.Index = 0;
            this.colorR.Text = "color...";
            this.colorR.Click += new System.EventHandler(this.colorR_Click);
            // 
            // color0
            // 
            //this.color0.Index = 0;
            this.color0.Text = "color...";
            this.color0.Click += new System.EventHandler(this.color0_Click);
            // 
            // gbCanvasInfo
            // 
            this.gbCanvasInfo.Controls.Add(this.lbP4);
            this.gbCanvasInfo.Controls.Add(this.lbP3);
            this.gbCanvasInfo.Controls.Add(this.lbP2);
            this.gbCanvasInfo.Controls.Add(this.lbP1);
            this.gbCanvasInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCanvasInfo.Location = new System.Drawing.Point(0, 0);
            this.gbCanvasInfo.Margin = new System.Windows.Forms.Padding(4);
            this.gbCanvasInfo.Name = "gbCanvasInfo";
            this.gbCanvasInfo.Padding = new System.Windows.Forms.Padding(4);
            this.gbCanvasInfo.Size = new System.Drawing.Size(251, 171);
            this.gbCanvasInfo.TabIndex = 0;
            this.gbCanvasInfo.TabStop = false;
            this.gbCanvasInfo.Text = " Map extent: ";
            // 
            // lbP4
            // 
            this.lbP4.Location = new System.Drawing.Point(10, 90);
            this.lbP4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbP4.Name = "lbP4";
            this.lbP4.Size = new System.Drawing.Size(220, 16);
            this.lbP4.TabIndex = 3;
            this.lbP4.Text = "p4: x,y ";
            // 
            // lbP3
            // 
            this.lbP3.Location = new System.Drawing.Point(10, 70);
            this.lbP3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbP3.Name = "lbP3";
            this.lbP3.Size = new System.Drawing.Size(220, 16);
            this.lbP3.TabIndex = 2;
            this.lbP3.Text = "p3: x,y ";
            // 
            // lbP2
            // 
            this.lbP2.Location = new System.Drawing.Point(10, 50);
            this.lbP2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbP2.Name = "lbP2";
            this.lbP2.Size = new System.Drawing.Size(220, 16);
            this.lbP2.TabIndex = 1;
            this.lbP2.Text = "p2: x,y ";
            // 
            // lbP1
            // 
            this.lbP1.Location = new System.Drawing.Point(10, 30);
            this.lbP1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbP1.Name = "lbP1";
            this.lbP1.Size = new System.Drawing.Size(220, 16);
            this.lbP1.TabIndex = 0;
            this.lbP1.Text = "p1: x,y ";
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(251, 36);
            this.GIS.Margin = new System.Windows.Forms.Padding(4);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(489, 522);
            this.GIS.TabIndex = 3;
            this.GIS.UseRTree = false;
            this.GIS.VisibleExtentChangeEvent += new System.EventHandler(this.GIS_VisibleExtentChange);
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(740, 582);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.paLeft);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(200, 120);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Minimap";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WinForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WinForm_KeyUp);
            this.paLeft.ResumeLayout(false);
            this.gbCanvasInfo.ResumeLayout(false);
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
            TGIS_LayerVector llm;
            TGIS_LayerVector lv;
            TGIS_LayerVector lw;

            GIS.Lock();
            GISm.Lock();

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\poland.ttkproject");

            GIS.SetCSByEPSG(2180);

            llm = (TGIS_LayerVector)
                      (TGIS_Utils.GisCreateLayer(
                          "country",
                          TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\country.shp"
                          )
                       );
            llm.UseConfig = false;
            llm.Params.Area.Color = TGIS_Color.White;
            llm.Params.Area.OutlineColor = TGIS_Color.Silver;
            GISm.Add(llm); //Add layer to minimap

            lv = new TGIS_LayerVector(); //Creating minimap transparent rectangle
            lv.Transparency = 30;
            lv.Params.Area.Color = TGIS_Color.Red;
            lv.Params.Area.OutlineWidth = -2;
            lv.Name = MINIMAP_R_NAME;
            lv.CS = llm.CS;
            GISm.Add(lv);
            minishp = ((TGIS_LayerVector)(GISm.Get(MINIMAP_R_NAME))).CreateShape(TGIS_ShapeType.Polygon);
            lw = new TGIS_LayerVector();
            lw.Params.Line.Color = TGIS_Color.Maroon;
            lw.Params.Line.Width = -2;
            lw.Name = MINIMAP_O_NAME;
            lw.CS = llm.CS;
            GISm.Add(lw);
            minishpo = ((TGIS_LayerVector)(GISm.Get(MINIMAP_O_NAME))).CreateShape(TGIS_ShapeType.Arc);

            GIS.Unlock();
            GISm.Unlock();

            GISm.FullExtent();
            GIS.FullExtent();

            GISm.RestrictedExtent = GISm.Extent;
            minishp.Layer.Extent = GISm.Extent;
            GISm.Cursor = Cursors.Hand;
            fminiMove = false;
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if(sender == btnFullExtent)
            {
                GIS.FullExtent();
            }
            else if(sender == btnZoomIn)
            {
                GIS.Zoom = GIS.Zoom * 2;
            }
            else if(sender == btnZoomOut)
            {
                GIS.Zoom = GIS.Zoom / 2;
            }
            else if(sender == btnZoom)
            {
                btnDrag.Checked = false;
                btnZoom.Checked = true;
                GIS.Mode = TGIS_ViewerMode.Zoom;
            }
            else if(sender == btnDrag)
            {
                btnDrag.Checked = true;
                btnZoom.Checked = false;
                GIS.Mode = TGIS_ViewerMode.Drag;
            }
        }

        private void GISm_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GIS.IsEmpty) return;
            if (e.Button == System.Windows.Forms.MouseButtons.Right) return;
            fminiMove = true;
        }

        private void GISm_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;

            if (GIS.IsEmpty) return;

            if ((!fminiMove) && (!ctrlPressed)) return;
            ptg = GISm.ScreenToMap(new Point(e.X, e.Y));
            minishp.SetPosition(ptg, null, 1);
            GISm.InvalidateWholeMap();
            if (ctrlPressed)
                GISm_MouseUp(sender, e);
        }

        private void GISm_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        // Mouse click to change rectangle position on minimap
        {
            TGIS_Point ptg;
            TGIS_Point p1, p2, p3, p4;

            if (GIS.IsEmpty) return;

            if (!fminiMove) return;

            ptg = GISm.ScreenToMap(new Point(e.X, e.Y));

            minishp.SetPosition(ptg, null, 1);

            GISm.InvalidateWholeMap();
            fminiMove = false;

            p1 = minishp.GetPoint(0, 0);
            p2 = minishp.GetPoint(0, 1);
            p3 = minishp.GetPoint(0, 2);
            p4.X = p1.X + (p2.X - p1.X) / 2;
            p4.Y = p1.Y + (p3.Y - p2.Y) / 2;
            GIS.Center = GISm.CS.ToCS(GIS.CS, p4);
        }

        private void GIS_ZoomChange(object sender, System.EventArgs e)
        {
            miniMapRefresh();
        }

        private void GIS_VisibleExtentChange(object sender, System.EventArgs e)
        {
            TGIS_Extent ex;

            ex = GIS.VisibleExtent;
            lP1 = new TGIS_Point(ex.XMin, ex.YMin);
            lP2 = new TGIS_Point(ex.XMax, ex.YMin);
            lP3 = new TGIS_Point(ex.XMax, ex.YMax);
            lP4 = new TGIS_Point(ex.XMin, ex.YMax);
            lbP1.Text = String.Format("P1 : x: {0:F2}   y: {1:F2}", lP1.X, lP1.Y);
            lbP2.Text = String.Format("P2 : x: {0:F2}   y: {1:F2}", lP2.X, lP2.Y);
            lbP3.Text = String.Format("P3 : x: {0:F2}   y: {1:F2}", lP3.X, lP3.Y);
            lbP4.Text = String.Format("P4 : x: {0:F2}   y: {1:F2}", lP4.X, lP4.Y);
            miniMapRefresh();
        }

        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;

            if (GIS.IsEmpty) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            stripBar1.Items[0].Text = String.Format("x: {0:F2}   y: {1:F2}", ptg.X, ptg.Y);
        }

        private void colorR_Click(object sender, System.EventArgs e)
        // Change color of the rectangle
        {
            TGIS_LayerVector lv;

            if (dlgColor.ShowDialog() != DialogResult.OK) return;
            lv = (TGIS_LayerVector)GISm.Get(MINIMAP_R_NAME);
            lv.Params.Area.Color = TGIS_Color.FromARGB((uint)dlgColor.Color.ToArgb());
            GISm.InvalidateWholeMap();
        }

        private void color0_Click(object sender, System.EventArgs e)
        // Change color of the outline
        {
            TGIS_LayerVector lv;

            if (dlgColor.ShowDialog() != DialogResult.OK) return;
            lv = (TGIS_LayerVector)GISm.Get(MINIMAP_O_NAME);
            lv.Params.Line.Color = TGIS_Color.FromARGB((uint)dlgColor.Color.ToArgb());
            GISm.InvalidateWholeMap();
        }

        private void miniMapRefresh()
        {
            TGIS_Point ptg1;
            TGIS_Point ptg2;
            TGIS_Point ptg3;
            TGIS_Point ptg4;
            TGIS_Extent ex;

            if (GIS.IsEmpty) return;

            ex = GISm.CS.ExtentFromCS(GIS.CS, GIS.VisibleExtent);
            ex = GIS.UnrotatedExtent(ex);

            if ((ex.XMin < -360) &&
                (ex.XMax > 360) &&
                (ex.YMin < -180) &&
                (ex.YMax > 180)) return;

            ptg1 = new TGIS_Point(ex.XMin, ex.YMin);
            ptg2 = new TGIS_Point(ex.XMax, ex.YMin);
            ptg3 = new TGIS_Point(ex.XMax, ex.YMax);
            ptg4 = new TGIS_Point(ex.XMin, ex.YMax);

            if (minishp != null)
            {
                minishp.Reset();
                minishp.Lock(TGIS_Lock.Extent);
                minishp.AddPart();
                minishp.AddPoint(ptg1);
                minishp.AddPoint(ptg2);
                minishp.AddPoint(ptg3);
                minishp.AddPoint(ptg4);
                minishp.Unlock();
            }

            if (minishpo != null)
            {
                minishpo.Reset();
                minishpo.Lock(TGIS_Lock.Extent);
                minishpo.AddPart();
                minishpo.AddPoint(ptg1);
                minishpo.AddPoint(ptg2);
                minishpo.AddPoint(ptg3);
                minishpo.AddPoint(ptg4);
                minishpo.AddPoint(ptg1);
                minishpo.Unlock();
            }
            GISm.InvalidateWholeMap();
        }

        private void WinForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        // On minimap: Mouse move + ctrl to preview rectangle position 
        {
            if (e.Control)
                ctrlPressed = true;
        }

        private void WinForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (ctrlPressed)
                ctrlPressed = false;
        }
    }
}
