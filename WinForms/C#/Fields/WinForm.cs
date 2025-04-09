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

        private System.Windows.Forms.CheckBox chckbxUseSymbols;
        private System.Windows.Forms.Button btnCreateLayer;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_Legend;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.TGIS_DataSet GIS_DataSet1;
        private System.Windows.Forms.DataGridView dataGrid1;
        private System.Windows.Forms.StatusStrip stsbr1;
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }

    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnCreateLayer_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;
            TGIS_Shape shp;
            int i;
            double an;
            Random rand;

            GIS.Close();

            rand = new Random();

            lv = new TGIS_LayerVector();
            lv.Name = "Fields";
            lv.Open();

            lv.AddField("rotateLabel", TGIS_FieldType.Float, 10, 4);
            lv.AddField("rotateSymbol", TGIS_FieldType.Float, 10, 4);
            lv.AddField("color", TGIS_FieldType.Number, 10, 0);
            lv.AddField("outlinecolor", TGIS_FieldType.Number, 10, 0);
            lv.AddField("size", TGIS_FieldType.Number, 10, 0);
            lv.AddField("label", TGIS_FieldType.String, 1, 0);
            lv.AddField("position", TGIS_FieldType.String, 1, 0);
            lv.AddField("scale", TGIS_FieldType.Float, 10, 4);

            for (i = 0; i < 11; i++)
            {
                shp = lv.CreateShape(TGIS_ShapeType.Point);
                shp.Lock(TGIS_Lock.Projection);
                shp.AddPart();
                shp.AddPoint(TGIS_Utils.GisPoint(rand.Next(20), rand.Next(20)));
                an = rand.Next(360);
                shp.SetField("rotateLabel", an);
                shp.SetField("rotateSymbol", an);
                shp.SetField("color", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
                shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
                shp.SetField("label", "Point" + Convert.ToString(i));
                shp.SetField("size", rand.Next(400));
                shp.SetField("position", TGIS_Utils.ConstructParamPosition((TGIS_LabelPosition)(rand.Next(9))));
                shp.SetField("scale", Math.PI / 180);
                shp.Unlock();
            }

            shp = lv.CreateShape(TGIS_ShapeType.Arc);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            for (i = 0; i < 11; i++)
                shp.AddPoint(TGIS_Utils.GisPoint(rand.Next(20) - 10, rand.Next(20) - 10));
            an = rand.Next(360);
            shp.SetField("rotateLabel", an);
            shp.SetField("rotateSymbol", an);
            shp.SetField("color", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
            shp.SetField("label", "Point" + Convert.ToString(1));
            shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
            shp.SetField("scale", Math.PI / 180);
            shp.Unlock();

            for (i = 1; i < 12; i++)
            {
                shp = lv.CreateShape(TGIS_ShapeType.Arc);
                shp.Lock(TGIS_Lock.Extent);
                shp.AddPart();
                shp.AddPoint(TGIS_Utils.GisPoint(20 + 2 * i, 0));
                shp.AddPoint(TGIS_Utils.GisPoint(30 + 2 * i, 10));
                an = rand.Next(360);
                shp.SetField("rotateLabel", an);
                shp.SetField("rotateSymbol", an);
                shp.SetField("size", i);
                shp.SetField("color", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
                shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
                shp.SetField("scale", Math.PI / 180);
                shp.Unlock();
            }

            shp = lv.CreateShape(TGIS_ShapeType.Polygon);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            for (i = 0; i < 4; i++)
                shp.AddPoint(TGIS_Utils.GisPoint(rand.Next(20) - 10, rand.Next(20) - 10));
            an = rand.Next(360);
            shp.SetField("rotateLabel", an);
            shp.SetField("rotateSymbol", an);
            shp.SetField("color", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
            shp.SetField("outlinecolor", (rand.Next(256) << 16) + (rand.Next(256) << 8) + rand.Next(256));
            shp.SetField("label", "Point" + Convert.ToString(2));
            shp.Unlock();

            lv.Params.Marker.ColorAsText = "FIELD:color";
            lv.Params.Marker.OutlineColorAsText = "FIELD:outlinecolor";
            lv.Params.Marker.OutlineWidth = 1;
            lv.Params.Marker.Size = 20 * 20; //converting points to twips -> 1pt = 20 twips

            if (chckbxUseSymbols.Checked)
            {
                lv.Params.Marker.Symbol = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\2267.cgm");
                lv.Params.Marker.SizeAsText = "FIELD:size:1 dip";
                lv.Params.Marker.SymbolRotateAsText = "FIELD:rotateSymbol";
            }

            lv.Params.Labels.Field = "label";
            lv.Params.Labels.Allocator = false;
            lv.Params.Labels.ColorAsText = "FIELD:color";
            lv.Params.Labels.OutlineColorAsText = "FIELD:outlinecolor";
            lv.Params.Labels.PositionAsText = "FIELD:position";
            lv.Params.Labels.FontColorAsText = "FIELD:color";
            lv.Params.Labels.RotateAsText = "FIELD:rotateLabel:1 deg";


            if (chckbxUseSymbols.Checked)
            {
                lv.Params.Line.Symbol = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\1301.cgm");
                lv.Params.Line.SymbolRotateAsText = "FIELD:rotateSymbol:1 deg";
            }
            lv.Params.Line.ColorAsText = "FIELD:color";
            lv.Params.Line.OutlineColorAsText = "FIELD:outlinecolor";
            lv.Params.Line.WidthAsText = "FIELD:size:1 dip";

            lv.Params.Area.SymbolRotateAsText = "rotateSymbol";
            if (chckbxUseSymbols.Checked)
                lv.Params.Area.Symbol = TGIS_Utils.SymbolList.Prepare(TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\1301.cgm");
            lv.Params.Area.ColorAsText = "FIELD:color";
            lv.Params.Area.OutlineColorAsText = "FIELD:outlinecolor";

            GIS.Add(lv);
            GIS.FullExtent();
            GIS_Legend.GIS_Layer = lv;
            GIS_Legend.Update();
            GIS_DataSet1.Open(lv, lv.Extent);
            dataGrid1.DataSource = GIS_DataSet1.Tables[0];
        }

        private void DataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            GIS.InvalidateWholeMap();
        }
    }
}
