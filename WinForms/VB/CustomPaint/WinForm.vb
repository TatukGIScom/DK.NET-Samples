' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' CustomPaint sample - demonstrates how to perform custom drawing on top of a
' TatukGIS map viewer using two complementary paint-hook mechanisms in VB.NET.
'
' Key concepts:
'
'   PaintShapeEvent
'     A per-shape callback registered on a TGIS_LayerVector.  It fires once
'     for every shape in that layer during each repaint cycle and replaces the
'     layer's default rendering.  The handler converts the shape's geographic
'     position to screen pixels via GIS.MapToScreen(shape.PointOnShape()), then
'     draws primitives using TGIS_RendererAbstract Canvas* methods.
'
'   PaintExtraEvent
'     A viewer-level callback fired once after all layers have finished
'     rendering.  Use it for overlays that must appear on top of everything
'     (watermarks, debug text, HUD elements).  This sample shows how to access
'     the renderer's native canvas handle (WinForms Graphics, SharpDX D2D,
'     SkiaSharp SKCanvas) for platform-specific drawing.  Using native handles
'     is NOT recommended for portable code; prefer TGIS_RendererAbstract.
'
'   Renderer switching
'     The cbRenderer combo box is populated from TGIS_Utils.RendererManager.Names.
'     Selecting an entry replaces GIS.Renderer at runtime and forces a repaint.
'
' Layer layout:
'   One TGIS_LayerVector ("CustomPaint") with four invisible point shapes at
'   the four geographic quadrants.  A "type" field selects the drawing primitive:
'     "Rectangle" at (-25, 25)  -> CanvasDrawRectangle (red border, yellow fill)
'     "Ellipse"   at ( 25, 25)  -> CanvasDrawEllipse   (black border, red fill)
'     "Image1"    at (-25,-25)  -> CanvasDrawBitmap(TGIS_Bitmap from file)
'     "Image2"    at ( 25,-25)  -> CanvasDrawBitmap(Int32() raw ARGB pixels)
'
' Compile with GIS_SKIA_SUPPORT defined to enable the SkiaSharp rendering path.
' =============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
#If GIS_SKIA_SUPPORT Then
Imports SkiaSharp
Imports TatukGIS.NDK.SkiaSharp
#End If

' add GIS_SKIA_SUPPORT to the project,
' if you want to see native skia rendering
Namespace CustomPaint
    ''' <summary>
    ''' Main application form for the CustomPaint sample.
    ''' Hosts a TGIS_ViewerWnd and demonstrates per-shape and per-frame custom
    ''' drawing using the TatukGIS abstract renderer API and native canvas handles.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.Container = Nothing
        ''' <summary>The "CustomPaint" in-memory vector layer.</summary>
        Private ll As TGIS_LayerVector
        ''' <summary>
        ''' Main GIS viewer.  WithEvents enables Handles clauses for viewer events.
        ''' </summary>
        Private WithEvents GIS As TGIS_ViewerWnd
        ''' <summary>Bitmap loaded from police.bmp, used for the Image1 drawing case.</summary>
        Private bmp As TGIS_Bitmap
        ''' <summary>Drop-down to switch the active rendering backend at runtime.</summary>
        Friend WithEvents cbRenderer As ComboBox
        ''' <summary>
        ''' Raw ARGB pixel array (5x5) for the Image2 drawing case.
        ''' Format is 0xAARRGGBB per element (TGIS_BitmapFormat.ARGB).
        ''' </summary>
        Private px(25) As Int32

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            Me.ActiveControl = GIS
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
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.cbRenderer = New System.Windows.Forms.ComboBox()
            Me.SuspendLayout()
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.AutoStyle = True
            Me.GIS.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.Level = 28.140189979287609R
            Me.GIS.Location = New System.Drawing.Point(0, 40)
            Me.GIS.Margin = New System.Windows.Forms.Padding(4)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 426)
            Me.GIS.TabIndex = 0
            Me.GIS.UseRTree = False
            '
            'cbRenderer
            '
            Me.cbRenderer.FormattingEnabled = True
            Me.cbRenderer.Location = New System.Drawing.Point(13, 13)
            Me.cbRenderer.Name = "cbRenderer"
            Me.cbRenderer.Size = New System.Drawing.Size(153, 21)
            Me.cbRenderer.TabIndex = 1
            '
            'WinForm
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.cbRenderer)
            Me.Controls.Add(Me.GIS)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - CustomPaint"
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
        ''' Initialises the raw ARGB pixel array used for the Image2 drawing case.
        ''' The array represents a 5x5 image in row-major order:
        '''   Row 0 (indices 0-4):   all opaque red   (&amp;HFFFF0000)
        '''   Diagonal column 2:     opaque blue      (&amp;HFF0000FF) at [7],[12],[17],[22]
        '''   All other pixels:      fully transparent (&amp;H0)
        ''' The format is ARGB - high byte is alpha (0xFF = fully opaque).
        ''' </summary>
        Private Sub initialize_pixels()
            ' Row 0: five opaque red pixels
            px(0) = CType(&HFFFF0000, Int32)
            px(1) = CType(&HFFFF0000, Int32)
            px(2) = CType(&HFFFF0000, Int32)
            px(3) = CType(&HFFFF0000, Int32)
            px(4) = CType(&HFFFF0000, Int32)
            ' Row 1: transparent, blue at column 2
            px(5) = CType(&H0, Int32)
            px(6) = CType(&H0, Int32)
            px(7) = CType(&HFF0000FF, Int32)  ' opaque blue
            px(8) = CType(&H0, Int32)
            px(9) = CType(&H0, Int32)
            ' Row 2
            px(10) = CType(&H0, Int32)
            px(11) = CType(&H0, Int32)
            px(12) = CType(&HFF0000FF, Int32)
            px(13) = CType(&H0, Int32)
            px(14) = CType(&H0, Int32)
            ' Row 3
            px(15) = CType(&H0, Int32)
            px(16) = CType(&H0, Int32)
            px(17) = CType(&HFF0000FF, Int32)
            px(18) = CType(&H0, Int32)
            px(19) = CType(&H0, Int32)
            ' Row 4
            px(20) = CType(&H0, Int32)
            px(21) = CType(&H0, Int32)
            px(22) = CType(&HFF0000FF, Int32)
            px(23) = CType(&H0, Int32)
            px(23) = CType(&H0, Int32)  ' note: index 23 assigned twice
        End Sub

        ''' <summary>
        ''' WinForm_Load - builds the demo layer with four invisible point shapes
        ''' and registers the PaintShapeEvent callback.
        '''
        ''' Each shape has Marker.Size=0 so no default symbol is drawn.
        ''' The "type" field value selects which drawing primitive to use in
        ''' GIS_PaintShapeEvent.  The layer extent is set wide enough to
        ''' include all shapes and a full-extent zoom is applied.
        ''' The renderer combo box is populated to allow live backend switching.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim shp As TGIS_Shape

            initialize_pixels()

            ll = New TGIS_LayerVector()
            ll.Name = "CustomPaint"

            GIS.Add(ll)

            ' Register the per-shape paint callback via AddHandler.
            ' GIS_PaintShapeEvent fires once per shape per repaint,
            ' replacing the layer's default rendering for those shapes.
            AddHandler ll.PaintShapeEvent, AddressOf GIS_PaintShapeEvent

            ' Add the "type" attribute that the paint handler reads.
            ll.AddField("type", TGIS_FieldType.String, 100, 0)
            ll.Extent = GIS.Extent

            ' --- Create the four demo shapes ---
            ' Lock(Extent) prevents extent expansion during construction;
            ' Unlock() releases the lock after AddPoint is called.

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(-25, 25))  ' NW quadrant
            shp.Params.Marker.Size = 0   ' invisible; all visuals come from custom paint
            shp.SetField("type", "Rectangle")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(25, 25))   ' NE quadrant
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Ellipse")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(-25, -25)) ' SW quadrant
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Image1")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(25, -25))  ' SE quadrant
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Image2")
            shp.Unlock()

            ' Set the layer extent to enclose all four shapes
            ll.Extent = TGIS_Utils.GisExtent(-100, -100, 100, 100)

            ' Load the file-based bitmap for the Image1 drawing case.
            ' TGIS_Bitmap is TatukGIS's cross-renderer bitmap wrapper.
            bmp = New TGIS_Bitmap()
            bmp.LoadFromFile(TGIS_Utils.GisSamplesDataDirDownload() & "Symbols\police.bmp")

            GIS.FullExtent()

            ' Populate the renderer selector with all registered backends
            cbRenderer.Items.Clear()
            For i As Integer = 0 To TGIS_Utils.RendererManager.Names.Count - 1
                cbRenderer.Items.Add(TGIS_Utils.RendererManager.Names(i))
            Next
            ' Pre-select the currently active renderer in the list
            cbRenderer.SelectedIndex = TGIS_Utils.RendererManager.Names.IndexOf(GIS.Renderer.GetType().Name)
        End Sub

        ''' <summary>
        ''' PaintShapeEvent callback - called once per shape per repaint pass.
        '''
        ''' Replaces the default rendering for every shape in the "CustomPaint"
        ''' layer.  GIS.MapToScreen(shape.PointOnShape()) converts the shape's
        ''' geographic coordinates to screen pixel coordinates in the viewer.
        '''
        ''' Drawing cases (selected by the "type" attribute field):
        '''   "Rectangle" - yellow-filled, red-bordered box  + label above
        '''   "Ellipse"   - red-filled, black-bordered oval  + label above
        '''   "Image1"    - TGIS_Bitmap (police.bmp) 20x20  + label above
        '''   "Image2"    - raw Int32() ARGB pixels 20x20   + label above
        '''                 (source offset (5,5) skips the solid-red top row)
        '''
        ''' All pixel sizes are fixed and do not scale with the map zoom level.
        ''' </summary>
        Private Sub GIS_PaintShapeEvent(_sender As Object, _e As TGIS_ShapeEventArgs)
            Dim pt As Point                 ' screen-pixel position of the shape
            Dim rdr As TGIS_RendererAbstract

            ' MapToScreen converts geographic coordinates to screen pixels.
            pt = GIS.MapToScreen(_e.Shape.PointOnShape())
            ' Run any remaining default rendering hooks (no-op for Size=0 markers).
            _e.Shape.Draw()
            ' Obtain the abstract renderer from the shape's owning layer.
            rdr = _e.Shape.Layer.Renderer

            ' --- Select drawing primitive by "type" field ---

            If CType(_e.Shape.GetField("type"), String) = "Rectangle" Then
                ' Yellow-filled, red-bordered rectangle
                rdr.CanvasPen.Color = TGIS_Color.Red
                rdr.CanvasBrush.Color = TGIS_Color.Yellow
                rdr.CanvasDrawRectangle(New Rectangle(pt.X, pt.Y, 20, 20))
                ' Draw label 20 pixels above the rectangle
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(New Rectangle(pt.X, pt.Y, 50, 20), "Rectangle")
            ElseIf CType(_e.Shape.GetField("type"), String) = "Ellipse" Then
                ' Red-filled, black-bordered ellipse
                rdr.CanvasPen.Color = TGIS_Color.Black
                rdr.CanvasBrush.Color = TGIS_Color.Red
                rdr.CanvasDrawEllipse(pt.X, pt.Y, 20, 20)
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(New Rectangle(pt.X, pt.Y, 50, 20), "Ellipse")
            ElseIf CType(_e.Shape.GetField("type"), String) = "Image1" Then
                ' Stretch TGIS_Bitmap to a 20x20 destination rectangle
                rdr.CanvasDrawBitmap(
                bmp,
                New Rectangle(
                  pt.X,
                  pt.Y,
                  20,
                  20
                ))
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(New Rectangle(pt.X, pt.Y, 50, 20), "Image1")
            ElseIf CType(_e.Shape.GetField("type"), String) = "Image2" Then
                ' Draw using a raw ARGB pixel array.
                ' New Point(5,5) = source offset (skip first 5 pixels = red row).
                ' TGIS_BitmapFormat.ARGB = pixel layout 0xAARRGGBB.
                ' TGIS_BitmapLinesOrder.Down = scanlines run top-to-bottom.
                rdr.CanvasDrawBitmap(
                  px,
                  New Point(5, 5),
                  New Rectangle(pt.X, pt.Y, 20, 20),
                  TGIS_BitmapFormat.ARGB,
                  TGIS_BitmapLinesOrder.Down
                )
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(New Rectangle(pt.X, pt.Y, 50, 20), "Image2")
            End If

        End Sub

        ''' <summary>
        ''' PaintExtraEvent callback - fired once per frame after all layers have
        ''' finished rendering.  Demonstrates accessing renderer-native canvas
        ''' handles for platform-specific drawing.  Prefer TGIS_RendererAbstract
        ''' Canvas* methods in production code for portability.
        '''
        ''' Supported backends:
        '''   TGIS_RendererWinForms  - System.Drawing.Graphics via CanvasNative()
        '''   TGIS_RendererSharpDX   - SharpDX.Direct2D1.RenderTarget (conditional)
        '''   TGIS_RendererSkiaSharp - SkiaSharp.SKCanvas (GIS_SKIA_SUPPORT define)
        ''' </summary>
        Private Sub GIS_PaintExtraEvent(_sender As Object, _e As TGIS_RendererEventArgs) Handles GIS.PaintExtraEvent
            Dim cnvWinForms As Graphics    ' GDI+ Graphics context
            Dim fontWinForms As Font
            Dim Brush As Brush
#If GIS_SKIA_SUPPORT Then
            Dim cnvSkia As SKCanvas        ' SkiaSharp canvas
#End If
#If GIS_SHARPDX_SUPPORT Then
            Dim cnvSharpDX As SharpDX.Direct2D1.RenderTarget
            Dim vector As SharpDX.Vector2
            Dim txtl As SharpDX.DirectWrite.TextLayout
            Dim txtf As SharpDX.DirectWrite.TextFormat
            Dim factory As SharpDX.DirectWrite.Factory
            Dim brushSharpDX As SharpDX.Direct2D1.Brush
#End If
            ' --- WinForms GDI+ renderer path ---
            ' CanvasNative() returns System.Drawing.Graphics for this backend.
            If TypeOf _e.Renderer Is TGIS_RendererWinForms Then
                fontWinForms = New Font(Me.Font, FontStyle.Regular)
                Brush = New SolidBrush(Color.Blue)
                cnvWinForms = CType(_e.Renderer.CanvasNative(), Graphics)
                cnvWinForms.DrawString("Hello from WinForms", fontWinForms, Brush, New Point(50, 50))
#If GIS_SHARPDX_SUPPORT Then
            ' --- SharpDX Direct2D renderer path ---
            ElseIf TypeOf _e.Renderer Is TGIS_RendererSharpDX Then
                cnvSharpDX = CType(_e.Renderer.CanvasNative(), SharpDX.Direct2D1.RenderTarget)

                vector = New SharpDX.Vector2(50, 50)
                factory = New SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared)
                brushSharpDX = New SharpDX.Direct2D1.SolidColorBrush(cnvSharpDX, SharpDX.Color4.Black)
                txtf = New SharpDX.DirectWrite.TextFormat(
                    factory,
                    "Arial",
                    SharpDX.DirectWrite.FontWeight.Regular,
                    0,
                   12
                )

                txtl = New SharpDX.DirectWrite.TextLayout(
                    factory,
                    "Hello from SharpDX",
                    txtf,
                    150,
                    50
                )

                cnvSharpDX.DrawTextLayout(
                    vector,
                    txtl,
                    brushSharpDX,
                    SharpDX.Direct2D1.DrawTextOptions.NoSnap
                )
#End If
#If GIS_SKIA_SUPPORT Then
            ' --- SkiaSharp renderer path (requires GIS_SKIA_SUPPORT) ---
            ElseIf TypeOf _e.Renderer Is TGIS_RendererSkiaSharp Then
                cnvSkia = CType(_e.Renderer.CanvasNative(), SKCanvas)
                cnvSkia.DrawText("Hello from Skia", New SKPoint(50, 50), New SKPaint())
#End If
            Else
                ' Future or unrecognised renderer backends: no action required.
            End If
        End Sub

        ''' <summary>
        ''' Replaces the viewer's active rendering backend when the user selects
        ''' a different entry in the combo box.
        ''' RendererManager.CreateInstance constructs a fresh renderer by class name.
        ''' ControlUpdateWholeMap forces an immediate full repaint with the new renderer.
        ''' </summary>
        Private Sub cbRenderer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRenderer.SelectedIndexChanged
            If cbRenderer.SelectedIndex >= 0 Then
                GIS.Renderer = TGIS_Utils.RendererManager.CreateInstance(TGIS_Utils.RendererManager.Names(cbRenderer.SelectedIndex))
            End If

            GIS.ControlUpdateWholeMap()
        End Sub
    End Class
End Namespace
