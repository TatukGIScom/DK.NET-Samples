' =============================================================================
' This source code is a part of TatukGIS Developer Kernel.
' =============================================================================
'
' TopologyLayer Sample - demonstrates topology editing and management in a GIS
' layer (VB.NET WinForms).
'
' What the sample shows:
'   - Loading and managing topologically structured data (line and polygon topology)
'   - Creating topology structures from existing feature layers
'   - Creating and deleting features within topology constraints
'   - Editing topology elements while maintaining integrity
'   - Adding elements to existing topology features
'   - Automatic and manual fixing of topology import errors
'   - Undo/Redo operations during topology editing (also with Ctrl+Z / Ctrl+Y)
'   - Snapping to chosen layers and snap type when adding or editing shapes
'   - Adding sequences of nodes and edges (Ctrl+click continues from the last edge)
'   - Rollback of topology changes
'   - Integration with attribute viewer and layer legend
'   - Toolbar controls for different editing modes (zoom, drag, select, edit)
'   - Zooming with the mouse wheel
'   - Progress tracking during topology operations
'
' Key TatukGIS NDK classes used:
'   TGIS_ViewerWnd           - main map viewer control
'   TGIS_TopoTool            - topology operations manager
'   TGIS_Layer               - base layer class
'   TGIS_Shape               - topology shape objects
'   TGIS_ControlAttributes   - attribute display control
'   TGIS_ControlLegend       - layer legend control
'   TGIS_ViewerMode          - editing modes (Select, Edit, Drag, Zoom)
'   TGIS_EditorSnapType      - snapping targets (vertex, edge, midpoint, ...)
'   BusyEvent                - progress tracking callback
' =============================================================================

Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace TopologyLayer
    ''' <summary>
    ''' Main form for the TopologyLayer sample application.
    ''' Hosts the map viewer together with a legend, an attribute inspector,
    ''' an editing toolbar and a progress panel, and exposes the topology
    ''' workflows provided by <see cref="TGIS_TopoTool"/> through the main menu.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form

        ''' <summary>Required designer variable.</summary>
        Private components As System.ComponentModel.IContainer

        ' --- Main menu ---
        Private mnuMain As System.Windows.Forms.MenuStrip
        Private menuFile As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuAdd As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuOpen As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuOpenLineTopoSample As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuOpenTopoPolygonSample As System.Windows.Forms.ToolStripMenuItem
        Private N1 As System.Windows.Forms.ToolStripSeparator
        Private WithEvents menuSelectVisible As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuSelectAll As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuDeselectAll As System.Windows.Forms.ToolStripMenuItem
        Private N3 As System.Windows.Forms.ToolStripSeparator
        Private WithEvents menuSave As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuClose As System.Windows.Forms.ToolStripMenuItem
        Private N2 As System.Windows.Forms.ToolStripSeparator
        Private WithEvents menuExit As System.Windows.Forms.ToolStripMenuItem
        Private menuTopology As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuTopoRollback As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuTopoSettings As System.Windows.Forms.ToolStripMenuItem
        Private menuTopoSeparatorLayer1 As System.Windows.Forms.ToolStripSeparator
        Private WithEvents menuTopoCreateTopology As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuTopoCreateFeatureLayer As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuTopoDeleteFeatureLayer As System.Windows.Forms.ToolStripMenuItem
        Private menuTopoSeparatorFeature1 As System.Windows.Forms.ToolStripSeparator
        Private WithEvents menuTopoCreateFeature As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuTopoDeleteFeature As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuTopoAddElementsToFeature As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuTopoDeleteFeatureElement As System.Windows.Forms.ToolStripMenuItem
        Private menuTopoSeparatorFix1 As System.Windows.Forms.ToolStripSeparator
        Private WithEvents menuTopoAutoFixImportErrors As System.Windows.Forms.ToolStripMenuItem
        Private WithEvents menuTopoManualFixImportErrors As System.Windows.Forms.ToolStripMenuItem

        ' --- Toolbar ---
        Private lstImage As System.Windows.Forms.ImageList
        Private toolbar As System.Windows.Forms.ToolStrip
        Private WithEvents btnFullExtent As System.Windows.Forms.ToolStripButton
        Private WithEvents btnZoom As System.Windows.Forms.ToolStripButton
        Private WithEvents btnDragMode As System.Windows.Forms.ToolStripButton
        Private WithEvents btnSelectMode As System.Windows.Forms.ToolStripButton
        Private sepEdit As System.Windows.Forms.ToolStripSeparator
        Private WithEvents btnEditMode As System.Windows.Forms.ToolStripButton
        Private WithEvents btnUndo As System.Windows.Forms.ToolStripButton
        Private WithEvents btnRedo As System.Windows.Forms.ToolStripButton
        Private WithEvents btnRevertShape As System.Windows.Forms.ToolStripButton
        Private WithEvents btnDelete As System.Windows.Forms.ToolStripButton
        Private sepAdd As System.Windows.Forms.ToolStripSeparator
        Private WithEvents btnAddShape As System.Windows.Forms.ToolStripButton
        Private lblSnapLayer As System.Windows.Forms.ToolStripLabel
        Private WithEvents btnSnapLayers As System.Windows.Forms.ToolStripDropDownButton
        Private btnSnapType As System.Windows.Forms.ToolStripDropDownButton

        ' --- Legend and attributes ---
        Private pnlGIS As System.Windows.Forms.Panel
        Private WithEvents gisLegend As TatukGIS.NDK.WinForms.TGIS_ControlLegend
        Private gisAttributes As TatukGIS.NDK.WinForms.TGIS_ControlAttributes

        ' --- Map viewer ---
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

        ' --- Progress panel ---
        Private panel1 As System.Windows.Forms.Panel
        Private lblProgress As System.Windows.Forms.Label
        Private progressbar As System.Windows.Forms.ProgressBar
        Private WithEvents btnCancel As System.Windows.Forms.Button

        Private dlgFileOpen As System.Windows.Forms.OpenFileDialog

        ' --- State fields ---

        ''' <summary>
        ''' Set by the Cancel button and passed back to the running operation
        ''' through the BusyEvent to abort it.
        ''' </summary>
        Private abort As Boolean

        ''' <summary>
        ''' Topology tool connected to the viewer, legend and attributes controls.
        ''' Runs all topology workflows (wizards, settings, rollback).
        ''' </summary>
        Private topoTool As TGIS_TopoTool

        ''' <summary>
        ''' Layer to which new shapes are added while the Add Shape mode is on
        ''' (set by the Add Shape button); Nothing otherwise.
        ''' </summary>
        Private editLayer As TGIS_Layer

        ' --- Snapping ---

        ' text of the snap layer list when no layer is checked
        Private Const NO_SNAPPING As String = "No snapping"

        ' index of the first snap type picture in lstImage, in the order of snapTypes
        Private Const SNAP_TYPE_IMAGE As Integer = 10

        ''' <summary>
        ''' Names of the layers checked in the snap layer list and applied to the editor.
        ''' </summary>
        Private snapLayerNames As New List(Of String)()

        ''' <summary>
        ''' Snap types offered in the snap type list, in the order of its items.
        ''' </summary>
        Private Shared ReadOnly snapTypes As TGIS_EditorSnapType() = { _
            TGIS_EditorSnapType.Point, _
            TGIS_EditorSnapType.Line, _
            TGIS_EditorSnapType.PointOverLine, _
            TGIS_EditorSnapType.EndPoint, _
            TGIS_EditorSnapType.Midpoint, _
            TGIS_EditorSnapType.Perpendicular _
        }

        ''' <summary>
        ''' Descriptions of the snap types, in the order of snapTypes.
        ''' </summary>
        Private Shared ReadOnly snapTypeHints As String() = { _
            "Vertex - snap to the nearest vertex of a shape", _
            "Edge - snap to the nearest point on a line segment or polygon edge", _
            "Vertex or edge - snap to a vertex if one is near, otherwise to an edge", _
            "End point - snap to the first or last vertex of a line or polygon", _
            "Midpoint - snap to the middle of a line segment or polygon edge", _
            "Perpendicular - snap to the point of an edge that makes a right angle with the previous segment" _
        }

        ''' <summary>Initialises WinForms designer components.</summary>
        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()
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
            Me.mnuMain = New System.Windows.Forms.MenuStrip()
            Me.menuFile = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuAdd = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuOpen = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuOpenLineTopoSample = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuOpenTopoPolygonSample = New System.Windows.Forms.ToolStripMenuItem()
            Me.N1 = New System.Windows.Forms.ToolStripSeparator()
            Me.menuSelectVisible = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuSelectAll = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuDeselectAll = New System.Windows.Forms.ToolStripMenuItem()
            Me.N3 = New System.Windows.Forms.ToolStripSeparator()
            Me.menuSave = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuClose = New System.Windows.Forms.ToolStripMenuItem()
            Me.N2 = New System.Windows.Forms.ToolStripSeparator()
            Me.menuExit = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopology = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoRollback = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoSettings = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoSeparatorLayer1 = New System.Windows.Forms.ToolStripSeparator()
            Me.menuTopoCreateTopology = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoCreateFeatureLayer = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoDeleteFeatureLayer = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoSeparatorFeature1 = New System.Windows.Forms.ToolStripSeparator()
            Me.menuTopoCreateFeature = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoDeleteFeature = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoAddElementsToFeature = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoDeleteFeatureElement = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoSeparatorFix1 = New System.Windows.Forms.ToolStripSeparator()
            Me.menuTopoAutoFixImportErrors = New System.Windows.Forms.ToolStripMenuItem()
            Me.menuTopoManualFixImportErrors = New System.Windows.Forms.ToolStripMenuItem()
            Me.lstImage = New System.Windows.Forms.ImageList(Me.components)
            Me.toolbar = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoom = New System.Windows.Forms.ToolStripButton()
            Me.btnDragMode = New System.Windows.Forms.ToolStripButton()
            Me.btnSelectMode = New System.Windows.Forms.ToolStripButton()
            Me.sepEdit = New System.Windows.Forms.ToolStripSeparator()
            Me.btnEditMode = New System.Windows.Forms.ToolStripButton()
            Me.btnUndo = New System.Windows.Forms.ToolStripButton()
            Me.btnRedo = New System.Windows.Forms.ToolStripButton()
            Me.btnRevertShape = New System.Windows.Forms.ToolStripButton()
            Me.btnDelete = New System.Windows.Forms.ToolStripButton()
            Me.sepAdd = New System.Windows.Forms.ToolStripSeparator()
            Me.btnAddShape = New System.Windows.Forms.ToolStripButton()
            Me.lblSnapLayer = New System.Windows.Forms.ToolStripLabel()
            Me.btnSnapLayers = New System.Windows.Forms.ToolStripDropDownButton()
            Me.btnSnapType = New System.Windows.Forms.ToolStripDropDownButton()
            Me.pnlGIS = New System.Windows.Forms.Panel()
            Me.gisLegend = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.gisAttributes = New TatukGIS.NDK.WinForms.TGIS_ControlAttributes()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.progressbar = New System.Windows.Forms.ProgressBar()
            Me.btnCancel = New System.Windows.Forms.Button()
            Me.lblProgress = New System.Windows.Forms.Label()
            Me.dlgFileOpen = New System.Windows.Forms.OpenFileDialog()
            Me.mnuMain.SuspendLayout()
            Me.toolbar.SuspendLayout()
            Me.pnlGIS.SuspendLayout()
            Me.panel1.SuspendLayout()
            Me.SuspendLayout()
            '
            'mnuMain
            '
            Me.mnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                Me.menuFile, _
                Me.menuTopology})
            Me.mnuMain.Location = New System.Drawing.Point(0, 0)
            Me.mnuMain.Name = "mnuMain"
            Me.mnuMain.Size = New System.Drawing.Size(1000, 24)
            Me.mnuMain.TabIndex = 4
            '
            'menuFile
            '
            Me.menuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { _
                Me.menuAdd, _
                Me.menuOpen, _
                Me.menuOpenLineTopoSample, _
                Me.menuOpenTopoPolygonSample, _
                Me.N1, _
                Me.menuSelectVisible, _
                Me.menuSelectAll, _
                Me.menuDeselectAll, _
                Me.N3, _
                Me.menuSave, _
                Me.menuClose, _
                Me.N2, _
                Me.menuExit})
            Me.menuFile.Name = "menuFile"
            Me.menuFile.Text = "File"
            '
            'menuAdd
            '
            Me.menuAdd.Name = "menuAdd"
            Me.menuAdd.Text = "Add..."
            '
            'menuOpen
            '
            Me.menuOpen.Name = "menuOpen"
            Me.menuOpen.Text = "Open..."
            '
            'menuOpenLineTopoSample
            '
            Me.menuOpenLineTopoSample.Name = "menuOpenLineTopoSample"
            Me.menuOpenLineTopoSample.Text = "Open sample Line Topology project"
            '
            'menuOpenTopoPolygonSample
            '
            Me.menuOpenTopoPolygonSample.Name = "menuOpenTopoPolygonSample"
            Me.menuOpenTopoPolygonSample.Text = "Open sample Polygon Topology project"
            '
            'N1
            '
            Me.N1.Name = "N1"
            '
            'menuSelectVisible
            '
            Me.menuSelectVisible.Name = "menuSelectVisible"
            Me.menuSelectVisible.Text = "Select Visible"
            '
            'menuSelectAll
            '
            Me.menuSelectAll.Name = "menuSelectAll"
            Me.menuSelectAll.Text = "Select all"
            '
            'menuDeselectAll
            '
            Me.menuDeselectAll.Name = "menuDeselectAll"
            Me.menuDeselectAll.Text = "Deselect all"
            '
            'N3
            '
            Me.N3.Name = "N3"
            '
            'menuSave
            '
            Me.menuSave.Name = "menuSave"
            Me.menuSave.Text = "Save"
            '
            'menuClose
            '
            Me.menuClose.Name = "menuClose"
            Me.menuClose.Text = "Close"
            '
            'N2
            '
            Me.N2.Name = "N2"
            '
            'menuExit
            '
            Me.menuExit.Name = "menuExit"
            Me.menuExit.Text = "Exit"
            '
            'menuTopology
            '
            Me.menuTopology.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { _
                Me.menuTopoRollback, _
                Me.menuTopoSettings, _
                Me.menuTopoSeparatorLayer1, _
                Me.menuTopoCreateTopology, _
                Me.menuTopoCreateFeatureLayer, _
                Me.menuTopoDeleteFeatureLayer, _
                Me.menuTopoSeparatorFeature1, _
                Me.menuTopoCreateFeature, _
                Me.menuTopoDeleteFeature, _
                Me.menuTopoAddElementsToFeature, _
                Me.menuTopoDeleteFeatureElement, _
                Me.menuTopoSeparatorFix1, _
                Me.menuTopoAutoFixImportErrors, _
                Me.menuTopoManualFixImportErrors})
            Me.menuTopology.Name = "menuTopology"
            Me.menuTopology.Text = "Topology"
            '
            'menuTopoRollback
            '
            Me.menuTopoRollback.Name = "menuTopoRollback"
            Me.menuTopoRollback.Text = "Rollback"
            '
            'menuTopoSettings
            '
            Me.menuTopoSettings.Name = "menuTopoSettings"
            Me.menuTopoSettings.Text = "Settings"
            '
            'menuTopoSeparatorLayer1
            '
            Me.menuTopoSeparatorLayer1.Name = "menuTopoSeparatorLayer1"
            '
            'menuTopoCreateTopology
            '
            Me.menuTopoCreateTopology.Name = "menuTopoCreateTopology"
            Me.menuTopoCreateTopology.Text = "Create topology..."
            '
            'menuTopoCreateFeatureLayer
            '
            Me.menuTopoCreateFeatureLayer.Name = "menuTopoCreateFeatureLayer"
            Me.menuTopoCreateFeatureLayer.Text = "Create feature layer..."
            '
            'menuTopoDeleteFeatureLayer
            '
            Me.menuTopoDeleteFeatureLayer.Name = "menuTopoDeleteFeatureLayer"
            Me.menuTopoDeleteFeatureLayer.Text = "Delete feature layer..."
            '
            'menuTopoSeparatorFeature1
            '
            Me.menuTopoSeparatorFeature1.Name = "menuTopoSeparatorFeature1"
            '
            'menuTopoCreateFeature
            '
            Me.menuTopoCreateFeature.Name = "menuTopoCreateFeature"
            Me.menuTopoCreateFeature.Text = "Create feature(s)..."
            '
            'menuTopoDeleteFeature
            '
            Me.menuTopoDeleteFeature.Name = "menuTopoDeleteFeature"
            Me.menuTopoDeleteFeature.Text = "Delete feature(s)..."
            '
            'menuTopoAddElementsToFeature
            '
            Me.menuTopoAddElementsToFeature.Name = "menuTopoAddElementsToFeature"
            Me.menuTopoAddElementsToFeature.Text = "Add element(s) to feature..."
            '
            'menuTopoDeleteFeatureElement
            '
            Me.menuTopoDeleteFeatureElement.Name = "menuTopoDeleteFeatureElement"
            Me.menuTopoDeleteFeatureElement.Text = "Delete feature element..."
            '
            'menuTopoSeparatorFix1
            '
            Me.menuTopoSeparatorFix1.Name = "menuTopoSeparatorFix1"
            '
            'menuTopoAutoFixImportErrors
            '
            Me.menuTopoAutoFixImportErrors.Name = "menuTopoAutoFixImportErrors"
            Me.menuTopoAutoFixImportErrors.Text = "Auto fix import errors..."
            '
            'menuTopoManualFixImportErrors
            '
            Me.menuTopoManualFixImportErrors.Name = "menuTopoManualFixImportErrors"
            Me.menuTopoManualFixImportErrors.Text = "Merge feature into neighbor..."
            '
            'lstImage
            '
            Me.lstImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
            Me.lstImage.ImageStream = CType(resources.GetObject("lstImage.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.lstImage.TransparentColor = System.Drawing.Color.Transparent
            Me.lstImage.Images.SetKeyName(0, "FullExtent")
            Me.lstImage.Images.SetKeyName(1, "Zoom")
            Me.lstImage.Images.SetKeyName(2, "Drag")
            Me.lstImage.Images.SetKeyName(3, "Select")
            Me.lstImage.Images.SetKeyName(4, "Edit")
            Me.lstImage.Images.SetKeyName(5, "Undo")
            Me.lstImage.Images.SetKeyName(6, "Redo")
            Me.lstImage.Images.SetKeyName(7, "RevertShape")
            Me.lstImage.Images.SetKeyName(8, "Delete")
            Me.lstImage.Images.SetKeyName(9, "AddShape")
            Me.lstImage.Images.SetKeyName(10, "SnapVertex")
            Me.lstImage.Images.SetKeyName(11, "SnapEdge")
            Me.lstImage.Images.SetKeyName(12, "SnapVertexOrEdge")
            Me.lstImage.Images.SetKeyName(13, "SnapEndPoint")
            Me.lstImage.Images.SetKeyName(14, "SnapMidpoint")
            Me.lstImage.Images.SetKeyName(15, "SnapPerpendicular")
            '
            'toolbar
            '
            Me.toolbar.AutoSize = False
            Me.toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.toolbar.ImageList = Me.lstImage
            Me.toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() { _
                Me.btnFullExtent, _
                Me.btnZoom, _
                Me.btnDragMode, _
                Me.btnSelectMode, _
                Me.sepEdit, _
                Me.btnEditMode, _
                Me.btnUndo, _
                Me.btnRedo, _
                Me.btnRevertShape, _
                Me.btnDelete, _
                Me.sepAdd, _
                Me.btnAddShape, _
                Me.lblSnapLayer, _
                Me.btnSnapLayers, _
                Me.btnSnapType})
            Me.toolbar.Location = New System.Drawing.Point(0, 24)
            Me.toolbar.Name = "toolbar"
            Me.toolbar.Padding = New System.Windows.Forms.Padding(0)
            Me.toolbar.ShowItemToolTips = True
            Me.toolbar.Size = New System.Drawing.Size(1000, 29)
            Me.toolbar.TabIndex = 2
            '
            'btnFullExtent
            '
            Me.btnFullExtent.AutoSize = False
            Me.btnFullExtent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Margin = New System.Windows.Forms.Padding(0)
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.Size = New System.Drawing.Size(28, 28)
            Me.btnFullExtent.ToolTipText = "Full Extent - zoom the map to show all layers"
            '
            'btnZoom
            '
            Me.btnZoom.AutoSize = False
            Me.btnZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnZoom.ImageIndex = 1
            Me.btnZoom.Margin = New System.Windows.Forms.Padding(0)
            Me.btnZoom.Name = "btnZoom"
            Me.btnZoom.Size = New System.Drawing.Size(28, 28)
            Me.btnZoom.ToolTipText = "Zoom Mode - drag a rectangle on the map to zoom into it (the mouse wheel zooms in every mode)"
            '
            'btnDragMode
            '
            Me.btnDragMode.AutoSize = False
            Me.btnDragMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnDragMode.ImageIndex = 2
            Me.btnDragMode.Margin = New System.Windows.Forms.Padding(0)
            Me.btnDragMode.Name = "btnDragMode"
            Me.btnDragMode.Size = New System.Drawing.Size(28, 28)
            Me.btnDragMode.ToolTipText = "Drag Mode - drag the map to pan it"
            '
            'btnSelectMode
            '
            Me.btnSelectMode.AutoSize = False
            Me.btnSelectMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnSelectMode.ImageIndex = 3
            Me.btnSelectMode.Margin = New System.Windows.Forms.Padding(0)
            Me.btnSelectMode.Name = "btnSelectMode"
            Me.btnSelectMode.Size = New System.Drawing.Size(28, 28)
            Me.btnSelectMode.ToolTipText = "Select Mode - click a shape to select it and show its attributes, Ctrl+click to add or remove shapes from the selection"
            '
            'sepEdit
            '
            Me.sepEdit.Name = "sepEdit"
            Me.sepEdit.Size = New System.Drawing.Size(6, 29)
            '
            'btnEditMode
            '
            Me.btnEditMode.AutoSize = False
            Me.btnEditMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnEditMode.ImageIndex = 4
            Me.btnEditMode.Margin = New System.Windows.Forms.Padding(0)
            Me.btnEditMode.Name = "btnEditMode"
            Me.btnEditMode.Size = New System.Drawing.Size(28, 28)
            Me.btnEditMode.ToolTipText = "Edit Mode - click a shape to edit its geometry, right-click to finish editing"
            '
            'btnUndo
            '
            Me.btnUndo.AutoSize = False
            Me.btnUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnUndo.ImageIndex = 5
            Me.btnUndo.Margin = New System.Windows.Forms.Padding(0)
            Me.btnUndo.Name = "btnUndo"
            Me.btnUndo.Size = New System.Drawing.Size(28, 28)
            Me.btnUndo.ToolTipText = "Undo (Ctrl+Z) - undo the last change to the shape being edited or added"
            '
            'btnRedo
            '
            Me.btnRedo.AutoSize = False
            Me.btnRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnRedo.ImageIndex = 6
            Me.btnRedo.Margin = New System.Windows.Forms.Padding(0)
            Me.btnRedo.Name = "btnRedo"
            Me.btnRedo.Size = New System.Drawing.Size(28, 28)
            Me.btnRedo.ToolTipText = "Redo (Ctrl+Y) - redo the last undone change to the shape being edited or added"
            '
            'btnRevertShape
            '
            Me.btnRevertShape.AutoSize = False
            Me.btnRevertShape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnRevertShape.ImageIndex = 7
            Me.btnRevertShape.Margin = New System.Windows.Forms.Padding(0)
            Me.btnRevertShape.Name = "btnRevertShape"
            Me.btnRevertShape.Size = New System.Drawing.Size(28, 28)
            Me.btnRevertShape.ToolTipText = "Revert Shape - discard all changes to the edited shape and finish editing"
            '
            'btnDelete
            '
            Me.btnDelete.AutoSize = False
            Me.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnDelete.ImageIndex = 8
            Me.btnDelete.Margin = New System.Windows.Forms.Padding(0)
            Me.btnDelete.Name = "btnDelete"
            Me.btnDelete.Size = New System.Drawing.Size(28, 28)
            Me.btnDelete.ToolTipText = "Delete - delete the edited shape"
            '
            'sepAdd
            '
            Me.sepAdd.Name = "sepAdd"
            Me.sepAdd.Size = New System.Drawing.Size(6, 29)
            '
            'btnAddShape
            '
            Me.btnAddShape.AutoSize = False
            Me.btnAddShape.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnAddShape.ImageIndex = 9
            Me.btnAddShape.Margin = New System.Windows.Forms.Padding(0)
            Me.btnAddShape.Name = "btnAddShape"
            Me.btnAddShape.Size = New System.Drawing.Size(28, 28)
            Me.btnAddShape.ToolTipText = "Add Shape - add shapes to the layer selected in the legend" & Global.Microsoft.VisualBasic.ChrW(10) & "Click on the map to start a shape, right-click to finish it" & Global.Microsoft.VisualBasic.ChrW(10) & "Hold Ctrl while clicking to start the next edge from the last edge instead of from the closest node" & Global.Microsoft.VisualBasic.ChrW(10) & "Stays on until another mode or another layer in the legend is chosen"
            '
            'lblSnapLayer
            '
            Me.lblSnapLayer.Margin = New System.Windows.Forms.Padding(6, 0, 2, 0)
            Me.lblSnapLayer.Name = "lblSnapLayer"
            Me.lblSnapLayer.Size = New System.Drawing.Size(50, 29)
            Me.lblSnapLayer.Text = "Snap to:"
            '
            'btnSnapLayers
            '
            Me.btnSnapLayers.AutoSize = False
            Me.btnSnapLayers.AutoToolTip = False
            Me.btnSnapLayers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
            Me.btnSnapLayers.Name = "btnSnapLayers"
            Me.btnSnapLayers.Size = New System.Drawing.Size(150, 23)
            Me.btnSnapLayers.Text = "No snapping"
            Me.btnSnapLayers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.btnSnapLayers.ToolTipText = "Snap to - check the layers whose shapes the vertices of added and edited shapes snap to (the own layer of the shape is included), with no layer checked snapping is off"
            '
            'btnSnapType
            '
            Me.btnSnapType.AutoSize = False
            Me.btnSnapType.AutoToolTip = False
            Me.btnSnapType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnSnapType.ImageIndex = 10
            Me.btnSnapType.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
            Me.btnSnapType.Name = "btnSnapType"
            Me.btnSnapType.Size = New System.Drawing.Size(38, 28)
            '
            'pnlGIS
            '
            Me.pnlGIS.Controls.Add(Me.gisLegend)
            Me.pnlGIS.Controls.Add(Me.gisAttributes)
            Me.pnlGIS.Dock = System.Windows.Forms.DockStyle.Left
            Me.pnlGIS.Location = New System.Drawing.Point(0, 53)
            Me.pnlGIS.Name = "pnlGIS"
            Me.pnlGIS.Padding = New System.Windows.Forms.Padding(1)
            Me.pnlGIS.Size = New System.Drawing.Size(200, 636)
            Me.pnlGIS.TabIndex = 1
            '
            'gisLegend
            '
            Me.gisLegend.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gisLegend.GIS_Viewer = Me.GIS
            Me.gisLegend.Location = New System.Drawing.Point(1, 1)
            Me.gisLegend.Name = "gisLegend"
            Me.gisLegend.Options = CType(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.gisLegend.Size = New System.Drawing.Size(198, 234)
            Me.gisLegend.TabIndex = 1
            '
            'gisAttributes
            '
            Me.gisAttributes.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.gisAttributes.Location = New System.Drawing.Point(1, 235)
            Me.gisAttributes.Name = "gisAttributes"
            Me.gisAttributes.Size = New System.Drawing.Size(198, 400)
            Me.gisAttributes.TabIndex = 0
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.Color.White
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(200, 53)
            Me.GIS.Name = "GIS"
            Me.GIS.ProgressiveUpdate = 2500
            Me.GIS.Size = New System.Drawing.Size(800, 636)
            Me.GIS.TabIndex = 0
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.progressbar)
            Me.panel1.Controls.Add(Me.btnCancel)
            Me.panel1.Controls.Add(Me.lblProgress)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.panel1.Location = New System.Drawing.Point(0, 689)
            Me.panel1.Name = "panel1"
            Me.panel1.Padding = New System.Windows.Forms.Padding(1)
            Me.panel1.Size = New System.Drawing.Size(1000, 40)
            Me.panel1.TabIndex = 3
            '
            'progressbar
            '
            Me.progressbar.Dock = System.Windows.Forms.DockStyle.Fill
            Me.progressbar.Location = New System.Drawing.Point(28, 1)
            Me.progressbar.Name = "progressbar"
            Me.progressbar.Size = New System.Drawing.Size(875, 38)
            Me.progressbar.TabIndex = 1
            '
            'btnCancel
            '
            Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
            Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold)
            Me.btnCancel.Location = New System.Drawing.Point(903, 1)
            Me.btnCancel.Name = "btnCancel"
            Me.btnCancel.Size = New System.Drawing.Size(96, 38)
            Me.btnCancel.TabIndex = 0
            Me.btnCancel.Text = "Cancel"
            '
            'lblProgress
            '
            Me.lblProgress.AutoSize = True
            Me.lblProgress.Dock = System.Windows.Forms.DockStyle.Left
            Me.lblProgress.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold)
            Me.lblProgress.Location = New System.Drawing.Point(1, 1)
            Me.lblProgress.MinimumSize = New System.Drawing.Size(0, 38)
            Me.lblProgress.Name = "lblProgress"
            Me.lblProgress.Size = New System.Drawing.Size(27, 38)
            Me.lblProgress.Text = "0%"
            Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(1000, 729)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.pnlGIS)
            Me.Controls.Add(Me.panel1)
            Me.Controls.Add(Me.toolbar)
            Me.Controls.Add(Me.mnuMain)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MainMenuStrip = Me.mnuMain
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - TopologyLayer"
            Me.mnuMain.ResumeLayout(False)
            Me.mnuMain.PerformLayout()
            Me.toolbar.ResumeLayout(False)
            Me.toolbar.PerformLayout()
            Me.pnlGIS.ResumeLayout(False)
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
#If NET5_0_OR_GREATER Then
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        ''' <summary>
        ''' Initialises the topology editor application. Sets up the topology tool
        ''' with connections to the map viewer, layer legend, and attribute inspector,
        ''' configures the viewer for unrestricted panning and populates all topology
        ''' menu captions with localized resource strings.
        ''' </summary>
        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ' On .NET topology stores are registered lazily (upon the first layer
            ' creation), so register them explicitly - otherwise creating a topology
            ' before any layer is opened fails with "Topology process cancelled"
            TatukGIS.NDK.__Global.SelfRegisterTopoStores()

            topoTool = New TGIS_TopoTool(Me, GIS, gisLegend, gisAttributes)

            GIS.RestrictedDrag = False

            menuTopology.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_TOPOLOGY)
            menuTopoRollback.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_ROLLBACK)
            menuTopoSettings.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_SETTINGS)
            menuTopoCreateTopology.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_CREATE_TOPO)
            menuTopoCreateFeatureLayer.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_CREATE_FEATURE_LAYER)
            menuTopoDeleteFeatureLayer.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_DELETE_FEATURE_LAYER)
            menuTopoCreateFeature.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_CREATE_FEATURES)
            menuTopoDeleteFeature.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_DELETE_FEATURES)
            menuTopoAddElementsToFeature.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_ADD_ELEMENTS)
            menuTopoDeleteFeatureElement.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_DELETE_ELEMENT)
            menuTopoAutoFixImportErrors.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_AUTO_FIX)
            menuTopoManualFixImportErrors.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_MANUAL_FIX)

            initEditingToolbar()
        End Sub

        ''' <summary>
        ''' Commits the current shape edit operation. Disables edit-related toolbar
        ''' buttons (undo, redo, revert, delete) after edit ends. Returns False if
        ''' the edit cannot be committed (validation error).
        ''' </summary>
        Private Function endEdit() As Boolean
            If Not GIS.Editor.TryEndEdit() Then
                Return False
            End If

            btnUndo.Enabled = False
            btnRedo.Enabled = False
            btnRevertShape.Enabled = False
            btnDelete.Enabled = False

            Return True
        End Function

        ''' <summary>
        ''' Asks whether to save pending changes and saves all layers if confirmed.
        ''' </summary>
        Private Sub trySave()
            If Not GIS.MustSave() Then
                Return
            End If

            If MessageBox.Show("Save changes?", "Confirm", _
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question _
                              ) = DialogResult.Yes Then
                GIS.SaveAll()
            End If
        End Sub

        ' =====================================================================
        ' Toolbar
        ' =====================================================================

        ''' <summary>
        ''' Fits the map viewport to show all loaded layers.
        ''' </summary>
        Private Sub btnFullExtent_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFullExtent.Click
            GIS.FullExtent()
        End Sub

        ''' <summary>
        ''' Switches to zoom mode for interactive scale control.
        ''' </summary>
        Private Sub btnZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnZoom.Click
            If Not finishAdding() Then
                Return
            End If

            GIS.Mode = TGIS_ViewerMode.Zoom
        End Sub

        ''' <summary>
        ''' Switches to pan/drag mode for interactive map panning.
        ''' </summary>
        Private Sub btnDragMode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDragMode.Click
            If Not finishAdding() Then
                Return
            End If

            GIS.Mode = TGIS_ViewerMode.Drag
        End Sub

        ''' <summary>
        ''' Switches to shape selection mode after ending any active edit.
        ''' </summary>
        Private Sub btnSelectMode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectMode.Click
            If Not endEdit() Then
                Return
            End If

            stopAdding()
            GIS.Mode = TGIS_ViewerMode.Select
        End Sub

        ''' <summary>
        ''' Switches to edit mode for interactive geometry editing.
        ''' </summary>
        Private Sub btnEditMode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditMode.Click
            If Not finishAdding() Then
                Return
            End If

            GIS.Mode = TGIS_ViewerMode.Edit
        End Sub

        ''' <summary>
        ''' Reverts the last shape edit operation.
        ''' </summary>
        Private Sub btnUndo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo.Click
            If GIS.Editor.CanUndo Then
                GIS.Editor.Undo()
            End If
        End Sub

        ''' <summary>
        ''' Restores the last undone shape edit operation.
        ''' </summary>
        Private Sub btnRedo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRedo.Click
            If GIS.Editor.CanRedo Then
                GIS.Editor.Redo()
            End If
        End Sub

        ''' <summary>
        ''' Reverts the current shape to its original state, discarding all edits.
        ''' </summary>
        Private Sub btnRevertShape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevertShape.Click
            GIS.Editor.RevertShape()
            btnSelectMode_Click(Me, EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Deletes the currently edited shape and returns to select mode.
        ''' </summary>
        Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            GIS.Editor.DeleteShape()
            btnSelectMode_Click(Me, EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Turns the Add Shape mode on for the layer selected in the legend. The
        ''' mode stays on, so that shapes (e.g. a sequence of nodes or edges) can be
        ''' added one after another, until another mode is chosen or the button is
        ''' pressed again.
        ''' </summary>
        Private Sub btnAddShape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddShape.Click
            ' pressed again - leave the Add Shape mode
            If btnAddShape.Checked Then
                btnSelectMode_Click(Me, EventArgs.Empty)
                Return
            End If

            If Not endEdit() Then
                Return
            End If

            If gisLegend.GIS_Layer Is Nothing Then
                MessageBox.Show("Select the layer for the new shape in the legend first.", _
                                "Add Shape", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            editLayer = gisLegend.GIS_Layer
            setAddingShape(True)
            GIS.Mode = TGIS_ViewerMode.Edit
        End Sub

        ' =====================================================================
        ' Progress panel
        ' =====================================================================

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            ' === WORKFLOW: Abort Long-Running Operation ===
            abort = True
        End Sub

        ''' <summary>
        ''' Progress callback during long-running topology operations. Updates the
        ''' progress bar and percentage label. User can abort via the Cancel button
        ''' (sets abort flag).
        ''' </summary>
        Private Sub GIS_BusyEvent(ByVal sender As Object, ByVal e As TGIS_BusyEventArgs) Handles GIS.BusyEvent
            Dim percent As Integer = 0

            If e.Pos <= 0 Then
                abort = False
            End If

            ' Pos and EndPos are -1 when processing ends
            If (e.Pos > 0) AndAlso (e.EndPos > 0) Then
                percent = CInt(Math.Min(e.Pos * 100 \ e.EndPos, 100))
            End If

            progressbar.Value = percent
            lblProgress.Text = percent.ToString() & "%"
            e.Abort = abort

            ' keep the progress panel painted and the Cancel button responsive
            Application.DoEvents()
        End Sub

        ' =====================================================================
        ' Map interaction
        ' =====================================================================

        ''' <summary>
        ''' Map click handler for both shape selection (SELECT mode) and geometry
        ''' editing (EDIT mode). Right-click finishes the added shape in the Add
        ''' Shape mode and switches to SELECT mode otherwise. Ctrl-click toggles
        ''' shape selection.
        '''
        ''' Algorithm:
        '''   1. Reject empty maps; handle right-click.
        '''   2. Convert screen click position to map coordinates and hit-test for shape.
        '''   3. In SELECT mode: Ctrl toggles selection; single-click selects one shape
        '''      and shows its attributes.
        '''   4. In EDIT mode: Click creates new shape in the Add Shape mode, or clicks
        '''      existing shape to edit its geometry. Enables undo/redo buttons.
        ''' </summary>
        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseUp
            Dim ptg As TGIS_Point
            Dim shp As TGIS_Shape

            If GIS.IsEmpty Then
                Return
            End If

            If e.Button = MouseButtons.Right Then
                ' finish the added shape, but stay in the Add Shape mode
                If btnAddShape.Checked Then
                    finishShape()
                Else
                    btnSelectMode_Click(Me, EventArgs.Empty)
                End If
                Return
            End If

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            shp = CType(GIS.Locate(ptg, 5 / GIS.Zoom), TGIS_Shape)

            If GIS.Mode = TGIS_ViewerMode.Select Then
                If shp Is Nothing Then
                    Return
                End If

                If (Control.ModifierKeys And Keys.Control) = Keys.Control Then
                    shp.IsSelected = Not shp.IsSelected
                    gisAttributes.ShowSelected(shp.Layer)
                Else
                    shp.Layer.DeselectAll()
                    shp.IsSelected = Not shp.IsSelected
                    gisAttributes.ShowShape(shp)
                End If

                gisLegend.GIS_Layer = shp.Layer
            ElseIf GIS.Mode = TGIS_ViewerMode.Edit Then
                If Not GIS.Editor.CurrentShape Is Nothing Then
                    Return
                End If

                If btnAddShape.Checked Then
                    ' Create a new shape in the layer chosen with the Add Shape button
                    addShape(ptg)
                Else
                    ' Edit existing shape geometry
                    If Not GIS.Editor.TryEditShape(shp, 0, ptg) Then
                        Return
                    End If

                    btnRedo.Enabled = True
                    btnUndo.Enabled = True
                    btnRevertShape.Enabled = True
                    btnDelete.Enabled = True
                End If

                GIS.InvalidateEditor(True)
            End If
        End Sub

        ' =====================================================================
        ' File menu
        ' =====================================================================

        ''' <summary>
        ''' Adds a new layer to the topology project. Opens file dialog, creates the
        ''' layer object, configures it, and updates the map extent and legend.
        '''
        ''' Algorithm:
        '''   1. Open file dialog to select a layer file.
        '''   2. Create layer object using factory function GisCreateLayer.
        '''   3. Load configuration from saved .ttkgp file if available.
        '''   4. Add layer to viewer; zoom to full extent if first layer, else just refresh.
        '''   5. Update legend to show new layer.
        ''' </summary>
        Private Sub menuAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuAdd.Click
            Dim layer As TGIS_Layer = Nothing
            Dim filename As String

            If dlgFileOpen.ShowDialog() <> DialogResult.OK Then
                Return
            End If
            filename = dlgFileOpen.FileName

            Try
                layer = TGIS_Utils.GisCreateLayer(Path.GetFileName(filename), filename)
                If Not layer Is Nothing Then
                    layer.ReadConfig()
                    GIS.Add(layer)
                End If
            Catch ex As Exception
                MessageBox.Show("Cannot add the file:" & Environment.NewLine & _
                                filename & Environment.NewLine & _
                                ex.Message)
            End Try

            If GIS.Items.Count = 1 Then
                GIS.FullExtent()
            Else
                GIS.InvalidateWholeMap()
            End If

            gisLegend.GIS_Layer = layer
        End Sub

        Private Sub menuOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpen.Click
            If dlgFileOpen.ShowDialog() <> DialogResult.OK Then
                Return
            End If

            GIS.Close()
            Try
                GIS.Open(dlgFileOpen.FileName)
            Catch ex As Exception
                MessageBox.Show("Cannot open the file:" & Environment.NewLine & _
                                dlgFileOpen.FileName & Environment.NewLine & _
                                ex.Message)
            End Try
        End Sub

        Private Sub menuOpenLineTopoSample_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpenLineTopoSample.Click
            Dim samples As String = TGIS_Utils.GisSamplesDataDirDownload("TopologyLayer.1")
            GIS.Open(samples & "Samples\TopologyLayer\LINETOPOLOGY\OIL.ttkproject")
        End Sub

        Private Sub menuOpenTopoPolygonSample_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpenTopoPolygonSample.Click
            Dim samples As String = TGIS_Utils.GisSamplesDataDirDownload("TopologyLayer.1")
            GIS.Open(samples & "Samples\TopologyLayer\POLYGONTOPOLOGY\CLC.ttkproject")
        End Sub

        Private Sub menuSelectVisible_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSelectVisible.Click
            Dim lv As TGIS_LayerVector = TryCast(gisLegend.GIS_Layer, TGIS_LayerVector)
            Dim shp As TGIS_Shape

            If lv Is Nothing Then
                Return
            End If

            For Each shp In lv.Loop(GIS.VisibleExtent)
                If Not shp.IsHidden Then
                    shp.IsSelected = True
                End If
            Next
        End Sub

        Private Sub menuSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSelectAll.Click
            Dim lv As TGIS_LayerVector = TryCast(gisLegend.GIS_Layer, TGIS_LayerVector)
            Dim shp As TGIS_Shape

            If lv Is Nothing Then
                Return
            End If

            GIS.Lock()
            Try
                For Each shp In lv.Loop()
                    If Not shp.IsHidden Then
                        shp.IsSelected = True
                    End If
                Next
            Finally
                GIS.Unlock()
            End Try
        End Sub

        Private Sub menuDeselectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuDeselectAll.Click
            Dim lv As TGIS_LayerVector = TryCast(gisLegend.GIS_Layer, TGIS_LayerVector)

            If GIS.IsEmpty OrElse lv Is Nothing Then
                Return
            End If

            lv.DeselectAll()
        End Sub

        Private Sub menuSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSave.Click
            trySave()
        End Sub

        Private Sub menuClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuClose.Click
            trySave()
            GIS.Close()
        End Sub

        Private Sub menuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuExit.Click
            trySave()
            Me.Close()
        End Sub

        ' =====================================================================
        ' Topology menu
        ' =====================================================================

        Private Sub menuTopoRollback_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoRollback.Click
            topoTool.Rollback()
        End Sub

        Private Sub menuTopoSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoSettings.Click
            topoTool.OpenSettingsForm()
        End Sub

        Private Sub menuTopoCreateTopology_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoCreateTopology.Click
            topoTool.RunTool(TGIS_TopoTools.CreateTopology)
        End Sub

        Private Sub menuTopoCreateFeatureLayer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoCreateFeatureLayer.Click
            topoTool.RunTool(TGIS_TopoTools.CreateFeatureLayer)
        End Sub

        Private Sub menuTopoDeleteFeatureLayer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoDeleteFeatureLayer.Click
            topoTool.RunTool(TGIS_TopoTools.DeleteFeatureLayer)
        End Sub

        Private Sub menuTopoCreateFeature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoCreateFeature.Click
            topoTool.RunTool(TGIS_TopoTools.CreateTopoFeature)
        End Sub

        Private Sub menuTopoDeleteFeature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoDeleteFeature.Click
            topoTool.RunTool(TGIS_TopoTools.DeleteTopoFeature)
        End Sub

        Private Sub menuTopoAddElementsToFeature_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoAddElementsToFeature.Click
            topoTool.RunTool(TGIS_TopoTools.AddElementsToTopoFeature)
        End Sub

        Private Sub menuTopoDeleteFeatureElement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoDeleteFeatureElement.Click
            topoTool.RunTool(TGIS_TopoTools.DeleteTopoFeatureElement)
        End Sub

        Private Sub menuTopoAutoFixImportErrors_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoAutoFixImportErrors.Click
            topoTool.RunTool(TGIS_TopoTools.AutoFixTopoImportErrors)
        End Sub

        Private Sub menuTopoManualFixImportErrors_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuTopoManualFixImportErrors.Click
            topoTool.RunTool(TGIS_TopoTools.ManualFixTopoImportErrors)
        End Sub

        ' =====================================================================
        ' Edit and Add Shape modes
        ' Mode buttons, the Add Shape mode kept on for sequences of shapes,
        ' Ctrl+Z / Ctrl+Y, mouse wheel zoom and ending the edit when the
        ' project, a layer or the legend selection changes
        ' =====================================================================

        ''' <summary>
        ''' Sets up the editing toolbar (snap type and snap layer lists, mode
        ''' buttons). Called when the form is loaded.
        ''' </summary>
        Private Sub initEditingToolbar()
            Dim item As ToolStripMenuItem

            ' snap types are shown as pictures, described by tooltips
            btnSnapType.DropDown.ShowItemToolTips = True
            For i As Integer = 0 To snapTypes.Length - 1
                item = New ToolStripMenuItem()
                item.DisplayStyle = ToolStripItemDisplayStyle.Image
                item.Image = lstImage.Images(SNAP_TYPE_IMAGE + i)
                item.ToolTipText = snapTypeHints(i)
                AddHandler item.Click, AddressOf snapTypeItem_Click
                btnSnapType.DropDownItems.Add(item)
            Next
            setSnapType(0)

            ' keep the snap layer list open while layers are being checked
            AddHandler btnSnapLayers.DropDown.Closing, AddressOf btnSnapLayers_DropDownClosing

            ' snapping is off until some snap layers are checked
            applySnapLayers()

            updateModeButtons()
        End Sub

        ''' <summary>
        ''' Presses the toolbar button of the current viewer mode. In the Add Shape
        ''' mode, the Add Shape button is pressed instead of the Edit Mode one.
        ''' </summary>
        Private Sub updateModeButtons()
            btnZoom.Checked = (GIS.Mode = TGIS_ViewerMode.Zoom)
            btnDragMode.Checked = (GIS.Mode = TGIS_ViewerMode.Drag)
            btnSelectMode.Checked = (GIS.Mode = TGIS_ViewerMode.Select)
            btnEditMode.Checked = (GIS.Mode = TGIS_ViewerMode.Edit) AndAlso Not btnAddShape.Checked
        End Sub

        ''' <summary>
        ''' Keeps the Add Shape button pressed while the Add Shape mode is on.
        ''' </summary>
        Private Sub setAddingShape(ByVal adding As Boolean)
            btnAddShape.Checked = adding
            updateModeButtons()
        End Sub

        ''' <summary>
        ''' Turns the Add Shape mode off.
        ''' </summary>
        Private Sub stopAdding()
            editLayer = Nothing
            setAddingShape(False)
        End Sub

        ''' <summary>
        ''' Leaves the Add Shape mode, finishing the shape being added. Returns
        ''' False if the shape cannot be finished (validation error).
        ''' </summary>
        Private Function finishAdding() As Boolean
            If Not btnAddShape.Checked Then
                Return True
            End If

            If Not endEdit() Then
                Return False
            End If

            stopAdding()
            Return True
        End Function

        ''' <summary>
        ''' Cancels the current shape edit (discarding its changes) and a pending
        ''' "Add Shape" operation, then switches the viewer to select mode.
        ''' </summary>
        Private Sub cancelEdit()
            If GIS.Editor.InEdit Then
                GIS.Editor.RevertShape()
            End If

            endEdit()
            stopAdding()
            GIS.Mode = TGIS_ViewerMode.Select
        End Sub

        ''' <summary>
        ''' Starts a new shape in the Add Shape mode layer at the clicked point.
        ''' </summary>
        Private Sub addShape(ByVal _ptg As TGIS_Point)
            Dim end_edit_required As Boolean

            If Not GIS.Editor.TryCreateShape(editLayer, _
                                             _ptg, _
                                             TGIS_ShapeType.Unknown, _
                                             end_edit_required) Then
                ' the layer does not accept new shapes
                btnSelectMode_Click(Me, EventArgs.Empty)
                Return
            End If

            If end_edit_required Then
                ' e.g. a topology node - finished at once, ready for the next one
                endEdit()
                Return
            End If

            btnUndo.Enabled = True
            btnRedo.Enabled = True
        End Sub

        ''' <summary>
        ''' Finishes the shape being added and keeps the Add Shape mode on for the
        ''' next one.
        ''' </summary>
        Private Sub finishShape()
            If GIS.Editor.InEdit Then
                endEdit()
            End If
        End Sub

        ''' <summary>
        ''' Handles Ctrl+Z (undo) and Ctrl+Y (redo) while a shape is being edited
        ''' or added. The map viewer does not take the keyboard focus, so the
        ''' shortcuts are processed on the form level.
        ''' </summary>
        Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean
            ' leave the shortcuts to text boxes, e.g. when editing attributes
            If GIS.Editor.InEdit AndAlso Not TypeOf ActiveControl Is TextBoxBase Then
                If keyData = (Keys.Control Or Keys.Z) Then
                    btnUndo_Click(Me, EventArgs.Empty)
                    Return True
                End If

                If keyData = (Keys.Control Or Keys.Y) Then
                    btnRedo_Click(Me, EventArgs.Empty)
                    Return True
                End If
            End If

            Return MyBase.ProcessCmdKey(msg, keyData)
        End Function

        ''' <summary>
        ''' Keeps the mode buttons in sync with the viewer mode, which can also be
        ''' changed by the topology tool (e.g. rollback switches to select mode).
        ''' </summary>
        Private Sub GIS_ModeChangeEvent(ByVal sender As Object, ByVal e As System.EventArgs) Handles GIS.ModeChangeEvent
            updateModeButtons()
        End Sub

        ''' <summary>
        ''' Called when the current project is closed, also when another project
        ''' is opened. Cancels any shape edit or pending "Add Shape" operation while
        ''' the project layers still exist - otherwise the editor would keep working
        ''' on the released layers and crash on the next map click or end of editing.
        ''' </summary>
        Private Sub GIS_ProjectCloseEvent(ByVal sender As Object, ByVal e As System.EventArgs) Handles GIS.ProjectCloseEvent
            cancelEdit()

            ' the snap layers belong to the closed project
            snapLayerNames.Clear()
            applySnapLayers()
        End Sub

        ''' <summary>
        ''' Removes a layer which is being deleted from the snap layers, so that
        ''' the editor does not keep snapping to a released layer.
        ''' </summary>
        Private Sub GIS_LayerDeleteEvent(ByVal sender As Object, ByVal e As TGIS_LayerEventArgs) Handles GIS.LayerDeleteEvent
            ' the layer of the added or edited shape is going away (e.g. deleted
            ' by a topology tool) - stop working on it before it is released
            If (e.Layer Is editLayer) OrElse (GIS.Editor.InEdit AndAlso (GIS.Editor.Layer Is e.Layer)) Then
                cancelEdit()
            End If

            If snapLayerNames.Remove(e.Layer.Name) Then
                applySnapLayers()
            End If
        End Sub

        ''' <summary>
        ''' Selecting another layer in the legend finishes the shape being edited
        ''' or added and switches the Edit or Add Shape mode to select mode, so that
        ''' shapes are not added to a layer which is no longer selected.
        ''' </summary>
        Private Sub gisLegend_LayerSelectEvent(ByVal sender As Object, ByVal e As TGIS_LayerEventArgs) Handles gisLegend.LayerSelectEvent
            Dim layer As TGIS_LayerAbstract = Nothing

            If Not btnAddShape.Checked AndAlso (GIS.Mode <> TGIS_ViewerMode.Edit) Then
                Return
            End If

            ' layer of the added or edited shape
            If btnAddShape.Checked Then
                layer = editLayer
            ElseIf GIS.Editor.InEdit Then
                layer = GIS.Editor.Layer
            End If

            ' the same layer selected again
            If (layer IsNot Nothing) AndAlso (e.Layer Is layer) Then
                Return
            End If

            btnSelectMode_Click(Me, EventArgs.Empty)
        End Sub

        ''' <summary>
        ''' Zooms the map in (wheel rolled forward) or out (wheel rolled back),
        ''' keeping the point under the cursor in place. Works in every mode, also
        ''' while a shape is being edited or added.
        ''' </summary>
        Private Sub GIS_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseWheel
            If GIS.IsEmpty Then
                Return
            End If

            ' 1.25x per wheel notch (Delta is 120 per notch, less on smooth wheels)
            GIS.ZoomBy(Math.Pow(1.25, e.Delta / 120.0), e.X, e.Y)
        End Sub

        ' =====================================================================
        ' Snapping
        ' Snap to layers (checked in a list) and snap type (chosen by picture)
        ' =====================================================================

        ''' <summary>
        ''' Fills the snap layer list with the vector layers of the current project,
        ''' keeping the checked ones.
        ''' </summary>
        Private Sub fillSnapLayers()
            Dim layer As TGIS_Layer
            Dim item As ToolStripMenuItem

            btnSnapLayers.DropDownItems.Clear()

            For i As Integer = 0 To GIS.Items.Count - 1
                layer = CType(GIS.Items(i), TGIS_Layer)

                ' skip helper layers, e.g. the ones of the topology tool
                If Not TypeOf layer Is TGIS_LayerVector OrElse layer.HideFromLegend Then
                    Continue For
                End If

                item = New ToolStripMenuItem(layer.Name)
                item.CheckOnClick = True
                item.Checked = snapLayerNames.Contains(layer.Name)
                AddHandler item.CheckedChanged, AddressOf snapLayerItem_CheckedChanged
                btnSnapLayers.DropDownItems.Add(item)
            Next

            If btnSnapLayers.DropDownItems.Count = 0 Then
                item = New ToolStripMenuItem("No vector layers")
                item.Enabled = False
                btnSnapLayers.DropDownItems.Add(item)
            End If
        End Sub

        ''' <summary>
        ''' Applies the checked snap layers to the editor. With no layer checked
        ''' snapping is turned off. The layer of the edited shape is added to the
        ''' snap layers by the editor itself when editing starts.
        ''' </summary>
        Private Sub applySnapLayers()
            GIS.Editor.ClearSnapLayers()
            GIS.Editor.BlockSnapping = (snapLayerNames.Count = 0)

            For Each layerName As String In snapLayerNames
                GIS.Editor.AddSnapLayer(GIS.Get(layerName))
            Next

            If snapLayerNames.Count = 0 Then
                btnSnapLayers.Text = NO_SNAPPING
            Else
                btnSnapLayers.Text = String.Join(", ", snapLayerNames)
            End If
        End Sub

        ''' <summary>
        ''' Refreshes the snap layer list, as layers may come and go with projects
        ''' and topology operations.
        ''' </summary>
        Private Sub btnSnapLayers_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSnapLayers.DropDownOpening
            fillSnapLayers()
        End Sub

        ''' <summary>
        ''' Keeps the snap layer list open while layers are being checked.
        ''' </summary>
        Private Sub btnSnapLayers_DropDownClosing(ByVal sender As Object, ByVal e As ToolStripDropDownClosingEventArgs)
            If e.CloseReason = ToolStripDropDownCloseReason.ItemClicked Then
                e.Cancel = True
            End If
        End Sub

        ''' <summary>
        ''' Adds or removes the layer of the clicked item from the snap layers.
        ''' </summary>
        Private Sub snapLayerItem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim item As ToolStripMenuItem = CType(sender, ToolStripMenuItem)

            If item.Checked Then
                snapLayerNames.Add(item.Text)
            Else
                snapLayerNames.Remove(item.Text)
            End If

            applySnapLayers()
        End Sub

        ''' <summary>
        ''' Applies the snap type of the clicked picture.
        ''' </summary>
        Private Sub snapTypeItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            setSnapType(btnSnapType.DropDownItems.IndexOf(CType(sender, ToolStripItem)))
        End Sub

        ''' <summary>
        ''' Applies the snap type to the editor and shows its picture on the toolbar.
        ''' </summary>
        Private Sub setSnapType(ByVal index As Integer)
            GIS.Editor.SnapType = snapTypes(index)
            btnSnapType.ImageIndex = SNAP_TYPE_IMAGE + index
            btnSnapType.ToolTipText = "Snap type: " & snapTypeHints(index)

            For i As Integer = 0 To btnSnapType.DropDownItems.Count - 1
                CType(btnSnapType.DropDownItems(i), ToolStripMenuItem).Checked = (i = index)
            Next
        End Sub
    End Class
End Namespace
