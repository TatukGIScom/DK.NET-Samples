using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Globalization;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace GPSTracker
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolBar1;
        private System.Windows.Forms.ToolStripButton toolBarButton1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton toolBarButton2;
        private System.Windows.Forms.ToolStripButton btnRecord;
        private System.Windows.Forms.ToolStripButton toolBarButton3;
        private System.Windows.Forms.ComboBox cbxCom;
        private System.Windows.Forms.ComboBox cbxBaud;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private TatukGIS.NDK.WinForms.TGIS_GpsNmea GPS;
        private System.Windows.Forms.TextBox edtPoint;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ImageList imageList1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TGIS_Shape currShape;
        private TGIS_Point lastPointGps;
        private TGIS_Point lastPointMap;
        private System.Windows.Forms.Label label1;

        public WinForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            toolTip1.SetToolTip(cbxCom, "Select com port");
            toolTip2.SetToolTip(edtPoint, "Type point name here and click Add");
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxBaud = new System.Windows.Forms.ComboBox();
            this.cbxCom = new System.Windows.Forms.ComboBox();
            this.toolBar1 = new System.Windows.Forms.ToolStrip();
            this.toolBarButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolBarButton2 = new System.Windows.Forms.ToolStripButton();
            this.btnRecord = new System.Windows.Forms.ToolStripButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolStripButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.edtPoint = new System.Windows.Forms.TextBox();
            this.GPS = new TatukGIS.NDK.WinForms.TGIS_GpsNmea();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxBaud);
            this.panel1.Controls.Add(this.cbxCom);
            this.panel1.Controls.Add(this.toolBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 33);
            this.panel1.TabIndex = 0;
            // 
            // cbxBaud
            // 
            this.cbxBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBaud.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200"});
            this.cbxBaud.Location = new System.Drawing.Point(127, 2);
            this.cbxBaud.Name = "cbxBaud";
            this.cbxBaud.Size = new System.Drawing.Size(72, 21);
            this.cbxBaud.TabIndex = 2;
            this.cbxBaud.TabStop = false;
            this.cbxBaud.SelectedIndexChanged += new System.EventHandler(this.cbxBaud_SelectedIndexChanged);
            // 
            // cbxCom
            // 
            this.cbxCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCom.Items.AddRange(new object[] {
            "Com 1",
            "Com 2",
            "Com 3",
            "Com 4",
            "Com 5",
            "Com 6",
            "Com 7",
            "Com 8",
            "Com 9",
            "Com 10"});
            this.cbxCom.Location = new System.Drawing.Point(60, 2);
            this.cbxCom.Name = "cbxCom";
            this.cbxCom.Size = new System.Drawing.Size(67, 21);
            this.cbxCom.TabIndex = 1;
            this.cbxCom.TabStop = false;
            this.cbxCom.SelectedIndexChanged += new System.EventHandler(this.cbxCom_SelectedIndexChanged);
            // 
            // toolBar1
            // 
            this.toolBar1.AutoSize = false;
            this.toolBar1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.btnSave,
            this.btnRecord,
            this.toolBarButton1,
            this.toolBarButton2,
            this.toolBarButton3});
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowItemToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(592, 33);
            this.toolBar1.TabIndex = 0;
            //this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Click += toolBar1_ButtonClick;
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 0;
            this.btnSave.Name = "btnSave";
            this.btnSave.ToolTipText = "Save the data";
            this.btnSave.Click += toolBar1_ButtonClick;
            // 
            // toolBarButton2
            // 
            this.toolBarButton2.Name = "toolBarButton2";
            this.toolBarButton2.Click += toolBar1_ButtonClick;
            // 
            // btnRecord
            // 
            this.btnRecord.ImageIndex = 1;
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.ToolTipText = "Turn on/off route recording";
            this.btnRecord.Click += toolBar1_ButtonClick;
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Click += toolBar1_ButtonClick;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.edtPoint);
            this.panel2.Controls.Add(this.GPS);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 433);
            this.panel2.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(72, 256);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // edtPoint
            // 
            this.edtPoint.Enabled = false;
            this.edtPoint.Location = new System.Drawing.Point(16, 224);
            this.edtPoint.Name = "edtPoint";
            this.edtPoint.Size = new System.Drawing.Size(185, 20);
            this.edtPoint.TabIndex = 1;
            this.edtPoint.Text = "Type name here";
            // 
            // GPS
            // 
            this.GPS.Active = false;
            this.GPS.BackColor = System.Drawing.SystemColors.ControlLight;
            this.GPS.BaudRate = 4800;
            this.GPS.Com = 1;
            this.GPS.Dock = System.Windows.Forms.DockStyle.Top;
            this.GPS.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GPS.Location = new System.Drawing.Point(0, 41);
            this.GPS.Name = "GPS";
            this.GPS.Size = new System.Drawing.Size(221, 176);
            this.GPS.TabIndex = 0;
            this.GPS.Text = " ";
            this.GPS.Timeout = 1000;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(221, 41);
            this.panel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 15);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(225, 33);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(367, 433);
            this.GIS.TabIndex = 2;
            this.GIS.UseRTree = false;
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
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
            this.Text = "TatukGIS GPS Tracker (NMEA)";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.WinForm_Closing);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void ToolBarButton1_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            int i;
            TGIS_LayerVector lv;

            cbxCom.SelectedIndex = GPS.Com - 1;

            for (i = 0; i < cbxBaud.Items.Count; i++)
            {
                if (Convert.ToInt32(cbxBaud.Items[i]) == GPS.BaudRate)
                {
                    cbxBaud.SelectedIndex = i;
                    break;
                }
            }

            GPS.Active = true;

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\WorldDCW\world.shp");


            GIS.Add(TGIS_Utils.GisCreateLayer("routes", "routes.kml"));
            GIS.Add(TGIS_Utils.GisCreateLayer("points", "points.kml"));

            lv = (TGIS_LayerVector)(GIS.Get("routes"));
            lv.Params.Line.Color = TGIS_Color.Red;
            try
            {
                lv.AddField("Date", TGIS_FieldType.String, 10, 0);
            }
            catch
            { }

            lv = (TGIS_LayerVector)(GIS.Get("points"));
            try
            {
                lv.AddField("Name", TGIS_FieldType.String, 10, 0);
            }
            catch
            { }

            lv = (TGIS_LayerVector)(GIS.Get("points"));
            try
            {
                lv.AddField("Date", TGIS_FieldType.String, 10, 0);
            }
            catch
            { }

            lv.Params.Labels.Value = "{Name}<br><i>{date}</i>";

            GPS.Active = true;
        }

        private void cbxCom_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GPS.Com = cbxCom.SelectedIndex + 1;
            GPS.Active = true;
        }

        private void cbxBaud_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GPS.BaudRate = Int32.Parse((string)cbxBaud.Items[cbxBaud.SelectedIndex]);
            GPS.Active = true;
        }

        private void GPS_Position(object sender, System.EventArgs e)
        {
            TGIS_Point ptg;
            Double dist;
            Double prec;
            TGIS_CSGeographicCoordinateSystem cs;

            if (((TimeSpan)(DateTime.Now - GPS.PositionTime)).Hours < 1)
            {
                edtPoint.Enabled = true;
                btnAdd.Enabled = true;
            }
            else
            {
                edtPoint.Enabled = false;
                btnAdd.Enabled = false;
            }

            // calculate delta of two read-out (in meters)

            cs = TGIS_Utils.CSGeographicCoordinateSystemList.ByEPSG(4326);

            ptg = TGIS_Utils.GisPoint(GPS.Longitude, GPS.Latitude);
            dist = cs.Datum.Ellipsoid.Distance(ptg, lastPointGps);
            lastPointGps = ptg;
            lastPointMap = GIS.CS.FromWGS(ptg);

            if (!btnRecord.Checked) return;

            prec = GPS.PositionPrec;
            if (prec == 0) prec = 5;

            // check if point in tolerance

            if (dist < prec) return;

            if (currShape != null)
            {
                currShape.AddPoint(lastPointMap);
                currShape.SetField("Date", DateTime.Now.ToString("G", DateTimeFormatInfo.InvariantInfo));
            }
            currShape = ((TGIS_LayerVector)GIS.Get("routes")).CreateShape(TGIS_ShapeType.Arc);
            currShape.AddPart();
            currShape.AddPoint(lastPointMap);

            GIS.Center = lastPointMap;
        }

        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;
            string lon, lat;

            if (GIS.IsEmpty) return;

            ptg = GIS.CS.ToWGS(GIS.ScreenToMap(new Point(e.X, e.Y)));

            try
            {
                lon = TGIS_Utils.GisLongitudeToStr(ptg.X);
            }
            catch
            {
                lon = "???";
            }

            try
            {
                lat = TGIS_Utils.GisLatitudeToStr(ptg.Y);
            }
            catch
            {
                lat = "???";
            }

            label1.Text = String.Format("{0} : {1}", lon, lat);
        }

        private void toolBar1_ButtonClick(object sender, System.EventArgs e)
        {
            if(sender == btnSave)
            {
                actSaveExecute(sender);
            }
            else if(sender == btnRecord)
            {
                actRecordExecute(sender);
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            TGIS_Shape shp;

            shp = ((TGIS_LayerVector)GIS.Get("points")).CreateShape(TGIS_ShapeType.Point);
            shp.AddPart();
            shp.AddPoint(lastPointMap);
            shp.SetField("Name", edtPoint.Text);
            shp.SetField("Date", DateTime.Now.ToString("G", DateTimeFormatInfo.InvariantInfo));

            GIS.Center = lastPointMap;
        }

        private void actSaveExecute(object sender)
        {
            if (sender != btnSave)
            {
                btnSave.Checked = true;
                Application.DoEvents();
            }

            if (currShape != null)
            {
                currShape.AddPoint(lastPointMap);
                currShape = null;
            }

            GIS.SaveAll();

            if (sender != btnSave)
            {
                btnSave.Checked = false;
                Application.DoEvents();
            }
        }

        private void actRecordExecute(object sender)
        {
            btnRecord.Checked = !btnRecord.Checked;

            if (!btnRecord.Checked)
            {
                // make recording inactive
                currShape = null;
            }
        }

        private void WinForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GIS.Editor.EndEdit();
            if (!GIS.MustSave()) return;

            if (MessageBox.Show("Save all unsaved work?", "TatukGIS", MessageBoxButtons.YesNo ) == DialogResult.Yes)
                GIS.SaveAll();
        }
    }
}
