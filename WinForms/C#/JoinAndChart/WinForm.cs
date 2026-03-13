using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace JoinAndChart
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
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZooIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStrip toolBar2;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ComboBox cmbValues;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolStrip toolBar3;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ComboBox cmbStyle;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Data.OleDb.OleDbConnection sqlConnection;
        private System.Data.OleDb.OleDbCommand sqlCommand;
        private System.Data.OleDb.OleDbDataAdapter sqlDA;
        private System.Data.DataSet sqlDS;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.ActiveControl = GIS;
            this.toolTip1.SetToolTip(this.cmbSize, "Chart Size");
            this.toolTip2.SetToolTip(this.cmbValues, "Chart Values");
            this.toolTip3.SetToolTip(this.cmbStyle, "Chart Style");
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbStyle = new System.Windows.Forms.ComboBox();
            this.toolBar3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmbValues = new System.Windows.Forms.ComboBox();
            this.toolBar2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZooIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
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
            this.panel4.Controls.Add(this.cmbStyle);
            this.panel4.Controls.Add(this.toolBar3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(393, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(199, 24);
            this.panel4.TabIndex = 2;
            // 
            // cmbStyle
            // 
            this.cmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyle.Items.AddRange(new object[] {
            "Pie",
            "Bar"});
            this.cmbStyle.Location = new System.Drawing.Point(8, 2);
            this.cmbStyle.Name = "cmbStyle";
            this.cmbStyle.Size = new System.Drawing.Size(88, 21);
            this.cmbStyle.TabIndex = 1;
            this.cmbStyle.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // toolBar3
            // 
            this.toolBar3.AutoSize = false;
            this.toolBar3.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.toolStripButton3});
            this.toolBar3.Location = new System.Drawing.Point(0, 0);
            this.toolBar3.Name = "toolBar3";
            this.toolBar3.ShowItemToolTips = true;
            this.toolBar3.Size = new System.Drawing.Size(199, 24);
            this.toolBar3.TabIndex = 0;
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Name = "toolStripButton3";
            //this.toolStripButton3.Style = System.Windows.Forms.ToolStripButtonStyle.Separator;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cmbValues);
            this.panel3.Controls.Add(this.toolBar2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(222, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(171, 24);
            this.panel3.TabIndex = 1;
            // 
            // cmbValues
            // 
            this.cmbValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValues.Items.AddRange(new object[] {
            "black:white",
            "pop2000:square_mil",
            "male2000:female2000"});
            this.cmbValues.Location = new System.Drawing.Point(8, 2);
            this.cmbValues.Name = "cmbValues";
            this.cmbValues.Size = new System.Drawing.Size(163, 21);
            this.cmbValues.TabIndex = 1;
            this.cmbValues.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // toolBar2
            // 
            this.toolBar2.AutoSize = false;
            this.toolBar2.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.toolStripButton2});
            this.toolBar2.Location = new System.Drawing.Point(0, 0);
            this.toolBar2.Name = "toolBar2";
            this.toolBar2.ShowItemToolTips = true;
            this.toolBar2.Size = new System.Drawing.Size(171, 24);
            this.toolBar2.TabIndex = 0;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            //this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripButtonStyle.Separator;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmbSize);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(222, 24);
            this.panel2.TabIndex = 0;
            // 
            // cmbSize
            // 
            this.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSize.Items.AddRange(new object[] {
            "pop2000",
            "male2000",
            "female2000",
            "under18",
            "asia",
            "black",
            "white",
            "hisp_lat"});
            this.cmbSize.Location = new System.Drawing.Point(77, 2);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(145, 21);
            this.cmbSize.TabIndex = 1;
            this.cmbSize.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnFullExtent,
            this.btnZooIn,
            this.btnZoomOut,
            this.toolStripButton1});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(222, 24);
            this.toolStrip1.TabIndex = 0;
            //this.toolStrip1.ButtonClick += new System.Windows.Forms.ToolStripButtonClickEventHandler(this.toolStrip1_ButtonClick);
            //this.toolStrip1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.toolStrip1_MouseMove);
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            // 
            // btnZooIn
            // 
            this.btnZooIn.ImageIndex = 1;
            this.btnZooIn.Name = "btnZooIn";
            this.btnZooIn.ToolTipText = "Zoom In";
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "Zoom Out";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            //this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripButtonStyle.Separator;
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
            // 
            // GIS
            // 
            this.GIS.BackColor = System.Drawing.SystemColors.ControlLight;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 24);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(592, 423);
            this.GIS.TabIndex = 0;
            this.GIS.UseRTree = false;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Join and Chart";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Leave += new System.EventHandler(this.WinForm_Leave);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
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
            cmbValues.SelectedIndex = 0;
            cmbStyle.SelectedIndex = 0;

            // create ADO .NET connection object
            sqlConnection = new OleDbConnection();
            sqlConnection.ConnectionString = String.Format(
              "Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}Stats.mdb",
              TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\Statistical\");
            sqlConnection.Open();

            // use layer to display charts
            ll = new TGIS_LayerSHP();
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\tl_2008_06_county.shp";
            ll.Name = "tl_2008_06_county";

            ll.UseConfig = false;
            ll.Params.Labels.Field = "name";
            ll.Params.Labels.Pattern = TGIS_BrushStyle.Clear;
            ll.Params.Labels.OutlineWidth = 0;
            ll.Params.Labels.FontColor = TGIS_Color.White;
            ll.Params.Labels.Color = TGIS_Color.Black;
            ll.Params.Labels.Position = TGIS_LabelPosition.MiddleCenter | TGIS_LabelPosition.Flow;
            
            GIS.Add(ll);
            GIS.FullExtent();

            cmb_SelectedIndexChanged(this, e);
        }

        private void WinForm_Leave(object sender, System.EventArgs e)
        {
            sqlConnection.Close();
        }

        private void cmb_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;
            string vsize;
            string vvalues;
            string vstyle;
            string cstring;
            double vmin;
            double vmax;

            if (GIS.Items.Count == 0) return;
            ll = (TGIS_LayerVector)GIS.Items[0];
            if (ll == null) return;

            // get params
            vsize = cmbSize.Items[cmbSize.SelectedIndex].ToString();
            vvalues = cmbValues.Items[cmbValues.SelectedIndex].ToString();
            vstyle = cmbStyle.Items[cmbStyle.SelectedIndex].ToString();

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

            // Set the chart style: "Pie" or "Bar"
            ll.Params.Chart.Style = TGIS_Utils.ParamChart(vstyle, TGIS_ChartStyle.Pie);
            
            // The chart size will be set by Render in the range of 350 to 1000
            // depending on the value of the "vsize" field
            ll.Params.Chart.Size = TGIS_Utils.GIS_RENDER_SIZE();
            ll.Params.Render.StartSize = 350;
            ll.Params.Render.EndSize = 1000;
            ll.Params.Render.Expression = vsize;

            // The Renderer will create 10 zones to group field values,
            // starting from "vmin" and edning with "vmax"
            ll.Params.Render.Zones = 10;
            ll.Params.Render.MinVal = vmin;
            ll.Params.Render.MaxVal = vmax;

            // For 'Bar' chart you can replace '0:0' by 'min:max' to set custom Y-axis limits.
            // 'vvalues' contains list of values displayed on the chart.
            // In this sample field names are used, e.g. 'male2000:female2000'.
            // Values need to be divided by a colon ':'.
            ll.Params.Render.Chart = "0:0:" + vvalues;

            // If necessary, the chart can also be included in the legend
            ll.Params.Chart.Legend = ll.Params.Render.Chart;
            ll.Params.Chart.ShowLegend = true;
            
            GIS.InvalidateWholeMap();
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent)
            {
                GIS.FullExtent();
            }
            else if (sender == btnZooIn) 
            {
                GIS.Zoom = GIS.Zoom * 2;
            }
            else if (sender == btnZoomOut)
            {
                GIS.Zoom = GIS.Zoom / 2;
            }
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
    }
}
