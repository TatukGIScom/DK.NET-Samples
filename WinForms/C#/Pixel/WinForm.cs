/* Pixel sample — demonstrates rendering raster layers with various visual effects.

   What the sample shows:
     - Loading a pre-built .ttkproject file containing a raster layer
     - Switching between rendering profiles via combo box selector
     - Normal rendering: standard colour display for multi-band imagery
     - Normal with histogram: histogram-stretch for enhanced contrast
     - Grayscale rendering: single-channel conversion for monochrome display
     - Transparent rendering: alpha-channel and transparency support
     - Colorize rendering: maps pixel values through custom colour gradients
     - Inversion rendering: inverts all colour channels (negative effect)
     - Inversion by RGB: inverts individual R, G, B channels separately

   Key TatukGIS API concepts shown here:
     TGIS_ViewerWnd                  - main visual map control
     TGIS_LayerPixel                 - raster/pixel layer (imagery, DEMs, etc.)
     GIS.Open()                      - opens .ttkproject with all layers
     GIS.FullExtent()                - zoom to combined extent of all layers
     TGIS_ViewerMode                 - interaction modes (Zoom, Drag)
     TGIS_Utils.GisSamplesDataDirDownload() - resolves bundled sample data path
*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Pixel
{
    /// <summary>
    /// Main form for the Pixel sample application.
    /// Demonstrates loading and displaying a raster pixel layer through
    /// various rendering project configurations.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFullExtent;   // Zoom to full extent
        private System.Windows.Forms.ToolStripButton btnZoom;         // Enable zoom mode
        private System.Windows.Forms.ToolStripButton btnDrag;         // Enable drag/pan mode
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ComboBox comboBox1;              // Project selector
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;            // TatukGIS map viewer
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;

        /// <summary>
        /// Initialises the form components and sets the GIS viewer as the
        /// active control so keyboard navigation works immediately.
        /// </summary>
        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // Give keyboard focus to the GIS viewer straight away so the
            // user can pan/zoom without needing an extra mouse click.
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
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // toolStrip1
            //
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullExtent,
            this.btnZoom,
            this.btnDrag,
            this.toolStripSeparator1,
            this.toolStripSeparator2});
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
            // toolStripSeparator2
            //
            this.toolStripSeparator2.Name = "toolStripButton2";
            //
            // imageList1
            //
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            //
            // comboBox1
            //
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Items.AddRange(new object[] {
            "Normal.ttkproject",
            "Normal with histogram.ttkproject",
            "Grayscale.ttkproject",
            "Transparent.ttkproject",
            "Colorize.ttkproject",
            "Inversion.ttkproject",
            "Inversion by RGB.ttkproject"});
            this.comboBox1.Location = new System.Drawing.Point(85, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(164, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
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
            // stripBar1
            //
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 3;
            //
            // panel1
            //
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 24);
            this.panel1.TabIndex = 4;
            //
            // WinForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Pixel Layer";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
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
        /// Handles the Form Load event.
        /// Selects the first project entry in the combo box, which triggers
        /// <see cref="comboBox1_SelectedIndexChanged"/> and loads the raster layer.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// Opens the .ttkproject file that corresponds to the currently selected
        /// combo-box item. The project file encodes the raster layer source and
        /// all rendering parameters (colour mode, histogram settings, etc.).
        /// GIS.Open() replaces any previously loaded content with the new project.
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\Projects\" +
                      comboBox1.Items[comboBox1.SelectedIndex]
                    );
        }

        /// <summary>
        /// Shared click handler for all toolbar buttons.
        /// Dispatches to the appropriate GIS viewer action based on which
        /// button raised the event.
        /// </summary>
        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent) GIS.FullExtent();          // Fit all layers in view
            else if(sender == btnDrag) GIS.Mode = TGIS_ViewerMode.Drag;  // Pan mode
            else if(sender == btnZoom) GIS.Mode = TGIS_ViewerMode.Zoom;  // Zoom mode
        }
    }
}
