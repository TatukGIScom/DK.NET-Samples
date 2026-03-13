using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace PixelOperations
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private ImageList imlButtons;
        private Button btnFulLExtent;
        private Button btnZoom;
        private Button btnDrag;
        private CheckBox cbPixels;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_Legend;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private Button btnOpen;
        private OpenFileDialog dgOpen;

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
            this.imlButtons = new System.Windows.Forms.ImageList(this.components);
            this.btnFulLExtent = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnDrag = new System.Windows.Forms.Button();
            this.cbPixels = new System.Windows.Forms.CheckBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.dgOpen = new System.Windows.Forms.OpenFileDialog();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.SuspendLayout();
            // 
            // imlButtons
            // 
            this.imlButtons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlButtons.ImageStream")));
            this.imlButtons.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imlButtons.Images.SetKeyName(0, "FullExtent.bmp");
            this.imlButtons.Images.SetKeyName(1, "ZoomEx.bmp");
            this.imlButtons.Images.SetKeyName(2, "Drag.bmp");
            this.imlButtons.Images.SetKeyName(3, "Open.bmp");
            this.imlButtons.Images.SetKeyName(4, "3DRotate.bmp");
            // 
            // btnFulLExtent
            // 
            this.btnFulLExtent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFulLExtent.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnFulLExtent.ImageIndex = 0;
            this.btnFulLExtent.ImageList = this.imlButtons;
            this.btnFulLExtent.Location = new System.Drawing.Point(29, 0);
            this.btnFulLExtent.Name = "btnFulLExtent";
            this.btnFulLExtent.Size = new System.Drawing.Size(30, 25);
            this.btnFulLExtent.TabIndex = 3;
            this.btnFulLExtent.UseVisualStyleBackColor = true;
            this.btnFulLExtent.Click += new System.EventHandler(this.btnFulLExtent_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoom.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnZoom.ImageIndex = 1;
            this.btnZoom.ImageList = this.imlButtons;
            this.btnZoom.Location = new System.Drawing.Point(58, 0);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(30, 25);
            this.btnZoom.TabIndex = 4;
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnDrag
            // 
            this.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrag.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnDrag.ImageIndex = 2;
            this.btnDrag.ImageList = this.imlButtons;
            this.btnDrag.Location = new System.Drawing.Point(87, 0);
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(30, 25);
            this.btnDrag.TabIndex = 5;
            this.btnDrag.UseVisualStyleBackColor = true;
            this.btnDrag.Click += new System.EventHandler(this.btnDrag_Click);
            // 
            // cbPixels
            // 
            this.cbPixels.AutoSize = true;
            this.cbPixels.Location = new System.Drawing.Point(123, 5);
            this.cbPixels.Name = "cbPixels";
            this.cbPixels.Size = new System.Drawing.Size(92, 17);
            this.cbPixels.TabIndex = 6;
            this.cbPixels.Text = "Change pixels";
            this.cbPixels.UseVisualStyleBackColor = true;
            this.cbPixels.CheckedChanged += new System.EventHandler(this.cbPixels_CheckedChanged);
            // 
            // btnOpen
            // 
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnOpen.ImageIndex = 3;
            this.btnOpen.ImageList = this.imlButtons;
            this.btnOpen.Location = new System.Drawing.Point(0, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(30, 25);
            this.btnOpen.TabIndex = 9;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // GIS
            // 
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(0, 34);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(582, 526);
            this.GIS.TabIndex = 8;
            // 
            // GIS_Legend
            // 
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_Legend.GIS_Group = null;
            this.GIS_Legend.GIS_Layer = null;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(583, 34);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GIS_Legend.ReverseOrder = false;
            this.GIS_Legend.Size = new System.Drawing.Size(201, 526);
            this.GIS_Legend.TabIndex = 7;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GIS_Legend);
            this.Controls.Add(this.cbPixels);
            this.Controls.Add(this.btnDrag);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.btnFulLExtent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - PixelOperation";

            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
            dgOpen.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.All, false);

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\San Bernardino\DOQ\37134877.jpg");
            cbPixels_CheckedChanged(sender, e);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            dgOpen.ShowDialog();

            GIS.Open(dgOpen.FileName);
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

        private void cbPixels_CheckedChanged(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;

            if (GIS.IsEmpty) return;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            if (cbPixels.Checked)
                lp.PixelOperationEvent += changePixels;
            else
                lp.PixelOperationEvent -= changePixels;

            GIS.InvalidateWholeMap();
        }

        private Boolean changePixels(
            Object _layer,
            TGIS_Extent _ext,
            int[] _source,
            ref int[] _output,
            int _width,
            int _height
            )
        {
            int rmaxval, rminval;
            int gmaxval, gminval;
            int bmaxval, bminval;
            int rdelta;
            int gdelta;
            int bdelta;
            int r, g, b;
            int j;
            TGIS_Color pixval;

            pixval = new TGIS_Color();

            rmaxval = -1000;
            rminval = 1000;
            gmaxval = -1000;
            gminval = 1000;
            bmaxval = -1000;
            bminval = 1000;
            for (j = 0; j < _source.Length - 1; j++)
            {
                pixval.ARGB = (uint)_source[j];
                r = pixval.R & 0xFF;
                g = pixval.G & 0xFF;
                b = pixval.B & 0xFF;

                if (r > rmaxval)
                    rmaxval = r;
                if (g > gmaxval)
                    gmaxval = g;
                if (b > bmaxval)
                    bmaxval = b;

                if (r < rminval)
                    rminval = r;
                if (g < gminval)
                    gminval = g;
                if (b < bminval)
                    bminval = b;
            }
            rdelta = Math.Max(1, rmaxval - rminval);
            gdelta = Math.Max(1, gmaxval - gminval);
            bdelta = Math.Max(1, bmaxval - bminval);

            for (j = 0; j < _source.Length - 1; j++)
            {
                pixval.ARGB = (uint)_source[j];
                r = pixval.R & 0xFF;
                g = pixval.G & 0xFF;
                b = pixval.B & 0xFF;
                r = (int)((((double)(r - rminval) / rdelta)) * 255);
                g = (int)((((double)(g - gminval) / gdelta)) * 255);
                b = (int)((((double)(b - bminval) / bdelta)) * 255);
                pixval = TGIS_Color.FromARGB((byte)0xFF, (byte)r, (byte)g, (byte)b);
                _output[j] = (int)pixval.ARGB;
            }
            return true;
        }
    }
}
