using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;
using System.Diagnostics;

namespace WFSManager
{
    /// <summary>
    /// Summary description for WFSForm.
    /// </summary>
    public class WFSForm : System.Windows.Forms.Form
    {
        const int GIS_EPSG_WGS84 = 4326;
        const String GIS_INI_AXIS_ORDER_NE = "NE";

        private Label lURL;
        private Button btnGetLayers;
        private GroupBox gbLayers;
        private TreeView tvLayers;
        private RichTextBox rbLayerInfo;
        private GroupBox gbOptions;
        private Label lbParameteres;
        private TextBox tbParameters;
        private Label lbVersion;
        private ComboBox cbxVersion;
        private GroupBox gbGMLSettings;
        private Label lbOutputFormat;
        private ComboBox cbxOutputFormat;
        private CheckBox cbReverse;
        private ComboBox cbxCoordSys;
        private Label lbCoordSys;
        private GroupBox gbFiltering;
        private CheckBox cbMaxFeatures;
        private CheckBox cbStartIndex;
        private TextBox tbMaxFeatures;
        private TextBox tbStartIndex;
        private CheckBox cbBoundBoxFilter;
        private CheckBox cbClipByVisible;
        private TextBox tbYMax;
        private TextBox tbXMax;
        private TextBox tbXMin;
        private TextBox tbYMin;
        private Label lbXMax;
        private Label lbYMin;
        private Label lbYMax;
        private Label lbXMin;
        private Button btnOpenURL;
        private Button btnAddLayer;
        private Button btnCancel;
        private ComboBox cbxURL;
        private TGIS_FileWFS wfs;
        private Object[] NodeData;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip cms;
        private ToolStripMenuItem locateOnMapToolStripMenuItem;
        private ToolStripMenuItem openMetadataToolStripMenuItem;
        private TGIS_ViewerWnd GIS;
        private WinForm mainForm;

        public WinForm getMainForm()
        {
            return mainForm;
        }

        public void setMainForm(WinForm _form)
        {
            mainForm = _form;
        }

        public TGIS_ViewerWnd getGIS()
        {
            return GIS;
        }
        public void setGIS(TGIS_ViewerWnd _gis)
        {
            GIS = _gis;
        }

        public WFSForm()
        {
            InitializeComponent();
        }

        public WFSForm(TGIS_ViewerWnd _gis)
        {
            InitializeComponent();
            GIS = _gis;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WFSForm));
            this.lURL = new System.Windows.Forms.Label();
            this.btnGetLayers = new System.Windows.Forms.Button();
            this.gbLayers = new System.Windows.Forms.GroupBox();
            this.rbLayerInfo = new System.Windows.Forms.RichTextBox();
            this.tvLayers = new System.Windows.Forms.TreeView();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.locateOnMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMetadataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.lbCoordSys = new System.Windows.Forms.Label();
            this.cbxCoordSys = new System.Windows.Forms.ComboBox();
            this.gbGMLSettings = new System.Windows.Forms.GroupBox();
            this.cbReverse = new System.Windows.Forms.CheckBox();
            this.cbxOutputFormat = new System.Windows.Forms.ComboBox();
            this.lbOutputFormat = new System.Windows.Forms.Label();
            this.cbxVersion = new System.Windows.Forms.ComboBox();
            this.lbVersion = new System.Windows.Forms.Label();
            this.tbParameters = new System.Windows.Forms.TextBox();
            this.lbParameteres = new System.Windows.Forms.Label();
            this.gbFiltering = new System.Windows.Forms.GroupBox();
            this.lbXMax = new System.Windows.Forms.Label();
            this.lbYMin = new System.Windows.Forms.Label();
            this.lbYMax = new System.Windows.Forms.Label();
            this.lbXMin = new System.Windows.Forms.Label();
            this.tbYMax = new System.Windows.Forms.TextBox();
            this.tbXMax = new System.Windows.Forms.TextBox();
            this.tbXMin = new System.Windows.Forms.TextBox();
            this.tbYMin = new System.Windows.Forms.TextBox();
            this.cbClipByVisible = new System.Windows.Forms.CheckBox();
            this.cbBoundBoxFilter = new System.Windows.Forms.CheckBox();
            this.tbMaxFeatures = new System.Windows.Forms.TextBox();
            this.tbStartIndex = new System.Windows.Forms.TextBox();
            this.cbMaxFeatures = new System.Windows.Forms.CheckBox();
            this.cbStartIndex = new System.Windows.Forms.CheckBox();
            this.btnOpenURL = new System.Windows.Forms.Button();
            this.btnAddLayer = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxURL = new System.Windows.Forms.ComboBox();
            this.gbLayers.SuspendLayout();
            this.cms.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.gbGMLSettings.SuspendLayout();
            this.gbFiltering.SuspendLayout();
            this.SuspendLayout();
            // 
            // lURL
            // 
            this.lURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lURL.AutoSize = true;
            this.lURL.Location = new System.Drawing.Point(12, 9);
            this.lURL.Name = "lURL";
            this.lURL.Size = new System.Drawing.Size(32, 13);
            this.lURL.TabIndex = 0;
            this.lURL.Text = "URL:";
            // 
            // btnGetLayers
            // 
            this.btnGetLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetLayers.Location = new System.Drawing.Point(621, 4);
            this.btnGetLayers.Name = "btnGetLayers";
            this.btnGetLayers.Size = new System.Drawing.Size(75, 23);
            this.btnGetLayers.TabIndex = 2;
            this.btnGetLayers.Text = "Get layers";
            this.btnGetLayers.UseVisualStyleBackColor = true;
            this.btnGetLayers.Click += new System.EventHandler(this.btnGetLayers_Click);
            // 
            // gbLayers
            // 
            this.gbLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLayers.Controls.Add(this.rbLayerInfo);
            this.gbLayers.Controls.Add(this.tvLayers);
            this.gbLayers.Location = new System.Drawing.Point(15, 32);
            this.gbLayers.Name = "gbLayers";
            this.gbLayers.Size = new System.Drawing.Size(681, 231);
            this.gbLayers.TabIndex = 3;
            this.gbLayers.TabStop = false;
            this.gbLayers.Text = "Layers:";
            // 
            // rbLayerInfo
            // 
            this.rbLayerInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rbLayerInfo.Location = new System.Drawing.Point(353, 16);
            this.rbLayerInfo.Name = "rbLayerInfo";
            this.rbLayerInfo.Size = new System.Drawing.Size(322, 209);
            this.rbLayerInfo.TabIndex = 1;
            this.rbLayerInfo.Text = "";
            // 
            // tvLayers
            // 
            this.tvLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvLayers.ContextMenuStrip = this.cms;
            this.tvLayers.Location = new System.Drawing.Point(3, 16);
            this.tvLayers.Name = "tvLayers";
            this.tvLayers.Size = new System.Drawing.Size(343, 209);
            this.tvLayers.TabIndex = 0;
            this.tvLayers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLayers_AfterSelect);
            // 
            // cms
            // 
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.locateOnMapToolStripMenuItem,
            this.openMetadataToolStripMenuItem});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(157, 48);
            // 
            // locateOnMapToolStripMenuItem
            // 
            this.locateOnMapToolStripMenuItem.Name = "locateOnMapToolStripMenuItem";
            this.locateOnMapToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.locateOnMapToolStripMenuItem.Text = "Locate on map";
            this.locateOnMapToolStripMenuItem.Click += new System.EventHandler(this.locateOnMapToolStripMenuItem_Click);
            // 
            // openMetadataToolStripMenuItem
            // 
            this.openMetadataToolStripMenuItem.Name = "openMetadataToolStripMenuItem";
            this.openMetadataToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.openMetadataToolStripMenuItem.Text = "Open metadata";
            this.openMetadataToolStripMenuItem.Click += new System.EventHandler(this.openMetadataToolStripMenuItem_Click);
            // 
            // gbOptions
            // 
            this.gbOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOptions.Controls.Add(this.lbCoordSys);
            this.gbOptions.Controls.Add(this.cbxCoordSys);
            this.gbOptions.Controls.Add(this.gbGMLSettings);
            this.gbOptions.Controls.Add(this.cbxVersion);
            this.gbOptions.Controls.Add(this.lbVersion);
            this.gbOptions.Controls.Add(this.tbParameters);
            this.gbOptions.Controls.Add(this.lbParameteres);
            this.gbOptions.Location = new System.Drawing.Point(13, 270);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(683, 117);
            this.gbOptions.TabIndex = 4;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // lbCoordSys
            // 
            this.lbCoordSys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCoordSys.AutoSize = true;
            this.lbCoordSys.Location = new System.Drawing.Point(354, 65);
            this.lbCoordSys.Name = "lbCoordSys";
            this.lbCoordSys.Size = new System.Drawing.Size(93, 13);
            this.lbCoordSys.TabIndex = 6;
            this.lbCoordSys.Text = "Coordinate system";
            // 
            // cbxCoordSys
            // 
            this.cbxCoordSys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxCoordSys.FormattingEnabled = true;
            this.cbxCoordSys.Location = new System.Drawing.Point(355, 81);
            this.cbxCoordSys.Name = "cbxCoordSys";
            this.cbxCoordSys.Size = new System.Drawing.Size(322, 21);
            this.cbxCoordSys.TabIndex = 5;
            // 
            // gbGMLSettings
            // 
            this.gbGMLSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbGMLSettings.Controls.Add(this.cbReverse);
            this.gbGMLSettings.Controls.Add(this.cbxOutputFormat);
            this.gbGMLSettings.Controls.Add(this.lbOutputFormat);
            this.gbGMLSettings.Location = new System.Drawing.Point(3, 50);
            this.gbGMLSettings.Name = "gbGMLSettings";
            this.gbGMLSettings.Size = new System.Drawing.Size(345, 61);
            this.gbGMLSettings.TabIndex = 4;
            this.gbGMLSettings.TabStop = false;
            this.gbGMLSettings.Text = "GML Settings";
            // 
            // cbReverse
            // 
            this.cbReverse.AutoSize = true;
            this.cbReverse.Location = new System.Drawing.Point(243, 26);
            this.cbReverse.Name = "cbReverse";
            this.cbReverse.Size = new System.Drawing.Size(88, 17);
            this.cbReverse.TabIndex = 2;
            this.cbReverse.Text = "Reverse X/Y";
            this.cbReverse.UseVisualStyleBackColor = true;
            // 
            // cbxOutputFormat
            // 
            this.cbxOutputFormat.FormattingEnabled = true;
            this.cbxOutputFormat.Location = new System.Drawing.Point(83, 24);
            this.cbxOutputFormat.Name = "cbxOutputFormat";
            this.cbxOutputFormat.Size = new System.Drawing.Size(147, 21);
            this.cbxOutputFormat.TabIndex = 1;
            // 
            // lbOutputFormat
            // 
            this.lbOutputFormat.AutoSize = true;
            this.lbOutputFormat.Location = new System.Drawing.Point(6, 27);
            this.lbOutputFormat.Name = "lbOutputFormat";
            this.lbOutputFormat.Size = new System.Drawing.Size(71, 13);
            this.lbOutputFormat.TabIndex = 0;
            this.lbOutputFormat.Text = "Output format";
            // 
            // cbxVersion
            // 
            this.cbxVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxVersion.FormattingEnabled = true;
            this.cbxVersion.Items.AddRange(new object[] {
            "1.0.0",
            "1.1.0",
            "2.0.0"});
            this.cbxVersion.Location = new System.Drawing.Point(589, 24);
            this.cbxVersion.Name = "cbxVersion";
            this.cbxVersion.Size = new System.Drawing.Size(88, 21);
            this.cbxVersion.TabIndex = 3;
            // 
            // lbVersion
            // 
            this.lbVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(541, 27);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(42, 13);
            this.lbVersion.TabIndex = 2;
            this.lbVersion.Text = "Version";
            // 
            // tbParameters
            // 
            this.tbParameters.Location = new System.Drawing.Point(72, 24);
            this.tbParameters.Name = "tbParameters";
            this.tbParameters.Size = new System.Drawing.Size(463, 20);
            this.tbParameters.TabIndex = 1;
            // 
            // lbParameteres
            // 
            this.lbParameteres.AutoSize = true;
            this.lbParameteres.Location = new System.Drawing.Point(6, 27);
            this.lbParameteres.Name = "lbParameteres";
            this.lbParameteres.Size = new System.Drawing.Size(60, 13);
            this.lbParameteres.TabIndex = 0;
            this.lbParameteres.Text = "Parameters";
            // 
            // gbFiltering
            // 
            this.gbFiltering.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFiltering.Controls.Add(this.lbXMax);
            this.gbFiltering.Controls.Add(this.lbYMin);
            this.gbFiltering.Controls.Add(this.lbYMax);
            this.gbFiltering.Controls.Add(this.lbXMin);
            this.gbFiltering.Controls.Add(this.tbYMax);
            this.gbFiltering.Controls.Add(this.tbXMax);
            this.gbFiltering.Controls.Add(this.tbXMin);
            this.gbFiltering.Controls.Add(this.tbYMin);
            this.gbFiltering.Controls.Add(this.cbClipByVisible);
            this.gbFiltering.Controls.Add(this.cbBoundBoxFilter);
            this.gbFiltering.Controls.Add(this.tbMaxFeatures);
            this.gbFiltering.Controls.Add(this.tbStartIndex);
            this.gbFiltering.Controls.Add(this.cbMaxFeatures);
            this.gbFiltering.Controls.Add(this.cbStartIndex);
            this.gbFiltering.Location = new System.Drawing.Point(18, 395);
            this.gbFiltering.Name = "gbFiltering";
            this.gbFiltering.Size = new System.Drawing.Size(678, 119);
            this.gbFiltering.TabIndex = 5;
            this.gbFiltering.TabStop = false;
            this.gbFiltering.Text = "Filtering";
            // 
            // lbXMax
            // 
            this.lbXMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbXMax.AutoSize = true;
            this.lbXMax.Location = new System.Drawing.Point(628, 58);
            this.lbXMax.Name = "lbXMax";
            this.lbXMax.Size = new System.Drawing.Size(34, 13);
            this.lbXMax.TabIndex = 13;
            this.lbXMax.Text = "XMax";
            // 
            // lbYMin
            // 
            this.lbYMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbYMin.AutoSize = true;
            this.lbYMin.Location = new System.Drawing.Point(532, 18);
            this.lbYMin.Name = "lbYMin";
            this.lbYMin.Size = new System.Drawing.Size(31, 13);
            this.lbYMin.TabIndex = 12;
            this.lbYMin.Text = "YMin";
            // 
            // lbYMax
            // 
            this.lbYMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbYMax.AutoSize = true;
            this.lbYMax.Location = new System.Drawing.Point(532, 100);
            this.lbYMax.Name = "lbYMax";
            this.lbYMax.Size = new System.Drawing.Size(34, 13);
            this.lbYMax.TabIndex = 11;
            this.lbYMax.Text = "YMax";
            // 
            // lbXMin
            // 
            this.lbXMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbXMin.AutoSize = true;
            this.lbXMin.Location = new System.Drawing.Point(433, 59);
            this.lbXMin.Name = "lbXMin";
            this.lbXMin.Size = new System.Drawing.Size(31, 13);
            this.lbXMin.TabIndex = 10;
            this.lbXMin.Text = "XMin";
            // 
            // tbYMax
            // 
            this.tbYMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbYMax.Enabled = false;
            this.tbYMax.Location = new System.Drawing.Point(520, 75);
            this.tbYMax.Name = "tbYMax";
            this.tbYMax.Size = new System.Drawing.Size(52, 20);
            this.tbYMax.TabIndex = 9;
            // 
            // tbXMax
            // 
            this.tbXMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbXMax.Enabled = false;
            this.tbXMax.Location = new System.Drawing.Point(571, 56);
            this.tbXMax.Name = "tbXMax";
            this.tbXMax.Size = new System.Drawing.Size(52, 20);
            this.tbXMax.TabIndex = 8;
            // 
            // tbXMin
            // 
            this.tbXMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbXMin.Enabled = false;
            this.tbXMin.Location = new System.Drawing.Point(469, 56);
            this.tbXMin.Name = "tbXMin";
            this.tbXMin.Size = new System.Drawing.Size(52, 20);
            this.tbXMin.TabIndex = 7;
            // 
            // tbYMin
            // 
            this.tbYMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbYMin.Enabled = false;
            this.tbYMin.Location = new System.Drawing.Point(520, 36);
            this.tbYMin.Name = "tbYMin";
            this.tbYMin.Size = new System.Drawing.Size(52, 20);
            this.tbYMin.TabIndex = 6;
            // 
            // cbClipByVisible
            // 
            this.cbClipByVisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbClipByVisible.AutoSize = true;
            this.cbClipByVisible.Enabled = false;
            this.cbClipByVisible.Location = new System.Drawing.Point(352, 28);
            this.cbClipByVisible.Name = "cbClipByVisible";
            this.cbClipByVisible.Size = new System.Drawing.Size(121, 17);
            this.cbClipByVisible.TabIndex = 5;
            this.cbClipByVisible.Text = "Clip by visible extent";
            this.cbClipByVisible.UseVisualStyleBackColor = true;
            this.cbClipByVisible.CheckedChanged += new System.EventHandler(this.cbClipByVisible_CheckedChanged);
            // 
            // cbBoundBoxFilter
            // 
            this.cbBoundBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbBoundBoxFilter.AutoSize = true;
            this.cbBoundBoxFilter.Location = new System.Drawing.Point(80, 74);
            this.cbBoundBoxFilter.Name = "cbBoundBoxFilter";
            this.cbBoundBoxFilter.Size = new System.Drawing.Size(117, 17);
            this.cbBoundBoxFilter.TabIndex = 4;
            this.cbBoundBoxFilter.Text = "Bounding-Box-Filter";
            this.cbBoundBoxFilter.UseVisualStyleBackColor = true;
            this.cbBoundBoxFilter.CheckedChanged += new System.EventHandler(this.cbBoundBoxFilter_CheckedChanged);
            // 
            // tbMaxFeatures
            // 
            this.tbMaxFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMaxFeatures.Enabled = false;
            this.tbMaxFeatures.Location = new System.Drawing.Point(202, 49);
            this.tbMaxFeatures.Name = "tbMaxFeatures";
            this.tbMaxFeatures.Size = new System.Drawing.Size(52, 20);
            this.tbMaxFeatures.TabIndex = 3;
            this.tbMaxFeatures.Text = "100";
            // 
            // tbStartIndex
            // 
            this.tbStartIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbStartIndex.Enabled = false;
            this.tbStartIndex.Location = new System.Drawing.Point(202, 26);
            this.tbStartIndex.Name = "tbStartIndex";
            this.tbStartIndex.Size = new System.Drawing.Size(52, 20);
            this.tbStartIndex.TabIndex = 2;
            this.tbStartIndex.Text = "1";
            // 
            // cbMaxFeatures
            // 
            this.cbMaxFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMaxFeatures.AutoSize = true;
            this.cbMaxFeatures.Location = new System.Drawing.Point(80, 51);
            this.cbMaxFeatures.Name = "cbMaxFeatures";
            this.cbMaxFeatures.Size = new System.Drawing.Size(111, 17);
            this.cbMaxFeatures.TabIndex = 1;
            this.cbMaxFeatures.Text = "Maximum features";
            this.cbMaxFeatures.UseVisualStyleBackColor = true;
            this.cbMaxFeatures.CheckedChanged += new System.EventHandler(this.cbMaxFeatures_CheckedChanged);
            // 
            // cbStartIndex
            // 
            this.cbStartIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbStartIndex.AutoSize = true;
            this.cbStartIndex.Location = new System.Drawing.Point(80, 28);
            this.cbStartIndex.Name = "cbStartIndex";
            this.cbStartIndex.Size = new System.Drawing.Size(76, 17);
            this.cbStartIndex.TabIndex = 0;
            this.cbStartIndex.Text = "Start index";
            this.cbStartIndex.UseVisualStyleBackColor = true;
            this.cbStartIndex.CheckedChanged += new System.EventHandler(this.cbStartIndex_CheckedChanged);
            // 
            // btnOpenURL
            // 
            this.btnOpenURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenURL.Location = new System.Drawing.Point(18, 515);
            this.btnOpenURL.Name = "btnOpenURL";
            this.btnOpenURL.Size = new System.Drawing.Size(75, 23);
            this.btnOpenURL.TabIndex = 14;
            this.btnOpenURL.Text = "Open URL";
            this.btnOpenURL.UseVisualStyleBackColor = true;
            this.btnOpenURL.Click += new System.EventHandler(this.btnOpenURL_Click);
            // 
            // btnAddLayer
            // 
            this.btnAddLayer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddLayer.Location = new System.Drawing.Point(286, 518);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new System.Drawing.Size(75, 23);
            this.btnAddLayer.TabIndex = 15;
            this.btnAddLayer.Text = "Add layer";
            this.btnAddLayer.UseVisualStyleBackColor = true;
            this.btnAddLayer.Click += new System.EventHandler(this.btnAddLayer_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.Location = new System.Drawing.Point(368, 518);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbxURL
            // 
            this.cbxURL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxURL.FormattingEnabled = true;
            this.cbxURL.Items.AddRange(new object[] {
            "http://geodata.nationaalgeoregister.nl/aan/wfs?version=1.0.0&request=GetCapabilities"});
            this.cbxURL.Location = new System.Drawing.Point(50, 6);
            this.cbxURL.Name = "cbxURL";
            this.cbxURL.Size = new System.Drawing.Size(565, 21);
            this.cbxURL.TabIndex = 17;
            this.cbxURL.Text = "http://geodata.nationaalgeoregister.nl/aan/wfs?version=1.0.0&request=GetCapabilities";
            // 
            // WFSForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(708, 550);
            this.Controls.Add(this.cbxURL);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddLayer);
            this.Controls.Add(this.btnOpenURL);
            this.Controls.Add(this.gbFiltering);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.gbLayers);
            this.Controls.Add(this.btnGetLayers);
            this.Controls.Add(this.lURL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WFSForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - WFSManager";
            this.Load += new System.EventHandler(this.WFSForm_Load);
            this.gbLayers.ResumeLayout(false);
            this.cms.ResumeLayout(false);
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.gbGMLSettings.ResumeLayout(false);
            this.gbGMLSettings.PerformLayout();
            this.gbFiltering.ResumeLayout(false);
            this.gbFiltering.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Run()
        {
            Application.Run(new WFSForm());
        }

        private void WFSForm_Load(object sender, System.EventArgs e)
        {
            if (wfs == null)
            {
                wfs = new TGIS_FileWFS(null, null);
                wfs.TimeOut = 5000;
            }

            NodeData = new Object[8];
        }

        private void btnGetLayers_Click(object sender, EventArgs e)
        {
            String str;
            int i;
            TreeNode root;
            TreeNode node;
            TGIS_WFSFeature fea;

            str = cbxURL.Text;

            if (str == "") return;

            tvLayers.Nodes.Clear();
            rbLayerInfo.Clear();
            wfs.Load(str);

            if (!String.IsNullOrEmpty(wfs.Error)) return;

            if (wfs.FeaturesCount == 0) return;

            tvLayers.BeginUpdate();

            root = tvLayers.Nodes.Add(wfs.Path);
            tvLayers.SelectedNode = root;
            for (i = 0; i <= wfs.FeaturesCount - 1; i++)
            {
                fea = wfs.get_Feature(i);
                node = tvLayers.SelectedNode.Nodes.Add(fea.Name);
                NodeData[i] = wfs.get_Feature(i);
            }
            root.Expand();
            tvLayers.EndUpdate();

            cbxOutputFormat.BeginUpdate();
            cbxOutputFormat.Items.Clear();
            for (i = 0; i < wfs.DataFormats.Count; i++)
                cbxOutputFormat.Items.Add(wfs.DataFormats[i]);
            cbxOutputFormat.EndUpdate();

            cbxCoordSys.BeginUpdate();
            cbxCoordSys.Items.Clear();
            cbxCoordSys.EndUpdate();

            tbXMax.Text = "";
            tbXMin.Text = "";
            tbYMax.Text = "";
            tbYMin.Text = "";
        }

        private void tvLayers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TGIS_WFSFeature fea;
            TGIS_Extent ext;
            int i;

            if ((e.Node != null) && (e.Node.Level > 0))
            {
                fea = (TGIS_WFSFeature)NodeData[e.Node.Index];
                rbLayerInfo.Clear();
                rbLayerInfo.AppendText("Name : " + fea.Name);
                rbLayerInfo.AppendText("\nTitle : " + fea.Title);
                if (fea.Description != "")
                    rbLayerInfo.AppendText("\nDescription : " + fea.Description);
                else
                    rbLayerInfo.AppendText("\nKeywords : " + fea.Keywords);
                rbLayerInfo.AppendText("\nDefault SRS : " + fea.DefaultSRS);
                rbLayerInfo.AppendText("\nWGS84 Bounding Box : ");
                rbLayerInfo.AppendText(String.Format("\n{0:f2}, {1:f2}, {2:f2}, {3:f2}", fea.WGS84BBox.XMin, fea.WGS84BBox.YMin, fea.WGS84BBox.XMax, fea.WGS84BBox.YMax));
                rbLayerInfo.Update();

                ext = getBBoxExtent(fea);

                if (!(TGIS_Utils.GisIsNoWorld(ext)))
                {
                    tbXMax.Text = ext.XMin.ToString();
                    tbYMax.Text = ext.YMax.ToString();
                    tbXMin.Text = ext.XMax.ToString();
                    tbYMin.Text = ext.YMax.ToString();
                }
                else
                {
                    tbXMax.Text = "";
                    tbYMax.Text = "";
                    tbXMin.Text = "";
                    tbYMin.Text = "";
                }

                cbxCoordSys.Text = "";
                cbxCoordSys.BeginUpdate();
                cbxCoordSys.Items.Clear();
                cbxCoordSys.Items.Add(fea.DefaultSRS);
                for (i = 0; i <= fea.OtherSRS.Count - 1; i++)
                    cbxCoordSys.Items.Add(fea.OtherSRS[i]);
                cbxCoordSys.EndUpdate();
            }
            else
            {
                rbLayerInfo.Clear();
                for (i = 0; i <= wfs.ServiceInfo.Count - 1; i++)
                    rbLayerInfo.AppendText("\n" + wfs.ServiceInfo[i]);
                rbLayerInfo.Update();
            }

        }

        private TGIS_WFSFeature getSelectedFeature()
        {
            if (((tvLayers.SelectedNode != null)) && (tvLayers.SelectedNode.Level > 0))
                return (TGIS_WFSFeature)NodeData[tvLayers.SelectedNode.Index];
            else
                return null;
        }

        private TGIS_Extent getBBoxExtent(TGIS_WFSFeature _fea)
        {
            TGIS_CSCoordinateSystem lcs, wgs;
            TGIS_Extent final;

            final = TGIS_Utils.GisNoWorld();

            if (_fea == null) return final;


            try
            {
                if (cbxCoordSys.SelectedIndex > -1)
                    lcs = TGIS_CSFactory.ByWKT(cbxCoordSys.Text);
                else
                    lcs = TGIS_CSFactory.ByWKT(_fea.DefaultSRS);
            }
            catch
            {
                lcs = TGIS_Utils.CSUnknownCoordinateSystem;
            }

            if (!cbClipByVisible.Checked)
            {
                wgs = TGIS_CSFactory.ByEPSG(GIS_EPSG_WGS84);

                if (!(lcs is TGIS_CSUnknownCoordinateSystem))
                {
                    final = lcs.ExtentFromCS(wgs, _fea.WGS84BBox);
                    if (lcs.Error != 0)
                        final = TGIS_Utils.GisNoWorld();
                }
            }
            else
            {
                wgs = GIS.CS;

                if (!(lcs is TGIS_CSUnknownCoordinateSystem))
                    final = lcs.ExtentFromCS(wgs, GIS.UnrotatedExtent(GIS.VisibleExtent));
                if (lcs.Error != 0)
                    final = TGIS_Utils.GisNoWorld();
            }
            return final;
        }

        private void cbStartIndex_CheckedChanged(object sender, EventArgs e)
        {
            tbStartIndex.Enabled = cbStartIndex.Checked;
        }

        private void cbMaxFeatures_CheckedChanged(object sender, EventArgs e)
        {
            tbMaxFeatures.Enabled = cbMaxFeatures.Checked;
        }

        private void cbBoundBoxFilter_CheckedChanged(object sender, EventArgs e)
        {
            tbXMax.Enabled = cbBoundBoxFilter.Checked;
            tbYMax.Enabled = cbBoundBoxFilter.Checked;
            tbXMin.Enabled = cbBoundBoxFilter.Checked;
            tbYMin.Enabled = cbBoundBoxFilter.Checked;

            cbClipByVisible.Enabled = cbBoundBoxFilter.Checked;
            cbClipByVisible_CheckedChanged(sender, e);
        }

        private void cbClipByVisible_CheckedChanged(object sender, EventArgs e)
        {
            TGIS_Extent ext;

            if (GIS.IsEmpty) return;

            ext = getBBoxExtent(getSelectedFeature());

            if (!(TGIS_Utils.GisIsNoWorld(ext)))
            {
                tbXMax.Text = ext.XMin.ToString();
                tbYMax.Text = ext.YMax.ToString();
                tbXMin.Text = ext.XMax.ToString();
                tbYMin.Text = ext.YMax.ToString();
            }
            else
            {
                tbXMax.Text = "";
                tbYMax.Text = "";
                tbXMin.Text = "";
                tbYMin.Text = "";
            }
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            TGIS_WFSFeature fea;
            String wfsPath;

            btnAddLayer.Text = "Wait...";
            btnAddLayer.Enabled = false;

            wfsPath = "&SERVICE=WFS";

            if (tbParameters.Text != "")
                wfsPath = wfsPath + "&" + tbParameters.Text;
            if (cbxVersion.Text != "")
                wfsPath = wfsPath + "&VERSION=" + cbxVersion.Text;

            fea = getSelectedFeature();

            if (fea != null)
                wfsPath = wfsPath + "&TYPENAME=" + fea.Name;
            if (cbxCoordSys.Text != "")
                wfsPath = wfsPath + "&SRSNAME=" + cbxCoordSys.Text;
            if (cbxOutputFormat.Text != "")
                wfsPath = wfsPath + "&OUTPUTFORMAT=" + cbxOutputFormat.Text;
            if (cbMaxFeatures.Checked)
                wfsPath = wfsPath + "&MAXFEATURES=" + tbMaxFeatures.Text;
            if (cbStartIndex.Checked)
                wfsPath = wfsPath + "&STARTINDEX=" + tbStartIndex.Text;
            if (cbBoundBoxFilter.Checked)
            {
                if (cbReverse.Checked)
                {
                    wfsPath = wfsPath + "&BBOX=" + tbYMin.Text + ", " + tbXMin.Text + ", " +
                                                   tbYMax.Text + ", " + tbXMax.Text;
                }
                else
                {
                    wfsPath = wfsPath + "&BBOX=" + tbXMin.Text + ", " + tbYMin.Text + ", " +
                                                   tbXMax.Text + ", " + tbYMax.Text;
                }
            }
            if (cbReverse.Checked)
                wfsPath = wfsPath + "&AxisOrder=" + GIS_INI_AXIS_ORDER_NE;

            mainForm.AppendCovarage(wfs.MakeUrl(wfsPath, cbxURL.Text));
            btnAddLayer.Enabled = true;
            btnAddLayer.Text = "Add layer";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOpenURL_Click(object sender, EventArgs e)
        {
            TGIS_LayerWFS lwfs;

            lwfs = new TGIS_LayerWFS();
            lwfs.Path = cbxURL.Text;
            GIS.Add(lwfs);
            GIS.FullExtent();
        }

        private void locateOnMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TGIS_WFSFeature fea;

            fea = getSelectedFeature();
            if (fea != null)
                GIS.VisibleExtent = fea.WGS84BBox;
        }

        private void openMetadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TGIS_WFSFeature fea;

            fea = getSelectedFeature();
            if (fea != null)
                Process.Start(fea.MetadataUrl, "open");
        }
    }
}
