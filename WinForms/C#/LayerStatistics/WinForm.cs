using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using System.Collections.Generic;

namespace LayerStatistics
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private GroupBox gbSelectLayer;
        private GroupBox gbSelectStatistics;
        private GroupBox gbSelectDefs;
        private Button btnCalculate;
        private Button btnOpen;
        private RadioButton rbCustom;
        private RadioButton rbPixel;
        private RadioButton rbGrid;
        private RadioButton rbVector;
        private Button btnAllStats;
        private Button btnStandardStats;
        private Button btnBasicStats;
        private Button btnDeselectAllDefs;
        private Button btnSelectAllDefs;
        private CheckBox cbFastStats;
        private CheckBox cbBessel;
        private TGIS_ViewerWnd GIS;
        private ProgressBar pProgress;
        private GroupBox gbResults;
        private RichTextBox rtbResult;
        private CheckedListBox cblStats;
        private CheckedListBox cblDefs;
        private String sample_vector;
        private String sample_grid;
        private String sample_pixel;
        private String custom_path;
        private Boolean abrt;
        
        // band names
	    private const String GIS_BAND_DEFAULT = "0";
	    private const String GIS_BAND_GRID = "Value";
	    private const String GIS_BAND_A = "A";
	    private const String GIS_BAND_R = "R";
	    private const String GIS_BAND_G = "G" ;
	    private const String GIS_BAND_B = "B" ;
	    private const String GIS_BAND_H = "H" ;
	    private const String GIS_BAND_S = "S" ;
	    private const String GIS_BAND_L = "L" ;
        const Single GIS_MAX_SINGLE = (Single)3.4e38;

        // buttons captions
        private const String BUTTON_CALCULATE = "Calculate statistics";
        private const String BUTTON_CANCEL = "Cancel";

        // predefined sets of statistical functions
        // find in documentation: GisStatisticsAll, GisStatisticsBasic, GisStatisticsStandard
        private readonly String[] STATISTICS_BASIC = {
            "Average",
            "Count",
            "Max",
            "Min",
            "Sum"
        };
        private readonly String[] STATISTICS_STANDARD = {
            "Average",
            "Count",
            "CountMissings",
            "Max",
            "Median",
            "Min",
            "Range",
            "StandardDeviation",
            "Sum",
            "Variance"
        };
        private OpenFileDialog openDialog;
        private Button btnSaveStats;
        private Button btnLoadStats;


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.gbSelectLayer = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.rbPixel = new System.Windows.Forms.RadioButton();
            this.rbGrid = new System.Windows.Forms.RadioButton();
            this.rbVector = new System.Windows.Forms.RadioButton();
            this.gbSelectStatistics = new System.Windows.Forms.GroupBox();
            this.cblStats = new System.Windows.Forms.CheckedListBox();
            this.btnAllStats = new System.Windows.Forms.Button();
            this.btnStandardStats = new System.Windows.Forms.Button();
            this.btnBasicStats = new System.Windows.Forms.Button();
            this.gbSelectDefs = new System.Windows.Forms.GroupBox();
            this.cblDefs = new System.Windows.Forms.CheckedListBox();
            this.btnDeselectAllDefs = new System.Windows.Forms.Button();
            this.btnSelectAllDefs = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.cbFastStats = new System.Windows.Forms.CheckBox();
            this.cbBessel = new System.Windows.Forms.CheckBox();
            this.pProgress = new System.Windows.Forms.ProgressBar();
            this.gbResults = new System.Windows.Forms.GroupBox();
            this.btnSaveStats = new System.Windows.Forms.Button();
            this.btnLoadStats = new System.Windows.Forms.Button();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.gbSelectLayer.SuspendLayout();
            this.gbSelectStatistics.SuspendLayout();
            this.gbSelectDefs.SuspendLayout();
            this.gbResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSelectLayer
            // 
            this.gbSelectLayer.Controls.Add(this.btnOpen);
            this.gbSelectLayer.Controls.Add(this.rbCustom);
            this.gbSelectLayer.Controls.Add(this.rbPixel);
            this.gbSelectLayer.Controls.Add(this.rbGrid);
            this.gbSelectLayer.Controls.Add(this.rbVector);
            this.gbSelectLayer.Location = new System.Drawing.Point(12, 12);
            this.gbSelectLayer.Name = "gbSelectLayer";
            this.gbSelectLayer.Size = new System.Drawing.Size(190, 116);
            this.gbSelectLayer.TabIndex = 0;
            this.gbSelectLayer.TabStop = false;
            this.gbSelectLayer.Text = "Select layer";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(72, 85);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "Open file...";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Location = new System.Drawing.Point(6, 88);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(60, 17);
            this.rbCustom.TabIndex = 3;
            this.rbCustom.TabStop = true;
            this.rbCustom.Text = "Custom";
            this.rbCustom.UseVisualStyleBackColor = true;
            this.rbCustom.CheckedChanged += new System.EventHandler(this.rbCustom_CheckedChanged);
            // 
            // rbPixel
            // 
            this.rbPixel.AutoSize = true;
            this.rbPixel.Location = new System.Drawing.Point(6, 65);
            this.rbPixel.Name = "rbPixel";
            this.rbPixel.Size = new System.Drawing.Size(47, 17);
            this.rbPixel.TabIndex = 2;
            this.rbPixel.TabStop = true;
            this.rbPixel.Text = "Pixel";
            this.rbPixel.UseVisualStyleBackColor = true;
            this.rbPixel.CheckedChanged += new System.EventHandler(this.rbPixel_CheckedChanged);
            // 
            // rbGrid
            // 
            this.rbGrid.AutoSize = true;
            this.rbGrid.Location = new System.Drawing.Point(6, 42);
            this.rbGrid.Name = "rbGrid";
            this.rbGrid.Size = new System.Drawing.Size(44, 17);
            this.rbGrid.TabIndex = 1;
            this.rbGrid.TabStop = true;
            this.rbGrid.Text = "Grid";
            this.rbGrid.UseVisualStyleBackColor = true;
            this.rbGrid.CheckedChanged += new System.EventHandler(this.rbGrid_CheckedChanged);
            // 
            // rbVector
            // 
            this.rbVector.AutoSize = true;
            this.rbVector.Location = new System.Drawing.Point(6, 19);
            this.rbVector.Name = "rbVector";
            this.rbVector.Size = new System.Drawing.Size(56, 17);
            this.rbVector.TabIndex = 0;
            this.rbVector.TabStop = true;
            this.rbVector.Text = "Vector";
            this.rbVector.UseVisualStyleBackColor = true;
            this.rbVector.CheckedChanged += new System.EventHandler(this.rbVector_CheckedChanged);
            // 
            // gbSelectStatistics
            // 
            this.gbSelectStatistics.Controls.Add(this.cblStats);
            this.gbSelectStatistics.Controls.Add(this.btnAllStats);
            this.gbSelectStatistics.Controls.Add(this.btnStandardStats);
            this.gbSelectStatistics.Controls.Add(this.btnBasicStats);
            this.gbSelectStatistics.Location = new System.Drawing.Point(12, 134);
            this.gbSelectStatistics.Name = "gbSelectStatistics";
            this.gbSelectStatistics.Size = new System.Drawing.Size(190, 230);
            this.gbSelectStatistics.TabIndex = 1;
            this.gbSelectStatistics.TabStop = false;
            this.gbSelectStatistics.Text = "Select statistics";
            // 
            // cblStats
            // 
            this.cblStats.FormattingEnabled = true;
            this.cblStats.Items.AddRange(new object[] {
            "Count",
            "CountMissings",
            "Sum",
            "Average",
            "StandardDeviation",
            "Sample",
            "Variance",
            "Median",
            "Min",
            "Max",
            "Range",
            "Minority",
            "Majority",
            "Variety",
            "Unique"});
            this.cblStats.Location = new System.Drawing.Point(6, 48);
            this.cblStats.Name = "cblStats";
            this.cblStats.Size = new System.Drawing.Size(178, 169);
            this.cblStats.TabIndex = 3;
            // 
            // btnAllStats
            // 
            this.btnAllStats.Location = new System.Drawing.Point(138, 19);
            this.btnAllStats.Name = "btnAllStats";
            this.btnAllStats.Size = new System.Drawing.Size(46, 23);
            this.btnAllStats.TabIndex = 2;
            this.btnAllStats.Text = "All";
            this.btnAllStats.UseVisualStyleBackColor = true;
            this.btnAllStats.Click += new System.EventHandler(this.btnAllStats_Click);
            // 
            // btnStandardStats
            // 
            this.btnStandardStats.Location = new System.Drawing.Point(68, 19);
            this.btnStandardStats.Name = "btnStandardStats";
            this.btnStandardStats.Size = new System.Drawing.Size(64, 23);
            this.btnStandardStats.TabIndex = 1;
            this.btnStandardStats.Text = "Standard";
            this.btnStandardStats.UseVisualStyleBackColor = true;
            this.btnStandardStats.Click += new System.EventHandler(this.btnStandardStats_Click);
            // 
            // btnBasicStats
            // 
            this.btnBasicStats.Location = new System.Drawing.Point(6, 19);
            this.btnBasicStats.Name = "btnBasicStats";
            this.btnBasicStats.Size = new System.Drawing.Size(56, 23);
            this.btnBasicStats.TabIndex = 0;
            this.btnBasicStats.Text = "Basic";
            this.btnBasicStats.UseVisualStyleBackColor = true;
            this.btnBasicStats.Click += new System.EventHandler(this.btnBasicStats_Click);
            // 
            // gbSelectDefs
            // 
            this.gbSelectDefs.Controls.Add(this.cblDefs);
            this.gbSelectDefs.Controls.Add(this.btnDeselectAllDefs);
            this.gbSelectDefs.Controls.Add(this.btnSelectAllDefs);
            this.gbSelectDefs.Location = new System.Drawing.Point(12, 370);
            this.gbSelectDefs.Name = "gbSelectDefs";
            this.gbSelectDefs.Size = new System.Drawing.Size(190, 260);
            this.gbSelectDefs.TabIndex = 2;
            this.gbSelectDefs.TabStop = false;
            this.gbSelectDefs.Text = "Select bands";
            // 
            // cblDefs
            // 
            this.cblDefs.FormattingEnabled = true;
            this.cblDefs.Items.AddRange(new object[] {
            ""});
            this.cblDefs.Location = new System.Drawing.Point(6, 48);
            this.cblDefs.Name = "cblDefs";
            this.cblDefs.Size = new System.Drawing.Size(178, 199);
            this.cblDefs.TabIndex = 2;
            // 
            // btnDeselectAllDefs
            // 
            this.btnDeselectAllDefs.Location = new System.Drawing.Point(100, 19);
            this.btnDeselectAllDefs.Name = "btnDeselectAllDefs";
            this.btnDeselectAllDefs.Size = new System.Drawing.Size(84, 23);
            this.btnDeselectAllDefs.TabIndex = 1;
            this.btnDeselectAllDefs.Text = "Deselect all";
            this.btnDeselectAllDefs.UseVisualStyleBackColor = true;
            this.btnDeselectAllDefs.Click += new System.EventHandler(this.btnDeselectAllDefs_Click);
            // 
            // btnSelectAllDefs
            // 
            this.btnSelectAllDefs.Location = new System.Drawing.Point(6, 19);
            this.btnSelectAllDefs.Name = "btnSelectAllDefs";
            this.btnSelectAllDefs.Size = new System.Drawing.Size(88, 23);
            this.btnSelectAllDefs.TabIndex = 0;
            this.btnSelectAllDefs.Text = "Select all";
            this.btnSelectAllDefs.UseVisualStyleBackColor = true;
            this.btnSelectAllDefs.Click += new System.EventHandler(this.btnSelectAllDefs_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnCalculate.Location = new System.Drawing.Point(12, 691);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(190, 23);
            this.btnCalculate.TabIndex = 3;
            this.btnCalculate.Text = "Calculate statistics";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // cbFastStats
            // 
            this.cbFastStats.AutoSize = true;
            this.cbFastStats.Checked = true;
            this.cbFastStats.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbFastStats.Location = new System.Drawing.Point(16, 665);
            this.cbFastStats.Name = "cbFastStats";
            this.cbFastStats.Size = new System.Drawing.Size(89, 17);
            this.cbFastStats.TabIndex = 4;
            this.cbFastStats.Text = "Fast statistics";
            this.cbFastStats.UseVisualStyleBackColor = true;
            // 
            // cbBessel
            // 
            this.cbBessel.AutoSize = true;
            this.cbBessel.Location = new System.Drawing.Point(16, 641);
            this.cbBessel.Name = "cbBessel";
            this.cbBessel.Size = new System.Drawing.Size(136, 17);
            this.cbBessel.TabIndex = 5;
            this.cbBessel.Text = "Use Bessel\'s correction";
            this.cbBessel.UseVisualStyleBackColor = true;
            // 
            // pProgress
            // 
            this.pProgress.Location = new System.Drawing.Point(208, 691);
            this.pProgress.Name = "pProgress";
            this.pProgress.Size = new System.Drawing.Size(714, 23);
            this.pProgress.TabIndex = 7;
            // 
            // gbResults
            // 
            this.gbResults.Controls.Add(this.btnSaveStats);
            this.gbResults.Controls.Add(this.btnLoadStats);
            this.gbResults.Controls.Add(this.rtbResult);
            this.gbResults.Location = new System.Drawing.Point(934, 12);
            this.gbResults.Name = "gbResults";
            this.gbResults.Size = new System.Drawing.Size(273, 702);
            this.gbResults.TabIndex = 8;
            this.gbResults.TabStop = false;
            this.gbResults.Text = "Results";
            // 
            // btnSaveStats
            // 
            this.btnSaveStats.Location = new System.Drawing.Point(147, 673);
            this.btnSaveStats.Name = "btnSaveStats";
            this.btnSaveStats.Size = new System.Drawing.Size(120, 23);
            this.btnSaveStats.TabIndex = 2;
            this.btnSaveStats.Text = "Save *.ttkstats";
            this.btnSaveStats.UseVisualStyleBackColor = true;
            this.btnSaveStats.Click += new System.EventHandler(this.btnSaveStats_Click);
            // 
            // btnLoadStats
            // 
            this.btnLoadStats.Location = new System.Drawing.Point(6, 673);
            this.btnLoadStats.Name = "btnLoadStats";
            this.btnLoadStats.Size = new System.Drawing.Size(120, 23);
            this.btnLoadStats.TabIndex = 1;
            this.btnLoadStats.Text = "Load *.ttkstats";
            this.btnLoadStats.UseVisualStyleBackColor = true;
            this.btnLoadStats.Click += new System.EventHandler(this.btnLoadStats_Click);
            // 
            // rtbResult
            // 
            this.rtbResult.Location = new System.Drawing.Point(6, 19);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(261, 648);
            this.rtbResult.TabIndex = 0;
            this.rtbResult.Text = "";
            // 
            // openDialog
            // 
            this.openDialog.FileName = "openFileDialog1";
            // 
            // GIS
            // 
            this.GIS.Location = new System.Drawing.Point(208, 12);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(714, 670);
            this.GIS.TabIndex = 6;
            this.GIS.BusyEvent += new TatukGIS.NDK.TGIS_BusyEvent(this.doBusyEvent);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1219, 726);
            this.Controls.Add(this.gbResults);
            this.Controls.Add(this.pProgress);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.cbBessel);
            this.Controls.Add(this.cbFastStats);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.gbSelectDefs);
            this.Controls.Add(this.gbSelectStatistics);
            this.Controls.Add(this.gbSelectLayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - LayerStatistics";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WinForm_FormClosed);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.gbSelectLayer.ResumeLayout(false);
            this.gbSelectLayer.PerformLayout();
            this.gbSelectStatistics.ResumeLayout(false);
            this.gbSelectDefs.ResumeLayout(false);
            this.gbResults.ResumeLayout(false);
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
            String sample_dir;

            abrt = false;
            btnCalculate.Text = BUTTON_CALCULATE;

            GIS.Mode = TGIS_ViewerMode.Zoom;

            openDialog.Filter = TGIS_Utils.GisSupportedFiles( TGIS_FileType.Grid | TGIS_FileType.Pixel | TGIS_FileType.Vector , false );

            // set file paths
            sample_dir = TGIS_Utils.GisSamplesDataDirDownload() + "/World/Countries/USA/States/California";
            sample_vector = sample_dir + "/Counties.shp";
            sample_grid = sample_dir + "/San Bernardino/NED/w001001.adf";
            sample_pixel = sample_dir + "/San Bernardino/DOQ/37134877.jpg";
            custom_path = "";

            // open sample vector layer
            rbVector.Checked = true;

            // set common functions
            checkPredefined(STATISTICS_STANDARD);
        }

        private void checkPredefined( String[] _predefined)
        {
            int id;

            for (int i = 0; i < cblStats.Items.Count; ++i)
            {
                cblStats.SetItemChecked(i, false);
            }

            foreach (String stat_fun in _predefined)
            {
                id = cblStats.Items.IndexOf(stat_fun);
                cblStats.SetItemChecked(id, true);
            }
        }

        private void disableOpenButton()
        {
            btnOpen.Enabled = false;
        }

        private void enableOpenButton()
        {
            btnOpen.Enabled = true;
        }

        private void rbVector_CheckedChanged(object sender, EventArgs e)
        {
            disableOpenButton();
            if(rbVector.Checked)
                openLayerAndStats(sample_vector);
        }

        private void rbGrid_CheckedChanged(object sender, EventArgs e)
        {
            disableOpenButton();
            if(rbGrid.Checked)
                openLayerAndStats(sample_grid);
        }

        private void rbPixel_CheckedChanged(object sender, EventArgs e)
        {
            disableOpenButton();
            if(rbPixel.Checked)
                openLayerAndStats(sample_pixel);
        }

        private void rbCustom_CheckedChanged(object sender, EventArgs e)
        {
            enableOpenButton();
            if (rbCustom.Checked)
            {
                if (!String.IsNullOrEmpty(custom_path))
                {
                    openLayerAndStats(custom_path);
                }
                else
                {
                    openFile();
                }
            }
        }
        private void openFile()
        {
            if( openDialog.ShowDialog() == DialogResult.OK)
            {
                custom_path = openDialog.FileName;
                openLayerAndStats(custom_path);
            }else
            {
                if( String.IsNullOrEmpty(custom_path))
                {
                    rbVector.Checked = true;
                }
            }
        }

        // depending on layer's type prepare list of available statistics definitions
        // field names for vector layers; band names for pixel layers
        private void prepareStatisticsDefinitions(TGIS_Layer layer)
        {
            TGIS_LayerVector lv;
            TGIS_LayerPixel lp;

            cblDefs.Items.Clear();

            if(layer is TGIS_LayerVector)
            {
                lv = (TGIS_LayerVector)layer;
                gbSelectDefs.Text = "Select fields";

                // fill with layer field names
                foreach( TGIS_FieldInfo field in lv.Fields)
                {
                    cblDefs.Items.Add(field.Name, true);
                }
            }
            else if(layer is TGIS_LayerPixel)
            {
                lp = (TGIS_LayerPixel)layer;
                gbSelectDefs.Text = "Select bands";

                // fill with appropriate band names
                if (lp.IsGrid())
                {
                    cblDefs.Items.Add(GIS_BAND_DEFAULT, false);
                }

                for( int i = 1; i <= lp.BandsCount; i++)
                {
                    cblDefs.Items.Add(i, false);
                }

                if (lp.IsGrid())
                {
                    cblDefs.Items.Add(GIS_BAND_GRID, true);
                }
                else
                {
                    cblDefs.Items.Add(GIS_BAND_A, false);
                    cblDefs.Items.Add(GIS_BAND_R, true);
                    cblDefs.Items.Add(GIS_BAND_G, true);
                    cblDefs.Items.Add(GIS_BAND_B, true);
                    cblDefs.Items.Add(GIS_BAND_H, false);
                    cblDefs.Items.Add(GIS_BAND_S, false);
                    cblDefs.Items.Add(GIS_BAND_L, false);
                }
            }
        }

        private void btnBasicStats_Click(object sender, EventArgs e)
        {
            checkPredefined(STATISTICS_BASIC);
        }

        private void btnStandardStats_Click(object sender, EventArgs e)
        {
            checkPredefined(STATISTICS_STANDARD);
        }

        private void btnAllStats_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblStats.Items.Count; i++)
            {
                cblStats.SetItemChecked(i, true);
            }
        }

        private void WinForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            abrt = true;
        }

        private void btnSelectAllDefs_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblDefs.Items.Count; i++)
            {
                cblDefs.SetItemChecked(i, true);
            }
        }

        private void btnDeselectAllDefs_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cblDefs.Items.Count; i++)
            {
                cblDefs.SetItemChecked(i, false);
            }
        }

        private Boolean prepareFunctions( out TGIS_StatisticalFunctions _funcs)
        {
            Boolean res;

            res = false;
            _funcs = TGIS_StatisticalFunctions.EmptyStatistics();

            for(int i = 0; i < cblStats.Items.Count; i++)
            {
                if (cblStats.GetItemChecked(i))
                {
                    res = true;
                    if (cblStats.Items[i].ToString() == "Average")
                        _funcs.Average = true;
                    else if (cblStats.Items[i].ToString() == "Count")
                        _funcs.Count = true;
                    else if (cblStats.Items[i].ToString() == "CountMissings")
                        _funcs.CountMissings = true;
                    else if (cblStats.Items[i].ToString() == "Max")
                        _funcs.Max = true;
                    else if (cblStats.Items[i].ToString() == "Majority")
                        _funcs.Majority = true;
                    else if (cblStats.Items[i].ToString() == "Median")
                        _funcs.Median = true;
                    else if (cblStats.Items[i].ToString() == "Min")
                        _funcs.Min = true;
                    else if (cblStats.Items[i].ToString() == "Minority")
                        _funcs.Minority = true;
                    else if (cblStats.Items[i].ToString() == "Range")
                        _funcs.Range = true;
                    else if (cblStats.Items[i].ToString() == "StandardDeviation")
                        _funcs.StandardDeviation = true;
                    else if (cblStats.Items[i].ToString() == "Sample")
                        _funcs.Sample = true;
                    else if (cblStats.Items[i].ToString() == "Sum")
                        _funcs.Sum = true;
                    else if (cblStats.Items[i].ToString() == "Variance")
                        _funcs.Variance = true;
                    else if (cblStats.Items[i].ToString() == "Variety")
                        _funcs.Variety = true;
                    else if (cblStats.Items[i].ToString() == "Unique")
                        _funcs.Unique = true;
                }
            }

            return res;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            TGIS_Layer ll;
            TGIS_StatisticalFunctions funcs;

            //cancel calculation
            if( btnCalculate.Text.Equals(BUTTON_CANCEL))
            {
                abrt = true;
                btnCalculate.Text = BUTTON_CALCULATE;
                return;
            }

            btnCalculate.Text = BUTTON_CANCEL;
            btnCalculate.Update();
            abrt = false;

            try
            {
                // statistical functions
                funcs = new TGIS_StatisticalFunctions();
                if (!prepareFunctions(out funcs))
                {
                    MessageBox.Show("Select at least one statistical function.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //creating statistical engine
                ll = (TGIS_Layer)GIS.Items[0];

                if (ll.Statistics == null)
                    return;
                
                // use Bessel's correction
                // if True, calculate "sample" standard devation and variance;
                // if False, calculate "population" version (this is default)
                ll.Statistics.UseBesselCorrection = cbBessel.Checked;

                // collect statistics definitions (fields or bands)
                for(int i = 0; i < cblDefs.Items.Count; i++)
                {
                    if (cblDefs.GetItemChecked(i))
                        ll.Statistics.Add( cblDefs.Items[i].ToString(), funcs );
                }

                if(ll.Statistics.DefinedResults.Count == 0)
                {
                    MessageBox.Show("Select at least one field for vector layer or band for pixel layer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // here the calculations starts
                
                // statistics class can calaculate statistics for a given extent;
                // for filtering data: "_extent", "_shape", "_de9im" and only for vector layers:
                // "_query", "_useSelected" parameters can be used
                
                // "Fast Statistics" works only for pixel layers (bu default);
                // big pixel layers are resampled to avoid long calculation;
                // the results are approximate with high accuracy
                ll.Statistics.Calculate(GIS.VisibleExtent, null, "", cbFastStats.Checked);
                
                // print results on TMemo control
                showResults(ll.Statistics, true );
            }
            finally
            {
                btnCalculate.Text = BUTTON_CALCULATE;
            }
        }

        private void showResults(TGIS_StatisticsAbstract _stats_engine, Boolean clear)
        {
            const String DASHED_LINE = "----------------------------------------";

            TGIS_StatisticsResult stats_result;
            List<TGIS_StatisticsItem> stats_available;
            String node_string;

            if(clear)
                rtbResult.Clear();

            foreach(String stats_name in _stats_engine.AvailableResults)
            {
                rtbResult.AppendText(DASHED_LINE + "\r\n");
                rtbResult.AppendText(stats_name + "\r\n");
                rtbResult.AppendText(DASHED_LINE + "\r\n");

                stats_result = _stats_engine.Get(stats_name);
                stats_available = stats_result.AvailableStatistics;
                foreach (TGIS_StatisticsItem stats_item in stats_available)
                {
                    node_string = ("    + " + stats_item.Name + " = " + stats_item.ToString());
                    rtbResult.AppendText(node_string + "\r\n");
                }
            }

            if (_stats_engine.Obsolete && ( DateTime.Compare( _stats_engine.Age, new DateTime(1899, 12, 30) ) > 0)  )
            {
                MessageBox.Show("Statistics are outdated.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void doBusyEvent(Object _sender, TGIS_BusyEventArgs _e)
        {
            if (_e.Pos < 0)
                pProgress.Value = 0;
            else
            if (_e.Pos == 0)
            {
                pProgress.Minimum = 0;
                pProgress.Maximum = 100;
                pProgress.Value = 0;
            }
            else
            {
                pProgress.Value = (int)_e.Pos;
            }

            _e.Abort = abrt;
            Application.DoEvents();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private TGIS_Layer getLayer()
        {
            return ((TGIS_Layer)GIS.Items[0]);
        }

        private void openLayerAndStats( String path )
        {
            TGIS_Layer ll;

            GIS.Open(path);
            ll = getLayer();
            prepareStatisticsDefinitions(ll);
            showResults(ll.Statistics, true);
        }

        private void btnLoadStats_Click(object sender, EventArgs e)
        {
            TGIS_Layer ll;

            ll = getLayer();
            if(!ll.Statistics.LoadFromFile())
            {
                MessageBox.Show("Loading failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveStats_Click(object sender, EventArgs e)
        {
            TGIS_Layer ll;

            ll = getLayer();
            ll.Statistics.SaveToFile();
        }
    }
}
