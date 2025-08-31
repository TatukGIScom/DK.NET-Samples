Imports System
Imports System.Drawing
Imports System.ComponentModel
Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace AddLayer
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As IContainer
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private label1 As Label
        Private panel2 As Panel
        Private btn3D As Button
        Private btnVectorize As Button
        Private btnStreamOrderStrahler As Button
        Private btnBasin As Button
        Private btnWatershed As Button
        Private btnAddOutlets As Button
        Private btnFlowAccumulation As Button
        Private btnFlowDirection As Button
        Private btnFillSinks As Button
        Private btnSink As Button
        Private tgiS_ControlLegend1 As TGIS_ControlLegend
        Private panel1 As Panel
        Private dem As TGIS_LayerPixel
        Private ext As TGIS_Extent
        Private hydrologyToolset As TGIS_Hydrology
        Const HYDRO_LAYER_SINK As String = "Sinks and flats"
        Const HYDRO_LAYER_DEM As String = "Hydrologically conditioned DEM"
        Const HYDRO_LAYER_DIRECTION As String = "Flow direction"
        Const HYDRO_LAYER_ACCUMULATION As String = "Flow accumulation"
        Const HYDRO_LAYER_STREAM_ORDER As String = "Stream order (Strahler)"
        Const HYDRO_LAYER_OUTLETS As String = "Outlets (pour points)"
        Const HYDRO_LAYER_WATERSHED As String = "Watersheds"
        Const HYDRO_LAYER_BASIN As String = "Basins"
        Const HYDRO_LAYER_STREAM_VEC As String = "Streams (vectorized)"
        Const HYDRO_LAYER_BASIN_VEC As String = "Basins (vectorized)"
        Const HYDRO_FIELD_ORDER As String = "ORDER"
        Private panel3 As Panel
        Private progressBar1 As ProgressBar
        Const HYDRO_FIELD_BASIN As String = "BASIN_ID"

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
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
            Dim tgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(WinForm))
            panel1 = New Panel()
            label1 = New Label()
            GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            panel2 = New Panel()
            btn3D = New Button()
            btnVectorize = New Button()
            btnStreamOrderStrahler = New Button()
            btnBasin = New Button()
            btnWatershed = New Button()
            btnAddOutlets = New Button()
            btnFlowAccumulation = New Button()
            btnFlowDirection = New Button()
            btnFillSinks = New Button()
            btnSink = New Button()
            tgiS_ControlLegend1 = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            panel3 = New Panel()
            progressBar1 = New ProgressBar()
            panel1.SuspendLayout()
            panel2.SuspendLayout()
            panel3.SuspendLayout()
            SuspendLayout()
            ' 
            ' panel1
            ' 
            panel1.Controls.Add(label1)
            panel1.Dock = DockStyle.Top
            panel1.Location = New Point(0, 0)
            panel1.Name = "panel1"
            panel1.Size = New Size(949, 24)
            panel1.TabIndex = 2
            ' 
            ' label1
            ' 
            label1.Dock = DockStyle.Fill
            label1.Font = New Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, 238)
            label1.ForeColor = SystemColors.HotTrack
            label1.Location = New Point(0, 0)
            label1.Margin = New Padding(1, 0, 1, 0)
            label1.Name = "label1"
            label1.Size = New Size(949, 24)
            label1.TabIndex = 0
            label1.Text = "This sample application is a step-by-step tutorial on how to perform common hydro" & "logical analyzes."
            label1.TextAlign = ContentAlignment.MiddleCenter
            ' 
            ' GIS
            ' 
            GIS.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            GIS.BackColor = Color.FromArgb(255, 255, 255)
            GIS.Location = New Point(197, 24)
            GIS.Name = "GIS"
            GIS.SelectionColor = Color.FromArgb(255, 0, 0)
            GIS.Size = New Size(583, 469)
            GIS.TabIndex = 3
            ' 
            ' panel2
            ' 
            panel2.Controls.Add(btn3D)
            panel2.Controls.Add(btnVectorize)
            panel2.Controls.Add(btnStreamOrderStrahler)
            panel2.Controls.Add(btnBasin)
            panel2.Controls.Add(btnWatershed)
            panel2.Controls.Add(btnAddOutlets)
            panel2.Controls.Add(btnFlowAccumulation)
            panel2.Controls.Add(btnFlowDirection)
            panel2.Controls.Add(btnFillSinks)
            panel2.Controls.Add(btnSink)
            panel2.Dock = DockStyle.Left
            panel2.Location = New Point(0, 24)
            panel2.Margin = New Padding(1, 1, 1, 1)
            panel2.Name = "panel2"
            panel2.Size = New Size(197, 491)
            panel2.TabIndex = 4
            ' 
            ' btn3D
            ' 
            btn3D.Dock = DockStyle.Top
            btn3D.Enabled = False
            btn3D.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 238)
            btn3D.Location = New Point(0, 216)
            btn3D.Margin = New Padding(1, 1, 1, 1)
            btn3D.Name = "btn3D"
            btn3D.Size = New Size(197, 24)
            btn3D.TabIndex = 9
            btn3D.Text = "View in 3D"
            btn3D.UseVisualStyleBackColor = True
            AddHandler btn3D.Click, New EventHandler(AddressOf btn3D_Click)
            ' 
            ' btnVectorize
            ' 
            btnVectorize.Dock = DockStyle.Top
            btnVectorize.Enabled = False
            btnVectorize.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 238)
            btnVectorize.Location = New Point(0, 192)
            btnVectorize.Margin = New Padding(1, 1, 1, 1)
            btnVectorize.Name = "btnVectorize"
            btnVectorize.Size = New Size(197, 24)
            btnVectorize.TabIndex = 8
            btnVectorize.Text = "Convert to vector"
            btnVectorize.UseVisualStyleBackColor = True
            AddHandler btnVectorize.Click, New EventHandler(AddressOf btnVectorize_Click)
            ' 
            ' btnStreamOrderStrahler
            ' 
            btnStreamOrderStrahler.Dock = DockStyle.Top
            btnStreamOrderStrahler.Enabled = False
            btnStreamOrderStrahler.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 238)
            btnStreamOrderStrahler.Location = New Point(0, 168)
            btnStreamOrderStrahler.Margin = New Padding(1, 1, 1, 1)
            btnStreamOrderStrahler.Name = "btnStreamOrderStrahler"
            btnStreamOrderStrahler.Size = New Size(197, 24)
            btnStreamOrderStrahler.TabIndex = 7
            btnStreamOrderStrahler.Text = "Stream Order (Strahler)"
            btnStreamOrderStrahler.UseVisualStyleBackColor = True
            AddHandler btnStreamOrderStrahler.Click, New EventHandler(AddressOf btnStreamOrderStrahler_Click)
            ' 
            ' btnBasin
            ' 
            btnBasin.Dock = DockStyle.Top
            btnBasin.Enabled = False
            btnBasin.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 238)
            btnBasin.Location = New Point(0, 144)
            btnBasin.Margin = New Padding(1, 1, 1, 1)
            btnBasin.Name = "btnBasin"
            btnBasin.Size = New Size(197, 24)
            btnBasin.TabIndex = 6
            btnBasin.Text = "Basin"
            btnBasin.UseVisualStyleBackColor = True
            AddHandler btnBasin.Click, New EventHandler(AddressOf btnBasin_Click)
            ' 
            ' btnWatershed
            ' 
            btnWatershed.Dock = DockStyle.Top
            btnWatershed.Enabled = False
            btnWatershed.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 238)
            btnWatershed.Location = New Point(0, 120)
            btnWatershed.Margin = New Padding(1, 1, 1, 1)
            btnWatershed.Name = "btnWatershed"
            btnWatershed.Size = New Size(197, 24)
            btnWatershed.TabIndex = 5
            btnWatershed.Text = "Watershed"
            btnWatershed.UseVisualStyleBackColor = True
            AddHandler btnWatershed.Click, New EventHandler(AddressOf btnWatershed_Click)
            ' 
            ' btnAddOutlets
            ' 
            btnAddOutlets.Dock = DockStyle.Top
            btnAddOutlets.Enabled = False
            btnAddOutlets.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 238)
            btnAddOutlets.Location = New Point(0, 96)
            btnAddOutlets.Margin = New Padding(1, 1, 1, 1)
            btnAddOutlets.Name = "btnAddOutlets"
            btnAddOutlets.Size = New Size(197, 24)
            btnAddOutlets.TabIndex = 4
            btnAddOutlets.Text = "Add outlets for Watershed"
            btnAddOutlets.UseVisualStyleBackColor = True
            AddHandler btnAddOutlets.Click, New EventHandler(AddressOf btnAddOutlets_Click)
            ' 
            ' btnFlowAccumulation
            ' 
            btnFlowAccumulation.Dock = DockStyle.Top
            btnFlowAccumulation.Enabled = False
            btnFlowAccumulation.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 238)
            btnFlowAccumulation.Location = New Point(0, 72)
            btnFlowAccumulation.Margin = New Padding(1, 1, 1, 1)
            btnFlowAccumulation.Name = "btnFlowAccumulation"
            btnFlowAccumulation.Size = New Size(197, 24)
            btnFlowAccumulation.TabIndex = 3
            btnFlowAccumulation.Text = "Flow Accumulation"
            btnFlowAccumulation.UseVisualStyleBackColor = True
            AddHandler btnFlowAccumulation.Click, New EventHandler(AddressOf btnFlowAccumulation_Click)
            ' 
            ' btnFlowDirection
            ' 
            btnFlowDirection.Dock = DockStyle.Top
            btnFlowDirection.Enabled = False
            btnFlowDirection.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 238)
            btnFlowDirection.Location = New Point(0, 48)
            btnFlowDirection.Margin = New Padding(1, 1, 1, 1)
            btnFlowDirection.Name = "btnFlowDirection"
            btnFlowDirection.Size = New Size(197, 24)
            btnFlowDirection.TabIndex = 2
            btnFlowDirection.Text = "Flow Direction"
            btnFlowDirection.UseVisualStyleBackColor = True
            AddHandler btnFlowDirection.Click, New EventHandler(AddressOf btnFlowDirection_Click)
            ' 
            ' btnFillSinks
            ' 
            btnFillSinks.Dock = DockStyle.Top
            btnFillSinks.Enabled = False
            btnFillSinks.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 238)
            btnFillSinks.Location = New Point(0, 24)
            btnFillSinks.Margin = New Padding(1, 1, 1, 1)
            btnFillSinks.Name = "btnFillSinks"
            btnFillSinks.Size = New Size(197, 24)
            btnFillSinks.TabIndex = 1
            btnFillSinks.Text = "Fill sinks"
            btnFillSinks.UseVisualStyleBackColor = True
            AddHandler btnFillSinks.Click, New EventHandler(AddressOf btnFillSinks_Click)
            ' 
            ' btnSink
            ' 
            btnSink.Dock = DockStyle.Top
            btnSink.Font = New Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 238)
            btnSink.Location = New Point(0, 0)
            btnSink.Margin = New Padding(1, 1, 1, 1)
            btnSink.Name = "btnSink"
            btnSink.Size = New Size(197, 24)
            btnSink.TabIndex = 0
            btnSink.Text = "Identify DEM problems"
            btnSink.UseVisualStyleBackColor = True
            AddHandler btnSink.Click, New EventHandler(AddressOf button1_Click)
            ' 
            ' tgiS_ControlLegend1
            ' 
            tgiS_ControlLegend1.CompactView = False
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            tgiS_ControlLegend1.DialogOptions = tgiS_ControlLegendDialogOptions1
            tgiS_ControlLegend1.Dock = DockStyle.Right
            tgiS_ControlLegend1.Font = New Font("Verdana", 8.0F, FontStyle.Regular, GraphicsUnit.Point, 238)
            tgiS_ControlLegend1.ForeColor = SystemColors.WindowText
            tgiS_ControlLegend1.GIS_Viewer = GIS
            tgiS_ControlLegend1.Location = New Point(780, 24)
            tgiS_ControlLegend1.Margin = New Padding(1, 1, 1, 1)
            tgiS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            tgiS_ControlLegend1.Name = "tgiS_ControlLegend1"
            tgiS_ControlLegend1.Options = CType(TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible, TatukGIS.NDK.TGIS_ControlLegendOption)
            tgiS_ControlLegend1.ReverseOrder = False
            tgiS_ControlLegend1.Size = New Size(169, 491)
            tgiS_ControlLegend1.TabIndex = 7
            ' 
            ' panel3
            ' 
            panel3.Controls.Add(progressBar1)
            panel3.Dock = DockStyle.Bottom
            panel3.Location = New Point(197, 495)
            panel3.Margin = New Padding(1, 1, 1, 1)
            panel3.Name = "panel3"
            panel3.Size = New Size(583, 20)
            panel3.TabIndex = 8
            ' 
            ' progressBar1
            ' 
            progressBar1.Dock = DockStyle.Fill
            progressBar1.Location = New Point(0, 0)
            progressBar1.Margin = New Padding(1, 1, 1, 1)
            progressBar1.Name = "progressBar1"
            progressBar1.Size = New Size(583, 20)
            progressBar1.TabIndex = 11
            ' 
            ' WinForm
            ' 
            AutoScaleDimensions = New SizeF(96F, 96F)
            AutoScaleMode = AutoScaleMode.Dpi
            ClientSize = New Size(949, 515)
            Controls.Add(panel3)
            Controls.Add(tgiS_ControlLegend1)
            Controls.Add(panel2)
            Controls.Add(GIS)
            Controls.Add(panel1)
            Icon = CType(resources.GetObject("$this.Icon"), Icon)
            Location = New Point(200, 120)
            Name = "WinForm"
            StartPosition = FormStartPosition.CenterScreen
            Text = "TatukGIS Samples - Hydrology"
            AddHandler Load, New EventHandler(AddressOf WinForm_Load)
            panel1.ResumeLayout(False)
            panel2.ResumeLayout(False)
            panel3.ResumeLayout(False)
            ResumeLayout(False)
        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Call Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Call Application.Run(New WinForm())
        End Sub

        Private Sub doBusyEvent(_sender As [Object], _e As TGIS_BusyEventArgs)
            If _e.Pos < 0 Then
                progressBar1.Value = 0
            ElseIf _e.Pos = 0 Then
                progressBar1.Minimum = 0
                progressBar1.Maximum = 100
                progressBar1.Value = 0
            Else
                progressBar1.Value = CInt(_e.Pos)
            End If
        End Sub

        ' Creates a new grid layer with the same parameters as input DEM and a given name
        Public Function CreateLayerPix(ByVal _dem As TGIS_LayerPixel, ByVal _name As String) As TGIS_LayerPixel
            Dim res As TGIS_LayerPixel = New TGIS_LayerPixel()
            res.Build(True, _dem.CS, _dem.Extent, _dem.BitWidth, _dem.BitHeight)
            res.Name = _name
            res.Params.Pixel.Antialias = False
            res.Params.Pixel.GridShadow = False
            Return res
        End Function

        ' Creates a new vector layer wita a given name, cs and type
        Public Function CreateLayerVec(ByVal _name As String, ByVal _cs As TGIS_CSCoordinateSystem, ByVal _type As TGIS_ShapeType) As TGIS_LayerVector
            Dim res As TGIS_LayerVector = New TGIS_LayerVector()
            res.Name = _name
            res.Open()
            res.CS = _cs
            res.DefaultShapeType = _type
            Return res
        End Function

        ' Gets a pixel layer with a given name from GIS
        Public Function GetLayerGrd(ByVal _name As String) As TGIS_LayerPixel
            Return TryCast(GIS.[Get](_name), TGIS_LayerPixel)
        End Function

        ' Gets a vector layer with a given name from GIS
        Public Function GetLayerVec(ByVal _name As String) As TGIS_LayerVector
            Return TryCast(GIS.[Get](_name), TGIS_LayerVector)
        End Function

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As EventArgs)
            GIS.Mode = TGIS_ViewerMode.Zoom
            GIS.RestrictedDrag = False
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\Poland\DEM\Bytowski_County.tif")
            dem = TryCast(GIS.Items(0), TGIS_LayerPixel)
            ext = dem.Extent
            dem.Params.Pixel.Antialias = False
            dem.Params.Pixel.GridShadow = False
            GIS.InvalidateWholeMap()
            hydrologyToolset = New TGIS_Hydrology()
            AddHandler Me.hydrologyToolset.BusyEvent, AddressOf doBusyEvent
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As EventArgs)
            btnSink.Enabled = False

            ' creating a grid layer for sinks
            Dim sinks As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_SINK)

            ' the Sink algorithm requires only a grid layer with DEM
            hydrologyToolset.Sink(dem, ext, sinks)
            GIS.Add(sinks)

            ' coloring pixels with sinks (pits) and flats
            Dim mn As String = sinks.MinHeight.ToString()
            Dim mx As String = sinks.MaxHeight.ToString()
            sinks.Params.Pixel.AltitudeMapZones.Add(String.Format("{0},{1},165:15:21:255,{2}-{3}", mn, mx, mn, mx))
            GIS.InvalidateWholeMap()
            btnFillSinks.Enabled = True
        End Sub

        Private Sub btnFillSinks_Click(ByVal sender As Object, ByVal e As EventArgs)
            btnFillSinks.Enabled = False

            ' turning off layers
            dem.Active = False
            GetLayerGrd(HYDRO_LAYER_SINK).Active = False

            ' creating a grid layer for a hydrologically conditioned DEM
            Dim hydro_dem As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_DEM)

            ' the Fill algorithm requires a grid layer with DEM
            hydrologyToolset.Fill(dem, ext, hydro_dem)
            GIS.Add(hydro_dem)

            ' applying the layer symbology
            Dim color_ramp As TGIS_GradientMap = TGIS_Utils.GisColorRampList.ByName("YellowGreen")
            Dim color_map As TGIS_ColorMap() = color_ramp.RealizeColorMap(TGIS_ColorMapMode.Continuous, 0, True)
            hydro_dem.GenerateRampEx(hydro_dem.MinHeight, hydro_dem.MaxHeight, color_map, Nothing)
            hydro_dem.Params.Pixel.GridShadow = True
            hydro_dem.Params.Pixel.Antialias = True
            hydro_dem.Params.Pixel.ShowLegend = False
            GIS.InvalidateWholeMap()
            btnFlowDirection.Enabled = True
        End Sub

        Private Sub btnFlowDirection_Click(ByVal sender As Object, ByVal e As EventArgs)
            btnFlowDirection.Enabled = False
            Dim hydro_dem As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DEM)
            hydro_dem.Active = False

            ' creating a grid layer for flow directions
            Dim flowdir As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_DIRECTION)

            ' the FlowDirection algorithm requires a hydrologically conditioned DEM
            hydrologyToolset.FlowDirection(hydro_dem, ext, flowdir)

            ' applying a turbo color ramp for direction codes
            flowdir.Params.Pixel.AltitudeMapZones.Add("1,1,48:18:59:255,1")
            flowdir.Params.Pixel.AltitudeMapZones.Add("2,2,71:117:237:255,2")
            flowdir.Params.Pixel.AltitudeMapZones.Add("4,4,29:206:214:255,4")
            flowdir.Params.Pixel.AltitudeMapZones.Add("8,8,98:252:108:255,8")
            flowdir.Params.Pixel.AltitudeMapZones.Add("16,16,210:232:53:255,16")
            flowdir.Params.Pixel.AltitudeMapZones.Add("32,32,254:154:45:255,32")
            flowdir.Params.Pixel.AltitudeMapZones.Add("64,64,217:56:6:255,64")
            flowdir.Params.Pixel.AltitudeMapZones.Add("128,128,122:4:3:255,128")
            flowdir.Params.Pixel.ShowLegend = True
            GIS.Add(flowdir)
            GIS.InvalidateWholeMap()
            btnFlowAccumulation.Enabled = True
        End Sub

        Private Sub btnFlowAccumulation_Click(ByVal sender As Object, ByVal e As EventArgs)
            btnFlowAccumulation.Enabled = False
            Dim flowdir As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DIRECTION)
            flowdir.Active = False

            ' creating a grid layer for flow accumulation
            Dim flowacc As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_ACCUMULATION)

            ' the FlowAccumulation algorithm requires a flow accumulation grid
            hydrologyToolset.FlowAccumulation(flowdir, ext, flowacc)
            GIS.Add(flowacc)

            ' performing a geometric classification for a better result visualization
            Dim classifier As TGIS_ClassificationPixel = New TGIS_ClassificationPixel(flowacc)

            Try
                classifier.Method = TGIS_ClassificationMethod.GeometricalInterval
                classifier.Band = "1"
                classifier.NumClasses = 5
                classifier.ColorRampName = "Bathymetry2"
                classifier.ColorRamp.DefaultReverse = True
                classifier.Classify()
                flowacc.Params.Pixel.ShowLegend = True
            Finally
                classifier = Nothing
            End Try

            GIS.InvalidateWholeMap()
            btnAddOutlets.Enabled = True
        End Sub

        Private Sub btnAddOutlets_Click(ByVal sender As Object, ByVal e As EventArgs)
            btnAddOutlets.Enabled = False

            ' creating a grid layer for outlets (pour points)
            Dim outlets As TGIS_LayerVector = CreateLayerVec(HYDRO_LAYER_OUTLETS, dem.CS, TGIS_ShapeType.Point)

            ' adding point symbology
            outlets.Params.Marker.Style = TGIS_MarkerStyle.TriangleUp
            outlets.Params.Marker.SizeAsText = "SIZE:8pt"

            ' adding two sample pour points
            ' outlets should be located to cells of high accumulated flow
            Dim shp As TGIS_Shape = outlets.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Projection)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(375007.548333318, 696503.13358447))
            shp.Unlock()
            shp = outlets.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Projection)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(399612.055851588, 706196.55502031))
            shp.Unlock()
            GIS.Add(outlets)
            GIS.InvalidateWholeMap()
            btnWatershed.Enabled = True
        End Sub

        Private Sub btnWatershed_Click(ByVal sender As Object, ByVal e As EventArgs)
            btnWatershed.Enabled = False
            Dim flowdir As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DIRECTION)
            Dim outlets As TGIS_LayerVector = GetLayerVec(HYDRO_LAYER_OUTLETS)

            ' creating a grid layer for watershed
            Dim watershed As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_WATERSHED)

            ' applying a symbology
            watershed.Params.Pixel.AltitudeMapZones.Add("1,1,62:138:86:255,1")
            watershed.Params.Pixel.AltitudeMapZones.Add("2,2,108:3:174:255,2")
            watershed.Transparency = 50
            watershed.Params.Pixel.ShowLegend = True

            ' the Watershed algorithm requires Flow Direction grid and outlets
            ' (may be vector, or grid)
            hydrologyToolset.Watershed(flowdir, outlets, "GIS_UID", ext, watershed)
            GIS.Add(watershed)
            GIS.InvalidateWholeMap()
            btnBasin.Enabled = True
        End Sub

        Private Sub btnBasin_Click(ByVal sender As Object, ByVal e As EventArgs)
            btnBasin.Enabled = False
            Dim flowdir As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DIRECTION)
            Dim flowacc As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_ACCUMULATION)
            flowacc.Active = False
            GetLayerGrd(HYDRO_LAYER_DEM).Active = False
            GetLayerGrd(HYDRO_LAYER_WATERSHED).Active = False
            GetLayerVec(HYDRO_LAYER_OUTLETS).Active = False

            ' creating a grid layer for Basin
            Dim basins As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_BASIN)

            ' the Basin algorithm only requires a Flow Direction grid
            hydrologyToolset.Basin(flowdir, ext, basins, CInt(Math.Round(flowacc.MaxHeight / 100)))
            GIS.Add(basins)

            ' classifying basin grid by unique values
            Dim classifier As TGIS_ClassificationPixel = New TGIS_ClassificationPixel(basins)

            Try
                classifier.Method = TGIS_ClassificationMethod.Unique
                classifier.Band = "Value"
                classifier.ShowLegend = False
                classifier.ColorRampName = "UniquePastel"
                classifier.ColorRamp.DefaultColorMapMode = TGIS_ColorMapMode.Discrete
                classifier.Classify()
            Finally
                classifier = Nothing
            End Try

            GIS.InvalidateWholeMap()
            btnStreamOrderStrahler.Enabled = True
        End Sub

        Private Sub btnStreamOrderStrahler_Click(ByVal sender As Object, ByVal e As EventArgs)
            btnStreamOrderStrahler.Enabled = False
            Dim flowdir As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DIRECTION)
            Dim flowacc As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_ACCUMULATION)

            ' creating a grid layer for stream order
            Dim stream_order As TGIS_LayerPixel = Me.CreateLayerPix(dem, HYDRO_LAYER_STREAM_ORDER)

            ' applying a symbology from the "Blues" color ramp
            stream_order.Params.Pixel.AltitudeMapZones.Add("1,1,78:179:211:255,1")
            stream_order.Params.Pixel.AltitudeMapZones.Add("2,2,43:140:190:255,2")
            stream_order.Params.Pixel.AltitudeMapZones.Add("3,3,8:104:172:255,3")
            stream_order.Params.Pixel.AltitudeMapZones.Add("4,4,8:64:129:255,4")
            stream_order.Params.Pixel.ShowLegend = True

            ' the StreamOrder algorithm requires Flow Direction and Accumulation grids
            hydrologyToolset.StreamOrder(flowdir, flowacc, ext, stream_order)
            GIS.Add(stream_order)
            GIS.InvalidateWholeMap()
            btnVectorize.Enabled = True
        End Sub

        Private Sub btnVectorize_Click(ByVal sender As Object, ByVal e As EventArgs)
            btnVectorize.Enabled = False
            Dim flowdir As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DIRECTION)
            Dim streams As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_STREAM_ORDER)
            Dim basins As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_BASIN)
            streams.Active = False
            basins.Active = False

            ' 1. Converting basins to polygon

            ' creating a vector polygon layer for basins
            Dim basins_vec As TGIS_LayerVector = CreateLayerVec(HYDRO_LAYER_BASIN_VEC, dem.CS, TGIS_ShapeType.Polygon)
            basins_vec.AddField(HYDRO_FIELD_BASIN, TGIS_FieldType.Number, 10, 0)

            ' using the GirdToPolygon vectorization tool
            Dim vectorizator As TGIS_GridToPolygon = New TGIS_GridToPolygon()

            Try
                AddHandler vectorizator.BusyEvent, AddressOf doBusyEvent
                vectorizator.Generate(basins, basins_vec, HYDRO_FIELD_BASIN)
            Finally
                vectorizator = Nothing
            End Try

            GIS.Add(basins_vec)

            ' classifying a basins vector layer by unique value
            Dim classifier As TGIS_ClassificationVector = New TGIS_ClassificationVector(basins_vec)

            Try
                classifier.Method = TGIS_ClassificationMethod.Unique
                classifier.Field = HYDRO_FIELD_BASIN
                classifier.ShowLegend = False
                classifier.ColorRampName = "Unique"
                classifier.ColorRamp.DefaultColorMapMode = TGIS_ColorMapMode.Discrete
                classifier.Classify()
            Finally
                classifier = Nothing
            End Try

            ' 2. Converting streams to polylines

            ' creating a vector layer for streams from Stream Order grid
            Dim streams_vec As TGIS_LayerVector = CreateLayerVec(HYDRO_LAYER_STREAM_VEC, dem.CS, TGIS_ShapeType.Arc)
            streams_vec.AddField(HYDRO_FIELD_ORDER, TGIS_FieldType.Number, 10, 0)

            ' applying a symbology and width based on a stream order value, and labeling
            streams_vec.Params.Line.WidthAsText = "RENDERER"
            streams_vec.Params.Line.ColorAsText = "ARGB:FF045A8D"
            streams_vec.Params.Render.Expression = HYDRO_FIELD_ORDER
            streams_vec.Params.Render.Zones = 4
            streams_vec.Params.Render.MinVal = 1
            streams_vec.Params.Render.MaxVal = 5
            streams_vec.Params.Render.StartSizeAsText = "SIZE:1pt"
            streams_vec.Params.Render.EndSizeAsText = "SIZE:4pt"
            streams_vec.Params.Labels.Value = "{HYDRO_FIELD_ORDER}"
            streams_vec.Params.Labels.FontSizeAsText = "SIZE:7pt"
            streams_vec.Params.Labels.FontColorAsText = "ARGB:FF045A8D"
            streams_vec.Params.Labels.ColorAsText = "ARGB:FFBDC9E1"
            streams_vec.Params.Labels.Alignment = TGIS_LabelAlignment.Follow
            hydrologyToolset.StreamToPolyline(flowdir, streams, ext, streams_vec, HYDRO_FIELD_ORDER)
            GIS.Add(streams_vec)
            GIS.InvalidateWholeMap()
            btn3D.Enabled = True
        End Sub

        Private Sub btn3D_Click(ByVal sender As Object, ByVal e As EventArgs)
            If GIS.View3D Then
                btn3D.Text = "View in 3D"
                GIS.View3D = False
            Else
                btn3D.Text = "View in 2D"
                Dim basins As TGIS_LayerVector = GetLayerVec(HYDRO_LAYER_BASIN_VEC)
                basins.Active = False
                Dim hdem As TGIS_LayerPixel = GetLayerGrd(HYDRO_LAYER_DEM)
                hdem.Active = True
                hdem.Params.ScaleZ = 1
                hdem.Params.NormalizedZ = TGIS_3DNormalizationType.Range
                Dim streams As TGIS_LayerVector = GetLayerVec(HYDRO_LAYER_STREAM_VEC)
                streams.Params.Labels.Visible = False
                streams.Layer3D = TGIS_3DLayerType.Off 
                GIS.InvalidateWholeMap()
                GIS.View3D = True
                GIS.Viewer3D.ShowLights = True
                GIS.Viewer3D.ShadowsLevel = 40
            End If
        End Sub
    End Class
End Namespace
