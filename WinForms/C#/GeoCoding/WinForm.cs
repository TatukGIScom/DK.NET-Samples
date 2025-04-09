using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.RTL;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Geocoding
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox memRoute;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSmallRoads;
        private System.Windows.Forms.TrackBar trkSmallRoads;
        private System.Windows.Forms.Label lblHighways;
        private System.Windows.Forms.TrackBar trkHighways;
        private System.Windows.Forms.Label lblAddrFrom;
        private System.Windows.Forms.TextBox edtAddrFrom;
        private System.Windows.Forms.Label lblAddrTo;
        private System.Windows.Forms.TextBox edtAddrTo;
        private System.Windows.Forms.Button btnResolve;
        private System.Windows.Forms.Button btnRoute;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.TGIS_LayerVector layerSrc;
        private TatukGIS.NDK.TGIS_LayerVector layerRoute;
        private TatukGIS.NDK.TGIS_Geocoding geoObj;
        private TatukGIS.NDK.TGIS_ShortestPath rtrObj;
        private System.Windows.Forms.ToolTip toolTip1;
        private TatukGIS.NDK.WinForms.TGIS_ControlScale GIS_ControlScale;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            toolTip1.SetToolTip(this.edtAddrFrom,
                                 @"""Pen"", ""Pennsylvania"", ""Penn 12"""
                               );
            toolTip1.SetToolTip(this.edtAddrTo,
                                 @"""Pen"", ""Pennsylvania"", ""Penn 12"""
                               );
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
            TatukGIS.NDK.TGIS_CSUnits tgiS_CSUnits1 = new TatukGIS.NDK.TGIS_CSUnits();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.memRoute = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRoute = new System.Windows.Forms.Button();
            this.btnResolve = new System.Windows.Forms.Button();
            this.edtAddrTo = new System.Windows.Forms.TextBox();
            this.lblAddrTo = new System.Windows.Forms.Label();
            this.edtAddrFrom = new System.Windows.Forms.TextBox();
            this.lblAddrFrom = new System.Windows.Forms.Label();
            this.trkHighways = new System.Windows.Forms.TrackBar();
            this.lblHighways = new System.Windows.Forms.Label();
            this.trkSmallRoads = new System.Windows.Forms.TrackBar();
            this.lblSmallRoads = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_ControlScale = new TatukGIS.NDK.WinForms.TGIS_ControlScale();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkHighways)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSmallRoads)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = "";
            this.panel1.Controls.Add(this.memRoute);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(404, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(188, 466);
            this.panel1.TabIndex = 0;
            // 
            // memRoute
            // 
            this.memRoute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memRoute.Location = new System.Drawing.Point(0, 249);
            this.memRoute.Multiline = true;
            this.memRoute.Name = "memRoute";
            this.memRoute.ReadOnly = true;
            this.memRoute.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.memRoute.Size = new System.Drawing.Size(188, 217);
            this.memRoute.TabIndex = 1;
            this.memRoute.WordWrap = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRoute);
            this.groupBox1.Controls.Add(this.btnResolve);
            this.groupBox1.Controls.Add(this.edtAddrTo);
            this.groupBox1.Controls.Add(this.lblAddrTo);
            this.groupBox1.Controls.Add(this.edtAddrFrom);
            this.groupBox1.Controls.Add(this.lblAddrFrom);
            this.groupBox1.Controls.Add(this.trkHighways);
            this.groupBox1.Controls.Add(this.lblHighways);
            this.groupBox1.Controls.Add(this.trkSmallRoads);
            this.groupBox1.Controls.Add(this.lblSmallRoads);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 249);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Routing parameters";
            // 
            // btnRoute
            // 
            this.btnRoute.Location = new System.Drawing.Point(94, 214);
            this.btnRoute.Name = "btnRoute";
            this.btnRoute.Size = new System.Drawing.Size(75, 23);
            this.btnRoute.TabIndex = 9;
            this.btnRoute.Text = "Find &Route";
            this.btnRoute.Click += new System.EventHandler(this.btnRoute_Click);
            // 
            // btnResolve
            // 
            this.btnResolve.Location = new System.Drawing.Point(94, 160);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(75, 23);
            this.btnResolve.TabIndex = 8;
            this.btnResolve.Text = "Find &Address";
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // edtAddrTo
            // 
            this.edtAddrTo.Location = new System.Drawing.Point(24, 192);
            this.edtAddrTo.Name = "edtAddrTo";
            this.edtAddrTo.Size = new System.Drawing.Size(145, 20);
            this.edtAddrTo.TabIndex = 7;
            this.edtAddrTo.Text = "wash";
            // 
            // lblAddrTo
            // 
            this.lblAddrTo.Location = new System.Drawing.Point(24, 176);
            this.lblAddrTo.Name = "lblAddrTo";
            this.lblAddrTo.Size = new System.Drawing.Size(40, 13);
            this.lblAddrTo.TabIndex = 6;
            this.lblAddrTo.Text = "&To";
            // 
            // edtAddrFrom
            // 
            this.edtAddrFrom.Location = new System.Drawing.Point(24, 136);
            this.edtAddrFrom.Name = "edtAddrFrom";
            this.edtAddrFrom.Size = new System.Drawing.Size(145, 20);
            this.edtAddrFrom.TabIndex = 5;
            this.edtAddrFrom.Text = "Chrys 1345";
            // 
            // lblAddrFrom
            // 
            this.lblAddrFrom.Location = new System.Drawing.Point(24, 120);
            this.lblAddrFrom.Name = "lblAddrFrom";
            this.lblAddrFrom.Size = new System.Drawing.Size(40, 13);
            this.lblAddrFrom.TabIndex = 4;
            this.lblAddrFrom.Text = "&From";
            // 
            // trkHighways
            // 
            this.trkHighways.AutoSize = false;
            this.trkHighways.LargeChange = 1;
            this.trkHighways.Location = new System.Drawing.Point(16, 88);
            this.trkHighways.Minimum = 1;
            this.trkHighways.Name = "trkHighways";
            this.trkHighways.Size = new System.Drawing.Size(161, 25);
            this.trkHighways.TabIndex = 3;
            this.trkHighways.Value = 5;
            // 
            // lblHighways
            // 
            this.lblHighways.Location = new System.Drawing.Point(24, 72);
            this.lblHighways.Name = "lblHighways";
            this.lblHighways.Size = new System.Drawing.Size(90, 13);
            this.lblHighways.TabIndex = 2;
            this.lblHighways.Text = "Prefer &highway";
            // 
            // trkSmallRoads
            // 
            this.trkSmallRoads.AutoSize = false;
            this.trkSmallRoads.LargeChange = 1;
            this.trkSmallRoads.Location = new System.Drawing.Point(16, 40);
            this.trkSmallRoads.Minimum = 1;
            this.trkSmallRoads.Name = "trkSmallRoads";
            this.trkSmallRoads.Size = new System.Drawing.Size(161, 25);
            this.trkSmallRoads.TabIndex = 1;
            this.trkSmallRoads.Value = 1;
            // 
            // lblSmallRoads
            // 
            this.lblSmallRoads.Location = new System.Drawing.Point(24, 24);
            this.lblSmallRoads.Name = "lblSmallRoads";
            this.lblSmallRoads.Size = new System.Drawing.Size(100, 13);
            this.lblSmallRoads.TabIndex = 0;
            this.lblSmallRoads.Text = "Prefer &local roads";
            // 
            // GIS
            // 
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 0);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Size = new System.Drawing.Size(404, 466);
            this.GIS.TabIndex = 1;
            // 
            // GIS_ControlScale
            // 
            this.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Control;
            this.GIS_ControlScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GIS_ControlScale.DividerColor1 = System.Drawing.Color.Black;
            this.GIS_ControlScale.DividerColor2 = System.Drawing.Color.White;
            this.GIS_ControlScale.GIS_Viewer = this.GIS;
            this.GIS_ControlScale.Location = new System.Drawing.Point(8, 8);
            this.GIS_ControlScale.Name = "GIS_ControlScale";
            this.GIS_ControlScale.PrepareEvent = null;
            this.GIS_ControlScale.Size = new System.Drawing.Size(145, 25);
            this.GIS_ControlScale.TabIndex = 1;
            tgiS_CSUnits1.DescriptionEx = null;
            this.GIS_ControlScale.Units = tgiS_CSUnits1;
            this.GIS_ControlScale.UnitsEPSG = 904201;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS_ControlScale);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Geocoding & Routing";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Leave += new System.EventHandler(this.WinForm_Leave);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkHighways)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSmallRoads)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinForm());
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            GIS.Lock();
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\Projects\California.TTKPROJECT");
            layerSrc = (TGIS_LayerVector)(GIS.Get("streets"));

            if (layerSrc == null) return;

            GIS.VisibleExtent = layerSrc.Extent;

            // create route layer
            layerRoute = new TGIS_LayerVector();
            layerRoute.UseConfig = false;
            layerRoute.Params.Line.Color = TGIS_Color.Red;
            layerRoute.Params.Line.Width = -2;
            layerRoute.Params.Marker.OutlineWidth = 1;
            layerRoute.Name = "RouteDisplay";
            layerRoute.CS = GIS.CS;
            GIS.Add(layerRoute);

            // create geocoding object, set fields for routing
            geoObj = new TGIS_Geocoding(layerSrc);
            geoObj.Offset = 0.0001;
            geoObj.RoadName = "FULLNAME";
            geoObj.LFrom = "LFROMADD";
            geoObj.LTo = "LTOADD";
            geoObj.RFrom = "RFROMADD";
            geoObj.RTo = "RTOADD";

            // create path object and loadsource  layer data
            rtrObj = new TGIS_ShortestPath(GIS);
            rtrObj.LinkTypeEvent += new TGIS_LinkTypeEvent(doLinkType);
            rtrObj.LoadTheData(layerSrc);
            rtrObj.RoadName = "FULLNAME";

            GIS.Unlock();

            GIS_ControlScale.Units = TGIS_Utils.CSUnitsList.ByEPSG(9035); // mile
        }

        private void WinForm_Leave(object sender, System.EventArgs e)
        {
            layerRoute.Dispose();
            geoObj.Dispose();
            /*rtrObj.Dispose();*/
        }

        private void doLinkType(object _sender, TGIS_LinkTypeEventArgs _e)
        {
            if (!(_e.Shape.GetField("MTFCC").ToString().CompareTo("S1400") < 0))
                // local roads
                _e.LinkType = 1;
            else
                _e.LinkType = 0;
        }

        private void btnRoute_Click(object sender, System.EventArgs e)
        {
            int i;
            TGIS_Shape shp;
            int res;
            TGIS_Point pt_a;
            TGIS_Point pt_b;
            string ang;
            string oldnam;

            // calculate wages
            rtrObj.set_CostModifiers(0, 1 - 1 / 11.0 * trkHighways.Value);
            rtrObj.set_CostModifiers(1, 1 - 1 / 11.0 * trkSmallRoads.Value);

            // locate shapes meeting query
            res = geoObj.Parse(edtAddrFrom.Text);
            // if not found, ask for more details
            if (res > 0)
                edtAddrFrom.Text = geoObj.get_Query(0);
            else
                edtAddrFrom.AppendText(" ???");

            // remember starting point
            if (res <= 0) return;
            pt_a = geoObj.get_Point(0);

            res = geoObj.Parse(edtAddrTo.Text);
            if (res > 0) edtAddrTo.Text = geoObj.get_Query(0);
            else edtAddrTo.AppendText(" ???");

            // remember ending point
            if (res <= 0) return;
            pt_b = geoObj.get_Point(0);

            // set starting and ending position
            rtrObj.UpdateTheData();
            rtrObj.Find(layerRoute.Unproject(pt_a), layerRoute.Unproject(pt_b));

            memRoute.Clear();
            oldnam = "#$@3eqewe";

            // display directions
            ang = "";
            for (i = 0; i < rtrObj.ItemsCount; i++)
            {
                switch (rtrObj.get_Items(i).Compass)
                {
                    case 0:
                        ang = "FWD  ";
                        break;
                    case 1:
                        ang = "RIGHT";
                        break;
                    case 2:
                        ang = "RIGHT";
                        break;
                    case 3:
                        ang = "RIGHT";
                        break;
                    case 4:
                        ang = "BACK ";
                        break;
                    case -1:
                        ang = "LEFT ";
                        break;
                    case -2:
                        ang = "LEFT ";
                        break;
                    case -3:
                        ang = "LEFT ";
                        break;
                    case -4:
                        ang = "BACK ";
                        break;
                }

                if (oldnam == rtrObj.get_Items(i).Name) continue;
                oldnam = rtrObj.get_Items(i).Name;

                memRoute.AppendText(ang + " " + rtrObj.get_Items(i).Name + "\r\n");
            }

            layerRoute.RevertShapes();

            // add shapes of our path to route layer (red)
            for (i = 0; i < rtrObj.ItemsCount; i++)
            {
                shp = rtrObj.get_Items(i).Layer.GetShape(rtrObj.get_Items(i).Uid);
                if (shp == null) continue;
                layerRoute.AddShape(shp);
                if (i == 0)
                    layerRoute.Extent = shp.Extent;
            }

            // mark starting point as green squere
            shp = layerRoute.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(pt_a);
            shp.Params.Marker.Color = TGIS_Color.Green;
            shp.Unlock();

            // mark starting point as red squere
            shp = layerRoute.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(pt_b);
            shp.Unlock();

            GIS.Lock();
            GIS.VisibleExtent = layerRoute.Extent;
            GIS.Zoom = 0.7 * GIS.Zoom;
            GIS.Unlock();
        }

        private void btnResolve_Click(object sender, System.EventArgs e)
        {
            int i;
            int r;
            TGIS_Shape shp;

            if (geoObj == null) return;

            layerRoute.RevertShapes();

            // locate shapes meeting query
            r = geoObj.Parse(edtAddrFrom.Text) - 1;
            if (r <= 0)
                edtAddrFrom.AppendText(" ???");

            for (i = 0; i <= r; i++)
            {
                edtAddrFrom.Text = geoObj.get_Query(i);
                Application.DoEvents();

                // add found shape to route layer (red color)
                shp = layerSrc.GetShape(geoObj.get_Uid(i));
                layerRoute.AddShape(shp);

                if (i == 0)
                    layerRoute.Extent = shp.ProjectedExtent;

                shp = layerSrc.GetShape(geoObj.get_UidEx(i));
                if (shp != null)
                    layerRoute.AddShape(shp);

                // mark address as green squere
                shp = layerRoute.CreateShape(TGIS_ShapeType.Point);
                shp.Lock(TGIS_Lock.Extent);
                shp.AddPart();
                shp.AddPoint(layerRoute.CS.FromCS(layerSrc.CS, geoObj.get_Point(i)));
                shp.Params.Marker.Color = TGIS_Color.Green;
                shp.Unlock();
            }
            GIS.Lock();
            GIS.VisibleExtent = layerRoute.ProjectedExtent;
            GIS.Zoom = 0.7 * GIS.Zoom;
            GIS.Unlock();
        }
    }
}
