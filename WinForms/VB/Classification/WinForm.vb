Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.NDK.Common

Namespace Classification
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private pClassification As Panel
        Private WithEvents cbRenderBy As ComboBox
        Private WithEvents cbMethod As ComboBox
        Private WithEvents cbField As ComboBox
        Private lblRenderBy As Label
        Private lblMethod As Label
        Private lblField As Label
        Private WithEvents btnOpen As Button
        Private pColor As Panel
        Private WithEvents chkShowInLegend As CheckBox
        Private WithEvents tbStartSize As TextBox
        Private WithEvents tbClassIdField As TextBox
        Private lblClassIdField As Label
        Private lblEndSize As Label
        Private lblStartSize As Label
        Private WithEvents pEndColor As Panel
        Private lblEndColor As Label
        Private WithEvents pStartColor As Panel
        Private lblStartColor As Label
        Private WithEvents tbEndSize As TextBox
        Private GISLegend As TGIS_ControlLegend
        Private GIS As TGIS_ViewerWnd
        Const RENDER_TYPE_SIZE As String = "Size / Width"
        Const RENDER_TYPE_COLOR As String = "Color"
        Const RENDER_TYPE_OUTLINE_WIDTH As String = "Outline width"
        Const RENDER_TYPE_OUTLINE_COLOR As String = "Outline color"
        Const STD_INTERVAL_ONE As String = "1 STDEV"
        Const STD_INTERVAL_ONE_HALF As String = "1/2 STDEV"
        Const STD_INTERVAL_ONE_THIRD As String = "1/3 STDEV"
        Const STD_INTERVAL_ONE_QUARTER As String = "1/4 STDEV"
        Const GIS_CLASSIFY_METHOD_DI As String = "Defined Interval"
        Const GIS_CLASSIFY_METHOD_EI As String = "Equal Interval"
        Const GIS_CLASSIFY_METHOD_GI As String = "Geometrical Interval"
        Const GIS_CLASSIFY_METHOD_MN As String = "Manual"
        Const GIS_CLASSIFY_METHOD_NB As String = "Natural Breaks"
        Const GIS_CLASSIFY_METHOD_KM As String = "K-Means"
        Const GIS_CLASSIFY_METHOD_KMS As String = "K-Means Spatial"
        Const GIS_CLASSIFY_METHOD_QN As String = "Quantile"
        Const GIS_CLASSIFY_METHOD_QR As String = "Quartile"
        Const GIS_CLASSIFY_METHOD_SD As String = "Standard Deviation"
        Const GIS_CLASSIFY_METHOD_SDC As String = "Standard Deviation with Central"
        Const GIS_CLASSIFY_METHOD_UNQ As String = "Unique"
        Private dlgColor As ColorDialog
        Private dlgOpen As OpenFileDialog
        Private WithEvents chkForceStatisticsCalculation As CheckBox
        Private pnlManual As Panel
        Private WithEvents btnAddManualBreak As Button
        Private edtManualBreaks As TextBox
        Private lblManual As Label
        Private pnlInterval As Panel
        Private WithEvents tbInterval As TextBox
        Private WithEvents cbInterval As ComboBox
        Private lblInterval As Label
        Private pnlClasses As Panel
        Private WithEvents cbClasses As ComboBox
        Private lblClasses As Label
        Private pRamps As Panel
        Private WithEvents chkColorRampName As CheckBox
        Private WithEvents cbColorRamp As ComboBox
        Private WithEvents chkColorRamp As CheckBox
        Private WithEvents cbColorMapMode As ComboBox
        Private lblColorMapMode As Label
        Private WithEvents chkReverse As CheckBox
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            InitializeComponent()
        End Sub

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
            Me.pStartColor.BackColor = System.Drawing.Color.FromArgb((CInt(((CByte((233)))))), (CInt(((CByte((248)))))), (CInt(((CByte((237)))))))
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

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Size = New System.Drawing.Size(1200, 800)
            pnlInterval.Location = pnlClasses.Location
            pnlManual.Location = pnlClasses.Location
            dlgOpen.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.All, False)
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\USA\States\California\Counties.shp")
            GIS.FullExtent()
            cbMethod.SelectedIndex = 0
            cbRenderBy.SelectedIndex = 1
            cbClasses.SelectedIndex = 4
            cbInterval.SelectedIndex = 0
            fillCbFields()
            fillCbColorRamps()
        End Sub

        Private Sub btnOpen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpen.Click
            Dim path As String
            If dlgOpen.ShowDialog() <> DialogResult.OK Then Return
            path = dlgOpen.FileName
            GIS.Open(path)
            GIS.FullExtent()
            fillCbFields()
            fillCbColorRamps()
        End Sub

        Private Sub fillCbFields()
            Dim lyr As TGIS_Layer
            Dim lv As TGIS_LayerVector
            Dim lp As TGIS_LayerPixel
            cbField.Items.Clear()
            lyr = TryCast(getLayer(), TGIS_Layer)

            If TypeOf lyr Is TGIS_LayerVector Then
                lv = TryCast(lyr, TGIS_LayerVector)
                cbField.Items.Add("GIS_UID")
                cbField.Items.Add("GIS_AREA")
                cbField.Items.Add("GIS_LENGTH")
                cbField.Items.Add("GIS_CENTROID_X")
                cbField.Items.Add("GIS_CENTROID_Y")

                For Each field As TGIS_FieldInfo In lv.Fields

                    Select Case field.FieldType
                        Case TGIS_FieldType.Number
                            cbField.Items.Add(field.Name)
                        Case TGIS_FieldType.Float
                            cbField.Items.Add(field.Name)
                    End Select
                Next
            ElseIf TypeOf lyr Is TGIS_LayerPixel Then
                lp = TryCast(lyr, TGIS_LayerPixel)

                For i As Integer = 1 To lp.BandsCount
                    cbField.Items.Add(i)
                Next
            End If

            cbField.SelectedIndex = 0
        End Sub

        Private Sub setColorRampControlEnabled(ByVal _enabled As Boolean)
            cbColorRamp.Enabled = _enabled
            chkColorRampName.Enabled = _enabled
            cbColorMapMode.Enabled = _enabled
            chkReverse.Enabled = _enabled
        End Sub

        Private Sub fillCbColorRamps()
            Dim ramp_name As String

            For i As Integer = 0 To TGIS_Utils.GisColorRampList.Count - 1
                ramp_name = TGIS_Utils.GisColorRampList(i).Name
                cbColorRamp.Items.Add(ramp_name)
                If ramp_name = "GreenBlue" Then cbColorRamp.SelectedIndex = i
            Next
        End Sub

        Private Function getLayer() As TGIS_Layer
            Dim res As TGIS_Layer = Nothing
            If GIS.Items.Count > 0 Then res = TryCast(GIS.Items(0), TGIS_Layer)
            Return res
        End Function

        Private Sub setInterval(ByVal _val As Boolean)
            tbInterval.Visible = _val
            lblInterval.Visible = _val
        End Sub

        Private Sub showInterval()
            setInterval(True)
        End Sub

        Private Sub hideInterval()
            setInterval(False)
        End Sub

        Private Sub setStdDev(ByVal _val As Boolean)
            cbInterval.Visible = _val
            lblInterval.Visible = _val
        End Sub

        Private Sub showStdDev()
            setStdDev(True)
        End Sub

        Private Sub hideStdDev()
            setStdDev(False)
        End Sub

        Private Sub setClasses(ByVal _val As Boolean)
            cbClasses.Visible = _val
            lblClasses.Visible = _val
        End Sub

        Private Sub showClasses()
            setClasses(True)
        End Sub

        Private Sub hideClasses()
            setClasses(False)
        End Sub

        Private Sub setManual(ByVal _val As Boolean)
            edtManualBreaks.Visible = _val
            lblManual.Visible = _val
            btnAddManualBreak.Visible = _val
        End Sub

        Private Sub showManual()
            setManual(True)
        End Sub

        Private Sub hideManual()
            setManual(False)
        End Sub

        Private Sub validateEdit(ByVal sender As Object, ByVal e As EventArgs) Handles tbClassIdField.TextChanged, tbEndSize.TextChanged, tbStartSize.TextChanged
            Dim d As Single
            If (cbMethod.Text.Equals(GIS_CLASSIFY_METHOD_DI)) OrElse (cbRenderBy.Text.Equals(RENDER_TYPE_SIZE)) OrElse (cbRenderBy.Text.Equals(RENDER_TYPE_OUTLINE_WIDTH)) AndAlso (Single.TryParse((TryCast(sender, TextBox)).Text, d)) Then doClassify(sender, e)
        End Sub

        Private Sub pStartColor_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pStartColor.Click
            If dlgColor.ShowDialog() <> DialogResult.OK Then Return
            pStartColor.BackColor = dlgColor.Color
            doClassify(sender, e)
        End Sub

        Private Sub pEndColor_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pEndColor.Click
            If dlgColor.ShowDialog() <> DialogResult.OK Then Return
            pEndColor.BackColor = dlgColor.Color
            doClassify(sender, e)
        End Sub

        Private Sub cbField_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbField.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        Private Sub cbMethod_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbMethod.SelectedIndexChanged
            Dim f As Single
            Dim method As String
            If Not Single.TryParse(tbInterval.Text, f) Then tbInterval.Text = "100"

            If cbMethod.SelectedIndex = 0 Then
                pnlClasses.Visible = False
                pnlInterval.Visible = False
                pnlManual.Visible = False
                Return
            End If

            cbColorRamp.SelectedIndex = cbColorRamp.Items.IndexOf("GreenBlue")
            cbColorMapMode.SelectedItem = TatukGIS.NDK.__Global.GIS_COLORMAPMODE_CONTINUOUS
            method = cbMethod.SelectedItem.ToString()

            If cbMethod.SelectedIndex = 0 Then
                hideInterval()
                hideStdDev()
                hideClasses()
                hideManual()
            ElseIf method.Equals(GIS_CLASSIFY_METHOD_DI) Then
                hideStdDev()
                hideClasses()
                hideManual()
                showInterval()
            ElseIf method.Equals(GIS_CLASSIFY_METHOD_MN) Then
                hideInterval()
                hideStdDev()
                hideClasses()
                showManual()
            ElseIf method.Equals(GIS_CLASSIFY_METHOD_QR) Then
                hideInterval()
                hideStdDev()
                hideClasses()
                hideManual()
                cbColorMapMode.SelectedItem = TatukGIS.NDK.TGIS_ColorRampNames.BrownGreen
            ElseIf (method.Equals(GIS_CLASSIFY_METHOD_SD)) OrElse (method.Equals(GIS_CLASSIFY_METHOD_SDC)) Then
                hideInterval()
                hideClasses()
                hideManual()
                showStdDev()
                cbColorMapMode.SelectedItem = TatukGIS.NDK.TGIS_ColorRampNames.BrownGreen
            ElseIf method.Equals(GIS_CLASSIFY_METHOD_UNQ) Then
                hideInterval()
                hideClasses()
                hideStdDev()
                hideManual()
                setColorRampControlEnabled(True)
                chkColorRamp.Checked = True
                cbColorRamp.SelectedItem = TGIS_ColorRampNames.Unique
                cbColorMapMode.SelectedItem = TatukGIS.NDK.__Global.GIS_COLORMAPMODE_DISCRETE
            Else
                hideInterval()
                hideStdDev()
                hideManual()
                showClasses()
            End If

            doClassify(sender, e)
        End Sub

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
            If cbMethod.SelectedIndex <= 0 Then Return
            create_field = False
            lyr = getLayer()
            If lyr Is Nothing Then Return

            If TypeOf lyr Is TGIS_LayerVector Then
                lv = TryCast(lyr, TGIS_LayerVector)
                class_id_field = tbClassIdField.Text
                create_field = class_id_field.Length > 0
                If create_field AndAlso (lv.FindField(class_id_field) < 0) Then lv.AddField(class_id_field, TGIS_FieldType.Number, 3, 0)
            ElseIf Not (TypeOf lyr Is TGIS_LayerPixel) Then
                MessageBox.Show(String.Format("Layer %s is not supported", (TryCast(lyr, TGIS_LayerPixel)).Name))
            End If

            classifier = TGIS_ClassificationFactory.CreateClassifier(lyr)
            classifier.Target = cbField.SelectedItem.ToString()
            classifier.NumClasses = cbClasses.SelectedIndex + 1
            classifier.StartColor = TGIS_Color.FromRGB(pStartColor.BackColor.R, pStartColor.BackColor.G, pStartColor.BackColor.B)
            classifier.EndColor = TGIS_Color.FromRGB(pEndColor.BackColor.R, pEndColor.BackColor.G, pEndColor.BackColor.B)
            classifier.ShowLegend = chkShowInLegend.Checked
            method = cbMethod.SelectedItem.ToString()

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

            Dim intervalVal As Single
            If Not Single.TryParse(tbInterval.Text, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"), intervalVal) Then Return
            classifier.Interval = intervalVal

            If (method = GIS_CLASSIFY_METHOD_SD) OrElse (method = GIS_CLASSIFY_METHOD_SDC) Then
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

            Dim class_breaks_arr As String() = edtManualBreaks.Text.Split(","c)

            For Each class_break_str As String In class_breaks_arr
                Dim class_break_val As Single
                If Not Single.TryParse(class_break_str, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"), class_break_val) Then Return
                classifier.AddClassBreak(class_break_val)
            Next

            If chkColorRampName.Checked Then

                If cbColorRamp.Text.Equals("None") Then
                    classifier.ColorRampName = ""
                Else
                    classifier.ColorRampName = TGIS_Utils.GisColorRampList(cbColorRamp.SelectedIndex).Name
                End If
            End If

            If chkColorRamp.Checked Then

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
                    classifier.ColorRamp = TatukGIS.NDK.__Global.GisColorRampList().ByName(ramp_name)
                End If

                classifier.ColorRamp.DefaultColorMapMode = colormap_mode
                classifier.ColorRamp.DefaultReverse = chkReverse.Checked
            Else
                classifier.ColorRamp = Nothing
            End If

            If TypeOf classifier Is TGIS_ClassificationVector Then
                classifier_vec = TryCast(classifier, TGIS_ClassificationVector)
                classifier_vec.StartSize = Integer.Parse(tbStartSize.Text)
                classifier_vec.EndSize = Integer.Parse(tbEndSize.Text)
                classifier_vec.ClassIdField = class_id_field
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

            classifier.ForceStatisticsCalculation = chkForceStatisticsCalculation.Checked

            If Not classifier.ForceStatisticsCalculation AndAlso classifier.MustCalculateStatistics() Then
                Dim res As DialogResult = MessageBox.Show("Statistics need to be calculated.", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)

                If res.Equals(DialogResult.OK) Then
                    lyr.Statistics.Calculate()
                Else
                    lyr.Statistics.ResetModified()
                    Return
                End If
            End If

            If classifier.Classify() AndAlso create_field AndAlso (lv IsNot Nothing) Then
                lv.SaveData()
            End If

            GIS.InvalidateWholeMap()
        End Sub

        Private Sub cbRenderBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbRenderBy.SelectedIndexChanged
            Dim ll As TGIS_Layer
            ll = getLayer()

            If TypeOf ll Is TGIS_LayerVector Then
                ll.ParamsList.ClearAndSetDefaults()

                If ((TryCast(ll, TGIS_LayerVector)).DefaultShapeType = TGIS_ShapeType.Polygon) AndAlso (cbRenderBy.SelectedItem.ToString() = RENDER_TYPE_SIZE) Then
                    MessageBox.Show("Method not allowed for polygons")
                    cbRenderBy.SelectedIndex = 1
                End If
            End If

            doClassify(sender, e)
        End Sub

        Private Sub chkShowInLegend_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkShowInLegend.CheckedChanged
            doClassify(sender, e)
        End Sub

        Private Sub cbClasses_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbClasses.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        Private Sub cbColorRamp_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbColorRamp.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        Private Sub tbInterval_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles tbInterval.TextChanged
            doClassify(sender, e)
        End Sub

        Private Sub cbClasses_SelectedIndexChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles cbClasses.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        Private Sub cbInterval_SelectedIndexChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles cbInterval.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        Private Sub btnAddManualBreak_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddManualBreak.Click
            doClassify(sender, e)
        End Sub

        Private Sub chkColorRampName_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkColorRampName.CheckedChanged
            cbColorRamp.Enabled = chkColorRampName.Checked OrElse chkColorRamp.Checked
            doClassify(sender, e)
        End Sub

        Private Sub cbColorMapMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbColorMapMode.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        Private Sub chkForceStatisticsCalculation_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkForceStatisticsCalculation.CheckedChanged
            doClassify(sender, e)
        End Sub

        Private Sub chkReverse_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkReverse.CheckedChanged
            doClassify(sender, e)
        End Sub

        Private Sub cbColorRamp_SelectedIndexChanged_1(ByVal sender As Object, ByVal e As EventArgs) Handles cbColorRamp.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        Private Sub chkColorRamp_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkColorRamp.CheckedChanged
            setColorRampControlEnabled(chkColorRamp.Checked)
        End Sub
    End Class
End Namespace
