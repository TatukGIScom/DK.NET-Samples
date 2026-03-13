using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace GridToVector
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Panel panel1;
        private TGIS_ViewerWnd GIS;
        private GroupBox gbLoadData;
        private Button btnLoadDEM;
        private Button btnLoadLand;
        private GroupBox gbGridToPolygon;
        private CheckBox chkSplit;
        private TextBox tbTolerance;
        private Label lblTolerance;
        private Button btnGridToPolygon;
        private TGIS_ControlAttributes GIS_Attr;
        private ProgressBar pProgressBar;
        private GroupBox gbGridToPoint;
        private GroupBox gbCommon;
        private CheckBox chkIgnoreNoData;
        private Label lblPointSpacing;
        private TextBox tbPointSpacing;
        private Button btnGridToPoint;
        const String LV_FIELD = "vector";
        const String LV_NAME = "polygons";

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            this.ActiveControl = GIS;
            GIS.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseWheel);
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
            this.gbGridToPoint = new System.Windows.Forms.GroupBox();
            this.btnGridToPoint = new System.Windows.Forms.Button();
            this.tbPointSpacing = new System.Windows.Forms.TextBox();
            this.lblPointSpacing = new System.Windows.Forms.Label();
            this.gbCommon = new System.Windows.Forms.GroupBox();
            this.chkIgnoreNoData = new System.Windows.Forms.CheckBox();
            this.lblTolerance = new System.Windows.Forms.Label();
            this.tbTolerance = new System.Windows.Forms.TextBox();
            this.GIS_Attr = new TatukGIS.NDK.WinForms.TGIS_ControlAttributes();
            this.gbGridToPolygon = new System.Windows.Forms.GroupBox();
            this.btnGridToPolygon = new System.Windows.Forms.Button();
            this.chkSplit = new System.Windows.Forms.CheckBox();
            this.gbLoadData = new System.Windows.Forms.GroupBox();
            this.btnLoadDEM = new System.Windows.Forms.Button();
            this.btnLoadLand = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.pProgressBar = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.gbGridToPoint.SuspendLayout();
            this.gbCommon.SuspendLayout();
            this.gbGridToPolygon.SuspendLayout();
            this.gbLoadData.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbGridToPoint);
            this.panel1.Controls.Add(this.gbCommon);
            this.panel1.Controls.Add(this.GIS_Attr);
            this.panel1.Controls.Add(this.gbGridToPolygon);
            this.panel1.Controls.Add(this.gbLoadData);
            this.panel1.Location = new System.Drawing.Point(2, -5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 662);
            this.panel1.TabIndex = 0;
            // 
            // gbGridToPoint
            // 
            this.gbGridToPoint.Controls.Add(this.btnGridToPoint);
            this.gbGridToPoint.Controls.Add(this.tbPointSpacing);
            this.gbGridToPoint.Controls.Add(this.lblPointSpacing);
            this.gbGridToPoint.Location = new System.Drawing.Point(11, 255);
            this.gbGridToPoint.Name = "gbGridToPoint";
            this.gbGridToPoint.Size = new System.Drawing.Size(200, 72);
            this.gbGridToPoint.TabIndex = 4;
            this.gbGridToPoint.TabStop = false;
            this.gbGridToPoint.Text = "Grid to point";
            // 
            // btnGridToPoint
            // 
            this.btnGridToPoint.Location = new System.Drawing.Point(5, 41);
            this.btnGridToPoint.Name = "btnGridToPoint";
            this.btnGridToPoint.Size = new System.Drawing.Size(188, 23);
            this.btnGridToPoint.TabIndex = 2;
            this.btnGridToPoint.Text = "Generate";
            this.btnGridToPoint.UseVisualStyleBackColor = true;
            this.btnGridToPoint.Click += new System.EventHandler(this.btnGridToPoint_Click);
            // 
            // tbPointSpacing
            // 
            this.tbPointSpacing.Location = new System.Drawing.Point(86, 13);
            this.tbPointSpacing.Name = "tbPointSpacing";
            this.tbPointSpacing.Size = new System.Drawing.Size(107, 20);
            this.tbPointSpacing.TabIndex = 1;
            // 
            // lblPointSpacing
            // 
            this.lblPointSpacing.AutoSize = true;
            this.lblPointSpacing.Location = new System.Drawing.Point(6, 16);
            this.lblPointSpacing.Name = "lblPointSpacing";
            this.lblPointSpacing.Size = new System.Drawing.Size(74, 13);
            this.lblPointSpacing.TabIndex = 0;
            this.lblPointSpacing.Text = "Point spacing:";
            // 
            // gbCommon
            // 
            this.gbCommon.Controls.Add(this.chkIgnoreNoData);
            this.gbCommon.Controls.Add(this.lblTolerance);
            this.gbCommon.Controls.Add(this.tbTolerance);
            this.gbCommon.Location = new System.Drawing.Point(10, 105);
            this.gbCommon.Name = "gbCommon";
            this.gbCommon.Size = new System.Drawing.Size(200, 62);
            this.gbCommon.TabIndex = 3;
            this.gbCommon.TabStop = false;
            this.gbCommon.Text = "Common parameters";
            // 
            // chkIgnoreNoData
            // 
            this.chkIgnoreNoData.AutoSize = true;
            this.chkIgnoreNoData.Location = new System.Drawing.Point(9, 35);
            this.chkIgnoreNoData.Name = "chkIgnoreNoData";
            this.chkIgnoreNoData.Size = new System.Drawing.Size(96, 17);
            this.chkIgnoreNoData.TabIndex = 2;
            this.chkIgnoreNoData.Text = "Ignore NoData";
            this.chkIgnoreNoData.UseVisualStyleBackColor = true;
            // 
            // lblTolerance
            // 
            this.lblTolerance.AutoSize = true;
            this.lblTolerance.Location = new System.Drawing.Point(6, 16);
            this.lblTolerance.Name = "lblTolerance";
            this.lblTolerance.Size = new System.Drawing.Size(58, 13);
            this.lblTolerance.TabIndex = 0;
            this.lblTolerance.Text = "Tolerance:";
            // 
            // tbTolerance
            // 
            this.tbTolerance.Location = new System.Drawing.Point(70, 13);
            this.tbTolerance.Name = "tbTolerance";
            this.tbTolerance.Size = new System.Drawing.Size(124, 20);
            this.tbTolerance.TabIndex = 1;
            // 
            // GIS_Attr
            // 
            this.GIS_Attr.Location = new System.Drawing.Point(11, 333);
            this.GIS_Attr.Name = "GIS_Attr";
            this.GIS_Attr.ReadOnly = true;
            this.GIS_Attr.Size = new System.Drawing.Size(199, 318);
            this.GIS_Attr.TabIndex = 2;
            // 
            // gbGridToPolygon
            // 
            this.gbGridToPolygon.Controls.Add(this.btnGridToPolygon);
            this.gbGridToPolygon.Controls.Add(this.chkSplit);
            this.gbGridToPolygon.Location = new System.Drawing.Point(11, 173);
            this.gbGridToPolygon.Name = "gbGridToPolygon";
            this.gbGridToPolygon.Size = new System.Drawing.Size(200, 76);
            this.gbGridToPolygon.TabIndex = 1;
            this.gbGridToPolygon.TabStop = false;
            this.gbGridToPolygon.Text = "Grid to polygon";
            // 
            // btnGridToPolygon
            // 
            this.btnGridToPolygon.Location = new System.Drawing.Point(6, 42);
            this.btnGridToPolygon.Name = "btnGridToPolygon";
            this.btnGridToPolygon.Size = new System.Drawing.Size(188, 23);
            this.btnGridToPolygon.TabIndex = 3;
            this.btnGridToPolygon.Text = "Generate";
            this.btnGridToPolygon.UseVisualStyleBackColor = true;
            this.btnGridToPolygon.Click += new System.EventHandler(this.btnGridToPolygon_Click);
            // 
            // chkSplit
            // 
            this.chkSplit.AutoSize = true;
            this.chkSplit.Checked = true;
            this.chkSplit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSplit.Location = new System.Drawing.Point(8, 19);
            this.chkSplit.Name = "chkSplit";
            this.chkSplit.Size = new System.Drawing.Size(83, 17);
            this.chkSplit.TabIndex = 2;
            this.chkSplit.Text = "Split shapes";
            this.chkSplit.UseVisualStyleBackColor = true;
            // 
            // gbLoadData
            // 
            this.gbLoadData.Controls.Add(this.btnLoadDEM);
            this.gbLoadData.Controls.Add(this.btnLoadLand);
            this.gbLoadData.Location = new System.Drawing.Point(10, 17);
            this.gbLoadData.Name = "gbLoadData";
            this.gbLoadData.Size = new System.Drawing.Size(200, 82);
            this.gbLoadData.TabIndex = 0;
            this.gbLoadData.TabStop = false;
            this.gbLoadData.Text = "Load data";
            // 
            // btnLoadDEM
            // 
            this.btnLoadDEM.Location = new System.Drawing.Point(7, 49);
            this.btnLoadDEM.Name = "btnLoadDEM";
            this.btnLoadDEM.Size = new System.Drawing.Size(187, 23);
            this.btnLoadDEM.TabIndex = 1;
            this.btnLoadDEM.Text = "DEM";
            this.btnLoadDEM.UseVisualStyleBackColor = true;
            this.btnLoadDEM.Click += new System.EventHandler(this.btnLoadDEM_Click);
            // 
            // btnLoadLand
            // 
            this.btnLoadLand.Location = new System.Drawing.Point(6, 19);
            this.btnLoadLand.Name = "btnLoadLand";
            this.btnLoadLand.Size = new System.Drawing.Size(188, 23);
            this.btnLoadLand.TabIndex = 0;
            this.btnLoadLand.Text = "Land Cover";
            this.btnLoadLand.UseVisualStyleBackColor = true;
            this.btnLoadLand.Click += new System.EventHandler(this.btnLoadLand_Click);
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Location = new System.Drawing.Point(227, -5);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(545, 633);
            this.GIS.TabIndex = 1;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            // 
            // pProgressBar
            // 
            this.pProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pProgressBar.Location = new System.Drawing.Point(227, 634);
            this.pProgressBar.Name = "pProgressBar";
            this.pProgressBar.Size = new System.Drawing.Size(545, 23);
            this.pProgressBar.TabIndex = 2;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(774, 658);
            this.Controls.Add(this.pProgressBar);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - GridToVector";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.gbGridToPoint.ResumeLayout(false);
            this.gbGridToPoint.PerformLayout();
            this.gbCommon.ResumeLayout(false);
            this.gbCommon.PerformLayout();
            this.gbGridToPolygon.ResumeLayout(false);
            this.gbGridToPolygon.PerformLayout();
            this.gbLoadData.ResumeLayout(false);
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
            btnLoadLand.PerformClick();
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void btnLoadLand_Click(object sender, EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\Luxembourg\CLC2018_CLC2018_V2018_20_Luxembourg.tif");
            tbTolerance.Text = "1";
            tbPointSpacing.Text = "1000";
        }

        private void btnLoadDEM_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\3D\elevation.grd");
            lp = GIS.Get("elevation") as TGIS_LayerPixel;
            lp.GenerateRamp(
              TGIS_Color.Blue,
              TGIS_Color.Lime,
              TGIS_Color.Red,
              lp.MinHeight,
              (lp.MinHeight + lp.MaxHeight) / 2,
              lp.MaxHeight,
              true,
              (lp.MaxHeight - lp.MinHeight) / 100,
              (lp.MaxHeight - lp.MinHeight) / 10,
              null,
              true,
              TGIS_ColorInterpolationMode.HSL
            );
            GIS.InvalidateWholeMap();
            tbTolerance.Text = "10";
            tbPointSpacing.Text = "200";
        }


        private void doBusyEvent(Object sender, TGIS_BusyEventArgs e)
        {
            switch (e.Pos)
            {
                case 0:
                    pProgressBar.Minimum = 0;
                    pProgressBar.Maximum = (int)e.EndPos;
                    pProgressBar.Value = (int)e.Pos;
                    break;
                case -1:
                    pProgressBar.Value = 0;
                    break;
                default:
                    pProgressBar.Value = (int)e.Pos;
                    break;

            }

        }

        private void GIS_MouseDown(object sender, MouseEventArgs e)
        {
            TGIS_Shape shp;

            if (GIS.IsEmpty)
            {
                return;
            }

            shp = GIS.Locate(GIS.ScreenToMap(new Point(e.X, e.Y)), 5 / GIS.Zoom) as TGIS_Shape;

            if (shp == null)
            {
                return;
            }

            shp.Layer.DeselectAll();
            shp.IsSelected = !shp.IsSelected;

            GIS_Attr.ShowShape(shp);
        }

        private void GIS_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GIS.IsEmpty) return;

            if (e.Delta < 0)
                GIS.ZoomBy(2 / 1.0, e.X, e.Y);
            else
                GIS.ZoomBy(1 / 2.0, e.X, e.Y);
        }

        private void btnGridToPolygon_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            TGIS_LayerVector lv;
            TGIS_GridToPolygon polygonizer;

            lp = GIS.Items[0] as TGIS_LayerPixel;

            if (GIS.Get(LV_NAME) != null)
            {
                GIS.Delete(LV_NAME);
            }

            lv = new TGIS_LayerVector();
            lv.Name = LV_NAME;
            lv.Open();
            lv.CS = lp.CS;
            lv.DefaultShapeType = TGIS_ShapeType.Polygon;
            lv.AddField(LV_FIELD, TGIS_FieldType.Float, 0, 0);
            lv.Transparency = 50;
            lv.Params.Area.OutlineColor = TGIS_Color.Black;

            polygonizer = new TGIS_GridToPolygon();
            polygonizer.Tolerance = float.Parse(tbTolerance.Text);
            polygonizer.SplitShapes = chkSplit.Checked;
            polygonizer.BusyEvent += doBusyEvent;
            polygonizer.Generate(lp, lv, LV_FIELD);

            GIS.Add(lv);
            GIS.InvalidateWholeMap();
        }

        private void btnGridToPoint_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp = GIS.Items[0] as TGIS_LayerPixel;

            if (GIS.Get(LV_NAME) != null)
                GIS.Delete(LV_NAME);

            TGIS_LayerVector lv = new TGIS_LayerVector();
            lv.Name = LV_NAME;
            lv.Open();
            lv.CS = lp.CS;
            lv.DefaultShapeType = TGIS_ShapeType.Point;
            lv.AddField(LV_FIELD, TGIS_FieldType.Float, 0, 0);
            lv.Params.Marker.Color = TGIS_Color.Black;
            lv.Params.Marker.Style = TGIS_MarkerStyle.Circle;
            lv.Params.Marker.SizeAsText = "SIZE:4pt";
            lv.Transparency = 75;

            TGIS_GridToPoint grid_to_point = new TGIS_GridToPoint();

            // common parameters
            grid_to_point.BusyEvent += doBusyEvent;
            grid_to_point.IgnoreNoData = chkIgnoreNoData.Checked;
            grid_to_point.Tolerance = float.Parse(tbTolerance.Text);
            grid_to_point.PointSpacing = float.Parse(tbPointSpacing.Text);

            grid_to_point.Generate(lp, lv, LV_FIELD);


            GIS.Add(lv);
            GIS.InvalidateWholeMap();
        }
    }
}
