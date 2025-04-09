using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace TigerGeocoding
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
        private System.Windows.Forms.ToolStrip toolBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOpenDefault;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox gbxFind;
        private System.Windows.Forms.TextBox edtAddress;
        private System.Windows.Forms.Button btnFindFirst;
        private System.Windows.Forms.Button btnFindAll;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.CheckBox chkExtended;
        private System.Windows.Forms.Button btnMatches;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.WinForms.TGIS_ControlScale GIS_ControlScale;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private TGIS_LayerVector layerSrc;
        private TGIS_LayerVector layerResult;
        private TGIS_Geocoding geoObj;
        private ArrayList infoFields;
        private ArrayList fieldNames;
        private int selectedRow;
        private int state;
        private bool doAbort;
        private bool fShown;
        private System.Windows.Forms.ListBox lstMemo;
        private System.Windows.Forms.ToolTip toolTip4;
        private MatchesForm frmMatches;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            toolTip1.SetToolTip(edtAddress, "See Help");
            toolTip2.SetToolTip(chkExtended, "See Help");
            toolTip3.SetToolTip(btnOpen, "Open a TIGER file");
            toolTip4.SetToolTip(lstMemo, "Double click to display info");
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
            TatukGIS.NDK.TGIS_CSUnits tgiS_CSUnits1 = new TatukGIS.NDK.TGIS_CSUnits();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.toolBar1 = new System.Windows.Forms.ToolStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnOpenDefault = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lstMemo = new System.Windows.Forms.ListBox();
            this.gbxFind = new System.Windows.Forms.GroupBox();
            this.btnMatches = new System.Windows.Forms.Button();
            this.chkExtended = new System.Windows.Forms.CheckBox();
            this.btnHelp = new System.Windows.Forms.Button();
            this.edtAddress = new System.Windows.Forms.TextBox();
            this.btnFindAll = new System.Windows.Forms.Button();
            this.btnFindFirst = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.GIS_ControlScale = new TatukGIS.NDK.WinForms.TGIS_ControlScale();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip4 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbxFind.SuspendLayout();
            this.GIS.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.AutoSize = false;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowItemToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(592, 29);
            this.toolBar1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.btnOpen);
            this.panel1.Controls.Add(this.btnOpenDefault);
            this.panel1.Controls.Add(this.toolBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 29);
            this.panel1.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progressBar1.Location = new System.Drawing.Point(177, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(297, 23);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(89, 2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(88, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnOpenDefault
            // 
            this.btnOpenDefault.Location = new System.Drawing.Point(0, 2);
            this.btnOpenDefault.Name = "btnOpenDefault";
            this.btnOpenDefault.Size = new System.Drawing.Size(89, 23);
            this.btnOpenDefault.TabIndex = 1;
            this.btnOpenDefault.Text = "Open Default";
            this.btnOpenDefault.Click += new System.EventHandler(this.btnOpenDefault_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "TIGER files (*.RT1)|*.RT1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lstMemo);
            this.panel2.Controls.Add(this.gbxFind);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(351, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(241, 437);
            this.panel2.TabIndex = 2;
            // 
            // lstMemo
            // 
            this.lstMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMemo.Location = new System.Drawing.Point(0, 129);
            this.lstMemo.Name = "lstMemo";
            this.lstMemo.ScrollAlwaysVisible = true;
            this.lstMemo.Size = new System.Drawing.Size(241, 308);
            this.lstMemo.TabIndex = 1;
            this.lstMemo.SelectedIndexChanged += new System.EventHandler(this.lstMemo_SelectedIndexChanged);
            // 
            // gbxFind
            // 
            this.gbxFind.Controls.Add(this.btnMatches);
            this.gbxFind.Controls.Add(this.chkExtended);
            this.gbxFind.Controls.Add(this.btnHelp);
            this.gbxFind.Controls.Add(this.edtAddress);
            this.gbxFind.Controls.Add(this.btnFindAll);
            this.gbxFind.Controls.Add(this.btnFindFirst);
            this.gbxFind.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxFind.Location = new System.Drawing.Point(0, 0);
            this.gbxFind.Name = "gbxFind";
            this.gbxFind.Size = new System.Drawing.Size(241, 129);
            this.gbxFind.TabIndex = 0;
            this.gbxFind.TabStop = false;
            this.gbxFind.Text = "Find Address(es):";
            // 
            // btnMatches
            // 
            this.btnMatches.Enabled = false;
            this.btnMatches.Location = new System.Drawing.Point(169, 89);
            this.btnMatches.Name = "btnMatches";
            this.btnMatches.Size = new System.Drawing.Size(57, 23);
            this.btnMatches.TabIndex = 5;
            this.btnMatches.Text = "Matches";
            this.btnMatches.Click += new System.EventHandler(this.btnMatches_Click);
            // 
            // chkExtended
            // 
            this.chkExtended.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkExtended.Location = new System.Drawing.Point(22, 58);
            this.chkExtended.Name = "chkExtended";
            this.chkExtended.Size = new System.Drawing.Size(154, 17);
            this.chkExtended.TabIndex = 4;
            this.chkExtended.Text = "&Exact street- a. city names";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(184, 58);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(41, 23);
            this.btnHelp.TabIndex = 3;
            this.btnHelp.Text = "&Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // edtAddress
            // 
            this.edtAddress.Location = new System.Drawing.Point(20, 30);
            this.edtAddress.Name = "edtAddress";
            this.edtAddress.Size = new System.Drawing.Size(205, 20);
            this.edtAddress.TabIndex = 0;
            // 
            // btnFindAll
            // 
            this.btnFindAll.Location = new System.Drawing.Point(88, 89);
            this.btnFindAll.Name = "btnFindAll";
            this.btnFindAll.Size = new System.Drawing.Size(63, 23);
            this.btnFindAll.TabIndex = 2;
            this.btnFindAll.Text = "Find &All";
            this.btnFindAll.Click += new System.EventHandler(this.btnFindAll_Click);
            // 
            // btnFindFirst
            // 
            this.btnFindFirst.Location = new System.Drawing.Point(19, 89);
            this.btnFindFirst.Name = "btnFindFirst";
            this.btnFindFirst.Size = new System.Drawing.Size(63, 23);
            this.btnFindFirst.TabIndex = 1;
            this.btnFindFirst.Text = "&Find First";
            this.btnFindFirst.Click += new System.EventHandler(this.btnFindFirst_Click);
            // 
            // GIS
            // 
            this.GIS.Controls.Add(this.GIS_ControlScale);
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.MinZoomSize = -5;
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(351, 437);
            this.GIS.TabIndex = 3;
            // 
            // GIS_ControlScale
            // 
            this.GIS_ControlScale.BackColor = System.Drawing.SystemColors.Control;
            this.GIS_ControlScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GIS_ControlScale.DividerColor1 = System.Drawing.Color.Black;
            this.GIS_ControlScale.DividerColor2 = System.Drawing.Color.White;
            this.GIS_ControlScale.GIS_Viewer = this.GIS;
            this.GIS_ControlScale.Location = new System.Drawing.Point(8, 10);
            this.GIS_ControlScale.Name = "GIS_ControlScale";
            this.GIS_ControlScale.PrepareEvent = null;
            this.GIS_ControlScale.Size = new System.Drawing.Size(145, 25);
            this.GIS_ControlScale.TabIndex = 0;
            tgiS_CSUnits1.DescriptionEx = null;
            this.GIS_ControlScale.Units = tgiS_CSUnits1;
            this.GIS_ControlScale.UnitsEPSG = 0;
            this.GIS_ControlScale.Visible = false;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Geocoding on TIGER / Line Files";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.WinForm_Closing);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.Leave += new System.EventHandler(this.WinForm_Leave);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gbxFind.ResumeLayout(false);
            this.gbxFind.PerformLayout();
            this.GIS.ResumeLayout(false);
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
            infoFields = new ArrayList();
            infoFields.Add("HOUSENUMBER");
            infoFields.Add("DIRPREFIX");
            infoFields.Add("STREETNAME");
            infoFields.Add("DIRSUFFIX");
            infoFields.Add("STREETTYPE");
            fieldNames = new ArrayList();
            fieldNames.Add("FULLNAME");
            fieldNames.Add("LFROMADD");
            fieldNames.Add("LTOADD");
            fieldNames.Add("RFROMADD");
            fieldNames.Add("RTOADD");
            fieldNames.Add("ZIPL");
            fieldNames.Add("ZIPR");

            selectedRow = -1;
            state = -1;
            frmMatches = new MatchesForm();
        }

        private void WinForm_Leave(object sender, System.EventArgs e)
        {
            if (geoObj != null)
            {
                geoObj.Dispose();
                geoObj = null;
            }
            if (layerSrc != null)
                GIS.Close();
            frmMatches = null;
            fieldNames = null;
            infoFields = null;
        }

        private void openCoverage(string _path)
        {
            // for pretty view
            Update();

            // free what it wants to
            if (layerResult != null)
            {
                GIS.Delete(layerResult.Name);
                layerResult = null;
            }
            if (geoObj != null)
            {
                geoObj.Dispose();
                geoObj = null;
            }
            if (layerSrc != null)
                GIS.Close();
            btnFindFirst.Enabled = false;
            btnFindAll.Enabled = false;
            btnHelp.Enabled = false;
            btnMatches.Enabled = false;

            progressBar1.Visible = true;
            GIS.BusyEvent += new TGIS_BusyEvent(Busy);
            GIS.Lock();
            GIS.Open(_path);
            GIS.BusyEvent += null;
            progressBar1.Visible = false;

            layerSrc = (TGIS_LayerVector)GIS.Items[0];
            if (layerSrc == null) return;
            layerSrc.Params.Line.SmartSize = -1;
            layerSrc.Params.Labels.Field = "FULLNAME";
            layerSrc.Params.Labels.Alignment = TGIS_LabelAlignment.Follow;
            layerSrc.Params.Labels.Color = TGIS_Color.Black;

            layerSrc.ParamsList.Add();
            layerSrc.Params.Query = "MTFCC < 'S1400'";
            layerSrc.Params.Line.Width = -2;
            layerSrc.Params.Line.Style = TGIS_PenStyle.Solid;
            layerSrc.UseConfig = false;
            layerSrc.CS = GIS.CS;

            // create route layer
            layerResult = new TGIS_LayerVector();
            layerResult.UseConfig = false;
            layerResult.Params.Line.Color = TGIS_Color.Red;
            layerResult.Params.Line.Width = -2;
            layerResult.Params.Marker.OutlineWidth = 1;
            layerResult.Name = "RouteDisplay";
            layerResult.CS = GIS.CS;
            GIS.Add(layerResult);

            // create geocod+ing object, set fields for routing
            geoObj = new TGIS_Geocoding(layerSrc);
            geoObj.Offset = 0.0001;
            geoObj.LoadFormulas(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\Geocoding\us_addresses.geo",
                                                     TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\Geocoding\tiger2008.geo"
                                                 );

            GIS.Unlock();
            GIS.FullExtent();

            GIS_ControlScale.Visible = true;

            btnFindFirst.Enabled = true;
            btnFindAll.Enabled = true;
            btnHelp.Enabled = true;

            // focus on edit window
            edtAddress.Text = "";
            edtAddress.Focus();

            lstMemo.Items.Clear();
            state = -1;
            selectedRow = -1;
        }

        private void Busy(object _sender, TGIS_BusyEventArgs _e)
        {
            // show progress
            if (_e.Pos == 0)
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                progressBar1.Value = 0;
                doAbort = false;
            }
            else if (_e.Pos == -1)
            {
                progressBar1.Maximum = 100;
                progressBar1.Value = 100;
            }
            else
            {
                if (doAbort == true)
                    _e.Abort = true;
                else
                {
                    progressBar1.Maximum = (int)_e.EndPos;
                    progressBar1.Value = (int)_e.Pos;
                }
            }
            Application.DoEvents();
        }

        private void WinForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            doAbort = true;
        }

        private void btnOpenDefault_Click(object sender, System.EventArgs e)
        {
            openCoverage(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\San Bernardino\TIGER\tl_2008_06071_edges_trunc.SHP");
        }

        private void btnOpen_Click(object sender, System.EventArgs e)
        {
            //open a file
            openFileDialog1.Filter = "SHP files (*.SHP)|*.SHP";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                openCoverage(openFileDialog1.FileName);
        }

        private void btnFindFirst_Click(object sender, System.EventArgs e)
        {
            findAddress(true, !chkExtended.Checked);
        }

        private void btnFindAll_Click(object sender, System.EventArgs e)
        {
            findAddress(false, !chkExtended.Checked);
        }

        private int parse(bool _findFirst, bool _extendedScope)
        {
            TObjectList<Object> resolvedAddresses = null;
            TObjectList<Object> resolvedAddresses2 = null;
            int res = 0;

            try
            {
                if (geoObj.Match(edtAddress.Text,
                                                     ref resolvedAddresses,
                                                     ref resolvedAddresses2))
                {
                    frmMatches.ShowMatches(resolvedAddresses,
                                                                    resolvedAddresses2
                                                                );
                    res = geoObj.ParseEx(resolvedAddresses,
                                                                resolvedAddresses2,
                                                                _findFirst,
                                                                _extendedScope,
                                                                true
                                                            );
                    btnMatches.Enabled = true;
                }
            }
            finally
            {
                resolvedAddresses = null;
                resolvedAddresses2 = null;
            }
            return res;
        }

        private void findAddress(bool _findFirst, bool _extendedScope)
        {
            int i, j;
            int r = -1;
            TGIS_Shape shp;
            string s;

            if (geoObj == null)
            {
                MessageBox.Show("Open a TIGER/Line file.", "Open Error",
                                                 MessageBoxButtons.OK
                                             );
                return;
            }

            layerResult.RevertAll();
            lstMemo.Items.Clear();
            state = -1;
            selectedRow = -1;
            btnMatches.Enabled = false;

            // locate shapes meeting query
            Cursor = Cursors.WaitCursor;
            try
            {
                r = parse(_findFirst, _extendedScope) - 1;
            }
            catch (EGIS_Exception e)
            {
                MessageBox.Show("EGIS exception: " + e.Message);
                r = -1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e);
                r = -1;
            }
            if (r < 0)
            {
                edtAddress.Text = edtAddress.Text + " ???";
            }
            else
            {
                edtAddress.Text = geoObj.get_Query(0);
                if (_findFirst == true)
                {
                    toolTip4.Active = false;
                    state = 0;
                }
                else
                {
                    toolTip4.Active = true;
                    state = 1;
                }
            }

            lstMemo.BeginUpdate();
            for (i = 0; i <= r; i++)
            {
                // add found shape to route layer (red color)
                shp = layerSrc.GetShape(geoObj.get_Uid(i));
                layerResult.AddShape(shp);

                if (i == 0)
                {
                    layerResult.Extent = shp.Extent;
                }

                if (_findFirst == true)
                {
                    if (i == 0)
                    {
                        for (j = 0; j < fieldNames.Count; j++)
                        {
                            s = (string)shp.GetField((string)fieldNames[j]);
                            lstMemo.Items.Add(fieldNames[j] + "=" + s);
                        }
                    }
                }
                else
                {
                    lstMemo.Items.Add(geoObj.get_Query(i));
                }

                shp = layerSrc.GetShape(geoObj.get_UidEx(i));
                if (shp != null)
                {
                    layerResult.AddShape(shp);
                    if (_findFirst == true)
                    {
                        if (i == 0)
                        {
                            lstMemo.Items.Add("---------------------------");
                            for (j = 0; j < fieldNames.Count; j++)
                            {
                                s = (string)shp.GetField((string)fieldNames[j]);
                                lstMemo.Items.Add(fieldNames[j] + "=" + s);
                            }
                        }
                    }
                }

                // mark address as green squere
                shp = layerResult.CreateShape(TGIS_ShapeType.Point);
                shp.Lock(TGIS_Lock.Extent);
                shp.AddPart();
                shp.AddPoint(geoObj.get_Point(i));
                shp.Params.Marker.Color = TGIS_Color.Yellow;
                shp.Unlock();
            }
            lstMemo.EndUpdate();

            GIS.Lock();
            GIS.VisibleExtent = layerResult.Extent;
            GIS.Zoom = 0.7 * GIS.Zoom;
            GIS.Unlock();

            Cursor = Cursors.Default;
            lstMemo.Focus();
        }

        private void btnHelp_Click(object sender, System.EventArgs e)
        {
            HelpForm frmHelp = new HelpForm();
            try
            {
                frmHelp.Show();
            }
            finally
            {
                frmHelp = null;
            }
        }

        private void btnMatches_Click(object sender, System.EventArgs e)
        {
            frmMatches.Show();
        }

        private void sgrdMemo_Click(object sender, System.EventArgs e)
        {
            fShown = false;
            showInfo();
        }

        private void sgrdMemo_DoubleClick(object sender, System.EventArgs e)
        {
            if (fShown == false)
                showInfo();
        }

        private void showInfo()
        {
            TGIS_Shape shp;

            if (layerSrc == null) return;
            if (selectedRow == -1) return;

            // get current shape
            shp = layerSrc.GetShape(geoObj.get_Uid(selectedRow));
            GIS.VisibleExtent = shp.Extent;


            GIS.Zoom = 0.7 * GIS.Zoom;
        }

        private void lstMemo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            bool canSelect;

            // check if the cell can be selected
            canSelect = (lstMemo.SelectedIndex < lstMemo.Items.Count) &&
                        !((lstMemo.Items.Count == 1) &&
                          (lstMemo.Items[lstMemo.SelectedIndex].ToString() == "")
                        );
            if ((canSelect == true) && (state == 1))
            {
                selectedRow = lstMemo.SelectedIndex;
                showInfo();
            }
        }
    }
}
