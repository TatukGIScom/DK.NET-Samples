// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// SelectByShape sample — demonstrates how to select vector features by drawing
// an arbitrary shape (circle or rectangle) interactively on the map.
//
// Key concepts shown:
//   - Interactive rubber-band drawing of a selection shape on the GIS viewer
//     using mouse events and the PaintExtraEvent hook.
//   - Converting screen pixel coordinates to geographic map coordinates via
//     ScreenToMap, so the drawn shape is in the layer's coordinate system.
//   - Building a circular selection area using TGIS_Topology.MakeBuffer, which
//     expands a point geometry outward by a given radius in map units.
//   - Building a rectangular selection polygon by manually adding four corner
//     points to a new polygon shape.
//   - Spatial query via TGIS_LayerVector.FindFirst / FindNext using the
//     GIS_RELATE_INTERSECT predicate (a DE-9IM relationship test) to find all
//     features that share at least one point with the selection shape.
//   - Visually highlighting matched features with IsSelected and listing their
//     "name" attribute in a text box.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace SelectByShape
{
    /// <summary>
    /// Main application form for the SelectByShape sample.
    /// Hosts the TatukGIS viewer control, two mode-toggle buttons (circle /
    /// rectangle), and a read-only text box that lists the names of the features
    /// found to intersect the drawn selection shape.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.TextBox textBox1;       // Lists names of selected features
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;   // Main map viewer control

        // Screen-space anchor point recorded when the left mouse button is pressed
        private System.Drawing.Point oldPos;
        // Current drag-end point (rectangle second corner) updated on every MouseMove
        private System.Drawing.Point oldPos2;
        // Current radius of the rubber-band circle in screen pixels
        private double oldRadius;

        private Panel panel1;
        private CheckBox btnRect;    // Toggle: select by rectangle
        private CheckBox btnCircle;  // Toggle: select by circle

        // Persistent reference to the most recently used in-memory layer
        // (reused across Load / MouseUp calls)
        private TGIS_LayerVector ll;

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
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRect = new System.Windows.Forms.CheckBox();
            this.btnCircle = new System.Windows.Forms.CheckBox();
            this.GIS.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // stripBar1
            //
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 0;
            //
            // toolStripLabel1
            //
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Use left mouse button to select by chosen shape";
            this.toolStripLabel1.Width = 575;
            //
            // textBox1
            //
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBox1.Location = new System.Drawing.Point(408, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(184, 447);
            this.textBox1.TabIndex = 1;
            //
            // GIS
            //
            this.GIS.Controls.Add(this.panel1);
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraPosition = null;
            this.GIS.CursorForCameraRotation = null;
            this.GIS.CursorForCameraXY = null;
            this.GIS.CursorForCameraXYZ = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.CursorForSunPosition = null;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 0);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(408, 447);
            this.GIS.TabIndex = 2;
            this.GIS.UseRTree = false;
            this.GIS.PaintExtraEvent += new TatukGIS.NDK.TGIS_RendererEvent(this.GIS_PaintExtraEvent);
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            this.GIS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseUp);
            //
            // panel1
            //
            this.panel1.Controls.Add(this.btnRect);
            this.panel1.Controls.Add(this.btnCircle);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 27);
            this.panel1.TabIndex = 0;
            //
            // btnRect
            //
            this.btnRect.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnRect.AutoSize = true;
            this.btnRect.Location = new System.Drawing.Point(65, 1);
            this.btnRect.Name = "btnRect";
            this.btnRect.Size = new System.Drawing.Size(76, 23);
            this.btnRect.TabIndex = 3;
            this.btnRect.Text = "By rectangle";
            this.btnRect.UseVisualStyleBackColor = true;
            this.btnRect.CheckedChanged += new System.EventHandler(this.btnRect_CheckedChanged);
            //
            // btnCircle
            //
            this.btnCircle.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnCircle.AutoSize = true;
            this.btnCircle.Location = new System.Drawing.Point(3, 1);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(57, 23);
            this.btnCircle.TabIndex = 2;
            this.btnCircle.Text = "By circle";
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.CheckedChanged += new System.EventHandler(this.btnCircle_CheckedChanged);
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Select by shape";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.GIS.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        /// Initialises the map when the form first becomes visible.
        /// Opens the base Counties shapefile and adds two empty in-memory vector
        /// layers that will hold visual indicators during selection:
        ///   "Points"  — invisible marker shapes (size 0) used only as geometry
        ///               containers for the MakeBuffer radius calculation.
        ///   "Buffers" — the rendered selection polygon (circle or rectangle).
        /// The viewer is locked during setup so that only one repaint fires.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            GIS.Lock();
            // Open the base shapefile; Counties.shp supplies the features to select from
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\USA\States\California\Counties.shp");

            // "Points" layer — invisible markers that act as geometry placeholders
            ll = new TGIS_LayerVector();
            ll.Params.Area.Color = TGIS_Color.Blue;
            ll.Transparency = 50;
            ll.Name = "Points";
            // Inherit the coordinate system from the viewer so all geometry is in the
            // same spatial reference and ScreenToMap conversions are correct
            ll.CS = GIS.CS;
            GIS.Add(ll);

            // "Buffers" layer — holds the selection shape drawn by the user
            ll = new TGIS_LayerVector();
            ll.Params.Area.Color = TGIS_Color.Blue;
            ll.Params.Area.OutlineColor = TGIS_Color.Blue;
            ll.Transparency = 60;   // Semi-transparent so the map beneath shows through
            ll.Name = "Buffers";
            ll.CS = GIS.CS;
            GIS.Add(ll);
            GIS.Unlock();

            // Default to rectangle mode on startup
            btnRect.Checked = true;
        }

        /// <summary>
        /// Records the anchor point when the user starts a drag.
        /// Right-click switches to the viewer's built-in Zoom mode so the user
        /// can pan or zoom without permanently leaving the custom Select mode.
        /// </summary>
        private void GIS_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GIS.IsEmpty) return;

            if (e.Button == MouseButtons.Right)
            {
                // Right-click: hand control back to the viewer's built-in zoom/pan behaviour
                GIS.Mode = TGIS_ViewerMode.Zoom;
                return;
            }
            // Initialise both anchor and drag-end to the same pixel so the shape
            // starts with zero size (prevents stale values from a previous selection)
            oldPos = new Point(e.X, e.Y);
            oldPos2 = new Point(e.X, e.Y);
            oldRadius = 0;
        }

        /// <summary>
        /// Updates the rubber-band dimensions while the left mouse button is held.
        /// For rectangle mode the second corner follows the cursor; for circle mode
        /// the radius is the Euclidean pixel distance from the anchor to the cursor.
        /// Calling GIS.Invalidate() triggers PaintExtraEvent on the next repaint so
        /// the rubber-band is redrawn every frame during the drag.
        /// </summary>
        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GIS.IsEmpty) return;

            if (GIS.Mode != TGIS_ViewerMode.Select) return;
            // Only update the rubber-band while the left button is pressed (dragging)
            if (!(e.Button == MouseButtons.Left)) return;

            if (btnRect.Checked == true)
            {
                // Track the second corner of the bounding rectangle
                oldPos2 = new Point(e.X, e.Y);
            }

            if (btnCircle.Checked == true)
            {
                // Pythagorean distance from the anchor to the current cursor position
                oldRadius = (int)Math.Round(Math.Sqrt(Math.Pow(oldPos.X - e.X, 2) + Math.Pow(oldPos.Y - e.Y, 2)));
            }

            // Trigger a repaint so PaintExtraEvent draws the updated rubber-band shape
            GIS.Invalidate();
        }

        /// <summary>
        /// Finalises the selection when the mouse button is released.
        ///
        /// Steps:
        ///   1. Guard against empty map, zero-size drag, or right-click.
        ///   2. Record invisible point markers in the "Points" layer as geometry
        ///      containers for the buffer / corner calculations.
        ///   3. Clear the "Buffers" layer and add the new selection polygon:
        ///        Circle   — TGIS_Topology.MakeBuffer approximates a circle as a
        ///                   32-vertex polygon; the radius is derived by converting
        ///                   oldRadius screen pixels into map units via ScreenToMap.
        ///        Rectangle — four corner points are assembled in map coordinates.
        ///   4. Iterate the base Counties layer (Items[0]) with FindFirst/FindNext
        ///      using the GIS_RELATE_INTERSECT DE-9IM predicate, which returns true
        ///      when two geometries share at least one point (boundary or interior).
        ///   5. Mark each matched feature as selected and append its name.
        /// </summary>
        private void GIS_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Topology tpl;
            TGIS_LayerVector ll;
            TGIS_Shape tmp;
            TGIS_Shape buf;
            TGIS_Point ptg = new TGIS_Point();   // Centre (circle) or first corner (rectangle) in map coords
            TGIS_Point ptg1 = new TGIS_Point();  // Edge point used to compute the circle radius in map units
            TGIS_Point ptg2 = new TGIS_Point();  // Opposite corner for rectangle in map coords
            double distance;

            if (GIS.IsEmpty) return;

            if (e.Button == MouseButtons.Right)
            {
                // Right-click: restore Select mode (reversed the right-click zoom set in MouseDown)
                GIS.Mode = TGIS_ViewerMode.Select;
                return;
            }

            // Guard: if the drag produced no movement, ignore (avoids zero-size shapes)
            if (btnRect.Checked == true)
            {
                if ((oldPos2.X == oldPos.X) && (oldPos2.Y == oldPos.Y)) return;
            }
            if (btnCircle.Checked == true)
            {
                if (oldRadius == 0) return;
            }

            // --- Add invisible marker(s) to the "Points" layer ---
            // Marker.Size = 0 hides them from the renderer; they exist only to
            // supply geometry for MakeBuffer (circle) or as placeholders (rectangle).
            ll = (TGIS_LayerVector)GIS.Get("Points");
            ll.Lock();

            tmp = ll.CreateShape(TGIS_ShapeType.Point);

            if (btnCircle.Checked)
            {
                // Convert the anchor screen pixel to a map coordinate (centre of circle)
                ptg = GIS.ScreenToMap(oldPos);
                tmp = ll.CreateShape(TGIS_ShapeType.Point);
                tmp.Params.Marker.Size = 0;
                // Lock with Extent so the layer's spatial extent is updated automatically
                tmp.Lock(TGIS_Lock.Extent);
                tmp.AddPart();
                tmp.AddPoint(ptg);
                tmp.Unlock();
                ll.Unlock();
                // Convert a point that is exactly oldRadius pixels to the right of centre;
                // the map-unit X difference between ptg1 and ptg is the circle radius
                ptg1 = GIS.ScreenToMap(new Point(oldPos.X + (int)oldRadius, e.Y));
            }

            if (btnRect.Checked)
            {
                // Record the first corner as an invisible marker
                ptg = GIS.ScreenToMap(oldPos);
                tmp = ll.CreateShape(TGIS_ShapeType.Point);
                tmp.Params.Marker.Size = 0;
                tmp.Lock(TGIS_Lock.Extent);
                tmp.AddPart();
                tmp.AddPoint(ptg);
                tmp.Unlock();
                // Record the second corner (drag endpoint) as a second invisible marker
                ptg = GIS.ScreenToMap(oldPos2);
                tmp.AddPoint(ptg);
                ll.Unlock();
                // Re-convert the first corner for use during rectangle construction below
                ptg1 = GIS.ScreenToMap(oldPos);
            }
            buf = ll.CreateShape(TGIS_ShapeType.Unknown);

            // --- Rebuild the "Buffers" layer with the new selection shape ---
            ll = (TGIS_LayerVector)GIS.Get("Buffers");
            // RevertShapes clears all shapes added since the last commit, effectively
            // resetting the layer to empty so previous selections are discarded
            ll.RevertShapes();

            if (btnCircle.Checked)
            {
                // The map-unit radius is the horizontal distance from centre to edge point
                distance = ptg1.X - ptg.X;

                tpl = new TGIS_Topology();
                // MakeBuffer approximates a circle as a 32-vertex polygon around tmp.
                // The last parameter (true) closes the ring automatically.
                buf = tpl.MakeBuffer(tmp, distance, 32, true);
                // AddShape transfers ownership of the geometry to the Buffers layer
                buf = ll.AddShape(buf);
            }

            if (btnRect.Checked)
            {
                // Build a closed rectangle polygon from the two diagonally opposite corners
                ptg2 = GIS.ScreenToMap(oldPos2);
                buf = ll.CreateShape(TGIS_ShapeType.Polygon);
                buf.AddPart();
                // Wind the four corners in order (top-left, top-right, bottom-right, bottom-left)
                buf.AddPoint(ptg1);
                buf.AddPoint(TGIS_Utils.GisPoint(ptg1.X, ptg2.Y));
                buf.AddPoint(ptg2);
                buf.AddPoint(TGIS_Utils.GisPoint(ptg2.X, ptg1.Y));
            }

            // --- Perform the spatial query on the base Counties layer (Items[0]) ---
            ll = (TGIS_LayerVector)GIS.Items[0];
            if (ll == null)
            {
                GIS.InvalidateWholeMap();
                return;
            }

            // Clear any previously selected features before the new selection pass
            ll.DeselectAll();

            textBox1.Clear();

            GIS.InvalidateWholeMap();
            GIS.Lock();
            // FindFirst / FindNext iterate over shapes whose bounding box overlaps
            // buf.Extent, then applies GIS_RELATE_INTERSECT (a DE-9IM test) to confirm
            // that the geometries actually share at least one point — not just bounding boxes.
            tmp = ll.FindFirst(buf.Extent, "", buf, TGIS_Utils.GIS_RELATE_INTERSECT());
            while (tmp != null)
            {
                // This feature intersects the selection polygon: highlight it and record its name
                textBox1.AppendText(tmp.GetField("name").ToString() + "\r\n");
                tmp.IsSelected = true;
                tmp = ll.FindNext();
            }
            GIS.Unlock();
        }

        /// <summary>
        /// Called by the viewer after all map layers have been drawn.
        /// Renders the rubber-band selection shape directly on the renderer canvas
        /// in screen space using the abstract renderer API.
        ///
        /// Using PaintExtraEvent (rather than a Windows GDI overlay) ensures the
        /// feedback is composited correctly on top of the map without flickering and
        /// works regardless of hardware acceleration.
        ///
        /// The pen colour is randomised each call to create a flickering "marching
        /// ants" visual effect on the outline during a drag operation.
        /// </summary>
        private void GIS_PaintExtraEvent(object _sender, TGIS_RendererEventArgs _e)
        {
            TGIS_RendererAbstract rdr;
            Random rnd;

            rnd = new Random();
            rdr = _e.Renderer;
            rdr.CanvasPen.Width = 1;
            // Random colour creates the animated "marching ants" outline effect
            rdr.CanvasPen.Color = TGIS_Color.FromBGR((uint)rnd.Next(0xFFFFFF));
            rdr.CanvasPen.Style = TGIS_PenStyle.Solid;
            // Clear brush so only the outline is drawn (no fill obscuring the map)
            rdr.CanvasBrush.Style = TGIS_BrushStyle.Clear;

            if (btnRect.Checked)
            {
                // Do not draw until the user has actually moved the cursor (zero-size guard)
                if ((oldPos.X == oldPos2.X) && (oldPos.Y == oldPos2.Y)) return;

                rdr.CanvasDrawRectangle(new Rectangle(oldPos.X, oldPos.Y, oldPos2.X - oldPos.X, oldPos2.Y - oldPos.Y));
            }
            if (btnCircle.Checked)
            {
                // CanvasDrawEllipse takes top-left corner X/Y, width, height in screen pixels.
                // Top-left is the centre offset by the radius on both axes.
                rdr.CanvasDrawEllipse(oldPos.X - (int)Math.Round(oldRadius), oldPos.Y - (int)Math.Round(oldRadius), (int)oldRadius * 2, (int)oldRadius * 2);
            }
        }

        /// <summary>
        /// Keeps the two mode buttons mutually exclusive: selecting rectangle
        /// automatically deselects circle and vice versa.
        /// </summary>
        private void btnRect_CheckedChanged(object sender, EventArgs e)
        {
            btnCircle.Checked = !btnRect.Checked;
        }

        /// <summary>
        /// Keeps the two mode buttons mutually exclusive: selecting circle
        /// automatically deselects rectangle and vice versa.
        /// </summary>
        private void btnCircle_CheckedChanged(object sender, EventArgs e)
        {
            btnRect.Checked = !btnCircle.Checked;
        }
    }
}
