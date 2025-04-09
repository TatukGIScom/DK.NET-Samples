using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Classification
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Panel pClassification;
        private ComboBox cbRenderBy;
        private ComboBox cbMethod;
        private ComboBox cbField;
        private Label lblRenderBy;
        private Label lblMethod;
        private Label lblField;
        private Button btnOpen;
        private Panel panel1;
        private ComboBox cbColorRamp;
        private CheckBox chkColorRamp;
        private CheckBox chkShowInLegend;
        private TextBox tbStartSize;
        private TextBox tbClassIdField;
        private Label lblClassIdField;
        private Label lblEndSize;
        private Label lblStartSize;
        private Panel pEndColor;
        private Label lblEndColor;
        private Panel pStartColor;
        private Label lblStartColor;
        private TextBox tbEndSize;
        private TGIS_ControlLegend GISLegend;
        private TGIS_ViewerWnd GIS;

        const String RENDER_TYPE_SIZE = "Size / Width";
        const String RENDER_TYPE_COLOR = "Color";
        const String RENDER_TYPE_OUTLINE_WIDTH = "Outline width";
        const String RENDER_TYPE_OUTLINE_COLOR = "Outline color";

        const String STD_INTERVAL_ONE = "1 STDEV";
        const String STD_INTERVAL_ONE_HALF = "1/2 STDEV";
        const String STD_INTERVAL_ONE_THIRD = "1/3 STDEV";
        const String STD_INTERVAL_ONE_QUARTER = "1/4 STDEV";

        const String GIS_CLASSIFY_METHOD_DI = "Defined Interval";
        const String GIS_CLASSIFY_METHOD_EI = "Equal Interval";
        const String GIS_CLASSIFY_METHOD_GI = "Geometrical Interval";
        const String GIS_CLASSIFY_METHOD_MN = "Manual";
        const String GIS_CLASSIFY_METHOD_NB = "Natural Breaks";
        const String GIS_CLASSIFY_METHOD_KM = "K-Means";
        const String GIS_CLASSIFY_METHOD_KMS = "K-Means Spatial";
        const String GIS_CLASSIFY_METHOD_QN = "Quantile";
        const String GIS_CLASSIFY_METHOD_QR = "Quartile";
        const String GIS_CLASSIFY_METHOD_SD = "Standard Deviation";
        const String GIS_CLASSIFY_METHOD_SDC = "Standard Deviation with Central";
        const String GIS_CLASSIFY_METHOD_UNQ = "Unique";

        private ColorDialog dlgColor;
        private OpenFileDialog dlgOpen;
        private CheckBox chkForceStatisticsCalculation;
        private Panel pnlManual;
        private Button btnAddManualBreak;
        private TextBox edtManualBreaks;
        private Label lblManual;
        private Panel pnlInterval;
        private TextBox tbInterval;
        private ComboBox cbInterval;
        private Label lblInterval;
        private CheckBox chkColorRampName;
        private Panel pnlClasses;
        private ComboBox cbClasses;
        private Label lblClasses;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.pClassification = new System.Windows.Forms.Panel();
            this.pnlClasses = new System.Windows.Forms.Panel();
            this.cbClasses = new System.Windows.Forms.ComboBox();
            this.lblClasses = new System.Windows.Forms.Label();
            this.pnlInterval = new System.Windows.Forms.Panel();
            this.tbInterval = new System.Windows.Forms.TextBox();
            this.cbInterval = new System.Windows.Forms.ComboBox();
            this.lblInterval = new System.Windows.Forms.Label();
            this.pnlManual = new System.Windows.Forms.Panel();
            this.btnAddManualBreak = new System.Windows.Forms.Button();
            this.edtManualBreaks = new System.Windows.Forms.TextBox();
            this.lblManual = new System.Windows.Forms.Label();
            this.chkForceStatisticsCalculation = new System.Windows.Forms.CheckBox();
            this.cbRenderBy = new System.Windows.Forms.ComboBox();
            this.cbMethod = new System.Windows.Forms.ComboBox();
            this.cbField = new System.Windows.Forms.ComboBox();
            this.lblRenderBy = new System.Windows.Forms.Label();
            this.lblMethod = new System.Windows.Forms.Label();
            this.lblField = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkColorRampName = new System.Windows.Forms.CheckBox();
            this.tbEndSize = new System.Windows.Forms.TextBox();
            this.cbColorRamp = new System.Windows.Forms.ComboBox();
            this.chkColorRamp = new System.Windows.Forms.CheckBox();
            this.chkShowInLegend = new System.Windows.Forms.CheckBox();
            this.tbStartSize = new System.Windows.Forms.TextBox();
            this.tbClassIdField = new System.Windows.Forms.TextBox();
            this.lblClassIdField = new System.Windows.Forms.Label();
            this.lblEndSize = new System.Windows.Forms.Label();
            this.lblStartSize = new System.Windows.Forms.Label();
            this.pEndColor = new System.Windows.Forms.Panel();
            this.lblEndColor = new System.Windows.Forms.Label();
            this.pStartColor = new System.Windows.Forms.Panel();
            this.lblStartColor = new System.Windows.Forms.Label();
            this.GISLegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.pClassification.SuspendLayout();
            this.pnlClasses.SuspendLayout();
            this.pnlInterval.SuspendLayout();
            this.pnlManual.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pClassification
            // 
            this.pClassification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pClassification.Controls.Add(this.pnlClasses);
            this.pClassification.Controls.Add(this.pnlInterval);
            this.pClassification.Controls.Add(this.pnlManual);
            this.pClassification.Controls.Add(this.chkForceStatisticsCalculation);
            this.pClassification.Controls.Add(this.cbRenderBy);
            this.pClassification.Controls.Add(this.cbMethod);
            this.pClassification.Controls.Add(this.cbField);
            this.pClassification.Controls.Add(this.lblRenderBy);
            this.pClassification.Controls.Add(this.lblMethod);
            this.pClassification.Controls.Add(this.lblField);
            this.pClassification.Controls.Add(this.btnOpen);
            this.pClassification.Location = new System.Drawing.Point(9, 9);
            this.pClassification.Margin = new System.Windows.Forms.Padding(2);
            this.pClassification.Name = "pClassification";
            this.pClassification.Size = new System.Drawing.Size(1364, 34);
            this.pClassification.TabIndex = 0;
            // 
            // pnlClasses
            // 
            this.pnlClasses.Controls.Add(this.cbClasses);
            this.pnlClasses.Controls.Add(this.lblClasses);
            this.pnlClasses.Location = new System.Drawing.Point(745, 0);
            this.pnlClasses.Margin = new System.Windows.Forms.Padding(2);
            this.pnlClasses.Name = "pnlClasses";
            this.pnlClasses.Size = new System.Drawing.Size(200, 34);
            this.pnlClasses.TabIndex = 16;
            this.pnlClasses.Visible = false;
            // 
            // cbClasses
            // 
            this.cbClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbClasses.FormattingEnabled = true;
            this.cbClasses.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.cbClasses.Location = new System.Drawing.Point(57, 7);
            this.cbClasses.Margin = new System.Windows.Forms.Padding(2);
            this.cbClasses.Name = "cbClasses";
            this.cbClasses.Size = new System.Drawing.Size(50, 21);
            this.cbClasses.TabIndex = 11;
            // 
            // lblClasses
            // 
            this.lblClasses.AutoSize = true;
            this.lblClasses.Location = new System.Drawing.Point(7, 10);
            this.lblClasses.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClasses.Name = "lblClasses";
            this.lblClasses.Size = new System.Drawing.Size(46, 13);
            this.lblClasses.TabIndex = 10;
            this.lblClasses.Text = "Classes:";
            // 
            // pnlInterval
            // 
            this.pnlInterval.Controls.Add(this.tbInterval);
            this.pnlInterval.Controls.Add(this.cbInterval);
            this.pnlInterval.Controls.Add(this.lblInterval);
            this.pnlInterval.Location = new System.Drawing.Point(949, 0);
            this.pnlInterval.Margin = new System.Windows.Forms.Padding(2);
            this.pnlInterval.Name = "pnlInterval";
            this.pnlInterval.Size = new System.Drawing.Size(200, 34);
            this.pnlInterval.TabIndex = 14;
            // 
            // tbInterval
            // 
            this.tbInterval.Location = new System.Drawing.Point(58, 7);
            this.tbInterval.Margin = new System.Windows.Forms.Padding(2);
            this.tbInterval.Name = "tbInterval";
            this.tbInterval.Size = new System.Drawing.Size(114, 20);
            this.tbInterval.TabIndex = 14;
            this.tbInterval.Text = "100";
            this.tbInterval.Visible = false;
            this.tbInterval.TextChanged += new System.EventHandler(this.tbInterval_TextChanged);
            // 
            // cbInterval
            // 
            this.cbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInterval.FormattingEnabled = true;
            this.cbInterval.Items.AddRange(new object[] {
            "1 STDEV",
            "1/2 STDEV",
            "1/3 STDEV",
            "1/4 STDEV"});
            this.cbInterval.Location = new System.Drawing.Point(58, 7);
            this.cbInterval.Margin = new System.Windows.Forms.Padding(2);
            this.cbInterval.Name = "cbInterval";
            this.cbInterval.Size = new System.Drawing.Size(140, 21);
            this.cbInterval.TabIndex = 13;
            this.cbInterval.SelectedIndexChanged += new System.EventHandler(this.cbInterval_SelectedIndexChanged_1);
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(9, 10);
            this.lblInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(45, 13);
            this.lblInterval.TabIndex = 12;
            this.lblInterval.Text = "Interval:";
            // 
            // pnlManual
            // 
            this.pnlManual.Controls.Add(this.btnAddManualBreak);
            this.pnlManual.Controls.Add(this.edtManualBreaks);
            this.pnlManual.Controls.Add(this.lblManual);
            this.pnlManual.Location = new System.Drawing.Point(1153, 0);
            this.pnlManual.Margin = new System.Windows.Forms.Padding(2);
            this.pnlManual.Name = "pnlManual";
            this.pnlManual.Size = new System.Drawing.Size(200, 34);
            this.pnlManual.TabIndex = 13;
            this.pnlManual.Visible = false;
            // 
            // btnAddManualBreak
            // 
            this.btnAddManualBreak.Location = new System.Drawing.Point(160, 6);
            this.btnAddManualBreak.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddManualBreak.Name = "btnAddManualBreak";
            this.btnAddManualBreak.Size = new System.Drawing.Size(40, 22);
            this.btnAddManualBreak.TabIndex = 13;
            this.btnAddManualBreak.Text = "Add";
            this.btnAddManualBreak.UseVisualStyleBackColor = true;
            this.btnAddManualBreak.Click += new System.EventHandler(this.btnAddManualBreak_Click);
            // 
            // edtManualBreaks
            // 
            this.edtManualBreaks.Location = new System.Drawing.Point(51, 7);
            this.edtManualBreaks.Margin = new System.Windows.Forms.Padding(2);
            this.edtManualBreaks.Name = "edtManualBreaks";
            this.edtManualBreaks.Size = new System.Drawing.Size(102, 20);
            this.edtManualBreaks.TabIndex = 12;
            this.edtManualBreaks.Text = "0,10.5,20,50";
            // 
            // lblManual
            // 
            this.lblManual.AutoSize = true;
            this.lblManual.Location = new System.Drawing.Point(2, 10);
            this.lblManual.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblManual.Name = "lblManual";
            this.lblManual.Size = new System.Drawing.Size(45, 13);
            this.lblManual.TabIndex = 6;
            this.lblManual.Text = "Manual:";
            // 
            // chkForceStatisticsCalculation
            // 
            this.chkForceStatisticsCalculation.AutoSize = true;
            this.chkForceStatisticsCalculation.Checked = true;
            this.chkForceStatisticsCalculation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkForceStatisticsCalculation.Location = new System.Drawing.Point(436, 9);
            this.chkForceStatisticsCalculation.Margin = new System.Windows.Forms.Padding(2);
            this.chkForceStatisticsCalculation.Name = "chkForceStatisticsCalculation";
            this.chkForceStatisticsCalculation.Size = new System.Drawing.Size(153, 17);
            this.chkForceStatisticsCalculation.TabIndex = 12;
            this.chkForceStatisticsCalculation.Text = "Force Statistics Calculation";
            this.chkForceStatisticsCalculation.UseVisualStyleBackColor = true;
            // 
            // cbRenderBy
            // 
            this.cbRenderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRenderBy.FormattingEnabled = true;
            this.cbRenderBy.Items.AddRange(new object[] {
            "Size / Width",
            "Color",
            "Outline width",
            "Outline color"});
            this.cbRenderBy.Location = new System.Drawing.Point(656, 7);
            this.cbRenderBy.Margin = new System.Windows.Forms.Padding(2);
            this.cbRenderBy.Name = "cbRenderBy";
            this.cbRenderBy.Size = new System.Drawing.Size(86, 21);
            this.cbRenderBy.TabIndex = 8;
            this.cbRenderBy.SelectedIndexChanged += new System.EventHandler(this.cbRenderBy_SelectedIndexChanged);
            // 
            // cbMethod
            // 
            this.cbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMethod.FormattingEnabled = true;
            this.cbMethod.Items.AddRange(new object[] {
            "Select ...",
            "Defined Interval",
            "Equal Interval",
            "Geometrical Interval",
            "Manual",
            "Natural Breaks",
            "K-Means",
            "K-Means Spatial",
            "Quantile",
            "Quartile",
            "Standard Deviation",
            "Standard Deviation with Central",
            "Unique"});
            this.cbMethod.Location = new System.Drawing.Point(304, 7);
            this.cbMethod.Margin = new System.Windows.Forms.Padding(2);
            this.cbMethod.Name = "cbMethod";
            this.cbMethod.Size = new System.Drawing.Size(122, 21);
            this.cbMethod.TabIndex = 7;
            this.cbMethod.SelectedIndexChanged += new System.EventHandler(this.cbMethod_SelectedIndexChanged);
            // 
            // cbField
            // 
            this.cbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbField.FormattingEnabled = true;
            this.cbField.Location = new System.Drawing.Point(118, 7);
            this.cbField.Margin = new System.Windows.Forms.Padding(2);
            this.cbField.Name = "cbField";
            this.cbField.Size = new System.Drawing.Size(136, 21);
            this.cbField.TabIndex = 6;
            this.cbField.SelectedIndexChanged += new System.EventHandler(this.cbField_SelectedIndexChanged);
            // 
            // lblRenderBy
            // 
            this.lblRenderBy.AutoSize = true;
            this.lblRenderBy.Location = new System.Drawing.Point(593, 10);
            this.lblRenderBy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRenderBy.Name = "lblRenderBy";
            this.lblRenderBy.Size = new System.Drawing.Size(59, 13);
            this.lblRenderBy.TabIndex = 3;
            this.lblRenderBy.Text = "Render by:";
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(259, 10);
            this.lblMethod.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(49, 13);
            this.lblMethod.TabIndex = 2;
            this.lblMethod.Text = "Method: ";
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(82, 10);
            this.lblField.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(32, 13);
            this.lblField.TabIndex = 1;
            this.lblField.Text = "Field:";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(2, 6);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(74, 22);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.chkColorRampName);
            this.panel1.Controls.Add(this.tbEndSize);
            this.panel1.Controls.Add(this.cbColorRamp);
            this.panel1.Controls.Add(this.chkColorRamp);
            this.panel1.Controls.Add(this.chkShowInLegend);
            this.panel1.Controls.Add(this.tbStartSize);
            this.panel1.Controls.Add(this.tbClassIdField);
            this.panel1.Controls.Add(this.lblClassIdField);
            this.panel1.Controls.Add(this.lblEndSize);
            this.panel1.Controls.Add(this.lblStartSize);
            this.panel1.Controls.Add(this.pEndColor);
            this.panel1.Controls.Add(this.lblEndColor);
            this.panel1.Controls.Add(this.pStartColor);
            this.panel1.Controls.Add(this.lblStartColor);
            this.panel1.Location = new System.Drawing.Point(9, 47);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1364, 34);
            this.panel1.TabIndex = 1;
            // 
            // chkColorRampName
            // 
            this.chkColorRampName.AutoSize = true;
            this.chkColorRampName.Checked = true;
            this.chkColorRampName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkColorRampName.Location = new System.Drawing.Point(701, 10);
            this.chkColorRampName.Margin = new System.Windows.Forms.Padding(2);
            this.chkColorRampName.Name = "chkColorRampName";
            this.chkColorRampName.Size = new System.Drawing.Size(128, 17);
            this.chkColorRampName.TabIndex = 15;
            this.chkColorRampName.Text = "Use ColorRampName";
            this.chkColorRampName.UseVisualStyleBackColor = true;
            this.chkColorRampName.CheckedChanged += new System.EventHandler(this.chkColorRampName_CheckedChanged);
            // 
            // tbEndSize
            // 
            this.tbEndSize.Location = new System.Drawing.Point(355, 8);
            this.tbEndSize.Margin = new System.Windows.Forms.Padding(2);
            this.tbEndSize.Name = "tbEndSize";
            this.tbEndSize.Size = new System.Drawing.Size(48, 20);
            this.tbEndSize.TabIndex = 14;
            this.tbEndSize.Text = "100";
            this.tbEndSize.TextChanged += new System.EventHandler(this.validateEdit);
            // 
            // cbColorRamp
            // 
            this.cbColorRamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColorRamp.FormattingEnabled = true;
            this.cbColorRamp.Location = new System.Drawing.Point(934, 8);
            this.cbColorRamp.Margin = new System.Windows.Forms.Padding(2);
            this.cbColorRamp.Name = "cbColorRamp";
            this.cbColorRamp.Size = new System.Drawing.Size(215, 21);
            this.cbColorRamp.TabIndex = 13;
            this.cbColorRamp.SelectedIndexChanged += new System.EventHandler(this.cbColorRamp_SelectedIndexChanged);
            // 
            // chkColorRamp
            // 
            this.chkColorRamp.AutoSize = true;
            this.chkColorRamp.Checked = true;
            this.chkColorRamp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkColorRamp.Location = new System.Drawing.Point(833, 10);
            this.chkColorRamp.Margin = new System.Windows.Forms.Padding(2);
            this.chkColorRamp.Name = "chkColorRamp";
            this.chkColorRamp.Size = new System.Drawing.Size(100, 17);
            this.chkColorRamp.TabIndex = 12;
            this.chkColorRamp.Text = "Use ColorRamp";
            this.chkColorRamp.UseVisualStyleBackColor = true;
            this.chkColorRamp.CheckedChanged += new System.EventHandler(this.chkUseColorRamp_CheckedChanged);
            // 
            // chkShowInLegend
            // 
            this.chkShowInLegend.AutoSize = true;
            this.chkShowInLegend.Checked = true;
            this.chkShowInLegend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowInLegend.Location = new System.Drawing.Point(595, 10);
            this.chkShowInLegend.Margin = new System.Windows.Forms.Padding(2);
            this.chkShowInLegend.Name = "chkShowInLegend";
            this.chkShowInLegend.Size = new System.Drawing.Size(99, 17);
            this.chkShowInLegend.TabIndex = 11;
            this.chkShowInLegend.Text = "Show in legend";
            this.chkShowInLegend.UseVisualStyleBackColor = true;
            this.chkShowInLegend.CheckedChanged += new System.EventHandler(this.chkShowInLegend_CheckedChanged);
            // 
            // tbStartSize
            // 
            this.tbStartSize.Location = new System.Drawing.Point(246, 8);
            this.tbStartSize.Margin = new System.Windows.Forms.Padding(2);
            this.tbStartSize.Name = "tbStartSize";
            this.tbStartSize.Size = new System.Drawing.Size(48, 20);
            this.tbStartSize.TabIndex = 9;
            this.tbStartSize.Text = "1";
            this.tbStartSize.TextChanged += new System.EventHandler(this.validateEdit);
            // 
            // tbClassIdField
            // 
            this.tbClassIdField.Location = new System.Drawing.Point(486, 8);
            this.tbClassIdField.Margin = new System.Windows.Forms.Padding(2);
            this.tbClassIdField.Name = "tbClassIdField";
            this.tbClassIdField.Size = new System.Drawing.Size(100, 20);
            this.tbClassIdField.TabIndex = 8;
            this.tbClassIdField.TextChanged += new System.EventHandler(this.validateEdit);
            // 
            // lblClassIdField
            // 
            this.lblClassIdField.AutoSize = true;
            this.lblClassIdField.Location = new System.Drawing.Point(411, 11);
            this.lblClassIdField.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblClassIdField.Name = "lblClassIdField";
            this.lblClassIdField.Size = new System.Drawing.Size(71, 13);
            this.lblClassIdField.TabIndex = 5;
            this.lblClassIdField.Text = "Class ID field:";
            // 
            // lblEndSize
            // 
            this.lblEndSize.AutoSize = true;
            this.lblEndSize.Location = new System.Drawing.Point(301, 11);
            this.lblEndSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEndSize.Name = "lblEndSize";
            this.lblEndSize.Size = new System.Drawing.Size(50, 13);
            this.lblEndSize.TabIndex = 4;
            this.lblEndSize.Text = "End size:";
            // 
            // lblStartSize
            // 
            this.lblStartSize.AutoSize = true;
            this.lblStartSize.Location = new System.Drawing.Point(186, 11);
            this.lblStartSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStartSize.Name = "lblStartSize";
            this.lblStartSize.Size = new System.Drawing.Size(53, 13);
            this.lblStartSize.TabIndex = 3;
            this.lblStartSize.Text = "Start size:";
            // 
            // pEndColor
            // 
            this.pEndColor.BackColor = System.Drawing.Color.Green;
            this.pEndColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pEndColor.Location = new System.Drawing.Point(158, 8);
            this.pEndColor.Margin = new System.Windows.Forms.Padding(2);
            this.pEndColor.Name = "pEndColor";
            this.pEndColor.Size = new System.Drawing.Size(24, 20);
            this.pEndColor.TabIndex = 2;
            this.pEndColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pEndColor_MouseClick);
            // 
            // lblEndColor
            // 
            this.lblEndColor.AutoSize = true;
            this.lblEndColor.Location = new System.Drawing.Point(97, 11);
            this.lblEndColor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEndColor.Name = "lblEndColor";
            this.lblEndColor.Size = new System.Drawing.Size(55, 13);
            this.lblEndColor.TabIndex = 2;
            this.lblEndColor.Text = "End color:";
            // 
            // pStartColor
            // 
            this.pStartColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(248)))), ((int)(((byte)(237)))));
            this.pStartColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pStartColor.Location = new System.Drawing.Point(69, 8);
            this.pStartColor.Margin = new System.Windows.Forms.Padding(2);
            this.pStartColor.Name = "pStartColor";
            this.pStartColor.Size = new System.Drawing.Size(24, 20);
            this.pStartColor.TabIndex = 1;
            this.pStartColor.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pStartColor_MouseClick);
            // 
            // lblStartColor
            // 
            this.lblStartColor.AutoSize = true;
            this.lblStartColor.Location = new System.Drawing.Point(7, 11);
            this.lblStartColor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStartColor.Name = "lblStartColor";
            this.lblStartColor.Size = new System.Drawing.Size(58, 13);
            this.lblStartColor.TabIndex = 0;
            this.lblStartColor.Text = "Start color:";
            // 
            // GISLegend
            // 
            this.GISLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GISLegend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GISLegend.GIS_Viewer = this.GIS;
            this.GISLegend.Location = new System.Drawing.Point(9, 86);
            this.GISLegend.Margin = new System.Windows.Forms.Padding(2);
            this.GISLegend.Name = "GISLegend";
            this.GISLegend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GISLegend.Size = new System.Drawing.Size(215, 667);
            this.GISLegend.TabIndex = 2;
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.AutoStyle = true;
            this.GIS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.Level = 28.140189979287609D;
            this.GIS.Location = new System.Drawing.Point(230, 86);
            this.GIS.Margin = new System.Windows.Forms.Padding(2);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(1143, 665);
            this.GIS.TabIndex = 3;
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1384, 761);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GISLegend);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pClassification);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Classification";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.pClassification.ResumeLayout(false);
            this.pClassification.PerformLayout();
            this.pnlClasses.ResumeLayout(false);
            this.pnlClasses.PerformLayout();
            this.pnlInterval.ResumeLayout(false);
            this.pnlInterval.PerformLayout();
            this.pnlManual.ResumeLayout(false);
            this.pnlManual.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
            Size = new System.Drawing.Size(1200, 800);

            pnlInterval.Location = pnlClasses.Location;
            pnlManual.Location = pnlClasses.Location;

            dlgOpen.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.All, false);

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\USA\States\California\Counties.shp");
            GIS.FullExtent();

            cbMethod.SelectedIndex = 0;
            cbRenderBy.SelectedIndex = 1;
            cbClasses.SelectedIndex = 4;
            cbInterval.SelectedIndex = 0;

            fillCbFields();
            fillCbColorRamps();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            String path;

            if (dlgOpen.ShowDialog() != DialogResult.OK)
                return;

            path = dlgOpen.FileName;

            GIS.Open(path);
            GIS.FullExtent();

            fillCbFields();
            fillCbColorRamps();
        }

        private void fillCbFields()
        {
            TGIS_Layer lyr;
            TGIS_LayerVector lv;
            TGIS_LayerPixel lp;

            cbField.Items.Clear();

            lyr = getLayer() as TGIS_Layer;

            if (lyr is TGIS_LayerVector)
            {
                lv = lyr as TGIS_LayerVector;

                cbField.Items.Add("GIS_UID");
                cbField.Items.Add("GIS_AREA");
                cbField.Items.Add("GIS_LENGTH");
                cbField.Items.Add("GIS_CENTROID_X");
                cbField.Items.Add("GIS_CENTROID_Y");

                foreach (TGIS_FieldInfo field in lv.Fields)
                {
                    switch (field.FieldType)
                    {
                        case TGIS_FieldType.Number: cbField.Items.Add(field.Name); break;
                        case TGIS_FieldType.Float: cbField.Items.Add(field.Name); break;
                    }
                }
            } else if (lyr is TGIS_LayerPixel)
            {
                lp = lyr as TGIS_LayerPixel;
                for (int i = 1; i <= lp.BandsCount; i++)
                {
                    cbField.Items.Add(i);
                }
            }

            cbField.SelectedIndex = 0;
        }

        private void fillCbColorRamps()
        {
            String ramp_name;

            for (int i = 0; i < TGIS_Utils.GisColorRampList.Count; i++)
            {
                ramp_name = TGIS_Utils.GisColorRampList[i].Name;

                cbColorRamp.Items.Add(ramp_name);

                if (ramp_name == "GreenBlue")
                    cbColorRamp.SelectedIndex = i;
            }
        }

        private TGIS_Layer getLayer()
        {
            TGIS_Layer res = null;

            if (GIS.Items.Count > 0)
                res = GIS.Items[0] as TGIS_Layer;

            return res;
        }

        private void validateEdit(object sender, EventArgs e)
        {
            float d;
            if ((cbMethod.Text.Equals(GIS_CLASSIFY_METHOD_DI)) ||
                (cbRenderBy.Text.Equals(RENDER_TYPE_SIZE)) ||
                (cbRenderBy.Text.Equals(RENDER_TYPE_OUTLINE_WIDTH)) &&
                (float.TryParse((sender as TextBox).Text, out d)))
                doClassify(sender, e);
        }

        private void pStartColor_MouseClick(object sender, MouseEventArgs e)
        {
            if (dlgColor.ShowDialog() != DialogResult.OK) return;

            pStartColor.BackColor = dlgColor.Color;

            doClassify(sender, e);
        }

        private void pEndColor_MouseClick(object sender, MouseEventArgs e)
        {
            if (dlgColor.ShowDialog() != DialogResult.OK) return;

            pEndColor.BackColor = dlgColor.Color;

            doClassify(sender, e);
        }

        private void cbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void cbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            Single f;
            String method;

            if (!float.TryParse(tbInterval.Text, out f))
                tbInterval.Text = "100";

            if (cbMethod.SelectedIndex == 0)
            {
                pnlClasses.Visible = false;
                pnlInterval.Visible = false;
                pnlManual.Visible = false;
                return;
            }

            cbColorRamp.SelectedIndex = cbColorRamp.Items.IndexOf("GreenBlue");

            method = cbMethod.SelectedItem.ToString();
            if (method.Equals(GIS_CLASSIFY_METHOD_DI))
            {
                pnlInterval.Visible = true;
                pnlClasses.Visible = false;
                pnlManual.Visible = false;

                tbInterval.Visible = true;
                cbInterval.Visible = false;
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_MN))
            {
                pnlInterval.Visible = false;
                pnlClasses.Visible = false;
                pnlManual.Visible = true;
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_QR))
            {
                pnlClasses.Visible = false;
                pnlInterval.Visible = false;
                pnlManual.Visible = false;
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_SD) || method.Equals(GIS_CLASSIFY_METHOD_SDC))
            {
                pnlClasses.Visible = false;
                pnlInterval.Visible = true;
                pnlManual.Visible = false;

                tbInterval.Visible = false;
                cbInterval.Visible = true;
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_UNQ))
            {
                pnlClasses.Visible = false;
                pnlInterval.Visible = false;
                pnlManual.Visible = false;

                cbColorRamp.SelectedIndex = cbColorRamp.Items.IndexOf("Unique");
            }
            else
            {
                pnlClasses.Visible = true;
                pnlInterval.Visible = false;
                pnlManual.Visible = false;
            }

            doClassify(sender, e);
        }

        [Obsolete]
        private void doClassify(object sender, EventArgs e)
        {
            TGIS_Layer lyr;
            TGIS_LayerVector lv = null;
            String method;
            String render_type;
            String interval;
            String class_id_field = "";
            Boolean create_field;
            TGIS_ClassificationAbstract classifier;
            TGIS_ClassificationVector classifier_vec;
            TGIS_ColorMapMode colormap_mode;

            if (cbMethod.SelectedIndex <= 0) return;

            create_field = false;
            lyr = getLayer();

            if (lyr == null) return;

            if (lyr is TGIS_LayerVector)
            {
                lv = lyr as TGIS_LayerVector;

                // add "ClassIdField" if provided
                class_id_field = tbClassIdField.Text;
                create_field = class_id_field.Length > 0;
                if (create_field && (lv.FindField(class_id_field) < 0))
                    lv.AddField(class_id_field, TGIS_FieldType.Number, 3, 0);
            } else if (!(lyr is TGIS_LayerPixel))
            {
                MessageBox.Show(String.Format("Layer %s is not supported", (lyr as TGIS_LayerPixel).Name));
            }

            classifier = TGIS_ClassificationFactory.CreateClassifier(lyr);

            // set common properties
            classifier.Target = cbField.SelectedItem.ToString();
            classifier.NumClasses = cbClasses.SelectedIndex + 1;
            classifier.StartColor = TGIS_Color.FromRGB(pStartColor.BackColor.R, pStartColor.BackColor.G, pStartColor.BackColor.B);
            classifier.EndColor = TGIS_Color.FromRGB(pEndColor.BackColor.R, pEndColor.BackColor.G, pEndColor.BackColor.B);
            classifier.ShowLegend = chkShowInLegend.Checked;

            // set method
            method = cbMethod.SelectedItem.ToString();
            if (method == GIS_CLASSIFY_METHOD_DI)
                classifier.Method = TGIS_ClassificationMethod.DefinedInterval;
            else if (method == GIS_CLASSIFY_METHOD_EI)
                classifier.Method = TGIS_ClassificationMethod.EqualInterval;
            else if (method == GIS_CLASSIFY_METHOD_GI)
                classifier.Method = TGIS_ClassificationMethod.GeometricalInterval;
            else if (method == GIS_CLASSIFY_METHOD_KM)
                classifier.Method = TGIS_ClassificationMethod.KMeans;
            else if (method == GIS_CLASSIFY_METHOD_KMS)
                classifier.Method = TGIS_ClassificationMethod.KMeansSpatial;
            else if (method == GIS_CLASSIFY_METHOD_MN)
                classifier.Method = TGIS_ClassificationMethod.Manual;
            else if (method == GIS_CLASSIFY_METHOD_NB)
                classifier.Method = TGIS_ClassificationMethod.NaturalBreaks;
            else if (method == GIS_CLASSIFY_METHOD_QN)
                classifier.Method = TGIS_ClassificationMethod.Quantile;
            else if (method == GIS_CLASSIFY_METHOD_QR)
                classifier.Method = TGIS_ClassificationMethod.Quartile;
            else if (method == GIS_CLASSIFY_METHOD_SD)
                classifier.Method = TGIS_ClassificationMethod.StandardDeviation;
            else if (method == GIS_CLASSIFY_METHOD_SDC)
                classifier.Method = TGIS_ClassificationMethod.StandardDeviationWithCentral;
            else if (method == GIS_CLASSIFY_METHOD_UNQ)
                classifier.Method = TGIS_ClassificationMethod.Unique;
            else
                classifier.Method = TGIS_ClassificationMethod.NaturalBreaks;

            // set interval
            float intervalVal;
            if (!float.TryParse(tbInterval.Text,
                System.Globalization.NumberStyles.AllowDecimalPoint,
                System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"),
                out intervalVal)) return;
            classifier.Interval = intervalVal;

            if ((method == GIS_CLASSIFY_METHOD_SD) || (method == GIS_CLASSIFY_METHOD_SDC))
            {
                interval = cbInterval.SelectedItem.ToString();
                if (interval == STD_INTERVAL_ONE)
                    classifier.Interval = 1.0;
                else if (interval == STD_INTERVAL_ONE_HALF)
                    classifier.Interval = 1.0 / 2;
                else if (interval == STD_INTERVAL_ONE_THIRD)
                    classifier.Interval = 1.0 / 3;
                else if (interval == STD_INTERVAL_ONE_QUARTER)
                    classifier.Interval = 1.0 / 4;
                else
                    classifier.Interval = 1;
            }

            // set manual
            string[] class_breaks_arr = edtManualBreaks.Text.Split(',');
            foreach (string class_break_str in class_breaks_arr)
            {
                float class_break_val;
                if (!float.TryParse(class_break_str,
                    System.Globalization.NumberStyles.AllowDecimalPoint,
                    System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"),
                    out class_break_val)) return;

                classifier.AddClassBreak(class_break_val);
            }

            if (chkColorRampName.Checked)
            {
                if(cbColorRamp.Text.Equals("None"))
                {
                    classifier.ColorRampName = "";
                }
                else
                {
                    classifier.ColorRampName = TGIS_Utils.GisColorRampList[cbColorRamp.SelectedIndex].Name;
                }
            }
      
            // NumClasses property is automatically calculated for methods:
            // DefinedInterval, Quartile, StandardDeviation(s)
            if (chkColorRamp.Checked)
            {
                if (method == GIS_CLASSIFY_METHOD_UNQ) colormap_mode = TGIS_ColorMapMode.Discrete;
                else colormap_mode = TGIS_ColorMapMode.Continuous;

                classifier.EstimateNumClasses();
                classifier.ColorRamp = TGIS_Utils.GisColorRampList[cbColorRamp.SelectedIndex].RealizeColorMap(
                    colormap_mode,
                    classifier.NumClasses,
                    false
                );
            } else
            {
                classifier.ColorRamp = null;
            }

            // vector-only params
            if (classifier is TGIS_ClassificationVector)
            {
                classifier_vec = classifier as TGIS_ClassificationVector;
                classifier_vec.StartSize = int.Parse(tbStartSize.Text);
                classifier_vec.EndSize = int.Parse(tbEndSize.Text);
                classifier_vec.ClassIdField = class_id_field;

                // render type
                render_type = cbRenderBy.SelectedItem.ToString();
                if (render_type.Equals(RENDER_TYPE_SIZE))
                {
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.Size;
                } else if (render_type.Equals(RENDER_TYPE_COLOR))
                {
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.Color;
                } else if (render_type.Equals(RENDER_TYPE_OUTLINE_WIDTH))
                {
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.OutlineWidth;
                } else if (render_type.Equals(RENDER_TYPE_OUTLINE_COLOR))
                {
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.OutlineColor;
                } else
                {
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.Color;
                }
            }

            /* The classification process is based on layer statistics.
               By default, the classifier will force the calculation of the required statistics.
               If the user wants to have control over it,
               "ForceStatisticsCalculation" property should be set to False.
               See the usual code in this case. */
            classifier.ForceStatisticsCalculation = chkForceStatisticsCalculation.Checked;

            // before the classification starts, layer statistics must be provided
            if (!classifier.ForceStatisticsCalculation && classifier.MustCalculateStatistics())
            {
                DialogResult res = MessageBox.Show("Statistics need to be calculated.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (res.Equals(DialogResult.OK))
                {
                    lyr.Statistics.Calculate();
                } else
                {
                    lyr.Statistics.ResetModified();
                    return;
                }
            }

            if (classifier.Classify() && create_field && (lv != null))
            {
                lv.SaveData();
            }

            GIS.InvalidateWholeMap();
        }

        private void cbRenderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            TGIS_Layer ll;

            ll = getLayer();

            if (ll is TGIS_LayerVector)
            {
                ll.ParamsList.ClearAndSetDefaults();
                if (((ll as TGIS_LayerVector).DefaultShapeType == TGIS_ShapeType.Polygon) && (cbRenderBy.SelectedItem.ToString() == RENDER_TYPE_SIZE))
                {
                    MessageBox.Show("Method not allowed for polygons");
                    cbRenderBy.SelectedIndex = 1;
                }
            }

            doClassify(sender, e);
        }

        private void cbInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void chkUseColorRamp_CheckedChanged(object sender, EventArgs e)
        {
            cbColorRamp.Enabled = chkColorRampName.Checked || chkColorRamp.Checked;

            doClassify(sender, e);
        }

        private void chkShowInLegend_CheckedChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void cbClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void cbColorRamp_SelectedIndexChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void tbInterval_TextChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void cbClasses_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void cbInterval_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void btnAddManualBreak_Click(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void chkColorRampName_CheckedChanged(object sender, EventArgs e)
        {
            cbColorRamp.Enabled = chkColorRampName.Checked || chkColorRamp.Checked;

            doClassify(sender, e);
        }
    }
}
