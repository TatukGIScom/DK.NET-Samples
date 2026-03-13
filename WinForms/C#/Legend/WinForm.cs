using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Legend
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
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_ControlLegend1;
        private System.Windows.Forms.Splitter splitter1;
        private Panel panel1;
        private Button btnGroups;
        private Button btnLayers;
        private ToolStrip toolStrip1;
        private ToolStripButton btnFullExtent;
        private ToolStripButton btnZoom;
        private ToolStripButton btnDrag;
        private ToolStripButton btnSaveConfig;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.GIS_ControlLegend1 = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnGroups = new System.Windows.Forms.Button();
            this.btnLayers = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveConfig = new System.Windows.Forms.ToolStripButton();
            //((System.ComponentModel.ISupportInitialize)(this.toolStripLabel1)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.toolStripLabel2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1,
            this.toolStripLabel2});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 2;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Scale :";
            this.toolStripLabel1.Width = 50;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Width = 525;
            // 
            // GIS_ControlLegend1
            // 
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_ControlLegend1.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_ControlLegend1.Dock = System.Windows.Forms.DockStyle.Left;
            this.GIS_ControlLegend1.GIS_Group = null;
            this.GIS_ControlLegend1.GIS_Layer = null;
            this.GIS_ControlLegend1.GIS_Viewer = this.GIS;
            this.GIS_ControlLegend1.Location = new System.Drawing.Point(0, 28);
            this.GIS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_ControlLegend1.Name = "GIS_ControlLegend1";
            this.GIS_ControlLegend1.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)));
            this.GIS_ControlLegend1.ReverseOrder = true;
            this.GIS_ControlLegend1.Size = new System.Drawing.Size(145, 419);
            this.GIS_ControlLegend1.TabIndex = 6;
            // 
            // GIS
            // 
            this.GIS.BackColor = System.Drawing.SystemColors.Control;
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.IncrementalPaint = false;
            this.GIS.Location = new System.Drawing.Point(148, 28);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Size = new System.Drawing.Size(444, 419);
            this.GIS.TabIndex = 8;
            this.GIS.AfterPaintEvent += new TGIS_PaintEvent(this.GIS_AfterPaint);
            // 
            // btnGroups
            // 
            this.btnGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGroups.Location = new System.Drawing.Point(73, 417);
            this.btnGroups.Name = "btnGroups";
            this.btnGroups.Size = new System.Drawing.Size(67, 23);
            this.btnGroups.TabIndex = 1;
            this.btnGroups.Text = "Groups";
            this.btnGroups.UseVisualStyleBackColor = true;
            this.btnGroups.Click += new System.EventHandler(this.btnGroups_Click);
            // 
            // btnLayers
            // 
            this.btnLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLayers.Location = new System.Drawing.Point(3, 417);
            this.btnLayers.Name = "btnLayers";
            this.btnLayers.Size = new System.Drawing.Size(67, 23);
            this.btnLayers.TabIndex = 0;
            this.btnLayers.Text = "Layers";
            this.btnLayers.UseVisualStyleBackColor = true;
            this.btnLayers.Click += new System.EventHandler(this.btnLayers_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(145, 28);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 419);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 28);
            this.panel1.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnZoom,
            this.btnDrag,
            this.btnFullExtent,
            this.toolStripSeparator1,
            this.btnSaveConfig });
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(111, 28);
            this.toolStrip1.TabIndex = 1;
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
            this.toolStripSeparator1.Name = "toolStripSeparator1";
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
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.ImageIndex = 3;
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Click += toolStrip1_ButtonClick;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.btnGroups);
            this.Controls.Add(this.btnLayers);
            this.Controls.Add(this.GIS_ControlLegend1);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Legend";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
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
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\poland.ttkproject");
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
            }
            else if(sender == btnDrag)
            {
                GIS.Mode = TGIS_ViewerMode.Drag;
            }
            else if(sender == btnSaveConfig)
            {
                if (GIS.IsEmpty) return;
                GIS.SaveAll();
            }
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

        private void btnLayers_Click(object sender, EventArgs e)
        {
            GIS_ControlLegend1.Mode = TGIS_ControlLegendMode.Layers;
        }

        private void btnGroups_Click(object sender, EventArgs e)
        {
            GIS_ControlLegend1.Mode = TGIS_ControlLegendMode.Groups;
        }

        private void GIS_AfterPaint(object sender, TGIS_PaintEventArgs e)
        {
            stripBar1.Items[1].Text = GIS.ScaleAsText;
        }
    }
}
