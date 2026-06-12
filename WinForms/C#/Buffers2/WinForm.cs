//=============================================================================
// This source code is a part of TatukGIS Developer Kernel.
//=============================================================================
/*
  Buffers2 - Advanced buffer operation with spatial intersection query.

  This sample extends the Buffers1 concept by demonstrating how to combine
  TGIS_Topology.MakeBuffer with a spatial search to find all features that
  intersect the resulting buffer polygon.

  What the sample shows:
    - Loading a real-world county shapefile (California counties) into the viewer
    - Creating a separate in-memory overlay layer (TGIS_LayerVector) to hold
      the buffer polygon, styled with a semi-transparent yellow fill
    - Using TGIS_LayerVector.FindFirst with an attribute filter (NAME='Merced')
      to locate a specific county as the buffer source shape
    - Computing a planar buffer around that county with TGIS_Topology.MakeBuffer;
      the distance is trackBar1.Value / 100 (converts the 0..200 integer range
      to 0..2 degrees in the geographic CRS)
    - Performing a two-stage spatial intersection query:
        Stage 1 - FindFirst(buf.Extent): bounding-box pre-filter (fast)
        Stage 2 - buf.IsCommonPoint(tmp): precise geometric overlap test
    - Marking intersecting counties blue (via MakeEditable + Params.Area.Color)
      and listing their names in a text box
    - Using a Timer (250 ms interval) to debounce rapid slider movements so
      the expensive query only runs after the user stops dragging

  Key TatukGIS API concepts shown here:
    TGIS_ViewerWnd      - the main visual map control
    TGIS_LayerVector    - a file-backed or in-memory vector layer
    TGIS_Topology       - utility class for spatial operations (MakeBuffer, etc.)
    TGIS_Shape          - a single geographic feature (point, line, or polygon)
    FindFirst / FindNext - iterator pair for querying shapes within a layer
    IsCommonPoint       - precise overlap test for determining shape intersection
    MakeEditable        - returns an editable copy of a read-only shape
*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Buffers2
{
    /// <summary>
    /// Main form for the Buffers2 sample.
    ///
    /// Loads California county data, computes a buffer around Merced County at
    /// a distance controlled by a slider, then highlights every county that
    /// intersects the buffer and lists their names in a text box.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        // ── designer / infrastructure fields ─────────────────────────────────
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnMinus;  // decrease distance by 25
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip stripBar1;     // shows distance in km
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;      // main map viewer
        private System.Windows.Forms.TextBox textBox1;          // lists intersecting counties
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStrip toolBar2;
        private System.Windows.Forms.TrackBar trackBar1;        // 0..200 buffer distance slider
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStrip toolBar3;
        private System.Windows.Forms.ToolStripButton btnPlus;   // increase distance by 25
        private System.Windows.Forms.Timer timer1;              // debounce: 250 ms
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnMinus = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.toolBar3 = new System.Windows.Forms.ToolStrip();
            this.btnPlus = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.toolBar2 = new System.Windows.Forms.ToolStrip();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            //
            // toolStrip1
            //
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnMinus});
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(23, 25);
            this.toolStrip1.TabIndex = 0;
            //
            // btnMinus
            //
            this.btnMinus.ImageIndex = 0;
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Click += toolStrip1_ButtonClick;
            //
            // imageList1
            //
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            //
            // stripBar1
            //
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            //
            // toolStripLabel1
            //
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Width = 575;
            //
            // GIS
            //
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraPosition = null;
            this.GIS.CursorForCameraRotation = null;
            this.GIS.CursorForCameraXY = null;
            this.GIS.CursorForCameraXYZ = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.CursorForSunPosition = null;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 25);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(477, 422);
            this.GIS.TabIndex = 3;
            this.GIS.UseRTree = false;
            //
            // textBox1 - read-only panel listing intersecting county names
            //
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBox1.Location = new System.Drawing.Point(477, 25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(115, 422);
            this.textBox1.TabIndex = 2;
            //
            // panel1
            //
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 25);
            this.panel1.TabIndex = 4;
            //
            // panel5
            //
            this.panel5.Controls.Add(this.toolBar3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(264, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(328, 25);
            this.panel5.TabIndex = 2;
            //
            // toolBar3
            //
            this.toolBar3.AutoSize = false;
            this.toolBar3.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnPlus});
            this.toolBar3.ImageList = this.imageList1;
            this.toolBar3.Location = new System.Drawing.Point(0, 0);
            this.toolBar3.Name = "toolBar3";
            this.toolBar3.ShowItemToolTips = true;
            this.toolBar3.Size = new System.Drawing.Size(328, 25);
            this.toolBar3.TabIndex = 0;
            //
            // btnPlus
            //
            this.btnPlus.ImageIndex = 1;
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Click += toolBar3_ButtonClick;
            //
            // panel3
            //
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(23, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(241, 25);
            this.panel3.TabIndex = 1;
            //
            // panel4
            //
            this.panel4.Controls.Add(this.trackBar1);
            this.panel4.Controls.Add(this.toolBar2);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(241, 25);
            this.panel4.TabIndex = 0;
            //
            // trackBar1 - buffer distance slider (0..200, divided by 100 = degrees)
            //
            this.trackBar1.Location = new System.Drawing.Point(0, 2);
            this.trackBar1.Maximum = 200;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(241, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            //
            // toolBar2
            //
            this.toolBar2.Location = new System.Drawing.Point(0, 0);
            this.toolBar2.Name = "toolBar2";
            this.toolBar2.ShowItemToolTips = true;
            this.toolBar2.Size = new System.Drawing.Size(241, 42);
            this.toolBar2.TabIndex = 0;
            //
            // panel2
            //
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(23, 25);
            this.panel2.TabIndex = 0;
            //
            // timer1 - 250 ms debounce timer
            //
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Buffers2";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
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
        /// Form load handler - opens the California counties shapefile and creates
        /// the buffer overlay layer.
        ///
        /// Steps:
        ///   1. Call TGIS_Utils.GisCreateLayer to open the shapefile; this factory
        ///      returns the correct TGIS_LayerVector subtype for the SHP format and
        ///      gives the layer the logical name "counties" used later by GIS.Get.
        ///   2. Lock the viewer, add the county layer, then add an empty in-memory
        ///      "buffer" layer (70 % transparent, yellow fill).
        ///   3. Assign the viewer's CS to the buffer layer so buffer distances are
        ///      interpreted in the same coordinate system as the county data.
        ///   4. Unlock, zoom to full extent, and immediately trigger the first
        ///      buffer computation by calling timer1_Tick directly.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            TGIS_LayerAbstract la;  // the file-backed county layer
            TGIS_LayerVector lb;    // in-memory buffer overlay layer

            la = TGIS_Utils.GisCreateLayer("counties",
                   TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\USA\States\California\Counties.shp"
                 );
            GIS.Lock();
            GIS.Add(la);

            // Buffer overlay: 70 % transparent yellow so the counties show through.
            lb = new TGIS_LayerVector();
            lb.Name = "buffer";
            lb.Transparency = 70;
            lb.Params.Area.Color = TGIS_Color.Yellow;
            // Assign the viewer CS so spatial distances are in the correct units.
            lb.CS = GIS.CS;
            GIS.Add(lb);

            GIS.Unlock();
            GIS.FullExtent();
            // Run the initial buffer query at slider position 0.
            timer1_Tick(sender, e);
        }

        /// <summary>
        /// Minus button handler: decrements the slider by 25 steps (clamped to minimum)
        /// and immediately triggers a buffer recompute.
        ///
        /// Steps of 25 are used here (vs. 1 in Buffers1) because the track-bar range
        /// is 0..200 and a coarser step provides a more responsive interaction.
        /// </summary>
        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (trackBar1.Value > trackBar1.Minimum + 25)
            {
                trackBar1.Value -= 25;
                timer1_Tick(this, e);
            }
            else if (trackBar1.Value > trackBar1.Minimum)
            {
                // Clamp to the minimum rather than going below it.
                trackBar1.Value = trackBar1.Minimum;
                timer1_Tick(this, e);
            }
        }

        /// <summary>
        /// Plus button handler: increments the slider by 25 steps (clamped to maximum)
        /// and immediately triggers a buffer recompute.
        /// </summary>
        private void toolBar3_ButtonClick(object sender, System.EventArgs e)
        {
            if (trackBar1.Value < trackBar1.Maximum - 25)
            {
                trackBar1.Value += 25;
                timer1_Tick(this, e);
            }
            else if (trackBar1.Value < trackBar1.Maximum)
            {
                // Clamp to the maximum rather than exceeding it.
                trackBar1.Value = trackBar1.Maximum;
                timer1_Tick(this, e);
            }
        }

        /// <summary>
        /// Core buffer and intersection logic, fired by the debounce timer.
        ///
        /// The timer is disabled at the start so that rapid slider movement does not
        /// queue multiple overlapping queries - only the final resting position of the
        /// slider triggers an actual computation.
        ///
        /// Algorithm:
        ///   1. Retrieve the "counties" and "buffer" layers by name.
        ///   2. Use FindFirst with attribute filter "NAME='Merced'" to locate the
        ///      source county shape anywhere in the world extent.
        ///   3. Call TGIS_Topology.MakeBuffer: distance = trackBar1.Value / 100.0
        ///      (converts the 0..200 integer to 0..2 geographic degrees).
        ///   4. Store the buffer in the overlay layer; reset per-shape colours on
        ///      the county layer (RevertShapes + IgnoreShapeParams = false).
        ///   5. Two-stage spatial query:
        ///        FindFirst(buf.Extent)   - bounding-box pre-filter (fast index scan)
        ///        buf.IsCommonPoint(tmp)  - precise geometric intersection test
        ///   6. Matching counties are made editable, coloured blue, and their names
        ///      appended to textBox1.
        ///   7. GIS.InvalidateWholeMap redraws the display in the finally block.
        /// </summary>
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;   // the county source layer
            TGIS_LayerVector lb;   // the buffer overlay layer
            TGIS_Shape shp;        // the Merced county shape (buffer source)
            TGIS_Shape tmp;        // iterator shape in FindFirst/FindNext loop
            TGIS_Shape buf;        // the computed buffer polygon stored in lb
            TGIS_Topology tpl;     // topology engine

            // Disable the timer so it does not fire again while processing.
            timer1.Enabled = false;

            try
            {
                // Retrieve layers by their logical names.
                ll = (TGIS_LayerVector)GIS.Get("counties");
                if (ll == null) return;

                lb = (TGIS_LayerVector)GIS.Get("buffer");
                if (lb == null) return;

                // Find "Merced" county by attribute filter over the whole-world extent.
                // GisWholeWorld() returns a TGIS_Extent that encompasses all coordinates,
                // so no shapes are excluded by bounding-box pre-filtering.
                shp = ll.FindFirst(TGIS_Utils.GisWholeWorld(), "NAME='Merced'");
                if (shp == null) return;

                tpl = new TGIS_Topology();
                try
                {
                    lb.RevertShapes();  // clear any previously computed buffer polygon
                    // Divide by 100.0 to convert the integer slider to fractional degrees.
                    tmp = tpl.MakeBuffer(shp, (1.0 * trackBar1.Value) / 100.0);
                    if (tmp != null)
                    {
                        // AddShape copies the geometry into the overlay layer and returns
                        // the stored shape reference (buf) used for the intersection query.
                        buf = lb.AddShape(tmp);
                        tmp = null;
                    }
                    else
                        buf = null;
                }
                finally
                {
                    tpl = null;
                }

                // ── Intersection query ────────────────────────────────────────────
                if (buf == null) return;

                ll = (TGIS_LayerVector)GIS.Get("counties");
                // Allow per-shape parameter overrides (needed to colour individual counties).
                ll.IgnoreShapeParams = false;
                if (ll == null) return;
                ll.RevertShapes();  // reset per-shape colours from the previous run
                textBox1.Clear();

                // Stage 1: bounding-box filter - only visit shapes whose extent
                // overlaps the buffer polygon's bounding box.
                tmp = ll.FindFirst(buf.Extent);
                while (tmp != null)
                {
                    // Stage 2: precise geometric test.
                    // IsCommonPoint returns true if the shapes share at least one point.
                    if (buf.IsCommonPoint(tmp))
                    {
                        // MakeEditable returns a writable copy of the shape so that
                        // Params.Area.Color can be changed.  Without this, modifying
                        // Params on a read-only shape from a file-backed layer has no effect.
                        tmp = tmp.MakeEditable();
                        textBox1.AppendText(tmp.GetField("name").ToString() + "\r\n");
                        tmp.Params.Area.Color = TGIS_Color.Blue;
                    }
                    tmp = ll.FindNext();  // advance to the next bounding-box candidate
                }
            }
            finally
            {
                // Always refresh the map, even if an early return occurred above.
                GIS.InvalidateWholeMap();
            }
        }

        /// <summary>
        /// Debounces rapid slider movement using the timer.
        ///
        /// Each scroll event resets the timer, so the actual buffer computation
        /// (timer1_Tick) only fires once the user pauses for 250 ms.  The current
        /// distance value is shown in the status bar immediately for responsiveness.
        /// </summary>
        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            timer1.Enabled = false;
            // Update the status bar label with the current distance while dragging.
            stripBar1.Items[0].Text = trackBar1.Value.ToString() + " km";
            timer1.Enabled = true;
        }
    }
}
