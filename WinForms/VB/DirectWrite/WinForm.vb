Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms
Imports TatukGIS.RTL
Imports System.IO

Namespace DirectWrite
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private WithEvents btnBuild As Button
        Private WithEvents btnImport As Button
        Private WithEvents btnMergeLayer As Button
        Private WithEvents btnWrite As Button
        Private WithEvents btnMergeHelper As Button
        Private GIS As TGIS_ViewerWnd
        Private numb As Integer
        Private exist As Boolean

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
        ''' the contents of me method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.btnBuild = New System.Windows.Forms.Button()
            Me.btnImport = New System.Windows.Forms.Button()
            Me.btnMergeLayer = New System.Windows.Forms.Button()
            Me.btnWrite = New System.Windows.Forms.Button()
            Me.btnMergeHelper = New System.Windows.Forms.Button()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.SuspendLayout()
            '
            'btnBuild
            '
            Me.btnBuild.Location = New System.Drawing.Point(1, 2)
            Me.btnBuild.Name = "btnBuild"
            Me.btnBuild.Size = New System.Drawing.Size(75, 23)
            Me.btnBuild.TabIndex = 0
            Me.btnBuild.Text = "Build layer"
            Me.btnBuild.UseVisualStyleBackColor = True
            '
            'btnImport
            '
            Me.btnImport.Enabled = False
            Me.btnImport.Location = New System.Drawing.Point(82, 2)
            Me.btnImport.Name = "btnImport"
            Me.btnImport.Size = New System.Drawing.Size(75, 23)
            Me.btnImport.TabIndex = 1
            Me.btnImport.Text = "Import layer"
            Me.btnImport.UseVisualStyleBackColor = True
            '
            'btnMergeLayer
            '
            Me.btnMergeLayer.Enabled = False
            Me.btnMergeLayer.Location = New System.Drawing.Point(163, 2)
            Me.btnMergeLayer.Name = "btnMergeLayer"
            Me.btnMergeLayer.Size = New System.Drawing.Size(75, 23)
            Me.btnMergeLayer.TabIndex = 2
            Me.btnMergeLayer.Text = "Merge layer"
            Me.btnMergeLayer.UseVisualStyleBackColor = True
            '
            'btnWrite
            '
            Me.btnWrite.Enabled = False
            Me.btnWrite.Location = New System.Drawing.Point(244, 2)
            Me.btnWrite.Name = "btnWrite"
            Me.btnWrite.Size = New System.Drawing.Size(75, 23)
            Me.btnWrite.TabIndex = 3
            Me.btnWrite.Text = "Direct write"
            Me.btnWrite.UseVisualStyleBackColor = True
            '
            'btnMergeHelper
            '
            Me.btnMergeHelper.Enabled = False
            Me.btnMergeHelper.Location = New System.Drawing.Point(325, 2)
            Me.btnMergeHelper.Name = "btnMergeHelper"
            Me.btnMergeHelper.Size = New System.Drawing.Size(84, 23)
            Me.btnMergeHelper.TabIndex = 4
            Me.btnMergeHelper.Text = "Merge helper"
            Me.btnMergeHelper.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.CursorFor3DSelect = Nothing
            Me.GIS.CursorForCameraZoom = Nothing
            Me.GIS.Location = New System.Drawing.Point(1, 31)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(583, 429)
            Me.GIS.TabIndex = 5
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(584, 461)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.btnMergeHelper)
            Me.Controls.Add(Me.btnWrite)
            Me.Controls.Add(Me.btnMergeLayer)
            Me.Controls.Add(Me.btnImport)
            Me.Controls.Add(Me.btnBuild)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - DirectWrite"

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
            GIS.Mode = TGIS_ViewerMode.Zoom
            numb = 0
            exist = True

            While exist
                If Directory.Exists("Shapes" + numb.ToString) Then
                    numb = numb + 1
                Else
                    exist = False
                End If
            End While

            Directory.CreateDirectory("Shapes" + numb.ToString)

        End Sub

        Private Sub btnBuild_Click(sender As Object, e As EventArgs) Handles btnBuild.Click
            Dim lv As TGIS_LayerSHP
            Dim ll As TGIS_LayerSHP

            GIS.Close()

            btnImport.Enabled = True

            lv = New TGIS_LayerSHP()
            If Directory.Exists("Shapes" + numb.ToString) Then
                numb = numb + 1
                Directory.CreateDirectory("Shapes" + numb.ToString)
            End If
            lv.Build(("Shapes" + numb.ToString + "\build.shp"), TGIS_Utils.GisExtent(-180, -90, 180, 90), TGIS_ShapeType.Point, TGIS_DimensionType.XY)

            lv.Open()
            ll = New TGIS_LayerSHP()

            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + "\World\WorldDCW\cities.shp"
            ll.Open()

            lv.ImportStructure(ll)
            lv.CS = ll.CS

            For Each shp As TGIS_Shape In ll.Loop
                lv.AddShape(shp, True)
            Next

            lv.SaveData()

            GIS.Add(lv)
            GIS.FullExtent()
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
            Dim ll As TGIS_LayerSHP
            Dim lv As TGIS_LayerSHP
            Dim shp As TGIS_Shape

            GIS.Close()

            btnMergeLayer.Enabled = True

            ll = New TGIS_LayerSHP()
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + "\World\WorldDCW\cities.shp"
            GIS.Add(ll)

            shp = TGIS_GeometryFactory.GisCreateShapeFromWKT("POLYGON((7.86 56.39,31.37 56.39,31.37 39.48,7.86 39.48,7.868 56.39))")

            lv = New TGIS_LayerSHP()
            lv.Path = "Shapes" + numb.ToString + "\imported.shp"
            lv.CS = ll.CS
            lv.ImportLayerEx(ll, ll.Extent, TGIS_ShapeType.Unknown, "", shp, TGIS_Utils.GIS_RELATE_CONTAINS(), False)

            GIS.Add(lv)
            lv.Params.Marker.Color = TGIS_Color.Green
            GIS.FullExtent()
            GIS.VisibleExtent = lv.Extent
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnMergeLayer_Click(sender As Object, e As EventArgs) Handles btnMergeLayer.Click
            Dim ll As TGIS_LayerSHP
            Dim lv As TGIS_LayerSHP
            Dim shp As TGIS_Shape

            GIS.Close()

            btnWrite.Enabled = True

            ll = New TGIS_LayerSHP()
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + "\World\WorldDCW\cities.shp"
            GIS.Add(ll)

            shp = TGIS_GeometryFactory.GisCreateShapeFromWKT("'POLYGON((7.86 56.39,31.37 56.39,31.37 39.48,7.86 39.48,7.868 56.39))")

            lv = New TGIS_LayerSHP()
            lv.Path = "Shapes" + numb.ToString + "\imported.shp"
            lv.CS = ll.CS
            lv.MergeLayerEx(ll, ll.Extent, TGIS_ShapeType.Unknown, "", shp, TGIS_Utils.GIS_RELATE_DISJOINT(), False, False)

            GIS.Add(lv)
            lv.Params.Marker.Color = TGIS_Color.Green
            GIS.FullExtent()
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnWrite_Click(sender As Object, e As EventArgs) Handles btnWrite.Click
            Dim ll As TGIS_LayerSHP
            Dim lv As TGIS_LayerSHP
            Dim shp As TGIS_Shape
            Dim dwh As TGIS_LayerVectorDirectWriteHelper

            GIS.Close()

            btnMergeHelper.Enabled = True

            ll = New TGIS_LayerSHP()

            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + "\World\WorldDCW\cities.shp"
            ll.Open()

            lv = New TGIS_LayerSHP()
            lv.ImportStructure(ll)
            lv.CS = ll.CS

            dwh = New TGIS_LayerVectorDirectWriteHelper(lv)
            dwh.Build(("Shapes" + numb.ToString + "\direct_write.shp"), ll.Extent, TGIS_ShapeType.Point, TGIS_DimensionType.XY)

            For Each shp In ll.Loop()
                dwh.AddShape(shp)
            Next

            dwh.Close()

            GIS.Add(lv)
            GIS.FullExtent()
        End Sub

        Private Sub btnMergeHelper_Click(sender As Object, e As EventArgs) Handles btnMergeHelper.Click
            Dim ll As TGIS_LayerSHP
            Dim lv As TGIS_LayerSHP
            Dim shp As TGIS_Shape
            Dim mh As TGIS_LayerVectorMergeHelper

            GIS.Close()

            btnMergeHelper.Enabled = False

            ll = New TGIS_LayerSHP()
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + "\World\WorldDCW\cities.shp"
            ll.Open()

            lv = New TGIS_LayerSHP()
            lv.ImportStructure(ll)
            lv.CS = ll.CS
            lv.Build(("Shapes" + numb.ToString + "\merge_helper.shp"), ll.Extent, TGIS_ShapeType.Point, TGIS_DimensionType.XY)

            mh = New TGIS_LayerVectorMergeHelper(lv, 500)

            For Each shp In ll.Loop()
                mh.AddShape(shp)
                mh.Commit()
            Next

            btnImport.Enabled = False
            btnMergeLayer.Enabled = False
            btnWrite.Enabled = False

            GIS.Add(lv)
            GIS.FullExtent()
        End Sub
    End Class
End Namespace
