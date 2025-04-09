using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.RTL;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace TemplatePrint
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
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoom;
        private System.Windows.Forms.ToolStripButton btnDrag;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_ControlLegend;
        private System.Windows.Forms.Splitter splitter1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.WinForms.TGIS_ControlScale GIS_ControlScale;
        private TGIS_ControlNorthArrow GIS_ControlNorthArrow;
        private TatukGIS.NDK.WinForms.TGIS_ControlPrintPreviewSimple GIS_ControlPrintPreviewSimple;
        private TatukGIS.NDK.TGIS_TemplatePrint template;
        private TatukGIS.NDK.WinForms.TGIS_PrintManager manager;

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
            TatukGIS.NDK.TGIS_CSUnits tgiS_CSUnits2 = new TatukGIS.NDK.TGIS_CSUnits();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.GIS_ControlLegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_ControlNorthArrow = new TatukGIS.NDK.WinForms.TGIS_ControlNorthArrow();
            this.GIS_ControlScale = new TatukGIS.NDK.WinForms.TGIS_ControlScale();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.GIS_ControlPrintPreviewSimple = new TatukGIS.NDK.WinForms.TGIS_ControlPrintPreviewSimple();
            this.panel1.SuspendLayout();
            this.GIS.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 28);
            this.panel1.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(248, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 25);
            this.button3.TabIndex = 3;
            this.button3.Text = "Change";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(166, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "Design";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(85, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "Print";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullExtent,
            this.toolStripSeparator1,
            this.btnZoom,
            this.btnDrag,
            this.toolStripSeparator2});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 28);
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
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // btnZoom
            // 
            this.btnZoom.ImageIndex = 1;
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Checked = true;
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
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.toolStripLabel3});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            this.stripBar1.Text = "stripBar1";
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
            this.toolStripLabel2.Text = "Value";
            this.toolStripLabel2.Width = 98;
            // 
            // GIS_ControlLegend
            // 
            this.GIS_ControlLegend.CompactView = false;
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_ControlLegend.DialogOptions = tgiS_ControlLegendDialogOptions2;
            this.GIS_ControlLegend.Dock = System.Windows.Forms.DockStyle.Left;
            this.GIS_ControlLegend.GIS_Viewer = this.GIS;
            this.GIS_ControlLegend.Location = new System.Drawing.Point(0, 28);
            this.GIS_ControlLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_ControlLegend.Name = "GIS_ControlLegend";
            this.GIS_ControlLegend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)));
            this.GIS_ControlLegend.ReverseOrder = true;
            this.GIS_ControlLegend.Size = new System.Drawing.Size(145, 419);
            this.GIS_ControlLegend.TabIndex = 2;
            // 
            // GIS
            // 
            this.GIS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Controls.Add(this.GIS_ControlNorthArrow);
            this.GIS.Controls.Add(this.GIS_ControlScale);
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.IncrementalPaint = false;
            this.GIS.Location = new System.Drawing.Point(148, 28);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(444, 419);
            this.GIS.TabIndex = 4;
            this.GIS.UseRTree = false;
            this.GIS.AfterPaintEvent += new TatukGIS.NDK.TGIS_PaintEvent(this.GIS_AfterPaint);
            // 
            // GIS_ControlNorthArrow
            // 
            this.GIS_ControlNorthArrow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS_ControlNorthArrow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GIS_ControlNorthArrow.Color1 = System.Drawing.Color.Black;
            this.GIS_ControlNorthArrow.Color2 = System.Drawing.Color.Black;
            this.GIS_ControlNorthArrow.GIS_Viewer = this.GIS;
            this.GIS_ControlNorthArrow.Location = new System.Drawing.Point(312, 6);
            this.GIS_ControlNorthArrow.Name = "GIS_ControlNorthArrow";
            this.GIS_ControlNorthArrow.Path = null;
            this.GIS_ControlNorthArrow.Size = new System.Drawing.Size(128, 128);
            this.GIS_ControlNorthArrow.Style = TatukGIS.NDK.TGIS_ControlNorthArrowStyle.Rose2;
            this.GIS_ControlNorthArrow.TabIndex = 1;
            // 
            // GIS_ControlScale
            // 
            this.GIS_ControlScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Window;
            this.GIS_ControlScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GIS_ControlScale.DividerColor1 = System.Drawing.Color.Black;
            this.GIS_ControlScale.DividerColor2 = System.Drawing.Color.White;
            this.GIS_ControlScale.GIS_Viewer = this.GIS;
            this.GIS_ControlScale.Location = new System.Drawing.Point(312, 389);
            this.GIS_ControlScale.Name = "GIS_ControlScale";
            this.GIS_ControlScale.PrepareEvent = null;
            this.GIS_ControlScale.Size = new System.Drawing.Size(129, 25);
            this.GIS_ControlScale.TabIndex = 0;
            tgiS_CSUnits2.DescriptionEx = null;
            this.GIS_ControlScale.Units = tgiS_CSUnits2;
            this.GIS_ControlScale.UnitsEPSG = 0;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(145, 28);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 419);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // GIS_ControlPrintPreviewSimple
            // 
            this.GIS_ControlPrintPreviewSimple.GIS_Viewer = this.GIS;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Text = "Template";
            this.toolStripLabel3.Width = 450;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.GIS_ControlLegend);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - TemplatePrint";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.GIS.ResumeLayout(false);
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

            // copy template file to the current directory as .ttktemplate
            string src_path = TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\PrintTemplate\printtemplate.tpl";
            string tpl_path = System.IO.Directory.GetCurrentDirectory() + "\\printtemplate.ttktemplate";
            TGIS_TemplatePrintBuilder.CopyTemplateFile( src_path, tpl_path, false );

            // prepare template for printing
            template = new TGIS_TemplatePrint();
            template.TemplatePath = tpl_path;
            template.set_GIS_Viewer(1, GIS);
            template.set_GIS_Legend(1, GIS_ControlLegend);
            template.set_GIS_Scale(1, GIS_ControlScale);
            template.set_GIS_NorthArrow(1, GIS_ControlNorthArrow);
            template.set_GIS_ViewerExtent(1, GIS.Extent);
            template.set_Text(1, "Title");
            template.set_Text(2, "Copyright");

            // prepare print manager
            manager = new TGIS_PrintManager();
            manager.Template = template;

            stripBar1.Items[2].Text = System.IO.Path.GetFileName(tpl_path);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.Cursor cr;

            cr = GIS.Cursor;
            GIS.Cursor = Cursors.WaitCursor;
            try
            {
                GIS_ControlPrintPreviewSimple.Preview(manager);
            }
            finally
            {
                GIS.Cursor = cr;
            }
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent) GIS.FullExtent();
            else if (sender == btnZoom)
            {
                GIS.Mode = TGIS_ViewerMode.Zoom;
                btnZoom.Checked = true;
                btnDrag.Checked = !btnZoom.Checked;
            }
            else if (sender == btnDrag)
            {
                GIS.Mode = TGIS_ViewerMode.Drag;
                btnDrag.Checked = true;
                btnZoom.Checked = !btnDrag.Checked;
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

        private void GIS_AfterPaint(object sender, TGIS_PaintEventArgs e)
        {
            stripBar1.Items[1].Text = GIS.ScaleAsText;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //We generally recommend using the following static function to call the designer:
            //TGIS_ControlPrintTemplateDesignerForm.ShowPrintTemplateDesigner(template, workingFolder, customPage, snap);
            //
            //However, we present below an equivalent call.
            //This can be used as a pattern for classes inheriting from TGIS_ControlPrintTemplateDesignerForm.
            string workingFolder = "";
            string customPage = "";
            string snap = "";

            TGIS_ControlPrintTemplateDesignerForm frm = new TGIS_ControlPrintTemplateDesignerForm(true);
            try
            {
                frm.Snap = snap;
                DialogResult result = frm.Execute( template, workingFolder, customPage );
            }
            finally
            {
                frm.Dispose();
            }

            stripBar1.Items[2].Text = System.IO.Path.GetFileName(template.TemplatePath);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg;
            string old;

            dlg = new OpenFileDialog();
            dlg.Filter = "Print template|*.tpl;*.ttktemplate";
            dlg.FileName = "";
            dlg.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                old = template.TemplatePath;
                try
                {
                    template.TemplatePath = dlg.FileName;
                    stripBar1.Items[2].Text = System.IO.Path.GetFileName(template.TemplatePath);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    template.TemplatePath = old;
                }
            }
        }
    }
}
