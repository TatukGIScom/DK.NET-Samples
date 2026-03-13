Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Geocoding
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
        Private memRoute As System.Windows.Forms.TextBox
        Private groupBox1 As System.Windows.Forms.GroupBox
        Private lblSmallRoads As System.Windows.Forms.Label
        Private trkSmallRoads As System.Windows.Forms.TrackBar
        Private lblHighways As System.Windows.Forms.Label
        Private trkHighways As System.Windows.Forms.TrackBar
        Private lblAddrFrom As System.Windows.Forms.Label
        Private edtAddrFrom As System.Windows.Forms.TextBox
        Private lblAddrTo As System.Windows.Forms.Label
        Private edtAddrTo As System.Windows.Forms.TextBox
        Private WithEvents btnResolve As System.Windows.Forms.Button
        Private WithEvents btnRoute As System.Windows.Forms.Button
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private layerSrc As TatukGIS.NDK.TGIS_LayerVector
        Private layerRoute As TatukGIS.NDK.TGIS_LayerVector
        Private geoObj As TatukGIS.NDK.TGIS_Geocoding
        Private rtrObj As TatukGIS.NDK.TGIS_ShortestPath
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private GIS_ControlScale As TatukGIS.NDK.WinForms.TGIS_ControlScale

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            toolTip1.SetToolTip(Me.edtAddrFrom, """Pen"", ""Pennsylvania"", ""Penn 12""")
            toolTip1.SetToolTip(Me.edtAddrTo, """Pen"", ""Pennsylvania"", ""Penn 12""")
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
            Dim TgiS_CSUnits1 As TatukGIS.NDK.TGIS_CSUnits = New TatukGIS.NDK.TGIS_CSUnits()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.memRoute = New System.Windows.Forms.TextBox()
            Me.groupBox1 = New System.Windows.Forms.GroupBox()
            Me.btnRoute = New System.Windows.Forms.Button()
            Me.btnResolve = New System.Windows.Forms.Button()
            Me.edtAddrTo = New System.Windows.Forms.TextBox()
            Me.lblAddrTo = New System.Windows.Forms.Label()
            Me.edtAddrFrom = New System.Windows.Forms.TextBox()
            Me.lblAddrFrom = New System.Windows.Forms.Label()
            Me.trkHighways = New System.Windows.Forms.TrackBar()
            Me.lblHighways = New System.Windows.Forms.Label()
            Me.trkSmallRoads = New System.Windows.Forms.TrackBar()
            Me.lblSmallRoads = New System.Windows.Forms.Label()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.GIS_ControlScale = New TatukGIS.NDK.WinForms.TGIS_ControlScale()
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.panel1.SuspendLayout()
            Me.groupBox1.SuspendLayout()
            CType(Me.trkHighways, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.trkSmallRoads, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.AccessibleDescription = ""
            Me.panel1.Controls.Add(Me.memRoute)
            Me.panel1.Controls.Add(Me.groupBox1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Right
            Me.panel1.Location = New System.Drawing.Point(404, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(188, 466)
            Me.panel1.TabIndex = 0
            '
            'memRoute
            '
            Me.memRoute.Dock = System.Windows.Forms.DockStyle.Fill
            Me.memRoute.Location = New System.Drawing.Point(0, 249)
            Me.memRoute.Multiline = True
            Me.memRoute.Name = "memRoute"
            Me.memRoute.ReadOnly = True
            Me.memRoute.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.memRoute.Size = New System.Drawing.Size(188, 217)
            Me.memRoute.TabIndex = 1
            Me.memRoute.WordWrap = False
            '
            'groupBox1
            '
            Me.groupBox1.Controls.Add(Me.btnRoute)
            Me.groupBox1.Controls.Add(Me.btnResolve)
            Me.groupBox1.Controls.Add(Me.edtAddrTo)
            Me.groupBox1.Controls.Add(Me.lblAddrTo)
            Me.groupBox1.Controls.Add(Me.edtAddrFrom)
            Me.groupBox1.Controls.Add(Me.lblAddrFrom)
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
            'btnRoute
            '
            Me.btnRoute.Location = New System.Drawing.Point(94, 214)
            Me.btnRoute.Name = "btnRoute"
            Me.btnRoute.Size = New System.Drawing.Size(75, 23)
            Me.btnRoute.TabIndex = 9
            Me.btnRoute.Text = "Find &Route"
            '
            'btnResolve
            '
            Me.btnResolve.Location = New System.Drawing.Point(94, 160)
            Me.btnResolve.Name = "btnResolve"
            Me.btnResolve.Size = New System.Drawing.Size(75, 23)
            Me.btnResolve.TabIndex = 8
            Me.btnResolve.Text = "Find &Address"
            '
            'edtAddrTo
            '
            Me.edtAddrTo.Location = New System.Drawing.Point(24, 192)
            Me.edtAddrTo.Name = "edtAddrTo"
            Me.edtAddrTo.Size = New System.Drawing.Size(145, 20)
            Me.edtAddrTo.TabIndex = 7
            Me.edtAddrTo.Text = "wash"
            '
            'lblAddrTo
            '
            Me.lblAddrTo.Location = New System.Drawing.Point(24, 176)
            Me.lblAddrTo.Name = "lblAddrTo"
            Me.lblAddrTo.Size = New System.Drawing.Size(40, 13)
            Me.lblAddrTo.TabIndex = 6
            Me.lblAddrTo.Text = "&To"
            '
            'edtAddrFrom
            '
            Me.edtAddrFrom.Location = New System.Drawing.Point(24, 136)
            Me.edtAddrFrom.Name = "edtAddrFrom"
            Me.edtAddrFrom.Size = New System.Drawing.Size(145, 20)
            Me.edtAddrFrom.TabIndex = 5
            Me.edtAddrFrom.Text = "Chrys 1345"
            '
            'lblAddrFrom
            '
            Me.lblAddrFrom.Location = New System.Drawing.Point(24, 120)
            Me.lblAddrFrom.Name = "lblAddrFrom"
            Me.lblAddrFrom.Size = New System.Drawing.Size(40, 13)
            Me.lblAddrFrom.TabIndex = 4
            Me.lblAddrFrom.Text = "&From"
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
            'GIS
            '
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 0)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(404, 466)
            Me.GIS.TabIndex = 1
            '
            'GIS_ControlScale
            '
            Me.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Control
            Me.GIS_ControlScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.GIS_ControlScale.DividerColor1 = System.Drawing.Color.Black
            Me.GIS_ControlScale.DividerColor2 = System.Drawing.Color.White
            Me.GIS_ControlScale.GIS_Viewer = Me.GIS
            Me.GIS_ControlScale.Location = New System.Drawing.Point(8, 8)
            Me.GIS_ControlScale.Name = "GIS_ControlScale"
            Me.GIS_ControlScale.PrepareEvent = Nothing
            Me.GIS_ControlScale.Size = New System.Drawing.Size(145, 25)
            Me.GIS_ControlScale.TabIndex = 1
            TgiS_CSUnits1.DescriptionEx = Nothing
            Me.GIS_ControlScale.Units = TgiS_CSUnits1
            Me.GIS_ControlScale.UnitsEPSG = 0
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS_ControlScale)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Geocoding & Routing"
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
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

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

            GIS.Lock()
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "Samples\Projects\California.ttkproject")
            layerSrc = CType(GIS.Get("streets"), TGIS_LayerVector)

            If layerSrc Is Nothing Then
                Exit Sub
            End If

            GIS.VisibleExtent = layerSrc.Extent

            ' create route layer
            layerRoute = New TGIS_LayerVector()
            layerRoute.UseConfig = False
            layerRoute.Params.Line.Color = TGIS_Color.Red
            layerRoute.Params.Line.Width = -2
            layerRoute.Params.Marker.OutlineWidth = 1
            layerRoute.Name = "RouteDisplay"
            layerRoute.CS = GIS.CS

            GIS.Add(layerRoute)

            ' create geocoding object, set fields for routing
            geoObj = New TGIS_Geocoding(layerSrc)
            geoObj.Offset = 0.0001
            geoObj.RoadName = "FULLNAME"
            geoObj.LFrom = "LFROMADD"
            geoObj.LTo = "LTOADD"
            geoObj.RFrom = "RFROMADD"
            geoObj.RTo = "RTOADD"

            ' create path object and load source layer data
            rtrObj = New TGIS_ShortestPath(GIS)
            AddHandler rtrObj.LinkTypeEvent, AddressOf doLinkType
            rtrObj.LoadTheData(layerSrc)
            rtrObj.RoadName = "FULLNAME"

            GIS.Unlock()

            GIS_ControlScale.Units = TGIS_Utils.CSUnitsList.ByEPSG(9035) ' mile
        End Sub

        Private Sub WinForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            layerRoute = Nothing
            geoObj = Nothing
            rtrObj = Nothing
        End Sub

        Private Sub doLinkType(ByVal _sender As Object, ByVal _e As TGIS_LinkTypeEventArgs)
            If Not (_e.Shape.GetField("MTFCC").ToString().CompareTo("S1400") < 0) Then
                ' local roads
                _e.LinkType = 1
            Else
                _e.LinkType = 0
            End If
        End Sub

        Private Sub btnRoute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRoute.Click
            Dim i As Integer
            Dim shp As TGIS_Shape
            Dim res As Integer
            Dim pt_a As TGIS_Point
            Dim pt_b As TGIS_Point
            Dim ang As String
            Dim oldnam As String

            ' calculate wages
            rtrObj.CostModifiers(0) = 1 - 1 / 11.0 * trkHighways.Value
            rtrObj.CostModifiers(1) = 1 - 1 / 11.0 * trkSmallRoads.Value

            ' locate shapes meeting query
            res = geoObj.Parse(edtAddrFrom.Text)
            ' if not found, ask for more details
            If res > 0 Then
                edtAddrFrom.Text = geoObj.Query(0)
            Else
                edtAddrFrom.AppendText(" ???")
            End If

            ' remember starting point
            If res <= 0 Then
                Return
            End If
            pt_a = geoObj.Point(0)

            res = geoObj.Parse(edtAddrTo.Text)
            If res > 0 Then
                edtAddrTo.Text = geoObj.Query(0)
            Else
                edtAddrTo.AppendText(" ???")
            End If

            ' remember ending point
            If res <= 0 Then
                Return
            End If
            pt_b = geoObj.Point(0)

            ' set starting and ending position
            rtrObj.UpdateTheData()
            rtrObj.Find(layerRoute.Unproject(pt_a), layerRoute.Unproject(pt_b))

            memRoute.Clear()
            oldnam = "#$@3eqewe"

            ' display directions
            ang = ""
            i = 0
            Do While i < rtrObj.ItemsCount
                Select Case rtrObj.Items(i).Compass
                    Case 0
                        ang = "FWD  "
                    Case 1
                        ang = "RIGHT"
                    Case 2
                        ang = "RIGHT"
                    Case 3
                        ang = "RIGHT"
                    Case 4
                        ang = "BACK "
                    Case -1
                        ang = "LEFT "
                    Case -2
                        ang = "LEFT "
                    Case -3
                        ang = "LEFT "
                    Case -4
                        ang = "BACK "
                End Select

                If oldnam = rtrObj.Items(i).Name Then
                    GoTo Continue1
                End If
                oldnam = rtrObj.Items(i).Name

                memRoute.AppendText(ang & " " & rtrObj.Items(i).Name & Constants.vbCrLf)
Continue1:
                i += 1
            Loop

            layerRoute.RevertAll()

            ' add shapes of our path to route layer (red)
            i = 0
            Do While i < rtrObj.ItemsCount
                shp = rtrObj.Items(i).Layer.GetShape(rtrObj.Items(i).Uid)
                If shp Is Nothing Then
                    GoTo Continue2
                End If
                layerRoute.AddShape(shp)
                If i = 0 Then
                    layerRoute.Extent = shp.Extent
                End If
Continue2:
                i += 1
            Loop

            ' mark starting point as green squere
            shp = layerRoute.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(pt_a)
            shp.Params.Marker.Color = TGIS_Color.Green
            shp.Unlock()

            ' mark starting point as red squere
            shp = layerRoute.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(pt_b)
            shp.Unlock()

            GIS.Lock()
            GIS.VisibleExtent = layerRoute.Extent
            GIS.Zoom = 0.7 * GIS.Zoom
            GIS.Unlock()
        End Sub

        Private Sub btnResolve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnResolve.Click
            Dim i As Integer
            Dim r As Integer
            Dim shp As TGIS_Shape

            If geoObj Is Nothing Then
                Return
            End If

            layerRoute.RevertAll()

            ' locate shapes meeting query
            r = geoObj.Parse(edtAddrFrom.Text) - 1
            If r <= 0 Then
                edtAddrFrom.AppendText(" ???")
            End If

            i = 0
            Do While i <= r
                edtAddrFrom.Text = geoObj.Query(i)
                Application.DoEvents()

                ' add found shape to route layer (red color)
                shp = layerSrc.GetShape(geoObj.Uid(i))
                layerRoute.AddShape(shp)

                If i = 0 Then
                    layerRoute.Extent = shp.ProjectedExtent
                End If

                shp = layerSrc.GetShape(geoObj.UidEx(i))
                If Not shp Is Nothing Then
                    layerRoute.AddShape(shp)
                End If

                ' mark address as green squere
                shp = layerRoute.CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()
                shp.AddPoint(layerRoute.CS.FromCS(layerSrc.CS, geoObj.Point(i)))
                shp.Params.Marker.Color = TGIS_Color.Green
                shp.Unlock()
                i += 1
            Loop
            GIS.Lock()
            GIS.VisibleExtent = layerRoute.ProjectedExtent
            GIS.Zoom = 0.7 * GIS.Zoom
            GIS.Unlock()
        End Sub
    End Class
End Namespace
