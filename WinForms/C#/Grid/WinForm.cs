using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Grid
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnZoom;
        private System.Windows.Forms.ToolStripButton btnDrag;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_ControlLegend;
        private Button btnUserINI;
        private Button btnShadow;

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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions2 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUserINI = new System.Windows.Forms.Button();
            this.btnShadow = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.GIS_ControlLegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.toolStripLabel1)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.toolStripLabel2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUserINI);
            this.panel1.Controls.Add(this.btnShadow);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 28);
            this.panel1.TabIndex = 0;
            // 
            // btnUserINI
            // 
            this.btnUserINI.Location = new System.Drawing.Point(345, 2);
            this.btnUserINI.Name = "btnUserINI";
            this.btnUserINI.Size = new System.Drawing.Size(75, 23);
            this.btnUserINI.TabIndex = 4;
            this.btnUserINI.Text = "User INI";
            this.btnUserINI.UseVisualStyleBackColor = true;
            this.btnUserINI.Click += new System.EventHandler(this.btnUserINI_Click);
            // 
            // btnShadow
            // 
            this.btnShadow.Location = new System.Drawing.Point(169, 2);
            this.btnShadow.Name = "btnShadow";
            this.btnShadow.Size = new System.Drawing.Size(86, 23);
            this.btnShadow.TabIndex = 3;
            this.btnShadow.Text = "Shadow on/off";
            this.btnShadow.UseVisualStyleBackColor = true;
            this.btnShadow.Click += new System.EventHandler(this.btnShadow_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(426, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(78, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Reload INI";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(261, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "User Defined";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Clear";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnZoom,
            this.btnDrag,
            this.btnFullExtent,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(593, 28);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += toolStrip1_ButtonClick;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Click += toolStrip1_ButtonClick;
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
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Click += toolStrip1_ButtonClick;
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
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1,
            this.toolStripLabel2});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            this.stripBar1.Text = "stripBar1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Altitude:";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Text = "Value";
            this.toolStripLabel2.Width = 475;
            // 
            // GIS_ControlLegend
            // 
            this.GIS_ControlLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_ControlLegend.DialogOptions = tgiS_ControlLegendDialogOptions2;
            this.GIS_ControlLegend.GIS_Group = null;
            this.GIS_ControlLegend.GIS_Layer = null;
            this.GIS_ControlLegend.GIS_Viewer = this.GIS;
            this.GIS_ControlLegend.Location = new System.Drawing.Point(0, 28);
            this.GIS_ControlLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_ControlLegend.Name = "GIS_ControlLegend";
            this.GIS_ControlLegend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)));
            this.GIS_ControlLegend.ReverseOrder = true;
            this.GIS_ControlLegend.Size = new System.Drawing.Size(145, 419);
            this.GIS_ControlLegend.TabIndex = 3;
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.IncrementalPaint = false;
            this.GIS.Location = new System.Drawing.Point(148, 28);
            this.GIS.MinZoomSize = -5;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(444, 419);
            this.GIS.TabIndex = 1;
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GIS_ControlLegend);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Grid";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.toolStripLabel1)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.toolStripLabel2)).EndInit();
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
            // open a file
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf");
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if(sender == btnFullExtent)
            {
                GIS.FullExtent();
            }
            else if(sender == btnZoom)
            {
                GIS.Mode = TGIS_ViewerMode.Zoom;
                btnDrag.Checked = false;
                btnZoom.Checked = true;
            }
            else if (sender == btnDrag) 
            { 
                GIS.Mode = TGIS_ViewerMode.Drag;
                btnZoom.Checked = false;
                btnDrag.Checked = true;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            TGIS_LayerPixel ll;

            ll = (TGIS_LayerPixel)(GIS.Items[0]);
            ll.Params.Pixel.AltitudeMapZones.Clear();
            GIS.InvalidateWholeMap();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            TGIS_LayerPixel ll;

            ll = (TGIS_LayerPixel)(GIS.Items[0]);
            ll.Params.Pixel.AltitudeMapZones.Clear();
            ll.Params.Pixel.AltitudeMapZones.Add("200,  400 , OLIVE , VERY LOW");
            ll.Params.Pixel.AltitudeMapZones.Add("400,  700 , OLIVE , LOW");
            ll.Params.Pixel.AltitudeMapZones.Add("700,  900 , GREEN , MID");
            ll.Params.Pixel.AltitudeMapZones.Add("900,  1200, BLUE  , HIGH");
            ll.Params.Pixel.AltitudeMapZones.Add("1200, 1700, RED   , SKY");
            ll.Params.Pixel.AltitudeMapZones.Add("1700, 2200, YELLOW, HEAVEN");
            GIS.InvalidateWholeMap();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            TGIS_LayerPixel ll;

            ll = (TGIS_LayerPixel)(GIS.Items[0]);
            ll.ConfigName = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf";
            ll.RereadConfig();
            GIS.InvalidateWholeMap();
        }

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

        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_LayerPixel ll;
            TGIS_Color rgb = new TGIS_Color();
            Double[] natives = null;
            Boolean transp = false;

            if (GIS.IsEmpty) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            ll = (TGIS_LayerPixel)(GIS.Items[0]);

            if (ll.Locate(ptg, ref rgb, ref natives, ref transp))
                stripBar1.Items[1].Text = natives[0].ToString();
            else
                stripBar1.Items[1].Text = "Unknown";
        }

        private void btnShadow_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel ll;

            ll = (TGIS_LayerPixel)(GIS.Items[0]);
            ll.Params.Pixel.GridShadow = !ll.Params.Pixel.GridShadow;
            GIS.InvalidateWholeMap();
        }

        private void btnUserINI_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel ll;

            ll = (TGIS_LayerPixel)(GIS.Items[0]);
            ll.ConfigName = TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\Projects\dem_ned.ini";
            ll.RereadConfig();
            GIS.InvalidateWholeMap();
        }
    }
}
