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
    /*
    Viewer sample — demonstrates basic GIS map viewing functionality (C#/.NET WinForms).

    What the sample shows:
      - Loading vector and raster data files from disk
      - Appending additional layers to an existing map
      - Managing multiple layers with TGIS_ControlLegend
      - Zoom navigation: full extent, zoom mode, zoom in/out
      - Pan/drag mode for moving around the map
      - Selection mode for identifying features
      - Printing maps with TGIS_ControlPrintPreview
      - Exporting map view to image file
      - Searching/querying layer features
      - Editing layer data (adding/modifying features)
      - Saving modified layers back to file
      - Switching between different interaction modes
      - Displaying layer properties and metadata
      - File dialogs for open, append, save operations

    Key TatukGIS API concepts shown here:
      TGIS_ViewerWnd              - main visual map control
      TGIS_ControlLegend          - layer tree and legend management
      TGIS_LayerVector            - vector layer (shapefile, etc.)
      TGIS_LayerPixel             - raster layer (image, grid)
      File I/O                    - loading/saving layers
      Zoom/pan navigation         - viewer extent control
      Layer management            - add, remove, reorder layers
      Feature selection           - interactive feature picking
      Print and export            - output generation
      Interaction modes           - zoom, drag, select, edit
    */
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
        private TatukGIS.NDK.WinForms.TGIS_ControlScale GIS_ControlScale;
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
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(WinForm));
            TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TGIS_ControlLegendDialogOptions();
            TGIS_CSUnits tgiS_csUnits1 = new TGIS_CSUnits();
            mainMenu1 = new ContextMenuStrip(components);
            mnuFile = new ToolStripMenuItem();
            mnuFileOpen = new ToolStripMenuItem();
            mnuAppend = new ToolStripMenuItem();
            mnuEditFile = new ToolStripMenuItem();
            mnuExportLayer = new ToolStripMenuItem();
            mnuFilePrint = new ToolStripMenuItem();
            mnuClose = new ToolStripMenuItem();
            ToolStripMenuItem9 = new ToolStripMenuItem();
            mnuFileExit = new ToolStripMenuItem();
            mnuView = new ToolStripMenuItem();
            mnuViewFullExtent = new ToolStripMenuItem();
            ToolStripMenuItem13 = new ToolStripMenuItem();
            mnuViewZoomMode = new ToolStripMenuItem();
            mnuViewDragMode = new ToolStripMenuItem();
            mnuViewSelectMode = new ToolStripMenuItem();
            mnuOptions = new ToolStripMenuItem();
            mnuUseRTree = new ToolStripMenuItem();
            mnuColor = new ToolStripMenuItem();
            mnuSearch = new ToolStripMenuItem();
            mnuFindShape = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            imageList1 = new ImageList(components);
            btnFileOpen = new ToolStripButton();
            btnAppend = new ToolStripButton();
            btnClose = new ToolStripButton();
            btnFilePrint = new ToolStripButton();
            btnEditFile = new ToolStripButton();
            btnSaveToImage = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnViewFullExtent = new ToolStripButton();
            btnViewZoomMode = new ToolStripButton();
            btnViewDragMode = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnViewSelectMode = new ToolStripButton();
            btnSearch = new ToolStripButton();
            btnSaveAll = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnLegendMode = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            btnCS = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            stsBar = new StatusStrip();
            stsBarPanel1 = new ToolStripLabel();
            stsBarPanel2 = new ToolStripLabel();
            stsBarPanel3 = new ToolStripLabel();
            stsBarPanel4 = new ToolStripLabel();
            splitter1 = new Splitter();
            dlgFileOpen = new OpenFileDialog();
            dlgFileSave = new SaveFileDialog();
            dlgColor = new ColorDialog();
            dlgFileAppend = new OpenFileDialog();
            dlgSaveImage = new SaveFileDialog();
            GIS = new TGIS_ViewerWnd();
            GIS_ControlLegend = new TGIS_ControlLegend();
            GIS_ControlScale = new TGIS_ControlScale();
            GIS_ControlPrintPreviewSimple = new TGIS_ControlPrintPreviewSimple();
            mainMenu1.SuspendLayout();
            toolStrip1.SuspendLayout();
            stsBar.SuspendLayout();
            GIS.SuspendLayout();
            SuspendLayout();
            // 
            // mainMenu1
            // 
            mainMenu1.Items.AddRange(new ToolStripItem[] { mnuFile, mnuView, mnuOptions, mnuSearch });
            mainMenu1.Name = "mainMenu1";
            mainMenu1.Size = new Size(117, 92);
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuFileOpen, mnuAppend, mnuEditFile, mnuExportLayer, mnuFilePrint, mnuClose, ToolStripMenuItem9, mnuFileExit });
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new Size(116, 22);
            mnuFile.Text = "File";
            // 
            // mnuFileOpen
            // 
            mnuFileOpen.Name = "mnuFileOpen";
            mnuFileOpen.Size = new Size(232, 22);
            mnuFileOpen.Text = "Open ...";
            mnuFileOpen.Click += mnuFileOpen_Click;
            // 
            // mnuAppend
            // 
            mnuAppend.Name = "mnuAppend";
            mnuAppend.ShortcutKeys = Keys.Control | Keys.A;
            mnuAppend.Size = new Size(232, 22);
            mnuAppend.Text = "Add ...";
            mnuAppend.Click += mnuAppend_Click;
            // 
            // mnuEditFile
            // 
            mnuEditFile.Name = "mnuEditFile";
            mnuEditFile.ShortcutKeys = Keys.Control | Keys.E;
            mnuEditFile.Size = new Size(232, 22);
            mnuEditFile.Text = "Edit project/config file";
            mnuEditFile.Click += mnuEditFile_Click;
            // 
            // mnuExportLayer
            // 
            mnuExportLayer.Name = "mnuExportLayer";
            mnuExportLayer.Size = new Size(232, 22);
            mnuExportLayer.Text = "Export ...";
            mnuExportLayer.Click += mnuExportLayer_Click;
            // 
            // mnuFilePrint
            // 
            mnuFilePrint.Name = "mnuFilePrint";
            mnuFilePrint.Size = new Size(232, 22);
            mnuFilePrint.Text = "Print ...";
            mnuFilePrint.Click += mnuFilePrint_Click;
            // 
            // mnuClose
            // 
            mnuClose.Name = "mnuClose";
            mnuClose.ShortcutKeys = Keys.Control | Keys.Q;
            mnuClose.Size = new Size(232, 22);
            mnuClose.Text = "Close";
            mnuClose.Click += mnuClose_Click;
            // 
            // ToolStripMenuItem9
            // 
            ToolStripMenuItem9.Name = "ToolStripMenuItem9";
            ToolStripMenuItem9.Size = new Size(232, 22);
            ToolStripMenuItem9.Text = "-";
            // 
            // mnuFileExit
            // 
            mnuFileExit.Name = "mnuFileExit";
            mnuFileExit.Size = new Size(232, 22);
            mnuFileExit.Text = "Exit";
            mnuFileExit.Click += mnuFileExit_Click;
            // 
            // mnuView
            // 
            mnuView.DropDownItems.AddRange(new ToolStripItem[] { mnuViewFullExtent, ToolStripMenuItem13, mnuViewZoomMode, mnuViewDragMode, mnuViewSelectMode });
            mnuView.Name = "mnuView";
            mnuView.Size = new Size(116, 22);
            mnuView.Text = "View";
            // 
            // mnuViewFullExtent
            // 
            mnuViewFullExtent.Name = "mnuViewFullExtent";
            mnuViewFullExtent.Size = new Size(128, 22);
            mnuViewFullExtent.Text = "Full Extent";
            mnuViewFullExtent.Click += mnuViewFullExtent_Click;
            // 
            // ToolStripMenuItem13
            // 
            ToolStripMenuItem13.Name = "ToolStripMenuItem13";
            ToolStripMenuItem13.Size = new Size(128, 22);
            ToolStripMenuItem13.Text = "-";
            // 
            // mnuViewZoomMode
            // 
            mnuViewZoomMode.Name = "mnuViewZoomMode";
            mnuViewZoomMode.Size = new Size(128, 22);
            mnuViewZoomMode.Text = "Zoom";
            mnuViewZoomMode.Click += mnuViewZoomMode_Click;
            // 
            // mnuViewDragMode
            // 
            mnuViewDragMode.Name = "mnuViewDragMode";
            mnuViewDragMode.Size = new Size(128, 22);
            mnuViewDragMode.Text = "Drag";
            mnuViewDragMode.Click += mnuViewDragMode_Click;
            // 
            // mnuViewSelectMode
            // 
            mnuViewSelectMode.Name = "mnuViewSelectMode";
            mnuViewSelectMode.Size = new Size(128, 22);
            mnuViewSelectMode.Text = "Info";
            mnuViewSelectMode.Click += mnuViewSelectMode_Click;
            // 
            // mnuOptions
            // 
            mnuOptions.DropDownItems.AddRange(new ToolStripItem[] { mnuUseRTree, mnuColor });
            mnuOptions.Name = "mnuOptions";
            mnuOptions.Size = new Size(116, 22);
            mnuOptions.Text = "Options";
            // 
            // mnuUseRTree
            // 
            mnuUseRTree.Name = "mnuUseRTree";
            mnuUseRTree.Size = new Size(128, 22);
            mnuUseRTree.Text = "Use R-tree";
            mnuUseRTree.Click += mnuUseRTree_Click;
            // 
            // mnuColor
            // 
            mnuColor.Name = "mnuColor";
            mnuColor.Size = new Size(128, 22);
            mnuColor.Text = "Color";
            mnuColor.Click += mnuColor_Click;
            // 
            // mnuSearch
            // 
            mnuSearch.DropDownItems.AddRange(new ToolStripItem[] { mnuFindShape });
            mnuSearch.Name = "mnuSearch";
            mnuSearch.Size = new Size(116, 22);
            mnuSearch.Text = "Search";
            // 
            // mnuFindShape
            // 
            mnuFindShape.Name = "mnuFindShape";
            mnuFindShape.ShortcutKeys = Keys.Control | Keys.F;
            mnuFindShape.Size = new Size(171, 22);
            mnuFindShape.Text = "Find shape";
            mnuFindShape.Click += mnuFindShape_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.AutoSize = false;
            toolStrip1.ImageList = imageList1;
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnFileOpen, btnAppend, btnClose, btnFilePrint, btnEditFile, btnSaveToImage, toolStripSeparator1, btnViewFullExtent, btnViewZoomMode, btnViewDragMode, toolStripSeparator2, btnViewSelectMode, btnSearch, btnSaveAll, toolStripSeparator3, btnLegendMode, toolStripSeparator4, btnCS, toolStripSeparator5 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(592, 24);
            toolStrip1.TabIndex = 0;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Fuchsia;
            imageList1.Images.SetKeyName(0, "");
            imageList1.Images.SetKeyName(1, "");
            imageList1.Images.SetKeyName(2, "");
            imageList1.Images.SetKeyName(3, "");
            imageList1.Images.SetKeyName(4, "");
            imageList1.Images.SetKeyName(5, "");
            imageList1.Images.SetKeyName(6, "");
            imageList1.Images.SetKeyName(7, "");
            imageList1.Images.SetKeyName(8, "");
            imageList1.Images.SetKeyName(9, "");
            imageList1.Images.SetKeyName(10, "");
            imageList1.Images.SetKeyName(11, "");
            imageList1.Images.SetKeyName(12, "DesktopSave1_b.bmp");
            imageList1.Images.SetKeyName(13, "ExtentLayer.bmp");
            imageList1.Images.SetKeyName(14, "FullScreen.bmp");
            // 
            // btnFileOpen
            // 
            btnFileOpen.ImageIndex = 4;
            btnFileOpen.Name = "btnFileOpen";
            btnFileOpen.Size = new Size(23, 21);
            btnFileOpen.ToolTipText = "Open GIS coverage";
            btnFileOpen.Click += toolStrip1_ButtonClick;
            // 
            // btnAppend
            // 
            btnAppend.ImageIndex = 6;
            btnAppend.Name = "btnAppend";
            btnAppend.Size = new Size(23, 21);
            btnAppend.ToolTipText = "Add layers";
            btnAppend.Click += toolStrip1_ButtonClick;
            // 
            // btnClose
            // 
            btnClose.ImageIndex = 8;
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(23, 21);
            btnClose.ToolTipText = "Close all layers";
            btnClose.Click += toolStrip1_ButtonClick;
            // 
            // btnFilePrint
            // 
            btnFilePrint.ImageIndex = 5;
            btnFilePrint.Name = "btnFilePrint";
            btnFilePrint.Size = new Size(23, 21);
            btnFilePrint.ToolTipText = "Print preview";
            btnFilePrint.Click += toolStrip1_ButtonClick;
            // 
            // btnEditFile
            // 
            btnEditFile.ImageIndex = 9;
            btnEditFile.Name = "btnEditFile";
            btnEditFile.Size = new Size(23, 21);
            btnEditFile.ToolTipText = "Edit project/config file";
            btnEditFile.Click += toolStrip1_ButtonClick;
            // 
            // btnSaveToImage
            // 
            btnSaveToImage.ImageIndex = 10;
            btnSaveToImage.Name = "btnSaveToImage";
            btnSaveToImage.Size = new Size(23, 21);
            btnSaveToImage.ToolTipText = "Save to Image";
            btnSaveToImage.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 24);
            // 
            // btnViewFullExtent
            // 
            btnViewFullExtent.ImageIndex = 0;
            btnViewFullExtent.Name = "btnViewFullExtent";
            btnViewFullExtent.Size = new Size(23, 21);
            btnViewFullExtent.ToolTipText = "Fit map into the window";
            btnViewFullExtent.Click += toolStrip1_ButtonClick;
            // 
            // btnViewZoomMode
            // 
            btnViewZoomMode.ImageIndex = 1;
            btnViewZoomMode.Name = "btnViewZoomMode";
            btnViewZoomMode.Size = new Size(23, 21);
            btnViewZoomMode.ToolTipText = "Zoom mode";
            btnViewZoomMode.Click += toolStrip1_ButtonClick;
            // 
            // btnViewDragMode
            // 
            btnViewDragMode.ImageIndex = 2;
            btnViewDragMode.Name = "btnViewDragMode";
            btnViewDragMode.Size = new Size(23, 21);
            btnViewDragMode.ToolTipText = "Drag mode";
            btnViewDragMode.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 24);
            // 
            // btnViewSelectMode
            // 
            btnViewSelectMode.ImageIndex = 3;
            btnViewSelectMode.Name = "btnViewSelectMode";
            btnViewSelectMode.Size = new Size(23, 21);
            btnViewSelectMode.ToolTipText = "Select mode";
            btnViewSelectMode.Click += toolStrip1_ButtonClick;
            // 
            // btnSearch
            // 
            btnSearch.ImageIndex = 7;
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(23, 21);
            btnSearch.ToolTipText = "Search";
            btnSearch.Click += toolStrip1_ButtonClick;
            // 
            // btnSaveAll
            // 
            btnSaveAll.ImageIndex = 11;
            btnSaveAll.Name = "btnSaveAll";
            btnSaveAll.Size = new Size(23, 21);
            btnSaveAll.ToolTipText = "Save changes";
            btnSaveAll.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 24);
            // 
            // btnLegendMode
            // 
            btnLegendMode.ImageIndex = 12;
            btnLegendMode.Name = "btnLegendMode";
            btnLegendMode.Size = new Size(23, 21);
            btnLegendMode.ToolTipText = "Legend mode";
            btnLegendMode.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 24);
            // 
            // btnCS
            // 
            btnCS.ImageIndex = 13;
            btnCS.Name = "btnCS";
            btnCS.Size = new Size(23, 21);
            btnCS.ToolTipText = "Coordinate system";
            btnCS.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 24);
            // 
            // stsBar
            // 
            stsBar.Items.AddRange(new ToolStripItem[] { stsBarPanel1, stsBarPanel2, stsBarPanel3, stsBarPanel4 });
            stsBar.Location = new Point(0, 424);
            stsBar.Name = "stsBar";
            stsBar.Size = new Size(592, 22);
            stsBar.TabIndex = 1;
            // 
            // stsBarPanel1
            // 
            stsBarPanel1.Name = "stsBarPanel1";
            stsBarPanel1.Size = new Size(0, 20);
            // 
            // stsBarPanel2
            // 
            stsBarPanel2.Name = "stsBarPanel2";
            stsBarPanel2.Size = new Size(0, 20);
            // 
            // stsBarPanel3
            // 
            stsBarPanel3.Name = "stsBarPanel3";
            stsBarPanel3.Size = new Size(0, 20);
            // 
            // stsBarPanel4
            // 
            stsBarPanel4.Name = "stsBarPanel4";
            stsBarPanel4.Size = new Size(0, 20);
            // 
            // splitter1
            // 
            splitter1.Location = new Point(166, 24);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 400);
            splitter1.TabIndex = 3;
            splitter1.TabStop = false;
            // 
            // dlgFileSave
            // 
            dlgFileSave.CreatePrompt = true;
            dlgFileSave.Filter = "Arcview Shape File (*.shp, *.shx, *.dbf)|*.SHP|Autocad (*.dxf)|*.DXF|Mapinfo Interchange (*.mif, *.mid)|*.MIF|TatukGIS SQL Layer (.ttkls)|*.TTKLS";
            dlgFileSave.Title = "Export layer";
            // 
            // dlgFileAppend
            // 
            dlgFileAppend.Multiselect = true;
            // 
            // dlgSaveImage
            // 
            dlgSaveImage.CreatePrompt = true;
            dlgSaveImage.Filter = "BMP|*.BMP|JPG|*.JPG|PNG|*.PNG|TIF|*.TIF";
            dlgSaveImage.Title = "Save to Image";
            // 
            // GIS
            // 
            GIS.AutoStyle = false;
            GIS.BackColor = Color.FromArgb(255, 255, 255);
            GIS.Controls.Add(GIS_ControlScale);
            GIS.Dock = DockStyle.Fill;
            GIS.Level = 1D;
            GIS.Location = new Point(169, 24);
            GIS.Mode = TGIS_ViewerMode.Zoom;
            GIS.Name = "GIS";
            GIS.RestrictedDrag = false;
            GIS.SelectionColor = Color.FromArgb(255, 255, 255);
            GIS.Size = new Size(423, 400);
            GIS.TabIndex = 4;
            GIS.TiledPaint = false;
            GIS.BusyEvent += GIS_Busy;
            GIS.PaintExtraEvent += GIS_PaintExtraEvent;
            GIS.SizeChanged += GIS_SizeChanged;
            GIS.MouseDown += GIS_MouseDown;
            GIS.DpiChangedBeforeParent += GIS_DpiChangedBeforeParent;
            GIS.DpiChangedAfterParent += GIS_DpiChangedAfterParent;
            GIS.MouseMove += GIS_MouseMove;
            GIS.Resize += GIS_Resize;
            // 
            // GIS_ControlLegend
            // 
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            GIS_ControlLegend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            GIS_ControlLegend.Dock = DockStyle.Left;
            GIS_ControlLegend.GIS_Viewer = GIS;
            GIS_ControlLegend.Location = new Point(0, 24);
            GIS_ControlLegend.Name = "GIS_ControlLegend";
            GIS_ControlLegend.Options = TGIS_ControlLegendOption.AllowMove | TGIS_ControlLegendOption.AllowActive | TGIS_ControlLegendOption.AllowExpand | TGIS_ControlLegendOption.AllowParams | TGIS_ControlLegendOption.AllowSelect | TGIS_ControlLegendOption.ShowSubLayers;
            GIS_ControlLegend.ReverseOrder = true;
            GIS_ControlLegend.Size = new Size(166, 400);
            GIS_ControlLegend.TabIndex = 2;
            // 
            // GIS_ControlScale
            // 
            GIS_ControlScale.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            GIS_ControlScale.BorderStyle = BorderStyle.FixedSingle;
            GIS_ControlScale.DividerColor1 = Color.Black;
            GIS_ControlScale.DividerColor2 = Color.White;
            GIS_ControlScale.GIS_Viewer = GIS;
            GIS_ControlScale.Location = new Point(245, 348);
            GIS_ControlScale.Name = "GIS_ControlScale";
            GIS_ControlScale.PrepareEvent = null;
            GIS_ControlScale.Size = new Size(166, 39);
            GIS_ControlScale.TabIndex = 2;
            tgiS_csUnits1.DescriptionEx = null;
            GIS_ControlScale.Units = tgiS_csUnits1;
            GIS_ControlScale.UnitsEPSG = 0;
            // 
            // GIS_ControlPrintPreviewSimple
            // 
            GIS_ControlPrintPreviewSimple.GIS_Viewer = GIS;
            // 
            // WinForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(592, 446);
            ContextMenuStrip = mainMenu1;
            Controls.Add(GIS);
            Controls.Add(splitter1);
            Controls.Add(GIS_ControlLegend);
            Controls.Add(stsBar);
            Controls.Add(toolStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(200, 120);
            Name = "WinForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TatukGIS Coverage Viewer";
            Closing += WinForm_Closing;
            Load += frmMain_Load;
            mainMenu1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            stsBar.ResumeLayout(false);
            stsBar.PerformLayout();
            GIS.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #if NET6_0_OR_GREATER
              ApplicationConfiguration.Initialize();
            #else
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
            #endif
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

            ex = GIS.Extent;
            w = 1024;
            h = (int)Math.Round((ex.YMax - ex.YMin) / (ex.XMax - ex.XMin) * w);
            ex = TGIS_Utils.GisExtent(ex.XMin, ex.YMin, ex.XMax, ex.YMin + ((ex.XMax - ex.XMin) / w) * h);
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

        private void GIS_DpiChangedAfterParent(object sender, EventArgs e)
        {
            //?
        }

        private void GIS_DpiChangedBeforeParent(object sender, EventArgs e)
        {
            //GIS.ResetPPI();
        }

        private void GIS_Resize(object sender, EventArgs e)
        {
            //?
        }

        private void GIS_SizeChanged(object sender, EventArgs e)
        {
            //?
        }
    }
}
