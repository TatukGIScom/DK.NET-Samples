using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace Interpolation
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private TGIS_ViewerWnd GIS;
        private Button btnGenerate;
        private Label lblMethod;
        private RadioButton rbIDW;
        private RadioButton rbKriging;
        private RadioButton rbSpline;
        private RadioButton rbHeatMap;
        private RadioButton rbConcentrationMap;
        private ProgressBar progressBar1;
        private Label lblSemivariance;
        private ComboBox cbSemivariance;
        private const String FIELD_VALUE = "TEMP";
        private const int GRID_RESOLUTION = 400;
        private TGIS_LayerVector src;
        private TGIS_LayerPixel dst;
        private TGIS_LayerVector plg;
        private TGIS_Extent ext;
        private double rat;
        private double dx;
        private double dy;
        private TGIS_Color clr;
        private int i;

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
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblMethod = new System.Windows.Forms.Label();
            this.rbIDW = new System.Windows.Forms.RadioButton();
            this.rbKriging = new System.Windows.Forms.RadioButton();
            this.rbSpline = new System.Windows.Forms.RadioButton();
            this.rbHeatMap = new System.Windows.Forms.RadioButton();
            this.rbConcentrationMap = new System.Windows.Forms.RadioButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblSemivariance = new System.Windows.Forms.Label();
            this.cbSemivariance = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Location = new System.Drawing.Point(152, 12);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(420, 358);
            this.GIS.TabIndex = 0;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerate.Location = new System.Drawing.Point(12, 376);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(134, 23);
            this.btnGenerate.TabIndex = 2;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(12, 13);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(43, 13);
            this.lblMethod.TabIndex = 3;
            this.lblMethod.Text = "Method";
            // 
            // rbIDW
            // 
            this.rbIDW.AutoSize = true;
            this.rbIDW.Location = new System.Drawing.Point(12, 30);
            this.rbIDW.Name = "rbIDW";
            this.rbIDW.Size = new System.Drawing.Size(107, 17);
            this.rbIDW.TabIndex = 4;
            this.rbIDW.TabStop = true;
            this.rbIDW.Text = "IDW interpolation";
            this.rbIDW.UseVisualStyleBackColor = true;
            // 
            // rbKriging
            // 
            this.rbKriging.AutoSize = true;
            this.rbKriging.Location = new System.Drawing.Point(12, 53);
            this.rbKriging.Name = "rbKriging";
            this.rbKriging.Size = new System.Drawing.Size(118, 17);
            this.rbKriging.TabIndex = 5;
            this.rbKriging.TabStop = true;
            this.rbKriging.Text = "Kriging Interpolation";
            this.rbKriging.UseVisualStyleBackColor = true;
            // 
            // rbSpline
            // 
            this.rbSpline.AutoSize = true;
            this.rbSpline.Location = new System.Drawing.Point(12, 76);
            this.rbSpline.Name = "rbSpline";
            this.rbSpline.Size = new System.Drawing.Size(114, 17);
            this.rbSpline.TabIndex = 6;
            this.rbSpline.TabStop = true;
            this.rbSpline.Text = "Spline interpolation";
            this.rbSpline.UseVisualStyleBackColor = true;
            // 
            // rbHeatMap
            // 
            this.rbHeatMap.AutoSize = true;
            this.rbHeatMap.Location = new System.Drawing.Point(12, 99);
            this.rbHeatMap.Name = "rbHeatMap";
            this.rbHeatMap.Size = new System.Drawing.Size(72, 17);
            this.rbHeatMap.TabIndex = 7;
            this.rbHeatMap.TabStop = true;
            this.rbHeatMap.Text = "Heat Map";
            this.rbHeatMap.UseVisualStyleBackColor = true;
            // 
            // rbConcentrationMap
            // 
            this.rbConcentrationMap.AutoSize = true;
            this.rbConcentrationMap.Location = new System.Drawing.Point(12, 122);
            this.rbConcentrationMap.Name = "rbConcentrationMap";
            this.rbConcentrationMap.Size = new System.Drawing.Size(115, 17);
            this.rbConcentrationMap.TabIndex = 8;
            this.rbConcentrationMap.TabStop = true;
            this.rbConcentrationMap.Text = "Concentration Map";
            this.rbConcentrationMap.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(152, 376);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(420, 23);
            this.progressBar1.TabIndex = 9;
            // 
            // lblSemivariance
            // 
            this.lblSemivariance.AutoSize = true;
            this.lblSemivariance.Location = new System.Drawing.Point(12, 157);
            this.lblSemivariance.Name = "lblSemivariance";
            this.lblSemivariance.Size = new System.Drawing.Size(71, 13);
            this.lblSemivariance.TabIndex = 10;
            this.lblSemivariance.Text = "Semivariance";
            // 
            // cbSemivariance
            // 
            this.cbSemivariance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSemivariance.FormattingEnabled = true;
            this.cbSemivariance.Items.AddRange(new object[] {
            "Power Law",
            "Exponential",
            "Gaussian",
            "Spherical",
            "Circular",
            "Linear"});
            this.cbSemivariance.Location = new System.Drawing.Point(12, 173);
            this.cbSemivariance.Name = "cbSemivariance";
            this.cbSemivariance.Size = new System.Drawing.Size(134, 21);
            this.cbSemivariance.TabIndex = 11;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(584, 411);
            this.Controls.Add(this.cbSemivariance);
            this.Controls.Add(this.lblSemivariance);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.rbConcentrationMap);
            this.Controls.Add(this.rbHeatMap);
            this.Controls.Add(this.rbSpline);
            this.Controls.Add(this.rbKriging);
            this.Controls.Add(this.rbIDW);
            this.Controls.Add(this.lblMethod);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.GIS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Interpolation";
            this.Load += new System.EventHandler(this.WinForm_Load);
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
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\Interpolation\Interpolation.ttkproject");
            GIS.CS = TGIS_CSFactory.ByEPSG(3395);

            GIS.FullExtent();

            rbIDW.CheckedChanged += doRbAnyClick;
            rbKriging.CheckedChanged += doRbAnyClick;
            rbSpline.CheckedChanged += doRbAnyClick;
            rbHeatMap.CheckedChanged += doRbAnyClick;
            rbConcentrationMap.CheckedChanged += doRbAnyClick;

            rbIDW.Checked = true;
            cbSemivariance.SelectedIndex = 0;
        }

        private void doRbAnyClick(Object _sender, EventArgs _e)
        {
            if (rbKriging.Checked)
            {
                lblSemivariance.Visible = true;
                cbSemivariance.Visible = true;
            }
            else
            {
                lblSemivariance.Visible = false;
                cbSemivariance.Visible = false;
            }
        }

        private void doBusyEvent(Object _sender, TGIS_BusyEventArgs _e)
        {
            if (_e.Pos < 0)
                progressBar1.Value = progressBar1.Maximum;
            else
            if (_e.Pos == 0)
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = (int)_e.EndPos;
                progressBar1.Value = 0;
            }
            else
            {
                if (_e.Pos % 100 == 0)
                    progressBar1.Value = (int)_e.Pos;
            }
        }

        private void doIDW()
        {
            TGIS_InterpolationIDW vtg;

            vtg = new TGIS_InterpolationIDW();

            // for windowed version of this method you need to set Windowed=True
            // and at least the Radius, e.g.
            // vtg.Windowed := True ;
            // vtg.Radius := ( ext.XMax - ext.XMin )/5.0 ;

            // attach the event to automatically update the progress bar
            vtg.BusyEvent += doBusyEvent;
            // set the exponent parameter of the IDW formula (default is 2.0,
            // 3.0 gives nice results in most cases)
            vtg.Exponent = 3.0;
            // generate the Inverse Distance Squared (IDW) interpolation grid
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent);
        }

        private void doKriging()
        {
            TGIS_InterpolationKriging vtg;

            vtg = new TGIS_InterpolationKriging();

            // for windowed version of this method you need to set Windowed=True
            // and at least the Radius, e.g.
            // vtg.Windowed := True ;
            // vtg.Radius := ( ext.XMax - ext.XMin )/5.0 ;

            // attach the event to automatically update the progress bar
            vtg.BusyEvent += doBusyEvent;

            // set Semivarinace; default is Power Law (code for case 0 is not needed)
            switch (cbSemivariance.SelectedIndex)
            {
                case 0: vtg.Semivariance = new TGIS_SemivariancePowerLaw(); break;
                case 1: vtg.Semivariance = new TGIS_SemivarianceExponential(); break;
                case 2: vtg.Semivariance = new TGIS_SemivarianceGaussian(); break;
                case 3: vtg.Semivariance = new TGIS_SemivarianceSpherical(); break;
                case 4: vtg.Semivariance = new TGIS_SemivarianceCircular(); break;
                case 5: vtg.Semivariance = new TGIS_SemivarianceLinear(); break;

            }

            // generate the Kriging interpolation grid
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent);
        }

        private void doSplines()
        {
            TGIS_InterpolationSplines vtg;

            vtg = new TGIS_InterpolationSplines();

            // for windowed version of this method you need to set Windowed=True
            // and at least the Radius, e.g.
            // vtg.Windowed := True ;
            // vtg.Radius := ( ext.XMax - ext.XMin )/5.0 ;

            // attach the event to automatically update the progress bar
            vtg.BusyEvent += doBusyEvent;
            // set the tension parameter of Splines (value need to be adjusted for
            // the data)
            vtg.Tension = 1e-9;
            // generate the Completely Regularized Splines interpolation grid
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent);
        }

        private void doHeatmap(Boolean _concentration)
        {
            TGIS_GaussianHeatmap vtg;
            String fld;

            vtg = new TGIS_GaussianHeatmap();

            // for Concentration Map the coordinate must be None and source field
            // must be empty
            vtg.Coordinate = TGIS_VectorToGridCoordinate.None;
            if (_concentration)
                fld = "";
            else
                fld = FIELD_VALUE;

            // attach the event to automatically update the progress bar
            vtg.BusyEvent += doBusyEvent;
            // estimate the 3-sigma for Gaussian (can be set manually with Radius)
            vtg.EstimateRadius(src, src.Extent, dst);
            // correct the Radius after estimation (if needed)
            vtg.Radius = vtg.Radius / 2.0;
            // generate the Heat/Concentaration Map grid
            vtg.Generate(src, src.Extent, fld, dst, dst.Extent);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            btnGenerate.Enabled = false;

            // obtain a handle to the source layer
            src = (TGIS_LayerVector)GIS.Get("temperatures");
            // obtain a handle to the polygonal layer (largest extent)
            plg = (TGIS_LayerVector)GIS.Get("country");

            // remove any previously created grid layer
            if (GIS.Get("grid") != null)
                GIS.Delete("Grid");

            // get the source layer extent
            ext = plg.Extent;

            // calculate the height/width ratio of the extent to properly set the grid
            // resolution
            rat = (ext.YMax - ext.YMin) / (ext.XMax - ext.XMin);

            // create and initialize the destination layer
            dst = new TGIS_LayerPixel();
            dst.Name = "grid";
            dst.Build(true, src.CS, ext, GRID_RESOLUTION, Convert.ToInt32(Math.Round(rat * GRID_RESOLUTION)));
            dst.Params.Pixel.GridShadow = false;

            // choose the start color of the grid ramp
            clr = TGIS_Color.Blue;

            // find out which vector-to-grid has beeno chosen
            if (rbIDW.Checked)
                // perform Inverse Distance Squared (IDW) interpolation
                doIDW();
            else
            if (rbKriging.Checked)
                // perform Kriging interpolation
                doKriging();
            else
            if (rbSpline.Checked)
                // perform Completely Regularized Splines interpolation
                doSplines();
            else
            if (rbHeatMap.Checked)
            {
                // perform Heat Map generation
                doHeatmap(false);
                // choose the start color for the grid ramp with ALPHA=0 to make it
                // semitransparent
                clr = TGIS_Color.FromARGB(0, 0, 0, 255);
            }
            else
            if (rbConcentrationMap.Checked)
            {
                // perform Concentration Map generation
                doHeatmap(true);
                // choose the start color for the grid ramp with ALPHA=0 to make it
                // semitransparent
                clr = TGIS_Color.FromARGB(0, 0, 0, 255);
            }

            // apply color ramp to the grid layer
            dst.GenerateRamp(
                clr, TGIS_Color.Lime, TGIS_Color.Red,
                dst.MinHeight, (dst.MaxHeight - dst.MinHeight) / 2.0, dst.MaxHeight, false,
                (dst.MaxHeight - dst.MinHeight) / 100.0,
                (dst.MaxHeight - dst.MinHeight) / 10.0,
                null, false
              );

            // limit the grid visibility only to the pixels contained within a polygon
            dst.CuttingPolygon = (TGIS_ShapePolygon)plg.GetShape(6).CreateCopy();

            // add the grid layer to the viewer
            GIS.Add(dst);
            // update the viewer to show the grid layer
            GIS.FullExtent();

            // reset the progress bar
            progressBar1.Value = 0;

            btnGenerate.Enabled = true;
        }
    }
}
