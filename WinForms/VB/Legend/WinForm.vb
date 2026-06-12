' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' Legend Sample - VB.NET WinForms (.NET)
'
' Demonstrates how to use the TGIS_ControlLegend panel alongside a map viewer.
' The legend control provides an interactive, dockable layer list that lets the
' user toggle layer visibility, reorder layers, expand/collapse layer symbology,
' and open layer property dialogs - all without writing any extra code.
'
' Key concepts shown:
'   - Placing TGIS_ControlLegend next to TGIS_ViewerWnd and linking them via
'     GIS_Viewer so the legend reflects the loaded map automatically.
'   - Switching between two display modes at runtime:
'       * TGIS_ControlLegendMode.Layers - flat list of every individual layer
'       * TGIS_ControlLegendMode.Groups - tree grouped by layer group membership
'   - Opening a .ttkproject file that bundles multiple SHP layers together.
'   - Controlling the viewer interaction mode (Zoom / Drag).
'   - Persisting any layer-style changes back to the project file with SaveAll.
'   - Reflecting the current map scale in the status bar after each repaint.
' =============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace Legend
    ' Legend sample — demonstrates interactive layer list and legend management.
    '
    ' What the sample shows:
    '   - Placing TGIS_ControlLegend next to TGIS_ViewerWnd with legend-to-viewer linking
    '   - Interactive layer visibility toggling via legend checkboxes
    '   - Reordering layers via drag-and-drop in legend panel
    '   - Expanding/collapsing layer symbology details
    '   - Opening layer property dialogs for advanced configuration
    '   - Switching between two display modes at runtime (Layers vs. Groups)
    '   - Loading .ttkproject files with multiple pre-configured layers
    '   - Persisting layer-style changes back to project file via SaveAll
    '   - Real-time status bar updates showing current map scale
    '
    ' Key TatukGIS API concepts shown here:
    '   TGIS_ViewerWnd              - main visual map control
    '   TGIS_ControlLegend          - interactive legend/layer list panel
    '   TGIS_ControlLegendMode      - display modes (Layers, Groups, etc.)
    '   GIS.Items                   - layer collection (get count, access by index)
    '   TGIS_Params                 - layer styling and rendering parameters
    '   GIS.VisibleExtent           - geographic extent of loaded layers
    '   GIS.Scale                   - map scale for display in status bar
    ''' <summary>
    ''' Main application form for the Legend sample.
    ''' Hosts a TGIS_ViewerWnd (map canvas) and a TGIS_ControlLegend panel
    ''' side-by-side, separated by a resizable splitter.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer

        ' --- UI controls (wired in InitializeComponent) ---
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip   ' Primary toolbar
        Private btnFullExtent As System.Windows.Forms.ToolStripButton   ' Zooms to full extent of all loaded layers
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton  ' Separator
        Private toolBarButton2 As System.Windows.Forms.ToolStripButton  ' Separator
        Private panel3 As System.Windows.Forms.Panel                    ' Container for the save toolbar
        Private WithEvents toolBar2 As System.Windows.Forms.ToolStrip   ' Secondary toolbar (Save config)
        Private btnZoom As System.Windows.Forms.ToolStripButton         ' Switches viewer to Zoom interaction mode
        Private btnDrag As System.Windows.Forms.ToolStripButton         ' Switches viewer to Drag/Pan interaction mode
        Private imageList1 As System.Windows.Forms.ImageList            ' Toolbar button icons
        Private statusBar1 As System.Windows.Forms.StatusStrip          ' Status bar at the bottom
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel  ' "Scale :" caption
        Private statusBarPanel2 As System.Windows.Forms.ToolStripStatusLabel  ' Current scale value
        Private panel1 As System.Windows.Forms.Panel                    ' Top toolbar container panel
        Private panel2 As System.Windows.Forms.Panel                    ' Left sub-panel for navigation toolbar
        Private toolBarButton3 As System.Windows.Forms.ToolStripButton  ' Save configuration button

        ''' <summary>
        ''' The interactive legend panel.
        ''' Linked to the GIS viewer via GIS_Viewer so it automatically lists all
        ''' layers loaded in the map. Users can toggle visibility, drag layers to
        ''' reorder them, expand nodes to inspect symbology, and double-click a
        ''' layer to open its properties dialog.
        ''' </summary>
        Private GIS_ControlLegend1 As TatukGIS.NDK.WinForms.TGIS_ControlLegend

        Private splitter1 As System.Windows.Forms.Splitter  ' Resizable divider between legend and map
        Friend WithEvents btnGroups As System.Windows.Forms.Button   ' Switches legend to Groups display mode
        Friend WithEvents btnLayers As System.Windows.Forms.Button   ' Switches legend to Layers display mode

        ''' <summary>
        ''' The main interactive map canvas (TGIS_ViewerWnd).
        ''' Renders all loaded layers, handles mouse-driven zooming and panning,
        ''' and fires AfterPaintEvent on each completed repaint.
        ''' </summary>
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

        ''' <summary>
        ''' Initialises the form, runs the designer-generated component wiring,
        ''' and ensures the map canvas receives keyboard focus on startup.
        ''' </summary>
        Public Sub New()
            ' Set up all controls, layouts and event subscriptions defined by the designer
            InitializeComponent()

            ' Give the map canvas initial keyboard focus so arrow-key panning works immediately
            Me.ActiveControl = GIS
        End Sub

        ''' <summary>
        ''' Releases managed resources when the form is closed.
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
            Dim TgiS_ControlLegendDialogOptions1 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.btnZoom = New System.Windows.Forms.ToolStripButton()
            Me.btnDrag = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton2 = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.statusBarPanel2 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.toolBar2 = New System.Windows.Forms.ToolStrip()
            Me.toolBarButton3 = New System.Windows.Forms.ToolStripButton()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.GIS_ControlLegend1 = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.splitter1 = New System.Windows.Forms.Splitter()
            Me.btnLayers = New System.Windows.Forms.Button()
            Me.btnGroups = New System.Windows.Forms.Button()


            Me.panel1.SuspendLayout()
            Me.panel3.SuspendLayout()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'toolBar1
            '

            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.toolBarButton1, Me.btnZoom, Me.btnDrag, Me.toolBarButton2})

            Me.toolBar1.Dock = System.Windows.Forms.DockStyle.None

            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(85, 28)
            Me.toolBar1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"

            '
            'btnZoom
            '
            Me.btnZoom.ImageIndex = 1
            Me.btnZoom.Name = "btnZoom"

            Me.btnZoom.ToolTipText = "Zoom Mode"
            '
            'btnDrag
            '
            Me.btnDrag.ImageIndex = 2
            Me.btnDrag.Name = "btnDrag"

            Me.btnDrag.ToolTipText = "Drag Mode"
            '
            'toolBarButton2
            '
            Me.toolBarButton2.Name = "toolBarButton2"

            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            Me.imageList1.Images.SetKeyName(3, "")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1, Me.statusBarPanel2})

            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 2
            '
            'statusBarPanel1
            '


            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Scale :"
            Me.statusBarPanel1.Width = 50
            '
            'statusBarPanel2
            '
            Me.statusBarPanel2.AutoSize = True
            Me.statusBarPanel2.Name = "statusBarPanel2"
            Me.statusBarPanel2.Width = 525
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 28)
            Me.panel1.TabIndex = 5
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel3.Location = New System.Drawing.Point(85, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(507, 28)
            Me.panel3.TabIndex = 1
            '
            'toolBar2
            '

            Me.toolBar2.AutoSize = False
            Me.toolBar2.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.toolBarButton3})


            Me.toolBar2.ImageList = Me.imageList1
            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowItemToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(507, 42)
            Me.toolBar2.TabIndex = 0

            '
            'toolBarButton3
            '
            Me.toolBarButton3.ImageIndex = 3
            Me.toolBarButton3.Name = "toolBarButton3"
            Me.toolBarButton3.Text = "Save config"
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(85, 28)
            Me.panel2.TabIndex = 0
            '
            'GIS_ControlLegend1
            '
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384
            Me.GIS_ControlLegend1.DialogOptions = TgiS_ControlLegendDialogOptions1
            Me.GIS_ControlLegend1.Dock = System.Windows.Forms.DockStyle.Left
            Me.GIS_ControlLegend1.GIS_Group = Nothing
            Me.GIS_ControlLegend1.GIS_Layer = Nothing
            Me.GIS_ControlLegend1.GIS_Viewer = Me.GIS
            Me.GIS_ControlLegend1.Location = New System.Drawing.Point(0, 28)
            Me.GIS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.GIS_ControlLegend1.Name = "GIS_ControlLegend1"
            ' AllowMove: user can drag layers to reorder them in the legend
            ' AllowActive: clicking a layer makes it the active (editable) layer
            ' AllowExpand: layer nodes can be expanded to show symbology classes
            ' AllowParams: double-clicking opens the layer properties dialog
            ' AllowSelect: layers can be selected (highlighted) in the legend
            Me.GIS_ControlLegend1.Options = CType(((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect), TatukGIS.NDK.TGIS_ControlLegendOption)
            ' ReverseOrder=True so the top layer in the legend matches the top
            ' rendering layer (visually topmost on the map)
            Me.GIS_ControlLegend1.ReverseOrder = True
            Me.GIS_ControlLegend1.Size = New System.Drawing.Size(145, 419)
            Me.GIS_ControlLegend1.TabIndex = 6
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.Control
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.IncrementalPaint = False
            Me.GIS.Location = New System.Drawing.Point(148, 28)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(444, 419)
            Me.GIS.TabIndex = 8
            '
            'splitter1
            '
            Me.splitter1.Location = New System.Drawing.Point(145, 28)
            Me.splitter1.Name = "splitter1"
            Me.splitter1.Size = New System.Drawing.Size(3, 419)
            Me.splitter1.TabIndex = 7
            Me.splitter1.TabStop = False
            '
            'btnLayers
            '
            Me.btnLayers.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnLayers.Location = New System.Drawing.Point(3, 417)
            Me.btnLayers.Name = "btnLayers"
            Me.btnLayers.Size = New System.Drawing.Size(67, 23)
            Me.btnLayers.TabIndex = 0
            Me.btnLayers.Text = "Layers"
            Me.btnLayers.UseVisualStyleBackColor = True
            '
            'btnGroups
            '
            Me.btnGroups.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.btnGroups.Location = New System.Drawing.Point(73, 417)
            Me.btnGroups.Name = "btnGroups"
            Me.btnGroups.Size = New System.Drawing.Size(67, 23)
            Me.btnGroups.TabIndex = 1
            Me.btnGroups.Text = "Groups"
            Me.btnGroups.UseVisualStyleBackColor = True
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.splitter1)
            Me.Controls.Add(Me.btnGroups)
            Me.Controls.Add(Me.btnLayers)
            Me.Controls.Add(Me.GIS_ControlLegend1)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Legend"


            Me.panel1.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' Application entry point.
        ''' Configures high-DPI and visual styles before launching the form.
        ''' </summary>
        <STAThread()>
        Shared Sub Main()
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Loads the sample Poland multi-layer project when the form first appears.
        ''' Opening a .ttkproject causes TGIS_ViewerWnd to load all referenced layer
        ''' files. Because GIS_ControlLegend1.GIS_Viewer is already set to GIS, the
        ''' legend populates itself automatically as each layer is registered.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' GisSamplesDataDirDownload() resolves the path to the shared sample data
            ' directory (configurable via the TatukGIS environment or registry).
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\Poland\DCW\poland.ttkproject")
        End Sub

        ''' <summary>
        ''' Handles clicks on the primary navigation toolbar.
        ''' Uses the index of the clicked item to determine the requested action.
        ''' Index 0 = Full Extent, 2 = Zoom mode, 3 = Drag mode
        ''' (Index 1 is a separator and is never clicked.)
        ''' </summary>
        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' Zoom out to fit the complete bounding box of all visible layers
                    GIS.FullExtent()
                Case 2
                    ' TGIS_ViewerMode.Zoom: left-drag to zoom in, right-click to zoom out
                    GIS.Mode = TGIS_ViewerMode.Zoom

                Case 3
                    ' TGIS_ViewerMode.Drag: click-drag to pan the visible map extent
                    GIS.Mode = TGIS_ViewerMode.Drag

            End Select
        End Sub

        ''' <summary>
        ''' Changes the primary toolbar cursor to a hand pointer when hovering over
        ''' active buttons, providing visual affordance feedback.
        ''' </summary>
        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Items(0).Bounds.Contains(p) OrElse toolBar1.Items(2).Bounds.Contains(p) OrElse toolBar1.Items(3).Bounds.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub

        ''' <summary>
        ''' Saves all layer configuration changes (symbology, visibility, order) back
        ''' to the originating project or layer files when the "Save config" button
        ''' is clicked. Guards against calling SaveAll on an empty viewer.
        ''' </summary>
        Private Sub toolBar2_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar2.ItemClicked
            If GIS.IsEmpty Then   ' Nothing loaded - nothing to save
                Return
            End If
            ' Persist any legend changes (visibility flags, symbology edits, layer order)
            ' back to the .ttkproject file and its referenced layer files.
            GIS.SaveAll()
        End Sub

        ''' <summary>
        ''' Changes the secondary toolbar cursor to a hand pointer when hovering
        ''' over the save button, providing visual affordance feedback.
        ''' </summary>
        Private Sub toolBar2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar2.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar2.Items(0).Bounds.Contains(p) Then
                toolBar2.Cursor = Cursors.Hand
            Else
                toolBar2.Cursor = Cursors.Default
            End If
        End Sub

        ''' <summary>
        ''' Fired by TGIS_ViewerWnd after every completed map repaint.
        ''' Updates the status bar with the human-readable map scale string
        ''' (e.g. "1 : 250 000") so the user always knows the current zoom level.
        ''' </summary>
        Private Sub GIS_AfterPaint(ByVal sender As Object, ByVal e As TGIS_PaintEventArgs) Handles GIS.AfterPaintEvent
            ' ScaleAsText returns a formatted "1 : N" string for the current viewport
            statusBar1.Items(1).Text = GIS.ScaleAsText
        End Sub

        ''' <summary>
        ''' Switches the legend to flat Layers mode.
        ''' In Layers mode every individual layer appears as a separate top-level
        ''' item in the legend list, regardless of any group hierarchy.
        ''' </summary>
        Private Sub btnLayers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLayers.Click
            GIS_ControlLegend1.Mode = TGIS_ControlLegendMode.Layers
        End Sub

        ''' <summary>
        ''' Switches the legend to Groups mode.
        ''' In Groups mode layers are nested inside their parent group nodes,
        ''' matching the logical structure defined in the project file.
        ''' </summary>
        Private Sub btnGroups_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroups.Click
            GIS_ControlLegend1.Mode = TGIS_ControlLegendMode.Groups
        End Sub
    End Class
End Namespace
