using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Locate
{
    /* Locate sample — demonstrates feature identification and location by finding shapes at
       the cursor position using spatial queries and coordinate conversion.

       What the sample shows:
         - Loading a polygon shapefile (California Counties) into the GIS viewer
         - Feature location: finding which shape is at a given position
         - Screen-to-map coordinate conversion via ScreenToMap
         - Spatial tolerance: pixel screen tolerance for easier feature selection
         - GIS.Locate: queries features at a geographic position
         - Shape attributes: retrieving field values from selected features
         - Shape flashing: visual feedback highlighting the selected shape
         - Dynamic status bar: real-time display of feature attributes

       Key TatukGIS API concepts shown here:
         TGIS_ViewerWnd              - main visual map control
         TGIS_ViewerWnd.ScreenToMap  - convert screen pixel to geographic coordinate
         TGIS_ViewerWnd.Locate       - find topmost shape at location with tolerance
         TGIS_Shape                  - geographic feature with field access
         TGIS_Shape.Flash()          - briefly highlight shape for visual feedback
         OnMouseMove / OnMouseDown   - user interaction event handling
         TGIS_ControlLegend          - layer list/legend panel
    */
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ImageList imageList1;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AccessibleDescription = "";
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 24);
            this.toolStrip1.TabIndex = 0;
            //this.toolStrip1.ButtonClick += new System.Windows.Forms.ToolStripButtonClickEventHandler(this.toolStrip1_ButtonClick);
            //this.toolStrip1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.toolStrip1_MouseMove);
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "FullExtent";
            this.btnFullExtent.Click += toolStrip1_ButtonClick;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageIndex = 1;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.ToolTipText = "ZooIn";
            this.btnZoomIn.Click += toolStrip1_ButtonClick;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "ZoomOut";
            this.btnZoomOut.Click += toolStrip1_ButtonClick;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 24);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 423);
            this.GIS.TabIndex = 1;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
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
            this.Text = "TatukGIS Samples - Locate";
            this.Load += new System.EventHandler(this.WinForm_Load);
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
        /// Loads the sample data (California counties shapefile) into the viewer on form initialization.
        /// The data path is obtained from GisSamplesDataDirDownload() to support both local and
        /// downloaded sample data directories.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\USA\States\California\Counties.shp");
        }

        /// <summary>
        /// Handles mouse down events to locate and highlight (flash) the shape at the clicked position.
        /// Converts screen coordinates to map coordinates, uses GIS.Locate with 5-pixel tolerance
        /// to find the underlying shape, and calls Flash() to highlight the selected feature.
        /// </summary>
        private void GIS_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_Shape shp;

            if (GIS.IsEmpty) return;
            if (GIS.IsEmpty) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            shp = (TGIS_Shape)GIS.Locate(ptg, 5 / GIS.Zoom); // 5 pixels precision
            if (shp != null)
                shp.Flash();
        }

        /// <summary>
        /// Handles mouse move events to locate features and display their attributes.
        /// Continuously locates the shape at the current cursor position and displays the
        /// "name" field value in the status bar. If no shape is found, the status bar is cleared.
        /// Skips location during map painting to avoid performance issues.
        /// </summary>
        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_Shape shp;
            object val;

            if (GIS.IsEmpty) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            if (!GIS.InPaint)
                shp = (TGIS_Shape)GIS.Locate(ptg, 5 / GIS.Zoom); // 5 pixels precision
            else return;
            if (shp == null)
                stripBar1.Text = "";
            else
            {
                val = shp.GetField("name");
                if (val != null)
                    stripBar1.Text = val.ToString();
            }
        }

        /// <summary>
        /// Handles toolbar button clicks for map navigation and view control.
        /// FullExtent button zooms to show all data; ZoomIn button doubles the zoom level;
        /// ZoomOut button halves the zoom level. Coordinates are automatically centered on current view.
        /// </summary>
        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent)
            {
                GIS.FullExtent();
            }
            else if (sender == btnZoomIn)
            {
                GIS.Zoom = GIS.Zoom * 2;
            }
            else if (sender == btnZoomOut)
            {
                GIS.Zoom = GIS.Zoom / 2;
            }
        }

        /// <summary>
        /// Updates the toolbar cursor based on mouse position. When hovering over toolbar buttons,
        /// the cursor changes to a hand pointer; otherwise, it returns to the default cursor.
        /// This provides visual feedback that buttons are interactive.
        /// </summary>
        private void toolStrip1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            if (toolStrip1.Items[0].Bounds.Contains(p) ||
                toolStrip1.Items[1].Bounds.Contains(p) ||
                toolStrip1.Items[2].Bounds.Contains(p))
                toolStrip1.Cursor = Cursors.Hand;
            else
                toolStrip1.Cursor = Cursors.Default;
        }
    }
}
