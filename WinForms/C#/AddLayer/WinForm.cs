//=============================================================================
// This source code is a part of TatukGIS Developer Kernel.
//=============================================================================
/*
  AddLayer Sample - Demonstrates how to programmatically add vector layers
  to a TatukGIS map viewer.

  Key concepts illustrated:
    - Creating a TGIS_LayerSHP instance directly (manual construction) and
      adding it to the viewer via GIS.Add.
    - Using TGIS_Utils.GisCreateLayer as a convenience factory that resolves
      the correct layer class from the file extension automatically.
    - Setting visual rendering parameters on a layer (area fill colour, line
      width, line outline width, line colour) through the Params property tree.
    - Suppressing automatic .ttkgp config-file loading with UseConfig = false
      so that the layer always starts with the explicitly assigned params.
    - Fitting the viewport to all loaded layers with GIS.FullExtent().
    - Switching the viewer interaction mode between Drag (pan) and Select.
    - Zooming programmatically by multiplying or dividing the current Zoom value.

  Data: DCW (Digital Chart of the World) Shapefiles for Poland, supplied
  via the TatukGIS sample data directory.
*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;             // Core TatukGIS types: TGIS_LayerSHP, TGIS_Color, TGIS_Utils, TGIS_ViewerMode
using TatukGIS.NDK.WinForms;   // WinForms-specific viewer control: TGIS_ViewerWnd

namespace AddLayer
{
    /// <summary>
    /// AddLayer sample — demonstrates how to programmatically add vector layers to a GIS viewer.
    /// Creates shapefile layers (TGIS_LayerSHP) for country polygons and rivers polylines, sets visual
    /// styling parameters (fill color, line width, line color), and adds them to the viewer using GIS.Add().
    /// Provides zoom navigation and interaction mode switching (pan/select).
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        // -------------------------------------------------------------------------
        // Designer-managed fields – layout is set up in InitializeComponent().
        // -------------------------------------------------------------------------
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        /// <summary>The TatukGIS map viewer control. All layers are added to this component.</summary>
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Panel panel1;
        /// <summary>Checkbox that toggles between Drag (pan) and Select interaction modes.</summary>
        private System.Windows.Forms.CheckBox chkDrag;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;

        /// <summary>
        /// Initialises the form and attaches the tooltip to the drag-mode checkbox.
        /// </summary>
        public WinForm()
        {
            InitializeComponent();
            // Provide a short usage hint directly on the checkbox control.
            this.toolTip1.SetToolTip(this.chkDrag, "Drag mode ON/OFF");
        }

        /// <summary>
        /// Disposes managed resources held by this form.
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

        // -------------------------------------------------------------------------
        // Windows Form Designer generated code – do not modify manually.
        // -------------------------------------------------------------------------
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDrag = new System.Windows.Forms.CheckBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // statusStrip1
            //
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 447);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(592, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            //
            // toolStripStatusLabel1
            //
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            //
            // imageList1
            //
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            //
            // panel1
            //
            this.panel1.Controls.Add(this.chkDrag);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 24);
            this.panel1.TabIndex = 2;
            //
            // chkDrag
            //
            this.chkDrag.Location = new System.Drawing.Point(77, 2);
            this.chkDrag.Name = "chkDrag";
            this.chkDrag.Size = new System.Drawing.Size(97, 22);
            this.chkDrag.TabIndex = 5;
            this.chkDrag.Text = "Dragging";
            this.chkDrag.Click += new System.EventHandler(this.chkDrag_Click);
            //
            // toolStrip1
            //
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(592, 25);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ImageList = imageList1;
            //
            // btnFullExtent
            //
            this.btnFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFullExtent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(23, 22);
            this.btnFullExtent.Text = "Full Extent";
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Click += new System.EventHandler(this.toolStripButton_Click);
            //
            // btnZoomIn
            //
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.ImageIndex = 1;
            this.btnZoomIn.Click += new System.EventHandler(this.toolStripButton_Click);
            //
            // btnZoomOut
            //
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Click += new System.EventHandler(this.toolStripButton_Click);
            //
            // GIS
            //
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 24);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 423);
            this.GIS.TabIndex = 3;
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 469);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - AddLayer";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        /// <summary>
        /// Application entry point.  Bootstraps the WinForms message loop and
        /// displays the main form.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #if NET6_0_OR_GREATER
              // .NET 6+ preferred bootstrap: respects app.manifest DPI settings.
              ApplicationConfiguration.Initialize();
            #else
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
            #endif
            Application.Run(new WinForm());
        }

        /// <summary>
        /// Handles the Form.Load event.  Creates and configures two Shapefile
        /// layers – a country polygon layer and a rivers polyline layer – then
        /// adds them to the GIS viewer and fits the viewport to their combined
        /// extent.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            TGIS_LayerSHP ll;

            // --- Layer 1: Country outline (polygon / area layer) ---
            // Construct the layer directly.  TGIS_LayerSHP wraps an ESRI
            // Shapefile; geometry type is inferred from the .shp file header.
            ll = new TGIS_LayerSHP();

            // GisSamplesDataDirDownload() returns the root path where TatukGIS
            // sample datasets were installed or downloaded.
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\Poland\DCW\country.shp";

            // A human-readable label used in legends and layer lists.
            ll.Name = "country";

            // Params.Area.Color sets the solid fill for polygon geometries.
            // LightGray provides a neutral base so the river overlay is readable.
            ll.Params.Area.Color = TGIS_Color.LightGray;

            // GIS.Add appends the layer to the internal stack.  Layers added
            // earlier are rendered first (painted at the bottom of the visual stack).
            GIS.Add(ll);

            // --- Layer 2: Rivers (polyline layer) ---
            // TGIS_Utils.GisCreateLayer is a factory that inspects the file
            // extension and instantiates the matching TGIS_Layer subclass.
            // Casting to TGIS_LayerSHP is safe for .shp files.
            // The first argument becomes the layer's Name property.
            ll = (TGIS_LayerSHP)(TGIS_Utils.GisCreateLayer("rivers",
                                       TGIS_Utils.GisSamplesDataDirDownload() +
                                       @"\World\Countries\Poland\DCW\lwaters.shp"
                                      )
                           );

            // UseConfig = false prevents the DK from loading a previously saved
            // .ttkgp configuration file for this layer, ensuring the rendering
            // parameters we set below take effect rather than cached values.
            ll.UseConfig = false;

            // OutlineWidth = 0 removes the contrasting halo drawn around lines,
            // yielding a clean single-colour stroke.
            ll.Params.Line.OutlineWidth = 0;

            // Line.Width is in screen pixels at the reference zoom level.
            ll.Params.Line.Width = 3;

            ll.Params.Line.Color = TGIS_Color.Blue;

            GIS.Add(ll);

            // Zoom the viewport to the combined bounding box of all layers so the
            // full map is visible immediately after load.
            GIS.FullExtent();
        }

        /// <summary>
        /// Shared click handler for all three toolbar buttons.
        /// Dispatches to the appropriate viewer operation based on which button
        /// raised the event.
        /// </summary>
        private void toolStripButton_Click(object sender, EventArgs e)
        {
            if (sender == btnFullExtent)
            {
                // Reset the viewport to show all loaded layers at once.
                GIS.FullExtent();
            }
            else if (sender == btnZoomIn)
            {
                // Double the zoom level – the visible area shrinks by half.
                GIS.Zoom = GIS.Zoom * 2;
            }
            else if (sender == btnZoomOut)
            {
                // Halve the zoom level – the visible area doubles.
                GIS.Zoom = GIS.Zoom / 2;
            }
        }

        /// <summary>
        /// Toggles the viewer's active interaction mode.
        /// <para>
        /// <see cref="TGIS_ViewerMode.Drag"/> – left-click and drag pans the map canvas.
        /// </para>
        /// <para>
        /// <see cref="TGIS_ViewerMode.Select"/> – left-click picks the topmost feature
        /// under the cursor.
        /// </para>
        /// </summary>
        private void chkDrag_Click(object sender, System.EventArgs e)
        {
            if (chkDrag.Checked)
                GIS.Mode = TGIS_ViewerMode.Drag;
            else
                GIS.Mode = TGIS_ViewerMode.Select;
        }
    }
}
