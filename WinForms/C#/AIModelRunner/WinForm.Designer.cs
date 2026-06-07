// =============================================================================
// Designer-managed file for WinForm.
//
// Visual Studio regenerates this file when the form is edited in the Designer.
// Keep ONLY designer-managed code (field declarations, InitializeComponent,
// Dispose) here. Business logic lives in WinForm.cs.
// =============================================================================

namespace AIModelRunner_New
{
    partial class WinForm
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        // ── Designer-managed controls ────────────────────────────────────────
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.FlowLayoutPanel topFlow;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ComboBox ModelComboBox;
        private System.Windows.Forms.Button btnAddModel;
        private System.Windows.Forms.RadioButton rbRunLayer;
        private System.Windows.Forms.RadioButton rbRunViewer;
        private System.Windows.Forms.Button btnRunModel;

        private System.Windows.Forms.Panel pnlNav;
        private System.Windows.Forms.FlowLayoutPanel navFlow;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnDrag;
        private System.Windows.Forms.Button btnFullExtent;
        private System.Windows.Forms.Button btnZoom;
        private System.Windows.Forms.Button btnZoomEx;

        private System.Windows.Forms.Panel pnlLog;
        private System.Windows.Forms.RichTextBox Memo1;

        private System.Windows.Forms.SplitContainer split;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_Legend;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

        private System.Windows.Forms.OpenFileDialog dlgFileOpen;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.pnlTop = new System.Windows.Forms.Panel();
            this.topFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ModelComboBox = new System.Windows.Forms.ComboBox();
            this.btnAddModel = new System.Windows.Forms.Button();
            this.rbRunLayer = new System.Windows.Forms.RadioButton();
            this.rbRunViewer = new System.Windows.Forms.RadioButton();
            this.btnRunModel = new System.Windows.Forms.Button();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.navFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnDrag = new System.Windows.Forms.Button();
            this.btnFullExtent = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnZoomEx = new System.Windows.Forms.Button();
            this.pnlLog = new System.Windows.Forms.Panel();
            this.Memo1 = new System.Windows.Forms.RichTextBox();
            this.split = new System.Windows.Forms.SplitContainer();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.dlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.pnlTop.SuspendLayout();
            this.topFlow.SuspendLayout();
            this.pnlNav.SuspendLayout();
            this.navFlow.SuspendLayout();
            this.pnlLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.split)).BeginInit();
            this.split.Panel1.SuspendLayout();
            this.split.Panel2.SuspendLayout();
            this.split.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.topFlow);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(4);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1188, 44);
            this.pnlTop.TabIndex = 0;
            // 
            // topFlow
            // 
            this.topFlow.Controls.Add(this.btnOpen);
            this.topFlow.Controls.Add(this.btnReset);
            this.topFlow.Controls.Add(this.label1);
            this.topFlow.Controls.Add(this.ModelComboBox);
            this.topFlow.Controls.Add(this.btnAddModel);
            this.topFlow.Controls.Add(this.rbRunLayer);
            this.topFlow.Controls.Add(this.rbRunViewer);
            this.topFlow.Controls.Add(this.btnRunModel);
            this.topFlow.Location = new System.Drawing.Point(0, 0);
            this.topFlow.Margin = new System.Windows.Forms.Padding(4);
            this.topFlow.Name = "topFlow";
            this.topFlow.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.topFlow.Size = new System.Drawing.Size(1188, 44);
            this.topFlow.TabIndex = 0;
            this.topFlow.WrapContents = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(7, 5);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 32);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(86, 5);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 32);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "Reset";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Location = new System.Drawing.Point(163, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Choose Model:";
            // 
            // ModelComboBox
            // 
            this.ModelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModelComboBox.Location = new System.Drawing.Point(273, 5);
            this.ModelComboBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ModelComboBox.Name = "ModelComboBox";
            this.ModelComboBox.Size = new System.Drawing.Size(205, 21);
            this.ModelComboBox.TabIndex = 2;
            // 
            // btnAddModel
            // 
            this.btnAddModel.Location = new System.Drawing.Point(482, 5);
            this.btnAddModel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnAddModel.Name = "btnAddModel";
            this.btnAddModel.Size = new System.Drawing.Size(170, 32);
            this.btnAddModel.TabIndex = 3;
            this.btnAddModel.Text = "Add Custom Model...";
            // 
            // rbRunLayer
            // 
            this.rbRunLayer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbRunLayer.Checked = true;
            this.rbRunLayer.Location = new System.Drawing.Point(656, 9);
            this.rbRunLayer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rbRunLayer.Name = "rbRunLayer";
            this.rbRunLayer.Size = new System.Drawing.Size(65, 24);
            this.rbRunLayer.TabIndex = 4;
            this.rbRunLayer.TabStop = true;
            this.rbRunLayer.Text = "Layer";
            // 
            // rbRunViewer
            // 
            this.rbRunViewer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rbRunViewer.Location = new System.Drawing.Point(725, 9);
            this.rbRunViewer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rbRunViewer.Name = "rbRunViewer";
            this.rbRunViewer.Size = new System.Drawing.Size(75, 24);
            this.rbRunViewer.TabIndex = 5;
            this.rbRunViewer.Text = "Viewer";
            // 
            // btnRunModel
            // 
            this.btnRunModel.Location = new System.Drawing.Point(804, 5);
            this.btnRunModel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.btnRunModel.Name = "btnRunModel";
            this.btnRunModel.Size = new System.Drawing.Size(110, 32);
            this.btnRunModel.TabIndex = 6;
            this.btnRunModel.Text = "Run Model";
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.navFlow);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNav.Location = new System.Drawing.Point(0, 787);
            this.pnlNav.Margin = new System.Windows.Forms.Padding(4);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(1188, 44);
            this.pnlNav.TabIndex = 4;
            // 
            // navFlow
            // 
            this.navFlow.Controls.Add(this.btnSelect);
            this.navFlow.Controls.Add(this.btnDrag);
            this.navFlow.Controls.Add(this.btnFullExtent);
            this.navFlow.Controls.Add(this.btnZoom);
            this.navFlow.Controls.Add(this.btnZoomEx);
            this.navFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navFlow.Location = new System.Drawing.Point(0, 0);
            this.navFlow.Margin = new System.Windows.Forms.Padding(4);
            this.navFlow.Name = "navFlow";
            this.navFlow.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.navFlow.Size = new System.Drawing.Size(1188, 44);
            this.navFlow.TabIndex = 0;
            this.navFlow.WrapContents = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(7, 7);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(100, 32);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Select";
            // 
            // btnDrag
            // 
            this.btnDrag.Location = new System.Drawing.Point(111, 7);
            this.btnDrag.Margin = new System.Windows.Forms.Padding(2);
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(100, 32);
            this.btnDrag.TabIndex = 1;
            this.btnDrag.Text = "Drag";
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.Location = new System.Drawing.Point(215, 7);
            this.btnFullExtent.Margin = new System.Windows.Forms.Padding(2);
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(100, 32);
            this.btnFullExtent.TabIndex = 2;
            this.btnFullExtent.Text = "Full Extent";
            // 
            // btnZoom
            // 
            this.btnZoom.Location = new System.Drawing.Point(319, 7);
            this.btnZoom.Margin = new System.Windows.Forms.Padding(2);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(100, 32);
            this.btnZoom.TabIndex = 3;
            this.btnZoom.Text = "Zoom";
            // 
            // btnZoomEx
            // 
            this.btnZoomEx.Location = new System.Drawing.Point(423, 7);
            this.btnZoomEx.Margin = new System.Windows.Forms.Padding(2);
            this.btnZoomEx.Name = "btnZoomEx";
            this.btnZoomEx.Size = new System.Drawing.Size(100, 32);
            this.btnZoomEx.TabIndex = 4;
            this.btnZoomEx.Text = "ZoomEx";
            // 
            // pnlLog
            // 
            this.pnlLog.Controls.Add(this.Memo1);
            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLog.Location = new System.Drawing.Point(0, 687);
            this.pnlLog.Margin = new System.Windows.Forms.Padding(4);
            this.pnlLog.Name = "pnlLog";
            this.pnlLog.Size = new System.Drawing.Size(1188, 100);
            this.pnlLog.TabIndex = 3;
            // 
            // Memo1
            // 
            this.Memo1.BackColor = System.Drawing.SystemColors.Window;
            this.Memo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Memo1.Location = new System.Drawing.Point(0, 0);
            this.Memo1.Margin = new System.Windows.Forms.Padding(4);
            this.Memo1.Name = "Memo1";
            this.Memo1.ReadOnly = true;
            this.Memo1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.Memo1.Size = new System.Drawing.Size(1188, 100);
            this.Memo1.TabIndex = 0;
            this.Memo1.Text = "";
            // 
            // split
            // 
            this.split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.split.Location = new System.Drawing.Point(0, 44);
            this.split.Margin = new System.Windows.Forms.Padding(4);
            this.split.Name = "split";
            // 
            // split.Panel1
            // 
            this.split.Panel1.Controls.Add(this.GIS_Legend);
            this.split.Panel1MinSize = 80;
            // 
            // split.Panel2
            // 
            this.split.Panel2.Controls.Add(this.GIS);
            this.split.Panel2MinSize = 80;
            this.split.Size = new System.Drawing.Size(1188, 643);
            this.split.SplitterDistance = 187;
            this.split.SplitterWidth = 5;
            this.split.TabIndex = 5;
            // 
            // GIS_Legend
            // 
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_Legend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(0, 0);
            this.GIS_Legend.Margin = new System.Windows.Forms.Padding(4);
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GIS_Legend.Size = new System.Drawing.Size(187, 643);
            this.GIS_Legend.TabIndex = 0;
            // 
            // GIS
            // 
            this.GIS.AutoStyle = false;
            this.GIS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Level = 1D;
            this.GIS.Location = new System.Drawing.Point(0, 0);
            this.GIS.Margin = new System.Windows.Forms.Padding(4);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(996, 643);
            this.GIS.TabIndex = 0;
            this.GIS.TiledPaint = false;
            // 
            // dlgFileOpen
            // 
            this.dlgFileOpen.Filter = resources.GetString("dlgFileOpen.Filter");
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1188, 831);
            this.Controls.Add(this.split);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlLog);
            this.Controls.Add(this.pnlNav);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(860, 583);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS AI Model Runner";
            this.pnlTop.ResumeLayout(false);
            this.topFlow.ResumeLayout(false);
            this.pnlNav.ResumeLayout(false);
            this.navFlow.ResumeLayout(false);
            this.pnlLog.ResumeLayout(false);
            this.split.Panel1.ResumeLayout(false);
            this.split.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split)).EndInit();
            this.split.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label label1;
    }
}
