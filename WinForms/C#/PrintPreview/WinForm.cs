using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace PrintPreview
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
        private System.Windows.Forms.Label lbPrintTitle;
        private System.Windows.Forms.Label lbPrintSubtitle;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_ControlLegend1;
        private System.Windows.Forms.Panel panel1;
        //private System.Windows.Forms.ToolBar toolBar1;
        private System.Windows.Forms.MenuStrip toolBar1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private TatukGIS.NDK.WinForms.TGIS_ControlPrintPreview GIS_ControlPrintPreview1;
        private System.Windows.Forms.TextBox edPrintTitle;
        private System.Windows.Forms.TextBox edPrintSubTitle;
        private System.Windows.Forms.Button btTitleFont;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btSubTitleFont;
        private System.Windows.Forms.CheckBox chkStandardPrint;
        private TatukGIS.NDK.WinForms.TGIS_ControlPrintPreviewSimple GIS_ControlPrintPreviewSimple1;
        private System.Windows.Forms.FontDialog dlgFontTitle;
        private System.Windows.Forms.FontDialog dlgFontSubtitle;
        private System.Windows.Forms.ToolTip toolTip2;
        private TGIS_PrintManager printManager;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.toolTip1.SetToolTip(this.btTitleFont, "define title font");
            this.toolTip2.SetToolTip(this.btSubTitleFont, "define subtitle font");
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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.lbPrintTitle = new System.Windows.Forms.Label();
            this.lbPrintSubtitle = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_ControlLegend1 = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolBar1 = new System.Windows.Forms.MenuStrip();
            this.GIS_ControlPrintPreview1 = new TatukGIS.NDK.WinForms.TGIS_ControlPrintPreview();
            this.edPrintTitle = new System.Windows.Forms.TextBox();
            this.edPrintSubTitle = new System.Windows.Forms.TextBox();
            this.btTitleFont = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btSubTitleFont = new System.Windows.Forms.Button();
            this.chkStandardPrint = new System.Windows.Forms.CheckBox();
            this.GIS_ControlPrintPreviewSimple1 = new TatukGIS.NDK.WinForms.TGIS_ControlPrintPreviewSimple();
            this.dlgFontTitle = new System.Windows.Forms.FontDialog();
            this.dlgFontSubtitle = new System.Windows.Forms.FontDialog();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbPrintTitle
            // 
            this.lbPrintTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPrintTitle.Location = new System.Drawing.Point(649, 62);
            this.lbPrintTitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbPrintTitle.Name = "lbPrintTitle";
            this.lbPrintTitle.Size = new System.Drawing.Size(80, 20);
            this.lbPrintTitle.TabIndex = 0;
            this.lbPrintTitle.Text = "Print title:";
            this.lbPrintTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbPrintSubtitle
            // 
            this.lbPrintSubtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPrintSubtitle.Location = new System.Drawing.Point(649, 175);
            this.lbPrintSubtitle.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbPrintSubtitle.Name = "lbPrintSubtitle";
            this.lbPrintSubtitle.Size = new System.Drawing.Size(110, 20);
            this.lbPrintSubtitle.TabIndex = 1;
            this.lbPrintSubtitle.Text = "Print subtitle:";
            // 
            // GIS
            // 
            this.GIS.AccessibleDescription = "217";
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Location = new System.Drawing.Point(300, 62);
            this.GIS.Margin = new System.Windows.Forms.Padding(5);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Drag;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(339, 264);
            this.GIS.TabIndex = 2;
            this.GIS.UseRTree = false;
            // 
            // GIS_ControlLegend1
            // 
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_ControlLegend1.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_ControlLegend1.GIS_Group = null;
            this.GIS_ControlLegend1.GIS_Layer = null;
            this.GIS_ControlLegend1.GIS_Viewer = this.GIS;
            this.GIS_ControlLegend1.Location = new System.Drawing.Point(12, 62);
            this.GIS_ControlLegend1.Margin = new System.Windows.Forms.Padding(5);
            this.GIS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_ControlLegend1.Name = "GIS_ControlLegend1";
            this.GIS_ControlLegend1.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GIS_ControlLegend1.ReverseOrder = true;
            this.GIS_ControlLegend1.Size = new System.Drawing.Size(274, 262);
            this.GIS_ControlLegend1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.toolBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(925, 45);
            this.panel1.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(301, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(301, 39);
            this.button1.TabIndex = 1;
            this.button1.Text = "TGIS_ControlPrintPreview";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 2);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(301, 39);
            this.button2.TabIndex = 2;
            this.button2.Text = "TGIS_ControlPrintPreviewSimple";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // toolBar1
            // 
            this.toolBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolBar1.AutoSize = false;
            this.toolBar1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Margin = new System.Windows.Forms.Padding(5);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowItemToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(925, 45);
            this.toolBar1.TabIndex = 0;
            // 
            // GIS_ControlPrintPreview1
            // 
            this.GIS_ControlPrintPreview1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS_ControlPrintPreview1.BackColor = System.Drawing.Color.Gray;
            this.GIS_ControlPrintPreview1.GIS_Viewer = this.GIS;
            this.GIS_ControlPrintPreview1.Location = new System.Drawing.Point(12, 338);
            this.GIS_ControlPrintPreview1.Margin = new System.Windows.Forms.Padding(5);
            this.GIS_ControlPrintPreview1.Name = "GIS_ControlPrintPreview1";
            this.GIS_ControlPrintPreview1.Size = new System.Drawing.Size(889, 364);
            this.GIS_ControlPrintPreview1.TabIndex = 5;
            // 
            // edPrintTitle
            // 
            this.edPrintTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edPrintTitle.Location = new System.Drawing.Point(650, 88);
            this.edPrintTitle.Margin = new System.Windows.Forms.Padding(5);
            this.edPrintTitle.Name = "edPrintTitle";
            this.edPrintTitle.Size = new System.Drawing.Size(262, 22);
            this.edPrintTitle.TabIndex = 6;
            this.edPrintTitle.Text = "Title";
            this.edPrintTitle.TextChanged += new System.EventHandler(this.edPrintTitle_TextChanged);
            // 
            // edPrintSubTitle
            // 
            this.edPrintSubTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edPrintSubTitle.Location = new System.Drawing.Point(650, 200);
            this.edPrintSubTitle.Margin = new System.Windows.Forms.Padding(5);
            this.edPrintSubTitle.Name = "edPrintSubTitle";
            this.edPrintSubTitle.Size = new System.Drawing.Size(262, 22);
            this.edPrintSubTitle.TabIndex = 7;
            this.edPrintSubTitle.Text = "Subtitle";
            this.edPrintSubTitle.TextChanged += new System.EventHandler(this.edPrintSubTitle_TextChanged);
            // 
            // btTitleFont
            // 
            this.btTitleFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btTitleFont.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btTitleFont.Location = new System.Drawing.Point(650, 125);
            this.btTitleFont.Margin = new System.Windows.Forms.Padding(5);
            this.btTitleFont.Name = "btTitleFont";
            this.btTitleFont.Size = new System.Drawing.Size(105, 32);
            this.btTitleFont.TabIndex = 8;
            this.btTitleFont.Text = "Font";
            this.btTitleFont.Click += new System.EventHandler(this.btTitleFont_Click);
            // 
            // btSubTitleFont
            // 
            this.btSubTitleFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSubTitleFont.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btSubTitleFont.Location = new System.Drawing.Point(650, 250);
            this.btSubTitleFont.Margin = new System.Windows.Forms.Padding(5);
            this.btSubTitleFont.Name = "btSubTitleFont";
            this.btSubTitleFont.Size = new System.Drawing.Size(105, 32);
            this.btSubTitleFont.TabIndex = 9;
            this.btSubTitleFont.Text = "Font";
            this.btSubTitleFont.Click += new System.EventHandler(this.btSubTitleFont_Click);
            // 
            // chkStandardPrint
            // 
            this.chkStandardPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkStandardPrint.Location = new System.Drawing.Point(650, 300);
            this.chkStandardPrint.Margin = new System.Windows.Forms.Padding(5);
            this.chkStandardPrint.Name = "chkStandardPrint";
            this.chkStandardPrint.Size = new System.Drawing.Size(164, 26);
            this.chkStandardPrint.TabIndex = 10;
            this.chkStandardPrint.Text = "Standard print";
            this.chkStandardPrint.CheckedChanged += new System.EventHandler(this.chkStandardPrint_CheckedChanged);
            // 
            // GIS_ControlPrintPreviewSimple1
            // 
            this.GIS_ControlPrintPreviewSimple1.GIS_Viewer = this.GIS;
            // 
            // dlgFontTitle
            // 
            this.dlgFontTitle.ShowApply = true;
            this.dlgFontTitle.ShowColor = true;
            this.dlgFontTitle.Apply += new System.EventHandler(this.dlgFontTitle_Apply);
            // 
            // dlgFontSubtitle
            // 
            this.dlgFontSubtitle.ShowApply = true;
            this.dlgFontSubtitle.ShowColor = true;
            this.dlgFontSubtitle.Apply += new System.EventHandler(this.dlgFontSubtitle_Apply);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(925, 728);
            this.Controls.Add(this.chkStandardPrint);
            this.Controls.Add(this.btSubTitleFont);
            this.Controls.Add(this.btTitleFont);
            this.Controls.Add(this.edPrintSubTitle);
            this.Controls.Add(this.edPrintTitle);
            this.Controls.Add(this.GIS_ControlPrintPreview1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GIS_ControlLegend1);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.lbPrintSubtitle);
            this.Controls.Add(this.lbPrintTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Sample Print Preview";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
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
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\poland.ttkproject");

            printManager = new TGIS_PrintManager();
            printManager.Title = edPrintTitle.Text;
            printManager.Subtitle = edPrintSubTitle.Text;
            printManager.Footer = "Footer";

            printManager.TitleFont.Name = "Arial";
            printManager.TitleFont.Size = 18;
            printManager.TitleFont.Style = to_gis_fontstyle(FontStyle.Bold);
            printManager.TitleFont.Color = to_gis_color(SystemColors.WindowText);

            printManager.SubtitleFont.Name = "Arial";
            printManager.SubtitleFont.Size = 14;
            printManager.SubtitleFont.Style = to_gis_fontstyle(FontStyle.Bold);
            printManager.SubtitleFont.Color = to_gis_color(SystemColors.WindowText);

            printManager.FooterFont.Name = "Arial";
            printManager.FooterFont.Size = 10;
            printManager.FooterFont.Style = to_gis_fontstyle(FontStyle.Regular);
            printManager.FooterFont.Color = to_gis_color(SystemColors.WindowText);

            printManager.PrintPageEvent += new TGIS_PrintPageEvent(GIS_PrintPage);
            GIS_ControlPrintPreview1.Preview(1, new PrintDocument(), printManager);
        }

        private Color to_native_color(TGIS_Color _cl)
        {
            return TGIS_FrameworkUtils.ToPlatformColor(_cl);
        }

        private TGIS_Color to_gis_color(Color _cl)
        {
            return TGIS_FrameworkUtils.FromPlatformColor(_cl);
        }

        private FontStyle to_native_fontstyle(TGIS_FontStyle _st)
        {
            FontStyle style = FontStyle.Regular;
            if ((_st & TGIS_FontStyle.Bold) != 0)
                style = style | FontStyle.Bold;
            if ((_st & TGIS_FontStyle.Italic) != 0)
                style = style | FontStyle.Italic;
            if ((_st & TGIS_FontStyle.StrikeOut) != 0)
                style = style | FontStyle.Strikeout;
            if ((_st & TGIS_FontStyle.Underline) != 0)
                style = style | FontStyle.Underline;
            return style;
        }

        private TGIS_FontStyle to_gis_fontstyle(FontStyle _st)
        {
            TGIS_FontStyle style = 0;
            if ((_st & FontStyle.Bold) != 0)
                style = style | TGIS_FontStyle.Bold;
            if ((_st & FontStyle.Italic) != 0)
                style = style | TGIS_FontStyle.Italic;
            if ((_st & FontStyle.Strikeout) != 0)
                style = style | TGIS_FontStyle.StrikeOut;
            if ((_st & FontStyle.Underline) != 0)
                style = style | TGIS_FontStyle.Underline;
            return style;
        }

        private int inch(double _value, int _dpi)
        {
            return Convert.ToInt32(Math.Round(_value * _dpi));
        }

        private int xy(double _value, double _factor)
        {
            return (int)Math.Round(_value * _factor);
        }

        private void GIS_PrintPage(object _sender, TatukGIS.NDK.WinForms.TGIS_PrintPageEventArgs _e)
        {
            double scale;
            Rectangle r;
            Rectangle fr;
            Rectangle mr;
            int x1, y1, x2, y2;
            int h;
            SizeF sz;
            Graphics canvas = _e.Canvas;
            TGIS_PrintManager pm;
            TGIS_Printer pr;
            int dpi;
            double fx, fy;
            Brush br;
            Pen pn;
            Font ft;

            pm = _e.PrintManager;
            pr = pm.Printer;
            dpi = pr.PpiX;

            fx = (double)pr.PrintArea.Width / pr.PageWidth;
            fy = (double)pr.PrintArea.Height / pr.PageHeight;

            // left panel
            x1 = xy(inch(0.1, dpi), fx);
            y1 = xy(inch(0.5, dpi), fy);
            x2 = pr.PrintArea.Width;
            y2 = pr.PrintArea.Height;

            // left panel: gray rectangle
            x2 = x2 - xy(pr.TwipsToX(2 * 1440), fx) - xy(inch(0.2, dpi), fx);
            r = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            br = new SolidBrush(Color.Gray);
            canvas.FillRectangle(br, r);
            br.Dispose();
            pn = new Pen(Color.Gray);
            canvas.DrawRectangle(pn, r);
            pn.Dispose();

            // left panel: white rectangle
            r = new Rectangle(r.Left - xy(inch(0.1, dpi), fx),
                              r.Top  - xy(inch(0.1, dpi), fy),
                              r.Width, r.Height
                             );
            br = new SolidBrush(Color.White);
            canvas.FillRectangle(br, r);
            br.Dispose();
            fr = r;

            // left panel: map
            x1 = r.Left   + xy(inch(0.1, dpi), fx);
            y1 = r.Top    + xy(inch(0.1, dpi), fy);
            x2 = r.Right  - xy(inch(0.1, dpi), fx);
            y2 = r.Bottom - xy(inch(0.1, dpi), fy);
            r = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            scale = 0;
            // 'scale' returned by the function is the real map scale used during printing.
            // It should be passed to the following DrawControl methods.
            // If legend or scale controls have to be printed before map (for some reason)
            // use the following frame:
            //    scale := 0 ;
            //    pm->GetDrawingParams( GIS, GIS.Extent, r, scale ) ; ...
            //    pm->DrawControl( GIS_ControlLegend1, r1, scale ) ;  ...
            //    pm->DrawMap( GIS, GIS.Extent, r, scale ) ;
            pm.DrawMap(GIS, GIS.Extent, r, ref scale);
            mr = r ;

            // left panel: black frame
            pn = new Pen(Color.Black);
            canvas.DrawRectangle(pn, fr);
            pn.Dispose();

            // right panel
            x1 = 0;
            y1 = xy(inch(0.5, dpi), fy);
            x2 = pr.PrintArea.Width;
            y2 = pr.PrintArea.Height;

            // right panel: gray rectangle
            x1 = x2 - xy(pr.TwipsToX(2 * 1440), fx);
            r = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            br = new SolidBrush(Color.Gray);
            canvas.FillRectangle(br, r);
            br.Dispose();
            pn = new Pen(Color.Gray);
            canvas.DrawRectangle(pn, r);
            pn.Dispose();

            // right panel: white rectangle
            r = new Rectangle(r.Left - xy(inch(0.1, dpi), fx),
                              r.Top  - xy(inch(0.1, dpi), fy),
                              r.Width, r.Height
                             );
            br = new SolidBrush(Color.White);
            canvas.FillRectangle(br, r);
            br.Dispose();
            fr = r;

            // right panel: legend
            x1 = r.Left   + (int)xy(inch(0.1, dpi), fx);
            y1 = r.Top    + (int)xy(inch(0.1, dpi), fy);
            x2 = r.Right  - (int)xy(inch(0.1, dpi), fx);
            y2 = r.Bottom - (int)xy(inch(0.1, dpi), fy);
            r = new Rectangle(x1, y1, x2 - x1, y2 - y1);
            pm.DrawControl(GIS_ControlLegend1, r, scale);

            // right panel: black frame
            pn = new Pen(Color.Black);
            canvas.DrawRectangle(pn, fr);
            pn.Dispose();


            // page number
            ft = new Font("Arial", 12);
            br = new SolidBrush(Color.Black);
            canvas.DrawString(String.Format("Page {0}", pr.PageNumber),
                               ft, br,
                               xy(pr.TwipsToX(720), fx),
                               xy(pr.TwipsToY(720), fy)
                             );
            br.Dispose();
            ft.Dispose();

            r = mr ; 
            // title
            ft = new Font(pm.TitleFont.Name, pm.TitleFont.Size,
                           to_native_fontstyle(pm.TitleFont.Style));
            br = new SolidBrush(to_native_color(pm.TitleFont.Color));
            sz = canvas.MeasureString(pm.Title, ft);
            canvas.DrawString(pm.Title, ft, br,
                               (int)(Math.Round((double)(r.Right - r.Left) / 2) -
                                     Math.Round(sz.Width / 2)),
                               xy(pr.TwipsToY(720), fy)
                             );
            br.Dispose();
            ft.Dispose();

            // subtitle
            h = (int)Math.Round(sz.Height);
            ft = new Font(pm.SubtitleFont.Name, pm.SubtitleFont.Size,
                          to_native_fontstyle(pm.SubtitleFont.Style));
            br = new SolidBrush(to_native_color(pm.SubtitleFont.Color));
            canvas.DrawString(pm.Subtitle, ft, br,
                               (int)(Math.Round((double)(r.Right - r.Left) / 2) -
                                     Math.Round(canvas.MeasureString(pm.Subtitle, ft).Width / 2)),
                               xy(pr.TwipsToY(720 + 200), fy) + h
                             );

            _e.LastPage = true;

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            GIS_ControlPrintPreviewSimple1.Preview(printManager);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            GIS_ControlPrintPreview1.Preview(1, new PrintDocument(), printManager);
        }

        private void edPrintTitle_TextChanged(object sender, System.EventArgs e)
        {
            printManager.Title = edPrintTitle.Text;
        }

        private void edPrintSubTitle_TextChanged(object sender, System.EventArgs e)
        {
            printManager.Subtitle = edPrintSubTitle.Text;
        }

        private void btTitleFont_Click(object sender, System.EventArgs e)
        {
            dlgFontTitle.Font = new Font(printManager.TitleFont.Name,
                                         printManager.TitleFont.Size,
                                         to_native_fontstyle(printManager.TitleFont.Style));
            dlgFontTitle.Color = to_native_color(printManager.TitleFont.Color);
            if (dlgFontTitle.ShowDialog() != DialogResult.OK) return;
            printManager.TitleFont.Name = dlgFontTitle.Font.Name;
            printManager.TitleFont.Size = (int)Math.Round(dlgFontTitle.Font.Size);
            printManager.TitleFont.Style = to_gis_fontstyle(dlgFontTitle.Font.Style);
            printManager.TitleFont.Color = to_gis_color(dlgFontTitle.Color);
        }

        private void btSubTitleFont_Click(object sender, System.EventArgs e)
        {
            dlgFontSubtitle.Font = new Font(printManager.SubtitleFont.Name,
                                            printManager.SubtitleFont.Size,
                                            to_native_fontstyle(printManager.SubtitleFont.Style));
            dlgFontSubtitle.Color = to_native_color(printManager.SubtitleFont.Color);
            if (dlgFontSubtitle.ShowDialog() != DialogResult.OK) return;
            printManager.SubtitleFont.Name = dlgFontSubtitle.Font.Name;
            printManager.SubtitleFont.Size = (int)Math.Round(dlgFontSubtitle.Font.Size);
            printManager.SubtitleFont.Style = to_gis_fontstyle(dlgFontSubtitle.Font.Style);
            printManager.SubtitleFont.Color = to_gis_color(dlgFontSubtitle.Color);
        }

        private void chkStandardPrint_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chkStandardPrint.Checked)
                printManager.PrintPageEvent -= new TatukGIS.NDK.WinForms.TGIS_PrintPageEvent(this.GIS_PrintPage);
            else
                printManager.PrintPageEvent += new TatukGIS.NDK.WinForms.TGIS_PrintPageEvent(this.GIS_PrintPage);

            GIS_ControlPrintPreview1.Preview(1, new PrintDocument(), printManager);
        }

        private void dlgFontTitle_Apply(object sender, System.EventArgs e)
        {
            printManager.TitleFont.Name = dlgFontTitle.Font.Name;
            printManager.TitleFont.Size = (int)Math.Round(dlgFontTitle.Font.Size);
            printManager.TitleFont.Style = to_gis_fontstyle(dlgFontTitle.Font.Style);
            printManager.TitleFont.Color = to_gis_color(dlgFontTitle.Color);
            GIS_ControlPrintPreview1.Preview(1, new PrintDocument(), printManager);
        }

        private void dlgFontSubtitle_Apply(object sender, System.EventArgs e)
        {
            printManager.SubtitleFont.Name = dlgFontSubtitle.Font.Name;
            printManager.SubtitleFont.Size = (int)Math.Round(dlgFontSubtitle.Font.Size);
            printManager.SubtitleFont.Style = to_gis_fontstyle(dlgFontSubtitle.Font.Style);
            printManager.SubtitleFont.Color = to_gis_color(dlgFontSubtitle.Color);
            GIS_ControlPrintPreview1.Preview(1, new PrintDocument(), printManager);
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }
}
