// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// Topology Sample - Demonstrates polygon set-algebra operations using TGIS_Topology.
//
// This sample loads two overlapping polygon shapes (A and B) from a shapefile
// and allows the user to compute the following topological combinations:
//
//   A + B   : Union            - area covered by either A or B (or both)
//   A * B   : Intersection     - area covered by both A and B
//   A - B   : Difference       - area in A but not in B
//   B - A   : Difference       - area in B but not in A
//   A xor B : SymDifference    - area in A or B but not in both (exclusive OR)
//
// Results are rendered in a separate in-memory TGIS_LayerVector layer coloured
// red with 50% transparency so the original shapes remain visible underneath.
//
// Key TatukGIS NDK classes used:
//   TGIS_Topology            - engine that performs polygon boolean operations
//   TGIS_TopologyCombineType - enumeration of the five combine modes
//   TGIS_LayerVector         - in-memory vector layer used to display results
//   TGIS_ShapePolygon        - strongly-typed polygon shape handle
//   TGIS_ViewerWnd           - map viewer WinForms control
// =============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Topology
{
    /// <summary>
    /// Main form for the Topology sample application.
    /// Demonstrates the five polygon boolean (set-algebra) operations supported
    /// by <see cref="TGIS_Topology"/>: Union, Intersection, Difference (A-B),
    /// Difference (B-A), and Symmetrical Difference (XOR).
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.Container components = null;

        // --- TatukGIS viewer and operation controls ---
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;   // Map viewer
        private System.Windows.Forms.Button btnAplusB;        // Union
        private System.Windows.Forms.Button btnAmultB;        // Intersection
        private System.Windows.Forms.Button btnAminusB;       // A minus B
        private System.Windows.Forms.Button btnBminusA;       // B minus A
        private System.Windows.Forms.Button btnAxorB;         // Symmetrical difference
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.Panel panel1;

        // --- State fields ---

        /// <summary>
        /// The topology engine used to compute polygon boolean operations.
        /// Stateless and reusable across multiple Combine() calls.
        /// </summary>
        private TGIS_Topology topologyObj;

        /// <summary>
        /// In-memory vector layer that receives and displays the computed result shape.
        /// Its shapes are cleared before every new operation via RevertShapes().
        /// </summary>
        private TGIS_LayerVector layerObj;

        /// <summary>Source polygon A – loaded from shape index 1 of the sample shapefile.</summary>
        private TGIS_ShapePolygon shpA;

        /// <summary>Source polygon B – loaded from shape index 2 of the sample shapefile.</summary>
        private TGIS_ShapePolygon shpB;

        /// <summary>
        /// Initialises WinForms designer components.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnAplusB = new System.Windows.Forms.Button();
            this.btnAmultB = new System.Windows.Forms.Button();
            this.btnAminusB = new System.Windows.Forms.Button();
            this.btnBminusA = new System.Windows.Forms.Button();
            this.btnAxorB = new System.Windows.Forms.Button();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            //
            // GIS
            //
            this.GIS.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 25);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(592, 422);
            this.GIS.TabIndex = 0;
            this.GIS.UseRTree = false;
            //
            // btnAplusB
            //
            this.btnAplusB.Location = new System.Drawing.Point(0, 1);
            this.btnAplusB.Name = "btnAplusB";
            this.btnAplusB.Size = new System.Drawing.Size(75, 23);
            this.btnAplusB.TabIndex = 1;
            this.btnAplusB.Text = "A + B";
            this.btnAplusB.Click += new System.EventHandler(this.btnAplusB_Click);
            //
            // btnAmultB
            //
            this.btnAmultB.Location = new System.Drawing.Point(75, 1);
            this.btnAmultB.Name = "btnAmultB";
            this.btnAmultB.Size = new System.Drawing.Size(75, 23);
            this.btnAmultB.TabIndex = 2;
            this.btnAmultB.Text = "A * B";
            this.btnAmultB.Click += new System.EventHandler(this.btnAmultB_Click);
            //
            // btnAminusB
            //
            this.btnAminusB.Location = new System.Drawing.Point(150, 1);
            this.btnAminusB.Name = "btnAminusB";
            this.btnAminusB.Size = new System.Drawing.Size(75, 23);
            this.btnAminusB.TabIndex = 3;
            this.btnAminusB.Text = "A - B";
            this.btnAminusB.Click += new System.EventHandler(this.btnAminusB_Click);
            //
            // btnBminusA
            //
            this.btnBminusA.Location = new System.Drawing.Point(225, 1);
            this.btnBminusA.Name = "btnBminusA";
            this.btnBminusA.Size = new System.Drawing.Size(75, 23);
            this.btnBminusA.TabIndex = 4;
            this.btnBminusA.Text = "B - A";
            this.btnBminusA.Click += new System.EventHandler(this.btnBminusA_Click);
            //
            // btnAxorB
            //
            this.btnAxorB.Location = new System.Drawing.Point(300, 1);
            this.btnAxorB.Name = "btnAxorB";
            this.btnAxorB.Size = new System.Drawing.Size(75, 23);
            this.btnAxorB.TabIndex = 5;
            this.btnAxorB.Text = "A xor B";
            this.btnAxorB.Click += new System.EventHandler(this.btnAxorB_Click);
            //
            // stripBar1
            //
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 6;
            //
            // panel1
            //
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 25);
            this.panel1.TabIndex = 7;
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.btnAxorB);
            this.Controls.Add(this.btnBminusA);
            this.Controls.Add(this.btnAminusB);
            this.Controls.Add(this.btnAmultB);
            this.Controls.Add(this.btnAplusB);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Topology";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Leave += new System.EventHandler(this.WinForm_Leave);
            this.ResumeLayout(false);

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
        /// Initialises the map viewer on form load:
        /// opens the sample shapefile, extracts the two source polygons as editable
        /// copies, and adds a transparent result layer to the viewer.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;

            // Create the topology engine (stateless; safe to reuse across calls)
            topologyObj = new TGIS_Topology();

            GIS.Lock();  // Suspend repaints while modifying the viewer

            // Open the bundled topology sample shapefile that contains two overlapping polygons
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\Topology\topology.shp");

            // Retrieve the first (and only) vector layer from the viewer
            ll = (TGIS_LayerVector)GIS.Items[0];
            if (ll == null) return;

            // MakeEditable returns a detached, editable copy of each shape so the
            // topology engine can safely access the geometry without modifying the layer.
            shpA = (TGIS_ShapePolygon)ll.GetShape(1).MakeEditable();
            if (shpA == null) return;

            shpB = (TGIS_ShapePolygon)ll.GetShape(2).MakeEditable();
            if (shpB == null) return;

            // Create a blank in-memory layer for displaying the operation result.
            // Placing it on top of the source layer in the viewer stack.
            layerObj = new TGIS_LayerVector();
            layerObj.Name = "output";
            layerObj.Transparency = 50;                    // 50% transparent so source shows through
            layerObj.Params.Area.Color = TGIS_Color.Red;   // Result polygon fill colour

            GIS.Add(layerObj);

            GIS.Unlock();       // Resume painting
            GIS.FullExtent();   // Zoom to fit all layers
        }

        /// <summary>
        /// Releases the topology engine when the form loses focus.
        /// </summary>
        private void WinForm_Leave(object sender, System.EventArgs e)
        {
            if (topologyObj != null)
                topologyObj = null;
        }

        /// <summary>
        /// Computes the Union of A and B (A + B).
        /// The resulting shape covers all area contained in either polygon,
        /// merging any overlapping regions into a single contiguous polygon.
        /// </summary>
        private void btnAplusB_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape tmp;

            // Clear the previous result before computing the new one
            layerObj.RevertShapes();
            tmp = topologyObj.Combine(shpA, shpB,
                                       TGIS_TopologyCombineType.Union
                                     );
            if (tmp != null)
            {
                layerObj.AddShape(tmp);  // Add the result shape to the output layer
                tmp = null;
            }
            GIS.InvalidateWholeMap();  // Repaint the viewer
        }

        /// <summary>
        /// Computes the Intersection of A and B (A * B).
        /// The resulting shape covers only the area that is simultaneously inside
        /// both polygon A and polygon B.
        /// </summary>
        private void btnAmultB_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape tmp;

            layerObj.RevertShapes();
            tmp = topologyObj.Combine(shpA, shpB,
                                       TGIS_TopologyCombineType.Intersection
                                     );
            if (tmp != null)
            {
                layerObj.AddShape(tmp);
                tmp = null;
            }
            GIS.InvalidateWholeMap();
        }

        /// <summary>
        /// Computes the Difference A minus B (A - B).
        /// The resulting shape is the area of polygon A with the overlap of B removed.
        /// The first argument is the "base" shape; the second is subtracted from it.
        /// </summary>
        private void btnAminusB_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape tmp;

            layerObj.RevertShapes();
            tmp = topologyObj.Combine(shpA, shpB,
                                       TGIS_TopologyCombineType.Difference
                                     );
            if (tmp != null)
            {
                layerObj.AddShape(tmp);
                tmp = null;
            }
            GIS.InvalidateWholeMap();
        }

        /// <summary>
        /// Computes the Difference B minus A (B - A).
        /// Identical logic to A-B but with operands swapped:
        /// the area of A is subtracted from the area of B.
        /// </summary>
        private void btnBminusA_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape tmp;

            layerObj.RevertShapes();
            // Note: shpB is the first (base) argument; shpA is what gets subtracted
            tmp = topologyObj.Combine(shpB, shpA,
                                       TGIS_TopologyCombineType.Difference
                                     );
            if (tmp != null)
            {
                layerObj.AddShape(tmp);
                tmp = null;
            }
            GIS.InvalidateWholeMap();
        }

        /// <summary>
        /// Computes the Symmetrical Difference of A and B (A xor B).
        /// The result covers area belonging to exactly one of the two polygons –
        /// equivalent to Union minus Intersection, or a logical XOR of the two areas.
        /// </summary>
        private void btnAxorB_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape tmp;

            layerObj.RevertShapes();
            tmp = topologyObj.Combine(shpA, shpB,
                                       TGIS_TopologyCombineType.SymmetricalDifference
                                     );
            if (tmp != null)
            {
                layerObj.AddShape(tmp);
                tmp = null;
            }
            GIS.InvalidateWholeMap();
        }
    }
}
