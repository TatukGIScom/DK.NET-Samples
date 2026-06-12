//=============================================================================
// This source code is a part of TatukGIS Developer Kernel.
//=============================================================================
//
// Renderer sample — demonstrates how to load and display a TatukGIS project
// file that contains pre-configured custom rendering rules.
//
// Key concepts shown:
//   - Opening a .ttkproject file with TGIS_ViewerWnd.Open
//   - Switching the viewer interaction mode between Zoom and Drag using the
//     TGIS_ViewerMode enumeration
//   - Restoring the full map extent with TGIS_ViewerWnd.FullExtent
//
// The rendering definitions (symbol styles, color ramps, scale-dependent
// rules, etc.) are stored inside renderer.ttkproject.  This form simply
// loads that project and wires up the toolbar buttons for map navigation.
//=============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;           // Core TatukGIS types (TGIS_ViewerMode, TGIS_Utils, …)
using TatukGIS.NDK.WinForms;  // WinForms-specific controls (TGIS_ViewerWnd)

namespace Renderer
{
    /// <summary>
    /// Main application form for the Renderer sample.
    /// Hosts the TGIS_ViewerWnd map control together with a navigation toolbar
    /// and a status strip.  On load it opens the pre-configured renderer
    /// project file and zooms to its full extent.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        // ---------------------------------------------------------------
        // Designer-managed fields (do not rename — referenced by .resx)
        // ---------------------------------------------------------------
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStrip toolStrip1;         // Navigation toolbar
        private System.Windows.Forms.ToolStripButton btnFullExtent; // Full-extent button
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnZoom;       // Zoom-mode button
        private System.Windows.Forms.ToolStripButton btnDrag;       // Drag/pan-mode button
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;          // TatukGIS map viewer
        private System.Windows.Forms.StatusStrip stripBar1;         // Status bar
        private System.Windows.Forms.ImageList imageList1;          // Toolbar icons

        /// <summary>
        /// Initialises the form components and sets the map viewer as the
        /// initially focused control so keyboard shortcuts work immediately.
        /// </summary>
        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            // Make the map viewer receive keyboard focus on startup so that
            // keyboard-based zoom / pan shortcuts work without an extra click.
            this.ActiveControl = GIS;
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            //
            // toolStrip1
            //
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullExtent,
            this.toolStripSeparator1,
            this.btnZoom,
            this.btnDrag});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 24);
            this.toolStrip1.TabIndex = 0;
            //
            // btnFullExtent
            //
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += toolStrip1_ButtonClick;
            //
            // toolStripButton1
            //
            this.toolStripSeparator1.Name = "toolStripButton1";
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
            // imageList1
            //
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            //
            // GIS — TatukGIS WinForms map viewer control
            //
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 24);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 423);
            this.GIS.TabIndex = 1;
            //
            // stripBar1
            //
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 2;
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Renderer";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// Application entry point.
        /// Configures DPI awareness and visual styles, then starts the message
        /// loop with the main form.
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
        /// Handles the Form.Load event.
        /// Opens the renderer project file (which contains all pre-configured
        /// layer rendering settings) and zooms to the full extent of the data.
        ///
        /// TGIS_Utils.GisSamplesDataDirDownload() returns the root path of the
        /// downloaded TatukGIS sample dataset.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            // Open the pre-built renderer project; layer styles are defined
            // inside the .ttkproject XML file — no code-level styling is needed.
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\Projects\renderer.ttkproject");

            // Zoom the view to fit all layers loaded by the project.
            GIS.FullExtent();
        }

        /// <summary>
        /// Shared click handler for all three toolbar buttons.
        /// Determines which button was clicked by reference comparison and
        /// performs the corresponding viewer action.
        ///
        /// TGIS_ViewerMode.Zoom  — rubber-band zoom / scroll-wheel zoom
        /// TGIS_ViewerMode.Drag  — click-and-drag panning
        /// </summary>
        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent)
                GIS.FullExtent();                          // Fit all layers in view
            else if (sender == btnZoom)
                GIS.Mode = TGIS_ViewerMode.Zoom;           // Enable zoom interaction
            else if (sender == btnDrag)
                GIS.Mode = TGIS_ViewerMode.Drag;           // Enable pan interaction
        }

        /// <summary>
        /// Changes the toolbar cursor to a hand when the pointer is over an
        /// active button, providing a visual affordance for clickable items.
        /// </summary>
        private void toolStrip1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            // Items 0, 2, 3 are the active buttons (Full Extent, Zoom, Drag);
            // item 1 is the separator and should not show the hand cursor.
            if (toolStrip1.Items[0].Bounds.Contains(p) ||
                toolStrip1.Items[2].Bounds.Contains(p) ||
                toolStrip1.Items[3].Bounds.Contains(p))
                toolStrip1.Cursor = Cursors.Hand;
            else
                toolStrip1.Cursor = Cursors.Default;
        }
    }
}
