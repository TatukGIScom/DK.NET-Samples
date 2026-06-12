// Zooming - TatukGIS Developer Kernel (DK1 sample (C# / WinForms).
//
// Demonstrates multiple ways to zoom and navigate a loaded map:
//   1. Loading a TatukGIS project file (.ttkproject) which bundles all
//      layer definitions, styles, and coordinate system settings.
//   2. Full-extent button - zooms the viewer to display all loaded data.
//   3. Zoom mode - rubber-band rectangle zoom (left click + drag to zoom in,
//      right click to zoom out by one step).
//   4. Drag mode - panning by clicking and dragging the map.
//   5. Mouse wheel zoom - smooth zoom centered on the cursor position using
//      GIS.ZoomBy(factor, x, y), where the x,y anchor keeps the point under
//      the cursor stationary while the rest of the map scales around it.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Zooming
{
    /// <summary>
    /// Main form for the Zooming sample.
    /// Demonstrates viewer navigation: full extent, zoom mode, drag mode,
    /// and mouse-wheel zoom anchored to the cursor position.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        // "Full Extent" toolbar button - zooms to show all loaded data
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        // "Zoom Mode" toolbar button - enables rubber-band zoom interaction
        private System.Windows.Forms.ToolStripButton btnZoom;
        // "Drag Mode" toolbar button - enables pan/drag interaction
        private System.Windows.Forms.ToolStripButton btnDrag;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        // The central GIS map viewer control
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

        public WinForm()
        {
            InitializeComponent();

            // Set the GIS control as the active control so it receives
            // keyboard focus and mouse wheel events without an extra click
            this.ActiveControl = GIS;

            // Wire the mouse wheel handler manually; the designer does not
            // expose MouseWheel in the event list for TGIS_ViewerWnd
            GIS.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseWheel);
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
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
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            //
            // btnZoom
            //
            this.btnZoom.ImageIndex = 1;
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Checked = true;
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
            // stripBar1
            //
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            //
            // toolStripLabel1 - hints the user about the mouse wheel feature
            //
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Use mouse wheel to zoom in/zoom out";
            this.toolStripLabel1.Width = 575;
            //
            // GIS
            //
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 24);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 423);
            this.GIS.TabIndex = 2;
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
            this.Text = "TatukGIS Samples - Zooming";
            this.Load += new System.EventHandler(this.WinForm_Load);
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
        /// Form load event handler.
        /// Opens the sample Poland project file. A .ttkproject file is a TatukGIS
        /// project format that bundles multiple layer definitions, their rendering
        /// styles, and coordinate system settings into a single file.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\Poland\DCW\poland.ttkproject");
        }

        /// <summary>
        /// Mouse wheel event handler - zooms in or out centered on the cursor position.
        ///
        /// GIS.ZoomBy(factor, x, y) scales the viewport by 'factor' around the
        /// screen point (x, y) so the map location under the cursor stays fixed:
        ///   e.Delta &lt; 0 (wheel down / away): zoom in  -> factor 5/4 = 1.25x wider view
        ///   e.Delta &gt; 0 (wheel up / toward):  zoom out -> factor 4/5 = 0.80x narrower view
        ///
        /// The e.X, e.Y coordinates from MouseEventArgs are already in client
        /// (control-relative) coordinates, so no conversion is needed here.
        /// </summary>
        private void GIS_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Do nothing if no layers are loaded
            if (GIS.IsEmpty) return;

            if (e.Delta < 0)
                // Wheel scrolled down: zoom in (magnify, 25% wider viewport)
                GIS.ZoomBy(5/4.0, e.X, e.Y);
            else
                // Wheel scrolled up: zoom out (shrink, 20% narrower viewport)
                GIS.ZoomBy(4/5.0, e.X, e.Y);
        }

        /// <summary>
        /// Toolbar button click handler - dispatches to the correct action
        /// based on which button was clicked.
        ///
        /// Full Extent: resets the viewport to show all loaded layers.
        /// Zoom Mode:   enables rubber-band rectangle zoom interaction.
        /// Drag Mode:   enables pan/drag interaction.
        ///
        /// The Checked state of btnZoom and btnDrag is toggled to give visual
        /// feedback about which mode is currently active.
        /// </summary>
        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent)
                // Zoom to the full extent of all loaded layers
                GIS.FullExtent();
            else if (sender == btnZoom)
            {
                GIS.Mode = TGIS_ViewerMode.Zoom;
                btnZoom.Checked = true;
                btnDrag.Checked = false;
            }
            else if (sender == btnDrag)
            {
                GIS.Mode = TGIS_ViewerMode.Drag;
                btnZoom.Checked = false;
                btnDrag.Checked = true;
            }
        }

        /// <summary>
        /// Toolbar mouse-move handler - changes the cursor to a hand pointer
        /// when hovering over an active button to hint that the button is clickable.
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
    }
}
