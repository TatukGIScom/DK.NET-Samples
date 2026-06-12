//=============================================================================
// This source code is a part of TatukGIS Developer Kernel.
//=============================================================================
//
// GeoCoding Sample - C# WinForms (TatukGIS NDK)
// ==============================================
// Demonstrates address geocoding and shortest-path routing using TatukGIS DK.
//
// Geocoding translates a human-readable address string (e.g. "Chrysler 1345")
// into geographic coordinates by matching it against street-range attribute
// fields stored in a vector layer.  The TGIS_Geocoding class performs this
// matching entirely on the client side using the loaded shapefile — no
// external service is needed unless OSM online geocoding is enabled.
//
// Routing finds the shortest path between two geocoded points using
// TGIS_ShortestPath, which builds an in-memory Dijkstra graph from the road
// network.  The resulting path segments are visualised by copying them into a
// separate in-memory "RouteDisplay" layer rendered in red, with green/red
// point markers for the start and end positions.
//
// Key concepts shown:
//   - Opening a TatukGIS project (.ttkproject) and retrieving a named layer
//   - Constructing TGIS_Geocoding with TIGER/Line address-range field mappings
//   - Using TGIS_Geocoding.Parse to resolve typed addresses to coordinates
//   - Building a TGIS_ShortestPath graph with per-link-type cost modifiers
//   - Handling the LinkTypeEvent callback to classify road segments by MTFCC code
//   - Drawing route results and address markers into a TGIS_LayerVector overlay
//   - Displaying a TGIS_ControlScale bar linked to the viewer
//
// Data used:
//   California.ttkproject — a TatukGIS project referencing US Census TIGER/Line
//   street data.  Relevant attribute fields:
//     FULLNAME  — full street name
//     LFROMADD  — address range start, left side of street
//     LTOADD    — address range end, left side of street
//     RFROMADD  — address range start, right side of street
//     RTOADD    — address range end, right side of street
//     MTFCC     — MAF/TIGER Feature Class Code; "S1400"+ = local/residential road
//=============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.RTL;           // TatukGIS run-time library utilities
using TatukGIS.NDK;           // Core TatukGIS .NET SDK types and classes
using TatukGIS.NDK.WinForms;  // WinForms-specific controls (TGIS_ViewerWnd, etc.)

namespace Geocoding
{
    /// <summary>
    /// Main application form for the GeoCoding &amp; Routing sample.
    /// Hosts a <see cref="TGIS_ViewerWnd"/> displaying California street data
    /// and provides address resolution and shortest-path route finding.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        // ------------------------------------------------------------------ //
        //  Designer-managed fields (do not modify names/types)                //
        // ------------------------------------------------------------------ //

        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;

        private System.Windows.Forms.Panel panel1;           // Right-hand control panel
        private System.Windows.Forms.TextBox memRoute;       // Read-only directions text area
        private System.Windows.Forms.GroupBox groupBox1;     // Groups routing parameter controls
        private System.Windows.Forms.Label lblSmallRoads;    // Label for trkSmallRoads
        private System.Windows.Forms.TrackBar trkSmallRoads; // Local-road preference slider (1–10)
        private System.Windows.Forms.Label lblHighways;      // Label for trkHighways
        private System.Windows.Forms.TrackBar trkHighways;   // Highway preference slider (1–10)
        private System.Windows.Forms.Label lblAddrFrom;      // Label for edtAddrFrom
        private System.Windows.Forms.TextBox edtAddrFrom;    // Start address input
        private System.Windows.Forms.Label lblAddrTo;        // Label for edtAddrTo
        private System.Windows.Forms.TextBox edtAddrTo;      // End address input
        private System.Windows.Forms.Button btnResolve;      // Resolve start address on map
        private System.Windows.Forms.Button btnRoute;        // Find shortest route
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;   // TatukGIS map viewer
        private TatukGIS.NDK.TGIS_LayerVector layerSrc;      // Source streets layer from project
        private TatukGIS.NDK.TGIS_LayerVector layerRoute;    // In-memory route display layer
        private TatukGIS.NDK.TGIS_Geocoding geoObj;          // Geocoding engine (address → coords)
        private TatukGIS.NDK.TGIS_ShortestPath rtrObj;       // Routing engine (Dijkstra)
        private System.Windows.Forms.ToolTip toolTip1;       // Tooltip provider for address boxes
        private TatukGIS.NDK.WinForms.TGIS_ControlScale GIS_ControlScale; // Scale-bar overlay

        /// <summary>
        /// Initialises the form and wires up tooltip hints on the address text boxes.
        /// The hints show example address strings that the California data set supports.
        /// </summary>
        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // Provide example address strings as tooltips on the From/To inputs
            // so users know what format the geocoder expects.
            toolTip1.SetToolTip(this.edtAddrFrom,
                                 @"""Pen"", ""Pennsylvania"", ""Penn 12"""
                               );
            toolTip1.SetToolTip(this.edtAddrTo,
                                 @"""Pen"", ""Pennsylvania"", ""Penn 12"""
                               );
        }

        /// <summary>
        /// Releases managed and unmanaged resources held by the form.
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
            this.components = new System.ComponentModel.Container();
            TatukGIS.NDK.TGIS_CSUnits tgiS_CSUnits1 = new TatukGIS.NDK.TGIS_CSUnits();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.memRoute = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRoute = new System.Windows.Forms.Button();
            this.btnResolve = new System.Windows.Forms.Button();
            this.edtAddrTo = new System.Windows.Forms.TextBox();
            this.lblAddrTo = new System.Windows.Forms.Label();
            this.edtAddrFrom = new System.Windows.Forms.TextBox();
            this.lblAddrFrom = new System.Windows.Forms.Label();
            this.trkHighways = new System.Windows.Forms.TrackBar();
            this.lblHighways = new System.Windows.Forms.Label();
            this.trkSmallRoads = new System.Windows.Forms.TrackBar();
            this.lblSmallRoads = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_ControlScale = new TatukGIS.NDK.WinForms.TGIS_ControlScale();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkHighways)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSmallRoads)).BeginInit();
            this.SuspendLayout();
            //
            // panel1
            //
            this.panel1.AccessibleDescription = "";
            this.panel1.Controls.Add(this.memRoute);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(404, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 466);
            this.panel1.TabIndex = 0;
            //
            // memRoute
            //
            this.memRoute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memRoute.Location = new System.Drawing.Point(0, 249);
            this.memRoute.Multiline = true;
            this.memRoute.Name = "memRoute";
            this.memRoute.ReadOnly = true;
            this.memRoute.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.memRoute.Size = new System.Drawing.Size(188, 217);
            this.memRoute.TabIndex = 1;
            this.memRoute.WordWrap = false;
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.btnRoute);
            this.groupBox1.Controls.Add(this.btnResolve);
            this.groupBox1.Controls.Add(this.edtAddrTo);
            this.groupBox1.Controls.Add(this.lblAddrTo);
            this.groupBox1.Controls.Add(this.edtAddrFrom);
            this.groupBox1.Controls.Add(this.lblAddrFrom);
            this.groupBox1.Controls.Add(this.trkHighways);
            this.groupBox1.Controls.Add(this.lblHighways);
            this.groupBox1.Controls.Add(this.trkSmallRoads);
            this.groupBox1.Controls.Add(this.lblSmallRoads);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 249);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Routing parameters";
            //
            // btnRoute
            //
            this.btnRoute.Location = new System.Drawing.Point(94, 214);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(75, 23);
            this.btnRoute.TabIndex = 9;
            this.btnRoute.Text = "Find &Route";
            this.btnRoute.Click += new System.EventHandler(this.btnRoute_Click);
            //
            // btnResolve
            //
            this.btnResolve.Location = new System.Drawing.Point(94, 160);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(75, 23);
            this.btnResolve.TabIndex = 8;
            this.btnResolve.Text = "Find &Address";
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            //
            // edtAddrTo
            //
            this.edtAddrTo.Location = new System.Drawing.Point(24, 192);
            this.edtAddrTo.Name = "edtAddrTo";
            this.edtAddrTo.Size = new System.Drawing.Size(145, 20);
            this.edtAddrTo.TabIndex = 7;
            this.edtAddrTo.Text = "wash";
            //
            // lblAddrTo
            //
            this.lblAddrTo.Location = new System.Drawing.Point(24, 176);
            this.lblAddrTo.Name = "lblAddrTo";
            this.lblAddrTo.Size = new System.Drawing.Size(40, 13);
            this.lblAddrTo.TabIndex = 6;
            this.lblAddrTo.Text = "&To";
            //
            // edtAddrFrom
            //
            this.edtAddrFrom.Location = new System.Drawing.Point(24, 136);
            this.edtAddrFrom.Name = "edtAddrFrom";
            this.edtAddrFrom.Size = new System.Drawing.Size(145, 20);
            this.edtAddrFrom.TabIndex = 5;
            this.edtAddrFrom.Text = "Chrys 1345";
            //
            // lblAddrFrom
            //
            this.lblAddrFrom.Location = new System.Drawing.Point(24, 120);
            this.lblAddrFrom.Name = "lblAddrFrom";
            this.lblAddrFrom.Size = new System.Drawing.Size(40, 13);
            this.lblAddrFrom.TabIndex = 4;
            this.lblAddrFrom.Text = "&From";
            //
            // trkHighways
            //
            this.trkHighways.AutoSize = false;
            this.trkHighways.LargeChange = 1;
            this.trkHighways.Location = new System.Drawing.Point(16, 88);
            this.trkHighways.Minimum = 1;
            this.trkHighways.Name = "trkHighways";
            this.trkHighways.Size = new System.Drawing.Size(161, 25);
            this.trkHighways.TabIndex = 3;
            this.trkHighways.Value = 5;
            //
            // lblHighways
            //
            this.lblHighways.Location = new System.Drawing.Point(24, 72);
            this.lblHighways.Name = "lblHighways";
            this.lblHighways.Size = new System.Drawing.Size(90, 13);
            this.lblHighways.TabIndex = 2;
            this.lblHighways.Text = "Prefer &highway";
            //
            // trkSmallRoads
            //
            this.trkSmallRoads.AutoSize = false;
            this.trkSmallRoads.LargeChange = 1;
            this.trkSmallRoads.Location = new System.Drawing.Point(16, 40);
            this.trkSmallRoads.Minimum = 1;
            this.trkSmallRoads.Name = "trkSmallRoads";
            this.trkSmallRoads.Size = new System.Drawing.Size(161, 25);
            this.trkSmallRoads.TabIndex = 1;
            this.trkSmallRoads.Value = 1;
            //
            // lblSmallRoads
            //
            this.lblSmallRoads.Location = new System.Drawing.Point(24, 24);
            this.lblSmallRoads.Name = "lblSmallRoads";
            this.lblSmallRoads.Size = new System.Drawing.Size(100, 13);
            this.lblSmallRoads.TabIndex = 0;
            this.lblSmallRoads.Text = "Prefer &local roads";
            //
            // GIS
            //
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 0);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Size = new System.Drawing.Size(404, 466);
            this.GIS.TabIndex = 1;
            //
            // GIS_ControlScale
            //
            this.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Control;
            this.GIS_ControlScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GIS_ControlScale.DividerColor1 = System.Drawing.Color.Black;
            this.GIS_ControlScale.DividerColor2 = System.Drawing.Color.White;
            this.GIS_ControlScale.GIS_Viewer = this.GIS;
            this.GIS_ControlScale.Location = new System.Drawing.Point(8, 8);
            this.GIS_ControlScale.Name = "GIS_ControlScale";
            this.GIS_ControlScale.PrepareEvent = null;
            this.GIS_ControlScale.Size = new System.Drawing.Size(145, 25);
            this.GIS_ControlScale.TabIndex = 1;
            tgiS_CSUnits1.DescriptionEx = null;
            this.GIS_ControlScale.Units = tgiS_CSUnits1;
            this.GIS_ControlScale.UnitsEPSG = 904201;
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS_ControlScale);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Geocoding & Routing";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Leave += new System.EventHandler(this.WinForm_Leave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkHighways)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSmallRoads)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Application entry point.  Configures visual styles and launches the form.
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
        /// Handles the form Load event.
        /// Opens the California street project, creates the route-display overlay
        /// layer, and initialises the geocoding and routing engines.
        /// </summary>
        /// <remarks>
        /// The viewer is locked during setup to suppress intermediate redraws —
        /// the standard DK pattern for multi-step viewer modifications.
        /// <see cref="TGIS_Utils.GisSamplesDataDirDownload"/> resolves the path
        /// to the TatukGIS sample data directory.
        /// </remarks>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            GIS.Lock();

            // Open the TatukGIS project which contains the California street data.
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\Projects\California.TTKPROJECT");

            // Retrieve the streets vector layer by name so the geocoding and
            // routing engines can operate directly on its shape and attribute data.
            layerSrc = (TGIS_LayerVector)(GIS.Get("streets"));

            if (layerSrc == null) return;

            // Zoom to the full extent of the street network on startup.
            GIS.VisibleExtent = layerSrc.Extent;

            // ------------------------------------------------------------------
            // Create an in-memory overlay layer for route visualisation.
            // UseConfig = false prevents the DK from persisting a style config.
            // The red line (Width = -2, negative = device pixels) and 1-px marker
            // outline are used for all copied route segment shapes.
            // ------------------------------------------------------------------
            layerRoute = new TGIS_LayerVector();
            layerRoute.UseConfig = false;
            layerRoute.Params.Line.Color = TGIS_Color.Red;
            layerRoute.Params.Line.Width = -2;
            layerRoute.Params.Marker.OutlineWidth = 1;
            layerRoute.Name = "RouteDisplay";
            layerRoute.CS = GIS.CS;  // Must share the viewer's coordinate system
            GIS.Add(layerRoute);

            // ------------------------------------------------------------------
            // Configure the geocoding engine with TIGER/Line field mappings.
            //   Offset    — how far along the segment to place the geocoded point
            //               (avoids placing it exactly on a road node).
            //   RoadName  — attribute field containing the full street name.
            //   LFrom/LTo — address number range for the left side of the street.
            //   RFrom/RTo — address number range for the right side of the street.
            //               TIGER/Line uses these four fields to interpolate the
            //               house number position along the segment.
            // ------------------------------------------------------------------
            geoObj = new TGIS_Geocoding(layerSrc);
            geoObj.Offset = 0.0001;
            geoObj.RoadName = "FULLNAME";
            geoObj.LFrom = "LFROMADD";
            geoObj.LTo = "LTOADD";
            geoObj.RFrom = "RFROMADD";
            geoObj.RTo = "RTOADD";

            // ------------------------------------------------------------------
            // Configure the routing engine.
            //   LoadTheData builds the Dijkstra graph from all arc shapes in
            //   layerSrc.  LinkTypeEvent classifies each segment so per-type cost
            //   modifiers can be applied.  RoadName labels each route step.
            // ------------------------------------------------------------------
            rtrObj = new TGIS_ShortestPath(GIS);
            rtrObj.LinkTypeEvent += new TGIS_LinkTypeEvent(doLinkType);
            rtrObj.LoadTheData(layerSrc);
            rtrObj.RoadName = "FULLNAME";

            GIS.Unlock();

            // Display the scale bar in miles (EPSG 9035 = statute mile).
            GIS_ControlScale.Units = TGIS_Utils.CSUnitsList.ByEPSG(9035);
        }

        /// <summary>
        /// Releases geocoding and routing objects when the form is closed.
        /// layerRoute is owned by the viewer and freed automatically.
        /// </summary>
        private void WinForm_Leave(object sender, System.EventArgs e)
        {
            layerRoute.Dispose();
            geoObj.Dispose();
            /*rtrObj.Dispose();*/
        }

        /// <summary>
        /// Classifies a road segment as highway (link type 0) or local road (link type 1).
        /// Called by <see cref="TGIS_ShortestPath"/> for every arc during graph construction.
        /// </summary>
        /// <remarks>
        /// The US Census MTFCC code "S1400" and above identify local/residential
        /// streets, alleys, and service roads.  Codes below "S1400" are interstates,
        /// US routes, state routes, and other major roads.
        ///
        /// The routing engine multiplies each segment's traversal cost by
        /// <c>CostModifiers[linkType]</c>.  A slider value of 10 for highways
        /// (CostModifiers[0] ≈ 0.09) makes major roads nearly free, routing the
        /// path onto them preferentially.
        /// </remarks>
        private void doLinkType(object _sender, TGIS_LinkTypeEventArgs _e)
        {
            if (!(_e.Shape.GetField("MTFCC").ToString().CompareTo("S1400") < 0))
                // MTFCC >= "S1400" → local/residential road
                _e.LinkType = 1;
            else
                // MTFCC < "S1400" → highway or major road
                _e.LinkType = 0;
        }

        /// <summary>
        /// Handles the Find Route button click.
        /// Geocodes both addresses, computes the shortest path between them,
        /// and displays the route as a red line with turn-by-turn directions.
        /// </summary>
        /// <remarks>
        /// Cost modifiers derived from the highway/local-road sliders are applied
        /// before routing so the user can tune road-type preference at runtime.
        /// The formula maps slider values 1–10 to cost multipliers 1.0–~0.09
        /// (lower multiplier = cheaper = preferred by the router).
        ///
        /// The <c>Compass</c> property of each route item encodes the turn
        /// direction: 0 = straight ahead, 1..3 = right (slight/right/sharp),
        /// 4 = U-turn, -1..-3 = left (slight/left/sharp), -4 = left U-turn.
        /// Consecutive steps on the same named street are suppressed.
        /// </remarks>
        private void btnRoute_Click(object sender, System.EventArgs e)
        {
            int i;
            TGIS_Shape shp;
            int res;
            TGIS_Point pt_a;
            TGIS_Point pt_b;
            string ang;
            string oldnam;

            // Apply slider-derived cost modifiers.
            // CostModifiers[0] = highway cost; CostModifiers[1] = local-road cost.
            rtrObj.set_CostModifiers(0, 1 - 1 / 11.0 * trkHighways.Value);
            rtrObj.set_CostModifiers(1, 1 - 1 / 11.0 * trkSmallRoads.Value);

            // Geocode the start address.
            // Parse() returns the number of matching candidates.
            // If candidates were found, refine the text box to the best match.
            res = geoObj.Parse(edtAddrFrom.Text);
            if (res > 0)
                edtAddrFrom.Text = geoObj.get_Query(0);
            else
                edtAddrFrom.AppendText(" ???");

            // Abort if the start address could not be resolved.
            if (res <= 0) return;
            pt_a = geoObj.get_Point(0);   // Geocoded start coordinate

            // Geocode the end address.
            res = geoObj.Parse(edtAddrTo.Text);
            if (res > 0) edtAddrTo.Text = geoObj.get_Query(0);
            else edtAddrTo.AppendText(" ???");

            // Abort if the end address could not be resolved.
            if (res <= 0) return;
            pt_b = geoObj.get_Point(0);   // Geocoded end coordinate

            // UpdateTheData refreshes the graph (e.g. after data edits).
            // Find() computes the shortest path; Unproject converts viewer
            // display coordinates to the layer's native coordinate system.
            rtrObj.UpdateTheData();
            rtrObj.Find(layerRoute.Unproject(pt_a), layerRoute.Unproject(pt_b));

            memRoute.Clear();
            oldnam = "#$@3eqewe";  // Sentinel to detect the first direction step

            // Build turn-by-turn directions text.
            ang = "";
            for (i = 0; i < rtrObj.ItemsCount; i++)
            {
                // Map the numeric Compass value to a readable direction abbreviation.
                switch (rtrObj.get_Items(i).Compass)
                {
                    case 0:  ang = "FWD  "; break;
                    case 1:  ang = "RIGHT"; break;   // Slight right
                    case 2:  ang = "RIGHT"; break;   // Right
                    case 3:  ang = "RIGHT"; break;   // Sharp right
                    case 4:  ang = "BACK "; break;   // U-turn
                    case -1: ang = "LEFT "; break;   // Slight left
                    case -2: ang = "LEFT "; break;   // Left
                    case -3: ang = "LEFT "; break;   // Sharp left
                    case -4: ang = "BACK "; break;   // Left U-turn
                }

                // Suppress consecutive items on the same named street.
                if (oldnam == rtrObj.get_Items(i).Name) continue;
                oldnam = rtrObj.get_Items(i).Name;

                memRoute.AppendText(ang + " " + rtrObj.get_Items(i).Name + "\r\n");
            }

            // Clear the route display layer and repopulate with the new path.
            layerRoute.RevertShapes();

            // Copy each route segment from the source layer into the display layer.
            for (i = 0; i < rtrObj.ItemsCount; i++)
            {
                shp = rtrObj.get_Items(i).Layer.GetShape(rtrObj.get_Items(i).Uid);
                if (shp == null) continue;
                layerRoute.AddShape(shp);
                if (i == 0)
                    layerRoute.Extent = shp.Extent;  // Seed the bounding extent
            }

            // Place a green point marker at the start position (pt_a).
            shp = layerRoute.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(pt_a);
            shp.Params.Marker.Color = TGIS_Color.Green;
            shp.Unlock();

            // Place a red (default) point marker at the end position (pt_b).
            shp = layerRoute.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(pt_b);
            shp.Unlock();

            // Zoom to the route bounding box, pulling back slightly (0.7×) for context.
            GIS.Lock();
            GIS.VisibleExtent = layerRoute.Extent;
            GIS.Zoom = 0.7 * GIS.Zoom;
            GIS.Unlock();
        }

        /// <summary>
        /// Handles the Find Address button click.
        /// Geocodes the address typed in <c>edtAddrFrom</c> and highlights all
        /// matching street segments on the map with a green point marker at each
        /// geocoded coordinate.
        /// </summary>
        /// <remarks>
        /// <c>Parse()</c> returns the number of matching candidates found in the
        /// street data.  Each candidate provides a <c>Query</c> (canonical name),
        /// a <c>Uid</c> (shape ID of the primary segment), and a <c>UidEx</c>
        /// (shape ID of the complementary segment on the opposite side of the road).
        /// A green marker is placed at <c>Point[i]</c>, the interpolated coordinate
        /// for the house number along the matched segment.
        /// </remarks>
        private void btnResolve_Click(object sender, System.EventArgs e)
        {
            int i;
            int r;
            TGIS_Shape shp;

            if (geoObj == null) return;

            // Clear any previously displayed route or address highlights.
            layerRoute.RevertShapes();

            // Parse returns the number of matches; r is the last valid index.
            // Append "???" if no match was found.
            r = geoObj.Parse(edtAddrFrom.Text) - 1;
            if (r <= 0)
                edtAddrFrom.AppendText(" ???");

            for (i = 0; i <= r; i++)
            {
                // Refine the text box to the canonical name of each matched street.
                edtAddrFrom.Text = geoObj.get_Query(i);
                Application.DoEvents();  // Allow the UI to refresh between results

                // Add the primary matching segment to the route display layer.
                shp = layerSrc.GetShape(geoObj.get_Uid(i));
                layerRoute.AddShape(shp);

                // Seed the bounding extent from the first matched segment.
                if (i == 0)
                    layerRoute.Extent = shp.ProjectedExtent;

                // Add the complementary (opposite-side) segment if available.
                shp = layerSrc.GetShape(geoObj.get_UidEx(i));
                if (shp != null)
                    layerRoute.AddShape(shp);

                // Place a green marker at the interpolated geocoded coordinate.
                // FromCS converts from the source layer's CS to the route layer's CS.
                // Lock/Unlock brackets shape modification to maintain internal extent.
                shp = layerRoute.CreateShape(TGIS_ShapeType.Point);
                shp.Lock(TGIS_Lock.Extent);
                shp.AddPart();
                shp.AddPoint(layerRoute.CS.FromCS(layerSrc.CS, geoObj.get_Point(i)));
                shp.Params.Marker.Color = TGIS_Color.Green;
                shp.Unlock();
            }

            // Zoom to the matched results with a slight zoom-out factor for context.
            GIS.Lock();
            GIS.VisibleExtent = layerRoute.ProjectedExtent;
            GIS.Zoom = 0.7 * GIS.Zoom;
            GIS.Unlock();
        }
    }
}
