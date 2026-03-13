using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace BitmapFill
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
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ComboBox ComboStatistic;
        private System.Windows.Forms.ComboBox ComboLabels;
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Panel panel2;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnFullExtent = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ComboStatistic = new System.Windows.Forms.ComboBox();
            this.ComboLabels = new System.Windows.Forms.ComboBox();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnFullExtent,
            this.btnZoomIn,
            this.btnZoomOut,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.ImageList = this.imageList1;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(740, 30);
            this.toolStrip1.TabIndex = 0;
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.ImageIndex = 0;
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.ToolTipText = "Full Extent";
            this.btnFullExtent.Click += new System.EventHandler(this.toolStrip1_ButtonClick);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageIndex = 1;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.ToolTipText = "ZoomIn";
            this.btnZoomIn.Click += new System.EventHandler(this.toolStrip1_ButtonClick);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.ImageIndex = 2;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.ToolTipText = "ZoomOut";
            this.btnZoomOut.Click += new System.EventHandler(this.toolStrip1_ButtonClick);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            //this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripButtonStyle.Separator;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            //this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripButtonStyle.Separator;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            // 
            // ComboStatistic
            // 
            this.ComboStatistic.AccessibleDescription = "0";
            this.ComboStatistic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboStatistic.Items.AddRange(new object[] {
            "By population",
            "By density"});
            this.ComboStatistic.Location = new System.Drawing.Point(94, 2);
            this.ComboStatistic.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ComboStatistic.Name = "ComboStatistic";
            this.ComboStatistic.Size = new System.Drawing.Size(180, 24);
            this.ComboStatistic.TabIndex = 1;
            this.ComboStatistic.TabStop = false;
            this.ComboStatistic.SelectedIndexChanged += new System.EventHandler(this.ComboStatistic_SelectedIndexChanged);
            // 
            // ComboLabels
            // 
            this.ComboLabels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboLabels.Items.AddRange(new object[] {
            "No Labels",
            "By FIPS",
            "By NAME"});
            this.ComboLabels.Location = new System.Drawing.Point(282, 2);
            this.ComboLabels.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ComboLabels.Name = "ComboLabels";
            this.ComboLabels.Size = new System.Drawing.Size(180, 24);
            this.ComboLabels.TabIndex = 2;
            this.ComboLabels.TabStop = false;
            this.ComboLabels.SelectedIndexChanged += new System.EventHandler(this.ComboLabels_SelectedIndexChanged);
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 558);
            this.stripBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(740, 24);
            this.stripBar1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox5);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(650, 30);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(90, 528);
            this.panel1.TabIndex = 5;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(9, 290);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(71, 61);
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(9, 220);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(71, 61);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(9, 150);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(71, 61);
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.AccessibleDescription = "57";
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(9, 80);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(71, 61);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(9, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 61);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ComboStatistic);
            this.panel2.Controls.Add(this.ComboLabels);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(740, 30);
            this.panel2.TabIndex = 6;
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 30);
            this.GIS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(650, 528);
            this.GIS.TabIndex = 7;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(740, 582);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - BitmapFill";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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

            // add states layer
            ll = new TGIS_LayerSHP();
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\Counties.SHP";
            ll.UseConfig = false;
            ll.Name = "counties";

            //set custom painting routine
            ll.PaintShapeEvent += new TGIS_ShapeEvent(PaintShape);
            GIS.Add(ll);

            ComboStatistic.SelectedIndex = 1;
            ComboLabels.SelectedIndex = 0;

            GIS.FullExtent();
        }

        private void PaintShape(object _sender, TGIS_ShapeEventArgs _e)
        {
            double population;
            double area;
            double factor;
            Object oldBitmap;
            TGIS_Shape shape = _e.Shape;

            // calculate parameters
            population = Double.Parse(shape.GetField("population").ToString());
            area = Double.Parse(shape.GetField("area").ToString());

            if (area == 0) return;

            oldBitmap = null;
            if ( shape.Params.Area.Bitmap != null )
                if (!shape.Params.Area.Bitmap.IsEmpty)
                    oldBitmap = shape.Params.Area.Bitmap.NativeBitmap;

            shape.Params.Area.Bitmap = new TGIS_Bitmap() ;
            shape.Params.Area.Pattern = TGIS_BrushStyle.Solid;
            shape.Params.Area.Color = TGIS_Color.Red;
            shape.Params.Area.OutlineColor = TGIS_Color.DimGray;
            shape.Params.Area.OutlineWidth = 20;

            if (ComboStatistic.SelectedIndex == 1)
            {
                factor = population / area;

                // assign different bitmaps for factor value
                if (factor < 1) shape.Params.Area.Bitmap.NativeBitmap = (Bitmap)pictureBox1.Image;
                else if (factor < 7) shape.Params.Area.Bitmap.NativeBitmap = (Bitmap)pictureBox2.Image;
                else if (factor < 52) shape.Params.Area.Bitmap.NativeBitmap = (Bitmap)pictureBox3.Image;
                else if (factor < 380) shape.Params.Area.Bitmap.NativeBitmap = (Bitmap)pictureBox4.Image;
                else if (factor < 3000) shape.Params.Area.Bitmap.NativeBitmap = (Bitmap)pictureBox5.Image;
            }
            else
            {
                factor = population;
                // assign different bitmaps for factor value
                if (factor < 5000) shape.Params.Area.Bitmap.NativeBitmap = (Bitmap)pictureBox1.Image;
                else if (factor < 22000) shape.Params.Area.Bitmap.NativeBitmap = (Bitmap)pictureBox2.Image;
                else if (factor < 104000) shape.Params.Area.Bitmap.NativeBitmap = (Bitmap)pictureBox3.Image;
                else if (factor < 478000) shape.Params.Area.Bitmap.NativeBitmap = (Bitmap)pictureBox4.Image;
                else if (factor < 2186000) shape.Params.Area.Bitmap.NativeBitmap = (Bitmap)pictureBox5.Image;
            }

            // draw bitmaps
            shape.Draw();

            shape.Params.Area.Bitmap.NativeBitmap = oldBitmap;
        }

        private void ComboLabels_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;

            // set labels for states
            ll = (TGIS_LayerVector)(GIS.Get("counties"));
            if (ll != null)
            {
                switch (ComboLabels.SelectedIndex)
                {
                    case 1: ll.Params.Labels.Field = "CNTYIDFP"; break;
                    case 2: ll.Params.Labels.Field = "NAME"; break;
                    default: ll.Params.Labels.Field = ""; break;
                }
            }
            GIS.InvalidateWholeMap();
        }

        private void toolStrip1_ButtonClick(object sender, EventArgs e)
        {
            if (sender == btnFullExtent)
            {
                GIS.FullExtent();
            }
            else if (sender == btnZoomIn)
            {
                GIS.Zoom = GIS.Zoom * 2;
            }
            else if (sender == btnZoomOut)
            {
                GIS.Zoom = GIS.Zoom / 2;
            }

        }

        private void ComboStatistic_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GIS.InvalidateWholeMap();
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

