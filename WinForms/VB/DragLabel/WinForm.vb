' DragLabel sample — demonstrates interactive draggable feature labels on maps (VB.NET).
'
' What the sample shows:
'   - Two parallel in-memory vector layers: realpoints (features) and sidekicks (labels)
'   - Realpoints layer with visible CGM ship symbols rendered as point markers
'   - Storing ship name attributes for use as label text
'   - Cached painting optimisation for real-time shape repositioning
'   - Sidekicks layer with invisible ghost points carrying floating labels
'   - UID matching between realpoints and sidekicks for efficient lookups
'   - Custom label rendering via PaintShapeLabelEvent callback
'   - Drawing connecting leader line from real position to label position
'   - Manual editor control for drag-to-reposition interaction
'   - Converting screen click positions to map coordinates with ScreenToMap
'   - Setting sidekick shape position during drag operation
'   - Programmatic movement loop demonstrating simultaneous realpoint/sidekick translation
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd                      - main visual map control
'   TGIS_LayerVector                    - in-memory vector layer for features
'   TGIS_Shape                          - individual geographic feature
'   PaintShapeLabelEvent (callback)     - per-shape label rendering hook
'   TGIS_RendererAbstract               - platform-agnostic drawing interface
'   GIS.Locate()                        - hit-test to find shape at point
'   GIS.MapToScreen()                   - convert geographic coords to screen pixels
'   GIS.ScreenToMap()                   - convert screen pixels to geographic coordinates
'   GIS.Editor.MouseBegin()             - start manual shape editing/dragging
'   TGIS_Shape.SetPosition()            - reposition shape in geographic space
'   CanvasDrawLine()                    - draw connecting leader line
'   DrawLabel()                         - render text label at shape position
' =============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Threading
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace DragLabel
    ''' <summary>
    ''' Main application form for the DragLabel sample.
    ''' Hosts a TGIS_ViewerWnd and manages two in-memory vector layers to
    ''' demonstrate interactive dragging of floating feature labels connected
    ''' to their source geometry by dynamically drawn leader lines.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.Container = Nothing
        Private statusBar1 As System.Windows.Forms.StatusStrip    ' status bar
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip  ' toolbar
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton ' "Animate"
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd ' GIS viewer
        Private Const LABEL_TEXT As String = "Ship "

        ''' <summary>
        ''' The sidekick shape currently being dragged. Nothing when idle.
        ''' </summary>
        Private currShape As TGIS_Shape


        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()
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
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 0
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.toolBarButton1})

            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(592, 29)
            Me.toolBar1.TabIndex = 1
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            Me.toolBarButton1.Text = "Animate"
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.IncrementalPaint = False
            Me.GIS.Location = New System.Drawing.Point(0, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 418)
            Me.GIS.TabIndex = 2
            Me.GIS.UseRTree = False
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.toolBar1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Draggable Labels"
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>The main entry point for the application.</summary>
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
        ''' WinForm_Load - builds the two-layer data model and populates 20
        ''' randomly positioned ship points.
        '''
        ''' "realpoints" layer:
        '''   Marker symbol loaded from a CGM file.  A "name" string field
        '''   stores the label text for each shape.
        '''
        ''' "sidekicks" layer:
        '''   Invisible markers (Marker.Size = 0).  PaintShapeLabelEvent wired
        '''   to doLabelPaint.  Labels.Allocator = False keeps labels at the
        '''   exact sidekick position with no auto-placement adjustment.
        '''
        '''   Each sidekick is offset from its real point by 15 / GIS.Zoom
        '''   map units so the label starts visually displaced from the symbol.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim ptg As TGIS_Point
            Dim rnd As Random
            Dim i As Integer

            ' --- "realpoints" layer ---
            ll = New TGIS_LayerVector()
            ' SymbolList.Prepare caches the compiled CGM symbol for fast rendering.
            ll.Params.Marker.Symbol = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() & "Symbols\2267.cgm")
            ll.Name = "realpoints"
            ll.CachedPaint = true

            GIS.Add(ll)
            ll.AddField("name", TGIS_FieldType.String, 100, 0)
            ll.Extent = TGIS_Utils.GisExtent(-180, -180, 180, 180) ' world-spanning extent

            ' --- "sidekicks" layer (draggable label anchors) ---
            ll = New TGIS_LayerVector()
            ll.Name = "sidekicks"
            ll.Params.Marker.Size = 0              ' invisible marker
            ll.Params.Labels.Position = TGIS_LabelPosition.MiddleCenter
            ll.CachedPaint = true

            GIS.Add(ll)
            ' Register the custom label painter via AddHandler (event delegate wiring).
            AddHandler ll.PaintShapeLabelEvent, AddressOf doLabelPaint
            ' Disable auto placement so labels stay exactly at the sidekick position.
            ll.Params.Labels.Allocator = False

            GIS.FullExtent()

            ' --- Populate with 20 random ship points ---
            rnd = New Random()
            For i = 0 To 19
                ptg = New TGIS_Point(rnd.Next(360) - 180, rnd.Next(180) - 90)

                ' Create and configure the visible real point
                shp = (CType(GIS.Get("realpoints"), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Extent)   ' freeze extent during construction
                shp.AddPart()
                shp.AddPoint(ptg)
                shp.Params.Marker.SymbolRotate = shp.Uid  ' unique rotation per ship
                shp.Params.Marker.Size = 250
                shp.Params.Marker.Color = TGIS_Color.FromRGB(rnd.Next(256), rnd.Next(256), rnd.Next(256))

                shp.SetField("name", String.Format(LABEL_TEXT & ": {0}", shp.Uid))
                shp.Unlock()

                ' Create the matching sidekick, offset by 15 pixels in map units
                shp = (CType(GIS.Get("sidekicks"), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Point)
                shp.Lock(TGIS_Lock.Extent)
                shp.AddPart()

                ' 15 / Zoom converts 15 screen pixels to the current map-unit scale
                ptg.X = ptg.X + 15 / GIS.Zoom
                ptg.Y = ptg.Y + 15 / GIS.Zoom
                shp.AddPoint(ptg)
                shp.Unlock()
            Next i

            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Animate button click handler.
        ''' Moves shape UID=5 (and its sidekick) 90 steps of (2,1) map units.
        ''' Thread.Sleep + Application.DoEvents keeps the UI responsive.
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Dim i As Integer
            Dim shp As TGIS_Shape

            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    shp = (CType(GIS.Get("realpoints"), TGIS_LayerVector)).GetShape(5)
                    For i = 0 To 89
                        If Me.IsDisposed Then
                            Exit For
                        End If
                        synchroMove(shp, 2, 1)
                        Thread.Sleep(50)
                        Application.DoEvents()
                    Next i
            End Select
        End Sub

        ''' <summary>
        ''' Moves the given real-point shape and its matching sidekick by
        ''' (_x, _y) map units simultaneously.
        '''
        ''' The sidekick is located by matching _shp.Uid.  The TGIS_Extent ex
        ''' captures the combined bounds of both points for optional use with
        ''' selective map invalidation.
        ''' </summary>
        Private Sub synchroMove(ByVal _shp As TGIS_Shape, ByVal _x As Integer, ByVal _y As Integer)
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim ptgA As TGIS_Point
            Dim ptgB As TGIS_Point
            Dim ex As TGIS_Extent

            ' Shift the real point
            ptgA = _shp.GetPoint(0, 0)
            ptgA.X = ptgA.X + _x
            ptgA.Y = ptgA.Y + _y
            _shp.SetPosition(ptgA, Nothing, 0)

            ' Locate and shift the matching sidekick by the same displacement
            ll = CType(GIS.Get("sidekicks"), TGIS_LayerVector)
            shp = ll.GetShape(_shp.Uid)
            ptgB = shp.GetPoint(0, 0)
            ptgB.X = ptgB.X + _x
            ptgB.Y = ptgB.Y + _y
            shp.SetPosition(ptgB, Nothing, 0)

            ' Bounding extent of both positions (for optional selective invalidation)
            ex.XMin = Math.Min(ptgA.X, ptgB.X)
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y)
            ex.XMax = Math.Max(ptgA.X, ptgB.X)
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y)
        End Sub

        ''' <summary>
        ''' PaintShapeLabelEvent callback - called for each sidekick during repaint.
        '''
        ''' Steps:
        '''   1. Find the matching realpoint by the sidekick's Uid.
        '''   2. Convert both geographic positions to screen pixels via MapToScreen.
        '''      MapToScreen translates TGIS_Point (map/projected coordinates) into
        '''      a System.Drawing.Point in screen pixels relative to the viewer.
        '''   3. Set pen properties and draw a blue leader line on the abstract
        '''      renderer canvas connecting the real position to the label anchor.
        '''   4. Set the label value from the "name" field and call DrawLabel so
        '''      TatukGIS renders the text using the layer's configured label style.
        ''' </summary>
        Private Sub doLabelPaint(ByVal _sender As Object, ByVal _e As TGIS_ShapeEventArgs)
            Dim ptA, ptB As Point
            Dim ll As TGIS_LayerVector
            Dim shape As TGIS_Shape = _e.Shape   ' the sidekick being painted
            Dim shp As TGIS_Shape                ' the matching real point
            Dim rnd As TGIS_RendererAbstract

            ' Get the abstract renderer interface from the viewer
            rnd = CType(GIS.Renderer, TGIS_RendererAbstract)

            ' Find the realpoints layer and retrieve the matching shape by UID
            ll = CType(GIS.Get("realpoints"), TGIS_LayerVector)
            shp = ll.GetShape(shape.Uid)

            ' Convert geographic coordinates to screen pixels.
            ' Viewer.Ref.MapToScreen gives pixel coordinates within the viewer.
            ptA = shape.Viewer.Ref.MapToScreen(shp.GetPoint(0, 0))    ' real symbol position
            ptB = shape.Viewer.Ref.MapToScreen(shape.GetPoint(0, 0))   ' label anchor position

            ' Draw the blue leader line
            rnd.CanvasPen.Color = TGIS_Color.Blue
            rnd.CanvasPen.Style = TGIS_PenStyle.Solid
            rnd.CanvasPen.Width = 1
            rnd.CanvasDrawLine(ptA.X, ptA.Y, ptB.X, ptB.Y)

            ' Set label text from the real feature and render it at the sidekick position
            shape.Params.Labels.Value = shp.GetField("name").ToString()
            shape.DrawLabel()
        End Sub

        ''' <summary>
        ''' Begins a label drag when the user clicks near a sidekick shape.
        ''' GIS.IsEmpty and GIS.InPaint guards prevent mid-repaint interference.
        ''' Locate searches with a 100-pixel tolerance (converted to map units
        ''' by dividing by GIS.Zoom).  Editor.MouseBegin is called directly to
        ''' avoid globally switching the viewer mode to Edit.
        ''' </summary>
        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseDown
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then Return
            If GIS.InPaint Then Return

            ' Locate the nearest sidekick within 100 pixels (in map units)
            ll = CType(GIS.Get("sidekicks"), TGIS_LayerVector)
            shp = ll.Locate(GIS.ScreenToMap(New Point(e.X, e.Y)), 100 / GIS.Zoom)
            currShape = shp
            If currShape Is Nothing Then Return

            ' Start the editor drag without switching the global viewer mode
            GIS.Editor.MouseBegin(New Point(e.X, e.Y), True)
        End Sub

        ''' <summary>
        ''' Repositions the tracked sidekick to follow the mouse cursor.
        ''' ScreenToMap reprojects the pixel position to geographic map coordinates.
        ''' GisIsPointInsideExtent prevents dragging outside the valid map extent.
        ''' </summary>
        Private Sub GIS_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseMove
            Dim ll As TGIS_LayerVector
            Dim shp As TGIS_Shape
            Dim ptgA As TGIS_Point
            Dim ptgB As TGIS_Point
            Dim ex As TGIS_Extent

            If GIS.IsEmpty Then Return
            If currShape Is Nothing Then Return

            ' Capture pre-move extent for the sidekick + its real counterpart
            ptgA = currShape.GetPoint(0, 0)
            ll = CType(GIS.Get("realpoints"), TGIS_LayerVector)
            shp = ll.GetShape(currShape.Uid)
            ptgB = shp.GetPoint(0, 0)
            ex.XMin = Math.Min(ptgA.X, ptgB.X)
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y)
            ex.XMax = Math.Max(ptgA.X, ptgB.X)
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y)

            ' Reproject mouse pixel to map coords and move the sidekick
            ptgA = GIS.ScreenToMap(New Point(e.X, e.Y))
            If TGIS_Utils.GisIsPointInsideExtent(ptgA, GIS.Extent) Then
                currShape.SetPosition(ptgA, Nothing, 0)
            End If

            ' Capture post-move extent
            ptgA = currShape.GetPoint(0, 0)
            ll = CType(GIS.Get("realpoints"), TGIS_LayerVector)
            shp = ll.GetShape(currShape.Uid)
            ptgB = shp.GetPoint(0, 0)
            ex.XMin = Math.Min(ptgA.X, ptgB.X)
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y)
            ex.XMax = Math.Max(ptgA.X, ptgB.X)
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y)
        End Sub

        ''' <summary>
        ''' Ends the current label drag by clearing the tracked shape reference.
        ''' </summary>
        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseUp
            If GIS.IsEmpty Then Return
            currShape = Nothing
        End Sub
    End Class
End Namespace
