//=============================================================================
// This source code is a part of TatukGIS Developer Kernel.
//=============================================================================
//
// Classification sample — demonstrates thematic data classification of vector
// and raster (pixel) layers using the TatukGIS TGIS_ClassificationAbstract API.
//
// Key concepts shown:
//   - Loading a layer with TGIS_ViewerWnd.Open
//   - Creating a classifier with TGIS_ClassificationFactory.CreateClassifier,
//     which returns the appropriate subclass for the layer type
//     (TGIS_ClassificationVector or TGIS_ClassificationPixel)
//   - Setting the classification target field (numeric attribute or band index)
//   - Choosing a classification method from TGIS_ClassificationMethod:
//       DefinedInterval, EqualInterval, GeometricalInterval, Manual,
//       NaturalBreaks, KMeans, KMeansSpatial, Quantile, Quartile,
//       StandardDeviation, StandardDeviationWithCentral, Unique
//   - Controlling the visual output via:
//       StartColor / EndColor       — color gradient endpoints
//       NumClasses                  — number of class breaks (auto for some methods)
//       Interval                    — interval size (DefinedInterval / StdDev methods)
//       ColorRamp / ColorRampName   — named palette from GisColorRampList
//       ColorRamp.DefaultColorMapMode — Continuous or Discrete color mapping
//       ColorRamp.DefaultReverse    — reverse the ramp direction
//   - Vector-specific properties via TGIS_ClassificationVector:
//       RenderType      — Color, Size, OutlineWidth, or OutlineColor
//       StartSize / EndSize — symbol size range for the Size render type
//       ClassIdField    — optional field to store the class ID per feature
//   - Managing layer statistics:
//       ForceStatisticsCalculation — auto-calculate or prompt the user
//       classifier.MustCalculateStatistics — check whether stats are needed
//       layer.Statistics.Calculate — explicit statistics computation
//   - Adding the classification result to TGIS_ControlLegend
//   - Refreshing the map with GIS.InvalidateWholeMap
//
// The sample opens the California Counties shapefile from the TatukGIS sample
// dataset by default, but allows the user to open any supported file.
//=============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;           // Core TatukGIS types (TGIS_ClassificationAbstract, TGIS_Utils, …)
using TatukGIS.NDK.WinForms;  // WinForms-specific controls (TGIS_ViewerWnd, TGIS_ControlLegend)
using TatukGIS.NDK.Common;    // Shared NDK helpers

namespace Classification
{
    /// <summary>
    /// Main application form for the Classification sample.
    /// <para>
    /// Provides a toolbar row with field, method, render-by, class-count, interval,
    /// and color controls.  Any change to any control immediately re-runs
    /// <see cref="doClassify"/> so the map updates in real time.
    /// </para>
    /// <para>
    /// The left panel hosts a <see cref="TGIS_ControlLegend"/> that shows the
    /// class legend produced by the classifier.  The right area is the
    /// <see cref="TGIS_ViewerWnd"/> map viewer.
    /// </para>
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        // ---------------------------------------------------------------
        // Designer-managed fields (do not rename — referenced by .resx)
        // ---------------------------------------------------------------
        private Panel pClassification;          // Top toolbar panel: field/method/renderby/classes
        private ComboBox cbRenderBy;            // Render type selector (Color, Size, …)
        private ComboBox cbMethod;              // Classification method selector
        private ComboBox cbField;              // Target field / band selector
        private Label lblRenderBy;
        private Label lblMethod;
        private Label lblField;
        private Button btnOpen;                 // Opens a different layer file
        private Panel pColor;                   // Second toolbar row: colors, sizes, legend toggle
        private CheckBox chkShowInLegend;       // Toggle legend population
        private TextBox tbStartSize;            // Min symbol size (Size render type)
        private TextBox tbClassIdField;         // Optional field name to persist class IDs
        private Label lblClassIdField;
        private Label lblEndSize;
        private Label lblStartSize;
        private Panel pEndColor;                // Color swatch for the highest-value class
        private Label lblEndColor;
        private Panel pStartColor;              // Color swatch for the lowest-value class
        private Label lblStartColor;
        private TextBox tbEndSize;              // Max symbol size (Size render type)
        private TGIS_ControlLegend GISLegend;  // Interactive layer legend panel
        private TGIS_ViewerWnd GIS;            // TatukGIS map viewer control

        // ---------------------------------------------------------------
        // Classification method display-string constants
        // These match the items in cbMethod and are compared against the
        // selected text to set TGIS_ClassificationMethod enum values.
        // ---------------------------------------------------------------
        const String RENDER_TYPE_SIZE          = "Size / Width";   // Vary symbol size by class
        const String RENDER_TYPE_COLOR         = "Color";           // Vary fill color by class
        const String RENDER_TYPE_OUTLINE_WIDTH = "Outline width";   // Vary outline width by class
        const String RENDER_TYPE_OUTLINE_COLOR = "Outline color";   // Vary outline color by class

        // Standard deviation interval fraction display strings
        const String STD_INTERVAL_ONE         = "1 STDEV";
        const String STD_INTERVAL_ONE_HALF    = "1/2 STDEV";
        const String STD_INTERVAL_ONE_THIRD   = "1/3 STDEV";
        const String STD_INTERVAL_ONE_QUARTER = "1/4 STDEV";

        // Classification method display strings (must match cbMethod items exactly)
        const String GIS_CLASSIFY_METHOD_DI  = "Defined Interval";                 // Fixed-width interval classes
        const String GIS_CLASSIFY_METHOD_EI  = "Equal Interval";                   // Equal data-range intervals
        const String GIS_CLASSIFY_METHOD_GI  = "Geometrical Interval";             // Geometrically progressing breaks
        const String GIS_CLASSIFY_METHOD_MN  = "Manual";                           // User-specified break values
        const String GIS_CLASSIFY_METHOD_NB  = "Natural Breaks";                   // Jenks natural breaks
        const String GIS_CLASSIFY_METHOD_KM  = "K-Means";                          // K-means clustering
        const String GIS_CLASSIFY_METHOD_KMS = "K-Means Spatial";                  // Spatially aware K-means
        const String GIS_CLASSIFY_METHOD_QN  = "Quantile";                         // Equal-count quantile classes
        const String GIS_CLASSIFY_METHOD_QR  = "Quartile";                         // Four quartile classes
        const String GIS_CLASSIFY_METHOD_SD  = "Standard Deviation";               // Std-dev symmetric classes
        const String GIS_CLASSIFY_METHOD_SDC = "Standard Deviation with Central";  // Std-dev with centre class
        const String GIS_CLASSIFY_METHOD_UNQ = "Unique";                           // One class per unique value

        private ColorDialog dlgColor;                    // System color-picker dialog
        private OpenFileDialog dlgOpen;                  // File-open dialog
        private CheckBox chkForceStatisticsCalculation;  // Auto-calc statistics before classifying
        private Panel pnlManual;                         // Panel: manual break entry controls
        private Button btnAddManualBreak;                // Apply manual breaks button
        private TextBox edtManualBreaks;                 // Comma-separated manual break values
        private Label lblManual;
        private Panel pnlInterval;                       // Panel: interval or std-dev combo
        private TextBox tbInterval;                      // Fixed interval value (DefinedInterval)
        private ComboBox cbInterval;                     // Std-dev interval fraction combo
        private Label lblInterval;
        private Panel pnlClasses;                        // Panel: number-of-classes picker
        private ComboBox cbClasses;                      // Class count selector (1–9)
        private Label lblClasses;
        private Panel pRamps;                            // Third toolbar row: color ramp controls
        private CheckBox chkColorRampName;               // Use ColorRampName string instead of object
        private ComboBox cbColorRamp;                    // Named color ramp selector
        private CheckBox chkColorRamp;                   // Enable color ramp override
        private ComboBox cbColorMapMode;                 // Continuous vs. Discrete color mapping
        private Label lblColorMapMode;
        private CheckBox chkReverse;                     // Reverse the ramp color order

        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Initialises the form components.
        /// Additional setup (opening the default layer, populating combos) happens
        /// in <see cref="WinForm_Load"/>.
        /// </summary>
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
            // pEndColor — color swatch for the highest-value class end color
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
            // pStartColor — color swatch for the lowest-value class start color
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
            // GISLegend — interactive legend linked to the GIS viewer
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
            // GIS — TatukGIS WinForms map viewer control
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
            // pRamps — third toolbar row: color-ramp controls
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
            // chkReverse — reverses the selected color ramp direction
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
            // cbColorMapMode — Continuous: smooth gradient; Discrete: per-class solid colors
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
            // chkColorRamp — enable/disable color ramp override
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
            // chkColorRampName — assign by name string rather than live ramp object
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
            // cbColorRamp — named color ramp picker populated from TGIS_Utils.GisColorRampList
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
        /// Application entry point.
        /// Configures DPI awareness and visual styles, then starts the message loop.
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

        /// <summary>
        /// Handles the Form.Load event.
        /// Positions overlay panels, builds the file-open filter, loads the
        /// default sample layer (California Counties shapefile), sets default
        /// control states, and populates the field and color-ramp pickers.
        /// <para>
        /// <c>TGIS_Utils.GisSamplesDataDirDownload()</c> returns the root path
        /// of the downloaded TatukGIS sample dataset.
        /// </para>
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            Size = new System.Drawing.Size(1200, 800);

            // Overlay the interval and manual panels at the same location as the
            // classes panel; visibility is toggled depending on the chosen method.
            pnlInterval.Location = pnlClasses.Location;
            pnlManual.Location = pnlClasses.Location;

            // Build the file-open filter from all registered TatukGIS layer formats
            dlgOpen.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.All, false);

            // Open the default sample layer and zoom to its full extent
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\USA\States\California\Counties.shp");
            GIS.FullExtent();

            // Set default selections: "Select ..." placeholder, Color render type,
            // 5 classes, first std-dev interval fraction
            cbMethod.SelectedIndex = 0;
            cbRenderBy.SelectedIndex = 1;    // Color
            cbClasses.SelectedIndex = 4;     // 5 classes
            cbInterval.SelectedIndex = 0;    // 1 STDEV

            fillCbFields();       // Populate field picker from the loaded layer
            fillCbColorRamps();   // Populate ramp picker from TGIS_Utils.GisColorRampList
        }

        /// <summary>
        /// Allows the user to open a different file for classification.
        /// After opening, zooms to the new layer's full extent and repopulates
        /// the field and color-ramp pickers.
        /// </summary>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            String path;

            if (dlgOpen.ShowDialog() != DialogResult.OK)
                return;

            path = dlgOpen.FileName;

            GIS.Open(path);
            GIS.FullExtent();

            fillCbFields();      // Refresh field list for the new layer
            fillCbColorRamps();  // Ramp list is global but re-populate to ensure sync
        }

        /// <summary>
        /// Populates <c>cbField</c> with classifiable fields from the current layer.
        /// <para>
        /// For vector layers (<see cref="TGIS_LayerVector"/>):
        /// always includes virtual fields (GIS_UID, GIS_AREA, GIS_LENGTH,
        /// GIS_CENTROID_X, GIS_CENTROID_Y), then adds numeric attribute fields
        /// of type <see cref="TGIS_FieldType.Number"/> or
        /// <see cref="TGIS_FieldType.Float"/>.
        /// </para>
        /// <para>
        /// For raster layers (<see cref="TGIS_LayerPixel"/>):
        /// adds one entry per band, since bands are the classifiable "fields"
        /// for pixel data.
        /// </para>
        /// </summary>
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

                // Virtual fields — always available; computed on-the-fly by the engine
                cbField.Items.Add("GIS_UID");        // Unique feature identifier
                cbField.Items.Add("GIS_AREA");       // Feature area (polygon layers)
                cbField.Items.Add("GIS_LENGTH");     // Feature perimeter / line length
                cbField.Items.Add("GIS_CENTROID_X"); // Centroid X coordinate
                cbField.Items.Add("GIS_CENTROID_Y"); // Centroid Y coordinate

                // Add numeric attribute fields from the layer schema
                foreach (TGIS_FieldInfo field in lv.Fields)
                {
                    switch (field.FieldType)
                    {
                        case TGIS_FieldType.Number: cbField.Items.Add(field.Name); break;
                        case TGIS_FieldType.Float:  cbField.Items.Add(field.Name); break;
                    }
                }
            }
            else if (lyr is TGIS_LayerPixel)
            {
                // For raster layers each band index acts as a classifiable "field"
                lp = lyr as TGIS_LayerPixel;
                for (int i = 1; i <= lp.BandsCount; i++)
                {
                    cbField.Items.Add(i);
                }
            }

            cbField.SelectedIndex = 0;
        }

        /// <summary>
        /// Enables or disables all color-ramp sub-controls as a unit.
        /// Called when <c>chkColorRamp</c> is toggled to keep the ramp-name
        /// checkbox, ramp picker, colormap mode, and reverse toggle in sync.
        /// </summary>
        private void setColorRampControlEnabled(Boolean _enabled)
        {
            cbColorRamp.Enabled      = _enabled;
            chkColorRampName.Enabled = _enabled;
            cbColorMapMode.Enabled   = _enabled;
            chkReverse.Enabled       = _enabled;
        }

        /// <summary>
        /// Populates <c>cbColorRamp</c> from the global
        /// <see cref="TGIS_Utils.GisColorRampList"/> registry.
        /// Pre-selects the "GreenBlue" ramp as the default.
        /// </summary>
        private void fillCbColorRamps()
        {
            String ramp_name;

            for (int i = 0; i < TGIS_Utils.GisColorRampList.Count; i++)
            {
                ramp_name = TGIS_Utils.GisColorRampList[i].Name;

                cbColorRamp.Items.Add(ramp_name);

                // Pre-select GreenBlue as the default ramp
                if (ramp_name == "GreenBlue")
                    cbColorRamp.SelectedIndex = i;
            }
        }

        /// <summary>
        /// Returns the first layer in the viewer's layer collection, or
        /// <c>null</c> if no layers have been loaded.
        /// Classification always operates on layer index 0.
        /// </summary>
        private TGIS_Layer getLayer()
        {
            TGIS_Layer res = null;

            if (GIS.Items.Count > 0)
                res = GIS.Items[0] as TGIS_Layer;

            return res;
        }

        // ---------------------------------------------------------------
        // Visibility helpers for optional control groups.
        // Each method shows or hides both the input control and its label
        // together so that only the controls relevant to the current method
        // are visible in the toolbar.
        // ---------------------------------------------------------------

        /// <summary>Shows or hides the fixed-interval text box and its label.</summary>
        private void setInterval(Boolean _val) { tbInterval.Visible = _val; lblInterval.Visible = _val; }
        private void showInterval() { setInterval(true); }
        private void hideInterval() { setInterval(false); }

        /// <summary>Shows or hides the std-dev interval fraction combo and its label.</summary>
        private void setStdDev(Boolean _val) { cbInterval.Visible = _val; lblInterval.Visible = _val; }
        private void showStdDev() { setStdDev(true); }
        private void hideStdDev() { setStdDev(false); }

        /// <summary>Shows or hides the number-of-classes combo and its label.</summary>
        private void setClasses(Boolean _val) { cbClasses.Visible = _val; lblClasses.Visible = _val; }
        private void showClasses() { setClasses(true); }
        private void hideClasses() { setClasses(false); }

        /// <summary>Shows or hides the manual break entry controls.</summary>
        private void setManual(Boolean _val) { edtManualBreaks.Visible = _val; lblManual.Visible = _val; btnAddManualBreak.Visible = _val; }
        private void showManual() { setManual(true); }
        private void hideManual() { setManual(false); }

        /// <summary>
        /// Validates the text in a size/interval edit control and re-classifies
        /// when the value is a valid float.  Called by TextChanged events on
        /// <c>tbInterval</c>, <c>tbStartSize</c>, <c>tbEndSize</c>, and
        /// <c>tbClassIdField</c>.  Only triggers <see cref="doClassify"/> when
        /// the current method or render type makes the edited field meaningful.
        /// </summary>
        private void validateEdit(object sender, EventArgs e)
        {
            float d;
            if ((cbMethod.Text.Equals(GIS_CLASSIFY_METHOD_DI)) ||
                (cbRenderBy.Text.Equals(RENDER_TYPE_SIZE)) ||
                (cbRenderBy.Text.Equals(RENDER_TYPE_OUTLINE_WIDTH)) &&
                (float.TryParse((sender as TextBox).Text, out d)))
                doClassify(sender, e);
        }

        /// <summary>
        /// Opens the color picker and applies the chosen color as the
        /// classification start color (lowest-value class) when the user
        /// clicks the start-color swatch panel.
        /// </summary>
        private void pStartColor_MouseClick(object sender, MouseEventArgs e)
        {
            if (dlgColor.ShowDialog() != DialogResult.OK) return;

            pStartColor.BackColor = dlgColor.Color;

            doClassify(sender, e);
        }

        /// <summary>
        /// Opens the color picker and applies the chosen color as the
        /// classification end color (highest-value class) when the user
        /// clicks the end-color swatch panel.
        /// </summary>
        private void pEndColor_MouseClick(object sender, MouseEventArgs e)
        {
            if (dlgColor.ShowDialog() != DialogResult.OK) return;

            pEndColor.BackColor = dlgColor.Color;

            doClassify(sender, e);
        }

        /// <summary>Re-classifies when the target field selection changes.</summary>
        private void cbField_SelectedIndexChanged(object sender, EventArgs e)
        {
            doClassify(sender, e);
        }

        /// <summary>
        /// Handles classification method changes.
        /// Resets the interval text box, updates the default color ramp and
        /// colormap mode for the newly selected method, shows or hides the
        /// relevant optional controls (class count, interval, std-dev fraction,
        /// manual breaks), then calls <see cref="doClassify"/>.
        /// <para>
        /// Methods that determine their own class count automatically
        /// (DefinedInterval, Quartile, StandardDeviation variants) hide the
        /// class-count combo; all others show it.
        /// </para>
        /// </summary>
        private void cbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            Single f;
            String method;

            // Reset interval to default if the current text is not a valid number
            if (!float.TryParse(tbInterval.Text, out f))
                tbInterval.Text = "100";

            // "Select …" placeholder — hide all optional controls
            if (cbMethod.SelectedIndex == 0)
            {
                pnlClasses.Visible = false;
                pnlInterval.Visible = false;
                pnlManual.Visible = false;
                return;
            }

            // Reset defaults before applying method-specific overrides
            cbColorRamp.SelectedIndex = cbColorRamp.Items.IndexOf("GreenBlue");
            cbColorMapMode.SelectedItem = TatukGIS.NDK.__Global.GIS_COLORMAPMODE_CONTINUOUS;

            method = cbMethod.SelectedItem.ToString();
            if (cbMethod.SelectedIndex == 0)
            {
                // Redundant guard — already handled above
                hideInterval(); hideStdDev(); hideClasses(); hideManual();
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_DI))
            {
                // Defined Interval: user specifies the interval width, class count is auto
                hideStdDev(); hideClasses(); hideManual();
                showInterval();
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_MN))
            {
                // Manual: user enters comma-separated break values
                hideInterval(); hideStdDev(); hideClasses();
                showManual();
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_QR))
            {
                // Quartile: always produces 4 classes — hide class count and interval
                hideInterval(); hideStdDev(); hideClasses(); hideManual();
                cbColorMapMode.SelectedItem = TatukGIS.NDK.TGIS_ColorRampNames.BrownGreen;
            }
            else if ((method.Equals(GIS_CLASSIFY_METHOD_SD)) ||
                     (method.Equals(GIS_CLASSIFY_METHOD_SDC)))
            {
                // Standard deviation variants: interval is a fraction of one std dev
                hideInterval(); hideClasses(); hideManual();
                showStdDev();
                cbColorMapMode.SelectedItem = TatukGIS.NDK.TGIS_ColorRampNames.BrownGreen;
            }
            else if (method.Equals(GIS_CLASSIFY_METHOD_UNQ))
            {
                // Unique: one class per distinct value — force discrete color mapping
                hideInterval(); hideClasses(); hideStdDev(); hideManual();
                setColorRampControlEnabled(true);
                chkColorRamp.Checked = true;
                cbColorRamp.SelectedItem = TGIS_ColorRampNames.Unique;
                cbColorMapMode.SelectedItem = TatukGIS.NDK.__Global.GIS_COLORMAPMODE_DISCRETE;
            }
            else
            {
                // All other methods (EqualInterval, NaturalBreaks, KMeans, Quantile, etc.)
                // allow the user to specify the number of classes
                hideInterval(); hideStdDev(); hideManual();
                showClasses();
            }

            doClassify(sender, e);
        }

        /// <summary>
        /// Core classification routine — called whenever any classification
        /// parameter changes.  Assembles a fully configured
        /// <see cref="TGIS_ClassificationAbstract"/> (or its
        /// <see cref="TGIS_ClassificationVector"/> subclass) and calls
        /// <c>Classify()</c> to apply the result to the layer's rendering
        /// parameters.
        /// <para>
        /// Workflow:
        /// <list type="number">
        ///   <item>Validate that a method is selected and a layer is loaded.</item>
        ///   <item>Optionally add a ClassIdField to the vector layer schema.</item>
        ///   <item>Create a classifier via <see cref="TGIS_ClassificationFactory.CreateClassifier"/>.</item>
        ///   <item>Set common properties: Target, NumClasses, StartColor, EndColor,
        ///         ShowLegend, Method, Interval, manual break points, ColorRamp.</item>
        ///   <item>Set vector-only properties: StartSize, EndSize, ClassIdField, RenderType.</item>
        ///   <item>Handle statistics: either force auto-calculation or prompt the user.</item>
        ///   <item>Call <c>classifier.Classify()</c> to write rendering parameters.</item>
        ///   <item>Call <c>GIS.InvalidateWholeMap()</c> to repaint the map.</item>
        /// </list>
        /// </para>
        /// </summary>
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

            // Only proceed if the user has actually selected a method
            if (cbMethod.SelectedIndex <= 0) return;

            create_field = false;
            lyr = getLayer();

            if (lyr == null) return;

            if (lyr is TGIS_LayerVector)
            {
                lv = lyr as TGIS_LayerVector;

                // If a class ID field name is provided, add it to the layer schema
                // (if it does not already exist) to store the class index per feature.
                class_id_field = tbClassIdField.Text;
                create_field = class_id_field.Length > 0;
                if (create_field && (lv.FindField(class_id_field) < 0))
                    lv.AddField(class_id_field, TGIS_FieldType.Number, 3, 0);
            }
            else if (!(lyr is TGIS_LayerPixel))
            {
                MessageBox.Show(String.Format("Layer %s is not supported", (lyr as TGIS_LayerPixel).Name));
            }

            // TGIS_ClassificationFactory.CreateClassifier inspects the layer type and
            // returns either TGIS_ClassificationVector or TGIS_ClassificationPixel.
            classifier = TGIS_ClassificationFactory.CreateClassifier(lyr);

            // --- Common properties ---

            // Target: the attribute field name (vector) or band index (pixel) to classify
            classifier.Target = cbField.SelectedItem.ToString();

            // NumClasses is automatically calculated (ignored) for methods that
            // determine their own class count: DefinedInterval, Quartile,
            // StandardDeviation, StandardDeviationWithCentral.
            classifier.NumClasses = cbClasses.SelectedIndex + 1;

            // Color gradient: StartColor = lowest value, EndColor = highest value
            classifier.StartColor = TGIS_Color.FromRGB(pStartColor.BackColor.R, pStartColor.BackColor.G, pStartColor.BackColor.B);
            classifier.EndColor   = TGIS_Color.FromRGB(pEndColor.BackColor.R,   pEndColor.BackColor.G,   pEndColor.BackColor.B);

            // ShowLegend: whether to populate the layer's legend with class entries
            classifier.ShowLegend = chkShowInLegend.Checked;

            // --- Classification method ---
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

            // --- Interval ---
            // For DefinedInterval: the fixed class width.
            // For StandardDeviation methods: overridden below with a fraction.
            float intervalVal;
            if (!float.TryParse(tbInterval.Text,
                System.Globalization.NumberStyles.AllowDecimalPoint,
                System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"),
                out intervalVal)) return;
            classifier.Interval = intervalVal;

            if ((method == GIS_CLASSIFY_METHOD_SD) || (method == GIS_CLASSIFY_METHOD_SDC))
            {
                // Standard deviation: Interval is a fraction of one standard deviation
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

            // --- Manual class breaks ---
            // Parse comma-separated break values and add each to the classifier.
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

            // --- Color ramp ---
            // Assigning a color ramp by name (ColorRampName) is preferred when
            // serialising to a project file; assigning by object (ColorRamp) gives
            // direct access to the live ramp for further customisation.
            if (chkColorRampName.Checked)
            {
                if (cbColorRamp.Text.Equals("None"))
                    classifier.ColorRampName = "";
                else
                    classifier.ColorRampName = TGIS_Utils.GisColorRampList[cbColorRamp.SelectedIndex].Name;
            }

            // NumClasses property is automatically calculated for methods:
            // DefinedInterval, Quartile, StandardDeviation(s)
            if (chkColorRamp.Checked)
            {
                // Determine whether to render colors as a smooth gradient or
                // as distinct per-class color blocks
                switch (cbColorMapMode.SelectedItem)
                {
                    case TatukGIS.NDK.__Global.GIS_COLORMAPMODE_CONTINUOUS:
                        colormap_mode = TGIS_ColorMapMode.Continuous;
                        break;
                    default:
                        colormap_mode = TGIS_ColorMapMode.Discrete;
                        break;
                }

                // Ramp can be assigned directly (ColorRamp) or by name (ColorRampName)
                ramp_name = cbColorRamp.SelectedItem.ToString();
                if (chkColorRampName.Checked)
                    classifier.ColorRampName = ramp_name;
                else
                    classifier.ColorRamp = TatukGIS.NDK.__Global.GisColorRampList().ByName(ramp_name);

                classifier.ColorRamp.DefaultColorMapMode = colormap_mode;
                classifier.ColorRamp.DefaultReverse = chkReverse.Checked;
            }
            else
            {
                classifier.ColorRamp = null;  // Use plain StartColor/EndColor gradient
            }

            // --- Vector-only parameters ---
            if (classifier is TGIS_ClassificationVector)
            {
                classifier_vec = classifier as TGIS_ClassificationVector;
                classifier_vec.StartSize    = int.Parse(tbStartSize.Text);  // Min symbol size
                classifier_vec.EndSize      = int.Parse(tbEndSize.Text);    // Max symbol size
                classifier_vec.ClassIdField = class_id_field;               // Field to store class IDs

                // Render type: how the classification is visualised per class
                render_type = cbRenderBy.SelectedItem.ToString();
                if (render_type.Equals(RENDER_TYPE_SIZE))
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.Size;
                else if (render_type.Equals(RENDER_TYPE_COLOR))
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.Color;
                else if (render_type.Equals(RENDER_TYPE_OUTLINE_WIDTH))
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.OutlineWidth;
                else if (render_type.Equals(RENDER_TYPE_OUTLINE_COLOR))
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.OutlineColor;
                else
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.Color;
            }

            /* --- Statistics management ---
               Classification algorithms (NaturalBreaks, KMeans, etc.) require layer
               statistics (min, max, mean, std dev) to be calculated first.

               ForceStatisticsCalculation = true (default):
                 The classifier automatically re-calculates statistics before each run,
                 ensuring they are always up to date at the cost of extra computation.

               ForceStatisticsCalculation = false:
                 The classifier checks MustCalculateStatistics; if statistics are
                 missing or stale, this code prompts the user whether to recalculate. */
            classifier.ForceStatisticsCalculation = chkForceStatisticsCalculation.Checked;

            if (!classifier.ForceStatisticsCalculation && classifier.MustCalculateStatistics())
            {
                DialogResult res = MessageBox.Show("Statistics need to be calculated.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (res.Equals(DialogResult.OK))
                    lyr.Statistics.Calculate();     // Compute statistics now
                else
                {
                    lyr.Statistics.ResetModified(); // Discard the stale-statistics flag
                    return;
                }
            }

            // Run the classification; if a ClassIdField was requested, persist the IDs.
            if (classifier.Classify() && create_field && (lv != null))
                lv.SaveData();

            // Repaint the viewer so the new classification colours are rendered
            GIS.InvalidateWholeMap();
        }

        /// <summary>
        /// Validates the selected render type against the layer's geometry type.
        /// Size rendering is not meaningful for polygon layers, so it is disallowed.
        /// <c>ParamsList.ClearAndSetDefaults()</c> resets any previous classification
        /// styling before the new render type is applied.
        /// </summary>
        private void cbRenderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            TGIS_Layer ll;

            ll = getLayer();

            if (ll is TGIS_LayerVector)
            {
                // Clear per-feature rendering parameters set by a previous classification
                ll.ParamsList.ClearAndSetDefaults();
                if (((ll as TGIS_LayerVector).DefaultShapeType == TGIS_ShapeType.Polygon) && (cbRenderBy.SelectedItem.ToString() == RENDER_TYPE_SIZE))
                {
                    MessageBox.Show("Method not allowed for polygons");
                    cbRenderBy.SelectedIndex = 1;  // Fall back to Color
                }
            }

            doClassify(sender, e);
        }

        // ---------------------------------------------------------------
        // Simple event-to-classify forwarders.
        // Each control change re-runs the full classification pipeline.
        // ---------------------------------------------------------------

        /// <summary>Re-classifies when the legend visibility toggle changes.</summary>
        private void chkShowInLegend_CheckedChanged(object sender, EventArgs e) { doClassify(sender, e); }

        /// <summary>Re-classifies when the interval text box value changes.</summary>
        private void tbInterval_TextChanged(object sender, EventArgs e) { doClassify(sender, e); }

        /// <summary>Re-classifies when the std-dev interval fraction selection changes.</summary>
        private void cbInterval_SelectedIndexChanged_1(object sender, EventArgs e) { doClassify(sender, e); }

        /// <summary>Re-classifies when the "Add" manual break button is clicked.</summary>
        private void btnAddManualBreak_Click(object sender, EventArgs e) { doClassify(sender, e); }

        /// <summary>
        /// Ensures the ramp combo is enabled when either the ramp-name checkbox
        /// or the main color-ramp checkbox is checked, then re-classifies.
        /// </summary>
        private void chkColorRampName_CheckedChanged(object sender, EventArgs e)
        {
            cbColorRamp.Enabled = chkColorRampName.Checked || chkColorRamp.Checked;
            doClassify(sender, e);
        }

        /// <summary>Re-classifies when the colormap mode (Continuous/Discrete) changes.</summary>
        private void cbColorMapMode_SelectedIndexChanged(object sender, EventArgs e) { doClassify(sender, e); }

        /// <summary>Re-classifies when the Force Statistics Calculation toggle changes.</summary>
        private void chkForceStatisticsCalculation_CheckedChanged(object sender, EventArgs e) { doClassify(sender, e); }

        /// <summary>Re-classifies when the color ramp reverse toggle changes.</summary>
        private void chkReverse_CheckedChanged(object sender, EventArgs e) { doClassify(sender, e); }

        /// <summary>Re-classifies when a different named color ramp is selected.</summary>
        private void cbColorRamp_SelectedIndexChanged_1(object sender, EventArgs e) { doClassify(sender, e); }

        /// <summary>
        /// Enables or disables the ramp sub-controls when the main "Use ColorRamp"
        /// checkbox is toggled.  Does NOT trigger re-classification by itself —
        /// classification is driven by the other controls.
        /// </summary>
        private void chkColorRamp_CheckedChanged(object sender, EventArgs e)
        {
            setColorRampControlEnabled(chkColorRamp.Checked);
        }

        /// <summary>Re-classifies when the number-of-classes selection changes.</summary>
        private void cbClasses_SelectedIndexChanged_2(object sender, EventArgs e) { doClassify(sender, e); }
    }
}
