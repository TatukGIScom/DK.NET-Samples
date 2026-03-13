Imports System.Drawing
Imports System.Windows.Forms
Imports TatukGIS.NDK

Public Class Form1
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

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        btnTextures.Enabled = False
        btnRoof.Enabled = False
        btnWalls.Enabled = False
        Button4.Enabled = False
        GIS_3D.GIS_Viewer = GIS
        cbx3DMode.SelectedIndex = 0
        ComboBox1_TextChanged(Me, New EventArgs())
        Me.ActiveControl = GIS

        AddHandler GIS.MouseWheel, AddressOf GIS_MouseWheel
    End Sub
    <STAThread>
    Shared Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Form1())
    End Sub
    Private Sub GIS_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        If GIS.IsEmpty Then
            Return
        End If

        If GIS.View3D Then
            ' allows MouseWheel works properly in ZoomMode
            GIS.Viewer3D.StoreMousePos(New Point(e.X, e.Y))

            Dim cam As TGIS_Point3D
            cam = GIS.Viewer3D.CameraPosition
            If e.Delta < 0 Then
                cam.Z = cam.Z * (1 + 0.05)
            Else
                cam.Z = cam.Z / (1 + 0.05)
            End If
            GIS.Viewer3D.CameraPosition = cam
        Else
            If e.Delta < 0 Then
                GIS.ZoomBy(2.0 / 3.0, e.X, e.Y - GIS.Top)
            Else
                GIS.ZoomBy(3.0 / 2.0, e.X, e.Y - GIS.Top)
            End If
        End If
    End Sub


    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        GIS.Lock()

        Try

            If GIS.View3D Then
                btn2D3D_Click(sender, e)
            End If

            GIS.Close()
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\Samples\3D\Building3D.ttkproject")
            cbx3DMode.SelectedIndex = 0
        Finally
            GIS.Unlock()
        End Try
    End Sub

    Private Sub btn2D3D_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2D3D.Click
        If GIS.IsEmpty Then
            Return
        End If

        If GIS.View3D And (GIS.Viewer3D IsNot Nothing) Then
            If GIS.Viewer3D.IsBusy Then
                Return
            End If
        End If
        GIS.View3D = Not GIS.View3D

        If GIS.View3D Then
            btn2D3D.Text = "2D View"
            btnTextures.Enabled = True
            btnRoof.Enabled = True
            btnWalls.Enabled = True
            Button4.Enabled = True
            GIS_3D.Enabled = True
        Else
            btn2D3D.Text = "3D View"
            btnTextures.Enabled = False
            btnRoof.Enabled = False
            btnWalls.Enabled = False
            Button4.Enabled = False
            GIS_3D.Enabled = False
        End If
        cbx3DMode.SelectedIndex = 0
    End Sub

    Private Sub ComboBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbx3DMode.TextChanged
        If Not GIS.View3D Then Exit Sub

        Select Case cbx3DMode.SelectedIndex
            Case 0
                GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.CameraPosition
            Case 1
                GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.CameraXYZ
            Case 2
                GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.CameraXY
            Case 3
                GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.CameraRotation
            Case 4
                GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.SunPosition
            Case 5
                GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.Zoom
            Case 6
                GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.Select
        End Select
    End Sub

    Private Sub btnFullExtent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Not GIS.View3D Then
            GIS.FullExtent()
        Else
            GIS.Viewer3D.ResetView()
        End If
    End Sub

    Private Sub btnRoof_Click(sender As Object, e As EventArgs) Handles btnRoof.Click
        Dim lv As TGIS_LayerVector

        lv = CType(GIS.Get("buildings"), TGIS_LayerVector)
        If lv Is Nothing Then Return


        If lv.Params.Area.Pattern = TGIS_BrushStyle.Clear Then
            btnRoof.Text = "Hide roof"
            lv.Params.Area.Pattern = TGIS_BrushStyle.Solid

        Else
            lv.Params.Area.Pattern = TGIS_BrushStyle.Clear
            btnRoof.Text = "Show roof"
        End If
        GIS.Viewer3D.UpdateWholeMap()
    End Sub

    Private Sub btnWalls_Click(sender As Object, e As EventArgs) Handles btnWalls.Click
        Dim lv As TGIS_LayerVector

        lv = CType(GIS.Get("buildings"), TGIS_LayerVector)
        If lv Is Nothing Then Return


        If lv.Params.Area.OutlinePattern = TGIS_BrushStyle.Clear Then
            btnWalls.Text = "Hide walls"
            lv.Params.Area.OutlinePattern = TGIS_BrushStyle.Solid

        Else
            lv.Params.Area.OutlinePattern = TGIS_BrushStyle.Clear
            btnWalls.Text = "Show walls"
        End If
        GIS.Viewer3D.UpdateWholeMap()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim lv As TGIS_LayerVector
        Dim shp As TGIS_Shape

        GIS.Lock()
        Try
            GIS.Close()

            lv = New TGIS_LayerVector()
            lv.Name = "volumetric_lines"
            GIS.Add(lv)
            shp = lv.CreateShape(TGIS_ShapeType.Arc, TGIS_DimensionType.XYZ)
            shp.Params.Line.Color = TGIS_Color.Red
            shp.Params.Line.Width = 450
            shp.Params.Line.OutlinePattern = TGIS_BrushStyle.Clear
            shp.Lock(TGIS_Lock.Projection)
            shp.AddPart()
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(-50, 50, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(50, 0, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(50, 50, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(50, 50, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(100, 50, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(150, 50, 0))
            shp.Unlock()

            shp = lv.CreateShape(TGIS_ShapeType.Arc, TGIS_DimensionType.XYZ)
            shp.Params.Line.Color = TGIS_Color.Blue
            shp.Params.Line.Width = 350
            shp.Params.Line.OutlinePattern = TGIS_BrushStyle.Clear
            shp.Lock(TGIS_Lock.Projection)
            shp.AddPart()
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(-50, 40, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, -10, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(60, -10, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(60, 40, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(60, 40, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(110, 40, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(160, 40, 0))
            shp.Unlock()

            shp = lv.CreateShape(TGIS_ShapeType.Arc, TGIS_DimensionType.XYZ)
            shp.Params.Line.Color = TGIS_Color.Green
            shp.Params.Line.Width = 250
            shp.Params.Line.OutlinePattern = TGIS_BrushStyle.Clear
            shp.Lock(TGIS_Lock.Projection)
            shp.AddPart()
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(-50, 30, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, -20, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(70, -20, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(70, 30, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(70, 30, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(120, 30, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(170, 30, 0))
            shp.Unlock()

            shp = lv.CreateShape(TGIS_ShapeType.Arc, TGIS_DimensionType.XYZ)
            shp.Params.Line.Color = TGIS_Color.Yellow
            shp.Params.Line.Width = 150
            shp.Params.Line.OutlinePattern = TGIS_BrushStyle.Clear
            shp.Lock(TGIS_Lock.Projection)
            shp.AddPart()
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(-50, 20, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, -30, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(80, -30, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(80, 20, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(80, 20, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(130, 20, 50))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(120, 30, 50))
            shp.Unlock()

            GIS.FullExtent()
            btn2D3D_Click(Me, EventArgs.Empty)
            GIS.Viewer3D.ShowLights = True
        Finally
            GIS.Unlock()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim lv As TGIS_LayerVector
        Dim shp As TGIS_Shape

        GIS.Lock()
        Try
            GIS.Close()

            lv = New TGIS_LayerVector()
            lv.Name = "multipatch"
            lv.Params.Area.Color = TGIS_Color.Yellow
            GIS.Add(lv)
            shp = lv.CreateShape(TGIS_ShapeType.MultiPatch, TGIS_DimensionType.XYZ)
            shp.Lock(TGIS_Lock.Projection)

            shp.AddPart()
            shp.SetPartType(0, TGIS_PartType.TriangleFan)
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(5, 4, 10))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 5))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 0, 5))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 8, 5))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 8, 5))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 5))
            shp.AddPart()
            shp.SetPartType(1, TGIS_PartType.TriangleStrip)
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 0, 5))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 0, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 8, 5))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 8, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 8, 5))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 8, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 5))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 0))
            shp.AddPart()
            shp.SetPartType(2, TGIS_PartType.OuterRing)
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(4, 0, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(4, 0, 3))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(6, 0, 3))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(6, 0, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 0, 0))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 0, 5))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 5))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 0))
            shp.AddPart()
            shp.SetPartType(3, TGIS_PartType.InnerRing)
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(1, 0, 2))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(1, 0, 4))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(3, 0, 4))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(3, 0, 2))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(1, 0, 2))
            shp.AddPart()
            shp.SetPartType(4, TGIS_PartType.InnerRing)
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(7, 0, 2))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(7, 0, 4))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(9, 0, 4))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(9, 0, 2))
            shp.AddPoint3D(TGIS_Utils.GisPoint3D(7, 0, 2))

            shp.Unlock()
            GIS.FullExtent()
            GIS.Zoom = GIS.Zoom / 2
            btn2D3D_Click(Me, EventArgs.Empty)
            GIS.Viewer3D.CameraPosition = TGIS_Utils.GisPoint3D(10 * (Math.PI / 180), 200 * (Math.PI / 180), 28)
            GIS.Viewer3D.ShowLights = True
            GIS.Viewer3D.ShowVectorEdges = False
        Finally
            GIS.Unlock()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If GIS.View3D Then
            GIS.Viewer3D.LightVector = Not GIS.Viewer3D.LightVector
            GIS.Viewer3D.UpdateWholeMap()
        End If
    End Sub

    Private Sub btnNavigation_Click(sender As Object, e As EventArgs) Handles btnNavigation.Click
        If Not GIS.View3D Then Return
        If Not GIS.Viewer3D.AdvNavigation Then
            GIS.Viewer3D.AdvNavigation = True
            btnNavigation.Text = "Std. Navigation"
            GIS.Viewer3D.FastMode = True
            btnRefresh.Text = "Unlock Refresh"
        Else
            GIS.Viewer3D.AdvNavigation = False
            btnNavigation.Text = "Adv. Navigation"
            GIS.Viewer3D.FastMode = False
            btnRefresh.Text = "Lock Refresh"

        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        If Not GIS.View3D Then Return
        If Not GIS.Viewer3D.FastMode Then
            GIS.Viewer3D.FastMode = True
            btnRefresh.Text = "Unlock Refresh"
        Else
            GIS.Viewer3D.FastMode = False
            btnRefresh.Text = "Lock Refresh"
        End If
    End Sub

    Private Sub btnTextures_Click(sender As Object, e As EventArgs) Handles btnTextures.Click
        Dim lv As TGIS_LayerVector

        lv = CType(GIS.Get("buildings"), TGIS_LayerVector)
        If lv Is Nothing Then Return

        If lv.Params.Area.Bitmap Is Nothing Then
            Dim bmp As New TGIS_Bitmap
            Try
                btnTextures.Text = "Hide Textures"
                bmp.LoadFromBitmap(PictureBox2.Image, "")
                lv.Params.Area.Bitmap = bmp
                bmp.LoadFromBitmap(PictureBox1.Image, "")
                lv.Params.Area.OutlineBitmap = bmp
            Finally
                bmp.Dispose()
            End Try
        Else
            btnTextures.Text = "Show Textures"
            lv.Params.Area.Bitmap = Nothing
            lv.Params.Area.OutlineBitmap = Nothing
        End If

        GIS.Viewer3D.UpdateWholeMap()
    End Sub

    Private Sub GIS_MouseDown(sender As Object, e As MouseEventArgs) Handles GIS.MouseDown
        Dim shp As TGIS_Shape
        Dim prec

        'if there Is no layer Or we are Not in select mode, exit
        If GIS.IsEmpty Then Return
        If GIS.IsEmpty Then Return

        If GIS.View3D Then

            If (GIS.Viewer3D.Mode = TGIS_Viewer3DMode.Select) Then
                prec = 20
                shp = CType((GIS.Locate(New Point(e.X, e.Y), prec)), TGIS_Shape)

                If (shp IsNot Nothing) Then
                    shp.IsSelected = Not shp.IsSelected
                    GIS.Viewer3D.UpdateAllSelectedObjects()
                End If
            End If
        End If
    End Sub
End Class
