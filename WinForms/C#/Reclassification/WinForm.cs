using System;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Reclassification
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
        private TGIS_ControlLegend GISLegend;
        private ProgressBar progress;
        private TGIS_ViewerWnd GIS;
        private Button btnUseAltitudeZones;
        private Button btnUseTable;
        private RichTextBox edtReclassTable;
        private RichTextBox edtAltitudeZones;
        private DataGridView sgrdReclassTable;
        private DataGridViewTextBoxColumn clmnStart;
        private DataGridViewTextBoxColumn clmnEnd;
        private DataGridViewTextBoxColumn clmnNew;
        private Button btnReclassify;
        private CheckBox chkNoData;
        private RichTextBox edtNoData;
        private GroupBox grpbxReclassify;
        private Boolean useAltitudeMapZones;

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
      TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions2 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
      this.GISLegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
      this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
      this.progress = new System.Windows.Forms.ProgressBar();
      this.grpbxReclassify = new System.Windows.Forms.GroupBox();
      this.chkNoData = new System.Windows.Forms.CheckBox();
      this.edtNoData = new System.Windows.Forms.RichTextBox();
      this.btnReclassify = new System.Windows.Forms.Button();
      this.sgrdReclassTable = new System.Windows.Forms.DataGridView();
      this.clmnStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmnEnd = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.clmnNew = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.edtReclassTable = new System.Windows.Forms.RichTextBox();
      this.edtAltitudeZones = new System.Windows.Forms.RichTextBox();
      this.btnUseAltitudeZones = new System.Windows.Forms.Button();
      this.btnUseTable = new System.Windows.Forms.Button();
      this.grpbxReclassify.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.sgrdReclassTable)).BeginInit();
      this.SuspendLayout();
      // 
      // GISLegend
      // 
      this.GISLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      tgiS_ControlLegendDialogOptions2.VectorWizardUniqueLimit = 256;
      tgiS_ControlLegendDialogOptions2.VectorWizardUniqueSearchLimit = 16384;
      this.GISLegend.DialogOptions = tgiS_ControlLegendDialogOptions2;
      this.GISLegend.GIS_Viewer = this.GIS;
      this.GISLegend.Location = new System.Drawing.Point(877, 12);
      this.GISLegend.Name = "GISLegend";
      this.GISLegend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
      this.GISLegend.Size = new System.Drawing.Size(182, 577);
      this.GISLegend.TabIndex = 0;
      // 
      // GIS
      // 
      this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.GIS.AutoStyle = true;
      this.GIS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.GIS.Level = 28.140189979287609D;
      this.GIS.Location = new System.Drawing.Point(237, 12);
      this.GIS.Name = "GIS";
      this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.GIS.Size = new System.Drawing.Size(634, 577);
      this.GIS.TabIndex = 2;
      // 
      // progress
      // 
      this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.progress.Location = new System.Drawing.Point(12, 595);
      this.progress.Name = "progress";
      this.progress.Size = new System.Drawing.Size(1047, 23);
      this.progress.TabIndex = 1;
      // 
      // grpbxReclassify
      // 
      this.grpbxReclassify.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.grpbxReclassify.Controls.Add(this.chkNoData);
      this.grpbxReclassify.Controls.Add(this.edtNoData);
      this.grpbxReclassify.Controls.Add(this.btnReclassify);
      this.grpbxReclassify.Controls.Add(this.sgrdReclassTable);
      this.grpbxReclassify.Controls.Add(this.edtReclassTable);
      this.grpbxReclassify.Controls.Add(this.edtAltitudeZones);
      this.grpbxReclassify.Controls.Add(this.btnUseAltitudeZones);
      this.grpbxReclassify.Controls.Add(this.btnUseTable);
      this.grpbxReclassify.Location = new System.Drawing.Point(12, 12);
      this.grpbxReclassify.Name = "grpbxReclassify";
      this.grpbxReclassify.Size = new System.Drawing.Size(219, 577);
      this.grpbxReclassify.TabIndex = 3;
      this.grpbxReclassify.TabStop = false;
      this.grpbxReclassify.Text = "Raster Reclassification";
      // 
      // chkNoData
      // 
      this.chkNoData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.chkNoData.AutoSize = true;
      this.chkNoData.Location = new System.Drawing.Point(6, 463);
      this.chkNoData.Name = "chkNoData";
      this.chkNoData.Size = new System.Drawing.Size(188, 17);
      this.chkNoData.TabIndex = 7;
      this.chkNoData.Text = "Assign NODATA to missing values";
      this.chkNoData.Checked = true;
      this.chkNoData.UseVisualStyleBackColor = true;
      this.chkNoData.CheckedChanged += new System.EventHandler(this.chkNoData_CheckedChanged);
      // 
      // edtNoData
      // 
      this.edtNoData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.edtNoData.BackColor = System.Drawing.SystemColors.Control;
      this.edtNoData.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtNoData.Location = new System.Drawing.Point(6, 486);
      this.edtNoData.Name = "edtNoData";
      this.edtNoData.ReadOnly = true;
      this.edtNoData.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.edtNoData.Size = new System.Drawing.Size(207, 34);
      this.edtNoData.TabIndex = 6;
      this.edtNoData.Text = "Cell values outside the defined ranges will be filled with NODATA value.\n";
      // 
      // btnReclassify
      // 
      this.btnReclassify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnReclassify.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
      this.btnReclassify.Location = new System.Drawing.Point(6, 526);
      this.btnReclassify.Name = "btnReclassify";
      this.btnReclassify.Size = new System.Drawing.Size(207, 45);
      this.btnReclassify.TabIndex = 5;
      this.btnReclassify.Text = "Reclassify";
      this.btnReclassify.UseVisualStyleBackColor = true;
      this.btnReclassify.Click += new System.EventHandler(this.btnReclassify_Click);
      // 
      // sgrdReclassTable
      // 
      this.sgrdReclassTable.BackgroundColor = System.Drawing.SystemColors.Control;
      this.sgrdReclassTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.sgrdReclassTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.sgrdReclassTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnStart,
            this.clmnEnd,
            this.clmnNew});
      this.sgrdReclassTable.Location = new System.Drawing.Point(6, 183);
      this.sgrdReclassTable.Name = "sgrdReclassTable";
      this.sgrdReclassTable.Size = new System.Drawing.Size(207, 175);
      this.sgrdReclassTable.TabIndex = 4;
      // 
      // clmnStart
      // 
      this.clmnStart.Frozen = true;
      this.clmnStart.HeaderText = "Start";
      this.clmnStart.Name = "clmnStart";
      this.clmnStart.Width = 55;
      // 
      // clmnEnd
      // 
      this.clmnEnd.Frozen = true;
      this.clmnEnd.HeaderText = "End";
      this.clmnEnd.Name = "clmnEnd";
      this.clmnEnd.Width = 55;
      // 
      // clmnNew
      // 
      this.clmnNew.Frozen = true;
      this.clmnNew.HeaderText = "New";
      this.clmnNew.Name = "clmnNew";
      this.clmnNew.Width = 55;
      // 
      // edtReclassTable
      // 
      this.edtReclassTable.BackColor = System.Drawing.SystemColors.Control;
      this.edtReclassTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtReclassTable.Location = new System.Drawing.Point(6, 77);
      this.edtReclassTable.Name = "edtReclassTable";
      this.edtReclassTable.ReadOnly = true;
      this.edtReclassTable.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.edtReclassTable.Size = new System.Drawing.Size(207, 100);
      this.edtReclassTable.TabIndex = 3;
            this.edtReclassTable.Text = "In Reclassification Table you can define: "+
      "\r\n- Value reclassification definition(Old value -> New value)"+
      "\r\n-Range reclassification definition(values from [Start..End] -> New value)"+
      "\r\n-Value for NODATA, by typing 'nodata'->New value";
      // 
      // edtAltitudeZones
      // 
      this.edtAltitudeZones.BackColor = System.Drawing.SystemColors.Control;
      this.edtAltitudeZones.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtAltitudeZones.Location = new System.Drawing.Point(6, 77);
      this.edtAltitudeZones.Name = "edtAltitudeZones";
      this.edtAltitudeZones.ReadOnly = true;
      this.edtAltitudeZones.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.edtAltitudeZones.Size = new System.Drawing.Size(207, 100);
      this.edtAltitudeZones.TabIndex = 2;
            this.edtAltitudeZones.Text =  "As an alternative to the Reclassification Table, reclassification can be run based on altitude map zones defined in the pixel layer Params property." +
      "\r\nAdditionally, this method copies colors from zones and automatically transfers them to the output layer.";
      // 
      // btnUseAltitudeZones
      // 
      this.btnUseAltitudeZones.Location = new System.Drawing.Point(6, 48);
      this.btnUseAltitudeZones.Name = "btnUseAltitudeZones";
      this.btnUseAltitudeZones.Size = new System.Drawing.Size(207, 23);
      this.btnUseAltitudeZones.TabIndex = 1;
      this.btnUseAltitudeZones.Text = "Use Altitude Zones";
      this.btnUseAltitudeZones.UseVisualStyleBackColor = true;
      this.btnUseAltitudeZones.Click += new System.EventHandler(this.btnUseAltitudeZones_Click);
      // 
      // btnUseTable
      // 
      this.btnUseTable.Location = new System.Drawing.Point(6, 19);
      this.btnUseTable.Name = "btnUseTable";
      this.btnUseTable.Size = new System.Drawing.Size(207, 23);
      this.btnUseTable.TabIndex = 0;
      this.btnUseTable.Text = "Use table";
      this.btnUseTable.UseVisualStyleBackColor = true;
      this.btnUseTable.Click += new System.EventHandler(this.btnUseTable_Click);
      // 
      // WinForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(1071, 630);
      this.Controls.Add(this.grpbxReclassify);
      this.Controls.Add(this.GIS);
      this.Controls.Add(this.progress);
      this.Controls.Add(this.GISLegend);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Location = new System.Drawing.Point(200, 120);
      this.Name = "WinForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "TatukGIS Samples - Reclassification";
      this.Load += new System.EventHandler(this.WinForm_Load);
      this.grpbxReclassify.ResumeLayout(false);
      this.grpbxReclassify.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.sgrdReclassTable)).EndInit();
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
            // Set reclass table
            for (int j = 0; j<=4; j++)
            {
                int i = j+1;
                sgrdReclassTable.Rows.Add();
                sgrdReclassTable.Rows[j].Cells[0].Value = (100*i).ToString();
                sgrdReclassTable.Rows[j].Cells[1].Value = (100*(i+1)).ToString();
                sgrdReclassTable.Rows[j].Cells[2].Value = (i).ToString();
            }

            sgrdReclassTable.Rows[4].Cells[0].Value = "311";
            sgrdReclassTable.Rows[4].Cells[1].Value = "";
            sgrdReclassTable.Rows[4].Cells[2].Value = "6";

            sgrdReclassTable.Rows[5].Cells[0].Value = "nodata";
            sgrdReclassTable.Rows[5].Cells[1].Value = "";
            sgrdReclassTable.Rows[5].Cells[2].Value = "999";
          
            btnUseTable.PerformClick();

        }

        private void btnUseTable_Click(object sender, EventArgs e)
        {
          edtReclassTable.Visible = true;
          sgrdReclassTable.Visible = true;
          edtAltitudeZones.Visible = false;

          OpenSampleForReclassTable();
        }

        private void btnUseAltitudeZones_Click(object sender, EventArgs e)
        {
          edtReclassTable.Visible = false;
          sgrdReclassTable.Visible = false;
          edtAltitudeZones.Visible = true;

          OpenSampleForUseAltitudeZones();
        }

        private void chkNoData_CheckedChanged(object sender, EventArgs e)
        {
          if (chkNoData.Checked)
            edtNoData.Text = "Cell values outside the defined ranges will be filled with NODATA value.";
          else
            edtNoData.Text = "Cell values outside the defined ranges will be filled with original value.";
        }

        private void doBusyEvent(Object _sender, TGIS_BusyEventArgs _e)
        {
            if (_e.Pos < 0)
                progress.Value = 0;
            else
            if (_e.Pos == 0)
            {
                progress.Minimum = 0;
                progress.Maximum = 100;
                progress.Value = 0;
            }
            else
            {
                progress.Value = (int)_e.Pos;
            }
        }

        private void btnReclassify_Click(object sender, EventArgs e)
        {
          TGIS_LayerPixel lp, lp_reclass;
          String name;
          TGIS_Reclassification reclassifier;
          String startValStr, endValStr, newValStr;
          double startVal, endVal, newVal;
          Boolean startExist, endExist, newExist;
          int row;

          lp = (TGIS_LayerPixel)GIS.Items[0];

          // Remove a layer from GIS if exist
          name = lp.Name + " (reclass)";
          if (GIS.Get(name) != null)
            GIS.Delete(name);

          // Prepare the destination layer
          lp_reclass = new TGIS_LayerPixel();
          lp_reclass.Name = name;
          lp_reclass.Build(true, lp.CS, lp.Extent, lp.BitWidth, lp.BitHeight);

          reclassifier = new TGIS_Reclassification();

          reclassifier.BusyEvent += doBusyEvent ;

          for (row = 0; row < sgrdReclassTable.RowCount; row++)
          {
            startValStr = sgrdReclassTable.Rows[row].Cells[0].Value.ToString();
            endValStr = sgrdReclassTable.Rows[row].Cells[1].Value.ToString();
            newValStr = sgrdReclassTable.Rows[row].Cells[2].Value.ToString();

            if ( String.IsNullOrEmpty( startValStr ) )
              continue;

            startExist = double.TryParse(startValStr, out startVal);
            endExist = double.TryParse(endValStr, out endVal);
            newExist = double.TryParse(newValStr, out newVal);

            // Assign a new value for the existing nodata
            if ((startValStr.Contains("nd") || startValStr.Contains("nodata") || startValStr.Contains("no-data")) && newExist)
              reclassifier.ReclassNoDataValue = (float)newVal;
            // Simple value-to-value reclassification
            else if (String.IsNullOrEmpty(endValStr) || ( startVal == endVal) && startExist && newExist)
              reclassifier.AddReclassValue((float)startVal, (float)newVal);
            // Assgin a new value for a value within the range
            else if (startExist && endExist && newExist)
              reclassifier.AddReclassRange((float)startVal, (float)endVal, (float)newVal);
          }

          // Assign NoData for unclassified cells if True, or leave existing values if False
          reclassifier.UseNoDataForMissingValues = chkNoData.Checked;

          // Run the reclassification tool
          reclassifier.Generate(lp, lp.Extent, lp_reclass, useAltitudeMapZones);

          if (!useAltitudeMapZones)
            ApplyUniqueStyle(lp_reclass, "UniqueDeep");

          lp_reclass.Params.Pixel.GridShadow = false;
          lp_reclass.Params.Pixel.Antialias = false;

          GIS.Add(lp_reclass);
          GIS.InvalidateWholeMap();
    }

        private void OpenSampleForUseAltitudeZones()
        {
          TGIS_LayerPixel lp;

          useAltitudeMapZones = true;

          GIS.Lock();

          try
          {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "Samples/3D/elevation.grd");
            lp = (TGIS_LayerPixel)GIS.Items[0];
            ApplyNaturalBreaksStyle(lp, "Geology");
          }
          finally
          {
            GIS.Unlock();
          }
        }

        private void OpenSampleForReclassTable()
        {
          TGIS_LayerPixel lp;

          useAltitudeMapZones = false;

          GIS.Lock();

          try
          {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "World\\Countries\\Luxembourg\\CLC2018_CLC2018_V2018_20_Luxembourg.tif");
            lp = (TGIS_LayerPixel)GIS.Items[0];
            ApplyUniqueStyle(lp, "UniquePastel");
          }
          finally
          {
            GIS.Unlock();
          }
        }

        private void ApplyNaturalBreaksStyle(TGIS_LayerPixel _lp, String _colorRampName)
        {
          TGIS_ClassificationPixel classifier;

          classifier = new TGIS_ClassificationPixel(_lp);
          classifier.Method= TGIS_ClassificationMethod.NaturalBreaks ;
          classifier.ColorRampName = _colorRampName;

          classifier.Classify();
        }

        private void ApplyUniqueStyle(TGIS_LayerPixel _lp, String _colorRampName)
        {
          TGIS_ClassificationPixel classifier;

          classifier = new TGIS_ClassificationPixel(_lp);
          classifier.Method = TGIS_ClassificationMethod.Unique;
          classifier.ColorRampName = _colorRampName;

          classifier.Classify();
        }
  }
}
