' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' Triangulation Sample – Demonstrates Delaunay triangulation and Voronoi diagrams.
'
' This sample opens a point shapefile (Polish cities) and uses two specialised
' TGIS_LayerVector subclasses from the TatukGIS NDK to compute geometric structures
' derived from the point distribution:
'
'   Delaunay Triangulation
'     Connects input points into triangles such that no point lies inside the
'     circumscribed circle of any triangle.  Produces a TIN (Triangulated
'     Irregular Network) suitable for surface modelling and proximity analysis.
'
'   Voronoi Diagram
'     Partitions the plane into one region per input point, where each region
'     contains all locations closer to that point than to any other input point.
'     The Voronoi diagram is the geometric dual of the Delaunay triangulation.
'
' After generation the result layer is colour-graduated by polygon area
' (GIS_AREA attribute) over a white-to-red (Voronoi) or white-to-blue (Delaunay)
' gradient rendered across 10 equal-interval zones.  This highlights relative
' cell / triangle area variation across the study region.
'
' User interaction:
'   - Click a point on the map to display its attributes in the Attributes panel
'   - Radio buttons select Delaunay vs Voronoi mode before generation
'   - "Generate" adds the result layer; duplicate layer names are rejected
'   - Toolbar buttons provide Full Extent, Zoom In, and Zoom Out navigation
'
' Key TatukGIS NDK classes used:
'   TGIS_LayerDelaunay       - generates and stores a Delaunay triangulation
'   TGIS_LayerVoronoi        - generates and stores a Voronoi diagram
'   TGIS_LayerVector         - base class; ImportLayer() copies source point data
'   TGIS_ControlAttributes   - side panel that shows shape attribute data on click
'   TGIS_ControlLegend       - layer legend / style control panel
'   TGIS_ViewerWnd           - WinForms map viewer control
' =============================================================================

Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Triangulation

    ''' <summary>
    ''' Main form for the Triangulation sample application.
    '''
    ''' Demonstrates Delaunay triangulation and Voronoi diagram generation using
    ''' <see cref="TGIS_LayerDelaunay"/> and <see cref="TGIS_LayerVoronoi"/>.
    ''' The result layer is colour-graduated by polygon area (GIS_AREA attribute)
    ''' to give an immediate visual impression of size variation across the plane.
    ''' </summary>
    Public Class frmMain
        Inherits System.Windows.Forms.Form

        ' --- Designer-declared controls ---
        Friend WithEvents StatusBar1 As System.Windows.Forms.StatusStrip          ' Status bar at the bottom
        Friend WithEvents Panel1 As System.Windows.Forms.Panel                    ' Panel hosting the toolbar
        Friend WithEvents imglst As System.Windows.Forms.ImageList                ' Toolbar button images
        Friend WithEvents tlbr As System.Windows.Forms.ToolStrip                  ' Navigation toolbar
        Friend WithEvents btnFullExtent As System.Windows.Forms.ToolStripButton   ' Zoom to fit all layers
        Friend WithEvents btnZoomIn As System.Windows.Forms.ToolStripButton       ' Double the zoom factor
        Friend WithEvents btnZoomOut As System.Windows.Forms.ToolStripButton      ' Halve the zoom factor
        Friend WithEvents GIS_Attributes As TatukGIS.NDK.WinForms.TGIS_ControlAttributes ' Shape attribute panel
        Friend WithEvents grpbxResult As System.Windows.Forms.GroupBox            ' Groups mode/name controls
        Friend WithEvents edtLayer As System.Windows.Forms.TextBox                ' Editable output layer name
        Friend WithEvents lblLayer As System.Windows.Forms.Label                  ' "Layer name:" label
        Friend WithEvents rbtnDelaunay As System.Windows.Forms.RadioButton        ' Select Delaunay mode
        Friend WithEvents rbtnVoronoi As System.Windows.Forms.RadioButton         ' Select Voronoi mode
        Friend WithEvents GIS_Legend As TatukGIS.NDK.WinForms.TGIS_ControlLegend ' Layer legend panel
        Friend WithEvents btnGenerate As System.Windows.Forms.Button              ' Trigger triangulation/diagram
        Friend WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd            ' Map viewer control

        ''' <summary>Initialises WinForms designer components.</summary>
        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            Me.ActiveControl = GIS
        End Sub

        ''' <summary>Clean up any resources being used.</summary>
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

        ''' <summary>The main entry point for the application.</summary>
        <STAThread>
        Shared Sub Main()
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New frmMain())
        End Sub

        ''' <summary>
        ''' Loads the Polish city point shapefile on form startup and configures
        ''' the city marker style.
        '''
        ''' A second "selected" param set is registered so that shapes shown in the
        ''' Attributes panel receive a distinct blue area fill, making the selected
        ''' shape easy to identify on the map.
        ''' </summary>
        Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim lv As TGIS_LayerVector

            ' Open the Polish city point data – this layer provides the seed
            ' point set for both the Delaunay and Voronoi algorithms.
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\Poland\DCW\city.shp")

            ' Retrieve the freshly loaded vector layer and customise its marker style
            lv = CType(GIS.Items(0), TGIS_LayerVector)
            lv.Params.Marker.Color = TGIS_Color.FromARGB(ColorTranslator.FromWin32(&H4080FF).A, ColorTranslator.FromWin32(&H4080FF).R, ColorTranslator.FromWin32(&H4080FF).G, ColorTranslator.FromWin32(&H4080FF).B)
            lv.Params.Marker.OutlineWidth = 2
            lv.Params.Marker.Style = TGIS_MarkerStyle.Circle

            ' Add a second named param set; shapes displayed in the attribute panel
            ' use the "selected" style so they stand out on the map.
            lv.ParamsList.Add()
            lv.Params.Style = "selected"
            lv.Params.Area.OutlineWidth = 1
            lv.Params.Area.Color = TGIS_Color.Blue

            GIS_Legend.Update()  ' Refresh legend to show the new layer style
        End Sub

        ''' <summary>
        ''' Handles toolbar button clicks for map navigation.
        ''' Dispatches to the appropriate viewer method based on which item was clicked.
        ''' </summary>
        Private Sub tlbr_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlbr.ItemClicked
            Select Case tlbr.Items.IndexOf(e.ClickedItem)
                Case 0
                    GIS.FullExtent()        ' Zoom to fit all layers
                Case 1
                    GIS.Zoom = GIS.Zoom * 2  ' Double zoom factor
                Case 2
                    GIS.Zoom = GIS.Zoom / 2  ' Halve zoom factor
            End Select
        End Sub

        ''' <summary>
        ''' Pre-fills the output layer name when the user selects Voronoi mode.
        ''' </summary>
        Private Sub rbtnVoronoi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnVoronoi.Click
            edtLayer.Text = "Voronoi"
        End Sub

        ''' <summary>
        ''' Pre-fills the output layer name when the user selects Delaunay mode.
        ''' </summary>
        Private Sub rbtnDelaunay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnDelaunay.Click
            edtLayer.Text = "Delaunay"
        End Sub

        ''' <summary>
        ''' Hit-tests the map on mouse press to find the nearest shape and displays
        ''' its attribute data in the GIS_Attributes side panel.
        '''
        ''' ScreenToMap converts pixel coordinates to geographic (map) coordinates.
        ''' Locate searches all layers for the nearest shape within a tolerance of
        ''' 5 screen pixels (converted to map units by dividing by the zoom factor).
        ''' </summary>
        Private Sub GIS_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseDown
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                Exit Sub
            End If
            If GIS.InPaint Then
                Return  ' Avoid reentrancy during a repaint cycle
            End If

            ' Convert screen pixel position to geographic map coordinates
            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            ' Locate the nearest shape within a 5-pixel screen tolerance
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape) ' 5 pixels precision
            If Not shp Is Nothing Then
                GIS_Attributes.ShowShape(shp)  ' Populate the attribute panel
            End If
        End Sub

        ''' <summary>
        ''' Creates either a <see cref="TGIS_LayerVoronoi"/> or
        ''' <see cref="TGIS_LayerDelaunay"/> layer, imports the city point data as
        ''' input, configures a graduated colour render style, and adds the result
        ''' to the viewer.
        '''
        ''' ImportLayer copies all features from the source layer into the new
        ''' triangulation layer and runs the algorithm immediately; after the call
        ''' the layer contains fully formed polygon shapes ready for display.
        '''
        ''' The GIS_AREA attribute (computed automatically by the triangulation engine)
        ''' drives a white-to-colour gradient over 10 equal-interval zones.
        ''' Voronoi uses red as the end colour; Delaunay uses blue.
        ''' </summary>
        Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
            Dim lv As TGIS_LayerVector

            ' Prevent overwriting an existing layer with the same name
            If Not GIS.Get(edtLayer.Text) Is Nothing Then
                MessageBox.Show("Result layer already exists. Use different name.")
                Exit Sub
            End If

            ' Instantiate the correct layer type based on the radio button selection
            If rbtnVoronoi.Checked Then
                lv = New TGIS_LayerVoronoi()   ' Voronoi diagram
            Else
                lv = New TGIS_LayerDelaunay()   ' Delaunay triangulation
            End If

            lv.Name = edtLayer.Text

            ' ImportLayer reads all point features from the source layer and uses them
            ' as the input seed set for the triangulation / diagram computation.
            ' TGIS_ShapeType.Unknown lets the engine infer the output shape type automatically.
            lv.ImportLayer(CType(GIS.Items(0), TGIS_LayerVector), GIS.Extent, TGIS_ShapeType.Unknown, "", False)
            lv.Transparency = 60  ' Semi-transparent so the source city layer shows through

            ' Configure graduated colour rendering keyed on the built-in GIS_AREA attribute.
            ' GIS_AREA holds the area of each output polygon in map coordinate units squared.
            lv.Params.Render.Expression = "GIS_AREA"
            lv.Params.Render.MinVal = 10000000      ' ~10 km² lower bound
            lv.Params.Render.MaxVal = 1300000000    ' ~1300 km² upper bound
            lv.Params.Render.StartColor = TGIS_Color.White
            ' Differentiate Voronoi (red) from Delaunay (blue) visually
            If rbtnVoronoi.Checked Then
                lv.Params.Render.EndColor = TGIS_Color.Red
            Else
                lv.Params.Render.EndColor = TGIS_Color.Blue
            End If

            lv.Params.Render.Zones = 10  ' Divide the colour range into 10 equal steps
            ' TGIS_Color.RenderColor instructs the renderer to substitute the
            ' computed gradient colour for the polygon fill during painting.
            lv.Params.Area.Color = TGIS_Color.RenderColor

            ' Inherit the coordinate system from the source layer so all layers align
            lv.CS = GIS.CS

            GIS.Add(lv)
            GIS.InvalidateWholeMap()   ' Repaint the viewer
            GIS_Legend.Invalidate()    ' Refresh the legend to show the new layer
        End Sub
    End Class
End Namespace
