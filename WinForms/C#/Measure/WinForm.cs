/* Measure sample — demonstrates interactive distance and area measurement on a map.

   What the sample shows:
     - Creating an in-memory TGIS_LayerVector to hold temporary measurement shapes
     - Using TGIS_ViewerWnd.Editor to create and track polyline and polygon shapes
     - Responding to EditorChangeEvent for live measurement updates
     - Using TGIS_CSUnits.AsLinear and AsAreal for human-readable output formatting
     - Polyline distance measurement with geodetic accuracy
     - Polygon area measurement with coordinate system awareness
     - Toggling between Line (distance) and Polygon (area) measurement modes
     - Real-time display of measurements as user places vertices
     - Clear button to reset and start new measurement

   Key TatukGIS API concepts shown here:
     TGIS_ViewerWnd              - main visual map control
     TGIS_ViewerWnd.Editor       - in-place shape creation and editing
     TGIS_LayerVector            - in-memory measurement shape layer
     TGIS_Shape                  - polyline/polygon measurement geometry
     TGIS_CSUnits                - unit formatting and conversion
     EditorChangeEvent           - live measurement update trigger
     TGIS_ViewerMode.Select/Edit/Drag - interaction mode transitions
     EPSG 904201                 - metric unit set for geodetic calculations
     EPSG 4326                   - WGS-84 geographic coordinate system
*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Measure
{
    /// <summary>
    /// Main application form for the Measure sample.
    /// Hosts the TatukGIS viewer and provides controls to measure line lengths
    /// and polygon areas directly on the map.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.Container components = null;

        // Status bar shown at the bottom of the form with usage instructions.
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;

        /// <summary>
        /// In-memory vector layer used to hold the temporary measurement shape.
        /// No file is backed; the shape exists only for the current session.
        /// </summary>
        private TGIS_LayerVector ll;

        // Mode flags — exactly one may be true at a time.
        private Boolean isLine;    // Polyline (distance) measurement is active
        private Boolean isPolygon; // Polygon (area) measurement is active

        private Panel panel1;  // Toolbar panel: holds the three action buttons
        private Panel panel2;  // Results panel: holds the length and area displays

        /// <summary>The TatukGIS map viewer control.</summary>
        private TGIS_ViewerWnd GIS;

        private TextBox tbArea;    // Displays the measured area (polygon mode only)
        private TextBox tbLength;  // Displays the measured length / perimeter
        private Label lblArea;
        private Label lblLength;
        private Button btnLine;    // Start polyline measurement
        private Button btnPolygon; // Start polygon measurement
        private Button btnClear;   // Clear current measurement and return to drag mode

        /// <summary>
        /// Unit formatter used to convert raw SI values (metres / m²) into
        /// labelled strings.  Initialised to EPSG 904201 (metric).
        /// </summary>
        private TGIS_CSUnits unit;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPolygon = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            //
            // stripBar1
            //
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(686, 19);
            this.stripBar1.TabIndex = 0;
            //
            // toolStripLabel1
            //
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Use left mouse button to measure";
            this.toolStripLabel1.Width = 669;
            //
            // panel1
            //
            this.panel1.Controls.Add(this.btnPolygon);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnLine);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 27);
            this.panel1.TabIndex = 0;
            //
            // panel2
            //
            this.panel2.Controls.Add(this.tbArea);
            this.panel2.Controls.Add(this.tbLength);
            this.panel2.Controls.Add(this.lblArea);
            this.panel2.Controls.Add(this.lblLength);
            this.panel2.Location = new System.Drawing.Point(474, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 396);
            this.panel2.TabIndex = 1;
            //
            // tbArea
            //
            this.tbArea.Location = new System.Drawing.Point(6, 99);
            this.tbArea.Name = "tbArea";
            this.tbArea.Size = new System.Drawing.Size(191, 20);
            this.tbArea.TabIndex = 3;
            //
            // tbLength
            //
            this.tbLength.Location = new System.Drawing.Point(6, 32);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(191, 20);
            this.tbLength.TabIndex = 2;
            //
            // lblArea
            //
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(3, 83);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(32, 13);
            this.lblArea.TabIndex = 1;
            this.lblArea.Text = "Area:";
            //
            // lblLength
            //
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(3, 16);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(43, 13);
            this.lblLength.TabIndex = 0;
            this.lblLength.Text = "Length:";
            //
            // GIS
            //
            this.GIS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraPosition = null;
            this.GIS.CursorForCameraRotation = null;
            this.GIS.CursorForCameraXY = null;
            this.GIS.CursorForCameraXYZ = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.CursorForSunPosition = null;
            this.GIS.Location = new System.Drawing.Point(15, 45);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(453, 396);
            this.GIS.TabIndex = 2;
            this.GIS.UseRTree = false;
            this.GIS.EditorChangeEvent += new System.EventHandler(this.GIS_EditorChangeEvent);
            this.GIS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseClick);
            //
            // btnLine
            //
            this.btnLine.Location = new System.Drawing.Point(3, 0);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(75, 23);
            this.btnLine.TabIndex = 5;
            this.btnLine.Text = "By line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(165, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // btnPolygon
            //
            this.btnPolygon.Location = new System.Drawing.Point(84, 0);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(75, 23);
            this.btnPolygon.TabIndex = 7;
            this.btnPolygon.Text = "By polygon";
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(686, 466);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Measure";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Application entry point.
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
        /// Initialises the map viewer: loads a world basemap, creates the
        /// in-memory measurement layer, and configures the shape editor.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            // Lock suspends rendering while multiple map changes are made,
            // preventing partial redraws during initialisation.
            GIS.Lock();

            // Load a world outline shapefile as the basemap.
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\WorldDCW\world.shp");

            // Create an in-memory vector layer to hold the measurement shape.
            // Because it has no file path, shapes exist only in memory.
            ll = new TGIS_LayerVector();
            ll.Params.Line.Color = TGIS_Color.Red;  // Red so the measurement stands out
            ll.Params.Line.Width = 25;               // Thick line for visibility

            // Assign WGS-84 geographic CS (EPSG 4326).
            // This is essential: with a geographic CS, LengthCS() and AreaCS()
            // compute geodetically correct distances on the Earth's surface.
            ll.SetCSByEPSG( 4326 );

            GIS.Add(ll);

            // Prevent the user from scrolling outside the initial world extent.
            GIS.RestrictedExtent = GIS.Extent;

            GIS.Unlock();

            // EPSG 904201 selects the TatukGIS standard metric unit formatter
            // (kilometres for linear, square kilometres for areal measurements).
            unit = TGIS_Utils.CSUnitsList.ByEPSG( 904201 );

            isLine = false;
            isPolygon = false;

            // Make the rubber-band preview line thicker for better visibility.
            GIS.Editor.EditingLinesStyle.PenWidth = 10;

            // AfterActivePoint draws a preview segment from the last committed
            // vertex to the current cursor position, giving live length feedback.
            GIS.Editor.Mode = TGIS_EditorMode.AfterActivePoint;
        }

        /// <summary>
        /// Prepares the viewer for polygon area measurement.
        /// Discards any existing shape, clears the result fields, and sets
        /// GIS.Mode to Select so the next mouse click starts a new Polygon.
        /// </summary>
        private void btnPolygon_Click(object sender, EventArgs e)
        {
            // Remove any in-progress measurement shape.
            GIS.Editor.DeleteShape();
            GIS.Editor.EndEdit();

            tbArea.Text = "";
            tbLength.Text = "";

            // Both perimeter and area will be displayed in this mode.
            isPolygon = true;
            isLine = false;

            // Waiting for the first click that anchors the polygon's first vertex.
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        /// <summary>
        /// Prepares the viewer for polyline distance measurement.
        /// Discards any existing shape, clears the result fields, and sets
        /// GIS.Mode to Select so the next mouse click starts a new Arc.
        /// </summary>
        private void btnLine_Click(object sender, EventArgs e)
        {
            // Remove any in-progress measurement shape.
            GIS.Editor.DeleteShape();
            GIS.Editor.EndEdit();

            tbArea.Text = "";
            tbLength.Text = "";

            // Only the length field will be updated; the area field stays empty.
            isPolygon = false;
            isLine = true;

            // Waiting for the first click that anchors the polyline's first vertex.
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        /// <summary>
        /// Clears the current measurement shape, resets the result fields,
        /// and returns the viewer to drag/pan mode.
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            GIS.Editor.DeleteShape();
            GIS.Editor.EndEdit();

            tbArea.Text = "";
            tbLength.Text = "";

            // Return to normal pan/zoom interaction.
            GIS.Mode = TGIS_ViewerMode.Drag;
        }

        /// <summary>
        /// Fired by the editor every time a vertex is placed or the mouse moves
        /// while editing.  Recomputes and displays the current length and, for
        /// polygons, the enclosed area.
        /// </summary>
        private void GIS_EditorChangeEvent(object sender, EventArgs e)
        {
            if (GIS.Editor.CurrentShape != null)
            {
                if (isLine)
                {
                    // LengthCS() returns the geodetic cumulative length of all
                    // polyline segments in the layer's native units (metres for
                    // EPSG 4326).  AsLinear formats it with the unit label.
                    tbLength.Text = unit.AsLinear(((TGIS_Shape)GIS.Editor.CurrentShape).LengthCS(), true);
                }
                else if (isPolygon)
                {
                    // For a polygon, LengthCS() is the perimeter.
                    tbLength.Text = unit.AsLinear(((TGIS_Shape)GIS.Editor.CurrentShape).LengthCS(), true);
                    // AreaCS() returns the geodetic enclosed area in square metres.
                    // "%s²" inserts the unit abbreviation followed by a superscript-2.
                    tbArea.Text = unit.AsAreal(((TGIS_Shape)GIS.Editor.CurrentShape).AreaCS(), true, "%s²");
                }
            }
        }

        /// <summary>
        /// Handles the first mouse click on the map that creates a new measurement
        /// shape.  Once the shape is created the viewer switches to Edit mode and
        /// the editor handles all subsequent vertex placements internally.
        /// </summary>
        private void GIS_MouseClick(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;

            // If the editor is already active, additional clicks are handled by
            // the editor itself — do not create a second shape.
            if (GIS.Mode == TGIS_ViewerMode.Edit)
                return;

            // Convert the screen pixel position to geographic map coordinates.
            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));

            if (isLine)
            {
                // Arc = polyline; LengthCS() will sum all segment lengths geodetically.
                GIS.Editor.CreateShape(ll, ptg, TGIS_ShapeType.Arc);
            }
            else if (isPolygon)
            {
                // Polygon; both AreaCS() and LengthCS() (perimeter) are available.
                GIS.Editor.CreateShape(ll, ptg, TGIS_ShapeType.Polygon);
            }

            // Switch to Edit mode so the editor owns all subsequent mouse clicks.
            GIS.Mode = TGIS_ViewerMode.Edit;
        }
    }
}
