'==============================================================================
' This source code is a part of TatukGIS Developer Kernel.
'==============================================================================
'
' Classification sample — demonstrates thematic data classification of vector
' and raster (pixel) layers using the TatukGIS TGIS_ClassificationAbstract API.
'
' Key concepts shown:
'   - Loading a layer with TGIS_ViewerWnd.Open
'   - Creating a classifier with TGIS_ClassificationFactory.CreateClassifier,
'     which returns the appropriate subclass for the layer type
'     (TGIS_ClassificationVector or TGIS_ClassificationPixel)
'   - Setting the classification target field (numeric attribute or band index)
'   - Choosing a classification method from TGIS_ClassificationMethod:
'       DefinedInterval, EqualInterval, GeometricalInterval, Manual,
'       NaturalBreaks, KMeans, KMeansSpatial, Quantile, Quartile,
'       StandardDeviation, StandardDeviationWithCentral, Unique
'   - Controlling the visual output via:
'       StartColor / EndColor       — color gradient endpoints
'       NumClasses                  — number of class breaks (auto for some methods)
'       Interval                    — interval size (DefinedInterval / StdDev methods)
'       ColorRamp / ColorRampName   — named palette from GisColorRampList
'       ColorRamp.DefaultColorMapMode — Continuous or Discrete color mapping
'       ColorRamp.DefaultReverse    — reverse the ramp direction
'   - Vector-specific properties via TGIS_ClassificationVector:
'       RenderType      — Color, Size, OutlineWidth, or OutlineColor
'       StartSize / EndSize — symbol size range for the Size render type
'       ClassIdField    — optional field to store the class ID per feature
'   - Managing layer statistics:
'       ForceStatisticsCalculation — auto-calculate or prompt the user
'       classifier.MustCalculateStatistics — check whether stats are needed
'       layer.Statistics.Calculate — explicit statistics computation
'   - Adding the classification result to TGIS_ControlLegend
'   - Refreshing the map with GIS.InvalidateWholeMap
'
' The sample opens the California Counties shapefile from the TatukGIS sample
' dataset by default, but allows the user to open any supported file.
'==============================================================================

Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK           ' Core TatukGIS types (TGIS_ClassificationAbstract, TGIS_Utils, …)
Imports TatukGIS.NDK.WinForms  ' WinForms-specific controls (TGIS_ViewerWnd, TGIS_ControlLegend)
Imports TatukGIS.NDK.Common    ' Shared NDK helpers

Namespace Classification
    ''' <summary>
    ''' Main application form for the Classification sample.
    ''' <para>
    ''' Provides a toolbar row with field, method, render-by, class-count, interval,
    ''' and color controls.  Any change to any control immediately re-runs
    ''' <c>doClassify</c> so the map updates in real time.
    ''' </para>
    ''' <para>
    ''' The left panel hosts a <c>TGIS_ControlLegend</c> that shows the class
    ''' legend produced by the classifier.  The right area is the
    ''' <c>TGIS_ViewerWnd</c> map viewer.
    ''' </para>
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ' ---------------------------------------------------------------
        ' Designer-managed fields (do not rename — referenced by .resx)
        ' ---------------------------------------------------------------
        Private pClassification As Panel          ' Top toolbar panel: field/method/renderby/classes
        Private WithEvents cbRenderBy As ComboBox ' Render type selector (Color, Size, …)
        Private WithEvents cbMethod As ComboBox   ' Classification method selector
        Private WithEvents cbField As ComboBox    ' Target field / band selector
        Private lblRenderBy As Label
        Private lblMethod As Label
        Private lblField As Label
        Private WithEvents btnOpen As Button       ' Opens a different layer file
        Private pColor As Panel                    ' Second toolbar row: colors, sizes, legend toggle
        Private WithEvents chkShowInLegend As CheckBox  ' Toggle legend population
        Private WithEvents tbStartSize As TextBox  ' Min symbol size (Size render type)
        Private WithEvents tbClassIdField As TextBox    ' Optional field name to persist class IDs
        Private lblClassIdField As Label
        Private lblEndSize As Label
        Private lblStartSize As Label
        Private WithEvents pEndColor As Panel      ' Color swatch for the highest-value class
        Private lblEndColor As Label
        Private WithEvents pStartColor As Panel    ' Color swatch for the lowest-value class
        Private lblStartColor As Label
        Private WithEvents tbEndSize As TextBox    ' Max symbol size (Size render type)
        Private GISLegend As TGIS_ControlLegend   ' Interactive layer legend panel
        Private GIS As TGIS_ViewerWnd             ' TatukGIS map viewer control

        ' ---------------------------------------------------------------
        ' Classification method and render-type display-string constants.
        ' These match the items in the combo boxes and are compared against
        ' selected text to set TGIS_ClassificationMethod / RenderType values.
        ' ---------------------------------------------------------------
        Const RENDER_TYPE_SIZE          As String = "Size / Width"   ' Vary symbol size by class
        Const RENDER_TYPE_COLOR         As String = "Color"           ' Vary fill color by class
        Const RENDER_TYPE_OUTLINE_WIDTH As String = "Outline width"   ' Vary outline width by class
        Const RENDER_TYPE_OUTLINE_COLOR As String = "Outline color"   ' Vary outline color by class

        ' Standard deviation interval fraction display strings
        Const STD_INTERVAL_ONE         As String = "1 STDEV"
        Const STD_INTERVAL_ONE_HALF    As String = "1/2 STDEV"
        Const STD_INTERVAL_ONE_THIRD   As String = "1/3 STDEV"
        Const STD_INTERVAL_ONE_QUARTER As String = "1/4 STDEV"

        ' Classification method display strings (must match cbMethod items exactly)
        Const GIS_CLASSIFY_METHOD_DI  As String = "Defined Interval"                ' Fixed-width interval classes
        Const GIS_CLASSIFY_METHOD_EI  As String = "Equal Interval"                  ' Equal data-range intervals
        Const GIS_CLASSIFY_METHOD_GI  As String = "Geometrical Interval"            ' Geometrically progressing breaks
        Const GIS_CLASSIFY_METHOD_MN  As String = "Manual"                          ' User-specified break values
        Const GIS_CLASSIFY_METHOD_NB  As String = "Natural Breaks"                  ' Jenks natural breaks
        Const GIS_CLASSIFY_METHOD_KM  As String = "K-Means"                         ' K-means clustering
        Const GIS_CLASSIFY_METHOD_KMS As String = "K-Means Spatial"                 ' Spatially aware K-means
        Const GIS_CLASSIFY_METHOD_QN  As String = "Quantile"                        ' Equal-count quantile classes
        Const GIS_CLASSIFY_METHOD_QR  As String = "Quartile"                        ' Four quartile classes
        Const GIS_CLASSIFY_METHOD_SD  As String = "Standard Deviation"              ' Std-dev symmetric classes
        Const GIS_CLASSIFY_METHOD_SDC As String = "Standard Deviation with Central" ' Std-dev with centre class
        Const GIS_CLASSIFY_METHOD_UNQ As String = "Unique"                          ' One class per unique value

        Private dlgColor As ColorDialog           ' System color-picker dialog
        Private dlgOpen As OpenFileDialog         ' File-open dialog
        Private WithEvents chkForceStatisticsCalculation As CheckBox  ' Auto-calc statistics
        Private pnlManual As Panel                ' Panel: manual break entry controls
        Private WithEvents btnAddManualBreak As Button  ' Apply manual breaks button
        Private edtManualBreaks As TextBox        ' Comma-separated manual break values
        Private lblManual As Label
        Private pnlInterval As Panel              ' Panel: interval or std-dev combo
        Private WithEvents tbInterval As TextBox  ' Fixed interval value (DefinedInterval)
        Private WithEvents cbInterval As ComboBox ' Std-dev interval fraction combo
        Private lblInterval As Label
        Private pnlClasses As Panel               ' Panel: number-of-classes picker
        Private WithEvents cbClasses As ComboBox  ' Class count selector (1–9)
        Private lblClasses As Label
        Private pRamps As Panel                   ' Third toolbar row: color ramp controls
        Private WithEvents chkColorRampName As CheckBox  ' Use ColorRampName string instead of object
        Private WithEvents cbColorRamp As ComboBox       ' Named color ramp selector
        Private WithEvents chkColorRamp As CheckBox      ' Enable color ramp override
        Private WithEvents cbColorMapMode As ComboBox    ' Continuous vs. Discrete color mapping
        Private lblColorMapMode As Label
        Private WithEvents chkReverse As CheckBox        ' Reverse the ramp color order
        Private components As System.ComponentModel.IContainer

        ''' <summary>
        ''' Initialises the form components.
        ''' Additional setup (opening the default layer, populating combos) happens
        ''' in the WinForm_Load handler.
        ''' </summary>
        Public Sub New()
            InitializeComponent()
        End Sub

        ''' <summary>Clean up any resources being used.</summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Dim tgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.pClassification = New System.Windows.Forms.Panel()
            Me.pnlClasses = New System.Windows.Forms.Panel()
            Me.cbClasses = New System.Windows.Forms.ComboBox()
            Me.lblClasses = New System.Windows.Forms.Label()
            Me.pnlInterval = New System.Windows.Forms.Panel()
            Me.tbInterval = New System.Windows.Forms.TextBox()
            Me.cbInterval = New System.Windows.Forms.ComboBox()
            Me.lblInterval = New System.Windows.Forms.Label()
            Me.pnlManual = New System.Windows.Forms.Panel()
            Me.btnAddManualBreak = New System.Windows.Forms.Button()
            Me.edtManualBreaks = New System.Windows.Forms.TextBox()
            Me.lblManual = New System.Windows.Forms.Label()
            Me.chkForceStatisticsCalculation = New System.Windows.Forms.CheckBox()
            Me.cbRenderBy = New System.Windows.Forms.ComboBox()
            Me.cbMethod = New System.Windows.Forms.ComboBox()
            Me.cbField = New System.Windows.Forms.ComboBox()
            Me.lblRenderBy = New System.Windows.Forms.Label()
            Me.lblMethod = New System.Windows.Forms.Label()
            Me.lblField = New System.Windows.Forms.Label()
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.pColor = New System.Windows.Forms.Panel()
            Me.tbEndSize = New System.Windows.Forms.TextBox()
            Me.chkShowInLegend = New System.Windows.Forms.CheckBox()
            Me.tbStartSize = New System.Windows.Forms.TextBox()
            Me.tbClassIdField = New System.Windows.Forms.TextBox()
            Me.lblClassIdField = New System.Windows.Forms.Label()
            Me.lblEndSize = New System.Windows.Forms.Label()
            Me.lblStartSize = New System.Windows.Forms.Label()
            Me.pEndColor = New System.Windows.Forms.Panel()
            Me.lblEndColor = New System.Windows.Forms.Label()
            Me.pStartColor = New System.Windows.Forms.Panel()
            Me.lblStartColor = New System.Windows.Forms.Label()
            Me.GISLegend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.dlgColor = New System.Windows.Forms.ColorDialog()
            Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
            Me.pRamps = New System.Windows.Forms.Panel()
            Me.chkReverse = New System.Windows.Forms.CheckBox()
            Me.cbColorMapMode = New System.Windows.Forms.ComboBox()
            Me.lblColorMapMode = New System.Windows.Forms.Label()
            Me.chkColorRamp = New System.Windows.Forms.CheckBox()
            Me.chkColorRampName = New System.Windows.Forms.CheckBox()
            Me.cbColorRamp = New System.Windows.Forms.ComboBox()
            Me.pClassification.SuspendLayout()
            Me.pnlClasses.SuspendLayout()
            Me.pnlInterval.SuspendLayout()
            Me.pnlManual.SuspendLayout()
            Me.pColor.SuspendLayout()
            Me.pRamps.SuspendLayout()
            Me.SuspendLayout()
            Me.pClassification.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right)), System.Windows.Forms.AnchorStyles))
            Me.pClassification.Controls.Add(Me.pnlClasses)
            Me.pClassification.Controls.Add(Me.pnlInterval)
            Me.pClassification.Controls.Add(Me.pnlManual)
            Me.pClassification.Controls.Add(Me.chkForceStatisticsCalculation)
            Me.pClassification.Controls.Add(Me.cbRenderBy)
            Me.pClassification.Controls.Add(Me.cbMethod)
            Me.pClassification.Controls.Add(Me.cbField)
            Me.pClassification.Controls.Add(Me.lblRenderBy)
            Me.pClassification.Controls.Add(Me.lblMethod)
            Me.pClassification.Controls.Add(Me.lblField)
            Me.pClassification.Controls.Add(Me.btnOpen)
            Me.pClassification.Location = New System.Drawing.Point(9, 9)
            Me.pClassification.Margin = New System.Windows.Forms.Padding(2)
            Me.pClassification.Name = "pClassification"
            Me.pClassification.Size = New System.Drawing.Size(1364, 34)
            Me.pClassification.TabIndex = 0
            Me.pnlClasses.Controls.Add(Me.cbClasses)
            Me.pnlClasses.Controls.Add(Me.lblClasses)
            Me.pnlClasses.Location = New System.Drawing.Point(745, 0)
            Me.pnlClasses.Margin = New System.Windows.Forms.Padding(2)
            Me.pnlClasses.Name = "pnlClasses"
            Me.pnlClasses.Size = New System.Drawing.Size(200, 34)
            Me.pnlClasses.TabIndex = 16
            Me.pnlClasses.Visible = False
            Me.cbClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbClasses.FormattingEnabled = True
            Me.cbClasses.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9"})
            Me.cbClasses.Location = New System.Drawing.Point(57, 7)
            Me.cbClasses.Margin = New System.Windows.Forms.Padding(2)
            Me.cbClasses.Name = "cbClasses"
            Me.cbClasses.Size = New System.Drawing.Size(50, 24)
            Me.cbClasses.TabIndex = 11
            Me.lblClasses.AutoSize = True
            Me.lblClasses.Location = New System.Drawing.Point(7, 10)
            Me.lblClasses.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblClasses.Name = "lblClasses"
            Me.lblClasses.Size = New System.Drawing.Size(59, 16)
            Me.lblClasses.TabIndex = 10
            Me.lblClasses.Text = "Classes:"
            Me.pnlInterval.Controls.Add(Me.tbInterval)
            Me.pnlInterval.Controls.Add(Me.cbInterval)
            Me.pnlInterval.Controls.Add(Me.lblInterval)
            Me.pnlInterval.Location = New System.Drawing.Point(949, 0)
            Me.pnlInterval.Margin = New System.Windows.Forms.Padding(2)
            Me.pnlInterval.Name = "pnlInterval"
            Me.pnlInterval.Size = New System.Drawing.Size(200, 34)
            Me.pnlInterval.TabIndex = 14
            Me.tbInterval.Location = New System.Drawing.Point(58, 7)
            Me.tbInterval.Margin = New System.Windows.Forms.Padding(2)
            Me.tbInterval.Name = "tbInterval"
            Me.tbInterval.Size = New System.Drawing.Size(114, 22)
            Me.tbInterval.TabIndex = 14
            Me.tbInterval.Text = "100"
            Me.tbInterval.Visible = False
            Me.cbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbInterval.FormattingEnabled = True
            Me.cbInterval.Items.AddRange(New Object() {"1 STDEV", "1/2 STDEV", "1/3 STDEV", "1/4 STDEV"})
            Me.cbInterval.Location = New System.Drawing.Point(58, 7)
            Me.cbInterval.Margin = New System.Windows.Forms.Padding(2)
            Me.cbInterval.Name = "cbInterval"
            Me.cbInterval.Size = New System.Drawing.Size(140, 24)
            Me.cbInterval.TabIndex = 13
            Me.lblInterval.AutoSize = True
            Me.lblInterval.Location = New System.Drawing.Point(9, 10)
            Me.lblInterval.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblInterval.Name = "lblInterval"
            Me.lblInterval.Size = New System.Drawing.Size(53, 16)
            Me.lblInterval.TabIndex = 12
            Me.lblInterval.Text = "Interval:"
            Me.pnlManual.Controls.Add(Me.btnAddManualBreak)
            Me.pnlManual.Controls.Add(Me.edtManualBreaks)
            Me.pnlManual.Controls.Add(Me.lblManual)
            Me.pnlManual.Location = New System.Drawing.Point(1153, 0)
            Me.pnlManual.Margin = New System.Windows.Forms.Padding(2)
            Me.pnlManual.Name = "pnlManual"
            Me.pnlManual.Size = New System.Drawing.Size(200, 34)
            Me.pnlManual.TabIndex = 13
            Me.pnlManual.Visible = False
            Me.btnAddManualBreak.Location = New System.Drawing.Point(160, 6)
            Me.btnAddManualBreak.Margin = New System.Windows.Forms.Padding(2)
            Me.btnAddManualBreak.Name = "btnAddManualBreak"
            Me.btnAddManualBreak.Size = New System.Drawing.Size(40, 22)
            Me.btnAddManualBreak.TabIndex = 13
            Me.btnAddManualBreak.Text = "Add"
            Me.btnAddManualBreak.UseVisualStyleBackColor = True
            Me.edtManualBreaks.Location = New System.Drawing.Point(51, 7)
            Me.edtManualBreaks.Margin = New System.Windows.Forms.Padding(2)
            Me.edtManualBreaks.Name = "edtManualBreaks"
            Me.edtManualBreaks.Size = New System.Drawing.Size(102, 22)
            Me.edtManualBreaks.TabIndex = 12
            Me.edtManualBreaks.Text = "0,10.5,20,50"
            Me.lblManual.AutoSize = True
            Me.lblManual.Location = New System.Drawing.Point(2, 10)
            Me.lblManual.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblManual.Name = "lblManual"
            Me.lblManual.Size = New System.Drawing.Size(54, 16)
            Me.lblManual.TabIndex = 6
            Me.lblManual.Text = "Manual:"
            Me.chkForceStatisticsCalculation.AutoSize = True
            Me.chkForceStatisticsCalculation.Checked = True
            Me.chkForceStatisticsCalculation.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkForceStatisticsCalculation.Location = New System.Drawing.Point(436, 9)
            Me.chkForceStatisticsCalculation.Margin = New System.Windows.Forms.Padding(2)
            Me.chkForceStatisticsCalculation.Name = "chkForceStatisticsCalculation"
            Me.chkForceStatisticsCalculation.Size = New System.Drawing.Size(186, 20)
            Me.chkForceStatisticsCalculation.TabIndex = 12
            Me.chkForceStatisticsCalculation.Text = "Force Statistics Calculation"
            Me.chkForceStatisticsCalculation.UseVisualStyleBackColor = True
            Me.cbRenderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbRenderBy.FormattingEnabled = True
            Me.cbRenderBy.Items.AddRange(New Object() {"Size / Width", "Color", "Outline width", "Outline color"})
            Me.cbRenderBy.Location = New System.Drawing.Point(656, 7)
            Me.cbRenderBy.Margin = New System.Windows.Forms.Padding(2)
            Me.cbRenderBy.Name = "cbRenderBy"
            Me.cbRenderBy.Size = New System.Drawing.Size(86, 24)
            Me.cbRenderBy.TabIndex = 8
            Me.cbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbMethod.FormattingEnabled = True
            Me.cbMethod.Items.AddRange(New Object() {"Select ...", "Defined Interval", "Equal Interval", "Geometrical Interval", "Manual", "Natural Breaks", "K-Means", "K-Means Spatial", "Quantile", "Quartile", "Standard Deviation", "Standard Deviation with Central", "Unique"})
            Me.cbMethod.Location = New System.Drawing.Point(304, 7)
            Me.cbMethod.Margin = New System.Windows.Forms.Padding(2)
            Me.cbMethod.Name = "cbMethod"
            Me.cbMethod.Size = New System.Drawing.Size(122, 24)
            Me.cbMethod.TabIndex = 7
            Me.cbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbField.FormattingEnabled = True
            Me.cbField.Location = New System.Drawing.Point(118, 7)
            Me.cbField.Margin = New System.Windows.Forms.Padding(2)
            Me.cbField.Name = "cbField"
            Me.cbField.Size = New System.Drawing.Size(136, 24)
            Me.cbField.TabIndex = 6
            Me.lblRenderBy.AutoSize = True
            Me.lblRenderBy.Location = New System.Drawing.Point(593, 10)
            Me.lblRenderBy.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblRenderBy.Name = "lblRenderBy"
            Me.lblRenderBy.Size = New System.Drawing.Size(73, 16)
            Me.lblRenderBy.TabIndex = 3
            Me.lblRenderBy.Text = "Render by:"
            Me.lblMethod.AutoSize = True
            Me.lblMethod.Location = New System.Drawing.Point(259, 10)
            Me.lblMethod.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblMethod.Name = "lblMethod"
            Me.lblMethod.Size = New System.Drawing.Size(58, 16)
            Me.lblMethod.TabIndex = 2
            Me.lblMethod.Text = "Method: "
            Me.lblField.AutoSize = True
            Me.lblField.Location = New System.Drawing.Point(82, 10)
            Me.lblField.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblField.Name = "lblField"
            Me.lblField.Size = New System.Drawing.Size(40, 16)
            Me.lblField.TabIndex = 1
            Me.lblField.Text = "Field:"
            Me.btnOpen.Location = New System.Drawing.Point(2, 6)
            Me.btnOpen.Margin = New System.Windows.Forms.Padding(2)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(74, 22)
            Me.btnOpen.TabIndex = 0
            Me.btnOpen.Text = "Open..."
            Me.btnOpen.UseVisualStyleBackColor = True
            Me.pColor.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right)), System.Windows.Forms.AnchorStyles))
            Me.pColor.Controls.Add(Me.tbEndSize)
            Me.pColor.Controls.Add(Me.chkShowInLegend)
            Me.pColor.Controls.Add(Me.tbStartSize)
            Me.pColor.Controls.Add(Me.tbClassIdField)
            Me.pColor.Controls.Add(Me.lblClassIdField)
            Me.pColor.Controls.Add(Me.lblEndSize)
            Me.pColor.Controls.Add(Me.lblStartSize)
            Me.pColor.Controls.Add(Me.pEndColor)
            Me.pColor.Controls.Add(Me.lblEndColor)
            Me.pColor.Controls.Add(Me.pStartColor)
            Me.pColor.Controls.Add(Me.lblStartColor)
            Me.pColor.Location = New System.Drawing.Point(9, 47)
            Me.pColor.Margin = New System.Windows.Forms.Padding(2)
            Me.pColor.Name = "pColor"
            Me.pColor.Size = New System.Drawing.Size(1364, 34)
            Me.pColor.TabIndex = 1
            Me.tbEndSize.Location = New System.Drawing.Point(355, 8)
            Me.tbEndSize.Margin = New System.Windows.Forms.Padding(2)
            Me.tbEndSize.Name = "tbEndSize"
            Me.tbEndSize.Size = New System.Drawing.Size(48, 22)
            Me.tbEndSize.TabIndex = 14
            Me.tbEndSize.Text = "100"
            Me.chkShowInLegend.AutoSize = True
            Me.chkShowInLegend.Checked = True
            Me.chkShowInLegend.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkShowInLegend.Location = New System.Drawing.Point(595, 10)
            Me.chkShowInLegend.Margin = New System.Windows.Forms.Padding(2)
            Me.chkShowInLegend.Name = "chkShowInLegend"
            Me.chkShowInLegend.Size = New System.Drawing.Size(117, 20)
            Me.chkShowInLegend.TabIndex = 11
            Me.chkShowInLegend.Text = "Show in legend"
            Me.chkShowInLegend.UseVisualStyleBackColor = True
            Me.tbStartSize.Location = New System.Drawing.Point(246, 8)
            Me.tbStartSize.Margin = New System.Windows.Forms.Padding(2)
            Me.tbStartSize.Name = "tbStartSize"
            Me.tbStartSize.Size = New System.Drawing.Size(48, 22)
            Me.tbStartSize.TabIndex = 9
            Me.tbStartSize.Text = "1"
            Me.tbClassIdField.Location = New System.Drawing.Point(486, 8)
            Me.tbClassIdField.Margin = New System.Windows.Forms.Padding(2)
            Me.tbClassIdField.Name = "tbClassIdField"
            Me.tbClassIdField.Size = New System.Drawing.Size(100, 22)
            Me.tbClassIdField.TabIndex = 8
            Me.lblClassIdField.AutoSize = True
            Me.lblClassIdField.Location = New System.Drawing.Point(411, 11)
            Me.lblClassIdField.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblClassIdField.Name = "lblClassIdField"
            Me.lblClassIdField.Size = New System.Drawing.Size(88, 16)
            Me.lblClassIdField.TabIndex = 5
            Me.lblClassIdField.Text = "Class ID field:"
            Me.lblEndSize.AutoSize = True
            Me.lblEndSize.Location = New System.Drawing.Point(301, 11)
            Me.lblEndSize.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblEndSize.Name = "lblEndSize"
            Me.lblEndSize.Size = New System.Drawing.Size(61, 16)
            Me.lblEndSize.TabIndex = 4
            Me.lblEndSize.Text = "End size:"
            Me.lblStartSize.AutoSize = True
            Me.lblStartSize.Location = New System.Drawing.Point(186, 11)
            Me.lblStartSize.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblStartSize.Name = "lblStartSize"
            Me.lblStartSize.Size = New System.Drawing.Size(64, 16)
            Me.lblStartSize.TabIndex = 3
            Me.lblStartSize.Text = "Start size:"
            ' pEndColor — color swatch for the highest-value class end color
            Me.pEndColor.BackColor = System.Drawing.Color.Green
            Me.pEndColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pEndColor.Location = New System.Drawing.Point(158, 8)
            Me.pEndColor.Margin = New System.Windows.Forms.Padding(2)
            Me.pEndColor.Name = "pEndColor"
            Me.pEndColor.Size = New System.Drawing.Size(24, 20)
            Me.pEndColor.TabIndex = 2
            'Me.pEndColor.MouseClick += New System.Windows.Forms.MouseEventHandler(AddressOf Me.pEndColor_MouseClick)
            Me.lblEndColor.AutoSize = True
            Me.lblEndColor.Location = New System.Drawing.Point(97, 11)
            Me.lblEndColor.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblEndColor.Name = "lblEndColor"
            Me.lblEndColor.Size = New System.Drawing.Size(67, 16)
            Me.lblEndColor.TabIndex = 2
            Me.lblEndColor.Text = "End color:"
            ' pStartColor — color swatch for the lowest-value class start color
            Me.pStartColor.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((233))))), (CInt(((CByte((248)))))), (CInt(((CByte((237))))))))
            Me.pStartColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pStartColor.Location = New System.Drawing.Point(69, 8)
            Me.pStartColor.Margin = New System.Windows.Forms.Padding(2)
            Me.pStartColor.Name = "pStartColor"
            Me.pStartColor.Size = New System.Drawing.Size(24, 20)
            Me.pStartColor.TabIndex = 1
            'Me.pStartColor.MouseClick += New System.Windows.Forms.MouseEventHandler(AddressOf Me.pStartColor_MouseClick)
            Me.lblStartColor.AutoSize = True
            Me.lblStartColor.Location = New System.Drawing.Point(7, 11)
            Me.lblStartColor.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
            Me.lblStartColor.Name = "lblStartColor"
            Me.lblStartColor.Size = New System.Drawing.Size(70, 16)
            Me.lblStartColor.TabIndex = 0
            Me.lblStartColor.Text = "Start color:"
            ' GISLegend — interactive legend linked to the GIS viewer
            Me.GISLegend.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left)), System.Windows.Forms.AnchorStyles))
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GISLegend.DialogOptions = tgiS_ControlLegendDialogOptions1
            Me.GISLegend.GIS_Viewer = Me.GIS
            Me.GISLegend.Location = New System.Drawing.Point(9, 123)
            Me.GISLegend.Margin = New System.Windows.Forms.Padding(2)
            Me.GISLegend.Name = "GISLegend"
            Me.GISLegend.Options = (CType((((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)), TatukGIS.NDK.TGIS_ControlLegendOption))
            Me.GISLegend.Size = New System.Drawing.Size(215, 630)
            Me.GISLegend.TabIndex = 2
            ' GIS — TatukGIS WinForms map viewer control
            Me.GIS.Anchor = (CType(((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right)), System.Windows.Forms.AnchorStyles))
            Me.GIS.AutoStyle = True
            Me.GIS.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((255)))))), (CInt(((CByte((255)))))), (CInt(((CByte((255)))))))
            Me.GIS.Level = 1.0R
            Me.GIS.Location = New System.Drawing.Point(230, 123)
            Me.GIS.Margin = New System.Windows.Forms.Padding(2)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb((CInt(((CByte((255)))))), (CInt(((CByte((0)))))), (CInt(((CByte((0)))))))
            Me.GIS.Size = New System.Drawing.Size(1143, 628)
            Me.GIS.TabIndex = 3
            Me.GIS.TiledPaint = False
            Me.dlgOpen.FileName = "openFileDialog1"
            ' pRamps — third toolbar row: color ramp controls
            Me.pRamps.Anchor = (CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right)), System.Windows.Forms.AnchorStyles))
            Me.pRamps.Controls.Add(Me.chkReverse)
            Me.pRamps.Controls.Add(Me.cbColorMapMode)
            Me.pRamps.Controls.Add(Me.lblColorMapMode)
            Me.pRamps.Controls.Add(Me.chkColorRamp)
            Me.pRamps.Controls.Add(Me.chkColorRampName)
            Me.pRamps.Controls.Add(Me.cbColorRamp)
            Me.pRamps.Location = New System.Drawing.Point(9, 85)
            Me.pRamps.Margin = New System.Windows.Forms.Padding(2)
            Me.pRamps.Name = "pRamps"
            Me.pRamps.Size = New System.Drawing.Size(1364, 34)
            Me.pRamps.TabIndex = 16
            Me.chkReverse.AutoSize = True
            Me.chkReverse.Location = New System.Drawing.Point(755, 6)
            Me.chkReverse.Name = "chkReverse"
            Me.chkReverse.Size = New System.Drawing.Size(78, 20)
            Me.chkReverse.TabIndex = 19
            Me.chkReverse.Text = "Reverse"
            Me.chkReverse.UseVisualStyleBackColor = True
            ' cbColorMapMode — Continuous: smooth gradient; Discrete: per-class solid colors
            Me.cbColorMapMode.FormattingEnabled = True
            Me.cbColorMapMode.Items.AddRange(New Object() {"Continuous", "Discrete"})
            Me.cbColorMapMode.Location = New System.Drawing.Point(628, 4)
            Me.cbColorMapMode.Name = "cbColorMapMode"
            Me.cbColorMapMode.Size = New System.Drawing.Size(121, 24)
            Me.cbColorMapMode.TabIndex = 18
            Me.cbColorMapMode.Text = "Continous"
            Me.lblColorMapMode.AutoSize = True
            Me.lblColorMapMode.Location = New System.Drawing.Point(515, 9)
            Me.lblColorMapMode.Name = "lblColorMapMode"
            Me.lblColorMapMode.Size = New System.Drawing.Size(107, 16)
            Me.lblColorMapMode.TabIndex = 17
            Me.lblColorMapMode.Text = "Colormap Mode:"
            ' chkColorRamp — enable/disable color ramp override
            Me.chkColorRamp.AutoSize = True
            Me.chkColorRamp.Checked = True
            Me.chkColorRamp.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkColorRamp.Location = New System.Drawing.Point(4, 8)
            Me.chkColorRamp.Margin = New System.Windows.Forms.Padding(2)
            Me.chkColorRamp.Name = "chkColorRamp"
            Me.chkColorRamp.Size = New System.Drawing.Size(123, 20)
            Me.chkColorRamp.TabIndex = 16
            Me.chkColorRamp.Text = "Use ColorRamp"
            Me.chkColorRamp.UseVisualStyleBackColor = True
            ' chkColorRampName — assign by name string rather than live ramp object
            Me.chkColorRampName.AutoSize = True
            Me.chkColorRampName.Checked = True
            Me.chkColorRampName.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkColorRampName.Location = New System.Drawing.Point(131, 8)
            Me.chkColorRampName.Margin = New System.Windows.Forms.Padding(2)
            Me.chkColorRampName.Name = "chkColorRampName"
            Me.chkColorRampName.Size = New System.Drawing.Size(160, 20)
            Me.chkColorRampName.TabIndex = 15
            Me.chkColorRampName.Text = "Use ColorRampName"
            Me.chkColorRampName.UseVisualStyleBackColor = True
            ' cbColorRamp — named color ramp picker populated from TGIS_Utils.GisColorRampList
            Me.cbColorRamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbColorRamp.FormattingEnabled = True
            Me.cbColorRamp.Location = New System.Drawing.Point(295, 6)
            Me.cbColorRamp.Margin = New System.Windows.Forms.Padding(2)
            Me.cbColorRamp.Name = "cbColorRamp"
            Me.cbColorRamp.Size = New System.Drawing.Size(215, 24)
            Me.cbColorRamp.TabIndex = 13
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0F, 96.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(1384, 761)
            Me.Controls.Add(Me.pRamps)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.GISLegend)
            Me.Controls.Add(Me.pColor)
            Me.Controls.Add(Me.pClassification)
            Me.Icon = (CType((resources.GetObject("$this.Icon")), System.Drawing.Icon))
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Margin = New System.Windows.Forms.Padding(2)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Classification"
            Me.pClassification.ResumeLayout(False)
            Me.pClassification.PerformLayout()
            Me.pnlClasses.ResumeLayout(False)
            Me.pnlClasses.PerformLayout()
            Me.pnlInterval.ResumeLayout(False)
            Me.pnlInterval.PerformLayout()
            Me.pnlManual.ResumeLayout(False)
            Me.pnlManual.PerformLayout()
            Me.pColor.ResumeLayout(False)
            Me.pColor.PerformLayout()
            Me.pRamps.ResumeLayout(False)
            Me.pRamps.PerformLayout()
            Me.ResumeLayout(False)
        End Sub

        ''' <summary>
        ''' Application entry point.
        ''' Configures DPI awareness and visual styles, then starts the message loop.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Handles the Form.Load event.
        ''' Positions overlay panels, builds the file-open filter, loads the
        ''' default sample layer (California Counties shapefile), sets default
        ''' control states, and populates the field and color-ramp pickers.
        ''' <para>
        ''' <c>TGIS_Utils.GisSamplesDataDirDownload()</c> returns the root path
        ''' of the downloaded TatukGIS sample dataset.
        ''' </para>
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Size = New System.Drawing.Size(1200, 800)

            ' Overlay the interval and manual panels at the same location as the
            ' classes panel; visibility is toggled depending on the chosen method.
            pnlInterval.Location = pnlClasses.Location
            pnlManual.Location = pnlClasses.Location

            ' Build the file-open filter from all registered TatukGIS layer formats
            dlgOpen.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.All, False)

            ' Open the default sample layer and zoom to its full extent
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\USA\States\California\Counties.shp")
            GIS.FullExtent()

            ' Set default selections: "Select …" placeholder, Color render type,
            ' 5 classes, first std-dev interval fraction
            cbMethod.SelectedIndex = 0
            cbRenderBy.SelectedIndex = 1   ' Color
            cbClasses.SelectedIndex = 4    ' 5 classes
            cbInterval.SelectedIndex = 0   ' 1 STDEV

            fillCbFields()       ' Populate field picker from the loaded layer
            fillCbColorRamps()   ' Populate ramp picker from TGIS_Utils.GisColorRampList
        End Sub

        ''' <summary>
        ''' Allows the user to open a different file for classification.
        ''' After opening, zooms to the new layer's full extent and repopulates
        ''' the field and color-ramp pickers.
        ''' </summary>
        Private Sub btnOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
            Dim path As String
            If dlgOpen.ShowDialog() <> DialogResult.OK Then Return
            path = dlgOpen.FileName
            GIS.Open(path)
            GIS.FullExtent()
            fillCbFields()      ' Refresh field list for the new layer
            fillCbColorRamps()  ' Ramp list is global but re-populate to ensure sync
        End Sub

        ''' <summary>
        ''' Populates <c>cbField</c> with classifiable fields from the current layer.
        ''' <para>
        ''' For vector layers (<c>TGIS_LayerVector</c>):
        ''' always includes virtual fields (GIS_UID, GIS_AREA, GIS_LENGTH,
        ''' GIS_CENTROID_X, GIS_CENTROID_Y), then adds numeric attribute fields
        ''' of type <c>TGIS_FieldType.Number</c> or <c>TGIS_FieldType.Float</c>.
        ''' </para>
        ''' <para>
        ''' For raster layers (<c>TGIS_LayerPixel</c>):
        ''' adds one entry per band, since bands are the classifiable "fields"
        ''' for pixel data.
        ''' </para>
        ''' </summary>
        Private Sub fillCbFields()
            Dim lyr As TGIS_Layer
            Dim lv As TGIS_LayerVector
            Dim lp As TGIS_LayerPixel
            cbField.Items.Clear()
            lyr = TryCast(getLayer(), TGIS_Layer)

            If TypeOf lyr Is TGIS_LayerVector Then
                lv = TryCast(lyr, TGIS_LayerVector)

                ' Virtual fields — always available; computed on-the-fly by the engine
                cbField.Items.Add("GIS_UID")        ' Unique feature identifier
                cbField.Items.Add("GIS_AREA")       ' Feature area (polygon layers)
                cbField.Items.Add("GIS_LENGTH")     ' Feature perimeter / line length
                cbField.Items.Add("GIS_CENTROID_X") ' Centroid X coordinate
                cbField.Items.Add("GIS_CENTROID_Y") ' Centroid Y coordinate

                ' Add numeric attribute fields from the layer schema
                For Each field As TGIS_FieldInfo In lv.Fields
                    Select Case field.FieldType
                        Case TGIS_FieldType.Number
                            cbField.Items.Add(field.Name)
                        Case TGIS_FieldType.Float
                            cbField.Items.Add(field.Name)
                    End Select
                Next
            ElseIf TypeOf lyr Is TGIS_LayerPixel Then
                ' For raster layers each band index acts as a classifiable "field"
                lp = TryCast(lyr, TGIS_LayerPixel)
                For i As Integer = 1 To lp.BandsCount
                    cbField.Items.Add(i)
                Next
            End If

            cbField.SelectedIndex = 0
        End Sub

        ''' <summary>
        ''' Enables or disables all color-ramp sub-controls as a unit.
        ''' Called when <c>chkColorRamp</c> is toggled to keep the ramp-name
        ''' checkbox, ramp picker, colormap mode, and reverse toggle in sync.
        ''' </summary>
        Private Sub setColorRampControlEnabled(ByVal _enabled As Boolean)
            cbColorRamp.Enabled      = _enabled
            chkColorRampName.Enabled = _enabled
            cbColorMapMode.Enabled   = _enabled
            chkReverse.Enabled       = _enabled
        End Sub

        ''' <summary>
        ''' Populates <c>cbColorRamp</c> from the global
        ''' <c>TGIS_Utils.GisColorRampList</c> registry.
        ''' Pre-selects the "GreenBlue" ramp as the default.
        ''' </summary>
        Private Sub fillCbColorRamps()
            Dim ramp_name As String

            For i As Integer = 0 To TGIS_Utils.GisColorRampList.Count - 1
                ramp_name = TGIS_Utils.GisColorRampList(i).Name
                cbColorRamp.Items.Add(ramp_name)
                ' Pre-select GreenBlue as the default ramp
                If ramp_name = "GreenBlue" Then cbColorRamp.SelectedIndex = i
            Next
        End Sub

        ''' <summary>
        ''' Returns the first layer in the viewer's layer collection, or
        ''' <c>Nothing</c> if no layers have been loaded.
        ''' Classification always operates on layer index 0.
        ''' </summary>
        Private Function getLayer() As TGIS_Layer
            Dim res As TGIS_Layer = Nothing
            If GIS.Items.Count > 0 Then res = TryCast(GIS.Items(0), TGIS_Layer)
            Return res
        End Function

        ' ---------------------------------------------------------------
        ' Visibility helpers for optional control groups.
        ' ---------------------------------------------------------------

        ''' <summary>Shows or hides the fixed-interval text box and its label.</summary>
        Private Sub setInterval(ByVal _val As Boolean)
            tbInterval.Visible = _val
            lblInterval.Visible = _val
        End Sub
        Private Sub showInterval() : setInterval(True) : End Sub
        Private Sub hideInterval() : setInterval(False) : End Sub

        ''' <summary>Shows or hides the std-dev interval fraction combo and its label.</summary>
        Private Sub setStdDev(ByVal _val As Boolean)
            cbInterval.Visible = _val
            lblInterval.Visible = _val
        End Sub
        Private Sub showStdDev() : setStdDev(True) : End Sub
        Private Sub hideStdDev() : setStdDev(False) : End Sub

        ''' <summary>Shows or hides the number-of-classes combo and its label.</summary>
        Private Sub setClasses(ByVal _val As Boolean)
            cbClasses.Visible = _val
            lblClasses.Visible = _val
        End Sub
        Private Sub showClasses() : setClasses(True) : End Sub
        Private Sub hideClasses() : setClasses(False) : End Sub

        ''' <summary>Shows or hides the manual break entry controls.</summary>
        Private Sub setManual(ByVal _val As Boolean)
            edtManualBreaks.Visible = _val
            lblManual.Visible = _val
            btnAddManualBreak.Visible = _val
        End Sub
        Private Sub showManual() : setManual(True) : End Sub
        Private Sub hideManual() : setManual(False) : End Sub

        ''' <summary>
        ''' Validates the text in a size/interval edit control and re-classifies
        ''' when the value is a valid float.  Only triggers <c>doClassify</c> when
        ''' the current method or render type makes the edited field meaningful.
        ''' </summary>
        Private Sub validateEdit(ByVal sender As Object, ByVal e As EventArgs) Handles tbClassIdField.TextChanged, tbEndSize.TextChanged, tbStartSize.TextChanged
            Dim d As Single
            If (cbMethod.Text.Equals(GIS_CLASSIFY_METHOD_DI)) OrElse (cbRenderBy.Text.Equals(RENDER_TYPE_SIZE)) OrElse (cbRenderBy.Text.Equals(RENDER_TYPE_OUTLINE_WIDTH)) AndAlso (Single.TryParse((TryCast(sender, TextBox)).Text, d)) Then doClassify(sender, e)
        End Sub

        ''' <summary>
        ''' Opens the color picker and applies the chosen color as the
        ''' classification start color (lowest-value class).
        ''' </summary>
        Private Sub pStartColor_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pStartColor.Click
            If dlgColor.ShowDialog() <> DialogResult.OK Then Return
            pStartColor.BackColor = dlgColor.Color
            doClassify(sender, e)
        End Sub

        ''' <summary>
        ''' Opens the color picker and applies the chosen color as the
        ''' classification end color (highest-value class).
        ''' </summary>
        Private Sub pEndColor_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pEndColor.Click
            If dlgColor.ShowDialog() <> DialogResult.OK Then Return
            pEndColor.BackColor = dlgColor.Color
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies when the target field selection changes.</summary>
        Private Sub cbField_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbField.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>
        ''' Handles classification method changes.
        ''' Resets the interval text box, updates the default color ramp and
        ''' colormap mode for the newly selected method, shows or hides the
        ''' relevant optional controls (class count, interval, std-dev fraction,
        ''' manual breaks), then calls <c>doClassify</c>.
        ''' <para>
        ''' Methods that determine their own class count automatically
        ''' (DefinedInterval, Quartile, StandardDeviation variants) hide the
        ''' class-count combo; all others show it.
        ''' </para>
        ''' </summary>
        Private Sub cbMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbMethod.SelectedIndexChanged
            Dim f As Single
            Dim method As String

            ' Reset interval to default if the current text is not a valid number
            If Not Single.TryParse(tbInterval.Text, f) Then tbInterval.Text = "100"

            ' "Select …" placeholder — hide all optional controls
            If cbMethod.SelectedIndex = 0 Then
                pnlClasses.Visible = False
                pnlInterval.Visible = False
                pnlManual.Visible = False
                Return
            End If

            ' Reset defaults before applying method-specific overrides
            cbColorRamp.SelectedIndex = cbColorRamp.Items.IndexOf("GreenBlue")
            cbColorMapMode.SelectedItem = TatukGIS.NDK.__Global.GIS_COLORMAPMODE_CONTINUOUS
            method = cbMethod.SelectedItem.ToString()

            If cbMethod.SelectedIndex = 0 Then
                ' Redundant guard — already handled above
                hideInterval() : hideStdDev() : hideClasses() : hideManual()
            ElseIf method.Equals(GIS_CLASSIFY_METHOD_DI) Then
                ' Defined Interval: user specifies the interval width, class count is auto
                hideStdDev() : hideClasses() : hideManual()
                showInterval()
            ElseIf method.Equals(GIS_CLASSIFY_METHOD_MN) Then
                ' Manual: user enters comma-separated break values
                hideInterval() : hideStdDev() : hideClasses()
                showManual()
            ElseIf method.Equals(GIS_CLASSIFY_METHOD_QR) Then
                ' Quartile: always produces 4 classes — hide class count and interval
                hideInterval() : hideStdDev() : hideClasses() : hideManual()
                cbColorMapMode.SelectedItem = TatukGIS.NDK.TGIS_ColorRampNames.BrownGreen
            ElseIf (method.Equals(GIS_CLASSIFY_METHOD_SD)) OrElse (method.Equals(GIS_CLASSIFY_METHOD_SDC)) Then
                ' Standard deviation variants: interval is a fraction of one std dev
                hideInterval() : hideClasses() : hideManual()
                showStdDev()
                cbColorMapMode.SelectedItem = TatukGIS.NDK.TGIS_ColorRampNames.BrownGreen
            ElseIf method.Equals(GIS_CLASSIFY_METHOD_UNQ) Then
                ' Unique: one class per distinct value — force discrete color mapping
                hideInterval() : hideClasses() : hideStdDev() : hideManual()
                setColorRampControlEnabled(True)
                chkColorRamp.Checked = True
                cbColorRamp.SelectedItem = TGIS_ColorRampNames.Unique
                cbColorMapMode.SelectedItem = TatukGIS.NDK.__Global.GIS_COLORMAPMODE_DISCRETE
            Else
                ' All other methods allow the user to specify the number of classes
                hideInterval() : hideStdDev() : hideManual()
                showClasses()
            End If

            doClassify(sender, e)
        End Sub

        ''' <summary>
        ''' Core classification routine — called whenever any classification
        ''' parameter changes.  Assembles a fully configured
        ''' <c>TGIS_ClassificationAbstract</c> (or its
        ''' <c>TGIS_ClassificationVector</c> subclass) and calls
        ''' <c>Classify()</c> to apply the result to the layer's rendering
        ''' parameters.
        ''' <para>
        ''' Workflow: validate inputs, optionally add ClassIdField, create
        ''' classifier, set Target/NumClasses/colors/method/interval/breaks/
        ''' ColorRamp, set vector-only params, handle statistics, call Classify,
        ''' then repaint with GIS.InvalidateWholeMap.
        ''' </para>
        ''' </summary>
        Private Sub doClassify(ByVal sender As Object, ByVal e As EventArgs)
            Dim lyr As TGIS_Layer
            Dim lv As TGIS_LayerVector = Nothing
            Dim method As String
            Dim ramp_name As String
            Dim render_type As String
            Dim interval As String
            Dim class_id_field As String = ""
            Dim create_field As Boolean
            Dim classifier As TGIS_ClassificationAbstract
            Dim classifier_vec As TGIS_ClassificationVector
            Dim colormap_mode As TGIS_ColorMapMode

            ' Only proceed if the user has actually selected a method
            If cbMethod.SelectedIndex <= 0 Then Return
            create_field = False
            lyr = getLayer()
            If lyr Is Nothing Then Return

            If TypeOf lyr Is TGIS_LayerVector Then
                lv = TryCast(lyr, TGIS_LayerVector)

                ' If a class ID field name is provided, add it to the layer schema
                ' (if it does not already exist) to store the class index per feature.
                class_id_field = tbClassIdField.Text
                create_field = class_id_field.Length > 0
                If create_field AndAlso (lv.FindField(class_id_field) < 0) Then lv.AddField(class_id_field, TGIS_FieldType.Number, 3, 0)
            ElseIf Not (TypeOf lyr Is TGIS_LayerPixel) Then
                MessageBox.Show(String.Format("Layer %s is not supported", (TryCast(lyr, TGIS_LayerPixel)).Name))
            End If

            ' TGIS_ClassificationFactory.CreateClassifier inspects the layer type and
            ' returns either TGIS_ClassificationVector or TGIS_ClassificationPixel.
            classifier = TGIS_ClassificationFactory.CreateClassifier(lyr)

            ' --- Common properties ---

            ' Target: the attribute field name (vector) or band index (pixel) to classify
            classifier.Target = cbField.SelectedItem.ToString()

            ' NumClasses is automatically calculated (ignored) for methods that
            ' determine their own class count: DefinedInterval, Quartile,
            ' StandardDeviation, StandardDeviationWithCentral.
            classifier.NumClasses = cbClasses.SelectedIndex + 1

            ' Color gradient: StartColor = lowest value, EndColor = highest value
            classifier.StartColor = TGIS_Color.FromRGB(pStartColor.BackColor.R, pStartColor.BackColor.G, pStartColor.BackColor.B)
            classifier.EndColor   = TGIS_Color.FromRGB(pEndColor.BackColor.R,   pEndColor.BackColor.G,   pEndColor.BackColor.B)

            ' ShowLegend: whether to populate the layer's legend with class entries
            classifier.ShowLegend = chkShowInLegend.Checked
            method = cbMethod.SelectedItem.ToString()

            ' --- Classification method ---
            If method = GIS_CLASSIFY_METHOD_DI Then
                classifier.Method = TGIS_ClassificationMethod.DefinedInterval
            ElseIf method = GIS_CLASSIFY_METHOD_EI Then
                classifier.Method = TGIS_ClassificationMethod.EqualInterval
            ElseIf method = GIS_CLASSIFY_METHOD_GI Then
                classifier.Method = TGIS_ClassificationMethod.GeometricalInterval
            ElseIf method = GIS_CLASSIFY_METHOD_KM Then
                classifier.Method = TGIS_ClassificationMethod.KMeans
            ElseIf method = GIS_CLASSIFY_METHOD_KMS Then
                classifier.Method = TGIS_ClassificationMethod.KMeansSpatial
            ElseIf method = GIS_CLASSIFY_METHOD_MN Then
                classifier.Method = TGIS_ClassificationMethod.Manual
            ElseIf method = GIS_CLASSIFY_METHOD_NB Then
                classifier.Method = TGIS_ClassificationMethod.NaturalBreaks
            ElseIf method = GIS_CLASSIFY_METHOD_QN Then
                classifier.Method = TGIS_ClassificationMethod.Quantile
            ElseIf method = GIS_CLASSIFY_METHOD_QR Then
                classifier.Method = TGIS_ClassificationMethod.Quartile
            ElseIf method = GIS_CLASSIFY_METHOD_SD Then
                classifier.Method = TGIS_ClassificationMethod.StandardDeviation
            ElseIf method = GIS_CLASSIFY_METHOD_SDC Then
                classifier.Method = TGIS_ClassificationMethod.StandardDeviationWithCentral
            ElseIf method = GIS_CLASSIFY_METHOD_UNQ Then
                classifier.Method = TGIS_ClassificationMethod.Unique
            Else
                classifier.Method = TGIS_ClassificationMethod.NaturalBreaks
            End If

            ' --- Interval ---
            ' For DefinedInterval: the fixed class width.
            ' For StandardDeviation methods: overridden below with a fraction.
            Dim intervalVal As Single
            If Not Single.TryParse(tbInterval.Text, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"), intervalVal) Then Return
            classifier.Interval = intervalVal

            If (method = GIS_CLASSIFY_METHOD_SD) OrElse (method = GIS_CLASSIFY_METHOD_SDC) Then
                ' Standard deviation: Interval is a fraction of one standard deviation
                interval = cbInterval.SelectedItem.ToString()
                If interval = STD_INTERVAL_ONE Then
                    classifier.Interval = 1.0
                ElseIf interval = STD_INTERVAL_ONE_HALF Then
                    classifier.Interval = 1.0 / 2
                ElseIf interval = STD_INTERVAL_ONE_THIRD Then
                    classifier.Interval = 1.0 / 3
                ElseIf interval = STD_INTERVAL_ONE_QUARTER Then
                    classifier.Interval = 1.0 / 4
                Else
                    classifier.Interval = 1
                End If
            End If

            ' --- Manual class breaks ---
            ' Parse comma-separated break values and add each to the classifier.
            Dim class_breaks_arr As String() = edtManualBreaks.Text.Split(","c)
            For Each class_break_str As String In class_breaks_arr
                Dim class_break_val As Single
                If Not Single.TryParse(class_break_str, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"), class_break_val) Then Return
                classifier.AddClassBreak(class_break_val)
            Next

            ' --- Color ramp (by name) ---
            ' Assigning by name is preferred when serialising to a project file.
            If chkColorRampName.Checked Then
                If cbColorRamp.Text.Equals("None") Then
                    classifier.ColorRampName = ""
                Else
                    classifier.ColorRampName = TGIS_Utils.GisColorRampList(cbColorRamp.SelectedIndex).Name
                End If
            End If

            ' --- Color ramp (by object) ---
            If chkColorRamp.Checked Then
                ' Determine whether to render as smooth gradient or per-class solid blocks
                Select Case cbColorMapMode.SelectedItem
                    Case TatukGIS.NDK.__Global.GIS_COLORMAPMODE_CONTINUOUS
                        colormap_mode = TGIS_ColorMapMode.Continuous
                    Case Else
                        colormap_mode = TGIS_ColorMapMode.Discrete
                End Select

                ramp_name = cbColorRamp.SelectedItem.ToString()
                If chkColorRampName.Checked Then
                    classifier.ColorRampName = ramp_name
                Else
                    ' Assign the live ramp object from the global registry
                    classifier.ColorRamp = TatukGIS.NDK.__Global.GisColorRampList().ByName(ramp_name)
                End If

                classifier.ColorRamp.DefaultColorMapMode = colormap_mode
                classifier.ColorRamp.DefaultReverse = chkReverse.Checked
            Else
                classifier.ColorRamp = Nothing  ' Use plain StartColor/EndColor gradient
            End If

            ' --- Vector-only parameters ---
            If TypeOf classifier Is TGIS_ClassificationVector Then
                classifier_vec = TryCast(classifier, TGIS_ClassificationVector)
                classifier_vec.StartSize    = Integer.Parse(tbStartSize.Text)  ' Min symbol size
                classifier_vec.EndSize      = Integer.Parse(tbEndSize.Text)    ' Max symbol size
                classifier_vec.ClassIdField = class_id_field                   ' Field to store class IDs

                ' Render type: how the classification is visualised per class
                render_type = cbRenderBy.SelectedItem.ToString()
                If render_type.Equals(RENDER_TYPE_SIZE) Then
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.Size
                ElseIf render_type.Equals(RENDER_TYPE_COLOR) Then
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.Color
                ElseIf render_type.Equals(RENDER_TYPE_OUTLINE_WIDTH) Then
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.OutlineWidth
                ElseIf render_type.Equals(RENDER_TYPE_OUTLINE_COLOR) Then
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.OutlineColor
                Else
                    classifier_vec.RenderType = TGIS_ClassificationRenderType.Color
                End If
            End If

            ' --- Statistics management ---
            ' ForceStatisticsCalculation = True (default): the classifier auto-recalculates.
            ' ForceStatisticsCalculation = False: prompt the user if stats are stale.
            classifier.ForceStatisticsCalculation = chkForceStatisticsCalculation.Checked

            If Not classifier.ForceStatisticsCalculation AndAlso classifier.MustCalculateStatistics() Then
                Dim res As DialogResult = MessageBox.Show("Statistics need to be calculated.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
                If res.Equals(DialogResult.OK) Then
                    lyr.Statistics.Calculate()    ' Compute statistics now
                Else
                    lyr.Statistics.ResetModified() ' Discard the stale-statistics flag
                    Return
                End If
            End If

            ' Run the classification; if a ClassIdField was requested, persist the IDs.
            If classifier.Classify() AndAlso create_field AndAlso (lv IsNot Nothing) Then
                lv.SaveData()
            End If

            ' Repaint the viewer so the new classification colours are rendered
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>
        ''' Validates the selected render type against the layer's geometry type.
        ''' Size rendering is not meaningful for polygon layers, so it is disallowed.
        ''' <c>ParamsList.ClearAndSetDefaults()</c> resets any previous classification
        ''' styling before the new render type is applied.
        ''' </summary>
        Private Sub cbRenderBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbRenderBy.SelectedIndexChanged
            Dim ll As TGIS_Layer
            ll = getLayer()

            If TypeOf ll Is TGIS_LayerVector Then
                ' Clear per-feature rendering parameters set by a previous classification
                ll.ParamsList.ClearAndSetDefaults()
                If ((TryCast(ll, TGIS_LayerVector)).DefaultShapeType = TGIS_ShapeType.Polygon) AndAlso (cbRenderBy.SelectedItem.ToString() = RENDER_TYPE_SIZE) Then
                    MessageBox.Show("Method not allowed for polygons")
                    cbRenderBy.SelectedIndex = 1  ' Fall back to Color
                End If
            End If

            doClassify(sender, e)
        End Sub

        ' ---------------------------------------------------------------
        ' Simple event-to-classify forwarders.
        ' ---------------------------------------------------------------

        ''' <summary>Re-classifies when the legend visibility toggle changes.</summary>
        Private Sub chkShowInLegend_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowInLegend.CheckedChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies when the number-of-classes selection changes.</summary>
        Private Sub cbClasses_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbClasses.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies when a different named color ramp is selected.</summary>
        Private Sub cbColorRamp_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbColorRamp.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies when the interval text box value changes.</summary>
        Private Sub tbInterval_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tbInterval.TextChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies (duplicate handler — VB event wiring).</summary>
        Private Sub cbClasses_SelectedIndexChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles cbClasses.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies when the std-dev interval fraction selection changes.</summary>
        Private Sub cbInterval_SelectedIndexChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles cbInterval.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies when the "Add" manual break button is clicked.</summary>
        Private Sub btnAddManualBreak_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddManualBreak.Click
            doClassify(sender, e)
        End Sub

        ''' <summary>
        ''' Ensures the ramp combo is enabled when either the ramp-name checkbox
        ''' or the main color-ramp checkbox is checked, then re-classifies.
        ''' </summary>
        Private Sub chkColorRampName_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkColorRampName.CheckedChanged
            cbColorRamp.Enabled = chkColorRampName.Checked OrElse chkColorRamp.Checked
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies when the colormap mode (Continuous/Discrete) changes.</summary>
        Private Sub cbColorMapMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbColorMapMode.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies when the Force Statistics Calculation toggle changes.</summary>
        Private Sub chkForceStatisticsCalculation_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkForceStatisticsCalculation.CheckedChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies when the color ramp reverse toggle changes.</summary>
        Private Sub chkReverse_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkReverse.CheckedChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>Re-classifies when a different named color ramp is selected (duplicate handler).</summary>
        Private Sub cbColorRamp_SelectedIndexChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles cbColorRamp.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        ''' <summary>
        ''' Enables or disables the ramp sub-controls when the main "Use ColorRamp"
        ''' checkbox is toggled.
        ''' </summary>
        Private Sub chkColorRamp_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkColorRamp.CheckedChanged
            setColorRampControlEnabled(chkColorRamp.Checked)
        End Sub
    End Class
End Namespace
