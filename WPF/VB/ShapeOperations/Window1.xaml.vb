Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows
Imports System.Windows.Input
Imports TatukGIS.NDK

Public Class Window1

    Private Sub Window1_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        currShape = Nothing
        edtShape = Nothing

        rbMove.IsChecked = True
        rbMove_Click(Me, New RoutedEventArgs())

        GIS.Lock()
        GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "Samples\3D\buildings.shp")

        edtLayer = New TGIS_LayerVector()
        edtLayer.CS = GIS.CS
        edtLayer.CachedPaint = False  ' makes tracking layer
        edtLayer.Params.Area.Pattern = TGIS_BrushStyle.Clear
        edtLayer.Params.Area.OutlineColor = TGIS_Color.Red
        GIS.Add(edtLayer)
        GIS.Unlock()

        GIS.Zoom = GIS.Zoom * 4

        mode = GIS.Mode
        checkBox.IsChecked = GIS.IsManipulationEnabled
        enableSelection()
    End Sub

    Private Sub rbRotate_Click(sender As Object, e As RoutedEventArgs)
        lbHint.Content = "Use long tap to select a shape to start rotating"
    End Sub

    Private Sub rbScale_Click(sender As Object, e As RoutedEventArgs)
        lbHint.Content = "Use long tap to select a shape to start scaling"
    End Sub

    Private Sub rbMove_Click(sender As Object, e As RoutedEventArgs)
        lbHint.Content = "Use long tap to select a shape to start moving"
    End Sub

    Private Sub TransformSelectedShape(_shp As TGIS_Shape, _xx As Double, _yx As Double, _xy As Double, _yy As Double, _dx As Double, _dy As Double)
        Dim centroid As TGIS_Point

        If (_shp Is Nothing) Then Exit Sub

        centroid = _shp.Centroid()

        ' transform
        ' x' = x*xx + y*xy + dx
        ' y' = x*yx + y*yy + dx
        ' z' = z
        _shp.Transform(TGIS_Utils.GisPoint3DFrom2D(centroid),
                       _xx, _yx, 0,
                       _xy, _yy, 0,
                       0, 0, 1,
                       _dx, _dy, 0,
                       False
                      )
    End Sub

    Private Sub RotateSelectedShape(_shp As TGIS_Shape, _angle As Double)
        TransformSelectedShape(
              _shp,
               Math.Cos(_angle), Math.Sin(_angle),
              -Math.Sin(_angle), Math.Cos(_angle),
              0, 0
        )
    End Sub

    Private Sub ScaleSelectedShape(_shp As TGIS_Shape, _x As Double, _y As Double)
        TransformSelectedShape(
              _shp,
              _x, 0,
              0, _y,
              0, 0
        )
    End Sub

    Private Sub TranslateSelectedShape(_shp As TGIS_Shape, _x As Double, _y As Double)
        TransformSelectedShape(
              _shp,
              1, 0,
              0, 1,
              _x, _y
        )
    End Sub

    Private Sub GIS_TapLongEvent(_sender As Object, _e As WPF.TGIS_TapRoutedEventArgs)
        If GIS.IsEmpty Then Exit Sub
        If (GIS.Mode <> TGIS_ViewerMode.Select) Then Exit Sub

        currShape = Nothing
        edtShape = Nothing

        ' find the clicked shape
        Dim pt As System.Drawing.Point = New System.Drawing.Point(Convert.ToInt32(_e.X), Convert.ToInt32(_e.Y))
        Dim shp As TGIS_Shape = CType(GIS.Locate(GIS.ScreenToMap(pt), 5 / GIS.Zoom), TGIS_Shape)

        If shp Is Nothing Then Exit Sub

        ' this temporary setting makes the default mouse (or touch) behavior disabled
        changeViewerMode(TGIS_ViewerMode.UserDefined)

        ' add a 'red' copied shape
        currShape = shp.MakeEditable()
        edtShape = edtLayer.AddShape(currShape.CreateCopy())

        lbHint.Content = "Selected shape : " + currShape.Uid.ToString

        ' remember the starting point
        prevPtg = GIS.ScreenToMap(pt)
        prevX = pt.X
        prevY = pt.Y

        ' refresh the 'red' shape only
        GIS.InvalidateTopmost()
    End Sub

    Private Sub GIS_MouseMove(sender As Object, e As MouseEventArgs)
        ' will be fired also for touches when IsManipulationEnabled = False
        ' content similar to GIS_ManipulationDelta

        disableSelection(e.LeftButton = MouseButtonState.Pressed)

        ' ProcessMessages will not be required 
        operateShape(e.MouseDevice.GetPosition(GIS), False)
    End Sub

    Private Sub GIS_MouseUp(sender As Object, e As MouseButtonEventArgs)
        ' will be fired also for touches when IsManipulationEnabled = False
        ' content similar to GIS_ManipulationCompleted

        selectShape(e.MouseDevice.GetPosition(GIS))
        endOperatingShape()
        enableSelection()
    End Sub

    Private Sub GIS_ManipulationDelta(sender As Object, e As ManipulationDeltaEventArgs)
        ' will be fired for touches when IsManipulationEnabled = True
        ' content similar to GIS_MouseMove

        disableSelection(True)

        ' ProcessMessages will be required 
        operateShape(e.ManipulationOrigin, True)
    End Sub

    Private Sub GIS_ManipulationCompleted(sender As Object, e As ManipulationCompletedEventArgs)
        ' will be fired for touches when IsManipulationEnabled = True
        ' content similar to GIS_MouseUp

        selectShape(e.ManipulationOrigin)
        endOperatingShape()
        enableSelection()
    End Sub

    Private Sub selectShape(_pt As System.Windows.Point)
        If GIS.IsEmpty Then Exit Sub
        If GIS.Mode <> TGIS_ViewerMode.Select Then Exit Sub

        If pointerMoved Then Exit Sub

        Dim ptg As TGIS_Point
        Dim shp As TGIS_Shape

        ' let's locate a shape after click
        ptg = GIS.ScreenToMap(New System.Drawing.Point(Convert.ToInt32(_pt.X),
                                                       Convert.ToInt32(_pt.Y)))
        shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
        If (shp IsNot Nothing) Then
            ' set it as selected
            shp.IsSelected = Not shp.IsSelected
        End If
    End Sub

    Private Sub operateShape(_pt As System.Windows.Point, _processMessages As Boolean)
        If (GIS.Mode <> TGIS_ViewerMode.UserDefined) Then Exit Sub
        If (edtShape Is Nothing) Then Exit Sub

        Dim ptg As TGIS_Point

        ptg = GIS.ScreenToMap(New System.Drawing.Point(Convert.ToInt32(_pt.X), Convert.ToInt32(_pt.Y)))

        If (rbRotate.IsChecked = True) Then
            ' Rotate by moving the mouse horizontally
            RotateSelectedShape(edtShape, ((Math.PI / 180) * ((_pt.X - prevX))))
        ElseIf (rbScale.IsChecked = True) Then
            If ((prevX <> 0) And (prevY <> 0)) Then
                ScaleSelectedShape(edtShape, _pt.X / prevX, _pt.Y / prevY)
            End If
        ElseIf (rbMove.IsChecked = True) Then
            TranslateSelectedShape(edtShape, (ptg.X - prevPtg.X), (ptg.Y - prevPtg.Y))
        End If

        ' refresh tracking layer
        GIS.InvalidateTopmost()
        If _processMessages Then
            GIS.ControlProcessMessages()
        End If

        prevPtg.X = ptg.X
        prevPtg.Y = ptg.Y
        prevX = _pt.X
        prevY = _pt.Y
    End Sub

    Private Sub endOperatingShape()
        If (GIS.Mode <> TGIS_ViewerMode.UserDefined) Then Exit Sub
        If (edtShape Is Nothing) Then Exit Sub

        lbHint.Content = "No selected shape. Select shape"
        ' copy the new geometry to the selected shape
        currShape.CopyGeometry(edtShape)
        ' clear the 'red' layer
        edtLayer.RevertAll()
        currShape = Nothing
        edtShape = Nothing
        ' restore the initial mode
        changeViewerMode(mode)
        ' refresh all layers
        GIS.InvalidateWholeMap()
    End Sub

    Private Sub disableSelection(_pressed As Boolean)
        If (GIS.Mode = TGIS_ViewerMode.Select) Then
            If _pressed Then
                pointerMoved = True
            End If
        End If
    End Sub

    Private Sub enableSelection()
        pointerMoved = False
    End Sub

    Private Sub changeViewerMode(_mode As TGIS_ViewerMode)
        GIS.Mode = _mode
        If GIS.Mode = TGIS_ViewerMode.Select Then
            btnMode.Content = "Mode: Select"
        ElseIf GIS.Mode = TGIS_ViewerMode.UserDefined Then
            btnMode.Content = "Mode: UserDefined"
        End If
        checkBox.IsChecked = GIS.IsManipulationEnabled
    End Sub

    Dim currShape As TGIS_Shape
    Dim edtShape As TGIS_Shape
    Dim edtLayer As TGIS_LayerVector
    Dim prevX As Double
    Dim prevY As Double
    Dim prevPtg As TGIS_Point
    Dim mode As TGIS_ViewerMode
    Dim pointerMoved As Boolean
End Class
