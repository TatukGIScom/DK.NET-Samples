using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using TatukGIS.NDK;
using TatukGIS.NDK.OpenCL;

namespace OCL_Viewshed
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.lblInfo = new System.Windows.Forms.Label();
            this.chkbxUseOpenCL = new System.Windows.Forms.CheckBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnOpenCLInfo = new System.Windows.Forms.Button();
            this.btnDeviceInfo = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(600, 13);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Click on the map and drag for real-time viewshed.";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkbxUseOpenCL
            // 
            this.chkbxUseOpenCL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkbxUseOpenCL.AutoSize = true;
            this.chkbxUseOpenCL.Checked = true;
            this.chkbxUseOpenCL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbxUseOpenCL.Location = new System.Drawing.Point(12, 410);
            this.chkbxUseOpenCL.Name = "chkbxUseOpenCL";
            this.chkbxUseOpenCL.Size = new System.Drawing.Size(87, 17);
            this.chkbxUseOpenCL.TabIndex = 2;
            this.chkbxUseOpenCL.Text = "Use OpenCL";
            this.chkbxUseOpenCL.UseVisualStyleBackColor = true;
            this.chkbxUseOpenCL.CheckedChanged += new System.EventHandler(this.chkbxUseOpenCL_CheckedChanged);
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.Location = new System.Drawing.Point(114, 411);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(166, 13);
            this.lblTime.TabIndex = 3;
            // 
            // btnOpenCLInfo
            // 
            this.btnOpenCLInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenCLInfo.Location = new System.Drawing.Point(286, 406);
            this.btnOpenCLInfo.Name = "btnOpenCLInfo";
            this.btnOpenCLInfo.Size = new System.Drawing.Size(160, 23);
            this.btnOpenCLInfo.TabIndex = 4;
            this.btnOpenCLInfo.Text = "Show OpenCL info";
            this.btnOpenCLInfo.UseVisualStyleBackColor = true;
            this.btnOpenCLInfo.Click += new System.EventHandler(this.btnOpenCLInfo_Click);
            // 
            // btnDeviceInfo
            // 
            this.btnDeviceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeviceInfo.Location = new System.Drawing.Point(452, 406);
            this.btnDeviceInfo.Name = "btnDeviceInfo";
            this.btnDeviceInfo.Size = new System.Drawing.Size(160, 23);
            this.btnDeviceInfo.TabIndex = 5;
            this.btnDeviceInfo.Text = "Show active device info";
            this.btnDeviceInfo.UseVisualStyleBackColor = true;
            this.btnDeviceInfo.Click += new System.EventHandler(this.btnDeviceInfo_Click);
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Location = new System.Drawing.Point(12, 25);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.UserDefined;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(600, 375);
            this.GIS.TabIndex = 0;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            this.GIS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseUp);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.btnOpenCLInfo);
            this.Controls.Add(this.btnDeviceInfo);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.chkbxUseOpenCL);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.GIS);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Viewshed on OpenCL";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.CheckBox chkbxUseOpenCL;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnOpenCLInfo;
        private System.Windows.Forms.Button btnDeviceInfo;    
    
        private TGIS_Viewshed cl_viewshed;
        private TGIS_LayerPixel cl_dem;
        private TGIS_LayerVector cl_observers;
        private TGIS_LayerPixel cl_output;
        private DateTime cl_time;
        private TimeSpan cl_span;
        private bool cl_proc;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!TGIS_Utils.GisOpenCLEngine().Available)
            {
                MessageBox.Show("OpenCL is not available. Falling back to CPU...");
                chkbxUseOpenCL.Checked = false;
                chkbxUseOpenCL.Enabled = false;
                btnOpenCLInfo.Enabled = false;
                btnDeviceInfo.Enabled = false;
            }
            else
                TGIS_Utils.GisOpenCLEngine().Enabled = true;

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + 
                @"\World\Countries\USA\States\California\San Bernardino\NED\w001001.adf");

            cl_proc = false;
        }

        private void clearOutput()
        {
            TGIS_LayerPixelLock lpl;

            lpl = cl_output.LockPixels( cl_output.Extent, cl_output.CS, true );
            try
            {
                for (int h = lpl.Bounds.Top; h <= lpl.Bounds.Bottom; h++)
                {
                    for (int w = lpl.Bounds.Left; w <= lpl.Bounds.Right; w++)
                    {
                        lpl.Grid[h][w] = 0.0f;
                    }
                }
            }
            finally
            {
                cl_output.UnlockPixels(ref lpl);
            }
        }

        private void GIS_MouseDown(object sender, MouseEventArgs e)
        {
            if (GIS.Mode != TGIS_ViewerMode.UserDefined) return;

            Point pt = new Point(e.X, e.Y);
            TGIS_Point ptg = GIS.ScreenToMap(pt);

            cl_dem = (TGIS_LayerPixel)GIS.Items[0];

            if (!TGIS_Utils.GisIsPointInsideExtent(ptg, cl_dem.Extent)) return;

            cl_observers = new TGIS_LayerVector();
            cl_observers.Name = "observers";
            cl_observers.CS = cl_dem.CS;
            cl_observers.Open();

            TGIS_Shape shp = cl_observers.CreateShape(TGIS_ShapeType.Point);
            shp.AddPart();
            shp.AddPoint(ptg);

            cl_output = new TGIS_LayerPixel();
            cl_output.Name = "viewshed";
            cl_output.Build(true, cl_dem.CS, cl_dem.Extent, cl_dem.BitWidth, cl_dem.BitHeight);
            cl_output.Params.Pixel.GridShadow = false;
            cl_output.Params.Pixel.AltitudeMapZones.Add("0,0," +
              TGIS_Utils.ConstructParamColor(TGIS_Color.FromARGB(255, 0, 0, 0)) + ",0");
            cl_output.Params.Pixel.AltitudeMapZones.Add("1,1," +
              TGIS_Utils.ConstructParamColor(TGIS_Color.FromARGB(0, 0, 255, 0)) + ",1");

            cl_viewshed = new TGIS_Viewshed();
            cl_viewshed.Radius = 20000; // 20km radius
            cl_viewshed.CurvedEarth = true;
            cl_viewshed.ObserverElevation = TGIS_ViewshedObserverElevation.OnDem;
            cl_viewshed.ViewshedOutput = TGIS_ViewshedOutput.Visibility;
            cl_viewshed.FillWithZeros = true;

            cl_viewshed.TerrainLayer = cl_dem;
            cl_viewshed.ObserversLayer = cl_observers;
            cl_viewshed.OutputLayer = cl_output;
            cl_viewshed.TerrainOffset = 0.0f;
            cl_viewshed.ObserversOffsetField = "";
            cl_viewshed.ObserversOffset = 30.0f; // 30m above ground

            cl_viewshed.Generate();

            cl_output.Transparency = 80;
            cl_output.CachedPaint = false;
            cl_observers.CachedPaint = false;
            GIS.Add(cl_output);
            GIS.Add(cl_observers);

            GIS.InvalidateTopmost();
            cl_time = DateTime.Now;

            cl_proc = true;
        }

        private void GIS_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (GIS.Mode != TGIS_ViewerMode.UserDefined) return;

            if (!cl_proc) return;

            if (cl_output == null) return;

            cl_span = DateTime.Now - cl_time;
            // limit the number of frames to 25 fps
            if (cl_span.TotalMilliseconds < 40) return;

            Point pt = new Point(e.X, e.Y);
            TGIS_Point ptg = GIS.ScreenToMap(pt);
            if (!TGIS_Utils.GisIsPointInsideExtent(ptg, cl_dem.Extent)) return;
            
            cl_observers.RevertAll();

            TGIS_Shape shp = cl_observers.CreateShape(TGIS_ShapeType.Point);
            shp.AddPart();
            shp.AddPoint(ptg);

            if (!TGIS_Utils.GisOpenCLEngine().Enabled)
                clearOutput();

            DateTime dt = DateTime.Now;
            cl_viewshed.Generate();
            TimeSpan ts = DateTime.Now - dt;
            lblTime.Text = "Generation time: " + ts.TotalMilliseconds.ToString("F0") + " ms";

            GIS.InvalidateTopmost();
            cl_time = DateTime.Now;
        }

        private void GIS_MouseUp(object sender, MouseEventArgs e)
        {
            if (GIS.Mode != TGIS_ViewerMode.UserDefined) return;

            if (!cl_proc) return;

            GIS.Delete(cl_observers.Name);
            GIS.Delete(cl_output.Name);
            GIS.InvalidateWholeMap();

            lblTime.Text = "";

            cl_proc = false;
        }

        private void chkbxUseOpenCL_CheckedChanged(object sender, EventArgs e)
        {
            TGIS_Utils.GisOpenCLEngine().Enabled = chkbxUseOpenCL.Checked;
        }

        private void btnOpenCLInfo_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            TGIS_OpenCLPlatformInfo pi;
            TGIS_OpenCLDeviceInfo di;

            for (int i = 0; i < TGIS_Utils.GisOpenCLEngine().PlatformCount; i++)
            {
                pi = TGIS_Utils.GisOpenCLEngine().get_Platforms(i);
                sb.AppendLine("Platform " + i.ToString());
                sb.AppendLine("    Name: " + pi.Name);
                sb.AppendLine("    Vendor: " + pi.Vendor);
                sb.AppendLine("    Version: " + pi.Version);
                sb.AppendLine("    Profile: " + pi.Profile);
                sb.AppendLine("    Number of devices: " + pi.DeviceCount.ToString());
                for (int k = 0; k < pi.DeviceCount; k++)
                {
                    di = pi.get_Devices(k);
                    sb.AppendLine("    Device " + k.ToString());
                    if (di.Available)
                        sb.AppendLine("        Available: True");
                    else
                        sb.AppendLine("        Available: False");
                    if (di.DeviceType == TGIS_OpenCLDeviceType.CPU)
                        sb.AppendLine("        Type: CPU");
                    else if (di.DeviceType == TGIS_OpenCLDeviceType.GPU)
                        sb.AppendLine("        Type: GPU");
                    else
                        sb.AppendLine("        Type: accelerator");
                    sb.AppendLine("        Name: " + di.Name);
                    sb.AppendLine("        Vendor: " + di.Vendor);
                    sb.AppendLine("        Version: " + di.Version);
                    sb.AppendLine("        Profile: " + di.Profile);
                    sb.AppendLine("        OpenCL C version: " + di.OpenCLCVersion);
                    sb.AppendLine("        Driver version: " + di.DriverVersion);
                    sb.AppendLine("        Maximum work group size: " + di.WorkGroupSize.ToString());
                    sb.AppendLine("        Clock frequency: " + di.ClockFrequency.ToString() + " MHz");
                    sb.AppendLine("        Maximum memory allocation size: " + di.MemoryAllocationSize.ToString() + " bytes");
                    sb.AppendLine("        Number of compute units: " + di.ComputeUnits.ToString());
                    sb.AppendLine("        Extensions:");
                    for (int l = 0; l < di.ExtensionCount; l++)
                        sb.AppendLine("            " + di.get_Extensions(l));
                }
            }

            frmInfo frm = new frmInfo();
            frm.Execute(this, "OpenCL info", sb.ToString());
        }

        private void btnDeviceInfo_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            TGIS_OpenCLDeviceInfo di = TGIS_Utils.GisOpenCLEngine().ActiveDevice;

            if (di.DeviceType == TGIS_OpenCLDeviceType.CPU)
                sb.AppendLine("Type: CPU");
            else if (di.DeviceType == TGIS_OpenCLDeviceType.GPU)
                sb.AppendLine("Type: GPU");
            else
                sb.AppendLine("Type: accelerator");
            sb.AppendLine("Name: " + di.Name);
            sb.AppendLine("Vendor: " + di.Vendor);
            sb.AppendLine("Version: " + di.Version);
            sb.AppendLine("Profile: " + di.Profile);
            sb.AppendLine("OpenCL C version: " + di.OpenCLCVersion);
            sb.AppendLine("Driver version: " + di.DriverVersion);
            sb.AppendLine("Maximum work group size: " + di.WorkGroupSize.ToString());
            sb.AppendLine("Clock frequency: " + di.ClockFrequency.ToString() + " MHz");
            sb.AppendLine("Maximum memory allocation size: " + di.MemoryAllocationSize.ToString() + " bytes");
            sb.AppendLine("Number of compute units: " + di.ComputeUnits.ToString());
            sb.AppendLine("Extensions:");
            for (int l = 0; l < di.ExtensionCount; l++)
                sb.AppendLine("    " + di.get_Extensions(l));

            frmInfo frm = new frmInfo();
            frm.Execute(this, "Active device info", sb.ToString());
        }
    }
}
