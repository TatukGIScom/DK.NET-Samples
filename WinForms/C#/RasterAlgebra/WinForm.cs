using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;

namespace RasterAlgebra
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Label lblSrc;
        private Button btnPixel;
        private Button btnGrid;
        private Button btnVector;
        private Label lblResultType;
        private RadioButton rbResultPixel;
        private RadioButton rbResultGrid;
        private Label lblResult;
        private TextBox tbFormula;
        private Button btnExecute;
        private ProgressBar pbProgress;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GISlegend;
        private const String SAMPLE_RESULT = "Result" ;

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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.lblSrc = new System.Windows.Forms.Label();
            this.btnPixel = new System.Windows.Forms.Button();
            this.btnGrid = new System.Windows.Forms.Button();
            this.btnVector = new System.Windows.Forms.Button();
            this.lblResultType = new System.Windows.Forms.Label();
            this.rbResultPixel = new System.Windows.Forms.RadioButton();
            this.rbResultGrid = new System.Windows.Forms.RadioButton();
            this.lblResult = new System.Windows.Forms.Label();
            this.tbFormula = new System.Windows.Forms.TextBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GISlegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.SuspendLayout();
            // 
            // lblSrc
            // 
            this.lblSrc.AutoSize = true;
            this.lblSrc.Location = new System.Drawing.Point(13, 13);
            this.lblSrc.Name = "lblSrc";
            this.lblSrc.Size = new System.Drawing.Size(81, 13);
            this.lblSrc.TabIndex = 0;
            this.lblSrc.Text = "Choose source:";
            // 
            // btnPixel
            // 
            this.btnPixel.Location = new System.Drawing.Point(100, 8);
            this.btnPixel.Name = "btnPixel";
            this.btnPixel.Size = new System.Drawing.Size(75, 23);
            this.btnPixel.TabIndex = 1;
            this.btnPixel.Text = "Open pixel";
            this.btnPixel.UseVisualStyleBackColor = true;
            this.btnPixel.Click += new System.EventHandler(this.btnPixel_Click);
            // 
            // btnGrid
            // 
            this.btnGrid.Location = new System.Drawing.Point(181, 8);
            this.btnGrid.Name = "btnGrid";
            this.btnGrid.Size = new System.Drawing.Size(75, 23);
            this.btnGrid.TabIndex = 2;
            this.btnGrid.Text = "Open grid";
            this.btnGrid.UseVisualStyleBackColor = true;
            this.btnGrid.Click += new System.EventHandler(this.btnGrid_Click);
            // 
            // btnVector
            // 
            this.btnVector.Location = new System.Drawing.Point(262, 8);
            this.btnVector.Name = "btnVector";
            this.btnVector.Size = new System.Drawing.Size(75, 23);
            this.btnVector.TabIndex = 3;
            this.btnVector.Text = "Open vector";
            this.btnVector.UseVisualStyleBackColor = true;
            this.btnVector.Click += new System.EventHandler(this.btnVector_Click);
            // 
            // lblResultType
            // 
            this.lblResultType.AutoSize = true;
            this.lblResultType.Location = new System.Drawing.Point(12, 42);
            this.lblResultType.Name = "lblResultType";
            this.lblResultType.Size = new System.Drawing.Size(63, 13);
            this.lblResultType.TabIndex = 4;
            this.lblResultType.Text = "Result type:";
            // 
            // rbResultPixel
            // 
            this.rbResultPixel.AutoSize = true;
            this.rbResultPixel.Checked = true;
            this.rbResultPixel.Location = new System.Drawing.Point(100, 42);
            this.rbResultPixel.Name = "rbResultPixel";
            this.rbResultPixel.Size = new System.Drawing.Size(47, 17);
            this.rbResultPixel.TabIndex = 5;
            this.rbResultPixel.TabStop = true;
            this.rbResultPixel.Text = "Pixel";
            this.rbResultPixel.UseVisualStyleBackColor = true;
            // 
            // rbResultGrid
            // 
            this.rbResultGrid.AutoSize = true;
            this.rbResultGrid.Location = new System.Drawing.Point(153, 42);
            this.rbResultGrid.Name = "rbResultGrid";
            this.rbResultGrid.Size = new System.Drawing.Size(44, 17);
            this.rbResultGrid.TabIndex = 6;
            this.rbResultGrid.Text = "Grid";
            this.rbResultGrid.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Location = new System.Drawing.Point(13, 67);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(46, 13);
            this.lblResult.TabIndex = 7;
            this.lblResult.Text = "Result =";
            // 
            // tbFormula
            // 
            this.tbFormula.Location = new System.Drawing.Point(100, 65);
            this.tbFormula.Name = "tbFormula";
            this.tbFormula.Size = new System.Drawing.Size(399, 20);
            this.tbFormula.TabIndex = 8;
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(505, 62);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 9;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(16, 92);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(564, 23);
            this.pbProgress.TabIndex = 10;
            // 
            // GIS
            // 
            this.GIS.Location = new System.Drawing.Point(16, 121);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(428, 333);
            this.GIS.TabIndex = 11;
            // 
            // GISlegend
            // 
            this.GISlegend.CompactView = false;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GISlegend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GISlegend.GIS_Viewer = this.GIS;
            this.GISlegend.Location = new System.Drawing.Point(450, 121);
            this.GISlegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GISlegend.Name = "GISlegend";
            this.GISlegend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GISlegend.ReverseOrder = false;
            this.GISlegend.Size = new System.Drawing.Size(130, 333);
            this.GISlegend.TabIndex = 12;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GISlegend);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.tbFormula);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.rbResultGrid);
            this.Controls.Add(this.rbResultPixel);
            this.Controls.Add(this.lblResultType);
            this.Controls.Add(this.btnVector);
            this.Controls.Add(this.btnGrid);
            this.Controls.Add(this.btnPixel);
            this.Controls.Add(this.lblSrc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - RasterAlgebra";
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

        private void applyRamp(TGIS_LayerPixel lp)
        {
            lp.GenerateRamp(
                TGIS_Color.Blue, TGIS_Color.Lime, TGIS_Color.Red,
                1.0 * Math.Floor(lp.MinHeight),
                (lp.MaxHeight + lp.MinHeight) / 2.0,
                1.0 * Math.Ceiling(lp.MaxHeight), true,
                (lp.MaxHeight - lp.MinHeight) / 100.0,
                (lp.MaxHeight - lp.MinHeight) / 10.0,
                null, false
            );

            lp.Params.Pixel.GridShadow = false;
        }

        private void btnPixel_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            String path;

            GIS.Close();

            path = TGIS_Utils.GisSamplesDataDirDownload() +
             @"\World\Countries\USA\States\California\San Bernardino\DOQ\37134877.jpg";

            lp = TGIS_Utils.GisCreateLayer("Pixel", path) as TGIS_LayerPixel;
            GIS.Add(lp);
            GIS.FullExtent();

            rbResultPixel.Checked = true;
            tbFormula.Text = "RGB(255 - pixel.R, 255 - pixel.G, 255 - pixel.B)";
        }

        private void btnGrid_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            String path;

            GIS.Close();

            path = TGIS_Utils.GisSamplesDataDirDownload() +
             @"\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf";

            lp = TGIS_Utils.GisCreateLayer("Grid", path) as TGIS_LayerPixel;
            lp.UseConfig = false;
            GIS.Add(lp);
            applyRamp(lp);
            GIS.FullExtent();

            rbResultGrid.Checked = true;
            tbFormula.Text = "IF(grid < AVG(grid), MIN(grid), MAX(grid))";
        }

        private void btnVector_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;
            String path;

            GIS.Close();

            path = TGIS_Utils.GisSamplesDataDirDownload() +
             @"\World\Countries\USA\States\California\San Bernardino\TIGER\tl_2008_06071_edges_trunc.shp";

            lv = TGIS_Utils.GisCreateLayer("Vector", path) as TGIS_LayerVector;
            lv.UseConfig = false;
            GIS.Add(lv);
            GIS.FullExtent();

            rbResultPixel.Checked = true;
            tbFormula.Text = "IF(NODATA(vector.GIS_UID), RGB(0,255,0), RGB(255,0,0))";
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel src;
            TGIS_LayerPixel dst;
            TGIS_RasterAlgebra ra;
            double gew;
            double lew;
            int w;
            int siz;
            int i;
            int j;

            if (GIS.IsEmpty)
            {
                MessageBox.Show("The viewer is empty!");
                return;
            }

            if(GIS.Get(SAMPLE_RESULT) != null)
            {
                GIS.Delete(SAMPLE_RESULT);
            }

            gew = GIS.Extent.XMax - GIS.Extent.XMin;

            src = null;
            siz = 0;

            for(i = 0; i < GIS.Items.Count; i++)
            {
                if(GIS.Items[i] is TGIS_LayerPixel)
                {
                    src = GIS.Items[i] as TGIS_LayerPixel;
                    lew = src.Extent.XMax - src.Extent.XMin;
                    w = (int)Math.Round(gew*src.BitWidth/lew);
                    siz = Math.Max(w, siz);
                }

                dst = new TGIS_LayerPixel();

                if(src != null)
                {
                    dst.Build(rbResultGrid.Checked, GIS.CS, GIS.Extent, siz, 0);
                }else
                {
                    dst.Build(rbResultGrid.Checked, GIS.CS, GIS.Extent, GIS.Width, 0);
                }

                dst.Name = SAMPLE_RESULT;

                GIS.Add(dst);

                ra = new TGIS_RasterAlgebra();

                ra.BusyEvent += doBusyEvent;

                for (i = 0; i < GIS.Items.Count; i++)
                {
                    ra.AddLayer(GIS.Items[i] as TGIS_Layer);
                }

                try
                {
                    ra.Execute(SAMPLE_RESULT + "=" + tbFormula.Text);
                }
                catch
                {
                    GIS.Delete(SAMPLE_RESULT);
                    MessageBox.Show("Incorrect formula");
                }

                if (dst.IsGrid())
                {
                    applyRamp(dst);
                }

                GIS.InvalidateWholeMap();
            }
        }

        private void doBusyEvent(object _sender, TGIS_BusyEventArgs _e)
        {
            if(_e.Pos < 0)
            {
                pbProgress.Value = pbProgress.Maximum;
            }else if(_e.Pos == 0)
            {
                pbProgress.Minimum = 0;
                pbProgress.Maximum = (int)_e.EndPos;
                pbProgress.Value = 0;
            }else
            {
                if(_e.Pos > 0)
                {
                    pbProgress.Value = (int)_e.Pos;
                }
            }
        }
    }
}
