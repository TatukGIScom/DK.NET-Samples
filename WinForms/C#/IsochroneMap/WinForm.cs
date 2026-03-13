using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.RTL;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace IsochroneMap
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSmallRoads;
        private System.Windows.Forms.TrackBar trkSmallRoads;
        private System.Windows.Forms.Label lblHighways;
        private System.Windows.Forms.TrackBar trkHighways;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.TextBox edtDistance;
        private System.Windows.Forms.Label lblZones;
        private System.Windows.Forms.TextBox edtZones;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.TGIS_LayerVector layerSrc;
        private TatukGIS.NDK.TGIS_LayerVector layerMarker;
        private TatukGIS.NDK.TGIS_Shape markerShp;
        private double costFactor;
        private int numZones;
        private TatukGIS.NDK.TGIS_LayerVector layerRoute;
        private TatukGIS.NDK.TGIS_Geocoding geoObj;
        private TatukGIS.NDK.TGIS_IsochroneMap rtrObj;
        private TatukGIS.NDK.TGIS_ShortestPath srtpObj;
        private TGIS_ControlLegend GIS_ControlLegend;
        private Label label1;
        private Button btnFullExtent;
        private ImageList imageList1;
        private Button btnZoomIn;
        private Button btnZoomOut;
        private TatukGIS.NDK.WinForms.TGIS_ControlScale GIS_ControlScale;

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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions2 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            TatukGIS.NDK.TGIS_CSUnits tgiS_CSUnits2 = new TatukGIS.NDK.TGIS_CSUnits();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.GIS_ControlLegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_ControlScale = new TatukGIS.NDK.WinForms.TGIS_ControlScale();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.edtZones = new System.Windows.Forms.TextBox();
            this.lblZones = new System.Windows.Forms.Label();
            this.edtDistance = new System.Windows.Forms.TextBox();
            this.lblDistance = new System.Windows.Forms.Label();
            this.trkHighways = new System.Windows.Forms.TrackBar();
            this.lblHighways = new System.Windows.Forms.Label();
            this.trkSmallRoads = new System.Windows.Forms.TrackBar();
            this.lblSmallRoads = new System.Windows.Forms.Label();
            this.btnFullExtent = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.GIS.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkHighways)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSmallRoads)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = "";
            this.panel1.Controls.Add(this.GIS_ControlLegend);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(404, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 504);
            this.panel1.TabIndex = 0;
            // 
            // GIS_ControlLegend
            // 
            this.GIS_ControlLegend.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_ControlLegend.DialogOptions = tgiS_ControlLegendDialogOptions2;
            this.GIS_ControlLegend.GIS_Group = null;
            this.GIS_ControlLegend.GIS_Layer = null;
            this.GIS_ControlLegend.GIS_Viewer = this.GIS;
            this.GIS_ControlLegend.Location = new System.Drawing.Point(0, 255);
            this.GIS_ControlLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_ControlLegend.Name = "GIS_ControlLegend";
            this.GIS_ControlLegend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GIS_ControlLegend.ReverseOrder = false;
            this.GIS_ControlLegend.Size = new System.Drawing.Size(188, 246);
            this.GIS_ControlLegend.TabIndex = 1;
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Controls.Add(this.GIS_ControlScale);
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Location = new System.Drawing.Point(-2, 24);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Size = new System.Drawing.Size(400, 482);
            this.GIS.TabIndex = 1;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            // 
            // GIS_ControlScale
            // 
            this.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Control;
            this.GIS_ControlScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GIS_ControlScale.DividerColor1 = System.Drawing.Color.Black;
            this.GIS_ControlScale.DividerColor2 = System.Drawing.Color.White;
            this.GIS_ControlScale.GIS_Viewer = this.GIS;
            this.GIS_ControlScale.Location = new System.Drawing.Point(3, 3);
            this.GIS_ControlScale.Name = "GIS_ControlScale";
            this.GIS_ControlScale.PrepareEvent = null;
            this.GIS_ControlScale.Size = new System.Drawing.Size(145, 25);
            this.GIS_ControlScale.TabIndex = 1;
            tgiS_CSUnits2.DescriptionEx = null;
            this.GIS_ControlScale.Units = tgiS_CSUnits2;
            this.GIS_ControlScale.UnitsEPSG = 904201;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.edtZones);
            this.groupBox1.Controls.Add(this.lblZones);
            this.groupBox1.Controls.Add(this.edtDistance);
            this.groupBox1.Controls.Add(this.lblDistance);
            this.groupBox1.Controls.Add(this.trkHighways);
            this.groupBox1.Controls.Add(this.lblHighways);
            this.groupBox1.Controls.Add(this.trkSmallRoads);
            this.groupBox1.Controls.Add(this.lblSmallRoads);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 249);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Routing parameters";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(21, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 34);
            this.label1.TabIndex = 8;
            this.label1.Text = "Click on the map to set start point and generate isochrone";
            // 
            // edtZones
            // 
            this.edtZones.Location = new System.Drawing.Point(24, 178);
            this.edtZones.Name = "edtZones";
            this.edtZones.Size = new System.Drawing.Size(145, 20);
            this.edtZones.TabIndex = 7;
            this.edtZones.Text = "3";
            // 
            // lblZones
            // 
            this.lblZones.Location = new System.Drawing.Point(24, 162);
            this.lblZones.Name = "lblZones";
            this.lblZones.Size = new System.Drawing.Size(40, 13);
            this.lblZones.TabIndex = 6;
            this.lblZones.Text = "&Zones";
            // 
            // edtDistance
            // 
            this.edtDistance.Location = new System.Drawing.Point(24, 136);
            this.edtDistance.Name = "edtDistance";
            this.edtDistance.Size = new System.Drawing.Size(145, 20);
            this.edtDistance.TabIndex = 5;
            this.edtDistance.Text = "4000";
            // 
            // lblDistance
            // 
            this.lblDistance.Location = new System.Drawing.Point(24, 120);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(59, 13);
            this.lblDistance.TabIndex = 4;
            this.lblDistance.Text = "&Distance";
            // 
            // trkHighways
            // 
            this.trkHighways.AutoSize = false;
            this.trkHighways.LargeChange = 1;
            this.trkHighways.Location = new System.Drawing.Point(16, 88);
            this.trkHighways.Minimum = 1;
            this.trkHighways.Name = "trkHighways";
            this.trkHighways.Size = new System.Drawing.Size(161, 25);
            this.trkHighways.TabIndex = 3;
            this.trkHighways.Value = 5;
            // 
            // lblHighways
            // 
            this.lblHighways.Location = new System.Drawing.Point(24, 72);
            this.lblHighways.Name = "lblHighways";
            this.lblHighways.Size = new System.Drawing.Size(90, 13);
            this.lblHighways.TabIndex = 2;
            this.lblHighways.Text = "Prefer &highway";
            // 
            // trkSmallRoads
            // 
            this.trkSmallRoads.AutoSize = false;
            this.trkSmallRoads.LargeChange = 1;
            this.trkSmallRoads.Location = new System.Drawing.Point(16, 40);
            this.trkSmallRoads.Minimum = 1;
            this.trkSmallRoads.Name = "trkSmallRoads";
            this.trkSmallRoads.Size = new System.Drawing.Size(161, 25);
            this.trkSmallRoads.TabIndex = 1;
            this.trkSmallRoads.Value = 1;
            // 
            // lblSmallRoads
            // 
            this.lblSmallRoads.Location = new System.Drawing.Point(24, 24);
            this.lblSmallRoads.Name = "lblSmallRoads";
            this.lblSmallRoads.Size = new System.Drawing.Size(100, 13);
            this.lblSmallRoads.TabIndex = 0;
            this.lblSmallRoads.Text = "Prefer &local roads";
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.ImageList = this.imageList1;
            this.btnFullExtent.Location = new System.Drawing.Point(1, 0);
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(35, 23);
            this.btnFullExtent.TabIndex = 2;
            this.btnFullExtent.UseVisualStyleBackColor = true;
            this.btnFullExtent.Click += new System.EventHandler(this.btnFullExtent_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "FullExtent.bmp");
            this.imageList1.Images.SetKeyName(1, "ZoomIn.bmp");
            this.imageList1.Images.SetKeyName(2, "ZoomOut.bmp");
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageIndex = 1;
            this.btnZoomIn.ImageList = this.imageList1;
            this.btnZoomIn.Location = new System.Drawing.Point(35, 0);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(35, 23);
            this.btnZoomIn.TabIndex = 3;
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.ImageList = this.imageList1;
            this.btnZoomOut.Location = new System.Drawing.Point(69, 0);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(35, 23);
            this.btnZoomOut.TabIndex = 4;
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 504);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.btnFullExtent);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Isochrone Map";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Leave += new System.EventHandler(this.WinForm_Leave);
            this.panel1.ResumeLayout(false);
            this.GIS.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkHighways)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSmallRoads)).EndInit();
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
            GIS.Lock();
            try
            {
                GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\USA\States\California\San Bernardino\TIGER\tl_2008_06071_edges_trunc.SHP");
                layerSrc = (TGIS_LayerVector)(GIS.Get("tl_2008_06071_edges_trunc"));

                if (layerSrc == null) return;
                if (!(layerSrc is TGIS_LayerVector)) return;

                // update the layer parameters to show roads types
                layerSrc.ParamsList.Add();
                layerSrc.Params.Line.Width = -2;
                layerSrc.Params.Query = "MTFCC<'S1400'";
                layerSrc.ParamsList.Add();
                layerSrc.Params.Line.Width = 1;
                layerSrc.Params.Line.Style = TGIS_PenStyle.Dash;
                layerSrc.Params.Query = "MTFCC='S1400'";

                GIS.VisibleExtent = layerSrc.Extent;
                GIS_ControlScale.Units = new TGIS_CSUnitsList().ByEPSG(9035); // mile

                // initial traversing cost
                costFactor = 5000.0;
                numZones = 5;

                // create route layer for result isochrone map
                layerRoute = new TGIS_LayerVector();
                layerRoute.UseConfig = false;
                layerRoute.Name = "Isochrone map for route";
                layerRoute.CS = GIS.CS;
                layerRoute.Params.Render.Expression = "GIS_COST";
                layerRoute.Params.Render.MinVal = 0;
                layerRoute.Params.Render.MaxVal = costFactor;
                layerRoute.Params.Render.Zones = numZones;
                layerRoute.Params.Area.Color = TGIS_Color.RenderColor;
                layerRoute.Params.Area.ShowLegend = true;
                layerRoute.Transparency = 50;
                GIS.Add(layerRoute);

                // create marker layer to show position
                layerMarker = new TGIS_LayerVector();
                layerMarker.UseConfig = false;
                layerMarker.Name = "Current Position";
                layerMarker.CS = GIS.CS;
                layerMarker.Params.Marker.Color = TGIS_Color.Red;
                GIS.Add(layerMarker);

                markerShp = null;

                // initialize isochrone map generator
                rtrObj = new TGIS_IsochroneMap(GIS);

                // initialize shortest path and attach events
                srtpObj = new TGIS_ShortestPath(GIS);
                srtpObj.LinkCostEvent += doLinkCost;
                srtpObj.LinkTypeEvent += doLinkType;
                srtpObj.LinkDynamicEvent += doLinkDynamic;

            }
            finally
            {
                GIS.Unlock();
            }
        }

        private void doLinkType(
            Object _sender,
            TGIS_LinkTypeEventArgs _e
         )
        {
            if (!(_e.Shape.GetField("MTFCC").ToString().CompareTo("S1400") < 0))
                _e.LinkType = 1;
            else
                _e.LinkType = 0;
        }

        private void doLinkCost(
            Object _sender,
            TGIS_LinkCostEventArgs _e
         )
        {
            if (_e.Shape.Layer.CS is TGIS_CSUnknownCoordinateSystem)
                _e.Cost = _e.Shape.Length();
            else
                _e.Cost = _e.Shape.LengthCS();

            _e.RevCost = _e.Cost;
        }

        private void doLinkDynamic(
            Object _sender,
            TGIS_LinkDynamicEventArgs _e
         )
        {
            TGIS_Shape shp;

            if (trkHighways.Value == 1)
            {
                shp = layerSrc.GetShape(_e.Uid);
                if (shp.GetField("MTFCC").ToString().CompareTo("S1400") < 0)
                {
                    _e.Cost = -1;
                    _e.RevCost = -1;
                }
            }
        }

        private void WinForm_Leave(object sender, System.EventArgs e)
        {
            layerRoute.Dispose();
            geoObj.Dispose();
            /*rtrObj.Dispose();*/
        }

        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;
            GIS.FullExtent();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;
            GIS.Zoom = GIS.Zoom * 2;
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;
            GIS.Zoom = GIS.Zoom / 2;
        }

        private void GIS_MouseDown(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;

            if (GIS.IsEmpty) return;
            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));

            if (markerShp == null)
            {
                markerShp = layerMarker.CreateShape(TGIS_ShapeType.Point);
                markerShp.Lock(TGIS_Lock.Extent);
                markerShp.AddPart();
                markerShp.AddPoint(ptg);
                markerShp.Unlock();
                markerShp.Invalidate();
            }
            else
            {
                markerShp.SetPosition(ptg, null, 0);
            }

            generateIsochrone();
        }

        private void generateIsochrone()
        {
            int i;

            if (markerShp == null)
            {
                MessageBox.Show("Please select a start point on the map");
                return;
            }

            layerRoute.RevertShapes();

            // maximum traversing cost for the isochrone map
            numZones = Convert.ToInt32(edtZones.Text);
            costFactor = Convert.ToInt32(edtDistance.Text);

            // update the renderer range
            layerRoute.Params.Render.MaxVal = costFactor;
            layerRoute.Params.Render.Zones = numZones;

            // calculate wages
            srtpObj.set_CostModifiers(0, 1.0 - 1.0 / 11.0 * trkHighways.Value);
            srtpObj.set_CostModifiers(1, 1.0 - 1.0 / 11.0 * trkSmallRoads.Value);

            // generate the isochrone maps
            for (i = 1; i <= numZones; i++)
                rtrObj.Generate(layerSrc, srtpObj, layerRoute, TGIS_ShapeType.Polygon,
                                 markerShp.Centroid(), costFactor / i, 0
                                );

            // smooth the result polygons shapes
            foreach (TGIS_Shape shp in layerRoute.Loop())
                shp.Smooth(10, false);

            layerRoute.RecalcExtent();
            GIS.Lock();
            GIS.VisibleExtent = layerRoute.ProjectedExtent;
            GIS.Zoom = 0.7 * GIS.Zoom;
            GIS.Unlock();
        }
    }
}
