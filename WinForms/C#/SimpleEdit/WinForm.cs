using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace SimpleEdit
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoom;
        private System.Windows.Forms.ToolStripButton btnDrag;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolBar2;
        private System.Windows.Forms.ComboBox cmbLayer;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStrip toolBar3;
        private System.Windows.Forms.ToolStripButton btnAddShape;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private System.Windows.Forms.ToolStripButton btnRevert;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnWinding;
        private System.Windows.Forms.ToolStripButton btnShowInfo;
        private System.Windows.Forms.ToolStripButton btnAutoCenter;
        private System.Windows.Forms.ToolStripButton btnNewStyle;
        private System.Windows.Forms.ToolStripButton btnEditorTools;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStrip toolBar4;
        private System.Windows.Forms.ComboBox cmbSnap;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.PrintDialog printDialog1;
        private TGIS_LayerAbstract editLayer;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuAddPart;
        private System.Windows.Forms.ToolStripMenuItem mnuDeletePart;
        private TGIS_Point menuPos;
        private SimpleEdit.InfoForm info;
        private TGIS_EditorHelper oEditorHelper;

        public MainForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.ActiveControl = GIS;
            this.toolTip1.SetToolTip(this.cmbLayer, "Select shape for editing");
            this.toolTip2.SetToolTip(this.cmbSnap, "Snap to layer");
            //this.GIS.Editor.ShowFast = false ;
            menuPos = new TGIS_Point(0, 0);
            info = new InfoForm();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoom = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.cmbSnap = new System.Windows.Forms.ComboBox();
            this.toolBar4 = new System.Windows.Forms.ToolStrip();
            this.btnNewStyle = new System.Windows.Forms.ToolStripButton();
            this.toolBar3 = new System.Windows.Forms.ToolStrip();
            this.btnAddShape = new System.Windows.Forms.ToolStripButton();
            this.btnUndo = new System.Windows.Forms.ToolStripButton();
            this.btnRedo = new System.Windows.Forms.ToolStripButton();
            this.btnRevert = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnWinding = new System.Windows.Forms.ToolStripButton();
            this.btnEditorTools = new System.Windows.Forms.ToolStripButton();
            this.btnShowInfo = new System.Windows.Forms.ToolStripButton();
            this.btnAutoCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbLayer = new System.Windows.Forms.ComboBox();
            this.toolBar2 = new System.Windows.Forms.ToolStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.menuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddPart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeletePart = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnPrint,
            this.toolStripSeparator1,
            this.btnFullExtent,
            this.toolStripSeparator2,
            this.btnZoom,
            this.btnDrag,
            this.btnSelect,
            this.btnEdit,
            this.toolStripSeparator3});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(185, 24);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 0;
            this.btnSave.Name = "btnSave";
            this.btnSave.ToolTipText = "Save";
            this.btnSave.Click += toolStrip1_ButtonClick;
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 1;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ToolTipText = "Print";
            this.btnPrint.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 2;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // btnZoom
            // 
            this.btnZoom.ImageIndex = 3;
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Checked = true;
            this.btnZoom.ToolTipText = "Zoom";
            this.btnZoom.Click += toolStrip1_ButtonClick;
            // 
            // btnDrag
            // 
            this.btnDrag.ImageIndex = 4;
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.ToolTipText = "Drag";
            this.btnDrag.Click += toolStrip1_ButtonClick;
            // 
            // btnSelect
            // 
            this.btnSelect.ImageIndex = 5;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.ToolTipText = "Select";
            this.btnSelect.Click += toolStrip1_ButtonClick;
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.ImageIndex = 6;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.ToolTipText = "Edit";
            this.btnEdit.Click += toolStrip1_ButtonClick;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
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
            this.imageList1.Images.SetKeyName(12, "");
            this.imageList1.Images.SetKeyName(13, "");
            this.imageList1.Images.SetKeyName(14, "");
            this.imageList1.Images.SetKeyName(15, "FullScreen.bmp");
            this.imageList1.Images.SetKeyName(16, "EdtPoly90.bmp");
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 445);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.toolStripLabel3,
            this.toolStripLabel4});
            this.stripBar1.Size = new System.Drawing.Size(659, 19);
            this.stripBar1.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Width = 342;
            // 
            // contextMenu1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddPart,
            this.mnuDeletePart,
            this.menuItem2,
            this.menuItem1});
            //this.contextMenuStrip1.Popup += new System.EventHandler(this.contextMenu1_Popup);
            // 
            // mnuAddPart
            // 
            this.mnuAddPart.Text = "&Add part";
            this.mnuAddPart.Click += new System.EventHandler(this.mnuAddPart_Click);
            // 
            // mnuDeletePart
            // 
            this.mnuDeletePart.Text = "&Delete part";
            this.mnuDeletePart.Click += new System.EventHandler(this.mnuDeletePart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbSnap);
            this.panel1.Controls.Add(this.toolBar4);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(659, 24);
            this.panel1.TabIndex = 2;
            // 
            // cmbSnap
            // 
            this.cmbSnap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbSnap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSnap.Location = new System.Drawing.Point(508, 2);
            this.cmbSnap.Name = "cmbSnap";
            this.cmbSnap.Size = new System.Drawing.Size(85, 21);
            this.cmbSnap.TabIndex = 4;
            this.cmbSnap.SelectedIndexChanged += new System.EventHandler(this.cmbSnap_SelectedIndexChanged);
            // 
            // toolBar4
            // 
            this.toolBar4.AutoSize = false;
            this.toolBar4.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnNewStyle});
            this.toolBar4.ImageList = this.imageList1;
            this.toolBar4.Location = new System.Drawing.Point(478, 0);
            this.toolBar4.Name = "toolBar4";
            this.toolBar4.ShowItemToolTips = true;
            this.toolBar4.Size = new System.Drawing.Size(181, 24);
            this.toolBar4.TabIndex = 3;
            // 
            // btnNewStyle
            // 
            this.btnNewStyle.ImageIndex = 15;
            this.btnNewStyle.Name = "btnNewStyle";
            this.btnNewStyle.ToolTipText = "New style";
            this.btnNewStyle.Click += toolBar4_ButtonClick;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.toolBar3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(270, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(208, 24);
            this.panel4.TabIndex = 2;
            // 
            // toolBar3
            // 
            this.toolBar3.AutoSize = false;
            this.toolBar3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddShape,
            this.toolStripSeparator4,
            this.btnUndo,
            this.btnRedo,
            this.btnRevert,
            this.btnDelete,
            this.btnWinding,
            this.toolStripSeparator5,
            this.btnEditorTools,
            this.btnShowInfo,
            this.btnAutoCenter,
            this.toolStripSeparator6});
            this.toolBar3.ImageList = this.imageList1;
            this.toolBar3.Location = new System.Drawing.Point(0, 0);
            this.toolBar3.Name = "toolBar3";
            this.toolBar3.ShowItemToolTips = true;
            this.toolBar3.Size = new System.Drawing.Size(208, 24);
            this.toolBar3.TabIndex = 0;
            // 
            // btnAddShape
            // 
            this.btnAddShape.Enabled = false;
            this.btnAddShape.ImageIndex = 7;
            this.btnAddShape.Name = "btnAddShape";
            this.btnAddShape.ToolTipText = "Add shape";
            this.btnAddShape.Click += toolBar3_ButtonClick;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // btnUndo
            // 
            this.btnUndo.Enabled = false;
            this.btnUndo.ImageIndex = 8;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.ToolTipText = "Undo";
            this.btnUndo.Click += toolBar3_ButtonClick;
            // 
            // btnRedo
            // 
            this.btnRedo.Enabled = false;
            this.btnRedo.ImageIndex = 9;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.ToolTipText = "Redo";
            this.btnRedo.Click += toolBar3_ButtonClick;
            // 
            // btnRevert
            // 
            this.btnRevert.Enabled = false;
            this.btnRevert.ImageIndex = 10;
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Click += toolBar3_ButtonClick;
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.ImageIndex = 11;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.ToolTipText = "Delete shape";
            this.btnDelete.Click += toolBar3_ButtonClick;
            // 
            // btnWinding
            // 
            this.btnWinding.Enabled = false;
            this.btnWinding.ImageIndex = 12;
            this.btnWinding.Name = "btnWinding";
            this.btnWinding.ToolTipText = "Change winding";
            this.btnWinding.Click += toolBar3_ButtonClick;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // btnEditorTools
            // 
            this.btnEditorTools.ImageIndex = 16;
            this.btnEditorTools.Name = "toolBarButton7";
            this.btnEditorTools.ToolTipText = "Editor Tools";
            this.btnEditorTools.Click += toolBar3_ButtonClick;
            // 
            // btnShowInfo
            // 
            this.btnShowInfo.ImageIndex = 13;
            this.btnShowInfo.Name = "btnShowInfo";
            this.btnShowInfo.ToolTipText = "Show info window";
            this.btnShowInfo.Click += toolBar3_ButtonClick;
            // 
            // btnAutoCenter
            // 
            this.btnAutoCenter.ImageIndex = 14;
            this.btnAutoCenter.Name = "btnAutoCenter";
            this.btnAutoCenter.ToolTipText = "Autoscroll";
            this.btnAutoCenter.Click += toolBar3_ButtonClick;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbLayer);
            this.panel3.Controls.Add(this.toolBar2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(185, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(85, 24);
            this.panel3.TabIndex = 1;
            // 
            // cmbLayer
            // 
            this.cmbLayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLayer.Location = new System.Drawing.Point(0, 2);
            this.cmbLayer.Name = "cmbLayer";
            this.cmbLayer.Size = new System.Drawing.Size(85, 21);
            this.cmbLayer.TabIndex = 1;
            this.cmbLayer.SelectedIndexChanged += new System.EventHandler(this.cmbLayer_SelectedIndexChanged);
            // 
            // toolBar2
            // 
            this.toolBar2.AutoSize = false;
            this.toolBar2.Location = new System.Drawing.Point(0, 0);
            this.toolBar2.Name = "toolBar2";
            this.toolBar2.ShowItemToolTips = true;
            this.toolBar2.Size = new System.Drawing.Size(85, 24);
            this.toolBar2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 24);
            this.panel2.TabIndex = 0;
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            // 
            // GIS
            // 
            this.GIS.AutoStyle = true;
            this.GIS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Level = 28.140189979287609D;
            this.GIS.Location = new System.Drawing.Point(0, 24);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(659, 421);
            this.GIS.TabIndex = 1;
            this.GIS.EditorChangeEvent += new System.EventHandler(this.GIS_EditorChanged);
            this.GIS.PaintExtraEvent += new TatukGIS.NDK.TGIS_RendererEvent(this.GIS_PaintExtraEvent);
            this.GIS.KeyDownEvent += new System.Windows.Forms.KeyEventHandler(this.GIS_KeyDown);
            this.GIS.KeyUpEvent += new System.Windows.Forms.KeyEventHandler(this.GIS_KeyUp);
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            this.GIS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseUp);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "End edit";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(659, 464);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - SimpleEdit";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.WinForm_Closing);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
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
            Application.Run(new MainForm());
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            TGIS_Layer ll;
            int i;

            stripBar1.Items[3].Text = "See: www.tatukgis.com";
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\samples\SimpleEdit\simpleedit.ttkproject");

            oEditorHelper = new TGIS_EditorHelper(GIS);

            cmbLayer.Items.Add("Show all");
            for (i = 0; i < GIS.Items.Count; i++)
            {
                ll = (TGIS_Layer)GIS.Items[i];
                if (ll is TGIS_LayerVector)
                    cmbLayer.Items.Add(ll.Name);
            }

            if (GIS.Items.Count > 0)
                cmbLayer.SelectedIndex = 0;

            cmbSnap.Items.Add("No snapping");
            for (i = 0; i < GIS.Items.Count; i++)
            {
                ll = (TGIS_Layer)GIS.Items[i];
                if (ll is TGIS_LayerVector)
                    cmbSnap.Items.Add(ll.Name);
            }

            if (GIS.Items.Count > 0)
                cmbSnap.SelectedIndex = 0;

            btnZoom.Checked = true;
            btnZoomClick();

        }


        private void WinForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            endEdit();
            btnSelectClick();

            if (!GIS.MustSave()) return;

            if (MessageBox.Show("Save all unsaved work ?", "TatukGIS",
                                  MessageBoxButtons.YesNo
                                ) == DialogResult.Yes
               )
                GIS.SaveAll();
        }

        private void endEdit()
        {
            btnEdit.Enabled = false;
            btnRevert.Enabled = false;
            btnDelete.Enabled = false;
            btnWinding.Enabled = false;
            btnEditorTools.Enabled = false;
            if (oEditorHelper != null)
            {
                oEditorHelper.DoEndEdit();
            }
            editLayer = null;
            GIS.Editor.EndEdit();
            GIS.Editor.EditorMode = TGIS_EditorModeEx.Normal;

            if (btnShowInfo.Checked)
            {
                info.ShowInfo(null);
            }
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnSave) btnSaveClick();
            else if (sender == btnPrint) btnPrintClick();
            else if (sender == btnFullExtent) btnFullExtentClick();
            else if (sender == btnZoom) btnZoomClick();
            else if (sender == btnDrag) btnDragClick();
            else if (sender == btnSelect) btnSelectClick();
            else if (sender == btnEdit) btnEditClick();
        }

        private void btnSaveClick()
        {
            endEdit();
            btnSelectClick();
            GIS.SaveAll();
        }

        private void btnPrintClick()
        {
            TGIS_PrintManager manager = new TGIS_PrintManager();

            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                manager.Footer = "Printed by TatukGIS. See our web page: www.tatukgis.com";
                manager.Subtitle = DateTime.Now.ToString();
                manager.Print(GIS, new TGIS_Printer(printDocument1));
            }
        }

        private void btnFullExtentClick()
        {
            GIS.FullExtent();
        }

        private void btnZoomClick()
        {
            btnDrag.Checked = false;
            btnSelect.Checked = false;
            btnEdit.Checked = false;

            endEdit();
            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        private void btnDragClick()
        {
            btnZoom.Checked = false;
            btnSelect.Checked = false;
            btnEdit.Checked = false;

            endEdit();
            GIS.Mode = TGIS_ViewerMode.Drag;
        }

        private void btnSelectClick()
        {
            btnZoom.Checked = false;
            btnDrag.Checked = false;
            btnEdit.Checked = false;

            endEdit();
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void btnEditClick()
        {
            btnZoom.Checked = false;
            btnDrag.Checked = false;
            btnSelect.Checked = false;

            btnEdit.Enabled = true;
            btnRevert.Enabled = true;
            btnDelete.Enabled = true;
            btnWinding.Enabled = true;
            btnEditorTools.Enabled = true;

            btnEdit.Checked = true;
            if (GIS.Mode == TGIS_ViewerMode.Edit)
                return;
            endEdit();
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void cmbLayer_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int i;
            TGIS_Layer ll;

            endEdit();
            GIS.Focus();
            btnSelectClick();

            for (i = 1; i < cmbLayer.Items.Count; i++)
            {
                ll = (TGIS_Layer)GIS.Get((string)cmbLayer.Items[i]);
                if (cmbLayer.SelectedIndex == 0)
                    ll.Active = true;
                else
                    ll.Active = (i == cmbLayer.SelectedIndex);
                if (ll.Active)
                    oEditorHelper.Layer = ll;
            }

            btnAddShape.Enabled = (cmbLayer.SelectedIndex != 0);

            GIS.Update();
        }

        private void toolBar3_ButtonClick(object sender, System.EventArgs e)
        {
            if(sender == btnAddShape) btnAddShapeClick();
            else if(sender == btnUndo) btnUndoClick();
            else if(sender == btnRedo) btnRedoClick();
            else if(sender == btnRevert) btnRevertClick();
            else if(sender == btnDelete) btnDeleteClick();
            else if(sender == btnWinding) btnWindingClick();
            else if(sender == btnEditorTools) btnEditorToolClick();
            else if(sender == btnShowInfo) btnShowInfoClick();
            else if(sender == btnAutoCenter) btnAutoCenterClick();
        }

        private void btnEditorToolClick()
        {
            oEditorHelper.DrawingTool = new TGIS_EditorToolLine();
            GIS.Editor.EditorMode = TGIS_EditorModeEx.Extended;
            oEditorHelper.SnapTracing = true;
            GIS.Editor.Mode = TGIS_EditorMode.AfterActivePoint;
        }

        private void btnAddShapeClick()
        {
            endEdit();
            GIS.Mode = TGIS_ViewerMode.Edit;
            editLayer = (TGIS_LayerAbstract)GIS.Get((string)cmbLayer.Items[cmbLayer.SelectedIndex]);
        }

        private void btnUndoClick()
        {
            GIS.Editor.Undo();
            if (GIS.AutoCenter)
                GIS.Zoom = GIS.Zoom;
            oEditorHelper.DoUndo();

        }

        private void btnRedoClick()
        {
            GIS.Editor.Redo();
            if (GIS.AutoCenter)
                GIS.Zoom = GIS.Zoom;
            oEditorHelper.DoRedo();
        }

        private void btnRevertClick()
        {
            GIS.Editor.RevertShape();
            if (btnShowInfo.Checked)
            {
                info.ShowInfo((TGIS_Shape)GIS.Editor.CurrentShape);
            }
        }

        private void btnDeleteClick()
        {
            GIS.Editor.DeleteShape();
            btnSelectClick();
        }

        private void btnWindingClick()
        {
            GIS.Editor.ChangeWinding();
        }

        private void btnShowInfoClick()
        {
            if (btnShowInfo.Checked)
            {
                info.ShowInfo((TGIS_Shape)GIS.Editor.CurrentShape);
                info.Show();
            }
            else
            {
                info.Hide();
            }
        }

        private void btnAutoCenterClick()
        {
            GIS.AutoCenter = btnAutoCenter.Checked;
        }

        private void cmbSnap_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (cmbSnap.SelectedIndex > 0)
                GIS.Editor.SnapLayer = GIS.Get((string)cmbSnap.Items[cmbSnap.SelectedIndex]);
            else
                GIS.Editor.SnapLayer = null;
            GIS.InvalidateEditor(true);
        }

        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;

            if (GIS.IsEmpty) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            stripBar1.Items[1].Text = String.Format("x: {0:F4}", ptg.X);
            stripBar1.Items[2].Text = String.Format("y: {0:F4}", ptg.Y);
            oEditorHelper.DoMouseMove(new Point(e.X, e.Y));

            GIS.Invalidate();
        }

        private void GIS_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                GIS.Mode = TGIS_ViewerMode.Select;
            if (e.KeyCode == Keys.Delete)
                if (GIS.Mode == TGIS_ViewerMode.Edit)
                {
                    GIS.Editor.DeleteShape();
                    GIS.Mode = TGIS_ViewerMode.Select;
                }
        }

        private void GIS_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                if (btnZoom.Checked)
                    GIS.Mode = TGIS_ViewerMode.Zoom;
                else if (btnDrag.Checked)
                    GIS.Mode = TGIS_ViewerMode.Drag;
                else if (btnEdit.Checked)
                    GIS.Mode = TGIS_ViewerMode.Edit;
            }
        }

        private void GIS_EditorChanged(object sender, System.EventArgs e)
        {
            btnUndo.Enabled = GIS.Editor.CanUndo;
            btnRedo.Enabled = GIS.Editor.CanRedo;
        }

        private void mnuAddPart_Click(object sender, System.EventArgs e)
        {
            GIS.Editor.CreatePart(TGIS_Utils.GisPoint3DFrom2D(menuPos));
            oEditorHelper.DoStartEdit();
        }

        private void mnuDeletePart_Click(object sender, System.EventArgs e)
        {
            GIS.Editor.DeletePart();
        }

        private void toolStrip1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            if (toolStrip1.Items[0].Bounds.Contains(p) ||
                toolStrip1.Items[1].Bounds.Contains(p) ||
                toolStrip1.Items[3].Bounds.Contains(p) ||
                toolStrip1.Items[5].Bounds.Contains(p) ||
                toolStrip1.Items[6].Bounds.Contains(p) ||
                toolStrip1.Items[7].Bounds.Contains(p) ||
                toolStrip1.Items[8].Bounds.Contains(p))
                toolStrip1.Cursor = Cursors.Hand;
            else
                toolStrip1.Cursor = Cursors.Default;
        }

        private void toolBar3_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            if (toolBar3.Items[0].Bounds.Contains(p) ||
                 toolBar3.Items[2].Bounds.Contains(p) ||
                 toolBar3.Items[3].Bounds.Contains(p) ||
                 toolBar3.Items[4].Bounds.Contains(p) ||
                 toolBar3.Items[5].Bounds.Contains(p) ||
                 toolBar3.Items[6].Bounds.Contains(p) ||
                 toolBar3.Items[8].Bounds.Contains(p) ||
                 toolBar3.Items[9].Bounds.Contains(p))
                toolBar3.Cursor = Cursors.Hand;
            else
                toolBar3.Cursor = Cursors.Default;
        }

        private void GIS_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_Shape shp;
            Double dist;
            int part;
            TGIS_Point proj;

            if (GIS.IsEmpty) return;
            if (GIS.IsEmpty) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));

            switch (e.Button)
            {
                case MouseButtons.Right:
                    if (GIS.Mode == TGIS_ViewerMode.Edit)
                    {
                        menuPos = ptg;
                        contextMenuStrip1.Show(GIS, new Point(e.X, e.Y));
                    }
                    break;
                case MouseButtons.Left:
                    if (GIS.Mode == TGIS_ViewerMode.Edit)
                    {
                        oEditorHelper.DoMouseUp(new Point(e.X, e.Y));
                        if (editLayer == null) return;
                        else
                        {
                            GIS.Editor.CreateShape(editLayer, ptg, TGIS_ShapeType.Unknown);
                            if (cmbSnap.SelectedIndex > 0)
                                GIS.Editor.SnapLayer = GIS.Get((string)cmbSnap.Items[cmbSnap.SelectedIndex]);
                            else
                                GIS.Editor.SnapLayer = null;
                            oEditorHelper.DoStartEdit();
                            GIS.InvalidateEditor(true);
                            if (btnShowInfo.Checked)
                            {
                                info.ShowInfo((TGIS_Shape)GIS.Editor.CurrentShape);
                            }
                            editLayer = null;
                            btnEditClick();
                        }
                    }
                    else if (GIS.Mode == TGIS_ViewerMode.Select)
                    {
                        endEdit();
                        if (btnShowInfo.Checked)
                        {
                            info.ShowInfo(null);
                        }


                        shp = (TGIS_Shape)GIS.Locate(ptg, 5 / GIS.Zoom);

                        if (shp == null)
                        {
                            oEditorHelper.DoMouseUp(new Point(e.X, e.Y));
                            return;
                        }
                        if (cmbLayer.SelectedIndex != 0)
                            if (shp.Layer != GIS.Get((string)cmbLayer.Items[cmbLayer.SelectedIndex]))
                                return;

                        dist = 0;
                        part = 0;
                        proj = new TGIS_Point(0, 0);
                        if (!GIS.InPaint)
                            shp = shp.Layer.LocateEx(ptg, 5 / GIS.Zoom, -1,
                                                     ref dist, ref part, ref proj
                                                     );
                        else return;
                        if (shp == null) return;

                        GIS.Editor.EditShape(shp, part);
                        oEditorHelper.DoStartEdit();
                        if (cmbSnap.SelectedIndex > 0)
                            GIS.Editor.SnapLayer = (TGIS_LayerAbstract)GIS.Get((string)cmbSnap.Items[cmbSnap.SelectedIndex]);
                        else
                            GIS.Editor.SnapLayer = null;

                        GIS.Mode = TGIS_ViewerMode.Edit;
                        GIS.InvalidateEditor(true);

                        if (btnShowInfo.Checked)
                        {
                            info.ShowInfo(shp);
                        }
                        btnEditClick();
                    }
                    break;
            }
        }

        private void toolBar4_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnNewStyle) btnNewStyleClick();
        }

        private void btnNewStyleClick()
        {
            GIS.Editor.EditingLinesStyle.PenStyle = TGIS_PenStyle.Dash;
            GIS.Editor.EditingLinesStyle.PenColor = TGIS_Color.Lime;

            GIS.Editor.EditingPointsStyle.PointsFont.Name = "Verdana";
            GIS.Editor.EditingPointsStyle.PointsFont.Size = 8;
            GIS.Editor.EditingPointsStyle.PointsFont.Color = TGIS_Color.White;
            GIS.Editor.EditingPointsStyle.PointsBackground = TGIS_Color.Green;

            GIS.Editor.EditingPointsStyle.ActivePoints.BrushStyle = TGIS_BrushStyle.Solid;
            GIS.Editor.EditingPointsStyle.ActivePoints.BrushColor = TGIS_Color.Green;
            GIS.Editor.EditingPointsStyle.ActivePoints.PenStyle = TGIS_PenStyle.Solid;
            GIS.Editor.EditingPointsStyle.ActivePoints.PenColor = TGIS_Color.Black;

            GIS.Editor.EditingPointsStyle.InactivePoints.BrushStyle = TGIS_BrushStyle.Solid;
            GIS.Editor.EditingPointsStyle.InactivePoints.BrushColor = TGIS_Color.Blue;
            GIS.Editor.EditingPointsStyle.InactivePoints.PenStyle = TGIS_PenStyle.Solid;
            GIS.Editor.EditingPointsStyle.InactivePoints.PenColor = TGIS_Color.Black;

            GIS.Editor.EditingPointsStyle.SelectedPoints.BrushStyle = TGIS_BrushStyle.Solid;
            GIS.Editor.EditingPointsStyle.SelectedPoints.BrushColor = TGIS_Color.Red;
            GIS.Editor.EditingPointsStyle.SelectedPoints.PenStyle = TGIS_PenStyle.Solid;
            GIS.Editor.EditingPointsStyle.SelectedPoints.PenColor = TGIS_Color.Black;

            GIS.Editor.EditingPointsStyle.Points3D.BrushStyle = TGIS_BrushStyle.Solid;
            GIS.Editor.EditingPointsStyle.Points3D.BrushColor = TGIS_Color.Purple;
            GIS.Editor.EditingPointsStyle.Points3D.PenStyle = TGIS_PenStyle.Solid;
            GIS.Editor.EditingPointsStyle.Points3D.PenColor = TGIS_Color.Olive;

            GIS.Editor.EditingPointsStyle.SnappingPoints.BrushStyle = TGIS_BrushStyle.Solid;
            GIS.Editor.EditingPointsStyle.SnappingPoints.BrushColor = TGIS_Color.Red;
            GIS.Editor.EditingPointsStyle.SnappingPoints.PenStyle = TGIS_PenStyle.Solid;
            GIS.Editor.EditingPointsStyle.SnappingPoints.PenColor = TGIS_Color.Yellow;

            if (GIS.Editor.InEdit) GIS.Editor.RefreshShape();
        }

        private void GIS_AfterPaintEvent(object sender, PaintEventArgs e)
        {
            stripBar1.Items[0].Text = String.Format("zoom: {0:F4}", GIS.Zoom);
        }

        private void GIS_PaintExtraEvent(object _sender, TGIS_RendererEventArgs _e)
        {
            oEditorHelper.Renderer = _e.Renderer;

            oEditorHelper.DoCanvasPaint();
        }

        private void GIS_MouseDown(object sender, MouseEventArgs e)
        {
            if (GIS.IsEmpty) return;

            if (e.Button == MouseButtons.Left)
                oEditorHelper.DoMouseDown(new Point(e.X, e.Y));
        }

        private void contextMenu1_Popup(object sender, EventArgs e)
        {

        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            endEdit();
        }
    }
}
