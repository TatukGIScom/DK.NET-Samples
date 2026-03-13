using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace AddLayer
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Button btnCutting;
        private Button btnZoom;
        private TGIS_ViewerWnd GIS;
        private TGIS_LayerVector ll;
        private TGIS_LayerPixel lp;
        private TGIS_ControlLegend tgiS_ControlLegend1;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        public WinForm()
        {
            InitializeComponent();
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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.btnCutting = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.tgiS_ControlLegend1 = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.SuspendLayout();
            // 
            // btnCutting
            // 
            this.btnCutting.Location = new System.Drawing.Point(1, 2);
            this.btnCutting.Name = "btnCutting";
            this.btnCutting.Size = new System.Drawing.Size(86, 23);
            this.btnCutting.TabIndex = 0;
            this.btnCutting.Text = "Do cutting";
            this.btnCutting.UseVisualStyleBackColor = true;
            this.btnCutting.Click += new System.EventHandler(this.btnCutting_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.Location = new System.Drawing.Point(93, 2);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(75, 23);
            this.btnZoom.TabIndex = 1;
            this.btnZoom.Text = "Zoom";
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraPosition = null;
            this.GIS.CursorForCameraRotation = null;
            this.GIS.CursorForCameraXY = null;
            this.GIS.CursorForCameraXYZ = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.CursorForSelect = null;
            this.GIS.CursorForSunPosition = null;
            this.GIS.CursorForZoom = null;
            this.GIS.Location = new System.Drawing.Point(1, 31);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(533, 380);
            this.GIS.TabIndex = 2;
            // 
            // tgiS_ControlLegend1
            // 
            this.tgiS_ControlLegend1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.tgiS_ControlLegend1.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.tgiS_ControlLegend1.GIS_Group = null;
            this.tgiS_ControlLegend1.GIS_Layer = null;
            this.tgiS_ControlLegend1.GIS_Viewer = this.GIS;
            this.tgiS_ControlLegend1.Location = new System.Drawing.Point(541, 31);
            this.tgiS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.tgiS_ControlLegend1.Name = "tgiS_ControlLegend1";
            this.tgiS_ControlLegend1.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.tgiS_ControlLegend1.ReverseOrder = false;
            this.tgiS_ControlLegend1.Size = new System.Drawing.Size(192, 380);
            this.tgiS_ControlLegend1.TabIndex = 3;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(734, 411);
            this.Controls.Add(this.tgiS_ControlLegend1);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.btnCutting);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - CuttingPolygon";
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
            TGIS_Shape shp;

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\VisibleEarth\world_8km.jpg");
            ll = new TGIS_LayerVector();
            ll.Name = "shape";
            GIS.Add(ll);

            shp = ll.CreateShape(TGIS_ShapeType.Polygon);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(-5, 8));
            shp.AddPoint(TGIS_Utils.GisPoint(40, 2));
            shp.AddPoint(TGIS_Utils.GisPoint(20, -20));
            shp.Unlock();
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        private void btnCutting_Click(object sender, EventArgs e)
        {
            lp = (TGIS_LayerPixel)(GIS.Items[0]);
            lp.CuttingPolygon = (TGIS_ShapePolygon)(ll.GetShape(1).CreateCopyCS(lp.CS));
            ll.Active = false;
            GIS.InvalidateWholeMap();
        }
    }
}
