
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL
Imports System

Namespace Interpolation
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        Private GIS As TGIS_ViewerWnd
        Private WithEvents btnGenerate As Button
        Private lblMethod As Label
        Private rbIDW As RadioButton
        Private rbKriging As RadioButton
        Private rbSpline As RadioButton
        Private rbHeatMap As RadioButton
        Private rbConcentrationMap As RadioButton
        Private progressBar1 As ProgressBar
        Private lblSemivariance As Label
        Private cbSemivariance As ComboBox
        Private Const FIELD_VALUE As [String] = "TEMP"
        Private Const GRID_RESOLUTION As Integer = 400
        Private src As TGIS_LayerVector
        Private dst As TGIS_LayerPixel
        Private plg As TGIS_LayerVector
        Private ext As TGIS_Extent
        Private rat As Double
        Private dx As Double
        Private dy As Double
        Private clr As TGIS_Color
        Private i As Integer

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            InitializeComponent()
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
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
            Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.btnGenerate = New System.Windows.Forms.Button()
            Me.lblMethod = New System.Windows.Forms.Label()
            Me.rbIDW = New System.Windows.Forms.RadioButton()
            Me.rbKriging = New System.Windows.Forms.RadioButton()
            Me.rbSpline = New System.Windows.Forms.RadioButton()
            Me.rbHeatMap = New System.Windows.Forms.RadioButton()
            Me.rbConcentrationMap = New System.Windows.Forms.RadioButton()
            Me.progressBar1 = New System.Windows.Forms.ProgressBar()
            Me.lblSemivariance = New System.Windows.Forms.Label()
            Me.cbSemivariance = New System.Windows.Forms.ComboBox()
            Me.SuspendLayout()
            ' 
            ' GIS
            ' 
            Me.GIS.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right)
            Me.GIS.Location = New System.Drawing.Point(152, 12)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CInt(CByte(255)), CInt(CByte(0)), CInt(CByte(0)))
            Me.GIS.Size = New System.Drawing.Size(420, 358)
            Me.GIS.TabIndex = 0
            ' 
            ' btnGenerate
            ' 
            Me.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left
            Me.btnGenerate.Location = New System.Drawing.Point(12, 376)
            Me.btnGenerate.Name = "btnGenerate"
            Me.btnGenerate.Size = New System.Drawing.Size(134, 23)
            Me.btnGenerate.TabIndex = 2
            Me.btnGenerate.Text = "Generate"
            Me.btnGenerate.UseVisualStyleBackColor = True
            ' 
            ' lblMethod
            ' 
            Me.lblMethod.AutoSize = True
            Me.lblMethod.Location = New System.Drawing.Point(12, 13)
            Me.lblMethod.Name = "lblMethod"
            Me.lblMethod.Size = New System.Drawing.Size(43, 13)
            Me.lblMethod.TabIndex = 3
            Me.lblMethod.Text = "Method"
            ' 
            ' rbIDW
            ' 
            Me.rbIDW.AutoSize = True
            Me.rbIDW.Location = New System.Drawing.Point(12, 30)
            Me.rbIDW.Name = "rbIDW"
            Me.rbIDW.Size = New System.Drawing.Size(107, 17)
            Me.rbIDW.TabIndex = 4
            Me.rbIDW.TabStop = True
            Me.rbIDW.Text = "IDW interpolation"
            Me.rbIDW.UseVisualStyleBackColor = True
            ' 
            ' rbKriging
            ' 
            Me.rbKriging.AutoSize = True
            Me.rbKriging.Location = New System.Drawing.Point(12, 53)
            Me.rbKriging.Name = "rbKriging"
            Me.rbKriging.Size = New System.Drawing.Size(118, 17)
            Me.rbKriging.TabIndex = 5
            Me.rbKriging.TabStop = True
            Me.rbKriging.Text = "Kriging Interpolation"
            Me.rbKriging.UseVisualStyleBackColor = True
            ' 
            ' rbSpline
            ' 
            Me.rbSpline.AutoSize = True
            Me.rbSpline.Location = New System.Drawing.Point(12, 76)
            Me.rbSpline.Name = "rbSpline"
            Me.rbSpline.Size = New System.Drawing.Size(114, 17)
            Me.rbSpline.TabIndex = 6
            Me.rbSpline.TabStop = True
            Me.rbSpline.Text = "Spline interpolation"
            Me.rbSpline.UseVisualStyleBackColor = True
            ' 
            ' rbHeatMap
            ' 
            Me.rbHeatMap.AutoSize = True
            Me.rbHeatMap.Location = New System.Drawing.Point(12, 99)
            Me.rbHeatMap.Name = "rbHeatMap"
            Me.rbHeatMap.Size = New System.Drawing.Size(72, 17)
            Me.rbHeatMap.TabIndex = 7
            Me.rbHeatMap.TabStop = True
            Me.rbHeatMap.Text = "Heat Map"
            Me.rbHeatMap.UseVisualStyleBackColor = True
            ' 
            ' rbConcentrationMap
            ' 
            Me.rbConcentrationMap.AutoSize = True
            Me.rbConcentrationMap.Location = New System.Drawing.Point(12, 122)
            Me.rbConcentrationMap.Name = "rbConcentrationMap"
            Me.rbConcentrationMap.Size = New System.Drawing.Size(115, 17)
            Me.rbConcentrationMap.TabIndex = 8
            Me.rbConcentrationMap.TabStop = True
            Me.rbConcentrationMap.Text = "Concentration Map"
            Me.rbConcentrationMap.UseVisualStyleBackColor = True
            ' 
            ' progressBar1
            ' 
            Me.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right)
            Me.progressBar1.Location = New System.Drawing.Point(152, 376)
            Me.progressBar1.Name = "progressBar1"
            Me.progressBar1.Size = New System.Drawing.Size(420, 23)
            Me.progressBar1.TabIndex = 9
            ' 
            ' lblSemivariance
            ' 
            Me.lblSemivariance.AutoSize = True
            Me.lblSemivariance.Location = New System.Drawing.Point(12, 157)
            Me.lblSemivariance.Name = "lblSemivariance"
            Me.lblSemivariance.Size = New System.Drawing.Size(71, 13)
            Me.lblSemivariance.TabIndex = 10
            Me.lblSemivariance.Text = "Semivariance"
            ' 
            ' cbSemivariance
            ' 
            Me.cbSemivariance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cbSemivariance.FormattingEnabled = True
            Me.cbSemivariance.Items.AddRange(New Object() {"Power Law", "Exponential", "Gaussian", "Spherical", "Circular", "Linear"})
            Me.cbSemivariance.Location = New System.Drawing.Point(12, 173)
            Me.cbSemivariance.Name = "cbSemivariance"
            Me.cbSemivariance.Size = New System.Drawing.Size(134, 21)
            Me.cbSemivariance.TabIndex = 11
            ' 
            ' WinForm
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0F, 96.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(584, 411)
            Me.Controls.Add(Me.cbSemivariance)
            Me.Controls.Add(Me.lblSemivariance)
            Me.Controls.Add(Me.progressBar1)
            Me.Controls.Add(Me.rbConcentrationMap)
            Me.Controls.Add(Me.rbHeatMap)
            Me.Controls.Add(Me.rbSpline)
            Me.Controls.Add(Me.rbKriging)
            Me.Controls.Add(Me.rbIDW)
            Me.Controls.Add(Me.lblMethod)
            Me.Controls.Add(Me.btnGenerate)
            Me.Controls.Add(Me.GIS)
            Me.Icon = DirectCast(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Interpolation"
            ''Me.Load += New System.EventHandler(AddressOf Me.WinForm_Load)
            Me.ResumeLayout(False)
            Me.PerformLayout()

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

        Private Sub WinForm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "Samples\Interpolation\Interpolation.ttkproject")
            GIS.CS = TGIS_CSFactory.ByEPSG(3395)

            GIS.FullExtent()

            AddHandler rbIDW.CheckedChanged, AddressOf doRbAnyClick
            AddHandler rbKriging.CheckedChanged, AddressOf doRbAnyClick
            AddHandler rbSpline.CheckedChanged, AddressOf doRbAnyClick
            AddHandler rbHeatMap.CheckedChanged, AddressOf doRbAnyClick
            AddHandler rbConcentrationMap.CheckedChanged, AddressOf doRbAnyClick

            rbIDW.Checked = True
            cbSemivariance.SelectedIndex = 0
        End Sub

        Private Sub doRbAnyClick(_sender As [Object], _e As EventArgs)
            If rbKriging.Checked Then
                lblSemivariance.Visible = True
                cbSemivariance.Visible = True
            Else
                lblSemivariance.Visible = False
                cbSemivariance.Visible = False
            End If
        End Sub

        Private Sub doBusyEvent(_sender As [Object], _e As TGIS_BusyEventArgs)
            If _e.Pos < 0 Then
                progressBar1.Value = progressBar1.Maximum
            ElseIf _e.Pos = 0 Then
                progressBar1.Minimum = 0
                progressBar1.Maximum = CInt(_e.EndPos)
                progressBar1.Value = 0
            Else
                If _e.Pos Mod 100 = 0 Then
                    progressBar1.Value = CInt(_e.Pos)
                End If
            End If
        End Sub

        Private Sub doIDW()
            Dim vtg As TGIS_InterpolationIDW

            vtg = New TGIS_InterpolationIDW()

            ' for windowed version of this method you need to set Windowed=True
            ' and at least the Radius, e.g.
            ' vtg.Windowed := True ;
            ' vtg.Radius := ( ext.XMax - ext.XMin )/5.0 ;

            ' attach the event to automatically update the progress bar
            AddHandler vtg.BusyEvent, AddressOf doBusyEvent
            ' set the exponent parameter of the IDW formula (default is 2.0,
            ' 3.0 gives nice results in most cases)
            vtg.Exponent = 3.0
            ' generate the Inverse Distance Squared (IDW) interpolation grid
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent)
        End Sub

        Private Sub doKriging()
            Dim vtg As TGIS_InterpolationKriging

            vtg = New TGIS_InterpolationKriging()

            ' for windowed version of this method you need to set Windowed=True
            ' and at least the Radius, e.g.
            ' vtg.Windowed := True ;
            ' vtg.Radius := ( ext.XMax - ext.XMin )/5.0 ;

            ' attach the event to automatically update the progress bar
            AddHandler vtg.BusyEvent, AddressOf doBusyEvent

            ' set Semivarinace; default is Power Law (code for case 0 is not needed)
            Select Case cbSemivariance.SelectedIndex
                Case 0
                    vtg.Semivariance = New TGIS_SemivariancePowerLaw()
                    Exit Select
                Case 1
                    vtg.Semivariance = New TGIS_SemivarianceExponential()
                    Exit Select
                Case 2
                    vtg.Semivariance = New TGIS_SemivarianceGaussian()
                    Exit Select
                Case 3
                    vtg.Semivariance = New TGIS_SemivarianceSpherical()
                    Exit Select
                Case 4
                    vtg.Semivariance = New TGIS_SemivarianceCircular()
                    Exit Select
                Case 5
                    vtg.Semivariance = New TGIS_SemivarianceLinear()
                    Exit Select

            End Select

            ' generate the Kriging interpolation grid
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent)
        End Sub

        Private Sub doSplines()
            Dim vtg As TGIS_InterpolationSplines

            vtg = New TGIS_InterpolationSplines()

            ' for windowed version of this method you need to set Windowed=True
            ' and at least the Radius, e.g.
            ' vtg.Windowed := True ;
            ' vtg.Radius := ( ext.XMax - ext.XMin )/5.0 ;

            ' attach the event to automatically update the progress bar
            AddHandler vtg.BusyEvent, AddressOf doBusyEvent
            ' set the tension parameter of Splines (value need to be adjusted for
            ' the data)
            vtg.Tension = 0.000000001
            ' generate the Completely Regularized Splines interpolation grid
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent)
        End Sub

        Private Sub doHeatmap(_concentration As [Boolean])
            Dim vtg As TGIS_GaussianHeatmap
            Dim fld As [String]

            vtg = New TGIS_GaussianHeatmap()

            ' for Concentration Map the coordinate must be None and source field
            ' must be empty
            vtg.Coordinate = TGIS_VectorToGridCoordinate.None
            If _concentration Then
                fld = ""
            Else
                fld = FIELD_VALUE
            End If

            ' attach the event to automatically update the progress bar
            AddHandler vtg.BusyEvent, AddressOf doBusyEvent
            ' estimate the 3-sigma for Gaussian (can be set manually with Radius)
            vtg.EstimateRadius(src, src.Extent, dst)
            ' correct the Radius after estimation (if needed)
            vtg.Radius = vtg.Radius / 2.0
            ' generate the Heat/Concentaration Map grid
            vtg.Generate(src, src.Extent, fld, dst, dst.Extent)
        End Sub

        Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
            btnGenerate.Enabled = False

            ' obtain a handle to the source layer
            src = DirectCast(GIS.[Get]("temperatures"), TGIS_LayerVector)
            ' obtain a handle to the polygonal layer (largest extent)
            plg = DirectCast(GIS.[Get]("country"), TGIS_LayerVector)

            ' remove any previously created grid layer
            If GIS.[Get]("grid") IsNot Nothing Then
                GIS.Delete("Grid")
            End If

            ' get the source layer extent
            ext = plg.Extent

            ' calculate the height/width ratio of the extent to properly set the grid
            ' resolution
            rat = (ext.YMax - ext.YMin) / (ext.XMax - ext.XMin)

            ' create and initialize the destination layer
            dst = New TGIS_LayerPixel()
            dst.Name = "grid"
            dst.Build(True, src.CS, ext, GRID_RESOLUTION, Convert.ToInt32(Math.Round(rat * GRID_RESOLUTION)))
            dst.Params.Pixel.GridShadow = False

            ' choose the start color of the grid ramp
            clr = TGIS_Color.Blue

            ' find out which vector-to-grid has beeno chosen
            If rbIDW.Checked Then
                ' perform Inverse Distance Squared (IDW) interpolation
                doIDW()
            ElseIf rbKriging.Checked Then
                ' perform Kriging interpolation
                doKriging()
            ElseIf rbSpline.Checked Then
                ' perform Completely Regularized Splines interpolation
                doSplines()
            ElseIf rbHeatMap.Checked Then
                ' perform Heat Map generation
                doHeatmap(False)
                ' choose the start color for the grid ramp with ALPHA=0 to make it
                ' semitransparent
                clr = TGIS_Color.FromARGB(0, 0, 0, 255)
            ElseIf rbConcentrationMap.Checked Then
                ' perform Concentration Map generation
                doHeatmap(True)
                ' choose the start color for the grid ramp with ALPHA=0 to make it
                ' semitransparent
                clr = TGIS_Color.FromARGB(0, 0, 0, 255)
            End If

            ' apply color ramp to the grid layer
            dst.GenerateRamp(clr, TGIS_Color.Lime, TGIS_Color.Red, dst.MinHeight, (dst.MaxHeight - dst.MinHeight) / 2.0, dst.MaxHeight,
                False, (dst.MaxHeight - dst.MinHeight) / 100.0, (dst.MaxHeight - dst.MinHeight) / 10.0, Nothing, False)

            ' limit the grid visibility only to the pixels contained within a polygon
            dst.CuttingPolygon = DirectCast(plg.GetShape(6).CreateCopy(), TGIS_ShapePolygon)

            ' add the grid layer to the viewer
            GIS.Add(dst)
            ' update the viewer to show the grid layer
            GIS.FullExtent()

            ' reset the progress bar
            progressBar1.Value = 0

            btnGenerate.Enabled = True
        End Sub
    End Class
End Namespace

'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
