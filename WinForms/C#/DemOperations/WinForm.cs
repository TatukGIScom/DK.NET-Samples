using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace DemOperations
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Button btnOpen;
        private ImageList ilIcons;
        private Button btnFullExtent;
        private Button btnZoom;
        private Button btnDrag;
        private Button btn3D;
        private Panel panel1;
        private Panel panel2;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend ControlLegend;
        private OpenFileDialog dlgOpen;
        private GroupBox gbShadow;
        private TrackBar trbShadow;
        private CheckBox cbxCustomGrid;
        private GroupBox gbOperations;
        private Button btnRun;
        private Label lbOperations;
        private ComboBox cbOperations;
        private GroupBox gbHillShadeParams;
        private Label lbAzimuth;
        private Label lbAltitude;
        private Label lbZFactor;
        private TextBox tbZFactor;
        private TextBox tbAltitude;
        private TextBox tbAzimuth;
        private GroupBox gbSloperParams;
        private CheckBox cbxCombined;
        private Label lbScale;
        private Label lbMode;
        private TextBox tbScale;
        private ComboBox cbMode;
        private CheckBox cbxAngle;
        private GroupBox gbCurvature;
        private ComboBox cbCurvMode;
        private Label lbCurvMode;

        const Single GIS_MAX_SINGLE = (Single)3.4e38;
        private ProgressBar pbProgress;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions1 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            this.btnOpen = new System.Windows.Forms.Button();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnFullExtent = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnDrag = new System.Windows.Forms.Button();
            this.btn3D = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.btnRun = new System.Windows.Forms.Button();
            this.gbOperations = new System.Windows.Forms.GroupBox();
            this.gbCurvature = new System.Windows.Forms.GroupBox();
            this.cbCurvMode = new System.Windows.Forms.ComboBox();
            this.lbCurvMode = new System.Windows.Forms.Label();
            this.cbxAngle = new System.Windows.Forms.CheckBox();
            this.gbSloperParams = new System.Windows.Forms.GroupBox();
            this.tbScale = new System.Windows.Forms.TextBox();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.lbScale = new System.Windows.Forms.Label();
            this.lbMode = new System.Windows.Forms.Label();
            this.gbHillShadeParams = new System.Windows.Forms.GroupBox();
            this.cbxCombined = new System.Windows.Forms.CheckBox();
            this.tbZFactor = new System.Windows.Forms.TextBox();
            this.tbAltitude = new System.Windows.Forms.TextBox();
            this.tbAzimuth = new System.Windows.Forms.TextBox();
            this.lbZFactor = new System.Windows.Forms.Label();
            this.lbAltitude = new System.Windows.Forms.Label();
            this.lbAzimuth = new System.Windows.Forms.Label();
            this.lbOperations = new System.Windows.Forms.Label();
            this.cbOperations = new System.Windows.Forms.ComboBox();
            this.cbxCustomGrid = new System.Windows.Forms.CheckBox();
            this.gbShadow = new System.Windows.Forms.GroupBox();
            this.trbShadow = new System.Windows.Forms.TrackBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.ControlLegend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.gbOperations.SuspendLayout();
            this.gbCurvature.SuspendLayout();
            this.gbSloperParams.SuspendLayout();
            this.gbHillShadeParams.SuspendLayout();
            this.gbShadow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbShadow)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnOpen.ImageIndex = 0;
            this.btnOpen.ImageList = this.ilIcons;
            this.btnOpen.Location = new System.Drawing.Point(0, 0);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(31, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Fuchsia;
            this.ilIcons.Images.SetKeyName(0, "Open.bmp");
            this.ilIcons.Images.SetKeyName(1, "FullExtent.bmp");
            this.ilIcons.Images.SetKeyName(2, "ZoomEx.bmp");
            this.ilIcons.Images.SetKeyName(3, "Drag.bmp");
            this.ilIcons.Images.SetKeyName(4, "3DRotate.bmp");
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFullExtent.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnFullExtent.ImageIndex = 1;
            this.btnFullExtent.ImageList = this.ilIcons;
            this.btnFullExtent.Location = new System.Drawing.Point(30, 0);
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(31, 23);
            this.btnFullExtent.TabIndex = 1;
            this.btnFullExtent.UseVisualStyleBackColor = true;
            this.btnFullExtent.Click += new System.EventHandler(this.btnFullExtent_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoom.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnZoom.ImageIndex = 2;
            this.btnZoom.ImageList = this.ilIcons;
            this.btnZoom.Location = new System.Drawing.Point(60, 0);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(31, 23);
            this.btnZoom.TabIndex = 2;
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnDrag
            // 
            this.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrag.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnDrag.ImageIndex = 3;
            this.btnDrag.ImageList = this.ilIcons;
            this.btnDrag.Location = new System.Drawing.Point(90, 0);
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(31, 23);
            this.btnDrag.TabIndex = 3;
            this.btnDrag.UseVisualStyleBackColor = true;
            this.btnDrag.Click += new System.EventHandler(this.btnDrag_Click);
            // 
            // btn3D
            // 
            this.btn3D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn3D.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btn3D.ImageIndex = 4;
            this.btn3D.ImageList = this.ilIcons;
            this.btn3D.Location = new System.Drawing.Point(120, 0);
            this.btn3D.Name = "btn3D";
            this.btn3D.Size = new System.Drawing.Size(31, 23);
            this.btn3D.TabIndex = 4;
            this.btn3D.UseVisualStyleBackColor = true;
            this.btn3D.Click += new System.EventHandler(this.btn3D_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbProgress);
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Controls.Add(this.gbOperations);
            this.panel1.Controls.Add(this.cbxCustomGrid);
            this.panel1.Controls.Add(this.gbShadow);
            this.panel1.Location = new System.Drawing.Point(0, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(223, 655);
            this.panel1.TabIndex = 5;
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(5, 379);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(210, 23);
            this.pbProgress.TabIndex = 0;
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(60, 341);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(105, 23);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run operation";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // gbOperations
            // 
            this.gbOperations.AutoSize = true;
            this.gbOperations.Controls.Add(this.gbCurvature);
            this.gbOperations.Controls.Add(this.cbxAngle);
            this.gbOperations.Controls.Add(this.gbSloperParams);
            this.gbOperations.Controls.Add(this.gbHillShadeParams);
            this.gbOperations.Controls.Add(this.lbOperations);
            this.gbOperations.Controls.Add(this.cbOperations);
            this.gbOperations.Location = new System.Drawing.Point(5, 93);
            this.gbOperations.Name = "gbOperations";
            this.gbOperations.Size = new System.Drawing.Size(210, 242);
            this.gbOperations.TabIndex = 2;
            this.gbOperations.TabStop = false;
            this.gbOperations.Text = "Operations";
            // 
            // gbCurvature
            // 
            this.gbCurvature.Controls.Add(this.cbCurvMode);
            this.gbCurvature.Controls.Add(this.lbCurvMode);
            this.gbCurvature.Location = new System.Drawing.Point(25, 67);
            this.gbCurvature.Name = "gbCurvature";
            this.gbCurvature.Size = new System.Drawing.Size(161, 51);
            this.gbCurvature.TabIndex = 5;
            this.gbCurvature.TabStop = false;
            this.gbCurvature.Visible = false;
            // 
            // cbCurvMode
            // 
            this.cbCurvMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurvMode.FormattingEnabled = true;
            this.cbCurvMode.Items.AddRange(new object[] {
            "Profile",
            "Plan"});
            this.cbCurvMode.SelectedIndex = 0;
            this.cbCurvMode.Location = new System.Drawing.Point(67, 19);
            this.cbCurvMode.Name = "cbCurvMode";
            this.cbCurvMode.Size = new System.Drawing.Size(74, 21);
            this.cbCurvMode.TabIndex = 1;
            // 
            // lbCurvMode
            // 
            this.lbCurvMode.AutoSize = true;
            this.lbCurvMode.Location = new System.Drawing.Point(16, 22);
            this.lbCurvMode.Name = "lbCurvMode";
            this.lbCurvMode.Size = new System.Drawing.Size(37, 13);
            this.lbCurvMode.TabIndex = 0;
            this.lbCurvMode.Text = "Mode:";
            // 
            // cbxAngle
            // 
            this.cbxAngle.AutoSize = true;
            this.cbxAngle.Location = new System.Drawing.Point(35, 76);
            this.cbxAngle.Name = "cbxAngle";
            this.cbxAngle.Size = new System.Drawing.Size(141, 17);
            this.cbxAngle.TabIndex = 4;
            this.cbxAngle.Text = "Angle instead of azimuth";
            this.cbxAngle.UseVisualStyleBackColor = true;
            this.cbxAngle.Visible = false;
            // 
            // gbSloperParams
            // 
            this.gbSloperParams.AutoSize = true;
            this.gbSloperParams.Controls.Add(this.tbScale);
            this.gbSloperParams.Controls.Add(this.cbMode);
            this.gbSloperParams.Controls.Add(this.lbScale);
            this.gbSloperParams.Controls.Add(this.lbMode);
            this.gbSloperParams.Location = new System.Drawing.Point(25, 67);
            this.gbSloperParams.Name = "gbSloperParams";
            this.gbSloperParams.Size = new System.Drawing.Size(161, 78);
            this.gbSloperParams.TabIndex = 3;
            this.gbSloperParams.TabStop = false;
            this.gbSloperParams.Visible = false;
            // 
            // tbScale
            // 
            this.tbScale.Location = new System.Drawing.Point(67, 39);
            this.tbScale.Name = "tbScale";
            this.tbScale.Size = new System.Drawing.Size(74, 20);
            this.tbScale.TabIndex = 3;
            this.tbScale.Text = "111120";
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "Degrees",
            "Percent"});
            this.cbMode.SelectedIndex = 0;
            this.cbMode.Location = new System.Drawing.Point(67, 13);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(74, 21);
            this.cbMode.TabIndex = 2;
            // 
            // lbScale
            // 
            this.lbScale.AutoSize = true;
            this.lbScale.Location = new System.Drawing.Point(16, 42);
            this.lbScale.Name = "lbScale";
            this.lbScale.Size = new System.Drawing.Size(37, 13);
            this.lbScale.TabIndex = 1;
            this.lbScale.Text = "Scale:";
            // 
            // lbMode
            // 
            this.lbMode.AutoSize = true;
            this.lbMode.Location = new System.Drawing.Point(16, 16);
            this.lbMode.Name = "lbMode";
            this.lbMode.Size = new System.Drawing.Size(37, 13);
            this.lbMode.TabIndex = 0;
            this.lbMode.Text = "Mode:";
            // 
            // gbHillShadeParams
            // 
            this.gbHillShadeParams.AutoSize = true;
            this.gbHillShadeParams.Controls.Add(this.cbxCombined);
            this.gbHillShadeParams.Controls.Add(this.tbZFactor);
            this.gbHillShadeParams.Controls.Add(this.tbAltitude);
            this.gbHillShadeParams.Controls.Add(this.tbAzimuth);
            this.gbHillShadeParams.Controls.Add(this.lbZFactor);
            this.gbHillShadeParams.Controls.Add(this.lbAltitude);
            this.gbHillShadeParams.Controls.Add(this.lbAzimuth);
            this.gbHillShadeParams.Location = new System.Drawing.Point(25, 67);
            this.gbHillShadeParams.Name = "gbHillShadeParams";
            this.gbHillShadeParams.Size = new System.Drawing.Size(161, 128);
            this.gbHillShadeParams.TabIndex = 2;
            this.gbHillShadeParams.TabStop = false;
            this.gbHillShadeParams.Visible = false;
            // 
            // cbxCombined
            // 
            this.cbxCombined.AutoSize = true;
            this.cbxCombined.Location = new System.Drawing.Point(68, 92);
            this.cbxCombined.Name = "cbxCombined";
            this.cbxCombined.Size = new System.Drawing.Size(73, 17);
            this.cbxCombined.TabIndex = 6;
            this.cbxCombined.Text = "Combined";
            this.cbxCombined.UseVisualStyleBackColor = true;
            // 
            // tbZFactor
            // 
            this.tbZFactor.Location = new System.Drawing.Point(67, 66);
            this.tbZFactor.Name = "tbZFactor";
            this.tbZFactor.Size = new System.Drawing.Size(74, 20);
            this.tbZFactor.TabIndex = 5;
            this.tbZFactor.Text = "1";
            // 
            // tbAltitude
            // 
            this.tbAltitude.Location = new System.Drawing.Point(67, 42);
            this.tbAltitude.Name = "tbAltitude";
            this.tbAltitude.Size = new System.Drawing.Size(74, 20);
            this.tbAltitude.TabIndex = 4;
            this.tbAltitude.Text = "45";
            // 
            // tbAzimuth
            // 
            this.tbAzimuth.Location = new System.Drawing.Point(67, 17);
            this.tbAzimuth.Name = "tbAzimuth";
            this.tbAzimuth.Size = new System.Drawing.Size(74, 20);
            this.tbAzimuth.TabIndex = 3;
            this.tbAzimuth.Text = "315";
            // 
            // lbZFactor
            // 
            this.lbZFactor.AutoSize = true;
            this.lbZFactor.Location = new System.Drawing.Point(17, 69);
            this.lbZFactor.Name = "lbZFactor";
            this.lbZFactor.Size = new System.Drawing.Size(47, 13);
            this.lbZFactor.TabIndex = 2;
            this.lbZFactor.Text = "Z factor:";
            // 
            // lbAltitude
            // 
            this.lbAltitude.AutoSize = true;
            this.lbAltitude.Location = new System.Drawing.Point(16, 45);
            this.lbAltitude.Name = "lbAltitude";
            this.lbAltitude.Size = new System.Drawing.Size(45, 13);
            this.lbAltitude.TabIndex = 1;
            this.lbAltitude.Text = "Altitude:";
            // 
            // lbAzimuth
            // 
            this.lbAzimuth.AutoSize = true;
            this.lbAzimuth.Location = new System.Drawing.Point(16, 20);
            this.lbAzimuth.Name = "lbAzimuth";
            this.lbAzimuth.Size = new System.Drawing.Size(47, 13);
            this.lbAzimuth.TabIndex = 0;
            this.lbAzimuth.Text = "Azimuth:";
            // 
            // lbOperations
            // 
            this.lbOperations.AutoSize = true;
            this.lbOperations.Location = new System.Drawing.Point(24, 23);
            this.lbOperations.Name = "lbOperations";
            this.lbOperations.Size = new System.Drawing.Size(56, 13);
            this.lbOperations.TabIndex = 1;
            this.lbOperations.Text = "Operation:";
            // 
            // cbOperations
            // 
            this.cbOperations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperations.FormattingEnabled = true;
            this.cbOperations.Items.AddRange(new object[] {
            "Hillshade",
            "Slope",
            "Slope Hydro",
            "Aspect",
            "TRI",
            "TPI",
            "Roughness",
            "Total Curvature",
            "Matrix Gain",
            "Flow dir"});
            this.cbOperations.SelectedIndex = 0;
            this.cbOperations.Location = new System.Drawing.Point(25, 45);
            this.cbOperations.Name = "cbOperations";
            this.cbOperations.Size = new System.Drawing.Size(161, 21);
            this.cbOperations.TabIndex = 0;
            this.cbOperations.SelectedIndexChanged += new System.EventHandler(this.cbOperations_SelectedIndexChanged);
            // 
            // cbxCustomGrid
            // 
            this.cbxCustomGrid.AutoSize = true;
            this.cbxCustomGrid.Location = new System.Drawing.Point(30, 70);
            this.cbxCustomGrid.Name = "cbxCustomGrid";
            this.cbxCustomGrid.Size = new System.Drawing.Size(173, 17);
            this.cbxCustomGrid.TabIndex = 1;
            this.cbxCustomGrid.Text = "Attach custom grid operation    ";
            this.cbxCustomGrid.UseVisualStyleBackColor = true;
            this.cbxCustomGrid.CheckedChanged += new System.EventHandler(this.cbCustomGrid_CheckedChanged);
            // 
            // gbShadow
            // 
            this.gbShadow.Controls.Add(this.trbShadow);
            this.gbShadow.Location = new System.Drawing.Point(5, 3);
            this.gbShadow.Name = "gbShadow";
            this.gbShadow.Size = new System.Drawing.Size(210, 61);
            this.gbShadow.TabIndex = 0;
            this.gbShadow.TabStop = false;
            this.gbShadow.Text = "Shadow angle";
            // 
            // trbShadow
            // 
            this.trbShadow.Location = new System.Drawing.Point(4, 12);
            this.trbShadow.Maximum = 360;
            this.trbShadow.Minimum = 1;
            this.trbShadow.Name = "trbShadow";
            this.trbShadow.Size = new System.Drawing.Size(201, 45);
            this.trbShadow.TabIndex = 0;
            this.trbShadow.TickFrequency = 30;
            this.trbShadow.Value = 90;
            this.trbShadow.Scroll += new System.EventHandler(this.tbShadow_Scroll);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 691);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(935, 19);
            this.panel2.TabIndex = 6;
            // 
            // ControlLegend
            // 
            this.ControlLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions1.VectorWizardUniqueSearchLimit = 16384;
            this.ControlLegend.DialogOptions = tgiS_ControlLegendDialogOptions1;
            this.ControlLegend.GIS_Group = null;
            this.ControlLegend.GIS_Layer = null;
            this.ControlLegend.GIS_Viewer = this.GIS;
            this.ControlLegend.Location = new System.Drawing.Point(784, 30);
            this.ControlLegend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.ControlLegend.Name = "ControlLegend";
            this.ControlLegend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers) 
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.ControlLegend.ReverseOrder = false;
            this.ControlLegend.Size = new System.Drawing.Size(151, 655);
            this.ControlLegend.TabIndex = 8;
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(219, 30);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(570, 655);
            this.GIS.TabIndex = 7;
            this.GIS.BusyEvent += new TatukGIS.NDK.TGIS_BusyEvent(this.GIS_BusyEvent);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(934, 711);
            this.Controls.Add(this.ControlLegend);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn3D);
            this.Controls.Add(this.btnDrag);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.btnFullExtent);
            this.Controls.Add(this.btnOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - DemOperations";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbOperations.ResumeLayout(false);
            this.gbOperations.PerformLayout();
            this.gbCurvature.ResumeLayout(false);
            this.gbCurvature.PerformLayout();
            this.gbSloperParams.ResumeLayout(false);
            this.gbSloperParams.PerformLayout();
            this.gbHillShadeParams.ResumeLayout(false);
            this.gbHillShadeParams.PerformLayout();
            this.gbShadow.ResumeLayout(false);
            this.gbShadow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbShadow)).EndInit();
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
            dlgOpen.Filter = TGIS_Utils.GisSupportedFiles(TGIS_FileType.All, false);
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf");
            GIS.FullExtent();

            cbOperations_SelectedIndexChanged(sender, e);
        }

        private Boolean changeDEM(
            Object _layer,
            TGIS_Extent _extent,
            Single[][] _source,
            ref Single[][] _output,
            int _width,
            int _height,
            ref Single _minz,
            ref Single _maxz)
        {
            const Double DEG_TO_RAD = Math.PI / 180.0;

            int i, j;
            Double sin_alt_rad;
            Double cos_alt_zsf;
            Double az_rad;
            Double square_z_sf;
            Double z_scale_factor;
            Single minz, maxz;
            Double ZFactor;
            Double Azimuth;
            Double Altitude;
            Double XRes;
            Double YRes;
            Double Scale;
            Single[] AWindow;
            int k;
            int xsize;
            int ysize;
            Double xscale;
            Double yscale;
            Single val;
            Boolean usealg;
            int l1, l2, l3;
            Single inodata;
            Double x, y, aspect, xx_plus_yy, cang;
            Boolean final_result;

            final_result = true;
            AWindow = new Single[9];

            xsize = _width;
            ysize = _height;
            xscale = (_extent.XMax - _extent.XMin) / xsize;
            yscale = (_extent.YMax - _extent.YMin) / ysize;
            inodata = ((TGIS_LayerPixel)_layer).NoDataValue;

            XRes = xscale;
            YRes = yscale;

            Scale = 1;
            minz = GIS_MAX_SINGLE;
            maxz = -GIS_MAX_SINGLE;

            ZFactor = 2;
            Azimuth = trbShadow.Value;
            Altitude = 15;

            sin_alt_rad = Math.Sin(Altitude * DEG_TO_RAD);
            az_rad = Azimuth * DEG_TO_RAD;
            z_scale_factor = ZFactor / (2 * Scale);
            cos_alt_zsf = Math.Cos(Altitude * DEG_TO_RAD);
            square_z_sf = z_scale_factor * z_scale_factor;

            for (i = 2; i < _height - 1; i++)
            {
                l1 = i - 2;
                l2 = i - 1;
                l3 = i;

                for (j = 1; j < _width - 2; j++)
                {
                    AWindow[0] = _source[l1][j - 1];
                    AWindow[1] = _source[l1][j];
                    AWindow[2] = _source[l1][j + 1];
                    AWindow[3] = _source[l2][j - 1];
                    AWindow[4] = _source[l2][j];
                    AWindow[5] = _source[l2][j + 1];
                    AWindow[6] = _source[l3][j - 1];
                    AWindow[7] = _source[l3][j];
                    AWindow[8] = _source[l3][j + 1];

                    usealg = true;
                    val = inodata;
                    if (Math.Abs(AWindow[4] - inodata) < 1e-10)
                    {
                        val = inodata;
                        usealg = false;
                    }
                    else
                    {
                        for (k = 0; k < 8; k++)
                        {
                            if (Math.Abs(AWindow[k] - inodata) < 1e-10)
                            {
                                val = inodata;
                                usealg = false;
                                break;
                            }
                        }
                    }

                    if (usealg)
                    {
                        x = (AWindow[3] - AWindow[5]) / XRes;
                        y = (AWindow[7] - AWindow[1]) / YRes;

                        xx_plus_yy = x * x + y * y;
                        aspect = Math.Atan2(y, x);
                        cang = (sin_alt_rad - cos_alt_zsf * Math.Sqrt(xx_plus_yy) * Math.Sin(aspect - az_rad)) / Math.Sqrt(1 + square_z_sf * xx_plus_yy);

                        if (cang <= 0.0)
                            cang = 1.0;
                        else
                            cang = 1.0 + (254.0 * cang);
                        val = (Single)cang;
                    }

                    if (_source[l1][j] != inodata)
                        _output[l1][j] = val;

                    if ((val < minz) && (val != inodata))
                        minz = val;

                    if ((val > maxz) && (val != inodata))
                        maxz = val;
                }
            }
            _minz = minz;
            _maxz = maxz;

            return final_result;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            dlgOpen.ShowDialog();

            GIS.Open(dlgOpen.FileName);
        }

        private void btnFullExtent_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.FullExtent();
        }

        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        private void btnDrag_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.Mode = TGIS_ViewerMode.Drag;
        }

        private void btn3D_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            GIS.View3D = !GIS.View3D;
        }

        private void tbShadow_Scroll(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            if (lp == null) return;

            lp.Params.Pixel.GridShadowAngle = trbShadow.Value;

            if (!GIS.InPaint)
                GIS.InvalidateWholeMap();
        }

        private void cbCustomGrid_CheckedChanged(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            if (lp == null) return;

            if (cbxCustomGrid.Checked)
            {
                lp.Params.Pixel.AltitudeMapZones.Clear();
                lp.Params.Pixel.GridShadow = false;
                lp.GridOperationEvent += changeDEM;
            }
            else
            {
                lp.GridOperationEvent -= changeDEM;
                lp.Params.Pixel.GridShadow = true;
            }
            GIS.InvalidateWholeMap();
        }

        private void cbOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            gbHillShadeParams.Visible = false;
            gbCurvature.Visible = false;
            cbxAngle.Visible = false;
            gbSloperParams.Visible = false;

            switch (cbOperations.SelectedIndex)
            {
                case 0: gbHillShadeParams.Visible = true; break;
                case 1: gbSloperParams.Visible = true; break;
                case 2: gbSloperParams.Visible = true; break;
                case 3: cbxAngle.Visible = true; break;
                case 7: gbCurvature.Visible = true; break;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            TGIS_LayerPixel lp;
            TGIS_LayerPixel ld;
            TGIS_DemGenerator dem;
            TGIS_DemOperation dop;
            TGIS_DemSlopeMode sm;
            TGIS_DemTotalCurvatureMode cm;

            lp = (TGIS_LayerPixel)GIS.Items[0];

            ld = new TGIS_LayerPixel();
            ld.Name = "out_";
            ld.CS = lp.CS;
            ld.Build(true, lp.CS, lp.Extent, lp.BitWidth, lp.BitHeight);

            dem = new TGIS_DemGenerator();

            switch (cbOperations.SelectedIndex)
            {
                // HillShade
                case 0:
                    dop = new TGIS_DemOperationHillShade(float.Parse(tbZFactor.Text),
                                                         float.Parse(tbAzimuth.Text),
                                                         float.Parse(tbAltitude.Text),
                                                         cbxCombined.Checked);
                    break;
                // Slope
                case 1:
                    switch (cbMode.SelectedIndex)
                    {
                        case 0: sm = TGIS_DemSlopeMode.Degrees; break;
                        case 1: sm = TGIS_DemSlopeMode.Percent; break;
                        default: sm = TGIS_DemSlopeMode.Degrees; break;
                    }
                    dop = new TGIS_DemOperationSlope(sm, float.Parse(tbScale.Text));
                    break;
                // SlopeHydro
                case 2:
                    switch (cbMode.SelectedIndex)
                    {
                        case 0: sm = TGIS_DemSlopeMode.Degrees; break;
                        case 1: sm = TGIS_DemSlopeMode.Percent; break;
                        default: sm = TGIS_DemSlopeMode.Degrees; break;
                    }
                    dop = new TGIS_DemOperationSlopeHydro(sm, float.Parse(tbScale.Text));
                    break;

                // Aspect
                case 3: dop = new TGIS_DemOperationAspect(cbxAngle.Checked); break;

                // TRI
                case 4: dop = new TGIS_DemOperationTRI(); break;

                // TPI
                case 5: dop = new TGIS_DemOperationTPI(); break;

                // Roughness
                case 6: dop = new TGIS_DemOperationRoughness(); break;

                // TotalCurvature
                case 7:
                    switch (cbCurvMode.SelectedIndex)
                    {
                        case 0: cm = TGIS_DemTotalCurvatureMode.Profile; break;
                        case 1: cm = TGIS_DemTotalCurvatureMode.Plan; break;
                        default: cm = TGIS_DemTotalCurvatureMode.Profile; break;
                    }
                    dop = new TGIS_DemOperationTotalCurvature(cm); break;

                // MatrixGain
                case 8: dop = new TGIS_DemOperationMatrixGain(); break;

                // Flow dir
                case 9: dop = new TGIS_DemOperationFlowDir(); break;

                // default
                default: dop = new TGIS_DemOperation(); break;
            }

            ld.Name = "out_" + dop.Description();

            if (GIS.Get(ld.Name) != null)
                GIS.Delete(ld.Name);

            ld.Params.Pixel.GridShadow = false;
            GIS.Add(ld);

            dem.Process(lp, lp.Extent, ld, dop, GIS_BusyEvent);

            GIS.InvalidateWholeMap();

        }

        private void GIS_BusyEvent(object _sender, TGIS_BusyEventArgs _e)
        {
            if (_e.EndPos <= 0)
                pbProgress.Visible = false;
            else
            {
                pbProgress.Visible = true;
                pbProgress.Value = (int)(_e.Pos / _e.EndPos * 100);
            }
            pbProgress.Update();

            Application.DoEvents();
        }
    }
}
