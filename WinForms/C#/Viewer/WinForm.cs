using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Viewer
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
        private System.Windows.Forms.ContextMenuStrip mainMenu1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnFileOpen;
        private System.Windows.Forms.ToolStripButton btnAppend;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.ToolStripButton btnFilePrint;
        private System.Windows.Forms.ToolStripButton btnEditFile;
        private System.Windows.Forms.ToolStripButton btnSaveToImage;
        private System.Windows.Forms.ToolStripButton btnViewFullExtent;
        private System.Windows.Forms.ToolStripButton btnViewZoomMode;
        private System.Windows.Forms.ToolStripButton btnViewDragMode;
        private System.Windows.Forms.ToolStripButton btnViewSelectMode;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripButton btnSaveAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip stsBar;
        private System.Windows.Forms.ToolStripLabel stsBarPanel1;
        private System.Windows.Forms.ToolStripLabel stsBarPanel2;
        private System.Windows.Forms.ToolStripLabel stsBarPanel3;
        private System.Windows.Forms.ToolStripLabel stsBarPanel4;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_ControlLegend;
        private System.Windows.Forms.Splitter splitter1;
        public TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuAppend;
        private System.Windows.Forms.ToolStripMenuItem mnuEditFile;
        private System.Windows.Forms.ToolStripMenuItem mnuExportLayer;
        private System.Windows.Forms.ToolStripMenuItem mnuFilePrint;
        private System.Windows.Forms.ToolStripMenuItem mnuClose;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuViewFullExtent;
        private System.Windows.Forms.ToolStripMenuItem mnuViewZoomMode;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem mnuViewDragMode;
        private System.Windows.Forms.ToolStripMenuItem mnuViewSelectMode;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuUseRTree;
        private System.Windows.Forms.ToolStripMenuItem mnuColor;
        private System.Windows.Forms.ToolStripMenuItem mnuSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuFindShape;
        private System.Windows.Forms.OpenFileDialog dlgFileOpen;
        private System.Windows.Forms.SaveFileDialog dlgFileSave;
        private System.Windows.Forms.ColorDialog dlgColor;
        private System.Windows.Forms.OpenFileDialog dlgFileAppend;
        private System.Windows.Forms.SaveFileDialog dlgSaveImage;
        private SearchForm srchForm;
        public InfoForm infForm;
        private ExportForm expForm;
        private EditForm edtForm;
        private ToolStripButton btnLegendMode;
        private ToolStripButton btnCS;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private TGIS_ControlPrintPreviewSimple GIS_ControlPrintPreviewSimple;

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
            this.mainMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAppend = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClose = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewFullExtent = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewZoomMode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewDragMode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSelectMode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUseRTree = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuColor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFindShape = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFileOpen = new System.Windows.Forms.ToolStripButton();
            this.btnAppend = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.btnFilePrint = new System.Windows.Forms.ToolStripButton();
            this.btnEditFile = new System.Windows.Forms.ToolStripButton();
            this.btnSaveToImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnViewFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnViewZoomMode = new System.Windows.Forms.ToolStripButton();
            this.btnViewDragMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnViewSelectMode = new System.Windows.Forms.ToolStripButton();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLegendMode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCS = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stsBar = new System.Windows.Forms.StatusStrip();
            this.stsBarPanel1 = new System.Windows.Forms.ToolStripLabel();
            this.stsBarPanel2 = new System.Windows.Forms.ToolStripLabel();
            this.stsBarPanel3 = new System.Windows.Forms.ToolStripLabel();
            this.stsBarPanel4 = new System.Windows.Forms.ToolStripLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgFileSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.dlgFileAppend = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveImage = new System.Windows.Forms.SaveFileDialog();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_ControlLegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS_ControlPrintPreviewSimple = new TatukGIS.NDK.WinForms.TGIS_ControlPrintPreviewSimple();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuOptions,
            this.mnuSearch});
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.mnuFileOpen,
            this.mnuAppend,
            this.mnuEditFile,
            this.mnuExportLayer,
            this.mnuFilePrint,
            this.mnuClose,
            this.ToolStripMenuItem9,
            this.mnuFileExit});
            this.mnuFile.Text = "File";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Text = "Open ...";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuAppend
            // 
            this.mnuAppend.ShortcutKeys = Keys.Control | Keys.A;
            this.mnuAppend.Text = "Add ...";
            this.mnuAppend.Click += new System.EventHandler(this.mnuAppend_Click);
            // 
            // mnuEditFile
            // 
            this.mnuEditFile.ShortcutKeys = Keys.Control | Keys.E;
            this.mnuEditFile.Text = "Edit project/config file";
            this.mnuEditFile.Click += new System.EventHandler(this.mnuEditFile_Click);
            // 
            // mnuExportLayer
            // 
            this.mnuExportLayer.Text = "Export ...";
            this.mnuExportLayer.Click += new System.EventHandler(this.mnuExportLayer_Click);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Text = "Print ...";
            this.mnuFilePrint.Click += new System.EventHandler(this.mnuFilePrint_Click);
            // 
            // mnuClose
            // 
            this.mnuClose.ShortcutKeys = Keys.Control | Keys.Q;
            this.mnuClose.Text = "Close";
            this.mnuClose.Click += new System.EventHandler(this.mnuClose_Click);
            // 
            // ToolStripMenuItem9
            // 
            this.ToolStripMenuItem9.Text = "-";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.mnuViewFullExtent,
            this.ToolStripMenuItem13,
            this.mnuViewZoomMode,
            this.mnuViewDragMode,
            this.mnuViewSelectMode});
            this.mnuView.Text = "View";
            // 
            // mnuViewFullExtent
            // 
            this.mnuViewFullExtent.Text = "Full Extent";
            this.mnuViewFullExtent.Click += new System.EventHandler(this.mnuViewFullExtent_Click);
            // 
            // ToolStripMenuItem13
            // 
            this.ToolStripMenuItem13.Text = "-";
            // 
            // mnuViewZoomMode
            // 
            this.mnuViewZoomMode.Text = "Zoom";
            this.mnuViewZoomMode.Click += new System.EventHandler(this.mnuViewZoomMode_Click);
            // 
            // mnuViewDragMode
            // 
            this.mnuViewDragMode.Text = "Drag";
            this.mnuViewDragMode.Click += new System.EventHandler(this.mnuViewDragMode_Click);
            // 
            // mnuViewSelectMode
            // 
            this.mnuViewSelectMode.Text = "Info";
            this.mnuViewSelectMode.Click += new System.EventHandler(this.mnuViewSelectMode_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.mnuUseRTree,
            this.mnuColor});
            this.mnuOptions.Text = "Options";
            // 
            // mnuUseRTree
            // 
            this.mnuUseRTree.Text = "Use R-tree";
            this.mnuUseRTree.Click += new System.EventHandler(this.mnuUseRTree_Click);
            // 
            // mnuColor
            // 
            this.mnuColor.Text = "Color";
            this.mnuColor.Click += new System.EventHandler(this.mnuColor_Click);
            // 
            // mnuSearch
            // 
            this.mnuSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            this.mnuFindShape});
            this.mnuSearch.Text = "Search";
            // 
            // mnuFindShape
            // 
            this.mnuFindShape.ShortcutKeys = Keys.Control | Keys.F;
            this.mnuFindShape.Text = "Find shape";
            this.mnuFindShape.Click += new System.EventHandler(this.mnuFindShape_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFileOpen,
            this.btnAppend,
            this.btnClose,
            this.btnFilePrint,
            this.btnEditFile,
            this.btnSaveToImage,
            this.toolStripSeparator1,
            this.btnViewFullExtent,
            this.btnViewZoomMode,
            this.btnViewDragMode,
            this.toolStripSeparator2,
            this.btnViewSelectMode,
            this.btnSearch,
            this.btnSaveAll,
            this.toolStripSeparator3,
            this.btnLegendMode,
            this.toolStripSeparator4,
            this.btnCS,
            this.toolStripSeparator5});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 24);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnFileOpen
            // 
            this.btnFileOpen.ImageIndex = 4;
            this.btnFileOpen.Name = "btnFileOpen";
            this.btnFileOpen.ToolTipText = "Open GIS coverage";
            this.btnFileOpen.Click += toolStrip1_ButtonClick;
            // 
            // btnAppend
            // 
            this.btnAppend.ImageIndex = 6;
            this.btnAppend.Name = "btnAppend";
            this.btnAppend.ToolTipText = "Add layers";
            this.btnAppend.Click += toolStrip1_ButtonClick;
            // 
            // btnClose
            // 
            this.btnClose.ImageIndex = 8;
            this.btnClose.Name = "btnClose";
            this.btnClose.ToolTipText = "Close all layers";
            this.btnClose.Click += toolStrip1_ButtonClick;
            // 
            // btnFilePrint
            // 
            this.btnFilePrint.ImageIndex = 5;
            this.btnFilePrint.Name = "btnFilePrint";
            this.btnFilePrint.ToolTipText = "Print preview";
            this.btnFilePrint.Click += toolStrip1_ButtonClick;
            // 
            // btnEditFile
            // 
            this.btnEditFile.ImageIndex = 9;
            this.btnEditFile.Name = "btnEditFile";
            this.btnEditFile.ToolTipText = "Edit project/config file";
            this.btnEditFile.Click += toolStrip1_ButtonClick;
            // 
            // btnSaveToImage
            // 
            this.btnSaveToImage.ImageIndex = 10;
            this.btnSaveToImage.Name = "btnSaveToImage";
            this.btnSaveToImage.ToolTipText = "Save to Image";
            this.btnSaveToImage.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // btnViewFullExtent
            // 
            this.btnViewFullExtent.ImageIndex = 0;
            this.btnViewFullExtent.Name = "btnViewFullExtent";
            this.btnViewFullExtent.ToolTipText = "Fit map into the window";
            this.btnViewFullExtent.Click += toolStrip1_ButtonClick;
            // 
            // btnViewZoomMode
            // 
            this.btnViewZoomMode.ImageIndex = 1;
            this.btnViewZoomMode.Name = "btnViewZoomMode";
            this.btnViewZoomMode.ToolTipText = "Zoom mode";
            this.btnViewZoomMode.Click += toolStrip1_ButtonClick;
            // 
            // btnViewDragMode
            // 
            this.btnViewDragMode.ImageIndex = 2;
            this.btnViewDragMode.Name = "btnViewDragMode";
            this.btnViewDragMode.ToolTipText = "Drag mode";
            this.btnViewDragMode.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // btnViewSelectMode
            // 
            this.btnViewSelectMode.ImageIndex = 3;
            this.btnViewSelectMode.Name = "btnViewSelectMode";
            this.btnViewSelectMode.ToolTipText = "Select mode";
            this.btnViewSelectMode.Click += toolStrip1_ButtonClick;
            // 
            // btnSearch
            // 
            this.btnSearch.ImageIndex = 7;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.ToolTipText = "Search";
            this.btnSearch.Click += toolStrip1_ButtonClick;
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.ImageIndex = 11;
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.ToolTipText = "Save changes";
            this.btnSaveAll.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // btnLegendMode
            // 
            this.btnLegendMode.ImageIndex = 12;
            this.btnLegendMode.Name = "btnLegendMode";
            this.btnLegendMode.ToolTipText = "Legend mode";
            this.btnLegendMode.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // btnCS
            // 
            this.btnCS.ImageIndex = 13;
            this.btnCS.Name = "btnCS";
            this.btnCS.ToolTipText = "Coordinate system";
            this.btnCS.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            this.imageList1.Images.SetKeyName(8, "");
            this.imageList1.Images.SetKeyName(9, "");
            this.imageList1.Images.SetKeyName(10, "");
            this.imageList1.Images.SetKeyName(11, "");
            this.imageList1.Images.SetKeyName(12, "DesktopSave1_b.bmp");
            this.imageList1.Images.SetKeyName(13, "ExtentLayer.bmp");
            this.imageList1.Images.SetKeyName(14, "FullScreen.bmp");
            // 
            // stsBar
            // 
            this.stsBar.Location = new System.Drawing.Point(0, 426);
            this.stsBar.Name = "stsBar";
            this.stsBar.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.stsBarPanel1,
            this.stsBarPanel2,
            this.stsBarPanel3,
            this.stsBarPanel4});
            this.stsBar.Size = new System.Drawing.Size(592, 20);
            this.stsBar.TabIndex = 1;
            // 
            // stsBarPanel1
            // 
            this.stsBarPanel1.Name = "stsBarPanel1";
            this.stsBarPanel1.Width = 60;
            // 
            // stsBarPanel2
            // 
            this.stsBarPanel2.Name = "stsBarPanel2";
            this.stsBarPanel2.Width = 200;
            // 
            // stsBarPanel3
            // 
            this.stsBarPanel3.Name = "stsBarPanel3";
            this.stsBarPanel3.Width = 200;
            // 
            // stsBarPanel4
            // 
            this.stsBarPanel4.Name = "stsBarPanel4";
            this.stsBarPanel4.Width = 115;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(166, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 402);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // dlgFileSave
            // 
            this.dlgFileSave.CreatePrompt = true;
            this.dlgFileSave.Filter = "Arcview Shape File (*.shp, *.shx, *.dbf)|*.SHP|Autocad (*.dxf)|*.DXF|Mapinfo Inte" +
    "rchange (*.mif, *.mid)|*.MIF|TatukGIS SQL Layer (.ttkls)|*.TTKLS";
            this.dlgFileSave.Title = "Export layer";
            // 
            // dlgFileAppend
            // 
            this.dlgFileAppend.Multiselect = true;
            // 
            // dlgSaveImage
            // 
            this.dlgSaveImage.CreatePrompt = true;
            this.dlgSaveImage.Filter = "BMP|*.BMP|JPG|*.JPG|PNG|*.PNG|TIF|*.TIF";
            this.dlgSaveImage.Title = "Save to Image";
            // 
            // GIS
            // 
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(169, 24);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.RestrictedDrag = false;
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Size = new System.Drawing.Size(423, 402);
            this.GIS.TabIndex = 4;
            this.GIS.BusyEvent += new TatukGIS.NDK.TGIS_BusyEvent(this.GIS_Busy);
            this.GIS.PaintExtraEvent += new TatukGIS.NDK.TGIS_RendererEvent(this.GIS_PaintExtraEvent);
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            // 
            // GIS_ControlLegend
            // 
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_ControlLegend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_ControlLegend.Dock = System.Windows.Forms.DockStyle.Left;
            this.GIS_ControlLegend.GIS_Group = null;
            this.GIS_ControlLegend.GIS_Layer = null;
            this.GIS_ControlLegend.GIS_Viewer = this.GIS;
            this.GIS_ControlLegend.Location = new System.Drawing.Point(0, 24);
            this.GIS_ControlLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_ControlLegend.Name = "GIS_ControlLegend";
            this.GIS_ControlLegend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)));
            this.GIS_ControlLegend.ReverseOrder = true;
            this.GIS_ControlLegend.Size = new System.Drawing.Size(166, 402);
            this.GIS_ControlLegend.TabIndex = 2;
            // 
            // GIS_ControlPrintPreviewSimple
            // 
            this.GIS_ControlPrintPreviewSimple.GIS_Viewer = this.GIS;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 446);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.GIS_ControlLegend);
            this.Controls.Add(this.stsBar);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.ContextMenuStrip = this.mainMenu1;
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Coverage Viewer";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.WinForm_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
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

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            // set File dialogs filters
            dlgFileOpen.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.All, false);
            dlgFileAppend.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.All, false);

            edtForm = new EditForm();
            updateToolBar();

        }

        private void updateToolBar()
        {
            bool notbusy;

            // update toolbar controls
            btnViewZoomMode.Checked = GIS.Mode == TGIS_ViewerMode.Zoom;
            btnViewDragMode.Checked = GIS.Mode == TGIS_ViewerMode.Drag;
            btnViewSelectMode.Checked = GIS.Mode == TGIS_ViewerMode.Select;
            btnSearch.Checked = false;

            notbusy = true;
            btnFileOpen.Enabled = notbusy;
            mnuFileExit.Enabled = notbusy;
            mnuExportLayer.Enabled = notbusy && (!GIS.IsEmpty);
            btnFilePrint.Enabled = notbusy && (!GIS.IsEmpty);
            mnuFilePrint.Enabled = notbusy && (!GIS.IsEmpty);
            btnViewFullExtent.Enabled = notbusy && (!GIS.IsEmpty);
            mnuViewFullExtent.Enabled = notbusy && (!GIS.IsEmpty);
            btnViewZoomMode.Enabled = notbusy && (!GIS.IsEmpty);
            mnuViewZoomMode.Enabled = notbusy && (!GIS.IsEmpty);
            btnViewDragMode.Enabled = notbusy && (!GIS.IsEmpty);
            mnuViewDragMode.Enabled = notbusy && (!GIS.IsEmpty);
            btnViewSelectMode.Enabled = notbusy && (!GIS.IsEmpty);
            mnuViewSelectMode.Enabled = notbusy && (!GIS.IsEmpty);
            btnAppend.Enabled = notbusy && (!GIS.IsEmpty);
            mnuAppend.Enabled = notbusy && (!GIS.IsEmpty);
            btnSearch.Enabled = notbusy && (!GIS.IsEmpty);
            mnuFindShape.Enabled = notbusy && (!GIS.IsEmpty);
            btnClose.Enabled = notbusy && (!GIS.IsEmpty);
            mnuClose.Enabled = notbusy && (!GIS.IsEmpty);
            btnEditFile.Enabled = notbusy && (!GIS.IsEmpty) && (edtForm.stripBar1.Items[1].Text != "");
            mnuEditFile.Enabled = notbusy && (!GIS.IsEmpty) && (edtForm.stripBar1.Items[1].Text != "");
            btnSaveToImage.Enabled = notbusy && (!GIS.IsEmpty);
            btnSaveAll.Enabled = notbusy && (GIS.MustSave());
            notbusy = stsBar.Items[0].Text == "";
        }



        private void OpenCoverage(string _path)
        {
            int i;

            // clear the viewer
            actCloseExecute();

            try
            {
                // open selected file
                GIS.Open(_path);

                mnuExportLayer.Enabled = false;
                for (i = 0; i < GIS.Items.Count; i++)
                {
                    // for layers of TGIS_LayerVector type enable export
                    if (((TGIS_LayerAbstract)GIS.Items[i]) is TGIS_LayerVector)
                        mnuExportLayer.Enabled = true;
                }

                stsBar.Items[3].Text = Path.GetFileName(_path);
            }
            catch (Exception e)
            {
                // if anything went wrong, show a warning
                MessageBox.Show("File can't be open\n" + e);
                GIS.Close();
                GIS_ControlLegend.Update();
            }
            GIS.UseRTree = mnuUseRTree.Checked;
            //? GIS.PrintTitle  = _path ;
            //? GIS.PrintFooter = "Printed by TatukGIS. See our web page: www.tatukgis.com" ;
        }

        private void AppendCoverage(string _path)
        {
            TGIS_Layer ll;

            stsBar.Items[3].Text = "";
            try
            {
                mnuExportLayer.Enabled = false;
                // create a new layer
                ll = TGIS_Utils.GisCreateLayer(Path.GetFileName(_path), _path);
                // and add it to the viewer
                if (ll != null)
                {
                    ll.ReadConfig();
                    GIS.Add(ll);
                    if (ll is TGIS_LayerVector)
                        mnuExportLayer.Enabled = true;
                    GIS.Update();
                }

                stsBar.Items[3].Text = stsBar.Items[3].Text +
                                                                Path.GetFileName(_path) + " ";
            }
            catch (Exception e)
            {
                MessageBox.Show("File can't be open\n" + e);
            }
            GIS.UseRTree = mnuUseRTree.Checked;
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFileOpen) actFileOpenExecute();
            else if (sender == btnAppend) actAppendExecute();
            else if (sender == btnClose) actCloseExecute();
            else if (sender == btnFilePrint) actFilePrintExecute();
            else if (sender == btnEditFile) actEditFileExecute();
            else if (sender == btnSaveToImage) actSaveToImageExecute();
            else if (sender == btnViewZoomMode) actViewZoomModeExecute();
            else if (sender == btnViewDragMode) actViewDragModeExecute();
            else if (sender == btnViewSelectMode) actViewSelectModeExecute();
            else if (sender == btnSearch) actSearchExecute();
            else if (sender == btnSaveAll) actSaveAllExecute();
            else if (sender == btnLegendMode) actLegendMode();
            else if (sender == btnCS) actCS();

            updateToolBar();
        }

        private void toolStrip1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            if (toolStrip1.Items[0].Bounds.Contains(p) ||
                     toolStrip1.Items[1].Bounds.Contains(p) ||
                     toolStrip1.Items[2].Bounds.Contains(p) ||
                     toolStrip1.Items[3].Bounds.Contains(p) ||
                     toolStrip1.Items[4].Bounds.Contains(p) ||
                     toolStrip1.Items[5].Bounds.Contains(p) ||
                     toolStrip1.Items[7].Bounds.Contains(p) ||
                     toolStrip1.Items[8].Bounds.Contains(p) ||
                     toolStrip1.Items[9].Bounds.Contains(p) ||
                     toolStrip1.Items[11].Bounds.Contains(p) ||
                     toolStrip1.Items[12].Bounds.Contains(p) ||
                     toolStrip1.Items[13].Bounds.Contains(p))
                toolStrip1.Cursor = Cursors.Hand;
            else
                toolStrip1.Cursor = Cursors.Default;
        }

        private void mnuFileOpen_Click(object sender, System.EventArgs e)
        {
            actFileOpenExecute();
            updateToolBar();
        }

        private void mnuAppend_Click(object sender, System.EventArgs e)
        {
            actAppendExecute();
            updateToolBar();
        }

        private void mnuEditFile_Click(object sender, System.EventArgs e)
        {
            actEditFileExecute();
            updateToolBar();
        }

        private void mnuExportLayer_Click(object sender, System.EventArgs e)
        {
            actFileExportExecute();
            updateToolBar();
        }

        private void mnuFilePrint_Click(object sender, System.EventArgs e)
        {
            actFilePrintExecute();
            updateToolBar();
        }

        private void mnuClose_Click(object sender, System.EventArgs e)
        {
            actCloseExecute();
            updateToolBar();
        }

        private void mnuFileExit_Click(object sender, System.EventArgs e)
        {
            actFileExitExecute();
        }

        private void mnuViewFullExtent_Click(object sender, System.EventArgs e)
        {
            actViewFullExtentExecute();
            updateToolBar();
        }

        private void mnuViewZoomMode_Click(object sender, System.EventArgs e)
        {
            actViewZoomModeExecute();
            updateToolBar();
        }

        private void mnuViewDragMode_Click(object sender, System.EventArgs e)
        {
            actViewDragModeExecute();
            updateToolBar();
        }

        private void mnuViewSelectMode_Click(object sender, System.EventArgs e)
        {
            actViewSelectModeExecute();
            updateToolBar();
        }

        private void mnuUseRTree_Click(object sender, System.EventArgs e)
        {
            mnuUseRTree.Checked = !mnuUseRTree.Checked;
            actUseRTreeExecute();
            updateToolBar();
        }

        private void mnuColor_Click(object sender, System.EventArgs e)
        {
            actColorExecute();
            updateToolBar();
        }

        private void mnuFindShape_Click(object sender, System.EventArgs e)
        {
            actSearchExecute();
            updateToolBar();
        }


        private void GIS_Busy(object _sender, TatukGIS.NDK.TGIS_BusyEventArgs _e)
        {
            // show busy state
            _e.Abort = false;
            if (_e.EndPos <= 0)
                stsBar.Items[0].Text = "";
            else
                stsBar.Items[0].Text = String.Format("Busy {0}%", Math.Round((Double)(_e.Pos * 100 / _e.EndPos), 0));
            Application.DoEvents();
        }

        private void GIS_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Shape shp;

            // if there is no layer or we are not in select mode, exit
            if (GIS.IsEmpty) return;
            if (GIS.IsEmpty) return;

            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            // let's try to locate a selected shape on the map
            shp = (TGIS_Shape)GIS.Locate(GIS.ScreenToMap(new Point(e.X, e.Y)),
                                          5 / GIS.Zoom
                                        );

            if (shp == null) return;
            // if any found flash it and show shape info
            shp.Flash();
            if (infForm == null)
            {
                infForm = new InfoForm();
                infForm.mainForm = this;
                infForm.Show();
            }
            infForm.ShowInfo(shp);
        }

        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;

            if (GIS.IsEmpty) return;

            // let's locate our position on the map and display coordinates, zoom
            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            stsBar.Items[1].Text = String.Format("X : {0:F4} | Y : {1:F4}",
                                                                                         ptg.X, ptg.Y
                                                                                     );
            stsBar.Items[2].Text = String.Format("Zoom : {0:F4} | ZoomEx : {1:F4}",
                                                                                         GIS.Zoom, GIS.ZoomEx
                                                                                     );
        }

        private void actFileOpenExecute()
        {
            string str, ext;

            if (dlgFileOpen.ShowDialog() != DialogResult.OK) return;

            str = dlgFileOpen.FileName;
            ext = Path.GetExtension(str);
            // if project selected, load it to editor
            if ((String.Compare(ext, ".ttkgp") == 0) ||
                (String.Compare(ext, ".ttkproject") == 0) ||
                (String.Compare(ext, ".ttkls") == 0))
            {
                edtForm.stripBar1.Items[1].Text = str;
                edtForm.Editor.Enabled = true;
                edtForm.Editor.Clear();
                edtForm.LoadFromFile(str);
                edtForm.btnSave.Enabled = false;
            }
            else
            // if config found, load it to editor
            if (File.Exists(str + ".ini"))
            {
                edtForm.stripBar1.Items[1].Text = str + ".ini";
                edtForm.Editor.Enabled = true;
                edtForm.Editor.Clear();
                edtForm.LoadFromFile(str + ".ini");
                edtForm.btnSave.Enabled = false;
            }
            else
            {
                edtForm.Editor.Enabled = false;
                edtForm.stripBar1.Items[1].Text = "";
                edtForm.Editor.Clear();
                edtForm.btnSave.Enabled = false;
            }

            // check file extension
            switch (dlgFileOpen.FilterIndex)
            {
                case 8:
                    str = str + "?ARC";
                    break;
                case 9:
                    str = str + "?PAL";
                    break;
                case 10:
                    str = str + "?LAB";
                    break;
            }
            // open selected file
            OpenCoverage(str);
        }

        private void actFileExportExecute()
        {
            TGIS_LayerVector ll;
            TGIS_Extent extent;
            TGIS_ShapeType shape_type;
            bool clipping;
            string ext;

            expForm = new ExportForm();
            try
            {
                expForm.mainForm = this;
                if (expForm.ShowDialog() == DialogResult.Cancel) return;

                dlgFileSave.FileName = "";
                if (dlgFileSave.ShowDialog() != DialogResult.OK) return;

                // check the extension to choose a proper layer
                ext = Path.GetExtension(dlgFileSave.FileName);
                if (String.Compare(ext.ToLower(), ".shp") == 0)
                    ll = new TGIS_LayerSHP();
                else if (String.Compare(ext.ToLower(), ".mif") == 0)
                    ll = new TGIS_LayerMIF();
                else if (String.Compare(ext.ToLower(), ".dxf") == 0)
                    ll = new TGIS_LayerDXF();
                else if (String.Compare(ext.ToLower(), ".ttkls") == 0)
                    ll = (TGIS_LayerVector)TGIS_Utils.GisCreateLayer("", dlgFileSave.FileName);
                else
                {
                    MessageBox.Show("Unrecognized file extension");
                    return;
                }

                shape_type = TGIS_ShapeType.Unknown;
                extent = TGIS_Utils.GisWholeWorld();
                clipping = false;

                // set the extent
                switch (expForm.SelectedExtent)
                {
                    case 0:
                        extent = TGIS_Utils.GisWholeWorld();
                        clipping = false;
                        break;
                    case 1:
                        extent = GIS.VisibleExtent;
                        clipping = false;
                        break;
                    case 2:
                        extent = GIS.VisibleExtent;
                        clipping = true;
                        break;
                    default:
                        MessageBox.Show("Untested extent case.");
                        return;
                }

                // set layer type
                switch (expForm.cmbShapeType.SelectedIndex)
                {
                    case 0:
                        shape_type = TGIS_ShapeType.Unknown;
                        break;
                    case 1:
                        shape_type = TGIS_ShapeType.Arc;
                        break;
                    case 2:
                        shape_type = TGIS_ShapeType.Polygon;
                        break;
                    case 3:
                        shape_type = TGIS_ShapeType.Point;
                        break;
                    case 4:
                        shape_type = TGIS_ShapeType.MultiPoint;
                        break;
                    default:
                        MessageBox.Show("Untested shape type case.");
                        return;
                }

                try
                {
                    // try to import existing layer to a new one and save it to the file
                    ll.Path = dlgFileSave.FileName;
                    ll.CS = expForm.CS;
                    ll.ImportLayer((TGIS_LayerVector)GIS.Get(expForm.cmbLayersList.Text),
                                                    extent,
                                                    shape_type,
                                                    expForm.edtQuery.Text,
                                                    clipping
                                                );
                }
                finally
                {
                    ll = null;
                }
            }
            finally
            {
                expForm = null;
            }
        }

        private void actAppendExecute()
        {
            int i;
            string fileName;

            if (dlgFileAppend.ShowDialog() != DialogResult.OK) return;

            for (i = 0; i < dlgFileAppend.FileNames.Length; i++)
            {
                fileName = dlgFileAppend.FileNames[i];
                switch (dlgFileAppend.FilterIndex)
                {
                    case 8:
                        fileName = fileName + "?ARC";
                        break;
                    case 9:
                        fileName = fileName + "?PAL";
                        break;
                    case 10:
                        fileName = fileName + "?LAB";
                        break;
                }
                // add all selected files to the viewer
                AppendCoverage(fileName);
            }
        }

        private void actCloseExecute()
        {
            GIS.Close();
            stsBar.Items[0].Text = "";
            stsBar.Items[1].Text = "";
            stsBar.Items[2].Text = "";
            stsBar.Items[3].Text = "";
        }

        private void actFilePrintExecute()
        {
            // let's see a print preview form
            GIS_ControlPrintPreviewSimple.Preview();
        }

        private void actEditFileExecute()
        {
            edtForm.ShowDialog();
        }

        private void actSaveToImageExecute()
        {
            TGIS_PixelExportManager pem;
            TGIS_LayerPixel lp;
            int w, h;
            int dpi;
            TGIS_LayerPixelSubFormat sf;
            TGIS_ViewerBmp vbmp;
            String path, ext;
            TGIS_PixelSubFormat sub;
            TGIS_CompressionType comp;
            TGIS_Extent ex;

            ex  = GIS.Extent;
            w   = 1024;
            h   = (int)Math.Round((ex.YMax - ex.YMin) / (ex.XMax - ex.XMin) * w);
            ex  = TGIS_Utils.GisExtent(ex.XMin, ex.YMin, ex.XMax, ex.YMin + ((ex.XMax - ex.XMin) / w) * h);
            dpi = 96;

            // save image from the viewer to file
            if (dlgSaveImage.ShowDialog() == DialogResult.OK)
            {

                switch (dlgSaveImage.FilterIndex)
                {
                    case 1: ext = ".bmp"; sub = TGIS_PixelSubFormat.BMP; comp = TGIS_CompressionType.None; break;
                    case 2: ext = ".jpg"; sub = TGIS_PixelSubFormat.JPEG; comp = TGIS_CompressionType.JPEG; break;
                    case 3: ext = ".png"; sub = TGIS_PixelSubFormat.PNG; comp = TGIS_CompressionType.PNG; break;
                    case 4: ext = ".tif"; sub = TGIS_PixelSubFormat.None; comp = TGIS_CompressionType.None; break;
                    default: ext = ""; sub = TGIS_PixelSubFormat.None; comp = TGIS_CompressionType.None; ; break;
                }

                path = dlgSaveImage.FileName;
                if (String.Compare(Path.GetExtension(path), "") == 0)
                    path = path + ext;

                lp = (TGIS_LayerPixel)TGIS_Utils.GisCreateLayer(path, path);
                sf = new TGIS_LayerPixelSubFormat(TGIS_PixelFormat.RGB, false, sub, comp, 90);
                lp.Build(lp.Path, false, GIS.CS, ex, w, h, sf);

                pem = new TGIS_PixelExportManager(lp);
                pem.BusyEvent += GIS_Busy;
                vbmp = new TGIS_ViewerBmp();
                vbmp.PaintExtraEvent += GIS_PaintExtraEvent;
                pem.ExportFrom(GIS, vbmp, ex, dpi);

                vbmp = null;
                pem = null;
                lp.SaveData();
                lp = null;
            }
        }

        private void actViewFullExtentExecute()
        {
            // show the whole world
            GIS.FullExtent();
        }

        private void actViewZoomModeExecute()
        {
            // set zoom mode
            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        private void actViewDragModeExecute()
        {
            // set drag mode
            GIS.Mode = TGIS_ViewerMode.Drag;
        }

        private void actViewSelectModeExecute()
        {
            // set select mode
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void actUseRTreeExecute()
        {
            GIS.UseRTree = mnuUseRTree.Checked;
        }

        private void actSearchExecute()
        {
            // show search form
            srchForm = new SearchForm();
            try
            {
                btnSearch.Checked = true;
                srchForm.mainForm = this;
                srchForm.ShowDialog();
            }
            finally
            {
                btnSearch.Checked = false;
                srchForm = null;
            }
        }

        private void actColorExecute()
        {
            // let's change the viewer color
            if (dlgColor.ShowDialog() != DialogResult.OK) return;

            GIS.BackColor = dlgColor.Color;
            GIS.Update();
        }

        private void actSaveAllExecute()
        {
            GIS.SaveAll();
        }

        private void actFileExitExecute()
        {
            // close the application
            Application.Exit();
        }

        private void WinForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            actCloseExecute();
        }

        private void GIS_Paint(object sender, PaintEventArgs e)
        {

        }

        private void actLegendMode()
        {
            if (GIS_ControlLegend.Mode == TGIS_ControlLegendMode.Layers)
                GIS_ControlLegend.Mode = TGIS_ControlLegendMode.Groups;
            else
                GIS_ControlLegend.Mode = TGIS_ControlLegendMode.Layers;
        }

        private void actCS()
        {
            TGIS_ControlCSSystem dlg;

            dlg = new TGIS_ControlCSSystem();

            if (dlg.Execute(GIS.CS) == DialogResult.OK)
                GIS.CS = dlg.CS;
        }

        private void GIS_PaintExtraEvent(object _sender, TGIS_RendererEventArgs _e)
        {

        }
    }
}
