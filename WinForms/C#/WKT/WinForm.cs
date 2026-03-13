using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace WKT
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
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Panel panel1;
        private TGIS_LayerVector layerObj;

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
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GIS
            // 
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.MinZoomSize = -5;
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(592, 351);
            this.GIS.TabIndex = 0;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.Items.AddRange(new object[] {
            "POINT",
            "MULTIPOINT",
            "LINESTRING",
            "MULTILINESTRING",
            "POLYGON",
            "POINT 3D",
            "MULTIPOINT 3D",
            "LINESTRING 3D",
            "MULTILINESTRING 3D",
            "POLYGON 3D",
            "GEOMETRYCOLLECTION"});
            this.cbType.Location = new System.Drawing.Point(0, 4);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(121, 21);
            this.cbType.TabIndex = 3;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Use list to change WKT type";
            this.toolStripLabel1.Width = 575;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(0, 380);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(592, 67);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 29);
            this.panel1.TabIndex = 3;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples  - Well Known Text (WKT)";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
            // create a layer
            layerObj = new TGIS_LayerVector();
            layerObj.Name = "output";
            layerObj.Params.Area.Color = TGIS_Color.Red;

            GIS.Add(layerObj);
            GIS.FullExtent();
            textBox1.Text = "POLYGON( ( 5 5, 25 5, 25 25, 5 25, 5 5), ( 10 10, 10 20, 20 20, 20 10, 10 10))";
            cbType.SelectedIndex = 4;
        }

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            TGIS_Shape shp;

            try
            {
                layerObj.RevertShapes();
                shp = TGIS_Utils.GisCreateShapeFromWKT(textBox1.Text);
                if (shp != null)
                {
                    layerObj.AddShape(shp);
                    shp = null;
                    textBox1.ForeColor = Color.Black;
                }
                else
                    textBox1.ForeColor = Color.Red;
            }
            catch
            {
                textBox1.ForeColor = Color.Red;
            }
            layerObj.RecalcExtent();
            GIS.RecalcExtent();
            GIS.FullExtent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (cbType.SelectedIndex)
            {
                case 0:
                    textBox1.Text = "POINT (30 30)";
                    break;
                case 1:
                    textBox1.Text = "MULTIPOINT (4 4, 5 5, 6 6 ,7 7)";
                    break;
                case 2:
                    textBox1.Text = "LINESTRING (3 3, 10 10)";
                    break;
                case 3:
                    textBox1.Text = "MULTILINESTRING ((5 5, 25 5, 25 25, 5 25, 5 5), (10 10, 10 20, 20 20, 20 10, 10 10))";
                    break;
                case 4:
                    textBox1.Text = "POLYGON ((5 5, 25 5, 25 25, 5 25, 5 5), (10 10, 10 20, 20 20, 20 10, 10 10))";
                    break;
                case 5:
                    textBox1.Text = "POINTZ (30 30 100)";
                    break;
                case 6:
                    textBox1.Text = "MULTIPOINTZ (4 4 100, 5 5 100, 6 6 100, 7 7 100)";
                    break;
                case 7:
                    textBox1.Text = "LINESTRINGZ (3 3 100, 10 10 100)";
                    break;
                case 8:
                    textBox1.Text = "MULTILINESTRINGZ ((5 5 100, 25 5 100, 25 25 100, 5 25 100, 5 5 100), (10 10 100, 10 20 100, 20 20 100, 20 10 100, 10 10 100))";
                    break;
                case 9:
                    textBox1.Text = "POLYGONZ ((5 5 100, 25 5 100, 25 25 100, 5 25 100, 5 5 100), (10 10 100, 10 20 100, 20 20 100, 20 10 100, 10 10 100))";
                    break;
                case 10:
                    textBox1.Text = "GEOMETRYCOLLECTION (POINT (30 30), LINESTRING (3 3, 10 10) )";
                    break;
            }
        }
    }
}
