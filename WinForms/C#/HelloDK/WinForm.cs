// HelloDK sample — introductory demonstration of core TatukGIS DK workflows (C# / WinForms).
//
// What the sample shows:
//   - Opening a vector Shapefile (world map) into the GIS viewer
//   - Switching the viewer interaction mode: Zoom / Drag / Select
//   - Creating an in-memory editable vector layer with a transparent polygon style
//   - Building a polygon shape programmatically by adding explicit vertices
//   - Click-to-select a feature using screen-to-map coordinate conversion
//   - Spatial proximity search via GIS.Locate to identify shapes near cursor
//   - Spatial containment query using DE-9IM matrix "T*****FF*" (touches/inside)
//   - SQL WHERE filtering to find shapes with attributes starting with 's'
//
// Key TatukGIS API concepts shown here:
//   TGIS_ViewerWnd              - main visual map control
//   TGIS_LayerVector            - vector layer (shapefile or in-memory)
//   TGIS_Shape                  - individual geographic feature
//   TGIS_ViewerMode             - interaction modes (Zoom, Drag, Select)
//   GIS.Locate()                - spatial proximity search at point
//   GIS.ScreenToMap()           - convert screen pixels to geographic coords
//   TGIS_LayerVector.Loop()     - spatial enumeration with DE-9IM filtering
//   Shape attributes / SQL WHERE - feature filtering and selection

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace DK.WinForms.CS
{
    /// <summary>
    /// HelloDK sample — introductory demonstration of core TatukGIS DK workflows.
    /// Opens a world shapefile, creates an in-memory vector layer with a polygon, performs spatial containment
    /// queries using DE-9IM topology predicates, and switches between interaction modes (Zoom, Drag, Select).
    /// Demonstrates screen-to-map coordinate conversion, feature location via GIS.Locate(), and SQL filtering.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;

        // "Open project" button - loads the sample world shapefile
        private Button btnOpen;
        // "Zooming" button - enables rubber-band zoom interaction
        private Button btnZoom;
        // "Dragging" button - enables pan/drag interaction
        private Button btnDrag;
        // "Create Shape" button - adds an editable layer with a sample polygon
        private Button btnCreate;
        // "Find Shape" button - runs DE-9IM spatial containment query
        private Button btnFind;
        // "Selecting" button - enables click-to-select interaction
        private Button btnSelect;
        // The central GIS map viewer control
        private TGIS_ViewerWnd GIS;

        public WinForm()
        {
            InitializeComponent();
        }

        /// <summary>Clean up any resources being used.</summary>
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnDrag = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // btnOpen
            //
            this.btnOpen.Location = new System.Drawing.Point(13, 13);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(82, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open project";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            //
            // btnZoom
            //
            this.btnZoom.Location = new System.Drawing.Point(101, 13);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(99, 23);
            this.btnZoom.TabIndex = 1;
            this.btnZoom.Text = "Zooming";
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            //
            // btnDrag
            //
            this.btnDrag.Location = new System.Drawing.Point(206, 13);
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(100, 23);
            this.btnDrag.TabIndex = 2;
            this.btnDrag.Text = "Dragging";
            this.btnDrag.UseVisualStyleBackColor = true;
            this.btnDrag.Click += new System.EventHandler(this.btnDrag_Click);
            //
            // btnCreate
            //
            this.btnCreate.Location = new System.Drawing.Point(393, 13);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(85, 23);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create shape";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreateShape_Click);
            //
            // btnFind
            //
            this.btnFind.Location = new System.Drawing.Point(484, 13);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(105, 23);
            this.btnFind.TabIndex = 5;
            this.btnFind.Text = "Find shape";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            //
            // GIS
            //
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Location = new System.Drawing.Point(-1, 43);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(888, 517);
            this.GIS.TabIndex = 7;
            this.GIS.TapSimpleEvent += new TatukGIS.RTL.TGIS_TapEvent(this.GIS_TapSimpleEvent);
            //
            // btnSelect
            //
            this.btnSelect.Location = new System.Drawing.Point(312, 13);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Selecting";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click_1);
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnDrag);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.btnOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Hello DK";
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>Application entry point.</summary>
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
        /// "Open project" button click handler.
        /// Opens the WorldDCW world Shapefile from the DK sample data directory and
        /// loads it into the viewer. The viewer auto-zooms to the full extent of
        /// the loaded data. After opening, the mode is set to Select so the user
        /// can immediately click on features.
        /// </summary>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\WorldDCW\world.shp");
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        /// <summary>
        /// "Zooming" button click handler.
        /// Switches the viewer to Zoom mode. In this mode the left mouse button
        /// draws a rubber-band rectangle to zoom into a region; the right button
        /// zooms out.
        /// </summary>
        private void btnZoom_Click(object sender, EventArgs e)
        {
            // Do nothing if no layers are loaded
            if (GIS.IsEmpty) return;
            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        /// <summary>
        /// "Selecting" button click handler (secondary binding).
        /// Switches the viewer to Select mode so clicks toggle shape selection.
        /// </summary>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            // Do nothing if no layers are loaded
            if (GIS.IsEmpty) return;
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        /// <summary>
        /// "Dragging" button click handler.
        /// Switches the viewer to Drag mode, allowing the user to pan the map
        /// by clicking and dragging with the mouse.
        /// </summary>
        private void btnDrag_Click(object sender, EventArgs e)
        {
            // Do nothing if no layers are loaded
            if (GIS.IsEmpty) return;
            GIS.Mode = TGIS_ViewerMode.Drag;
        }

        /// <summary>
        /// "Create Shape" button click handler.
        /// Creates a new in-memory TGIS_LayerVector named "edit layer", gives it a
        /// transparent fill with a blue outline, then adds a single quadrilateral
        /// polygon to it. This layer is not backed by a file - it exists only while
        /// the application is running.
        /// </summary>
        private void btnCreateShape_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_Shape shp;

            // Guard: if the edit layer already exists, do nothing (idempotent)
            ll = (TGIS_LayerVector)GIS.Get("edit layer");
            if (ll != null) return;

            // Create a new in-memory vector layer and register it with the viewer
            ll = new TGIS_LayerVector();
            ll.Name = "edit layer";
            // Inherit the viewer's coordinate system so coordinates are interpreted correctly
            ll.CS = GIS.CS;

            // Style: transparent fill (Clear pattern) with a solid blue outline,
            // so the underlying world layer remains visible through the polygon
            ll.Params.Area.OutlineColor = TGIS_Color.Blue;
            ll.Params.Area.Pattern = TGIS_BrushStyle.Clear;

            // Register the layer with the viewer; it will appear on top of existing layers
            GIS.Add(ll);

            // Create a new Polygon shape inside the layer
            shp = ll.CreateShape(TGIS_ShapeType.Polygon);

            // Lock(Extent) batches vertex additions so the bounding box is recalculated
            // only once when Unlock is called, improving performance for bulk edits
            shp.Lock(TGIS_Lock.Extent);

            // AddPart starts the first ring of the polygon; a shape can have multiple
            // parts (e.g., islands or holes in a multi-polygon)
            shp.AddPart();

            // Add the four corner vertices of the polygon (coordinates in the map's CS)
            shp.AddPoint(new TGIS_Point(10, 10));
            shp.AddPoint(new TGIS_Point(10, 80));
            shp.AddPoint(new TGIS_Point(80, 90));
            shp.AddPoint(new TGIS_Point(90, 10));

            // Unlock finalises the shape geometry: recalculates extents and closes
            // the polygon ring automatically if the first and last points differ
            shp.Unlock();

            // Redraw the entire map canvas to show the newly added polygon
            GIS.InvalidateWholeMap();
        }

        /// <summary>
        /// TapSimple event handler - fired on every single mouse click on the viewer.
        /// When the viewer is in Select mode, this converts the click position from
        /// screen pixels to map coordinates, finds the nearest shape within a
        /// tolerance of 5 pixels, and toggles its selection state.
        /// </summary>
        /// <param name="_e">Event args containing X/Y screen pixel coordinates.</param>
        private void GIS_TapSimpleEvent(object _sender, TatukGIS.RTL.TGIS_TapEventArgs _e)
        {
            TGIS_Shape shp;
            TGIS_Point ptg;
            TGIS_LayerVector lv;
            double precision;

            // Ignore taps when the viewer is not in Select mode
            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            // Convert screen pixel coordinates to geographic map coordinates.
            // ScreenToMap accounts for the current zoom level and pan offset.
            ptg = GIS.ScreenToMap(new Point((int)_e.X, (int)_e.Y));

            // Get the world layer to manage its selection state
            lv = (TGIS_LayerVector)GIS.Items[0];

            // Compute the hit-test tolerance: 5 screen pixels expressed in map units.
            // Dividing by Zoom converts pixels to the map's coordinate unit.
            precision = 5 / GIS.Zoom;

            // Search all layers for the topmost shape within 'precision' of the click point
            shp = (TGIS_Shape)GIS.Locate(ptg, precision);

            // If no shape was found near the click point, do nothing
            if (shp == null) return;

            // Clear any previously selected shapes before applying the new selection
            lv.DeselectAll();

            // Toggle selection: clicking a selected shape deselects it, and vice versa
            shp.IsSelected = !shp.IsSelected;

            // Repaint the map to reflect the updated selection highlight
            GIS.InvalidateWholeMap();
        }

        /// <summary>
        /// "Find Shape" button click handler.
        /// Uses DE-9IM (Dimensionally Extended 9-Intersection Model) spatial
        /// relationship to find all world features that are fully contained inside
        /// the polygon created by btnCreateShape_Click.
        ///
        /// The DE-9IM matrix "T*****FF*" encodes the "contains" relationship:
        ///   - 'T' at position [0]: interiors must intersect (non-empty)
        ///   - "FF" at positions [6,7]: the query shape's boundary and exterior
        ///     must NOT intersect the target shape's interior - i.e. the target
        ///     lies entirely within the query polygon.
        ///
        /// An additional SQL LIKE filter restricts results to features whose
        /// 'label' field starts with the letter 's'.
        /// </summary>
        private void btnFind_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_LayerVector lv;
            TGIS_Shape selShp;

            // The edit layer must exist (created by btnCreateShape_Click) to provide
            // the selection polygon; exit early if it has not been created yet
            ll = (TGIS_LayerVector)GIS.Get("edit layer");
            if (ll == null) return;

            // Retrieve the world layer - its name is derived from the filename ('world')
            lv = (TGIS_LayerVector)GIS.Get("world");

            // Clear any previous selection on the world layer before applying the new one
            lv.DeselectAll();

            // Retrieve the first (and only) shape from the edit layer to use as
            // the spatial query boundary
            selShp = ll.GetShape(1);  // just the first shape from the layer

            // MakeEditable pins the shape into memory so it survives the subsequent
            // iteration; file-backed shapes are otherwise evicted from cache
            selShp = selShp.MakeEditable();

            // Lock the viewer to batch all selection redraws into a single repaint
            GIS.Lock();

            // Loop over all shapes in the world layer whose bounding box overlaps
            // selShp.Extent, whose 'label' field matches the SQL pattern 's%', AND
            // whose DE-9IM relationship with selShp satisfies "T*****FF*" (Contains)
            foreach (TGIS_Shape shp in lv.Loop(selShp.Extent, "label LIKE 's%'", selShp, "T*****FF*"))
            {
                shp.IsSelected = true;
            }

            // Unlock releases the batched repaint and triggers a single screen refresh
            GIS.Unlock();

            // Force a full map redraw to show the newly selected shapes highlighted
            GIS.InvalidateWholeMap();
        }

        /// <summary>
        /// "Selecting" button click handler (primary binding).
        /// Switches the viewer to Select mode so clicks toggle shape selection.
        /// </summary>
        private void btnSelect_Click_1(object sender, EventArgs e)
        {
            // Do nothing if no layers are loaded
            if (GIS.IsEmpty) return;
            GIS.Mode = TGIS_ViewerMode.Select;
        }
    }
}
