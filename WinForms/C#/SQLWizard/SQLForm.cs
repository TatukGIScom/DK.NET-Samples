using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;
using ADODB;
using MSDASC;
namespace SQLWizard
{
    /// <summary>
    /// Summary description for SQLForm.
    /// </summary>
    public class SQLForm : System.Windows.Forms.Form
    {
        //consts
        const int LAYERSQL_NATIVE = 0;
        const int LAYERSQL_OPENGISBLOB = 1;
        const int LAYERSQL_OPENGISBLOB2 = 2;
        const int LAYERSQL_GEOMEDIA = 3;
        const int LAYERSQL_POSTGIS = 4;
        const int LAYERSQL_PERSONALGDB = 5;
        const int LAYERSQL_SDEBINARY = 6;
        const int LAYERSQL_PIXELSTORE2 = 7;
        const int LAYERSQL_KATMAI = 8;
        const int LAYERSQL_ORACLESPATIAL = 9;
        const int LAYERSQL_SDERASTER = 10;
        const int LAYERSQL_ORACLEGEORASTER = 11;
        const int LAYERSQL_SPATIALWARE = 12;
        const int LAYERSQL_DB2GSE = 13;
        const int LAYERSQL_IFXSDB = 14;
        const int LAYERSQL_FGDB = 15;
        const int LAYERSQL_ORACLESPATIALPC = 16;
        const int LAYERSQL_ORACLESPATIALTIN = 17;
        const int LAYERSQL_GEOMEDIA_MSSQL = 18;
        const int LAYERSQL_GEOMEDIA_SDO = 19;
        const int LAYERSQL_ANYWHERE_SPATIAL = 20;
        const int LAYERSQL_OGR = 21;

        // var
        private GroupBox gbParams;
        private Button btnClose;
        private Button btnHelp;
        private RadioButton rbOGR;
        private RadioButton rbGDB;
        private RadioButton rbSQLite;
        private RadioButton rbADO;
        private ComboBox cbxConnString;
        private Button btnConnect;
        private ComboBox cbxStorage;
        private ComboBox cbxDialect;
        private Button btnPath;
        private Button btnBuild;
        private GroupBox gbLayers;
        private TreeView tvLayers;
        private RichTextBox rtbLayerParams;
        private Button btnAddLayer;
        private Button btnCreateConf;
        private OpenFileDialog dlgOpen;
        private SaveFileDialog dlgSave;
        private Label lvAdditionalLayerParams;
        private Label lbDialect;
        private Label lbStorage;
        private Label lbConnStr;
        private Label lbPath;
        private TGIS_ViewerWnd GIS;
        private ImageList imgList;
        private TStringList lst = new TStringList();
        private FolderBrowserDialog dlgOpenFolder;

        public TGIS_ViewerWnd getGIS()
        {
            return GIS;
        }

        public void setGIS(TGIS_ViewerWnd _gis)
        {
            GIS = _gis;
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        public SQLForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SQLForm));
            this.gbParams = new System.Windows.Forms.GroupBox();
            this.lbPath = new System.Windows.Forms.Label();
            this.lbConnStr = new System.Windows.Forms.Label();
            this.lbStorage = new System.Windows.Forms.Label();
            this.lbDialect = new System.Windows.Forms.Label();
            this.btnBuild = new System.Windows.Forms.Button();
            this.cbxConnString = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cbxStorage = new System.Windows.Forms.ComboBox();
            this.cbxDialect = new System.Windows.Forms.ComboBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.rbOGR = new System.Windows.Forms.RadioButton();
            this.rbGDB = new System.Windows.Forms.RadioButton();
            this.rbSQLite = new System.Windows.Forms.RadioButton();
            this.rbADO = new System.Windows.Forms.RadioButton();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.gbLayers = new System.Windows.Forms.GroupBox();
            this.lvAdditionalLayerParams = new System.Windows.Forms.Label();
            this.rtbLayerParams = new System.Windows.Forms.RichTextBox();
            this.tvLayers = new System.Windows.Forms.TreeView();
            this.btnAddLayer = new System.Windows.Forms.Button();
            this.btnCreateConf = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.dlgOpenFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.gbParams.SuspendLayout();
            this.gbLayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbParams
            // 
            this.gbParams.Controls.Add(this.lbPath);
            this.gbParams.Controls.Add(this.lbConnStr);
            this.gbParams.Controls.Add(this.lbStorage);
            this.gbParams.Controls.Add(this.lbDialect);
            this.gbParams.Controls.Add(this.btnBuild);
            this.gbParams.Controls.Add(this.cbxConnString);
            this.gbParams.Controls.Add(this.btnConnect);
            this.gbParams.Controls.Add(this.cbxStorage);
            this.gbParams.Controls.Add(this.cbxDialect);
            this.gbParams.Controls.Add(this.btnPath);
            this.gbParams.Controls.Add(this.rbOGR);
            this.gbParams.Controls.Add(this.rbGDB);
            this.gbParams.Controls.Add(this.rbSQLite);
            this.gbParams.Controls.Add(this.rbADO);
            this.gbParams.Location = new System.Drawing.Point(3, 7);
            this.gbParams.Name = "gbParams";
            this.gbParams.Size = new System.Drawing.Size(439, 152);
            this.gbParams.TabIndex = 0;
            this.gbParams.TabStop = false;
            this.gbParams.Text = "Parameters";
            // 
            // lbPath
            // 
            this.lbPath.AutoSize = true;
            this.lbPath.Location = new System.Drawing.Point(12, 50);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(32, 13);
            this.lbPath.TabIndex = 14;
            this.lbPath.Text = "Path:";
            // 
            // lbConnStr
            // 
            this.lbConnStr.AutoSize = true;
            this.lbConnStr.Location = new System.Drawing.Point(12, 50);
            this.lbConnStr.Name = "lbConnStr";
            this.lbConnStr.Size = new System.Drawing.Size(89, 13);
            this.lbConnStr.TabIndex = 13;
            this.lbConnStr.Text = "Connection string";
            // 
            // lbStorage
            // 
            this.lbStorage.AutoSize = true;
            this.lbStorage.Location = new System.Drawing.Point(139, 97);
            this.lbStorage.Name = "lbStorage";
            this.lbStorage.Size = new System.Drawing.Size(44, 13);
            this.lbStorage.TabIndex = 12;
            this.lbStorage.Text = "Storage";
            // 
            // lbDialect
            // 
            this.lbDialect.AutoSize = true;
            this.lbDialect.Location = new System.Drawing.Point(11, 97);
            this.lbDialect.Name = "lbDialect";
            this.lbDialect.Size = new System.Drawing.Size(43, 13);
            this.lbDialect.TabIndex = 11;
            this.lbDialect.Text = "Dialect:";
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(352, 67);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(75, 23);
            this.btnBuild.TabIndex = 10;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // cbxConnString
            // 
            this.cbxConnString.FormattingEnabled = true;
            this.cbxConnString.Location = new System.Drawing.Point(12, 69);
            this.cbxConnString.Name = "cbxConnString";
            this.cbxConnString.Size = new System.Drawing.Size(334, 21);
            this.cbxConnString.TabIndex = 9;
            this.cbxConnString.SelectedIndexChanged += new System.EventHandler(this.cbxConnString_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(352, 112);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cbxStorage
            // 
            this.cbxStorage.FormattingEnabled = true;
            this.cbxStorage.Location = new System.Drawing.Point(139, 114);
            this.cbxStorage.Name = "cbxStorage";
            this.cbxStorage.Size = new System.Drawing.Size(207, 21);
            this.cbxStorage.TabIndex = 7;
            // 
            // cbxDialect
            // 
            this.cbxDialect.FormattingEnabled = true;
            this.cbxDialect.Location = new System.Drawing.Point(12, 114);
            this.cbxDialect.Name = "cbxDialect";
            this.cbxDialect.Size = new System.Drawing.Size(121, 21);
            this.cbxDialect.TabIndex = 6;
            this.cbxDialect.SelectedIndexChanged += new System.EventHandler(this.cbxDialect_SelectedIndexChanged);
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(352, 67);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(75, 23);
            this.btnPath.TabIndex = 5;
            this.btnPath.Text = "...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // rbOGR
            // 
            this.rbOGR.AutoSize = true;
            this.rbOGR.Location = new System.Drawing.Point(378, 16);
            this.rbOGR.Name = "rbOGR";
            this.rbOGR.Size = new System.Drawing.Size(49, 17);
            this.rbOGR.TabIndex = 3;
            this.rbOGR.Text = "OGR";
            this.rbOGR.UseVisualStyleBackColor = true;
            this.rbOGR.CheckedChanged += new System.EventHandler(this.rbOGR_CheckedChanged);
            // 
            // rbGDB
            // 
            this.rbGDB.AutoSize = true;
            this.rbGDB.Location = new System.Drawing.Point(243, 16);
            this.rbGDB.Name = "rbGDB";
            this.rbGDB.Size = new System.Drawing.Size(64, 17);
            this.rbGDB.TabIndex = 2;
            this.rbGDB.Text = "FileGDB";
            this.rbGDB.UseVisualStyleBackColor = true;
            this.rbGDB.CheckedChanged += new System.EventHandler(this.rbGDB_CheckedChanged);
            // 
            // rbSQLite
            // 
            this.rbSQLite.AutoSize = true;
            this.rbSQLite.Location = new System.Drawing.Point(126, 16);
            this.rbSQLite.Name = "rbSQLite";
            this.rbSQLite.Size = new System.Drawing.Size(57, 17);
            this.rbSQLite.TabIndex = 1;
            this.rbSQLite.Text = "SQLite";
            this.rbSQLite.UseVisualStyleBackColor = true;
            this.rbSQLite.CheckedChanged += new System.EventHandler(this.rbSQLite_CheckedChanged);
            // 
            // rbADO
            // 
            this.rbADO.Location = new System.Drawing.Point(12, 16);
            this.rbADO.Name = "rbADO";
            this.rbADO.Size = new System.Drawing.Size(104, 24);
            this.rbADO.TabIndex = 0;
            this.rbADO.Text = "ADO";
            this.rbADO.UseVisualStyleBackColor = true;
            this.rbADO.CheckedChanged += new System.EventHandler(this.rbADO_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(448, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(101, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(448, 41);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(101, 23);
            this.btnHelp.TabIndex = 2;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            // 
            // gbLayers
            // 
            this.gbLayers.Controls.Add(this.lvAdditionalLayerParams);
            this.gbLayers.Controls.Add(this.rtbLayerParams);
            this.gbLayers.Controls.Add(this.tvLayers);
            this.gbLayers.Location = new System.Drawing.Point(3, 165);
            this.gbLayers.Name = "gbLayers";
            this.gbLayers.Size = new System.Drawing.Size(439, 388);
            this.gbLayers.TabIndex = 11;
            this.gbLayers.TabStop = false;
            this.gbLayers.Text = "Available layers";
            // 
            // lvAdditionalLayerParams
            // 
            this.lvAdditionalLayerParams.AutoSize = true;
            this.lvAdditionalLayerParams.Location = new System.Drawing.Point(6, 271);
            this.lvAdditionalLayerParams.Name = "lvAdditionalLayerParams";
            this.lvAdditionalLayerParams.Size = new System.Drawing.Size(136, 13);
            this.lvAdditionalLayerParams.TabIndex = 13;
            this.lvAdditionalLayerParams.Text = "Additional layer parameters:";
            // 
            // rtbLayerParams
            // 
            this.rtbLayerParams.Location = new System.Drawing.Point(3, 287);
            this.rtbLayerParams.Name = "rtbLayerParams";
            this.rtbLayerParams.Size = new System.Drawing.Size(430, 95);
            this.rtbLayerParams.TabIndex = 1;
            this.rtbLayerParams.Text = "";
            // 
            // tvLayers
            // 
            this.tvLayers.Location = new System.Drawing.Point(3, 16);
            this.tvLayers.Name = "tvLayers";
            this.tvLayers.Size = new System.Drawing.Size(430, 248);
            this.tvLayers.TabIndex = 0;
            this.tvLayers.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvLayers_AfterSelect);
            // 
            // btnAddLayer
            // 
            this.btnAddLayer.Location = new System.Drawing.Point(448, 181);
            this.btnAddLayer.Name = "btnAddLayer";
            this.btnAddLayer.Size = new System.Drawing.Size(101, 23);
            this.btnAddLayer.TabIndex = 2;
            this.btnAddLayer.Text = "Add layer";
            this.btnAddLayer.UseVisualStyleBackColor = true;
            this.btnAddLayer.Click += new System.EventHandler(this.btnAddLayer_Click);
            // 
            // btnCreateConf
            // 
            this.btnCreateConf.Location = new System.Drawing.Point(448, 210);
            this.btnCreateConf.Name = "btnCreateConf";
            this.btnCreateConf.Size = new System.Drawing.Size(101, 23);
            this.btnCreateConf.TabIndex = 12;
            this.btnCreateConf.Text = "Create config";
            this.btnCreateConf.UseVisualStyleBackColor = true;
            this.btnCreateConf.Click += new System.EventHandler(this.btnCreateConf_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            this.dlgOpen.Filter = "SQLLite|*.sql|All files|*.*";
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "TatukGIS SQL Layer|*.ttkls|TatukGIS PixelStore|**.ttkps";
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imgList.Images.SetKeyName(0, "ExtentLayer.png");
            this.imgList.Images.SetKeyName(1, "Point.png");
            this.imgList.Images.SetKeyName(2, "MultiPoint.png");
            this.imgList.Images.SetKeyName(3, "Line.png");
            this.imgList.Images.SetKeyName(4, "Polygon.png");
            this.imgList.Images.SetKeyName(5, "TatukGIS.png");
            this.imgList.Images.SetKeyName(6, "Unknown.bmp");
            // 
            // SQLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(561, 577);
            this.Controls.Add(this.btnCreateConf);
            this.Controls.Add(this.btnAddLayer);
            this.Controls.Add(this.gbLayers);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gbParams);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "SQLForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - SQLWizard";
            this.Load += new System.EventHandler(this.SQLForm_Load);
            this.gbParams.ResumeLayout(false);
            this.gbParams.PerformLayout();
            this.gbLayers.ResumeLayout(false);
            this.gbLayers.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Run()
        {
            Application.Run(new SQLForm());
        }

        private void SQLForm_Load(object sender, System.EventArgs e)
        {
            cbxDialect.Items.Clear();
            cbxDialect.Items.Add("ADVANTAGE");
            cbxDialect.Items.Add("BLACKFISHSQL");
            cbxDialect.Items.Add("DB2");
            cbxDialect.Items.Add("FILEGDB");
            cbxDialect.Items.Add("INFORMIX");
            cbxDialect.Items.Add("INTERBASE");
            cbxDialect.Items.Add("MSJET");
            cbxDialect.Items.Add("MSSQL");
            cbxDialect.Items.Add("MYSQL");
            cbxDialect.Items.Add("NEXUSDB");
            cbxDialect.Items.Add("ORACLE");
            cbxDialect.Items.Add("POSTGRESQL");
            cbxDialect.Items.Add("SAPDB");
            cbxDialect.Items.Add("SQLITE");
            cbxDialect.Items.Add("SYBASE");
            cbxDialect.Items.Add("INTERSYSTEMS");
            cbxDialect.Items.Add("OGR");

            cbxStorage.Items.Clear();
            cbxStorage.Items.Add("*");
            cbxStorage.Items.Add("AnywhereSpatial");
            cbxStorage.Items.Add("DB2SpatialExtender");
            cbxStorage.Items.Add("FileGDB");
            cbxStorage.Items.Add("Geomedia");
            cbxStorage.Items.Add("GeomediaMsSpatial");
            cbxStorage.Items.Add("GeomediaOracleSpatial");
            cbxStorage.Items.Add("IfxSpatialDataBlade");
            cbxStorage.Items.Add("Katmai");
            cbxStorage.Items.Add("Native");
            cbxStorage.Items.Add("OpenGisBlob");
            cbxStorage.Items.Add("OpenGisBlob2");
            cbxStorage.Items.Add("OracleGeoraster");
            cbxStorage.Items.Add("OracleSpatial");
            cbxStorage.Items.Add("OracleSpatialPc");
            cbxStorage.Items.Add("OracleSpatialTin");
            cbxStorage.Items.Add("PersonalGdb");
            cbxStorage.Items.Add("PixelStore2");
            cbxStorage.Items.Add("PostGis");
            cbxStorage.Items.Add("SdeBinary");
            cbxStorage.Items.Add("SdeRaster");
            cbxStorage.Items.Add("SpatialWare");
            cbxStorage.Items.Add("OGR");

            cbxStorage.SelectedIndex = 0;

            cbxConnString.Text = "";

            rtbLayerParams.Clear();
            tvLayers.Nodes.Clear();

            btnAddLayer.Enabled = false;
            btnCreateConf.Enabled = false;

            if (cbxConnString.Items.Count > 0)
            {
                cbxConnString.SelectedIndex = 0;
                cbxConnString_SelectedIndexChanged(sender, e);
            }
            rbADO.Checked = true;
        }

        private TStrings prepareConnectString(String _txt)
        {
            TGIS_Tokenizer tkn;
            char[] c;
            TStringList finalResult;

            tkn = new TGIS_Tokenizer();
            c = new char[1];
            c[0] = ';';
            tkn.ExecuteEx(_txt, c[0]);
            finalResult = new TStringList();
            finalResult.AddStrings(tkn.Result);
            return finalResult;
        }

        private TStrings prepareOCI()
        {
            TStrings str;
            TStrings finalResult;

            finalResult = new TStringList();
            str = prepareConnectString(cbxConnString.Text);

            finalResult.Add("User_Name=" + str.get_Values("User ID"));
            finalResult.Add("Password=" + str.get_Values("Password"));
            finalResult.Add("Database=" + str.get_Values("Data Source"));

            return finalResult;
        }

        private String prepareADONETCS()
        {
            TGIS_Tokenizer tkn;
            int i;
            char[] c;
            String finalResult;

            tkn = new TGIS_Tokenizer();
            c = new char[1];
            c[0] = ';';
            tkn.Execute(cbxConnString.Text, c);
            finalResult = "";

            for (i = 0; i < tkn.Result.Count - 1; i++)
            {
                if (tkn.Result.get_Names(i) == "Integrated Security" ||
                    tkn.Result.get_Names(i) == "Persist Security Info" ||
                    tkn.Result.get_Names(i) == "User ID" ||
                    tkn.Result.get_Names(i) == "Initial Catalog" ||
                    tkn.Result.get_Names(i) == "Password" ||
                    tkn.Result.get_Names(i) == "Data source")

                    finalResult = finalResult + tkn.Result[i] + ';';
            }
            finalResult = finalResult + "MultipleActiveResultSets=True";
            return finalResult;
        }

        private String getSQLPath(String _storage,
                                  String _name,
                                  Boolean _isRaster = false,
                                  Boolean _isOci = false,
                                  Boolean _isAdoNet = false)
        {
            String finalResult;
            int r;
            TStrings str;
            String cs;
            String cn;

            if (_isOci)
            {
                str = prepareConnectString(cbxConnString.Text);

                finalResult = "TatukGIS Layer" + "\n" +
                              "Storage=" + _storage + "\n" +
                              "Dialect= " + cbxDialect.Text + "\n" +
                              "User_Name=" + str.get_Values("User ID") + "\n" +
                              "Password=" + str.get_Values("Password") + "\n" +
                              "Database=" + str.get_Values("Data Source") + "\n" +
                              "Layer=" + _storage + "\n";
            }
            else
            {
                if (_isAdoNet)
                    cs = prepareADONETCS();
                else if (rbGDB.Checked)
                    cs = cbxConnString.Text;
                else
                    cs = cbxConnString.Text;

                if (rbSQLite.Checked)
                    cn = "Sqlite";
                else if (_isAdoNet)
                    cn = "ADONET";
                else if (rbGDB.Checked)
                    cn = "Path";
                else
                    cn = "ADO";

                finalResult = "[TatukGIS Layer]" + "\n" +
                              "Storage= " + _storage + "\n" +
                              cn + "=" + cs + "\n" +
                              "Dialect= " + cbxDialect.Text + "\n" +
                              "Layer=" + _name;
            }

            for (r = 0; r < getRtbLinesCount() - 1; r++)
                finalResult = finalResult + "\n" + lst.get_Names(r) + "=" + lst.get_ValueFromIndex(r) + "\n";

            if (_isRaster)
                finalResult = finalResult + "\n" + ".ttkps";
            else
                finalResult = finalResult + "\n" + ".ttkls";

            //finalResult = TGIS_Utils.ConstructParamString(finalResult);
            return finalResult;
        }

        private void fillTree(String _name, TList<TGIS_LayerInfo> _ls, int _type)
        {
            int i, m;
            TreeNode tr;
            TreeNode tc;

            tvLayers.BeginUpdate();

            tr = tvLayers.Nodes.Add(_name);
            tr.ImageIndex = imgList.Images.Count - 1;
            tr.SelectedImageIndex = imgList.Images.Count - 1;
            tvLayers.SelectedNode = tr;

            _ls.Sort( ( a, b ) => a.Caption.CompareTo( b.Caption ) );

            for (i = 0; i <= _ls.Count - 1; i++)
            {
                tc = tvLayers.SelectedNode.Nodes.Add(_ls[i].Name);
                m = (int)_ls[i].ShapeType;
                if (m == -1)
                    m = imgList.Images.Count - 1;
                tc.ImageIndex = m;
                tc.SelectedImageIndex = m;
                tc.Tag = (Object)_type;

                tr.Expand();

            }

            tvLayers.EndUpdate();
        }

        private void cbxConnString_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;

            i = cbxConnString.SelectedIndex;

            if (i < 0) return;
        }

        private void rbADO_CheckedChanged(object sender, EventArgs e)
        {
            lbPath.Visible = false;
            btnPath.Visible = false;
            lbConnStr.Visible = true;
            btnBuild.Visible = true;
        }

        private void rbSQLite_CheckedChanged(object sender, EventArgs e)
        {
            lbPath.Visible = true;
            btnPath.Visible = true;
            lbConnStr.Visible = false;
            dlgOpen.FilterIndex = 1;
            btnBuild.Visible = false;
            cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("SQLITE");
            cbxDialect_SelectedIndexChanged(sender, e);
        }

        private void cbxDialect_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxStorage.Items.Clear();

            if (cbxDialect.Text == "FILEGDB")
                cbxStorage.Items.Add("FILEGDB");
            else if (cbxDialect.Text == "OGR")
                cbxStorage.Items.Add("OGR");
            else
            {
                cbxStorage.Items.Add("*");
                cbxStorage.Items.Add("Native");
                cbxStorage.Items.Add("OpenGisBlob");
                cbxStorage.Items.Add("OpenGisBlob2");
                cbxStorage.Items.Add("PixelStore2");

                if (cbxDialect.Text == "ORACLE")
                {
                    cbxStorage.Items.Add("GeomediaOracleSpatial");
                    cbxStorage.Items.Add("OracleGeoraster");
                    cbxStorage.Items.Add("OracleSpatial");
                    cbxStorage.Items.Add("OracleSpatialPc");
                    cbxStorage.Items.Add("OracleSpatialTin");
                    cbxStorage.Items.Add("SdeBinary");
                    cbxStorage.Items.Add("SdeRaster");
                }
                else if (cbxDialect.Text == "POSTGRESQL")
                    cbxStorage.Items.Add("PostGis");
                else if (cbxDialect.Text == "MSSQL")
                {
                    cbxStorage.Items.Add("Geomedia");
                    cbxStorage.Items.Add("GeomediaMsSpatial");
                    cbxStorage.Items.Add("Katmai");
                    cbxStorage.Items.Add("SdeBinary");
                    cbxStorage.Items.Add("SdeRaster");
                    cbxStorage.Items.Add("SpatialWare");
                }
                else if (cbxDialect.Text == "DB2")
                    cbxStorage.Items.Add("DB2SpatialExtender");
                else if (cbxDialect.Text == "INFORMIX")
                    cbxStorage.Items.Add("IfxSpatialDataBlade");
                else if (cbxDialect.Text == "SYBASE")
                    cbxStorage.Items.Add("AnywhereSpatial");
                else if (cbxDialect.Text == "MSJET")
                {
                    cbxStorage.Items.Add("Geomedia");
                    cbxStorage.Items.Add("PersonalGdb");
                }
            }
            cbxStorage.SelectedIndex = 0;
        }

        private void rbGDB_CheckedChanged(object sender, EventArgs e)
        {
            lbPath.Visible = true;
            btnPath.Visible = true;
            lbConnStr.Visible = false;
            btnBuild.Visible = false;
            dlgOpen.FilterIndex = 2;
            cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("FILEGDB".ToUpperInvariant());
            cbxDialect_SelectedIndexChanged(sender, e);
        }

        private void rbOGR_CheckedChanged(object sender, EventArgs e)
        {
            lbPath.Visible = true;
            btnPath.Visible = true;
            lbConnStr.Visible = false;
            dlgOpen.FilterIndex = 3;
            btnBuild.Visible = false;
            cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("OGR");
            cbxDialect_SelectedIndexChanged(sender, e);
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            cbxConnString.Text = PromptDataSource();

            if (cbxConnString.Text.ToUpper().IndexOf("SQLOLEDB") >= 1)
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("MSSQL");
            else if (cbxConnString.Text.ToUpper().IndexOf("SQLNCLI") >= 1)
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("MSSQL");
            else if (cbxConnString.Text.ToUpper().IndexOf("MSDAORA") >= 1)
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("ORACLE");
            else if (cbxConnString.Text.ToUpper().IndexOf("ORACLE") >= 1)
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("ORACLE");
            else if (cbxConnString.Text.ToUpper().IndexOf("POSTGRES") >= 1)
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("POSTGRESQL");
            else if (cbxConnString.Text.ToUpper().IndexOf("ACE") >= 1)
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("MSJET");
            else if (cbxConnString.Text.ToUpper().IndexOf("JET") >= 1)
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("MSJET");
            else if (cbxConnString.Text.ToUpper().IndexOf("MS ACCESS") >= 1)
                cbxDialect.SelectedIndex = cbxDialect.Items.IndexOf("MSJET");

            cbxDialect_SelectedIndexChanged(sender, e);

        }

        private String PromptDataSource()
        {
            //? its displayed behind form
            String strConn;
            Object _conn = null;

            strConn = "";
            DataLinks _link = new DataLinks();
            _conn = _link.PromptNew();
            if (_conn == null)
                return String.Empty;
            strConn = ((Connection)_conn).ConnectionString;

            return strConn;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            TList<TGIS_LayerInfo> ls;
            TGIS_LayerVectorSqlAbstract lv;
            TGIS_LayerPixelSqlAbstract lp;
            String lname;
            TGIS_LayerFGDB lf;
            TGIS_LayerOGR lo;

            if (cbxConnString.Text == "" || cbxDialect.Text == "") return;

            lname = "test";
            tvLayers.Nodes.Clear();
            rtbLayerParams.Clear();

            if (canUseStorage("NATIVE"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("NATIVE", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("Native", ls, LAYERSQL_NATIVE);
                }
            }

            if (canUseStorage("OPENGISBLOB"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("OPENGISBLOB", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("OpenGIS", ls, LAYERSQL_OPENGISBLOB);
                }
            }
            else if (canUseStorage("OPENGISBLOB2"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("OPENGISBLOB2", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("OpenGIS", ls, LAYERSQL_OPENGISBLOB2);
                }
            }

            if (canUseStorage("PIXELSTORE2"))
            {
                lp = (TGIS_LayerPixelSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("PIXELSTORE2", lname, true));

                if (lp != null)
                {
                    ls = lp.GetAvailableLayers();
                    fillTree("PixelStore", ls, LAYERSQL_PIXELSTORE2);
                }
            }

            if (canUseStorage("GEOMEDIA") &&
                (canUseDialect("MSSQL") ||
                 canUseDialect("ORACLE") ||
                 canUseDialect("MSJET")))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("GEOMEDIA", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("Geomedia", ls, LAYERSQL_GEOMEDIA);
                }
            }

            if (canUseStorage("PERSONALGDB") && canUseDialect("MSJET"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("PERSONALGDB", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("Personal GDB", ls, LAYERSQL_PERSONALGDB);
                }
            }

            if (canUseStorage("POSTGIS") && canUseDialect("POSTGRESQL"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("POSTGIS", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("PostGIS", ls, LAYERSQL_POSTGIS);
                }
            }

            if ((canUseStorage("SDEBINARY") || canUseStorage("SDEOGCWKB")) &&
                (canUseDialect("MSSQL") || canUseDialect("ORACLE")))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("SDEBINARY", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("ArcSDE Vector", ls, LAYERSQL_SDEBINARY);
                }
            }

            if (canUseStorage("SDERASTER") && canUseDialect("MSSQL"))
            {
                lp = (TGIS_LayerPixelSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("SDERASTER", lname, true));

                if (lp != null)
                {
                    ls = lp.GetAvailableLayers();
                    fillTree("ArcSDE Vector", ls, LAYERSQL_SDERASTER);
                }
            }

            if (canUseStorage("KATMAI") && canUseDialect("MSSQL"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("SDERASTER", lname, false, false, true));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("MsSql Spatial", ls, LAYERSQL_KATMAI);
                }
            }

            if (canUseStorage("SPATIALWARE") && canUseDialect("MSSQL"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("SPATIALWARE", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("SpatialWare", ls, LAYERSQL_SPATIALWARE);
                }
            }

            if (canUseStorage("ORACLESPATIAL") && canUseDialect("ORACLE"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("ORACLESPATIAL", lname, false, true));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("Oracle Spatial", ls, LAYERSQL_ORACLESPATIAL);
                }
            }

            if (canUseStorage("ORACLEGEORASTER") && canUseDialect("ORACLE"))
            {
                lp = (TGIS_LayerPixelSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("ORACLEGEORASTER", lname, true, true));

                if (lp != null)
                {
                    ls = lp.GetAvailableLayers();
                    fillTree("Oracle Georaster", ls, LAYERSQL_ORACLEGEORASTER);
                }
            }

            if (canUseStorage("DB2SpatialExtender") && canUseDialect("DB2"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("DB2GSE", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("DB2 Spatial Extender", ls, LAYERSQL_DB2GSE);
                }
            }

            if (canUseStorage("IfxSpatialDataBlade") && canUseDialect("INFORMIX"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("IFXSDB", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("IFX Spatial Data Blade", ls, LAYERSQL_IFXSDB);
                }
            }

            if (canUseStorage("FILEGDB") && canUseDialect("FILEGDB"))
            {
                lf = new TGIS_LayerFGDB();
                lf.Path = cbxConnString.Text;
                ls = lf.GetAvailableLayers();
                fillTree("FILEGDB", ls, LAYERSQL_FGDB);
            }

            if (canUseStorage("ORACLESPATIAL_PC") && canUseDialect("ORACLE"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("ORACLESPATIAL_PC", lname, false, true));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("Oracle Spatial PC", ls, LAYERSQL_ORACLESPATIALPC);
                }
            }

            if (canUseStorage("ORACLESPATIAL_TIN") && canUseDialect("ORACLE"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("ORACLESPATIAL_TIN", lname, false, true));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("Oracle Spatial TIN", ls, LAYERSQL_ORACLESPATIALTIN);
                }
            }

            if (canUseStorage("GEOMEDIAMSSPATIAL") && canUseDialect("MSSQL"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("GEOMEDIAMSSPATIAL", lname, false, false, true));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("Geomedia MsSpatial", ls, LAYERSQL_GEOMEDIA_MSSQL);
                }
            }

            if (canUseStorage("GEOMEDIAORACLESPATIAL") && canUseDialect("ORACLE"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("GEOMEDIAORACLESPATIAL", lname, false, true));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("Geomedia Oracle Spatial", ls, LAYERSQL_GEOMEDIA_SDO);
                }
            }

            if (canUseStorage("ANYWHERESPATIAL") && canUseDialect("SYBASE"))
            {
                lv = (TGIS_LayerVectorSqlAbstract)TGIS_Utils.GisCreateLayer(lname, getSQLPath("ANYWHERESPATIAL", lname));

                if (lv != null)
                {
                    ls = lv.GetAvailableLayers();
                    fillTree("Anywhere Spatial", ls, LAYERSQL_ANYWHERE_SPATIAL);
                }
            }

            if (canUseStorage("OGR") && canUseDialect("OGR"))
            {
                lo = new TGIS_LayerOGR();
                lo.Path = cbxConnString.Text;
                ls = lo.GetAvailableLayers();
                fillTree("OGR", ls, LAYERSQL_OGR);
            }
        }

        private Boolean canUseStorage(String _type)
        {
            Boolean finalResult;

            finalResult = true;

            if (cbxStorage.Text == "" || cbxStorage.Text == "*") return finalResult;

            finalResult = cbxStorage.Text.ToUpper() == _type.ToUpper();
            return finalResult;
        }

        private Boolean canUseDialect(String _type)
        {
            return cbxDialect.Text.ToUpper() == _type.ToUpper();
        }

        private void tvLayers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TGIS_Layer ll;
            int i;

            if (tvLayers.SelectedNode != null && tvLayers.SelectedNode.Level > 0)
            {
                btnAddLayer.Enabled = true;
                btnCreateConf.Enabled = true;

                ll = getLayer(tvLayers.SelectedNode.Text, (int)tvLayers.SelectedNode.Tag);
                rtbLayerParams.Clear();
                if (ll is TGIS_LayerVectorSqlAbstract)
                    for (i = 0; i < ((TGIS_LayerVectorSqlAbstract)ll).SQLParametersEx.Count - 1; i++)
                    {
                        rtbLayerParams.AppendText(((TGIS_LayerVectorSqlAbstract)ll).SQLParametersEx[i] + "=");
                        lst.Add(((TGIS_LayerVectorSqlAbstract)ll).SQLParametersEx[i] + "=");
                    }
                else if (ll is TGIS_LayerPixelSqlAbstract)
                    for (i = 0; i < ((TGIS_LayerPixelSqlAbstract)ll).SQLParametersEx.Count - 1; i++)
                    {
                        rtbLayerParams.AppendText(((TGIS_LayerPixelSqlAbstract)ll).SQLParametersEx[i] + "=");
                        lst.Add(((TGIS_LayerPixelSqlAbstract)ll).SQLParametersEx[i] + "=");
                    }
            }
            else
            {
                btnAddLayer.Enabled = false;
                btnCreateConf.Enabled = false;
            }

        }

        private TGIS_Layer getLayer(String _name, int _type)
        {
            switch (_type)
            {
                default: return null;
                case LAYERSQL_NATIVE:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("NATIVE", _name));
                case LAYERSQL_OPENGISBLOB:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("OPENGISBLOB", _name));
                case LAYERSQL_OPENGISBLOB2:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("OPENGISBLOB2", _name));
                case LAYERSQL_GEOMEDIA:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("GEOMEDIA", _name));
                case LAYERSQL_POSTGIS:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("POSTGIS", _name));
                case LAYERSQL_PERSONALGDB:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("PERSONALGDB", _name));
                case LAYERSQL_SDEBINARY:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("SDEBINARY", _name));
                case LAYERSQL_PIXELSTORE2:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("PIXELSTORE2", _name, true));
                case LAYERSQL_KATMAI:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("KATMAI", _name, false, false, true));
                case LAYERSQL_ORACLESPATIAL:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("ORACLESPATIAL", _name, false, true));
                case LAYERSQL_SDERASTER:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("SDERASTER", _name, true));
                case LAYERSQL_ORACLEGEORASTER:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("ORACLEGEORASTER", _name, true, true));
                case LAYERSQL_SPATIALWARE:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("SPATIALWARE", _name));
                case LAYERSQL_DB2GSE:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("DB2GSE", _name));
                case LAYERSQL_IFXSDB:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("IFXSDB", _name));
                case LAYERSQL_FGDB:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("FILEGDB", _name));
                case LAYERSQL_ORACLESPATIALPC:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("ORACLESPATIAL_PC", _name, false, true));
                case LAYERSQL_ORACLESPATIALTIN:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("ORACLESPATIAL_TIN", _name, false, true));
                case LAYERSQL_GEOMEDIA_MSSQL:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("GEOMEDIAMSSPATIAL", _name, false, false, true));
                case LAYERSQL_GEOMEDIA_SDO:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("GEOMEDIAORACLESPATIAL", _name, false, true));
                case LAYERSQL_ANYWHERE_SPATIAL:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("ANYWHERESPATIAL", _name));
                case LAYERSQL_OGR:
                    return TGIS_Utils.GisCreateLayer(_name, getSQLPath("OGR", _name));
            }
        }

        private void addNewLayer(String _name, int _type)
        {
            TGIS_Layer ll;

            ll = getLayer(_name, _type);
            if (ll != null)
            {
                ll.Open();
                GIS.Add(ll);
            }
        }

        private void btnAddLayer_Click(object sender, EventArgs e)
        {
            TreeNode tr;

            tr = findFirstNode(tvLayers.SelectedNode);
            while (tr != null)
            {
                if (tr.Level > 0)
                    if (tr.IsSelected)
                        addNewLayer(tr.Text, (int)tr.Tag);
                tr = tr.NextVisibleNode;
            }

            if (GIS.Items.Count == 1)
                GIS.FullExtent();
            else
                GIS.InvalidateWholeMap();
        }

        private TreeNode findFirstNode(TreeNode _tr)
        {
            while (_tr.Parent != null)
                _tr = _tr.Parent;
            return _tr;
        }

        private int getRtbLinesCount()
        {
            return rtbLayerParams.GetLineFromCharIndex(rtbLayerParams.GetCharFromPosition(new Point(1, rtbLayerParams.Height - 1))) -
                   rtbLayerParams.GetLineFromCharIndex(rtbLayerParams.GetCharFromPosition(new Point(1, 1)));
        }

        private void btnCreateConf_Click(object sender, EventArgs e)
        {
            String layerName;
            String ttklspath;
            String storage;
            TGIS_Config ini;
            int p;
            TStrings str;
            int lt;
            Boolean isttkps;

            if (tvLayers.SelectedNode != null && tvLayers.SelectedNode.Level > 0)
            {
                lt = (int)tvLayers.SelectedNode.Tag;
                isttkps = (lt == LAYERSQL_PIXELSTORE2) || (lt == LAYERSQL_SDERASTER) || (lt == LAYERSQL_ORACLEGEORASTER);

                if (isttkps)
                    dlgSave.FilterIndex = 2;
                else
                    dlgSave.FilterIndex = 1;

                dlgSave.ShowDialog();

                ttklspath = dlgSave.FileName;

                layerName = tvLayers.SelectedNode.Name;

                switch (lt)
                {
                    case LAYERSQL_NATIVE: storage = "NATIVE"; break;
                    case LAYERSQL_OPENGISBLOB: storage = "OPENGISBLOB"; break;
                    case LAYERSQL_OPENGISBLOB2: storage = "OPENGISBLOB2"; break;
                    case LAYERSQL_GEOMEDIA: storage = "GEOMEDIA"; break;
                    case LAYERSQL_POSTGIS: storage = "POSTGIS"; break;
                    case LAYERSQL_PERSONALGDB: storage = "PERSONALGDB"; break;
                    case LAYERSQL_SDEBINARY: storage = "SDEBINARY"; break;
                    case LAYERSQL_PIXELSTORE2: storage = "PIXELSTORE2"; break;
                    case LAYERSQL_KATMAI: storage = "KATMAI"; break;
                    case LAYERSQL_ORACLESPATIAL: storage = "ORACLESPATIAL"; break;
                    case LAYERSQL_SDERASTER: storage = "SDERASTER"; break;
                    case LAYERSQL_ORACLEGEORASTER: storage = "ORACLEGEORASTER"; break;
                    case LAYERSQL_SPATIALWARE: storage = "SPATIALWARE"; break;
                    case LAYERSQL_DB2GSE: storage = "DB2GSE"; break;
                    case LAYERSQL_IFXSDB: storage = "IFXSDB"; break;
                    case LAYERSQL_FGDB: storage = "FILEGDB"; break;
                    case LAYERSQL_OGR: storage = "OGR"; break;
                    case LAYERSQL_ORACLESPATIALPC: storage = "ORACLESPATIAL_PC"; break;
                    case LAYERSQL_ORACLESPATIALTIN: storage = "ORACLESPATIAL_TIN"; break;
                    case LAYERSQL_ANYWHERE_SPATIAL: storage = "ANYWHERESPATIAL"; break;
                    default: storage = "NATIVE"; break;
                }

                ini = TGIS_ConfigFactory.CreateConfig(null, ttklspath);

                ini.Section = "TatukGIS Layer";
                ini.WriteString("STORAGE", storage, "");
                ini.WriteString("LAYER", layerName, "");
                ini.WriteString("DIALECT", cbxDialect.Text, "");

                if (storage == "KATMAI")
                    ini.WriteString(ini.Section, "ADONET", prepareADONETCS());
                else if (storage == "ORACLESPATIAL")
                {
                    str = prepareOCI();
                    for (p = 0; p < str.Count - 1; p++)
                        ini.WriteString(str.get_Names(p), str.get_ValueFromIndex(p), "");
                }
                else
                {
                    if (rbSQLite.Checked)
                        ini.WriteString(ini.Section, "Sqlite", TGIS_Utils.GisPathRelative(TGIS_Utils.GisFilePath(ttklspath), cbxConnString.Text));
                    if (rbGDB.Checked)
                        ini.WriteString(ini.Section, "Path", TGIS_Utils.GisPathRelative(TGIS_Utils.GisFilePath(ttklspath), cbxConnString.Text));
                    else
                        ini.WriteString(ini.Section, "ADO", cbxConnString.Text);
                }

                for (p = 0; p < getRtbLinesCount(); p++)
                    ini.WriteString(lst.get_Names(p), lst.get_ValueFromIndex(p), "");

                ini.Save();
            }
            else
                return;

        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            String path;

            if (rbGDB.Checked)
            {
                dlgOpenFolder.ShowDialog();
                dlgOpenFolder.ShowNewFolderButton = false;
                path = dlgOpenFolder.SelectedPath;
                if (path != "")
                    cbxConnString.Text = path;
            }
            else
            {
                dlgOpen.ShowDialog();
                cbxConnString.Text = dlgOpen.FileName;
            }
        }
    }
}
