/* Fields sample — demonstrates attribute field creation and data-driven rendering.

   What the sample shows:
     - Defining custom attribute fields on in-memory vector layer via AddField
     - Field properties: name, type (TGIS_FieldType), width, decimal precision
     - Writing field values to individual shapes via TGIS_Shape.SetField
     - Data-driven rendering using "FIELD:<name>" expression syntax
     - Per-attribute colour assignment via field values
     - Per-attribute size control via field values
     - Per-attribute rotation via field values
     - Label text from field values
     - Label positioning from field values
     - Exposing layer attribute table via TGIS_DataSet
     - Displaying attributes in DataGridView grid control
     - Interactive grid editing with automatic map refresh
     - Lock types: Projection (layer CS) vs Extent (world coordinates)

   Key TatukGIS API concepts shown here:
     TGIS_ViewerWnd              - main visual map control
     TGIS_LayerVector            - vector layer with attribute schema
     TGIS_LayerVector.AddField() - define custom attribute field
     TGIS_FieldType              - data type enumeration (String, Integer, Date, etc.)
     TGIS_Shape.SetField()       - update feature attribute value
     TGIS_DataSet                - attribute table accessor
     "FIELD:<name>" expression   - dynamic rendering based on field value
     TGIS_Params.Marker          - point symbol parameters (colour, size, rotation)
     TGIS_Params.Line            - line rendering parameters
     TGIS_Params.Area            - polygon fill parameters
     TGIS_Params.Labels          - label text and positioning
     TGIS_Lock                   - coordinate system locking (Projection vs Extent)
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Fields
{
    // -------------------------------------------------------------------------
    // Designer-generated partial class: layout and control initialization only.
    // The application logic lives in the second partial declaration below.
    // -------------------------------------------------------------------------
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.chckbxUseSymbols = new System.Windows.Forms.CheckBox();
            this.btnCreateLayer = new System.Windows.Forms.Button();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_DataSet1 = new TatukGIS.NDK.TGIS_DataSet();
            this.dataGrid1 = new System.Windows.Forms.DataGridView();
            this.stsbr1 = new System.Windows.Forms.StatusStrip();
            ((System.ComponentModel.ISupportInitialize)(this.GIS_DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
            this.SuspendLayout();
            //
            // chckbxUseSymbols
            //
            this.chckbxUseSymbols.AutoSize = true;
            this.chckbxUseSymbols.Location = new System.Drawing.Point(81, 4);
            this.chckbxUseSymbols.Name = "chckbxUseSymbols";
            this.chckbxUseSymbols.Size = new System.Drawing.Size(87, 17);
            this.chckbxUseSymbols.TabIndex = 1;
            this.chckbxUseSymbols.Text = "Use Symbols";
            this.chckbxUseSymbols.UseVisualStyleBackColor = true;
            //
            // btnCreateLayer
            //
            this.btnCreateLayer.Location = new System.Drawing.Point(0, 0);
            this.btnCreateLayer.Name = "btnCreateLayer";
            this.btnCreateLayer.Size = new System.Drawing.Size(75, 23);
            this.btnCreateLayer.TabIndex = 2;
            this.btnCreateLayer.Text = "Create Layer";
            this.btnCreateLayer.UseVisualStyleBackColor = true;
            this.btnCreateLayer.Click += new System.EventHandler(this.btnCreateLayer_Click);
            //
            // GIS_Legend
            //
            this.GIS_Legend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_Legend.GIS_Group = null;
            this.GIS_Legend.GIS_Layer = null;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(0, 27);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)));
            this.GIS_Legend.ReverseOrder = true;
            this.GIS_Legend.Size = new System.Drawing.Size(141, 320);
            this.GIS_Legend.TabIndex = 3;
            //
            // GIS
            //
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Location = new System.Drawing.Point(141, 27);
            this.GIS.MinZoomSize = -5;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.SelectionTransparency = 50;
            this.GIS.Size = new System.Drawing.Size(483, 320);
            this.GIS.TabIndex = 4;
            //
            // GIS_DataSet1
            //
            this.GIS_DataSet1.DataSetName = "NewDataSet";
            //
            // dataGrid1
            //
            this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGrid1.DataMember = "";
            //this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGrid1.Location = new System.Drawing.Point(0, 347);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(624, 145);
            this.dataGrid1.TabIndex = 5;
            this.dataGrid1.CurrentCellChanged += DataGrid1_CurrentCellChanged;
            //
            // stsbr1
            //
            this.stsbr1.Location = new System.Drawing.Point(0, 492);
            this.stsbr1.Name = "stsbr1";
            this.stsbr1.Size = new System.Drawing.Size(624, 22);
            this.stsbr1.TabIndex = 7;
            this.stsbr1.Text = "Open a layer properties form to change parameters";
            //
            // frmMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(624, 514);
            this.Controls.Add(this.stsbr1);
            this.Controls.Add(this.dataGrid1);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GIS_Legend);
            this.Controls.Add(this.btnCreateLayer);
            this.Controls.Add(this.chckbxUseSymbols);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Fields";
            ((System.ComponentModel.ISupportInitialize)(this.GIS_DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        // Form-level control field declarations
        private System.Windows.Forms.CheckBox chckbxUseSymbols;  // Toggle CGM symbol rendering
        private System.Windows.Forms.Button btnCreateLayer;       // Triggers layer creation
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_Legend; // Layer legend panel
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;            // Interactive map viewer
        private TatukGIS.NDK.TGIS_DataSet GIS_DataSet1;               // Bridges layer to DataGridView
        private System.Windows.Forms.DataGridView dataGrid1;           // Attribute table grid
        private System.Windows.Forms.StatusStrip stsbr1;               // Status bar
    }

    /// <summary>
    /// Application entry point.
    /// </summary>
    static class Program
    {
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
            Application.Run(new frmMain());
        }
    }

    /// <summary>
    /// Main form for the Fields sample.
    /// Demonstrates defining attribute fields on a vector layer and using
    /// per-shape field values to drive rendering parameters such as color,
    /// size, rotation, label text, and label placement position.
    /// </summary>
    public partial class frmMain : Form
    {
        /// <summary>
        /// Initialises the form and its designer-generated controls.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the "Create Layer" button click.
        /// Builds an in-memory TGIS_LayerVector, adds typed attribute fields,
        /// populates it with randomly-positioned Point, Arc, and Polygon shapes,
        /// assigns per-shape field values, then wires those fields into the
        /// layer's rendering parameters via "FIELD:&lt;name&gt;" expressions.
        /// </summary>
        private void btnCreateLayer_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;  // In-memory vector layer that owns all shapes and fields
            TGIS_Shape shp;       // Individual shape being created (Point / Arc / Polygon)
            int i;                // Loop counter
            double an;            // Random rotation angle (degrees) for label and symbol
            Random rand;

            GIS.Close();  // Discard any previously loaded layers before rebuilding

            rand = new Random();

            // Create a new in-memory vector layer.  No file path is given, so
            // the layer lives entirely in RAM and is not persisted to disk.
            lv = new TGIS_LayerVector();
            lv.Name = "Fields";
            lv.Open();  // Must be opened before adding fields or shapes

            // -----------------------------------------------------------------
            // Field definitions
            // AddField(name, type, width, decimals)
            //   TGIS_FieldType.Float  : floating-point value with decimal places
            //   TGIS_FieldType.Number : integer-compatible numeric field (decimals=0)
            //   TGIS_FieldType.String : text field; width is the character count
            //
            // These fields are later referenced in rendering-parameter
            // "FIELD:<name>" expressions so the DK renderer reads their values
            // per shape at draw time.
            // -----------------------------------------------------------------
            lv.AddField("rotateLabel",  TGIS_FieldType.Float,  10, 4); // Label rotation (degrees)
            lv.AddField("rotateSymbol", TGIS_FieldType.Float,  10, 4); // Symbol/marker rotation (degrees)
            lv.AddField("color",        TGIS_FieldType.Number, 10, 0); // Fill/stroke color as packed RGB integer
            lv.AddField("outlinecolor", TGIS_FieldType.Number, 10, 0); // Outline color as packed RGB integer
            lv.AddField("size",         TGIS_FieldType.Number, 10, 0); // Symbol or line width size value
            lv.AddField("label",        TGIS_FieldType.String,  1, 0); // Text to display as the shape label
            lv.AddField("position",     TGIS_FieldType.String,  1, 0); // Encoded label placement position
            lv.AddField("scale",        TGIS_FieldType.Float,  10, 4); // Scale factor (Pi/180 for deg->rad)

            // -----------------------------------------------------------------
            // Create 11 Point shapes at random locations within [0,20] x [0,20].
            // Each point receives its own random color, rotation, label, and
            // label-position value.
            // TGIS_Lock.Projection: coordinates are interpreted in the layer's CRS.
            // -----------------------------------------------------------------
            for (i = 0; i < 11; i++)
            {
                shp = lv.CreateShape(TGIS_ShapeType.Point);
                shp.Lock(TGIS_Lock.Projection);  // Coordinates are in the layer's coordinate system
                shp.AddPart();                    // A Point shape requires exactly one part
                shp.AddPoint(TGIS_Utils.GisPoint(rand.Next(20), rand.Next(20)));

                an = rand.Next(360);  // One random angle drives both label and symbol rotation

                // SetField(name, value) writes the attribute for the current shape.
                shp.SetField("rotateLabel",  an);
                shp.SetField("rotateSymbol", an);

                // Pack three 8-bit random channel values: 0x00RRGGBB
                shp.SetField("color",        (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
                shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
                shp.SetField("label",        "Point" + Convert.ToString(i));
                shp.SetField("size",         rand.Next(400));

                // ConstructParamPosition encodes a TGIS_LabelPosition value as
                // the string token expected by Params.Labels.PositionAsText.
                shp.SetField("position", TGIS_Utils.ConstructParamPosition((TGIS_LabelPosition)(rand.Next(9))));

                // Pi/180 is stored so the renderer can convert raw degree values
                // from rotateLabel/rotateSymbol fields to radians when needed.
                shp.SetField("scale", Math.PI / 180);
                shp.Unlock();  // Commit geometry and attributes; shape joins the layer
            }

            // -----------------------------------------------------------------
            // One multi-vertex Arc (open polyline) with 11 random vertices
            // spread around the origin [-10,10] x [-10,10].
            // TGIS_Lock.Extent: coordinates are in world/extent space.
            // -----------------------------------------------------------------
            shp = lv.CreateShape(TGIS_ShapeType.Arc);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            for (i = 0; i < 11; i++)
                shp.AddPoint(TGIS_Utils.GisPoint(rand.Next(20) - 10, rand.Next(20) - 10));
            an = rand.Next(360);
            shp.SetField("rotateLabel",  an);
            shp.SetField("rotateSymbol", an);
            shp.SetField("color",        (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
            shp.SetField("label",        "Point" + Convert.ToString(1));
            shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
            shp.SetField("scale",        Math.PI / 180);
            shp.Unlock();

            // -----------------------------------------------------------------
            // 11 two-vertex Arc shapes with incrementing "size" field values
            // (i = 1..11), arranged in a horizontal row.
            // This group demonstrates that "FIELD:size" drives line width so
            // each segment is drawn progressively thicker.
            // -----------------------------------------------------------------
            for (i = 1; i < 12; i++)
            {
                shp = lv.CreateShape(TGIS_ShapeType.Arc);
                shp.Lock(TGIS_Lock.Extent);
                shp.AddPart();
                shp.AddPoint(TGIS_Utils.GisPoint(20 + 2 * i, 0));
                shp.AddPoint(TGIS_Utils.GisPoint(30 + 2 * i, 10));
                an = rand.Next(360);
                shp.SetField("rotateLabel",  an);
                shp.SetField("rotateSymbol", an);
                shp.SetField("size",         i);  // Increasing size produces visibly thicker lines
                shp.SetField("color",        (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
                shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
                shp.SetField("scale",        Math.PI / 180);
                shp.Unlock();
            }

            // -----------------------------------------------------------------
            // One Polygon with 4 random vertices near the origin.
            // -----------------------------------------------------------------
            shp = lv.CreateShape(TGIS_ShapeType.Polygon);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            for (i = 0; i < 4; i++)
                shp.AddPoint(TGIS_Utils.GisPoint(rand.Next(20) - 10, rand.Next(20) - 10));
            an = rand.Next(360);
            shp.SetField("rotateLabel",  an);
            shp.SetField("rotateSymbol", an);
            shp.SetField("color",        (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
            shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
            shp.SetField("label",        "Point" + Convert.ToString(2));
            shp.Unlock();

            // =================================================================
            // Rendering parameter configuration
            //
            // The "FIELD:<fieldname>" token in any *AsText property tells the
            // DK renderer to evaluate the named field on each shape at draw time
            // instead of using a single static value.  An optional unit suffix
            // (":1 dip", ":1 deg") converts the raw numeric value to the
            // expected rendering unit.
            // =================================================================

            // --- Point / Marker rendering ---
            lv.Params.Marker.ColorAsText        = "FIELD:color";        // Per-shape fill color
            lv.Params.Marker.OutlineColorAsText = "FIELD:outlinecolor"; // Per-shape outline color
            lv.Params.Marker.OutlineWidth       = 1;                    // Static 1-pixel outline
            lv.Params.Marker.Size               = 20 * 20; // Size in twips; 1 point = 20 twips

            if (chckbxUseSymbols.Checked)
            {
                // Prepare() registers the CGM file in the SymbolList cache and
                // returns a handle used by the renderer to look up the symbol.
                lv.Params.Marker.Symbol             = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() + @"Symbols\2267.cgm");
                lv.Params.Marker.SizeAsText         = "FIELD:size:1 dip";       // Size from field, device-independent pixels
                lv.Params.Marker.SymbolRotateAsText = "FIELD:rotateSymbol";     // Symbol rotation from field
            }

            // --- Label rendering ---
            lv.Params.Labels.Field              = "label";              // Field that supplies label text
            lv.Params.Labels.Allocator          = false;                // Disable automatic conflict avoidance
            lv.Params.Labels.ColorAsText        = "FIELD:color";        // Label background color from field
            lv.Params.Labels.OutlineColorAsText = "FIELD:outlinecolor"; // Label outline color from field
            lv.Params.Labels.PositionAsText     = "FIELD:position";    // Label anchor position from field
            lv.Params.Labels.FontColorAsText    = "FIELD:color";        // Font color from field
            lv.Params.Labels.RotateAsText       = "FIELD:rotateLabel:1 deg"; // Label rotation from field, in degrees

            // --- Line / Arc rendering ---
            if (chckbxUseSymbols.Checked)
            {
                lv.Params.Line.Symbol             = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() + @"Symbols\1301.cgm");
                lv.Params.Line.SymbolRotateAsText = "FIELD:rotateSymbol:1 deg";
            }
            lv.Params.Line.ColorAsText        = "FIELD:color";
            lv.Params.Line.OutlineColorAsText = "FIELD:outlinecolor";
            lv.Params.Line.WidthAsText        = "FIELD:size:1 dip"; // Line width from "size" field

            // --- Area / Polygon rendering ---
            lv.Params.Area.SymbolRotateAsText = "rotateSymbol"; // Note: no FIELD: prefix here intentionally
            if (chckbxUseSymbols.Checked)
                lv.Params.Area.Symbol = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() + @"Symbols\1301.cgm");
            lv.Params.Area.ColorAsText        = "FIELD:color";
            lv.Params.Area.OutlineColorAsText = "FIELD:outlinecolor";

            // Add the finished layer to the viewer, zoom to fit, refresh the
            // legend panel, and expose the attribute table via TGIS_DataSet so
            // the DataGridView can display and edit field values.
            GIS.Add(lv);
            GIS.FullExtent();
            GIS_Legend.GIS_Layer = lv;
            GIS_Legend.Update();

            // Open the dataset over the full layer extent, then bind the first
            // (and only) DataTable produced by TGIS_DataSet to the grid.
            GIS_DataSet1.Open(lv, lv.Extent);
            dataGrid1.DataSource = GIS_DataSet1.Tables[0];
        }

        /// <summary>
        /// Handles current-cell changes in the attribute table grid.
        /// Redrawing the map ensures any field edits made in the grid
        /// (color, size, label text, etc.) are immediately visible in the viewer.
        /// </summary>
        private void DataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            GIS.InvalidateWholeMap();
        }
    }
}
