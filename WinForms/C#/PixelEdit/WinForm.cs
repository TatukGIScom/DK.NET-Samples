// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// PixelEdit - demonstrates how to read and modify raster (pixel) layer data
// programmatically using TatukGIS.
//
// Five operations are shown, each triggered by a dedicated button:
//
//  1. Terrain profile (btnProfile)
//     Iterates over every pixel that lies under a polyline shape, reading the
//     grid elevation value (px.Value) and the cumulative distance along the
//     line (px.Distance).  Results are printed to the Memo text box.
//     API: TGIS_LayerPixel.Loop(distanceStep, shape, editable)
//
//  2. Grid min/max (btnMinMax)
//     Finds the pixels with the lowest and highest elevation values inside a
//     polygon area.  A temporary vector layer with cross markers is added at
//     the two extreme locations so they are visible in the viewer.
//     API: TGIS_LayerPixel.Loop(extent, step, shape, mode, editable)
//
//  3. Bitmap average colour (btnAverageColor)
//     Reads every pixel of a satellite image that falls inside a country
//     polygon (Spain, shape #679) to compute the average R, G, B colour.
//     It then writes that flat colour back into every pixel of the same
//     region, demonstrating read-then-write iteration.
//     API: TGIS_PixelItem.Color
//
//  4. Create new JPG (btnCreateBitmap)
//     Creates a brand-new JPEG raster layer spanning the entire world in
//     WGS-84 (EPSG:4326), then uses LockPixels/UnlockPixels to paint a
//     square region red by writing directly into the raw bitmap buffer.
//     API: TGIS_LayerJPG.Build(), TGIS_LayerPixel.LockPixels(),
//          TGIS_LayerPixelLock.Bitmap[], TGIS_LayerPixelLock.BitmapPos()
//
//  5. Create new GRD (btnCreateGrid)
//     Creates a brand-new ESRI Grid raster layer, then fills a region with
//     random elevation values using the low-level Grid[][] accessor.
//     API: TGIS_LayerGRD.Build(), TGIS_LayerPixelLock.Grid[][]
//
// Key TatukGIS API concepts shown here:
//   - TGIS_LayerPixel      : base class for all raster layers
//   - TGIS_LayerJPG        : JPEG raster layer subclass
//   - TGIS_LayerGRD        : ESRI Grid raster layer subclass
//   - TGIS_PixelItem       : represents a single raster cell during iteration;
//                            exposes Value (float), Color (TGIS_Color),
//                            Center (geographic point), Distance (profile)
//   - Loop()               : enumerator-based iteration over raster cells
//                            (read-only or editable depending on last parameter)
//   - LockPixels()         : locks a rectangular region for direct raw-buffer
//                            access; must be paired with UnlockPixels()
//   - TGIS_LayerPixelLock  : holds the locked pixel data and its pixel bounds
//   - TGIS_CSFactory.ByEPSG: creates a coordinate system from an EPSG code
//   - GIS.Lock/Unlock       : suspends/resumes map repainting during batch updates
// =============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace PixelEdit
{
    /// <summary>
    /// Main form for the PixelEdit sample application.
    /// Provides five buttons that each demonstrate a different aspect of
    /// reading and writing raster pixel data through the TatukGIS API.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;   // TatukGIS map viewer
        private Button btnProfile;        // Reads elevation profile along a polyline
        private Button btnMinMax;         // Finds min/max elevation inside a polygon
        private Button btnAverageColor;   // Computes and applies average colour in a region
        private Button btnCreateBitmap;   // Creates and fills a new JPEG raster layer
        private Button btnCreateGrid;     // Creates and fills a new ESRI Grid layer
        private TGIS_ControlLegend GIS_Legend;  // Layer legend panel docked to the left
        private TextBox Memo;             // Output text area for results
        private System.Windows.Forms.Panel panel1;

        /// <summary>
        /// Initialises the form and its designer-generated controls.
        /// </summary>
        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
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
            this.btnCreateGrid = new System.Windows.Forms.Button();
            this.btnCreateBitmap = new System.Windows.Forms.Button();
            this.btnAverageColor = new System.Windows.Forms.Button();
            this.btnMinMax = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.Memo = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.Controls.Add(this.btnCreateGrid);
            this.panel1.Controls.Add(this.btnCreateBitmap);
            this.panel1.Controls.Add(this.btnAverageColor);
            this.panel1.Controls.Add(this.btnMinMax);
            this.panel1.Controls.Add(this.btnProfile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 24);
            this.panel1.TabIndex = 0;
            //
            // btnCreateGrid
            //
            this.btnCreateGrid.Location = new System.Drawing.Point(480, 0);
            this.btnCreateGrid.Name = "btnCreateGrid";
            this.btnCreateGrid.Size = new System.Drawing.Size(120, 22);
            this.btnCreateGrid.TabIndex = 10;
            this.btnCreateGrid.Text = "Create new GRD";
            this.btnCreateGrid.UseVisualStyleBackColor = true;
            this.btnCreateGrid.Click += new System.EventHandler(this.btnCreateGrid_Click);
            //
            // btnCreateBitmap
            //
            this.btnCreateBitmap.Location = new System.Drawing.Point(360, 0);
            this.btnCreateBitmap.Name = "btnCreateBitmap";
            this.btnCreateBitmap.Size = new System.Drawing.Size(120, 22);
            this.btnCreateBitmap.TabIndex = 9;
            this.btnCreateBitmap.Text = "Create new JPG";
            this.btnCreateBitmap.UseVisualStyleBackColor = true;
            this.btnCreateBitmap.Click += new System.EventHandler(this.btnCreateBitmap_Click);
            //
            // btnAverageColor
            //
            this.btnAverageColor.Location = new System.Drawing.Point(240, 0);
            this.btnAverageColor.Name = "btnAverageColor";
            this.btnAverageColor.Size = new System.Drawing.Size(120, 22);
            this.btnAverageColor.TabIndex = 8;
            this.btnAverageColor.Text = "Bitmap average color";
            this.btnAverageColor.UseVisualStyleBackColor = true;
            this.btnAverageColor.Click += new System.EventHandler(this.btnAverageColor_Click);
            //
            // btnMinMax
            //
            this.btnMinMax.Location = new System.Drawing.Point(120, 0);
            this.btnMinMax.Name = "btnMinMax";
            this.btnMinMax.Size = new System.Drawing.Size(120, 22);
            this.btnMinMax.TabIndex = 7;
            this.btnMinMax.Text = "Grid Min/Max";
            this.btnMinMax.UseVisualStyleBackColor = true;
            this.btnMinMax.Click += new System.EventHandler(this.btnMinMax_Click);
            //
            // btnProfile
            //
            this.btnProfile.Location = new System.Drawing.Point(0, 0);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(120, 22);
            this.btnProfile.TabIndex = 6;
            this.btnProfile.Text = "Terrain profile";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            //
            // GIS
            //
            this.GIS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(169, 24);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(466, 352);
            this.GIS.TabIndex = 3;
            //
            // GIS_Legend
            //
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_Legend.Dock = System.Windows.Forms.DockStyle.Left;
            this.GIS_Legend.GIS_Group = null;
            this.GIS_Legend.GIS_Layer = null;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(0, 24);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GIS_Legend.ReverseOrder = false;
            this.GIS_Legend.Size = new System.Drawing.Size(169, 352);
            this.GIS_Legend.TabIndex = 2;
            //
            // Memo
            //
            this.Memo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Memo.Location = new System.Drawing.Point(0, 376);
            this.Memo.Multiline = true;
            this.Memo.Name = "Memo";
            this.Memo.ReadOnly = true;
            this.Memo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Memo.Size = new System.Drawing.Size(635, 89);
            this.Memo.TabIndex = 1;
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(635, 465);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GIS_Legend);
            this.Controls.Add(this.Memo);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - PixelEdit";
            this.panel1.ResumeLayout(false);
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
        /// Demonstrates reading a terrain elevation profile along a polyline.
        /// <para>
        /// Opens the grid project, retrieves the elevation grid layer and the
        /// polyline shape from the "line" layer, then calls
        /// <c>Loop(distanceStep, shape, editable)</c> to iterate over every
        /// raster cell that the line crosses.  For each cell, <c>px.Distance</c>
        /// gives the cumulative distance along the line and <c>px.Value</c> gives
        /// the elevation.  Results are appended to the Memo text box.
        /// </para>
        /// <para>
        /// Passing <c>false</c> for the editable parameter means values can be
        /// read but not written back; the profile loop is purely read-only.
        /// </para>
        /// </summary>
        private void btnProfile_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            TGIS_LayerVector lv;
            TGIS_Shape shp;

            Memo.Clear();

            // Open the project that contains the DEM grid and the profile line
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\PixelEdit\grid.ttkproject");

            // Retrieve layers by name from the loaded project
            lp = (TGIS_LayerPixel)GIS.Get("elevation");
            lv = (TGIS_LayerVector)GIS.Get("line");

            // MakeEditable() returns a writable copy so we can flag it as selected
            shp = lv.GetShape(1).MakeEditable();
            shp.IsSelected = true;  // Highlight the profile line in the viewer

            // Loop(step=0, shape, editable=false): step=0 visits every raster cell
            // the line crosses (no sub-pixel sampling).  editable=false = read-only.
            foreach (TatukGIS.NDK.TGIS_PixelItem px in lp.Loop(0, shp, false))
            {
                Memo.AppendText(String.Format("Distance: {0}, Height:{1}\r\n", px.Distance, px.Value));
            }
        }

        /// <summary>
        /// Demonstrates finding the minimum and maximum elevation values inside
        /// a polygon area and marking those locations on the map.
        /// <para>
        /// Opens the grid project, retrieves the elevation layer and a polygon
        /// shape, then iterates all cells within the polygon using
        /// <c>Loop(extent, step, shape, "T", editable)</c>.  The "T" flag causes
        /// the iteration to be clipped to the shape boundary.  After finding the
        /// extreme values, a temporary vector layer with cross markers is added at
        /// the min/max geographic locations.
        /// </para>
        /// </summary>
        private void btnMinMax_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            TGIS_LayerVector lv;
            TGIS_LayerVector ltmp;
            TGIS_Shape shp;
            TGIS_Shape shptmp;
            Double dmin, dmax;
            TGIS_Point ptmin, ptmax;

            Memo.Clear();

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\PixelEdit\grid.ttkproject");

            lp = (TGIS_LayerPixel)GIS.Get("elevation");
            lv = (TGIS_LayerVector)GIS.Get("polygon");
            shp = lv.GetShape(1).MakeEditable();
            shp.IsSelected = true;  // Highlight the polygon to show the search area

            // Initialise sentinels to the extreme values so the first real pixel wins
            dmax = -1e38;
            dmin = 1e38;
            ptmin = new TGIS_Point(0, 0);
            ptmax = new TGIS_Point(0, 0);

            // Loop over pixels clipped to the polygon ("T" = use shape as mask)
            foreach (TGIS_PixelItem px in lp.Loop(shp.Extent, 0, shp, "T", false))
            {
                if (px.Value < dmin)
                {
                    dmin = px.Value;
                    ptmin = px.Center;  // Geographic centre of this raster cell
                }

                if (px.Value > dmax)
                {
                    dmax = px.Value;
                    ptmax = px.Center;
                }
            }

            // Create a temporary in-memory vector layer in the same CRS to hold markers
            ltmp = new TGIS_LayerVector();
            ltmp.CS = lp.CS;
            GIS.Add(ltmp);

            // Style the marker layer: cross symbols sized in screen pixels (negative = screen units)
            ltmp.Params.Marker.Style = TGIS_MarkerStyle.Cross;
            ltmp.Params.Marker.Size = -10;  // Negative size = screen pixels, not map units
            ltmp.Params.Marker.Color = TGIS_Color.Black;

            // Add a point shape at the minimum elevation location
            shptmp = ltmp.CreateShape(TGIS_ShapeType.Point);
            shptmp.AddPart();
            shptmp.AddPoint(ptmin);

            // Add a point shape at the maximum elevation location
            shptmp = ltmp.CreateShape(TGIS_ShapeType.Point);
            shptmp.AddPart();
            shptmp.AddPoint(ptmax);

            GIS.InvalidateWholeMap();  // Force a full repaint to show the markers

            Memo.AppendText(String.Format("Min: {0}, Location: {1} {2}\r\n", dmin, ptmin.X, ptmin.Y));
            Memo.AppendText(String.Format("Max: {0}, Location: {1} {2}\r\n", dmax, ptmax.X, ptmax.Y));
        }

        /// <summary>
        /// Demonstrates reading pixel colour values from a bitmap raster layer
        /// and writing a computed average colour back.
        /// <para>
        /// Opens the Blue Marble satellite project, then uses two passes over the
        /// pixels inside Spain (shape #679):
        /// <list type="number">
        ///   <item>Read pass (editable=false): accumulates the R, G, B channel
        ///   values of every pixel inside the country polygon.</item>
        ///   <item>Write pass (editable=true): replaces every pixel inside Spain
        ///   with the computed average colour, demonstrating in-place modification.</item>
        /// </list>
        /// </para>
        /// <para>
        /// The "T" flag in the Loop call causes iteration to be clipped to the
        /// exact polygon boundary rather than its bounding box.
        /// </para>
        /// </summary>
        private void btnAverageColor_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            TGIS_LayerVector lv;
            TGIS_Shape shp;
            double r, g, b;
            byte rr, gg, bb;
            int cnt;
            TGIS_Color cl;

            Memo.Clear();

            // Open the Blue Marble satellite imagery project
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\PixelEdit\bitmap.ttkproject");

            // Obtain references to the raster layer and the countries vector layer
            lp = (TGIS_LayerPixel)GIS.Get("bluemarble");
            lv = (TGIS_LayerVector)GIS.Get("countries");

            // Shape 679 is Spain; MakeEditable() returns a writable copy
            shp = lv.GetShape(679).MakeEditable();

            // Pan and zoom the viewer to Spain (Lock/Unlock suppresses intermediate repaints)
            GIS.Lock();
            GIS.VisibleExtent = shp.ProjectedExtent;
            GIS.Zoom = GIS.Zoom / 2.0;  // Zoom out slightly to show surrounding context
            GIS.Unlock();

            cnt = 0;
            r = 0;
            g = 0;
            b = 0;

            // First pass: iterate pixels inside Spain, accumulating colour channel sums
            // "T" = clip iteration to the shape boundary
            foreach (TGIS_PixelItem px in lp.Loop(shp.Extent, 0, shp, "T", false))
            {
                r = r + px.Color.R;  // Red channel (0-255)
                g = g + px.Color.G;  // Green channel (0-255)
                b = b + px.Color.B;  // Blue channel (0-255)
                cnt++;
            }

            if (cnt > 0)
            {
                // Compute per-channel average and build the replacement colour
                rr = Convert.ToByte(Math.Truncate(r / cnt));
                gg = Convert.ToByte(Math.Truncate(g / cnt));
                bb = Convert.ToByte(Math.Truncate(b / cnt));
                cl = TGIS_Color.FromRGB(rr, gg, bb);

                // Second pass: overwrite every pixel inside Spain with the average colour.
                // editable=true enables writing; assigning px.Color flushes the change.
                foreach (TGIS_PixelItem px in lp.Loop(shp.Extent, 0, shp, "T", true))
                {
                    px.Color = cl;
                }
            }

            GIS.InvalidateWholeMap();  // Repaint to show the modified pixels
        }

        /// <summary>
        /// Demonstrates creating a new JPEG raster layer from scratch and writing
        /// individual pixel values using the low-level <c>LockPixels</c> interface.
        /// <para>
        /// <list type="number">
        ///   <item><c>Build()</c> defines the file path, coordinate system
        ///   (WGS-84 = EPSG:4326), geographic extent, and pixel dimensions.</item>
        ///   <item><c>LockPixels()</c> locks a sub-region for direct buffer access.
        ///   The returned <c>TGIS_LayerPixelLock</c> exposes <c>Bitmap[]</c> (flat
        ///   ARGB integer array) and <c>BitmapPos(x,y)</c> to compute the offset.</item>
        ///   <item>The locked region is painted solid red, then <c>UnlockPixels()</c>
        ///   flushes the writes and releases the lock.</item>
        ///   <item><c>SaveData()</c> persists the result to the JPEG file on disk.</item>
        /// </list>
        /// </para>
        /// </summary>
        private void btnCreateBitmap_Click(object sender, EventArgs e)
        {
            TGIS_LayerJPG lp;
            TGIS_LayerPixelLock lck;
            int x, y;

            Memo.Clear();

            lp = new TGIS_LayerJPG();
            try
            {
                // Build() creates the JPEG file and sets up the georeferencing.
                // EPSG:4326 = WGS-84 geographic CRS; extent covers the whole world.
                lp.Build("test.jpg", TGIS_CSFactory.ByEPSG(4326),
                         TGIS_Utils.GisExtent(-180, -90, 180, 90), 360, 180
                        );

                // Lock a 90x90-degree sub-region centred on the equator for direct access.
                // true = request write (editable) access.
                lck = lp.LockPixels(TGIS_Utils.GisExtent(-45, -45, 45, 45), lp.CS, true);
                try
                {
                    // Iterate over every pixel column (x) and row (y) within the locked bounds.
                    // BitmapPos(x,y) maps 2-D coordinates to the flat Bitmap[] array offset.
                    // TGIS_Color.Red.ARGB is the 32-bit ARGB integer for opaque red.
                    for (x = lck.Bounds.Left; x <= lck.Bounds.Right; x++)
                    {
                        for (y = lck.Bounds.Top; y <= lck.Bounds.Bottom; y++)
                        {
                            lck.Bitmap[lck.BitmapPos(x, y)] = (int)TGIS_Color.Red.ARGB;
                        }
                    }
                }
                finally
                {
                    // UnlockPixels() flushes buffered writes back into the layer and
                    // releases the lock.  Always call this in a finally block.
                    lp.UnlockPixels(ref lck);
                }

                lp.SaveData();  // Persist the modified pixels to the JPEG file on disk
            }
            finally
            {
                lp.Dispose();  // Release the layer object
            }

            GIS.Open("test.jpg");  // Display the newly created image in the viewer
        }

        /// <summary>
        /// Demonstrates creating a new ESRI Grid raster layer from scratch and
        /// writing floating-point elevation values using the low-level
        /// <c>LockPixels</c> interface.
        /// <para>
        /// The approach is identical to <see cref="btnCreateBitmap_Click"/> but
        /// uses a TGIS_LayerGRD (continuous floating-point values) and the
        /// <c>Grid[row][col]</c> accessor instead of the <c>Bitmap[]</c> flat
        /// ARGB array.  Note that the Grid accessor uses row-major order:
        /// <c>Grid[y][x]</c>.
        /// </para>
        /// </summary>
        private void btnCreateGrid_Click(object sender, EventArgs e)
        {
            TGIS_LayerGRD lp;
            TGIS_LayerPixelLock lck;
            int x, y;
            Random rnd;

            Memo.Clear();

            lp = new TGIS_LayerGRD();
            try
            {
                // Build() creates the GRD file with the given CRS, extent, and dimensions.
                lp.Build("test.grd", TGIS_CSFactory.ByEPSG(4326),
                         TGIS_Utils.GisExtent(-180, -90, 180, 90), 360, 180
                        );

                // Lock the central sub-region for write access
                rnd = new Random();
                lck = lp.LockPixels(TGIS_Utils.GisExtent(-45, -45, 45, 45), lp.CS, true);
                try
                {
                    for (x = lck.Bounds.Left; x <= lck.Bounds.Right; x++)
                    {
                        for (y = lck.Bounds.Top; y <= lck.Bounds.Bottom; y++)
                        {
                            // Grid[y][x] stores a floating-point elevation value.
                            // Row-major storage: the first index is the row (y), second is the column (x).
                            lck.Grid[y][x] = rnd.Next(100);
                        }
                    }
                }
                finally
                {
                    lp.UnlockPixels(ref lck);  // Flush writes and release the lock
                }

                lp.SaveData();  // Write the grid data to disk
            }
            finally
            {
                lp.Dispose();
            };

            GIS.Open("test.grd");  // Display the newly created grid in the viewer
        }
    }
}
