using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.RTL;
using TatukGIS.NDK;

namespace Viewshed
{
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnFullExtent = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.gbMapMode = new System.Windows.Forms.GroupBox();
            this.rbtnAddObserver = new System.Windows.Forms.RadioButton();
            this.rbtnZoom = new System.Windows.Forms.RadioButton();
            this.gbVisibleLayer = new System.Windows.Forms.GroupBox();
            this.rbtnAGL = new System.Windows.Forms.RadioButton();
            this.rbtnViewshedFreq = new System.Windows.Forms.RadioButton();
            this.rbtnViewshedBinary = new System.Windows.Forms.RadioButton();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblObserverElevation = new System.Windows.Forms.Label();
            this.edtObserverElevation = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.gbMapMode.SuspendLayout();
            this.gbVisibleLayer.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Location = new System.Drawing.Point(169, 28);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.UserDefined;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(560, 340);
            this.GIS.TabIndex = 0;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.Location = new System.Drawing.Point(12, 12);
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(148, 23);
            this.btnFullExtent.TabIndex = 1;
            this.btnFullExtent.Text = "Full Extent";
            this.btnFullExtent.UseVisualStyleBackColor = true;
            this.btnFullExtent.Click += new System.EventHandler(this.btnFullExtent_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(12, 41);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(148, 23);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // gbMapMode
            // 
            this.gbMapMode.Controls.Add(this.rbtnAddObserver);
            this.gbMapMode.Controls.Add(this.rbtnZoom);
            this.gbMapMode.Location = new System.Drawing.Point(12, 70);
            this.gbMapMode.Name = "gbMapMode";
            this.gbMapMode.Size = new System.Drawing.Size(148, 69);
            this.gbMapMode.TabIndex = 3;
            this.gbMapMode.TabStop = false;
            this.gbMapMode.Text = "Map Mode";
            // 
            // rbtnAddObserver
            // 
            this.rbtnAddObserver.AutoSize = true;
            this.rbtnAddObserver.Checked = true;
            this.rbtnAddObserver.Location = new System.Drawing.Point(16, 42);
            this.rbtnAddObserver.Name = "rbtnAddObserver";
            this.rbtnAddObserver.Size = new System.Drawing.Size(90, 17);
            this.rbtnAddObserver.TabIndex = 1;
            this.rbtnAddObserver.TabStop = true;
            this.rbtnAddObserver.Text = "Add Observer";
            this.rbtnAddObserver.UseVisualStyleBackColor = true;
            this.rbtnAddObserver.CheckedChanged += new System.EventHandler(this.rbtnAddObserver_CheckedChanged);
            // 
            // rbtnZoom
            // 
            this.rbtnZoom.AutoSize = true;
            this.rbtnZoom.Location = new System.Drawing.Point(16, 19);
            this.rbtnZoom.Name = "rbtnZoom";
            this.rbtnZoom.Size = new System.Drawing.Size(52, 17);
            this.rbtnZoom.TabIndex = 0;
            this.rbtnZoom.Text = "Zoom";
            this.rbtnZoom.UseVisualStyleBackColor = true;
            this.rbtnZoom.CheckedChanged += new System.EventHandler(this.rbtnZoom_CheckedChanged);
            // 
            // gbVisibleLayer
            // 
            this.gbVisibleLayer.Controls.Add(this.rbtnAGL);
            this.gbVisibleLayer.Controls.Add(this.rbtnViewshedFreq);
            this.gbVisibleLayer.Controls.Add(this.rbtnViewshedBinary);
            this.gbVisibleLayer.Location = new System.Drawing.Point(12, 145);
            this.gbVisibleLayer.Name = "gbVisibleLayer";
            this.gbVisibleLayer.Size = new System.Drawing.Size(148, 94);
            this.gbVisibleLayer.TabIndex = 4;
            this.gbVisibleLayer.TabStop = false;
            this.gbVisibleLayer.Text = "Visible Layer";
            // 
            // rbtnAGL
            // 
            this.rbtnAGL.AutoSize = true;
            this.rbtnAGL.Location = new System.Drawing.Point(16, 65);
            this.rbtnAGL.Name = "rbtnAGL";
            this.rbtnAGL.Size = new System.Drawing.Size(123, 17);
            this.rbtnAGL.TabIndex = 2;
            this.rbtnAGL.Text = "Above-Ground-Level";
            this.rbtnAGL.UseVisualStyleBackColor = true;
            this.rbtnAGL.CheckedChanged += new System.EventHandler(this.rbtnAGL_CheckedChanged);
            // 
            // rbtnViewshedFreq
            // 
            this.rbtnViewshedFreq.AutoSize = true;
            this.rbtnViewshedFreq.Location = new System.Drawing.Point(16, 42);
            this.rbtnViewshedFreq.Name = "rbtnViewshedFreq";
            this.rbtnViewshedFreq.Size = new System.Drawing.Size(127, 17);
            this.rbtnViewshedFreq.TabIndex = 1;
            this.rbtnViewshedFreq.Text = "Viewshed (frequency)";
            this.rbtnViewshedFreq.UseVisualStyleBackColor = true;
            this.rbtnViewshedFreq.CheckedChanged += new System.EventHandler(this.rbtnViewshedColor_CheckedChanged);
            // 
            // rbtnViewshedBinary
            // 
            this.rbtnViewshedBinary.AutoSize = true;
            this.rbtnViewshedBinary.Checked = true;
            this.rbtnViewshedBinary.Location = new System.Drawing.Point(16, 19);
            this.rbtnViewshedBinary.Name = "rbtnViewshedBinary";
            this.rbtnViewshedBinary.Size = new System.Drawing.Size(108, 17);
            this.rbtnViewshedBinary.TabIndex = 0;
            this.rbtnViewshedBinary.TabStop = true;
            this.rbtnViewshedBinary.Text = "Viewshed (binary)";
            this.rbtnViewshedBinary.UseVisualStyleBackColor = true;
            this.rbtnViewshedBinary.CheckedChanged += new System.EventHandler(this.rbtnViewshedBinary_CheckedChanged);
            // 
            // lblHint
            // 
            this.lblHint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHint.Location = new System.Drawing.Point(166, 9);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(563, 16);
            this.lblHint.TabIndex = 5;
            this.lblHint.Text = "Click on the map to add an observer.";
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblObserverElevation
            // 
            this.lblObserverElevation.AutoSize = true;
            this.lblObserverElevation.Location = new System.Drawing.Point(12, 242);
            this.lblObserverElevation.Name = "lblObserverElevation";
            this.lblObserverElevation.Size = new System.Drawing.Size(137, 13);
            this.lblObserverElevation.TabIndex = 6;
            this.lblObserverElevation.Text = "Observer Elevation (meters)";
            // 
            // edtObserverElevation
            // 
            this.edtObserverElevation.Location = new System.Drawing.Point(12, 258);
            this.edtObserverElevation.Name = "edtObserverElevation";
            this.edtObserverElevation.Size = new System.Drawing.Size(148, 20);
            this.edtObserverElevation.TabIndex = 7;
            this.edtObserverElevation.Text = "30";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStrip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 371);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(737, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusStrip
            // 
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(0, 17);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 393);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.edtObserverElevation);
            this.Controls.Add(this.lblObserverElevation);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.gbVisibleLayer);
            this.Controls.Add(this.gbMapMode);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnFullExtent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Viewshed";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbMapMode.ResumeLayout(false);
            this.gbMapMode.PerformLayout();
            this.gbVisibleLayer.ResumeLayout(false);
            this.gbVisibleLayer.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Button btnFullExtent;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox gbMapMode;
        private System.Windows.Forms.RadioButton rbtnAddObserver;
        private System.Windows.Forms.RadioButton rbtnZoom;
        private System.Windows.Forms.GroupBox gbVisibleLayer;
        private System.Windows.Forms.RadioButton rbtnAGL;
        private System.Windows.Forms.RadioButton rbtnViewshedFreq;
        private System.Windows.Forms.RadioButton rbtnViewshedBinary;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblObserverElevation;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusStrip;
        private System.Windows.Forms.TextBox edtObserverElevation;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinForm());
        }

        const String SAMPLE_VIEWSHED_NAME = "Viewshed";
        const String SAMPLE_AGL_NAME = "Above-Ground-Level";

        private TGIS_LayerPixel lTerrain;
        private TGIS_LayerVector lObservers;
        private TGIS_LayerPixel lViewshed;
        private TGIS_LayerPixel lAGL;

        public WinForm()
        {
            InitializeComponent();
        }

        private void setLayerActive()
        {
            GIS.Lock();
            makeViewshedRamp();
            if (GIS.Get(SAMPLE_VIEWSHED_NAME) != null)
            {
                lViewshed.Active = !rbtnAGL.Checked;
                lAGL.Active = rbtnAGL.Checked;
                GIS.InvalidateWholeMap();
            }
            GIS.Unlock();

            showComment();
        }

        private void showComment()
        {
            if( rbtnViewshedBinary.Checked )
            {
                lblHint.Text = "Green - area of visibility.";
            }
            else
            if( rbtnViewshedFreq.Checked )
            {
                lblHint.Text = "Visibility frequency; " +
                               "Red - one  observer is visible; " +
                               "Green - all observers are visible.";
            }
            else
            if (rbtnAGL.Checked && lAGL != null)
            {
                lblHint.Text = "Minimum height that must be added to a nonvisible cell " +
                               "to make it visible by at least one observer; " +
                               "Red = " + Math.Round(lAGL.MaxHeight) + " m";
            }
        }

        private void makeViewshedRamp()
        {
            if (GIS.Get(SAMPLE_VIEWSHED_NAME) == null)
                return ;

            lViewshed.Transparency = 50;
            lViewshed.Params.Pixel.GridShadow = false;
            lViewshed.Params.Pixel.AltitudeMapZones.Clear();

            if (rbtnViewshedBinary.Checked)
            {
                lViewshed.GenerateRamp(
                  TGIS_Color.FromARGB(127, 0, 255, 0),
                  TGIS_Color.None,
                  TGIS_Color.FromARGB(127, 0, 255, 0),
                  lViewshed.MinHeight, 0.01,
                  lViewshed.MaxHeight, false,
                  (lViewshed.MaxHeight - lViewshed.MinHeight) / 100,
                  (lViewshed.MaxHeight - lViewshed.MinHeight) / 10,
                  null, false
                );
            }
            else
            if (rbtnViewshedFreq.Checked)
            {
                lViewshed.GenerateRamp(
                  TGIS_Color.FromARGB(127, 255, 0, 0),
                  TGIS_Color.None,
                  TGIS_Color.FromARGB(127, 0, 255, 0),
                  0, 0,
                  lViewshed.MaxHeight, false,
                  (lViewshed.MaxHeight - lViewshed.MinHeight) / 100,
                  (lViewshed.MaxHeight - lViewshed.MinHeight) / 10,
                  null, false
                );
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GIS.Lock();
            GIS.Open( TGIS_Utils.GisSamplesDataDirDownload() + "World\\Countries\\USA\\States\\California\\San Bernardino\\NED\\w001001.adf" );

            // obtain the DEM layer
            lTerrain = (TGIS_LayerPixel)GIS.Get("w001001");
            lTerrain.Params.Pixel.AltitudeMapZones.Clear();

            // create a layer for storing the observer locations
            lObservers = new TGIS_LayerVector();
            lObservers.Name = "Observers";
            lObservers.CS = lTerrain.CS;
            lObservers.Open();

            // add a symbol to represent observers
            lObservers.Params.Marker.Symbol =
              TGIS_Utils.SymbolList.Prepare("LIBSVG:std:TowerCommunication01");
            lObservers.Params.Marker.Color = TGIS_Color.White;
            lObservers.Params.Marker.OutlineColor = TGIS_Color.White;
            lObservers.Params.Marker.Size = -32;

            GIS.Add(lObservers);
            GIS.Unlock();
            GIS.FullExtent();
        }

        private void GIS_MouseDown(object sender, MouseEventArgs e)
        {
            TGIS_Point pt;
            TGIS_Shape shp;
            TGIS_Viewshed vs;
            Single elev;

            // read observer elevation offset
            if (GIS.Mode == TGIS_ViewerMode.UserDefined)
            {
                try
                {
                    elev = (Single)TatukGIS.RTL.__Global.DotStrToFloat(edtObserverElevation.Text);
                }
                catch
                {
                    MessageBox.Show("'" + edtObserverElevation.Text + "' is not a valid floating point value.");
                    return;
                }

                GIS.Lock();
                try
                {
                    // check if the point lays within the DEM area
                    pt = GIS.ScreenToMap(new Point(e.X, e.Y));
                    if (!TGIS_Utils.GisIsPointInsideExtent(pt, lTerrain.Extent))
                      return;

                    // add observer to the observer layer
                    shp = lObservers.CreateShape(TGIS_ShapeType.Point, TGIS_DimensionType.XY);
                    shp.AddPart();
                    shp.AddPoint(pt);

                    // remove previous viewshed/AGL layers
                    if (GIS.Get(SAMPLE_VIEWSHED_NAME) != null)
                    {
                        GIS.Delete(lAGL.Name);
                        lAGL = null;
                        GIS.Delete(lViewshed.Name);
                        lViewshed = null;
                    }

                    // create and set up the layer to host viewshed
                    lViewshed = new TGIS_LayerPixel();
                    lViewshed.Build(true, lTerrain.CS, lTerrain.Extent, lTerrain.BitWidth, lTerrain.BitHeight);
                    lViewshed.Name = SAMPLE_VIEWSHED_NAME;
                    lViewshed.Open();

                    // create and set up the layer to host above-ground-level
                    lAGL = new TGIS_LayerPixel();
                    lAGL.Build(true, lTerrain.CS, lTerrain.Extent, lTerrain.BitWidth, lTerrain.BitHeight);
                    lAGL.Name = SAMPLE_AGL_NAME;
                    lAGL.Open();

                    // create viewshed tool
                    vs = new TGIS_Viewshed();
                    // set the base observer elevation to be read from the DEM layer
                    vs.ObserverElevation = TGIS_ViewshedObserverElevation.OnDem;
                    // turn on the correction for earth curvature and refractivity
                    vs.CurvedEarth = true;

                    vs.Generate(lTerrain, lObservers, lViewshed, lAGL, 0.0f, "", elev);

                    lViewshed.Active = !rbtnAGL.Checked;
                    lAGL.Active = rbtnAGL.Checked;

                    GIS.Add(lAGL);
                    GIS.Add(lViewshed);
                    lAGL.Transparency = 50;
                    lViewshed.Transparency = 50;
                    lObservers.Move(-2);

                    // apply (binary or frequency) color ramp to the viewshed layer
                    makeViewshedRamp();

                    // apply color ramp to the AGL layer
                    lAGL.Params.Pixel.GridShadow = false;
                    lAGL.GenerateRamp(
                      TGIS_Color.FromARGB(127, 0, 255, 0),
                      TGIS_Color.None,
                      TGIS_Color.FromARGB(127, 255, 0, 0),
                      0, 1,
                      lAGL.MaxHeight, false,
                      (lAGL.MaxHeight - lAGL.MinHeight) / 100,
                      (lAGL.MaxHeight - lAGL.MinHeight) / 10,
                      null, false
                    );

                    GIS.InvalidateWholeMap();
                }
                finally
                {
                    GIS.Unlock();
                }
                showComment();
            }
        }

        private void GIS_MouseMove(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_Color cl;
            Double[] vals;
            Boolean transp;
            String str;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));

            cl = new TGIS_Color();
            vals = new Double[0];
            transp = new Boolean();

            str = "";

            if (lViewshed != null && lViewshed.Locate(ptg, ref cl, ref vals, ref transp))
                if( vals[0] != lViewshed.NoDataValue )
                    str = str + "Frequency: " + vals[0] ;
            if (lAGL != null && lAGL.Locate(ptg, ref cl, ref vals, ref transp))
                if( vals[0] != lAGL.NoDataValue )
                	str = str + "Above-Ground-Level: " + vals[0] ;
            statusStrip.Text = str;
        }

        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            GIS.FullExtent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            GIS.Lock();
            if (GIS.Get(SAMPLE_VIEWSHED_NAME) != null)
            {
                GIS.Delete(lAGL.Name);
                lAGL = null;
                GIS.Delete(lViewshed.Name);
                lViewshed = null;
            }
            lObservers.RevertAll();
            GIS.FullExtent();
            GIS.Unlock();
        }

        private void rbtnZoom_CheckedChanged(object sender, EventArgs e)
        {
            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        private void rbtnAddObserver_CheckedChanged(object sender, EventArgs e)
        {
            GIS.Mode = TGIS_ViewerMode.UserDefined;
        }

        private void rbtnViewshedBinary_CheckedChanged(object sender, EventArgs e)
        {
            setLayerActive();
        }

        private void rbtnViewshedColor_CheckedChanged(object sender, EventArgs e)
        {
            setLayerActive();
        }

        private void rbtnAGL_CheckedChanged(object sender, EventArgs e)
        {
            setLayerActive();
        }
    }
}
