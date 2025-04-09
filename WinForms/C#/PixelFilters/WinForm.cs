using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace PixelFilters
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private TGIS_ViewerWnd GIS;
        private ProgressBar pProgress;
        private TGIS_ControlLegend GIS_Legend;
        private Button btnExecute;
        private Button btnReset;
        private ListBox lbFilters;
        private Label lblFilters;
        private Label lblMask;
        private Label lblMaskSize;
        private TrackBar tbMaskSize;
        private Label lblMaskSizeValue;
        private ComboBox cbMask;
        private Label lblStructuring;
        private ComboBox cbStructuring;
        private Boolean bFirst;

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
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.pProgress = new System.Windows.Forms.ProgressBar();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lbFilters = new System.Windows.Forms.ListBox();
            this.lblFilters = new System.Windows.Forms.Label();
            this.lblMask = new System.Windows.Forms.Label();
            this.lblMaskSize = new System.Windows.Forms.Label();
            this.tbMaskSize = new System.Windows.Forms.TrackBar();
            this.lblMaskSizeValue = new System.Windows.Forms.Label();
            this.cbMask = new System.Windows.Forms.ComboBox();
            this.lblStructuring = new System.Windows.Forms.Label();
            this.cbStructuring = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbMaskSize)).BeginInit();
            this.SuspendLayout();
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Location = new System.Drawing.Point(174, 12);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(488, 442);
            this.GIS.TabIndex = 0;
            // 
            // pProgress
            // 
            this.pProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pProgress.Location = new System.Drawing.Point(12, 431);
            this.pProgress.Name = "pProgress";
            this.pProgress.Size = new System.Drawing.Size(156, 23);
            this.pProgress.TabIndex = 1;
            // 
            // GIS_Legend
            // 
            this.GIS_Legend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GIS_Legend.CompactView = false;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(12, 367);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GIS_Legend.ReverseOrder = false;
            this.GIS_Legend.Size = new System.Drawing.Size(156, 58);
            this.GIS_Legend.TabIndex = 2;
            // 
            // btnExecute
            // 
            this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExecute.Location = new System.Drawing.Point(12, 338);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(93, 338);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lbFilters
            // 
            this.lbFilters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbFilters.FormattingEnabled = true;
            this.lbFilters.Items.AddRange(new object[] {
            "Threshold",
            "Salt-And-Pepper Noise",
            "Gaussian Noise",
            "Convolution",
            "Sobel Magnitude",
            "Range",
            "Midpoint",
            "Minimum",
            "Maximum",
            "Arithmetic Mean",
            "Alpha-Trimmed Mean",
            "Contra-Harmonic Mean",
            "Geometric Mean",
            "Harmonic Mean",
            "Wieghted Mean",
            "Yp Mean",
            "Majority",
            "Minority",
            "Median",
            "Wieghted Median",
            "Sum",
            "Standard Deviation",
            "Unique Count",
            "Erosion",
            "Dilatation",
            "Opening",
            "Closing",
            "Top-Hat",
            "Bottom-Hat"});
            this.lbFilters.Location = new System.Drawing.Point(12, 28);
            this.lbFilters.Name = "lbFilters";
            this.lbFilters.Size = new System.Drawing.Size(156, 212);
            this.lbFilters.TabIndex = 5;
            this.lbFilters.SelectedIndexChanged += new System.EventHandler(this.lbFilters_SelectedIndexChanged);
            // 
            // lblFilters
            // 
            this.lblFilters.AutoSize = true;
            this.lblFilters.Location = new System.Drawing.Point(10, 12);
            this.lblFilters.Name = "lblFilters";
            this.lblFilters.Size = new System.Drawing.Size(34, 13);
            this.lblFilters.TabIndex = 6;
            this.lblFilters.Text = "Filters";
            // 
            // lblMask
            // 
            this.lblMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMask.AutoSize = true;
            this.lblMask.Location = new System.Drawing.Point(12, 247);
            this.lblMask.Name = "lblMask";
            this.lblMask.Size = new System.Drawing.Size(33, 13);
            this.lblMask.TabIndex = 7;
            this.lblMask.Text = "Mask";
            // 
            // lblMaskSize
            // 
            this.lblMaskSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaskSize.AutoSize = true;
            this.lblMaskSize.Location = new System.Drawing.Point(10, 247);
            this.lblMaskSize.Name = "lblMaskSize";
            this.lblMaskSize.Size = new System.Drawing.Size(56, 13);
            this.lblMaskSize.TabIndex = 8;
            this.lblMaskSize.Text = "Mask Size";
            // 
            // tbMaskSize
            // 
            this.tbMaskSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMaskSize.Location = new System.Drawing.Point(10, 263);
            this.tbMaskSize.Maximum = 12;
            this.tbMaskSize.Name = "tbMaskSize";
            this.tbMaskSize.Size = new System.Drawing.Size(121, 45);
            this.tbMaskSize.TabIndex = 9;
            this.tbMaskSize.Value = 1;
            this.tbMaskSize.ValueChanged += new System.EventHandler(this.tbMaskSize_ValueChanged);
            // 
            // lblMaskSizeValue
            // 
            this.lblMaskSizeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMaskSizeValue.AutoSize = true;
            this.lblMaskSizeValue.Location = new System.Drawing.Point(129, 267);
            this.lblMaskSizeValue.Name = "lblMaskSizeValue";
            this.lblMaskSizeValue.Size = new System.Drawing.Size(24, 13);
            this.lblMaskSizeValue.TabIndex = 10;
            this.lblMaskSizeValue.Text = "3x3";
            // 
            // cbMask
            // 
            this.cbMask.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMask.FormattingEnabled = true;
            this.cbMask.Items.AddRange(new object[] {
            "Low-Pass 3x3",
            "Low-Pass 5x5",
            "Low-Pass 7x7",
            "High-Pass 3x3",
            "High-Pass 5x5",
            "High-Pass 7x7",
            "Gaussian 3x3",
            "Gaussian 5x5",
            "Gaussian 7x7",
            "Laplacian 3x3",
            "Laplacian 5x5",
            "GradientNorth",
            "GradientEast",
            "GradientSouth",
            "GradientWest",
            "GradientNorthwest",
            "GradientNortheast",
            "GradientSouthwest",
            "GradientSoutheast",
            "PointDetector",
            "LineDetectorHorizontal",
            "LineDetectorVertical",
            "LineDetectorLeftDiagonal",
            "LineDetectorRightDiagonal"});
            this.cbMask.Location = new System.Drawing.Point(11, 263);
            this.cbMask.Name = "cbMask";
            this.cbMask.Size = new System.Drawing.Size(157, 21);
            this.cbMask.TabIndex = 11;
            // 
            // lblStructuring
            // 
            this.lblStructuring.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStructuring.AutoSize = true;
            this.lblStructuring.Location = new System.Drawing.Point(10, 295);
            this.lblStructuring.Name = "lblStructuring";
            this.lblStructuring.Size = new System.Drawing.Size(104, 13);
            this.lblStructuring.TabIndex = 12;
            this.lblStructuring.Text = "Structuring Elements";
            // 
            // cbStructuring
            // 
            this.cbStructuring.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbStructuring.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStructuring.FormattingEnabled = true;
            this.cbStructuring.Items.AddRange(new object[] {
            "Square",
            "Diamond",
            "Disk",
            "Horizontal Line",
            "Vertical Line",
            "Left Diagonal Line",
            "Right Diagonal Line"});
            this.cbStructuring.Location = new System.Drawing.Point(12, 311);
            this.cbStructuring.Name = "cbStructuring";
            this.cbStructuring.Size = new System.Drawing.Size(156, 21);
            this.cbStructuring.TabIndex = 13;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(674, 466);
            this.Controls.Add(this.cbStructuring);
            this.Controls.Add(this.lblStructuring);
            this.Controls.Add(this.cbMask);
            this.Controls.Add(this.lblMaskSizeValue);
            this.Controls.Add(this.tbMaskSize);
            this.Controls.Add(this.lblMaskSize);
            this.Controls.Add(this.lblMask);
            this.Controls.Add(this.lblFilters);
            this.Controls.Add(this.lbFilters);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.GIS_Legend);
            this.Controls.Add(this.pProgress);
            this.Controls.Add(this.GIS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.MinimumSize = new System.Drawing.Size(690, 505);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - PixelFilters";
            this.Load += new System.EventHandler(this.WinForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbMaskSize)).EndInit();
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

        private void open()
        {
            TGIS_LayerPixel ll;

            GIS.Close(); ;
            ll = (TGIS_LayerPixel)(
            TGIS_Utils.GisCreateLayer("", TGIS_Utils.GisSamplesDataDirDownload() +
              "World\\Countries\\USA\\States\\California\\San Bernardino\\NED\\w001001.adf")
            );
            ll.Open();
            ll.Params.Pixel.AltitudeMapZones.Clear();
            ll.Params.Pixel.GridShadow = false;

            GIS.Add(ll);
            GIS.FullExtent();

            bFirst = true;
        }

        private void onChange()
        {
            if ((lbFilters.SelectedIndex == 0) ||
                 (lbFilters.SelectedIndex == 1) ||
                 (lbFilters.SelectedIndex == 2)
                )
            {
                cbStructuring.Visible = false;
                lblStructuring.Visible = false;
                lblMask.Visible = false;
                cbMask.Visible = false;
                lblMaskSize.Visible = false;
                lblMaskSizeValue.Visible = false;
                tbMaskSize.Visible = false;
            }
            else
            {
                if ((lbFilters.SelectedIndex == 23) ||
                     (lbFilters.SelectedIndex == 24) ||
                     (lbFilters.SelectedIndex == 25) ||
                     (lbFilters.SelectedIndex == 26) ||
                     (lbFilters.SelectedIndex == 27) ||
                     (lbFilters.SelectedIndex == 28)
                    )
                {
                    cbStructuring.Visible = true;
                    lblStructuring.Visible = true;
                    lblMask.Visible = true;
                    cbMask.Visible = true;
                    lblMaskSize.Visible = true;
                    lblMaskSizeValue.Visible = true;
                    tbMaskSize.Visible = true;
                }
                else
                {
                    cbStructuring.Visible = false;
                    lblStructuring.Visible = false;
                    lblMask.Visible = false;
                    cbMask.Visible = false;
                    lblMaskSize.Visible = false;
                    lblMaskSizeValue.Visible = false;
                    tbMaskSize.Visible = false;
                }

                if (lbFilters.SelectedIndex == 3)
                {
                    cbMask.Visible = true;
                    lblMask.Visible = true;
                    lblMaskSize.Visible = false;
                    lblMaskSizeValue.Visible = false;
                    tbMaskSize.Visible = false;
                }
                else
                {
                    cbMask.Visible = false;
                    lblMask.Visible = false;
                    lblMaskSize.Visible = true;
                    lblMaskSizeValue.Visible = true;
                    tbMaskSize.Visible = true;
                }
            }
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            lbFilters.SelectedIndex = 0 ;
            cbStructuring.SelectedIndex = 0;
            cbMask.SelectedIndex = 0;

            onChange();
            open();
        }

        private void lbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            onChange();
        }

        private void tbMaskSize_ValueChanged(object sender, EventArgs e)
        {
            int i;
            i = 2 * tbMaskSize.Value + 1;
            lblMaskSizeValue.Text = i + "x" + i;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            open();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            TGIS_PixelFilterAbstract flrt = null;
            TGIS_LayerPixel input;
            TGIS_LayerPixel output;
            TGIS_PixelFilterMaskType mask = TGIS_PixelFilterMaskType.Custom;
            TGIS_PixelFilterStructuringElementType struc = TGIS_PixelFilterStructuringElementType.Custom;
            int block;

            input = (TGIS_LayerPixel)(GIS.Items[0]);
        
            if( bFirst ){
                output = new TGIS_LayerPixel();
                output.Name = "Result";
                output.Build( true, input.CS, input.Extent, input.BitWidth, input.BitHeight ) ;
                output.Open();
            }else{
                output = input ;
            }

            block = 2 * tbMaskSize.Value + 1;
        
            switch( lbFilters.SelectedIndex ){
                case 0:{
                    flrt = new TGIS_PixelFilterThreshold();
                    ((TGIS_PixelFilterThreshold)flrt).Threshold = (float)((input.MinHeight + input.MaxHeight)*0.3);
                    break;
                }
                case 1:{
                    flrt = new TGIS_PixelFilterNoiseSaltPepper();
                    ((TGIS_PixelFilterNoiseSaltPepper)flrt).Amount = 10;
                    break;
                }
                case 2:{
                    flrt = new TGIS_PixelFilterNoiseGaussian();
                    ((TGIS_PixelFilterNoiseGaussian)flrt).Amount = 10;
                    break; 
                }
                case 3:{
                    flrt = new TGIS_PixelFilterConvolution();
                    switch( cbMask.SelectedIndex ){
                        case 0  :
                          mask = TGIS_PixelFilterMaskType.LowPass3x3 ;
                          break;
                        case 1  :
                          mask = TGIS_PixelFilterMaskType.LowPass5x5 ;
                          break;
                        case 2  :
                          mask = TGIS_PixelFilterMaskType.LowPass7x7 ;
                          break;
                        case 3  :
                          mask = TGIS_PixelFilterMaskType.HighPass3x3 ;
                          break;
                        case 4  :
                          mask = TGIS_PixelFilterMaskType.HighPass5x5 ;
                          break;
                        case 5  :
                          mask = TGIS_PixelFilterMaskType.HighPass7x7 ;
                          break;
                        case 6  :
                          mask = TGIS_PixelFilterMaskType.Gaussian3x3 ;
                          break;
                        case 7  :
                          mask = TGIS_PixelFilterMaskType.Gaussian5x5 ;
                          break;
                        case 8  :
                          mask = TGIS_PixelFilterMaskType.Gaussian7x7 ;
                          break;
                        case 9  :
                          mask = TGIS_PixelFilterMaskType.Laplacian3x3 ;
                          break;
                        case 10 :
                          mask = TGIS_PixelFilterMaskType.Laplacian5x5 ;
                          break;
                        case 11 :
                          mask = TGIS_PixelFilterMaskType.GradientNorth ;
                          break;
                        case 12 :
                          mask = TGIS_PixelFilterMaskType.GradientEast ;
                          break;
                        case 13 :
                          mask = TGIS_PixelFilterMaskType.GradientSouth ;
                          break;
                        case 14 :
                          mask = TGIS_PixelFilterMaskType.GradientWest ;
                          break;
                        case 15 :
                          mask = TGIS_PixelFilterMaskType.GradientNorthwest ;
                          break;
                        case 16 :
                          mask = TGIS_PixelFilterMaskType.GradientNortheast ;
                          break;
                        case 17 :
                          mask = TGIS_PixelFilterMaskType.GradientSouthwest ;
                          break;
                        case 18 :
                          mask = TGIS_PixelFilterMaskType.GradientSoutheast ;
                          break;
                        case 19 :
                          mask = TGIS_PixelFilterMaskType.PointDetector ;
                          break;
                        case 20 :
                          mask = TGIS_PixelFilterMaskType.LineDetectorHorizontal ;
                          break;
                        case 21 :
                          mask = TGIS_PixelFilterMaskType.LineDetectorVertical ;
                          break;
                        case 22 :
                          mask = TGIS_PixelFilterMaskType.LineDetectorLeftDiagonal ;
                          break;
                        case 23 :
                          mask = TGIS_PixelFilterMaskType.LineDetectorHorizontal ;
                          break;
                    }
                    ((TGIS_PixelFilterConvolution)flrt).MaskType = mask;
                    break;
                }
                case 4:{
                    flrt = new TGIS_PixelFilterSobelMagnitude();
                    ((TGIS_PixelFilterSobelMagnitude)flrt).BlockSize = block;
                    break;
                }
                case 5:{
                    flrt = new TGIS_PixelFilterRange();
                    ((TGIS_PixelFilterRange)flrt).BlockSize = block;
                    break;
                }
                case 6:{
                    flrt = new TGIS_PixelFilterMidpoint();
                    ((TGIS_PixelFilterMidpoint)flrt).BlockSize = block;
                    break;
                }
                case 7:{
                    flrt = new TGIS_PixelFilterMinimum();
                    ((TGIS_PixelFilterMinimum)flrt).BlockSize = block;
                    break;
                }
                case 8:{
                    flrt = new TGIS_PixelFilterMaximum();
                    ((TGIS_PixelFilterMaximum)flrt).BlockSize = block;
                    break;
                }
                case 9:{
                    flrt = new TGIS_PixelFilterArithmeticMean();
                    ((TGIS_PixelFilterArithmeticMean)flrt).BlockSize = block;
                    break;
                }
                case 10:{
                    flrt = new TGIS_PixelFilterAlphaTrimmedMean();
                    ((TGIS_PixelFilterAlphaTrimmedMean)flrt).BlockSize = block;
                    break;
                }
                case 11:{
                    flrt = new TGIS_PixelFilterContraHarmonicMean();
                    ((TGIS_PixelFilterContraHarmonicMean)flrt).BlockSize = block;
                    break;
                }
                case 12:{
                    flrt = new TGIS_PixelFilterGeometricMean();
                    ((TGIS_PixelFilterGeometricMean)flrt).BlockSize = block;
                    break;
                }
                case 13:{
                    flrt = new TGIS_PixelFilterHarmonicMean();
                    ((TGIS_PixelFilterHarmonicMean)flrt).BlockSize = block;
                    break;
                }
                case 14:{
                    flrt = new TGIS_PixelFilterWeightedMean();
                    ((TGIS_PixelFilterWeightedMean)flrt).BlockSize = block;
                    break;
                }
                case 15:{
                    flrt = new TGIS_PixelFilterYpMean();
                    ((TGIS_PixelFilterYpMean)flrt).BlockSize = block;
                    break;
                }
                case 16:{
                    flrt = new TGIS_PixelFilterMajority();
                    ((TGIS_PixelFilterMajority)flrt).BlockSize = block;
                    break;
                }
                case 17:{
                    flrt = new TGIS_PixelFilterMinority();
                    ((TGIS_PixelFilterMinority)flrt).BlockSize = block;
                    break;
                }
                case 18:{
                    flrt = new TGIS_PixelFilterMedian();
                    ((TGIS_PixelFilterMedian)flrt).BlockSize = block;
                    break;
                }
                case 19:{
                    flrt = new TGIS_PixelFilterWeightedMedian();
                    ((TGIS_PixelFilterWeightedMedian)flrt).BlockSize = block;
                    break;
                }
                case 20:{
                    flrt = new TGIS_PixelFilterSum();
                    ((TGIS_PixelFilterSum)flrt).BlockSize = block;
                    break;
                }
                case 21:{
                    flrt = new TGIS_PixelFilterStandardDeviation();
                    ((TGIS_PixelFilterStandardDeviation)flrt).BlockSize = block;
                    break;
                }
                case 22:{
                    flrt = new TGIS_PixelFilterUniqueCount();
                    ((TGIS_PixelFilterUniqueCount)flrt).BlockSize = block;
                    break;
                }
                case 23:{
                    flrt = new TGIS_PixelFilterErosion();
                    switch( cbStructuring.SelectedIndex ){
                        case 0 :
                          struc = TGIS_PixelFilterStructuringElementType.Square             ;
                          break;
                        case 1 :
                          struc = TGIS_PixelFilterStructuringElementType.Diamond            ;
                          break;
                        case 2 :
                          struc = TGIS_PixelFilterStructuringElementType.Disk               ;
                          break;
                        case 3 :
                          struc = TGIS_PixelFilterStructuringElementType.LineHorizontal     ;
                          break;
                        case 4 :
                          struc = TGIS_PixelFilterStructuringElementType.LineVertical       ;
                          break;
                        case 5 :
                          struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal   ;
                          break;
                        case 6 :
                          struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal  ;
                          break;
                    }
                    ((TGIS_PixelFilterErosion)flrt ).StructuringElementType = struc;
                    ((TGIS_PixelFilterErosion)flrt ).BlockSize = block;
                    break;
                }
                case 24:{
                    flrt = new TGIS_PixelFilterDilation();
                    switch( cbStructuring.SelectedIndex ){
                        case 0 :
                          struc = TGIS_PixelFilterStructuringElementType.Square             ;
                          break;
                        case 1 :
                          struc = TGIS_PixelFilterStructuringElementType.Diamond            ;
                          break;
                        case 2 :
                          struc = TGIS_PixelFilterStructuringElementType.Disk               ;
                          break;
                        case 3 :
                          struc = TGIS_PixelFilterStructuringElementType.LineHorizontal     ;
                          break;
                        case 4 :
                          struc = TGIS_PixelFilterStructuringElementType.LineVertical       ;
                          break;
                        case 5 :
                          struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal   ;
                          break;
                        case 6 :
                          struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal  ;
                          break;
                    }
                    ((TGIS_PixelFilterDilation)flrt ).StructuringElementType = struc;
                    ((TGIS_PixelFilterDilation)flrt ).BlockSize = block;
                    break;
                }
                case 25:{
                    flrt = new TGIS_PixelFilterOpening();
                    switch( cbStructuring.SelectedIndex ){
                        case 0 :
                          struc = TGIS_PixelFilterStructuringElementType.Square             ;
                          break;
                        case 1 :
                          struc = TGIS_PixelFilterStructuringElementType.Diamond            ;
                          break;
                        case 2 :
                          struc = TGIS_PixelFilterStructuringElementType.Disk               ;
                          break;
                        case 3 :
                          struc = TGIS_PixelFilterStructuringElementType.LineHorizontal     ;
                          break;
                        case 4 :
                          struc = TGIS_PixelFilterStructuringElementType.LineVertical       ;
                          break;
                        case 5 :
                          struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal   ;
                          break;
                        case 6 :
                          struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal  ;
                          break; 
                    }
                    ((TGIS_PixelFilterOpening)flrt ).StructuringElementType = struc;
                    ((TGIS_PixelFilterOpening)flrt ).BlockSize = block;
                    break;
                }
                case 26:{
                    flrt = new TGIS_PixelFilterClosing();
                    switch( cbStructuring.SelectedIndex ){
                        case 0 :
                          struc = TGIS_PixelFilterStructuringElementType.Square             ;
                          break;
                        case 1 :
                          struc = TGIS_PixelFilterStructuringElementType.Diamond            ;
                          break;
                        case 2 :
                          struc = TGIS_PixelFilterStructuringElementType.Disk               ;
                          break;
                        case 3 :
                          struc = TGIS_PixelFilterStructuringElementType.LineHorizontal     ;
                          break;
                        case 4 :
                          struc = TGIS_PixelFilterStructuringElementType.LineVertical       ;
                          break;
                        case 5 :
                          struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal   ;
                          break;
                        case 6 :
                          struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal  ;
                          break;
                    }
                    ((TGIS_PixelFilterClosing)flrt ).StructuringElementType = struc;
                    ((TGIS_PixelFilterClosing)flrt ).BlockSize = block;
                    break;
                }
                case 27:{
                    flrt = new TGIS_PixelFilterTopHat();
                    switch( cbStructuring.SelectedIndex ){
                        case 0 :
                          struc = TGIS_PixelFilterStructuringElementType.Square             ;
                          break;
                        case 1 :
                          struc = TGIS_PixelFilterStructuringElementType.Diamond            ;
                          break;
                        case 2 :
                          struc = TGIS_PixelFilterStructuringElementType.Disk               ;
                          break;
                        case 3 :
                          struc = TGIS_PixelFilterStructuringElementType.LineHorizontal     ;
                          break;
                        case 4 :
                          struc = TGIS_PixelFilterStructuringElementType.LineVertical       ;
                          break;
                        case 5 :
                          struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal   ;
                          break;
                        case 6 :
                          struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal  ;
                          break; 
                    }
                    ((TGIS_PixelFilterTopHat)flrt ).StructuringElementType = struc;
                    ((TGIS_PixelFilterTopHat)flrt ).BlockSize = block;
                    break;
                }
                case 28:{
                    flrt = new TGIS_PixelFilterBottomHat();
                    switch( cbStructuring.SelectedIndex ){
                        case 0 :
                          struc = TGIS_PixelFilterStructuringElementType.Square             ;
                          break;
                        case 1 :
                          struc = TGIS_PixelFilterStructuringElementType.Diamond            ;
                          break;
                        case 2 :
                          struc = TGIS_PixelFilterStructuringElementType.Disk               ;
                          break;
                        case 3 :
                          struc = TGIS_PixelFilterStructuringElementType.LineHorizontal     ;
                          break;
                        case 4 :
                          struc = TGIS_PixelFilterStructuringElementType.LineVertical       ;
                          break;
                        case 5 :
                          struc = TGIS_PixelFilterStructuringElementType.LineLeftDiagonal   ;
                          break;
                        case 6 :
                          struc = TGIS_PixelFilterStructuringElementType.LineRightDiagonal  ;
                          break;
                    }
                    ((TGIS_PixelFilterBottomHat)flrt ).StructuringElementType = struc;
                    ((TGIS_PixelFilterBottomHat)flrt ).BlockSize = block;
                    break;
                }
            }
        
            flrt.SourceLayer = input;
            flrt.DestinationLayer = output;
            flrt.Band = 1;
            flrt.ColorSpace = TGIS_PixelFilterColorSpace.HSL;
            flrt.BusyEvent += doBusyEvent ;
            flrt.Execute();
        
            output.Params.Pixel.GridShadow = false;
            output.ApplyAntialiasSettings( true );
            if ( bFirst ) {
                GIS.Delete( input.Name );
                GIS.Add( output ) ;
                bFirst = false ;
            }
            GIS.InvalidateWholeMap() ;
        }

        private void doBusyEvent(Object _sender, TGIS_BusyEventArgs _e)
        {
            if (_e.Pos < 0)
                pProgress.Value = pProgress.Maximum;
            else
            if (_e.Pos == 0)
            {
                pProgress.Minimum = 0;
                pProgress.Maximum = (int)_e.EndPos;
                pProgress.Value = 0;
            }
            else
            {
                if (_e.Pos % 100 == 0)
                    pProgress.Value = (int)_e.Pos;
            }
        }
    }
}
