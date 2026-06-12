// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// Legend Sample - C# WinForms (.NET)
//
// Demonstrates how to use the TGIS_ControlLegend panel alongside a map viewer.
// The legend control provides an interactive, dockable layer list that lets the
// user toggle layer visibility, reorder layers, expand/collapse layer symbology,
// and open layer property dialogs - all without writing any extra code.
//
// Key concepts shown:
//   - Placing TGIS_ControlLegend next to TGIS_ViewerWnd and linking them via
//     GIS_Viewer so the legend reflects the loaded map automatically.
//   - Switching between two display modes at runtime:
//       * TGIS_ControlLegendMode.Layers - flat list of every individual layer
//       * TGIS_ControlLegendMode.Groups - tree grouped by layer group membership
//   - Opening a .ttkproject file that bundles multiple SHP layers together.
//   - Controlling the viewer interaction mode (Zoom / Drag).
//   - Persisting any layer-style changes back to the project file with SaveAll.
//   - Reflecting the current map scale in the status bar after each repaint.
// =============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Legend
{
    /* Legend sample — demonstrates interactive layer list and legend management.

       What the sample shows:
         - Placing TGIS_ControlLegend next to TGIS_ViewerWnd with legend-to-viewer linking
         - Interactive layer visibility toggling via legend checkboxes
         - Reordering layers via drag-and-drop in legend panel
         - Expanding/collapsing layer symbology details
         - Opening layer property dialogs for advanced configuration
         - Switching between two display modes at runtime (Layers vs. Groups)
         - Loading .ttkproject files with multiple pre-configured layers
         - Persisting layer-style changes back to project file via SaveAll
         - Real-time status bar updates showing current map scale

       Key TatukGIS API concepts shown here:
         TGIS_ViewerWnd              - main visual map control
         TGIS_ControlLegend          - interactive legend/layer list panel
         TGIS_ControlLegendMode      - display modes (Layers, Groups, etc.)
         GIS.Items                   - layer collection (get count, access by index)
         TGIS_Params                 - layer styling and rendering parameters
         GIS.VisibleExtent           - geographic extent of loaded layers
         GIS.Scale                   - map scale for display in status bar
    */
    /// <summary>
    /// Main application form for the Legend sample.
    /// Hosts a TGIS_ViewerWnd (map canvas) and a TGIS_ControlLegend panel
    /// side-by-side, separated by a resizable splitter.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;

        // --- UI controls (wired in InitializeComponent) ---
        private System.Windows.Forms.ImageList imageList1;       // Toolbar button icons
        private System.Windows.Forms.StatusStrip stripBar1;      // Status bar at the bottom
        private System.Windows.Forms.ToolStripLabel toolStripLabel1; // "Scale :" caption label
        private System.Windows.Forms.ToolStripLabel toolStripLabel2; // Current scale value label

        /// <summary>
        /// The interactive legend panel.
        /// Linked to the GIS viewer via GIS_Viewer so it automatically lists all
        /// layers loaded in the map. Users can toggle visibility, drag layers to
        /// reorder them, expand nodes to inspect symbology, and double-click a
        /// layer to open its properties dialog.
        /// </summary>
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_ControlLegend1;

        private System.Windows.Forms.Splitter splitter1; // Resizable divider between legend and map
        private Panel panel1;                            // Container for the toolbar strip
        private Button btnGroups;                        // Switches legend to Groups display mode
        private Button btnLayers;                        // Switches legend to Layers display mode
        private ToolStrip toolStrip1;
        private ToolStripButton btnFullExtent;           // Zooms to full extent of all loaded layers
        private ToolStripButton btnZoom;                 // Switches viewer to Zoom interaction mode
        private ToolStripButton btnDrag;                 // Switches viewer to Drag/Pan interaction mode
        private ToolStripButton btnSaveConfig;           // Saves project configuration to disk
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;

        /// <summary>
        /// The main interactive map canvas (TGIS_ViewerWnd).
        /// Renders all loaded layers, handles mouse-driven zooming and panning,
        /// and fires AfterPaintEvent on each completed repaint.
        /// </summary>
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

        /// <summary>
        /// Initialises the form, runs the designer-generated component wiring,
        /// and ensures the map canvas receives keyboard focus on startup.
        /// </summary>
        public WinForm()
        {
            // Set up all controls, layouts and event subscriptions defined by the designer
            InitializeComponent();

            // Give the map canvas initial keyboard focus so arrow-key panning works immediately
            this.ActiveControl = GIS;
        }

        /// <summary>
        /// Releases managed resources when the form is closed.
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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.GIS_ControlLegend1 = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnGroups = new System.Windows.Forms.Button();
            this.btnLayers = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveConfig = new System.Windows.Forms.ToolStripButton();
            //((System.ComponentModel.ISupportInitialize)(this.toolStripLabel1)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.toolStripLabel2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // imageList1
            //
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            //
            // stripBar1
            //
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1,
            this.toolStripLabel2});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 2;
            //
            // toolStripLabel1
            //
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Scale :";
            this.toolStripLabel1.Width = 50;
            //
            // toolStripLabel2
            //
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Width = 525;
            //
            // GIS_ControlLegend1
            //
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_ControlLegend1.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_ControlLegend1.Dock = System.Windows.Forms.DockStyle.Left;
            this.GIS_ControlLegend1.GIS_Group = null;
            this.GIS_ControlLegend1.GIS_Layer = null;
            this.GIS_ControlLegend1.GIS_Viewer = this.GIS;
            this.GIS_ControlLegend1.Location = new System.Drawing.Point(0, 28);
            this.GIS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_ControlLegend1.Name = "GIS_ControlLegend1";
            // AllowMove: user can drag layers to reorder them in the legend
            // AllowActive: clicking a layer makes it the active (editable) layer
            // AllowExpand: layer nodes can be expanded to show symbology classes
            // AllowParams: double-clicking opens the layer properties dialog
            // AllowSelect: layers can be selected (highlighted) in the legend
            this.GIS_ControlLegend1.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)));
            // ReverseOrder=true so the top layer in the legend matches the top
            // rendering layer (visually topmost on the map)
            this.GIS_ControlLegend1.ReverseOrder = true;
            this.GIS_ControlLegend1.Size = new System.Drawing.Size(145, 419);
            this.GIS_ControlLegend1.TabIndex = 6;
            //
            // GIS
            //
            this.GIS.BackColor = System.Drawing.SystemColors.Control;
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.IncrementalPaint = false;
            this.GIS.Location = new System.Drawing.Point(148, 28);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Size = new System.Drawing.Size(444, 419);
            this.GIS.TabIndex = 8;
            this.GIS.AfterPaintEvent += new TGIS_PaintEvent(this.GIS_AfterPaint);
            //
            // btnGroups
            //
            this.btnGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGroups.Location = new System.Drawing.Point(73, 417);
            this.btnGroups.Name = "btnGroups";
            this.btnGroups.Size = new System.Drawing.Size(67, 23);
            this.btnGroups.TabIndex = 1;
            this.btnGroups.Text = "Groups";
            this.btnGroups.UseVisualStyleBackColor = true;
            this.btnGroups.Click += new System.EventHandler(this.btnGroups_Click);
            //
            // btnLayers
            //
            this.btnLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLayers.Location = new System.Drawing.Point(3, 417);
            this.btnLayers.Name = "btnLayers";
            this.btnLayers.Size = new System.Drawing.Size(67, 23);
            this.btnLayers.TabIndex = 0;
            this.btnLayers.Text = "Layers";
            this.btnLayers.UseVisualStyleBackColor = true;
            this.btnLayers.Click += new System.EventHandler(this.btnLayers_Click);
            //
            // splitter1
            //
            this.splitter1.Location = new System.Drawing.Point(145, 28);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 419);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            //
            // panel1
            //
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 28);
            this.panel1.TabIndex = 5;
            //
            // toolStrip1
            //
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnZoom,
            this.btnDrag,
            this.btnFullExtent,
            this.toolStripSeparator1,
            this.btnSaveConfig });
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(111, 28);
            this.toolStrip1.TabIndex = 1;
            //
            // btnFullExtent
            //
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += toolStrip1_ButtonClick;
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            //
            // btnZoom
            //
            this.btnZoom.ImageIndex = 1;
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.ToolTipText = "Zoom Mode";
            this.btnZoom.Click += toolStrip1_ButtonClick;
            //
            // btnDrag
            //
            this.btnDrag.ImageIndex = 2;
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.ToolTipText = "Drag Mode";
            this.btnDrag.Click += toolStrip1_ButtonClick;
            //
            // toolStripSeparator2
            //
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            //
            // btnSaveConfig
            //
            this.btnSaveConfig.ImageIndex = 3;
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Click += toolStrip1_ButtonClick;
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.btnGroups);
            this.Controls.Add(this.btnLayers);
            this.Controls.Add(this.GIS_ControlLegend1);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Legend";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Application entry point.
        /// Configures high-DPI and visual styles before launching the form.
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
        /// Loads the sample Poland multi-layer project when the form first appears.
        /// Opening a .ttkproject causes TGIS_ViewerWnd to load all referenced layer
        /// files. Because GIS_ControlLegend1.GIS_Viewer is already set to GIS, the
        /// legend populates itself automatically as each layer is registered.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            // GisSamplesDataDirDownload() resolves the path to the shared sample data
            // directory (configurable via the TatukGIS environment or registry).
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\Poland\DCW\poland.ttkproject");
        }

        /// <summary>
        /// Unified click handler for all toolbar buttons.
        /// Determines which button was clicked by reference comparison and
        /// delegates to the appropriate viewer action.
        /// </summary>
        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if(sender == btnFullExtent)
            {
                // Zoom out to fit the complete bounding box of all visible layers
                GIS.FullExtent();
            }
            else if(sender == btnZoom)
            {
                // TGIS_ViewerMode.Zoom: left-drag to zoom in, right-click to zoom out
                GIS.Mode = TGIS_ViewerMode.Zoom;
            }
            else if(sender == btnDrag)
            {
                // TGIS_ViewerMode.Drag: click-drag to pan the visible map extent
                GIS.Mode = TGIS_ViewerMode.Drag;
            }
            else if(sender == btnSaveConfig)
            {
                if (GIS.IsEmpty) return;  // Nothing loaded - nothing to save

                // Persist any legend changes (visibility flags, symbology edits, layer order)
                // back to the .ttkproject file and its referenced layer files.
                GIS.SaveAll();
            }
        }

        /// <summary>
        /// Changes the toolbar cursor to a hand pointer when hovering over
        /// active toolbar buttons, providing visual affordance feedback.
        /// </summary>
        private void toolStrip1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            if (toolStrip1.Items[0].Bounds.Contains(p) ||
                toolStrip1.Items[2].Bounds.Contains(p) ||
                toolStrip1.Items[3].Bounds.Contains(p))
                toolStrip1.Cursor = Cursors.Hand;
            else
                toolStrip1.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Switches the legend to flat Layers mode.
        /// In Layers mode every individual layer appears as a separate top-level
        /// item in the legend list, regardless of any group hierarchy.
        /// </summary>
        private void btnLayers_Click(object sender, EventArgs e)
        {
            GIS_ControlLegend1.Mode = TGIS_ControlLegendMode.Layers;
        }

        /// <summary>
        /// Switches the legend to Groups mode.
        /// In Groups mode layers are nested inside their parent group nodes,
        /// matching the logical structure defined in the project file.
        /// </summary>
        private void btnGroups_Click(object sender, EventArgs e)
        {
            GIS_ControlLegend1.Mode = TGIS_ControlLegendMode.Groups;
        }

        /// <summary>
        /// Fired by TGIS_ViewerWnd after every completed map repaint.
        /// Updates the status bar with the human-readable map scale string
        /// (e.g. "1 : 250 000") so the user always knows the current zoom level.
        /// </summary>
        private void GIS_AfterPaint(object sender, TGIS_PaintEventArgs e)
        {
            // ScaleAsText returns a formatted "1 : N" string for the current viewport
            stripBar1.Items[1].Text = GIS.ScaleAsText;
        }
    }
}
