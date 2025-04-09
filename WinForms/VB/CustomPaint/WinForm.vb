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
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private ll As TGIS_LayerVector
        Private WithEvents GIS As TGIS_ViewerWnd
        Private bmp As TGIS_Bitmap
        Friend WithEvents cbRenderer As ComboBox
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

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub initialize_pixels()

            px(0) = CType(&HFFFF0000, Int32)
            px(1) = CType(&HFFFF0000, Int32)
            px(2) = CType(&HFFFF0000, Int32)
            px(3) = CType(&HFFFF0000, Int32)
            px(4) = CType(&HFFFF0000, Int32)
            px(5) = CType(&H0, Int32)
            px(6) = CType(&H0, Int32)
            px(7) = CType(&HFF0000FF, Int32)
            px(8) = CType(&H0, Int32)
            px(9) = CType(&H0, Int32)
            px(10) = CType(&H0, Int32)
            px(11) = CType(&H0, Int32)
            px(12) = CType(&HFF0000FF, Int32)
            px(13) = CType(&H0, Int32)
            px(14) = CType(&H0, Int32)
            px(15) = CType(&H0, Int32)
            px(16) = CType(&H0, Int32)
            px(17) = CType(&HFF0000FF, Int32)
            px(18) = CType(&H0, Int32)
            px(19) = CType(&H0, Int32)
            px(20) = CType(&H0, Int32)
            px(21) = CType(&H0, Int32)
            px(22) = CType(&HFF0000FF, Int32)
            px(23) = CType(&H0, Int32)
            px(23) = CType(&H0, Int32)
        End Sub


        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim shp As TGIS_Shape

            initialize_pixels()

            ll = New TGIS_LayerVector()
            ll.Name = "CustomPaint"

            GIS.Add(ll)

            ''  ll.PaintShapeEvent += GIS_PaintShapeEvent

            AddHandler ll.PaintShapeEvent, AddressOf GIS_PaintShapeEvent

            ll.AddField("type", TGIS_FieldType.String, 100, 0)
            ll.Extent = GIS.Extent

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(-25, 25))
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Rectangle")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(25, 25))
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Ellipse")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(-25, -25))
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Image1")
            shp.Unlock()

            shp = ll.CreateShape(TGIS_ShapeType.Point)
            shp.Lock(TGIS_Lock.Extent)
            shp.AddPart()
            shp.AddPoint(TGIS_Utils.GisPoint(25, -25))
            shp.Params.Marker.Size = 0
            shp.SetField("type", "Image2")
            shp.Unlock()

            ll.Extent = TGIS_Utils.GisExtent(-100, -100, 100, 100)

            bmp = New TGIS_Bitmap()
            bmp.LoadFromFile(TGIS_Utils.GisSamplesDataDirDownload() + "Symbols\police.bmp")

            GIS.FullExtent()

            cbRenderer.Items.Clear()
            For i As Integer = 0 To TGIS_Utils.RendererManager.Names.Count - 1
                cbRenderer.Items.Add(TGIS_Utils.RendererManager.Names(i))
            Next
            cbRenderer.SelectedIndex = TGIS_Utils.RendererManager.Names.IndexOf(GIS.Renderer.GetType().Name)
        End Sub

        Private Sub GIS_PaintShapeEvent(_sender As Object, _e As TGIS_ShapeEventArgs)
            Dim pt As Point
            Dim rdr As TGIS_RendererAbstract

            pt = GIS.MapToScreen(_e.Shape.PointOnShape())
            _e.Shape.Draw()
            rdr = _e.Shape.Layer.Renderer

            ''Drawing with our renderer
            If CType(_e.Shape.GetField("type"), String) = "Rectangle" Then
                rdr.CanvasPen.Color = TGIS_Color.Red
                rdr.CanvasBrush.Color = TGIS_Color.Yellow
                rdr.CanvasDrawRectangle(New Rectangle(pt.X, pt.Y, 20, 20))
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(New Rectangle(pt.X, pt.Y, 50, 20), "Rectangle")
            ElseIf CType(_e.Shape.GetField("type"), String) = "Ellipse" Then
                rdr.CanvasPen.Color = TGIS_Color.Black
                rdr.CanvasBrush.Color = TGIS_Color.Red
                rdr.CanvasDrawEllipse(pt.X, pt.Y, 20, 20)
                pt.Y = pt.Y - 20
                rdr.CanvasDrawText(New Rectangle(pt.X, pt.Y, 50, 20), "Ellipse")
            ElseIf CType(_e.Shape.GetField("type"), String) = "Image1" Then
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

        Private Sub GIS_PaintExtraEvent(_sender As Object, _e As TGIS_RendererEventArgs) Handles GIS.PaintExtraEvent
            Dim cnvWinForms As Graphics
            Dim fontWinForms As Font
            Dim Brush As Brush
#If GIS_SKIA_SUPPORT Then
            Dim cnvSkia As SKCanvas
#End If
#If GIS_SHARPDX_SUPPORT Then
            Dim cnvSharpDX As SharpDX.Direct2D1.RenderTarget
            Dim vector As SharpDX.Vector2
            Dim txtl As SharpDX.DirectWrite.TextLayout
            Dim txtf As SharpDX.DirectWrite.TextFormat
            Dim factory As SharpDX.DirectWrite.Factory
            Dim brushSharpDX As SharpDX.Direct2D1.Brush
#End If
            '' drawing with native objects, Not recommended
            If TypeOf _e.Renderer Is TGIS_RendererWinForms Then
                fontWinForms = New Font(Me.Font, FontStyle.Regular)
                Brush = New SolidBrush(Color.Blue)
                cnvWinForms = CType(_e.Renderer.CanvasNative(), Graphics)
                cnvWinForms.DrawString("Hello from WinForms", fontWinForms, Brush, New Point(50, 50))
#If GIS_SHARPDX_SUPPORT Then
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
            ElseIf TypeOf _e.Renderer Is TGIS_RendererSkiaSharp Then
                cnvSkia = CType(_e.Renderer.CanvasNative(), SKCanvas)
                cnvSkia.DrawText("Hello from Skia", New SKPoint(50, 50), New SKPaint())
#End If
            Else
                ''for other renderers
            End If
        End Sub

        Private Sub cbRenderer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbRenderer.SelectedIndexChanged
            If cbRenderer.SelectedIndex >= 0 Then
                GIS.Renderer = TGIS_Utils.RendererManager.CreateInstance(TGIS_Utils.RendererManager.Names(cbRenderer.SelectedIndex))
            End If

            GIS.ControlUpdateWholeMap()
        End Sub
    End Class
End Namespace
