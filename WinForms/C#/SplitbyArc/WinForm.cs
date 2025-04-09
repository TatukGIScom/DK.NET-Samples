using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace SplitbyArc
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel paLeft;
        private System.Windows.Forms.Button btnLine;
        private System.Windows.Forms.Button btnSplit;
        private System.Windows.Forms.GroupBox gboxResult;
        private System.Windows.Forms.Label lbInfo;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TGIS_LayerVector layerObj;      //layer for new shapes
        private TGIS_ShapePolygon shpPolygon;   //shape for split
        private TGIS_ShapeArc shpArc;               //shape for line
        private TGIS_LayerVector layerPolygon;
        private TGIS_LayerVector layerArc;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.paLeft = new System.Windows.Forms.Panel();
            this.gboxResult = new System.Windows.Forms.GroupBox();
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnSplit = new System.Windows.Forms.Button();
            this.btnLine = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.paLeft.SuspendLayout();
            this.gboxResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Click mouse to add line points.";
            this.toolStripLabel1.Width = 575;
            // 
            // paLeft
            // 
            this.paLeft.Controls.Add(this.gboxResult);
            this.paLeft.Controls.Add(this.btnSplit);
            this.paLeft.Controls.Add(this.btnLine);
            this.paLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.paLeft.Location = new System.Drawing.Point(0, 0);
            this.paLeft.Name = "paLeft";
            this.paLeft.Size = new System.Drawing.Size(185, 447);
            this.paLeft.TabIndex = 1;
            // 
            // gboxResult
            // 
            this.gboxResult.Controls.Add(this.lbInfo);
            this.gboxResult.Location = new System.Drawing.Point(8, 104);
            this.gboxResult.Name = "gboxResult";
            this.gboxResult.Size = new System.Drawing.Size(169, 49);
            this.gboxResult.TabIndex = 2;
            this.gboxResult.TabStop = false;
            this.gboxResult.Text = " Shapes after split : ";
            // 
            // lbInfo
            // 
            this.lbInfo.Location = new System.Drawing.Point(8, 24);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(40, 13);
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Text = "...";
            // 
            // btnSplit
            // 
            this.btnSplit.Enabled = false;
            this.btnSplit.Location = new System.Drawing.Point(8, 64);
            this.btnSplit.Name = "btnSplit";
            this.btnSplit.Size = new System.Drawing.Size(129, 25);
            this.btnSplit.TabIndex = 1;
            this.btnSplit.Text = "Split shape";
            this.btnSplit.Click += new System.EventHandler(this.btnSplit_Click);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(8, 24);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(129, 25);
            this.btnLine.TabIndex = 0;
            this.btnLine.Text = "New line / Create line";
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(185, 0);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Edit;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(407, 447);
            this.GIS.TabIndex = 2;
            this.GIS.UseRTree = false;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            this.GIS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseUp);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.paLeft);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Split by Arc";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.paLeft.ResumeLayout(false);
            this.gboxResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WinForm());
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            GIS.Lock();
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\Topology\topology3.shp");

            layerPolygon = ((TGIS_LayerVector)GIS.Items[0]);
            shpPolygon = (TGIS_ShapePolygon)(layerPolygon.GetShape(1).MakeEditable());
            if (shpPolygon == null) return;

            layerArc = new TGIS_LayerVector();     //create layer for line
            layerArc.Params.Line.Color = TGIS_Color.Red;
            layerArc.Params.Line.Width = 25;
            if (layerArc == null) return;
            GIS.Add(layerArc);

            shpArc = (TGIS_ShapeArc)
                             ((TGIS_LayerVector)GIS.Items[1]).CreateShape(TGIS_ShapeType.Arc);
            if (shpArc == null) return;
            shpArc.AddPart();

            layerObj = new TGIS_LayerVector();      //create layer for new shapes - after split
            layerObj.Name = "Splits";
            GIS.Add(layerObj);

            GIS.Unlock();
            GIS.FullExtent();
            GIS.RestrictedExtent = GIS.Extent;
        }

        private void btnLine_Click(object sender, System.EventArgs e)
        {
            layerObj.RevertAll();        //clear layer with new polygons
            shpArc.Reset();                 //clear line
            shpArc.AddPart();               //initiate for new points
            lbInfo.Text = "...";
            GIS.InvalidateWholeMap();
            btnSplit.Enabled = false;
        }

        private void btnSplit_Click(object sender, System.EventArgs e)
        {
            int n;
            IList shape_list;
            TGIS_Topology topology_obj;
            Random rnd;

            layerObj.RevertAll();
            topology_obj = new TGIS_Topology();
            shape_list = topology_obj.SplitByArc(shpPolygon, shpArc, true);
            if (shape_list != null)
            {
                lbInfo.Text = shape_list.Count.ToString();
                rnd = new Random();
                for (n = 0; n < shape_list.Count; n++)
                {
                    ((TGIS_Shape)shape_list[n]).Params.Area.Color =
                                                                TGIS_Color.FromRGB((byte)rnd.Next(256),
                                                                                    (byte)rnd.Next(256),
                                                                                    (byte)rnd.Next(256)
                                                                                  );
                    layerObj.AddShape((TGIS_Shape)shape_list[n]);
                }
                shape_list = null;
            }
            GIS.InvalidateWholeMap();
        }

        private void GIS_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;

            //add point to line
            if (GIS.IsEmpty) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            shpArc.Lock(TGIS_Lock.Extent);
            shpArc.AddPoint(ptg);
            shpArc.Unlock();
            GIS.InvalidateWholeMap();
            if (shpArc.Intersect(shpPolygon)) btnSplit.Enabled = true;
        }

        private void GIS_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;

            //add point to line
            if (GIS.IsEmpty) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            shpArc.Lock(TGIS_Lock.Extent);
            shpArc.AddPoint(ptg);
            shpArc.Unlock();
            GIS.InvalidateWholeMap();
            if (shpArc.Intersect(shpPolygon)) btnSplit.Enabled = true;
        }
    }
}
