// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// Interpolation Sample - Generate a raster grid from vector point data using
// various spatial interpolation methods.
//
// This sample demonstrates:
//   - Opening a TatukGIS project file (.ttkproject) containing:
//       "temperatures" - a vector point layer with a numeric "TEMP" field
//       "country"      - a polygon layer defining the output extent and clip mask
//   - Setting the viewer's coordinate system to EPSG:3395 (World Mercator) so
//     that distance-based calculations are performed in metres.
//   - Creating an in-memory TGIS_LayerPixel as the interpolation output,
//     sized to match the polygon layer's geographic extent with square pixels.
//   - Running five vector-to-grid conversion methods:
//       IDW (Inverse Distance Weighting)  - weights samples by 1/distance^exponent
//       Kriging                           - geostatistical method using a
//                                           semivariogram model
//       Completely Regularized Splines    - smooth radial basis function surface
//       Gaussian Heat Map                 - kernel density using TEMP field values
//       Concentration Map                 - kernel density counting points only
//   - Applying a Blue->Lime->Red colour ramp via GenerateRamp.
//   - Clipping the output to the country polygon using CuttingPolygon.
//   - Reporting interpolation progress via a BusyEvent callback.
//
// Key TatukGIS API classes used:
//   TGIS_ViewerWnd              - WinForms map viewer control
//   TGIS_LayerVector            - source point / polygon layer
//   TGIS_LayerPixel             - destination raster grid layer
//   TGIS_InterpolationIDW       - IDW interpolation engine
//   TGIS_InterpolationKriging   - Kriging interpolation engine
//   TGIS_InterpolationSplines   - Completely Regularized Splines engine
//   TGIS_GaussianHeatmap        - Gaussian heat/concentration map engine
//   TGIS_CSFactory              - coordinate system factory (EPSG lookup)
// =============================================================================

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
    /// Main form for the Interpolation sample application.
    /// Loads a point layer and a country polygon, then lets the user generate
    /// a spatial interpolation grid using one of five supported methods.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private TGIS_ViewerWnd GIS;                  // Main map viewer
        private Button btnGenerate;                  // Triggers the interpolation run
        private Label lblMethod;                     // "Method" group label
        private RadioButton rbIDW;                   // IDW interpolation selector
        private RadioButton rbKriging;               // Kriging interpolation selector
        private RadioButton rbSpline;                // Splines interpolation selector
        private RadioButton rbHeatMap;               // Heat Map selector
        private RadioButton rbConcentrationMap;      // Concentration Map selector
        private ProgressBar progressBar1;            // Shows interpolation progress
        private Label lblSemivariance;               // "Semivariance" label (Kriging only)
        private ComboBox cbSemivariance;             // Kriging semivariance model picker

        // Name of the attribute field in the point layer to interpolate
        private const String FIELD_VALUE = "TEMP";
        // Output grid width in pixels; height is computed from extent aspect ratio
        private const int GRID_RESOLUTION = 400;

        // Fields shared between WinForm_Load and btnGenerate_Click
        private TGIS_LayerVector src;  // Source point layer ("temperatures")
        private TGIS_LayerPixel dst;   // Destination raster grid layer ("grid")
        private TGIS_LayerVector plg;  // Country polygon layer for extent/clip
        private TGIS_Extent ext;       // Geographic bounding box of the polygon
        private double rat;            // Height/width aspect ratio of the extent
        private double dx;
        private double dy;
        private TGIS_Color clr;        // Start colour of the colour ramp
        private int i;

        /// <summary>Required designer variable.</summary>
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
            #if NET6_0_OR_GREATER
              ApplicationConfiguration.Initialize();
            #else
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
            #endif
            Application.Run(new WinForm());
        }

        /// <summary>
        /// Loads the project file and wires up the method radio button handlers.
        /// The project contains "temperatures" (points) and "country" (polygon).
        /// Setting GIS.CS to EPSG:3395 (World Mercator) ensures that distance
        /// calculations inside the interpolation engine use consistent metre units.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            // Open the bundled project; layers are created from paths stored inside
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\Interpolation\Interpolation.ttkproject");
            // Override the viewer CRS to World Mercator for accurate distance-based interpolation
            GIS.CS = TGIS_CSFactory.ByEPSG(3395);

            GIS.FullExtent();

            // Wire radio buttons so Kriging-specific UI appears/disappears appropriately
            rbIDW.CheckedChanged += doRbAnyClick;
            rbKriging.CheckedChanged += doRbAnyClick;
            rbSpline.CheckedChanged += doRbAnyClick;
            rbHeatMap.CheckedChanged += doRbAnyClick;
            rbConcentrationMap.CheckedChanged += doRbAnyClick;

            // Select IDW as the default method; hide the Kriging-only semivariance picker
            rbIDW.Checked = true;
            cbSemivariance.SelectedIndex = 0;
        }

        /// <summary>
        /// Shared handler for all five method radio buttons.
        /// Shows the Semivariance combo only when Kriging is selected, because
        /// no other method uses a semivariogram model.
        /// </summary>
        private void doRbAnyClick(Object _sender, EventArgs _e)
        {
            if (rbKriging.Checked)
            {
                // Kriging requires a semivariogram model choice
                lblSemivariance.Visible = true;
                cbSemivariance.Visible = true;
            }
            else
            {
                // All other methods work without a semivariogram
                lblSemivariance.Visible = false;
                cbSemivariance.Visible = false;
            }
        }

        /// <summary>
        /// Progress callback invoked by the interpolation engine at regular intervals.
        /// Convention used by TatukGIS busy events:
        ///   Pos == 0        => initialise the progress bar (EndPos is the maximum)
        ///   Pos &lt; 0     => operation finished (reset to maximum to show completion)
        ///   Pos &gt; 0     => update bar; throttled every 100 steps to reduce UI overhead
        /// </summary>
        private void doBusyEvent(Object _sender, TGIS_BusyEventArgs _e)
        {
            if (_e.Pos < 0)
                // Finished: show full bar briefly before the caller resets it
                progressBar1.Value = progressBar1.Maximum;
            else
            if (_e.Pos == 0)
            {
                // Initialise: configure the range for this operation
                progressBar1.Minimum = 0;
                progressBar1.Maximum = (int)_e.EndPos;
                progressBar1.Value = 0;
            }
            else
            {
                // Throttle UI updates to every 100 steps to avoid sluggishness
                if (_e.Pos % 100 == 0)
                    progressBar1.Value = (int)_e.Pos;
            }
        }

        /// <summary>
        /// Runs Inverse Distance Weighting (IDW) interpolation.
        /// IDW estimates each output cell as a weighted average of all sample points,
        /// where the weight is 1/distance^Exponent.  An Exponent of 3.0 produces a
        /// more localised surface with sharper peaks than the default of 2.0.
        /// For large datasets, enable Windowed mode with a search Radius to limit
        /// the number of contributing samples and improve performance.
        /// </summary>
        private void doIDW()
        {
            TGIS_InterpolationIDW vtg;

            vtg = new TGIS_InterpolationIDW();

            // For windowed version of this method you need to set Windowed=True
            // and at least the Radius, e.g.:
            //   vtg.Windowed = true;
            //   vtg.Radius = (ext.XMax - ext.XMin) / 5.0;

            // Attach the event to automatically update the progress bar
            vtg.BusyEvent += doBusyEvent;
            // Exponent 3.0 gives sharper, more localised peaks around sample points
            // compared to the default 2.0 (Inverse Distance Squared)
            vtg.Exponent = 3.0;
            // Generate the IDW grid: read FIELD_VALUE from src, write to dst
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent);
        }

        /// <summary>
        /// Runs Ordinary Kriging interpolation.
        /// Kriging models spatial autocorrelation via a semivariogram and produces
        /// a statistically optimal (Best Linear Unbiased) estimate at each cell.
        /// The semivariogram model (Power Law, Exponential, Gaussian, etc.) should
        /// be chosen to fit the known spatial structure of the data.
        /// </summary>
        private void doKriging()
        {
            TGIS_InterpolationKriging vtg;

            vtg = new TGIS_InterpolationKriging();

            // For windowed version of this method you need to set Windowed=True
            // and at least the Radius, e.g.:
            //   vtg.Windowed = true;
            //   vtg.Radius = (ext.XMax - ext.XMin) / 5.0;

            // Attach the event to automatically update the progress bar
            vtg.BusyEvent += doBusyEvent;

            // Select the semivariogram model based on the combo box choice.
            // Power Law is the default; other models may better represent the data.
            switch (cbSemivariance.SelectedIndex)
            {
                case 0: vtg.Semivariance = new TGIS_SemivariancePowerLaw(); break;     // scale-free power relationship
                case 1: vtg.Semivariance = new TGIS_SemivarianceExponential(); break;  // exponential decay of correlation
                case 2: vtg.Semivariance = new TGIS_SemivarianceGaussian(); break;     // smooth Gaussian bell-curve decay
                case 3: vtg.Semivariance = new TGIS_SemivarianceSpherical(); break;    // linear then flat (common in geology)
                case 4: vtg.Semivariance = new TGIS_SemivarianceCircular(); break;     // circular model
                case 5: vtg.Semivariance = new TGIS_SemivarianceLinear(); break;       // simple linear model
            }

            // Generate the Kriging interpolation grid
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent);
        }

        /// <summary>
        /// Runs Completely Regularized Splines (CRS) interpolation.
        /// CRS fits a smooth surface through the sample points using radial basis
        /// functions.  A very small Tension (1e-9) gives a minimum-curvature surface;
        /// increase it if unwanted oscillations appear in data-sparse regions.
        /// </summary>
        private void doSplines()
        {
            TGIS_InterpolationSplines vtg;

            vtg = new TGIS_InterpolationSplines();

            // For windowed version of this method you need to set Windowed=True
            // and at least the Radius, e.g.:
            //   vtg.Windowed = true;
            //   vtg.Radius = (ext.XMax - ext.XMin) / 5.0;

            // Attach the event to automatically update the progress bar
            vtg.BusyEvent += doBusyEvent;
            // Tension controls surface stiffness; 1e-9 produces a very smooth result
            vtg.Tension = 1e-9;
            // Generate the Completely Regularized Splines interpolation grid
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent);
        }

        /// <summary>
        /// Runs Gaussian Heat Map or Concentration Map generation.
        /// Both modes use <see cref="TGIS_GaussianHeatmap"/>; the difference is:
        ///   Heat Map (_concentration=false):  each point spreads its TEMP value
        ///     using a Gaussian kernel; high-value points create brighter hotspots.
        ///   Concentration Map (_concentration=true): ignores TEMP, counts only
        ///     point density so every point contributes equally.
        /// EstimateRadius computes the optimal kernel bandwidth automatically;
        /// halving it produces a finer, more peaked result.
        /// </summary>
        private void doHeatmap(Boolean _concentration)
        {
            TGIS_GaussianHeatmap vtg;
            String fld;

            vtg = new TGIS_GaussianHeatmap();

            // Coordinate.None uses point centroids as-is (no centroid offset)
            vtg.Coordinate = TGIS_VectorToGridCoordinate.None;
            // Concentration Map: empty field name means point presence only
            if (_concentration)
                fld = "";
            else
                fld = FIELD_VALUE;

            // Attach the event to automatically update the progress bar
            vtg.BusyEvent += doBusyEvent;
            // Estimate the Gaussian kernel radius from the average nearest-neighbour
            // distance in the source layer (3-sigma bandwidth)
            vtg.EstimateRadius(src, src.Extent, dst);
            // Halve the estimated radius for a more localised, peaked result
            vtg.Radius = vtg.Radius / 2.0;
            // Generate the Heat/Concentration Map grid
            vtg.Generate(src, src.Extent, fld, dst, dst.Extent);
        }

        /// <summary>
        /// Main Generate button handler: orchestrates the full interpolation workflow.
        ///
        /// Steps:
        ///   1. Retrieve source point and country polygon layers by name.
        ///   2. Delete any previous "grid" layer to avoid duplicates.
        ///   3. Create a blank in-memory TGIS_LayerPixel sized to the polygon extent
        ///      with square pixels (aspect ratio correction applied).
        ///   4. Dispatch to the chosen interpolation method.
        ///   5. Apply a Blue->Lime->Red colour ramp across the full value range.
        ///   6. Clip the output to the country polygon via CuttingPolygon.
        ///   7. Add the grid to the viewer and zoom to show the result.
        /// </summary>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            btnGenerate.Enabled = false;

            // Retrieve named layers from the loaded project
            src = (TGIS_LayerVector)GIS.Get("temperatures");  // point data source
            plg = (TGIS_LayerVector)GIS.Get("country");       // polygon for extent/clip

            // Remove any previous interpolation result so we start fresh
            if (GIS.Get("grid") != null)
                GIS.Delete("Grid");

            // Use the polygon's extent as output domain (larger than scattered points)
            ext = plg.Extent;

            // Compute aspect ratio so output pixels remain square when projected
            rat = (ext.YMax - ext.YMin) / (ext.XMax - ext.XMin);

            // Create and initialise the in-memory raster output layer
            dst = new TGIS_LayerPixel();
            dst.Name = "grid";
            // Build allocates the pixel buffer: true=in-memory, src.CS=CRS,
            // GRID_RESOLUTION=width, rounded height maintains square pixels
            dst.Build(true, src.CS, ext, GRID_RESOLUTION, Convert.ToInt32(Math.Round(rat * GRID_RESOLUTION)));
            // Disable hill-shading - the interpolated values are not elevation data
            dst.Params.Pixel.GridShadow = false;

            // Default ramp start colour (overridden to transparent for heat maps)
            clr = TGIS_Color.Blue;

            // Dispatch to the selected interpolation method
            if (rbIDW.Checked)
                // Inverse Distance Weighting
                doIDW();
            else
            if (rbKriging.Checked)
                // Geostatistical Kriging
                doKriging();
            else
            if (rbSpline.Checked)
                // Completely Regularized Splines
                doSplines();
            else
            if (rbHeatMap.Checked)
            {
                // Gaussian heat map weighted by TEMP field
                doHeatmap(false);
                // Use transparent blue start so basemap shows through zero-density areas
                clr = TGIS_Color.FromARGB(0, 0, 0, 255);
            }
            else
            if (rbConcentrationMap.Checked)
            {
                // Gaussian concentration map (point density only)
                doHeatmap(true);
                clr = TGIS_Color.FromARGB(0, 0, 0, 255);
            }

            // Apply a three-stop colour ramp across the full value range of the grid.
            // Blue (low) -> Lime (mid) -> Red (high), divided into fine steps for
            // a smooth gradient.
            dst.GenerateRamp(
                clr, TGIS_Color.Lime, TGIS_Color.Red,
                dst.MinHeight, (dst.MaxHeight - dst.MinHeight) / 2.0, dst.MaxHeight, false,
                (dst.MaxHeight - dst.MinHeight) / 100.0,
                (dst.MaxHeight - dst.MinHeight) / 10.0,
                null, false
              );

            // Clip the grid to the country boundary so only cells within the polygon
            // are rendered.  GetShape(6) is the 7th shape (0-based index) in the
            // polygon layer; CreateCopy is needed because CuttingPolygon takes ownership.
            dst.CuttingPolygon = (TGIS_ShapePolygon)plg.GetShape(6).CreateCopy();

            // Add the generated grid to the viewer layer list
            GIS.Add(dst);
            // Zoom to display the full interpolated result
            GIS.FullExtent();

            // Reset the progress bar
            progressBar1.Value = 0;

            btnGenerate.Enabled = true;
        }
    }
}
