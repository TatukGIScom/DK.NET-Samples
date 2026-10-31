// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// TopologyTutorial Sample - a step-by-step introduction to the low-level
// topology API (C#/.NET WinForms): building topology primitives, editing them
// while topological relationships are maintained automatically, and composing
// feature layers (hierarchical, levels 0-3) on top of those primitives.
//
// Topology model in a nutshell:
//   A topology does not store geometry per feature. Instead, it stores a set
//   of shared, non-overlapping primitives:
//     NODE - a point; an isolated point or the start/end point of an edge,
//     EDGE - a line between exactly two nodes (start node and end node),
//            edges never cross each other, they can only meet at nodes,
//     FACE - an area bounded by a closed ring of edges; faces are never
//            created by hand, they are derived automatically whenever edges
//            close a ring (and updated whenever edges change).
//   Because neighbouring objects share the same primitives, editing a shared
//   node or edge updates every object that uses it - there are no gaps,
//   overlaps or "sliver" polygons between neighbours.
//
//   Features (the objects a user actually works with: parcels, roads,
//   hydrants, ...) are stored in feature layers and reference primitives:
//     level 0     - features composed directly of primitives
//                   (polygon -> faces, line -> edges, point -> nodes),
//     level 1..n  - hierarchical features composed of features from the
//                   layer one level below (e.g. districts made of parcels).
//   Changing a primitive therefore automatically changes all features on all
//   levels that are built from it.
//
// What the sample shows (each numbered button is one step):
//   [0]      obtaining TGIS_TopoEditUtils from the viewer editor and choosing
//            the topology edit mode (Autocommit / Transaction)
//   [1]      creating an empty topology in the selected storage
//            (SQLite / PostGIS / Oracle) and opening its project
//   [2]      adding isolated nodes
//   [3]      adding edges between nodes - closing a ring creates a face
//   [4]      adding a node on an existing edge - the edge is split
//            (compare SplitMergeMode = false / true)
//   [5]      moving a node - connected edges follow it
//   [6]      adding a dangling edge (connected at one end only)
//   [7]      adding an edge across a face - the face is split in two
//   [8]      adding a closed edge inside a face - an inner face (hole) appears
//   [9]      changing edge geometry - adjacent faces are updated
//   [10]     deleting a node - the two edges it joined are merged
//   [11]     deleting an edge - the two faces it separated are merged
//   [12]     adding more primitives to have data for feature layers
//   [13-16]  building polygon feature layers POLY0..POLY3 (levels 0..3)
//   [17]     building line feature layers LINE0..LINE3
//   [18]     building point feature layers POINT0..POINT3
//   Every step writes a short description of its result to the log box;
//   node/edge/face IDs are drawn as labels so the changes can be followed
//   directly on the map.
//
// Key TatukGIS NDK classes used:
//   TGIS_EditorTopo.TopoEditUtils  - topology editing engine bound to the viewer
//   TGIS_TopoEditUtils             - high level primitive/feature editing:
//                                    AddNewNode, AddNewEdge, ModifyNode,
//                                    ModifyEdge, DeleteNode, DeleteEdge,
//                                    CreateNewFeatureLayer, CreateAndAddFeatures,
//                                    SetCurrentLayer, Commit
//   TGIS_TopoStoreAbstract         - storage of a topology; CreateTopology,
//                                    DropTopology, GetNode, GetEdge, GetLayer,
//                                    Tolerance, SearchTolerance, SplitMergeMode
//   TGIS_TopoStoreNative/Pgis/Sdo  - concrete stores (SQLite, PostGIS, Oracle)
//   TopoStoreManager               - registry of the available stores;
//                                    CreateInstance, IsRegistered
//   TGIS_LayerTopoSqlAbstract      - viewer layer presenting topology
//                                    primitives or features
//   TGIS_TopoLayer                 - topology layer definition kept in a store
//   TGIS_TopologyEditMode          - Autocommit or Transaction
//
// Notes:
//   - Object IDs are assigned by the storage engine. SQLite, PostGIS and
//     Oracle number new primitives/features slightly differently, therefore
//     the sample uses storage-specific IDs in a few places. In a real
//     application IDs are obtained from user selection or from the API
//     (e.g. Store.GetEdgeByPoint), never hard-coded.
//   - SQLite topologies are always edited in Transaction mode.
//   - PostGIS and Oracle require a running database server with topology
//     support; connection parameters can be entered on the form or stored in
//     an optional TopologyTutorial.local ini file next to the executable.
//     They also need the database client library (libpq.dll / oci.dll),
//     which is not part of the Developer Kernel. It must be built for the
//     same platform as the application; PostgreSQL and Oracle installations
//     provide 64-bit libraries, so "Prefer 32-bit" is turned off in the
//     .NET Framework project.
//   - PostGIS and Oracle storages are part of the Enterprise edition. The
//     sample reaches them only through TopoStoreManager, so it does not refer
//     to Enterprise-only classes. They are disabled on the form when not
//     installed. They also need a license that includes the Enterprise
//     edition; the Developer Kernel checks it when the topology layers are
//     opened (step [1]).
// =============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace TopologyTutorial
{
    /// <summary>
    /// Main form of the TopologyTutorial sample.
    /// The left panel holds the numbered tutorial buttons. Only one button is
    /// enabled at a time - each step enables the next one when it finishes,
    /// because every step relies on the topology state left by the previous one.
    /// The right panel holds storage/topology parameters (editable only before
    /// the tutorial starts), the map, the legend and the log box.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        // --- Tutorial steps ---
        private System.Windows.Forms.Panel pnlTutorial;
        private System.Windows.Forms.Button btn00_StartTutorial;
        private System.Windows.Forms.Button btn01_CreateEmptyTopology;
        private System.Windows.Forms.Button btn02_CreateNodes;
        private System.Windows.Forms.Button btn03_CreateEdgesAndFaces;
        private System.Windows.Forms.Button btn04_AddNodeSplittingAnEdge;
        private System.Windows.Forms.Button btn05_MoveNode;
        private System.Windows.Forms.Button btn06_AddDanglingEdge;
        private System.Windows.Forms.Button btn07_AddEdgeSplittingAFace;
        private System.Windows.Forms.Button btn08_AddEdgeCreatingInnerFace;
        private System.Windows.Forms.Button btn09_ModifyGeometryOfEdge;
        private System.Windows.Forms.Button btn10_DeleteNode;
        private System.Windows.Forms.Button btn11_DeleteEdge;
        private System.Windows.Forms.Button btn12_AddAdditionalPrimitives;
        private System.Windows.Forms.Button btn13_AddFeatureLayerPolyLevel0;
        private System.Windows.Forms.Button btn14_AddFeatureLayerPolyLevel1;
        private System.Windows.Forms.Button btn15_AddFeatureLayerPolyLevel2;
        private System.Windows.Forms.Button btn16_AddFeatureLayerPolyLevel3;
        private System.Windows.Forms.Button btn17_AddFeatureLayersLine;
        private System.Windows.Forms.Button btn18_AddFeatureLayersPoint;
        private System.Windows.Forms.Button btn19_CloseTutorial;

        // --- Map, legend and log ---
        private System.Windows.Forms.Panel pnlMain;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;            // map viewer; its Editor hosts TopoEditUtils
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend gisLegend;  // layer list; follows the most recent layer
        private System.Windows.Forms.TextBox mmLog;                  // result of each step is described here

        // --- Parameters; disabled once the tutorial starts ---
        private System.Windows.Forms.TableLayoutPanel pnlTopoParams;
        private System.Windows.Forms.GroupBox grpStorage;
        private System.Windows.Forms.RadioButton rbtnSqlite;         // native SQLite topology (no server needed)
        private System.Windows.Forms.RadioButton rbtnPostgis;        // PostGIS topology extension
        private System.Windows.Forms.RadioButton rbtnOracle;         // Oracle Spatial SDO_TOPO
        private System.Windows.Forms.GroupBox grpTopo;
        private System.Windows.Forms.Label lblTopoSchemaName;
        private System.Windows.Forms.TextBox edtTopoSchemaName;      // topology schema name (also layer name prefix)
        private System.Windows.Forms.Label lblTopoEditMode;
        private System.Windows.Forms.ComboBox cbxTopoEditMode;       // 0 = Autocommit, 1 = Transaction
        private System.Windows.Forms.Label lblTopoFolder;
        private System.Windows.Forms.TextBox edtTopoFolder;          // output folder for project/database files
        private System.Windows.Forms.GroupBox grpDb;                 // DB connection; visible for PostGIS/Oracle only
        private System.Windows.Forms.TableLayoutPanel tlpDb;
        private System.Windows.Forms.Label lblDbName;
        private System.Windows.Forms.TextBox edtDbName;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox edtHost;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox edtPort;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox edtUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox edtPass;

        /// <summary>
        /// Topology editing engine. It is NOT created by the sample - it is owned
        /// by the viewer editor (see btn00_StartTutorial_Click).
        /// </summary>
        private TGIS_TopoEditUtils topoEditUtils;

        // Coordinates (EPSG:4326, degrees) used to build the initial topology.
        // Points marked "node" become nodes in step [2]; the remaining points are
        // intermediate vertices of the edges created in step [3].
        private static readonly TGIS_Point[] POINTS = {
            TGIS_Utils.GisPoint(15.35, 55.03),  // node 1
            TGIS_Utils.GisPoint(25.51, 53.51),
            TGIS_Utils.GisPoint(30.93, 51.91),
            TGIS_Utils.GisPoint(36.87, 47.27),  // node 2
            TGIS_Utils.GisPoint(40.55, 40.87),
            TGIS_Utils.GisPoint(42.51, 32.11),  // node 3
            TGIS_Utils.GisPoint(26.66, 26.37),
            TGIS_Utils.GisPoint(11.57, 39.33),
            TGIS_Utils.GisPoint(21.00, 41.00),  // node 4
            TGIS_Utils.GisPoint(31.00, 41.00)   // node 5
        };

        // Feature layer names; the digit is the hierarchy level:
        //   level 0 - features built directly from primitives (faces/edges/nodes)
        //   level N - features built from features of level N-1
        private const string POLY0_LAYER_NAME = "POLY0";
        private const string POLY1_LAYER_NAME = "POLY1";
        private const string POLY2_LAYER_NAME = "POLY2";
        private const string POLY3_LAYER_NAME = "POLY3";
        private const string LINE0_LAYER_NAME = "LINE0";
        private const string LINE1_LAYER_NAME = "LINE1";
        private const string LINE2_LAYER_NAME = "LINE2";
        private const string LINE3_LAYER_NAME = "LINE3";
        private const string POINT0_LAYER_NAME = "POINT0";
        private const string POINT1_LAYER_NAME = "POINT1";
        private const string POINT2_LAYER_NAME = "POINT2";
        private const string POINT3_LAYER_NAME = "POINT3";

        /// <summary>
        /// Initialises WinForms designer components.
        /// </summary>
        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // Transaction is the default topology edit mode
            cbxTopoEditMode.SelectedIndex = 1;
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(WinForm));
            pnlTutorial = new Panel();
            btn19_CloseTutorial = new Button();
            btn18_AddFeatureLayersPoint = new Button();
            btn17_AddFeatureLayersLine = new Button();
            btn16_AddFeatureLayerPolyLevel3 = new Button();
            btn15_AddFeatureLayerPolyLevel2 = new Button();
            btn14_AddFeatureLayerPolyLevel1 = new Button();
            btn13_AddFeatureLayerPolyLevel0 = new Button();
            btn12_AddAdditionalPrimitives = new Button();
            btn11_DeleteEdge = new Button();
            btn10_DeleteNode = new Button();
            btn09_ModifyGeometryOfEdge = new Button();
            btn08_AddEdgeCreatingInnerFace = new Button();
            btn07_AddEdgeSplittingAFace = new Button();
            btn06_AddDanglingEdge = new Button();
            btn05_MoveNode = new Button();
            btn04_AddNodeSplittingAnEdge = new Button();
            btn03_CreateEdgesAndFaces = new Button();
            btn02_CreateNodes = new Button();
            btn01_CreateEmptyTopology = new Button();
            btn00_StartTutorial = new Button();
            pnlMain = new Panel();
            GIS = new TGIS_ViewerWnd();
            gisLegend = new TGIS_ControlLegend();
            mmLog = new TextBox();
            pnlTopoParams = new TableLayoutPanel();
            grpStorage = new GroupBox();
            rbtnOracle = new RadioButton();
            rbtnPostgis = new RadioButton();
            rbtnSqlite = new RadioButton();
            grpTopo = new GroupBox();
            edtTopoFolder = new TextBox();
            lblTopoFolder = new Label();
            cbxTopoEditMode = new ComboBox();
            lblTopoEditMode = new Label();
            edtTopoSchemaName = new TextBox();
            lblTopoSchemaName = new Label();
            grpDb = new GroupBox();
            tlpDb = new TableLayoutPanel();
            lblDbName = new Label();
            lblHost = new Label();
            lblPort = new Label();
            edtDbName = new TextBox();
            edtHost = new TextBox();
            edtPort = new TextBox();
            lblUser = new Label();
            lblPass = new Label();
            edtUser = new TextBox();
            edtPass = new TextBox();
            pnlTutorial.SuspendLayout();
            pnlMain.SuspendLayout();
            pnlTopoParams.SuspendLayout();
            grpStorage.SuspendLayout();
            grpTopo.SuspendLayout();
            grpDb.SuspendLayout();
            tlpDb.SuspendLayout();
            SuspendLayout();
            //
            // pnlTutorial
            //
            pnlTutorial.AutoScroll = true;
            pnlTutorial.Controls.Add(btn19_CloseTutorial);
            pnlTutorial.Controls.Add(btn18_AddFeatureLayersPoint);
            pnlTutorial.Controls.Add(btn17_AddFeatureLayersLine);
            pnlTutorial.Controls.Add(btn16_AddFeatureLayerPolyLevel3);
            pnlTutorial.Controls.Add(btn15_AddFeatureLayerPolyLevel2);
            pnlTutorial.Controls.Add(btn14_AddFeatureLayerPolyLevel1);
            pnlTutorial.Controls.Add(btn13_AddFeatureLayerPolyLevel0);
            pnlTutorial.Controls.Add(btn12_AddAdditionalPrimitives);
            pnlTutorial.Controls.Add(btn11_DeleteEdge);
            pnlTutorial.Controls.Add(btn10_DeleteNode);
            pnlTutorial.Controls.Add(btn09_ModifyGeometryOfEdge);
            pnlTutorial.Controls.Add(btn08_AddEdgeCreatingInnerFace);
            pnlTutorial.Controls.Add(btn07_AddEdgeSplittingAFace);
            pnlTutorial.Controls.Add(btn06_AddDanglingEdge);
            pnlTutorial.Controls.Add(btn05_MoveNode);
            pnlTutorial.Controls.Add(btn04_AddNodeSplittingAnEdge);
            pnlTutorial.Controls.Add(btn03_CreateEdgesAndFaces);
            pnlTutorial.Controls.Add(btn02_CreateNodes);
            pnlTutorial.Controls.Add(btn01_CreateEmptyTopology);
            pnlTutorial.Controls.Add(btn00_StartTutorial);
            pnlTutorial.Dock = DockStyle.Left;
            pnlTutorial.Location = new Point(0, 0);
            pnlTutorial.Name = "pnlTutorial";
            pnlTutorial.Padding = new Padding(1);
            pnlTutorial.Size = new Size(340, 861);
            pnlTutorial.TabIndex = 0;
            //
            // btn19_CloseTutorial
            //
            btn19_CloseTutorial.Dock = DockStyle.Top;
            btn19_CloseTutorial.Enabled = false;
            btn19_CloseTutorial.Location = new Point(1, 780);
            btn19_CloseTutorial.Name = "btn19_CloseTutorial";
            btn19_CloseTutorial.Size = new Size(338, 41);
            btn19_CloseTutorial.TabIndex = 19;
            btn19_CloseTutorial.Text = "[19] Close Tutorial";
            btn19_CloseTutorial.Click += btn19_CloseTutorial_Click;
            //
            // btn18_AddFeatureLayersPoint
            //
            btn18_AddFeatureLayersPoint.Dock = DockStyle.Top;
            btn18_AddFeatureLayersPoint.Enabled = false;
            btn18_AddFeatureLayersPoint.Location = new Point(1, 739);
            btn18_AddFeatureLayersPoint.Name = "btn18_AddFeatureLayersPoint";
            btn18_AddFeatureLayersPoint.Size = new Size(338, 41);
            btn18_AddFeatureLayersPoint.TabIndex = 18;
            btn18_AddFeatureLayersPoint.Text = "[18] Add Feature Layers (POINT, levels 0-3)";
            btn18_AddFeatureLayersPoint.Click += btn18_AddFeatureLayersPoint_Click;
            //
            // btn17_AddFeatureLayersLine
            //
            btn17_AddFeatureLayersLine.Dock = DockStyle.Top;
            btn17_AddFeatureLayersLine.Enabled = false;
            btn17_AddFeatureLayersLine.Location = new Point(1, 698);
            btn17_AddFeatureLayersLine.Name = "btn17_AddFeatureLayersLine";
            btn17_AddFeatureLayersLine.Size = new Size(338, 41);
            btn17_AddFeatureLayersLine.TabIndex = 17;
            btn17_AddFeatureLayersLine.Text = "[17] Add Feature Layers (LINE, levels 0-3)";
            btn17_AddFeatureLayersLine.Click += btn17_AddFeatureLayersLine_Click;
            //
            // btn16_AddFeatureLayerPolyLevel3
            //
            btn16_AddFeatureLayerPolyLevel3.Dock = DockStyle.Top;
            btn16_AddFeatureLayerPolyLevel3.Enabled = false;
            btn16_AddFeatureLayerPolyLevel3.Location = new Point(1, 657);
            btn16_AddFeatureLayerPolyLevel3.Name = "btn16_AddFeatureLayerPolyLevel3";
            btn16_AddFeatureLayerPolyLevel3.Size = new Size(338, 41);
            btn16_AddFeatureLayerPolyLevel3.TabIndex = 16;
            btn16_AddFeatureLayerPolyLevel3.Text = "[16] Add Feature Layer (POLY, level 3)";
            btn16_AddFeatureLayerPolyLevel3.Click += btn16_AddFeatureLayerPolyLevel3_Click;
            //
            // btn15_AddFeatureLayerPolyLevel2
            //
            btn15_AddFeatureLayerPolyLevel2.Dock = DockStyle.Top;
            btn15_AddFeatureLayerPolyLevel2.Enabled = false;
            btn15_AddFeatureLayerPolyLevel2.Location = new Point(1, 616);
            btn15_AddFeatureLayerPolyLevel2.Name = "btn15_AddFeatureLayerPolyLevel2";
            btn15_AddFeatureLayerPolyLevel2.Size = new Size(338, 41);
            btn15_AddFeatureLayerPolyLevel2.TabIndex = 15;
            btn15_AddFeatureLayerPolyLevel2.Text = "[15] Add Feature Layer (POLY, level 2)";
            btn15_AddFeatureLayerPolyLevel2.Click += btn15_AddFeatureLayerPolyLevel2_Click;
            //
            // btn14_AddFeatureLayerPolyLevel1
            //
            btn14_AddFeatureLayerPolyLevel1.Dock = DockStyle.Top;
            btn14_AddFeatureLayerPolyLevel1.Enabled = false;
            btn14_AddFeatureLayerPolyLevel1.Location = new Point(1, 575);
            btn14_AddFeatureLayerPolyLevel1.Name = "btn14_AddFeatureLayerPolyLevel1";
            btn14_AddFeatureLayerPolyLevel1.Size = new Size(338, 41);
            btn14_AddFeatureLayerPolyLevel1.TabIndex = 14;
            btn14_AddFeatureLayerPolyLevel1.Text = "[14] Add Feature Layer (POLY, level 1)";
            btn14_AddFeatureLayerPolyLevel1.Click += btn14_AddFeatureLayerPolyLevel1_Click;
            //
            // btn13_AddFeatureLayerPolyLevel0
            //
            btn13_AddFeatureLayerPolyLevel0.Dock = DockStyle.Top;
            btn13_AddFeatureLayerPolyLevel0.Enabled = false;
            btn13_AddFeatureLayerPolyLevel0.Location = new Point(1, 534);
            btn13_AddFeatureLayerPolyLevel0.Name = "btn13_AddFeatureLayerPolyLevel0";
            btn13_AddFeatureLayerPolyLevel0.Size = new Size(338, 41);
            btn13_AddFeatureLayerPolyLevel0.TabIndex = 13;
            btn13_AddFeatureLayerPolyLevel0.Text = "[13] Add Feature Layer (POLY, level 0)";
            btn13_AddFeatureLayerPolyLevel0.Click += btn13_AddFeatureLayerPolyLevel0_Click;
            //
            // btn12_AddAdditionalPrimitives
            //
            btn12_AddAdditionalPrimitives.Dock = DockStyle.Top;
            btn12_AddAdditionalPrimitives.Enabled = false;
            btn12_AddAdditionalPrimitives.Location = new Point(1, 493);
            btn12_AddAdditionalPrimitives.Name = "btn12_AddAdditionalPrimitives";
            btn12_AddAdditionalPrimitives.Size = new Size(338, 41);
            btn12_AddAdditionalPrimitives.TabIndex = 12;
            btn12_AddAdditionalPrimitives.Text = "[12] Add Additional Primitives";
            btn12_AddAdditionalPrimitives.Click += btn12_AddAdditionalPrimitives_Click;
            //
            // btn11_DeleteEdge
            //
            btn11_DeleteEdge.Dock = DockStyle.Top;
            btn11_DeleteEdge.Enabled = false;
            btn11_DeleteEdge.Location = new Point(1, 452);
            btn11_DeleteEdge.Name = "btn11_DeleteEdge";
            btn11_DeleteEdge.Size = new Size(338, 41);
            btn11_DeleteEdge.TabIndex = 11;
            btn11_DeleteEdge.Text = "[11] Delete Edge";
            btn11_DeleteEdge.Click += btn11_DeleteEdge_Click;
            //
            // btn10_DeleteNode
            //
            btn10_DeleteNode.Dock = DockStyle.Top;
            btn10_DeleteNode.Enabled = false;
            btn10_DeleteNode.Location = new Point(1, 411);
            btn10_DeleteNode.Name = "btn10_DeleteNode";
            btn10_DeleteNode.Size = new Size(338, 41);
            btn10_DeleteNode.TabIndex = 10;
            btn10_DeleteNode.Text = "[10] Delete Node";
            btn10_DeleteNode.Click += btn10_DeleteNode_Click;
            //
            // btn09_ModifyGeometryOfEdge
            //
            btn09_ModifyGeometryOfEdge.Dock = DockStyle.Top;
            btn09_ModifyGeometryOfEdge.Enabled = false;
            btn09_ModifyGeometryOfEdge.Location = new Point(1, 370);
            btn09_ModifyGeometryOfEdge.Name = "btn09_ModifyGeometryOfEdge";
            btn09_ModifyGeometryOfEdge.Size = new Size(338, 41);
            btn09_ModifyGeometryOfEdge.TabIndex = 9;
            btn09_ModifyGeometryOfEdge.Text = "[9] Modify Geometry of Edge";
            btn09_ModifyGeometryOfEdge.Click += btn09_ModifyGeometryOfEdge_Click;
            //
            // btn08_AddEdgeCreatingInnerFace
            //
            btn08_AddEdgeCreatingInnerFace.Dock = DockStyle.Top;
            btn08_AddEdgeCreatingInnerFace.Enabled = false;
            btn08_AddEdgeCreatingInnerFace.Location = new Point(1, 329);
            btn08_AddEdgeCreatingInnerFace.Name = "btn08_AddEdgeCreatingInnerFace";
            btn08_AddEdgeCreatingInnerFace.Size = new Size(338, 41);
            btn08_AddEdgeCreatingInnerFace.TabIndex = 8;
            btn08_AddEdgeCreatingInnerFace.Text = "[8] Add Edge Creating an Inner Face";
            btn08_AddEdgeCreatingInnerFace.Click += btn08_AddEdgeCreatingInnerFace_Click;
            //
            // btn07_AddEdgeSplittingAFace
            //
            btn07_AddEdgeSplittingAFace.Dock = DockStyle.Top;
            btn07_AddEdgeSplittingAFace.Enabled = false;
            btn07_AddEdgeSplittingAFace.Location = new Point(1, 288);
            btn07_AddEdgeSplittingAFace.Name = "btn07_AddEdgeSplittingAFace";
            btn07_AddEdgeSplittingAFace.Size = new Size(338, 41);
            btn07_AddEdgeSplittingAFace.TabIndex = 7;
            btn07_AddEdgeSplittingAFace.Text = "[7] Add Edge Splitting a Face";
            btn07_AddEdgeSplittingAFace.Click += btn07_AddEdgeSplittingAFace_Click;
            //
            // btn06_AddDanglingEdge
            //
            btn06_AddDanglingEdge.Dock = DockStyle.Top;
            btn06_AddDanglingEdge.Enabled = false;
            btn06_AddDanglingEdge.Location = new Point(1, 247);
            btn06_AddDanglingEdge.Name = "btn06_AddDanglingEdge";
            btn06_AddDanglingEdge.Size = new Size(338, 41);
            btn06_AddDanglingEdge.TabIndex = 6;
            btn06_AddDanglingEdge.Text = "[6] Add Dangling Edge";
            btn06_AddDanglingEdge.Click += btn06_AddDanglingEdge_Click;
            //
            // btn05_MoveNode
            //
            btn05_MoveNode.Dock = DockStyle.Top;
            btn05_MoveNode.Enabled = false;
            btn05_MoveNode.Location = new Point(1, 206);
            btn05_MoveNode.Name = "btn05_MoveNode";
            btn05_MoveNode.Size = new Size(338, 41);
            btn05_MoveNode.TabIndex = 5;
            btn05_MoveNode.Text = "[5] Move Node";
            btn05_MoveNode.Click += btn05_MoveNode_Click;
            //
            // btn04_AddNodeSplittingAnEdge
            //
            btn04_AddNodeSplittingAnEdge.Dock = DockStyle.Top;
            btn04_AddNodeSplittingAnEdge.Enabled = false;
            btn04_AddNodeSplittingAnEdge.Location = new Point(1, 165);
            btn04_AddNodeSplittingAnEdge.Name = "btn04_AddNodeSplittingAnEdge";
            btn04_AddNodeSplittingAnEdge.Size = new Size(338, 41);
            btn04_AddNodeSplittingAnEdge.TabIndex = 4;
            btn04_AddNodeSplittingAnEdge.Text = "[4] Add Node Splitting an Edge";
            btn04_AddNodeSplittingAnEdge.Click += btn04_AddNodeSplittingAnEdge_Click;
            //
            // btn03_CreateEdgesAndFaces
            //
            btn03_CreateEdgesAndFaces.Dock = DockStyle.Top;
            btn03_CreateEdgesAndFaces.Enabled = false;
            btn03_CreateEdgesAndFaces.Location = new Point(1, 124);
            btn03_CreateEdgesAndFaces.Name = "btn03_CreateEdgesAndFaces";
            btn03_CreateEdgesAndFaces.Size = new Size(338, 41);
            btn03_CreateEdgesAndFaces.TabIndex = 3;
            btn03_CreateEdgesAndFaces.Text = "[3] Create Edges and Faces";
            btn03_CreateEdgesAndFaces.Click += btn03_CreateEdgesAndFaces_Click;
            //
            // btn02_CreateNodes
            //
            btn02_CreateNodes.Dock = DockStyle.Top;
            btn02_CreateNodes.Enabled = false;
            btn02_CreateNodes.Location = new Point(1, 83);
            btn02_CreateNodes.Name = "btn02_CreateNodes";
            btn02_CreateNodes.Size = new Size(338, 41);
            btn02_CreateNodes.TabIndex = 2;
            btn02_CreateNodes.Text = "[2] Create Nodes";
            btn02_CreateNodes.Click += btn02_CreateNodes_Click;
            //
            // btn01_CreateEmptyTopology
            //
            btn01_CreateEmptyTopology.Dock = DockStyle.Top;
            btn01_CreateEmptyTopology.Enabled = false;
            btn01_CreateEmptyTopology.Location = new Point(1, 42);
            btn01_CreateEmptyTopology.Name = "btn01_CreateEmptyTopology";
            btn01_CreateEmptyTopology.Size = new Size(338, 41);
            btn01_CreateEmptyTopology.TabIndex = 1;
            btn01_CreateEmptyTopology.Text = "[1] Create Empty Topology";
            btn01_CreateEmptyTopology.Click += btn01_CreateEmptyTopology_Click;
            //
            // btn00_StartTutorial
            //
            btn00_StartTutorial.Dock = DockStyle.Top;
            btn00_StartTutorial.Location = new Point(1, 1);
            btn00_StartTutorial.Name = "btn00_StartTutorial";
            btn00_StartTutorial.Size = new Size(338, 41);
            btn00_StartTutorial.TabIndex = 0;
            btn00_StartTutorial.Text = "[0] Start Tutorial";
            btn00_StartTutorial.Click += btn00_StartTutorial_Click;
            //
            // pnlMain
            //
            pnlMain.Controls.Add(GIS);
            pnlMain.Controls.Add(gisLegend);
            pnlMain.Controls.Add(mmLog);
            pnlMain.Controls.Add(pnlTopoParams);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(340, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new Padding(1);
            pnlMain.Size = new Size(1044, 861);
            pnlMain.TabIndex = 1;
            //
            // GIS
            //
            GIS.BackColor = Color.White;
            GIS.BorderStyle = BorderStyle.None;
            GIS.DelayedUpdate = 700;
            GIS.Dock = DockStyle.Fill;
            GIS.KeepScale = false;
            GIS.Location = new Point(1, 111);
            GIS.Name = "GIS";
            GIS.ProgressiveUpdate = 2500;
            GIS.Size = new Size(842, 599);
            GIS.TabIndex = 1;
            //
            // gisLegend
            //
            gisLegend.Dock = DockStyle.Right;
            gisLegend.GIS_Viewer = GIS;
            gisLegend.Location = new Point(843, 111);
            gisLegend.Name = "gisLegend";
            gisLegend.Options = TGIS_ControlLegendOption.AllowMove | TGIS_ControlLegendOption.AllowActive | TGIS_ControlLegendOption.AllowExpand | TGIS_ControlLegendOption.AllowParams | TGIS_ControlLegendOption.AllowSelect | TGIS_ControlLegendOption.ShowSubLayers | TGIS_ControlLegendOption.AllowParamsVisible;
            gisLegend.Size = new Size(200, 599);
            gisLegend.TabIndex = 2;
            //
            // mmLog
            //
            mmLog.Dock = DockStyle.Bottom;
            mmLog.Location = new Point(1, 710);
            mmLog.Multiline = true;
            mmLog.Name = "mmLog";
            mmLog.ScrollBars = ScrollBars.Vertical;
            mmLog.Size = new Size(1042, 150);
            mmLog.TabIndex = 3;
            mmLog.Text = "=== TOPOLOGY TUTORIAL ===\r\n";
            //
            // pnlTopoParams
            //
            pnlTopoParams.ColumnCount = 3;
            pnlTopoParams.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 206F));
            pnlTopoParams.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 423F));
            pnlTopoParams.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlTopoParams.Controls.Add(grpStorage, 0, 0);
            pnlTopoParams.Controls.Add(grpTopo, 1, 0);
            pnlTopoParams.Controls.Add(grpDb, 2, 0);
            pnlTopoParams.Dock = DockStyle.Top;
            pnlTopoParams.Location = new Point(1, 1);
            pnlTopoParams.Name = "pnlTopoParams";
            pnlTopoParams.Padding = new Padding(1);
            pnlTopoParams.RowCount = 1;
            pnlTopoParams.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlTopoParams.Size = new Size(1042, 110);
            pnlTopoParams.TabIndex = 0;
            //
            // grpStorage
            //
            grpStorage.Controls.Add(rbtnOracle);
            grpStorage.Controls.Add(rbtnPostgis);
            grpStorage.Controls.Add(rbtnSqlite);
            grpStorage.Dock = DockStyle.Fill;
            grpStorage.Location = new Point(4, 4);
            grpStorage.Name = "grpStorage";
            grpStorage.Size = new Size(200, 102);
            grpStorage.TabIndex = 0;
            grpStorage.TabStop = false;
            grpStorage.Text = "Topology Storage Type";
            //
            // rbtnOracle
            //
            rbtnOracle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            rbtnOracle.Location = new Point(5, 66);
            rbtnOracle.Name = "rbtnOracle";
            rbtnOracle.Size = new Size(190, 19);
            rbtnOracle.TabIndex = 2;
            rbtnOracle.Text = "Oracle";
            rbtnOracle.UseVisualStyleBackColor = true;
            rbtnOracle.CheckedChanged += rbtnOracle_CheckedChanged;
            //
            // rbtnPostgis
            //
            rbtnPostgis.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            rbtnPostgis.Location = new Point(5, 43);
            rbtnPostgis.Name = "rbtnPostgis";
            rbtnPostgis.Size = new Size(190, 19);
            rbtnPostgis.TabIndex = 1;
            rbtnPostgis.Text = "PostGIS";
            rbtnPostgis.UseVisualStyleBackColor = true;
            rbtnPostgis.CheckedChanged += rbtnPostgis_CheckedChanged;
            //
            // rbtnSqlite
            //
            rbtnSqlite.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            rbtnSqlite.Checked = true;
            rbtnSqlite.Location = new Point(5, 20);
            rbtnSqlite.Name = "rbtnSqlite";
            rbtnSqlite.Size = new Size(190, 19);
            rbtnSqlite.TabIndex = 0;
            rbtnSqlite.TabStop = true;
            rbtnSqlite.Text = "SQLite";
            rbtnSqlite.UseVisualStyleBackColor = true;
            rbtnSqlite.CheckedChanged += rbtnSqlite_CheckedChanged;
            //
            // grpTopo
            //
            grpTopo.Controls.Add(edtTopoFolder);
            grpTopo.Controls.Add(lblTopoFolder);
            grpTopo.Controls.Add(cbxTopoEditMode);
            grpTopo.Controls.Add(lblTopoEditMode);
            grpTopo.Controls.Add(edtTopoSchemaName);
            grpTopo.Controls.Add(lblTopoSchemaName);
            grpTopo.Dock = DockStyle.Fill;
            grpTopo.Location = new Point(210, 11);
            grpTopo.Margin = new Padding(3, 10, 3, 3);
            grpTopo.Name = "grpTopo";
            grpTopo.Size = new Size(417, 95);
            grpTopo.TabIndex = 1;
            grpTopo.TabStop = false;
            //
            // edtTopoFolder
            //
            edtTopoFolder.Location = new Point(11, 69);
            edtTopoFolder.Name = "edtTopoFolder";
            edtTopoFolder.Size = new Size(400, 23);
            edtTopoFolder.TabIndex = 2;
            //
            // lblTopoFolder
            //
            lblTopoFolder.AutoSize = true;
            lblTopoFolder.Location = new Point(11, 51);
            lblTopoFolder.Name = "lblTopoFolder";
            lblTopoFolder.Size = new Size(79, 15);
            lblTopoFolder.TabIndex = 5;
            lblTopoFolder.Text = "Output folder";
            //
            // cbxTopoEditMode
            //
            cbxTopoEditMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxTopoEditMode.Items.AddRange(new object[] { "Autocommit", "Transaction" });
            cbxTopoEditMode.Location = new Point(217, 21);
            cbxTopoEditMode.Name = "cbxTopoEditMode";
            cbxTopoEditMode.Size = new Size(192, 23);
            cbxTopoEditMode.TabIndex = 1;
            //
            // lblTopoEditMode
            //
            lblTopoEditMode.AutoSize = true;
            lblTopoEditMode.Location = new Point(217, 3);
            lblTopoEditMode.Name = "lblTopoEditMode";
            lblTopoEditMode.Size = new Size(107, 15);
            lblTopoEditMode.TabIndex = 4;
            lblTopoEditMode.Text = "Topology Edit Mode";
            //
            // edtTopoSchemaName
            //
            edtTopoSchemaName.Location = new Point(11, 21);
            edtTopoSchemaName.Name = "edtTopoSchemaName";
            edtTopoSchemaName.Size = new Size(200, 23);
            edtTopoSchemaName.TabIndex = 0;
            //
            // lblTopoSchemaName
            //
            lblTopoSchemaName.AutoSize = true;
            lblTopoSchemaName.Location = new Point(11, 3);
            lblTopoSchemaName.Name = "lblTopoSchemaName";
            lblTopoSchemaName.Size = new Size(130, 15);
            lblTopoSchemaName.TabIndex = 3;
            lblTopoSchemaName.Text = "Topology Schema Name";
            //
            // grpDb
            //
            grpDb.Controls.Add(tlpDb);
            grpDb.Dock = DockStyle.Fill;
            grpDb.Location = new Point(633, 11);
            grpDb.Margin = new Padding(3, 10, 3, 3);
            grpDb.Name = "grpDb";
            grpDb.Size = new Size(405, 95);
            grpDb.TabIndex = 2;
            grpDb.TabStop = false;
            grpDb.Visible = false;
            //
            // tlpDb
            //
            tlpDb.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tlpDb.BackColor = Color.Transparent;
            tlpDb.ColumnCount = 3;
            tlpDb.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpDb.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpDb.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tlpDb.Controls.Add(lblDbName, 0, 0);
            tlpDb.Controls.Add(lblHost, 1, 0);
            tlpDb.Controls.Add(lblPort, 2, 0);
            tlpDb.Controls.Add(edtDbName, 0, 1);
            tlpDb.Controls.Add(edtHost, 1, 1);
            tlpDb.Controls.Add(edtPort, 2, 1);
            tlpDb.Controls.Add(lblUser, 0, 2);
            tlpDb.Controls.Add(lblPass, 1, 2);
            tlpDb.Controls.Add(edtUser, 0, 3);
            tlpDb.Controls.Add(edtPass, 1, 3);
            tlpDb.Location = new Point(12, 0);
            tlpDb.Margin = new Padding(0);
            tlpDb.Name = "tlpDb";
            tlpDb.RowCount = 4;
            tlpDb.RowStyles.Add(new RowStyle(SizeType.Absolute, 21F));
            tlpDb.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpDb.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
            tlpDb.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tlpDb.Size = new Size(384, 95);
            tlpDb.TabIndex = 0;
            //
            // lblDbName
            //
            lblDbName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblDbName.AutoSize = true;
            lblDbName.BackColor = SystemColors.Control;
            lblDbName.Location = new Point(3, 3);
            lblDbName.Margin = new Padding(3, 0, 3, 3);
            lblDbName.Name = "lblDbName";
            lblDbName.Size = new Size(50, 15);
            lblDbName.TabIndex = 5;
            lblDbName.Text = "DB Name";
            //
            // lblHost
            //
            lblHost.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblHost.AutoSize = true;
            lblHost.BackColor = SystemColors.Control;
            lblHost.Location = new Point(131, 3);
            lblHost.Margin = new Padding(3, 0, 3, 3);
            lblHost.Name = "lblHost";
            lblHost.Size = new Size(32, 15);
            lblHost.TabIndex = 6;
            lblHost.Text = "Host";
            //
            // lblPort
            //
            lblPort.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPort.AutoSize = true;
            lblPort.BackColor = SystemColors.Control;
            lblPort.Location = new Point(259, 3);
            lblPort.Margin = new Padding(3, 0, 3, 3);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(29, 15);
            lblPort.TabIndex = 7;
            lblPort.Text = "Port";
            //
            // edtDbName
            //
            edtDbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtDbName.Location = new Point(3, 21);
            edtDbName.Margin = new Padding(3, 0, 3, 0);
            edtDbName.Name = "edtDbName";
            edtDbName.Size = new Size(122, 23);
            edtDbName.TabIndex = 0;
            //
            // edtHost
            //
            edtHost.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtHost.Location = new Point(131, 21);
            edtHost.Margin = new Padding(3, 0, 3, 0);
            edtHost.Name = "edtHost";
            edtHost.Size = new Size(122, 23);
            edtHost.TabIndex = 1;
            //
            // edtPort
            //
            edtPort.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtPort.Location = new Point(259, 21);
            edtPort.Margin = new Padding(3, 0, 3, 0);
            edtPort.Name = "edtPort";
            edtPort.Size = new Size(122, 23);
            edtPort.TabIndex = 2;
            //
            // lblUser
            //
            lblUser.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblUser.AutoSize = true;
            lblUser.BackColor = SystemColors.Control;
            lblUser.Location = new Point(3, 51);
            lblUser.Margin = new Padding(3, 0, 3, 3);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(30, 15);
            lblUser.TabIndex = 8;
            lblUser.Text = "User";
            //
            // lblPass
            //
            lblPass.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPass.AutoSize = true;
            lblPass.BackColor = SystemColors.Control;
            lblPass.Location = new Point(131, 51);
            lblPass.Margin = new Padding(3, 0, 3, 3);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(57, 15);
            lblPass.TabIndex = 9;
            lblPass.Text = "Password";
            //
            // edtUser
            //
            edtUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtUser.Location = new Point(3, 69);
            edtUser.Margin = new Padding(3, 0, 3, 0);
            edtUser.Name = "edtUser";
            edtUser.Size = new Size(122, 23);
            edtUser.TabIndex = 3;
            //
            // edtPass
            //
            edtPass.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtPass.Location = new Point(131, 69);
            edtPass.Margin = new Padding(3, 0, 3, 0);
            edtPass.Name = "edtPass";
            edtPass.Size = new Size(122, 23);
            edtPass.TabIndex = 4;
            //
            // WinForm
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1384, 861);
            Controls.Add(pnlMain);
            Controls.Add(pnlTutorial);
            Font = new Font("Segoe UI", 9F);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(640, 480);
            Name = "WinForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TatukGIS Samples - TopologyTutorial";
            Load += WinForm_Load;
            pnlTutorial.ResumeLayout(false);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            pnlTopoParams.ResumeLayout(false);
            grpStorage.ResumeLayout(false);
            grpTopo.ResumeLayout(false);
            grpTopo.PerformLayout();
            grpDb.ResumeLayout(false);
            tlpDb.ResumeLayout(false);
            tlpDb.PerformLayout();
            ResumeLayout(false);
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
        /// Builds one hierarchical feature: all features with IDs from index1 to
        /// index2 in the source (lower level) feature layer are selected and then
        /// grouped into a single new feature of the destination (higher level) layer.
        /// Example: features [1, 2] of POLY0 -> one feature of POLY1 covering both.
        /// The new feature does not copy any geometry - it only references the source
        /// features, so its shape is always derived from the underlying primitives.
        /// </summary>
        public void AddFeatures(
            object sender,
            int index1, int index2,
            TGIS_LayerTopoSqlAbstract sourceFeatureLayer,
            TGIS_TopoLayer destinationTopoLayer)
        {
            StringBuilder idList = new StringBuilder();
            for (int i = index1; i <= index2; i++)
            {
                // collect IDs for the log message
                idList.Append(i);
                if (i < index2)
                    idList.Append(", ");

                // CreateAndAddFeatures works on the current selection of the source
                // layer, so mark every feature that should become part of the new one
                TGIS_Shape shape = sourceFeatureLayer.GetShape(i);
                if (shape != null)
                    shape.IsSelected = true;
            }

            // make sure the edit runs inside a global transaction
            // (no-op if a transaction is already open or in Autocommit mode)
            if (!topoEditUtils.Transaction)
                topoEditUtils.StartGlobalTransaction(sourceFeatureLayer.Store);

            // all selected source features are grouped into ONE new feature;
            // the result is the ID of the new feature or -1 on error
            long newFeatureId = topoEditUtils.CreateAndAddFeatures(sourceFeatureLayer, destinationTopoLayer);
            if (newFeatureId >= 0)
                LogMsg(sender, string.Format(
                    "Feature [{0}] was created in layer [{1}] from features [{2}] in feature layer [{3}]",
                    newFeatureId, destinationTopoLayer.Name, idList, sourceFeatureLayer.Name
                ));

            // write pending feature edits to the store (still inside the transaction;
            // the caller decides when to Commit)
            topoEditUtils.SaveGeometryEdits();
        }

        /// <summary>
        /// Step [0] - prepares the topology editing engine.
        /// TGIS_TopoEditUtils is the central class of this tutorial: every primitive
        /// and feature operation goes through it. It keeps track of the current layer,
        /// the current topology store and the transaction state.
        /// </summary>
        private void btn00_StartTutorial_Click(object sender, EventArgs e)
        {
            btn00_StartTutorial.Enabled = false;

            // Topology is an integral part of both GIS and the Editor,
            // therefore we should use the existing TopoEditUtils instance
            // rather than creating our own.
            // DON'T DO THIS:
            // topoEditUtils = new TGIS_TopoEditUtils(GIS, TGIS_TopologyEditMode.Transaction);
            topoEditUtils = ((TGIS_EditorTopo)GIS.Editor).TopoEditUtils;

            // Edit mode decides how changes reach the database:
            //   Autocommit  - each operation is stored immediately, no rollback,
            //   Transaction - operations are grouped and stored on Commit
            //                 (or discarded on rollback).
            // Note: SQLite topology is always switched to Transaction mode internally.
            topoEditUtils.EditMode = TopoEditMode;

            MessageBox.Show(
                "Welcome to the Topology Tutorial.\r\n\r\n" +
                "This tutorial demonstrates the use of the low-level topology API for creating and editing topology primitives, managing topological relationships, and building feature layers from topological structures.\r\n\r\n" +
                "Follow the numbered steps and review the source code below to learn how topology operations are performed and observe their impact on the topology structure and related objects."
            );

            btn01_CreateEmptyTopology.Enabled = true;
            // storage and topology parameters must not change during the tutorial
            pnlTopoParams.Enabled = false;
        }

        /// <summary>
        /// Step [1] - creates a new, empty topology and opens it in the viewer.
        /// A topology store (TGIS_TopoStoreNative / Pgis / Sdo) is used only for
        /// administrative work here: drop an old topology and create a new one.
        /// CreateTopology creates the database objects (node, edge and face tables),
        /// .ttkls layer access files and a .ttkproject project file in TopoFolder.
        /// Opening that project adds three primitive layers to the viewer:
        /// NODE, EDGE and FACE.
        /// </summary>
        private void btn01_CreateEmptyTopology_Click(object sender, EventArgs e)
        {
            btn01_CreateEmptyTopology.Enabled = false;

            // project and access files (and the SQLite database) are stored here
            try
            {
                Directory.CreateDirectory(TopoFolder);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("Cannot create directory: {0}", TopoFolder));
                restartTutorial();
                return;
            }

            // the administrative store object is no longer needed after this block;
            // the layers opened below create and share their own store instance
            using (TGIS_TopoStoreAbstract topoStore = CreateTopoStore(TopoStorageType))
            {
                // Database storages load their client library (libpq.dll / oci.dll) and
                // connect to the server on the first call; if either is missing, an
                // exception is thrown - it is reported instead of stopping the tutorial.
                try
                {
                    // remove the topology left by a previous run of the tutorial, so that
                    // every run starts from the same state and gets the same object IDs
                    if (topoStore.DropTopology(
                          ConnectionString,  // connection string (ttkls)
                          TopoFolder,        // topology output folder
                          TopoName,          // topology name
                          TopoSchemaName     // topology schema name
                        ) < 0)
                    {
                        showStorageError(topoStore.ErrorMsg);
                        restartTutorial();
                        return;
                    }

                    // Tolerance and precision are fundamental topology settings:
                    //  - tolerance: points closer than this distance are treated as the same
                    //    point (new geometry is snapped to existing nodes/edges within it),
                    //  - precision: number of decimal digits kept for coordinates.
                    // Both are expressed in units of the SRID (degrees for EPSG:4326).
                    if (topoStore.CreateTopology(
                          ConnectionString,  // connection string (ttkls)
                          TopoFolder,        // topology output folder
                          TopoName,          // topology name
                          TopoSchemaName,    // topology schema name
                          4326,              // SRID (spatial reference identifier) for topology
                          0.1,               // topology tolerance
                          2,                 // topology precision
                          TopoExtent         // topology extent
                        ) < 0)
                    {
                        showStorageError(topoStore.ErrorMsg);
                        restartTutorial();
                        return;
                    }
                    else
                        LogMsg(sender, string.Format(
                            "Topology {0} [{1}] successfully created in {2} directory...",
                            TopoSchemaName, TopoStorageTypeString, TopoFolder
                        ));
                }
                catch (Exception ex)
                {
                    showStorageError(ex.Message);
                    restartTutorial();
                    return;
                }
            }

            // the project created by CreateTopology contains the NODE, EDGE and FACE
            // layers of the new (still empty) topology
            GIS.Open(TopoProjectPath);
            LogMsg(sender, string.Format(
                "New topology project [{0}] contains layers: [{1}, {2}, {3}].",
                GIS.ProjectName, NodeLayerName, EdgeLayerName, FaceLayerName
            ));

            btn02_CreateNodes.Enabled = true;
        }

        /// <summary>
        /// Step [2] - adds five isolated nodes.
        /// A node is the simplest topology primitive. Nodes added here are not
        /// connected to anything yet; in the next step edges will be drawn between
        /// them. The layers are labelled with object IDs, so the effect of every
        /// subsequent step can be followed on the map.
        /// </summary>
        private void btn02_CreateNodes_Click(object sender, EventArgs e)
        {
            btn02_CreateNodes.Enabled = false;

            // An empty topology has no meaningful extent yet, so set it explicitly.
            // Labels show the topology ID of each primitive:
            //   nodes - yellow, edges - lime, faces - fuchsia.
            NodeLayer.Extent = TopoExtent;
            NodeLayer.Params.Labels.Field = UidFieldName;
            NodeLayer.Params.Labels.Color = TGIS_Color.Yellow;

            EdgeLayer.Extent = TopoExtent;
            EdgeLayer.Params.Labels.Field = UidFieldName;
            EdgeLayer.Params.Labels.Color = TGIS_Color.Lime;

            FaceLayer.Extent = TopoExtent;
            FaceLayer.Params.Labels.Field = UidFieldName;
            FaceLayer.Params.Labels.Color = TGIS_Color.Fuchsia;

            // `SetCurrentLayer` starts global transaction
            // and sets necessary internal `TopoEditUtils` parameters
            topoEditUtils.SetCurrentLayer(NodeLayer);

            // SearchTolerance is adjusted in relation to GIS.VisibleExtent when viewer is assigned:
            // topoEditUtils.Store.SearchTolerance = 5 / GIS.Zoom;
            // In this case  a priori value, 5 x topology tolerance
            topoEditUtils.Store.SearchTolerance = 5 * topoEditUtils.Store.Tolerance;

            // one shape object is reused for all nodes - AddNewNode only reads its
            // geometry, the shape itself is not stored in the topology
            TGIS_ShapePoint pointShape = new TGIS_ShapePoint();
            StringBuilder nodeList = new StringBuilder();

            // AddNewNode returns the ID of the new node (or -1 on error)
            long nodeId = topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, POINTS[0]));
            if (nodeId < 0)
            {
                // potential error can be checked by Store.ErrorMsg
                MessageBox.Show(NodeLayer.Store.ErrorMsg);
                return;
            }
            nodeList.Append(nodeId);

            nodeId = topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, POINTS[3]));
            nodeList.Append(", " + nodeId);

            nodeId = topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, POINTS[5]));
            nodeList.Append(", " + nodeId);

            nodeId = topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, POINTS[8]));
            nodeList.Append(", " + nodeId);

            nodeId = topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, POINTS[9]));
            nodeList.Append(", " + nodeId);

            // close the global transaction - store all new nodes in the database
            topoEditUtils.Commit();

            GIS.VisibleExtent = TopoExtent;
            // flush layer changes to the project/database and refresh the view
            GIS.SaveAll();

            LogMsg(sender, string.Format("The following nodes were created: [{0}].", nodeList));

            btn03_CreateEdgesAndFaces.Enabled = true;
        }

        /// <summary>
        /// Step [3] - connects nodes with edges; a closed ring of edges creates a face.
        /// Each edge starts and ends at an existing node (POINTS[0] -> [3] -> [5] -> [0])
        /// and may have any number of intermediate vertices. After the third edge
        /// closes the ring, the topology engine detects the enclosed area and creates
        /// face [1] automatically - faces are never added by hand.
        /// </summary>
        private void btn03_CreateEdgesAndFaces_Click(object sender, EventArgs e)
        {
            btn03_CreateEdgesAndFaces.Enabled = false;

            // set Edge layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(EdgeLayer);

            TGIS_ShapeArc arcShape = new TGIS_ShapeArc();
            List<TGIS_Point> pointList = new List<TGIS_Point>();
            StringBuilder edgeList = new StringBuilder();

            // edge 1: node POINTS[0] -> node POINTS[3] via two intermediate vertices
            pointList.Clear();
            pointList.Add(POINTS[0]);
            pointList.Add(POINTS[1]);
            pointList.Add(POINTS[2]);
            pointList.Add(POINTS[3]);
            long edgeId = topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointList));
            edgeList.Append(edgeId);

            // edge 2: node POINTS[3] -> node POINTS[5]
            pointList.Clear();
            pointList.Add(POINTS[3]);
            pointList.Add(POINTS[4]);
            pointList.Add(POINTS[5]);
            edgeId = topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointList));
            edgeList.Append(", " + edgeId);

            // edge 3: node POINTS[5] -> back to node POINTS[0];
            // this edge closes the ring, so face [1] is created automatically
            pointList.Clear();
            pointList.Add(POINTS[5]);
            pointList.Add(POINTS[6]);
            pointList.Add(POINTS[7]);
            pointList.Add(POINTS[0]);
            edgeId = topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointList));
            edgeList.Append(", " + edgeId);

            topoEditUtils.Commit();

            LogMsg(sender, string.Format(
                "The following edges were created: [{0}].\r\n" +
                "The face [1] was automatically created.",
                edgeList
            ));

            btn04_AddNodeSplittingAnEdge.Enabled = true;
        }

        /// <summary>
        /// Step [4] - adds nodes lying ON existing edges.
        /// An edge may only have nodes at its ends, so a node placed on an edge splits
        /// it into two edges. How the split is stored depends on SplitMergeMode:
        ///   false (default) - the original edge is shortened (keeps its ID) and one
        ///                     new edge is added for the remaining part,
        ///   true            - the original edge is deleted and two brand-new edges
        ///                     are added (SQLite only).
        /// The choice matters when other data refer to edge IDs.
        /// </summary>
        private void btn04_AddNodeSplittingAnEdge_Click(object sender, EventArgs e)
        {
            btn04_AddNodeSplittingAnEdge.Enabled = false;

            // set node layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(NodeLayer);

            TGIS_ShapePoint pointShape = new TGIS_ShapePoint();
            try
            {
                // `SplitMergeMode = false` (default) modifies existing edge and add new one
                topoEditUtils.Store.SplitMergeMode = false;

                // thanks to topology `Tolerance` this point will be snapped to POINTS[7]
                long nodeId = topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, TGIS_Utils.GisPoint(11.5, 39.3)));
                if (nodeId < 0)
                {
                    MessageBox.Show(topoEditUtils.Store.ErrorMsg);
                    return;
                }

                LogMsg(sender, string.Format(
                    "Mode: SplitMergeMode=False\r\n" +
                    "New node [{0}] was added.\r\n" +
                    "Edge [3] was split and shortened. Edge [4] was added.",
                    nodeId
                ));

                // `SplitMergeMode = true` deletes existing edge and add 2 new ones
                // This mode is available only for SQLite (native)
                topoEditUtils.Store.SplitMergeMode = true;

                nodeId = topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, POINTS[2]));
                if (nodeId < 0)
                {
                    MessageBox.Show(topoEditUtils.Store.ErrorMsg);
                    return;
                }

                LogMsg(sender, string.Format(
                    "Mode: SplitMergeMode=True\r\n" +
                    "New node [{0}] was added.\r\n" +
                    "Edge [1] was removed. New edges [5, 6] were added.",
                    nodeId
                ));
            }
            finally
            {
                // reset `SplitMergeMode` to default
                topoEditUtils.Store.SplitMergeMode = false;
            }

            // No Commit for SQLite/PostGIS: the global transaction stays open and
            // SetCurrentLayer in the next step continues it (StartGlobalTransaction does
            // nothing while a transaction is active), so these changes are committed
            // together with the next step. Oracle needs the commit here.
            if (TopoStorageType == TGIS_TopoStorageType.SDO)
            {
                topoEditUtils.Commit();
            }

            btn05_MoveNode.Enabled = true;
        }

        /// <summary>
        /// Step [5] - moves an existing node.
        /// ModifyNode changes the node position; the ends of all edges connected to
        /// the node move with it, and the faces bounded by those edges are reshaped
        /// accordingly. This is the key benefit of topology: one edit, consistent
        /// result for every object sharing the node.
        /// </summary>
        private void btn05_MoveNode_Click(object sender, EventArgs e)
        {
            btn05_MoveNode.Enabled = false;

            // set Node layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(NodeLayer);

            // node added in step [4] near (11.5, 39.3)
            long nodeId = 6;
            // previous topological operations resulted in different node numbering for Oracle
            // the user must know which node is to be modified
            if (TopoStorageType == TGIS_TopoStorageType.SDO)
                nodeId = 7;

            TGIS_ShapePoint pointShape = new TGIS_ShapePoint();

            // new position: move the node to the left (west)
            topoEditUtils.ModifyNode(nodeId, CreateShapePointFromPoint(pointShape, TGIS_Utils.GisPoint(4.0, 39.3)));

            topoEditUtils.Commit();

            // Oracle needs additional invalidation
            if (TopoStorageType == TGIS_TopoStorageType.SDO)
            {
                GIS.SaveAll();
                GIS.InvalidateWholeMap();
            }

            LogMsg(sender, string.Format("Node [{0}] was moved left.", nodeId));

            btn06_AddDanglingEdge.Enabled = true;
        }

        /// <summary>
        /// Step [6] - adds a dangling edge.
        /// The new edge connects the boundary node with an isolated node lying inside
        /// face [1]. Since it does not close any ring, no new face is created - the
        /// edge simply "hangs" inside the face. Such edges are valid in topology and
        /// are typical e.g. for dead-end roads or streams.
        /// </summary>
        private void btn06_AddDanglingEdge_Click(object sender, EventArgs e)
        {
            btn06_AddDanglingEdge.Enabled = false;

            // set Edge layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(EdgeLayer);

            // storage-specific IDs (see the note in the header)
            long nodeId1 = 4;
            long nodeId2 = 6;
            if (TopoStorageType == TGIS_TopoStorageType.SDO)
            {
                nodeId1 = 7;
                nodeId2 = 5;
            }

            TGIS_ShapeArc arcShape = new TGIS_ShapeArc();
            List<TGIS_Point> pointsList = new List<TGIS_Point>();

            // Store.GetNode reads a node object by its ID; here it is used to get
            // exact node coordinates, so the new edge starts and ends precisely
            // at the existing nodes
            TGIS_TopoNode node1 = topoEditUtils.Store.GetNode(nodeId1);
            TGIS_TopoNode node2 = topoEditUtils.Store.GetNode(nodeId2);

            pointsList.Add(node1.Point);
            pointsList.Add(node2.Point);
            long edgeId = topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            LogMsg(sender, string.Format(
                "New dangling Edge [{0}] was added. Created from Nodes [{1}, {2}].\r\n" +
                "Dangling edge is an edge connected to the network or another object at only one end, while the other end remains unconnected.",
                edgeId, nodeId1, nodeId2
            ));

            topoEditUtils.Commit();
            // Oracle needs additional invalidation
            if (TopoStorageType == TGIS_TopoStorageType.SDO)
            {
                GIS.SaveAll();
                GIS.InvalidateWholeMap();
            }

            btn07_AddEdgeSplittingAFace.Enabled = true;
        }

        /// <summary>
        /// Step [7] - adds an edge that splits a face.
        /// The new edge connects the free end of the dangling edge from step [6] with
        /// a node on the boundary of face [1]. Together, the dangling edge and the new
        /// edge form a path across the face from one side of its boundary to another,
        /// so the face is cut into two parts: one keeps the original face ID, the
        /// other gets a new ID. Edge/face relationships (left/right face of each
        /// edge) are updated by the engine.
        /// </summary>
        private void btn07_AddEdgeSplittingAFace_Click(object sender, EventArgs e)
        {
            btn07_AddEdgeSplittingAFace.Enabled = false;

            // set Edge layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(EdgeLayer);

            // free end of the dangling edge and a boundary node (storage-specific IDs)
            long nodeId1 = 4;
            long nodeId2 = 2;
            if (TopoStorageType == TGIS_TopoStorageType.SDO)
            {
                nodeId1 = 5;
                nodeId2 = 3;
            }

            TGIS_ShapeArc arcShape = new TGIS_ShapeArc();
            List<TGIS_Point> pointsList = new List<TGIS_Point>();

            TGIS_TopoNode node1 = topoEditUtils.Store.GetNode(nodeId1);
            TGIS_TopoNode node2 = topoEditUtils.Store.GetNode(nodeId2);

            // straight edge from node to node, crossing the face interior
            pointsList.Add(node1.Point);
            pointsList.Add(node2.Point);
            long edgeId = topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            // no Commit here - the transaction continues in the next step
            // and is committed there together with its changes
            LogMsg(sender, string.Format(
                "New edge [{0}] was added. Face [1] was split into two faces.",
                edgeId
            ));

            btn08_AddEdgeCreatingInnerFace.Enabled = true;
        }

        /// <summary>
        /// Step [8] - adds a closed edge inside a face.
        /// An edge whose start and end node are the same forms a ring by itself.
        /// Here the ring lies inside face [1], so the engine creates a new face for
        /// the ring interior and at the same time makes that area a hole of face [1]
        /// (exactly like an island in a lake).
        /// </summary>
        private void btn08_AddEdgeCreatingInnerFace_Click(object sender, EventArgs e)
        {
            btn08_AddEdgeCreatingInnerFace.Enabled = false;

            // set Edge layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(EdgeLayer);

            // isolated node created in step [2] (storage-specific ID)
            long nodeId = 5;
            if (TopoStorageType == TGIS_TopoStorageType.SDO)
                nodeId = 6;

            TGIS_ShapeArc arcShape = new TGIS_ShapeArc();
            List<TGIS_Point> pointsList = new List<TGIS_Point>();

            TGIS_TopoNode node = topoEditUtils.Store.GetNode(nodeId);

            // a loop: starts and ends at the same node (a triangle inside the face)
            pointsList.Add(node.Point);
            pointsList.Add(TGIS_Utils.GisPoint(35.0, 37.0));
            pointsList.Add(TGIS_Utils.GisPoint(27.0, 31.0));
            pointsList.Add(node.Point);
            long edgeId = topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            topoEditUtils.Commit();

            LogMsg(sender, string.Format(
                "New Edge [{0}] was added.\r\n" +
                "A hole was created in Face [1].\r\n" +
                "New Face [3] was created.",
                edgeId
            ));

            btn09_ModifyGeometryOfEdge.Enabled = true;
        }

        /// <summary>
        /// Step [9] - changes the shape of an existing edge.
        /// ModifyEdge replaces the edge vertices. The start and end points must stay
        /// on the same nodes (to move an end, move the node - see step [5]); only the
        /// shape between them may change. The faces on both sides of the edge are
        /// recalculated automatically.
        /// </summary>
        private void btn09_ModifyGeometryOfEdge_Click(object sender, EventArgs e)
        {
            btn09_ModifyGeometryOfEdge.Enabled = false;

            // storage-specific edge ID (see the note in the header)
            long edgeId = 3;
            if (TopoStorageType == TGIS_TopoStorageType.SDO)
                edgeId = 4;

            // set Edge layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(EdgeLayer);

            TGIS_ShapeArc arcShape = new TGIS_ShapeArc();
            List<TGIS_Point> pointsList = new List<TGIS_Point>();

            // read the current edge; its indexer returns the vertices
            TGIS_TopoEdge edge = topoEditUtils.Store.GetEdge(edgeId);

            // keep the existing vertices (including both end nodes)
            // and insert one extra vertex before the last one
            pointsList.Clear();
            pointsList.Add(edge[0]);
            pointsList.Add(edge[1]);
            pointsList.Add(TGIS_Utils.GisPoint(14.0, 28.0));
            pointsList.Add(edge[2]);

            // 0 = success, -1 = error (e.g. new geometry crosses another edge)
            if (topoEditUtils.ModifyEdge(edgeId, CreateShapeArcFromPointList(arcShape, pointsList)) != 0)
                MessageBox.Show(topoEditUtils.Store.ErrorMsg);

            topoEditUtils.Commit();

            LogMsg(sender, string.Format(
                "Edge [{0}] geometry was modified.\r\n" +
                "Face [1] was updated automatically.",
                edgeId
            ));

            btn10_DeleteNode.Enabled = true;
        }

        /// <summary>
        /// Step [10] - deletes a node joining exactly two edges.
        /// Removing such a node is the reverse of step [4]: the two edges that met at
        /// the node are merged into a single edge, and the node position becomes an
        /// ordinary vertex of that edge. (A node where three or more edges meet cannot
        /// be removed this way - the edges would have nowhere to end.)
        /// </summary>
        private void btn10_DeleteNode_Click(object sender, EventArgs e)
        {
            btn10_DeleteNode.Enabled = false;

            // set Node layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(NodeLayer);
            // storage-specific node ID (see the note in the header)
            long nodeId = 3;
            if (TopoStorageType == TGIS_TopoStorageType.SDO)
                nodeId = 4;

            // remember the node position - it is used below to find the merged edge
            TGIS_Point ptg = topoEditUtils.Store.GetNode(nodeId).Point;

            // 0 = success, -1 = error
            if (topoEditUtils.DeleteNode(nodeId) != 0)
                MessageBox.Show(topoEditUtils.Store.ErrorMsg);

            topoEditUtils.Commit();

            // The merged edge passes through the former node position. GetEdgeByPoint
            // is the typical way to identify an edge by location (e.g. a mouse click)
            // instead of by a known ID.
            long newEdgeId = topoEditUtils.Store.GetEdgeByPoint(ptg, topoEditUtils.Store.Tolerance);

            LogMsg(sender, string.Format(
                "Node [{0}] was deleted.\r\n" +
                "Edges were merged into Edge [{1}].",
                nodeId, newEdgeId
            ));

            btn11_DeleteEdge.Enabled = true;
        }

        /// <summary>
        /// Step [11] - deletes an edge separating two faces.
        /// This is the reverse of step [7]: once the edge between two faces is gone,
        /// there is nothing to separate them, so they are merged into one face.
        /// Nodes at the edge ends are kept.
        /// </summary>
        private void btn11_DeleteEdge_Click(object sender, EventArgs e)
        {
            btn11_DeleteEdge.Enabled = false;

            // set Edge layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(EdgeLayer);
            // edge [7] was created in step [7] - it splits the original face
            // (0 = success, -1 = error)
            if (topoEditUtils.DeleteEdge(7) != 0)
                MessageBox.Show(topoEditUtils.Store.ErrorMsg);

            topoEditUtils.Commit();

            LogMsg(sender,
                "Edge [7] was deleted.\r\n" +
                "Face [1] merged into Face [2]."
            );

            btn12_AddAdditionalPrimitives.Enabled = true;
        }

        /// <summary>
        /// Step [12] - adds more primitives as input data for feature layers.
        /// Three separate squares are built, each from two nodes and three edges:
        /// two L-shaped edges forming the outline and one diagonal edge. The diagonal
        /// splits each square into two triangular faces, so this step adds six new
        /// faces in total. Note that only nodes and edges are added - faces appear by
        /// themselves.
        /// </summary>
        private void btn12_AddAdditionalPrimitives_Click(object sender, EventArgs e)
        {
            btn12_AddAdditionalPrimitives.Enabled = false;

            // set Edge layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(EdgeLayer);

            TGIS_ShapePoint pointShape = new TGIS_ShapePoint();
            TGIS_ShapeArc arcShape = new TGIS_ShapeArc();
            List<TGIS_Point> pointsList = new List<TGIS_Point>();

            // --- square 1 (top-left) ---
            // two nodes at opposite corners
            topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, TGIS_Utils.GisPoint( 5.0, 72.0)));
            topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, TGIS_Utils.GisPoint(15.0, 62.0)));
            // top + right side
            pointsList.Clear();
            pointsList.Add(TGIS_Utils.GisPoint( 5.0, 72.0));
            pointsList.Add(TGIS_Utils.GisPoint(15.0, 72.0));
            pointsList.Add(TGIS_Utils.GisPoint(15.0, 62.0));
            topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            // left + bottom side - closes the square, one face is created
            pointsList.Clear();
            pointsList.Add(TGIS_Utils.GisPoint( 5.0, 72.0));
            pointsList.Add(TGIS_Utils.GisPoint( 5.0, 62.0));
            pointsList.Add(TGIS_Utils.GisPoint(15.0, 62.0));
            topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            // diagonal - splits the square face into two triangles
            pointsList.Clear();
            pointsList.Add(TGIS_Utils.GisPoint( 5.0, 72.0));
            pointsList.Add(TGIS_Utils.GisPoint(15.0, 62.0));
            topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            // --- square 2 (top-right) - built the same way ---
            topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, TGIS_Utils.GisPoint(40.0, 72.0)));
            topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, TGIS_Utils.GisPoint(50.0, 62.0)));
            pointsList.Clear();
            pointsList.Add(TGIS_Utils.GisPoint(40.0, 72.0));
            pointsList.Add(TGIS_Utils.GisPoint(50.0, 72.0));
            pointsList.Add(TGIS_Utils.GisPoint(50.0, 62.0));
            topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            pointsList.Clear();
            pointsList.Add(TGIS_Utils.GisPoint(40.0, 72.0));
            pointsList.Add(TGIS_Utils.GisPoint(40.0, 62.0));
            pointsList.Add(TGIS_Utils.GisPoint(50.0, 62.0));
            topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            pointsList.Clear();
            pointsList.Add(TGIS_Utils.GisPoint(40.0, 72.0));
            pointsList.Add(TGIS_Utils.GisPoint(50.0, 62.0));
            topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            // --- square 3 (right) - built the same way ---
            topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, TGIS_Utils.GisPoint(45.0, 35.0)));
            topoEditUtils.AddNewNode(CreateShapePointFromPoint(pointShape, TGIS_Utils.GisPoint(55.0, 25.0)));
            pointsList.Clear();
            pointsList.Add(TGIS_Utils.GisPoint(45.0, 35.0));
            pointsList.Add(TGIS_Utils.GisPoint(55.0, 35.0));
            pointsList.Add(TGIS_Utils.GisPoint(55.0, 25.0));
            topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            pointsList.Clear();
            pointsList.Add(TGIS_Utils.GisPoint(45.0, 35.0));
            pointsList.Add(TGIS_Utils.GisPoint(45.0, 25.0));
            pointsList.Add(TGIS_Utils.GisPoint(55.0, 25.0));
            topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));

            pointsList.Clear();
            pointsList.Add(TGIS_Utils.GisPoint(45.0, 35.0));
            pointsList.Add(TGIS_Utils.GisPoint(55.0, 25.0));
            topoEditUtils.AddNewEdge(CreateShapeArcFromPointList(arcShape, pointsList));
            topoEditUtils.Commit();

            // the topology now extends beyond the initial view
            GIS.FullExtent();

            LogMsg(sender, "Few additional nodes, edges and faces were added.");

            btn13_AddFeatureLayerPolyLevel0.Enabled = true;
        }

        /// <summary>
        /// Step [13] - creates polygon feature layer POLY0 (hierarchy level 0).
        /// So far we worked with primitives only. Real-world objects are stored as
        /// features in feature layers. A level 0 polygon layer is built directly on
        /// the FACE layer - each feature references one or more faces and its
        /// geometry is the union of those faces.
        /// The general recipe used here (and repeated in all following steps):
        ///   1. CreateNewFeatureLayer    - register the layer in the topology,
        ///                                 naming the layer it is built from,
        ///   2. GisCreateLayer + GIS.Add - open it in the viewer via its .ttkls file,
        ///   3. Store.GetLayer           - get the topology definition of the layer,
        ///   4. select source objects and call CreateAndAddFeatures,
        ///   5. SaveGeometryEdits + Commit.
        /// In this step every face becomes a separate feature.
        /// </summary>
        private void btn13_AddFeatureLayerPolyLevel0_Click(object sender, EventArgs e)
        {
            btn13_AddFeatureLayerPolyLevel0.Enabled = false;

            // set face layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(FaceLayer);

            // 1. Register a new feature layer in the topology. The second parameter
            //    is the layer the features are built from - here the FACE primitive
            //    layer, which makes POLY0 a level 0 polygon layer.
            //    (0 = success, -1 = error)
            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(POLY0_LAYER_NAME), FaceLayerName) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(POLY0_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            // make sure feature edits below run inside a global transaction
            if (!topoEditUtils.Transaction)
                topoEditUtils.StartGlobalTransaction(topoEditUtils.Store);

            // 2. Open the new layer in the viewer. CreateNewFeatureLayer has written
            //    a .ttkls access file for it; GetTopoLayerAccessFile returns its path
            //    and GisCreateLayer creates the proper layer class from it.
            string featureLayerPath = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(POLY0_LAYER_NAME));
            string featureLayerName = AddSchemaPrefix(NormalizeName(POLY0_LAYER_NAME));

            TGIS_LayerTopoSqlAbstract featureLayer0 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(featureLayerName, featureLayerPath);
            // feature layers share the coordinate system of the topology
            featureLayer0.SetCSByEPSG(topoEditUtils.Store.SRID);

            GIS.Add(featureLayer0);
            // hatch pattern + labels make features of each level easy to distinguish
            featureLayer0.Params.Area.Pattern = TGIS_BrushStyle.Horizontal;
            featureLayer0.Params.Labels.Field = UidFieldName;
            featureLayer0.Params.Labels.Color = TGIS_Color.Aqua;

            LogMsg(sender, string.Format(
                "New feature layer [{0}] was created from face layer [{1}] .",
                NormalizeName(POLY0_LAYER_NAME), FaceLayerName
            ));

            // 3. TGIS_TopoLayer is the topology-side definition of the layer (type,
            //    hierarchy level, parent/child relation); it is the destination
            //    parameter for CreateAndAddFeatures.
            TGIS_TopoLayer topoLayer = topoEditUtils.Store.GetLayer(NormalizeName(POLY0_LAYER_NAME));
            if (topoLayer == null)
            {
                MessageBox.Show(string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(POLY0_LAYER_NAME)
                ));
                return;
            }

            // 4. select all faces and creates features in feature layer
            //    (shapes returned by Loop are temporary cursor objects; MakeEditable
            //    returns a copy held by the layer, so the selection flag set on it
            //    survives after the loop moves on)
            int count = 0;
            foreach (TGIS_Shape shape in FaceLayer.Loop())
            {
                count++;
                TGIS_Shape shapeEdit = shape.MakeEditable();
                shapeEdit.IsSelected = true;
            }

            // When the source is a primitive layer (level 0), every selected
            // primitive becomes a separate feature. On higher levels all selected
            // features are grouped into one feature (see AddFeatures).
            if (topoEditUtils.CreateAndAddFeatures(FaceLayer, topoLayer) != -1)
                LogMsg(sender, string.Format(
                    "[{0}] features created and added to feature layer [{1}].",
                    count, NormalizeName(POLY0_LAYER_NAME)
                ));

            // 5. store the features and close the transaction
            topoEditUtils.SaveGeometryEdits();
            topoEditUtils.Commit();

            // hide primitive layers, so only the new feature layer is visible
            NodeLayer.Active = false;
            EdgeLayer.Active = false;
            FaceLayer.Active = false;

            gisLegend.GIS_Layer = featureLayer0;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            btn14_AddFeatureLayerPolyLevel1.Enabled = true;
        }

        /// <summary>
        /// Step [14] - creates hierarchical polygon layer POLY1 (level 1).
        /// Level 1 features are not built from faces but from features of the
        /// level 0 layer POLY0. Each new feature groups two POLY0 features - think of
        /// parcels (POLY0) grouped into blocks (POLY1). Geometry is still not
        /// copied: a POLY1 feature refers to POLY0 features, which refer to faces.
        /// If a face changes, POLY0 and POLY1 follow automatically.
        /// </summary>
        private void btn14_AddFeatureLayerPolyLevel1_Click(object sender, EventArgs e)
        {
            btn14_AddFeatureLayerPolyLevel1.Enabled = false;

            // set face layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(FaceLayer);

            // the source layer is now POLY0 (a feature layer, not a primitive one),
            // which makes POLY1 a level 1 hierarchical layer
            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(POLY1_LAYER_NAME), AddSchemaPrefix(NormalizeName(POLY0_LAYER_NAME))) != 0)
            {
                LogMsg(sender, string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(POLY1_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            if (!topoEditUtils.Transaction)
                topoEditUtils.StartGlobalTransaction(topoEditUtils.Store);

            // open the new layer in the viewer (same as in step [13])
            string fl1Name = AddSchemaPrefix(NormalizeName(POLY1_LAYER_NAME));
            string fl1Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(POLY1_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract featureLayer1 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl1Name, fl1Path);
            featureLayer1.SetCSByEPSG(topoEditUtils.Store.SRID);

            GIS.Add(featureLayer1);
            featureLayer1.Params.Area.Pattern = TGIS_BrushStyle.Vertical;
            featureLayer1.Params.Labels.Field = UidFieldName;
            featureLayer1.Params.Labels.Color = TGIS_Color.LightGray;

            LogMsg(sender, string.Format(
                "New feature layer [{0}] was created from feature layer [{1}] .",
                NormalizeName(POLY1_LAYER_NAME), NormalizeName(POLY0_LAYER_NAME)
            ));

            // source features are taken from the POLY0 viewer layer,
            // the destination is the topology definition of POLY1
            TGIS_LayerTopoSqlAbstract featureLayer0 = (TGIS_LayerTopoSqlAbstract)GIS.Get(AddSchemaPrefix(NormalizeName(POLY0_LAYER_NAME)));
            TGIS_TopoLayer topoLayer1 = topoEditUtils.Store.GetLayer(NormalizeName(POLY1_LAYER_NAME));
            if (topoLayer1 == null)
            {
                MessageBox.Show(string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(POLY1_LAYER_NAME)
                ));
                return;
            }

            // Four POLY1 features are created, each from a pair of POLY0 features.
            // Feature IDs in POLY0 differ for SQLite, hence the storage-specific ranges.

            // first set of conversion
            int index1 = 1, index2 = 2;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 2; index2 = 3;
            }
            AddFeatures(sender, index1, index2, featureLayer0, topoLayer1);

            // next set of conversion
            index1 = 3; index2 = 4;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 4; index2 = 5;
            }
            AddFeatures(sender, index1, index2, featureLayer0, topoLayer1);

            // next set of conversion
            index1 = 5; index2 = 6;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 6; index2 = 7;
            }
            AddFeatures(sender, index1, index2, featureLayer0, topoLayer1);

            // next set of conversion
            index1 = 7; index2 = 8;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 8; index2 = 9;
            }
            AddFeatures(sender, index1, index2, featureLayer0, topoLayer1);

            topoEditUtils.Commit();

            // show only the newest level
            featureLayer0.Active = false;

            gisLegend.GIS_Layer = featureLayer1;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            btn15_AddFeatureLayerPolyLevel2.Enabled = true;
        }

        /// <summary>
        /// Step [15] - creates polygon layer POLY2 (level 2) from POLY1 features.
        /// Same recipe as in step [14], one level higher: every POLY2 feature
        /// groups two POLY1 features.
        /// </summary>
        private void btn15_AddFeatureLayerPolyLevel2_Click(object sender, EventArgs e)
        {
            btn15_AddFeatureLayerPolyLevel2.Enabled = false;

            // set face layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(FaceLayer);

            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(POLY2_LAYER_NAME), AddSchemaPrefix(NormalizeName(POLY1_LAYER_NAME))) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(POLY2_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            if (!topoEditUtils.Transaction)
                topoEditUtils.StartGlobalTransaction(topoEditUtils.Store);

            string fl2Name = AddSchemaPrefix(NormalizeName(POLY2_LAYER_NAME));
            string fl2Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(POLY2_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract featureLayer2 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl2Name, fl2Path);
            featureLayer2.SetCSByEPSG(topoEditUtils.Store.SRID);

            GIS.Add(featureLayer2);
            featureLayer2.Params.Area.Pattern = TGIS_BrushStyle.Cross;
            featureLayer2.Params.Labels.Field = UidFieldName;
            featureLayer2.Params.Labels.Color = TGIS_Color.Navy;

            LogMsg(sender, string.Format(
                "New feature layer [{0}] was created from feature layer [{1}] .",
                NormalizeName(POLY2_LAYER_NAME), NormalizeName(POLY1_LAYER_NAME)
            ));

            TGIS_LayerTopoSqlAbstract featureLayer1 = (TGIS_LayerTopoSqlAbstract)GIS.Get(AddSchemaPrefix(NormalizeName(POLY1_LAYER_NAME)));

            TGIS_TopoLayer topoLayer2 = topoEditUtils.Store.GetLayer(NormalizeName(POLY2_LAYER_NAME));
            if (topoLayer2 == null)
            {
                MessageBox.Show(string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(POLY2_LAYER_NAME)
                ));
                return;
            }

            // two POLY2 features: POLY1 [1, 2] and POLY1 [3, 4]
            AddFeatures(sender, 1, 2, featureLayer1, topoLayer2);
            AddFeatures(sender, 3, 4, featureLayer1, topoLayer2);

            topoEditUtils.Commit();
            featureLayer1.Active = false;

            gisLegend.GIS_Layer = featureLayer2;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            btn16_AddFeatureLayerPolyLevel3.Enabled = true;
        }

        /// <summary>
        /// Step [16] - creates polygon layer POLY3 (level 3) from POLY2 features.
        /// The top of the hierarchy: a single POLY3 feature groups the two POLY2
        /// features. Its geometry is resolved through three levels of features
        /// down to the faces - and still contains no geometry of its own.
        /// </summary>
        private void btn16_AddFeatureLayerPolyLevel3_Click(object sender, EventArgs e)
        {
            btn16_AddFeatureLayerPolyLevel3.Enabled = false;

            // set face layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(FaceLayer);

            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(POLY3_LAYER_NAME), AddSchemaPrefix(NormalizeName(POLY2_LAYER_NAME))) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(POLY3_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            if (!topoEditUtils.Transaction)
                topoEditUtils.StartGlobalTransaction(topoEditUtils.Store);

            string fl3Name = AddSchemaPrefix(NormalizeName(POLY3_LAYER_NAME));
            string fl3Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(POLY3_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract featureLayer3 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl3Name, fl3Path);
            featureLayer3.SetCSByEPSG(topoEditUtils.Store.SRID);

            GIS.Add(featureLayer3);
            featureLayer3.Params.Area.Pattern = TGIS_BrushStyle.DiagCross;
            featureLayer3.Params.Labels.Field = UidFieldName;
            featureLayer3.Params.Labels.Color = TGIS_Color.Gray;

            LogMsg(sender, string.Format(
                "New feature layer [{0}] was created from feature layer [{1}] .",
                NormalizeName(POLY3_LAYER_NAME), NormalizeName(POLY2_LAYER_NAME)
            ));

            TGIS_LayerTopoSqlAbstract featureLayer2 = (TGIS_LayerTopoSqlAbstract)GIS.Get(AddSchemaPrefix(NormalizeName(POLY2_LAYER_NAME)));

            TGIS_TopoLayer topoLayer3 = topoEditUtils.Store.GetLayer(NormalizeName(POLY3_LAYER_NAME));
            if (topoLayer3 == null)
            {
                MessageBox.Show(string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(POLY3_LAYER_NAME)
                ));
                return;
            }

            // one POLY3 feature made of both POLY2 features
            // (storage-specific IDs, see the note in the header)
            int index1 = 1, index2 = 2;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 5; index2 = 6;
            }
            AddFeatures(sender, index1, index2, featureLayer2, topoLayer3);

            topoEditUtils.Commit();
            featureLayer2.Active = false;

            gisLegend.GIS_Layer = featureLayer3;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            btn17_AddFeatureLayersLine.Enabled = true;
        }

        /// <summary>
        /// Step [17] - creates line feature layers LINE0..LINE3 (levels 0-3).
        /// Exactly the same hierarchy as for polygons, but built on the EDGE layer:
        ///   LINE0 - one feature per edge (level 0),
        ///   LINE1 - groups of LINE0 features (e.g. road segments -> roads),
        ///   LINE2 - groups of LINE1 features,
        ///   LINE3 - groups of LINE2 features.
        /// Line and polygon features can share the same edges: moving an edge
        /// updates both a road (line feature) and the parcels it borders
        /// (polygon features).
        /// </summary>
        private void btn17_AddFeatureLayersLine_Click(object sender, EventArgs e)
        {
            btn17_AddFeatureLayersLine.Enabled = false;

            // hide the polygon hierarchy and show edges - the source for LINE0
            ((TGIS_LayerVector)GIS.Get(AddSchemaPrefix(NormalizeName(POLY3_LAYER_NAME)))).Active = false;
            EdgeLayer.Active = true;
            GIS.InvalidateWholeMap();

            // --- Level 0 ---
            // set EDGE layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(EdgeLayer);

            // LINE0 is built from the EDGE primitive layer
            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(LINE0_LAYER_NAME), EdgeLayerName) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(LINE0_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            if (!topoEditUtils.Transaction)
                topoEditUtils.StartGlobalTransaction(topoEditUtils.Store);

            string fl0Name = AddSchemaPrefix(NormalizeName(LINE0_LAYER_NAME));
            string fl0Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(LINE0_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract fl0 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl0Name, fl0Path);
            fl0.SetCSByEPSG(topoEditUtils.Store.SRID);
            GIS.Add(fl0);
            fl0.Params.Line.Color = TGIS_Color.Aqua;
            fl0.Params.Labels.Field = UidFieldName;
            fl0.Params.Labels.Color = TGIS_Color.Aqua;

            TGIS_TopoLayer lt0 = topoEditUtils.Store.GetLayer(NormalizeName(LINE0_LAYER_NAME));
            if (lt0 == null)
            {
                MessageBox.Show(string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(LINE0_LAYER_NAME)
                ));
                return;
            }

            // select all edges  and creates features in feature layer
            int count = 0;
            foreach (TGIS_Shape shape in EdgeLayer.Loop())
            {
                count++;
                TGIS_Shape shapeEdit = shape.MakeEditable();
                shapeEdit.IsSelected = true;
            }

            // primitive source -> one LINE0 feature per selected edge
            if (topoEditUtils.CreateAndAddFeatures(EdgeLayer, lt0) != -1)
                LogMsg(sender, string.Format(
                    "[{0}] features created and added to feature layer [{1}].",
                    count, NormalizeName(LINE0_LAYER_NAME)
                ));

            topoEditUtils.SaveGeometryEdits();
            topoEditUtils.Commit();

            EdgeLayer.Active = false;
            gisLegend.GIS_Layer = fl0;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            // --- Level 1 ---
            // set LINE0 layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(fl0);

            // LINE1 is built from LINE0 features
            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(LINE1_LAYER_NAME), AddSchemaPrefix(NormalizeName(LINE0_LAYER_NAME))) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(LINE1_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            // feature layer - level 1
            string fl1Name = AddSchemaPrefix(NormalizeName(LINE1_LAYER_NAME));
            string fl1Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(LINE1_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract fl1 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl1Name, fl1Path);
            fl1.SetCSByEPSG(topoEditUtils.Store.SRID);
            GIS.Add(fl1);
            fl1.Params.Line.Color = TGIS_Color.Fuchsia;
            fl1.Params.Labels.Field = UidFieldName;
            fl1.Params.Labels.Color = TGIS_Color.Fuchsia;

            // layer topo - level 1
            TGIS_TopoLayer lt1 = topoEditUtils.Store.GetLayer(NormalizeName(LINE1_LAYER_NAME));
            if (lt1 == null)
            {
                LogMsg(sender, string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(LINE1_LAYER_NAME)
                ));
                return;
            }

            // Four LINE1 features: the first one groups the edges of the original
            // polygon, the next three group the edges of each square from step [12].
            // (storage-specific ID ranges, see the note in the header)
            int index1 = 1, index2 = 6;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 3; index2 = 9;
            }
            AddFeatures(sender, index1, index2, fl0, lt1);

            index1 = 7; index2 = 9;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 10; index2 = 12;
            }
            AddFeatures(sender, index1, index2, fl0, lt1);

            index1 = 10; index2 = 12;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 13; index2 = 15;
            }
            AddFeatures(sender, index1, index2, fl0, lt1);

            index1 = 13; index2 = 15;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 16; index2 = 18;
            }
            AddFeatures(sender, index1, index2, fl0, lt1);

            topoEditUtils.Commit();
            fl0.Active = false;

            gisLegend.GIS_Layer = fl1;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            // --- Level 2 ---
            // set LINE1 as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(fl1);

            // LINE2 is built from LINE1 features
            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(LINE2_LAYER_NAME), AddSchemaPrefix(NormalizeName(LINE1_LAYER_NAME))) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(LINE2_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            // feature layer - level 2
            string fl2Name = AddSchemaPrefix(NormalizeName(LINE2_LAYER_NAME));
            string fl2Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(LINE2_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract fl2 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl2Name, fl2Path);
            fl2.SetCSByEPSG(topoEditUtils.Store.SRID);
            GIS.Add(fl2);
            fl2.Params.Line.Color = TGIS_Color.Lime;
            fl2.Params.Labels.Field = UidFieldName;
            fl2.Params.Labels.Color = TGIS_Color.Lime;

            // layer topo - level 2
            TGIS_TopoLayer lt2 = topoEditUtils.Store.GetLayer(NormalizeName(LINE2_LAYER_NAME));
            if (lt2 == null)
            {
                MessageBox.Show(string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(LINE2_LAYER_NAME)
                ));
                return;
            }

            // two LINE2 features: LINE1 [1, 2] and LINE1 [3, 4]
            AddFeatures(sender, 1, 2, fl1, lt2);
            AddFeatures(sender, 3, 4, fl1, lt2);

            topoEditUtils.Commit();
            fl1.Active = false;

            gisLegend.GIS_Layer = fl2;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            // --- Level 3 ---
            // set LINE2 as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(fl2);

            // LINE3 is built from LINE2 features
            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(LINE3_LAYER_NAME), AddSchemaPrefix(NormalizeName(LINE2_LAYER_NAME))) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(LINE3_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            // feature layer - level 3
            string fl3Name = AddSchemaPrefix(NormalizeName(LINE3_LAYER_NAME));
            string fl3Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(LINE3_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract fl3 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl3Name, fl3Path);
            fl3.SetCSByEPSG(topoEditUtils.Store.SRID);
            GIS.Add(fl3);
            fl3.Params.Line.Color = TGIS_Color.Maroon;
            fl3.Params.Labels.Field = UidFieldName;
            fl3.Params.Labels.Color = TGIS_Color.Maroon;

            // layer topo - level 3
            TGIS_TopoLayer lt3 = topoEditUtils.Store.GetLayer(NormalizeName(LINE3_LAYER_NAME));
            if (lt3 == null)
            {
                MessageBox.Show(string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(LINE3_LAYER_NAME)
                ));
                return;
            }

            // one LINE3 feature made of both LINE2 features
            index1 = 1; index2 = 2;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 5; index2 = 6;
            }
            AddFeatures(sender, index1, index2, fl2, lt3);

            topoEditUtils.Commit();
            fl2.Active = false;

            gisLegend.GIS_Layer = fl3;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            btn18_AddFeatureLayersPoint.Enabled = true;
        }

        /// <summary>
        /// Step [18] - creates point feature layers POINT0..POINT3 (levels 0-3).
        /// The last variant of the same hierarchy, built on the NODE layer:
        /// POINT0 holds one feature per node, higher levels group features of the
        /// level below (a multipoint feature, e.g. all hydrants of one district).
        /// Note: on Oracle the tutorial stops after level 0.
        /// </summary>
        private void btn18_AddFeatureLayersPoint_Click(object sender, EventArgs e)
        {
            btn18_AddFeatureLayersPoint.Enabled = false;

            // hide the line hierarchy and show nodes - the source for POINT0
            ((TGIS_LayerVector)GIS.Get(AddSchemaPrefix(NormalizeName(LINE3_LAYER_NAME)))).Active = false;
            NodeLayer.Active = true;
            GIS.InvalidateWholeMap();

            // --- Level 0 ---
            // set NODE layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(NodeLayer);

            // POINT0 is built from the NODE primitive layer
            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(POINT0_LAYER_NAME), NodeLayerName) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(POINT0_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            if (!topoEditUtils.Transaction)
                topoEditUtils.StartGlobalTransaction(topoEditUtils.Store);

            string fl0Name = AddSchemaPrefix(NormalizeName(POINT0_LAYER_NAME));
            string fl0Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(POINT0_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract fl0 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl0Name, fl0Path);
            fl0.SetCSByEPSG(topoEditUtils.Store.SRID);
            GIS.Add(fl0);
            fl0.Params.Marker.Color = TGIS_Color.Aqua;
            fl0.Params.Labels.Field = UidFieldName;
            fl0.Params.Labels.Color = TGIS_Color.Aqua;

            TGIS_TopoLayer lt0 = topoEditUtils.Store.GetLayer(NormalizeName(POINT0_LAYER_NAME));
            if (lt0 == null)
            {
                MessageBox.Show(string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(POINT0_LAYER_NAME)
                ));
                return;
            }

            // select all nodes and creates features in feature layer
            int count = 0;
            foreach (TGIS_Shape shape in NodeLayer.Loop())
            {
                count++;
                TGIS_Shape shapeEdit = shape.MakeEditable();
                shapeEdit.IsSelected = true;
            }

            // primitive source -> one POINT0 feature per selected node
            if (topoEditUtils.CreateAndAddFeatures(NodeLayer, lt0) != -1)
                LogMsg(sender, string.Format(
                    "[{0}] features created and added to feature layer [{1}].",
                    count, NormalizeName(POINT0_LAYER_NAME)
                ));

            topoEditUtils.SaveGeometryEdits();
            topoEditUtils.Commit();

            NodeLayer.Active = false;
            gisLegend.GIS_Layer = fl0;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            // Oracle: point hierarchy is demonstrated on level 0 only
            if (TopoStorageType == TGIS_TopoStorageType.SDO)
            {
                btn18_AddFeatureLayersPoint.Enabled = false;
                btn19_CloseTutorial.Enabled = true;
                return;
            }

            // --- Level 1 ---
            // set POINT0 layer as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(fl0);

            // POINT1 is built from POINT0 features
            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(POINT1_LAYER_NAME), AddSchemaPrefix(NormalizeName(POINT0_LAYER_NAME))) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(POINT1_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            // feature layer - level 1
            string fl1Name = AddSchemaPrefix(NormalizeName(POINT1_LAYER_NAME));
            string fl1Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(POINT1_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract fl1 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl1Name, fl1Path);
            fl1.SetCSByEPSG(topoEditUtils.Store.SRID);
            GIS.Add(fl1);
            fl1.Params.Marker.Color = TGIS_Color.Fuchsia;
            fl1.Params.Labels.Field = UidFieldName;
            fl1.Params.Labels.Color = TGIS_Color.Fuchsia;

            // layer topo - level 1
            TGIS_TopoLayer lt1 = topoEditUtils.Store.GetLayer(NormalizeName(POINT1_LAYER_NAME));
            if (lt1 == null)
            {
                LogMsg(sender, string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(POINT1_LAYER_NAME)
                ));
                return;
            }

            // Four POINT1 features: the first one groups the nodes of the original
            // polygon, the next three group the two nodes of each square from step [12].
            // (storage-specific ID ranges, see the note in the header)
            int index1 = 1, index2 = 6;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 1; index2 = 7;
            }
            AddFeatures(sender, index1, index2, fl0, lt1);

            index1 = 7; index2 = 8;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 8; index2 = 9;
            }
            AddFeatures(sender, index1, index2, fl0, lt1);

            index1 = 9; index2 = 10;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 10; index2 = 11;
            }
            AddFeatures(sender, index1, index2, fl0, lt1);

            index1 = 11; index2 = 12;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 12; index2 = 13;
            }
            AddFeatures(sender, index1, index2, fl0, lt1);

            topoEditUtils.Commit();
            fl0.Active = false;

            gisLegend.GIS_Layer = fl1;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            // --- Level 2 ---
            // set POINT1 as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(fl1);

            // POINT2 is built from POINT1 features
            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(POINT2_LAYER_NAME), AddSchemaPrefix(NormalizeName(POINT1_LAYER_NAME))) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(POINT2_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            // feature layer - level 2
            string fl2Name = AddSchemaPrefix(NormalizeName(POINT2_LAYER_NAME));
            string fl2Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(POINT2_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract fl2 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl2Name, fl2Path);
            fl2.SetCSByEPSG(topoEditUtils.Store.SRID);
            GIS.Add(fl2);
            fl2.Params.Marker.Color = TGIS_Color.Lime;
            fl2.Params.Labels.Field = UidFieldName;
            fl2.Params.Labels.Color = TGIS_Color.Lime;

            // layer topo - level 2
            TGIS_TopoLayer lt2 = topoEditUtils.Store.GetLayer(NormalizeName(POINT2_LAYER_NAME));
            if (lt2 == null)
            {
                LogMsg(sender, string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(POINT2_LAYER_NAME)
                ));
                return;
            }

            // two POINT2 features: POINT1 [1, 2] and POINT1 [3, 4]
            AddFeatures(sender, 1, 2, fl1, lt2);
            AddFeatures(sender, 3, 4, fl1, lt2);

            topoEditUtils.Commit();
            fl1.Active = false;

            gisLegend.GIS_Layer = fl2;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            // --- Level 3 ---
            // set POINT2 as current layer, start new global transaction
            topoEditUtils.SetCurrentLayer(fl2);

            // POINT3 is built from POINT2 features
            if (topoEditUtils.CreateNewFeatureLayer(NormalizeName(POINT3_LAYER_NAME), AddSchemaPrefix(NormalizeName(POINT2_LAYER_NAME))) != 0)
            {
                MessageBox.Show(string.Format(
                    "Failed to create feature layer [{0}]:\r\n" +
                    "{1}",
                    NormalizeName(POINT3_LAYER_NAME), topoEditUtils.Store.ErrorMsg
                ));
                return;
            }

            // feature layer - level 3
            string fl3Name = AddSchemaPrefix(NormalizeName(POINT3_LAYER_NAME));
            string fl3Path = topoEditUtils.GetTopoLayerAccessFile(TopoFolder, TopoSchemaName, NormalizeName(POINT3_LAYER_NAME));
            TGIS_LayerTopoSqlAbstract fl3 = (TGIS_LayerTopoSqlAbstract)TGIS_Utils.GisCreateLayer(fl3Name, fl3Path);
            fl3.SetCSByEPSG(topoEditUtils.Store.SRID);
            GIS.Add(fl3);
            fl3.Params.Marker.Color = TGIS_Color.Maroon;
            fl3.Params.Labels.Field = UidFieldName;
            fl3.Params.Labels.Color = TGIS_Color.Maroon;

            // layer topo - level 3
            TGIS_TopoLayer lt3 = topoEditUtils.Store.GetLayer(NormalizeName(POINT3_LAYER_NAME));
            if (lt3 == null)
            {
                LogMsg(sender, string.Format(
                    "Feature layer [{0}] was not found in the store.",
                    NormalizeName(POINT3_LAYER_NAME)
                ));
                return;
            }

            // one POINT3 feature made of both POINT2 features
            index1 = 1; index2 = 2;
            if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
            {
                index1 = 5; index2 = 6;
            }
            AddFeatures(sender, index1, index2, fl2, lt3);

            topoEditUtils.Commit();
            fl2.Active = false;

            gisLegend.GIS_Layer = fl3;
            GIS.SaveAll();
            GIS.InvalidateWholeMap();

            btn19_CloseTutorial.Enabled = true;
        }

        /// <summary>
        /// Step [19] - ends the tutorial. The topology and its project remain in the
        /// output folder and can be opened later in any TatukGIS application.
        /// </summary>
        private void btn19_CloseTutorial_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Fills shp with a single-part polyline made of pList vertices and returns
        /// it. The same shape object is reused for many edges, so it is Reset first.
        /// Lock(Projection) means the coordinates are given in the layer (topology)
        /// coordinate system - no reprojection from the viewer CS is applied.
        /// </summary>
        public TGIS_ShapeArc CreateShapeArcFromPointList(TGIS_ShapeArc shp, List<TGIS_Point> pList)
        {
            if (shp == null)
                return null;
            if (pList == null)
                return null;

            // clear geometry left from the previous use
            shp.Reset();
            // batch geometry update; points are in the native (layer) CS
            shp.Lock(TGIS_Lock.Projection);
            try
            {
                shp.AddPart();

                for (int i = 0; i < pList.Count; i++)
                    shp.AddPoint(pList[i]);
            }
            finally
            {
                shp.Unlock();
            }

            return shp;
        }

        /// <summary>
        /// Fills shp with a single point and returns it - the point counterpart of
        /// CreateShapeArcFromPointList, used to pass node geometry to the API.
        /// </summary>
        public TGIS_ShapePoint CreateShapePointFromPoint(TGIS_ShapePoint shp, TGIS_Point ptg)
        {
            if (shp == null)
                return null;

            // clear geometry left from the previous use
            shp.Reset();
            shp.Lock(TGIS_Lock.Projection);
            try
            {
                shp.AddPart();
                shp.AddPoint(ptg);
            }
            finally
            {
                shp.Unlock();
            }

            return shp;
        }

        /// <summary>
        /// Each storage has its own store class with the same abstract interface,
        /// so the rest of the tutorial works with TGIS_TopoStoreAbstract only.
        /// PostGIS and Oracle stores are part of the Enterprise edition, so they are
        /// created by storage type through TopoStoreManager.
        /// The caller owns the returned object.
        /// </summary>
        public TGIS_TopoStoreAbstract CreateTopoStore(TGIS_TopoStorageType topoStorageType)
        {
            switch (topoStorageType)
            {
                case TGIS_TopoStorageType.SQLITE: return new TGIS_TopoStoreNative();
                case TGIS_TopoStorageType.PGIS:   return TatukGIS.NDK.__Global.TopoStoreManager().CreateInstance(topoStorageType);
                case TGIS_TopoStorageType.SDO:    return TatukGIS.NDK.__Global.TopoStoreManager().CreateInstance(topoStorageType);
                default: throw new Exception("Unknown Topology Storage Type. Tutorial terminated");
            }
        }

        /// <summary>
        /// Default parameters: output goes to the 'Out' folder next to the
        /// executable, topology schema is named TEST_TOPO.
        /// PostGIS and Oracle options are disabled unless their stores are
        /// registered (Enterprise edition installed). Whether the license includes
        /// the Enterprise edition is checked by the Developer Kernel itself when the
        /// topology layers are opened; a failure is reported by step [1].
        /// </summary>
        private void WinForm_Load(object sender, EventArgs e)
        {
            // On .NET topology stores are registered lazily (upon the first layer
            // creation), so register them explicitly - otherwise creating a topology
            // before any layer is opened fails with "Topology process cancelled"
            TatukGIS.NDK.__Global.SelfRegisterTopoStores();

            edtTopoFolder.Text = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Out");
            edtTopoSchemaName.Text = "TEST_TOPO";

            if (!TatukGIS.NDK.__Global.TopoStoreManager().IsRegistered("TGIS_TopoStorePgis"))
            {
                rbtnPostgis.Enabled = false;
                rbtnPostgis.Text = "PostGIS (Enterprise only)";
            }

            if (!TatukGIS.NDK.__Global.TopoStoreManager().IsRegistered("TGIS_TopoStoreSdo"))
            {
                rbtnOracle.Enabled = false;
                rbtnOracle.Text = "Oracle (Enterprise only)";
            }

            // start maximized when the default window size does not fit the screen
            Rectangle workingArea = Screen.FromControl(this).WorkingArea;
            if (Width > workingArea.Width || Height > workingArea.Height)
                WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Reports why step [1] failed together with what the selected storage
        /// needs. Database storages need a license that includes the Enterprise
        /// edition. The Developer Kernel does not distribute the database client
        /// libraries: they come with PostgreSQL / Oracle client installations and
        /// must match the platform of the application (32 or 64-bit). The Developer
        /// Kernel looks for them in the x86 / x64 subfolder of the application folder,
        /// then in the application folder and finally on the system PATH.
        /// </summary>
        private void showStorageError(string msg)
        {
            string hint;
            switch (TopoStorageType)
            {
                case TGIS_TopoStorageType.PGIS:
                    hint = "PostGIS storage needs:\r\n" +
                           "- a Developer Kernel license that includes the Enterprise edition,\r\n" +
                           "- the PostgreSQL client library libpq.dll with its dependencies,\r\n" +
                           "  built for the same platform as this application (32 or 64-bit),\r\n" +
                           "  in the x86 / x64 subfolder of the application folder,\r\n" +
                           "  in the application folder or on the PATH,\r\n" +
                           "- a running PostgreSQL server with the postgis and postgis_topology\r\n" +
                           "  extensions, reachable with the connection parameters on the form.";
                    break;
                case TGIS_TopoStorageType.SDO:
                    hint = "Oracle storage needs:\r\n" +
                           "- a Developer Kernel license that includes the Enterprise edition,\r\n" +
                           "- the Oracle client library oci.dll (e.g. Oracle Instant Client),\r\n" +
                           "  built for the same platform as this application (32 or 64-bit),\r\n" +
                           "  in the x86 / x64 subfolder of the application folder,\r\n" +
                           "  in the application folder or on the PATH,\r\n" +
                           "- a running Oracle database with Spatial topology support,\r\n" +
                           "  reachable with the connection parameters on the form.";
                    break;
                default:
                    hint = "";
                    break;
            }

            if (hint != "")
            {
                // a client library of the other platform cannot be loaded at all
                string platform;
                if (Environment.Is64BitProcess)
                    platform = "This application runs as a 64-bit process, so the client library must be 64-bit too.";
                else
                    platform = "This application runs as a 32-bit process, so the client library must be 32-bit too.\r\n" +
                               "PostgreSQL and Oracle installations provide 64-bit client libraries,\r\n" +
                               "so make sure the 64-bit version of the application is used.";

                hint = "\r\n\r\n" + platform + "\r\n\r\n" + hint;
            }

            MessageBox.Show(
                string.Format("Topology {0} [{1}] cannot be created:", TopoSchemaName, TopoStorageTypeString) + "\r\n" +
                msg + hint + "\r\n\r\n" +
                "The tutorial returns to step [0], so the storage type and the parameters can be changed."
            );
        }

        /// <summary>
        /// Step [1] failed, so the tutorial goes back to step [0]: the storage type
        /// and all parameters can be changed and the tutorial started again
        /// (a new run of step [1] drops whatever the failed run left behind).
        /// </summary>
        private void restartTutorial()
        {
            btn01_CreateEmptyTopology.Enabled = false;
            btn00_StartTutorial.Enabled = true;
            pnlTopoParams.Enabled = true;
        }

        /// <summary>
        /// Connection string (.ttkls format) for database storages.
        /// SQLite needs none - its database file is created in TopoFolder.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                switch (TopoStorageType)
                {
                    case TGIS_TopoStorageType.PGIS: return prepareConnectionStringPgis();
                    case TGIS_TopoStorageType.SDO:  return prepareConnectionStringSdo();
                    default: return "";
                }
            }
        }

        public string DbName
        {
            get { return edtDbName.Text; }
        }

        /// <summary>
        /// Find the primitive layers opened from the topology project by name.
        /// The layer class depends on the storage, but all of them descend from
        /// TGIS_LayerTopoSqlAbstract, which is all the tutorial needs.
        /// PostGIS and Oracle layer classes are part of the Enterprise edition,
        /// so they are referred to by the base class only.
        /// </summary>
        public TGIS_LayerTopoSqlAbstract EdgeLayer
        {
            get { return (TGIS_LayerTopoSqlAbstract)GIS.Get(EdgeLayerName); }
        }

        public TGIS_LayerTopoSqlAbstract FaceLayer
        {
            get { return (TGIS_LayerTopoSqlAbstract)GIS.Get(FaceLayerName); }
        }

        public TGIS_LayerTopoSqlAbstract NodeLayer
        {
            get { return (TGIS_LayerTopoSqlAbstract)GIS.Get(NodeLayerName); }
        }

        /// <summary>
        /// Primitive table names differ per storage (TOPO_*_NAME_* constants);
        /// in the project they are prefixed with the schema name.
        /// </summary>
        public string EdgeLayerName
        {
            get
            {
                string result = "";
                switch (TopoStorageType)
                {
                    case TGIS_TopoStorageType.SQLITE: result = TatukGIS.NDK.__Global.TOPO_EDGE_NAME_SQLITE; break;
                    case TGIS_TopoStorageType.PGIS:   result = TatukGIS.NDK.__Global.TOPO_EDGE_NAME_PGIS;   break;
                    case TGIS_TopoStorageType.SDO:    result = TatukGIS.NDK.__Global.TOPO_EDGE_NAME_SDO;    break;
                }

                return AddSchemaPrefix(result);
            }
        }

        public string FaceLayerName
        {
            get
            {
                string result = "";
                switch (TopoStorageType)
                {
                    case TGIS_TopoStorageType.SQLITE: result = TatukGIS.NDK.__Global.TOPO_FACE_NAME_SQLITE; break;
                    case TGIS_TopoStorageType.PGIS:   result = TatukGIS.NDK.__Global.TOPO_FACE_NAME_PGIS;   break;
                    case TGIS_TopoStorageType.SDO:    result = TatukGIS.NDK.__Global.TOPO_FACE_NAME_SDO;    break;
                }

                return AddSchemaPrefix(result);
            }
        }

        public string Host
        {
            get { return edtHost.Text; }
        }

        public string NodeLayerName
        {
            get
            {
                string result = "";
                switch (TopoStorageType)
                {
                    case TGIS_TopoStorageType.SQLITE: result = TatukGIS.NDK.__Global.TOPO_NODE_NAME_SQLITE; break;
                    case TGIS_TopoStorageType.PGIS:   result = TatukGIS.NDK.__Global.TOPO_NODE_NAME_PGIS;   break;
                    case TGIS_TopoStorageType.SDO:    result = TatukGIS.NDK.__Global.TOPO_NODE_NAME_SDO;    break;
                }

                return AddSchemaPrefix(result);
            }
        }

        public string Password
        {
            get { return edtPass.Text; }
        }

        public int Port
        {
            get { return int.Parse(edtPort.Text); }
        }

        /// <summary>
        /// Maps the combo box selection to TGIS_TopologyEditMode.
        /// </summary>
        public TGIS_TopologyEditMode TopoEditMode
        {
            get
            {
                switch (cbxTopoEditMode.SelectedIndex)
                {
                    case 0:  return TGIS_TopologyEditMode.Autocommit;
                    case 1:  return TGIS_TopologyEditMode.Transaction;
                    default: return TGIS_TopologyEditMode.Transaction;
                }
            }
        }

        /// <summary>
        /// Extent of the topology (EPSG:4326 degrees) - covers all objects created
        /// by the tutorial.
        /// </summary>
        public TGIS_Extent TopoExtent
        {
            get { return TGIS_Utils.GisExtent(4, 25, 55, 72); }
        }

        /// <summary>
        /// Output folder; a separate subfolder per storage type (Out\SQLITE, ...)
        /// lets the tutorial be run for every storage without conflicts.
        /// </summary>
        public string TopoFolder
        {
            get { return Path.Combine(edtTopoFolder.Text, TopoStorageTypeString); }
        }

        /// <summary>
        /// Topology name passed to CreateTopology/DropTopology; on Oracle it is the
        /// same as the schema name.
        /// </summary>
        public string TopoName
        {
            get
            {
                if (TopoStorageType == TGIS_TopoStorageType.SDO)
                    return TopoSchemaName;
                else
                    return TopoSchemaName + "_DB";
            }
        }

        /// <summary>
        /// Path of the .ttkproject file written by CreateTopology.
        /// </summary>
        public string TopoProjectPath
        {
            get { return Path.Combine(TopoFolder, string.Format("{0}.ttkproject", TopoSchemaName)); }
        }

        /// <summary>
        /// PostgreSQL folds unquoted identifiers to lower case, Oracle and SQLite
        /// layers use upper case - the schema name follows the database convention.
        /// </summary>
        public string TopoSchemaName
        {
            get
            {
                if (TopoStorageType == TGIS_TopoStorageType.PGIS)
                    return edtTopoSchemaName.Text.ToLowerInvariant();
                else
                    return edtTopoSchemaName.Text.ToUpperInvariant();
            }
        }

        public TGIS_TopoStorageType TopoStorageType
        {
            get
            {
                if (rbtnSqlite.Checked)
                    return TGIS_TopoStorageType.SQLITE;
                else if (rbtnPostgis.Checked)
                    return TGIS_TopoStorageType.PGIS;
                else if (rbtnOracle.Checked)
                    return TGIS_TopoStorageType.SDO;
                else
                    return TGIS_TopoStorageType.UNKNOWN;
            }
        }

        public string TopoStorageTypeString
        {
            get
            {
                switch (TopoStorageType)
                {
                    case TGIS_TopoStorageType.SQLITE: return "SQLITE";
                    case TGIS_TopoStorageType.PGIS:   return "PGIS";
                    case TGIS_TopoStorageType.SDO:    return "SDO";
                    default: return "";
                }
            }
        }

        /// <summary>
        /// Field holding the topology ID of an object, used for map labels.
        /// SQLite layers expose it as TOPO_ID, database layers as GIS_UID.
        /// </summary>
        public string UidFieldName
        {
            get
            {
                if (TopoStorageType == TGIS_TopoStorageType.SQLITE)
                    return TatukGIS.NDK.__Global.TOPO_ID;
                else
                    return TatukGIS.NDK.__Global.GIS_FIELD_UID;
            }
        }

        public string User
        {
            get { return edtUser.Text; }
        }

        public void HideDbParams()
        {
            grpDb.Visible = false;
        }

        /// <summary>
        /// Appends a step result to the log box (prefixed by the button caption).
        /// </summary>
        public void LogMsg(object sender, string msg)
        {
            if (sender is Button)
                mmLog.AppendText(string.Format("{0}:", ((Button)sender).Text) + "\r\n");

            mmLog.AppendText(msg + "\r\n");
            mmLog.AppendText("--------------------------------------------------\r\n");
        }

        /// <summary>
        /// Viewer layer name = &lt;schema&gt;_&lt;layer&gt;, e.g. TEST_TOPO_POLY0.
        /// </summary>
        public string AddSchemaPrefix(string name)
        {
            return TopoSchemaName + "_" + name;
        }

        /// <summary>
        /// Adjusts the letter case of a layer name to the database convention
        /// (lower case for PostGIS, upper case otherwise).
        /// </summary>
        public string NormalizeName(string name)
        {
            if (TopoStorageType == TGIS_TopoStorageType.PGIS)
                return name.ToLowerInvariant();
            else
                return name.ToUpperInvariant();
        }

        /// <summary>
        /// Builds a .ttkls connection string for PostGIS accessed via libpq.
        /// 'Storage=Topo' tells the SQL layer that the database holds a topology.
        /// The '%s' in the Layer entry is a placeholder - the topology store fills it
        /// in (with the topology schema) when it uses the connection string.
        /// A literal \n (backslash + n) is the TatukGIS line break escape.
        /// </summary>
        private string prepareConnectionStringPgis()
        {
            StringBuilder connectionString = new StringBuilder();
            connectionString.AppendLine("[TatukGIS Layer]");
            connectionString.AppendLine("Dialect=POSTGRESQL");
            connectionString.AppendLine("ENGINEOPTIONS=16");
            connectionString.AppendLine("GeometryFieldCast=Native");
            connectionString.AppendLine("IndexColumn=gid");
            connectionString.AppendLine(@"GeometryColumn=geom\n");
            connectionString.AppendLine("Layer=%s;" + DbName);
            connectionString.AppendLine(string.Format("LIBPQ=host={0} port={1} dbname={2} user={3} password={4}", Host, Port, DbName, User, Password));
            connectionString.AppendLine(@"Storage=Topo\n");
            connectionString.AppendLine(TatukGIS.NDK.__Global.GIS_TTKLS_EXT);

            return connectionString.ToString();
        }

        /// <summary>
        /// Builds a .ttkls connection string for Oracle Spatial accessed via OCI
        /// (user/password@host:port/service).
        /// </summary>
        private string prepareConnectionStringSdo()
        {
            StringBuilder connectionString = new StringBuilder();
            connectionString.AppendLine("[TatukGIS Layer]");
            connectionString.AppendLine("Dialect=ORACLE");
            connectionString.AppendLine("ENGINEOPTIONS=16");
            connectionString.AppendLine("Layer=%s;");
            connectionString.AppendLine(string.Format("OCI={0}/{1}@{2}:{3}/{4}", User, Password, Host, Port, DbName));
            connectionString.AppendLine(@"Storage=Topo\n");
            connectionString.AppendLine(@"REUSEQUERY=False\n");
            connectionString.AppendLine(TatukGIS.NDK.__Global.GIS_TTKLS_EXT);

            return connectionString.ToString();
        }

        /// <summary>
        /// Overrides the default DB parameters with values from the optional
        /// TopologyTutorial.local ini file, e.g.:
        ///   [PGIS]
        ///   DbName=postgres
        ///   Host=localhost
        ///   Port=5432
        ///   User=postgres
        ///   Password=...
        /// The file keeps credentials out of the source code.
        /// </summary>
        private void loadLocalDbParams(string section)
        {
            // optional, private connection parameters (not distributed with the sample)
            string iniPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "TopologyTutorial.local");
            if (!File.Exists(iniPath))
                return;

            Dictionary<string, string> ini = readIniSection(iniPath, section);
            edtDbName.Text = readIniString(ini, "DbName",   edtDbName.Text);
            edtHost.Text   = readIniString(ini, "Host",     edtHost.Text);
            edtPort.Text   = readIniString(ini, "Port",     edtPort.Text);
            edtUser.Text   = readIniString(ini, "User",     edtUser.Text);
            edtPass.Text   = readIniString(ini, "Password", edtPass.Text);
        }

        /// <summary>
        /// Reads all "name=value" entries of one [section] of an ini file.
        /// Section and entry names are case-insensitive, as in Windows ini files.
        /// </summary>
        private static Dictionary<string, string> readIniSection(string path, string section)
        {
            Dictionary<string, string> result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            bool inSection = false;

            foreach (string rawLine in File.ReadAllLines(path))
            {
                string line = rawLine.Trim();
                if (line.Length == 0 || line.StartsWith(";"))
                    continue;

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    inSection = string.Equals(line.Substring(1, line.Length - 2).Trim(), section, StringComparison.OrdinalIgnoreCase);
                    continue;
                }

                int pos = line.IndexOf('=');
                if (inSection && pos > 0)
                {
                    string name = line.Substring(0, pos).Trim();
                    if (!result.ContainsKey(name))
                        result[name] = line.Substring(pos + 1).Trim();
                }
            }

            return result;
        }

        private static string readIniString(Dictionary<string, string> ini, string name, string defaultValue)
        {
            string value;
            if (ini.TryGetValue(name, out value))
                return value;
            else
                return defaultValue;
        }

        /// <summary>
        /// Storage selection: database storages show connection parameters filled
        /// with typical defaults (then overridden by TopologyTutorial.local);
        /// SQLite needs no connection at all.
        /// </summary>
        private void rbtnOracle_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbtnOracle.Checked)
                return;

            ShowDbParams();
            edtDbName.Text = "FREEPDB1";
            edtHost.Text = "localhost";
            edtPort.Text = "1521";
            edtUser.Text = "";
            edtPass.Text = "";
            loadLocalDbParams("SDO");
        }

        private void rbtnPostgis_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbtnPostgis.Checked)
                return;

            ShowDbParams();
            edtDbName.Text = "postgres";
            edtHost.Text = "localhost";
            edtPort.Text = "5432";
            edtUser.Text = "postgres";
            edtPass.Text = "";
            loadLocalDbParams("PGIS");
        }

        private void rbtnSqlite_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbtnSqlite.Checked)
                return;

            HideDbParams();
        }

        public void ShowDbParams()
        {
            grpDb.Visible = true;
        }
    }
}
