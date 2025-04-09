Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL
Imports System.Diagnostics

Namespace WFSManager

    ''' <summary>
    ''' Summary description for WFSForm.
    ''' </summary>
    Public Class WFSForm
        Inherits System.Windows.Forms.Form

        Private Const GIS_EPSG_WGS84 As Integer = 4326

        Private Const GIS_INI_AXIS_ORDER_NE As String = "NE"

        Private lURL As Label

        Private WithEvents btnGetLayers As Button

        Private gbLayers As GroupBox

        Private WithEvents tvLayers As TreeView

        Private rbLayerInfo As RichTextBox

        Private gbOptions As GroupBox

        Private lbParameteres As Label

        Private tbParameters As TextBox

        Private lbVersion As Label

        Private cbxVersion As ComboBox

        Private gbGMLSettings As GroupBox

        Private lbOutputFormat As Label

        Private cbxOutputFormat As ComboBox

        Private cbReverse As CheckBox

        Private cbxCoordSys As ComboBox

        Private lbCoordSys As Label

        Private gbFiltering As GroupBox

        Private WithEvents cbMaxFeatures As CheckBox

        Private WithEvents cbStartIndex As CheckBox

        Private tbMaxFeatures As TextBox

        Private tbStartIndex As TextBox

        Private WithEvents cbBoundBoxFilter As CheckBox

        Private WithEvents cbClipByVisible As CheckBox

        Private tbYMax As TextBox

        Private tbXMax As TextBox

        Private tbXMin As TextBox

        Private tbYMin As TextBox

        Private lbXMax As Label

        Private lbYMin As Label

        Private lbYMax As Label

        Private lbXMin As Label

        Private WithEvents btnOpenURL As Button

        Private WithEvents btnAddLayer As Button

        Private WithEvents btnCancel As Button

        Private cbxURL As ComboBox

        Private wfs As TGIS_FileWFS

        Private NodeData() As Object

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

        Private cms As ContextMenuStrip

        Private WithEvents locateOnMapToolStripMenuItem As ToolStripMenuItem

        Private WithEvents openMetadataToolStripMenuItem As ToolStripMenuItem

        Private GIS As TGIS_ViewerWnd

        Private mainForm As WinForm

        Public Function getMainForm() As WinForm
            Return mainForm
        End Function

        Public Sub setMainForm(ByVal _form As WinForm)
            mainForm = _form
        End Sub

        Public Function getGIS() As TGIS_ViewerWnd
            Return GIS
        End Function

        Public Sub setGIS(ByVal _gis As TGIS_ViewerWnd)
            GIS = _gis
        End Sub

        Public Sub New()
            MyBase.New
            InitializeComponent()
        End Sub

        Public Sub New(ByVal _gis As TGIS_ViewerWnd)
            MyBase.New
            InitializeComponent()
            GIS = _gis
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If (Not (Me.components) Is Nothing) Then
                    Me.components.Dispose()
                End If

            End If

            MyBase.Dispose(disposing)
        End Sub
#Region "Windows Form Designer generated code"

        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WFSForm))
            Me.lURL = New System.Windows.Forms.Label()
            Me.btnGetLayers = New System.Windows.Forms.Button()
            Me.gbLayers = New System.Windows.Forms.GroupBox()
            Me.rbLayerInfo = New System.Windows.Forms.RichTextBox()
            Me.tvLayers = New System.Windows.Forms.TreeView()
            Me.cms = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.locateOnMapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.openMetadataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.gbOptions = New System.Windows.Forms.GroupBox()
            Me.lbCoordSys = New System.Windows.Forms.Label()
            Me.cbxCoordSys = New System.Windows.Forms.ComboBox()
            Me.gbGMLSettings = New System.Windows.Forms.GroupBox()
            Me.cbReverse = New System.Windows.Forms.CheckBox()
            Me.cbxOutputFormat = New System.Windows.Forms.ComboBox()
            Me.lbOutputFormat = New System.Windows.Forms.Label()
            Me.cbxVersion = New System.Windows.Forms.ComboBox()
            Me.lbVersion = New System.Windows.Forms.Label()
            Me.tbParameters = New System.Windows.Forms.TextBox()
            Me.lbParameteres = New System.Windows.Forms.Label()
            Me.gbFiltering = New System.Windows.Forms.GroupBox()
            Me.lbXMax = New System.Windows.Forms.Label()
            Me.lbYMin = New System.Windows.Forms.Label()
            Me.lbYMax = New System.Windows.Forms.Label()
            Me.lbXMin = New System.Windows.Forms.Label()
            Me.tbYMax = New System.Windows.Forms.TextBox()
            Me.tbXMax = New System.Windows.Forms.TextBox()
            Me.tbXMin = New System.Windows.Forms.TextBox()
            Me.tbYMin = New System.Windows.Forms.TextBox()
            Me.cbClipByVisible = New System.Windows.Forms.CheckBox()
            Me.cbBoundBoxFilter = New System.Windows.Forms.CheckBox()
            Me.tbMaxFeatures = New System.Windows.Forms.TextBox()
            Me.tbStartIndex = New System.Windows.Forms.TextBox()
            Me.cbMaxFeatures = New System.Windows.Forms.CheckBox()
            Me.cbStartIndex = New System.Windows.Forms.CheckBox()
            Me.btnOpenURL = New System.Windows.Forms.Button()
            Me.btnAddLayer = New System.Windows.Forms.Button()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.cbxURL = New System.Windows.Forms.ComboBox()
            Me.gbLayers.SuspendLayout()
            Me.cms.SuspendLayout()
            Me.gbOptions.SuspendLayout()
            Me.gbGMLSettings.SuspendLayout()
            Me.gbFiltering.SuspendLayout()
            Me.SuspendLayout()
            '
            'lURL
            '
            Me.lURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lURL.AutoSize = True
            Me.lURL.Location = New System.Drawing.Point(12, 9)
            Me.lURL.Name = "lURL"
            Me.lURL.Size = New System.Drawing.Size(32, 13)
            Me.lURL.TabIndex = 0
            Me.lURL.Text = "URL:"
            '
            'btnGetLayers
            '
            Me.btnGetLayers.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnGetLayers.Location = New System.Drawing.Point(621, 4)
            Me.btnGetLayers.Name = "btnGetLayers"
            Me.btnGetLayers.Size = New System.Drawing.Size(75, 23)
            Me.btnGetLayers.TabIndex = 2
            Me.btnGetLayers.Text = "Get layers"
            Me.btnGetLayers.UseVisualStyleBackColor = True
            '
            'gbLayers
            '
            Me.gbLayers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.gbLayers.Controls.Add(Me.rbLayerInfo)
            Me.gbLayers.Controls.Add(Me.tvLayers)
            Me.gbLayers.Location = New System.Drawing.Point(15, 32)
            Me.gbLayers.Name = "gbLayers"
            Me.gbLayers.Size = New System.Drawing.Size(681, 231)
            Me.gbLayers.TabIndex = 3
            Me.gbLayers.TabStop = False
            Me.gbLayers.Text = "Layers:"
            '
            'rbLayerInfo
            '
            Me.rbLayerInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.rbLayerInfo.Location = New System.Drawing.Point(353, 16)
            Me.rbLayerInfo.Name = "rbLayerInfo"
            Me.rbLayerInfo.Size = New System.Drawing.Size(322, 209)
            Me.rbLayerInfo.TabIndex = 1
            Me.rbLayerInfo.Text = ""
            '
            'tvLayers
            '
            Me.tvLayers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.tvLayers.ContextMenuStrip = Me.cms
            Me.tvLayers.Location = New System.Drawing.Point(3, 16)
            Me.tvLayers.Name = "tvLayers"
            Me.tvLayers.Size = New System.Drawing.Size(343, 209)
            Me.tvLayers.TabIndex = 0
            '
            'cms
            '
            Me.cms.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.locateOnMapToolStripMenuItem, Me.openMetadataToolStripMenuItem})
            Me.cms.Name = "cms"
            Me.cms.Size = New System.Drawing.Size(157, 48)
            '
            'locateOnMapToolStripMenuItem
            '
            Me.locateOnMapToolStripMenuItem.Name = "locateOnMapToolStripMenuItem"
            Me.locateOnMapToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
            Me.locateOnMapToolStripMenuItem.Text = "Locate on map"
            '
            'openMetadataToolStripMenuItem
            '
            Me.openMetadataToolStripMenuItem.Name = "openMetadataToolStripMenuItem"
            Me.openMetadataToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
            Me.openMetadataToolStripMenuItem.Text = "Open metadata"
            '
            'gbOptions
            '
            Me.gbOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.gbOptions.Controls.Add(Me.lbCoordSys)
            Me.gbOptions.Controls.Add(Me.cbxCoordSys)
            Me.gbOptions.Controls.Add(Me.gbGMLSettings)
            Me.gbOptions.Controls.Add(Me.cbxVersion)
            Me.gbOptions.Controls.Add(Me.lbVersion)
            Me.gbOptions.Controls.Add(Me.tbParameters)
            Me.gbOptions.Controls.Add(Me.lbParameteres)
            Me.gbOptions.Location = New System.Drawing.Point(13, 270)
            Me.gbOptions.Name = "gbOptions"
            Me.gbOptions.Size = New System.Drawing.Size(683, 117)
            Me.gbOptions.TabIndex = 4
            Me.gbOptions.TabStop = False
            Me.gbOptions.Text = "Options"
            '
            'lbCoordSys
            '
            Me.lbCoordSys.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lbCoordSys.AutoSize = True
            Me.lbCoordSys.Location = New System.Drawing.Point(354, 65)
            Me.lbCoordSys.Name = "lbCoordSys"
            Me.lbCoordSys.Size = New System.Drawing.Size(93, 13)
            Me.lbCoordSys.TabIndex = 6
            Me.lbCoordSys.Text = "Coordinate system"
            '
            'cbxCoordSys
            '
            Me.cbxCoordSys.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cbxCoordSys.FormattingEnabled = True
            Me.cbxCoordSys.Location = New System.Drawing.Point(355, 81)
            Me.cbxCoordSys.Name = "cbxCoordSys"
            Me.cbxCoordSys.Size = New System.Drawing.Size(322, 21)
            Me.cbxCoordSys.TabIndex = 5
            '
            'gbGMLSettings
            '
            Me.gbGMLSettings.Controls.Add(Me.cbReverse)
            Me.gbGMLSettings.Controls.Add(Me.cbxOutputFormat)
            Me.gbGMLSettings.Controls.Add(Me.lbOutputFormat)
            Me.gbGMLSettings.Location = New System.Drawing.Point(3, 50)
            Me.gbGMLSettings.Name = "gbGMLSettings"
            Me.gbGMLSettings.Size = New System.Drawing.Size(345, 61)
            Me.gbGMLSettings.TabIndex = 4
            Me.gbGMLSettings.TabStop = False
            Me.gbGMLSettings.Text = "GML Settings"
            '
            'cbReverse
            '
            Me.cbReverse.AutoSize = True
            Me.cbReverse.Location = New System.Drawing.Point(243, 26)
            Me.cbReverse.Name = "cbReverse"
            Me.cbReverse.Size = New System.Drawing.Size(88, 17)
            Me.cbReverse.TabIndex = 2
            Me.cbReverse.Text = "Reverse X/Y"
            Me.cbReverse.UseVisualStyleBackColor = True
            '
            'cbxOutputFormat
            '
            Me.cbxOutputFormat.FormattingEnabled = True
            Me.cbxOutputFormat.Location = New System.Drawing.Point(83, 24)
            Me.cbxOutputFormat.Name = "cbxOutputFormat"
            Me.cbxOutputFormat.Size = New System.Drawing.Size(147, 21)
            Me.cbxOutputFormat.TabIndex = 1
            '
            'lbOutputFormat
            '
            Me.lbOutputFormat.AutoSize = True
            Me.lbOutputFormat.Location = New System.Drawing.Point(6, 27)
            Me.lbOutputFormat.Name = "lbOutputFormat"
            Me.lbOutputFormat.Size = New System.Drawing.Size(71, 13)
            Me.lbOutputFormat.TabIndex = 0
            Me.lbOutputFormat.Text = "Output format"
            '
            'cbxVersion
            '
            Me.cbxVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cbxVersion.FormattingEnabled = True
            Me.cbxVersion.Items.AddRange(New Object() {"1.0.0", "1.1.0", "2.0.0"})
            Me.cbxVersion.Location = New System.Drawing.Point(589, 24)
            Me.cbxVersion.Name = "cbxVersion"
            Me.cbxVersion.Size = New System.Drawing.Size(88, 21)
            Me.cbxVersion.TabIndex = 3
            '
            'lbVersion
            '
            Me.lbVersion.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lbVersion.AutoSize = True
            Me.lbVersion.Location = New System.Drawing.Point(541, 27)
            Me.lbVersion.Name = "lbVersion"
            Me.lbVersion.Size = New System.Drawing.Size(42, 13)
            Me.lbVersion.TabIndex = 2
            Me.lbVersion.Text = "Version"
            '
            'tbParameters
            '
            Me.tbParameters.Location = New System.Drawing.Point(72, 24)
            Me.tbParameters.Name = "tbParameters"
            Me.tbParameters.Size = New System.Drawing.Size(463, 20)
            Me.tbParameters.TabIndex = 1
            '
            'lbParameteres
            '
            Me.lbParameteres.AutoSize = True
            Me.lbParameteres.Location = New System.Drawing.Point(6, 27)
            Me.lbParameteres.Name = "lbParameteres"
            Me.lbParameteres.Size = New System.Drawing.Size(60, 13)
            Me.lbParameteres.TabIndex = 0
            Me.lbParameteres.Text = "Parameters"
            '
            'gbFiltering
            '
            Me.gbFiltering.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.gbFiltering.Controls.Add(Me.lbXMax)
            Me.gbFiltering.Controls.Add(Me.lbYMin)
            Me.gbFiltering.Controls.Add(Me.lbYMax)
            Me.gbFiltering.Controls.Add(Me.lbXMin)
            Me.gbFiltering.Controls.Add(Me.tbYMax)
            Me.gbFiltering.Controls.Add(Me.tbXMax)
            Me.gbFiltering.Controls.Add(Me.tbXMin)
            Me.gbFiltering.Controls.Add(Me.tbYMin)
            Me.gbFiltering.Controls.Add(Me.cbClipByVisible)
            Me.gbFiltering.Controls.Add(Me.cbBoundBoxFilter)
            Me.gbFiltering.Controls.Add(Me.tbMaxFeatures)
            Me.gbFiltering.Controls.Add(Me.tbStartIndex)
            Me.gbFiltering.Controls.Add(Me.cbMaxFeatures)
            Me.gbFiltering.Controls.Add(Me.cbStartIndex)
            Me.gbFiltering.Location = New System.Drawing.Point(18, 395)
            Me.gbFiltering.Name = "gbFiltering"
            Me.gbFiltering.Size = New System.Drawing.Size(678, 119)
            Me.gbFiltering.TabIndex = 5
            Me.gbFiltering.TabStop = False
            Me.gbFiltering.Text = "Filtering"
            '
            'lbXMax
            '
            Me.lbXMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lbXMax.AutoSize = True
            Me.lbXMax.Location = New System.Drawing.Point(628, 58)
            Me.lbXMax.Name = "lbXMax"
            Me.lbXMax.Size = New System.Drawing.Size(34, 13)
            Me.lbXMax.TabIndex = 13
            Me.lbXMax.Text = "XMax"
            '
            'lbYMin
            '
            Me.lbYMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lbYMin.AutoSize = True
            Me.lbYMin.Location = New System.Drawing.Point(532, 18)
            Me.lbYMin.Name = "lbYMin"
            Me.lbYMin.Size = New System.Drawing.Size(31, 13)
            Me.lbYMin.TabIndex = 12
            Me.lbYMin.Text = "YMin"
            '
            'lbYMax
            '
            Me.lbYMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lbYMax.AutoSize = True
            Me.lbYMax.Location = New System.Drawing.Point(532, 100)
            Me.lbYMax.Name = "lbYMax"
            Me.lbYMax.Size = New System.Drawing.Size(34, 13)
            Me.lbYMax.TabIndex = 11
            Me.lbYMax.Text = "YMax"
            '
            'lbXMin
            '
            Me.lbXMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lbXMin.AutoSize = True
            Me.lbXMin.Location = New System.Drawing.Point(433, 59)
            Me.lbXMin.Name = "lbXMin"
            Me.lbXMin.Size = New System.Drawing.Size(31, 13)
            Me.lbXMin.TabIndex = 10
            Me.lbXMin.Text = "XMin"
            '
            'tbYMax
            '
            Me.tbYMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tbYMax.Enabled = False
            Me.tbYMax.Location = New System.Drawing.Point(520, 75)
            Me.tbYMax.Name = "tbYMax"
            Me.tbYMax.Size = New System.Drawing.Size(52, 20)
            Me.tbYMax.TabIndex = 9
            '
            'tbXMax
            '
            Me.tbXMax.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tbXMax.Enabled = False
            Me.tbXMax.Location = New System.Drawing.Point(571, 56)
            Me.tbXMax.Name = "tbXMax"
            Me.tbXMax.Size = New System.Drawing.Size(52, 20)
            Me.tbXMax.TabIndex = 8
            '
            'tbXMin
            '
            Me.tbXMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tbXMin.Enabled = False
            Me.tbXMin.Location = New System.Drawing.Point(469, 56)
            Me.tbXMin.Name = "tbXMin"
            Me.tbXMin.Size = New System.Drawing.Size(52, 20)
            Me.tbXMin.TabIndex = 7
            '
            'tbYMin
            '
            Me.tbYMin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tbYMin.Enabled = False
            Me.tbYMin.Location = New System.Drawing.Point(520, 36)
            Me.tbYMin.Name = "tbYMin"
            Me.tbYMin.Size = New System.Drawing.Size(52, 20)
            Me.tbYMin.TabIndex = 6
            '
            'cbClipByVisible
            '
            Me.cbClipByVisible.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cbClipByVisible.AutoSize = True
            Me.cbClipByVisible.Enabled = False
            Me.cbClipByVisible.Location = New System.Drawing.Point(352, 28)
            Me.cbClipByVisible.Name = "cbClipByVisible"
            Me.cbClipByVisible.Size = New System.Drawing.Size(121, 17)
            Me.cbClipByVisible.TabIndex = 5
            Me.cbClipByVisible.Text = "Clip by visible extent"
            Me.cbClipByVisible.UseVisualStyleBackColor = True
            '
            'cbBoundBoxFilter
            '
            Me.cbBoundBoxFilter.AutoSize = True
            Me.cbBoundBoxFilter.Location = New System.Drawing.Point(80, 74)
            Me.cbBoundBoxFilter.Name = "cbBoundBoxFilter"
            Me.cbBoundBoxFilter.Size = New System.Drawing.Size(117, 17)
            Me.cbBoundBoxFilter.TabIndex = 4
            Me.cbBoundBoxFilter.Text = "Bounding-Box-Filter"
            Me.cbBoundBoxFilter.UseVisualStyleBackColor = True
            '
            'tbMaxFeatures
            '
            Me.tbMaxFeatures.Enabled = False
            Me.tbMaxFeatures.Location = New System.Drawing.Point(202, 49)
            Me.tbMaxFeatures.Name = "tbMaxFeatures"
            Me.tbMaxFeatures.Size = New System.Drawing.Size(52, 20)
            Me.tbMaxFeatures.TabIndex = 3
            Me.tbMaxFeatures.Text = "100"
            '
            'tbStartIndex
            '
            Me.tbStartIndex.Enabled = False
            Me.tbStartIndex.Location = New System.Drawing.Point(202, 26)
            Me.tbStartIndex.Name = "tbStartIndex"
            Me.tbStartIndex.Size = New System.Drawing.Size(52, 20)
            Me.tbStartIndex.TabIndex = 2
            Me.tbStartIndex.Text = "1"
            '
            'cbMaxFeatures
            '
            Me.cbMaxFeatures.AutoSize = True
            Me.cbMaxFeatures.Location = New System.Drawing.Point(80, 51)
            Me.cbMaxFeatures.Name = "cbMaxFeatures"
            Me.cbMaxFeatures.Size = New System.Drawing.Size(111, 17)
            Me.cbMaxFeatures.TabIndex = 1
            Me.cbMaxFeatures.Text = "Maximum features"
            Me.cbMaxFeatures.UseVisualStyleBackColor = True
            '
            'cbStartIndex
            '
            Me.cbStartIndex.AutoSize = True
            Me.cbStartIndex.Location = New System.Drawing.Point(80, 28)
            Me.cbStartIndex.Name = "cbStartIndex"
            Me.cbStartIndex.Size = New System.Drawing.Size(76, 17)
            Me.cbStartIndex.TabIndex = 0
            Me.cbStartIndex.Text = "Start index"
            Me.cbStartIndex.UseVisualStyleBackColor = True
            '
            'btnOpenURL
            '
            Me.btnOpenURL.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnOpenURL.Location = New System.Drawing.Point(18, 515)
            Me.btnOpenURL.Name = "btnOpenURL"
            Me.btnOpenURL.Size = New System.Drawing.Size(75, 23)
            Me.btnOpenURL.TabIndex = 14
            Me.btnOpenURL.Text = "Open URL"
            Me.btnOpenURL.UseVisualStyleBackColor = True
            '
            'btnAddLayer
            '
            Me.btnAddLayer.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.btnAddLayer.Location = New System.Drawing.Point(286, 518)
            Me.btnAddLayer.Name = "btnAddLayer"
            Me.btnAddLayer.Size = New System.Drawing.Size(75, 23)
            Me.btnAddLayer.TabIndex = 15
            Me.btnAddLayer.Text = "Add layer"
            Me.btnAddLayer.UseVisualStyleBackColor = True
            '
            'btnCancel
            '
            Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
            Me.btnCancel.Location = New System.Drawing.Point(368, 518)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(75, 23)
            Me.btnCancel.TabIndex = 16
            Me.btnCancel.Text = "Cancel"
            Me.btnCancel.UseVisualStyleBackColor = True
            '
            'cbxURL
            '
            Me.cbxURL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.cbxURL.FormattingEnabled = True
            Me.cbxURL.Items.AddRange(New Object() {"http://geodata.nationaalgeoregister.nl/aan/wfs?version=1.0.0&request=GetCapabilities"})
            Me.cbxURL.Location = New System.Drawing.Point(50, 6)
            Me.cbxURL.Name = "cbxURL"
            Me.cbxURL.Size = New System.Drawing.Size(565, 21)
            Me.cbxURL.TabIndex = 17
            Me.cbxURL.Text = "http://geodata.nationaalgeoregister.nl/aan/wfs?version=1.0.0&request=GetCapabilities"
            '
            'WFSForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(708, 550)
            Me.Controls.Add(Me.cbxURL)
            Me.Controls.Add(Me.btnCancel)
            Me.Controls.Add(Me.btnAddLayer)
            Me.Controls.Add(Me.btnOpenURL)
            Me.Controls.Add(Me.gbFiltering)
            Me.Controls.Add(Me.gbOptions)
            Me.Controls.Add(Me.gbLayers)
            Me.Controls.Add(Me.btnGetLayers)
            Me.Controls.Add(Me.lURL)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WFSForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - WFSManager"
            Me.gbLayers.ResumeLayout(False)
            Me.cms.ResumeLayout(False)
            Me.gbOptions.ResumeLayout(False)
            Me.gbOptions.PerformLayout()
            Me.gbGMLSettings.ResumeLayout(False)
            Me.gbGMLSettings.PerformLayout()
            Me.gbFiltering.ResumeLayout(False)
            Me.gbFiltering.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        <STAThread()>
        Shared Sub Run()
            Application.Run(New WFSForm)
        End Sub

        Private Sub WFSForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If (wfs Is Nothing) Then
                wfs = New TGIS_FileWFS(Nothing, Nothing)
                wfs.TimeOut = 5000

            End If

            NodeData = New Object((8) - 1) {}
        End Sub

        Private Sub btnGetLayers_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetLayers.Click
            Dim str As String
            Dim i As Integer
            Dim root As TreeNode
            Dim node As TreeNode
            Dim fea As TGIS_WFSFeature
            str = cbxURL.Text
            If (str = "") Then
                Return
            End If

            tvLayers.Nodes.Clear()
            rbLayerInfo.Clear()
            wfs.Load(str)
            If Not String.IsNullOrEmpty(wfs.Error) Then
                Return
            End If

            If (wfs.FeaturesCount = 0) Then
                Return
            End If

            tvLayers.BeginUpdate()
            root = tvLayers.Nodes.Add(wfs.Path)
            tvLayers.SelectedNode = root
            i = 0
            Do While (i _
                        <= (wfs.FeaturesCount - 1))
                fea = wfs.Feature(i)
                node = tvLayers.SelectedNode.Nodes.Add(fea.Name)
                NodeData(i) = wfs.Feature(i)
                i = (i + 1)
            Loop

            root.Expand()
            tvLayers.EndUpdate()
            cbxOutputFormat.BeginUpdate()
            cbxOutputFormat.Items.Clear()
            i = 0
            Do While (i < wfs.DataFormats.Count)
                cbxOutputFormat.Items.Add(wfs.DataFormats(i))
                i = (i + 1)
            Loop

            cbxOutputFormat.EndUpdate()
            cbxCoordSys.BeginUpdate()
            cbxCoordSys.Items.Clear()
            cbxCoordSys.EndUpdate()
            tbXMax.Text = ""
            tbXMin.Text = ""
            tbYMax.Text = ""
            tbYMin.Text = ""
        End Sub

        Private Sub tvLayers_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles tvLayers.AfterSelect
            Dim fea As TGIS_WFSFeature
            Dim ext As TGIS_Extent
            Dim i As Integer
            If ((Not (e.Node) Is Nothing) _
                        AndAlso (e.Node.Level > 0)) Then
                fea = CType(NodeData(e.Node.Index), TGIS_WFSFeature)
                rbLayerInfo.Clear()
                rbLayerInfo.AppendText(("Name : " + fea.Name))
                rbLayerInfo.AppendText("\nTitle : " + fea.Title)
                If (fea.Description <> "") Then
                    rbLayerInfo.AppendText("\nDescription : " + fea.Description)
                Else
                    rbLayerInfo.AppendText("\nKeywords : " + fea.Keywords)
                End If

                rbLayerInfo.AppendText("\nDefault SRS : " + fea.DefaultSRS)
                rbLayerInfo.AppendText("\nWGS84 Bounding Box : ")
                rbLayerInfo.AppendText(String.Format("\n{0:f2}, {1:f2}, {2:f2}, {3:f2}", fea.WGS84BBox.XMin, fea.WGS84BBox.YMin, fea.WGS84BBox.XMax, fea.WGS84BBox.YMax))
                rbLayerInfo.Update()
                ext = getBBoxExtent(fea)
                If Not TGIS_Utils.GisIsNoWorld(ext) Then
                    tbXMax.Text = ext.XMin.ToString
                    tbYMax.Text = ext.YMax.ToString
                    tbXMin.Text = ext.XMax.ToString
                    tbYMin.Text = ext.YMax.ToString
                Else
                    tbXMax.Text = ""
                    tbYMax.Text = ""
                    tbXMin.Text = ""
                    tbYMin.Text = ""
                End If

                cbxCoordSys.Text = ""
                cbxCoordSys.BeginUpdate()
                cbxCoordSys.Items.Clear()
                cbxCoordSys.Items.Add(fea.DefaultSRS)
                i = 0
                Do While (i _
                            <= (fea.OtherSRS.Count - 1))
                    cbxCoordSys.Items.Add(fea.OtherSRS(i))
                    i = (i + 1)
                Loop

                cbxCoordSys.EndUpdate()
            Else
                rbLayerInfo.Clear()
                i = 0
                Do While (i _
                            <= (wfs.ServiceInfo.Count - 1))
                    rbLayerInfo.AppendText("\n" + wfs.ServiceInfo(i))
                    i = (i + 1)
                Loop

                rbLayerInfo.Update()
            End If

        End Sub

        Private Function getSelectedFeature() As TGIS_WFSFeature
            If ((Not (tvLayers.SelectedNode) Is Nothing) _
                        AndAlso (tvLayers.SelectedNode.Level > 0)) Then
                Return CType(NodeData(tvLayers.SelectedNode.Index), TGIS_WFSFeature)
            Else
                Return Nothing
            End If

        End Function

        Private Function getBBoxExtent(ByVal _fea As TGIS_WFSFeature) As TGIS_Extent
            Dim wgs As TGIS_CSCoordinateSystem
            Dim lcs As TGIS_CSCoordinateSystem
            Dim final As TGIS_Extent
            final = TGIS_Utils.GisNoWorld
            If (_fea Is Nothing) Then
                Return final
            End If

            Try
                If (cbxCoordSys.SelectedIndex > -1) Then
                    lcs = TGIS_CSFactory.ByWKT(cbxCoordSys.Text)
                Else
                    lcs = TGIS_CSFactory.ByWKT(_fea.DefaultSRS)
                End If

            Catch err As System.Exception
                lcs = TGIS_Utils.CSUnknownCoordinateSystem
            End Try

            If Not cbClipByVisible.Checked Then
                wgs = TGIS_CSFactory.ByEPSG(GIS_EPSG_WGS84)
                If Not (TypeOf lcs Is TGIS_CSUnknownCoordinateSystem) Then
                    final = lcs.ExtentFromCS(wgs, _fea.WGS84BBox)
                    If (lcs.Error <> 0) Then
                        final = TGIS_Utils.GisNoWorld
                    End If

                End If

            Else
                wgs = GIS.CS
                If Not (TypeOf lcs Is TGIS_CSUnknownCoordinateSystem) Then
                    final = lcs.ExtentFromCS(wgs, GIS.UnrotatedExtent(GIS.VisibleExtent))
                End If

                If (lcs.Error <> 0) Then
                    final = TGIS_Utils.GisNoWorld
                End If

            End If

            Return final
        End Function

        Private Sub cbStartIndex_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbStartIndex.CheckedChanged
            tbStartIndex.Enabled = cbStartIndex.Checked
        End Sub

        Private Sub cbMaxFeatures_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbMaxFeatures.CheckedChanged
            tbMaxFeatures.Enabled = cbMaxFeatures.Checked
        End Sub

        Private Sub cbBoundBoxFilter_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbBoundBoxFilter.CheckedChanged
            tbXMax.Enabled = cbBoundBoxFilter.Checked
            tbYMax.Enabled = cbBoundBoxFilter.Checked
            tbXMin.Enabled = cbBoundBoxFilter.Checked
            tbYMin.Enabled = cbBoundBoxFilter.Checked
            cbClipByVisible.Enabled = cbBoundBoxFilter.Checked
            cbClipByVisible_CheckedChanged(sender, e)
        End Sub

        Private Sub cbClipByVisible_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbClipByVisible.CheckedChanged
            Dim ext As TGIS_Extent
            If GIS.IsEmpty Then
                Return
            End If

            ext = getBBoxExtent(getSelectedFeature)
            If Not TGIS_Utils.GisIsNoWorld(ext) Then
                tbXMax.Text = ext.XMin.ToString
                tbYMax.Text = ext.YMax.ToString
                tbXMin.Text = ext.XMax.ToString
                tbYMin.Text = ext.YMax.ToString
            Else
                tbXMax.Text = ""
                tbYMax.Text = ""
                tbXMin.Text = ""
                tbYMin.Text = ""
            End If

        End Sub

        Private Sub btnAddLayer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddLayer.Click
            Dim fea As TGIS_WFSFeature
            Dim wfsPath As String

            btnAddLayer.Text = "Wait..."
            btnAddLayer.Enabled = False

            wfsPath = "&SERVICE=WFS"
            If (tbParameters.Text <> "") Then
                wfsPath = (wfsPath + ("&" + tbParameters.Text))
            End If

            If (cbxVersion.Text <> "") Then
                wfsPath = (wfsPath + ("&VERSION=" + cbxVersion.Text))
            End If

            fea = getSelectedFeature()

            If (Not (fea) Is Nothing) Then
                wfsPath = (wfsPath + ("&TYPENAME=" + fea.Name))
            End If

            If (cbxCoordSys.Text <> "") Then
                wfsPath = (wfsPath + ("&SRSNAME=" + cbxCoordSys.Text))
            End If

            If (cbxOutputFormat.Text <> "") Then
                wfsPath = (wfsPath + ("&OUTPUTFORMAT=" + cbxOutputFormat.Text))
            End If

            If cbMaxFeatures.Checked Then
                wfsPath = (wfsPath + ("&MAXFEATURES=" + tbMaxFeatures.Text))
            End If

            If cbStartIndex.Checked Then
                wfsPath = (wfsPath + ("&STARTINDEX=" + tbStartIndex.Text))
            End If

            If cbBoundBoxFilter.Checked Then
                If cbReverse.Checked Then
                    wfsPath = (wfsPath + ("&BBOX=" _
                                + (tbYMin.Text + (", " _
                                + (tbXMin.Text + (", " _
                                + (tbYMax.Text + (", " + tbXMax.Text))))))))
                Else
                    wfsPath = (wfsPath + ("&BBOX=" _
                                + (tbXMin.Text + (", " _
                                + (tbYMin.Text + (", " _
                                + (tbXMax.Text + (", " + tbYMax.Text))))))))
                End If

            End If

            If cbReverse.Checked Then
                wfsPath = (wfsPath + ("&AxisOrder=" + GIS_INI_AXIS_ORDER_NE))
            End If

            mainForm.AppendCovarage(wfs.MakeUrl(wfsPath, cbxURL.Text))
            btnAddLayer.Enabled = True
            btnAddLayer.Text = "Add layer"
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Close()
        End Sub

        Private Sub btnOpenURL_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOpenURL.Click
            Dim lwfs As TGIS_LayerWFS
            lwfs = New TGIS_LayerWFS
            lwfs.Path = cbxURL.Text
            GIS.Add(lwfs)
            GIS.FullExtent()
        End Sub

        Private Sub locateOnMapToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles locateOnMapToolStripMenuItem.Click
            Dim fea As TGIS_WFSFeature
            fea = getSelectedFeature()

            If (Not (fea) Is Nothing) Then
                GIS.VisibleExtent = fea.WGS84BBox
            End If

        End Sub

        Private Sub openMetadataToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles openMetadataToolStripMenuItem.Click
            Dim fea As TGIS_WFSFeature
            fea = getSelectedFeature()

            If (Not (fea) Is Nothing) Then
                Process.Start(fea.MetadataUrl, "open")
            End If

        End Sub
    End Class
End Namespace