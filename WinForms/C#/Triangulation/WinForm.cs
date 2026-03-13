using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Triangulation
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions2 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.imglst = new System.Windows.Forms.ImageList(this.components);
            this.GIS_Attributes = new TatukGIS.NDK.WinForms.TGIS_ControlAttributes();
            this.grpbxResult = new System.Windows.Forms.GroupBox();
            this.lblLayer = new System.Windows.Forms.Label();
            this.edtLayer = new System.Windows.Forms.TextBox();
            this.rbtnDelaunay = new System.Windows.Forms.RadioButton();
            this.rbtnVoronoi = new System.Windows.Forms.RadioButton();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.grpbxResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 382);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(584, 22);
            this.stripBar1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tlbr);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 26);
            this.panel1.TabIndex = 1;
            // 
            // tlbr
            // 
            this.tlbr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlbr.AutoSize = false;
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut});
            this.tlbr.Dock = System.Windows.Forms.DockStyle.None;
            this.tlbr.ImageList = this.imglst;
            this.tlbr.Location = new System.Drawing.Point(0, 0);
            this.tlbr.Name = "tlbr";
            this.tlbr.ShowItemToolTips = true;
            this.tlbr.Size = new System.Drawing.Size(584, 26);
            this.tlbr.TabIndex = 0;
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += tlbr_ButtonClick;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageIndex = 1;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.ToolTipText = "Zoom In";
            this.btnZoomIn.Click += tlbr_ButtonClick;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "Zoom Out";
            this.btnZoomOut.Click += tlbr_ButtonClick;
            // 
            // imglst
            // 
            this.imglst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglst.ImageStream")));
            this.imglst.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imglst.Images.SetKeyName(0, "FullExtent.bmp");
            this.imglst.Images.SetKeyName(1, "ZoomIn.bmp");
            this.imglst.Images.SetKeyName(2, "ZoomOut.bmp");
            // 
            // GIS_Attributes
            // 
            this.GIS_Attributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS_Attributes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GIS_Attributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.GIS_Attributes.Location = new System.Drawing.Point(424, 28);
            this.GIS_Attributes.Name = "GIS_Attributes";
            this.GIS_Attributes.Size = new System.Drawing.Size(160, 141);
            this.GIS_Attributes.TabIndex = 2;
            // 
            // grpbxResult
            // 
            this.grpbxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpbxResult.Controls.Add(this.lblLayer);
            this.grpbxResult.Controls.Add(this.edtLayer);
            this.grpbxResult.Controls.Add(this.rbtnDelaunay);
            this.grpbxResult.Controls.Add(this.rbtnVoronoi);
            this.grpbxResult.Location = new System.Drawing.Point(424, 175);
            this.grpbxResult.Name = "grpbxResult";
            this.grpbxResult.Size = new System.Drawing.Size(160, 92);
            this.grpbxResult.TabIndex = 3;
            this.grpbxResult.TabStop = false;
            this.grpbxResult.Text = "Result";
            // 
            // lblLayer
            // 
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(6, 68);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(68, 13);
            this.lblLayer.TabIndex = 3;
            this.lblLayer.Text = "Layer name :";
            // 
            // edtLayer
            // 
            this.edtLayer.Location = new System.Drawing.Point(80, 65);
            this.edtLayer.Name = "edtLayer";
            this.edtLayer.Size = new System.Drawing.Size(74, 20);
            this.edtLayer.TabIndex = 2;
            this.edtLayer.Text = "Voronoi";
            // 
            // rbtnDelaunay
            // 
            this.rbtnDelaunay.AutoSize = true;
            this.rbtnDelaunay.Location = new System.Drawing.Point(6, 42);
            this.rbtnDelaunay.Name = "rbtnDelaunay";
            this.rbtnDelaunay.Size = new System.Drawing.Size(134, 17);
            this.rbtnDelaunay.TabIndex = 1;
            this.rbtnDelaunay.TabStop = true;
            this.rbtnDelaunay.Text = "Delaunay Triangulation";
            this.rbtnDelaunay.UseVisualStyleBackColor = true;
            this.rbtnDelaunay.Click += new System.EventHandler(this.rbtnDelaunay_Click);
            // 
            // rbtnVoronoi
            // 
            this.rbtnVoronoi.AutoSize = true;
            this.rbtnVoronoi.Checked = true;
            this.rbtnVoronoi.Location = new System.Drawing.Point(6, 19);
            this.rbtnVoronoi.Name = "rbtnVoronoi";
            this.rbtnVoronoi.Size = new System.Drawing.Size(103, 17);
            this.rbtnVoronoi.TabIndex = 0;
            this.rbtnVoronoi.TabStop = true;
            this.rbtnVoronoi.Text = "Voronoi Diagram";
            this.rbtnVoronoi.UseVisualStyleBackColor = true;
            this.rbtnVoronoi.Click += new System.EventHandler(this.rbtnVoronoi_Click);
            // 
            // GIS_Legend
            // 
            this.GIS_Legend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions2;
            this.GIS_Legend.GIS_Group = null;
            this.GIS_Legend.GIS_Layer = null;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(424, 302);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)));
            this.GIS_Legend.ReverseOrder = true;
            this.GIS_Legend.Size = new System.Drawing.Size(160, 80);
            this.GIS_Legend.TabIndex = 4;
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Location = new System.Drawing.Point(0, 28);
            this.GIS.MinZoomSize = -5;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(424, 354);
            this.GIS.TabIndex = 5;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(430, 273);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(148, 23);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(584, 404);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GIS_Legend);
            this.Controls.Add(this.grpbxResult);
            this.Controls.Add(this.GIS_Attributes);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Triangulation";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.grpbxResult.ResumeLayout(false);
            this.grpbxResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imglst;
        private System.Windows.Forms.ToolStrip tlbr;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private TatukGIS.NDK.WinForms.TGIS_ControlAttributes GIS_Attributes;
        private System.Windows.Forms.GroupBox grpbxResult;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_Legend;
        private System.Windows.Forms.Label lblLayer;
        private System.Windows.Forms.TextBox edtLayer;
        private System.Windows.Forms.RadioButton rbtnDelaunay;
        private System.Windows.Forms.RadioButton rbtnVoronoi;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Button btnGenerate;
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;

            // open a file
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\city.shp");

            // and add a new parametr
            lv = (TGIS_LayerVector)(GIS.Items[0]);
            lv.Params.Marker.Color = TGIS_Color.FromARGB((uint)ColorTranslator.FromWin32(0x4080FF).ToArgb());
            lv.Params.Marker.OutlineWidth = 2;
            lv.Params.Marker.Style = TGIS_MarkerStyle.Circle;

            lv.ParamsList.Add();
            lv.Params.Style = "selected";
            lv.Params.Area.OutlineWidth = 1;
            lv.Params.Area.Color = TGIS_Color.Blue;

            GIS_Legend.Update();
        }

        private void tlbr_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent) GIS.FullExtent();
            else if(sender == btnZoomIn) GIS.Zoom = GIS.Zoom * 2;
            else if(sender == btnZoomOut) GIS.Zoom = GIS.Zoom / 2;
        }

        private void rbtnVoronoi_Click(object sender, EventArgs e)
        {
            edtLayer.Text = "Voronoi";
        }

        private void rbtnDelaunay_Click(object sender, EventArgs e)
        {
            edtLayer.Text = "Delaunay";
        }

        private void GIS_MouseDown(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_Shape shp;

            if (GIS.IsEmpty) return;
            if (GIS.IsEmpty) return;

            // let's locate a shape after click
            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            shp = (TGIS_Shape)(GIS.Locate(ptg, 5 / GIS.Zoom)); // 5 pixels precision
            if (shp != null)
                GIS_Attributes.ShowShape(shp);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;

            if (GIS.Get(edtLayer.Text) != null)
            {
                MessageBox.Show("Result layer already exists. Use different name.");
                return;
            }

            if (rbtnVoronoi.Checked)
                lv = new TGIS_LayerVoronoi();
            else
                lv = new TGIS_LayerDelaunay();

            lv.Name = edtLayer.Text;
            lv.ImportLayer((TGIS_LayerVector)(GIS.Items[0]), GIS.Extent,
                            TGIS_ShapeType.Unknown, "", false
                          );
            lv.Transparency = 60;

            lv.Params.Render.Expression = "GIS_AREA";
            lv.Params.Render.MinVal = 10000000;
            lv.Params.Render.MaxVal = 1300000000;
            lv.Params.Render.StartColor = TGIS_Color.White;
            if (rbtnVoronoi.Checked)
                lv.Params.Render.EndColor = TGIS_Color.Red;
            else
                lv.Params.Render.EndColor = TGIS_Color.Blue;

            lv.Params.Render.Zones = 10;
            lv.Params.Area.Color = TGIS_Color.RenderColor;
            lv.CS = GIS.CS;

            GIS.Add(lv);
            GIS.InvalidateWholeMap();
            GIS_Legend.Invalidate();
        }
    }
}
