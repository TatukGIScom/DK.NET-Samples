using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;
using System.IO;

namespace DirectWrite
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Button btnBuild;
        private Button btnImport;
        private Button btnMergeLayer;
        private Button btnWrite;
        private Button btnMergeHelper;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private int number;
        private Boolean exist;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnMergeLayer = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnMergeHelper = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.SuspendLayout();
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(1, 2);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 0;
            this.btnBuild.Text = "Build layer";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // btnImport
            // 
            this.btnImport.Enabled = false;
            this.btnImport.Location = new System.Drawing.Point(82, 2);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "Import layer";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnMergeLayer
            // 
            this.btnMergeLayer.Enabled = false;
            this.btnMergeLayer.Location = new System.Drawing.Point(163, 2);
            this.btnMergeLayer.Name = "btnMergeLayer";
            this.btnMergeLayer.Size = new System.Drawing.Size(75, 23);
            this.btnMergeLayer.TabIndex = 2;
            this.btnMergeLayer.Text = "Merge layer";
            this.btnMergeLayer.UseVisualStyleBackColor = true;
            this.btnMergeLayer.Click += new System.EventHandler(this.btnMergeLayer_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Enabled = false;
            this.btnWrite.Location = new System.Drawing.Point(244, 2);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 3;
            this.btnWrite.Text = "Direct write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // btnMergeHelper
            // 
            this.btnMergeHelper.Enabled = false;
            this.btnMergeHelper.Location = new System.Drawing.Point(325, 2);
            this.btnMergeHelper.Name = "btnMergeHelper";
            this.btnMergeHelper.Size = new System.Drawing.Size(84, 23);
            this.btnMergeHelper.TabIndex = 4;
            this.btnMergeHelper.Text = "Merge helper";
            this.btnMergeHelper.UseVisualStyleBackColor = true;
            this.btnMergeHelper.Click += new System.EventHandler(this.btnMergeHelper_Click);
            // 
            // GIS
            // 
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(1, 31);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(583, 429);
            this.GIS.TabIndex = 5;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(584, 461);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.btnMergeHelper);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.btnMergeLayer);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnBuild);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - DirectWrite";
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinForm());
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            GIS.Mode = TGIS_ViewerMode.Zoom;
            number = 0;
            exist = true;

            while (exist)
            {
                if (Directory.Exists("Shapes" + number))
                {
                    number++;
                }
                else
                {
                    exist = false;
                }
            }

            Directory.CreateDirectory("Shapes" + number);
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            TGIS_LayerSHP lv;
            TGIS_LayerSHP ll;

            btnImport.Enabled = true;
            btnMergeLayer.Enabled = false;
            btnMergeHelper.Enabled = false;
            btnImport.Enabled = true;

            GIS.Close();

            lv = new TGIS_LayerSHP();

            if (Directory.Exists("Shapes" + number))
            {
                number++;
                Directory.CreateDirectory("Shapes" + number);
            }

            lv.Build(("Shapes" + number + @"\build.shp"), TGIS_Utils.GisExtent(-180, -90, 180, 90), TGIS_ShapeType.Point, TGIS_DimensionType.XY);

            lv.Open();
            ll = new TGIS_LayerSHP();

            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\WorldDCW\cities.shp";
            ll.Open();

            lv.ImportStructure(ll);
            lv.CS = ll.CS;

            foreach ( TGIS_Shape shp in ll.Loop() )
            {
                lv.AddShape(shp, true);
            }


            lv.SaveData();

            GIS.Add(lv);
            GIS.FullExtent();
            GIS.InvalidateWholeMap();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            TGIS_LayerSHP ll;
            TGIS_LayerSHP lv;
            TGIS_Shape shp;

            btnMergeLayer.Enabled = true;

            GIS.Close();

            ll = new TGIS_LayerSHP();
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\WorldDCW\cities.shp";
            GIS.Add(ll);

            shp = TGIS_GeometryFactory.GisCreateShapeFromWKT("POLYGON((7.86 56.39,31.37 56.39,31.37 39.48,7.86 39.48,7.868 56.39))");

            lv = new TGIS_LayerSHP();
            lv.Path = "Shapes" + number + @"\imported.shp";
            lv.CS = ll.CS;
            lv.ImportLayerEx(ll, ll.Extent, TGIS_ShapeType.Unknown, "", shp, TGIS_Utils.GIS_RELATE_CONTAINS(), false);

            GIS.Add(lv);
            lv.Params.Marker.Color = TGIS_Color.Green;
            GIS.FullExtent();
            GIS.VisibleExtent = lv.Extent;
            GIS.InvalidateWholeMap();

        }

        private void btnMergeLayer_Click(object sender, EventArgs e)
        {
            TGIS_LayerSHP ll;
            TGIS_LayerSHP lv;
            TGIS_Shape shp;

            btnWrite.Enabled = true;

            GIS.Close();

            ll = new TGIS_LayerSHP();
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\WorldDCW\cities.shp";
            GIS.Add(ll);

            shp = TGIS_GeometryFactory.GisCreateShapeFromWKT("'POLYGON((7.86 56.39,31.37 56.39,31.37 39.48,7.86 39.48,7.868 56.39))");

            lv = new TGIS_LayerSHP();
            lv.Path = "Shapes" + number + @"\imported.shp";
            lv.CS = ll.CS;
            lv.MergeLayerEx(ll, ll.Extent, TGIS_ShapeType.Unknown, "", shp, TGIS_Utils.GIS_RELATE_DISJOINT(), false, false);

            GIS.Add(lv);
            lv.Params.Marker.Color = TGIS_Color.Green;

            GIS.FullExtent();

            GIS.InvalidateWholeMap();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            TGIS_LayerSHP lv;
            TGIS_LayerVector ll;
            TGIS_LayerVectorDirectWriteHelper dwh;

            btnMergeHelper.Enabled = true;

            GIS.Close();

            ll = new TGIS_LayerSHP();

            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\WorldDCW\cities.shp";
            ll.Open();

            lv = new TGIS_LayerSHP();
            lv.ImportStructure(ll);
            lv.CS = ll.CS;

            dwh = new TGIS_LayerVectorDirectWriteHelper(lv);
            dwh.Build(("Shapes" + number + @"\direct_write.shp"), ll.Extent, TGIS_ShapeType.Point, TGIS_DimensionType.XY);

            foreach ( TGIS_Shape shp in ll.Loop() )
            {
                dwh.AddShape(shp);
            }
            dwh.Close();

            GIS.Add(lv);
            GIS.FullExtent();
        }

        private void btnMergeHelper_Click(object sender, EventArgs e)
        {
            TGIS_LayerSHP lv;
            TGIS_LayerVector ll;
            TGIS_LayerVectorMergeHelper mh;

            btnMergeHelper.Enabled = false;
            GIS.Close();

            ll = new TGIS_LayerSHP();
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\WorldDCW\cities.shp";
            ll.Open();

            lv = new TGIS_LayerSHP();
            lv.ImportStructure(ll);
            lv.CS = ll.CS;
            lv.Build(("Shapes" + number + @"\merge_helper.shp"), ll.Extent, TGIS_ShapeType.Point, TGIS_DimensionType.XY);

            mh = new TGIS_LayerVectorMergeHelper(lv, 500);

            foreach (TGIS_Shape shp in ll.Loop())
            {
                mh.AddShape(shp);
                mh.Commit();
            }

            btnImport.Enabled = false;
            btnMergeLayer.Enabled = false;
            btnWrite.Enabled = false;

            GIS.Add(lv);
            GIS.FullExtent();
        }
    }
}
