using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace PixelEdit
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
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private Button btnProfile;
        private Button btnMinMax;
        private Button btnAverageColor;
        private Button btnCreateBitmap;
        private Button btnCreateGrid;
        private TGIS_ControlLegend GIS_Legend;
        private TextBox Memo;
        private System.Windows.Forms.Panel panel1;

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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCreateGrid = new System.Windows.Forms.Button();
            this.btnCreateBitmap = new System.Windows.Forms.Button();
            this.btnAverageColor = new System.Windows.Forms.Button();
            this.btnMinMax = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.Memo = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCreateGrid);
            this.panel1.Controls.Add(this.btnCreateBitmap);
            this.panel1.Controls.Add(this.btnAverageColor);
            this.panel1.Controls.Add(this.btnMinMax);
            this.panel1.Controls.Add(this.btnProfile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 24);
            this.panel1.TabIndex = 0;
            // 
            // btnCreateGrid
            // 
            this.btnCreateGrid.Location = new System.Drawing.Point(480, 0);
            this.btnCreateGrid.Name = "btnCreateGrid";
            this.btnCreateGrid.Size = new System.Drawing.Size(120, 22);
            this.btnCreateGrid.TabIndex = 10;
            this.btnCreateGrid.Text = "Create new GRD";
            this.btnCreateGrid.UseVisualStyleBackColor = true;
            this.btnCreateGrid.Click += new System.EventHandler(this.btnCreateGrid_Click);
            // 
            // btnCreateBitmap
            // 
            this.btnCreateBitmap.Location = new System.Drawing.Point(360, 0);
            this.btnCreateBitmap.Name = "btnCreateBitmap";
            this.btnCreateBitmap.Size = new System.Drawing.Size(120, 22);
            this.btnCreateBitmap.TabIndex = 9;
            this.btnCreateBitmap.Text = "Create new JPG";
            this.btnCreateBitmap.UseVisualStyleBackColor = true;
            this.btnCreateBitmap.Click += new System.EventHandler(this.btnCreateBitmap_Click);
            // 
            // btnAverageColor
            // 
            this.btnAverageColor.Location = new System.Drawing.Point(240, 0);
            this.btnAverageColor.Name = "btnAverageColor";
            this.btnAverageColor.Size = new System.Drawing.Size(120, 22);
            this.btnAverageColor.TabIndex = 8;
            this.btnAverageColor.Text = "Bitmap average color";
            this.btnAverageColor.UseVisualStyleBackColor = true;
            this.btnAverageColor.Click += new System.EventHandler(this.btnAverageColor_Click);
            // 
            // btnMinMax
            // 
            this.btnMinMax.Location = new System.Drawing.Point(120, 0);
            this.btnMinMax.Name = "btnMinMax";
            this.btnMinMax.Size = new System.Drawing.Size(120, 22);
            this.btnMinMax.TabIndex = 7;
            this.btnMinMax.Text = "Grid Min/Max";
            this.btnMinMax.UseVisualStyleBackColor = true;
            this.btnMinMax.Click += new System.EventHandler(this.btnMinMax_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.Location = new System.Drawing.Point(0, 0);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(120, 22);
            this.btnProfile.TabIndex = 6;
            this.btnProfile.Text = "Terrain profile";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // GIS
            // 
            this.GIS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(169, 24);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(466, 352);
            this.GIS.TabIndex = 3;
            // 
            // GIS_Legend
            // 
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_Legend.Dock = System.Windows.Forms.DockStyle.Left;
            this.GIS_Legend.GIS_Group = null;
            this.GIS_Legend.GIS_Layer = null;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(0, 24);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GIS_Legend.ReverseOrder = false;
            this.GIS_Legend.Size = new System.Drawing.Size(169, 352);
            this.GIS_Legend.TabIndex = 2;
            // 
            // Memo
            // 
            this.Memo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Memo.Location = new System.Drawing.Point(0, 376);
            this.Memo.Multiline = true;
            this.Memo.Name = "Memo";
            this.Memo.ReadOnly = true;
            this.Memo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Memo.Size = new System.Drawing.Size(635, 89);
            this.Memo.TabIndex = 1;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(635, 465);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.GIS_Legend);
            this.Controls.Add(this.Memo);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - PixelEdit";
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

        private void btnProfile_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            TGIS_LayerVector lv;
            TGIS_Shape shp;

            Memo.Clear();

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\PixelEdit\grid.ttkproject");

            lp = (TGIS_LayerPixel)GIS.Get("elevation");
            lv = (TGIS_LayerVector)GIS.Get("line");
            shp = lv.GetShape(1).MakeEditable();
            shp.IsSelected = true;

            foreach (TatukGIS.NDK.TGIS_PixelItem px in lp.Loop(0, shp, false))
            {
                Memo.AppendText(String.Format("Distance: {0}, Height:{1}\r\n", px.Distance, px.Value));
            }
        }

        private void btnMinMax_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            TGIS_LayerVector lv;
            TGIS_LayerVector ltmp;
            TGIS_Shape shp;
            TGIS_Shape shptmp;
            Double dmin, dmax;
            TGIS_Point ptmin, ptmax;

            Memo.Clear();

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\PixelEdit\grid.ttkproject");

            lp = (TGIS_LayerPixel)GIS.Get("elevation");
            lv = (TGIS_LayerVector)GIS.Get("polygon");
            shp = lv.GetShape(1).MakeEditable();
            shp.IsSelected = true;

            dmax = -1e38;
            dmin = 1e38;
            ptmin = new TGIS_Point(0, 0);
            ptmax = new TGIS_Point(0, 0);

            foreach (TGIS_PixelItem px in lp.Loop(shp.Extent, 0, shp, "T", false))
            {
                if (px.Value < dmin)
                {
                    dmin = px.Value;
                    ptmin = px.Center;
                }

                if (px.Value > dmax)
                {
                    dmax = px.Value;
                    ptmax = px.Center;
                }
            }

            ltmp = new TGIS_LayerVector();
            ltmp.CS = lp.CS;
            GIS.Add(ltmp);

            ltmp.Params.Marker.Style = TGIS_MarkerStyle.Cross;
            ltmp.Params.Marker.Size = -10;
            ltmp.Params.Marker.Color = TGIS_Color.Black;

            shptmp = ltmp.CreateShape(TGIS_ShapeType.Point);
            shptmp.AddPart();
            shptmp.AddPoint(ptmin);

            shptmp = ltmp.CreateShape(TGIS_ShapeType.Point);
            shptmp.AddPart();
            shptmp.AddPoint(ptmax);

            GIS.InvalidateWholeMap();

            Memo.AppendText(String.Format("Min: {0}, Location: {1} {2}\r\n", dmin, ptmin.X, ptmin.Y));
            Memo.AppendText(String.Format("Max: {0}, Location: {1} {2}\r\n", dmax, ptmax.X, ptmax.Y));
        }

        private void btnAverageColor_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            TGIS_LayerVector lv;
            TGIS_Shape shp;
            double r, g, b;
            byte rr, gg, bb;
            int cnt;
            TGIS_Color cl;

            Memo.Clear();

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\PixelEdit\bitmap.ttkproject");

            lp = (TGIS_LayerPixel)GIS.Get("bluemarble");
            lv = (TGIS_LayerVector)GIS.Get("countries");

            shp = lv.GetShape(679).MakeEditable(); // Spain
            GIS.Lock();
            GIS.VisibleExtent = shp.ProjectedExtent;
            GIS.Zoom = GIS.Zoom / 2.0;
            GIS.Unlock();

            cnt = 0;
            r = 0;
            g = 0;
            b = 0;

            foreach (TGIS_PixelItem px in lp.Loop(shp.Extent, 0, shp, "T", false))
            {
                r = r + px.Color.R;
                g = g + px.Color.G;
                b = b + px.Color.B;
                cnt++;
            }

            if (cnt > 0)
            {
                rr = Convert.ToByte(Math.Truncate(r / cnt));
                gg = Convert.ToByte(Math.Truncate(g / cnt));
                bb = Convert.ToByte(Math.Truncate(b / cnt));
                cl = TGIS_Color.FromRGB(rr, gg, bb);
                foreach (TGIS_PixelItem px in lp.Loop(shp.Extent, 0, shp, "T", true))
                {
                    px.Color = cl;
                }
            }

            GIS.InvalidateWholeMap();
        }

        private void btnCreateBitmap_Click(object sender, EventArgs e)
        {
            TGIS_LayerJPG lp;
            TGIS_LayerPixelLock lck;
            int x, y;

            Memo.Clear();

            lp = new TGIS_LayerJPG();
            try
            {
                lp.Build("test.jpg", TGIS_CSFactory.ByEPSG(4326),
                         TGIS_Utils.GisExtent(-180, -90, 180, 90), 360, 180
                        );

                // direct access to pixels
                lck = lp.LockPixels(TGIS_Utils.GisExtent(-45, -45, 45, 45), lp.CS, true);
                try
                {
                    for (x = lck.Bounds.Left; x <= lck.Bounds.Right; x++)
                    {
                        for (y = lck.Bounds.Top; y <= lck.Bounds.Bottom; y++)
                        {
                            lck.Bitmap[lck.BitmapPos(x, y)] = (int)TGIS_Color.Red.ARGB;
                        }
                    }
                }
                finally
                {
                    lp.UnlockPixels(ref lck);
                }

                lp.SaveData();
            }
            finally
            {
                lp.Dispose();
            }

            GIS.Open("test.jpg");
        }

        private void btnCreateGrid_Click(object sender, EventArgs e)
        {
            TGIS_LayerGRD lp;
            TGIS_LayerPixelLock lck;
            int x, y;
            Random rnd;

            Memo.Clear();

            lp = new TGIS_LayerGRD();
            try
            {
                lp.Build("test.grd", TGIS_CSFactory.ByEPSG(4326),
                         TGIS_Utils.GisExtent(-180, -90, 180, 90), 360, 180
                        );

                // direct access to pixels
                rnd = new Random();
                lck = lp.LockPixels(TGIS_Utils.GisExtent(-45, -45, 45, 45), lp.CS, true);
                try
                {
                    for (x = lck.Bounds.Left; x <= lck.Bounds.Right; x++)
                    {
                        for (y = lck.Bounds.Top; y <= lck.Bounds.Bottom; y++)
                        {
                            lck.Grid[y][x] = rnd.Next(100);
                        }
                    }
                }
                finally
                {
                    lp.UnlockPixels(ref lck);
                }

                lp.SaveData();
            }
            finally
            {
                lp.Dispose();
            };

            GIS.Open("test.grd");

        }
    }
}
