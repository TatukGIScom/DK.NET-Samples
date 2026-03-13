Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Public Class Window1

    Private Sub Window1_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        GIS_Legend.GIS_Viewer = GIS
        GIS_Scale.GIS_Viewer = GIS
        GIS_Arrow.GIS_Viewer = GIS
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\\World\\WorldDCW\\world.shp")
    End Sub

    Private Sub btnZoom_Click(sender As Object, e As RoutedEventArgs)
        GIS.Mode = TGIS_ViewerMode.Zoom
    End Sub

    Private Sub btnDrag_Click(sender As Object, e As RoutedEventArgs)
        GIS.Mode = TGIS_ViewerMode.Drag
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As RoutedEventArgs)
        GIS.Mode = TGIS_ViewerMode.Select
    End Sub

    Private Sub btnClose_Click(sender As Object, e As RoutedEventArgs)
        GIS.Close()
    End Sub

    Private Sub btnFullExtent_Click(sender As Object, e As RoutedEventArgs)
        GIS.FullExtent()
    End Sub

    Private Sub btnCS_Click(sender As Object, e As RoutedEventArgs)
        Dim wgs84 As TGIS_CSGeographicCoordinateSystem
        Dim mercator As TGIS_CSProjectedCoordinateSystem

        wgs84 = TGIS_Utils.CSGeographicCoordinateSystemList.ByEPSG(4326)
        mercator = TGIS_Utils.CSProjectedCoordinateSystemList.ByEPSG(3857)
        If (GIS.CS Is wgs84) Then
            GIS.CS = mercator
        ElseIf (GIS.CS Is mercator) Then
            GIS.CS = wgs84
        End If
        GIS.FullExtent()
    End Sub

    Private Sub GIS_TapSimpleEvent(_sender As Object, _e As WPF.TGIS_TapRoutedEventArgs)
        If GIS.IsEmpty Then Exit Sub
        If (GIS.Mode <> TGIS_ViewerMode.Select) Then Exit Sub

        Dim pt As System.Drawing.Point = New System.Drawing.Point(Convert.ToInt32(_e.X), Convert.ToInt32(_e.Y))
        'let's try to locate a selected shape on the map
        Dim shp As TGIS_Shape = CType(GIS.Locate(GIS.ScreenToMap(pt), 5 / GIS.Zoom), TGIS_Shape)

        If (shp Is Nothing) Then Exit Sub
        'if any found select it
        shp.IsSelected = Not shp.IsSelected
    End Sub

    Private Sub GIS_TapLongEvent(_sender As Object, _e As WPF.TGIS_TapRoutedEventArgs)
        If GIS.IsEmpty Then Exit Sub
        If (GIS.Mode <> TGIS_ViewerMode.Select) Then Exit Sub

        Dim ll As TGIS_LayerVector = CType(GIS.Items(0), TGIS_LayerVector)
        ll.DeselectAll()
        Dim pt As System.Drawing.Point = New System.Drawing.Point(Convert.ToInt32(_e.X), Convert.ToInt32(_e.Y))
        Dim shp As TGIS_Shape = CType(GIS.Locate(GIS.ScreenToMap(pt), 5 / GIS.Zoom), TGIS_Shape)

        If (shp Is Nothing) Then Exit Sub
        ' if any found select it And show shape info
        shp.IsSelected = Not shp.IsSelected
        ShowInfo(shp)
    End Sub
    Private Sub ShowInfo(_shp As TGIS_Shape)

    End Sub
End Class
