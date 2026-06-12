using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Legend
{
    /// <summary>
    /// PrintPdf sample — demonstrates map printing to PDF format with customizable page layout and scaling.
    /// Loads vector and raster data, allows users to configure print settings (paper size, orientation, margins),
    /// and exports the map to a PDF file via the GIS.Print() API. Supports scaling to fit page and preserving
    /// spatial reference systems during export.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ImageList imageList1;
        private ToolStrip toolStrip1;
        private ToolStripButton btnFullExtent;
        private ToolStripButton btnZoom;
        private ToolStripButton btnDrag;
        private ToolStripSeparator toolStripSeparator1;
        private Panel panel1;
        private Button btnPrint;
        private GroupBox groupBox1;
        private RadioButton radioButton1;
        private RadioButton radioButton5;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private TGIS_ControlLegend GISLegend;
        private TGIS_ControlScale GISScale;
        private SaveFileDialog dlgSave;
        private TGIS_ViewerWnd GIS;
        private string PdfFileName;
        private StatusStrip stripBar1;
        private ToolStripStatusLabel toolStripLabel1;

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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions2 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            TatukGIS.NDK.TGIS_CSUnits tgiS_CSUnits2 = new TatukGIS.NDK.TGIS_CSUnits();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.GISLegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GISScale = new TatukGIS.NDK.WinForms.TGIS_ControlScale();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GIS.SuspendLayout();
            this.stripBar1.SuspendLayout();
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
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnZoom,
            this.btnDrag,
            this.btnFullExtent});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1368, 62);
            this.toolStrip1.TabIndex = 1;
            // 
            // btnZoom
            // 
            this.btnZoom.ImageIndex = 1;
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(58, 55);
            this.btnZoom.ToolTipText = "Zoom Mode";
            // 
            // btnDrag
            // 
            this.btnDrag.ImageIndex = 2;
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(58, 55);
            this.btnDrag.ToolTipText = "Drag Mode";
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(58, 55);
            this.btnFullExtent.ToolTipText = "Full Extent";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 62);
            this.panel1.Margin = new System.Windows.Forms.Padding(8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 828);
            this.panel1.TabIndex = 3;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(50, 532);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(225, 58);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton4);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Location = new System.Drawing.Point(15, 90);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(8);
            this.groupBox1.Size = new System.Drawing.Size(365, 428);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(18, 330);
            this.radioButton5.Margin = new System.Windows.Forms.Padding(8);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(130, 32);
            this.radioButton5.TabIndex = 4;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Multi-page print";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(18, 260);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(8);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(155, 32);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Use PrintPage event";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(18, 190);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(8);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(130, 32);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Print a template";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(18, 120);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(8);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(121, 32);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Standard print";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(18, 50);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(8);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(118, 32);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "GIS.PrintPdf()";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // GISLegend
            // 
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueSearchLimit = 16384;
            this.GISLegend.DialogOptions = tgiS_ControlLegendDialogOptions2;
            this.GISLegend.Dock = System.Windows.Forms.DockStyle.Right;
            this.GISLegend.GIS_Viewer = this.GIS;
            this.GISLegend.Location = new System.Drawing.Point(1096, 62);
            this.GISLegend.Margin = new System.Windows.Forms.Padding(8);
            this.GISLegend.Name = "GISLegend";
            this.GISLegend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GISLegend.Size = new System.Drawing.Size(272, 828);
            this.GISLegend.TabIndex = 4;
            // 
            // GIS
            // 
            this.GIS.AutoStyle = false;
            this.GIS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Controls.Add(this.GISScale);
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Level = 1D;
            this.GIS.Location = new System.Drawing.Point(395, 62);
            this.GIS.Margin = new System.Windows.Forms.Padding(8);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(701, 828);
            this.GIS.TabIndex = 6;
            this.GIS.TiledPaint = false;
            // 
            // GISScale
            // 
            this.GISScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GISScale.DividerColor1 = System.Drawing.Color.Black;
            this.GISScale.DividerColor2 = System.Drawing.Color.White;
            this.GISScale.GIS_Viewer = this.GIS;
            this.GISScale.Location = new System.Drawing.Point(22, 32);
            this.GISScale.Margin = new System.Windows.Forms.Padding(8);
            this.GISScale.Name = "GISScale";
            this.GISScale.PrepareEvent = null;
            this.GISScale.Size = new System.Drawing.Size(297, 63);
            this.GISScale.TabIndex = 0;
            tgiS_CSUnits2.DescriptionEx = null;
            this.GISScale.Units = tgiS_CSUnits2;
            this.GISScale.UnitsEPSG = 0;
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "pdf";
            this.dlgSave.Filter = "Pdf File (*.pdf)|*.PDF";
            this.dlgSave.Title = "Select a file";
            // 
            // stripBar1
            // 
            this.stripBar1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.stripBar1.Location = new System.Drawing.Point(0, 890);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Padding = new System.Windows.Forms.Padding(2, 0, 35, 0);
            this.stripBar1.Size = new System.Drawing.Size(1368, 22);
            this.stripBar1.TabIndex = 5;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(1331, 9);
            this.toolStripLabel1.Spring = true;
            this.toolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(240F, 240F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1368, 912);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GISLegend);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(100, 60);
            this.Margin = new System.Windows.Forms.Padding(8);
            this.Name = "WinForm";
            this.Text = "TatukGIS Samples - PrintPdf";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GIS.ResumeLayout(false);
            this.stripBar1.ResumeLayout(false);
            this.stripBar1.PerformLayout();
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
            // comment the line for .Net Framework
            #if NET6_0_OR_GREATER
              ApplicationConfiguration.Initialize();
            #else
              Application.SetHighDpiMode(HighDpiMode.SystemAware);
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
            #endif
            Application.Run(new WinForm());
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            // open a file
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\poland.ttkproject");
            PdfFileName = "";
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent)
            {
                GIS.FullExtent();
            }
            else if (sender == btnZoom)
            {
                GIS.Mode = TGIS_ViewerMode.Zoom;
            }
            else if (sender == btnDrag)
            {
                GIS.Mode = TGIS_ViewerMode.Drag;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (PdfFileName == "")
            {
                dlgSave.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                dlgSave.FileName = "";
            }
            else
            {
                dlgSave.InitialDirectory = System.IO.Path.GetDirectoryName(PdfFileName);
                dlgSave.FileName = System.IO.Path.GetFileName(PdfFileName);
            }
            if (dlgSave.ShowDialog() != DialogResult.OK) return;
            PdfFileName = dlgSave.FileName;
            toolStripLabel1.Text = PdfFileName;

            // all PrintPdf() methods below
            // have its versions with a stream instead of file name
            if (radioButton1.Checked)
            {
                // GIS.PrintPdf
                GIS.PrintPdf(PdfFileName,
                             (float)(210 * 72 / 25.4),
                             (float)(297 * 72 / 25.4)
                            );
            }
            else if (radioButton2.Checked)
            {
                // standard print
                var pm = new TGIS_PrintManager();
                pm.PrintPdf(GIS, PdfFileName,
                            (float)(210 * 72 / 25.4),
                            (float)(297 * 72 / 25.4)
                           );
            }
            else if (radioButton3.Checked)
            {
                // template
                var tp = new TGIS_TemplatePrint();
                tp.TemplatePath = TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\PrintTemplate\printtemplate.tpl";
                tp.set_GIS_Viewer(1, GIS);
                tp.set_GIS_ViewerExtent(1, GIS.VisibleExtent);
                tp.set_GIS_ViewerScale(1, 0);
                tp.set_GIS_Scale(1, GISScale);
                tp.set_GIS_Legend(1, GISLegend);
                tp.set_Text(1, "Title Title");
                tp.set_Text(2, "Copyright");

                var pm = new TGIS_PrintManager();
                pm.Template = tp;
                pm.PrintPdf(GIS, PdfFileName,
                            (float)(210 * 72 / 25.4),
                            (float)(297 * 72 / 25.4)
                          );
            }
            else if (radioButton4.Checked)
            {
                //PrintPage event
                var pm = new TGIS_PrintManager();
                // PrintPage for custom paint
                pm.PrintPageEvent += new TGIS_PrintPageEvent(PrintPage);
                pm.PrintPdf(GIS, PdfFileName,
                            (float)(210 * 72 / 25.4),
                            (float)(297 * 72 / 25.4)
                           );
            }
            else if (radioButton5.Checked)
            {
                // multi-page: mix of different scenarios
                var pm = new TGIS_PrintManager();
                // BeforePrintPage defines the way a page will be printed
                pm.BeforePrintPageEvent += new TGIS_PrintPageEvent(BeforePrintPage) ;
                pm.PrintPdf(GIS, PdfFileName,
                            (float)(210 * 72 / 25.4),
                            (float)(297 * 72 / 25.4)
                          ) ;
            }
        }

        private int inch(float _value, int _ppi)
        {
            return (int)Math.Round(_value * _ppi);
        }

        private void PrintPage(object _sender, TGIS_PrintPageEventArgs _e)
        {
            int l, t, w, h;
            Rectangle r;
            double scale;
            string s;
            Point pt;
            TGIS_PrintManager pm;
            TGIS_Printer pr;
            TGIS_RendererAbstract rd;

            pm = _e.PrintManager;
            pr = pm.Printer;
            rd = pr.Renderer;

            rd.CanvasBrush.Color = TGIS_Color.Black;
            rd.CanvasBrush.Style = TGIS_BrushStyle.Solid;

            l = 0;
            t = inch((float)0.5, pr.PpiY);
            w = pr.PageWidth;
            h = pr.PageHeight - t;

            // right panel
            l = w - pr.TwipsToX(2 * 1440);
            w = pr.TwipsToX(2 * 1440);
            rd.CanvasBrush.Color = TGIS_Color.Gray;
            rd.CanvasBrush.Style = TGIS_BrushStyle.Solid;
            rd.CanvasPen.Style = TGIS_PenStyle.Clear;
            rd.CanvasDrawRectangle(new Rectangle(l, t, w, h));

            l = l - inch((float)0.1, pr.PpiX);
            t = t - inch((float)0.1, pr.PpiY);
            rd.CanvasBrush.Color = TGIS_Color.White;
            rd.CanvasBrush.Style = TGIS_BrushStyle.Solid;
            rd.CanvasPen.Color = TGIS_Color.Black;
            rd.CanvasPen.Style = TGIS_PenStyle.Solid;
            rd.CanvasPen.Width = rd.TwipsToPoints(20);
            rd.CanvasDrawRectangle(new Rectangle(l, t, w, h));

            // legend
            pm.DrawControl(GISLegend, new Rectangle(l + 1, t + 1, w - 2, h - 2));

            l = inch((float)0.1, pr.PpiX);
            t = inch((float)0.5, pr.PpiY);
            w = pr.PageWidth - l;
            h = pr.PageHeight - t;

            // left panel
            w = w - pr.TwipsToX(2 * 1440) - inch((float)0.2, pr.PpiX);
            rd.CanvasBrush.Color = TGIS_Color.Gray;
            rd.CanvasBrush.Style = TGIS_BrushStyle.Solid;
            rd.CanvasPen.Style = TGIS_PenStyle.Clear;
            rd.CanvasDrawRectangle(new Rectangle(l, t, w, h));
            // for future use
            r = new Rectangle(l, t, w, h);

            l = l - inch((float)0.1, pr.PpiX);
            t = t - inch((float)0.1, pr.PpiY);
            rd.CanvasBrush.Color = TGIS_Color.White;
            rd.CanvasBrush.Style = TGIS_BrushStyle.Solid;
            rd.CanvasPen.Color = TGIS_Color.Black;
            rd.CanvasPen.Style = TGIS_PenStyle.Solid;
            rd.CanvasPen.Width = rd.TwipsToPoints(20);
            rd.CanvasDrawRectangle(new Rectangle(l, t, w, h));

            // map
            scale = 0;
            pm.DrawMap(GIS, GIS.Extent, new Rectangle(l + 1, t + 1, w - 2, h - 2), ref scale);

            // scale
            l = l + inch((float)0.5, pr.PpiX);
            t = t + h - inch((float)1.0, pr.PpiY);
            w = inch((float)3.0, pr.PpiX);
            h = inch((float)0.5, pr.PpiY);
            pm.DrawControl(GISScale, new Rectangle(l, t, w, h));

            // page number
            rd.CanvasBrush.Color = TGIS_Color.White;
            rd.CanvasFont.Color = TGIS_Color.Black;
            rd.CanvasFont.Name = "Arial";
            rd.CanvasFont.Size = 12;
            s = "Page " + pr.PageNumber.ToString();
            pt = rd.CanvasTextExtent(s);
            l = pr.TwipsToX(720);
            t = r.Top + pr.TwipsToY(720);
            w = pt.X;
            h = pt.Y;
            rd.CanvasDrawText(new Rectangle(l, t, w, h), s);

            // title
            pm.Title = "Print Title";
            s = pm.Title;
            rd.CanvasFont.Assign(pm.TitleFont);
            pt = rd.CanvasTextExtent(pm.Title);
            l = (int)Math.Round((r.Right - r.Left) / 2.0) - (int)Math.Round(pt.X / 2.0);
            t = r.Top + pr.TwipsToY(720);
            w = pt.X;
            h = pt.Y;
            rd.CanvasDrawText(new Rectangle(l, t, w, h), s);

            // subitle
            pm.Subtitle = "Print Subtitle";
            s = pm.Subtitle;
            rd.CanvasFont.Assign(pm.SubtitleFont);
            pt = rd.CanvasTextExtent(pm.Subtitle);
            l = (int)Math.Round((r.Right - r.Left) / 2.0) - (int)Math.Round(pt.X / 2.0);
            t = r.Top + pr.TwipsToY(720) + h + pr.TwipsToY(200);
            w = pt.X;
            h = pt.Y;
            rd.CanvasDrawText(new Rectangle(l, t, w, h), s);

            if (pr.PageNumber >= 2)
                _e.LastPage = true;
            else
                _e.LastPage = false;
        }
        private void BeforePrintPage(object _sender, TGIS_PrintPageEventArgs _e)
        {
            TGIS_PrintManager pm = _e.PrintManager;
            TGIS_Printer pr = pm.Printer;
            TGIS_TemplatePrint tp;

            if (pr.PageNumber == 1)
            {
                // prepare first page: PrintPage event
                pm.Template = null;
                pm.PrintPageEvent += new TGIS_PrintPageEvent(PrintPage);
            }
            else if (pr.PageNumber == 2)
            {
                // prepare second page: standard print
                pm.Template = null;
                pm.PrintPageEvent -= new TGIS_PrintPageEvent(PrintPage);
            }
            else if (pr.PageNumber == 3)
            {
            // prepare third page: ttktemplate
                tp = new TGIS_TemplatePrint();
                tp.TemplatePath = TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\PrintTemplate\printtemplate.tpl";
                tp.set_GIS_Viewer(1, GIS);
                tp.set_GIS_ViewerExtent(1, GIS.VisibleExtent);
                tp.set_GIS_ViewerScale(1, GIS.ScaleAsFloat);
                tp.set_GIS_Scale(1, GISScale);
                tp.set_GIS_Legend(1, GISLegend);
                tp.set_Text(1, "Page " + pr.PageNumber.ToString());
                tp.set_Text(2, tp.TemplatePath);
                pm.Template = tp;
                pm.PrintPageEvent = null;
            }

            if (pr.PageNumber >= 3)
                _e.LastPage = true;
            else
                _e.LastPage = false;

        }
    }
}
