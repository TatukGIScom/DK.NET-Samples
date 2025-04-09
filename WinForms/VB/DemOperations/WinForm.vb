Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL

Namespace DemOperations
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents btnOpen As Button
        Private WithEvents btnFullExtent As Button
        Private WithEvents btnZoom As Button
        Private WithEvents btnDrag As Button
        Private WithEvents btn3D As Button
        Private WithEvents btnRun As Button
        Private ilIcons As ImageList
        Private panel1 As Panel
        Private panel2 As Panel
        Private WithEvents GIS As TGIS_ViewerWnd
        Private ControlLegend As TGIS_ControlLegend
        Private dlgOpen As OpenFileDialog
        Private gbShadow As GroupBox
        Private WithEvents trbShadow As TrackBar
        Private WithEvents cbxCustomGrid As CheckBox
        Private gbOperations As GroupBox
        Private lbOperations As Label
        Private WithEvents cbOperations As ComboBox
        Private gbHillShadeParams As GroupBox
        Private lbAzimuth As Label
        Private lbAltitude As Label
        Private lbZFactor As Label
        Private tbZFactor As TextBox
        Private tbAltitude As TextBox
        Private tbAzimuth As TextBox
        Private gbSloperParams As GroupBox
        Private cbxCombined As CheckBox
        Private lbScale As Label
        Private lbMode As Label
        Private lbCurvMode As Label
        Private tbScale As TextBox
        Private cbMode As ComboBox
        Private cbxAngle As CheckBox
        Private gbCurvature As GroupBox
        Private cbCurvMode As ComboBox
        Private pbProgress As ProgressBar

        Private Const GIS_MAX_SINGLE As Single = CType(3.4E+38, Single)


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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Dim TgiS_ControlLegendDialogOptions6 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.ilIcons = New System.Windows.Forms.ImageList(Me.components)
            Me.btnFullExtent = New System.Windows.Forms.Button()
            Me.btnZoom = New System.Windows.Forms.Button()
            Me.btnDrag = New System.Windows.Forms.Button()
            Me.btn3D = New System.Windows.Forms.Button()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.pbProgress = New System.Windows.Forms.ProgressBar()
            Me.btnRun = New System.Windows.Forms.Button()
            Me.gbOperations = New System.Windows.Forms.GroupBox()
            Me.gbCurvature = New System.Windows.Forms.GroupBox()
            Me.cbCurvMode = New System.Windows.Forms.ComboBox()
            Me.lbCurvMode = New System.Windows.Forms.Label()
            Me.cbxAngle = New System.Windows.Forms.CheckBox()
            Me.gbSloperParams = New System.Windows.Forms.GroupBox()
            Me.tbScale = New System.Windows.Forms.TextBox()
            Me.cbMode = New System.Windows.Forms.ComboBox()
            Me.lbScale = New System.Windows.Forms.Label()
            Me.lbMode = New System.Windows.Forms.Label()
            Me.gbHillShadeParams = New System.Windows.Forms.GroupBox()
            Me.cbxCombined = New System.Windows.Forms.CheckBox()
            Me.tbZFactor = New System.Windows.Forms.TextBox()
            Me.tbAltitude = New System.Windows.Forms.TextBox()
            Me.tbAzimuth = New System.Windows.Forms.TextBox()
            Me.lbZFactor = New System.Windows.Forms.Label()
            Me.lbAltitude = New System.Windows.Forms.Label()
            Me.lbAzimuth = New System.Windows.Forms.Label()
            Me.lbOperations = New System.Windows.Forms.Label()
            Me.cbOperations = New System.Windows.Forms.ComboBox()
            Me.cbxCustomGrid = New System.Windows.Forms.CheckBox()
            Me.gbShadow = New System.Windows.Forms.GroupBox()
            Me.trbShadow = New System.Windows.Forms.TrackBar()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.dlgOpen = New System.Windows.Forms.OpenFileDialog()
            Me.ControlLegend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.gbOperations.SuspendLayout()
            Me.gbCurvature.SuspendLayout()
            Me.gbSloperParams.SuspendLayout()
            Me.gbHillShadeParams.SuspendLayout()
            Me.gbShadow.SuspendLayout()
            CType(Me.trbShadow, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnOpen
            '
            Me.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnOpen.ForeColor = System.Drawing.SystemColors.AppWorkspace
            Me.btnOpen.ImageIndex = 0
            Me.btnOpen.ImageList = Me.ilIcons
            Me.btnOpen.Location = New System.Drawing.Point(0, 0)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(31, 23)
            Me.btnOpen.TabIndex = 0
            Me.btnOpen.UseVisualStyleBackColor = True
            '
            'ilIcons
            '
            Me.ilIcons.ImageStream = CType(resources.GetObject("ilIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.ilIcons.TransparentColor = System.Drawing.Color.Fuchsia
            Me.ilIcons.Images.SetKeyName(0, "Open.bmp")
            Me.ilIcons.Images.SetKeyName(1, "FullExtent.bmp")
            Me.ilIcons.Images.SetKeyName(2, "ZoomEx.bmp")
            Me.ilIcons.Images.SetKeyName(3, "Drag.bmp")
            Me.ilIcons.Images.SetKeyName(4, "3DRotate.bmp")
            '
            'btnFullExtent
            '
            Me.btnFullExtent.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnFullExtent.ForeColor = System.Drawing.SystemColors.AppWorkspace
            Me.btnFullExtent.ImageIndex = 1
            Me.btnFullExtent.ImageList = Me.ilIcons
            Me.btnFullExtent.Location = New System.Drawing.Point(30, 0)
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.Size = New System.Drawing.Size(31, 23)
            Me.btnFullExtent.TabIndex = 1
            Me.btnFullExtent.UseVisualStyleBackColor = True
            '
            'btnZoom
            '
            Me.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnZoom.ForeColor = System.Drawing.SystemColors.AppWorkspace
            Me.btnZoom.ImageIndex = 2
            Me.btnZoom.ImageList = Me.ilIcons
            Me.btnZoom.Location = New System.Drawing.Point(60, 0)
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Size = New System.Drawing.Size(31, 23)
            Me.btnZoom.TabIndex = 2
            Me.btnZoom.UseVisualStyleBackColor = True
            '
            'btnDrag
            '
            Me.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btnDrag.ForeColor = System.Drawing.SystemColors.AppWorkspace
            Me.btnDrag.ImageIndex = 3
            Me.btnDrag.ImageList = Me.ilIcons
            Me.btnDrag.Location = New System.Drawing.Point(90, 0)
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Size = New System.Drawing.Size(31, 23)
            Me.btnDrag.TabIndex = 3
            Me.btnDrag.UseVisualStyleBackColor = True
            '
            'btn3D
            '
            Me.btn3D.FlatStyle = System.Windows.Forms.FlatStyle.Flat
            Me.btn3D.ForeColor = System.Drawing.SystemColors.AppWorkspace
            Me.btn3D.ImageIndex = 4
            Me.btn3D.ImageList = Me.ilIcons
            Me.btn3D.Location = New System.Drawing.Point(120, 0)
            Me.btn3D.Name = "btn3D"
            Me.btn3D.Size = New System.Drawing.Size(31, 23)
            Me.btn3D.TabIndex = 4
            Me.btn3D.UseVisualStyleBackColor = True
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.pbProgress)
            Me.panel1.Controls.Add(Me.btnRun)
            Me.panel1.Controls.Add(Me.gbOperations)
            Me.panel1.Controls.Add(Me.cbxCustomGrid)
            Me.panel1.Controls.Add(Me.gbShadow)
            Me.panel1.Location = New System.Drawing.Point(0, 30)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(223, 655)
            Me.panel1.TabIndex = 5
            '
            'pbProgress
            '
            Me.pbProgress.Location = New System.Drawing.Point(5, 379)
            Me.pbProgress.Name = "pbProgress"
            Me.pbProgress.Size = New System.Drawing.Size(210, 23)
            Me.pbProgress.TabIndex = 0
            '
            'btnRun
            '
            Me.btnRun.Location = New System.Drawing.Point(60, 341)
            Me.btnRun.Name = "btnRun"
            Me.btnRun.Size = New System.Drawing.Size(105, 23)
            Me.btnRun.TabIndex = 0
            Me.btnRun.Text = "Run operation"
            Me.btnRun.UseVisualStyleBackColor = True
            '
            'gbOperations
            '
            Me.gbOperations.AutoSize = True
            Me.gbOperations.Controls.Add(Me.gbCurvature)
            Me.gbOperations.Controls.Add(Me.cbxAngle)
            Me.gbOperations.Controls.Add(Me.gbSloperParams)
            Me.gbOperations.Controls.Add(Me.gbHillShadeParams)
            Me.gbOperations.Controls.Add(Me.lbOperations)
            Me.gbOperations.Controls.Add(Me.cbOperations)
            Me.gbOperations.Location = New System.Drawing.Point(5, 93)
            Me.gbOperations.Name = "gbOperations"
            Me.gbOperations.Size = New System.Drawing.Size(210, 242)
            Me.gbOperations.TabIndex = 2
            Me.gbOperations.TabStop = False
            Me.gbOperations.Text = "Operations"
            '
            'gbCurvature
            '
            Me.gbCurvature.Controls.Add(Me.cbCurvMode)
            Me.gbCurvature.Controls.Add(Me.lbCurvMode)
            Me.gbCurvature.Location = New System.Drawing.Point(25, 67)
            Me.gbCurvature.Name = "gbCurvature"
            Me.gbCurvature.Size = New System.Drawing.Size(161, 51)
            Me.gbCurvature.TabIndex = 5
            Me.gbCurvature.TabStop = False
            Me.gbCurvature.Visible = False
            '
            'cbCurvMode
            '
            Me.cbCurvMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbCurvMode.FormattingEnabled = True
            Me.cbCurvMode.Items.AddRange(New Object() {"Profile", "Plan"})
            Me.cbCurvMode.SelectedIndex = 0
            Me.cbCurvMode.Location = New System.Drawing.Point(67, 19)
            Me.cbCurvMode.Name = "cbCurvMode"
            Me.cbCurvMode.Size = New System.Drawing.Size(74, 21)
            Me.cbCurvMode.TabIndex = 1
            '
            'lbCurvMode
            '
            Me.lbCurvMode.AutoSize = True
            Me.lbCurvMode.Location = New System.Drawing.Point(16, 22)
            Me.lbCurvMode.Name = "lbCurvMode"
            Me.lbCurvMode.Size = New System.Drawing.Size(37, 13)
            Me.lbCurvMode.TabIndex = 0
            Me.lbCurvMode.Text = "Mode:"
            '
            'cbxAngle
            '
            Me.cbxAngle.AutoSize = True
            Me.cbxAngle.Location = New System.Drawing.Point(35, 76)
            Me.cbxAngle.Name = "cbxAngle"
            Me.cbxAngle.Size = New System.Drawing.Size(141, 17)
            Me.cbxAngle.TabIndex = 4
            Me.cbxAngle.Text = "Angle instead of azimuth"
            Me.cbxAngle.UseVisualStyleBackColor = True
            Me.cbxAngle.Visible = False
            '
            'gbSloperParams
            '
            Me.gbSloperParams.AutoSize = True
            Me.gbSloperParams.Controls.Add(Me.tbScale)
            Me.gbSloperParams.Controls.Add(Me.cbMode)
            Me.gbSloperParams.Controls.Add(Me.lbScale)
            Me.gbSloperParams.Controls.Add(Me.lbMode)
            Me.gbSloperParams.Location = New System.Drawing.Point(25, 67)
            Me.gbSloperParams.Name = "gbSloperParams"
            Me.gbSloperParams.Size = New System.Drawing.Size(161, 78)
            Me.gbSloperParams.TabIndex = 3
            Me.gbSloperParams.TabStop = False
            Me.gbSloperParams.Visible = False
            '
            'tbScale
            '
            Me.tbScale.Location = New System.Drawing.Point(67, 39)
            Me.tbScale.Name = "tbScale"
            Me.tbScale.Size = New System.Drawing.Size(74, 20)
            Me.tbScale.TabIndex = 3
            Me.tbScale.Text = "111120"
            '
            'cbMode
            '
            Me.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbMode.FormattingEnabled = True
            Me.cbMode.Items.AddRange(New Object() {"Degrees", "Percent"})
            Me.cbMode.SelectedIndex = 0
            Me.cbMode.Location = New System.Drawing.Point(67, 13)
            Me.cbMode.Name = "cbMode"
            Me.cbMode.Size = New System.Drawing.Size(74, 21)
            Me.cbMode.TabIndex = 2
            '
            'lbScale
            '
            Me.lbScale.AutoSize = True
            Me.lbScale.Location = New System.Drawing.Point(16, 42)
            Me.lbScale.Name = "lbScale"
            Me.lbScale.Size = New System.Drawing.Size(37, 13)
            Me.lbScale.TabIndex = 1
            Me.lbScale.Text = "Scale:"
            '
            'lbMode
            '
            Me.lbMode.AutoSize = True
            Me.lbMode.Location = New System.Drawing.Point(16, 16)
            Me.lbMode.Name = "lbMode"
            Me.lbMode.Size = New System.Drawing.Size(37, 13)
            Me.lbMode.TabIndex = 0
            Me.lbMode.Text = "Mode:"
            '
            'gbHillShadeParams
            '
            Me.gbHillShadeParams.AutoSize = True
            Me.gbHillShadeParams.Controls.Add(Me.cbxCombined)
            Me.gbHillShadeParams.Controls.Add(Me.tbZFactor)
            Me.gbHillShadeParams.Controls.Add(Me.tbAltitude)
            Me.gbHillShadeParams.Controls.Add(Me.tbAzimuth)
            Me.gbHillShadeParams.Controls.Add(Me.lbZFactor)
            Me.gbHillShadeParams.Controls.Add(Me.lbAltitude)
            Me.gbHillShadeParams.Controls.Add(Me.lbAzimuth)
            Me.gbHillShadeParams.Location = New System.Drawing.Point(25, 67)
            Me.gbHillShadeParams.Name = "gbHillShadeParams"
            Me.gbHillShadeParams.Size = New System.Drawing.Size(161, 128)
            Me.gbHillShadeParams.TabIndex = 2
            Me.gbHillShadeParams.TabStop = False
            Me.gbHillShadeParams.Visible = False
            '
            'cbxCombined
            '
            Me.cbxCombined.AutoSize = True
            Me.cbxCombined.Location = New System.Drawing.Point(68, 92)
            Me.cbxCombined.Name = "cbxCombined"
            Me.cbxCombined.Size = New System.Drawing.Size(73, 17)
            Me.cbxCombined.TabIndex = 6
            Me.cbxCombined.Text = "Combined"
            Me.cbxCombined.UseVisualStyleBackColor = True
            '
            'tbZFactor
            '
            Me.tbZFactor.Location = New System.Drawing.Point(67, 66)
            Me.tbZFactor.Name = "tbZFactor"
            Me.tbZFactor.Size = New System.Drawing.Size(74, 20)
            Me.tbZFactor.TabIndex = 5
            Me.tbZFactor.Text = "1"
            '
            'tbAltitude
            '
            Me.tbAltitude.Location = New System.Drawing.Point(67, 42)
            Me.tbAltitude.Name = "tbAltitude"
            Me.tbAltitude.Size = New System.Drawing.Size(74, 20)
            Me.tbAltitude.TabIndex = 4
            Me.tbAltitude.Text = "45"
            '
            'tbAzimuth
            '
            Me.tbAzimuth.Location = New System.Drawing.Point(67, 17)
            Me.tbAzimuth.Name = "tbAzimuth"
            Me.tbAzimuth.Size = New System.Drawing.Size(74, 20)
            Me.tbAzimuth.TabIndex = 3
            Me.tbAzimuth.Text = "315"
            '
            'lbZFactor
            '
            Me.lbZFactor.AutoSize = True
            Me.lbZFactor.Location = New System.Drawing.Point(17, 69)
            Me.lbZFactor.Name = "lbZFactor"
            Me.lbZFactor.Size = New System.Drawing.Size(47, 13)
            Me.lbZFactor.TabIndex = 2
            Me.lbZFactor.Text = "Z factor:"
            '
            'lbAltitude
            '
            Me.lbAltitude.AutoSize = True
            Me.lbAltitude.Location = New System.Drawing.Point(16, 45)
            Me.lbAltitude.Name = "lbAltitude"
            Me.lbAltitude.Size = New System.Drawing.Size(45, 13)
            Me.lbAltitude.TabIndex = 1
            Me.lbAltitude.Text = "Altitude:"
            '
            'lbAzimuth
            '
            Me.lbAzimuth.AutoSize = True
            Me.lbAzimuth.Location = New System.Drawing.Point(16, 20)
            Me.lbAzimuth.Name = "lbAzimuth"
            Me.lbAzimuth.Size = New System.Drawing.Size(47, 13)
            Me.lbAzimuth.TabIndex = 0
            Me.lbAzimuth.Text = "Azimuth:"
            '
            'lbOperations
            '
            Me.lbOperations.AutoSize = True
            Me.lbOperations.Location = New System.Drawing.Point(24, 23)
            Me.lbOperations.Name = "lbOperations"
            Me.lbOperations.Size = New System.Drawing.Size(56, 13)
            Me.lbOperations.TabIndex = 1
            Me.lbOperations.Text = "Operation:"
            '
            'cbOperations
            '
            Me.cbOperations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbOperations.FormattingEnabled = True
            Me.cbOperations.Items.AddRange(New Object() {"Hillshade", "Slope", "Slope Hydro", "Aspect", "TRI", "TPI", "Roughness", "Total Curvature", "Matrix Gain", "Flow dir"})
            Me.cbOperations.SelectedIndex = 0
            Me.cbOperations.Location = New System.Drawing.Point(25, 45)
            Me.cbOperations.Name = "cbOperations"
            Me.cbOperations.Size = New System.Drawing.Size(161, 21)
            Me.cbOperations.TabIndex = 0
            '
            'cbxCustomGrid
            '
            Me.cbxCustomGrid.AutoSize = True
            Me.cbxCustomGrid.Location = New System.Drawing.Point(30, 70)
            Me.cbxCustomGrid.Name = "cbxCustomGrid"
            Me.cbxCustomGrid.Size = New System.Drawing.Size(173, 17)
            Me.cbxCustomGrid.TabIndex = 1
            Me.cbxCustomGrid.Text = "Attach custom grid operation    "
            Me.cbxCustomGrid.UseVisualStyleBackColor = True
            '
            'gbShadow
            '
            Me.gbShadow.Controls.Add(Me.trbShadow)
            Me.gbShadow.Location = New System.Drawing.Point(5, 3)
            Me.gbShadow.Name = "gbShadow"
            Me.gbShadow.Size = New System.Drawing.Size(210, 61)
            Me.gbShadow.TabIndex = 0
            Me.gbShadow.TabStop = False
            Me.gbShadow.Text = "Shadow angle"
            '
            'trbShadow
            '
            Me.trbShadow.Location = New System.Drawing.Point(4, 12)
            Me.trbShadow.Maximum = 360
            Me.trbShadow.Minimum = 1
            Me.trbShadow.Name = "trbShadow"
            Me.trbShadow.Size = New System.Drawing.Size(201, 45)
            Me.trbShadow.TabIndex = 0
            Me.trbShadow.TickFrequency = 30
            Me.trbShadow.Value = 90
            '
            'panel2
            '
            Me.panel2.Location = New System.Drawing.Point(0, 691)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(935, 19)
            Me.panel2.TabIndex = 6
            '
            'ControlLegend
            '
            Me.ControlLegend.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            TgiS_ControlLegendDialogOptions6.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions6.VectorWizardUniqueSearchLimit = 16384
            Me.ControlLegend.DialogOptions = TgiS_ControlLegendDialogOptions6
            Me.ControlLegend.GIS_Group = Nothing
            Me.ControlLegend.GIS_Layer = Nothing
            Me.ControlLegend.GIS_Viewer = Me.GIS
            Me.ControlLegend.Location = New System.Drawing.Point(784, 30)
            Me.ControlLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.ControlLegend.Name = "ControlLegend"
            Me.ControlLegend.Options = CType(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.ControlLegend.ReverseOrder = False
            Me.ControlLegend.Size = New System.Drawing.Size(151, 655)
            Me.ControlLegend.TabIndex = 8
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.CursorFor3DSelect = Nothing
            Me.GIS.CursorForCameraZoom = Nothing
            Me.GIS.Location = New System.Drawing.Point(219, 30)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(570, 655)
            Me.GIS.TabIndex = 7
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(934, 711)
            Me.Controls.Add(Me.ControlLegend)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel2)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.btn3D)
            Me.Controls.Add(Me.btnDrag)
            Me.Controls.Add(Me.btnZoom)
            Me.Controls.Add(Me.btnFullExtent)
            Me.Controls.Add(Me.btnOpen)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - DemOperations"
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            Me.gbOperations.ResumeLayout(False)
            Me.gbOperations.PerformLayout()
            Me.gbCurvature.ResumeLayout(False)
            Me.gbCurvature.PerformLayout()
            Me.gbSloperParams.ResumeLayout(False)
            Me.gbSloperParams.PerformLayout()
            Me.gbHillShadeParams.ResumeLayout(False)
            Me.gbHillShadeParams.PerformLayout()
            Me.gbShadow.ResumeLayout(False)
            Me.gbShadow.PerformLayout()
            CType(Me.trbShadow, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            dlgOpen.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.All, False)
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf")
            GIS.FullExtent()
        End Sub

        Private Function ChangeDem(ByVal _layer As Object,
                                   ByVal _extent As TGIS_Extent,
                                   ByVal _source As Single()(),
                                   ByRef _output As Single()(),
                                   ByVal _width As Integer,
                                   ByVal _height As Integer,
                                   ByRef _minz As Single,
                                   ByRef _maxz As Single
                                  ) As Boolean
            Const DEG_TO_RAD As Double = Math.PI / 180.0

            Dim xsize As Integer
            Dim ysize As Integer
            Dim l1, l2, l3 As Integer
            Dim sin_alt_rad As Double
            Dim cos_alt_zsf As Double
            Dim az_rad As Double
            Dim square_z_sf As Double
            Dim z_scale_factor As Double
            Dim ZFactor As Double
            Dim Azimuth As Double
            Dim Altitude As Double
            Dim XRes As Double
            Dim YRes As Double
            Dim Scale As Double
            Dim xscale As Double
            Dim yscale As Double
            Dim x, y, aspect, xx_plus_yy, cang As Double
            Dim minz, maxz As Single
            Dim AWindow As Single()
            Dim Val As Single
            Dim inodata As Single
            Dim Combined As Boolean
            Dim usealg As Boolean
            Dim abrt As Boolean
            Dim final_result As Boolean

            final_result = True
            ReDim AWindow(9)

            xsize = _width
            ysize = _height
            xscale = (_extent.XMax - _extent.XMin) / xsize
            yscale = (_extent.YMax - _extent.YMin) / ysize
            abrt = False
            inodata = CType(_layer, TGIS_LayerPixel).NoDataValue

            XRes = xscale
            YRes = yscale

            Scale = 1
            minz = GIS_MAX_SINGLE
            maxz = -GIS_MAX_SINGLE

            ZFactor = 2
            Azimuth = trbShadow.Value
            Altitude = 15
            Combined = False

            sin_alt_rad = Math.Sin(Altitude * DEG_TO_RAD)
            az_rad = Azimuth * DEG_TO_RAD
            z_scale_factor = ZFactor / (2 * Scale)
            cos_alt_zsf = Math.Cos(Altitude * DEG_TO_RAD)
            square_z_sf = z_scale_factor * z_scale_factor

            For i As Integer = 2 To _height - 2
                l1 = i - 2
                l2 = i - 1
                l3 = i

                For j As Integer = 1 To _width - 2
                    AWindow(0) = _source(l1)(j - 1)
                    AWindow(1) = _source(l1)(j)
                    AWindow(2) = _source(l1)(j + 1)
                    AWindow(3) = _source(l2)(j - 1)
                    AWindow(4) = _source(l2)(j)
                    AWindow(5) = _source(l2)(j + 1)
                    AWindow(6) = _source(l3)(j - 1)
                    AWindow(7) = _source(l3)(j)
                    AWindow(8) = _source(l3)(j + 1)

                    usealg = True
                    Val = inodata

                    If Math.Abs(AWindow(4) - inodata) < 0.0000000001 Then
                        Val = inodata
                        usealg = False
                    Else
                        For k As Integer = 0 To 8
                            If Math.Abs(AWindow(k) - inodata) < 0.0000000001 Then
                                Val = inodata
                                usealg = False
                                Exit For
                            End If
                        Next
                    End If
                    If usealg Then
                        x = (AWindow(3) - AWindow(5)) / XRes
                        y = (AWindow(7) - AWindow(1)) / YRes

                        xx_plus_yy = x * x + y * y
                        aspect = Math.Atan2(y, x)
                        cang = (sin_alt_rad - cos_alt_zsf * Math.Sqrt(xx_plus_yy) * Math.Sin(aspect - az_rad)) / Math.Sqrt(1 + square_z_sf * xx_plus_yy)

                        If (cang <= 0.0) Then
                            cang = 1.0
                        Else
                            cang = 1.0 + (254.0 * cang)
                        End If
                        Val = CType(cang, Single)
                    End If
                    If (_source(l1)(j) <> inodata) Then
                        _output(l1)(j) = Val
                    End If
                    If ((Val < minz) And (Val! = inodata)) Then
                        minz = Val
                    End If
                    If ((Val > maxz) And (Val! = inodata)) Then
                        maxz = Val
                    End If
                Next
            Next
            _minz = minz
            _maxz = maxz

            Return final_result

        End Function

        Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
            dlgOpen.ShowDialog()
            GIS.Open(dlgOpen.FileName)
        End Sub

        Private Sub btnFullExtent_Click(sender As Object, e As EventArgs) Handles btnFullExtent.Click
            If (GIS.IsEmpty) Then Return

            GIS.FullExtent()
        End Sub

        Private Sub btnZoom_Click(sender As Object, e As EventArgs) Handles btnZoom.Click
            If (GIS.IsEmpty) Then Return

            GIS.Mode = TGIS_ViewerMode.Zoom
        End Sub

        Private Sub btnDrag_Click(sender As Object, e As EventArgs) Handles btnDrag.Click
            If (GIS.IsEmpty) Then Return

            GIS.Mode = TGIS_ViewerMode.Drag
        End Sub

        Private Sub btn3D_Click(sender As Object, e As EventArgs) Handles btn3D.Click
            If (GIS.IsEmpty) Then Return

            GIS.View3D = Not GIS.View3D
        End Sub

        Private Sub trbShadow_Scroll(sender As Object, e As EventArgs) Handles trbShadow.Scroll
            Dim lp As TGIS_LayerPixel

            lp = CType(GIS.Items(0), TGIS_LayerPixel)
            If (lp Is Nothing) Then Return

            lp.Params.Pixel.GridShadowAngle = trbShadow.Value

            If (Not GIS.InPaint) Then
                GIS.InvalidateWholeMap()
            End If
        End Sub

        Private Sub cbxCustomGrid_CheckedChanged(sender As Object, e As EventArgs) Handles cbxCustomGrid.CheckedChanged
            Dim lp As TGIS_LayerPixel

            lp = CType(GIS.Items(0), TGIS_LayerPixel)

            If (lp Is Nothing) Then Return

            If (cbxCustomGrid.Checked) Then
                lp.Params.Pixel.AltitudeMapZones.Clear()
                lp.Params.Pixel.GridShadow = False
                lp.GridOperationEvent = AddressOf ChangeDem
            Else
                lp.GridOperationEvent = Nothing
                lp.Params.Pixel.GridShadow = True
            End If
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub cbOperations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbOperations.SelectedIndexChanged
            gbHillShadeParams.Visible = False
            gbCurvature.Visible = False
            cbxAngle.Visible = False
            gbSloperParams.Visible = False

            Select Case (cbOperations.SelectedIndex)
                Case 0 : gbHillShadeParams.Visible = True
                Case 1 : gbSloperParams.Visible = True
                Case 2 : gbSloperParams.Visible = True
                Case 3 : cbxAngle.Visible = True
                Case 7 : gbCurvature.Visible = True
            End Select
        End Sub

        Private Sub btnRun_Click(sender As Object, e As EventArgs) Handles btnRun.Click
            Dim lp As TGIS_LayerPixel
            Dim ld As TGIS_LayerPixel
            Dim dop As TGIS_DemOperation
            Dim dem As TGIS_DemGenerator
            Dim sm As TGIS_DemSlopeMode
            Dim cm As TGIS_DemTotalCurvatureMode

            lp = CType(GIS.Items(0), TGIS_LayerPixel)

            ld = New TGIS_LayerPixel
            ld.Name = "out_"
            ld.CS = lp.CS
            ld.Build(True, lp.CS, lp.Extent, lp.BitWidth, lp.BitHeight)

            dem = New TGIS_DemGenerator

            Select Case cbOperations.SelectedIndex
                'HillShade
                Case 0
                    dop = New TGIS_DemOperationHillShade(Double.Parse(tbZFactor.Text),
                                                         Double.Parse(tbAzimuth.Text),
                                                         Double.Parse(tbAltitude.Text),
                                                         cbxCombined.Checked)
                'Slope
                Case 1
                    Select Case cbMode.SelectedIndex
                        Case 0 : sm = TGIS_DemSlopeMode.Degrees
                        Case 1 : sm = TGIS_DemSlopeMode.Percent
                        Case Else : sm = TGIS_DemSlopeMode.Degrees
                    End Select
                    dop = New TGIS_DemOperationSlope(sm, Double.Parse(tbScale.Text))
                'SlopeHydro
                Case 2
                    Select Case cbMode.SelectedIndex
                        Case 0 : sm = TGIS_DemSlopeMode.Degrees
                        Case 1 : sm = TGIS_DemSlopeMode.Percent
                        Case Else : sm = TGIS_DemSlopeMode.Degrees
                    End Select
                    dop = New TGIS_DemOperationSlopeHydro(sm, Double.Parse(tbScale.Text))
                'Aspect
                Case 3 : dop = New TGIS_DemOperationAspect(cbxAngle.Checked)
                ' TRI
                Case 4 : dop = New TGIS_DemOperationTRI()
                'TPI
                Case 5 : dop = New TGIS_DemOperationTPI()
                'Roughness
                Case 6 : dop = New TGIS_DemOperationRoughness()
                'TotalCurvature
                Case 7
                    Select Case cbCurvMode.SelectedIndex
                        Case 0 : cm = TGIS_DemTotalCurvatureMode.Profile
                        Case 1 : cm = TGIS_DemTotalCurvatureMode.Plan
                        Case Else : cm = TGIS_DemTotalCurvatureMode.Profile
                    End Select
                    dop = New TGIS_DemOperationTotalCurvature(cm)
                'MatrixGain
                Case 8 : dop = New TGIS_DemOperationMatrixGain()
                'Flow dir
                Case 9 : dop = New TGIS_DemOperationFlowDir()
                    'default
                Case Else : dop = New TGIS_DemOperation()
            End Select

            ld.Name = "out_" + dop.Description

            If Not GIS.Get(ld.Name) Is Nothing Then
                GIS.Delete(ld.Name)
            End If

            ld.Params.Pixel.GridShadow = False
            GIS.Add(ld)
            dem.Process(lp, lp.Extent, ld, dop, AddressOf GIS_BusyEvent)
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub GIS_BusyEvent(_sender As Object, _e As TGIS_BusyEventArgs) Handles GIS.BusyEvent
            If _e.EndPos <= 0 Then
                pbProgress.Visible = False
            Else
                pbProgress.Visible = True
                pbProgress.Value = CType(_e.Pos / _e.EndPos * 100, Integer)
            End If
            pbProgress.Update()
            Application.DoEvents()
        End Sub
    End Class
End Namespace

