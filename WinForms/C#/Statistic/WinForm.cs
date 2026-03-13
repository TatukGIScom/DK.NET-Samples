using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Statistic
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
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnFullExtent;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ComboBox ComboStatistic;
        private System.Windows.Forms.ComboBox ComboLabels;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;

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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.ComboStatistic = new System.Windows.Forms.ComboBox();
            this.ComboLabels = new System.Windows.Forms.ComboBox();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut,
            this.toolStripSeparator1});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 24);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // ComboStatistic
            // 
            this.ComboStatistic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboStatistic.Items.AddRange(new object[] {
            "By population",
            "By area"});
            this.ComboStatistic.Location = new System.Drawing.Point(77, 2);
            this.ComboStatistic.Name = "ComboStatistic";
            this.ComboStatistic.Size = new System.Drawing.Size(145, 21);
            this.ComboStatistic.TabIndex = 1;
            this.ComboStatistic.TabStop = false;
            this.ComboStatistic.SelectedIndexChanged += new System.EventHandler(this.ComboLabels_SelectedIndexChanged);
            // 
            // ComboLabels
            // 
            this.ComboLabels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboLabels.Items.AddRange(new object[] {
            "No labels",
            "By FIPS",
            "By NAME"});
            this.ComboLabels.Location = new System.Drawing.Point(226, 2);
            this.ComboLabels.Name = "ComboLabels";
            this.ComboLabels.Size = new System.Drawing.Size(145, 21);
            this.ComboLabels.TabIndex = 2;
            this.ComboLabels.TabStop = false;
            this.ComboLabels.SelectedIndexChanged += new System.EventHandler(this.ComboLabels_SelectedIndexChanged);
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 24);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 423);
            this.GIS.TabIndex = 3;
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ComboLabels);
            this.panel1.Controls.Add(this.ComboStatistic);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 24);
            this.panel1.TabIndex = 5;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Statistics";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
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

            // add states layer
            ll = new TGIS_LayerSHP();
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\Counties.SHP";
            ll.Name = "counties";

            // set custom paint procedure
            ll.PaintShapeEvent += new TGIS_ShapeEvent(PaintShape);
            GIS.Add(ll);
            GIS.FullExtent();

            ComboStatistic.SelectedIndex = 0;
            ComboLabels.SelectedIndex = 0;
        }

        private void ComboLabels_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;

            ll = (TGIS_LayerVector)GIS.Get("counties");

            // change labels values
            if (ll != null)
            {
                switch (ComboLabels.SelectedIndex)
                {
                    case 1:
                        ll.Params.Labels.Field = "CNTYIDFP";
                        break;
                    case 2:
                        ll.Params.Labels.Field = "NAME";
                        break;
                    default:
                        ll.Params.Labels.Field = "";
                        break;
                }
            }

            GIS.InvalidateWholeMap();
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            if (sender == btnFullExtent) GIS.FullExtent();
            else if(sender == btnZoomIn) GIS.Zoom = GIS.Zoom * 2;
            else if (sender == btnZoomOut) GIS.Zoom = GIS.Zoom / 2;
        }

        private void PaintShape(object _sender, TGIS_ShapeEventArgs _e)
        {
            double population;
            double area;
            double factor;
            TGIS_Shape shape = _e.Shape;

            // calculate factors
            population = Double.Parse(shape.GetField("population").ToString());
            area = Double.Parse(shape.GetField("area").ToString());

            if (ComboStatistic.SelectedIndex == 1)
            {
                factor = area;

                // assign different bitmaps for factor value
                if (factor < 40) shape.Params.Area.Color = TGIS_Color.FromBGR(0x00F00C);
                else if (factor < 130) shape.Params.Area.Color = TGIS_Color.FromBGR(0xAEFFB3);
                else if (factor < 480) shape.Params.Area.Color = TGIS_Color.FromBGR(0xCCCCFF);
                else if (factor < 2000) shape.Params.Area.Color = TGIS_Color.FromBGR(0x3535FF);
                else if (factor < 10000) shape.Params.Area.Color = TGIS_Color.FromBGR(0x0000B3);
                else shape.Params.Area.Color = TGIS_Color.FromBGR(0x3000B3);
            }
            else
            {
                factor = population;

                // assign different bitmaps for factor value
                if (factor < 5000) shape.Params.Area.Color = TGIS_Color.FromBGR(0x00F00C);
                else if (factor < 22000) shape.Params.Area.Color = TGIS_Color.FromBGR(0xAEFFB3);
                else if (factor < 104000) shape.Params.Area.Color = TGIS_Color.FromBGR(0xCCCCFF);
                else if (factor < 478000) shape.Params.Area.Color = TGIS_Color.FromBGR(0x3535FF);
                else if (factor < 2186000) shape.Params.Area.Color = TGIS_Color.FromBGR(0x0000B3);
                else shape.Params.Area.Color = TGIS_Color.FromBGR(0x3000B3);
            }

            shape.Draw();
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
