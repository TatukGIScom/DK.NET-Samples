//=============================================================================
// This source code is a part of TatukGIS Developer Kernel.
//=============================================================================
//
// SQLLayer Sample - C# WinForms (TatukGIS NDK)
// =============================================
// Demonstrates how to open and display an SQL layer using TatukGIS DK.
//
// An SQL layer (*.ttkls file) is a virtual layer definition that describes how
// to retrieve spatial data from a relational database (e.g. PostgreSQL/PostGIS,
// SQL Server, SQLite, Oracle Spatial) using a SQL query.  The .ttkls file
// contains the connection string, the SQL statement, and field mappings; it
// can be opened just like any other layer format supported by the DK.
//
// Key concepts shown:
//   - Opening a .ttkls project with TGIS_ViewerWnd.Open
//   - Fitting the view to the full data extent with FullExtent
//   - Switching the viewer between Zoom and Drag interaction modes
//
// To adapt this sample to your own database:
//   Edit gistest.ttkls to supply your connection string and SQL query.
//   The file can also be opened in the TatukGIS Editor for visual configuration.
//=============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;          // Core TatukGIS .NET SDK types
using TatukGIS.NDK.WinForms; // WinForms-specific controls (TGIS_ViewerWnd, etc.)

namespace SQLLayer
{
    /// <summary>
    /// Main application form for the SQLLayer sample.
    /// Hosts a <see cref="TGIS_ViewerWnd"/> that renders data loaded from
    /// a .ttkls SQL layer definition file.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        // ------------------------------------------------------------------ //
        //  Designer-managed fields (do not modify names/types)                //
        // ------------------------------------------------------------------ //

        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;

        private System.Windows.Forms.ToolStrip toolStrip1;            // Main toolbar
        private System.Windows.Forms.ToolStripButton btnFullExtent;   // Zoom to full extent
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1; // Visual separator
        private System.Windows.Forms.ToolStripButton btnZoom;         // Activate zoom mode
        private System.Windows.Forms.ToolStripButton btnDrag;         // Activate drag/pan mode
        private System.Windows.Forms.StatusStrip stripBar1;           // Status bar
        private System.Windows.Forms.ImageList imageList1;            // Toolbar icon images
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;            // TatukGIS map viewer

        /// <summary>
        /// Initialises the form and its child controls.
        /// Tooltip hints for the address text boxes are set here after
        /// InitializeComponent completes.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
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
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            //
            // GIS
            //
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 24);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(592, 423);
            this.GIS.TabIndex = 2;
            this.GIS.UseRTree = false;
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
            this.Text = "TatukGIS Samples - SQL Layer";
            this.Load += new System.EventHandler(this.WinForm_Load);
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
        /// Opens the SQL layer project file and zooms to the full data extent.
        /// </summary>
        /// <remarks>
        /// <see cref="TGIS_Utils.GisSamplesDataDirDownload"/> returns the path to
        /// the TatukGIS sample data directory.  The gistest.ttkls file inside
        /// Samples\SQLLayers\ holds the database connection string and the SQL
        /// query that defines the virtual layer.  Edit that file to point at your
        /// own database before running.
        /// </remarks>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            // Open the SQL layer definition file (.ttkls).
            // TGIS_ViewerWnd.Open accepts any supported layer or project format.
            // For .ttkls it connects to the configured database and executes the
            // SQL query to produce a virtual vector layer.
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\SQLLayers\gistest.ttkls");

            // Zoom the view so that all loaded features are visible.
            GIS.FullExtent();
        }

        /// <summary>
        /// Changes the toolbar cursor to a hand pointer when the mouse is over
        /// an active button, and resets it otherwise.
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
        /// Handles clicks on all toolbar buttons through a single shared handler.
        /// Dispatches to the appropriate viewer action based on which button was clicked.
        /// </summary>
        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent)
            {
                // Zoom the map to the full extent of all loaded layers.
                GIS.FullExtent();
            }
            else if (sender == btnZoom)
            {
                // Activate rubber-band zoom mode.
                // Uncheck the drag button to keep mutual exclusion.
                btnDrag.Checked = false;
                GIS.Mode = TGIS_ViewerMode.Zoom;
            }
            else if (sender == btnDrag)
            {
                // Activate pan/drag mode.
                // Uncheck the zoom button to keep mutual exclusion.
                btnZoom.Checked = false;
                GIS.Mode = TGIS_ViewerMode.Drag;
            }
        }
    }
}
