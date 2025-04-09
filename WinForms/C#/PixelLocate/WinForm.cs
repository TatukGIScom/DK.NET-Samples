using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace PixelLocate
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
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel paTop;
        private System.Windows.Forms.GroupBox gbOriginal;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox gbChannels;
        private System.Windows.Forms.Label lbRGBValueC;
        private System.Windows.Forms.Panel paColorC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar tbBrightness;
        private Button btnGrid;
        private Button btnImage;
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.paTop = new System.Windows.Forms.Panel();
            this.btnGrid = new System.Windows.Forms.Button();
            this.btnImage = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbBrightness = new System.Windows.Forms.TrackBar();
            this.gbChannels = new System.Windows.Forms.GroupBox();
            this.paColorC = new System.Windows.Forms.Panel();
            this.lbRGBValueC = new System.Windows.Forms.Label();
            this.gbOriginal = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.paTop.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBrightness)).BeginInit();
            this.gbChannels.SuspendLayout();
            this.gbOriginal.SuspendLayout();
            this.SuspendLayout();
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
            // paTop
            // 
            this.paTop.Controls.Add(this.btnGrid);
            this.paTop.Controls.Add(this.btnImage);
            this.paTop.Controls.Add(this.groupBox1);
            this.paTop.Controls.Add(this.gbChannels);
            this.paTop.Controls.Add(this.gbOriginal);
            this.paTop.Dock = System.Windows.Forms.DockStyle.Left;
            this.paTop.Location = new System.Drawing.Point(0, 0);
            this.paTop.Name = "paTop";
            this.paTop.Size = new System.Drawing.Size(209, 466);
            this.paTop.TabIndex = 2;
            // 
            // btnGrid
            // 
            this.btnGrid.Location = new System.Drawing.Point(112, 41);
            this.btnGrid.Name = "btnGrid";
            this.btnGrid.Size = new System.Drawing.Size(75, 23);
            this.btnGrid.TabIndex = 4;
            this.btnGrid.Text = "Open grid";
            this.btnGrid.UseVisualStyleBackColor = true;
            this.btnGrid.Click += new System.EventHandler(this.btnGrid_Click);
            // 
            // btnImage
            // 
            this.btnImage.Location = new System.Drawing.Point(21, 41);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(75, 23);
            this.btnImage.TabIndex = 3;
            this.btnImage.Text = "Open image";
            this.btnImage.UseVisualStyleBackColor = true;
            this.btnImage.Click += new System.EventHandler(this.btnImage_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbBrightness);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(10, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 65);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Brightness ";
            // 
            // tbBrightness
            // 
            this.tbBrightness.AutoSize = false;
            this.tbBrightness.Location = new System.Drawing.Point(8, 24);
            this.tbBrightness.Maximum = 100;
            this.tbBrightness.Minimum = -100;
            this.tbBrightness.Name = "tbBrightness";
            this.tbBrightness.Size = new System.Drawing.Size(169, 33);
            this.tbBrightness.TabIndex = 0;
            this.tbBrightness.TickFrequency = 10;
            this.tbBrightness.Scroll += new System.EventHandler(this.tbBrightness_Scroll);
            this.tbBrightness.Enabled = false;
            // 
            // gbChannels
            // 
            this.gbChannels.Controls.Add(this.paColorC);
            this.gbChannels.Controls.Add(this.lbRGBValueC);
            this.gbChannels.Location = new System.Drawing.Point(10, 179);
            this.gbChannels.Name = "gbChannels";
            this.gbChannels.Size = new System.Drawing.Size(193, 74);
            this.gbChannels.TabIndex = 1;
            this.gbChannels.TabStop = false;
            this.gbChannels.Text = " Channels value :";
            // 
            // paColorC
            // 
            this.paColorC.Location = new System.Drawing.Point(8, 22);
            this.paColorC.Name = "paColorC";
            this.paColorC.Size = new System.Drawing.Size(65, 17);
            this.paColorC.TabIndex = 1;
            // 
            // lbRGBValueC
            // 
            this.lbRGBValueC.Location = new System.Drawing.Point(8, 44);
            this.lbRGBValueC.Name = "lbRGBValueC";
            this.lbRGBValueC.Size = new System.Drawing.Size(176, 27);
            this.lbRGBValueC.TabIndex = 0;
            this.lbRGBValueC.Text = "0, 0, 0";
            // 
            // gbOriginal
            // 
            this.gbOriginal.Controls.Add(this.textBox1);
            this.gbOriginal.Location = new System.Drawing.Point(10, 263);
            this.gbOriginal.Name = "gbOriginal";
            this.gbOriginal.Size = new System.Drawing.Size(193, 182);
            this.gbOriginal.TabIndex = 0;
            this.gbOriginal.TabStop = false;
            this.gbOriginal.Text = " Original color value :";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(8, 32);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(177, 138);
            this.textBox1.TabIndex = 0;
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(209, 0);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(383, 466);
            this.GIS.TabIndex = 3;
            this.GIS.UseRTree = false;
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.paTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Pixel locate";
            this.paTop.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbBrightness)).EndInit();
            this.gbChannels.ResumeLayout(false);
            this.gbOriginal.ResumeLayout(false);
            this.gbOriginal.PerformLayout();
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


        private void tbBrightness_Scroll(object sender, System.EventArgs e)
        {
            TGIS_LayerPixel lp;

            lp = (TGIS_LayerPixel)GIS.Items[0];
            if (lp == null) return;

            lp.Params.Pixel.Brightness = tbBrightness.Value;
            GIS.InvalidateWholeMap();
        }

        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_LayerPixel lp;
            TGIS_Color rgbMapped = new TGIS_Color();
            double[] nativesVals = null;
            bool bT = false;
            int i;

            if (GIS.IsEmpty) return;

            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            lp = (TGIS_LayerPixel)GIS.Items[0];

            if (lp == null) return;

            if (lp.Locate(ptg, ref rgbMapped, ref nativesVals, ref bT))
            {
                paColorC.BackColor = Color.FromArgb(rgbMapped.R,
                                                     rgbMapped.G,
                                                     rgbMapped.B
                                                   );
                lbRGBValueC.Text = String.Format("RGB :  {0} , {1} , {2} ",
                                                  rgbMapped.R,
                                                  rgbMapped.G,
                                                  rgbMapped.B
                                                );
                textBox1.Clear();
                for (i = 0; i < nativesVals.Length; i++)
                    textBox1.AppendText(String.Format("CH{0} =  {1:F0}\r\n",
                                                        i, nativesVals[i]
                                                       )
                                       );
            }
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\San Bernardino\DOQ\37134877.jpg");
            tbBrightness.Enabled = true;
            tbBrightness.Value = 0;
        }

        private void btnGrid_Click(object sender, EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf");
            tbBrightness.Enabled = false;
            tbBrightness.Value = 0;
        }
    }
}
