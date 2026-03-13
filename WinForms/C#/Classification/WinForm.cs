using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.NDK.Common;

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
        private Panel pColor;
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
        private Panel pnlClasses;
        private ComboBox cbClasses;
        private Label lblClasses;
        private Panel pRamps;
        private CheckBox chkColorRampName;
        private ComboBox cbColorRamp;
        private CheckBox chkColorRamp;
        private ComboBox cbColorMapMode;
        private Label lblColorMapMode;
        private CheckBox chkReverse;

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
            TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TGIS_ControlLegendDialogOptions();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(WinForm));
            pClassification=new Panel();
            pnlClasses=new Panel();
            cbClasses=new ComboBox();
            lblClasses=new Label();
            pnlInterval=new Panel();
            tbInterval=new TextBox();
            cbInterval=new ComboBox();
            lblInterval=new Label();
            pnlManual=new Panel();
            btnAddManualBreak=new Button();
            edtManualBreaks=new TextBox();
            lblManual=new Label();
            chkForceStatisticsCalculation=new CheckBox();
            cbRenderBy=new ComboBox();
            cbMethod=new ComboBox();
            cbField=new ComboBox();
            lblRenderBy=new Label();
            lblMethod=new Label();
            lblField=new Label();
            btnOpen=new Button();
            pColor=new Panel();
            tbEndSize=new TextBox();
            chkShowInLegend=new CheckBox();
            tbStartSize=new TextBox();
            tbClassIdField=new TextBox();
            lblClassIdField=new Label();
            lblEndSize=new Label();
            lblStartSize=new Label();
            pEndColor=new Panel();
            lblEndColor=new Label();
            pStartColor=new Panel();
            lblStartColor=new Label();
            GISLegend=new TGIS_ControlLegend();
            GIS=new TGIS_ViewerWnd();
            dlgColor=new ColorDialog();
            dlgOpen=new OpenFileDialog();
            pRamps=new Panel();
            chkReverse=new CheckBox();
            cbColorMapMode=new ComboBox();
            lblColorMapMode=new Label();
            chkColorRamp=new CheckBox();
            chkColorRampName=new CheckBox();
            cbColorRamp=new ComboBox();
            pClassification.SuspendLayout();
            pnlClasses.SuspendLayout();
            pnlInterval.SuspendLayout();
            pnlManual.SuspendLayout();
            pColor.SuspendLayout();
            pRamps.SuspendLayout();
            SuspendLayout();
            // 
            // pClassification
            // 
            pClassification.Anchor=AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;
            pClassification.Controls.Add(pnlClasses);
            pClassification.Controls.Add(pnlInterval);
            pClassification.Controls.Add(pnlManual);
            pClassification.Controls.Add(chkForceStatisticsCalculation);
            pClassification.Controls.Add(cbRenderBy);
            pClassification.Controls.Add(cbMethod);
            pClassification.Controls.Add(cbField);
            pClassification.Controls.Add(lblRenderBy);
            pClassification.Controls.Add(lblMethod);
            pClassification.Controls.Add(lblField);
            pClassification.Controls.Add(btnOpen);
            pClassification.Location=new Point(9, 9);
            pClassification.Margin=new Padding(2);
            pClassification.Name="pClassification";
            pClassification.Size=new Size(1364, 34);
            pClassification.TabIndex=0;
            // 
            // pnlClasses
            // 
            pnlClasses.Controls.Add(cbClasses);
            pnlClasses.Controls.Add(lblClasses);
            pnlClasses.Location=new Point(745, 0);
            pnlClasses.Margin=new Padding(2);
            pnlClasses.Name="pnlClasses";
            pnlClasses.Size=new Size(200, 34);
            pnlClasses.TabIndex=16;
            pnlClasses.Visible=false;
            // 
            // cbClasses
            // 
            cbClasses.DropDownStyle=ComboBoxStyle.DropDownList;
            cbClasses.FormattingEnabled=true;
            cbClasses.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
            cbClasses.Location=new Point(57, 7);
            cbClasses.Margin=new Padding(2);
            cbClasses.Name="cbClasses";
            cbClasses.Size=new Size(50, 23);
            cbClasses.TabIndex=11;
            cbClasses.SelectedIndexChanged+=cbClasses_SelectedIndexChanged_2;
            // 
            // lblClasses
            // 
            lblClasses.AutoSize=true;
            lblClasses.Location=new Point(7, 10);
            lblClasses.Margin=new Padding(2, 0, 2, 0);
            lblClasses.Name="lblClasses";
            lblClasses.Size=new Size(48, 15);
            lblClasses.TabIndex=10;
            lblClasses.Text="Classes:";
            // 
            // pnlInterval
            // 
            pnlInterval.Controls.Add(tbInterval);
            pnlInterval.Controls.Add(cbInterval);
            pnlInterval.Controls.Add(lblInterval);
            pnlInterval.Location=new Point(949, 0);
            pnlInterval.Margin=new Padding(2);
            pnlInterval.Name="pnlInterval";
            pnlInterval.Size=new Size(200, 34);
            pnlInterval.TabIndex=14;
            // 
            // tbInterval
            // 
            tbInterval.Location=new Point(58, 7);
            tbInterval.Margin=new Padding(2);
            tbInterval.Name="tbInterval";
            tbInterval.Size=new Size(114, 23);
            tbInterval.TabIndex=14;
            tbInterval.Text="100";
            tbInterval.Visible=false;
            tbInterval.TextChanged+=tbInterval_TextChanged;
            // 
            // cbInterval
            // 
            cbInterval.DropDownStyle=ComboBoxStyle.DropDownList;
            cbInterval.FormattingEnabled=true;
            cbInterval.Items.AddRange(new object[] { "1 STDEV", "1/2 STDEV", "1/3 STDEV", "1/4 STDEV" });
            cbInterval.Location=new Point(58, 7);
            cbInterval.Margin=new Padding(2);
            cbInterval.Name="cbInterval";
            cbInterval.Size=new Size(140, 23);
            cbInterval.TabIndex=13;
            cbInterval.SelectedIndexChanged+=cbInterval_SelectedIndexChanged_1;
            // 
            // lblInterval
            // 
            lblInterval.AutoSize=true;
            lblInterval.Location=new Point(9, 10);
            lblInterval.Margin=new Padding(2, 0, 2, 0);
            lblInterval.Name="lblInterval";
            lblInterval.Size=new Size(49, 15);
            lblInterval.TabIndex=12;
            lblInterval.Text="Interval:";
            // 
            // pnlManual
            // 
            pnlManual.Controls.Add(btnAddManualBreak);
            pnlManual.Controls.Add(edtManualBreaks);
            pnlManual.Controls.Add(lblManual);
            pnlManual.Location=new Point(1153, 0);
            pnlManual.Margin=new Padding(2);
            pnlManual.Name="pnlManual";
            pnlManual.Size=new Size(200, 34);
            pnlManual.TabIndex=13;
            pnlManual.Visible=false;
            // 
            // btnAddManualBreak
            // 
            btnAddManualBreak.Location=new Point(160, 6);
            btnAddManualBreak.Margin=new Padding(2);
            btnAddManualBreak.Name="btnAddManualBreak";
            btnAddManualBreak.Size=new Size(40, 22);
            btnAddManualBreak.TabIndex=13;
            btnAddManualBreak.Text="Add";
            btnAddManualBreak.UseVisualStyleBackColor=true;
            btnAddManualBreak.Click+=btnAddManualBreak_Click;
            // 
            // edtManualBreaks
            // 
            edtManualBreaks.Location=new Point(51, 7);
            edtManualBreaks.Margin=new Padding(2);
            edtManualBreaks.Name="edtManualBreaks";
            edtManualBreaks.Size=new Size(102, 23);
            edtManualBreaks.TabIndex=12;
            edtManualBreaks.Text="0,10.5,20,50";
            // 
            // lblManual
            // 
            lblManual.AutoSize=true;
            lblManual.Location=new Point(2, 10);
            lblManual.Margin=new Padding(2, 0, 2, 0);
            lblManual.Name="lblManual";
            lblManual.Size=new Size(50, 15);
            lblManual.TabIndex=6;
            lblManual.Text="Manual:";
            // 
            // chkForceStatisticsCalculation
            // 
            chkForceStatisticsCalculation.AutoSize=true;
            chkForceStatisticsCalculation.Checked=true;
            chkForceStatisticsCalculation.CheckState=CheckState.Checked;
            chkForceStatisticsCalculation.Location=new Point(436, 9);
            chkForceStatisticsCalculation.Margin=new Padding(2);
            chkForceStatisticsCalculation.Name="chkForceStatisticsCalculation";
            chkForceStatisticsCalculation.Size=new Size(167, 19);
            chkForceStatisticsCalculation.TabIndex=12;
            chkForceStatisticsCalculation.Text="Force Statistics Calculation";
            chkForceStatisticsCalculation.UseVisualStyleBackColor=true;
            chkForceStatisticsCalculation.CheckedChanged+=chkForceStatisticsCalculation_CheckedChanged;
            // 
            // cbRenderBy
            // 
            cbRenderBy.DropDownStyle=ComboBoxStyle.DropDownList;
            cbRenderBy.FormattingEnabled=true;
            cbRenderBy.Items.AddRange(new object[] { "Size / Width", "Color", "Outline width", "Outline color" });
            cbRenderBy.Location=new Point(656, 7);
            cbRenderBy.Margin=new Padding(2);
            cbRenderBy.Name="cbRenderBy";
            cbRenderBy.Size=new Size(86, 23);
            cbRenderBy.TabIndex=8;
            cbRenderBy.SelectedIndexChanged+=cbRenderBy_SelectedIndexChanged;
            // 
            // cbMethod
            // 
            cbMethod.DropDownStyle=ComboBoxStyle.DropDownList;
            cbMethod.FormattingEnabled=true;
            cbMethod.Items.AddRange(new object[] { "Select ...", "Defined Interval", "Equal Interval", "Geometrical Interval", "Manual", "Natural Breaks", "K-Means", "K-Means Spatial", "Quantile", "Quartile", "Standard Deviation", "Standard Deviation with Central", "Unique" });
            cbMethod.Location=new Point(304, 7);
            cbMethod.Margin=new Padding(2);
            cbMethod.Name="cbMethod";
            cbMethod.Size=new Size(122, 23);
            cbMethod.TabIndex=7;
            cbMethod.SelectedIndexChanged+=cbMethod_SelectedIndexChanged;
            // 
            // cbField
            // 
            cbField.DropDownStyle=ComboBoxStyle.DropDownList;
            cbField.FormattingEnabled=true;
            cbField.Location=new Point(118, 7);
            cbField.Margin=new Padding(2);
            cbField.Name="cbField";
            cbField.Size=new Size(136, 23);
            cbField.TabIndex=6;
            cbField.SelectedIndexChanged+=cbField_SelectedIndexChanged;
            // 
            // lblRenderBy
            // 
            lblRenderBy.AutoSize=true;
            lblRenderBy.Location=new Point(593, 10);
            lblRenderBy.Margin=new Padding(2, 0, 2, 0);
            lblRenderBy.Name="lblRenderBy";
            lblRenderBy.Size=new Size(63, 15);
            lblRenderBy.TabIndex=3;
            lblRenderBy.Text="Render by:";
            // 
            // lblMethod
            // 
            lblMethod.AutoSize=true;
            lblMethod.Location=new Point(259, 10);
            lblMethod.Margin=new Padding(2, 0, 2, 0);
            lblMethod.Name="lblMethod";
            lblMethod.Size=new Size(55, 15);
            lblMethod.TabIndex=2;
            lblMethod.Text="Method: ";
            // 
            // lblField
            // 
            lblField.AutoSize=true;
            lblField.Location=new Point(82, 10);
            lblField.Margin=new Padding(2, 0, 2, 0);
            lblField.Name="lblField";
            lblField.Size=new Size(35, 15);
            lblField.TabIndex=1;
            lblField.Text="Field:";
            // 
            // btnOpen
            // 
            btnOpen.Location=new Point(2, 6);
            btnOpen.Margin=new Padding(2);
            btnOpen.Name="btnOpen";
            btnOpen.Size=new Size(74, 22);
            btnOpen.TabIndex=0;
            btnOpen.Text="Open...";
            btnOpen.UseVisualStyleBackColor=true;
            btnOpen.Click+=btnOpen_Click;
            // 
            // pColor
            // 
            pColor.Anchor=AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;
            pColor.Controls.Add(tbEndSize);
            pColor.Controls.Add(chkShowInLegend);
            pColor.Controls.Add(tbStartSize);
            pColor.Controls.Add(tbClassIdField);
            pColor.Controls.Add(lblClassIdField);
            pColor.Controls.Add(lblEndSize);
            pColor.Controls.Add(lblStartSize);
            pColor.Controls.Add(pEndColor);
            pColor.Controls.Add(lblEndColor);
            pColor.Controls.Add(pStartColor);
            pColor.Controls.Add(lblStartColor);
            pColor.Location=new Point(9, 47);
            pColor.Margin=new Padding(2);
            pColor.Name="pColor";
            pColor.Size=new Size(1364, 34);
            pColor.TabIndex=1;
            // 
            // tbEndSize
            // 
            tbEndSize.Location=new Point(355, 8);
            tbEndSize.Margin=new Padding(2);
            tbEndSize.Name="tbEndSize";
            tbEndSize.Size=new Size(48, 23);
            tbEndSize.TabIndex=14;
            tbEndSize.Text="100";
            tbEndSize.TextChanged+=validateEdit;
            // 
            // chkShowInLegend
            // 
            chkShowInLegend.AutoSize=true;
            chkShowInLegend.Checked=true;
            chkShowInLegend.CheckState=CheckState.Checked;
            chkShowInLegend.Location=new Point(595, 10);
            chkShowInLegend.Margin=new Padding(2);
            chkShowInLegend.Name="chkShowInLegend";
            chkShowInLegend.Size=new Size(107, 19);
            chkShowInLegend.TabIndex=11;
            chkShowInLegend.Text="Show in legend";
            chkShowInLegend.UseVisualStyleBackColor=true;
            chkShowInLegend.CheckedChanged+=chkShowInLegend_CheckedChanged;
            // 
            // tbStartSize
            // 
            tbStartSize.Location=new Point(246, 8);
            tbStartSize.Margin=new Padding(2);
            tbStartSize.Name="tbStartSize";
            tbStartSize.Size=new Size(48, 23);
            tbStartSize.TabIndex=9;
            tbStartSize.Text="1";
            tbStartSize.TextChanged+=validateEdit;
            // 
            // tbClassIdField
            // 
            tbClassIdField.Location=new Point(486, 8);
            tbClassIdField.Margin=new Padding(2);
            tbClassIdField.Name="tbClassIdField";
            tbClassIdField.Size=new Size(100, 23);
            tbClassIdField.TabIndex=8;
            tbClassIdField.TextChanged+=validateEdit;
            // 
            // lblClassIdField
            // 
            lblClassIdField.AutoSize=true;
            lblClassIdField.Location=new Point(411, 11);
            lblClassIdField.Margin=new Padding(2, 0, 2, 0);
            lblClassIdField.Name="lblClassIdField";
            lblClassIdField.Size=new Size(77, 15);
            lblClassIdField.TabIndex=5;
            lblClassIdField.Text="Class ID field:";
            // 
            // lblEndSize
            // 
            lblEndSize.AutoSize=true;
            lblEndSize.Location=new Point(301, 11);
            lblEndSize.Margin=new Padding(2, 0, 2, 0);
            lblEndSize.Name="lblEndSize";
            lblEndSize.Size=new Size(52, 15);
            lblEndSize.TabIndex=4;
            lblEndSize.Text="End size:";
            // 
            // lblStartSize
            // 
            lblStartSize.AutoSize=true;
            lblStartSize.Location=new Point(186, 11);
            lblStartSize.Margin=new Padding(2, 0, 2, 0);
            lblStartSize.Name="lblStartSize";
            lblStartSize.Size=new Size(56, 15);
            lblStartSize.TabIndex=3;
            lblStartSize.Text="Start size:";
            // 
            // pEndColor
            // 
            pEndColor.BackColor=Color.Green;
            pEndColor.BorderStyle=BorderStyle.FixedSingle;
            pEndColor.Location=new Point(158, 8);
            pEndColor.Margin=new Padding(2);
            pEndColor.Name="pEndColor";
            pEndColor.Size=new Size(24, 20);
            pEndColor.TabIndex=2;
            pEndColor.MouseClick+=pEndColor_MouseClick;
            // 
            // lblEndColor
            // 
            lblEndColor.AutoSize=true;
            lblEndColor.Location=new Point(97, 11);
            lblEndColor.Margin=new Padding(2, 0, 2, 0);
            lblEndColor.Name="lblEndColor";
            lblEndColor.Size=new Size(60, 15);
            lblEndColor.TabIndex=2;
            lblEndColor.Text="End color:";
            // 
            // pStartColor
            // 
            pStartColor.BackColor=Color.FromArgb(233, 248, 237);
            pStartColor.BorderStyle=BorderStyle.FixedSingle;
            pStartColor.Location=new Point(69, 8);
            pStartColor.Margin=new Padding(2);
            pStartColor.Name="pStartColor";
            pStartColor.Size=new Size(24, 20);
            pStartColor.TabIndex=1;
            pStartColor.MouseClick+=pStartColor_MouseClick;
            // 
            // lblStartColor
            // 
            lblStartColor.AutoSize=true;
            lblStartColor.Location=new Point(7, 11);
            lblStartColor.Margin=new Padding(2, 0, 2, 0);
            lblStartColor.Name="lblStartColor";
            lblStartColor.Size=new Size(64, 15);
            lblStartColor.TabIndex=0;
            lblStartColor.Text="Start color:";
            // 
            // GISLegend
            // 
            GISLegend.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit=256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit=16384;
            GISLegend.DialogOptions=tgiS_ControlLegendDialogOptions1;
            GISLegend.GIS_Viewer=GIS;
            GISLegend.Location=new Point(9, 123);
            GISLegend.Margin=new Padding(2);
            GISLegend.Name="GISLegend";
            GISLegend.Options=TGIS_ControlLegendOption.AllowMove|TGIS_ControlLegendOption.AllowActive|TGIS_ControlLegendOption.AllowExpand|TGIS_ControlLegendOption.AllowParams|TGIS_ControlLegendOption.AllowSelect|TGIS_ControlLegendOption.ShowSubLayers|TGIS_ControlLegendOption.AllowParamsVisible;
            GISLegend.Size=new Size(215, 630);
            GISLegend.TabIndex=2;
            // 
            // GIS
            // 
            GIS.Anchor=AnchorStyles.Top|AnchorStyles.Bottom|AnchorStyles.Left|AnchorStyles.Right;
            GIS.AutoStyle=true;
            GIS.BackColor=Color.FromArgb(255, 255, 255);
            GIS.Level=1D;
            GIS.Location=new Point(230, 123);
            GIS.Margin=new Padding(2);
            GIS.Name="GIS";
            GIS.SelectionColor=Color.FromArgb(255, 0, 0);
            GIS.Size=new Size(1143, 628);
            GIS.TabIndex=3;
            GIS.TiledPaint=false;
            // 
            // dlgOpen
            // 
            dlgOpen.FileName="openFileDialog1";
            // 
            // pRamps
            // 
            pRamps.Anchor=AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right;
            pRamps.Controls.Add(chkReverse);
            pRamps.Controls.Add(cbColorMapMode);
            pRamps.Controls.Add(lblColorMapMode);
            pRamps.Controls.Add(chkColorRamp);
            pRamps.Controls.Add(chkColorRampName);
            pRamps.Controls.Add(cbColorRamp);
            pRamps.Location=new Point(9, 85);
            pRamps.Margin=new Padding(2);
            pRamps.Name="pRamps";
            pRamps.Size=new Size(1364, 34);
            pRamps.TabIndex=16;
            // 
            // chkReverse
            // 
            chkReverse.AutoSize=true;
            chkReverse.Location=new Point(755, 6);
            chkReverse.Name="chkReverse";
            chkReverse.Size=new Size(66, 19);
            chkReverse.TabIndex=19;
            chkReverse.Text="Reverse";
            chkReverse.UseVisualStyleBackColor=true;
            chkReverse.CheckedChanged+=chkReverse_CheckedChanged;
            // 
            // cbColorMapMode
            // 
            cbColorMapMode.FormattingEnabled=true;
            cbColorMapMode.Items.AddRange(new object[] { "Continuous", "Discrete" });
            cbColorMapMode.Location=new Point(628, 4);
            cbColorMapMode.Name="cbColorMapMode";
            cbColorMapMode.Size=new Size(121, 23);
            cbColorMapMode.TabIndex=18;
            cbColorMapMode.Text="Continous";
            cbColorMapMode.SelectedIndexChanged+=cbColorMapMode_SelectedIndexChanged;
            // 
            // lblColorMapMode
            // 
            lblColorMapMode.AutoSize=true;
            lblColorMapMode.Location=new Point(515, 9);
            lblColorMapMode.Name="lblColorMapMode";
            lblColorMapMode.Size=new Size(97, 15);
            lblColorMapMode.TabIndex=17;
            lblColorMapMode.Text="Colormap Mode:";
            // 
            // chkColorRamp
            // 
            chkColorRamp.AutoSize=true;
            chkColorRamp.Checked=true;
            chkColorRamp.CheckState=CheckState.Checked;
            chkColorRamp.Location=new Point(4, 8);
            chkColorRamp.Margin=new Padding(2);
            chkColorRamp.Name="chkColorRamp";
            chkColorRamp.Size=new Size(108, 19);
            chkColorRamp.TabIndex=16;
            chkColorRamp.Text="Use ColorRamp";
            chkColorRamp.UseVisualStyleBackColor=true;
            chkColorRamp.CheckedChanged+=chkColorRamp_CheckedChanged;
            // 
            // chkColorRampName
            // 
            chkColorRampName.AutoSize=true;
            chkColorRampName.Checked=true;
            chkColorRampName.CheckState=CheckState.Checked;
            chkColorRampName.Location=new Point(131, 8);
            chkColorRampName.Margin=new Padding(2);
            chkColorRampName.Name="chkColorRampName";
            chkColorRampName.Size=new Size(140, 19);
            chkColorRampName.TabIndex=15;
            chkColorRampName.Text="Use ColorRampName";
            chkColorRampName.UseVisualStyleBackColor=true;
            chkColorRampName.CheckedChanged+=chkColorRampName_CheckedChanged;
            // 
            // cbColorRamp
            // 
            cbColorRamp.DropDownStyle=ComboBoxStyle.DropDownList;
            cbColorRamp.FormattingEnabled=true;
            cbColorRamp.Location=new Point(295, 6);
            cbColorRamp.Margin=new Padding(2);
            cbColorRamp.Name="cbColorRamp";
            cbColorRamp.Size=new Size(215, 23);
            cbColorRamp.TabIndex=13;
            cbColorRamp.SelectedIndexChanged+=cbColorRamp_SelectedIndexChanged_1;
            // 
            // WinForm
            // 
            AutoScaleDimensions=new SizeF(96F, 96F);
            AutoScaleMode=AutoScaleMode.Dpi;
            ClientSize=new Size(1384, 761);
            Controls.Add(pRamps);
            Controls.Add(GIS);
            Controls.Add(GISLegend);
            Controls.Add(pColor);
            Controls.Add(pClassification);
            Icon=(Icon)resources.GetObject("$this.Icon");
            Location=new Point(200, 120);
            Margin=new Padding(2);
            Name="WinForm";
            StartPosition=FormStartPosition.CenterScreen;
            Text="TatukGIS Samples - Classification";
            Load+=WinForm_Load;
            pClassification.ResumeLayout(false);
            pClassification.PerformLayout();
            pnlClasses.ResumeLayout(false);
            pnlClasses.PerformLayout();
            pnlInterval.ResumeLayout(false);
            pnlInterval.PerformLayout();
            pnlManual.ResumeLayout(false);
            pnlManual.PerformLayout();
            pColor.ResumeLayout(false);
            pColor.PerformLayout();
            pRamps.ResumeLayout(false);
            pRamps.PerformLayout();
            ResumeLayout(false);
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
            }
            else if (lyr is TGIS_LayerPixel)
            {
                lp = lyr as TGIS_LayerPixel;
                for (int i = 1; i <= lp.BandsCount; i++)
                {
                    cbField.Items.Add(i);
                }
            }

            cbField.SelectedIndex = 0;
        }

        private void setColorRampControlEnabled(Boolean _enabled)
        {
            cbColorRamp.Enabled = _enabled;
            chkColorRampName.Enabled = _enabled;
            cbColorMapMode.Enabled = _enabled;
            chkReverse.Enabled = _enabled;
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
        private void setInterval(Boolean _val)
        {
            tbInterval.Visible = _val;
            lblInterval.Visible = _val;
        }

        private void showInterval()
        {
            setInterval(true);
        }

        private void hideInterval()
        {
            setInterval(false);
        }
        private void setStdDev(Boolean _val)
        {
            cbInterval.Visible = _val;
            lblInterval.Visible = _val;
        }

        private void showStdDev()
        {
            setStdDev(true);
        }

        private void hideStdDev()
        {
            setStdDev(false);
        }

        private void setClasses(Boolean _val)
        {
            cbClasses.Visible = _val;
            lblClasses.Visible = _val;
        }

        private void showClasses()
        {
            setClasses(true);
        }

        private void hideClasses()
        {
            setClasses(false);
        }

        private void setManual(Boolean _val)
        {
            edtManualBreaks.Visible = _val;
            lblManual.Visible = _val;
            btnAddManualBreak.Visible = _val;
        }

        private void showManual()
        {
            setManual(true);
        }

        private void hideManual()
        {
            setManual(false);
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
            cbColorMapMode.SelectedItem = TatukGIS.NDK.__Global.GIS_COLORMAPMODE_CONTINUOUS;

            method = cbMethod.SelectedItem.ToString();
            // no selection
            if (cbMethod.SelectedIndex == 0)
            {
                hideInterval();
                hideStdDev();
                hideClasses();
                hideManual();
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_DI))
            {
                hideStdDev();
                hideClasses();
                hideManual();

                showInterval();
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_MN))
            {
                hideInterval();
                hideStdDev();
                hideClasses();

                showManual();
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_QR))
            {
                hideInterval();
                hideStdDev();
                hideClasses();
                hideManual();

                cbColorMapMode.SelectedItem = TatukGIS.NDK.TGIS_ColorRampNames.BrownGreen;
            }
            else if ((method.Equals(GIS_CLASSIFY_METHOD_SD)) ||
                    (method.Equals(GIS_CLASSIFY_METHOD_SDC)))
            {
                hideInterval();
                hideClasses();
                hideManual();

                showStdDev();

                cbColorMapMode.SelectedItem = TatukGIS.NDK.TGIS_ColorRampNames.BrownGreen;
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_UNQ))
            {
                hideInterval();
                hideClasses();
                hideStdDev();
                hideManual();

                setColorRampControlEnabled(true);

                chkColorRamp.Checked = true;
                cbColorRamp.SelectedItem = TGIS_ColorRampNames.Unique;
                cbColorMapMode.SelectedItem = TatukGIS.NDK.__Global.GIS_COLORMAPMODE_DISCRETE;
            }
            else
            {
                hideInterval();
                hideStdDev();
                hideManual();
                showClasses();
            }

            doClassify(sender, e);
        }

        [Obsolete]
        private void doClassify(object sender, EventArgs e)
        {
            TGIS_Layer lyr;
            TGIS_LayerVector lv = null;
            String method;
            String ramp_name;
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
            }
            else if (!(lyr is TGIS_LayerPixel))
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
                if (cbColorRamp.Text.Equals("None"))
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
                // colormap mode
                switch (cbColorMapMode.SelectedItem)
                {
                    case TatukGIS.NDK.__Global.GIS_COLORMAPMODE_CONTINUOUS:
                        colormap_mode = TGIS_ColorMapMode.Continuous;
                        break;
                    default:
                        colormap_mode = TGIS_ColorMapMode.Discrete;
                        break;
                }

                // ramp can be assigned directly (ColorRamp) or by name (ColorRampName)
                ramp_name = cbColorRamp.SelectedItem.ToString();
                if (chkColorRampName.Checked)
                {
                    classifier.ColorRampName = ramp_name;
                }
                else
                {
                    classifier.ColorRamp = TatukGIS.NDK.__Global.GisColorRampList().ByName(ramp_name);
                }
                classifier.ColorRamp.DefaultColorMapMode = colormap_mode;
                classifier.ColorRamp.DefaultReverse = chkReverse.Checked;
            }
            else
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
                }
                else if (render_type.Equals(RENDER_TYPE_COLOR))
                {
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.Color;
                }
                else if (render_type.Equals(RENDER_TYPE_OUTLINE_WIDTH))
                {
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.OutlineWidth;
                }
                else if (render_type.Equals(RENDER_TYPE_OUTLINE_COLOR))
                {
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.OutlineColor;
                }
                else
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
                }
                else
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

        private void chkShowInLegend_CheckedChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void tbInterval_TextChanged(object sender, EventArgs e)
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

        private void cbColorMapMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void chkForceStatisticsCalculation_CheckedChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void chkReverse_CheckedChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void cbColorRamp_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        private void chkColorRamp_CheckedChanged(object sender, EventArgs e)
        {
            setColorRampControlEnabled(chkColorRamp.Checked);
        }

        private void cbClasses_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }
    }
}
