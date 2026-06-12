// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// Triangulation Sample – Demonstrates Delaunay triangulation and Voronoi diagrams.
//
// This sample opens a point shapefile (Polish cities) and uses two specialised
// TGIS_LayerVector subclasses from the TatukGIS NDK to compute geometric structures
// derived from the point distribution:
//
//   Delaunay Triangulation
//     Connects input points into triangles such that no point lies inside the
//     circumscribed circle of any triangle.  Produces a TIN (Triangulated
//     Irregular Network) suitable for surface modelling and proximity analysis.
//
//   Voronoi Diagram
//     Partitions the plane into one region per input point, where each region
//     contains all locations closer to that point than to any other input point.
//     The Voronoi diagram is the geometric dual of the Delaunay triangulation.
//
// After generation the result layer is colour-graduated by polygon area
// (GIS_AREA attribute) over a white-to-red (Voronoi) or white-to-blue (Delaunay)
// gradient rendered across 10 equal-interval zones.  This highlights relative
// cell / triangle area variation across the study region.
//
// User interaction:
//   - Click a point on the map to display its attributes in the Attributes panel
//   - Radio buttons select Delaunay vs Voronoi mode before generation
//   - "Generate" adds the result layer; duplicate layer names are rejected
//   - Toolbar buttons provide Full Extent, Zoom In, and Zoom Out navigation
//
// Key TatukGIS NDK classes used:
//   TGIS_LayerDelaunay       – generates and stores a Delaunay triangulation
//   TGIS_LayerVoronoi        – generates and stores a Voronoi diagram
//   TGIS_LayerVector         – base class; ImportLayer() copies source point data
//   TGIS_ControlAttributes   – side panel that shows shape attribute data on click
//   TGIS_ControlLegend       – layer legend / style control panel
//   TGIS_ViewerWnd           – WinForms map viewer control
// =============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Triangulation
{
    /// <summary>
    /// Designer-generated partial class for the Triangulation sample main form.
    /// Contains InitializeComponent and field declarations only.
    /// The application logic lives in the second partial class below.
    /// </summary>
    partial class frmMain
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions2 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlbr = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.imglst = new System.Windows.Forms.ImageList(this.components);
            this.GIS_Attributes = new TatukGIS.NDK.WinForms.TGIS_ControlAttributes();
            this.grpbxResult = new System.Windows.Forms.GroupBox();
            this.lblLayer = new System.Windows.Forms.Label();
            this.edtLayer = new System.Windows.Forms.TextBox();
            this.rbtnDelaunay = new System.Windows.Forms.RadioButton();
            this.rbtnVoronoi = new System.Windows.Forms.RadioButton();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.grpbxResult.SuspendLayout();
            this.SuspendLayout();
            //
            // stripBar1
            //
            this.stripBar1.Location = new System.Drawing.Point(0, 382);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(584, 22);
            this.stripBar1.TabIndex = 0;
            //
            // panel1
            //
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tlbr);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 26);
            this.panel1.TabIndex = 1;
            //
            // tlbr
            //
            this.tlbr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlbr.AutoSize = false;
            this.tlbr.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut});
            this.tlbr.Dock = System.Windows.Forms.DockStyle.None;
            this.tlbr.ImageList = this.imglst;
            this.tlbr.Location = new System.Drawing.Point(0, 0);
            this.tlbr.Name = "tlbr";
            this.tlbr.ShowItemToolTips = true;
            this.tlbr.Size = new System.Drawing.Size(584, 26);
            this.tlbr.TabIndex = 0;
            //
            // btnFullExtent
            //
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += tlbr_ButtonClick;
            //
            // btnZoomIn
            //
            this.btnZoomIn.ImageIndex = 1;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.ToolTipText = "Zoom In";
            this.btnZoomIn.Click += tlbr_ButtonClick;
            //
            // btnZoomOut
            //
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "Zoom Out";
            this.btnZoomOut.Click += tlbr_ButtonClick;
            //
            // imglst
            //
            this.imglst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglst.ImageStream")));
            this.imglst.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imglst.Images.SetKeyName(0, "FullExtent.bmp");
            this.imglst.Images.SetKeyName(1, "ZoomIn.bmp");
            this.imglst.Images.SetKeyName(2, "ZoomOut.bmp");
            //
            // GIS_Attributes
            //
            this.GIS_Attributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS_Attributes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GIS_Attributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.GIS_Attributes.Location = new System.Drawing.Point(424, 28);
            this.GIS_Attributes.Name = "GIS_Attributes";
            this.GIS_Attributes.Size = new System.Drawing.Size(160, 141);
            this.GIS_Attributes.TabIndex = 2;
            //
            // grpbxResult
            //
            this.grpbxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpbxResult.Controls.Add(this.lblLayer);
            this.grpbxResult.Controls.Add(this.edtLayer);
            this.grpbxResult.Controls.Add(this.rbtnDelaunay);
            this.grpbxResult.Controls.Add(this.rbtnVoronoi);
            this.grpbxResult.Location = new System.Drawing.Point(424, 175);
            this.grpbxResult.Name = "grpbxResult";
            this.grpbxResult.Size = new System.Drawing.Size(160, 92);
            this.grpbxResult.TabIndex = 3;
            this.grpbxResult.TabStop = false;
            this.grpbxResult.Text = "Result";
            //
            // lblLayer
            //
            this.lblLayer.AutoSize = true;
            this.lblLayer.Location = new System.Drawing.Point(6, 68);
            this.lblLayer.Name = "lblLayer";
            this.lblLayer.Size = new System.Drawing.Size(68, 13);
            this.lblLayer.TabIndex = 3;
            this.lblLayer.Text = "Layer name :";
            //
            // edtLayer
            //
            this.edtLayer.Location = new System.Drawing.Point(80, 65);
            this.edtLayer.Name = "edtLayer";
            this.edtLayer.Size = new System.Drawing.Size(74, 20);
            this.edtLayer.TabIndex = 2;
            this.edtLayer.Text = "Voronoi";
            //
            // rbtnDelaunay
            //
            this.rbtnDelaunay.AutoSize = true;
            this.rbtnDelaunay.Location = new System.Drawing.Point(6, 42);
            this.rbtnDelaunay.Name = "rbtnDelaunay";
            this.rbtnDelaunay.Size = new System.Drawing.Size(134, 17);
            this.rbtnDelaunay.TabIndex = 1;
            this.rbtnDelaunay.TabStop = true;
            this.rbtnDelaunay.Text = "Delaunay Triangulation";
            this.rbtnDelaunay.UseVisualStyleBackColor = true;
            this.rbtnDelaunay.Click += new System.EventHandler(this.rbtnDelaunay_Click);
            //
            // rbtnVoronoi
            //
            this.rbtnVoronoi.AutoSize = true;
            this.rbtnVoronoi.Checked = true;
            this.rbtnVoronoi.Location = new System.Drawing.Point(6, 19);
            this.rbtnVoronoi.Name = "rbtnVoronoi";
            this.rbtnVoronoi.Size = new System.Drawing.Size(103, 17);
            this.rbtnVoronoi.TabIndex = 0;
            this.rbtnVoronoi.TabStop = true;
            this.rbtnVoronoi.Text = "Voronoi Diagram";
            this.rbtnVoronoi.UseVisualStyleBackColor = true;
            this.rbtnVoronoi.Click += new System.EventHandler(this.rbtnVoronoi_Click);
            //
            // GIS_Legend
            //
            this.GIS_Legend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions2;
            this.GIS_Legend.GIS_Group = null;
            this.GIS_Legend.GIS_Layer = null;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(424, 302);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)));
            this.GIS_Legend.ReverseOrder = true;
            this.GIS_Legend.Size = new System.Drawing.Size(160, 80);
            this.GIS_Legend.TabIndex = 4;
            //
            // GIS
            //
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Location = new System.Drawing.Point(0, 28);
            this.GIS.MinZoomSize = -5;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(424, 354);
            this.GIS.TabIndex = 5;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            //
            // btnGenerate
            //
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Location = new System.Drawing.Point(430, 273);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(148, 23);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            //
            // frmMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(584, 404);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GIS_Legend);
            this.Controls.Add(this.grpbxResult);
            this.Controls.Add(this.GIS_Attributes);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Triangulation";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.grpbxResult.ResumeLayout(false);
            this.grpbxResult.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // --- Designer-declared controls ---
        private System.Windows.Forms.StatusStrip stripBar1;        // Status bar at the bottom
        private System.Windows.Forms.Panel panel1;                 // Panel hosting the toolbar
        private System.Windows.Forms.ImageList imglst;             // Toolbar button images
        private System.Windows.Forms.ToolStrip tlbr;               // Navigation toolbar
        private System.Windows.Forms.ToolStripButton btnFullExtent; // Zoom to fit all layers
        private System.Windows.Forms.ToolStripButton btnZoomIn;     // Double the zoom factor
        private System.Windows.Forms.ToolStripButton btnZoomOut;    // Halve the zoom factor
        private TatukGIS.NDK.WinForms.TGIS_ControlAttributes GIS_Attributes; // Shape attribute display panel
        private System.Windows.Forms.GroupBox grpbxResult;         // Groups mode/name controls
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_Legend; // Layer legend panel
        private System.Windows.Forms.Label lblLayer;               // "Layer name:" label
        private System.Windows.Forms.TextBox edtLayer;             // Editable output layer name
        private System.Windows.Forms.RadioButton rbtnDelaunay;     // Select Delaunay mode
        private System.Windows.Forms.RadioButton rbtnVoronoi;      // Select Voronoi mode
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;         // Map viewer control
        private System.Windows.Forms.Button btnGenerate;           // Trigger triangulation/diagram
    }

    /// <summary>
    /// Application entry-point container.
    /// </summary>
    static class Program
    {
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
            Application.Run(new frmMain());
        }
    }

    /// <summary>
    /// Main form for the Triangulation sample application.
    ///
    /// <para>Demonstrates Delaunay triangulation and Voronoi diagram generation
    /// using <see cref="TGIS_LayerDelaunay"/> and <see cref="TGIS_LayerVoronoi"/>.
    /// The result layer is colour-graduated by polygon area (GIS_AREA attribute)
    /// to give an immediate visual impression of size variation across the plane.</para>
    /// </summary>
    public partial class frmMain : Form
    {
        /// <summary>Initialises WinForms designer components.</summary>
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the Polish city point shapefile on form startup and configures
        /// the city marker style.
        ///
        /// <para>A second "selected" param set is also registered so that shapes
        /// shown in the Attributes panel receive a distinct blue area fill,
        /// making the selected shape easy to identify on the map.</para>
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;

            // Open the Polish city point data – this layer provides the seed
            // point set for both the Delaunay and Voronoi algorithms.
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\Poland\DCW\city.shp");

            // Retrieve the freshly loaded vector layer and customise its marker style
            lv = (TGIS_LayerVector)(GIS.Items[0]);
            lv.Params.Marker.Color = TGIS_Color.FromARGB((uint)ColorTranslator.FromWin32(0x4080FF).ToArgb());
            lv.Params.Marker.OutlineWidth = 2;
            lv.Params.Marker.Style = TGIS_MarkerStyle.Circle;

            // Add a second named param set; shapes displayed in the attribute panel
            // use the "selected" style so they stand out visually on the map.
            lv.ParamsList.Add();
            lv.Params.Style = "selected";
            lv.Params.Area.OutlineWidth = 1;
            lv.Params.Area.Color = TGIS_Color.Blue;

            GIS_Legend.Update();  // Refresh legend to show the new layer style
        }

        /// <summary>
        /// Handles toolbar button clicks for map navigation.
        /// Dispatches to the appropriate viewer method based on which button was pressed.
        /// </summary>
        private void tlbr_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent) GIS.FullExtent();        // Zoom to fit all layers
            else if(sender == btnZoomIn) GIS.Zoom = GIS.Zoom * 2; // Double zoom factor
            else if(sender == btnZoomOut) GIS.Zoom = GIS.Zoom / 2; // Halve zoom factor
        }

        /// <summary>
        /// Pre-fills the output layer name when the user selects Voronoi mode.
        /// </summary>
        private void rbtnVoronoi_Click(object sender, EventArgs e)
        {
            edtLayer.Text = "Voronoi";
        }

        /// <summary>
        /// Pre-fills the output layer name when the user selects Delaunay mode.
        /// </summary>
        private void rbtnDelaunay_Click(object sender, EventArgs e)
        {
            edtLayer.Text = "Delaunay";
        }

        /// <summary>
        /// Hit-tests the map on mouse press to find the nearest shape and displays
        /// its attribute data in the GIS_Attributes side panel.
        ///
        /// <para><see cref="TGIS_ViewerWnd.ScreenToMap"/> converts pixel coordinates
        /// to geographic (map) coordinates.  <see cref="TGIS_ViewerWnd.Locate"/>
        /// searches all layers for the nearest shape within a tolerance of 5 screen
        /// pixels (converted to map units by dividing by the current zoom factor).</para>
        /// </summary>
        private void GIS_MouseDown(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_Shape shp;

            if (GIS.IsEmpty) return;
            if (GIS.IsEmpty) return;

            // Convert screen pixel position to geographic map coordinates
            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            // Locate the nearest shape within a 5-pixel screen tolerance
            shp = (TGIS_Shape)(GIS.Locate(ptg, 5 / GIS.Zoom)); // 5 pixels precision
            if (shp != null)
                GIS_Attributes.ShowShape(shp);  // Populate the attribute panel
        }

        /// <summary>
        /// Creates either a <see cref="TGIS_LayerVoronoi"/> or
        /// <see cref="TGIS_LayerDelaunay"/> layer, imports the city point data as
        /// input, configures a graduated colour render style, and adds the result
        /// to the viewer.
        ///
        /// <para><see cref="TGIS_LayerVector.ImportLayer"/> copies all features from
        /// the source layer (GIS.Items[0]) into the new triangulation layer and
        /// runs the triangulation/diagram algorithm immediately.  After the call the
        /// layer contains fully formed polygon shapes ready for display.</para>
        ///
        /// <para>The GIS_AREA attribute (computed automatically by the triangulation
        /// engine) drives a white-to-colour gradient rendered over 10 equal-interval
        /// zones, giving an immediate visual impression of cell/triangle size variation.
        /// Voronoi uses red as the end colour; Delaunay uses blue.</para>
        /// </summary>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;

            // Prevent overwriting an existing layer with the same name
            if (GIS.Get(edtLayer.Text) != null)
            {
                MessageBox.Show("Result layer already exists. Use different name.");
                return;
            }

            // Instantiate the correct layer type based on the radio button selection
            if (rbtnVoronoi.Checked)
                lv = new TGIS_LayerVoronoi();   // Voronoi diagram
            else
                lv = new TGIS_LayerDelaunay();   // Delaunay triangulation

            lv.Name = edtLayer.Text;

            // ImportLayer reads all point features from the source layer and uses them
            // as the input seed set for the triangulation / diagram computation.
            // TGIS_ShapeType.Unknown lets the engine infer the output shape type automatically.
            lv.ImportLayer((TGIS_LayerVector)(GIS.Items[0]), GIS.Extent,
                            TGIS_ShapeType.Unknown, "", false
                          );
            lv.Transparency = 60;  // Semi-transparent so the source city layer shows through

            // Configure graduated colour rendering keyed on the built-in GIS_AREA attribute.
            // GIS_AREA holds the area of each output polygon in map coordinate units squared.
            lv.Params.Render.Expression = "GIS_AREA";
            lv.Params.Render.MinVal = 10000000;     // ~10 km² lower bound
            lv.Params.Render.MaxVal = 1300000000;   // ~1300 km² upper bound
            lv.Params.Render.StartColor = TGIS_Color.White;
            // Differentiate Voronoi (red) from Delaunay (blue) visually
            if (rbtnVoronoi.Checked)
                lv.Params.Render.EndColor = TGIS_Color.Red;
            else
                lv.Params.Render.EndColor = TGIS_Color.Blue;

            lv.Params.Render.Zones = 10;  // Divide the colour range into 10 equal steps
            // TGIS_Color.RenderColor instructs the renderer to substitute the
            // computed gradient colour for the polygon fill during painting.
            lv.Params.Area.Color = TGIS_Color.RenderColor;

            // Inherit the coordinate system from the source layer so all layers align
            lv.CS = GIS.CS;

            GIS.Add(lv);
            GIS.InvalidateWholeMap();   // Repaint the viewer
            GIS_Legend.Invalidate();    // Refresh the legend to show the new layer
        }
    }
}
