// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// Transform sample - Polynomial georeferencing of a raster image.
// C# / .NET WinForms edition.
//
// This sample demonstrates how to georeference (rectify) an unregistered
// raster image using the TatukGIS DK polynomial transform API:
//
//   TGIS_TransformPolynomial
//     Maps pixel (source) coordinates to real-world (target) coordinates using
//     ground-control points (GCPs) and a fitted polynomial.  The polynomial
//     order determines warp complexity:
//       First (affine):    handles translation, rotation, scale, shear.
//                          Requires at least 3 non-collinear GCPs.
//       Second (quadratic): corrects gentle curvature.  Requires >= 6 GCPs.
//       Third (cubic):      corrects stronger distortions.  Requires >= 10 GCPs.
//
//   TGIS_LayerPixel.Transform
//     Attaching a transform to a raster layer causes the DK to warp the image
//     on-the-fly when rendered, without modifying the source file.
//
//   TGIS_TransformPolynomial.CuttingPolygon
//     An optional WKT polygon (in source/pixel coordinates) that masks the
//     visible area of the raster to a region of interest.
//
// Workflow:
//   btnTransform  - Define 4 corner GCPs, fit first-order polynomial,
//                   assign CRS (EPSG 102748), and display the result.
//   btnCutting    - Same GCPs but with a CuttingPolygon masking the image.
//   btnSave       - Save the current transform to a ".trn" sidecar file.
//   btnRead       - Reload a previously saved transform sidecar.
//
// Data: Samples\Rectify\satellite.jpg  (an unrectified aerial/satellite image)
// =============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace Transform
{
    /// <summary>
    /// Main form for the Transform sample.
    /// Demonstrates polynomial georeferencing of a raster image using
    /// TGIS_TransformPolynomial and TGIS_LayerPixel.Transform.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Button btnTransform;  // Apply 4-GCP first-order polynomial transform
        private Button btnCutting;    // Apply transform with CuttingPolygon mask
        private Button btnSave;       // Save transform to .trn sidecar file
        private Button btnRead;       // Load transform from .trn sidecar file
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;  // Map viewer control

        // Extension for transform sidecar files.
        // The DK convention is "<image_path>.trn" to store polynomial GCP data.
        private String GIS_TRN_EXT = ".trn";

        private Label lbCoords;  // Status label showing cursor map coordinates

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
            this.btnTransform = new System.Windows.Forms.Button();
            this.btnCutting = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.lbCoords = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // btnTransform
            //
            this.btnTransform.Location = new System.Drawing.Point(15, 15);
            this.btnTransform.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTransform.Name = "btnTransform";
            this.btnTransform.Size = new System.Drawing.Size(128, 29);
            this.btnTransform.TabIndex = 0;
            this.btnTransform.Text = "Transform";
            this.btnTransform.UseVisualStyleBackColor = true;
            this.btnTransform.Click += new System.EventHandler(this.btnTransform_Click);
            //
            // btnCutting
            //
            this.btnCutting.Location = new System.Drawing.Point(16, 52);
            this.btnCutting.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCutting.Name = "btnCutting";
            this.btnCutting.Size = new System.Drawing.Size(126, 29);
            this.btnCutting.TabIndex = 1;
            this.btnCutting.Text = "Cutting polygon";
            this.btnCutting.UseVisualStyleBackColor = true;
            this.btnCutting.Click += new System.EventHandler(this.btnCutting_Click);
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(16, 90);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(126, 29);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save to file";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnRead
            //
            this.btnRead.Location = new System.Drawing.Point(16, 128);
            this.btnRead.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(126, 29);
            this.btnRead.TabIndex = 3;
            this.btnRead.Text = "Read from file";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            //
            // GIS
            //
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(150, -1);
            this.GIS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(582, 674);
            this.GIS.TabIndex = 4;
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            //
            // lbCoords
            //
            this.lbCoords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbCoords.AutoSize = true;
            this.lbCoords.Location = new System.Drawing.Point(154, 678);
            this.lbCoords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCoords.Name = "lbCoords";
            this.lbCoords.Size = new System.Drawing.Size(0, 17);
            this.lbCoords.TabIndex = 5;
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(730, 701);
            this.Controls.Add(this.lbCoords);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCutting);
            this.Controls.Add(this.btnTransform);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Transform";
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
        /// Opens the unrectified satellite image on form load.
        /// No transform is applied at startup; the user clicks buttons to georeference.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            // GisSamplesDataDirDownload resolves the shared sample data folder
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\Rectify\satellite.jpg");
        }

        /// <summary>
        /// Applies a first-order polynomial georeference to the satellite image.
        ///
        /// Workflow:
        ///   1. Creates TGIS_TransformPolynomial and adds four corner GCPs.
        ///   2. Fits a first-order (affine) polynomial to the GCPs via Prepare().
        ///   3. Assigns the transform to the raster layer and activates warping.
        ///   4. Declares the CRS via SetCSByEPSG (EPSG 102748 =
        ///      NAD83 / Washington South State Plane in US survey feet).
        ///   5. Recomputes the layer extent and zooms to full extent.
        ///
        /// After this step the image is correctly georeferenced and can be
        /// overlaid with other layers sharing the same CRS.
        /// </summary>
        private void btnTransform_Click(object sender, EventArgs e)
        {
            TGIS_TransformPolynomial trn;
            TGIS_LayerPixel lp;

            // Access the first (and only) layer, which is the satellite image
            lp = (TGIS_LayerPixel)GIS.Items[0];

            trn = new TGIS_TransformPolynomial();

            // Add four corner ground-control points (GCPs).
            // Each GCP maps a source pixel coordinate to a target real-world coordinate.
            // Source: image pixel convention (Y decreases downward, so negative Y for bottom).
            // Target: State Plane CRS (EPSG 102748), units in feet.
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, -944.5),
                          TGIS_Utils.GisPoint(1273285.84090909, 239703.615056818),
                          0,    // GCP index
                          true  // enabled
                         );
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, 0.5),
                          TGIS_Utils.GisPoint(1273285.84090909, 244759.524147727),
                          1,
                          true
                         );
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, 0.5),
                          TGIS_Utils.GisPoint(1279722.65909091, 245859.524147727),
                          2,
                          true
                         );
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, -944.5),
                          TGIS_Utils.GisPoint(1279744.93181818, 239725.887784091),
                          3,
                          true
                         );

            // Fit the polynomial (First order = affine: translation, rotation, scale, shear)
            trn.Prepare(TGIS_PolynomialOrder.First);

            // Assign the transform to the layer and activate on-the-fly warping
            lp.Transform = trn;
            lp.Transform.Active = true;

            // Declare the CRS so the viewer knows the real-world coordinate space
            lp.SetCSByEPSG(102748);

            // Recompute extent in the new CRS and zoom to show the whole image
            GIS.RecalcExtent();
            GIS.FullExtent();
        }

        /// <summary>
        /// Applies a first-order polynomial transform with a CuttingPolygon mask.
        ///
        /// Identical to btnTransform_Click but adds a CuttingPolygon in pixel
        /// (source) coordinates.  Only the area inside the polygon is rendered
        /// after warping; the rest of the image is clipped out.  This is useful
        /// when only a sub-region of the scan is geometrically accurate or of
        /// interest.
        ///
        /// Note: the CRS is not set here so the result is displayed in pixel
        /// space; combine with SetCSByEPSG if CRS registration is also required.
        /// </summary>
        private void btnCutting_Click(object sender, EventArgs e)
        {
            TGIS_TransformPolynomial trn;
            TGIS_LayerPixel lp;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            trn = new TGIS_TransformPolynomial();

            // Four corner GCPs (same pixel-to-world mapping as btnTransform_Click)
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, -944.5),
              TGIS_Utils.GisPoint(1273285.84090909, 239703.615056818),
              0,
              true
             );
            trn.AddPoint(TGIS_Utils.GisPoint(-0.5, 0.5),
                          TGIS_Utils.GisPoint(1273285.84090909, 244759.524147727),
                          1,
                          true
                         );
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, 0.5),
                          TGIS_Utils.GisPoint(1279722.65909091, 244759.524147727),
                          2,
                          true
                         );
            trn.AddPoint(TGIS_Utils.GisPoint(1246.5, -944.5),
                          TGIS_Utils.GisPoint(1279744.93181818, 239725.887784091),
                          3,
                          true
                         );

            // WKT polygon in SOURCE (pixel) coordinates that masks the visible region.
            // Pixels outside this polygon are not rendered after the warp is applied.
            trn.CuttingPolygon = "POLYGON((421.508902077151 -320.017804154303," +
                                 "518.161721068249 -223.364985163205," +
                                 "688.725519287834 -210.572700296736," +
                                 "864.974777448071 -254.635014836795," +
                                 "896.244807121662 -335.652818991098," +
                                 "894.823442136499 -453.626112759644," +
                                 "823.755192878338 -615.661721068249," +
                                 "516.740356083086 -607.13353115727," +
                                 "371.761127596439 -533.222551928783," +
                                 "340.491097922849 -456.46884272997," +
                                 "421.508902077151 -320.017804154303))";

            trn.Prepare(TGIS_PolynomialOrder.First);
            lp.Transform = trn;
            lp.Transform.Active = true;

            GIS.RecalcExtent();
            GIS.FullExtent();
        }

        /// <summary>
        /// Saves the current polynomial transform to a ".trn" sidecar file.
        ///
        /// The sidecar file stores all GCPs and polynomial coefficients so the
        /// georeferencing solution can be reloaded later without re-entering data.
        /// The file is named "satellite.jpg.trn" by convention.
        ///
        /// This is a no-op if no transform has been assigned to the layer yet.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            // Only save if a transform has been assigned
            if (lp.Transform != null)
                lp.Transform.SaveToFile("satellite.jpg" + GIS_TRN_EXT);
        }

        /// <summary>
        /// Loads a polynomial transform from a ".trn" sidecar file and applies it.
        ///
        /// Creates a new TGIS_TransformPolynomial, populates it from the sidecar,
        /// assigns it to the raster layer, activates warping, and zooms to fit.
        /// </summary>
        private void btnRead_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            TGIS_TransformPolynomial trn;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            // Create a transform and load all GCPs + coefficients from the sidecar
            trn = new TGIS_TransformPolynomial();
            trn.LoadFromFile("satellite.jpg" + GIS_TRN_EXT);

            // Assign to the layer and activate on-the-fly warping
            lp.Transform = trn;
            lp.Transform.Active = true;

            GIS.RecalcExtent();
            GIS.FullExtent();
        }

        /// <summary>
        /// Converts the cursor screen position to map coordinates and displays
        /// them in the status label.  Gives the user real-time coordinate feedback
        /// as they move the mouse over the georeferenced image.
        /// </summary>
        private void GIS_MouseMove(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;

            if (GIS.IsEmpty) return;

            // Convert screen pixel to map coordinate using the current view transform
            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));

            // Format to 4 decimal places for readability
            lbCoords.Text = String.Format("X: {0:0.0000} | Y: {1:0.0000}", ptg.X, ptg.Y);
        }
    }
}
