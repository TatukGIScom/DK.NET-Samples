' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' SelectByShape sample — demonstrates how to select vector features by drawing
' an arbitrary shape (circle or rectangle) interactively on the map.
'
' Key concepts shown:
'   - Interactive rubber-band drawing of a selection shape on the GIS viewer
'     using mouse events and the PaintExtraEvent hook.
'   - Converting screen pixel coordinates to geographic map coordinates via
'     ScreenToMap, so the drawn shape is in the layer's coordinate system.
'   - Building a circular selection area using TGIS_Topology.MakeBuffer, which
'     expands a point geometry outward by a given radius in map units.
'   - Building a rectangular selection polygon by manually adding four corner
'     points to a new polygon shape.
'   - Spatial query via TGIS_LayerVector.FindFirst / FindNext using the
'     GIS_RELATE_INTERSECT predicate (a DE-9IM relationship test) to find all
'     features that share at least one point with the selection shape.
'   - Visually highlighting matched features with IsSelected and listing their
'     "name" attribute in a text box.

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace SelectByShape
    ''' <summary>
    ''' Main application form for the SelectByShape sample.
    ''' Hosts the TatukGIS viewer control, two mode-toggle buttons (circle /
    ''' rectangle), and a read-only text box that lists the names of the features
    ''' found to intersect the drawn selection shape.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private textBox1 As System.Windows.Forms.TextBox          ' Lists names of selected features
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd  ' Main map viewer control

        ' Screen-space anchor point recorded when the left mouse button is pressed
        Private oldPos As System.Drawing.Point
        ' Current drag-end point (rectangle second corner) updated on every MouseMove
        Private oldPos2 As System.Drawing.Point
        ' Current radius of the rubber-band circle in screen pixels
        Private oldRadius As Double
        Private oldZoom As Integer
        Private oldColor As TGIS_Color
        Friend WithEvents btnCircle As CheckBox     ' Toggle: select by circle
        Friend WithEvents btnRectangle As CheckBox  ' Toggle: select by rectangle

        ' Persistent reference to the most recently used in-memory layer
        Private ll As TGIS_LayerVector

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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.textBox1 = New System.Windows.Forms.TextBox()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.btnCircle = New System.Windows.Forms.CheckBox()
            Me.btnRectangle = New System.Windows.Forms.CheckBox()

            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1})

            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 0
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = True
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Use left mouse button to select by chosen shape"
            Me.statusBarPanel1.Width = 575
            '
            'textBox1
            '
            Me.textBox1.BackColor = System.Drawing.SystemColors.Window
            Me.textBox1.Dock = System.Windows.Forms.DockStyle.Right
            Me.textBox1.Location = New System.Drawing.Point(408, 0)
            Me.textBox1.Multiline = True
            Me.textBox1.Name = "textBox1"
            Me.textBox1.ReadOnly = True
            Me.textBox1.Size = New System.Drawing.Size(184, 447)
            Me.textBox1.TabIndex = 1
            '
            'GIS
            '
            Me.GIS.CursorFor3DSelect = Nothing
            Me.GIS.CursorForCameraZoom = Nothing
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.SelectionTransparency = 50
            Me.GIS.Size = New System.Drawing.Size(408, 418)
            Me.GIS.TabIndex = 2
            Me.GIS.UseRTree = False
            '
            'btnCircle
            '
            Me.btnCircle.Appearance = System.Windows.Forms.Appearance.Button
            Me.btnCircle.AutoSize = True
            Me.btnCircle.Location = New System.Drawing.Point(3, 3)
            Me.btnCircle.Name = "btnCircle"
            Me.btnCircle.Size = New System.Drawing.Size(57, 23)
            Me.btnCircle.TabIndex = 3
            Me.btnCircle.Text = "By cricle"
            Me.btnCircle.UseVisualStyleBackColor = True
            '
            'btnRectangle
            '
            Me.btnRectangle.Appearance = System.Windows.Forms.Appearance.Button
            Me.btnRectangle.AutoSize = True
            Me.btnRectangle.Location = New System.Drawing.Point(62, 3)
            Me.btnRectangle.Name = "btnRectangle"
            Me.btnRectangle.Size = New System.Drawing.Size(76, 23)
            Me.btnRectangle.TabIndex = 4
            Me.btnRectangle.Text = "By rectangle"
            Me.btnRectangle.UseVisualStyleBackColor = True
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.btnRectangle)
            Me.Controls.Add(Me.btnCircle)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.textBox1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Select by shape"

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
        ''' Initialises the map when the form first becomes visible.
        ''' Opens the base Counties shapefile and adds two empty in-memory vector
        ''' layers that will hold visual indicators during selection:
        '''   "Points"  — invisible marker shapes (size 0) used only as geometry
        '''               containers for the MakeBuffer radius calculation.
        '''   "Buffers" — the rendered selection polygon (circle or rectangle).
        ''' The viewer is locked during setup so only one repaint fires on Unlock.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            GIS.Lock()
            ' Open the base shapefile; Counties.shp supplies the features to select from
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\USA\States\California\Counties.shp")

            ' "Points" layer — invisible markers that act as geometry placeholders
            ll = New TGIS_LayerVector()
            ll.Params.Area.Color = TGIS_Color.Blue
            ll.Transparency = 50
            ll.Name = "Points"
            ' Inherit the coordinate system from the viewer so all geometry is in the
            ' same spatial reference and ScreenToMap conversions are correct
            ll.CS = GIS.CS
            GIS.Add(ll)

            ' "Buffers" layer — holds the selection shape drawn by the user
            ll = New TGIS_LayerVector()
            ll.Params.Area.Color = TGIS_Color.Blue
            ll.Params.Area.OutlineColor = TGIS_Color.Blue
            ll.Transparency = 60    ' Semi-transparent so the map beneath shows through
            ll.Name = "Buffers"
            ll.CS = GIS.CS
            GIS.Add(ll)
            GIS.Unlock()

            ' Default to rectangle mode on startup
            btnRectangle.Checked = True
        End Sub

        ''' <summary>
        ''' Records the anchor point when the user starts a drag.
        ''' Right-click switches to the viewer's built-in Zoom mode so the user
        ''' can pan or zoom without permanently leaving the custom Select mode.
        ''' </summary>
        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseDown
            If GIS.IsEmpty Then
                Return
            End If

            If e.Button = MouseButtons.Right Then
                ' Right-click: hand control back to the viewer's built-in zoom/pan behaviour
                GIS.Mode = TGIS_ViewerMode.Zoom
                Return
            End If
            ' Initialise both anchor and drag-end to the same pixel so the shape starts
            ' with zero size (prevents stale values from a previous selection)
            oldPos = New Point(e.X, e.Y)
            oldPos2 = New Point(e.X, e.Y)
            oldRadius = 0
        End Sub

        ''' <summary>
        ''' Updates the rubber-band dimensions while the left mouse button is held.
        ''' For rectangle mode the second corner follows the cursor; for circle mode
        ''' the radius is the Euclidean pixel distance from the anchor to the cursor.
        ''' Calling GIS.Invalidate() triggers PaintExtraEvent on the next repaint so
        ''' the rubber-band is redrawn every frame during the drag.
        ''' </summary>
        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseMove
            If GIS.IsEmpty Then
                Return
            End If

            If GIS.Mode <> TGIS_ViewerMode.Select Then
                Return
            End If

            ' Only update the rubber-band while the left button is pressed (dragging)
            If Not e.Button = MouseButtons.Left Then
                Return
            End If

            If btnRectangle.Checked = True Then
                ' Track the second corner of the bounding rectangle
                oldPos2 = New Point(e.X, e.Y)
            End If

            If btnCircle.Checked = True Then
                ' Pythagorean distance from the anchor to the current cursor position
                oldRadius = CType(Math.Round(Math.Sqrt(Math.Pow(oldPos.X - e.X, 2) + Math.Pow(oldPos.Y - e.Y, 2))), Integer)
            End If

            ' Trigger a repaint so PaintExtraEvent draws the updated rubber-band shape
            GIS.Invalidate()
        End Sub

        ''' <summary>
        ''' Finalises the selection when the mouse button is released.
        '''
        ''' Steps:
        '''   1. Guard against empty map, zero-size drag, or right-click.
        '''   2. Record invisible point markers in the "Points" layer as geometry
        '''      containers for the buffer / corner calculations.
        '''   3. Clear the "Buffers" layer and add the new selection polygon:
        '''        Circle    — TGIS_Topology.MakeBuffer approximates a circle as a
        '''                    32-vertex polygon; the radius is derived by converting
        '''                    oldRadius screen pixels into map units via ScreenToMap.
        '''        Rectangle — four corner points are assembled in map coordinates.
        '''   4. Iterate the base Counties layer (Items[0]) with FindFirst/FindNext
        '''      using GIS_RELATE_INTERSECT (a DE-9IM predicate) which returns True
        '''      when two geometries share at least one point (boundary or interior).
        '''   5. Mark each matched feature as selected and append its name.
        ''' </summary>
        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseUp
            Dim tpl As TGIS_Topology
            Dim ll As TGIS_LayerVector
            Dim tmp As TGIS_Shape
            Dim buf As TGIS_Shape
            Dim ptg, ptg1, ptg2 As TGIS_Point  ' map-coordinate points used in geometry construction
            Dim distance As Double              ' Circle radius in map units

            buf = Nothing

            If GIS.IsEmpty Then
                Return
            End If

            If e.Button = MouseButtons.Right Then
                ' Right-click: restore Select mode (reversed the right-click zoom set in MouseDown)
                GIS.Mode = TGIS_ViewerMode.Select
                Return
            End If

            ' Guard: if the drag produced no movement, ignore (avoids zero-size shapes)
            If btnRectangle.Checked Then
                If ((oldPos2.X = oldPos.X) And (oldPos2.Y = oldPos.Y)) Then
                    Return
                End If
            End If

            If btnCircle.Checked Then
                If oldRadius = 0 Then
                    Return
                End If
            End If

            ' --- Add invisible marker(s) to the "Points" layer ---
            ' Marker.Size = 0 hides them from the renderer; they exist only to
            ' supply geometry for MakeBuffer (circle) or as placeholders (rectangle).
            ll = CType(GIS.Get("Points"), TGIS_LayerVector)
            ll.Lock()
            tmp = ll.CreateShape(TGIS_ShapeType.Point)
            tmp.Params.Marker.Size = 0
            tmp.AddPart()

            If btnCircle.Checked Then
                ' Convert the anchor screen pixel to a map coordinate (centre of circle)
                ptg = GIS.ScreenToMap(oldPos)
                tmp = ll.CreateShape(TGIS_ShapeType.Point)
                tmp.Params.Marker.Size = 0
                ' Lock with Extent so the layer's spatial extent is updated automatically
                tmp.Lock(TGIS_Lock.Extent)
                tmp.AddPart()
                tmp.AddPoint(ptg)
                tmp.Unlock()
                ll.Unlock()
                ' Convert a point that is exactly oldRadius pixels to the right of centre;
                ' the map-unit X difference between ptg1 and ptg is the circle radius
                ptg1 = GIS.ScreenToMap(New Point(oldPos.X + CType(oldRadius, Integer), e.Y))
            End If

            If btnRectangle.Checked Then
                ' Record the first corner as an invisible marker
                ptg = GIS.ScreenToMap(oldPos)
                tmp.AddPoint(ptg)
                tmp.Unlock()
                ' Record the second corner (drag endpoint) as a second invisible marker
                tmp = ll.CreateShape(TGIS_ShapeType.Point)
                tmp.Params.Marker.Size = 0
                tmp.Lock(TGIS_Lock.Extent)
                tmp.AddPart()
                ptg = GIS.ScreenToMap(oldPos2)
                tmp.AddPoint(ptg)
                tmp.Unlock()
                ll.Unlock()
                ' Re-convert the first corner for use during rectangle construction below
                ptg1 = GIS.ScreenToMap(oldPos)
            End If

            ' --- Rebuild the "Buffers" layer with the new selection shape ---
            ll = CType(GIS.Get("Buffers"), TGIS_LayerVector)
            ' RevertShapes clears all shapes added since the last commit, effectively
            ' resetting the layer to empty so previous selections are discarded
            ll.RevertShapes()

            If btnCircle.Checked Then
                ' The map-unit radius is the horizontal distance from centre to edge point
                distance = ptg1.X - ptg.X
                tpl = New TGIS_Topology()
                ' MakeBuffer approximates a circle as a 32-vertex polygon around tmp.
                ' The last parameter (True) closes the ring automatically.
                buf = tpl.MakeBuffer(tmp, distance, 32, True)
                ' AddShape transfers ownership of the geometry to the Buffers layer
                buf = ll.AddShape(buf)
            End If

            If btnRectangle.Checked Then
                ' Build a closed rectangle polygon from the two diagonally opposite corners
                ptg2 = GIS.ScreenToMap(oldPos2)
                buf = ll.CreateShape(TGIS_ShapeType.Polygon)
                buf.AddPart()
                ' Wind the four corners in order (top-left, top-right, bottom-right, bottom-left)
                buf.AddPoint(ptg1)
                buf.AddPoint(TGIS_Utils.GisPoint(ptg1.X, ptg2.Y))
                buf.AddPoint(ptg2)
                buf.AddPoint(TGIS_Utils.GisPoint(ptg2.X, ptg1.Y))
            End If

            ' --- Perform the spatial query on the base Counties layer (Items[0]) ---
            ll = CType(GIS.Items(0), TGIS_LayerVector)
            If ll Is Nothing Then
                GIS.InvalidateWholeMap()
                Return
            End If

            ' Clear any previously selected features before the new selection pass
            ll.DeselectAll()

            textBox1.Clear()

            GIS.InvalidateWholeMap()
            GIS.Lock()
            ' FindFirst / FindNext iterate over shapes whose bounding box overlaps
            ' buf.Extent, then applies GIS_RELATE_INTERSECT (a DE-9IM predicate) to
            ' confirm the geometries actually share at least one point — not just bounding boxes.
            tmp = ll.FindFirst(buf.Extent, "", buf, TGIS_Utils.GIS_RELATE_INTERSECT)
            Do While Not tmp Is Nothing
                ' This feature intersects the selection polygon: highlight it and record its name
                textBox1.AppendText(tmp.GetField("name").ToString + vbCrLf)
                tmp.IsSelected = True
                tmp = ll.FindNext
            Loop

            GIS.Unlock()
        End Sub

        ''' <summary>
        ''' Called by the viewer after all map layers have been drawn.
        ''' Renders the rubber-band selection shape directly on the renderer canvas
        ''' in screen space using the abstract renderer API.
        '''
        ''' Using PaintExtraEvent (rather than a Windows GDI overlay) ensures the
        ''' feedback is composited correctly on top of the map without flickering and
        ''' works regardless of hardware acceleration.
        '''
        ''' The pen colour is randomised each call to create a flickering "marching
        ''' ants" visual effect on the outline during a drag operation.
        ''' </summary>
        Private Sub GIS_PaintExtraEvent(_sender As Object, _e As TGIS_RendererEventArgs) Handles GIS.PaintExtraEvent
            Dim rdr As TGIS_RendererAbstract
            Dim rnd As Random

            rnd = New Random()
            rdr = _e.Renderer
            rdr.CanvasPen.Width = 1
            ' Random colour creates the animated "marching ants" outline effect
            rdr.CanvasPen.Color = TGIS_Color.FromBGR(CType(rnd.Next(&HFFFFFF), UInteger))
            rdr.CanvasPen.Style = TGIS_PenStyle.Solid
            ' Clear brush so only the outline is drawn (no fill obscuring the map)
            rdr.CanvasBrush.Style = TGIS_BrushStyle.Clear

            If btnRectangle.Checked Then
                ' Do not draw until the user has actually moved the cursor (zero-size guard)
                If ((oldPos.X = oldPos2.X) And (oldPos2.Y = oldPos.Y)) Then
                    Return
                End If
                rdr.CanvasDrawRectangle(New Rectangle(oldPos.X, oldPos.Y, oldPos2.X - oldPos.X, oldPos2.Y - oldPos.Y))
            End If

            If btnCircle.Checked Then
                ' CanvasDrawEllipse takes top-left corner X/Y, width, height in screen pixels.
                ' Top-left is the centre offset by the radius on both axes.
                rdr.CanvasDrawEllipse(oldPos.X - CType(Math.Round(oldRadius), Integer), oldPos.Y - CType(Math.Round(oldRadius), Integer), CType(oldRadius * 2, Integer), CType(oldRadius * 2, Integer))
            End If

        End Sub

        ''' <summary>
        ''' Keeps the two mode buttons mutually exclusive: selecting circle
        ''' automatically deselects rectangle and vice versa.
        ''' </summary>
        Private Sub btnCircle_CheckedChanged(sender As Object, e As EventArgs) Handles btnCircle.CheckedChanged
            btnRectangle.Checked = Not btnCircle.Checked
        End Sub

        ''' <summary>
        ''' Keeps the two mode buttons mutually exclusive: selecting rectangle
        ''' automatically deselects circle and vice versa.
        ''' </summary>
        Private Sub btnRectangle_CheckedChanged(sender As Object, e As EventArgs) Handles btnRectangle.CheckedChanged
            btnCircle.Checked = Not btnRectangle.Checked
        End Sub
    End Class
End Namespace
