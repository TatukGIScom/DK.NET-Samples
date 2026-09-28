// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// TopologyLayer Sample - demonstrates topology editing and management in a GIS
// layer (C#/.NET WinForms).
//
// What the sample shows:
//   - Loading and managing topologically structured data (line and polygon topology)
//   - Creating topology structures from existing feature layers
//   - Creating and deleting features within topology constraints
//   - Editing topology elements while maintaining integrity
//   - Adding elements to existing topology features
//   - Automatic and manual fixing of topology import errors
//   - Undo/Redo operations during topology editing (also with Ctrl+Z / Ctrl+Y)
//   - Snapping to chosen layers and snap type when adding or editing shapes
//   - Adding sequences of nodes and edges (Ctrl+click continues from the last edge)
//   - Rollback of topology changes
//   - Integration with attribute viewer and layer legend
//   - Toolbar controls for different editing modes (zoom, drag, select, edit)
//   - Zooming with the mouse wheel
//   - Progress tracking during topology operations
//
// Key TatukGIS NDK classes used:
//   TGIS_ViewerWnd           - main map viewer control
//   TGIS_TopoTool            - topology operations manager
//   TGIS_Layer               - base layer class
//   TGIS_Shape               - topology shape objects
//   TGIS_ControlAttributes   - attribute display control
//   TGIS_ControlLegend       - layer legend control
//   TGIS_ViewerMode          - editing modes (Select, Edit, Drag, Zoom)
//   TGIS_EditorSnapType      - snapping targets (vertex, edge, midpoint, ...)
//   BusyEvent                - progress tracking callback
// =============================================================================

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace TopologyLayer
{
    /// <summary>
    /// Main form for the TopologyLayer sample application.
    /// Hosts the map viewer together with a legend, an attribute inspector,
    /// an editing toolbar and a progress panel, and exposes the topology
    /// workflows provided by <see cref="TGIS_TopoTool"/> through the main menu.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;

        // --- Main menu ---
        private System.Windows.Forms.MenuStrip mnuMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuAdd;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripMenuItem menuOpenLineTopoSample;
        private System.Windows.Forms.ToolStripMenuItem menuOpenTopoPolygonSample;
        private System.Windows.Forms.ToolStripSeparator N1;
        private System.Windows.Forms.ToolStripMenuItem menuSelectVisible;
        private System.Windows.Forms.ToolStripMenuItem menuSelectAll;
        private System.Windows.Forms.ToolStripMenuItem menuDeselectAll;
        private System.Windows.Forms.ToolStripSeparator N3;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuClose;
        private System.Windows.Forms.ToolStripSeparator N2;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuTopology;
        private System.Windows.Forms.ToolStripMenuItem menuTopoRollback;
        private System.Windows.Forms.ToolStripMenuItem menuTopoSettings;
        private System.Windows.Forms.ToolStripSeparator menuTopoSeparatorLayer1;
        private System.Windows.Forms.ToolStripMenuItem menuTopoCreateTopology;
        private System.Windows.Forms.ToolStripMenuItem menuTopoCreateFeatureLayer;
        private System.Windows.Forms.ToolStripMenuItem menuTopoDeleteFeatureLayer;
        private System.Windows.Forms.ToolStripSeparator menuTopoSeparatorFeature1;
        private System.Windows.Forms.ToolStripMenuItem menuTopoCreateFeature;
        private System.Windows.Forms.ToolStripMenuItem menuTopoDeleteFeature;
        private System.Windows.Forms.ToolStripMenuItem menuTopoAddElementsToFeature;
        private System.Windows.Forms.ToolStripMenuItem menuTopoDeleteFeatureElement;
        private System.Windows.Forms.ToolStripSeparator menuTopoSeparatorFix1;
        private System.Windows.Forms.ToolStripMenuItem menuTopoAutoFixImportErrors;
        private System.Windows.Forms.ToolStripMenuItem menuTopoManualFixImportErrors;

        // --- Toolbar ---
        private System.Windows.Forms.ImageList lstImage;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoom;
        private System.Windows.Forms.ToolStripButton btnDragMode;
        private System.Windows.Forms.ToolStripButton btnSelectMode;
        private System.Windows.Forms.ToolStripSeparator sepEdit;
        private System.Windows.Forms.ToolStripButton btnEditMode;
        private System.Windows.Forms.ToolStripButton btnUndo;
        private System.Windows.Forms.ToolStripButton btnRedo;
        private System.Windows.Forms.ToolStripButton btnRevertShape;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator sepAdd;
        private System.Windows.Forms.ToolStripButton btnAddShape;
        private System.Windows.Forms.ToolStripLabel lblSnapLayer;
        private System.Windows.Forms.ToolStripDropDownButton btnSnapLayers;
        private System.Windows.Forms.ToolStripDropDownButton btnSnapType;

        // --- Legend and attributes ---
        private System.Windows.Forms.Panel pnlGIS;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend gisLegend;
        private TatukGIS.NDK.WinForms.TGIS_ControlAttributes gisAttributes;

        // --- Map viewer ---
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

        // --- Progress panel ---
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar progressbar;
        private System.Windows.Forms.Button btnCancel;

        private System.Windows.Forms.OpenFileDialog dlgFileOpen;

        // --- State fields ---

        /// <summary>
        /// Set by the Cancel button and passed back to the running operation
        /// through the BusyEvent to abort it.
        /// </summary>
        private bool abort;

        /// <summary>
        /// Topology tool connected to the viewer, legend and attributes controls.
        /// Runs all topology workflows (wizards, settings, rollback).
        /// </summary>
        private TGIS_TopoTool topoTool;

        /// <summary>
        /// Layer to which new shapes are added while the Add Shape mode is on
        /// (set by the Add Shape button); null otherwise.
        /// </summary>
        private TGIS_Layer editLayer;

        // --- Snapping ---

        // text of the snap layer list when no layer is checked
        private const string NO_SNAPPING = "No snapping";

        // index of the first snap type picture in lstImage, in the order of snapTypes
        private const int SNAP_TYPE_IMAGE = 10;

        /// <summary>
        /// Names of the layers checked in the snap layer list and applied to the editor.
        /// </summary>
        private List<string> snapLayerNames = new List<string>();

        /// <summary>
        /// Snap types offered in the snap type list, in the order of its items.
        /// </summary>
        private static readonly TGIS_EditorSnapType[] snapTypes = {
            TGIS_EditorSnapType.Point,
            TGIS_EditorSnapType.Line,
            TGIS_EditorSnapType.PointOverLine,
            TGIS_EditorSnapType.EndPoint,
            TGIS_EditorSnapType.Midpoint,
            TGIS_EditorSnapType.Perpendicular
        };

        /// <summary>
        /// Descriptions of the snap types, in the order of snapTypes.
        /// </summary>
        private static readonly string[] snapTypeHints = {
            "Vertex - snap to the nearest vertex of a shape",
            "Edge - snap to the nearest point on a line segment or polygon edge",
            "Vertex or edge - snap to a vertex if one is near, otherwise to an edge",
            "End point - snap to the first or last vertex of a line or polygon",
            "Midpoint - snap to the middle of a line segment or polygon edge",
            "Perpendicular - snap to the point of an edge that makes a right angle with the previous segment"
        };

        /// <summary>
        /// Initialises WinForms designer components.
        /// </summary>
        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(WinForm));
            TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TGIS_ControlLegendDialogOptions();
            mnuMain = new MenuStrip();
            menuFile = new ToolStripMenuItem();
            menuAdd = new ToolStripMenuItem();
            menuOpen = new ToolStripMenuItem();
            menuOpenLineTopoSample = new ToolStripMenuItem();
            menuOpenTopoPolygonSample = new ToolStripMenuItem();
            N1 = new ToolStripSeparator();
            menuSelectVisible = new ToolStripMenuItem();
            menuSelectAll = new ToolStripMenuItem();
            menuDeselectAll = new ToolStripMenuItem();
            N3 = new ToolStripSeparator();
            menuSave = new ToolStripMenuItem();
            menuClose = new ToolStripMenuItem();
            N2 = new ToolStripSeparator();
            menuExit = new ToolStripMenuItem();
            menuTopology = new ToolStripMenuItem();
            menuTopoRollback = new ToolStripMenuItem();
            menuTopoSettings = new ToolStripMenuItem();
            menuTopoSeparatorLayer1 = new ToolStripSeparator();
            menuTopoCreateTopology = new ToolStripMenuItem();
            menuTopoCreateFeatureLayer = new ToolStripMenuItem();
            menuTopoDeleteFeatureLayer = new ToolStripMenuItem();
            menuTopoSeparatorFeature1 = new ToolStripSeparator();
            menuTopoCreateFeature = new ToolStripMenuItem();
            menuTopoDeleteFeature = new ToolStripMenuItem();
            menuTopoAddElementsToFeature = new ToolStripMenuItem();
            menuTopoDeleteFeatureElement = new ToolStripMenuItem();
            menuTopoSeparatorFix1 = new ToolStripSeparator();
            menuTopoAutoFixImportErrors = new ToolStripMenuItem();
            menuTopoManualFixImportErrors = new ToolStripMenuItem();
            lstImage = new ImageList(components);
            toolbar = new ToolStrip();
            btnFullExtent = new ToolStripButton();
            btnZoom = new ToolStripButton();
            btnDragMode = new ToolStripButton();
            btnSelectMode = new ToolStripButton();
            sepEdit = new ToolStripSeparator();
            btnEditMode = new ToolStripButton();
            btnUndo = new ToolStripButton();
            btnRedo = new ToolStripButton();
            btnRevertShape = new ToolStripButton();
            btnDelete = new ToolStripButton();
            sepAdd = new ToolStripSeparator();
            btnAddShape = new ToolStripButton();
            lblSnapLayer = new ToolStripLabel();
            btnSnapLayers = new ToolStripDropDownButton();
            btnSnapType = new ToolStripDropDownButton();
            pnlGIS = new Panel();
            gisLegend = new TGIS_ControlLegend();
            GIS = new TGIS_ViewerWnd();
            gisAttributes = new TGIS_ControlAttributes();
            panel1 = new Panel();
            progressbar = new ProgressBar();
            btnCancel = new Button();
            lblProgress = new Label();
            dlgFileOpen = new OpenFileDialog();
            mnuMain.SuspendLayout();
            toolbar.SuspendLayout();
            pnlGIS.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // mnuMain
            // 
            mnuMain.ImageScalingSize = new Size(20, 20);
            mnuMain.Items.AddRange(new ToolStripItem[] { menuFile, menuTopology });
            mnuMain.Location = new Point(0, 0);
            mnuMain.Name = "mnuMain";
            mnuMain.Padding = new Padding(10, 2, 0, 2);
            mnuMain.Size = new Size(1250, 30);
            mnuMain.TabIndex = 4;
            // 
            // menuFile
            // 
            menuFile.DropDownItems.AddRange(new ToolStripItem[] { menuAdd, menuOpen, menuOpenLineTopoSample, menuOpenTopoPolygonSample, N1, menuSelectVisible, menuSelectAll, menuDeselectAll, N3, menuSave, menuClose, N2, menuExit });
            menuFile.Name = "menuFile";
            menuFile.Size = new Size(46, 26);
            menuFile.Text = "File";
            // 
            // menuAdd
            // 
            menuAdd.Name = "menuAdd";
            menuAdd.Size = new Size(355, 26);
            menuAdd.Text = "Add...";
            menuAdd.Click += menuAdd_Click;
            // 
            // menuOpen
            // 
            menuOpen.Name = "menuOpen";
            menuOpen.Size = new Size(355, 26);
            menuOpen.Text = "Open...";
            menuOpen.Click += menuOpen_Click;
            // 
            // menuOpenLineTopoSample
            // 
            menuOpenLineTopoSample.Name = "menuOpenLineTopoSample";
            menuOpenLineTopoSample.Size = new Size(355, 26);
            menuOpenLineTopoSample.Text = "Open sample Line Topology project";
            menuOpenLineTopoSample.Click += menuOpenLineTopoSample_Click;
            // 
            // menuOpenTopoPolygonSample
            // 
            menuOpenTopoPolygonSample.Name = "menuOpenTopoPolygonSample";
            menuOpenTopoPolygonSample.Size = new Size(355, 26);
            menuOpenTopoPolygonSample.Text = "Open sample Polygon Topology project";
            menuOpenTopoPolygonSample.Click += menuOpenTopoPolygonSample_Click;
            // 
            // N1
            // 
            N1.Name = "N1";
            N1.Size = new Size(352, 6);
            // 
            // menuSelectVisible
            // 
            menuSelectVisible.Name = "menuSelectVisible";
            menuSelectVisible.Size = new Size(355, 26);
            menuSelectVisible.Text = "Select Visible";
            menuSelectVisible.Click += menuSelectVisible_Click;
            // 
            // menuSelectAll
            // 
            menuSelectAll.Name = "menuSelectAll";
            menuSelectAll.Size = new Size(355, 26);
            menuSelectAll.Text = "Select all";
            menuSelectAll.Click += menuSelectAll_Click;
            // 
            // menuDeselectAll
            // 
            menuDeselectAll.Name = "menuDeselectAll";
            menuDeselectAll.Size = new Size(355, 26);
            menuDeselectAll.Text = "Deselect all";
            menuDeselectAll.Click += menuDeselectAll_Click;
            // 
            // N3
            // 
            N3.Name = "N3";
            N3.Size = new Size(352, 6);
            // 
            // menuSave
            // 
            menuSave.Name = "menuSave";
            menuSave.Size = new Size(355, 26);
            menuSave.Text = "Save";
            menuSave.Click += menuSave_Click;
            // 
            // menuClose
            // 
            menuClose.Name = "menuClose";
            menuClose.Size = new Size(355, 26);
            menuClose.Text = "Close";
            menuClose.Click += menuClose_Click;
            // 
            // N2
            // 
            N2.Name = "N2";
            N2.Size = new Size(352, 6);
            // 
            // menuExit
            // 
            menuExit.Name = "menuExit";
            menuExit.Size = new Size(355, 26);
            menuExit.Text = "Exit";
            menuExit.Click += menuExit_Click;
            // 
            // menuTopology
            // 
            menuTopology.DropDownItems.AddRange(new ToolStripItem[] { menuTopoRollback, menuTopoSettings, menuTopoSeparatorLayer1, menuTopoCreateTopology, menuTopoCreateFeatureLayer, menuTopoDeleteFeatureLayer, menuTopoSeparatorFeature1, menuTopoCreateFeature, menuTopoDeleteFeature, menuTopoAddElementsToFeature, menuTopoDeleteFeatureElement, menuTopoSeparatorFix1, menuTopoAutoFixImportErrors, menuTopoManualFixImportErrors });
            menuTopology.Name = "menuTopology";
            menuTopology.Size = new Size(86, 26);
            menuTopology.Text = "Topology";
            // 
            // menuTopoRollback
            // 
            menuTopoRollback.Name = "menuTopoRollback";
            menuTopoRollback.Size = new Size(289, 26);
            menuTopoRollback.Text = "Rollback";
            menuTopoRollback.Click += menuTopoRollback_Click;
            // 
            // menuTopoSettings
            // 
            menuTopoSettings.Name = "menuTopoSettings";
            menuTopoSettings.Size = new Size(289, 26);
            menuTopoSettings.Text = "Settings";
            menuTopoSettings.Click += menuTopoSettings_Click;
            // 
            // menuTopoSeparatorLayer1
            // 
            menuTopoSeparatorLayer1.Name = "menuTopoSeparatorLayer1";
            menuTopoSeparatorLayer1.Size = new Size(286, 6);
            // 
            // menuTopoCreateTopology
            // 
            menuTopoCreateTopology.Name = "menuTopoCreateTopology";
            menuTopoCreateTopology.Size = new Size(289, 26);
            menuTopoCreateTopology.Text = "Create topology...";
            menuTopoCreateTopology.Click += menuTopoCreateTopology_Click;
            // 
            // menuTopoCreateFeatureLayer
            // 
            menuTopoCreateFeatureLayer.Name = "menuTopoCreateFeatureLayer";
            menuTopoCreateFeatureLayer.Size = new Size(289, 26);
            menuTopoCreateFeatureLayer.Text = "Create feature layer...";
            menuTopoCreateFeatureLayer.Click += menuTopoCreateFeatureLayer_Click;
            // 
            // menuTopoDeleteFeatureLayer
            // 
            menuTopoDeleteFeatureLayer.Name = "menuTopoDeleteFeatureLayer";
            menuTopoDeleteFeatureLayer.Size = new Size(289, 26);
            menuTopoDeleteFeatureLayer.Text = "Delete feature layer...";
            menuTopoDeleteFeatureLayer.Click += menuTopoDeleteFeatureLayer_Click;
            // 
            // menuTopoSeparatorFeature1
            // 
            menuTopoSeparatorFeature1.Name = "menuTopoSeparatorFeature1";
            menuTopoSeparatorFeature1.Size = new Size(286, 6);
            // 
            // menuTopoCreateFeature
            // 
            menuTopoCreateFeature.Name = "menuTopoCreateFeature";
            menuTopoCreateFeature.Size = new Size(289, 26);
            menuTopoCreateFeature.Text = "Create feature(s)...";
            menuTopoCreateFeature.Click += menuTopoCreateFeature_Click;
            // 
            // menuTopoDeleteFeature
            // 
            menuTopoDeleteFeature.Name = "menuTopoDeleteFeature";
            menuTopoDeleteFeature.Size = new Size(289, 26);
            menuTopoDeleteFeature.Text = "Delete feature(s)...";
            menuTopoDeleteFeature.Click += menuTopoDeleteFeature_Click;
            // 
            // menuTopoAddElementsToFeature
            // 
            menuTopoAddElementsToFeature.Name = "menuTopoAddElementsToFeature";
            menuTopoAddElementsToFeature.Size = new Size(289, 26);
            menuTopoAddElementsToFeature.Text = "Add element(s) to feature...";
            menuTopoAddElementsToFeature.Click += menuTopoAddElementsToFeature_Click;
            // 
            // menuTopoDeleteFeatureElement
            // 
            menuTopoDeleteFeatureElement.Name = "menuTopoDeleteFeatureElement";
            menuTopoDeleteFeatureElement.Size = new Size(289, 26);
            menuTopoDeleteFeatureElement.Text = "Delete feature element...";
            menuTopoDeleteFeatureElement.Click += menuTopoDeleteFeatureElement_Click;
            // 
            // menuTopoSeparatorFix1
            // 
            menuTopoSeparatorFix1.Name = "menuTopoSeparatorFix1";
            menuTopoSeparatorFix1.Size = new Size(286, 6);
            // 
            // menuTopoAutoFixImportErrors
            // 
            menuTopoAutoFixImportErrors.Name = "menuTopoAutoFixImportErrors";
            menuTopoAutoFixImportErrors.Size = new Size(289, 26);
            menuTopoAutoFixImportErrors.Text = "Auto fix import errors...";
            menuTopoAutoFixImportErrors.Click += menuTopoAutoFixImportErrors_Click;
            // 
            // menuTopoManualFixImportErrors
            // 
            menuTopoManualFixImportErrors.Name = "menuTopoManualFixImportErrors";
            menuTopoManualFixImportErrors.Size = new Size(289, 26);
            menuTopoManualFixImportErrors.Text = "Merge feature into neighbor...";
            menuTopoManualFixImportErrors.Click += menuTopoManualFixImportErrors_Click;
            // 
            // lstImage
            // 
            lstImage.ColorDepth = ColorDepth.Depth32Bit;
            lstImage.ImageStream = (ImageListStreamer)resources.GetObject("lstImage.ImageStream");
            lstImage.TransparentColor = Color.Transparent;
            lstImage.Images.SetKeyName(0, "FullExtent");
            lstImage.Images.SetKeyName(1, "Zoom");
            lstImage.Images.SetKeyName(2, "Drag");
            lstImage.Images.SetKeyName(3, "Select");
            lstImage.Images.SetKeyName(4, "Edit");
            lstImage.Images.SetKeyName(5, "Undo");
            lstImage.Images.SetKeyName(6, "Redo");
            lstImage.Images.SetKeyName(7, "RevertShape");
            lstImage.Images.SetKeyName(8, "Delete");
            lstImage.Images.SetKeyName(9, "AddShape");
            lstImage.Images.SetKeyName(10, "SnapVertex");
            lstImage.Images.SetKeyName(11, "SnapEdge");
            lstImage.Images.SetKeyName(12, "SnapVertexOrEdge");
            lstImage.Images.SetKeyName(13, "SnapEndPoint");
            lstImage.Images.SetKeyName(14, "SnapMidpoint");
            lstImage.Images.SetKeyName(15, "SnapPerpendicular");
            // 
            // toolbar
            // 
            toolbar.AutoSize = false;
            toolbar.GripStyle = ToolStripGripStyle.Hidden;
            toolbar.ImageList = lstImage;
            toolbar.ImageScalingSize = new Size(20, 20);
            toolbar.Items.AddRange(new ToolStripItem[] { btnFullExtent, btnZoom, btnDragMode, btnSelectMode, sepEdit, btnEditMode, btnUndo, btnRedo, btnRevertShape, btnDelete, sepAdd, btnAddShape, lblSnapLayer, btnSnapLayers, btnSnapType });
            toolbar.Location = new Point(0, 30);
            toolbar.Name = "toolbar";
            toolbar.Padding = new Padding(0);
            toolbar.Size = new Size(1250, 36);
            toolbar.TabIndex = 2;
            // 
            // btnFullExtent
            // 
            btnFullExtent.AutoSize = false;
            btnFullExtent.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnFullExtent.ImageIndex = 0;
            btnFullExtent.Margin = new Padding(0);
            btnFullExtent.Name = "btnFullExtent";
            btnFullExtent.Size = new Size(28, 28);
            btnFullExtent.ToolTipText = "Full Extent - zoom the map to show all layers";
            btnFullExtent.Click += btnFullExtent_Click;
            // 
            // btnZoom
            // 
            btnZoom.AutoSize = false;
            btnZoom.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnZoom.ImageIndex = 1;
            btnZoom.Margin = new Padding(0);
            btnZoom.Name = "btnZoom";
            btnZoom.Size = new Size(28, 28);
            btnZoom.ToolTipText = "Zoom Mode - drag a rectangle on the map to zoom into it (the mouse wheel zooms in every mode)";
            btnZoom.Click += btnZoom_Click;
            // 
            // btnDragMode
            // 
            btnDragMode.AutoSize = false;
            btnDragMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDragMode.ImageIndex = 2;
            btnDragMode.Margin = new Padding(0);
            btnDragMode.Name = "btnDragMode";
            btnDragMode.Size = new Size(28, 28);
            btnDragMode.ToolTipText = "Drag Mode - drag the map to pan it";
            btnDragMode.Click += btnDragMode_Click;
            // 
            // btnSelectMode
            // 
            btnSelectMode.AutoSize = false;
            btnSelectMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSelectMode.ImageIndex = 3;
            btnSelectMode.Margin = new Padding(0);
            btnSelectMode.Name = "btnSelectMode";
            btnSelectMode.Size = new Size(28, 28);
            btnSelectMode.ToolTipText = "Select Mode - click a shape to select it and show its attributes, Ctrl+click to add or remove shapes from the selection";
            btnSelectMode.Click += btnSelectMode_Click;
            // 
            // sepEdit
            // 
            sepEdit.Name = "sepEdit";
            sepEdit.Size = new Size(6, 36);
            // 
            // btnEditMode
            // 
            btnEditMode.AutoSize = false;
            btnEditMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditMode.ImageIndex = 4;
            btnEditMode.Margin = new Padding(0);
            btnEditMode.Name = "btnEditMode";
            btnEditMode.Size = new Size(28, 28);
            btnEditMode.ToolTipText = "Edit Mode - click a shape to edit its geometry, right-click to finish editing";
            btnEditMode.Click += btnEditMode_Click;
            // 
            // btnUndo
            // 
            btnUndo.AutoSize = false;
            btnUndo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnUndo.ImageIndex = 5;
            btnUndo.Margin = new Padding(0);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(28, 28);
            btnUndo.ToolTipText = "Undo (Ctrl+Z) - undo the last change to the shape being edited or added";
            btnUndo.Click += btnUndo_Click;
            // 
            // btnRedo
            // 
            btnRedo.AutoSize = false;
            btnRedo.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRedo.ImageIndex = 6;
            btnRedo.Margin = new Padding(0);
            btnRedo.Name = "btnRedo";
            btnRedo.Size = new Size(28, 28);
            btnRedo.ToolTipText = "Redo (Ctrl+Y) - redo the last undone change to the shape being edited or added";
            btnRedo.Click += btnRedo_Click;
            // 
            // btnRevertShape
            // 
            btnRevertShape.AutoSize = false;
            btnRevertShape.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRevertShape.ImageIndex = 7;
            btnRevertShape.Margin = new Padding(0);
            btnRevertShape.Name = "btnRevertShape";
            btnRevertShape.Size = new Size(28, 28);
            btnRevertShape.ToolTipText = "Revert Shape - discard all changes to the edited shape and finish editing";
            btnRevertShape.Click += btnRevertShape_Click;
            // 
            // btnDelete
            // 
            btnDelete.AutoSize = false;
            btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDelete.ImageIndex = 8;
            btnDelete.Margin = new Padding(0);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(28, 28);
            btnDelete.ToolTipText = "Delete - delete the edited shape";
            btnDelete.Click += btnDelete_Click;
            // 
            // sepAdd
            // 
            sepAdd.Name = "sepAdd";
            sepAdd.Size = new Size(6, 36);
            // 
            // btnAddShape
            // 
            btnAddShape.AutoSize = false;
            btnAddShape.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddShape.ImageIndex = 9;
            btnAddShape.Margin = new Padding(0);
            btnAddShape.Name = "btnAddShape";
            btnAddShape.Size = new Size(28, 28);
            btnAddShape.ToolTipText = "Add Shape - add shapes to the layer selected in the legend\nClick on the map to start a shape, right-click to finish it\nHold Ctrl while clicking to start the next edge from the last edge instead of from the closest node\nStays on until another mode or another layer in the legend is chosen";
            btnAddShape.Click += btnAddShape_Click;
            // 
            // lblSnapLayer
            // 
            lblSnapLayer.Margin = new Padding(6, 0, 2, 0);
            lblSnapLayer.Name = "lblSnapLayer";
            lblSnapLayer.Size = new Size(63, 36);
            lblSnapLayer.Text = "Snap to:";
            // 
            // btnSnapLayers
            // 
            btnSnapLayers.AutoSize = false;
            btnSnapLayers.AutoToolTip = false;
            btnSnapLayers.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnSnapLayers.Name = "btnSnapLayers";
            btnSnapLayers.Size = new Size(150, 23);
            btnSnapLayers.Text = "No snapping";
            btnSnapLayers.TextAlign = ContentAlignment.MiddleLeft;
            btnSnapLayers.ToolTipText = "Snap to - check the layers whose shapes the vertices of added and edited shapes snap to (the own layer of the shape is included), with no layer checked snapping is off";
            btnSnapLayers.DropDownOpening += btnSnapLayers_DropDownOpening;
            //
            // btnSnapType
            //
            btnSnapType.AutoSize = false;
            btnSnapType.AutoToolTip = false;
            btnSnapType.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSnapType.ImageIndex = 10;
            btnSnapType.Margin = new Padding(4, 0, 0, 0);
            btnSnapType.Name = "btnSnapType";
            btnSnapType.Size = new Size(38, 28);
            // 
            // pnlGIS
            // 
            pnlGIS.Controls.Add(gisLegend);
            pnlGIS.Controls.Add(gisAttributes);
            pnlGIS.Dock = DockStyle.Left;
            pnlGIS.Location = new Point(0, 66);
            pnlGIS.Margin = new Padding(4, 4, 4, 4);
            pnlGIS.Name = "pnlGIS";
            pnlGIS.Padding = new Padding(1);
            pnlGIS.Size = new Size(250, 795);
            pnlGIS.TabIndex = 1;
            // 
            // gisLegend
            // 
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            gisLegend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            gisLegend.Dock = DockStyle.Fill;
            gisLegend.GIS_Viewer = GIS;
            gisLegend.Location = new Point(1, 1);
            gisLegend.Margin = new Padding(4, 4, 4, 4);
            gisLegend.Name = "gisLegend";
            gisLegend.Options = TGIS_ControlLegendOption.AllowMove | TGIS_ControlLegendOption.AllowActive | TGIS_ControlLegendOption.AllowExpand | TGIS_ControlLegendOption.AllowParams | TGIS_ControlLegendOption.AllowSelect | TGIS_ControlLegendOption.ShowSubLayers | TGIS_ControlLegendOption.AllowParamsVisible;
            gisLegend.Size = new Size(248, 416);
            gisLegend.TabIndex = 1;
            gisLegend.LayerSelectEvent += gisLegend_LayerSelectEvent;
            // 
            // GIS
            // 
            GIS.AutoStyle = false;
            GIS.BackColor = Color.FromArgb(255, 255, 255);
            GIS.Dock = DockStyle.Fill;
            GIS.Level = 1D;
            GIS.Location = new Point(250, 66);
            GIS.Margin = new Padding(4, 4, 4, 4);
            GIS.Name = "GIS";
            GIS.SelectionColor = Color.FromArgb(255, 0, 0);
            GIS.Size = new Size(1000, 795);
            GIS.TabIndex = 0;
            GIS.TiledPaint = false;
            GIS.BusyEvent += GIS_BusyEvent;
            GIS.LayerDeleteEvent += GIS_LayerDeleteEvent;
            GIS.ProjectCloseEvent += GIS_ProjectCloseEvent;
            GIS.ModeChangeEvent += GIS_ModeChangeEvent;
            GIS.MouseUp += GIS_MouseUp;
            // 
            // gisAttributes
            // 
            gisAttributes.Dock = DockStyle.Bottom;
            gisAttributes.Location = new Point(1, 417);
            gisAttributes.Margin = new Padding(4, 4, 4, 4);
            gisAttributes.Name = "gisAttributes";
            gisAttributes.Size = new Size(248, 377);
            gisAttributes.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(progressbar);
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(lblProgress);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 861);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(1);
            panel1.Size = new Size(1250, 50);
            panel1.TabIndex = 3;
            // 
            // progressbar
            // 
            progressbar.Dock = DockStyle.Fill;
            progressbar.Location = new Point(50, 1);
            progressbar.Margin = new Padding(4, 4, 4, 4);
            progressbar.Name = "progressbar";
            progressbar.Size = new Size(1079, 48);
            progressbar.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Dock = DockStyle.Right;
            btnCancel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            btnCancel.Location = new Point(1129, 1);
            btnCancel.Margin = new Padding(4, 4, 4, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 48);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Dock = DockStyle.Left;
            lblProgress.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            lblProgress.Location = new Point(1, 1);
            lblProgress.Margin = new Padding(4, 0, 4, 0);
            lblProgress.MinimumSize = new Size(0, 48);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(49, 48);
            lblProgress.TabIndex = 2;
            lblProgress.Text = "0%";
            lblProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // WinForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1250, 911);
            Controls.Add(GIS);
            Controls.Add(pnlGIS);
            Controls.Add(panel1);
            Controls.Add(toolbar);
            Controls.Add(mnuMain);
            Font = new Font("Segoe UI", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mnuMain;
            Margin = new Padding(4, 4, 4, 4);
            Name = "WinForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TatukGIS Samples - TopologyLayer";
            Load += WinForm_Load;
            mnuMain.ResumeLayout(false);
            mnuMain.PerformLayout();
            toolbar.ResumeLayout(false);
            toolbar.PerformLayout();
            pnlGIS.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #if NET6_0_OR_GREATER
              ApplicationConfiguration.Initialize();
            #else
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
            #endif
            Application.Run(new WinForm());
        }

        /// <summary>
        /// Initialises the topology editor application. Sets up the topology tool
        /// with connections to the map viewer, layer legend, and attribute inspector,
        /// configures the viewer for unrestricted panning and populates all topology
        /// menu captions with localized resource strings.
        /// </summary>
        private void WinForm_Load(object sender, EventArgs e)
        {
            // On .NET topology stores are registered lazily (upon the first layer
            // creation), so register them explicitly - otherwise creating a topology
            // before any layer is opened fails with "Topology process cancelled"
            TatukGIS.NDK.__Global.SelfRegisterTopoStores();

            topoTool = new TGIS_TopoTool(this, GIS, gisLegend, gisAttributes);

            GIS.RestrictedDrag = false;

            menuTopology.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_TOPOLOGY);
            menuTopoRollback.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_ROLLBACK);
            menuTopoSettings.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_SETTINGS);
            menuTopoCreateTopology.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_CREATE_TOPO);
            menuTopoCreateFeatureLayer.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_CREATE_FEATURE_LAYER);
            menuTopoDeleteFeatureLayer.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_DELETE_FEATURE_LAYER);
            menuTopoCreateFeature.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_CREATE_FEATURES);
            menuTopoDeleteFeature.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_DELETE_FEATURES);
            menuTopoAddElementsToFeature.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_ADD_ELEMENTS);
            menuTopoDeleteFeatureElement.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_DELETE_ELEMENT);
            menuTopoAutoFixImportErrors.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_AUTO_FIX);
            menuTopoManualFixImportErrors.Text = TatukGIS.NDK.__Global._rsrc(TatukGIS.NDK.__Global.GIS_RS_TOPO_MENU_MANUAL_FIX);

            initEditingToolbar();
        }

        /// <summary>
        /// Commits the current shape edit operation. Disables edit-related toolbar
        /// buttons (undo, redo, revert, delete) after edit ends. Returns false if
        /// the edit cannot be committed (validation error).
        /// </summary>
        private bool endEdit()
        {
            if (!GIS.Editor.TryEndEdit())
                return false;

            btnUndo.Enabled = false;
            btnRedo.Enabled = false;
            btnRevertShape.Enabled = false;
            btnDelete.Enabled = false;

            return true;
        }

        /// <summary>
        /// Asks whether to save pending changes and saves all layers if confirmed.
        /// </summary>
        private void trySave()
        {
            if (!GIS.MustSave())
                return;

            if (MessageBox.Show("Save changes?", "Confirm",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question
                               ) == DialogResult.Yes)
                GIS.SaveAll();
        }

        // =====================================================================
        // Toolbar
        // =====================================================================

        /// <summary>
        /// Fits the map viewport to show all loaded layers.
        /// </summary>
        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            GIS.FullExtent();
        }

        /// <summary>
        /// Switches to zoom mode for interactive scale control.
        /// </summary>
        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (!finishAdding())
                return;

            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        /// <summary>
        /// Switches to pan/drag mode for interactive map panning.
        /// </summary>
        private void btnDragMode_Click(object sender, EventArgs e)
        {
            if (!finishAdding())
                return;

            GIS.Mode = TGIS_ViewerMode.Drag;
        }

        /// <summary>
        /// Switches to shape selection mode after ending any active edit.
        /// </summary>
        private void btnSelectMode_Click(object sender, EventArgs e)
        {
            if (!endEdit())
                return;

            stopAdding();
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        /// <summary>
        /// Switches to edit mode for interactive geometry editing.
        /// </summary>
        private void btnEditMode_Click(object sender, EventArgs e)
        {
            if (!finishAdding())
                return;

            GIS.Mode = TGIS_ViewerMode.Edit;
        }

        /// <summary>
        /// Reverts the last shape edit operation.
        /// </summary>
        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (GIS.Editor.CanUndo)
                GIS.Editor.Undo();
        }

        /// <summary>
        /// Restores the last undone shape edit operation.
        /// </summary>
        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (GIS.Editor.CanRedo)
                GIS.Editor.Redo();
        }

        /// <summary>
        /// Reverts the current shape to its original state, discarding all edits.
        /// </summary>
        private void btnRevertShape_Click(object sender, EventArgs e)
        {
            GIS.Editor.RevertShape();
            btnSelectMode_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Deletes the currently edited shape and returns to select mode.
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            GIS.Editor.DeleteShape();
            btnSelectMode_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Turns the Add Shape mode on for the layer selected in the legend. The
        /// mode stays on, so that shapes (e.g. a sequence of nodes or edges) can be
        /// added one after another, until another mode is chosen or the button is
        /// pressed again.
        /// </summary>
        private void btnAddShape_Click(object sender, EventArgs e)
        {
            // pressed again - leave the Add Shape mode
            if (btnAddShape.Checked)
            {
                btnSelectMode_Click(this, EventArgs.Empty);
                return;
            }

            if (!endEdit())
                return;

            if (gisLegend.GIS_Layer == null)
            {
                MessageBox.Show("Select the layer for the new shape in the legend first.",
                                "Add Shape", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            editLayer = gisLegend.GIS_Layer;
            setAddingShape(true);
            GIS.Mode = TGIS_ViewerMode.Edit;
        }

        // =====================================================================
        // Progress panel
        // =====================================================================

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // === WORKFLOW: Abort Long-Running Operation ===
            abort = true;
        }

        /// <summary>
        /// Progress callback during long-running topology operations. Updates the
        /// progress bar and percentage label. User can abort via the Cancel button
        /// (sets abort flag).
        /// </summary>
        private void GIS_BusyEvent(object sender, TGIS_BusyEventArgs e)
        {
            int percent = 0;

            if (e.Pos <= 0)
                abort = false;

            // Pos and EndPos are -1 when processing ends
            if ((e.Pos > 0) && (e.EndPos > 0))
                percent = (int)Math.Min(e.Pos * 100 / e.EndPos, 100);

            progressbar.Value = percent;
            lblProgress.Text = percent.ToString() + "%";
            e.Abort = abort;

            // keep the progress panel painted and the Cancel button responsive
            Application.DoEvents();
        }

        // =====================================================================
        // Map interaction
        // =====================================================================

        /// <summary>
        /// Map click handler for both shape selection (SELECT mode) and geometry
        /// editing (EDIT mode). Right-click finishes the added shape in the Add
        /// Shape mode and switches to SELECT mode otherwise. Ctrl-click toggles
        /// shape selection.
        ///
        /// Algorithm:
        ///   1. Reject empty maps; handle right-click.
        ///   2. Convert screen click position to map coordinates and hit-test for shape.
        ///   3. In SELECT mode: Ctrl toggles selection; single-click selects one shape
        ///      and shows its attributes.
        ///   4. In EDIT mode: Click creates new shape in the Add Shape mode, or clicks
        ///      existing shape to edit its geometry. Enables undo/redo buttons.
        /// </summary>
        private void GIS_MouseUp(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_Shape shp;

            if (GIS.IsEmpty)
                return;

            if (e.Button == MouseButtons.Right)
            {
                // finish the added shape, but stay in the Add Shape mode
                if (btnAddShape.Checked)
                    finishShape();
                else
                    btnSelectMode_Click(this, EventArgs.Empty);
                return;
            }

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            shp = (TGIS_Shape)GIS.Locate(ptg, 5 / GIS.Zoom);

            if (GIS.Mode == TGIS_ViewerMode.Select)
            {
                if (shp == null)
                    return;

                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    shp.IsSelected = !shp.IsSelected;
                    gisAttributes.ShowSelected(shp.Layer);
                }
                else
                {
                    shp.Layer.DeselectAll();
                    shp.IsSelected = !shp.IsSelected;
                    gisAttributes.ShowShape(shp);
                }

                gisLegend.GIS_Layer = shp.Layer;
            }
            else if (GIS.Mode == TGIS_ViewerMode.Edit)
            {
                if (GIS.Editor.CurrentShape != null)
                    return;

                if (btnAddShape.Checked)
                {
                    // Create a new shape in the layer chosen with the Add Shape button
                    addShape(ptg);
                }
                else
                {
                    // Edit existing shape geometry
                    if (!GIS.Editor.TryEditShape(shp, 0, ptg))
                        return;

                    btnRedo.Enabled = true;
                    btnUndo.Enabled = true;
                    btnRevertShape.Enabled = true;
                    btnDelete.Enabled = true;
                }

                GIS.InvalidateEditor(true);
            }
        }

        // =====================================================================
        // File menu
        // =====================================================================

        /// <summary>
        /// Adds a new layer to the topology project. Opens file dialog, creates the
        /// layer object, configures it, and updates the map extent and legend.
        ///
        /// Algorithm:
        ///   1. Open file dialog to select a layer file.
        ///   2. Create layer object using factory function GisCreateLayer.
        ///   3. Load configuration from saved .ttkgp file if available.
        ///   4. Add layer to viewer; zoom to full extent if first layer, else just refresh.
        ///   5. Update legend to show new layer.
        /// </summary>
        private void menuAdd_Click(object sender, EventArgs e)
        {
            TGIS_Layer layer = null;
            string filename;

            if (dlgFileOpen.ShowDialog() != DialogResult.OK)
                return;
            filename = dlgFileOpen.FileName;

            try
            {
                layer = TGIS_Utils.GisCreateLayer(Path.GetFileName(filename), filename);
                if (layer != null)
                {
                    layer.ReadConfig();
                    GIS.Add(layer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot add the file:" + Environment.NewLine +
                                filename + Environment.NewLine +
                                ex.Message);
            }

            if (GIS.Items.Count == 1)
                GIS.FullExtent();
            else
                GIS.InvalidateWholeMap();

            gisLegend.GIS_Layer = layer;
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            if (dlgFileOpen.ShowDialog() != DialogResult.OK)
                return;

            GIS.Close();
            try
            {
                GIS.Open(dlgFileOpen.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open the file:" + Environment.NewLine +
                                dlgFileOpen.FileName + Environment.NewLine +
                                ex.Message);
            }
        }

        private void menuOpenLineTopoSample_Click(object sender, EventArgs e)
        {
            string samples = TGIS_Utils.GisSamplesDataDirDownload("TopologyLayer.1");
            GIS.Open(samples + @"Samples\TopologyLayer\LINETOPOLOGY\OIL.ttkproject");
        }

        private void menuOpenTopoPolygonSample_Click(object sender, EventArgs e)
        {
            string samples = TGIS_Utils.GisSamplesDataDirDownload("TopologyLayer.1");
            GIS.Open(samples + @"Samples\TopologyLayer\POLYGONTOPOLOGY\CLC.ttkproject");
        }

        private void menuSelectVisible_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv = gisLegend.GIS_Layer as TGIS_LayerVector;
            if (lv == null)
                return;

            foreach (TGIS_Shape shp in lv.Loop(GIS.VisibleExtent))
            {
                if (!shp.IsHidden)
                    shp.IsSelected = true;
            }
        }

        private void menuSelectAll_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv = gisLegend.GIS_Layer as TGIS_LayerVector;
            if (lv == null)
                return;

            GIS.Lock();
            try
            {
                foreach (TGIS_Shape shp in lv.Loop())
                {
                    if (!shp.IsHidden)
                        shp.IsSelected = true;
                }
            }
            finally
            {
                GIS.Unlock();
            }
        }

        private void menuDeselectAll_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv = gisLegend.GIS_Layer as TGIS_LayerVector;
            if (GIS.IsEmpty || lv == null)
                return;

            lv.DeselectAll();
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            trySave();
        }

        private void menuClose_Click(object sender, EventArgs e)
        {
            trySave();
            GIS.Close();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            trySave();
            Close();
        }

        // =====================================================================
        // Topology menu
        // =====================================================================

        private void menuTopoRollback_Click(object sender, EventArgs e)
        {
            topoTool.Rollback();
        }

        private void menuTopoSettings_Click(object sender, EventArgs e)
        {
            topoTool.OpenSettingsForm();
        }

        private void menuTopoCreateTopology_Click(object sender, EventArgs e)
        {
            topoTool.RunTool(TGIS_TopoTools.CreateTopology);
        }

        private void menuTopoCreateFeatureLayer_Click(object sender, EventArgs e)
        {
            topoTool.RunTool(TGIS_TopoTools.CreateFeatureLayer);
        }

        private void menuTopoDeleteFeatureLayer_Click(object sender, EventArgs e)
        {
            topoTool.RunTool(TGIS_TopoTools.DeleteFeatureLayer);
        }

        private void menuTopoCreateFeature_Click(object sender, EventArgs e)
        {
            topoTool.RunTool(TGIS_TopoTools.CreateTopoFeature);
        }

        private void menuTopoDeleteFeature_Click(object sender, EventArgs e)
        {
            topoTool.RunTool(TGIS_TopoTools.DeleteTopoFeature);
        }

        private void menuTopoAddElementsToFeature_Click(object sender, EventArgs e)
        {
            topoTool.RunTool(TGIS_TopoTools.AddElementsToTopoFeature);
        }

        private void menuTopoDeleteFeatureElement_Click(object sender, EventArgs e)
        {
            topoTool.RunTool(TGIS_TopoTools.DeleteTopoFeatureElement);
        }

        private void menuTopoAutoFixImportErrors_Click(object sender, EventArgs e)
        {
            topoTool.RunTool(TGIS_TopoTools.AutoFixTopoImportErrors);
        }

        private void menuTopoManualFixImportErrors_Click(object sender, EventArgs e)
        {
            topoTool.RunTool(TGIS_TopoTools.ManualFixTopoImportErrors);
        }

        // =====================================================================
        // Edit and Add Shape modes
        // Mode buttons, the Add Shape mode kept on for sequences of shapes,
        // Ctrl+Z / Ctrl+Y, mouse wheel zoom and ending the edit when the
        // project, a layer or the legend selection changes
        // =====================================================================

        /// <summary>
        /// Sets up the editing toolbar (snap type and snap layer lists, mode
        /// buttons) and the mouse wheel zoom. Called when the form is loaded.
        /// </summary>
        private void initEditingToolbar()
        {
            ToolStripMenuItem item;

            // the designer does not list MouseWheel among the viewer events
            GIS.MouseWheel += new MouseEventHandler(GIS_MouseWheel);

            // snap types are shown as pictures, described by tooltips
            btnSnapType.DropDown.ShowItemToolTips = true;
            for (int i = 0; i < snapTypes.Length; i++)
            {
                item = new ToolStripMenuItem();
                item.DisplayStyle = ToolStripItemDisplayStyle.Image;
                item.Image = lstImage.Images[SNAP_TYPE_IMAGE + i];
                item.ToolTipText = snapTypeHints[i];
                item.Click += new EventHandler(snapTypeItem_Click);
                btnSnapType.DropDownItems.Add(item);
            }
            setSnapType(0);

            // keep the snap layer list open while layers are being checked
            btnSnapLayers.DropDown.Closing += new ToolStripDropDownClosingEventHandler(btnSnapLayers_DropDownClosing);

            // snapping is off until some snap layers are checked
            applySnapLayers();

            updateModeButtons();
        }

        /// <summary>
        /// Presses the toolbar button of the current viewer mode. In the Add Shape
        /// mode, the Add Shape button is pressed instead of the Edit Mode one.
        /// </summary>
        private void updateModeButtons()
        {
            btnZoom.Checked       = (GIS.Mode == TGIS_ViewerMode.Zoom);
            btnDragMode.Checked   = (GIS.Mode == TGIS_ViewerMode.Drag);
            btnSelectMode.Checked = (GIS.Mode == TGIS_ViewerMode.Select);
            btnEditMode.Checked   = (GIS.Mode == TGIS_ViewerMode.Edit) && !btnAddShape.Checked;
        }

        /// <summary>
        /// Keeps the Add Shape button pressed while the Add Shape mode is on.
        /// </summary>
        private void setAddingShape(bool adding)
        {
            btnAddShape.Checked = adding;
            updateModeButtons();
        }

        /// <summary>
        /// Turns the Add Shape mode off.
        /// </summary>
        private void stopAdding()
        {
            editLayer = null;
            setAddingShape(false);
        }

        /// <summary>
        /// Leaves the Add Shape mode, finishing the shape being added. Returns
        /// false if the shape cannot be finished (validation error).
        /// </summary>
        private bool finishAdding()
        {
            if (!btnAddShape.Checked)
                return true;

            if (!endEdit())
                return false;

            stopAdding();
            return true;
        }

        /// <summary>
        /// Cancels the current shape edit (discarding its changes) and a pending
        /// "Add Shape" operation, then switches the viewer to select mode.
        /// </summary>
        private void cancelEdit()
        {
            if (GIS.Editor.InEdit)
                GIS.Editor.RevertShape();

            endEdit();
            stopAdding();
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        /// <summary>
        /// Starts a new shape in the Add Shape mode layer at the clicked point.
        /// </summary>
        private void addShape(TGIS_Point _ptg)
        {
            bool end_edit_required;

            if (!GIS.Editor.TryCreateShape(editLayer,
                                           _ptg,
                                           TGIS_ShapeType.Unknown,
                                           out end_edit_required))
            {
                // the layer does not accept new shapes
                btnSelectMode_Click(this, EventArgs.Empty);
                return;
            }

            if (end_edit_required)
            {
                // e.g. a topology node - finished at once, ready for the next one
                endEdit();
                return;
            }

            btnUndo.Enabled = true;
            btnRedo.Enabled = true;
        }

        /// <summary>
        /// Finishes the shape being added and keeps the Add Shape mode on for the
        /// next one.
        /// </summary>
        private void finishShape()
        {
            if (GIS.Editor.InEdit)
                endEdit();
        }

        /// <summary>
        /// Handles Ctrl+Z (undo) and Ctrl+Y (redo) while a shape is being edited
        /// or added. The map viewer does not take the keyboard focus, so the
        /// shortcuts are processed on the form level.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // leave the shortcuts to text boxes, e.g. when editing attributes
            if (GIS.Editor.InEdit && !(ActiveControl is TextBoxBase))
            {
                if (keyData == (Keys.Control | Keys.Z))
                {
                    btnUndo_Click(this, EventArgs.Empty);
                    return true;
                }

                if (keyData == (Keys.Control | Keys.Y))
                {
                    btnRedo_Click(this, EventArgs.Empty);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Keeps the mode buttons in sync with the viewer mode, which can also be
        /// changed by the topology tool (e.g. rollback switches to select mode).
        /// </summary>
        private void GIS_ModeChangeEvent(object sender, EventArgs e)
        {
            updateModeButtons();
        }

        /// <summary>
        /// Called when the current project is closed, also when another project
        /// is opened. Cancels any shape edit or pending "Add Shape" operation while
        /// the project layers still exist - otherwise the editor would keep working
        /// on the released layers and crash on the next map click or end of editing.
        /// </summary>
        private void GIS_ProjectCloseEvent(object sender, EventArgs e)
        {
            cancelEdit();

            // the snap layers belong to the closed project
            snapLayerNames.Clear();
            applySnapLayers();
        }

        /// <summary>
        /// Removes a layer which is being deleted from the snap layers, so that
        /// the editor does not keep snapping to a released layer.
        /// </summary>
        private void GIS_LayerDeleteEvent(object sender, TGIS_LayerEventArgs e)
        {
            // the layer of the added or edited shape is going away (e.g. deleted
            // by a topology tool) - stop working on it before it is released
            if ((e.Layer == editLayer) || (GIS.Editor.InEdit && (GIS.Editor.Layer == e.Layer)))
                cancelEdit();

            if (snapLayerNames.Remove(e.Layer.Name))
                applySnapLayers();
        }

        /// <summary>
        /// Selecting another layer in the legend finishes the shape being edited
        /// or added and switches the Edit or Add Shape mode to select mode, so that
        /// shapes are not added to a layer which is no longer selected.
        /// </summary>
        private void gisLegend_LayerSelectEvent(object sender, TGIS_LayerEventArgs e)
        {
            TGIS_LayerAbstract layer = null;

            if (!btnAddShape.Checked && (GIS.Mode != TGIS_ViewerMode.Edit))
                return;

            // layer of the added or edited shape
            if (btnAddShape.Checked)
                layer = editLayer;
            else if (GIS.Editor.InEdit)
                layer = GIS.Editor.Layer;

            // the same layer selected again
            if ((layer != null) && (e.Layer == layer))
                return;

            btnSelectMode_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Zooms the map in (wheel rolled forward) or out (wheel rolled back),
        /// keeping the point under the cursor in place. Works in every mode, also
        /// while a shape is being edited or added.
        /// </summary>
        private void GIS_MouseWheel(object sender, MouseEventArgs e)
        {
            if (GIS.IsEmpty)
                return;

            // 1.25x per wheel notch (Delta is 120 per notch, less on smooth wheels)
            GIS.ZoomBy(Math.Pow(1.25, e.Delta / 120.0), e.X, e.Y);
        }

        // =====================================================================
        // Snapping
        // Snap to layers (checked in a list) and snap type (chosen by picture)
        // =====================================================================

        /// <summary>
        /// Fills the snap layer list with the vector layers of the current project,
        /// keeping the checked ones.
        /// </summary>
        private void fillSnapLayers()
        {
            TGIS_Layer layer;
            ToolStripMenuItem item;

            btnSnapLayers.DropDownItems.Clear();

            for (int i = 0; i < GIS.Items.Count; i++)
            {
                layer = (TGIS_Layer)GIS.Items[i];

                // skip helper layers, e.g. the ones of the topology tool
                if (!(layer is TGIS_LayerVector) || layer.HideFromLegend)
                    continue;

                item = new ToolStripMenuItem(layer.Name);
                item.CheckOnClick = true;
                item.Checked = snapLayerNames.Contains(layer.Name);
                item.CheckedChanged += new EventHandler(snapLayerItem_CheckedChanged);
                btnSnapLayers.DropDownItems.Add(item);
            }

            if (btnSnapLayers.DropDownItems.Count == 0)
            {
                item = new ToolStripMenuItem("No vector layers");
                item.Enabled = false;
                btnSnapLayers.DropDownItems.Add(item);
            }
        }

        /// <summary>
        /// Applies the checked snap layers to the editor. With no layer checked
        /// snapping is turned off. The layer of the edited shape is added to the
        /// snap layers by the editor itself when editing starts.
        /// </summary>
        private void applySnapLayers()
        {
            GIS.Editor.ClearSnapLayers();
            GIS.Editor.BlockSnapping = (snapLayerNames.Count == 0);

            foreach (string name in snapLayerNames)
                GIS.Editor.AddSnapLayer(GIS.Get(name));

            if (snapLayerNames.Count == 0)
                btnSnapLayers.Text = NO_SNAPPING;
            else
                btnSnapLayers.Text = string.Join(", ", snapLayerNames);
        }

        /// <summary>
        /// Refreshes the snap layer list, as layers may come and go with projects
        /// and topology operations.
        /// </summary>
        private void btnSnapLayers_DropDownOpening(object sender, EventArgs e)
        {
            fillSnapLayers();
        }

        /// <summary>
        /// Keeps the snap layer list open while layers are being checked.
        /// </summary>
        private void btnSnapLayers_DropDownClosing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
                e.Cancel = true;
        }

        /// <summary>
        /// Adds or removes the layer of the clicked item from the snap layers.
        /// </summary>
        private void snapLayerItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            if (item.Checked)
                snapLayerNames.Add(item.Text);
            else
                snapLayerNames.Remove(item.Text);

            applySnapLayers();
        }

        /// <summary>
        /// Applies the snap type of the clicked picture.
        /// </summary>
        private void snapTypeItem_Click(object sender, EventArgs e)
        {
            setSnapType(btnSnapType.DropDownItems.IndexOf((ToolStripItem)sender));
        }

        /// <summary>
        /// Applies the snap type to the editor and shows its picture on the toolbar.
        /// </summary>
        private void setSnapType(int index)
        {
            GIS.Editor.SnapType = snapTypes[index];
            btnSnapType.ImageIndex = SNAP_TYPE_IMAGE + index;
            btnSnapType.ToolTipText = "Snap type: " + snapTypeHints[index];

            for (int i = 0; i < btnSnapType.DropDownItems.Count; i++)
                ((ToolStripMenuItem)btnSnapType.DropDownItems[i]).Checked = (i == index);
        }
    }
}
