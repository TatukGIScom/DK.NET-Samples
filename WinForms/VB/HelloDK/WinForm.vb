' HelloDK sample — introductory demonstration of core TatukGIS DK workflows (VB.NET / WinForms).
'
' What the sample shows:
'   - Opening a vector Shapefile (world map) into the GIS viewer
'   - Switching the viewer interaction mode: Zoom / Drag / Select
'   - Creating an in-memory editable vector layer with a transparent polygon style
'   - Building a polygon shape programmatically by adding explicit vertices
'   - Click-to-select a feature using screen-to-map coordinate conversion
'   - Spatial proximity search via GIS.Locate to identify shapes near cursor
'   - Spatial containment query using DE-9IM matrix "T*****FF*" (touches/inside)
'   - SQL WHERE filtering to find shapes with attributes starting with 's'
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd              - main visual map control
'   TGIS_LayerVector            - vector layer (shapefile or in-memory)
'   TGIS_Shape                  - individual geographic feature
'   TGIS_ViewerMode             - interaction modes (Zoom, Drag, Select)
'   GIS.Locate()                - spatial proximity search at point
'   GIS.ScreenToMap()           - convert screen pixels to geographic coords
'   TGIS_LayerVector.Loop()     - spatial enumeration with DE-9IM filtering
'   Shape attributes / SQL WHERE - feature filtering and selection

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace DK.WinForms.VB
    ''' <summary>
    ''' HelloDK sample — introductory demonstration of core TatukGIS DK workflows.
    ''' Opens a world shapefile, creates an in-memory vector layer with a polygon, performs spatial containment
    ''' queries using DE-9IM topology predicates, and switches between interaction modes (Zoom, Drag, Select).
    ''' Demonstrates screen-to-map coordinate conversion, feature location via GIS.Locate(), and SQL filtering.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ' "Open project" button - loads the sample world shapefile
        Friend WithEvents btnOpen As Button
        ' "Zooming" button - enables rubber-band zoom interaction
        Friend WithEvents btnZoom As Button
        ' "Dragging" button - enables pan/drag interaction
        Friend WithEvents btnDrag As Button
        ' "Create Shape" button - adds an editable layer with a sample polygon
        Friend WithEvents btnCreate As Button
        ' "Find Shape" button - runs DE-9IM spatial containment query
        Friend WithEvents btnFind As Button
        ' The central GIS map viewer control
        Friend WithEvents GIS As TGIS_ViewerWnd
        ' "Selecting" button - enables click-to-select interaction
        Friend WithEvents btnSelect As Button

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            InitializeComponent()
        End Sub

        ''' <summary>Clean up any resources being used.</summary>
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.btnOpen = New System.Windows.Forms.Button()
            Me.btnZoom = New System.Windows.Forms.Button()
            Me.btnDrag = New System.Windows.Forms.Button()
            Me.btnCreate = New System.Windows.Forms.Button()
            Me.btnFind = New System.Windows.Forms.Button()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.btnSelect = New System.Windows.Forms.Button()
            Me.SuspendLayout()
            '
            'btnOpen
            '
            Me.btnOpen.Location = New System.Drawing.Point(13, 13)
            Me.btnOpen.Name = "btnOpen"
            Me.btnOpen.Size = New System.Drawing.Size(120, 23)
            Me.btnOpen.TabIndex = 0
            Me.btnOpen.Text = "Open project"
            Me.btnOpen.UseVisualStyleBackColor = True
            '
            'btnZoom
            '
            Me.btnZoom.Location = New System.Drawing.Point(139, 13)
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Size = New System.Drawing.Size(76, 23)
            Me.btnZoom.TabIndex = 1
            Me.btnZoom.Text = "Zooming"
            Me.btnZoom.UseVisualStyleBackColor = True
            '
            'btnDrag
            '
            Me.btnDrag.Location = New System.Drawing.Point(221, 13)
            Me.btnDrag.Name = "btnDrag"
            Me.btnDrag.Size = New System.Drawing.Size(69, 23)
            Me.btnDrag.TabIndex = 2
            Me.btnDrag.Text = "Dragging"
            Me.btnDrag.UseVisualStyleBackColor = True
            '
            'btnCreate
            '
            Me.btnCreate.Location = New System.Drawing.Point(377, 13)
            Me.btnCreate.Name = "btnCreate"
            Me.btnCreate.Size = New System.Drawing.Size(102, 23)
            Me.btnCreate.TabIndex = 4
            Me.btnCreate.Text = "Create shape"
            Me.btnCreate.UseVisualStyleBackColor = True
            '
            'btnFind
            '
            Me.btnFind.Location = New System.Drawing.Point(485, 13)
            Me.btnFind.Name = "btnFind"
            Me.btnFind.Size = New System.Drawing.Size(122, 23)
            Me.btnFind.TabIndex = 5
            Me.btnFind.Text = "Find shapes"
            Me.btnFind.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Location = New System.Drawing.Point(-4, 47)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(890, 516)
            Me.GIS.TabIndex = 7
            '
            'btnSelect
            '
            Me.btnSelect.Location = New System.Drawing.Point(296, 13)
            Me.btnSelect.Name = "btnSelect"
            Me.btnSelect.Size = New System.Drawing.Size(75, 23)
            Me.btnSelect.TabIndex = 3
            Me.btnSelect.Text = "Selecting"
            Me.btnSelect.UseVisualStyleBackColor = True
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(884, 561)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.btnFind)
            Me.Controls.Add(Me.btnCreate)
            Me.Controls.Add(Me.btnSelect)
            Me.Controls.Add(Me.btnDrag)
            Me.Controls.Add(Me.btnZoom)
            Me.Controls.Add(Me.btnOpen)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Hello DK"
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>Application entry point.</summary>
        <STAThread>
        Shared Sub Main()
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        End Sub

        ''' <summary>
        ''' "Open project" button click handler.
        ''' Opens the WorldDCW world Shapefile from the DK sample data directory and
        ''' loads it into the viewer. After opening, the mode is set to Select so
        ''' the user can immediately click on features.
        ''' </summary>
        Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\WorldDCW\world.shp")
            GIS.Mode = TGIS_ViewerMode.Select
        End Sub

        ''' <summary>
        ''' "Zooming" button click handler.
        ''' Switches the viewer to Zoom mode. In this mode the left mouse button
        ''' draws a rubber-band rectangle to zoom into a region; the right button
        ''' zooms out.
        ''' </summary>
        Private Sub btnZoom_Click(sender As Object, e As EventArgs) Handles btnZoom.Click
            ' Do nothing if no layers are loaded
            If GIS.IsEmpty Then Return
            GIS.Mode = TGIS_ViewerMode.Zoom
        End Sub

        ''' <summary>
        ''' "Dragging" button click handler.
        ''' Switches the viewer to Drag mode, allowing the user to pan the map
        ''' by clicking and dragging with the mouse.
        ''' </summary>
        Private Sub btnDrag_Click(sender As Object, e As EventArgs) Handles btnDrag.Click
            ' Do nothing if no layers are loaded
            If GIS.IsEmpty Then Return
            GIS.Mode = TGIS_ViewerMode.Drag
        End Sub

        ''' <summary>
        ''' "Create Shape" button click handler.
        ''' Creates a new in-memory TGIS_LayerVector named "edit layer", gives it a
        ''' transparent fill with a blue outline, then adds a single quadrilateral
        ''' polygon to it. This layer is not backed by a file.
        ''' </summary>
        Private Sub btnCreateShape_Click(sender As Object, e As EventArgs) Handles btnCreate.Click
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape

            ' Guard: if the edit layer already exists, do nothing (idempotent)
            ll = CType(GIS.Get("edit layer"), TGIS_LayerVector)
            If Not ll Is Nothing Then Return

            ' Create a new in-memory vector layer and register it with the viewer
            ll = New TGIS_LayerVector()
            ll.Name = "edit layer"
            ' Inherit the viewer's coordinate system so coordinates are interpreted correctly
            ll.CS = GIS.CS

            ' Style: transparent fill (Clear pattern) with a solid blue outline,
            ' so the underlying world layer remains visible through the polygon
            ll.Params.Area.OutlineColor = TGIS_Color.Blue
            ll.Params.Area.Pattern = TGIS_BrushStyle.Clear

            ' Register the layer with the viewer; it will appear on top of existing layers
            GIS.Add(ll)

            ' Create a new Polygon shape inside the layer
            shp = ll.CreateShape(TGIS_ShapeType.Polygon)

            ' Lock(Extent) batches vertex additions so the bounding box is recalculated
            ' only once when Unlock is called, improving performance for bulk edits
            shp.Lock(TGIS_Lock.Extent)

            ' AddPart starts the first ring of the polygon; a shape can have multiple
            ' parts (e.g., islands or holes in a multi-polygon)
            shp.AddPart()

            ' Add the four corner vertices of the polygon (coordinates in the map's CS)
            shp.AddPoint(New TGIS_Point(10, 10))
            shp.AddPoint(New TGIS_Point(10, 80))
            shp.AddPoint(New TGIS_Point(80, 90))
            shp.AddPoint(New TGIS_Point(90, 10))

            ' Unlock finalises the shape geometry: recalculates extents and closes
            ' the polygon ring automatically if the first and last points differ
            shp.Unlock()

            ' Redraw the entire map canvas to show the newly added polygon
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>
        ''' "Find Shape" button click handler.
        ''' Uses DE-9IM (Dimensionally Extended 9-Intersection Model) spatial
        ''' relationship to find all world features fully contained inside the
        ''' polygon created by btnCreateShape_Click.
        '''
        ''' The DE-9IM matrix "T*****FF*" encodes the "contains" relationship:
        '''   - 'T' at position [0]: interiors must intersect (non-empty)
        '''   - "FF" at positions [6,7]: the query shape's boundary and exterior
        '''     must NOT intersect the target shape's interior - i.e. the target
        '''     lies entirely within the query polygon.
        '''
        ''' An additional SQL LIKE filter restricts results to features whose
        ''' 'label' field starts with the letter 's'.
        ''' </summary>
        Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
            Dim ll As TGIS_LayerVector
            Dim lv As TGIS_LayerVector
            Dim tmpShp As TGIS_Shape
            Dim selShp As TGIS_Shape

            ' The edit layer must exist (created by btnCreateShape_Click) to provide
            ' the selection polygon; exit early if it has not been created yet
            ll = CType(GIS.Get("edit layer"), TGIS_LayerVector)
            If ll Is Nothing Then Return

            ' Retrieve the world layer - its name is derived from the filename ('world')
            lv = CType(GIS.Get("world"), TGIS_LayerVector)

            ' Clear any previous selection on the world layer before applying the new one
            lv.DeselectAll()

            ' Retrieve the first (and only) shape from the edit layer to use as
            ' the spatial query boundary
            selShp = ll.GetShape(1) ' just the first shape from the layer

            ' MakeEditable pins the shape into memory so it survives the subsequent
            ' iteration; file-backed shapes are otherwise evicted from cache
            selShp = selShp.MakeEditable()

            ' Loop over all shapes in the world layer whose bounding box overlaps
            ' selShp.Extent, whose 'label' field matches the SQL pattern 's%', AND
            ' whose DE-9IM relationship with selShp satisfies "T*****FF*" (Contains)
            For Each tmpShp In lv.Loop(selShp.Extent, "label LIKE 's%'", selShp, "T*****FF*")
                tmpShp.IsSelected = True
            Next

            ' Force a full map redraw to show the newly selected shapes highlighted
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>
        ''' TapSimple event handler - fired on every single mouse click on the viewer.
        ''' When the viewer is in Select mode, this converts the click position from
        ''' screen pixels to map coordinates, finds the nearest shape within a
        ''' tolerance of 5 pixels, and toggles its selection state.
        ''' </summary>
        ''' <param name="_e">Event args containing X/Y screen pixel coordinates.</param>
        Private Sub GIS_TapSimpleEvent(_sender As Object, _e As TatukGIS.RTL.TGIS_TapEventArgs) Handles GIS.TapSimpleEvent
            Dim shp As TGIS_Shape
            Dim ptg As TGIS_Point
            Dim lv As TGIS_LayerVector
            Dim precision As Double

            ' Ignore taps when the viewer is not in Select mode
            If Not GIS.Mode = TGIS_ViewerMode.Select Then Return

            ' Convert screen pixel coordinates to geographic map coordinates.
            ' ScreenToMap accounts for the current zoom level and pan offset.
            ptg = GIS.ScreenToMap(New Point(CInt(_e.X), CInt(_e.Y)))

            ' Get the world layer to manage its selection state
            lv = CType(GIS.Items(0), TGIS_LayerVector)

            ' Compute the hit-test tolerance: 5 screen pixels expressed in map units.
            ' Dividing by Zoom converts pixels to the map's coordinate unit.
            precision = 5 / GIS.Zoom

            ' Search all layers for the topmost shape within 'precision' of the click point
            shp = CType(GIS.Locate(ptg, precision), TGIS_Shape)

            ' If no shape was found near the click point, do nothing
            If shp Is Nothing Then Return

            ' Clear any previously selected shapes before applying the new selection
            lv.DeselectAll()

            ' Toggle selection: clicking a selected shape deselects it, and vice versa
            shp.IsSelected = Not shp.IsSelected

            ' Repaint the map to reflect the updated selection highlight
            GIS.InvalidateWholeMap()
        End Sub

        ''' <summary>
        ''' "Selecting" button click handler.
        ''' Switches the viewer to Select mode so clicks toggle shape selection.
        ''' </summary>
        Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
            ' Do nothing if no layers are loaded
            If GIS.IsEmpty Then Return
            GIS.Mode = TGIS_ViewerMode.Select
        End Sub
    End Class
End Namespace
