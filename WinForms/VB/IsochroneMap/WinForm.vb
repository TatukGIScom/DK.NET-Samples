
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.RTL
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports System

Namespace IsochroneMap
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private panel1 As System.Windows.Forms.Panel
        Private groupBox1 As System.Windows.Forms.GroupBox
        Private lblSmallRoads As System.Windows.Forms.Label
        Private trkSmallRoads As System.Windows.Forms.TrackBar
        Private lblHighways As System.Windows.Forms.Label
        Private trkHighways As System.Windows.Forms.TrackBar
        Private lblDistance As System.Windows.Forms.Label
        Private edtDistance As System.Windows.Forms.TextBox
        Private lblZones As System.Windows.Forms.Label
        Private edtZones As System.Windows.Forms.TextBox
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private layerSrc As TatukGIS.NDK.TGIS_LayerVector
        Private layerMarker As TatukGIS.NDK.TGIS_LayerVector
        Private markerShp As TatukGIS.NDK.TGIS_Shape
        Private costFactor As Double
        Private numZones As Integer
        Private layerRoute As TatukGIS.NDK.TGIS_LayerVector
        Private geoObj As TatukGIS.NDK.TGIS_Geocoding
        Private rtrObj As TatukGIS.NDK.TGIS_IsochroneMap
        Private srtpObj As TatukGIS.NDK.TGIS_ShortestPath
        Private GIS_ControlLegend As TGIS_ControlLegend
        Private label1 As Label
        Private WithEvents btnFullExtent As Button
        Private imageList1 As ImageList
        Private WithEvents btnZoomIn As Button
        Private WithEvents btnZoomOut As Button
        Private GIS_ControlScale As TatukGIS.NDK.WinForms.TGIS_ControlScale

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
            Me.components = New System.ComponentModel.Container()
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Dim TgiS_CSUnits1 As TatukGIS.NDK.TGIS_CSUnits = New TatukGIS.NDK.TGIS_CSUnits()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.GIS_ControlLegend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.GIS_ControlScale = New TatukGIS.NDK.WinForms.TGIS_ControlScale()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.label1 = New System.Windows.Forms.Label()
            Me.edtZones = New System.Windows.Forms.TextBox()
            Me.lblZones = New System.Windows.Forms.Label()
            Me.edtDistance = New System.Windows.Forms.TextBox()
            Me.lblDistance = New System.Windows.Forms.Label()
            Me.trkHighways = New System.Windows.Forms.TrackBar()
            Me.lblHighways = New System.Windows.Forms.Label()
            Me.trkSmallRoads = New System.Windows.Forms.TrackBar()
            Me.lblSmallRoads = New System.Windows.Forms.Label()
            Me.btnFullExtent = New System.Windows.Forms.Button()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.btnZoomIn = New System.Windows.Forms.Button()
            Me.btnZoomOut = New System.Windows.Forms.Button()
            Me.panel1.SuspendLayout()
            Me.GIS.SuspendLayout()
            Me.groupBox1.SuspendLayout()
            CType(Me.trkHighways, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.trkSmallRoads, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.AccessibleDescription = ""
            Me.panel1.Controls.Add(Me.GIS_ControlLegend)
            Me.panel1.Controls.Add(Me.groupBox1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Right
            Me.panel1.Location = New System.Drawing.Point(404, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(188, 504)
            Me.panel1.TabIndex = 0
            '
            'GIS_ControlLegend
            '
            Me.GIS_ControlLegend.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GIS_ControlLegend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GIS_ControlLegend.GIS_Group = Nothing
            Me.GIS_ControlLegend.GIS_Layer = Nothing
            Me.GIS_ControlLegend.GIS_Viewer = Me.GIS
            Me.GIS_ControlLegend.Location = New System.Drawing.Point(0, 255)
            Me.GIS_ControlLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GIS_ControlLegend.Name = "GIS_ControlLegend"
            Me.GIS_ControlLegend.Options = CType(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GIS_ControlLegend.ReverseOrder = False
            Me.GIS_ControlLegend.Size = New System.Drawing.Size(188, 246)
            Me.GIS_ControlLegend.TabIndex = 1
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Controls.Add(Me.GIS_ControlScale)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Location = New System.Drawing.Point(-2, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(400, 482)
            Me.GIS.TabIndex = 1
            '
            'GIS_ControlScale
            '
            Me.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Control
            Me.GIS_ControlScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.GIS_ControlScale.DividerColor1 = System.Drawing.Color.Black
            Me.GIS_ControlScale.DividerColor2 = System.Drawing.Color.White
            Me.GIS_ControlScale.GIS_Viewer = Me.GIS
            Me.GIS_ControlScale.Location = New System.Drawing.Point(3, 3)
            Me.GIS_ControlScale.Name = "GIS_ControlScale"
            Me.GIS_ControlScale.PrepareEvent = Nothing
            Me.GIS_ControlScale.Size = New System.Drawing.Size(145, 25)
            Me.GIS_ControlScale.TabIndex = 1
            TgiS_CSUnits1.DescriptionEx = Nothing
            Me.GIS_ControlScale.Units = TgiS_CSUnits1
            Me.GIS_ControlScale.UnitsEPSG = 904201
            '
            'groupBox1
            '
            Me.groupBox1.Controls.Add(Me.label1)
            Me.groupBox1.Controls.Add(Me.edtZones)
            Me.groupBox1.Controls.Add(Me.lblZones)
            Me.groupBox1.Controls.Add(Me.edtDistance)
            Me.groupBox1.Controls.Add(Me.lblDistance)
            Me.groupBox1.Controls.Add(Me.trkHighways)
            Me.groupBox1.Controls.Add(Me.lblHighways)
            Me.groupBox1.Controls.Add(Me.trkSmallRoads)
            Me.groupBox1.Controls.Add(Me.lblSmallRoads)
            Me.groupBox1.Dock = System.Windows.Forms.DockStyle.Top
            Me.groupBox1.Location = New System.Drawing.Point(0, 0)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(188, 249)
            Me.groupBox1.TabIndex = 0
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "Routing parameters"
            '
            'label1
            '
            Me.label1.Location = New System.Drawing.Point(21, 212)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(150, 34)
            Me.label1.TabIndex = 8
            Me.label1.Text = "Click on the map to set start point and generate isochrone"
            '
            'edtZones
            '
            Me.edtZones.Location = New System.Drawing.Point(24, 178)
            Me.edtZones.Name = "edtZones"
            Me.edtZones.Size = New System.Drawing.Size(145, 20)
            Me.edtZones.TabIndex = 7
            Me.edtZones.Text = "3"
            '
            'lblZones
            '
            Me.lblZones.Location = New System.Drawing.Point(24, 162)
            Me.lblZones.Name = "lblZones"
            Me.lblZones.Size = New System.Drawing.Size(40, 13)
            Me.lblZones.TabIndex = 6
            Me.lblZones.Text = "&Zones"
            '
            'edtDistance
            '
            Me.edtDistance.Location = New System.Drawing.Point(24, 136)
            Me.edtDistance.Name = "edtDistance"
            Me.edtDistance.Size = New System.Drawing.Size(145, 20)
            Me.edtDistance.TabIndex = 5
            Me.edtDistance.Text = "4000"
            '
            'lblDistance
            '
            Me.lblDistance.Location = New System.Drawing.Point(24, 120)
            Me.lblDistance.Name = "lblDistance"
            Me.lblDistance.Size = New System.Drawing.Size(59, 13)
            Me.lblDistance.TabIndex = 4
            Me.lblDistance.Text = "&Distance"
            '
            'trkHighways
            '
            Me.trkHighways.AutoSize = False
            Me.trkHighways.LargeChange = 1
            Me.trkHighways.Location = New System.Drawing.Point(16, 88)
            Me.trkHighways.Minimum = 1
            Me.trkHighways.Name = "trkHighways"
            Me.trkHighways.Size = New System.Drawing.Size(161, 25)
            Me.trkHighways.TabIndex = 3
            Me.trkHighways.Value = 5
            '
            'lblHighways
            '
            Me.lblHighways.Location = New System.Drawing.Point(24, 72)
            Me.lblHighways.Name = "lblHighways"
            Me.lblHighways.Size = New System.Drawing.Size(90, 13)
            Me.lblHighways.TabIndex = 2
            Me.lblHighways.Text = "Prefer &highway"
            '
            'trkSmallRoads
            '
            Me.trkSmallRoads.AutoSize = False
            Me.trkSmallRoads.LargeChange = 1
            Me.trkSmallRoads.Location = New System.Drawing.Point(16, 40)
            Me.trkSmallRoads.Minimum = 1
            Me.trkSmallRoads.Name = "trkSmallRoads"
            Me.trkSmallRoads.Size = New System.Drawing.Size(161, 25)
            Me.trkSmallRoads.TabIndex = 1
            Me.trkSmallRoads.Value = 1
            '
            'lblSmallRoads
            '
            Me.lblSmallRoads.Location = New System.Drawing.Point(24, 24)
            Me.lblSmallRoads.Name = "lblSmallRoads"
            Me.lblSmallRoads.Size = New System.Drawing.Size(100, 13)
            Me.lblSmallRoads.TabIndex = 0
            Me.lblSmallRoads.Text = "Prefer &local roads"
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.ImageList = Me.imageList1
            Me.btnFullExtent.Location = New System.Drawing.Point(1, 0)
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.Size = New System.Drawing.Size(35, 23)
            Me.btnFullExtent.TabIndex = 2
            Me.btnFullExtent.UseVisualStyleBackColor = True
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "FullExtent.bmp")
            Me.imageList1.Images.SetKeyName(1, "ZoomIn.bmp")
            Me.imageList1.Images.SetKeyName(2, "ZoomOut.bmp")
            '
            'btnZoomIn
            '
            Me.btnZoomIn.ImageIndex = 1
            Me.btnZoomIn.ImageList = Me.imageList1
            Me.btnZoomIn.Location = New System.Drawing.Point(35, 0)
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.Size = New System.Drawing.Size(35, 23)
            Me.btnZoomIn.TabIndex = 3
            Me.btnZoomIn.UseVisualStyleBackColor = True
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.ImageList = Me.imageList1
            Me.btnZoomOut.Location = New System.Drawing.Point(69, 0)
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.Size = New System.Drawing.Size(35, 23)
            Me.btnZoomOut.TabIndex = 4
            Me.btnZoomOut.UseVisualStyleBackColor = True
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 504)
            Me.Controls.Add(Me.btnZoomOut)
            Me.Controls.Add(Me.btnZoomIn)
            Me.Controls.Add(Me.btnFullExtent)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Isochrone Map"
            Me.panel1.ResumeLayout(False)
            Me.GIS.ResumeLayout(False)
            Me.groupBox1.ResumeLayout(False)
            Me.groupBox1.PerformLayout()
            CType(Me.trkHighways, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.trkSmallRoads, System.ComponentModel.ISupportInitialize).EndInit()
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

        Private Sub WinForm_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            GIS.Lock()
            Try
                GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "World\Countries\USA\States\California\San Bernardino\TIGER\tl_2008_06071_edges_trunc.SHP")
                layerSrc = DirectCast(GIS.[Get]("tl_2008_06071_edges_trunc"), TGIS_LayerVector)

                If layerSrc Is Nothing Then
                    Return
                End If
                If Not (TypeOf layerSrc Is TGIS_LayerVector) Then
                    Return
                End If

                ' update the layer parameters to show roads types
                layerSrc.ParamsList.Add()
                layerSrc.Params.Line.Width = -2
                layerSrc.Params.Query = "MTFCC<'S1400'"
                layerSrc.ParamsList.Add()
                layerSrc.Params.Line.Width = 1
                layerSrc.Params.Line.Style = TGIS_PenStyle.Dash
                layerSrc.Params.Query = "MTFCC='S1400'"

                GIS.VisibleExtent = layerSrc.Extent
                GIS_ControlScale.Units = New TGIS_CSUnitsList().ByEPSG(9035)
                ' mile
                ' initial traversing cost
                costFactor = 5000.0
                numZones = 5

                ' create route layer for result isochrone map
                layerRoute = New TGIS_LayerVector()
                layerRoute.UseConfig = False
                layerRoute.Name = "Isochrone map for route"
                layerRoute.CS = GIS.CS
                layerRoute.Params.Render.Expression = "GIS_COST"
                layerRoute.Params.Render.MinVal = 0
                layerRoute.Params.Render.MaxVal = costFactor
                layerRoute.Params.Render.Zones = numZones
                layerRoute.Params.Area.Color = TGIS_Color.RenderColor
                layerRoute.Params.Area.ShowLegend = True
                layerRoute.Transparency = 50
                GIS.Add(layerRoute)

                ' create marker layer to show position
                layerMarker = New TGIS_LayerVector()
                layerMarker.UseConfig = False
                layerMarker.Name = "Current Position"
                layerMarker.CS = GIS.CS
                layerMarker.Params.Marker.Color = TGIS_Color.Red
                GIS.Add(layerMarker)

                markerShp = Nothing

                ' initialize isochrone map generator
                rtrObj = New TGIS_IsochroneMap(GIS)

                ' initialize shortest path and attach events
                srtpObj = New TGIS_ShortestPath(GIS)
                AddHandler srtpObj.LinkCostEvent, AddressOf doLinkCost
                AddHandler srtpObj.LinkTypeEvent, AddressOf doLinkType

                AddHandler srtpObj.LinkDynamicEvent, AddressOf doLinkDynamic
            Finally
                GIS.Unlock()
            End Try
        End Sub

        Private Sub doLinkType(_sender As [Object], _e As TGIS_LinkTypeEventArgs)
            If Not (_e.Shape.GetField("MTFCC").ToString().CompareTo("S1400") < 0) Then
                _e.LinkType = 1
            Else
                _e.LinkType = 0
            End If
        End Sub

        Private Sub doLinkCost(_sender As [Object], _e As TGIS_LinkCostEventArgs)
            If TypeOf _e.Shape.Layer.CS Is TGIS_CSUnknownCoordinateSystem Then
                _e.Cost = _e.Shape.Length()
            Else
                _e.Cost = _e.Shape.LengthCS()
            End If

            _e.RevCost = _e.Cost
        End Sub

        Private Sub doLinkDynamic(_sender As [Object], _e As TGIS_LinkDynamicEventArgs)
            Dim shp As TGIS_Shape

            If trkHighways.Value = 1 Then
                shp = layerSrc.GetShape(_e.Uid)
                If shp.GetField("MTFCC").ToString().CompareTo("S1400") < 0 Then
                    _e.Cost = -1
                    _e.RevCost = -1
                End If
            End If
        End Sub

        Private Sub WinForm_Leave(sender As Object, e As System.EventArgs) Handles Me.Leave
            layerRoute.Dispose()
            geoObj.Dispose()
        End Sub

        Private Sub btnFullExtent_Click(sender As Object, e As EventArgs) Handles btnFullExtent.Click
            If GIS.IsEmpty Then
                Return
            End If
            GIS.FullExtent()
        End Sub

        Private Sub btnZoomIn_Click(sender As Object, e As EventArgs) Handles btnZoomIn.Click
            If GIS.IsEmpty Then
                Return
            End If
            GIS.Zoom = GIS.Zoom * 2
        End Sub

        Private Sub btnZoomOut_Click(sender As Object, e As EventArgs) Handles btnZoomOut.Click
            If GIS.IsEmpty Then
                Return
            End If
            GIS.Zoom = GIS.Zoom / 2
        End Sub

        Private Sub GIS_MouseDown(sender As Object, e As MouseEventArgs) Handles GIS.MouseDown
            Dim ptg As TGIS_Point

            If GIS.IsEmpty Then
                Return
            End If
            If GIS.Mode <> TGIS_ViewerMode.[Select] Then
                Return
            End If

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))

            If markerShp Is Nothing Then
                markerShp = layerMarker.CreateShape(TGIS_ShapeType.Point)
                markerShp.Lock(TGIS_Lock.Extent)
                markerShp.AddPart()
                markerShp.AddPoint(ptg)
                markerShp.Unlock()
                markerShp.Invalidate()
            Else
                markerShp.SetPosition(ptg, Nothing, 0)
            End If

            generateIsochrone()
        End Sub

        Private Sub generateIsochrone()
            Dim i As Integer

            If markerShp Is Nothing Then
                MessageBox.Show("Please select a start point on the map")
                Return
            End If

            layerRoute.RevertShapes()

            ' maximum traversing cost for the isochrone map
            numZones = Convert.ToInt32(edtZones.Text)
            costFactor = Convert.ToInt32(edtDistance.Text)

            ' update the renderer range
            layerRoute.Params.Render.MaxVal = costFactor
            layerRoute.Params.Render.Zones = numZones

            ' calculate wages
            srtpObj.CostModifiers(0) = 1 - 1 / 11 * trkHighways.Value
            srtpObj.CostModifiers(1) = 1 - 1 / 11 * trkSmallRoads.Value

            ' generate the isochrone maps
            For i = 1 To numZones
                rtrObj.Generate(layerSrc, srtpObj, layerRoute, TGIS_ShapeType.Polygon, markerShp.Centroid(), costFactor / i,
                    0)
            Next

            ' smooth the result polygons shapes
            For Each shp As TGIS_Shape In layerRoute.[Loop]()
                shp.Smooth(10, False)
            Next

            layerRoute.RecalcExtent()
            GIS.Lock()
            GIS.VisibleExtent = layerRoute.ProjectedExtent
            GIS.Zoom = 0.7 * GIS.Zoom
            GIS.Unlock()
        End Sub
    End Class
End Namespace
