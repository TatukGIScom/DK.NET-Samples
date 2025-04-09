using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace JoinAndRender
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolBar2;
        private System.Windows.Forms.ToolStripSeparator toolStripButton2;
        private System.Windows.Forms.Panel panColorStart;
        private System.Windows.Forms.Panel panColorEnd;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStrip toolBar3;
        private System.Windows.Forms.ToolStripSeparator toolStripButton3;
        private System.Windows.Forms.TrackBar scrTransparency;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend tgiS_ControlLegend1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Data.OleDb.OleDbConnection sqlConnection;
        private System.Data.OleDb.OleDbCommand sqlCommand;
        private System.Data.OleDb.OleDbDataAdapter sqlDA;
        private System.Data.DataSet sqlDS;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            //?this.ActiveControl = GIS ;
            this.toolTip1.SetToolTip(this.cmbSize, "Chart Size");
            this.toolTip1.SetToolTip(this.panColorStart, "Click to change start color");
            this.toolTip1.SetToolTip(this.panColorEnd, "Click to change end color");
            this.toolTip1.SetToolTip(this.panColorEnd, "Transparency");
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions2 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.scrTransparency = new System.Windows.Forms.TrackBar();
            this.toolBar3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripSeparator();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panColorEnd = new System.Windows.Forms.Panel();
            this.panColorStart = new System.Windows.Forms.Panel();
            this.toolBar2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tgiS_ControlLegend1 = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrTransparency)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 24);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.scrTransparency);
            this.panel4.Controls.Add(this.toolBar3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(318, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(274, 24);
            this.panel4.TabIndex = 2;
            // 
            // scrTransparency
            // 
            this.scrTransparency.Location = new System.Drawing.Point(6, 0);
            this.scrTransparency.Maximum = 100;
            this.scrTransparency.Name = "scrTransparency";
            this.scrTransparency.Size = new System.Drawing.Size(137, 45);
            this.scrTransparency.TabIndex = 1;
            this.scrTransparency.TickFrequency = 25;
            this.scrTransparency.Value = 100;
            this.scrTransparency.Scroll += new System.EventHandler(this.scrTransparency_Scroll);
            // 
            // toolBar3
            // 
            this.toolBar3.AutoSize = false;
            this.toolBar3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3});
            this.toolBar3.Location = new System.Drawing.Point(0, 0);
            this.toolBar3.Name = "toolBar3";
            this.toolBar3.ShowItemToolTips = true;
            this.toolBar3.Size = new System.Drawing.Size(274, 24);
            this.toolBar3.TabIndex = 0;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Name = "toolStripButton3";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panColorEnd);
            this.panel3.Controls.Add(this.panColorStart);
            this.panel3.Controls.Add(this.toolBar2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(224, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(94, 24);
            this.panel3.TabIndex = 1;
            // 
            // panColorEnd
            // 
            this.panColorEnd.BackColor = System.Drawing.Color.Navy;
            this.panColorEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panColorEnd.Location = new System.Drawing.Point(50, 2);
            this.panColorEnd.Name = "panColorEnd";
            this.panColorEnd.Size = new System.Drawing.Size(42, 22);
            this.panColorEnd.TabIndex = 2;
            this.panColorEnd.Click += new System.EventHandler(this.panColorEnd_Click);
            // 
            // panColorStart
            // 
            this.panColorStart.BackColor = System.Drawing.Color.Aqua;
            this.panColorStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panColorStart.Location = new System.Drawing.Point(6, 2);
            this.panColorStart.Name = "panColorStart";
            this.panColorStart.Size = new System.Drawing.Size(42, 22);
            this.panColorStart.TabIndex = 1;
            this.panColorStart.Click += new System.EventHandler(this.panColorStart_Click);
            // 
            // toolBar2
            // 
            this.toolBar2.AutoSize = false;
            this.toolBar2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
            this.toolBar2.Location = new System.Drawing.Point(0, 0);
            this.toolBar2.Name = "toolBar2";
            this.toolBar2.ShowItemToolTips = true;
            this.toolBar2.Size = new System.Drawing.Size(94, 24);
            this.toolBar2.TabIndex = 0;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbSize);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(224, 24);
            this.panel2.TabIndex = 0;
            // 
            // cmbSize
            // 
            this.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSize.Items.AddRange(new object[] {
            "pop2000",
            "under18",
            "asia",
            "black",
            "white",
            "hisp_lat",
            "male2000",
            "female2000"});
            this.cmbSize.Location = new System.Drawing.Point(77, 2);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(145, 21);
            this.cmbSize.TabIndex = 1;
            this.cmbSize.TabStop = false;
            this.cmbSize.SelectedIndexChanged += new System.EventHandler(this.cmbSize_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut,
            this.toolStripButton1});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(224, 24);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += toolStrip1_ButtonClick;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageIndex = 1;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.ToolTipText = "Zoom In";
            this.btnZoomIn.Click += toolStrip1_ButtonClick;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "Zoom Out";
            this.btnZoomOut.Click += toolStrip1_ButtonClick;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            this.stripBar1.Text = "Use the trackbar to change transparency";
            // 
            // tgiS_ControlLegend1
            // 
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions2.VectorWizardUniqueSearchLimit = 16384;
            this.tgiS_ControlLegend1.DialogOptions = tgiS_ControlLegendDialogOptions2;
            this.tgiS_ControlLegend1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tgiS_ControlLegend1.GIS_Group = null;
            this.tgiS_ControlLegend1.GIS_Layer = null;
            this.tgiS_ControlLegend1.GIS_Viewer = this.GIS;
            this.tgiS_ControlLegend1.Location = new System.Drawing.Point(0, 24);
            this.tgiS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.tgiS_ControlLegend1.Name = "tgiS_ControlLegend1";
            this.tgiS_ControlLegend1.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)));
            this.tgiS_ControlLegend1.ReverseOrder = true;
            this.tgiS_ControlLegend1.Size = new System.Drawing.Size(143, 423);
            this.tgiS_ControlLegend1.TabIndex = 2;
            // 
            // GIS
            // 
            this.GIS.BackColor = System.Drawing.SystemColors.Control;
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(143, 24);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(449, 423);
            this.GIS.TabIndex = 3;
            this.GIS.UseRTree = false;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.tgiS_ControlLegend1);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Join and Render";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Leave += new System.EventHandler(this.WinForm_Leave);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrTransparency)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
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
            TGIS_LayerSHP ll;

            cmbSize.SelectedIndex = 0;

            // create ADO .NET connection object
            sqlConnection = new OleDbConnection();
            sqlConnection.ConnectionString = String.Format(
                "Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}Stats.mdb",
                TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\Statistical\");
            sqlConnection.Open();

            // create a layer, set render and label params
            ll = new TGIS_LayerSHP();
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\tl_2008_06_county.shp";
            ll.Name = "district";

            ll.UseConfig = false;
            ll.Params.Labels.Field = "name";
            ll.Params.Labels.Pattern = TGIS_BrushStyle.Clear;
            ll.Params.Labels.OutlineWidth = 0;
            ll.Params.Labels.FontColor = TGIS_Color.White;
            ll.Params.Labels.Color = TGIS_Color.Black;
            ll.Params.Labels.Position = TGIS_LabelPosition.MiddleCenter |
                                                                            TGIS_LabelPosition.Flow;
            ll.Params.Render.StartSize = 350;
            ll.Params.Render.EndSize = 1000;

            GIS.Add(ll);
            GIS.FullExtent();

            cmbSize_SelectedIndexChanged(this, e);
        }

        private void WinForm_Leave(object sender, System.EventArgs e)
        {
            sqlConnection.Close();
        }

        private void cmbSize_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;
            string vsize;
            string cstring;
            double vmin;
            double vmax;

            if (GIS.Items.Count == 0) return;
            ll = (TGIS_LayerVector)GIS.Items[0];
            if (ll == null) return;

            // get params
            vsize = cmbSize.Items[cmbSize.SelectedIndex].ToString();

            // find min, max values for shapes
            cstring = String.Format("SELECT min({0}) AS mini, max({1}) AS maxi FROM ce2000t",
                                                            vsize, vsize);
            sqlCommand = new OleDbCommand(cstring, sqlConnection);
            sqlDA = new OleDbDataAdapter();
            sqlDA.SelectCommand = sqlCommand;
            sqlDS = new DataSet();
            sqlDA.Fill(sqlDS);

            vmin = Double.Parse(sqlDS.Tables[0].Rows[0]["mini"].ToString());
            vmax = Double.Parse(sqlDS.Tables[0].Rows[0]["maxi"].ToString());

            // let's load data to recordset
            cstring = "select * FROM ce2000t ORDER BY fips";
            sqlCommand = new OleDbCommand(cstring, sqlConnection);
            sqlDA = new OleDbDataAdapter();
            sqlDA.SelectCommand = sqlCommand;
            sqlDS = new DataSet();
            sqlDA.Fill(sqlDS);

            // connect layer with query results
            ll.JoinNET = sqlDS.Tables[0];

            // set primary and foreign keys
            ll.JoinPrimary = "cntyidfp";
            ll.JoinForeign = "fips";

            // render results
            ll.Params.Render.Expression = vsize;
            ll.Params.Render.Zones = 10;
            ll.Params.Render.MinVal = vmin;
            ll.Params.Render.MaxVal = vmax;
            ll.Params.Render.StartColor = TGIS_Color.FromARGB((uint)panColorStart.BackColor.ToArgb());
            ll.Params.Render.EndColor = TGIS_Color.FromARGB((uint)panColorEnd.BackColor.ToArgb());
            ll.Params.Area.Color = TGIS_Color.RenderColor;
            ll.Params.Area.ShowLegend = true;

            GIS.InvalidateWholeMap();
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent) GIS.FullExtent();
            else if (sender == btnZoomIn) GIS.Zoom = GIS.Zoom * 2;
            else if (sender == btnZoomOut) GIS.Zoom = GIS.Zoom / 2;
        }

        private void toolStrip1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);

            if (toolStrip1.Items[0].Bounds.Contains(p) ||
                     toolStrip1.Items[1].Bounds.Contains(p) ||
                     toolStrip1.Items[2].Bounds.Contains(p))
                toolStrip1.Cursor = Cursors.Hand;
            else
                toolStrip1.Cursor = Cursors.Default;
        }

        private void panColorStart_Click(object sender, System.EventArgs e)
        {
            if (colorDialog1.ShowDialog() != DialogResult.OK) return;

            panColorStart.BackColor = colorDialog1.Color;
            cmbSize_SelectedIndexChanged(sender, e);
        }

        private void panColorEnd_Click(object sender, System.EventArgs e)
        {
            if (colorDialog1.ShowDialog() != DialogResult.OK) return;

            panColorEnd.BackColor = colorDialog1.Color;
            cmbSize_SelectedIndexChanged(sender, e);
        }

        private void scrTransparency_Scroll(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;

            ll = (TGIS_LayerVector)GIS.Items[0];
            if (ll == null) return;

            // change transparency
            ll.Transparency = scrTransparency.Value;

            GIS.InvalidateWholeMap();
        }
    }
}
