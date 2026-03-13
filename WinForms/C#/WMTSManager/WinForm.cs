using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace WMTSManager
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Button btnNew;
        private ImageList ilIcons;
        private Button btnClose;
        private Button btnFulLExtent;
        private Button btnZoom;
        private Button btnDrag;
        private Button btnSelect;
        private TGIS_ControlLegend ControlLegend;
        private TGIS_ViewerWnd GIS;
        private static WinForm form;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            this.btnNew = new System.Windows.Forms.Button();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFulLExtent = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnDrag = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.ControlLegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.ImageIndex = 0;
            this.btnNew.ImageList = this.ilIcons;
            this.btnNew.Location = new System.Drawing.Point(0, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(28, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Fuchsia;
            this.ilIcons.Images.SetKeyName(0, "FullScreen.bmp");
            this.ilIcons.Images.SetKeyName(1, "Open.bmp");
            this.ilIcons.Images.SetKeyName(2, "FullExtent.bmp");
            this.ilIcons.Images.SetKeyName(3, "ZoomEx.bmp");
            this.ilIcons.Images.SetKeyName(4, "Drag.bmp");
            this.ilIcons.Images.SetKeyName(5, "SelectLocate.bmp");
            this.ilIcons.Images.SetKeyName(6, "Close.bmp");
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ImageIndex = 6;
            this.btnClose.ImageList = this.ilIcons;
            this.btnClose.Location = new System.Drawing.Point(27, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnFulLExtent
            // 
            this.btnFulLExtent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFulLExtent.ImageIndex = 2;
            this.btnFulLExtent.ImageList = this.ilIcons;
            this.btnFulLExtent.Location = new System.Drawing.Point(54, 0);
            this.btnFulLExtent.Name = "btnFulLExtent";
            this.btnFulLExtent.Size = new System.Drawing.Size(28, 23);
            this.btnFulLExtent.TabIndex = 2;
            this.btnFulLExtent.UseVisualStyleBackColor = true;
            this.btnFulLExtent.Click += new System.EventHandler(this.btnFulLExtent_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoom.ImageIndex = 3;
            this.btnZoom.ImageList = this.ilIcons;
            this.btnZoom.Location = new System.Drawing.Point(81, 0);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(28, 23);
            this.btnZoom.TabIndex = 3;
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnDrag
            // 
            this.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrag.ImageIndex = 4;
            this.btnDrag.ImageList = this.ilIcons;
            this.btnDrag.Location = new System.Drawing.Point(108, 0);
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(28, 23);
            this.btnDrag.TabIndex = 4;
            this.btnDrag.UseVisualStyleBackColor = true;
            this.btnDrag.Click += new System.EventHandler(this.btnDrag_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.ImageIndex = 5;
            this.btnSelect.ImageList = this.ilIcons;
            this.btnSelect.Location = new System.Drawing.Point(135, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(28, 23);
            this.btnSelect.TabIndex = 5;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // ControlLegend
            // 
            this.ControlLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.ControlLegend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.ControlLegend.GIS_Group = null;
            this.ControlLegend.GIS_Layer = null;
            this.ControlLegend.GIS_Viewer = this.GIS;
            this.ControlLegend.Location = new System.Drawing.Point(0, 29);
            this.ControlLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.ControlLegend.Name = "ControlLegend";
            this.ControlLegend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.ControlLegend.ReverseOrder = false;
            this.ControlLegend.Size = new System.Drawing.Size(164, 514);
            this.ControlLegend.TabIndex = 7;
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(170, 29);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(584, 514);
            this.GIS.TabIndex = 8;
            // 
            // WinForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(749, 566);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.ControlLegend);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnDrag);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.btnFulLExtent);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNew);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - WMTSManager";
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
            form = new WinForm();
            Application.Run(form);
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            // to solve WebException "Could not create SSL/TLS secure channel."
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;// | SecurityProtocolType.Ssl3;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            WMTSForm wfs;

            wfs = new WMTSForm();
            wfs.setGIS(GIS);
            wfs.ShowDialog();
        }

        public void AppendCovarage(String _path)
        {
            TGIS_Layer ll;

            ll = TGIS_Utils.GisCreateLayer(Path.GetFileName(_path), _path);

            if (ll != null)
            {
                ll.ReadConfig();
                try
                {
                    GIS.Add(ll);
                }
                catch
                {
                    ll = null;
                }
            }


            ControlLegend.GIS_Layer = ll;

            if (GIS.Items.Count == 1)
                GIS.FullExtent();
            else
                GIS.InvalidateWholeMap();
        }

        private void btnFulLExtent_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.FullExtent();
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        private void btnDrag_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.Mode = TGIS_ViewerMode.Drag;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.Close();
        }
    }
}
