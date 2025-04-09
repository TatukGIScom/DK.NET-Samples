Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Classification
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private pClassification As Panel
        Private WithEvents tbInterval As TextBox
        Private WithEvents cbInterval As ComboBox
        Private WithEvents cbClasses As ComboBox
        Private WithEvents cbRenderBy As ComboBox
        Private WithEvents cbMethod As ComboBox
        Private WithEvents cbField As ComboBox
        Private lblInterval As Label
        Private lblClasses As Label
        Private lblRenderBy As Label
        Private lblMethod As Label
        Private lblField As Label
        Private WithEvents btnOpen As Button
        Private panel1 As Panel
        Private WithEvents cbColorRamp As ComboBox
        Private WithEvents chkUseColorRamp As CheckBox
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
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.pClassification = New System.Windows.Forms.Panel()
            Me.tbInterval = New System.Windows.Forms.TextBox()
            Me.cbInterval = New System.Windows.Forms.ComboBox()
            Me.cbClasses = New System.Windows.Forms.ComboBox()
            Me.cbRenderBy = New System.Windows.Forms.ComboBox()
            Me.cbMethod = New System.Windows.Forms.ComboBox()
            Me.cbField = New System.Windows.Forms.ComboBox()
            Me.lblInterval = New System.Windows.Forms.Label()
            Me.lblClasses = New System.Windows.Forms.Label()
            Me.lblRenderBy = New System.Windows.Forms.Label()
            Me.lblMethod = New System.Windows.Forms.Label()
            Me.lblField = New System.Windows.Forms.Label()
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.tbEndSize = New System.Windows.Forms.TextBox()
            Me.cbColorRamp = New System.Windows.Forms.ComboBox()
            Me.chkUseColorRamp = New System.Windows.Forms.CheckBox()
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
            Me.pClassification.SuspendLayout()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'pClassification
            '
            Me.pClassification.Controls.Add(Me.tbInterval)
            Me.pClassification.Controls.Add(Me.cbInterval)
            Me.pClassification.Controls.Add(Me.cbClasses)
            Me.pClassification.Controls.Add(Me.cbRenderBy)
            Me.pClassification.Controls.Add(Me.cbMethod)
            Me.pClassification.Controls.Add(Me.cbField)
            Me.pClassification.Controls.Add(Me.lblInterval)
            Me.pClassification.Controls.Add(Me.lblClasses)
            Me.pClassification.Controls.Add(Me.lblRenderBy)
            Me.pClassification.Controls.Add(Me.lblMethod)
            Me.pClassification.Controls.Add(Me.lblField)
            Me.pClassification.Controls.Add(Me.btnOpen)
            Me.pClassification.Location = New System.Drawing.Point(12, 12)
            Me.pClassification.Name = "pClassification"
            Me.pClassification.Size = New System.Drawing.Size(935, 47)
            Me.pClassification.TabIndex = 0
            '
            'tbInterval
            '
            Me.tbInterval.Location = New System.Drawing.Point(796, 13)
            Me.tbInterval.Name = "tbInterval"
            Me.tbInterval.Size = New System.Drawing.Size(52, 20)
            Me.tbInterval.TabIndex = 11
            Me.tbInterval.Text = "100"
            Me.tbInterval.Visible = False
            '
            'cbInterval
            '
            Me.cbInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbInterval.FormattingEnabled = True
            Me.cbInterval.Items.AddRange(New Object() {"1 STDEV", "1/2 STDEV", "1/3 STDEV", "1/4 STDEV"})
            Me.cbInterval.Location = New System.Drawing.Point(796, 13)
            Me.cbInterval.Name = "cbInterval"
            Me.cbInterval.Size = New System.Drawing.Size(121, 21)
            Me.cbInterval.TabIndex = 10
            '
            'cbClasses
            '
            Me.cbClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbClasses.FormattingEnabled = True
            Me.cbClasses.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9"})
            Me.cbClasses.Location = New System.Drawing.Point(674, 13)
            Me.cbClasses.Name = "cbClasses"
            Me.cbClasses.Size = New System.Drawing.Size(65, 21)
            Me.cbClasses.TabIndex = 9
            '
            'cbRenderBy
            '
            Me.cbRenderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbRenderBy.FormattingEnabled = True
            Me.cbRenderBy.Items.AddRange(New Object() {"Size / Width", "Color", "Outline width", "Outline color"})
            Me.cbRenderBy.Location = New System.Drawing.Point(529, 13)
            Me.cbRenderBy.Name = "cbRenderBy"
            Me.cbRenderBy.Size = New System.Drawing.Size(87, 21)
            Me.cbRenderBy.TabIndex = 8
            '
            'cbMethod
            '
            Me.cbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbMethod.FormattingEnabled = True
            Me.cbMethod.Items.AddRange(New Object() {"Select ...", "Defined Interval", "Equal Interval", "Geometrical Interval", "Natural Breaks", "K-Means", "K-Means Spatial", "Quantile", "Quartile", "Standard Deviation", "Standard Deviation with Central", "Unique"})
            Me.cbMethod.Location = New System.Drawing.Point(337, 13)
            Me.cbMethod.Name = "cbMethod"
            Me.cbMethod.Size = New System.Drawing.Size(121, 21)
            Me.cbMethod.TabIndex = 7
            '
            'cbField
            '
            Me.cbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbField.FormattingEnabled = True
            Me.cbField.Location = New System.Drawing.Point(140, 13)
            Me.cbField.Name = "cbField"
            Me.cbField.Size = New System.Drawing.Size(136, 21)
            Me.cbField.TabIndex = 6
            '
            'lblInterval
            '
            Me.lblInterval.AutoSize = True
            Me.lblInterval.Location = New System.Drawing.Point(745, 16)
            Me.lblInterval.Name = "lblInterval"
            Me.lblInterval.Size = New System.Drawing.Size(45, 13)
            Me.lblInterval.TabIndex = 5
            Me.lblInterval.Text = "Interval:"
            '
            'lblClasses
            '
            Me.lblClasses.AutoSize = True
            Me.lblClasses.Location = New System.Drawing.Point(622, 16)
            Me.lblClasses.Name = "lblClasses"
            Me.lblClasses.Size = New System.Drawing.Size(46, 13)
            Me.lblClasses.TabIndex = 4
            Me.lblClasses.Text = "Classes:"
            '
            'lblRenderBy
            '
            Me.lblRenderBy.AutoSize = True
            Me.lblRenderBy.Location = New System.Drawing.Point(464, 16)
            Me.lblRenderBy.Name = "lblRenderBy"
            Me.lblRenderBy.Size = New System.Drawing.Size(59, 13)
            Me.lblRenderBy.TabIndex = 3
            Me.lblRenderBy.Text = "Render by:"
            '
            'lblMethod
            '
            Me.lblMethod.AutoSize = True
            Me.lblMethod.Location = New System.Drawing.Point(282, 16)
            Me.lblMethod.Name = "lblMethod"
            Me.lblMethod.Size = New System.Drawing.Size(49, 13)
            Me.lblMethod.TabIndex = 2
            Me.lblMethod.Text = "Method: "
            '
            'lblField
            '
            Me.lblField.AutoSize = True
            Me.lblField.Location = New System.Drawing.Point(102, 16)
            Me.lblField.Name = "lblField"
            Me.lblField.Size = New System.Drawing.Size(32, 13)
            Me.lblField.TabIndex = 1
            Me.lblField.Text = "Field:"
            '
            'btnOpen
            '
            Me.btnOpen.Location = New System.Drawing.Point(12, 11)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(75, 23)
            Me.btnOpen.TabIndex = 0
            Me.btnOpen.Text = "Open..."
            Me.btnOpen.UseVisualStyleBackColor = True
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.tbEndSize)
            Me.panel1.Controls.Add(Me.cbColorRamp)
            Me.panel1.Controls.Add(Me.chkUseColorRamp)
            Me.panel1.Controls.Add(Me.chkShowInLegend)
            Me.panel1.Controls.Add(Me.tbStartSize)
            Me.panel1.Controls.Add(Me.tbClassIdField)
            Me.panel1.Controls.Add(Me.lblClassIdField)
            Me.panel1.Controls.Add(Me.lblEndSize)
            Me.panel1.Controls.Add(Me.lblStartSize)
            Me.panel1.Controls.Add(Me.pEndColor)
            Me.panel1.Controls.Add(Me.lblEndColor)
            Me.panel1.Controls.Add(Me.pStartColor)
            Me.panel1.Controls.Add(Me.lblStartColor)
            Me.panel1.Location = New System.Drawing.Point(12, 65)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(935, 44)
            Me.panel1.TabIndex = 1
            '
            'tbEndSize
            '
            Me.tbEndSize.Location = New System.Drawing.Point(355, 12)
            Me.tbEndSize.Name = "tbEndSize"
            Me.tbEndSize.Size = New System.Drawing.Size(48, 20)
            Me.tbEndSize.TabIndex = 14
            Me.tbEndSize.Text = "100"
            '
            'cbColorRamp
            '
            Me.cbColorRamp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbColorRamp.Enabled = False
            Me.cbColorRamp.FormattingEnabled = True
            Me.cbColorRamp.Location = New System.Drawing.Point(796, 11)
            Me.cbColorRamp.Name = "cbColorRamp"
            Me.cbColorRamp.Size = New System.Drawing.Size(121, 21)
            Me.cbColorRamp.TabIndex = 13
            '
            'chkUseColorRamp
            '
            Me.chkUseColorRamp.AutoSize = True
            Me.chkUseColorRamp.Checked = True
            Me.chkUseColorRamp.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkUseColorRamp.Location = New System.Drawing.Point(693, 14)
            Me.chkUseColorRamp.Name = "chkUseColorRamp"
            Me.chkUseColorRamp.Size = New System.Drawing.Size(97, 17)
            Me.chkUseColorRamp.TabIndex = 12
            Me.chkUseColorRamp.Text = "Use color ramp"
            Me.chkUseColorRamp.UseVisualStyleBackColor = True
            '
            'chkShowInLegend
            '
            Me.chkShowInLegend.AutoSize = True
            Me.chkShowInLegend.Checked = True
            Me.chkShowInLegend.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkShowInLegend.Location = New System.Drawing.Point(592, 14)
            Me.chkShowInLegend.Name = "chkShowInLegend"
            Me.chkShowInLegend.Size = New System.Drawing.Size(99, 17)
            Me.chkShowInLegend.TabIndex = 11
            Me.chkShowInLegend.Text = "Show in legend"
            Me.chkShowInLegend.UseVisualStyleBackColor = True
            '
            'tbStartSize
            '
            Me.tbStartSize.Location = New System.Drawing.Point(245, 12)
            Me.tbStartSize.Name = "tbStartSize"
            Me.tbStartSize.Size = New System.Drawing.Size(48, 20)
            Me.tbStartSize.TabIndex = 9
            Me.tbStartSize.Text = "1"
            '
            'tbClassIdField
            '
            Me.tbClassIdField.Location = New System.Drawing.Point(486, 12)
            Me.tbClassIdField.Name = "tbClassIdField"
            Me.tbClassIdField.Size = New System.Drawing.Size(100, 20)
            Me.tbClassIdField.TabIndex = 8
            '
            'lblClassIdField
            '
            Me.lblClassIdField.AutoSize = True
            Me.lblClassIdField.Location = New System.Drawing.Point(409, 15)
            Me.lblClassIdField.Name = "lblClassIdField"
            Me.lblClassIdField.Size = New System.Drawing.Size(71, 13)
            Me.lblClassIdField.TabIndex = 5
            Me.lblClassIdField.Text = "Class ID field:"
            '
            'lblEndSize
            '
            Me.lblEndSize.AutoSize = True
            Me.lblEndSize.Location = New System.Drawing.Point(299, 15)
            Me.lblEndSize.Name = "lblEndSize"
            Me.lblEndSize.Size = New System.Drawing.Size(50, 13)
            Me.lblEndSize.TabIndex = 4
            Me.lblEndSize.Text = "End size:"
            '
            'lblStartSize
            '
            Me.lblStartSize.AutoSize = True
            Me.lblStartSize.Location = New System.Drawing.Point(186, 15)
            Me.lblStartSize.Name = "lblStartSize"
            Me.lblStartSize.Size = New System.Drawing.Size(53, 13)
            Me.lblStartSize.TabIndex = 3
            Me.lblStartSize.Text = "Start size:"
            '
            'pEndColor
            '
            Me.pEndColor.BackColor = System.Drawing.Color.Green
            Me.pEndColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pEndColor.Location = New System.Drawing.Point(157, 11)
            Me.pEndColor.Name = "pEndColor"
            Me.pEndColor.Size = New System.Drawing.Size(23, 21)
            Me.pEndColor.TabIndex = 2
            '
            'lblEndColor
            '
            Me.lblEndColor.AutoSize = True
            Me.lblEndColor.Location = New System.Drawing.Point(97, 15)
            Me.lblEndColor.Name = "lblEndColor"
            Me.lblEndColor.Size = New System.Drawing.Size(55, 13)
            Me.lblEndColor.TabIndex = 2
            Me.lblEndColor.Text = "End color:"
            '
            'pStartColor
            '
            Me.pStartColor.BackColor = System.Drawing.Color.FromArgb(CType(CType(233, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(237, Byte), Integer))
            Me.pStartColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.pStartColor.Location = New System.Drawing.Point(68, 11)
            Me.pStartColor.Name = "pStartColor"
            Me.pStartColor.Size = New System.Drawing.Size(23, 21)
            Me.pStartColor.TabIndex = 1
            '
            'lblStartColor
            '
            Me.lblStartColor.AutoSize = True
            Me.lblStartColor.Location = New System.Drawing.Point(9, 15)
            Me.lblStartColor.Name = "lblStartColor"
            Me.lblStartColor.Size = New System.Drawing.Size(58, 13)
            Me.lblStartColor.TabIndex = 0
            Me.lblStartColor.Text = "Start color:"
            '
            'GISLegend
            '
            Me.GISLegend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.GISLegend.CompactView = False
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GISLegend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GISLegend.GIS_Viewer = Me.GIS
            Me.GISLegend.Location = New System.Drawing.Point(12, 115)
            Me.GISLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GISLegend.Name = "GISLegend"
            Me.GISLegend.Options = CType(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GISLegend.ReverseOrder = False
            Me.GISLegend.Size = New System.Drawing.Size(211, 486)
            Me.GISLegend.TabIndex = 2
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Location = New System.Drawing.Point(229, 115)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(718, 486)
            Me.GIS.TabIndex = 3
            '
            'dlgOpen
            '
            Me.dlgOpen.FileName = "openFileDialog1"
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(959, 613)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.GISLegend)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.pClassification)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Classification"
            Me.pClassification.ResumeLayout(False)
            Me.pClassification.PerformLayout()
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            dlgOpen.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.All, False)
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\USA\States\California\Counties.shp")
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

        Private Sub validateEdit(ByVal sender As Object, ByVal e As EventArgs) Handles tbStartSize.TextChanged, tbInterval.TextChanged, tbEndSize.TextChanged, tbClassIdField.TextChanged
            Dim d As Single
            If (cbMethod.Text.Equals(GIS_CLASSIFY_METHOD_DI)) OrElse (cbRenderBy.Text.Equals(RENDER_TYPE_SIZE)) OrElse (cbRenderBy.Text.Equals(RENDER_TYPE_OUTLINE_WIDTH)) AndAlso (Single.TryParse((TryCast(sender, TextBox)).Text, d)) Then doClassify(sender, e)
        End Sub

        Private Sub pStartColor_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pStartColor.MouseClick
            If dlgColor.ShowDialog() <> DialogResult.OK Then Return
            pStartColor.BackColor = dlgColor.Color
            doClassify(sender, e)
        End Sub

        Private Sub pEndColor_MouseClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pEndColor.MouseClick
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
            method = cbMethod.SelectedItem.ToString()

            If method.Equals(GIS_CLASSIFY_METHOD_DI) Then
                tbInterval.Visible = True
                tbInterval.Enabled = True
                cbInterval.Visible = False
                cbClasses.Enabled = False
            ElseIf method.Equals(GIS_CLASSIFY_METHOD_QR) Then
                cbInterval.Visible = False
                cbClasses.Enabled = False
                tbInterval.Visible = True
                tbInterval.Enabled = False
            ElseIf method.Equals(GIS_CLASSIFY_METHOD_SD) OrElse method.Equals(GIS_CLASSIFY_METHOD_SDC) Then
                tbInterval.Visible = False
                cbInterval.Visible = True
                cbClasses.Enabled = False
            Else
                cbInterval.Visible = False
                cbClasses.Enabled = True
                tbInterval.Visible = True
                tbInterval.Enabled = False
                If method.Equals(GIS_CLASSIFY_METHOD_UNQ) Then
                    cbColorRamp.SelectedIndex = cbColorRamp.Items.IndexOf("Unique")
                Else
                    cbColorRamp.SelectedIndex = cbColorRamp.Items.IndexOf("GreenBlue")
                End If
            End If

            doClassify(sender, e)
        End Sub

        Private Sub doClassify(ByVal sender As Object, ByVal e As EventArgs)
            Dim lyr As TGIS_Layer
            Dim lv As TGIS_LayerVector = Nothing
            Dim method As String
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
                classifier.EstimateNumClasses()
            Else
                classifier.Method = TGIS_ClassificationMethod.NaturalBreaks
            End If

            classifier.Interval = Single.Parse(tbInterval.Text)

            If (method = GIS_CLASSIFY_METHOD_SD) OrElse (method = GIS_CLASSIFY_METHOD_SDC) Then
                interval = cbInterval.SelectedItem.ToString()

                If interval = STD_INTERVAL_ONE Then
                    classifier.Interval = 1
                ElseIf interval = STD_INTERVAL_ONE_HALF Then
                    classifier.Interval = 1 / 2
                ElseIf interval = STD_INTERVAL_ONE_THIRD Then
                    classifier.Interval = 1 / 3
                ElseIf interval = STD_INTERVAL_ONE_QUARTER Then
                    classifier.Interval = 1 / 4
                Else
                    classifier.Interval = 1
                End If
            End If

            If chkUseColorRamp.Checked Then
                If method = GIS_CLASSIFY_METHOD_UNQ Then
                    colormap_mode = TGIS_ColorMapMode.Discrete
                Else
                    colormap_mode = TGIS_ColorMapMode.Continuous
                End If
                classifier.ColorRamp = TGIS_Utils.GisColorRampList(cbColorRamp.SelectedIndex).RealizeColorMap(colormap_mode, classifier.NumClasses, False)
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

            If classifier.MustCalculateStatistics() Then
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

        Private Sub cbInterval_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbInterval.SelectedIndexChanged
            doClassify(sender, e)
        End Sub

        Private Sub chkUseColorRamp_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkUseColorRamp.CheckedChanged
            cbColorRamp.Enabled = Not cbColorRamp.Enabled
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
    End Class
End Namespace
