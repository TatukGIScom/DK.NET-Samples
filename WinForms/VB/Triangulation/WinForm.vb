Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Triangulation

    Public Class frmMain
        Inherits System.Windows.Forms.Form

        Friend WithEvents StatusBar1 As System.Windows.Forms.StatusStrip
        Friend WithEvents Panel1 As System.Windows.Forms.Panel
        Friend WithEvents imglst As System.Windows.Forms.ImageList
        Friend WithEvents tlbr As System.Windows.Forms.ToolStrip
        Friend WithEvents btnFullExtent As System.Windows.Forms.ToolStripButton
        Friend WithEvents btnZoomIn As System.Windows.Forms.ToolStripButton
        Friend WithEvents btnZoomOut As System.Windows.Forms.ToolStripButton
        Friend WithEvents GIS_Attributes As TatukGIS.NDK.WinForms.TGIS_ControlAttributes
        Friend WithEvents grpbxResult As System.Windows.Forms.GroupBox
        Friend WithEvents edtLayer As System.Windows.Forms.TextBox
        Friend WithEvents lblLayer As System.Windows.Forms.Label
        Friend WithEvents rbtnDelaunay As System.Windows.Forms.RadioButton
        Friend WithEvents rbtnVoronoi As System.Windows.Forms.RadioButton
        Friend WithEvents GIS_Legend As TatukGIS.NDK.WinForms.TGIS_ControlLegend
        Friend WithEvents btnGenerate As System.Windows.Forms.Button
        Friend WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

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

        'Form overrides dispose to clean up the component list.
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Me.StatusBar1 = New System.Windows.Forms.StatusStrip()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.tlbr = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolStripButton()
            Me.imglst = New System.Windows.Forms.ImageList(Me.components)
            Me.GIS_Attributes = New TatukGIS.NDK.WinForms.TGIS_ControlAttributes()
            Me.grpbxResult = New System.Windows.Forms.GroupBox()
            Me.edtLayer = New System.Windows.Forms.TextBox()
            Me.lblLayer = New System.Windows.Forms.Label()
            Me.rbtnDelaunay = New System.Windows.Forms.RadioButton()
            Me.rbtnVoronoi = New System.Windows.Forms.RadioButton()
            Me.GIS_Legend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.btnGenerate = New System.Windows.Forms.Button()
            Me.Panel1.SuspendLayout()
            Me.grpbxResult.SuspendLayout()
            Me.SuspendLayout()
            '
            'StatusBar1
            '
            Me.StatusBar1.Location = New System.Drawing.Point(0, 382)
            Me.StatusBar1.Name = "StatusBar1"
            Me.StatusBar1.Size = New System.Drawing.Size(584, 22)
            Me.StatusBar1.TabIndex = 0
            '
            'Panel1
            '
            Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Panel1.Controls.Add(Me.tlbr)
            Me.Panel1.Location = New System.Drawing.Point(0, 0)
            Me.Panel1.Name = "Panel1"
            Me.Panel1.Size = New System.Drawing.Size(584, 26)
            Me.Panel1.TabIndex = 1
            '
            'tlbr
            '
            Me.tlbr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.tlbr.Appearance = System.Windows.Forms.ToolStripAppearance.Flat
            Me.tlbr.AutoSize = False
            Me.tlbr.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut})
            Me.tlbr.Dock = System.Windows.Forms.DockStyle.None
            Me.tlbr.DropDownArrows = True
            Me.tlbr.ImageList = Me.imglst
            Me.tlbr.Location = New System.Drawing.Point(0, 0)
            Me.tlbr.Name = "tlbr"
            Me.tlbr.ShowItemToolTips = True
            Me.tlbr.Size = New System.Drawing.Size(584, 26)
            Me.tlbr.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'btnZoomIn
            '
            Me.btnZoomIn.ImageIndex = 1
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.ToolTipText = "Zoom In"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "Zoom Out"
            '
            'imglst
            '
            Me.imglst.ImageStream = CType(resources.GetObject("imglst.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imglst.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imglst.Images.SetKeyName(0, "FullExtent.bmp")
            Me.imglst.Images.SetKeyName(1, "ZoomIn.bmp")
            Me.imglst.Images.SetKeyName(2, "ZoomOut.bmp")
            '
            'GIS_Attributes
            '
            Me.GIS_Attributes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS_Attributes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
            Me.GIS_Attributes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
            Me.GIS_Attributes.Location = New System.Drawing.Point(424, 28)
            Me.GIS_Attributes.Name = "GIS_Attributes"
            Me.GIS_Attributes.Size = New System.Drawing.Size(160, 141)
            Me.GIS_Attributes.TabIndex = 2
            '
            'grpbxResult
            '
            Me.grpbxResult.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.grpbxResult.Controls.Add(Me.edtLayer)
            Me.grpbxResult.Controls.Add(Me.lblLayer)
            Me.grpbxResult.Controls.Add(Me.rbtnDelaunay)
            Me.grpbxResult.Controls.Add(Me.rbtnVoronoi)
            Me.grpbxResult.Location = New System.Drawing.Point(424, 175)
            Me.grpbxResult.Name = "grpbxResult"
            Me.grpbxResult.Size = New System.Drawing.Size(160, 92)
            Me.grpbxResult.TabIndex = 3
            Me.grpbxResult.TabStop = False
            Me.grpbxResult.Text = "Result"
            '
            'edtLayer
            '
            Me.edtLayer.Location = New System.Drawing.Point(81, 65)
            Me.edtLayer.Name = "edtLayer"
            Me.edtLayer.Size = New System.Drawing.Size(73, 20)
            Me.edtLayer.TabIndex = 3
            Me.edtLayer.Text = "Voronoi"
            '
            'lblLayer
            '
            Me.lblLayer.AutoSize = True
            Me.lblLayer.Location = New System.Drawing.Point(6, 68)
            Me.lblLayer.Name = "lblLayer"
            Me.lblLayer.Size = New System.Drawing.Size(68, 13)
            Me.lblLayer.TabIndex = 2
            Me.lblLayer.Text = "Layer name :"
            '
            'rbtnDelaunay
            '
            Me.rbtnDelaunay.AutoSize = True
            Me.rbtnDelaunay.Location = New System.Drawing.Point(6, 42)
            Me.rbtnDelaunay.Name = "rbtnDelaunay"
            Me.rbtnDelaunay.Size = New System.Drawing.Size(134, 17)
            Me.rbtnDelaunay.TabIndex = 1
            Me.rbtnDelaunay.Text = "Delaunay Triangulation"
            Me.rbtnDelaunay.UseVisualStyleBackColor = True
            '
            'rbtnVoronoi
            '
            Me.rbtnVoronoi.AutoSize = True
            Me.rbtnVoronoi.Checked = True
            Me.rbtnVoronoi.Location = New System.Drawing.Point(6, 19)
            Me.rbtnVoronoi.Name = "rbtnVoronoi"
            Me.rbtnVoronoi.Size = New System.Drawing.Size(103, 17)
            Me.rbtnVoronoi.TabIndex = 0
            Me.rbtnVoronoi.TabStop = True
            Me.rbtnVoronoi.Text = "Voronoi Diagram"
            Me.rbtnVoronoi.UseVisualStyleBackColor = True
            '
            'GIS_Legend
            '
            Me.GIS_Legend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GIS_Legend.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GIS_Legend.GIS_Group = Nothing
            Me.GIS_Legend.GIS_Layer = Nothing
            Me.GIS_Legend.GIS_Viewer = Me.GIS
            Me.GIS_Legend.Location = New System.Drawing.Point(424, 302)
            Me.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GIS_Legend.Name = "GIS_Legend"
            Me.GIS_Legend.Options = CType((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.GIS_Legend.ReverseOrder = True
            Me.GIS_Legend.Size = New System.Drawing.Size(160, 80)
            Me.GIS_Legend.TabIndex = 4
            '
            'GIS
            '
            Me.GIS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Location = New System.Drawing.Point(0, 28)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(424, 354)
            Me.GIS.TabIndex = 6
            '
            'btnGenerate
            '
            Me.btnGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.btnGenerate.Location = New System.Drawing.Point(430, 273)
            Me.btnGenerate.Name = "btnGenerate"
            Me.btnGenerate.Size = New System.Drawing.Size(148, 23)
            Me.btnGenerate.TabIndex = 5
            Me.btnGenerate.Text = "Generate"
            Me.btnGenerate.UseVisualStyleBackColor = True
            '
            'frmMain
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(584, 404)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.btnGenerate)
            Me.Controls.Add(Me.GIS_Legend)
            Me.Controls.Add(Me.grpbxResult)
            Me.Controls.Add(Me.GIS_Attributes)
            Me.Controls.Add(Me.Panel1)
            Me.Controls.Add(Me.StatusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "frmMain"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Triangulation"
            Me.Panel1.ResumeLayout(False)
            Me.grpbxResult.ResumeLayout(False)
            Me.grpbxResult.PerformLayout()
            Me.ResumeLayout(False)

        End Sub
#End Region

        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New frmMain())
        End Sub

        Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim lv As TGIS_LayerVector

            ' open a file
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\Poland\DCW\city.shp")

            ' and add a new parametr
            lv = CType(GIS.Items(0), TGIS_LayerVector)
            lv.Params.Marker.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&H4080FF).A, ColorTranslator.FromWin32(&H4080FF).R, ColorTranslator.FromWin32(&H4080FF).G, ColorTranslator.FromWin32(&H4080FF).B)
            lv.Params.Marker.OutlineWidth = 2
            lv.Params.Marker.Style = TGIS_MarkerStyle.Circle

            lv.ParamsList.Add()
            lv.Params.Style = "selected"
            lv.Params.Area.OutlineWidth = 1
            lv.Params.Area.Color = TGIS_Color.Blue

            GIS_Legend.Update()
        End Sub

        Private Sub tlbr_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlbr.ItemClicked
            Select Case tlbr.Items.IndexOf(e.ClickedItem)
                Case 0
                    GIS.FullExtent()
                Case 1
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    GIS.Zoom = GIS.Zoom / 2
            End Select
        End Sub

        Private Sub rbtnVoronoi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnVoronoi.Click
            edtLayer.Text = "Voronoi"
        End Sub

        Private Sub rbtnDelaunay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelaunay.Click
            edtLayer.Text = "Delaunay"
        End Sub

        Private Sub GIS_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseDown
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                Exit Sub
            End If
            If GIS.InPaint Then
                Return
            End If

            ' let's locate a shape after click
            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
            If Not shp Is Nothing Then
                GIS_Attributes.ShowShape(shp)
            End If
        End Sub

        Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
            Dim lv As TGIS_LayerVector

            If Not GIS.Get(edtLayer.Text) Is Nothing Then
                MessageBox.Show("Result layer already exists. Use different name.")
                Exit Sub
            End If

            If rbtnVoronoi.Checked Then
                lv = New TGIS_LayerVoronoi()
            Else
                lv = New TGIS_LayerDelaunay()
            End If

            lv.Name = edtLayer.Text
            lv.ImportLayer(CType(GIS.Items(0), TGIS_LayerVector), GIS.Extent, TGIS_ShapeType.Unknown, "", False)
            lv.Transparency = 60

            lv.Params.Render.Expression = "GIS_AREA"
            lv.Params.Render.MinVal = 10000000
            lv.Params.Render.MaxVal = 1300000000
            lv.Params.Render.StartColor = TGIS_Color.White
            If rbtnVoronoi.Checked Then
                lv.Params.Render.EndColor = TGIS_Color.Red
            Else
                lv.Params.Render.EndColor = TGIS_Color.Blue
            End If

            lv.Params.Render.Zones = 10
            lv.Params.Area.Color = TGIS_Color.RenderColor
            lv.CS = GIS.CS

            GIS.Add(lv)
            GIS.InvalidateWholeMap()
            GIS_Legend.Invalidate()
        End Sub
    End Class
End Namespace
