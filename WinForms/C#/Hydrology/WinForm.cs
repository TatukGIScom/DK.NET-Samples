using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace AddLayer
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
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private Label label1;
        private Panel panel2;
        private Button btn3D;
        private Button btnVectorize;
        private Button btnStreamOrderStrahler;
        private Button btnBasin;
        private Button btnWatershed;
        private Button btnAddOutlets;
        private Button btnFlowAccumulation;
        private Button btnFlowDirection;
        private Button btnFillSinks;
        private Button btnSink;
        private TGIS_ControlLegend tgiS_ControlLegend1;
        private System.Windows.Forms.Panel panel1;

        private TGIS_LayerPixel dem;
        private TGIS_Extent ext;
        private TGIS_Hydrology hydrologyToolset;

        const string HYDRO_LAYER_SINK = "Sinks and flats";
        const string HYDRO_LAYER_DEM = "Hydrologically conditioned DEM";
        const string HYDRO_LAYER_DIRECTION = "Flow direction";
        const string HYDRO_LAYER_ACCUMULATION = "Flow accumulation";
        const string HYDRO_LAYER_STREAM_ORDER = "Stream order (Strahler)";
        const string HYDRO_LAYER_OUTLETS = "Outlets (pour points)";
        const string HYDRO_LAYER_WATERSHED = "Watersheds";
        const string HYDRO_LAYER_BASIN = "Basins";
        const string HYDRO_LAYER_STREAM_VEC = "Streams (vectorized)";
        const string HYDRO_LAYER_BASIN_VEC = "Basins (vectorized)";
        const string HYDRO_FIELD_ORDER = "ORDER";
        private Panel panel3;
        private ProgressBar progressBar1;
        const string HYDRO_FIELD_BASIN = "BASIN_ID";


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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn3D = new System.Windows.Forms.Button();
            this.btnVectorize = new System.Windows.Forms.Button();
            this.btnStreamOrderStrahler = new System.Windows.Forms.Button();
            this.btnBasin = new System.Windows.Forms.Button();
            this.btnWatershed = new System.Windows.Forms.Button();
            this.btnAddOutlets = new System.Windows.Forms.Button();
            this.btnFlowAccumulation = new System.Windows.Forms.Button();
            this.btnFlowDirection = new System.Windows.Forms.Button();
            this.btnFillSinks = new System.Windows.Forms.Button();
            this.btnSink = new System.Windows.Forms.Button();
            this.tgiS_ControlLegend1 = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.panel3 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 24);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(949, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "This sample application is a step-by-step tutorial on how to perform common hydro" +
    "logical analyzes.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Location = new System.Drawing.Point(197, 24);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(583, 469);
            this.GIS.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn3D);
            this.panel2.Controls.Add(this.btnVectorize);
            this.panel2.Controls.Add(this.btnStreamOrderStrahler);
            this.panel2.Controls.Add(this.btnBasin);
            this.panel2.Controls.Add(this.btnWatershed);
            this.panel2.Controls.Add(this.btnAddOutlets);
            this.panel2.Controls.Add(this.btnFlowAccumulation);
            this.panel2.Controls.Add(this.btnFlowDirection);
            this.panel2.Controls.Add(this.btnFillSinks);
            this.panel2.Controls.Add(this.btnSink);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(197, 491);
            this.panel2.TabIndex = 4;
            // 
            // btn3D
            // 
            this.btn3D.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn3D.Enabled = false;
            this.btn3D.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btn3D.Location = new System.Drawing.Point(0, 216);
            this.btn3D.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btn3D.Name = "btn3D";
            this.btn3D.Size = new System.Drawing.Size(197, 24);
            this.btn3D.TabIndex = 9;
            this.btn3D.Text = "View in 3D";
            this.btn3D.UseVisualStyleBackColor = true;
            this.btn3D.Click += new System.EventHandler(this.btn3D_Click);
            // 
            // btnVectorize
            // 
            this.btnVectorize.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVectorize.Enabled = false;
            this.btnVectorize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnVectorize.Location = new System.Drawing.Point(0, 192);
            this.btnVectorize.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnVectorize.Name = "btnVectorize";
            this.btnVectorize.Size = new System.Drawing.Size(197, 24);
            this.btnVectorize.TabIndex = 8;
            this.btnVectorize.Text = "Convert to vector";
            this.btnVectorize.UseVisualStyleBackColor = true;
            this.btnVectorize.Click += new System.EventHandler(this.btnVectorize_Click);
            // 
            // btnStreamOrderStrahler
            // 
            this.btnStreamOrderStrahler.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStreamOrderStrahler.Enabled = false;
            this.btnStreamOrderStrahler.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnStreamOrderStrahler.Location = new System.Drawing.Point(0, 168);
            this.btnStreamOrderStrahler.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnStreamOrderStrahler.Name = "btnStreamOrderStrahler";
            this.btnStreamOrderStrahler.Size = new System.Drawing.Size(197, 24);
            this.btnStreamOrderStrahler.TabIndex = 7;
            this.btnStreamOrderStrahler.Text = "Stream Order (Strahler)";
            this.btnStreamOrderStrahler.UseVisualStyleBackColor = true;
            this.btnStreamOrderStrahler.Click += new System.EventHandler(this.btnStreamOrderStrahler_Click);
            // 
            // btnBasin
            // 
            this.btnBasin.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBasin.Enabled = false;
            this.btnBasin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnBasin.Location = new System.Drawing.Point(0, 144);
            this.btnBasin.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnBasin.Name = "btnBasin";
            this.btnBasin.Size = new System.Drawing.Size(197, 24);
            this.btnBasin.TabIndex = 6;
            this.btnBasin.Text = "Basin";
            this.btnBasin.UseVisualStyleBackColor = true;
            this.btnBasin.Click += new System.EventHandler(this.btnBasin_Click);
            // 
            // btnWatershed
            // 
            this.btnWatershed.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnWatershed.Enabled = false;
            this.btnWatershed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnWatershed.Location = new System.Drawing.Point(0, 120);
            this.btnWatershed.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnWatershed.Name = "btnWatershed";
            this.btnWatershed.Size = new System.Drawing.Size(197, 24);
            this.btnWatershed.TabIndex = 5;
            this.btnWatershed.Text = "Watershed";
            this.btnWatershed.UseVisualStyleBackColor = true;
            this.btnWatershed.Click += new System.EventHandler(this.btnWatershed_Click);
            // 
            // btnAddOutlets
            // 
            this.btnAddOutlets.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddOutlets.Enabled = false;
            this.btnAddOutlets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnAddOutlets.Location = new System.Drawing.Point(0, 96);
            this.btnAddOutlets.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnAddOutlets.Name = "btnAddOutlets";
            this.btnAddOutlets.Size = new System.Drawing.Size(197, 24);
            this.btnAddOutlets.TabIndex = 4;
            this.btnAddOutlets.Text = "Add outlets for Watershed";
            this.btnAddOutlets.UseVisualStyleBackColor = true;
            this.btnAddOutlets.Click += new System.EventHandler(this.btnAddOutlets_Click);
            // 
            // btnFlowAccumulation
            // 
            this.btnFlowAccumulation.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFlowAccumulation.Enabled = false;
            this.btnFlowAccumulation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnFlowAccumulation.Location = new System.Drawing.Point(0, 72);
            this.btnFlowAccumulation.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnFlowAccumulation.Name = "btnFlowAccumulation";
            this.btnFlowAccumulation.Size = new System.Drawing.Size(197, 24);
            this.btnFlowAccumulation.TabIndex = 3;
            this.btnFlowAccumulation.Text = "Flow Accumulation";
            this.btnFlowAccumulation.UseVisualStyleBackColor = true;
            this.btnFlowAccumulation.Click += new System.EventHandler(this.btnFlowAccumulation_Click);
            // 
            // btnFlowDirection
            // 
            this.btnFlowDirection.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFlowDirection.Enabled = false;
            this.btnFlowDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnFlowDirection.Location = new System.Drawing.Point(0, 48);
            this.btnFlowDirection.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnFlowDirection.Name = "btnFlowDirection";
            this.btnFlowDirection.Size = new System.Drawing.Size(197, 24);
            this.btnFlowDirection.TabIndex = 2;
            this.btnFlowDirection.Text = "Flow Direction";
            this.btnFlowDirection.UseVisualStyleBackColor = true;
            this.btnFlowDirection.Click += new System.EventHandler(this.btnFlowDirection_Click);
            // 
            // btnFillSinks
            // 
            this.btnFillSinks.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFillSinks.Enabled = false;
            this.btnFillSinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnFillSinks.Location = new System.Drawing.Point(0, 24);
            this.btnFillSinks.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnFillSinks.Name = "btnFillSinks";
            this.btnFillSinks.Size = new System.Drawing.Size(197, 24);
            this.btnFillSinks.TabIndex = 1;
            this.btnFillSinks.Text = "Fill sinks";
            this.btnFillSinks.UseVisualStyleBackColor = true;
            this.btnFillSinks.Click += new System.EventHandler(this.btnFillSinks_Click);
            // 
            // btnSink
            // 
            this.btnSink.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSink.Location = new System.Drawing.Point(0, 0);
            this.btnSink.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.btnSink.Name = "btnSink";
            this.btnSink.Size = new System.Drawing.Size(197, 24);
            this.btnSink.TabIndex = 0;
            this.btnSink.Text = "Identify DEM problems";
            this.btnSink.UseVisualStyleBackColor = true;
            this.btnSink.Click += new System.EventHandler(this.button1_Click);
            // 
            // tgiS_ControlLegend1
            // 
            this.tgiS_ControlLegend1.CompactView = false;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.tgiS_ControlLegend1.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.tgiS_ControlLegend1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tgiS_ControlLegend1.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.tgiS_ControlLegend1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tgiS_ControlLegend1.GIS_Viewer = this.GIS;
            this.tgiS_ControlLegend1.Location = new System.Drawing.Point(780, 24);
            this.tgiS_ControlLegend1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.tgiS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.tgiS_ControlLegend1.Name = "tgiS_ControlLegend1";
            this.tgiS_ControlLegend1.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.tgiS_ControlLegend1.ReverseOrder = false;
            this.tgiS_ControlLegend1.Size = new System.Drawing.Size(169, 491);
            this.tgiS_ControlLegend1.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(197, 495);
            this.panel3.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(583, 20);
            this.panel3.TabIndex = 8;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(0, 0);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(583, 20);
            this.progressBar1.TabIndex = 11;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(949, 515);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tgiS_ControlLegend1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Hydrology";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
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

        private void doBusyEvent(Object _sender, TGIS_BusyEventArgs _e)
        {
            if (_e.Pos < 0)
                progressBar1.Value = 0;
            else
            if (_e.Pos == 0)
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Value = 0;
            }
            else
            {
                progressBar1.Value = (int)_e.Pos;
            }
        }

        // Creates a new grid layer with the same parameters as input DEM and a given name
        public TGIS_LayerPixel CreateLayerPix(TGIS_LayerPixel _dem, String _name)
        {
            TGIS_LayerPixel res = new TGIS_LayerPixel();
            res.Build(true, _dem.CS, _dem.Extent, _dem.BitWidth, _dem.BitHeight);
            res.Name = _name;
            res.Params.Pixel.Antialias = false;
            res.Params.Pixel.GridShadow = false;
            return res;
        }

        // Creates a new vector layer wita a given name, cs and type
        public TGIS_LayerVector CreateLayerVec(String _name, TGIS_CSCoordinateSystem _cs, TGIS_ShapeType _type)
        {
            TGIS_LayerVector res = new TGIS_LayerVector();
            res.Name = _name;
            res.Open();
            res.CS = _cs;
            res.DefaultShapeType = _type;
            return res;
        }

        // Gets a pixel layer with a given name from GIS
        public TGIS_LayerPixel GetLayerGrd(String _name)
        {
            return GIS.Get(_name) as TGIS_LayerPixel;
        }

        // Gets a vector layer with a given name from GIS
        public TGIS_LayerVector GetLayerVec(String _name)
        {
            return GIS.Get(_name) as TGIS_LayerVector;
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            GIS.Mode = TGIS_ViewerMode.Zoom;
            GIS.RestrictedDrag = false;
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\Poland\DEM\Bytowski_County.tif");

            dem = GIS.Items[0] as TGIS_LayerPixel;
            ext = dem.Extent;

            dem.Params.Pixel.Antialias = false;
            dem.Params.Pixel.GridShadow = false;
            GIS.InvalidateWholeMap();

            hydrologyToolset = new TGIS_Hydrology();
            hydrologyToolset.BusyEvent += doBusyEvent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnSink.Enabled = false;

            // creating a grid layer for sinks
            TGIS_LayerPixel sinks = CreateLayerPix(dem, HYDRO_LAYER_SINK);

            // the Sink algorithm requires only a grid layer with DEM
            hydrologyToolset.Sink(dem, ext, sinks);

            GIS.Add(sinks);

            // coloring pixels with sinks (pits) and flats
            String mn = sinks.MinHeight.ToString();
            String mx = sinks.MaxHeight.ToString();
            sinks.Params.Pixel.AltitudeMapZones.Add(
              String.Format("{0},{1},165:15:21:255,{2}-{3}", mn, mx, mn, mx)
            );
            GIS.InvalidateWholeMap();

            btnFillSinks.Enabled = true;
        }

        private void btnFillSinks_Click(object sender, EventArgs e)
        {
            btnFillSinks.Enabled = false;

            // turning off layers
            dem.Active = false;
            GetLayerGrd(HYDRO_LAYER_SINK).Active = false;

            // creating a grid layer for a hydrologically conditioned DEM
            TGIS_LayerPixel hydro_dem = CreateLayerPix(dem, HYDRO_LAYER_DEM);

            // the Fill algorithm requires a grid layer with DEM
            hydrologyToolset.Fill(dem, ext, hydro_dem);

            GIS.Add(hydro_dem);

            // applying the layer symbology
            TGIS_GradientMap color_ramp = TGIS_Utils.GisColorRampList.ByName("YellowGreen");
            TGIS_ColorMap[] color_map = color_ramp.RealizeColorMap(TGIS_ColorMapMode.Continuous, 0, true);
            hydro_dem.GenerateRampEx(hydro_dem.MinHeight, hydro_dem.MaxHeight, color_map, null);
            hydro_dem.Params.Pixel.GridShadow = true;
            hydro_dem.Params.Pixel.Antialias = true;
            hydro_dem.Params.Pixel.ShowLegend = false;

            GIS.InvalidateWholeMap();

            btnFlowDirection.Enabled = true;
        }

        private void btnFlowDirection_Click(object sender, EventArgs e)
        {
            btnFlowDirection.Enabled = false;

            TGIS_LayerPixel hydro_dem = GetLayerGrd(HYDRO_LAYER_DEM);
            hydro_dem.Active = false;

            // creating a grid layer for flow directions
            TGIS_LayerPixel flowdir = CreateLayerPix(dem, HYDRO_LAYER_DIRECTION);

            // the FlowDirection algorithm requires a hydrologically conditioned DEM
            hydrologyToolset.FlowDirection(hydro_dem, ext, flowdir);

            // applying a turbo color ramp for direction codes
            flowdir.Params.Pixel.AltitudeMapZones.Add("1,1,48:18:59:255,1");
            flowdir.Params.Pixel.AltitudeMapZones.Add("2,2,71:117:237:255,2");
            flowdir.Params.Pixel.AltitudeMapZones.Add("4,4,29:206:214:255,4");
            flowdir.Params.Pixel.AltitudeMapZones.Add("8,8,98:252:108:255,8");
            flowdir.Params.Pixel.AltitudeMapZones.Add("16,16,210:232:53:255,16");
            flowdir.Params.Pixel.AltitudeMapZones.Add("32,32,254:154:45:255,32");
            flowdir.Params.Pixel.AltitudeMapZones.Add("64,64,217:56:6:255,64");
            flowdir.Params.Pixel.AltitudeMapZones.Add("128,128,122:4:3:255,128");
            flowdir.Params.Pixel.ShowLegend = true;

            GIS.Add(flowdir);
            GIS.InvalidateWholeMap();

            btnFlowAccumulation.Enabled = true;
        }

        private void btnFlowAccumulation_Click(object sender, EventArgs e)
        {
            btnFlowAccumulation.Enabled = false;

            TGIS_LayerPixel flowdir = GetLayerGrd(HYDRO_LAYER_DIRECTION);
            flowdir.Active = false;

            // creating a grid layer for flow accumulation
            TGIS_LayerPixel flowacc = CreateLayerPix(dem, HYDRO_LAYER_ACCUMULATION);

            // the FlowAccumulation algorithm requires a flow accumulation grid
            hydrologyToolset.FlowAccumulation(flowdir, ext, flowacc);

            GIS.Add(flowacc);

            // performing a geometric classification for a better result visualization
            TGIS_ClassificationPixel classifier = new TGIS_ClassificationPixel(flowacc);
            try
            {
                classifier.Method = TGIS_ClassificationMethod.GeometricalInterval;
                classifier.Band = "1";
                classifier.NumClasses = 5;
                classifier.ColorRamp = TGIS_Utils.GisColorRampList.ByName("Bathymetry2")
                  .RealizeColorMap(TGIS_ColorMapMode.Continuous, 0, true);

                classifier.Classify();
                flowacc.Params.Pixel.ShowLegend = true;
            }
            finally
            {
                classifier = null;
            };

            GIS.InvalidateWholeMap();

            btnAddOutlets.Enabled = true;
        }


        private void btnAddOutlets_Click(object sender, EventArgs e)
        {
            btnAddOutlets.Enabled = false;

            // creating a grid layer for outlets (pour points)
            TGIS_LayerVector outlets = CreateLayerVec(HYDRO_LAYER_OUTLETS, dem.CS, TGIS_ShapeType.Point);

            // adding point symbology
            outlets.Params.Marker.Style = TGIS_MarkerStyle.TriangleUp;
            outlets.Params.Marker.SizeAsText = "SIZE:8pt";

            // adding two sample pour points
            // outlets should be located to cells of high accumulated flow
            TGIS_Shape shp = outlets.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Projection);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(375007.548333318, 696503.13358447));
            shp.Unlock();

            shp = outlets.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Projection);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(399612.055851588, 706196.55502031));
            shp.Unlock();

            GIS.Add(outlets);
            GIS.InvalidateWholeMap();

            btnWatershed.Enabled = true;
        }

        private void btnWatershed_Click(object sender, EventArgs e)
        {
            btnWatershed.Enabled = false;

            TGIS_LayerPixel flowdir = GetLayerGrd(HYDRO_LAYER_DIRECTION);
            TGIS_LayerVector outlets = GetLayerVec(HYDRO_LAYER_OUTLETS);

            // creating a grid layer for watershed
            TGIS_LayerPixel watershed = CreateLayerPix(dem, HYDRO_LAYER_WATERSHED);

            // applying a symbology
            watershed.Params.Pixel.AltitudeMapZones.Add("1,1,62:138:86:255,1");
            watershed.Params.Pixel.AltitudeMapZones.Add("2,2,108:3:174:255,2");
            watershed.Transparency = 50;

            watershed.Params.Pixel.ShowLegend = true;

            // the Watershed algorithm requires Flow Direction grid and outlets
            // (may be vector, or grid)
            hydrologyToolset.Watershed(flowdir, outlets, "GIS_UID", ext, watershed);

            GIS.Add(watershed);
            GIS.InvalidateWholeMap();

            btnBasin.Enabled = true;
        }

        private void btnBasin_Click(object sender, EventArgs e)
        {
            btnBasin.Enabled = false;

            TGIS_LayerPixel flowdir = GetLayerGrd(HYDRO_LAYER_DIRECTION);
            TGIS_LayerPixel flowacc = GetLayerGrd(HYDRO_LAYER_ACCUMULATION);
            flowacc.Active = false;
            GetLayerGrd(HYDRO_LAYER_DEM).Active = false;
            GetLayerGrd(HYDRO_LAYER_WATERSHED).Active = false;
            GetLayerVec(HYDRO_LAYER_OUTLETS).Active = false;

            // creating a grid layer for Basin
            TGIS_LayerPixel basins = CreateLayerPix(dem, HYDRO_LAYER_BASIN);

            // the Basin algorithm only requires a Flow Direction grid
            hydrologyToolset.Basin(flowdir, ext, basins, (int)Math.Round(flowacc.MaxHeight / 100));

            GIS.Add(basins);

            // classifying basin grid by unique values
            TGIS_ClassificationPixel classifier = new TGIS_ClassificationPixel(basins);
            try
            {
                classifier.Method = TGIS_ClassificationMethod.Unique;
                classifier.Band = "Value";
                classifier.ShowLegend = false;
                classifier.ColorRampName = "UniquePastel";
                
                classifier.Classify();
            }
            finally
            {
                classifier = null;
            };

            GIS.InvalidateWholeMap();

            btnStreamOrderStrahler.Enabled = true;
        }


        private void btnStreamOrderStrahler_Click(object sender, EventArgs e)
        {
            btnStreamOrderStrahler.Enabled = false;

            TGIS_LayerPixel flowdir = GetLayerGrd(HYDRO_LAYER_DIRECTION);
            TGIS_LayerPixel flowacc = GetLayerGrd(HYDRO_LAYER_ACCUMULATION);

            // creating a grid layer for stream order
            TGIS_LayerPixel stream_order = CreateLayerPix(dem, HYDRO_LAYER_STREAM_ORDER);

            // applying a symbology from the "Blues" color ramp
            stream_order.Params.Pixel.AltitudeMapZones.Add("1,1,78:179:211:255,1");
            stream_order.Params.Pixel.AltitudeMapZones.Add("2,2,43:140:190:255,2");
            stream_order.Params.Pixel.AltitudeMapZones.Add("3,3,8:104:172:255,3");
            stream_order.Params.Pixel.AltitudeMapZones.Add("4,4,8:64:129:255,4");
            stream_order.Params.Pixel.ShowLegend = true;

            // the StreamOrder algorithm requires Flow Direction and Accumulation grids
            hydrologyToolset.StreamOrder(flowdir, flowacc, ext, stream_order);

            GIS.Add(stream_order);
            GIS.InvalidateWholeMap();

            btnVectorize.Enabled = true;
        }

        private void btnVectorize_Click(object sender, EventArgs e)
        {
            btnVectorize.Enabled = false;

            TGIS_LayerPixel flowdir = GetLayerGrd(HYDRO_LAYER_DIRECTION);
            TGIS_LayerPixel streams = GetLayerGrd(HYDRO_LAYER_STREAM_ORDER);
            TGIS_LayerPixel basins = GetLayerGrd(HYDRO_LAYER_BASIN);

            streams.Active = false;
            basins.Active = false;

            // 1. Converting basins to polygon

            // creating a vector polygon layer for basins
            TGIS_LayerVector basins_vec = CreateLayerVec(HYDRO_LAYER_BASIN_VEC, dem.CS, TGIS_ShapeType.Polygon);
            basins_vec.AddField(HYDRO_FIELD_BASIN, TGIS_FieldType.Number, 10, 0);

            // using the GirdToPolygon vectorization tool
            TGIS_GridToPolygon vectorizator = new TGIS_GridToPolygon();
            try
            {
                vectorizator.BusyEvent += doBusyEvent;
                vectorizator.Generate(basins, basins_vec, HYDRO_FIELD_BASIN);
            }
            finally
            {
                vectorizator = null;
            };

            GIS.Add(basins_vec);

            // classifying a basins vector layer by unique value
            TGIS_ClassificationVector classifier = new TGIS_ClassificationVector(basins_vec);
            try
            {
                classifier.Method = TGIS_ClassificationMethod.Unique;
                classifier.Field = HYDRO_FIELD_BASIN;
                classifier.ShowLegend = false;
                classifier.ColorRampName = "Unique";
                
                classifier.Classify();
            }
            finally
            {
                classifier = null;
            };

            // 2. Converting streams to polylines

            // creating a vector layer for streams from Stream Order grid
            TGIS_LayerVector streams_vec = CreateLayerVec(HYDRO_LAYER_STREAM_VEC, dem.CS, TGIS_ShapeType.Arc);
            streams_vec.AddField(HYDRO_FIELD_ORDER, TGIS_FieldType.Number, 10, 0);

            // applying a symbology and width based on a stream order value, and labeling
            streams_vec.Params.Line.WidthAsText = "RENDERER";
            streams_vec.Params.Line.ColorAsText = "ARGB:FF045A8D";
            streams_vec.Params.Render.Expression = HYDRO_FIELD_ORDER;
            streams_vec.Params.Render.Zones = 4;
            streams_vec.Params.Render.MinVal = 1;
            streams_vec.Params.Render.MaxVal = 5;
            streams_vec.Params.Render.StartSizeAsText = "SIZE:1pt";
            streams_vec.Params.Render.EndSizeAsText = "SIZE:4pt";
            streams_vec.Params.Labels.Value = "{HYDRO_FIELD_ORDER}";
            streams_vec.Params.Labels.FontSizeAsText = "SIZE:7pt";
            streams_vec.Params.Labels.FontColorAsText = "ARGB:FF045A8D";
            streams_vec.Params.Labels.ColorAsText = "ARGB:FFBDC9E1";
            streams_vec.Params.Labels.Alignment = TGIS_LabelAlignment.Follow;

            hydrologyToolset.StreamToPolyline(flowdir, streams, ext, streams_vec, HYDRO_FIELD_ORDER);

            GIS.Add(streams_vec);
            GIS.InvalidateWholeMap();

            btn3D.Enabled = true;
        }

        private void btn3D_Click(object sender, EventArgs e)
        {
            if (GIS.View3D)
            {
                btn3D.Text = "View in 3D";
                GIS.View3D = false;
            }
            else
            {
                btn3D.Text = "View in 2D";

                TGIS_LayerVector basins = GetLayerVec(HYDRO_LAYER_BASIN_VEC);
                basins.Active = false;

                TGIS_LayerPixel hdem = GetLayerGrd(HYDRO_LAYER_DEM);
                hdem.Active = true;
                hdem.Params.ScaleZ = 1;
                hdem.Params.NormalizedZ = TGIS_3DNormalizationType.Range;

                TGIS_LayerVector streams = GetLayerVec(HYDRO_LAYER_STREAM_VEC);
                streams.Params.Labels.Visible = false;
                streams.Layer3D = TGIS_3DLayerType.Off;

                GIS.InvalidateWholeMap();

                GIS.View3D = true;
                GIS.Viewer3D.ShowLights = true;
                GIS.Viewer3D.ShadowsLevel = 40;
            }
        }
    }
}