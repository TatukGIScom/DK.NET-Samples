using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Enumerators
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
            this.imglst1 = new System.Windows.Forms.ImageList(this.components);
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.pnl1 = new System.Windows.Forms.Panel();
            this.tlbr1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.btnNeighbors = new System.Windows.Forms.ToolStripButton();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.pnl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imglst1
            // 
            this.imglst1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglst1.ImageStream")));
            this.imglst1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imglst1.Images.SetKeyName(0, "FullExtent.bmp");
            this.imglst1.Images.SetKeyName(1, "ZoomIn.bmp");
            this.imglst1.Images.SetKeyName(2, "ZoomOut.bmp");
            this.imglst1.Images.SetKeyName(3, "Drag.bmp");
            this.imglst1.Images.SetKeyName(4, "LayerProperties.bmp");
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Size = new System.Drawing.Size(494, 400);
            this.GIS.TabIndex = 1;
            // 
            // pnl1
            // 
            this.pnl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl1.Controls.Add(this.tlbr1);
            this.pnl1.Location = new System.Drawing.Point(0, 0);
            this.pnl1.Name = "pnl1";
            this.pnl1.Size = new System.Drawing.Size(494, 23);
            this.pnl1.TabIndex = 3;
            // 
            // tlbr1
            // 
            this.tlbr1.AutoSize = false;
            this.tlbr1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut,
            this.btnDrag,
            this.btnNeighbors});
            this.tlbr1.ImageList = this.imglst1;
            this.tlbr1.Location = new System.Drawing.Point(0, 0);
            this.tlbr1.Name = "tlbr1";
            this.tlbr1.ShowItemToolTips = true;
            this.tlbr1.Size = new System.Drawing.Size(494, 23);
            this.tlbr1.TabIndex = 0;
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += tlbr1_ButtonClick;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageIndex = 1;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.ToolTipText = "Zoom In";
            this.btnZoomIn.Click += tlbr1_ButtonClick;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "Zoom Out";
            this.btnZoomOut.Click += tlbr1_ButtonClick;
            // 
            // btnDrag
            // 
            this.btnDrag.ImageIndex = 3;
            this.btnDrag.Name = "btnDrag";
            //this.btnDrag.DisplayStyle = System.Windows.Forms.ToolStripButtonStyle.ToggleButton;
            this.btnDrag.ToolTipText = "Drag mode on/off";
            this.btnDrag.Click += tlbr1_ButtonClick;
            // 
            // btnNeighbors
            // 
            this.btnNeighbors.ImageIndex = 4;
            this.btnNeighbors.Name = "btnNeighbors";
            this.btnNeighbors.ToolTipText = "Count Neighbors";
            this.btnNeighbors.Click += tlbr1_ButtonClick;
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 432);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(494, 22);
            this.stripBar1.TabIndex = 4;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(494, 454);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.pnl1);
            this.Controls.Add(this.GIS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Use enumerators to find neighbors";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.pnl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void BtnFullExtent_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.ImageList imglst1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Panel pnl1;
        private System.Windows.Forms.ToolStrip tlbr1;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton btnDrag;
        private System.Windows.Forms.ToolStripButton btnNeighbors;
        private System.Windows.Forms.StatusStrip stripBar1;
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

        private void actNeighbors()
        {
            int cnt;
            int max_cnt;
            TGIS_LayerVector lv;
            TGIS_Shape tmpshp;
            max_cnt = 0;

            lv = (TGIS_LayerVector)GIS.Items[0];

            if (lv.FindField("COUNT") < 0)
                lv.AddField("COUNT", TGIS_FieldType.Number, 10, 0);

            GIS.HourglassPrepare();
            try
            {
                // mark all shapes that can be affected as editable
                // to keep the layer conststent after modyfying shapes
                // also compute numer of shape stah can be affected

                foreach (TGIS_Shape shp in lv.Loop())
                {
                    cnt = -1;
                    foreach (TGIS_Shape shpNbr in lv.Loop(shp.ProjectedExtent, "", shp, "****T", true))
                    {
                        cnt = cnt + 1;
                        GIS.HourglassShake();
                    }
                    tmpshp = shp.MakeEditable();
                    tmpshp.SetField("COUNT", cnt);
                    if (cnt > max_cnt)
                    {
                        max_cnt = cnt;
                    }
                }
            }
            finally
            {
                GIS.HourglassRelease();
                lv.Params.Labels.Field = "COUNT";
                lv.Params.Render.Expression = "COUNT";
                lv.Params.Render.MinVal = 1;
                lv.Params.Render.MaxVal = max_cnt;
                lv.Params.Render.StartColor = TGIS_Color.White;
                lv.Params.Render.EndColor = TGIS_Color.Red;
                lv.Params.Render.Zones = 5;
                lv.Params.Area.Color = TGIS_Color.RenderColor;
                lv.Viewer.Ref.InvalidateWholeMap();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // add states layer
            GIS.Add(TGIS_Utils.GisCreateLayer("world",
                       TGIS_Utils.GisSamplesDataDirDownload() +
                         @"\World\Countries\USA\States\California\tl_2008_06_county.shp")
                   );
            GIS.FullExtent();
        }

        private void tlbr1_ButtonClick(object sender, System.EventArgs e)
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
            else if (sender == btnDrag)
            {
                if (GIS.Mode == TGIS_ViewerMode.Drag) GIS.Mode = TGIS_ViewerMode.Select;
                else GIS.Mode = TGIS_ViewerMode.Drag;
            }
            else if (sender == btnNeighbors)
            {
                actNeighbors();
            }
        }
    }
}
