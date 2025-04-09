using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace DK.WinForms.CS
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
        private Button btnOpen;
        private Button btnZoom;
        private Button btnDrag;
        private Button btnCreate;
        private Button btnFind;
        private Button btnSelect;
        private TGIS_ViewerWnd GIS;

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
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnDrag = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(13, 13);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(82, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open project";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.Location = new System.Drawing.Point(101, 13);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(99, 23);
            this.btnZoom.TabIndex = 1;
            this.btnZoom.Text = "Zooming";
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnDrag
            // 
            this.btnDrag.Location = new System.Drawing.Point(206, 13);
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(100, 23);
            this.btnDrag.TabIndex = 2;
            this.btnDrag.Text = "Dragging";
            this.btnDrag.UseVisualStyleBackColor = true;
            this.btnDrag.Click += new System.EventHandler(this.btnDrag_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(393, 13);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(85, 23);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create shape";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreateShape_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(484, 13);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(105, 23);
            this.btnFind.TabIndex = 5;
            this.btnFind.Text = "Find shape";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Location = new System.Drawing.Point(-1, 43);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(888, 517);
            this.GIS.TabIndex = 7;
            this.GIS.TapSimpleEvent += new TatukGIS.RTL.TGIS_TapEvent(this.GIS_TapSimpleEvent);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(312, 13);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Selecting";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click_1);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnDrag);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.btnOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Hello DK";
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //Open a project
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\\World\\WorldDCW\\world.shp");
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            //check if viewer is not empty, if is then exit
            if (GIS.IsEmpty) return;
            //set "Zoom" mode on viewer
            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            //check if viewer is not empty, if is then exit
            if (GIS.IsEmpty) return;
            //set "Select" mode on viewer
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void btnDrag_Click(object sender, EventArgs e)
        {
            //check if viewer is not empty, if is then exit
            if (GIS.IsEmpty) return;
            //set "Drag" mode on viewer
            GIS.Mode = TGIS_ViewerMode.Drag;
        }

        private void btnCreateShape_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_Shape shp;

            // lets find if such layer already exists
            ll = (TGIS_LayerVector)GIS.Get("edit layer");
            if (ll != null) return;


            // create a new layer and add it to the viewer
            ll = new TGIS_LayerVector();
            ll.Name = "edit layer";
            ll.CS = GIS.CS; // same coordinate system as a viewer

            // making inside of polygon transparent with blue border
            ll.Params.Area.OutlineColor = TGIS_Color.Blue;
            ll.Params.Area.Pattern = TGIS_BrushStyle.Clear;

            // add layer to the viewer
            GIS.Add(ll);

            // create a shape and add it to polygon
            shp = ll.CreateShape(TGIS_ShapeType.Polygon);



            // we group operation together 
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            // add some veritices
            shp.AddPoint(new TGIS_Point(10, 10));
            shp.AddPoint(new TGIS_Point(10, 80));
            shp.AddPoint(new TGIS_Point(80, 90));
            shp.AddPoint(new TGIS_Point(90, 10));

            // unlock operation, close shape if necessary
            shp.Unlock();

            // and now refresh map
            GIS.InvalidateWholeMap();
        }

        private void GIS_TapSimpleEvent(object _sender, TatukGIS.RTL.TGIS_TapEventArgs _e)
        {
            TGIS_Shape shp;
            TGIS_Point ptg;
            TGIS_LayerVector lv;
            double precision;

            // ignore clicking if mode is other then select
            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            // convert screen coordinates to map coordinates
            ptg = GIS.ScreenToMap(new Point((int)_e.X, (int)_e.Y));

            //get layer from the viewer
            lv = (TGIS_LayerVector)GIS.Items[0];

            // calculate precision of location as 5 pixels
            precision = 5 / GIS.Zoom;

            // let's try to locate a selected shape on the map
            shp = (TGIS_Shape)GIS.Locate(ptg, precision);

            // not found?
            if (shp == null) return;

            //deselect selected shapes
            lv.DeselectAll();

            // mark shape as selected
            shp.IsSelected = !shp.IsSelected;

            // and refresh a map
            GIS.InvalidateWholeMap();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_LayerVector lv;
            TGIS_Shape selShp;

            //get layer from the viewer
            ll = (TGIS_LayerVector)GIS.Get("edit layer");
            if (ll == null) return;

            // lets get a layer with world shape
            // names are constructed based on layer name
            lv = (TGIS_LayerVector)GIS.Get("world");

            //deselect selected shapes
            lv.DeselectAll();

            // and we need a created shape, with we want   
            // to use as selection shape
            selShp = ll.GetShape(1);  // just a first shape form the layer

            // for file based layer we should pin shape to memory
            // otherwise it should be discarded 
            selShp = selShp.MakeEditable();

            // Group all future screen updates into one drawing operatiiom
            GIS.Lock();

            // so now we search for all shapes with DE9-IM relationship
            // which labels starts with 's' (with use of SQL syntax)
            // in this case we find "T*****FF*" contains relationship
            // which means that we will find only shapes inside the polygon
            foreach (TGIS_Shape shp in lv.Loop(selShp.Extent, "label LIKE 's%'", selShp, "T*****FF*"))
            {
                shp.IsSelected = true;
            }

            //unlock updating of the viewer
            GIS.Unlock();

            // and now refresh map
            GIS.InvalidateWholeMap();
        }

        private void btnSelect_Click_1(object sender, EventArgs e)
        {
            //check if viewer is not empty, if is then exit
            if (GIS.IsEmpty) return;
            //set "Select" mode on viewer
            GIS.Mode = TGIS_ViewerMode.Select;
        }
    }
}
