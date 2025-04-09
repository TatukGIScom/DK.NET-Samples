Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace AddLayer
    '' <summary>
    '' Summary description for WinForm.
    '' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        '' <summary>
        '' Required designer variable.
        '' </summary>
        Private components As System.ComponentModel.IContainer
        Private imageList1 As System.Windows.Forms.ImageList
        Friend WithEvents GIS As TGIS_ViewerWnd
        Friend WithEvents btnFullExtent As Button
        Friend WithEvents btnReset As Button
        Friend WithEvents gbMapMode As GroupBox
        Friend WithEvents rbtnAddObserver As RadioButton
        Friend WithEvents rbtnZoom As RadioButton
        Friend WithEvents gbVisibleLayer As GroupBox
        Friend WithEvents rbtnAGL As RadioButton
        Friend WithEvents rbtnViewshedFreq As RadioButton
        Friend WithEvents rbtnViewshedBinary As RadioButton
        Friend WithEvents lblHint As Label
        Friend WithEvents lblObserverElevation As Label
        Friend WithEvents StatusStrip1 As StatusStrip
        Friend WithEvents statusStrip As ToolStripStatusLabel
        Friend WithEvents edtObserverElevation As TextBox

        Public Sub New()
            '
            '' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            '' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub

        '' <summary>
        '' Clean up any resources being used.
        '' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        '' <summary>
        '' Required method for Designer support - do not modify
        '' the contents of this method with the code editor.
        '' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.btnFullExtent = New System.Windows.Forms.Button()
            Me.btnReset = New System.Windows.Forms.Button()
            Me.gbMapMode = New System.Windows.Forms.GroupBox()
            Me.rbtnAddObserver = New System.Windows.Forms.RadioButton()
            Me.rbtnZoom = New System.Windows.Forms.RadioButton()
            Me.gbVisibleLayer = New System.Windows.Forms.GroupBox()
            Me.rbtnAGL = New System.Windows.Forms.RadioButton()
            Me.rbtnViewshedFreq = New System.Windows.Forms.RadioButton()
            Me.rbtnViewshedBinary = New System.Windows.Forms.RadioButton()
            Me.lblHint = New System.Windows.Forms.Label()
            Me.lblObserverElevation = New System.Windows.Forms.Label()
            Me.edtObserverElevation = New System.Windows.Forms.TextBox()
            Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
            Me.statusStrip = New System.Windows.Forms.ToolStripStatusLabel()
            Me.gbMapMode.SuspendLayout()
            Me.gbVisibleLayer.SuspendLayout()
            Me.StatusStrip1.SuspendLayout()
            Me.SuspendLayout()
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Location = New System.Drawing.Point(169, 29)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
            Me.GIS.Size = New System.Drawing.Size(582, 374)
            Me.GIS.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.Location = New System.Drawing.Point(12, 12)
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.Size = New System.Drawing.Size(148, 23)
            Me.btnFullExtent.TabIndex = 1
            Me.btnFullExtent.Text = "Full Extent"
            Me.btnFullExtent.UseVisualStyleBackColor = True
            '
            'btnReset
            '
            Me.btnReset.Location = New System.Drawing.Point(12, 41)
            Me.btnReset.Name = "btnReset"
            Me.btnReset.Size = New System.Drawing.Size(148, 23)
            Me.btnReset.TabIndex = 2
            Me.btnReset.Text = "Reset"
            Me.btnReset.UseVisualStyleBackColor = True
            '
            'gbMapMode
            '
            Me.gbMapMode.Controls.Add(Me.rbtnAddObserver)
            Me.gbMapMode.Controls.Add(Me.rbtnZoom)
            Me.gbMapMode.Location = New System.Drawing.Point(12, 70)
            Me.gbMapMode.Name = "gbMapMode"
            Me.gbMapMode.Size = New System.Drawing.Size(148, 71)
            Me.gbMapMode.TabIndex = 3
            Me.gbMapMode.TabStop = False
            Me.gbMapMode.Text = "Map Mode"
            '
            'rbtnAddObserver
            '
            Me.rbtnAddObserver.AutoSize = True
            Me.rbtnAddObserver.Checked = True
            Me.rbtnAddObserver.Location = New System.Drawing.Point(15, 42)
            Me.rbtnAddObserver.Name = "rbtnAddObserver"
            Me.rbtnAddObserver.Size = New System.Drawing.Size(90, 17)
            Me.rbtnAddObserver.TabIndex = 1
            Me.rbtnAddObserver.TabStop = True
            Me.rbtnAddObserver.Text = "Add Observer"
            Me.rbtnAddObserver.UseVisualStyleBackColor = True
            '
            'rbtnZoom
            '
            Me.rbtnZoom.AutoSize = True
            Me.rbtnZoom.Location = New System.Drawing.Point(15, 19)
            Me.rbtnZoom.Name = "rbtnZoom"
            Me.rbtnZoom.Size = New System.Drawing.Size(52, 17)
            Me.rbtnZoom.TabIndex = 0
            Me.rbtnZoom.Text = "Zoom"
            Me.rbtnZoom.UseVisualStyleBackColor = True
            '
            'gbVisibleLayer
            '
            Me.gbVisibleLayer.Controls.Add(Me.rbtnAGL)
            Me.gbVisibleLayer.Controls.Add(Me.rbtnViewshedFreq)
            Me.gbVisibleLayer.Controls.Add(Me.rbtnViewshedBinary)
            Me.gbVisibleLayer.Location = New System.Drawing.Point(12, 147)
            Me.gbVisibleLayer.Name = "gbVisibleLayer"
            Me.gbVisibleLayer.Size = New System.Drawing.Size(148, 94)
            Me.gbVisibleLayer.TabIndex = 4
            Me.gbVisibleLayer.TabStop = False
            Me.gbVisibleLayer.Text = "Visible Layer"
            '
            'rbtnAGL
            '
            Me.rbtnAGL.AutoSize = True
            Me.rbtnAGL.Location = New System.Drawing.Point(15, 65)
            Me.rbtnAGL.Name = "rbtnAGL"
            Me.rbtnAGL.Size = New System.Drawing.Size(123, 17)
            Me.rbtnAGL.TabIndex = 2
            Me.rbtnAGL.Text = "Above-Ground-Level"
            Me.rbtnAGL.UseVisualStyleBackColor = True
            '
            'rbtnViewshedFreq
            '
            Me.rbtnViewshedFreq.AutoSize = True
            Me.rbtnViewshedFreq.Location = New System.Drawing.Point(15, 42)
            Me.rbtnViewshedFreq.Name = "rbtnViewshedFreq"
            Me.rbtnViewshedFreq.Size = New System.Drawing.Size(127, 17)
            Me.rbtnViewshedFreq.TabIndex = 1
            Me.rbtnViewshedFreq.Text = "Viewshed (frequancy)"
            Me.rbtnViewshedFreq.UseVisualStyleBackColor = True
            '
            'rbtnViewshedBinary
            '
            Me.rbtnViewshedBinary.AutoSize = True
            Me.rbtnViewshedBinary.Checked = True
            Me.rbtnViewshedBinary.Location = New System.Drawing.Point(15, 19)
            Me.rbtnViewshedBinary.Name = "rbtnViewshedBinary"
            Me.rbtnViewshedBinary.Size = New System.Drawing.Size(108, 17)
            Me.rbtnViewshedBinary.TabIndex = 0
            Me.rbtnViewshedBinary.TabStop = True
            Me.rbtnViewshedBinary.Text = "Viewshed (binary)"
            Me.rbtnViewshedBinary.UseVisualStyleBackColor = True
            '
            'lblHint
            '
            Me.lblHint.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lblHint.Location = New System.Drawing.Point(166, 9)
            Me.lblHint.Name = "lblHint"
            Me.lblHint.Size = New System.Drawing.Size(585, 17)
            Me.lblHint.TabIndex = 5
            Me.lblHint.Text = "Click on the map to add an observer."
            Me.lblHint.TextAlign = System.Drawing.ContentAlignment.TopCenter
            '
            'lblObserverElevation
            '
            Me.lblObserverElevation.AutoSize = True
            Me.lblObserverElevation.Location = New System.Drawing.Point(9, 244)
            Me.lblObserverElevation.Name = "lblObserverElevation"
            Me.lblObserverElevation.Size = New System.Drawing.Size(137, 13)
            Me.lblObserverElevation.TabIndex = 6
            Me.lblObserverElevation.Text = "Observer Elevation (meters)"
            '
            'edtObserverElevation
            '
            Me.edtObserverElevation.Location = New System.Drawing.Point(12, 260)
            Me.edtObserverElevation.Name = "edtObserverElevation"
            Me.edtObserverElevation.Size = New System.Drawing.Size(148, 20)
            Me.edtObserverElevation.TabIndex = 7
            Me.edtObserverElevation.Text = "30"
            '
            'StatusStrip1
            '
            Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statusStrip})
            Me.StatusStrip1.Location = New System.Drawing.Point(0, 406)
            Me.StatusStrip1.Name = "StatusStrip1"
            Me.StatusStrip1.Size = New System.Drawing.Size(763, 22)
            Me.StatusStrip1.TabIndex = 8
            Me.StatusStrip1.Text = "StatusStrip1"
            '
            'statusStrip
            '
            Me.statusStrip.Name = "statusStrip"
            Me.statusStrip.Size = New System.Drawing.Size(0, 17)
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(763, 428)
            Me.Controls.Add(Me.StatusStrip1)
            Me.Controls.Add(Me.edtObserverElevation)
            Me.Controls.Add(Me.lblObserverElevation)
            Me.Controls.Add(Me.lblHint)
            Me.Controls.Add(Me.gbVisibleLayer)
            Me.Controls.Add(Me.gbMapMode)
            Me.Controls.Add(Me.btnReset)
            Me.Controls.Add(Me.btnFullExtent)
            Me.Controls.Add(Me.GIS)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Viewshed"
            Me.gbMapMode.ResumeLayout(False)
            Me.gbMapMode.PerformLayout()
            Me.gbVisibleLayer.ResumeLayout(False)
            Me.gbVisibleLayer.PerformLayout()
            Me.StatusStrip1.ResumeLayout(False)
            Me.StatusStrip1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        '' <summary>
        '' The main entry point for the application.
        '' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        Const SAMPLE_VIEWSHED_NAME As String = "Viewshed"
        Const SAMPLE_AGL_NAME As String = "Above-Ground-Level"

        Private lTerrain As TGIS_LayerPixel
        Private lObservers As TGIS_LayerVector
        Private lViewshed As TGIS_LayerPixel
        Private lAGL As TGIS_LayerPixel

        Private Sub setLayerActive()
            GIS.Lock()
            makeViewshedRamp()
            If GIS.Get(SAMPLE_VIEWSHED_NAME) IsNot Nothing Then
                lViewshed.Active = Not rbtnAGL.Checked
                lAGL.Active = rbtnAGL.Checked
                GIS.InvalidateWholeMap()
            End If

            GIS.Unlock()

            showComment()
        End Sub

        Private Sub showComment()
            If rbtnViewshedBinary.Checked Then
                lblHint.Text = "Green - area of visibility."
            End If
            If rbtnViewshedFreq.Checked Then
                lblHint.Text = "Visibility frequency; " +
                               "Red - one  observer is visible; " +
                               "Green - all observers are visible."
            End If
            If rbtnAGL.Checked Then
                lblHint.Text = "Minimum height that must be added to a nonvisible cell " +
                               "to make it visible by at least one observer; " +
                               "Red = " + Math.Round(lAGL.MaxHeight).ToString + " m"
            End If
        End Sub

        Private Sub makeViewshedRamp()
            If GIS.Get(SAMPLE_VIEWSHED_NAME) Is Nothing Then
                Return
            End If

            lViewshed.Transparency = 50
            lViewshed.Params.Pixel.GridShadow = False
            lViewshed.Params.Pixel.AltitudeMapZones.Clear()

            If rbtnViewshedBinary.Checked Then
                lViewshed.GenerateRamp(
                  TGIS_Color.FromARGB(127, 0, 255, 0),
                  TGIS_Color.None,
                  TGIS_Color.FromARGB(127, 0, 255, 0),
                  lViewshed.MinHeight, 0.01,
                  lViewshed.MaxHeight, False,
                  (lViewshed.MaxHeight - lViewshed.MinHeight) / 100,
                  (lViewshed.MaxHeight - lViewshed.MinHeight) / 10,
                  Nothing, False
                )
            ElseIf rbtnViewshedFreq.Checked Then
                lViewshed.GenerateRamp(
                  TGIS_Color.FromARGB(127, 255, 0, 0),
                  TGIS_Color.None,
                  TGIS_Color.FromARGB(127, 0, 255, 0),
                  0, 0,
                  lViewshed.MaxHeight, False,
                  (lViewshed.MaxHeight - lViewshed.MinHeight) / 100,
                  (lViewshed.MaxHeight - lViewshed.MinHeight) / 10,
                  Nothing, False
                )
            End If
        End Sub


        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            GIS.Lock()
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "World\\Countries\\USA\\States\\California\\San Bernardino\\NED\\w001001.adf")

            '' obtain the DEM layer
            lTerrain = CType(GIS.Get("w001001"), TGIS_LayerPixel)
            lTerrain.Params.Pixel.AltitudeMapZones.Clear()

            '' create a layer for storing the observer locations
            lObservers = New TGIS_LayerVector()
            lObservers.Name = "Observers"
            lObservers.CS = lTerrain.CS
            lObservers.Open()

            '' add a symbol to represent observers
            lObservers.Params.Marker.Symbol =
              TGIS_Utils.SymbolList.Prepare("LIBSVG:std:TowerCommunication01")
            lObservers.Params.Marker.Color = TGIS_Color.White
            lObservers.Params.Marker.OutlineColor = TGIS_Color.White
            lObservers.Params.Marker.Size = -32

            GIS.Add(lObservers)
            GIS.Unlock()
            GIS.FullExtent()
        End Sub

        Private Sub GIS_MouseDown(sender As Object, e As MouseEventArgs) Handles GIS.MouseDown
            Dim pt As TGIS_Point
            Dim shp As TGIS_Shape
            Dim vs As TGIS_Viewshed
            Dim elev As Single

            '' read observer elevation offset
            If GIS.Mode = TGIS_ViewerMode.UserDefined Then
                Try
                    elev = CType(TatukGIS.RTL.__Global.DotStrToFloat(edtObserverElevation.Text), Single)
                Catch ex As Exception
                    MessageBox.Show("'" + edtObserverElevation.Text + "'' is not a valid floating point value.")
                    Return
                End Try

                GIS.Lock()
                Try
                    '' check if the point lays within the DEM area
                    pt = GIS.ScreenToMap(New Point(e.X, e.Y))
                    If Not TGIS_Utils.GisIsPointInsideExtent(pt, lTerrain.Extent) Then
                        Return
                    End If

                    '' add observer to the observer layer
                    shp = lObservers.CreateShape(TGIS_ShapeType.Point, TGIS_DimensionType.XY)
                    shp.AddPart()
                    shp.AddPoint(pt)

                    '' remove previous viewshed/AGL layers
                    If GIS.Get(SAMPLE_VIEWSHED_NAME) IsNot Nothing Then
                        GIS.Delete(lAGL.Name)
                        lAGL = Nothing
                        GIS.Delete(lViewshed.Name)
                        lViewshed = Nothing
                    End If

                    '' create And set up the layer to host viewshed
                    lViewshed = New TGIS_LayerPixel()
                    lViewshed.Build(True, lTerrain.CS, lTerrain.Extent, lTerrain.BitWidth, lTerrain.BitHeight)
                    lViewshed.Name = SAMPLE_VIEWSHED_NAME
                    lViewshed.Open()

                    '' create And set up the layer to host above-ground-level
                    lAGL = New TGIS_LayerPixel()
                    lAGL.Build(True, lTerrain.CS, lTerrain.Extent, lTerrain.BitWidth, lTerrain.BitHeight)
                    lAGL.Name = SAMPLE_AGL_NAME
                    lAGL.Open()

                    '' create viewshed tool
                    vs = New TGIS_Viewshed()
                    '' set the base observer elevation to be read from the DEM layer
                    vs.ObserverElevation = TGIS_ViewshedObserverElevation.OnDem
                    '' turn on the correction for earth curvature And refractivity
                    vs.CurvedEarth = True

                    vs.Generate(lTerrain, lObservers, lViewshed, lAGL, 0.0F, "", elev)

                    lViewshed.Active = Not rbtnAGL.Checked
                    lAGL.Active = rbtnAGL.Checked

                    GIS.Add(lAGL)
                    GIS.Add(lViewshed)
                    lAGL.Transparency = 50
                    lViewshed.Transparency = 50
                    lObservers.Move(-2)

                    '' apply (binary Or frequency) color ramp to the viewshed layer
                    makeViewshedRamp()

                    '' apply color ramp to the AGL layer
                    lAGL.Params.Pixel.GridShadow = False
                    lAGL.GenerateRamp(
                      TGIS_Color.FromARGB(127, 0, 255, 0),
                      TGIS_Color.None,
                      TGIS_Color.FromARGB(127, 255, 0, 0),
                      0, 1,
                      lAGL.MaxHeight, False,
                      (lAGL.MaxHeight - lAGL.MinHeight) / 100,
                      (lAGL.MaxHeight - lAGL.MinHeight) / 10,
                      Nothing, False
                    )

                    GIS.InvalidateWholeMap()
                Finally
                    GIS.Unlock()
                End Try
                showComment()
            End If
        End Sub

        Private Sub btnFullExtent_Click(sender As Object, e As EventArgs) Handles btnFullExtent.Click
            GIS.FullExtent()
        End Sub

        Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
            GIS.Lock()
            If GIS.Get(SAMPLE_VIEWSHED_NAME) IsNot Nothing Then
                GIS.Delete(lAGL.Name)
                lAGL = Nothing
                GIS.Delete(lViewshed.Name)
                lViewshed = Nothing
            End If
            lObservers.RevertAll()
            GIS.FullExtent()
            GIS.Unlock()
        End Sub

        Private Sub rbtnZoom_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnZoom.CheckedChanged
            GIS.Mode = TGIS_ViewerMode.Zoom
        End Sub

        Private Sub rbtnAddObserver_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnAddObserver.CheckedChanged
            GIS.Mode = TGIS_ViewerMode.UserDefined
        End Sub

        Private Sub rbtnViewshedBinary_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnViewshedBinary.CheckedChanged
            setLayerActive()
        End Sub

        Private Sub rbtnViewshedColor_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnViewshedFreq.CheckedChanged
            setLayerActive()
        End Sub

        Private Sub rbtnAGL_CheckedChanged(sender As Object, e As EventArgs) Handles rbtnAGL.CheckedChanged
            setLayerActive()
        End Sub

        Private Sub GIS_MouseMove(sender As Object, e As MouseEventArgs) Handles GIS.MouseMove
            Dim ptg As TGIS_Point
            Dim cl As TGIS_Color
            Dim vals As Double()
            Dim transp As Boolean
            Dim str As String

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))

            str = ""

            If lViewshed IsNot Nothing Then
                If lViewshed.Locate(ptg, cl, vals, transp) Then
                    If vals(0) <> lViewshed.NoDataValue Then
                        str = str + "Frequency: " + vals(0).ToString 
                    End If
                End If
            End If
            If lAGL IsNot Nothing Then
                If lAGL.Locate(ptg, cl, vals, transp) Then
                    If vals(0) <> lAGL.NoDataValue Then
                        str = str + "Above-Ground-Level: " + vals(0).ToString
                    End If
                End If
            End If
            statusStrip.Text = str
        End Sub
    End Class
End Namespace
