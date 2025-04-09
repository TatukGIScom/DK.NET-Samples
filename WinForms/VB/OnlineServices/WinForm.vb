Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports TatukGIS.RTL
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace OnlineServices
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Enum MapStyle
            International
            English
            InternationalHillshade
            EnglishHillshade
        End Enum

        Private Const LOCAL_LAYER_TILES As String = "tiles"

        Private shpList As List(Of Long) = New List(Of Long)()

        Friend WithEvents grpbxMap As GroupBox
        Friend WithEvents cmbbxMap As ComboBox
        Friend WithEvents grpbxGeocoding As GroupBox
        Friend WithEvents btnGeocoding As Button
        Friend WithEvents cmbbxGeocodingLimit As ComboBox
        Friend WithEvents lblGeocodingLimit As Label
        Friend WithEvents edtGeocodingAddress As TextBox
        Friend WithEvents lblGeocodingAddress As Label
        Friend WithEvents grpbxRouting As GroupBox
        Friend WithEvents grpbxIsochrone As GroupBox
        Friend WithEvents GIS As TGIS_ViewerWnd
        Friend WithEvents grpbxRoutingDir As GroupBox
        Friend WithEvents rbtnRoutingProfileFoot As RadioButton
        Friend WithEvents rbtnRoutingProfileBike As RadioButton
        Friend WithEvents rbtnRoutingProfileCar As RadioButton
        Friend WithEvents lblRoutingProfile As Label
        Friend WithEvents rbtnIsochroneProfileFoot As RadioButton
        Friend WithEvents rbtnIsochroneProfileBike As RadioButton
        Friend WithEvents rbtnIsochroneProfileCar As RadioButton
        Friend WithEvents lblIsochroneProfile As Label
        Friend WithEvents btnIsochrone As Button
        Friend WithEvents edtIsochroneAddress As TextBox
        Friend WithEvents lblIsochroneAddress As Label
        Friend WithEvents cmbbxIsochroneBuckets As ComboBox
        Friend WithEvents lblIsochroneBuckets As Label
        Friend WithEvents edtIsochroneTime As TextBox
        Friend WithEvents lblIsochroneTime As Label
        Friend WithEvents btnRouting As Button
        Friend WithEvents btnRoudingDelete As Button
        Friend WithEvents btnRoutingAdd As Button
        Friend WithEvents strgrdRouting As DataGridView
        Friend WithEvents lblRoutingDirInfo As Label
        Friend WithEvents lblRoutingDirTime As Label
        Friend WithEvents lblRoutingDirDist As Label
        Friend WithEvents strgrdRoutingDir As DataGridView
        Friend WithEvents GIS_Scale As TGIS_ControlScale
        Friend WithEvents Key As DataGridViewTextBoxColumn
        Friend WithEvents Value As DataGridViewTextBoxColumn
        Friend WithEvents Dir As DataGridViewTextBoxColumn
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim TgiS_CSUnits2 As TatukGIS.NDK.TGIS_CSUnits = New TatukGIS.NDK.TGIS_CSUnits()
            Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.grpbxMap = New System.Windows.Forms.GroupBox()
            Me.cmbbxMap = New System.Windows.Forms.ComboBox()
            Me.grpbxGeocoding = New System.Windows.Forms.GroupBox()
            Me.btnGeocoding = New System.Windows.Forms.Button()
            Me.cmbbxGeocodingLimit = New System.Windows.Forms.ComboBox()
            Me.lblGeocodingLimit = New System.Windows.Forms.Label()
            Me.edtGeocodingAddress = New System.Windows.Forms.TextBox()
            Me.lblGeocodingAddress = New System.Windows.Forms.Label()
            Me.grpbxRouting = New System.Windows.Forms.GroupBox()
            Me.btnRouting = New System.Windows.Forms.Button()
            Me.btnRoudingDelete = New System.Windows.Forms.Button()
            Me.btnRoutingAdd = New System.Windows.Forms.Button()
            Me.strgrdRouting = New System.Windows.Forms.DataGridView()
            Me.Key = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Value = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.rbtnRoutingProfileFoot = New System.Windows.Forms.RadioButton()
            Me.rbtnRoutingProfileBike = New System.Windows.Forms.RadioButton()
            Me.rbtnRoutingProfileCar = New System.Windows.Forms.RadioButton()
            Me.lblRoutingProfile = New System.Windows.Forms.Label()
            Me.grpbxIsochrone = New System.Windows.Forms.GroupBox()
            Me.btnIsochrone = New System.Windows.Forms.Button()
            Me.edtIsochroneAddress = New System.Windows.Forms.TextBox()
            Me.lblIsochroneAddress = New System.Windows.Forms.Label()
            Me.cmbbxIsochroneBuckets = New System.Windows.Forms.ComboBox()
            Me.lblIsochroneBuckets = New System.Windows.Forms.Label()
            Me.edtIsochroneTime = New System.Windows.Forms.TextBox()
            Me.lblIsochroneTime = New System.Windows.Forms.Label()
            Me.rbtnIsochroneProfileFoot = New System.Windows.Forms.RadioButton()
            Me.rbtnIsochroneProfileBike = New System.Windows.Forms.RadioButton()
            Me.rbtnIsochroneProfileCar = New System.Windows.Forms.RadioButton()
            Me.lblIsochroneProfile = New System.Windows.Forms.Label()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.GIS_Scale = New TatukGIS.NDK.WinForms.TGIS_ControlScale()
            Me.grpbxRoutingDir = New System.Windows.Forms.GroupBox()
            Me.strgrdRoutingDir = New System.Windows.Forms.DataGridView()
            Me.lblRoutingDirInfo = New System.Windows.Forms.Label()
            Me.lblRoutingDirTime = New System.Windows.Forms.Label()
            Me.lblRoutingDirDist = New System.Windows.Forms.Label()
            Me.Dir = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.grpbxMap.SuspendLayout()
            Me.grpbxGeocoding.SuspendLayout()
            Me.grpbxRouting.SuspendLayout()
            CType(Me.strgrdRouting, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.grpbxIsochrone.SuspendLayout()
            Me.GIS.SuspendLayout()
            Me.grpbxRoutingDir.SuspendLayout()
            CType(Me.strgrdRoutingDir, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'grpbxMap
            '
            Me.grpbxMap.Controls.Add(Me.cmbbxMap)
            Me.grpbxMap.Location = New System.Drawing.Point(12, 12)
            Me.grpbxMap.Name = "grpbxMap"
            Me.grpbxMap.Size = New System.Drawing.Size(242, 55)
            Me.grpbxMap.TabIndex = 0
            Me.grpbxMap.TabStop = False
            Me.grpbxMap.Text = "Map"
            '
            'cmbbxMap
            '
            Me.cmbbxMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbbxMap.FormattingEnabled = True
            Me.cmbbxMap.Items.AddRange(New Object() {"International", "English", "International with hillshade", "English with hillshade"})
            Me.cmbbxMap.Location = New System.Drawing.Point(16, 19)
            Me.cmbbxMap.Name = "cmbbxMap"
            Me.cmbbxMap.Size = New System.Drawing.Size(211, 21)
            Me.cmbbxMap.TabIndex = 0
            '
            'grpbxGeocoding
            '
            Me.grpbxGeocoding.Controls.Add(Me.btnGeocoding)
            Me.grpbxGeocoding.Controls.Add(Me.cmbbxGeocodingLimit)
            Me.grpbxGeocoding.Controls.Add(Me.lblGeocodingLimit)
            Me.grpbxGeocoding.Controls.Add(Me.edtGeocodingAddress)
            Me.grpbxGeocoding.Controls.Add(Me.lblGeocodingAddress)
            Me.grpbxGeocoding.Location = New System.Drawing.Point(12, 73)
            Me.grpbxGeocoding.Name = "grpbxGeocoding"
            Me.grpbxGeocoding.Size = New System.Drawing.Size(242, 84)
            Me.grpbxGeocoding.TabIndex = 1
            Me.grpbxGeocoding.TabStop = False
            Me.grpbxGeocoding.Text = "Geocoding"
            '
            'btnGeocoding
            '
            Me.btnGeocoding.Location = New System.Drawing.Point(152, 48)
            Me.btnGeocoding.Name = "btnGeocoding"
            Me.btnGeocoding.Size = New System.Drawing.Size(75, 23)
            Me.btnGeocoding.TabIndex = 4
            Me.btnGeocoding.Text = "Find"
            Me.btnGeocoding.UseVisualStyleBackColor = True
            '
            'cmbbxGeocodingLimit
            '
            Me.cmbbxGeocodingLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbbxGeocodingLimit.FormattingEnabled = True
            Me.cmbbxGeocodingLimit.Items.AddRange(New Object() {"1", "5", "10"})
            Me.cmbbxGeocodingLimit.Location = New System.Drawing.Point(47, 48)
            Me.cmbbxGeocodingLimit.Name = "cmbbxGeocodingLimit"
            Me.cmbbxGeocodingLimit.Size = New System.Drawing.Size(41, 21)
            Me.cmbbxGeocodingLimit.TabIndex = 3
            '
            'lblGeocodingLimit
            '
            Me.lblGeocodingLimit.AutoSize = True
            Me.lblGeocodingLimit.Location = New System.Drawing.Point(13, 51)
            Me.lblGeocodingLimit.Name = "lblGeocodingLimit"
            Me.lblGeocodingLimit.Size = New System.Drawing.Size(28, 13)
            Me.lblGeocodingLimit.TabIndex = 2
            Me.lblGeocodingLimit.Text = "Limit"
            '
            'edtGeocodingAddress
            '
            Me.edtGeocodingAddress.Location = New System.Drawing.Point(64, 22)
            Me.edtGeocodingAddress.Name = "edtGeocodingAddress"
            Me.edtGeocodingAddress.Size = New System.Drawing.Size(163, 20)
            Me.edtGeocodingAddress.TabIndex = 1
            Me.edtGeocodingAddress.Text = "Gdynia, Plac Kaszubski 8"
            '
            'lblGeocodingAddress
            '
            Me.lblGeocodingAddress.AutoSize = True
            Me.lblGeocodingAddress.Location = New System.Drawing.Point(13, 25)
            Me.lblGeocodingAddress.Name = "lblGeocodingAddress"
            Me.lblGeocodingAddress.Size = New System.Drawing.Size(45, 13)
            Me.lblGeocodingAddress.TabIndex = 0
            Me.lblGeocodingAddress.Text = "Address"
            '
            'grpbxRouting
            '
            Me.grpbxRouting.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.grpbxRouting.Controls.Add(Me.btnRouting)
            Me.grpbxRouting.Controls.Add(Me.btnRoudingDelete)
            Me.grpbxRouting.Controls.Add(Me.btnRoutingAdd)
            Me.grpbxRouting.Controls.Add(Me.strgrdRouting)
            Me.grpbxRouting.Controls.Add(Me.rbtnRoutingProfileFoot)
            Me.grpbxRouting.Controls.Add(Me.rbtnRoutingProfileBike)
            Me.grpbxRouting.Controls.Add(Me.rbtnRoutingProfileCar)
            Me.grpbxRouting.Controls.Add(Me.lblRoutingProfile)
            Me.grpbxRouting.Location = New System.Drawing.Point(12, 163)
            Me.grpbxRouting.Name = "grpbxRouting"
            Me.grpbxRouting.Size = New System.Drawing.Size(242, 298)
            Me.grpbxRouting.TabIndex = 2
            Me.grpbxRouting.TabStop = False
            Me.grpbxRouting.Text = "Routing"
            '
            'btnRouting
            '
            Me.btnRouting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnRouting.Location = New System.Drawing.Point(152, 261)
            Me.btnRouting.Name = "btnRouting"
            Me.btnRouting.Size = New System.Drawing.Size(75, 23)
            Me.btnRouting.TabIndex = 7
            Me.btnRouting.Text = "Find"
            Me.btnRouting.UseVisualStyleBackColor = True
            '
            'btnRoudingDelete
            '
            Me.btnRoudingDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnRoudingDelete.Location = New System.Drawing.Point(45, 261)
            Me.btnRoudingDelete.Name = "btnRoudingDelete"
            Me.btnRoudingDelete.Size = New System.Drawing.Size(23, 23)
            Me.btnRoudingDelete.TabIndex = 6
            Me.btnRoudingDelete.Text = "-"
            Me.btnRoudingDelete.UseVisualStyleBackColor = True
            '
            'btnRoutingAdd
            '
            Me.btnRoutingAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnRoutingAdd.Location = New System.Drawing.Point(16, 261)
            Me.btnRoutingAdd.Name = "btnRoutingAdd"
            Me.btnRoutingAdd.Size = New System.Drawing.Size(23, 23)
            Me.btnRoutingAdd.TabIndex = 5
            Me.btnRoutingAdd.Text = "+"
            Me.btnRoutingAdd.UseVisualStyleBackColor = True
            '
            'strgrdRouting
            '
            Me.strgrdRouting.AllowUserToResizeColumns = False
            Me.strgrdRouting.AllowUserToResizeRows = False
            Me.strgrdRouting.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.strgrdRouting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.strgrdRouting.ColumnHeadersVisible = False
            Me.strgrdRouting.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Key, Me.Value})
            Me.strgrdRouting.Location = New System.Drawing.Point(16, 46)
            Me.strgrdRouting.MultiSelect = False
            Me.strgrdRouting.Name = "strgrdRouting"
            Me.strgrdRouting.RowHeadersVisible = False
            Me.strgrdRouting.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
            Me.strgrdRouting.ShowEditingIcon = False
            Me.strgrdRouting.Size = New System.Drawing.Size(211, 209)
            Me.strgrdRouting.TabIndex = 4
            '
            'Key
            '
            Me.Key.Frozen = True
            Me.Key.HeaderText = ""
            Me.Key.Name = "Key"
            Me.Key.ReadOnly = True
            Me.Key.Width = 64
            '
            'Value
            '
            Me.Value.HeaderText = ""
            Me.Value.Name = "Value"
            Me.Value.Width = 256
            '
            'rbtnRoutingProfileFoot
            '
            Me.rbtnRoutingProfileFoot.AutoSize = True
            Me.rbtnRoutingProfileFoot.Location = New System.Drawing.Point(157, 23)
            Me.rbtnRoutingProfileFoot.Name = "rbtnRoutingProfileFoot"
            Me.rbtnRoutingProfileFoot.Size = New System.Drawing.Size(46, 17)
            Me.rbtnRoutingProfileFoot.TabIndex = 3
            Me.rbtnRoutingProfileFoot.TabStop = True
            Me.rbtnRoutingProfileFoot.Text = "Foot"
            Me.rbtnRoutingProfileFoot.UseVisualStyleBackColor = True
            '
            'rbtnRoutingProfileBike
            '
            Me.rbtnRoutingProfileBike.AutoSize = True
            Me.rbtnRoutingProfileBike.Location = New System.Drawing.Point(105, 23)
            Me.rbtnRoutingProfileBike.Name = "rbtnRoutingProfileBike"
            Me.rbtnRoutingProfileBike.Size = New System.Drawing.Size(46, 17)
            Me.rbtnRoutingProfileBike.TabIndex = 2
            Me.rbtnRoutingProfileBike.TabStop = True
            Me.rbtnRoutingProfileBike.Text = "Bike"
            Me.rbtnRoutingProfileBike.UseVisualStyleBackColor = True
            '
            'rbtnRoutingProfileCar
            '
            Me.rbtnRoutingProfileCar.AutoSize = True
            Me.rbtnRoutingProfileCar.Checked = True
            Me.rbtnRoutingProfileCar.Location = New System.Drawing.Point(58, 23)
            Me.rbtnRoutingProfileCar.Name = "rbtnRoutingProfileCar"
            Me.rbtnRoutingProfileCar.Size = New System.Drawing.Size(41, 17)
            Me.rbtnRoutingProfileCar.TabIndex = 1
            Me.rbtnRoutingProfileCar.TabStop = True
            Me.rbtnRoutingProfileCar.Text = "Car"
            Me.rbtnRoutingProfileCar.UseVisualStyleBackColor = True
            '
            'lblRoutingProfile
            '
            Me.lblRoutingProfile.AutoSize = True
            Me.lblRoutingProfile.Location = New System.Drawing.Point(13, 25)
            Me.lblRoutingProfile.Name = "lblRoutingProfile"
            Me.lblRoutingProfile.Size = New System.Drawing.Size(39, 13)
            Me.lblRoutingProfile.TabIndex = 0
            Me.lblRoutingProfile.Text = "Profile:"
            '
            'grpbxIsochrone
            '
            Me.grpbxIsochrone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.grpbxIsochrone.Controls.Add(Me.btnIsochrone)
            Me.grpbxIsochrone.Controls.Add(Me.edtIsochroneAddress)
            Me.grpbxIsochrone.Controls.Add(Me.lblIsochroneAddress)
            Me.grpbxIsochrone.Controls.Add(Me.cmbbxIsochroneBuckets)
            Me.grpbxIsochrone.Controls.Add(Me.lblIsochroneBuckets)
            Me.grpbxIsochrone.Controls.Add(Me.edtIsochroneTime)
            Me.grpbxIsochrone.Controls.Add(Me.lblIsochroneTime)
            Me.grpbxIsochrone.Controls.Add(Me.rbtnIsochroneProfileFoot)
            Me.grpbxIsochrone.Controls.Add(Me.rbtnIsochroneProfileBike)
            Me.grpbxIsochrone.Controls.Add(Me.rbtnIsochroneProfileCar)
            Me.grpbxIsochrone.Controls.Add(Me.lblIsochroneProfile)
            Me.grpbxIsochrone.Location = New System.Drawing.Point(12, 467)
            Me.grpbxIsochrone.Name = "grpbxIsochrone"
            Me.grpbxIsochrone.Size = New System.Drawing.Size(242, 161)
            Me.grpbxIsochrone.TabIndex = 3
            Me.grpbxIsochrone.TabStop = False
            Me.grpbxIsochrone.Text = "Isochrone"
            '
            'btnIsochrone
            '
            Me.btnIsochrone.Location = New System.Drawing.Point(152, 125)
            Me.btnIsochrone.Name = "btnIsochrone"
            Me.btnIsochrone.Size = New System.Drawing.Size(75, 23)
            Me.btnIsochrone.TabIndex = 10
            Me.btnIsochrone.Text = "Find"
            Me.btnIsochrone.UseVisualStyleBackColor = True
            '
            'edtIsochroneAddress
            '
            Me.edtIsochroneAddress.Location = New System.Drawing.Point(64, 99)
            Me.edtIsochroneAddress.Name = "edtIsochroneAddress"
            Me.edtIsochroneAddress.Size = New System.Drawing.Size(163, 20)
            Me.edtIsochroneAddress.TabIndex = 9
            Me.edtIsochroneAddress.Text = "Gdynia, Plac Kaszubski 8"
            '
            'lblIsochroneAddress
            '
            Me.lblIsochroneAddress.AutoSize = True
            Me.lblIsochroneAddress.Location = New System.Drawing.Point(13, 102)
            Me.lblIsochroneAddress.Name = "lblIsochroneAddress"
            Me.lblIsochroneAddress.Size = New System.Drawing.Size(45, 13)
            Me.lblIsochroneAddress.TabIndex = 8
            Me.lblIsochroneAddress.Text = "Address"
            '
            'cmbbxIsochroneBuckets
            '
            Me.cmbbxIsochroneBuckets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbbxIsochroneBuckets.FormattingEnabled = True
            Me.cmbbxIsochroneBuckets.Items.AddRange(New Object() {"1", "5", "10"})
            Me.cmbbxIsochroneBuckets.Location = New System.Drawing.Point(118, 72)
            Me.cmbbxIsochroneBuckets.Name = "cmbbxIsochroneBuckets"
            Me.cmbbxIsochroneBuckets.Size = New System.Drawing.Size(109, 21)
            Me.cmbbxIsochroneBuckets.TabIndex = 7
            '
            'lblIsochroneBuckets
            '
            Me.lblIsochroneBuckets.AutoSize = True
            Me.lblIsochroneBuckets.Location = New System.Drawing.Point(13, 75)
            Me.lblIsochroneBuckets.Name = "lblIsochroneBuckets"
            Me.lblIsochroneBuckets.Size = New System.Drawing.Size(97, 13)
            Me.lblIsochroneBuckets.TabIndex = 6
            Me.lblIsochroneBuckets.Text = "Number of buckets"
            '
            'edtIsochroneTime
            '
            Me.edtIsochroneTime.Location = New System.Drawing.Point(118, 46)
            Me.edtIsochroneTime.Name = "edtIsochroneTime"
            Me.edtIsochroneTime.Size = New System.Drawing.Size(109, 20)
            Me.edtIsochroneTime.TabIndex = 5
            Me.edtIsochroneTime.Text = "600"
            '
            'lblIsochroneTime
            '
            Me.lblIsochroneTime.AutoSize = True
            Me.lblIsochroneTime.Location = New System.Drawing.Point(13, 49)
            Me.lblIsochroneTime.Name = "lblIsochroneTime"
            Me.lblIsochroneTime.Size = New System.Drawing.Size(99, 13)
            Me.lblIsochroneTime.TabIndex = 4
            Me.lblIsochroneTime.Text = "Time limit (seconds)"
            '
            'rbtnIsochroneProfileFoot
            '
            Me.rbtnIsochroneProfileFoot.AutoSize = True
            Me.rbtnIsochroneProfileFoot.Location = New System.Drawing.Point(157, 23)
            Me.rbtnIsochroneProfileFoot.Name = "rbtnIsochroneProfileFoot"
            Me.rbtnIsochroneProfileFoot.Size = New System.Drawing.Size(46, 17)
            Me.rbtnIsochroneProfileFoot.TabIndex = 3
            Me.rbtnIsochroneProfileFoot.TabStop = True
            Me.rbtnIsochroneProfileFoot.Text = "Foot"
            Me.rbtnIsochroneProfileFoot.UseVisualStyleBackColor = True
            '
            'rbtnIsochroneProfileBike
            '
            Me.rbtnIsochroneProfileBike.AutoSize = True
            Me.rbtnIsochroneProfileBike.Location = New System.Drawing.Point(105, 23)
            Me.rbtnIsochroneProfileBike.Name = "rbtnIsochroneProfileBike"
            Me.rbtnIsochroneProfileBike.Size = New System.Drawing.Size(46, 17)
            Me.rbtnIsochroneProfileBike.TabIndex = 2
            Me.rbtnIsochroneProfileBike.Text = "Bike"
            Me.rbtnIsochroneProfileBike.UseVisualStyleBackColor = True
            '
            'rbtnIsochroneProfileCar
            '
            Me.rbtnIsochroneProfileCar.AutoSize = True
            Me.rbtnIsochroneProfileCar.Checked = True
            Me.rbtnIsochroneProfileCar.Location = New System.Drawing.Point(58, 23)
            Me.rbtnIsochroneProfileCar.Name = "rbtnIsochroneProfileCar"
            Me.rbtnIsochroneProfileCar.Size = New System.Drawing.Size(41, 17)
            Me.rbtnIsochroneProfileCar.TabIndex = 1
            Me.rbtnIsochroneProfileCar.TabStop = True
            Me.rbtnIsochroneProfileCar.Text = "Car"
            Me.rbtnIsochroneProfileCar.UseVisualStyleBackColor = True
            '
            'lblIsochroneProfile
            '
            Me.lblIsochroneProfile.AutoSize = True
            Me.lblIsochroneProfile.Location = New System.Drawing.Point(13, 25)
            Me.lblIsochroneProfile.Name = "lblIsochroneProfile"
            Me.lblIsochroneProfile.Size = New System.Drawing.Size(39, 13)
            Me.lblIsochroneProfile.TabIndex = 0
            Me.lblIsochroneProfile.Text = "Profile:"
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.Controls.Add(Me.GIS_Scale)
            Me.GIS.Location = New System.Drawing.Point(260, 0)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(640, 640)
            Me.GIS.TabIndex = 4
            '
            'GIS_Scale
            '
            Me.GIS_Scale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_Scale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.GIS_Scale.DividerColor1 = System.Drawing.Color.Black
            Me.GIS_Scale.DividerColor2 = System.Drawing.Color.White
            Me.GIS_Scale.GIS_Viewer = Nothing
            Me.GIS_Scale.Location = New System.Drawing.Point(455, 588)
            Me.GIS_Scale.Name = "GIS_Scale"
            Me.GIS_Scale.PrepareEvent = Nothing
            Me.GIS_Scale.Size = New System.Drawing.Size(185, 40)
            Me.GIS_Scale.TabIndex = 0
            TgiS_CSUnits2.DescriptionEx = Nothing
            Me.GIS_Scale.Units = TgiS_CSUnits2
            Me.GIS_Scale.UnitsEPSG = 0
            '
            'grpbxRoutingDir
            '
            Me.grpbxRoutingDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.grpbxRoutingDir.Controls.Add(Me.strgrdRoutingDir)
            Me.grpbxRoutingDir.Controls.Add(Me.lblRoutingDirInfo)
            Me.grpbxRoutingDir.Controls.Add(Me.lblRoutingDirTime)
            Me.grpbxRoutingDir.Controls.Add(Me.lblRoutingDirDist)
            Me.grpbxRoutingDir.Location = New System.Drawing.Point(906, 11)
            Me.grpbxRoutingDir.Name = "grpbxRoutingDir"
            Me.grpbxRoutingDir.Size = New System.Drawing.Size(195, 617)
            Me.grpbxRoutingDir.TabIndex = 5
            Me.grpbxRoutingDir.TabStop = False
            Me.grpbxRoutingDir.Text = "Routing directions"
            '
            'strgrdRoutingDir
            '
            Me.strgrdRoutingDir.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.strgrdRoutingDir.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
            Me.strgrdRoutingDir.ColumnHeadersVisible = False
            Me.strgrdRoutingDir.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Dir})
            Me.strgrdRoutingDir.Location = New System.Drawing.Point(16, 87)
            Me.strgrdRoutingDir.MultiSelect = False
            Me.strgrdRoutingDir.Name = "strgrdRoutingDir"
            Me.strgrdRoutingDir.RowHeadersVisible = False
            Me.strgrdRoutingDir.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
            Me.strgrdRoutingDir.ShowEditingIcon = False
            Me.strgrdRoutingDir.Size = New System.Drawing.Size(164, 517)
            Me.strgrdRoutingDir.TabIndex = 3
            '
            'lblRoutingDirInfo
            '
            Me.lblRoutingDirInfo.AutoSize = True
            Me.lblRoutingDirInfo.Location = New System.Drawing.Point(15, 62)
            Me.lblRoutingDirInfo.Name = "lblRoutingDirInfo"
            Me.lblRoutingDirInfo.Size = New System.Drawing.Size(109, 13)
            Me.lblRoutingDirInfo.TabIndex = 2
            Me.lblRoutingDirInfo.Text = "Double-click to zoom:"
            '
            'lblRoutingDirTime
            '
            Me.lblRoutingDirTime.AutoSize = True
            Me.lblRoutingDirTime.Location = New System.Drawing.Point(15, 43)
            Me.lblRoutingDirTime.Name = "lblRoutingDirTime"
            Me.lblRoutingDirTime.Size = New System.Drawing.Size(65, 13)
            Me.lblRoutingDirTime.TabIndex = 1
            Me.lblRoutingDirTime.Text = "Total time: ?"
            '
            'lblRoutingDirDist
            '
            Me.lblRoutingDirDist.AutoSize = True
            Me.lblRoutingDirDist.Location = New System.Drawing.Point(15, 23)
            Me.lblRoutingDirDist.Name = "lblRoutingDirDist"
            Me.lblRoutingDirDist.Size = New System.Drawing.Size(86, 13)
            Me.lblRoutingDirDist.TabIndex = 0
            Me.lblRoutingDirDist.Text = "Total distance: ?"
            '
            'Dir
            '
            Me.Dir.HeaderText = ""
            Me.Dir.Name = "Dir"
            Me.Dir.ReadOnly = True
            Me.Dir.Width = 384
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(1113, 640)
            Me.Controls.Add(Me.grpbxRoutingDir)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.grpbxIsochrone)
            Me.Controls.Add(Me.grpbxRouting)
            Me.Controls.Add(Me.grpbxGeocoding)
            Me.Controls.Add(Me.grpbxMap)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - OnlineServices"
            Me.grpbxMap.ResumeLayout(False)
            Me.grpbxGeocoding.ResumeLayout(False)
            Me.grpbxGeocoding.PerformLayout()
            Me.grpbxRouting.ResumeLayout(False)
            Me.grpbxRouting.PerformLayout()
            CType(Me.strgrdRouting, System.ComponentModel.ISupportInitialize).EndInit()
            Me.grpbxIsochrone.ResumeLayout(False)
            Me.grpbxIsochrone.PerformLayout()
            Me.GIS.ResumeLayout(False)
            Me.grpbxRoutingDir.ResumeLayout(False)
            Me.grpbxRoutingDir.PerformLayout()
            CType(Me.strgrdRoutingDir, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Function growExtent(ByVal _ext As TGIS_Extent, ByVal _fac As Double) As TGIS_Extent
            Dim ctr As TGIS_Point = TGIS_Utils.GisPoint(0.5D * (_ext.XMin + _ext.XMax),
                                                        0.5D * (_ext.YMin + _ext.YMax))

            Dim xsiz As Double = 0.5D * _fac * (_ext.XMax - _ext.XMin)
            Dim ysiz As Double = 0.5D * _fac * (_ext.YMax - _ext.YMin)

            Return TGIS_Utils.GisExtent(ctr.X - xsiz, ctr.Y - ysiz,
                                        ctr.X + xsiz, ctr.Y + ysiz)
        End Function

        Private Function resizeExtent(ByVal _ext As TGIS_Extent, ByVal _siz As Double) As TGIS_Extent
            Dim xsiz As Double = _ext.XMax - _ext.XMin
            Dim ysiz As Double = _ext.YMax - _ext.YMin

            If ((xsiz > _siz) Or (ysiz > _siz)) Then Return _ext

            Dim ctr As TGIS_Point = TGIS_Utils.GisPoint(0.5D * (_ext.XMin + _ext.XMax),
                                                        0.5D * (_ext.YMin + _ext.YMax))

            Return TGIS_Utils.GisExtent(ctr.X - 0.5D * _siz, ctr.Y - 0.5D * _siz,
                                        ctr.X + 0.5D * _siz, ctr.Y + 0.5D * _siz)
        End Function

        Private Sub resetLayers()
            If (GIS.Get("fgeocoding") IsNot Nothing) Then GIS.Delete("fgeocoding")
            If (GIS.Get("route") IsNot Nothing) Then GIS.Delete("route")
            If (GIS.Get("isochrone") IsNot Nothing) Then GIS.Delete("isochrone")
        End Sub

        Private Sub loadTiles(ByVal _style As MapStyle)
            Dim b As Boolean = GIS.Get(LOCAL_LAYER_TILES) IsNot Nothing

            If (b) Then GIS.Delete(LOCAL_LAYER_TILES)

            Dim lwt As TGIS_LayerWebTiles = New TGIS_LayerWebTiles()
            Dim path As String = TGIS_Utils.GisSamplesDataDirDownload() + "\Samples\WebServices\"
            Select Case _style
                Case MapStyle.International
                    path += "TatukGIS OpenStreetMap Tiles.ttkwp"
                Case MapStyle.English
                    path += "TatukGIS OpenStreetMap Tiles (English).ttkwp"
                Case MapStyle.InternationalHillshade
                    path += "TatukGIS OpenStreetMap Hillshade Tiles.ttkwp"
                Case MapStyle.EnglishHillshade
                    path += "TatukGIS OpenStreetMap Hillshade Tiles (English).ttkwp"
            End Select
            lwt.Path = path
            lwt.Open()
            lwt.Name = LOCAL_LAYER_TILES

            GIS.Add(lwt)
            lwt.Move(999)

            If (b) Then
                GIS.InvalidateWholeMap()
            Else
                GIS.VisibleExtent = lwt.Extent
            End If

        End Sub

        Private Function sign2dir(ByVal _sign As Integer) As String
            Dim res As String = ""
            Select Case _sign
                Case -99
                    res = "[unknown]"
                Case -98
                    res = "Make a u-turn"
                Case -8
                    res = "Make a left u-turn"
                Case -7
                    res = "Keep left"
                Case -6
                    res = "Exit roundabout"
                Case -3
                    res = "Sharp turn left"
                Case -2
                    res = "Turn left"
                Case -1
                    res = "Slight turn left"
                Case 0
                    res = "Continue"
                Case 1
                    res = "Slight turn right"
                Case 2
                    res = "Turn right"
                Case 3
                    res = "Sharp turn right"
                Case 4
                    res = "Finish"
                Case 5
                    res = "Reach the intermediate destination"
                Case 6
                    res = "Enter roundabout and take the "
                Case 7
                    res = "Keep right"
                Case 8
                    res = "Make a right u-turn"
                Case 101
                    res = "Start trip"
                Case 102
                    res = "Transfer"
                Case 103
                    res = "End trip"
                Case Else
                    res = "Ignore"
            End Select
            Return res
        End Function

        Private Function exitNumber(ByVal _s As String) As String
            Dim res As String
            Select Case _s.Chars(_s.Length - 1)
                Case "1"
                    res = _s + "st"
                Case "2"
                    res = _s + "nd"
                Case "3"
                    res = _s + "rd"
                Case Else
                    res = _s + "th"
            End Select
            Return res
        End Function

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            cmbbxMap.SelectedIndex = 1
            cmbbxGeocodingLimit.SelectedIndex = 0
            cmbbxIsochroneBuckets.SelectedIndex = 1

            Dim lst(2) As String
            lst(0) = "From"
            lst(1) = "Gdynia"
            strgrdRouting.Rows.Add(lst)
            lst(0) = "Through"
            lst(1) = "Czestochowa"
            strgrdRouting.Rows.Add(lst)
            lst(0) = "To"
            lst(1) = "Wroclaw"
            strgrdRouting.Rows.Add(lst)

            loadTiles(MapStyle.English)
        End Sub

        Private Sub addDir(ByVal _dir As String, ByVal _uid As Int64)
            strgrdRoutingDir.Rows.Add(_dir)
            shpList.Add(_uid)
        End Sub

        Private Sub cmbbxMap_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbbxMap.SelectedIndexChanged
            Select Case cmbbxMap.SelectedIndex
                Case 0
                    loadTiles(MapStyle.International)
                Case 1
                    loadTiles(MapStyle.English)
                Case 2
                    loadTiles(MapStyle.InternationalHillshade)
                Case 3
                    loadTiles(MapStyle.EnglishHillshade)
            End Select
        End Sub

        Private Sub btnGeocoding_Click(sender As Object, e As EventArgs) Handles btnGeocoding.Click
            resetLayers()

            If (String.IsNullOrEmpty(edtGeocodingAddress.Text)) Then
                MessageBox.Show("Address not specified.")
                Return
            End If

            Dim ogeo As TGIS_OSMGeocoding = New TGIS_OSMGeocoding()

            ogeo.Limit = Convert.ToInt32(cmbbxGeocodingLimit.Text)
            Dim lgeo As TGIS_LayerVector = ogeo.Forward(edtGeocodingAddress.Text)
            If (lgeo.GetLastUid() > 0) Then
                lblRoutingDirDist.Text = "Total distance: ?"
                lblRoutingDirTime.Text = "Total time: ?"
                strgrdRoutingDir.Rows.Clear()

                GIS.Add(lgeo)

                Dim ext As TGIS_Extent = resizeExtent(lgeo.ProjectedExtent, 500D)
                ext = growExtent(ext, 1.2D)

                GIS.VisibleExtent = ext
            Else
                MessageBox.Show("Address not found.")
            End If
        End Sub

        Private Sub btnRoutingAdd_Click(sender As Object, e As EventArgs) Handles btnRoutingAdd.Click
            strgrdRouting.RowCount = strgrdRouting.RowCount + 1
            strgrdRouting.Rows(strgrdRouting.RowCount - 3).Cells(0).Value = "Through"
            strgrdRouting.Rows(strgrdRouting.RowCount - 2).Cells(0).Value = "To"
        End Sub

        Private Sub btnRoudingDelete_Click(sender As Object, e As EventArgs) Handles btnRoudingDelete.Click
            If (strgrdRouting.RowCount = 3) Then Return

            strgrdRouting.RowCount = strgrdRouting.RowCount - 1
            strgrdRouting.Rows(strgrdRouting.RowCount - 2).Cells(0).Value = "To"
        End Sub

        Private Sub strgrdRouting_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles strgrdRouting.CellBeginEdit
            If (e.RowIndex = strgrdRouting.RowCount - 1) Then
                strgrdRouting.Rows(strgrdRouting.RowCount - 2).Cells(0).Value = "Through"
                strgrdRouting.Rows(strgrdRouting.RowCount - 1).Cells(0).Value = "To"
            End If
        End Sub

        Private Sub btnRouting_Click(sender As Object, e As EventArgs) Handles btnRouting.Click
            resetLayers()

            For i As Integer = 0 To strgrdRouting.RowCount - 2
                If (String.IsNullOrEmpty(CType(strgrdRouting.Rows(i).Cells(1).Value, String))) Then
                    MessageBox.Show("Address not specified.")
                    Return
                End If
            Next

            Dim names As TStringList = New TStringList()
            Dim ortg As TGIS_OSMRouting = New TGIS_OSMRouting()

            If (rbtnRoutingProfileCar.Checked) Then
                ortg.Profile = TGIS_OSMRoutingProfile.Car
            ElseIf (rbtnRoutingProfileBike.Checked) Then
                ortg.Profile = TGIS_OSMRoutingProfile.Bike
            ElseIf (rbtnRoutingProfileFoot.Checked) Then
                ortg.Profile = TGIS_OSMRoutingProfile.Foot
            End If

            For i As Integer = 0 To strgrdRouting.RowCount - 2
                names.Add(CType(strgrdRouting.Rows(i).Cells(1).Value, String))
            Next

            Dim lrtg As TGIS_LayerVector = ortg.Route(names)

            If (lrtg IsNot Nothing) Then
                Dim b As Boolean = False
                Dim dist As Integer = 0
                Dim time As Integer = 0
                For Each shp As TGIS_Shape In lrtg.Loop(lrtg.Extent, "( type = 'route' )")
                    dist += Convert.ToInt32(shp.GetField("distance"))
                    time += Convert.ToInt32(shp.GetField("time"))
                    b = True
                Next

                GIS.Add(lrtg)
                Dim ext As TGIS_Extent = resizeExtent(lrtg.ProjectedExtent, 500D)
                GIS.VisibleExtent = growExtent(ext, 1.2D)

                If (b) Then
                    Dim uid As Int64 = 0
                    Dim str As String
                    Dim i As Integer

                    If (dist < 1000) Then
                        str = dist.ToString() + " m"
                    Else
                        str = String.Format("{0:0.##} km", dist / 1000D)
                    End If

                    lblRoutingDirDist.Text = "Total distance: " + str

                    Dim hrs As Integer = time / 3600
                    Dim mns As Integer = (time / 60) - hrs * 60
                    If (hrs = 0) Then
                        str = mns.ToString() + " min"
                    Else
                        str = hrs.ToString() + " h " + mns.ToString() + " min"
                    End If
                    lblRoutingDirTime.Text = "Total time: " + str

                    Dim bfin As Boolean = False
                    strgrdRoutingDir.RowCount = 1
                    strgrdRoutingDir.Rows(0).Cells(0).Value = ""
                    shpList.Clear()

                    For Each shp As TGIS_Shape In lrtg.Loop(lrtg.Extent, "( type = 'route' )")
                        uid = shp.Uid
                        i = Convert.ToInt32(shp.GetField("sign"))
                        str = Convert.ToString(shp.GetField("name"))

                        Dim dir As String = ""
                        Select Case i
                            Case -98, -8, 8, 5
                                dir = sign2dir(i)
                            Case 6
                                dir = sign2dir(i) + exitNumber(Convert.ToString(shp.GetField("exit"))) + " exit"
                            Case Else
                                dir = sign2dir(i)
                                If (Not String.IsNullOrEmpty(str)) Then
                                    If (i = 0) Then
                                        dir += " on " + str
                                    Else
                                        dir += " onto " + str
                                    End If
                                End If
                        End Select

                        If (i = 5) Then
                            addDir(dir, uid)
                            bfin = True
                            Continue For
                        End If

                        dist = Convert.ToInt32(shp.GetField("distance"))
                        If (dist < 1000) Then
                            dir += " (" + dist.ToString() + " m, "
                        Else
                            dir += String.Format(" ({0:0.##} km, ", dist / 1000D)
                        End If

                        time = Convert.ToInt32(shp.GetField("time"))
                        hrs = time / 3600
                        mns = (time / 60) - hrs * 60
                        If (hrs = 0) Then
                            If (mns = 0) Then
                                dir += "<1 min)"
                            Else
                                dir += mns.ToString() + " min)"
                            End If
                        Else
                            dir += hrs.ToString() + " h " + mns.ToString() + " min)"
                        End If

                        addDir(dir, uid)
                    Next

                    If (bfin) Then
                        addDir("Reach the final destination", uid)
                    Else
                        addDir("Reach the destination", uid)
                    End If
                Else
                    MessageBox.Show("Route not found.")
                End If
            End If
        End Sub

        Private Function tryConvertToInt32(ByVal _str As String, ByRef _val As Integer) As Boolean
            Dim res As Boolean = False

            Try
                _val = Convert.ToInt32(_str)
                res = True
            Catch
                Return False
            End Try

            Return res
        End Function

        Private Sub btnIsochrone_Click(sender As Object, e As EventArgs) Handles btnIsochrone.Click
            resetLayers()

            If (String.IsNullOrEmpty(edtIsochroneAddress.Text)) Then
                MessageBox.Show("Address not specified.")
                Return
            End If

            Dim time As Integer = 0
            If (Not tryConvertToInt32(edtIsochroneTime.Text, time)) Then
                MessageBox.Show("'" + edtIsochroneTime.Text +
                                "' is not a positive number.")
                Return
            End If

            Dim oiso As TGIS_OSMIsochrone = New TGIS_OSMIsochrone()

            If (rbtnIsochroneProfileCar.Checked) Then
                oiso.Profile = TGIS_OSMRoutingProfile.Car
            ElseIf (rbtnIsochroneProfileBike.Checked) Then
                oiso.Profile = TGIS_OSMRoutingProfile.Bike
            ElseIf (rbtnIsochroneProfileFoot.Checked) Then
                oiso.Profile = TGIS_OSMRoutingProfile.Foot
            End If

            oiso.Buckets = Convert.ToInt32(cmbbxIsochroneBuckets.Text)
            oiso.TimeLimit = time
            Dim liso As TGIS_LayerVector = oiso.Isochrone(edtIsochroneAddress.Text)

            If (liso.GetLastUid() > 0) Then
                lblRoutingDirDist.Text = "Total distance: ?"
                lblRoutingDirTime.Text = "Total time: ?"
                strgrdRoutingDir.Rows.Clear()

                GIS.Add(liso)

                Dim ext As TGIS_Extent = resizeExtent(liso.ProjectedExtent, 500D)
                ext = growExtent(ext, 1.2D)

                GIS.VisibleExtent = ext
            Else
                MessageBox.Show("Address not found.")
            End If
        End Sub

        Private Sub strgrdRoutingDir_DoubleClick(sender As Object, e As EventArgs) Handles strgrdRoutingDir.DoubleClick
            Dim lrtg As TGIS_LayerVector = CType(GIS.Get("route"), TGIS_LayerVector)

            If (lrtg Is Nothing) Then Return

            Dim shp As TGIS_Shape = lrtg.GetShape(shpList(strgrdRoutingDir.SelectedCells(0).RowIndex))
            GIS.VisibleExtent = resizeExtent(shp.ProjectedExtent, 500D)
        End Sub
    End Class
End Namespace
