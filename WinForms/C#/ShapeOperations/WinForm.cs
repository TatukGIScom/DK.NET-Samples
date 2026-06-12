// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// ShapeOperations sample - Interactive geometry transformation of vector shapes.
//
// This sample demonstrates how to interactively apply affine geometric
// transformations to individual vector shapes using the TatukGIS DK .NET API:
//
//   Rotate:    Spins the selected shape around its centroid.  Horizontal mouse
//              movement maps to rotation angle (1 pixel = 1 degree).
//   Scale:     Grows or shrinks the shape by comparing the current mouse
//              position to the previous one (ratio-based per axis).
//   Move:      Translates the shape by the map-coordinate delta between
//              consecutive mouse-move events.
//
// The sample uses a two-layer approach:
//   1. The original shapefile layer holds the real data.
//   2. An in-memory TGIS_LayerVector (edtLayer) renders a live preview of the
//      shape being edited.  CachedPaint = false makes it a "tracking" layer so
//      InvalidateTopmost triggers an immediate repaint.
// On commit (second mouse-up), the edited geometry is written back to the
// source shape via CopyGeometry, and the preview layer is cleared.
//
// Data: Samples\3D\buildings.shp
// =============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;
using System.Diagnostics;

namespace ShapeOperations
{
    /// <summary>
    /// Main form for the ShapeOperations sample.
    /// Demonstrates interactive rotate, scale, and translate operations on
    /// individual vector shapes using TGIS_Shape.Transform with an affine matrix.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Label lbHint;                                        // Status/hint label
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;          // Map viewer control

        // In-memory overlay layer used as a live edit preview.
        // Shapes are added here during editing and discarded on commit/cancel.
        private TGIS_LayerVector edtLayer;

        // The original shape in the source layer, placed in editable state.
        // CopyGeometry writes the final geometry back to this object on commit.
        private TGIS_Shape currShape;

        // Working copy of currShape that lives in edtLayer and receives all
        // intermediate transform calls during the drag session.
        private TGIS_Shape edtShape;

        // Screen-pixel coordinates from the previous mouse event.
        // Used to derive the rotation angle (delta X) and scale ratios.
        private int prevX;
        private int prevY;

        // Map-coordinate position from the previous mouse event.
        // Used to compute the translation delta in map units.
        private TGIS_Point prevPtg;

        // Flag that enables transform dragging; toggled by the first mouse-up
        // (select) and cleared by the second mouse-up (commit).
        private Boolean handleMouseMove;

        private RadioButton rbRotate;  // Rotate mode selector
        private RadioButton rbScale;   // Scale mode selector
        private RadioButton rbMove;    // Move (translate) mode selector

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
            this.lbHint = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.rbRotate = new System.Windows.Forms.RadioButton();
            this.rbScale = new System.Windows.Forms.RadioButton();
            this.rbMove = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            //
            // lbHint
            //
            this.lbHint.AutoSize = true;
            this.lbHint.Location = new System.Drawing.Point(197, 9);
            this.lbHint.Name = "lbHint";
            this.lbHint.Size = new System.Drawing.Size(0, 13);
            this.lbHint.TabIndex = 3;
            //
            // GIS
            //
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(-2, 33);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(786, 527);
            this.GIS.TabIndex = 4;
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            this.GIS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseUp);
            //
            // btnRotate
            //
            this.rbRotate.AutoSize = true;
            this.rbRotate.Location = new System.Drawing.Point(12, 7);
            this.rbRotate.Name = "btnRotate";
            this.rbRotate.Size = new System.Drawing.Size(57, 17);
            this.rbRotate.TabIndex = 5;
            this.rbRotate.Text = "Rotate";
            this.rbRotate.UseVisualStyleBackColor = true;
            this.rbRotate.CheckedChanged += new System.EventHandler(this.btnRotate_CheckedChanged);
            //
            // btnScale
            //
            this.rbScale.AutoSize = true;
            this.rbScale.Location = new System.Drawing.Point(75, 7);
            this.rbScale.Name = "btnScale";
            this.rbScale.Size = new System.Drawing.Size(52, 17);
            this.rbScale.TabIndex = 6;
            this.rbScale.Text = "Scale";
            this.rbScale.UseVisualStyleBackColor = true;
            this.rbScale.CheckedChanged += new System.EventHandler(this.btnScale_CheckedChanged);
            //
            // btnMove
            //
            this.rbMove.AutoSize = true;
            this.rbMove.Location = new System.Drawing.Point(133, 7);
            this.rbMove.Name = "btnMove";
            this.rbMove.Size = new System.Drawing.Size(52, 17);
            this.rbMove.TabIndex = 7;
            this.rbMove.Text = "Move";
            this.rbMove.UseVisualStyleBackColor = true;
            this.rbMove.CheckedChanged += new System.EventHandler(this.btnMove_CheckedChanged);
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.rbMove);
            this.Controls.Add(this.rbScale);
            this.Controls.Add(this.rbRotate);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.lbHint);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - ShapeOperations";
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
        /// Initialises the sample on form load:
        /// opens the buildings shapefile, creates the in-memory preview layer,
        /// and zooms in to a useful starting extent.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            currShape = null;
            edtShape  = null;
            handleMouseMove = false;

            // Pre-select Rotate mode so the UI is in a known state
            rbRotate.PerformClick();

            GIS.Lock();  // Suspend repaints while configuring layers

            // GisSamplesDataDirDownload resolves the shared sample data folder
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\3D\buildings.shp");

            // Create the in-memory edit/preview overlay layer.
            // CachedPaint = false marks it as a "tracking" layer: the tile
            // cache is bypassed so InvalidateTopmost causes an immediate redraw.
            edtLayer = new TGIS_LayerVector();
            edtLayer.CS = GIS.CS;                                    // Match viewer CRS
            edtLayer.CachedPaint = false;                            // Tracking layer
            edtLayer.Params.Area.Pattern = TGIS_BrushStyle.Clear;   // Transparent fill
            edtLayer.Params.Area.OutlineColor = TGIS_Color.Red;     // Red outline

            GIS.Add( edtLayer );
            GIS.Unlock();

            // Zoom in 4x from the default extent so buildings are visible
            GIS.Zoom = GIS.Zoom * 4;
        }

        /// <summary>
        /// Core affine transform applied to a shape's geometry.
        ///
        /// Applies a 3x3 affine matrix centred on the shape's centroid so that
        /// rotate and scale operations appear to happen around the shape itself
        /// rather than around the coordinate origin.
        ///
        /// Matrix convention:
        ///   x' = x*xx + y*xy + dx
        ///   y' = x*yx + y*yy + dy
        ///   z' = z  (z is preserved unchanged)
        ///
        /// After updating the geometry, InvalidateTopmost redraws only the
        /// topmost (preview) layer, which avoids a costly full-map repaint.
        /// </summary>
        /// <param name="_shp">The shape to transform (lives in edtLayer).</param>
        /// <param name="_xx">Matrix element [0,0] - x-to-x factor.</param>
        /// <param name="_yx">Matrix element [1,0] - x-to-y factor.</param>
        /// <param name="_xy">Matrix element [0,1] - y-to-x factor.</param>
        /// <param name="_yy">Matrix element [1,1] - y-to-y factor.</param>
        /// <param name="_dx">Translation along X in map units.</param>
        /// <param name="_dy">Translation along Y in map units.</param>
        private void TransformSelectedShape(TGIS_Shape _shp, double _xx, double _yx,
                                            double _xy, double _yy, double _dx, double _dy)
        {
            TGIS_Point centroid;

            if (_shp == null) return;

            // Compute the geometric centroid so the pivot is at the shape centre
            centroid = _shp.Centroid();

            // x' = x*xx + y*xy + dx
            // y' = x*yx + y*yy + dy
            // z' = z
            _shp.Transform(TGIS_Utils.GisPoint3DFrom2D(centroid), // pivot
                             _xx, _yx, 0,   // row 0 of matrix
                             _xy, _yy, 0,   // row 1 of matrix
                              0,  0,  1,    // row 2 (z pass-through)
                             _dx, _dy, 0,   // translation vector
                             false          // do not recalc layer extent yet
                            );

            // Refresh only the preview layer for performance
            GIS.InvalidateTopmost();
        }

        /// <summary>
        /// Rotates the shape by <paramref name="_angle"/> radians around its centroid.
        /// Uses the standard 2-D rotation matrix:
        ///   | cos(a)  sin(a) |
        ///   |-sin(a)  cos(a) |
        /// </summary>
        private void RotateSelectedShape(TGIS_Shape _shp, double _angle)
        {
            TransformSelectedShape(
              _shp,
               Math.Cos(_angle), Math.Sin(_angle),   // x' row
              -Math.Sin(_angle), Math.Cos(_angle),   // y' row
                       0, 0                           // no additional translation
            );
        }

        /// <summary>
        /// Scales the shape by independent factors along each axis.
        /// Values greater than 1 enlarge; values between 0 and 1 shrink.
        /// The centroid is used as the pivot so the shape stays in place.
        /// </summary>
        private void ScaleSelectedShape(TGIS_Shape _shp, double _x, double _y)
        {
            TransformSelectedShape(
              _shp,
               _x, 0,   // x scale factor on diagonal
                0, _y,  // y scale factor on diagonal
                0, 0    // no translation
            );
        }

        /// <summary>
        /// Translates (moves) the shape by the given map-coordinate offset.
        /// The identity submatrix leaves all coordinates unchanged; only dx/dy move.
        /// </summary>
        private void TranslateSelectedShape(TGIS_Shape _shp, double _x, double _y)
        {
            TransformSelectedShape(
              _shp,
              1, 0,    // identity: x unchanged
              0, 1,    // identity: y unchanged
              _x, _y  // pure translation
            );
        }

        /// <summary>
        /// Responds to mouse movement over the viewer.
        /// While a shape is selected (handleMouseMove = true), dispatches
        /// incremental affine transforms to the preview shape in edtLayer.
        ///
        /// Rotate:    horizontal pixel delta -> radians (Math.PI/180 * deltaX).
        /// Scale:     current screen position / previous screen position per axis.
        /// Translate: map-coordinate delta since last event.
        /// </summary>
        private void GIS_MouseMove(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;

            if (edtShape == null) return;  // No shape selected; ignore

            if (handleMouseMove)
            {
                // Convert current screen position to map coordinates
                ptg = GIS.ScreenToMap(new Point(e.X, e.Y));

                if (rbRotate.Checked)
                    // 1 pixel of horizontal movement = 1 degree of rotation
                    RotateSelectedShape(edtShape, ((Math.PI / 180) * ((e.X - prevX))));
                // Rotate by moving the mouse horizontally
                else if (rbScale.Checked)
                {
                    // Guard against division-by-zero at the very first event
                    if ((prevX != 0) && (prevY != 0))
                        ScaleSelectedShape(edtShape, (double)e.X / prevX, (double)e.Y / prevY);
                }
                else if (rbMove.Checked)
                    // Delta in map units between this event and the last
                    TranslateSelectedShape(edtShape, (ptg.X - prevPtg.X), (ptg.Y - prevPtg.Y));

                // Update previous-position tracking for the next incremental step
                prevPtg.X = ptg.X;
                prevPtg.Y = ptg.Y;
                prevX = e.X;
                prevY = e.Y;
            }
        }

        /// <summary>
        /// Handles both the shape-selection click (first mouse-up) and the
        /// geometry-commit click (second mouse-up).
        ///
        /// First click (currShape == null):
        ///   - GIS.Locate finds the nearest shape within a 5-pixel tolerance.
        ///   - MakeEditable returns a writable proxy of the source shape.
        ///   - A copy is added to the preview layer so edits are visible without
        ///     touching the original layer data.
        ///   - handleMouseMove is toggled to start processing transform events.
        ///
        /// Second click (currShape != null):
        ///   - CopyGeometry writes the final preview geometry back to the source
        ///     shape, making the change permanent in the layer.
        ///   - RevertAll clears the preview layer.
        ///   - InvalidateWholeMap forces a full repaint showing the committed result.
        /// </summary>
        private void GIS_MouseUp(object sender, MouseEventArgs e)
        {
            TGIS_Shape shp;
            TGIS_Point ptg;

            lbHint.Text = "No selected shape. Select shape";

            // --- COMMIT path ---
            if (currShape != null)
            {
                // Write the transformed preview geometry back to the real layer
                currShape.CopyGeometry( edtShape );

                // Discard all shapes in the preview layer
                edtLayer.RevertAll();

                currShape = null;
                edtShape  = null;

                // Full repaint to show the committed shape in its real layer style
                GIS.InvalidateWholeMap();
                handleMouseMove = false;
                return;
            }

            // --- SELECT path ---
            if (GIS.IsEmpty) return;
            if (GIS.IsEmpty) return;  // guard repeated intentionally in original
            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            // Convert screen click to map coordinates
            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));

            // Locate the nearest shape; tolerance = 5 screen pixels in map units
            shp = (TGIS_Shape)(GIS.Locate(ptg, 5 / GIS.Zoom));
            if (shp == null) return;

            // MakeEditable returns a proxy that allows geometry modification
            currShape = shp.MakeEditable();

            // Place a copy of the shape into the overlay layer for live editing
            edtShape = edtLayer.AddShape(currShape.CreateCopy());

            lbHint.Text = "Selected shape : " + currShape.Uid + ". Click to commit changes";

            // Seed previous-position state for the first incremental delta
            prevPtg.X = ptg.X;
            prevPtg.Y = ptg.Y;
            prevX = e.X;
            prevY = e.Y;

            // Toggle: start accepting mouse-move transforms
            handleMouseMove = !handleMouseMove;
        }

        /// <summary>
        /// Switches to Rotate mode. If a shape is currently being edited,
        /// cancels the edit session so the user starts fresh in the new mode.
        /// </summary>
        private void btnRotate_CheckedChanged(object sender, EventArgs e)
        {
            lbHint.Text = "Select shape to start rotating";
            if (currShape != null)
            {
                GIS.InvalidateTopmost();
                currShape = null;
                handleMouseMove = false;
                return;
            }
        }

        /// <summary>
        /// Switches to Scale mode. Cancels any in-progress edit session.
        /// </summary>
        private void btnScale_CheckedChanged(object sender, EventArgs e)
        {
            lbHint.Text = "Select shape to start scaling";
            if (currShape != null)
            {
                GIS.InvalidateTopmost();
                currShape = null;
                handleMouseMove = false;
                return;
            }
        }

        /// <summary>
        /// Switches to Move (translate) mode. Cancels any in-progress edit session.
        /// </summary>
        private void btnMove_CheckedChanged(object sender, EventArgs e)
        {
            lbHint.Text = "Select shape to start moving";
            if (currShape != null)
            {
                GIS.InvalidateTopmost();
                currShape = null;
                handleMouseMove = false;
                return;
            }
        }
    }
}
