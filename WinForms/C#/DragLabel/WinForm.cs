/* DragLabel sample — demonstrates interactive draggable feature labels on maps (C# .NET).

   What the sample shows:
     - Two parallel in-memory vector layers: realpoints (features) and sidekicks (labels)
     - Realpoints layer with visible CGM ship symbols rendered as point markers
     - Storing ship name attributes for use as label text
     - Disabling cached painting to ensure real-time shape repositioning
     - Sidekicks layer with invisible ghost points carrying floating labels
     - UID matching between realpoints and sidekicks for efficient lookups
     - Custom label rendering via PaintShapeLabelEvent callback
     - Drawing connecting leader line from real position to label position
     - Manual editor control for drag-to-reposition interaction
     - Converting screen click positions to map coordinates with ScreenToMap
     - Setting sidekick shape position during drag operation
     - Programmatic movement loop demonstrating simultaneous realpoint/sidekick translation

   Key TatukGIS API concepts shown here:
     TGIS_ViewerWnd                      - main visual map control
     TGIS_LayerVector                    - in-memory vector layer for features
     TGIS_Shape                          - individual geographic feature
     PaintShapeLabelEvent (callback)     - per-shape label rendering hook
     TGIS_RendererAbstract               - platform-agnostic drawing interface
     GIS.Locate()                        - hit-test to find shape at point
     GIS.MapToScreen()                   - convert geographic coords to screen pixels
     GIS.ScreenToMap()                   - convert screen pixels to geographic coordinates
     GIS.Editor.MouseBegin()             - start manual shape editing/dragging
     TGIS_Shape.SetPosition()            - reposition shape in geographic space
     CanvasDrawLine()                    - draw connecting leader line
     DrawLabel()                         - render text label at shape position
*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace DragLabel
{
    /// <summary>
    /// Main application form for the DragLabel sample.
    /// Hosts a TGIS_ViewerWnd and manages two in-memory vector layers to
    /// demonstrate interactive dragging of floating feature labels connected
    /// to their source geometry by dynamically drawn leader lines.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.StatusStrip stripBar1;      // status bar
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;        // GIS viewer
        private const string LABEL_TEXT = "Ship ";
        private ToolStripButton toolStripButton1; // "Animate" toolbar button
        private ToolStrip toolStrip1;

        /// <summary>
        /// The sidekick shape currently being dragged by the user.
        /// Null when no drag operation is in progress.
        /// </summary>
        private TGIS_Shape currShape;


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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SuspendLayout();
            //
            // stripBar1
            //
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 0;
            //
            // GIS
            //
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 418);
            this.GIS.TabIndex = 2;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            this.GIS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseUp);
            //
            // toolStripButton1
            //
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Text = "Animate";
            this.toolStripButton1.Click += toolStrip1_ButtonClick;
            //
            // toolStrip1
            //
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 29);
            this.toolStrip1.TabIndex = 1;
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Draggable Labels";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);

        }

        private void ToolStripButton1_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
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
        /// WinForm_Load - builds the two-layer data model and populates it
        /// with 20 randomly positioned ship points.
        ///
        /// "realpoints" layer:
        ///   CachedPaint = false so the viewer redraws moving shapes every
        ///   frame.  A CGM ship marker symbol is loaded.  Each shape receives
        ///   a random colour and rotation, and its Uid is stored in the "name"
        ///   field for label text.
        ///
        /// "sidekicks" layer:
        ///   Invisible markers (Marker.Size = 0).  The PaintShapeLabelEvent
        ///   delegate is registered to doLabelPaint.  Labels.Allocator = false
        ///   disables auto label-placement so each label stays exactly at the
        ///   sidekick's geographic position.
        ///
        ///   Each sidekick is created at the same position as its real-point
        ///   counterpart plus a 15-pixel map-unit offset so it starts visually
        ///   separated from the symbol.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_Shape shp;
            TGIS_Point ptg;
            Random rnd;
            int i;

            // --- "realpoints" layer ---
            ll = new TGIS_LayerVector();
            // Load the CGM ship symbol; SymbolList.Prepare caches the compiled
            // symbol for efficient repeated rendering.
            ll.Params.Marker.Symbol = TGIS_Utils.SymbolList.Prepare(
                                  TGIS_Utils.GisSamplesDataDirDownload() + @"Symbols\2267.cgm"
                                );
            ll.Name = "realpoints";
            // Disable tile cache so position changes are reflected immediately.
            ll.CachedPaint = false;

            GIS.Add(ll);
            ll.AddField("name", TGIS_FieldType.String, 100, 0);
            ll.Extent = TGIS_Utils.GisExtent(-180, -180, 180, 180); // world-spanning

            // --- "sidekicks" layer (the draggable label anchors) ---
            ll = new TGIS_LayerVector();
            ll.Name = "sidekicks";
            ll.Params.Marker.Size = 0;  // invisible marker
            ll.Params.Labels.Position = TGIS_LabelPosition.MiddleCenter;

            GIS.Add(ll);
            // Register the custom paint callback that draws the leader line
            // and calls shape.DrawLabel() for each sidekick during repaint.
            ll.PaintShapeLabelEvent += new TGIS_ShapeEvent(doLabelPaint);
            // Disable auto label placement - sidekick position IS the label position.
            ll.Params.Labels.Allocator = false;
            ll.CachedPaint = false;

            GIS.FullExtent();

            // --- Populate with 20 random ship points ---
            rnd = new Random();
            for (i = 0; i < 20; i++)
            {
                ptg = new TGIS_Point(rnd.Next(360) - 180,
                                      rnd.Next(180) - 90
                                    );

                // Create and configure the visible real point
                shp = ((TGIS_LayerVector)GIS.Get("realpoints")).CreateShape(TGIS_ShapeType.Point, TGIS_DimensionType.XY);
                shp.Lock(TGIS_Lock.Extent);   // freeze extent during construction
                shp.AddPart();
                shp.AddPoint(ptg);
                shp.Params.Marker.SymbolRotate = shp.Uid;  // unique angle per ship
                shp.Params.Marker.Size = 250;
                shp.Params.Marker.Color = TGIS_Color.FromRGB((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
                shp.Params.Marker.OutlineColor = shp.Params.Marker.Color;

                shp.SetField("name", String.Format(LABEL_TEXT + ": {0}", shp.Uid));
                shp.Unlock();

                // Create the matching sidekick, offset so it starts displaced
                // from the symbol (15 pixels expressed in current map units).
                shp = ((TGIS_LayerVector)GIS.Get("sidekicks")).CreateShape(TGIS_ShapeType.Point);
                shp.Lock(TGIS_Lock.Extent);
                shp.AddPart();

                // 15 / Zoom converts 15 screen pixels to the current map-unit scale.
                ptg.X = ptg.X + 15 / GIS.Zoom;
                ptg.Y = ptg.Y + 15 / GIS.Zoom;
                shp.AddPoint(ptg);
                shp.Unlock();
            }

            GIS.FullExtent();
        }

        /// <summary>
        /// Animate button click handler.
        /// Moves shape UID=5 (and its sidekick) 90 steps of (2,1) map units.
        /// Thread.Sleep + Application.DoEvents keeps the UI responsive.
        /// </summary>
        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            int i;
            TGIS_Shape shp;

            shp = ((TGIS_LayerVector)GIS.Get("realpoints")).GetShape(5);
            for (i = 0; i < 90; i++)
            {
                if (this.IsDisposed)
                {
                    break;
                }
                synchroMove(shp, 2, 1);
                Thread.Sleep(50);
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Moves the given real-point shape and its matching sidekick by
        /// (_x, _y) map units simultaneously.
        ///
        /// The sidekick is found by matching _shp.Uid because the two layers
        /// were populated with shapes whose UIDs run in the same sequence.
        ///
        /// The TGIS_Extent 'ex' is computed as the union of both point
        /// positions for use with selective map invalidation if desired.
        /// SetPosition already triggers viewer repaint internally.
        /// </summary>
        private void synchroMove(TGIS_Shape _shp, int _x, int _y)
        {
            TGIS_LayerVector ll;
            TGIS_Shape shp;
            TGIS_Point ptgA;
            TGIS_Point ptgB;
            TGIS_Extent ex;

            // Shift the real point by the requested offset
            ptgA = _shp.GetPoint(0, 0);
            ptgA.X = ptgA.X + _x;
            ptgA.Y = ptgA.Y + _y;
            _shp.SetPosition(ptgA, null, 0);

            // Find and shift the matching sidekick by the same displacement
            ll = (TGIS_LayerVector)GIS.Get("sidekicks");
            shp = ll.GetShape(_shp.Uid);
            ptgB = shp.GetPoint(0, 0);
            ptgB.X = ptgB.X + _x;
            ptgB.Y = ptgB.Y + _y;
            shp.SetPosition(ptgB, null, 0);

            // Bounding extent of both positions (for optional selective invalidation)
            ex.XMin = Math.Min(ptgA.X, ptgB.X);
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y);
            ex.XMax = Math.Max(ptgA.X, ptgB.X);
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y);
        }

        /// <summary>
        /// PaintShapeLabelEvent callback - called by the "sidekicks" layer
        /// for each sidekick shape during a viewer repaint pass.
        ///
        /// Responsibilities:
        ///   1. Locate the matching realpoint using the sidekick's Uid.
        ///   2. Convert both geographic positions to screen pixels via
        ///      MapToScreen (TGIS_Point -> System.Drawing.Point).
        ///   3. Set pen properties on the abstract renderer and draw the
        ///      blue leader line connecting the real position to the label.
        ///   4. Copy the "name" field value and call DrawLabel to render the
        ///      label text at the sidekick's current geographic position.
        ///
        /// TGIS_RendererAbstract provides a hardware-agnostic drawing API
        /// that works regardless of the active rendering backend
        /// (WinForms GDI+, SharpDX Direct2D, SkiaSharp, etc.).
        /// </summary>
        private void doLabelPaint(object _sender, TGIS_ShapeEventArgs _e)
        {
            Point ptA, ptB;                  // screen-pixel positions
            TGIS_LayerVector ll;
            TGIS_Shape shape = _e.Shape;     // the sidekick being painted
            TGIS_Shape shp;                  // the matching real point
            TGIS_RendererAbstract rnd;

            // Find the realpoints layer and the matching shape by UID
            ll = (TGIS_LayerVector)GIS.Get("realpoints");
            shp = ll.GetShape(shape.Uid);

            // MapToScreen converts geographic coordinates to screen pixels.
            // ptA = real symbol position; ptB = floating label anchor.
            ptA = shape.Viewer.Ref.MapToScreen(shp.GetPoint(0, 0));
            ptB = shape.Viewer.Ref.MapToScreen(shape.GetPoint(0, 0));

            // Draw the leader line on the abstract renderer canvas
            rnd = (TGIS_RendererAbstract)(GIS.Renderer);
            rnd.CanvasPen.Color = TGIS_Color.Blue;
            rnd.CanvasPen.Style = TGIS_PenStyle.Solid;
            rnd.CanvasPen.Width = 1;
            rnd.CanvasDrawLine(ptA.X, ptA.Y, ptB.X, ptB.Y);

            // Set the label text from the real feature and render it at the
            // sidekick's position using the layer's configured label style.
            shape.Params.Labels.Value = shp.GetField("name").ToString();
            shape.DrawLabel();
        }

        /// <summary>
        /// Begins a label drag when the user clicks near a sidekick shape.
        ///
        /// GIS.IsEmpty and GIS.InPaint guards prevent starting an edit while
        /// the viewer is empty or mid-repaint.
        ///
        /// Locate searches the sidekicks layer for the nearest shape within
        /// a 100-pixel tolerance (converted to map units by dividing by Zoom).
        ///
        /// GIS.Editor.MouseBegin is called directly rather than switching
        /// GIS.Mode to Edit, so normal pan/zoom is not globally disabled.
        /// </summary>
        private void GIS_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_Shape shp;

            if (GIS.IsEmpty) return;
            if (GIS.InPaint) return;

            // Find the sidekick closest to the click position
            ll = (TGIS_LayerVector)GIS.Get("sidekicks");
            shp = ll.Locate(GIS.ScreenToMap(new Point(e.X, e.Y)),
                             100 / GIS.Zoom    // 100px tolerance in map units
                           );
            currShape = shp;
            if (currShape == null) return;

            // Initiate editor drag state without switching the global viewer mode
            GIS.Editor.MouseBegin(new Point(e.X, e.Y), true);
        }

        /// <summary>
        /// Repositions the tracked sidekick shape to follow the mouse cursor.
        ///
        /// The pre- and post-move bounding extents are computed for potential
        /// selective repaint; SetPosition already triggers a full repaint.
        ///
        /// ScreenToMap converts the pixel mouse position to geographic map
        /// coordinates.  GisIsPointInsideExtent prevents the label from being
        /// dragged outside the viewer's valid geographic extent.
        /// </summary>
        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_Shape shp;
            TGIS_Point ptgA;
            TGIS_Point ptgB;
            TGIS_Extent ex;

            if (GIS.IsEmpty) return;
            if (currShape == null) return;

            // Capture pre-move extent (sidekick + its real counterpart)
            ptgA = currShape.GetPoint(0, 0);
            ll = (TGIS_LayerVector)GIS.Get("realpoints");
            shp = ll.GetShape(currShape.Uid);
            ptgB = shp.GetPoint(0, 0);
            ex.XMin = Math.Min(ptgA.X, ptgB.X);
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y);
            ex.XMax = Math.Max(ptgA.X, ptgB.X);
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y);

            // Reproject mouse pixel position to map coordinates and move
            ptgA = GIS.ScreenToMap(new Point(e.X, e.Y));
            if (TGIS_Utils.GisIsPointInsideExtent(ptgA, GIS.Extent))
                currShape.SetPosition(ptgA, null, 0);

            // Capture post-move extent
            ptgA = currShape.GetPoint(0, 0);
            ll = (TGIS_LayerVector)GIS.Get("realpoints");
            shp = ll.GetShape(currShape.Uid);
            ptgB = shp.GetPoint(0, 0);
            ex.XMin = Math.Min(ptgA.X, ptgB.X);
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y);
            ex.XMax = Math.Max(ptgA.X, ptgB.X);
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y);
        }

        /// <summary>
        /// Ends the current label drag by clearing the tracked shape reference.
        /// </summary>
        private void GIS_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GIS.IsEmpty) return;
            currShape = null;
        }
    }
}
