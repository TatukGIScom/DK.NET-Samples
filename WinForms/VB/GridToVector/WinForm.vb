Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace GridToVector
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        Private panel1 As Panel
        Private WithEvents GIS As TGIS_ViewerWnd
        Private gbLoadData As GroupBox
        Private WithEvents btnLoadDEM As Button
        Private WithEvents btnLoadLand As Button
        Private gbGridToPolygon As GroupBox
        Private chkSplit As CheckBox
        Private tbTolerance As TextBox
        Private lblTolerance As Label
        Private WithEvents btnGridToPolygon As Button
        Private GIS_Attr As TGIS_ControlAttributes
        Private pProgressBar As ProgressBar
        Private gbGridToPoint As GroupBox
        Private gbCommon As GroupBox
        Private chkIgnoreNoData As CheckBox
        Private lblPointSpacing As Label
        Private tbPointSpacing As TextBox
        Private WithEvents btnGridToPoint As Button
        Const LV_FIELD As String = "vector"
        Const LV_NAME As String = "polygons"
        Private components As System.ComponentModel.IContainer

        Public Sub New()
            InitializeComponent()
            Me.ActiveControl = GIS
            AddHandler GIS.MouseWheel, New System.Windows.Forms.MouseEventHandler(AddressOf Me.GIS_MouseWheel)
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then

                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If

            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.gbGridToPoint = New System.Windows.Forms.GroupBox()
            Me.btnGridToPoint = New System.Windows.Forms.Button()
            Me.tbPointSpacing = New System.Windows.Forms.TextBox()
            Me.lblPointSpacing = New System.Windows.Forms.Label()
            Me.gbCommon = New System.Windows.Forms.GroupBox()
            Me.chkIgnoreNoData = New System.Windows.Forms.CheckBox()
            Me.lblTolerance = New System.Windows.Forms.Label()
            Me.tbTolerance = New System.Windows.Forms.TextBox()
            Me.GIS_Attr = New TatukGIS.NDK.WinForms.TGIS_ControlAttributes()
            Me.gbGridToPolygon = New System.Windows.Forms.GroupBox()
            Me.btnGridToPolygon = New System.Windows.Forms.Button()
            Me.chkSplit = New System.Windows.Forms.CheckBox()
            Me.gbLoadData = New System.Windows.Forms.GroupBox()
            Me.btnLoadDEM = New System.Windows.Forms.Button()
            Me.btnLoadLand = New System.Windows.Forms.Button()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.pProgressBar = New System.Windows.Forms.ProgressBar()
            Me.panel1.SuspendLayout()
            Me.gbGridToPoint.SuspendLayout()
            Me.gbCommon.SuspendLayout()
            Me.gbGridToPolygon.SuspendLayout()
            Me.gbLoadData.SuspendLayout()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.gbGridToPoint)
            Me.panel1.Controls.Add(Me.gbCommon)
            Me.panel1.Controls.Add(Me.GIS_Attr)
            Me.panel1.Controls.Add(Me.gbGridToPolygon)
            Me.panel1.Controls.Add(Me.gbLoadData)
            Me.panel1.Location = New System.Drawing.Point(2, -5)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(219, 662)
            Me.panel1.TabIndex = 0
            '
            'gbGridToPoint
            '
            Me.gbGridToPoint.Controls.Add(Me.btnGridToPoint)
            Me.gbGridToPoint.Controls.Add(Me.tbPointSpacing)
            Me.gbGridToPoint.Controls.Add(Me.lblPointSpacing)
            Me.gbGridToPoint.Location = New System.Drawing.Point(11, 255)
            Me.gbGridToPoint.Name = "gbGridToPoint"
            Me.gbGridToPoint.Size = New System.Drawing.Size(200, 72)
            Me.gbGridToPoint.TabIndex = 4
            Me.gbGridToPoint.TabStop = False
            Me.gbGridToPoint.Text = "Grid to point"
            '
            'btnGridToPoint
            '
            Me.btnGridToPoint.Location = New System.Drawing.Point(5, 41)
            Me.btnGridToPoint.Name = "btnGridToPoint"
            Me.btnGridToPoint.Size = New System.Drawing.Size(188, 23)
            Me.btnGridToPoint.TabIndex = 2
            Me.btnGridToPoint.Text = "Generate"
            Me.btnGridToPoint.UseVisualStyleBackColor = True
            '
            'tbPointSpacing
            '
            Me.tbPointSpacing.Location = New System.Drawing.Point(86, 13)
            Me.tbPointSpacing.Name = "tbPointSpacing"
            Me.tbPointSpacing.Size = New System.Drawing.Size(107, 20)
            Me.tbPointSpacing.TabIndex = 1
            '
            'lblPointSpacing
            '
            Me.lblPointSpacing.AutoSize = True
            Me.lblPointSpacing.Location = New System.Drawing.Point(6, 16)
            Me.lblPointSpacing.Name = "lblPointSpacing"
            Me.lblPointSpacing.Size = New System.Drawing.Size(74, 13)
            Me.lblPointSpacing.TabIndex = 0
            Me.lblPointSpacing.Text = "Point spacing:"
            '
            'gbCommon
            '
            Me.gbCommon.Controls.Add(Me.chkIgnoreNoData)
            Me.gbCommon.Controls.Add(Me.lblTolerance)
            Me.gbCommon.Controls.Add(Me.tbTolerance)
            Me.gbCommon.Location = New System.Drawing.Point(10, 105)
            Me.gbCommon.Name = "gbCommon"
            Me.gbCommon.Size = New System.Drawing.Size(200, 62)
            Me.gbCommon.TabIndex = 3
            Me.gbCommon.TabStop = False
            Me.gbCommon.Text = "Common parameters"
            '
            'chkIgnoreNoData
            '
            Me.chkIgnoreNoData.AutoSize = True
            Me.chkIgnoreNoData.Location = New System.Drawing.Point(9, 35)
            Me.chkIgnoreNoData.Name = "chkIgnoreNoData"
            Me.chkIgnoreNoData.Size = New System.Drawing.Size(96, 17)
            Me.chkIgnoreNoData.TabIndex = 2
            Me.chkIgnoreNoData.Text = "Ignore NoData"
            Me.chkIgnoreNoData.UseVisualStyleBackColor = True
            '
            'lblTolerance
            '
            Me.lblTolerance.AutoSize = True
            Me.lblTolerance.Location = New System.Drawing.Point(6, 16)
            Me.lblTolerance.Name = "lblTolerance"
            Me.lblTolerance.Size = New System.Drawing.Size(58, 13)
            Me.lblTolerance.TabIndex = 0
            Me.lblTolerance.Text = "Tolerance:"
            '
            'tbTolerance
            '
            Me.tbTolerance.Location = New System.Drawing.Point(70, 13)
            Me.tbTolerance.Name = "tbTolerance"
            Me.tbTolerance.Size = New System.Drawing.Size(124, 20)
            Me.tbTolerance.TabIndex = 1
            '
            'GIS_Attr
            '
            Me.GIS_Attr.Location = New System.Drawing.Point(11, 333)
            Me.GIS_Attr.Name = "GIS_Attr"
            Me.GIS_Attr.ReadOnly = True
            Me.GIS_Attr.Size = New System.Drawing.Size(199, 318)
            Me.GIS_Attr.TabIndex = 2
            '
            'gbGridToPolygon
            '
            Me.gbGridToPolygon.Controls.Add(Me.btnGridToPolygon)
            Me.gbGridToPolygon.Controls.Add(Me.chkSplit)
            Me.gbGridToPolygon.Location = New System.Drawing.Point(11, 173)
            Me.gbGridToPolygon.Name = "gbGridToPolygon"
            Me.gbGridToPolygon.Size = New System.Drawing.Size(200, 76)
            Me.gbGridToPolygon.TabIndex = 1
            Me.gbGridToPolygon.TabStop = False
            Me.gbGridToPolygon.Text = "Grid to polygon"
            '
            'btnGridToPolygon
            '
            Me.btnGridToPolygon.Location = New System.Drawing.Point(6, 42)
            Me.btnGridToPolygon.Name = "btnGridToPolygon"
            Me.btnGridToPolygon.Size = New System.Drawing.Size(188, 23)
            Me.btnGridToPolygon.TabIndex = 3
            Me.btnGridToPolygon.Text = "Generate"
            Me.btnGridToPolygon.UseVisualStyleBackColor = True
            '
            'chkSplit
            '
            Me.chkSplit.AutoSize = True
            Me.chkSplit.Checked = True
            Me.chkSplit.CheckState = System.Windows.Forms.CheckState.Checked
            Me.chkSplit.Location = New System.Drawing.Point(8, 19)
            Me.chkSplit.Name = "chkSplit"
            Me.chkSplit.Size = New System.Drawing.Size(83, 17)
            Me.chkSplit.TabIndex = 2
            Me.chkSplit.Text = "Split shapes"
            Me.chkSplit.UseVisualStyleBackColor = True
            '
            'gbLoadData
            '
            Me.gbLoadData.Controls.Add(Me.btnLoadDEM)
            Me.gbLoadData.Controls.Add(Me.btnLoadLand)
            Me.gbLoadData.Location = New System.Drawing.Point(10, 17)
            Me.gbLoadData.Name = "gbLoadData"
            Me.gbLoadData.Size = New System.Drawing.Size(200, 82)
            Me.gbLoadData.TabIndex = 0
            Me.gbLoadData.TabStop = False
            Me.gbLoadData.Text = "Load data"
            '
            'btnLoadDEM
            '
            Me.btnLoadDEM.Location = New System.Drawing.Point(7, 49)
            Me.btnLoadDEM.Name = "btnLoadDEM"
            Me.btnLoadDEM.Size = New System.Drawing.Size(187, 23)
            Me.btnLoadDEM.TabIndex = 1
            Me.btnLoadDEM.Text = "DEM"
            Me.btnLoadDEM.UseVisualStyleBackColor = True
            '
            'btnLoadLand
            '
            Me.btnLoadLand.Location = New System.Drawing.Point(6, 19)
            Me.btnLoadLand.Name = "btnLoadLand"
            Me.btnLoadLand.Size = New System.Drawing.Size(188, 23)
            Me.btnLoadLand.TabIndex = 0
            Me.btnLoadLand.Text = "Land Cover"
            Me.btnLoadLand.UseVisualStyleBackColor = True
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Location = New System.Drawing.Point(227, -5)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(545, 633)
            Me.GIS.TabIndex = 1
            '
            'pProgressBar
            '
            Me.pProgressBar.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.pProgressBar.Location = New System.Drawing.Point(227, 634)
            Me.pProgressBar.Name = "pProgressBar"
            Me.pProgressBar.Size = New System.Drawing.Size(545, 23)
            Me.pProgressBar.TabIndex = 2
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(774, 658)
            Me.Controls.Add(Me.pProgressBar)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - GridToVector"
            Me.panel1.ResumeLayout(False)
            Me.gbGridToPoint.ResumeLayout(False)
            Me.gbGridToPoint.PerformLayout()
            Me.gbCommon.ResumeLayout(False)
            Me.gbCommon.PerformLayout()
            Me.gbGridToPolygon.ResumeLayout(False)
            Me.gbGridToPolygon.PerformLayout()
            Me.gbLoadData.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            btnLoadLand.PerformClick()
            GIS.Mode = TGIS_ViewerMode.[Select]
        End Sub

        Private Sub btnLoadLand_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadLand.Click
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\Luxembourg\CLC2018_CLC2018_V2018_20_Luxembourg.tif")
            tbTolerance.Text = "1"
            tbPointSpacing.Text = "1000"
        End Sub

        Private Sub btnLoadDEM_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadDEM.Click
            Dim lp As TGIS_LayerPixel
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "Samples\3D\elevation.grd")
            lp = TryCast(GIS.[Get]("elevation"), TGIS_LayerPixel)
            lp.GenerateRamp(TGIS_Color.Blue, TGIS_Color.Lime, TGIS_Color.Red, lp.MinHeight, (lp.MinHeight + lp.MaxHeight) / 2, lp.MaxHeight, True, (lp.MaxHeight - lp.MinHeight) / 100, (lp.MaxHeight - lp.MinHeight) / 10, Nothing, True, TGIS_ColorInterpolationMode.HSL)
            GIS.InvalidateWholeMap()
            tbTolerance.Text = "10"
            tbPointSpacing.Text = "200"
        End Sub

        Private Sub doBusyEvent(ByVal sender As Object, ByVal e As TGIS_BusyEventArgs)
            Select Case e.Pos
                Case 0
                    pProgressBar.Minimum = 0
                    pProgressBar.Maximum = CInt(e.EndPos)
                    pProgressBar.Value = CInt(e.Pos)
                Case -1
                    pProgressBar.Value = 0
                Case Else
                    pProgressBar.Value = CInt(e.Pos)
            End Select
        End Sub

        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles GIS.MouseDown
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                Return
            End If

            shp = TryCast(GIS.Locate(GIS.ScreenToMap(New Point(e.X, e.Y)), 5 / GIS.Zoom), TGIS_Shape)

            If shp Is Nothing Then
                Return
            End If

            shp.Layer.DeselectAll()
            shp.IsSelected = Not shp.IsSelected
            GIS_Attr.ShowShape(shp)
        End Sub

        Private Sub GIS_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
            If GIS.IsEmpty Then Return

            If e.Delta < 0 Then
                GIS.ZoomBy(2 / 1.0, e.X, e.Y)
            Else
                GIS.ZoomBy(1 / 2.0, e.X, e.Y)
            End If
        End Sub

        Private Sub btnGridToPolygon_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGridToPolygon.Click
            Dim lp As TGIS_LayerPixel
            Dim lv As TGIS_LayerVector
            Dim polygonizer As TGIS_GridToPolygon
            lp = TryCast(GIS.Items(0), TGIS_LayerPixel)

            If GIS.[Get](LV_NAME) IsNot Nothing Then
                GIS.Delete(LV_NAME)
            End If

            lv = New TGIS_LayerVector()
            lv.Name = LV_NAME
            lv.Open()
            lv.CS = lp.CS
            lv.DefaultShapeType = TGIS_ShapeType.Polygon
            lv.AddField(LV_FIELD, TGIS_FieldType.Float, 0, 0)
            lv.Transparency = 50
            lv.Params.Area.OutlineColor = TGIS_Color.Black
            polygonizer = New TGIS_GridToPolygon()
            polygonizer.Tolerance = Single.Parse(tbTolerance.Text)
            polygonizer.SplitShapes = chkSplit.Checked
            AddHandler polygonizer.BusyEvent, AddressOf doBusyEvent
            polygonizer.Generate(lp, lv, LV_FIELD)
            GIS.Add(lv)
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub btnGridToPoint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGridToPoint.Click
            Dim lp As TGIS_LayerPixel = TryCast(GIS.Items(0), TGIS_LayerPixel)
            If GIS.[Get](LV_NAME) IsNot Nothing Then GIS.Delete(LV_NAME)
            Dim lv As TGIS_LayerVector = New TGIS_LayerVector()
            lv.Name = LV_NAME
            lv.Open()
            lv.CS = lp.CS
            lv.DefaultShapeType = TGIS_ShapeType.Point
            lv.AddField(LV_FIELD, TGIS_FieldType.Float, 0, 0)
            lv.Params.Marker.Color = TGIS_Color.Black
            lv.Params.Marker.Style = TGIS_MarkerStyle.Circle
            lv.Params.Marker.SizeAsText = "SIZE:4pt"
            lv.Transparency = 75
            Dim grid_to_point As TGIS_GridToPoint = New TGIS_GridToPoint()
            AddHandler grid_to_point.BusyEvent, AddressOf doBusyEvent
            grid_to_point.IgnoreNoData = chkIgnoreNoData.Checked
            grid_to_point.Tolerance = Single.Parse(tbTolerance.Text)
            grid_to_point.PointSpacing = Single.Parse(tbPointSpacing.Text)
            grid_to_point.Generate(lp, lv, LV_FIELD)
            GIS.Add(lv)
            GIS.InvalidateWholeMap()
        End Sub
    End Class
End Namespace
