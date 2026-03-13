using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace SQLLayerAdvanced
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private TatukGIS.NDK.WinForms.TGIS_ControlLegend GIS_Legend;
        private Panel panel1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private RichTextBox memo;
        private Button btnLog;
        private Button btnAttach;
        private Button btnBuild;
        private GroupBox groupBox1;
        private Button btnOpenStyles;
        private ComboBox cbStyles;
        private Button btnGetStyles;
        private Button btnOpenProjects;
        private ComboBox cbProjects;
        private Button btnGetProjects;
        private Button btnWriteConfigStyle;
        private Button btnWriteConfigProject;
        private Button btnImport;
        private Button btnOpen;
        private ImageList ilIcons;
        private Button btnZoom;
        private Button btnDrag;
        private Button btnFullExtent;
        private String currDir;

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
            TatukGIS.NDK.TGIS_ControlLegendDialogOptions tgiS_ControlLegendDialogOptions3 = new TatukGIS.NDK.TGIS_ControlLegendDialogOptions();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenStyles = new System.Windows.Forms.Button();
            this.cbStyles = new System.Windows.Forms.ComboBox();
            this.btnGetStyles = new System.Windows.Forms.Button();
            this.btnOpenProjects = new System.Windows.Forms.Button();
            this.cbProjects = new System.Windows.Forms.ComboBox();
            this.btnGetProjects = new System.Windows.Forms.Button();
            this.btnWriteConfigStyle = new System.Windows.Forms.Button();
            this.btnWriteConfigProject = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnAttach = new System.Windows.Forms.Button();
            this.btnBuild = new System.Windows.Forms.Button();
            this.memo = new System.Windows.Forms.RichTextBox();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnDrag = new System.Windows.Forms.Button();
            this.btnFullExtent = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_Legend = new TatukGIS.NDK.WinForms.TGIS_ControlLegend();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnLog);
            this.panel1.Controls.Add(this.btnAttach);
            this.panel1.Controls.Add(this.btnBuild);
            this.panel1.Location = new System.Drawing.Point(-1, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 456);
            this.panel1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenStyles);
            this.groupBox1.Controls.Add(this.cbStyles);
            this.groupBox1.Controls.Add(this.btnGetStyles);
            this.groupBox1.Controls.Add(this.btnOpenProjects);
            this.groupBox1.Controls.Add(this.cbProjects);
            this.groupBox1.Controls.Add(this.btnGetProjects);
            this.groupBox1.Controls.Add(this.btnWriteConfigStyle);
            this.groupBox1.Controls.Add(this.btnWriteConfigProject);
            this.groupBox1.Controls.Add(this.btnImport);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Location = new System.Drawing.Point(4, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(209, 346);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Embedded projects";
            // 
            // btnOpenStyles
            // 
            this.btnOpenStyles.Location = new System.Drawing.Point(133, 255);
            this.btnOpenStyles.Name = "btnOpenStyles";
            this.btnOpenStyles.Size = new System.Drawing.Size(70, 23);
            this.btnOpenStyles.TabIndex = 7;
            this.btnOpenStyles.Text = "Apply";
            this.btnOpenStyles.UseVisualStyleBackColor = true;
            this.btnOpenStyles.Click += new System.EventHandler(this.btnOpenStyles_Click);
            // 
            // cbStyles
            // 
            this.cbStyles.FormattingEnabled = true;
            this.cbStyles.Location = new System.Drawing.Point(6, 255);
            this.cbStyles.Name = "cbStyles";
            this.cbStyles.Size = new System.Drawing.Size(121, 21);
            this.cbStyles.TabIndex = 8;
            // 
            // btnGetStyles
            // 
            this.btnGetStyles.Location = new System.Drawing.Point(6, 226);
            this.btnGetStyles.Name = "btnGetStyles";
            this.btnGetStyles.Size = new System.Drawing.Size(197, 23);
            this.btnGetStyles.TabIndex = 6;
            this.btnGetStyles.Text = "6. Get available styles";
            this.btnGetStyles.UseVisualStyleBackColor = true;
            this.btnGetStyles.Click += new System.EventHandler(this.btnGetStyles_Click);
            // 
            // btnOpenProjects
            // 
            this.btnOpenProjects.Location = new System.Drawing.Point(133, 180);
            this.btnOpenProjects.Name = "btnOpenProjects";
            this.btnOpenProjects.Size = new System.Drawing.Size(70, 23);
            this.btnOpenProjects.TabIndex = 4;
            this.btnOpenProjects.Text = "Open";
            this.btnOpenProjects.UseVisualStyleBackColor = true;
            this.btnOpenProjects.Click += new System.EventHandler(this.btnOpenProjects_Click);
            // 
            // cbProjects
            // 
            this.cbProjects.FormattingEnabled = true;
            this.cbProjects.Location = new System.Drawing.Point(6, 180);
            this.cbProjects.Name = "cbProjects";
            this.cbProjects.Size = new System.Drawing.Size(121, 21);
            this.cbProjects.TabIndex = 5;
            // 
            // btnGetProjects
            // 
            this.btnGetProjects.Location = new System.Drawing.Point(6, 151);
            this.btnGetProjects.Name = "btnGetProjects";
            this.btnGetProjects.Size = new System.Drawing.Size(197, 23);
            this.btnGetProjects.TabIndex = 4;
            this.btnGetProjects.Text = "5. Get available projects";
            this.btnGetProjects.UseVisualStyleBackColor = true;
            this.btnGetProjects.Click += new System.EventHandler(this.btnGetProjects_Click);
            // 
            // btnWriteConfigStyle
            // 
            this.btnWriteConfigStyle.Location = new System.Drawing.Point(6, 106);
            this.btnWriteConfigStyle.Name = "btnWriteConfigStyle";
            this.btnWriteConfigStyle.Size = new System.Drawing.Size(197, 23);
            this.btnWriteConfigStyle.TabIndex = 3;
            this.btnWriteConfigStyle.Text = "4. Write styles configuration";
            this.btnWriteConfigStyle.UseVisualStyleBackColor = true;
            this.btnWriteConfigStyle.Click += new System.EventHandler(this.btnWriteConfigStyle_Click);
            // 
            // btnWriteConfigProject
            // 
            this.btnWriteConfigProject.Location = new System.Drawing.Point(6, 77);
            this.btnWriteConfigProject.Name = "btnWriteConfigProject";
            this.btnWriteConfigProject.Size = new System.Drawing.Size(197, 23);
            this.btnWriteConfigProject.TabIndex = 2;
            this.btnWriteConfigProject.Text = "3. Write project configuration";
            this.btnWriteConfigProject.UseVisualStyleBackColor = true;
            this.btnWriteConfigProject.Click += new System.EventHandler(this.btnWriteConfigProject_Click);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(6, 48);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(197, 23);
            this.btnImport.TabIndex = 1;
            this.btnImport.Text = "2. Import layers";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(6, 19);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(197, 23);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "1. Open sample project";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(14, 67);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(114, 23);
            this.btnLog.TabIndex = 2;
            this.btnLog.Text = "Log changes";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnAttach
            // 
            this.btnAttach.Location = new System.Drawing.Point(14, 38);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(114, 23);
            this.btnAttach.TabIndex = 1;
            this.btnAttach.Text = "Attach trace event";
            this.btnAttach.UseVisualStyleBackColor = true;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(14, 9);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(114, 23);
            this.btnBuild.TabIndex = 0;
            this.btnBuild.Text = "Build empty layer";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // memo
            // 
            this.memo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memo.Location = new System.Drawing.Point(-1, 476);
            this.memo.Name = "memo";
            this.memo.ReadOnly = true;
            this.memo.Size = new System.Drawing.Size(879, 120);
            this.memo.TabIndex = 6;
            this.memo.Text = "";
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Fuchsia;
            this.ilIcons.Images.SetKeyName(0, "Drag.bmp");
            this.ilIcons.Images.SetKeyName(1, "FullExtent.bmp");
            this.ilIcons.Images.SetKeyName(2, "ZoomEx.bmp");
            // 
            // btnZoom
            // 
            this.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoom.ImageIndex = 2;
            this.btnZoom.ImageList = this.ilIcons;
            this.btnZoom.Location = new System.Drawing.Point(28, -1);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(30, 23);
            this.btnZoom.TabIndex = 7;
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnDrag
            // 
            this.btnDrag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrag.ImageIndex = 0;
            this.btnDrag.ImageList = this.ilIcons;
            this.btnDrag.Location = new System.Drawing.Point(57, -1);
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(30, 23);
            this.btnDrag.TabIndex = 8;
            this.btnDrag.UseVisualStyleBackColor = true;
            this.btnDrag.Click += new System.EventHandler(this.btnDrag_Click);
            // 
            // btnFullExtent
            // 
            this.btnFullExtent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFullExtent.ImageIndex = 1;
            this.btnFullExtent.ImageList = this.ilIcons;
            this.btnFullExtent.Location = new System.Drawing.Point(-1, -1);
            this.btnFullExtent.Name = "btnFullExtent";
            this.btnFullExtent.Size = new System.Drawing.Size(30, 23);
            this.btnFullExtent.TabIndex = 9;
            this.btnFullExtent.UseVisualStyleBackColor = true;
            this.btnFullExtent.Click += new System.EventHandler(this.btnFullExtent_Click);
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(217, 21);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(507, 456);
            this.GIS.TabIndex = 5;
            // 
            // GIS_Legend
            // 
            this.GIS_Legend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            tgiS_ControlLegendDialogOptions3.VectorWizardUniqueLimit = 256;
            tgiS_ControlLegendDialogOptions3.VectorWizardUniqueSearchLimit = 16384;
            this.GIS_Legend.DialogOptions = tgiS_ControlLegendDialogOptions3;
            this.GIS_Legend.GIS_Group = null;
            this.GIS_Legend.GIS_Layer = null;
            this.GIS_Legend.GIS_Viewer = this.GIS;
            this.GIS_Legend.Location = new System.Drawing.Point(724, 21);
            this.GIS_Legend.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers;
            this.GIS_Legend.Name = "GIS_Legend";
            this.GIS_Legend.Options = ((TatukGIS.NDK.TGIS_ControlLegendOption)(((((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove | TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect)
            | TatukGIS.NDK.TGIS_ControlLegendOption.ShowSubLayers)
            | TatukGIS.NDK.TGIS_ControlLegendOption.AllowParamsVisible)));
            this.GIS_Legend.ReverseOrder = false;
            this.GIS_Legend.Size = new System.Drawing.Size(154, 456);
            this.GIS_Legend.TabIndex = 0;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(875, 597);
            this.Controls.Add(this.btnFullExtent);
            this.Controls.Add(this.btnDrag);
            this.Controls.Add(this.btnZoom);
            this.Controls.Add(this.memo);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GIS_Legend);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - SQLLayerAdvanced";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
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
            currDir = AppDomain.CurrentDomain.BaseDirectory;
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            TGIS_LayerSqlSqlite lsql;

            lsql = new TGIS_LayerSqlSqlite();

            lsql.SetCSByEPSG(4326);
            lsql.Path = getGISTESTPath(false);
            lsql.Build(lsql.Path, TGIS_Utils.GisExtent(14, 49, 24, 55), TGIS_ShapeType.Point, TGIS_DimensionType.XY);

            GIS.Add(lsql);
            GIS.FullExtent();
            GIS.InvalidateWholeMap();
        }

        private String getGISTESTPath(Boolean _logging)
        {
            String str;
            if (_logging)
                str = "\nLogging=True\n";
            else
                str = "";

            return ("[TatukGIS Layer]\n" +
                    "Storage=Native\n" +
                    "LAYER=GISTEST\n" +
                    "DIALECT=SQLITE\n" +
                    "Sqlite=" + currDir + "gistest.sqlite\n" +
                    "ENGINEOPTIONS=16" + str + "\n.ttkls"
                   );
        }

        private void traceLog(String _str)
        {
            memo.AppendText(_str + Environment.NewLine);
        }

        private void btnAttach_Click(object sender, EventArgs e)
        {
            TGIS_LayerSqlSqlite lsql;

            GIS.Close();

            lsql = new TGIS_LayerSqlSqlite();
            lsql.Path = getGISTESTPath(false);
            lsql.SQLExecuteEvent += traceLog;

            GIS.Add(lsql);
            GIS.FullExtent();
            GIS.InvalidateWholeMap();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            TGIS_LayerSqlSqlite lsql;
            int i;
            TGIS_Shape shp;
            TStrings logs;
            Random rnd;

            GIS.Close();

            rnd = new Random();

            lsql = new TGIS_LayerSqlSqlite();
            lsql.Path = getGISTESTPath(true);
            lsql.SetCSByEPSG(4326);
            lsql.Build(lsql.Path, TGIS_Utils.GisExtent(14, 49, 24, 55), TGIS_ShapeType.Point, TGIS_DimensionType.XY);

            GIS.Add(lsql);

            for (i = 1; i <= 10; i++)
            {
                shp = lsql.CreateShape(TGIS_ShapeType.Point, TGIS_DimensionType.XY);
                shp.AddPart();
                shp.AddPoint(TGIS_Utils.GisPoint(14 + rnd.Next(0, 10), 49 + rnd.Next(0, 10)));
            }
            GIS.SaveData();
            GIS.FullExtent();
            GIS.InvalidateWholeMap();

            logs = lsql.GetLogs();
            memo.AppendText(logs.Text);
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\poland.ttkproject");
            GIS.InvalidateWholeMap();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            int i;
            TGIS_LayerVector lv;
            TGIS_LayerSqlSqlite lsql;

            if (GIS.IsEmpty) return;

            for (i = 0; i < GIS.Items.Count - 1; i++)
            {
                lv = (TGIS_LayerVector)GIS.Items[i];

                lsql = new TGIS_LayerSqlSqlite();
                lsql.Path = "[TatukGIS Layer]\n" +
                            "Storage=Native\n" +
                            "LAYER=" + TGIS_Utils.GisCanonicalSQLName(lv.Name) + "\n" +
                            "DIALECT=SQLITE\n" +
                            "Sqlite=" + currDir + "gistest.sqlite\n" +
                            "ENGINEOPTIONS=16\n.ttkls";

                lsql.SetCSByEPSG(lv.CS.EPSG);
                lsql.ImportLayer(lv, lv.Extent, TGIS_ShapeType.Unknown, "", false);
            }
        }

        private void btnWriteConfigProject_Click(object sender, EventArgs e)
        {
            TGIS_LayerVectorSqlAbstract lv;
            TStrings lst;
            int i;
            TGIS_Layer ll;
            TGIS_Config cfg;

            if (GIS.IsEmpty) return;

            lst = new TStrings();
            for (i = 0; i < GIS.Items.Count - 1; i++)
            {
                ll = (TGIS_Layer)GIS.Items[i];
                ll.Path = "[TatukGIS Layer]\\n" +
                          "Storage=Native\\n" +
                          "Sqlite=<#PATH#>gistest.sqlite\\n" +
                          "Dialect=SQLITE\\n" +
                          "Layer=" + TGIS_Utils.GisCanonicalSQLName(ll.Name) + "\\n" +
                          "Style=" + TGIS_Utils.GisCanonicalSQLName(ll.Name) + "\\n.ttkls";
            }
            cfg = TGIS_ConfigFactory.CreateConfig(null, "test.ttkproject");

            GIS.SaveProjectAsEx(cfg, "");
            cfg.GetStrings(lst);

            lv = new TGIS_LayerSqlSqlite();
            lv.Path = getGISTESTPath(false);
            GIS.Add(lv);
            lv.CreateProjectTable();
            lv.WriteProject("POLAND", "Map of Poland", lst.Text);
        }

        private void btnWriteConfigStyle_Click(object sender, EventArgs e)
        {
            TGIS_LayerVectorSqlAbstract lv;
            TStrings lst;
            TGIS_Layer ll;

            if (GIS.IsEmpty) return;

            lv = (TGIS_LayerVectorSqlAbstract)GIS.Get("GISTEST");
            lv.CreateStyleTable();

            lst = new TStrings();

            foreach (TGIS_LayerAbstract la in GIS.Items)
            {
                ll = (TGIS_Layer)la;
                ((TGIS_Config)GIS.ProjectFile).SetLayer(ll);
                lst.Clear();
                ll.ParamsList.SaveToStrings(lst);
                lv.WriteStyle(TGIS_Utils.GisCanonicalSQLName(ll.Name), ll.Caption, lst.Text);
            }
        }

        private void btnGetProjects_Click(object sender, EventArgs e)
        {
            TGIS_LayerVectorSqlAbstract lv;
            TStrings lst;
            int i;

            lv = (TGIS_LayerVectorSqlAbstract)GIS.Get("GISTEST");
            if (lv == null)
            {
                lv = new TGIS_LayerSqlSqlite();
                lv.Path = getGISTESTPath(false);
                GIS.Add(lv);
            }
            lst = lv.GetAvailableProjects();
            for (i = 0; i < lst.Count; i++)
            {
                cbProjects.Items.Add(lst[i]);
            }
        }

        private void btnGetStyles_Click(object sender, EventArgs e)
        {
            TGIS_LayerVectorSqlAbstract lv;
            TStrings lst;
            int i;

            lv = (TGIS_LayerVectorSqlAbstract)GIS.Get("GISTEST");

            if (lv == null)
            {
                lv = new TGIS_LayerSqlSqlite();
                lv.Path = getGISTESTPath(false);
                GIS.Add(lv);
            }

            lst = lv.GetAvailableStyles();
            for (i = 0; i < lst.Count; i++)
            {
                cbStyles.Items.Add(lst[i]);
            }
        }

        private void btnOpenProjects_Click(object sender, EventArgs e)
        {
            TGIS_LayerVectorSqlAbstract lv;
            TStringList tkn;
            String name;

            lv = (TGIS_LayerVectorSqlAbstract)GIS.Get("GISTEST");

            if (lv == null) return;

            tkn = new TStringList();
            tkn.Add("PATH=" + TGIS_Utils.ConstructParamString(currDir));
            name = cbProjects.SelectedItem.ToString();
            if (name == "")
                name = "POLAND";
            GIS.OpenEx(lv.GetProject(name + ".ttkproject", tkn), ".ttkproject");
            GIS.InvalidateWholeMap();
        }

        private void btnOpenStyles_Click(object sender, EventArgs e)
        {
            TGIS_LayerVectorSqlAbstract lv;

            if (GIS.IsEmpty) return;

            lv = (TGIS_LayerVectorSqlAbstract)GIS.Get("GISTEST");

            if (lv == null) return;

            lv.ApplyStyle(lv.ReadStyle(cbStyles.SelectedItem.ToString()));
            GIS.InvalidateWholeMap();
        }
    }
}
