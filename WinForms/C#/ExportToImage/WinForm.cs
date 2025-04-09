using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;
using System.IO;

namespace ExportToImage
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {

        const int DEFAULT_PPI = 300;
        const int DEFAULT_PPI_WEB = 96;
        const int DEFAULT_PPI_DOC = 300;
        const int DEFAULT_WIDTHPIX = 4200;
        const int DEFAULT_WIDTHPIX_WEB = 640;
        const int DEFAULT_WIDTH_DOC_MM = 160;
        const int DEFAULT_WIDTH_DOC_CM = 16;
        const double DEFAULT_WIDTH_DOC_INCH = 6.3;

        const int UNITS_MM = 0;
        const int UNITS_CM = 1;
        const int UNITS_INCH = 2;
        private GroupBox groupBox1;
        private RadioButton rbGrid;
        private RadioButton rbImage;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private GroupBox groupBox2;
        private Button btnOpen;
        private TextBox tbPath;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Button btnExport;
        private RadioButton rbWebQ;
        private RadioButton rbDocQ;
        private RadioButton rbBestQ;
        private Label lbExtent;
        private Label lbFormat;
        private RadioButton rbExtentVisible;
        private RadioButton rbExtentFull;
        private ComboBox cbFormat;
        private TGIS_LayerPixel lstp;
        private TGIS_LayerPixel lpx;
        private TGIS_Extent FExtent;
        private double expWidth,
                        expHeight,
                        PixWidth,
                        PixHeight;
        private int Ppi;
        private SaveFileDialog dlgSaveImage;
        private SaveFileDialog dlgSaveGrid;
        private T_capability[] caps;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGrid = new System.Windows.Forms.RadioButton();
            this.rbImage = new System.Windows.Forms.RadioButton();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbExtent = new System.Windows.Forms.Label();
            this.lbFormat = new System.Windows.Forms.Label();
            this.rbExtentVisible = new System.Windows.Forms.RadioButton();
            this.rbExtentFull = new System.Windows.Forms.RadioButton();
            this.cbFormat = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbWebQ = new System.Windows.Forms.RadioButton();
            this.rbDocQ = new System.Windows.Forms.RadioButton();
            this.rbBestQ = new System.Windows.Forms.RadioButton();
            this.btnExport = new System.Windows.Forms.Button();
            this.dlgSaveImage = new System.Windows.Forms.SaveFileDialog();
            this.dlgSaveGrid = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGrid);
            this.groupBox1.Controls.Add(this.rbImage);
            this.groupBox1.Controls.Add(this.GIS);
            this.groupBox1.Location = new System.Drawing.Point(25, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 231);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Viewer";
            // 
            // rbGrid
            // 
            this.rbGrid.AutoSize = true;
            this.rbGrid.Location = new System.Drawing.Point(438, 59);
            this.rbGrid.Name = "rbGrid";
            this.rbGrid.Size = new System.Drawing.Size(44, 17);
            this.rbGrid.TabIndex = 2;
            this.rbGrid.Text = "Grid";
            this.rbGrid.UseVisualStyleBackColor = true;
            this.rbGrid.CheckedChanged += new System.EventHandler(this.rbGrid_CheckedChanged);
            // 
            // rbImage
            // 
            this.rbImage.AutoSize = true;
            this.rbImage.Checked = true;
            this.rbImage.Location = new System.Drawing.Point(438, 35);
            this.rbImage.Name = "rbImage";
            this.rbImage.Size = new System.Drawing.Size(54, 17);
            this.rbImage.TabIndex = 1;
            this.rbImage.TabStop = true;
            this.rbImage.Text = "Image";
            this.rbImage.UseVisualStyleBackColor = true;
            this.rbImage.CheckedChanged += new System.EventHandler(this.rbImage_CheckedChanged);
            // 
            // GIS
            // 
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(6, 19);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(426, 198);
            this.GIS.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpen);
            this.groupBox2.Controls.Add(this.tbPath);
            this.groupBox2.Location = new System.Drawing.Point(25, 277);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(517, 55);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(458, 18);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(35, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(6, 20);
            this.tbPath.Name = "tbPath";
            this.tbPath.ReadOnly = true;
            this.tbPath.Size = new System.Drawing.Size(426, 20);
            this.tbPath.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbExtent);
            this.groupBox3.Controls.Add(this.lbFormat);
            this.groupBox3.Controls.Add(this.rbExtentVisible);
            this.groupBox3.Controls.Add(this.rbExtentFull);
            this.groupBox3.Controls.Add(this.cbFormat);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(25, 349);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(517, 123);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            // 
            // lbExtent
            // 
            this.lbExtent.AutoSize = true;
            this.lbExtent.Location = new System.Drawing.Point(20, 51);
            this.lbExtent.Name = "lbExtent";
            this.lbExtent.Size = new System.Drawing.Size(37, 13);
            this.lbExtent.TabIndex = 19;
            this.lbExtent.Text = "Extent";
            // 
            // lbFormat
            // 
            this.lbFormat.AutoSize = true;
            this.lbFormat.Location = new System.Drawing.Point(21, 22);
            this.lbFormat.Name = "lbFormat";
            this.lbFormat.Size = new System.Drawing.Size(39, 13);
            this.lbFormat.TabIndex = 18;
            this.lbFormat.Text = "Format";
            // 
            // rbExtentVisible
            // 
            this.rbExtentVisible.AutoSize = true;
            this.rbExtentVisible.Location = new System.Drawing.Point(61, 74);
            this.rbExtentVisible.Name = "rbExtentVisible";
            this.rbExtentVisible.Size = new System.Drawing.Size(55, 17);
            this.rbExtentVisible.TabIndex = 17;
            this.rbExtentVisible.Text = "Visible";
            this.rbExtentVisible.UseVisualStyleBackColor = true;
            // 
            // rbExtentFull
            // 
            this.rbExtentFull.AutoSize = true;
            this.rbExtentFull.Checked = true;
            this.rbExtentFull.Location = new System.Drawing.Point(61, 51);
            this.rbExtentFull.Name = "rbExtentFull";
            this.rbExtentFull.Size = new System.Drawing.Size(41, 17);
            this.rbExtentFull.TabIndex = 16;
            this.rbExtentFull.TabStop = true;
            this.rbExtentFull.Text = "Full";
            this.rbExtentFull.UseVisualStyleBackColor = true;
            // 
            // cbFormat
            // 
            this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormat.FormattingEnabled = true;
            this.cbFormat.Location = new System.Drawing.Point(61, 19);
            this.cbFormat.Name = "cbFormat";
            this.cbFormat.Size = new System.Drawing.Size(121, 21);
            this.cbFormat.TabIndex = 15;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbWebQ);
            this.groupBox4.Controls.Add(this.rbDocQ);
            this.groupBox4.Controls.Add(this.rbBestQ);
            this.groupBox4.Location = new System.Drawing.Point(251, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(260, 109);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Resolution";
            // 
            // rbWebQ
            // 
            this.rbWebQ.AutoSize = true;
            this.rbWebQ.Location = new System.Drawing.Point(7, 83);
            this.rbWebQ.Name = "rbWebQ";
            this.rbWebQ.Size = new System.Drawing.Size(66, 17);
            this.rbWebQ.TabIndex = 2;
            this.rbWebQ.Text = "For Web";
            this.rbWebQ.UseVisualStyleBackColor = true;
            // 
            // rbDocQ
            // 
            this.rbDocQ.AutoSize = true;
            this.rbDocQ.Location = new System.Drawing.Point(7, 60);
            this.rbDocQ.Name = "rbDocQ";
            this.rbDocQ.Size = new System.Drawing.Size(90, 17);
            this.rbDocQ.TabIndex = 1;
            this.rbDocQ.Text = "For document";
            this.rbDocQ.UseVisualStyleBackColor = true;
            // 
            // rbBestQ
            // 
            this.rbBestQ.AutoSize = true;
            this.rbBestQ.Checked = true;
            this.rbBestQ.Location = new System.Drawing.Point(7, 37);
            this.rbBestQ.Name = "rbBestQ";
            this.rbBestQ.Size = new System.Drawing.Size(79, 17);
            this.rbBestQ.TabIndex = 0;
            this.rbBestQ.TabStop = true;
            this.rbBestQ.Text = "Best quality";
            this.rbBestQ.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(237, 478);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dlgSaveImage
            // 
            this.dlgSaveImage.Filter = "JPEG File Interchange Format (*.jpg)|*.jpg|Portable Network Graphic (*.png)|*.png" +
    "|Tag Image File Format (*.tif)|*.tif|Window Bitmap (*.bmp)|*.bmp|TatukGIS PixelS" +
    "tore (*.ttkps)|*.ttkps";
            // 
            // dlgSaveGrid
            // 
            this.dlgSaveGrid.Filter = "Arc/Info Binary Grid (*.flt)|*.flt|Arc/Info ASCII Grid (*.grd)|*.grd|TatukGIS Pix" +
    "elStore (*.ttkps)|*.ttkps";
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(568, 508);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - ExportToImage";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\VisibleEarth\world_8km.jpg");
        }
        
        public class T_capability
        {
            public TGIS_LayerPixelSubFormat C;

            public T_capability(TGIS_LayerPixelSubFormat _c)
            {
                C = _c.CreateCopy();
            }
        }

        private void ValuesInit()
        {
            int i, j;
            TGIS_Layer la;
            double density,
                density0,
                density1;
            int widthpix;
            double ext_delta,
                ext_width;

            density0 = 0;
            density = density0;
            Ppi = DEFAULT_PPI;
            j = 0;
            for (i = GIS.Items.Count - 1; i > 0; i--)
            {
                la = (TGIS_Layer)GIS.Items[i];

                if (la is TGIS_LayerPixel)
                {
                    ext_width = la.Extent.XMax - la.Extent.XMin;

                    density1 = ((TGIS_LayerPixel)la).BitWidth / ext_width;
                    if (density1 > density0)
                    {
                        density = density1;
                        j = i;
                    }
                    density0 = density1;
                }
            }

            if (density == 0)
                widthpix = 4200;
            else
            {
                la = (TGIS_Layer)GIS.Items[j];
                ext_width = la.Extent.XMax - la.Extent.XMin;
                ext_delta = (FExtent.XMax - FExtent.XMin) / ext_width;

                widthpix = (int)Math.Round(ext_delta * ((TGIS_LayerPixel)GIS.Items[j]).BitWidth);
            }

            PixWidth = widthpix;

            if ((FExtent.XMax - FExtent.XMin) != 0)
                PixHeight = ((FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth);
            else
            {
                PixWidth = 2;
                PixHeight = 2;
            }

        }

        private void rbImage_CheckedChanged(object sender, EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\VisibleEarth\world_8km.jpg");
            tbPath.Clear();
            cbFormat.ResetText();
            cbFormat.Items.Clear();
            groupBox3.Enabled = false;
            btnExport.Enabled = false;
        }

        private void rbGrid_CheckedChanged(object sender, EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\San Bernardino\NED\hdr.adf");
            tbPath.Clear();
            cbFormat.ResetText();
            cbFormat.Items.Clear();
            groupBox3.Enabled = false;
            btnExport.Enabled = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixelSubFormat c;
            if (cbFormat.SelectedIndex >= 0)
                c = caps[cbFormat.SelectedIndex].C;
            else
                c = lpx.DefaultSubFormat;

            if (rbExtentFull.Checked)
            {
                FExtent = GIS.Extent;
            }
            else if (rbExtentVisible.Checked)
            {
                FExtent = GIS.VisibleExtent;
            }

            if (rbBestQ.Checked)
            {
                ValuesInit();
            }
            else if (rbDocQ.Checked)
            {
                Ppi = DEFAULT_PPI_DOC;
                expWidth = DEFAULT_WIDTH_DOC_INCH;
                if ((FExtent.XMax - FExtent.XMin) != 0)
                    expHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * expWidth;
                else
                {
                    expWidth = 2;
                    expHeight = 2;
                }

                ValueWHpix();
            }
            else if (rbWebQ.Checked)
            {
                Ppi = DEFAULT_PPI_WEB;
                PixWidth = DEFAULT_WIDTHPIX_WEB;

                if ((FExtent.XMax - FExtent.XMin) != 0)
                    PixHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth;
                else
                {
                    PixWidth = 2;
                    PixHeight = 2;
                }

                ValuesWH();
            }

            lpx.ImportLayer(lstp, lstp.Extent, lstp.CS, (uint)PixWidth, (uint)PixHeight, c);
            MessageBox.Show("Done!", "File exported");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            TList<TGIS_LayerPixelSubFormat> clst;
            int i;

            if (rbImage.Checked)
            {
                if (dlgSaveImage.ShowDialog() != DialogResult.OK)
                    return;

                tbPath.Text = dlgSaveImage.FileName;
            }
            else if (rbGrid.Checked)
            {
                if (dlgSaveGrid.ShowDialog() != DialogResult.OK)
                    return;

                

                tbPath.Text = dlgSaveGrid.FileName;
            }

            if (cbFormat.Items.Count != 0)
                cbFormat.Items.Clear();

            lstp = (TGIS_LayerPixel)GIS.Items[0];
            if (rbImage.Checked)
                lpx = TGIS_Utils.GisCreateLayer(GetFileName(tbPath.Text), dlgSaveImage.FileName) as TGIS_LayerPixel;
            else
                lpx = TGIS_Utils.GisCreateLayer(GetFileName(tbPath.Text), dlgSaveGrid.FileName) as TGIS_LayerPixel;

            clst = lpx.Capabilities;
            i = 0;
            caps = new T_capability[clst.Count];
            foreach (TGIS_LayerPixelSubFormat c in clst)
            {
                cbFormat.Items.Add(c.ToString());
                caps[i] = new T_capability(c);
                i++;
            }

            cbFormat.SelectedIndex = 0;

            groupBox3.Enabled = true;
            btnExport.Enabled = true;
            
        }

        private String GetFileName(String _path)
        {
            return Path.GetFileNameWithoutExtension(_path);
        }

        private void ValuesWH()
        {
            expWidth = PixWidth / Ppi;

            if ((FExtent.XMax - FExtent.XMin) != 0)
                expHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * expWidth;
            else
            {
                expWidth = 2;
                expHeight = 2;
            }
        }

        private void ValueWHpix()
        {
            PixWidth = expWidth * Ppi;

            if ((FExtent.XMax - FExtent.XMin) != 0)
                PixHeight = (FExtent.YMax - FExtent.YMin) / (FExtent.XMax - FExtent.XMin) * PixWidth;
            else
            {
                PixWidth = 2;
                PixHeight = 2;
            }

        }
    }
}
