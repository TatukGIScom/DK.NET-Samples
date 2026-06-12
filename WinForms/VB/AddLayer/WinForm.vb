'=============================================================================
' This source code is a part of TatukGIS Developer Kernel.
'=============================================================================
' AddLayer Sample - Demonstrates how to programmatically add vector layers
' to a TatukGIS map viewer.
'
' Key concepts illustrated:
'   - Creating a TGIS_LayerSHP instance directly (manual construction) and
'     adding it to the viewer via GIS.Add.
'   - Using TGIS_Utils.GisCreateLayer as a convenience factory that resolves
'     the correct layer class from the file extension automatically.
'   - Setting visual rendering parameters on a layer (area fill colour, line
'     width, line outline width, line colour) through the Params property tree.
'   - Suppressing automatic .ttkgp config-file loading with UseConfig = False
'     so that the layer always starts with the explicitly assigned params.
'   - Fitting the viewport to all loaded layers with GIS.FullExtent().
'   - Switching the viewer interaction mode between Drag (pan) and Select.
'   - Zooming programmatically by multiplying or dividing the current Zoom value.
'
' Data: DCW (Digital Chart of the World) Shapefiles for Poland, supplied
' via the TatukGIS sample data directory.
'=============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK             ' Core TatukGIS types: TGIS_LayerSHP, TGIS_Color, TGIS_Utils, TGIS_ViewerMode
Imports TatukGIS.NDK.WinForms    ' WinForms-specific viewer control: TGIS_ViewerWnd

Namespace AddLayer
    ''' <summary>
    ''' AddLayer sample — demonstrates how to programmatically add vector layers to a GIS viewer.
    ''' Creates shapefile layers (TGIS_LayerSHP) for country polygons and rivers polylines, sets visual
    ''' styling parameters (fill color, line width, line color), and adds them to the viewer using GIS.Add().
    ''' Provides zoom navigation and interaction mode switching (pan/select).
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ' -------------------------------------------------------------------------
        ' Designer-managed fields – layout is configured in InitializeComponent().
        ' -------------------------------------------------------------------------
        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private imageList1 As System.Windows.Forms.ImageList
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private btnZoomIn As System.Windows.Forms.ToolStripButton
        Private btnZoomOut As System.Windows.Forms.ToolStripButton
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton
        ''' <summary>Checkbox that toggles between Drag (pan) and Select interaction modes.</summary>
        Private WithEvents chkDrag As System.Windows.Forms.CheckBox
        Private toolTip1 As System.Windows.Forms.ToolTip
        ''' <summary>The TatukGIS map viewer control. All layers are added to this component.</summary>
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private panel1 As System.Windows.Forms.Panel

        ''' <summary>
        ''' Initialises the form, configures the tooltip on the drag checkbox,
        ''' and sets keyboard focus to the GIS viewer.
        ''' </summary>
        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            ' Attach a short usage hint to the checkbox control.
            Me.toolTip1.SetToolTip(Me.chkDrag, "Drag mode ON/OFF")
            ' Give keyboard focus to the viewer so scroll-wheel zoom works immediately.
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
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.chkDrag = New System.Windows.Forms.CheckBox()
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            '
            'toolBar1
            '
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut, Me.toolBarButton1})
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(592, 24)
            Me.toolBar1.TabIndex = 2
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
            Me.btnZoomIn.ToolTipText = "ZoomIn"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "ZoomOut"
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            '
            'chkDrag
            '
            Me.chkDrag.Location = New System.Drawing.Point(77, 2)
            Me.chkDrag.Name = "chkDrag"
            Me.chkDrag.Size = New System.Drawing.Size(97, 22)
            Me.chkDrag.TabIndex = 5
            Me.chkDrag.Text = "Dragging"
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 423)
            Me.GIS.TabIndex = 0
            Me.GIS.UseRTree = False
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.chkDrag)
            Me.panel1.Controls.Add(Me.toolBar1)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 24)
            Me.panel1.TabIndex = 2
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - AddLayer"

            Me.panel1.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' Application entry point.  Bootstraps the WinForms message loop and
        ''' displays the main form.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
#If NET5_0_OR_GREATER Then
            ' Enable per-monitor DPI awareness on .NET 5+ for sharp rendering on
            ' high-DPI displays.
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Handles the Form.Load event.  Creates and configures two Shapefile
        ''' layers – a country polygon layer and a rivers polyline layer – then
        ''' adds them to the GIS viewer and fits the viewport to their combined
        ''' extent.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_LayerSHP

            ' --- Layer 1: Country outline (polygon / area layer) ---
            ' Construct the layer directly.  TGIS_LayerSHP wraps an ESRI
            ' Shapefile; the geometry type is inferred from the .shp file header.
            ll = New TGIS_LayerSHP()

            ' GisSamplesDataDirDownload() returns the root path where TatukGIS
            ' sample datasets were installed or downloaded.
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\Poland\DCW\country.shp"

            ' A human-readable label used in legends and layer lists.
            ll.Name = "country"

            ' Params.Area.Color sets the solid fill for polygon geometries.
            ' LightGray provides a neutral base so the river overlay is readable.
            ll.Params.Area.Color = TGIS_Color.LightGray

            ' GIS.Add appends the layer to the internal stack.  Layers added
            ' earlier are rendered first (painted at the bottom of the visual stack).
            GIS.Add(ll)

            ' --- Layer 2: Rivers (polyline layer) ---
            ' TGIS_Utils.GisCreateLayer is a factory that inspects the file
            ' extension and instantiates the matching TGIS_Layer subclass.
            ' Casting to TGIS_LayerSHP is safe for .shp files.
            ' The first argument becomes the layer's Name property.
            ll = CType(TGIS_Utils.GisCreateLayer("rivers", TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\Poland\DCW\lwaters.shp"), TGIS_LayerSHP)

            ' UseConfig = False prevents the DK from loading a previously saved
            ' .ttkgp configuration file for this layer, ensuring the rendering
            ' parameters we set below take effect rather than cached values.
            ll.UseConfig = False

            ' OutlineWidth = 0 removes the contrasting halo drawn around lines,
            ' yielding a clean single-colour stroke.
            ll.Params.Line.OutlineWidth = 0

            ' Line.Width is in screen pixels at the reference zoom level.
            ll.Params.Line.Width = 3
            ll.Params.Line.Color = TGIS_Color.Blue

            GIS.Add(ll)

            ' Zoom the viewport to the combined bounding box of all layers so the
            ' full map is visible immediately after load.
            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Handles toolbar item clicks dispatched via ItemClicked.
        ''' Uses the item's index in the toolbar collection to identify which
        ''' button was pressed.
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' Reset the viewport to show all loaded layers at once.
                    GIS.FullExtent()
                Case 1
                    ' Double the zoom level – the visible area shrinks by half.
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    ' Halve the zoom level – the visible area doubles.
                    GIS.Zoom = GIS.Zoom / 2
            End Select
        End Sub

        ''' <summary>
        ''' Toggles the viewer's active interaction mode.
        ''' <para>
        ''' <see cref="TGIS_ViewerMode.Drag"/> – left-click and drag pans the map canvas.
        ''' </para>
        ''' <para>
        ''' <see cref="TGIS_ViewerMode.Select"/> – left-click picks the topmost feature
        ''' under the cursor.
        ''' </para>
        ''' </summary>
        Private Sub chkDrag_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDrag.Click
            If chkDrag.Checked Then
                GIS.Mode = TGIS_ViewerMode.Drag
            Else
                GIS.Mode = TGIS_ViewerMode.Select
            End If
        End Sub

        ''' <summary>
        ''' Changes the toolbar cursor to a hand when hovering over one of the
        ''' three action buttons, providing a standard affordance that the items
        ''' are clickable.
        ''' </summary>
        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            ' Show the hand cursor only while the pointer is over an active button.
            If toolBar1.Items(0).Bounds.Contains(p) OrElse toolBar1.Items(1).Bounds.Contains(p) OrElse toolBar1.Items(2).Bounds.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub
    End Class
End Namespace
