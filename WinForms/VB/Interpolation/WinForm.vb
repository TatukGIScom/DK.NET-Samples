' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' Interpolation Sample (VB.NET WinForms / NDK edition)
'
' Demonstrates generating a raster grid from vector point data using various
' spatial interpolation methods.
'
' This sample demonstrates:
'   - Opening a TatukGIS project file (.ttkproject) containing:
'       "temperatures" - a vector point layer with a numeric "TEMP" field
'       "country"      - a polygon layer used for extent and clip mask
'   - Setting GIS.CS to EPSG:3395 (World Mercator) for metre-based distances.
'   - Creating an in-memory TGIS_LayerPixel as the interpolation output,
'     sized with an aspect ratio correction so pixels are square.
'   - Running five vector-to-grid methods:
'       IDW                - Inverse Distance Weighting (weight = 1/dist^exponent)
'       Kriging            - geostatistical interpolation with semivariogram model
'       Splines            - Completely Regularized Splines (radial basis functions)
'       Heat Map           - Gaussian kernel density weighted by TEMP field
'       Concentration Map  - Gaussian kernel density counting points only
'   - Applying a Blue->Lime->Red colour ramp with GenerateRamp.
'   - Clipping the output to the country polygon via CuttingPolygon.
'   - Reporting progress via a BusyEvent callback.
'
' Key TatukGIS API classes used:
'   TGIS_ViewerWnd              - WinForms map viewer control
'   TGIS_LayerVector            - source point / polygon layer
'   TGIS_LayerPixel             - destination raster grid layer
'   TGIS_InterpolationIDW       - IDW interpolation engine
'   TGIS_InterpolationKriging   - Kriging interpolation engine
'   TGIS_InterpolationSplines   - Completely Regularized Splines engine
'   TGIS_GaussianHeatmap        - Gaussian heat/concentration map engine
'   TGIS_CSFactory              - coordinate system factory (EPSG lookup)
' =============================================================================

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
    ''' Main form for the Interpolation sample application.
    ''' Loads a point layer and country polygon, then lets the user generate a
    ''' spatial interpolation grid using one of five supported methods.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        Private GIS As TGIS_ViewerWnd                ' Main map viewer
        Private WithEvents btnGenerate As Button     ' Triggers the interpolation run
        Private lblMethod As Label                   ' "Method" group label
        Private rbIDW As RadioButton                 ' IDW interpolation selector
        Private rbKriging As RadioButton             ' Kriging interpolation selector
        Private rbSpline As RadioButton              ' Splines interpolation selector
        Private rbHeatMap As RadioButton             ' Heat Map selector
        Private rbConcentrationMap As RadioButton    ' Concentration Map selector
        Private progressBar1 As ProgressBar          ' Shows interpolation progress
        Private lblSemivariance As Label             ' "Semivariance" label (Kriging only)
        Private cbSemivariance As ComboBox           ' Kriging semivariance model picker

        ' Name of the attribute field in the point layer to interpolate
        Private Const FIELD_VALUE As [String] = "TEMP"
        ' Output grid width in pixels; height computed from extent aspect ratio
        Private Const GRID_RESOLUTION As Integer = 400

        ' Fields shared between WinForm_Load and btnGenerate_Click
        Private src As TGIS_LayerVector  ' Source point layer ("temperatures")
        Private dst As TGIS_LayerPixel   ' Destination raster grid layer ("grid")
        Private plg As TGIS_LayerVector  ' Country polygon layer for extent/clip
        Private ext As TGIS_Extent       ' Geographic bounding box of the polygon
        Private rat As Double            ' Height/width aspect ratio of the extent
        Private dx As Double
        Private dy As Double
        Private clr As TGIS_Color        ' Start colour of the colour ramp
        Private i As Integer

        ''' <summary>Required designer variable.</summary>
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
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Loads the project file and wires up the method radio button handlers.
        ''' Setting GIS.CS to EPSG:3395 (World Mercator) ensures that distance
        ''' calculations inside the interpolation engine use consistent metre units.
        ''' IDW is pre-selected as the default method.
        ''' </summary>
        Private Sub WinForm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            ' Open the bundled project; layers are created from paths stored inside
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "Samples\Interpolation\Interpolation.ttkproject")
            ' Override the viewer CRS to World Mercator for accurate distance-based interpolation
            GIS.CS = TGIS_CSFactory.ByEPSG(3395)

            GIS.FullExtent()

            ' Wire radio buttons so Kriging-specific UI appears/disappears appropriately
            AddHandler rbIDW.CheckedChanged, AddressOf doRbAnyClick
            AddHandler rbKriging.CheckedChanged, AddressOf doRbAnyClick
            AddHandler rbSpline.CheckedChanged, AddressOf doRbAnyClick
            AddHandler rbHeatMap.CheckedChanged, AddressOf doRbAnyClick
            AddHandler rbConcentrationMap.CheckedChanged, AddressOf doRbAnyClick

            ' Select IDW as the default method; semivariance picker starts hidden
            rbIDW.Checked = True
            cbSemivariance.SelectedIndex = 0
        End Sub

        ''' <summary>
        ''' Shared handler for all five method radio buttons.
        ''' Shows the Semivariance combo only when Kriging is selected because
        ''' no other method uses a semivariogram model.
        ''' </summary>
        Private Sub doRbAnyClick(_sender As [Object], _e As EventArgs)
            If rbKriging.Checked Then
                ' Kriging requires a semivariogram model choice
                lblSemivariance.Visible = True
                cbSemivariance.Visible = True
            Else
                ' All other methods work without a semivariogram
                lblSemivariance.Visible = False
                cbSemivariance.Visible = False
            End If
        End Sub

        ''' <summary>
        ''' Progress callback invoked by the interpolation engine at regular intervals.
        ''' TatukGIS busy event convention:
        '''   Pos = 0    => initialise progress bar (EndPos is the maximum)
        '''   Pos &lt; 0 => operation finished
        '''   Pos &gt; 0 => update progress; throttled every 100 steps
        ''' </summary>
        Private Sub doBusyEvent(_sender As [Object], _e As TGIS_BusyEventArgs)
            If _e.Pos < 0 Then
                ' Finished: show full bar briefly
                progressBar1.Value = progressBar1.Maximum
            ElseIf _e.Pos = 0 Then
                ' Initialise: configure the range for this operation
                progressBar1.Minimum = 0
                progressBar1.Maximum = CInt(_e.EndPos)
                progressBar1.Value = 0
            Else
                ' Throttle UI updates to every 100 steps to avoid sluggishness
                If _e.Pos Mod 100 = 0 Then
                    progressBar1.Value = CInt(_e.Pos)
                End If
            End If
        End Sub

        ''' <summary>
        ''' Runs Inverse Distance Weighting (IDW) interpolation.
        ''' Weights each sample by 1/distance^Exponent.  An Exponent of 3.0
        ''' produces a more localised surface with sharper peaks than the default 2.0.
        ''' Enable Windowed=True with a search Radius to limit contributors and
        ''' improve performance on very large datasets.
        ''' </summary>
        Private Sub doIDW()
            Dim vtg As TGIS_InterpolationIDW

            vtg = New TGIS_InterpolationIDW()

            ' For windowed version of this method you need to set Windowed=True
            ' and at least the Radius, e.g.:
            '   vtg.Windowed = True
            '   vtg.Radius = (ext.XMax - ext.XMin) / 5.0

            ' Attach the event to automatically update the progress bar
            AddHandler vtg.BusyEvent, AddressOf doBusyEvent
            ' Exponent 3.0 gives sharper local peaks than the default 2.0
            vtg.Exponent = 3.0
            ' Generate the IDW grid: read FIELD_VALUE from src, write to dst
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent)
        End Sub

        ''' <summary>
        ''' Runs Ordinary Kriging interpolation.
        ''' Kriging models spatial autocorrelation via a semivariogram and produces
        ''' a statistically optimal (BLUE) estimate at each output cell.
        ''' The semivariogram model should be chosen to fit the spatial structure
        ''' of the data (e.g. Spherical for gradual transition, Gaussian for smooth).
        ''' </summary>
        Private Sub doKriging()
            Dim vtg As TGIS_InterpolationKriging

            vtg = New TGIS_InterpolationKriging()

            ' For windowed version of this method you need to set Windowed=True
            ' and at least the Radius, e.g.:
            '   vtg.Windowed = True
            '   vtg.Radius = (ext.XMax - ext.XMin) / 5.0

            ' Attach the event to automatically update the progress bar
            AddHandler vtg.BusyEvent, AddressOf doBusyEvent

            ' Select the semivariogram model based on the combo box choice
            Select Case cbSemivariance.SelectedIndex
                Case 0
                    vtg.Semivariance = New TGIS_SemivariancePowerLaw()     ' scale-free power relationship
                    Exit Select
                Case 1
                    vtg.Semivariance = New TGIS_SemivarianceExponential()  ' exponential decay of correlation
                    Exit Select
                Case 2
                    vtg.Semivariance = New TGIS_SemivarianceGaussian()     ' smooth Gaussian bell-curve decay
                    Exit Select
                Case 3
                    vtg.Semivariance = New TGIS_SemivarianceSpherical()    ' linear then flat (common in geology)
                    Exit Select
                Case 4
                    vtg.Semivariance = New TGIS_SemivarianceCircular()     ' circular model
                    Exit Select
                Case 5
                    vtg.Semivariance = New TGIS_SemivarianceLinear()       ' simple linear model
                    Exit Select
            End Select

            ' Generate the Kriging interpolation grid
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent)
        End Sub

        ''' <summary>
        ''' Runs Completely Regularized Splines (CRS) interpolation.
        ''' CRS fits a smooth surface using radial basis functions.  A very small
        ''' Tension (1e-9) gives a minimum-curvature result; increase it if
        ''' oscillations appear in sparse-data regions.
        ''' </summary>
        Private Sub doSplines()
            Dim vtg As TGIS_InterpolationSplines

            vtg = New TGIS_InterpolationSplines()

            ' For windowed version of this method you need to set Windowed=True
            ' and at least the Radius, e.g.:
            '   vtg.Windowed = True
            '   vtg.Radius = (ext.XMax - ext.XMin) / 5.0

            ' Attach the event to automatically update the progress bar
            AddHandler vtg.BusyEvent, AddressOf doBusyEvent
            ' Tension=1e-9 produces a very smooth minimum-curvature surface
            vtg.Tension = 0.000000001
            ' Generate the Completely Regularized Splines interpolation grid
            vtg.Generate(src, src.Extent, FIELD_VALUE, dst, dst.Extent)
        End Sub

        ''' <summary>
        ''' Runs Gaussian Heat Map or Concentration Map generation.
        ''' Heat Map (_concentration=False): each point spreads its TEMP value
        '''   through a Gaussian kernel; high-value points create brighter hotspots.
        ''' Concentration Map (_concentration=True): ignores field values; every
        '''   point contributes equally to count point density.
        ''' EstimateRadius computes the optimal kernel bandwidth automatically;
        ''' halving it produces a finer, more peaked result.
        ''' </summary>
        Private Sub doHeatmap(_concentration As [Boolean])
            Dim vtg As TGIS_GaussianHeatmap
            Dim fld As [String]

            vtg = New TGIS_GaussianHeatmap()

            ' Coordinate.None uses point centroids as-is (no offset)
            vtg.Coordinate = TGIS_VectorToGridCoordinate.None
            ' Concentration Map: supply empty field name so only point presence counts
            If _concentration Then
                fld = ""
            Else
                fld = FIELD_VALUE
            End If

            ' Attach the event to automatically update the progress bar
            AddHandler vtg.BusyEvent, AddressOf doBusyEvent
            ' Estimate 3-sigma Gaussian kernel radius from the data distribution
            vtg.EstimateRadius(src, src.Extent, dst)
            ' Halve the estimated radius for a more localised, peaked result
            vtg.Radius = vtg.Radius / 2.0
            ' Generate the Heat/Concentration Map grid
            vtg.Generate(src, src.Extent, fld, dst, dst.Extent)
        End Sub

        ''' <summary>
        ''' Main Generate button handler: orchestrates the full interpolation workflow.
        '''
        ''' Steps:
        '''   1. Retrieve source point and country polygon layers by name.
        '''   2. Delete any previous "grid" layer to avoid duplicates.
        '''   3. Create a blank in-memory TGIS_LayerPixel with square pixels.
        '''   4. Dispatch to the selected interpolation method.
        '''   5. Apply a Blue->Lime->Red colour ramp via GenerateRamp.
        '''   6. Clip to the country polygon via CuttingPolygon.
        '''   7. Add the grid to the viewer and zoom to show the result.
        ''' </summary>
        Private Sub btnGenerate_Click(sender As Object, e As EventArgs) Handles btnGenerate.Click
            btnGenerate.Enabled = False

            ' Retrieve named layers from the loaded project
            src = DirectCast(GIS.[Get]("temperatures"), TGIS_LayerVector)  ' point data source
            plg = DirectCast(GIS.[Get]("country"), TGIS_LayerVector)       ' polygon for extent/clip

            ' Remove any previous interpolation result so we start fresh
            If GIS.[Get]("grid") IsNot Nothing Then
                GIS.Delete("Grid")
            End If

            ' Use the polygon's extent as output domain (larger than scattered points)
            ext = plg.Extent

            ' Compute aspect ratio so output pixels remain square when projected
            rat = (ext.YMax - ext.YMin) / (ext.XMax - ext.XMin)

            ' Create and initialise the in-memory raster output layer
            dst = New TGIS_LayerPixel()
            dst.Name = "grid"
            ' Build allocates the pixel buffer: True=in-memory, src.CS=CRS,
            ' GRID_RESOLUTION=width, rounded height maintains square pixels
            dst.Build(True, src.CS, ext, GRID_RESOLUTION, Convert.ToInt32(Math.Round(rat * GRID_RESOLUTION)))
            ' Disable hill-shading - the interpolated values are not elevation data
            dst.Params.Pixel.GridShadow = False

            ' Default ramp start colour (overridden to transparent blue for heat maps)
            clr = TGIS_Color.Blue

            ' Dispatch to the selected interpolation method
            If rbIDW.Checked Then
                ' Inverse Distance Weighting
                doIDW()
            ElseIf rbKriging.Checked Then
                ' Geostatistical Kriging
                doKriging()
            ElseIf rbSpline.Checked Then
                ' Completely Regularized Splines
                doSplines()
            ElseIf rbHeatMap.Checked Then
                ' Gaussian heat map weighted by TEMP field
                doHeatmap(False)
                ' Use transparent blue start so basemap shows through zero-density areas
                clr = TGIS_Color.FromARGB(0, 0, 0, 255)
            ElseIf rbConcentrationMap.Checked Then
                ' Gaussian concentration map (point density only)
                doHeatmap(True)
                clr = TGIS_Color.FromARGB(0, 0, 0, 255)
            End If

            ' Apply a three-stop colour ramp: Blue (low) -> Lime (mid) -> Red (high)
            dst.GenerateRamp(clr, TGIS_Color.Lime, TGIS_Color.Red, dst.MinHeight, (dst.MaxHeight - dst.MinHeight) / 2.0, dst.MaxHeight,
                False, (dst.MaxHeight - dst.MinHeight) / 100.0, (dst.MaxHeight - dst.MinHeight) / 10.0, Nothing, False)

            ' Clip the grid to the country boundary.  GetShape(6) is the 7th shape
            ' (0-based); CreateCopy is required because CuttingPolygon takes ownership.
            dst.CuttingPolygon = DirectCast(plg.GetShape(6).CreateCopy(), TGIS_ShapePolygon)

            ' Add the generated grid to the viewer layer list
            GIS.Add(dst)
            ' Zoom to display the full interpolated result
            GIS.FullExtent()

            ' Reset the progress bar
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
