/* ExportToImage sample — demonstrates exporting GIS layers to raster image formats.

   What the sample shows:
     - Loading raster imagery (JPEG, TIFF, etc.) into the GIS viewer
     - Loading DEM/elevation grids (ArcInfo ADF) into the viewer
     - Creating output TGIS_LayerPixel via automatic driver detection
     - Querying layer Capabilities to discover format sub-options
     - Discovering pixel depth, compression, and other driver-specific formats
     - Controlling export resolution via three strategies:
       * Best quality: pixel size matched to highest-density source layer
       * For document: fixed physical paper width at 300 DPI (print quality)
       * For Web: fixed pixel width (640 px) at 96 DPI (screen resolution)
     - Controlling export spatial coverage: full or visible extent
     - Performing raster conversion with ImportLayer resampling
     - Saving output to user-selected file format and location
     - Supporting multiple output formats (GeoTIFF, PNG, JPEG, etc.)

   Key TatukGIS API concepts shown here:
     TGIS_ViewerWnd              - main visual map control
     TGIS_LayerPixel             - raster/pixel layer for output format
     TGIS_Utils.GisCreateLayer() - automatic layer type detection from extension
     TGIS_Layer.Capabilities     - query format-specific driver options
     ImportLayer()               - convert/resample layer to target format
     Resolution strategies       - quality, document, web export modes
     Spatial extent              - full vs. visible area selection
     Compression options         - driver-specific compression settings
     TGIS_Params                 - layer styling and rendering parameters
*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;
using System.IO;

namespace ExportToImage
{
    /// <summary>
    /// Main application form for the ExportToImage sample.
    /// Lets the user choose between a satellite image and an elevation grid,
    /// pick an output file and format, then export to a raster file.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        // --- Resolution / dimension constants ---

        /// <summary>Default PPI for best-quality and document export modes.
        /// 300 DPI is the standard minimum for print-quality raster output.</summary>
        const int DEFAULT_PPI = 300;

        /// <summary>Screen/web resolution in DPI. 96 PPI is the standard Windows
        /// screen DPI and produces compact images suitable for web display.</summary>
        const int DEFAULT_PPI_WEB = 96;

        /// <summary>Document print resolution in DPI.</summary>
        const int DEFAULT_PPI_DOC = 300;

        /// <summary>Fallback output width in pixels when no raster layer is present
        /// to derive a natural resolution. 4200 px at 300 DPI = 14-inch wide image.</summary>
        const int DEFAULT_WIDTHPIX = 4200;

        /// <summary>Default pixel width for the web export profile (640 px wide).</summary>
        const int DEFAULT_WIDTHPIX_WEB = 640;

        /// <summary>Document page-width references used by the document preset.
        /// 160 mm / 16 cm / 6.3 inches is a typical A4 text-area width.</summary>
        const int DEFAULT_WIDTH_DOC_MM = 160;
        const int DEFAULT_WIDTH_DOC_CM = 16;
        const double DEFAULT_WIDTH_DOC_INCH = 6.3;

        // Unit selector constants (reserved for potential UI extension)
        const int UNITS_MM   = 0;
        const int UNITS_CM   = 1;
        const int UNITS_INCH = 2;

        // --- UI controls (managed by InitializeComponent) ---
        private GroupBox groupBox1;   // Viewer panel group
        private RadioButton rbGrid;   // Switch to elevation grid mode
        private RadioButton rbImage;  // Switch to raster image mode
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS; // Interactive map viewer
        private GroupBox groupBox2;   // File path group
        private Button btnOpen;       // Opens the save-file dialog
        private TextBox tbPath;       // Displays chosen output file path
        private GroupBox groupBox3;   // Export options group (enabled after file chosen)
        private GroupBox groupBox4;   // Resolution sub-group
        private Button btnExport;     // Triggers the export action
        private RadioButton rbWebQ;   // Web quality preset
        private RadioButton rbDocQ;   // Document quality preset
        private RadioButton rbBestQ;  // Best quality preset
        private Label lbExtent;
        private Label lbFormat;
        private RadioButton rbExtentVisible; // Export current viewport only
        private RadioButton rbExtentFull;    // Export full geographic extent
        private ComboBox cbFormat;    // Lists available TGIS_LayerPixelSubFormats

        /// <summary>Source pixel layer loaded in the viewer.
        /// This is the layer whose pixel data will be resampled into the output file.</summary>
        private TGIS_LayerPixel lstp;

        /// <summary>Target pixel layer that writes to the chosen output file.
        /// Created by GisCreateLayer and populated by ImportLayer.</summary>
        private TGIS_LayerPixel lpx;

        /// <summary>Geographic bounding box used for the export.
        /// Set from either GIS.Extent (full) or GIS.VisibleExtent (viewport).</summary>
        private TGIS_Extent FExtent;

        /// <summary>Physical output size in inches (used by the document preset to
        /// derive pixel dimensions from PPI).</summary>
        private double expWidth, expHeight;

        /// <summary>Output image dimensions in pixels. Passed to ImportLayer.
        /// Aspect ratio is always derived from the geographic extent.</summary>
        private double PixWidth, PixHeight;

        /// <summary>Pixels per inch for the current export mode.</summary>
        private int Ppi;

        private SaveFileDialog dlgSaveImage; // Save dialog filtered to image formats
        private SaveFileDialog dlgSaveGrid;  // Save dialog filtered to grid formats

        /// <summary>Parallel array to cbFormat.Items that holds a deep copy of each
        /// TGIS_LayerPixelSubFormat so the selection survives list modifications.</summary>
        private T_capability[] caps;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGrid = new System.Windows.Forms.RadioButton();
            this.rbImage = new System.Windows.Forms.RadioButton();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbExtent = new System.Windows.Forms.Label();
            this.lbFormat = new System.Windows.Forms.Label();
            this.rbExtentVisible = new System.Windows.Forms.RadioButton();
            this.rbExtentFull = new System.Windows.Forms.RadioButton();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbWebQ = new System.Windows.Forms.RadioButton();
            this.rbDocQ = new System.Windows.Forms.RadioButton();
            this.rbBestQ = new System.Windows.Forms.RadioButton();
            this.btnExport = new System.Windows.Forms.Button();
            this.dlgSaveImage = new System.Windows.Forms.SaveFileDialog();
            this.dlgSaveGrid = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.rbGrid);
            this.groupBox1.Controls.Add(this.rbImage);
            this.groupBox1.Controls.Add(this.GIS);
            this.groupBox1.Location = new System.Drawing.Point(25, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 231);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Viewer";
            //
            // rbGrid
            //
            this.rbGrid.AutoSize = true;
            this.rbGrid.Location = new System.Drawing.Point(438, 59);
            this.rbGrid.Name = "rbGrid";
            this.rbGrid.Size = new System.Drawing.Size(44, 17);
            this.rbGrid.TabIndex = 2;
            this.rbGrid.Text = "Grid";
            this.rbGrid.UseVisualStyleBackColor = true;
            this.rbGrid.CheckedChanged += new System.EventHandler(this.rbGrid_CheckedChanged);
            //
            // rbImage
            //
            this.rbImage.AutoSize = true;
            this.rbImage.Checked = true;
            this.rbImage.Location = new System.Drawing.Point(438, 35);
            this.rbImage.Name = "rbImage";
            this.rbImage.Size = new System.Drawing.Size(54, 17);
            this.rbImage.TabIndex = 1;
            this.rbImage.TabStop = true;
            this.rbImage.Text = "Image";
            this.rbImage.UseVisualStyleBackColor = true;
            this.rbImage.CheckedChanged += new System.EventHandler(this.rbImage_CheckedChanged);
            //
            // GIS
            //
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(6, 19);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(426, 198);
            this.GIS.TabIndex = 0;
            //
            // groupBox2
            //
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Controls.Add(this.tbPath);
            this.groupBox2.Location = new System.Drawing.Point(25, 277);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(517, 55);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File";
            //
            // btnOpen
            //
            this.btnOpen.Location = new System.Drawing.Point(458, 18);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(35, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            //
            // tbPath
            //
            this.tbPath.Location = new System.Drawing.Point(6, 20);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(426, 20);
            this.tbPath.TabIndex = 0;
            //
            // groupBox3
            //
            this.groupBox3.Controls.Add(this.lbExtent);
            this.groupBox3.Controls.Add(this.lbFormat);
            this.groupBox3.Controls.Add(this.rbExtentVisible);
            this.groupBox3.Controls.Add(this.rbExtentFull);
            this.groupBox3.Controls.Add(this.cbFormat);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(25, 349);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(517, 123);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            //
            // lbExtent
            //
            this.lbExtent.AutoSize = true;
            this.lbExtent.Location = new System.Drawing.Point(20, 51);
            this.lbExtent.Name = "lbExtent";
            this.lbExtent.Size = new System.Drawing.Size(37, 13);
            this.lbExtent.TabIndex = 19;
            this.lbExtent.Text = "Extent";
            //
            // lbFormat
            //
            this.lbFormat.AutoSize = true;
            this.lbFormat.Location = new System.Drawing.Point(21, 22);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(39, 13);
            this.lbFormat.TabIndex = 18;
            this.lbFormat.Text = "Format";
            //
            // rbExtentVisible
            //
            this.rbExtentVisible.AutoSize = true;
            this.rbExtentVisible.Location = new System.Drawing.Point(61, 74);
            this.rbExtentVisible.Name = "rbExtentVisible";
            this.rbExtentVisible.Size = new System.Drawing.Size(55, 17);
            this.rbExtentVisible.TabIndex = 17;
            this.rbExtentVisible.Text = "Visible";
            this.rbExtentVisible.UseVisualStyleBackColor = true;
            //
            // rbExtentFull
            //
            this.rbExtentFull.AutoSize = true;
            this.rbExtentFull.Checked = true;
            this.rbExtentFull.Location = new System.Drawing.Point(61, 51);
            this.rbExtentFull.Name = "rbExtentFull";
            this.rbExtentFull.Size = new System.Drawing.Size(41, 17);
            this.rbExtentFull.TabIndex = 16;
            this.rbExtentFull.TabStop = true;
            this.rbExtentFull.Text = "Full";
            this.rbExtentFull.UseVisualStyleBackColor = true;
            //
            // cbFormat
            //
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Location = new System.Drawing.Point(61, 19);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(121, 21);
            this.cbFormat.TabIndex = 15;
            //
            // groupBox4
            //
            this.groupBox4.Controls.Add(this.rbWebQ);
            this.groupBox4.Controls.Add(this.rbDocQ);
            this.groupBox4.Controls.Add(this.rbBestQ);
            this.groupBox4.Location = new System.Drawing.Point(251, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 109);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Resolution";
            //
            // rbWebQ
            //
            this.rbWebQ.AutoSize = true;
            this.rbWebQ.Location = new System.Drawing.Point(7, 83);
            this.rbWebQ.Name = "rbWebQ";
            this.rbWebQ.Size = new System.Drawing.Size(66, 17);
            this.rbWebQ.TabIndex = 2;
            this.rbWebQ.Text = "For Web";
            this.rbWebQ.UseVisualStyleBackColor = true;
            //
            // rbDocQ
            //
            this.rbDocQ.AutoSize = true;
            this.rbDocQ.Location = new System.Drawing.Point(7, 60);
            this.rbDocQ.Name = "rbDocQ";
            this.rbDocQ.Size = new System.Drawing.Size(90, 17);
            this.rbDocQ.TabIndex = 1;
            this.rbDocQ.Text = "For document";
            this.rbDocQ.UseVisualStyleBackColor = true;
            //
            // rbBestQ
            //
            this.rbBestQ.AutoSize = true;
            this.rbBestQ.Checked = true;
            this.rbBestQ.Location = new System.Drawing.Point(7, 37);
            this.rbBestQ.Name = "rbBestQ";
            this.rbBestQ.Size = new System.Drawing.Size(79, 17);
            this.rbBestQ.TabIndex = 0;
            this.rbBestQ.TabStop = true;
            this.rbBestQ.Text = "Best quality";
            this.rbBestQ.UseVisualStyleBackColor = true;
            //
            // btnExport
            //
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(237, 478);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            //
            // dlgSaveImage - filter covers the image formats that DK can write
            //
            this.dlgSaveImage.Filter = "JPEG File Interchange Format (*.jpg)|*.jpg|Portable Network Graphic (*.png)|*.png" +
    "|Tag Image File Format (*.tif)|*.tif|Window Bitmap (*.bmp)|*.bmp|TatukGIS PixelS" +
    "tore (*.ttkps)|*.ttkps";
            //
            // dlgSaveGrid - filter covers the elevation/numeric grid formats DK can write
            //
            this.dlgSaveGrid.Filter = "Arc/Info Binary Grid (*.flt)|*.flt|Arc/Info ASCII Grid (*.grd)|*.grd|TatukGIS Pix" +
    "elStore (*.ttkps)|*.ttkps";
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(568, 508);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - ExportToImage";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Application entry point. Configures high-DPI and visual styles before
        /// launching the main form.
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
        /// Loads the default satellite image (world_8km.jpg) into the viewer on
        /// startup so the user immediately sees a sample layer to export.
        /// TGIS_Utils.GisSamplesDataDirDownload() resolves to the shared sample
        /// data directory, which is downloaded separately.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\VisibleEarth\world_8km.jpg");
        }

        /// <summary>
        /// Thin wrapper that deep-copies a TGIS_LayerPixelSubFormat descriptor.
        /// Stored in the caps[] array parallel to the cbFormat items so that each
        /// combobox entry owns an independent copy that survives list modifications.
        /// </summary>
        public class T_capability
        {
            /// <summary>The wrapped sub-format descriptor (pixel depth, compression, etc.).</summary>
            public TGIS_LayerPixelSubFormat C;

            /// <param name="_c">Source sub-format to copy. CreateCopy produces a
            /// deep copy independent of the originating format list.</param>
            public T_capability(TGIS_LayerPixelSubFormat _c)
            {
                C = _c.CreateCopy();
            }
        }

        /// <summary>
        /// Calculate the optimal output pixel dimensions for the "Best quality" preset.
        ///
        /// Scans all pixel layers in the viewer to find the one with the highest pixel
        /// density (BitWidth / geographic extent width). The export pixel width is then
        /// scaled so that the chosen export extent is rendered at that same native density,
        /// preserving the maximum detail available in the source data. Falls back to
        /// DEFAULT_WIDTHPIX (4200 px) when no raster layer is loaded.
        /// </summary>
        private void ValuesInit()
        {
            int i, j;
            TGIS_Layer la;
            double density,   // Pixel density (px/map-unit) of the best layer found
                   density0,  // Density of the previously examined layer
                   density1;  // Density of the current layer being examined
            int widthpix;     // Computed output width in pixels
            double ext_delta, // Ratio: export extent width / best-layer extent width
                   ext_width; // Geographic width of the layer under examination

            density0 = 0;
            density = density0;
            Ppi = DEFAULT_PPI;
            j = 0;

            // Iterate from the top layer downwards to find the densest raster layer
            for (i = GIS.Items.Count - 1; i > 0; i--)
            {
                la = (TGIS_Layer)GIS.Items[i];

                if (la is TGIS_LayerPixel)
                {
                    ext_width = la.Extent.XMax - la.Extent.XMin;

                    // BitWidth is the layer's native pixel width over its full extent;
                    // dividing by geographic width gives pixels per map unit.
                    density1 = ((TGIS_LayerPixel)la).BitWidth / ext_width;
                    if (density1 > density0)
                    {
                        density = density1;
                        j = i; // Remember the index of the highest-density layer
                    }
                    density0 = density1;
                }
            }

            if (density == 0)
                // No raster layers found; use the predefined fallback
                widthpix = 4200;
            else
            {
                la = (TGIS_Layer)GIS.Items[j];
                ext_width = la.Extent.XMax - la.Extent.XMin;

                // ext_delta is the fraction of the best layer's extent covered by FExtent.
                // Multiplying by BitWidth gives the source pixels in that region, which is
                // the ideal export width to preserve native resolution without upscaling.
                ext_delta = (FExtent.XMax - FExtent.XMin) / ext_width;
                widthpix = (int)Math.Round(ext_delta * ((TGIS_LayerPixel)GIS.Items[j]).BitWidth);
            }

            PixWidth = widthpix;

            // Preserve geographic aspect ratio so the exported image is not distorted
            if ((FExtent.XMax - FExtent.XMin) != 0)
                PixHeight = ((FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth);
            else
            {
                // Zero-width extent guard: produce a minimal valid raster
                PixWidth = 2;
                PixHeight = 2;
            }
        }

        /// <summary>
        /// Switch the viewer to the sample satellite image and reset export controls.
        /// The world_8km.jpg is a global RGB mosaic at ~8 km/pixel, suitable for
        /// demonstrating image-format export (JPEG, PNG, TIFF, BMP, TTKPS).
        /// </summary>
        private void rbImage_CheckedChanged(object sender, EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\VisibleEarth\world_8km.jpg");
            tbPath.Clear();
            cbFormat.ResetText();
            cbFormat.Items.Clear();
            // Disable export options until a destination file has been selected
            groupBox3.Enabled = false;
            btnExport.Enabled = false;
        }

        /// <summary>
        /// Switch the viewer to a DEM elevation grid (ADF format) and reset export
        /// controls. The hdr.adf is an ESRI Arc/Info Binary Grid header; DK opens
        /// the entire dataset by pointing at that file. Grid export supports formats
        /// such as Arc/Info FLT, ASCII GRD, and TatukGIS PixelStore (TTKPS).
        /// </summary>
        private void rbGrid_CheckedChanged(object sender, EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\USA\States\California\San Bernardino\NED\hdr.adf");
            tbPath.Clear();
            cbFormat.ResetText();
            cbFormat.Items.Clear();
            groupBox3.Enabled = false;
            btnExport.Enabled = false;
        }

        /// <summary>
        /// Perform the raster export.
        ///
        /// Steps:
        /// 1. Resolve the chosen TGIS_LayerPixelSubFormat from the combobox, or
        ///    fall back to the layer's natural default.
        /// 2. Set FExtent from the user's spatial coverage choice.
        /// 3. Calculate PixWidth / PixHeight according to the resolution preset.
        /// 4. Call lpx.ImportLayer to resample the source into the output file.
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            // Retrieve the sub-format selected by the user (bit depth, compression, etc.)
            TGIS_LayerPixelSubFormat c;
            if (cbFormat.SelectedIndex >= 0)
                c = caps[cbFormat.SelectedIndex].C;
            else
                c = lpx.DefaultSubFormat; // Fall back to the format's natural default

            // Determine the geographic area to export
            if (rbExtentFull.Checked)
            {
                FExtent = GIS.Extent;         // Full bounding box of all loaded layers
            }
            else if (rbExtentVisible.Checked)
            {
                FExtent = GIS.VisibleExtent;  // Current viewport in map coordinates
            }

            // --- Resolution strategy ---
            if (rbBestQ.Checked)
            {
                // Match pixel density of the highest-resolution source layer
                ValuesInit();
            }
            else if (rbDocQ.Checked)
            {
                // Document preset: fixed physical width (6.3 inches), 300 DPI
                Ppi = DEFAULT_PPI_DOC;
                expWidth = DEFAULT_WIDTH_DOC_INCH;
                if ((FExtent.XMax - FExtent.XMin) != 0)
                    // Aspect ratio preserved from the geographic extent
                    expHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * expWidth;
                else
                {
                    expWidth = 2;
                    expHeight = 2;
                }
                // Convert physical size (inches) * DPI -> pixel dimensions
                ValueWHpix();
            }
            else if (rbWebQ.Checked)
            {
                // Web preset: fixed pixel width (640 px), 96 DPI
                Ppi = DEFAULT_PPI_WEB;
                PixWidth = DEFAULT_WIDTHPIX_WEB;

                if ((FExtent.XMax - FExtent.XMin) != 0)
                    PixHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth;
                else
                {
                    PixWidth = 2;
                    PixHeight = 2;
                }
                // Derive physical dimensions from pixel count / DPI (stored for reference)
                ValuesWH();
            }

            // ImportLayer resamples lstp (source layer) into lpx (output file):
            //   lstp         - source TGIS_LayerPixel to read pixel data from
            //   lstp.Extent  - full geographic coverage of the source
            //   lstp.CS      - coordinate system, ensuring correct spatial referencing
            //   PixWidth/PixHeight - desired output raster dimensions in pixels
            //   c            - sub-format descriptor (bit depth, compression, etc.)
            lpx.ImportLayer(lstp, lstp.Extent, lstp.CS, (uint)PixWidth, (uint)PixHeight, c);
            MessageBox.Show("Done!", "File exported");
        }

        /// <summary>
        /// Open the save-file dialog, create the output pixel layer via GisCreateLayer,
        /// and populate the format combobox with the sub-formats the chosen file format
        /// supports.
        ///
        /// TGIS_LayerPixelSubFormat describes properties like bit depth and compression
        /// that vary by format (e.g. TIFF supports LZW, DEFLATE, none; JPEG supports
        /// quality levels). Each item in cbFormat represents one valid combination.
        /// </summary>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            TList<TGIS_LayerPixelSubFormat> clst;
            int i;

            // Show the appropriate save dialog based on the current data-type selection
            if (rbImage.Checked)
            {
                if (dlgSaveImage.ShowDialog() != DialogResult.OK)
                    return;

                tbPath.Text = dlgSaveImage.FileName;
            }
            else if (rbGrid.Checked)
            {
                if (dlgSaveGrid.ShowDialog() != DialogResult.OK)
                    return;

                tbPath.Text = dlgSaveGrid.FileName;
            }

            if (cbFormat.Items.Count != 0)
                cbFormat.Items.Clear();

            // GIS.Items[0] is the bottom-most (primary) layer in the viewer stack
            lstp = (TGIS_LayerPixel)GIS.Items[0];

            // GisCreateLayer selects the correct DK layer driver from the file
            // extension (e.g. ".jpg" -> JPEG driver, ".tif" -> TIFF driver, etc.)
            // and returns an empty TGIS_LayerPixel ready to receive raster data.
            if (rbImage.Checked)
                lpx = TGIS_Utils.GisCreateLayer(GetFileName(tbPath.Text), dlgSaveImage.FileName) as TGIS_LayerPixel;
            else
                lpx = TGIS_Utils.GisCreateLayer(GetFileName(tbPath.Text), dlgSaveGrid.FileName) as TGIS_LayerPixel;

            // Capabilities returns all valid TGIS_LayerPixelSubFormat options for this format.
            // Build the combobox and a parallel caps[] array of deep copies.
            clst = lpx.Capabilities;
            i = 0;
            caps = new T_capability[clst.Count];
            foreach (TGIS_LayerPixelSubFormat c in clst)
            {
                cbFormat.Items.Add(c.ToString());
                caps[i] = new T_capability(c); // Deep copy to survive list disposal
                i++;
            }

            cbFormat.SelectedIndex = 0;

            // Enable export controls now that a valid destination and layer exist
            groupBox3.Enabled = true;
            btnExport.Enabled = true;
        }

        /// <summary>
        /// Return the file name without its extension.
        /// DK's GisCreateLayer expects the layer name (without path or extension)
        /// as the first argument so it can appear correctly in the viewer legend.
        /// </summary>
        private String GetFileName(String _path)
        {
            return Path.GetFileNameWithoutExtension(_path);
        }

        /// <summary>
        /// Convert pixel dimensions to physical (inch) dimensions and preserve the
        /// geographic aspect ratio.
        /// expWidth  = PixWidth / Ppi  (inches wide at the chosen DPI setting)
        /// expHeight = expWidth * (geographic height / geographic width)
        /// </summary>
        private void ValuesWH()
        {
            expWidth = PixWidth / Ppi;

            if ((FExtent.XMax - FExtent.XMin) != 0)
                expHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * expWidth;
            else
            {
                expWidth = 2;
                expHeight = 2;
            }
        }

        /// <summary>
        /// Convert physical (inch) dimensions to pixel dimensions and preserve the
        /// geographic aspect ratio.
        /// PixWidth  = expWidth * Ppi  (pixels wide given physical width and DPI)
        /// PixHeight = PixWidth * (geographic height / geographic width)
        /// </summary>
        private void ValueWHpix()
        {
            PixWidth = expWidth * Ppi;

            if ((FExtent.XMax - FExtent.XMin) != 0)
                PixHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth;
            else
            {
                PixWidth = 2;
                PixHeight = 2;
            }
        }
    }
}
