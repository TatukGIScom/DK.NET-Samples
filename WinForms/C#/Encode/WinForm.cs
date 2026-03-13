using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;

namespace Encode
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnCloseAll;
        private System.Windows.Forms.Button btnOpenBase;
        private System.Windows.Forms.Button btnEncode;
        private System.Windows.Forms.Button btnOpenEncoded;
        private System.Windows.Forms.StatusStrip stripBar1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOpenEncoded = new System.Windows.Forms.Button();
            this.btnEncode = new System.Windows.Forms.Button();
            this.btnOpenBase = new System.Windows.Forms.Button();
            this.btnCloseAll = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOpenEncoded);
            this.panel1.Controls.Add(this.btnEncode);
            this.panel1.Controls.Add(this.btnOpenBase);
            this.panel1.Controls.Add(this.btnCloseAll);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 24);
            this.panel1.TabIndex = 0;
            // 
            // btnOpenEncoded
            // 
            this.btnOpenEncoded.Location = new System.Drawing.Point(275, 0);
            this.btnOpenEncoded.Name = "btnOpenEncoded";
            this.btnOpenEncoded.Size = new System.Drawing.Size(100, 22);
            this.btnOpenEncoded.TabIndex = 2;
            this.btnOpenEncoded.Text = "Open Encoded";
            this.btnOpenEncoded.Click += new System.EventHandler(this.btnOpenEncoded_Click);
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(175, 0);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(100, 22);
            this.btnEncode.TabIndex = 1;
            this.btnEncode.Text = "Encode Layer";
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // btnOpenBase
            // 
            this.btnOpenBase.Location = new System.Drawing.Point(75, 0);
            this.btnOpenBase.Name = "btnOpenBase";
            this.btnOpenBase.Size = new System.Drawing.Size(100, 22);
            this.btnOpenBase.TabIndex = 0;
            this.btnOpenBase.Text = "Open Base";
            this.btnOpenBase.Click += new System.EventHandler(this.btnOpenBase_Click);
            // 
            // btnCloseAll
            // 
            this.btnCloseAll.Location = new System.Drawing.Point(0, 0);
            this.btnCloseAll.Name = "btnCloseAll";
            this.btnCloseAll.Size = new System.Drawing.Size(75, 22);
            this.btnCloseAll.TabIndex = 3;
            this.btnCloseAll.Text = "Close All";
            this.btnCloseAll.Click += new System.EventHandler(this.btnCloseAll_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 42);
            this.toolStrip1.TabIndex = 0;
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 1;
            // 
            // GIS
            // 
            this.GIS.Cursor = System.Windows.Forms.Cursors.Default;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 24);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 100;
            this.GIS.Size = new System.Drawing.Size(592, 423);
            this.GIS.TabIndex = 2;
            this.GIS.UseRTree = false;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.stripBar1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Encode";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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

        private void btnCloseAll_Click(object sender, System.EventArgs e)
        {
            GIS.Close();
        }

        private void btnOpenBase_Click(object sender, System.EventArgs e)
        {
            TGIS_LayerSHP ll;

            GIS.Close();

            // add states layer
            ll = new TGIS_LayerSHP();
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() + @"\World\WorldDCW\world.shp";
            ll.Name = "base";
            ll.Params.Labels.Field = "NAME";
            GIS.Add(ll);
            GIS.FullExtent();
        }

        private void btnEncode_Click(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ls;
            TGIS_LayerSHP ld;

            if (GIS.IsEmpty)
            {
                MessageBox.Show("Open Base layer first");
                return;
            }

            ls = (TGIS_LayerVector)(GIS.Items[0]);
            if (ls.Name == "encoded")
            {
                MessageBox.Show("This layer is alredy encoded, Open Base layer");
                return;
            }

            ld = new TGIS_LayerSHP();
            ld.ReadEvent += new TGIS_ReadWriteEvent(this.doRead);
            ld.WriteEvent += new TGIS_ReadWriteEvent(this.doWrite);
            ld.Path = "encoded.shp";

            ld.ImportLayer(ls, GIS.Extent,
                                            TGIS_ShapeType.Polygon, "", false
                                        );
        }

        private void btnOpenEncoded_Click(object sender, System.EventArgs e)
        {
            TGIS_LayerSHP ll;

            GIS.Close();

            // add states layer
            ll = new TGIS_LayerSHP();
            ll.Path = "encoded.shp";
            ll.Name = "encoded";
            ll.ReadEvent += new TGIS_ReadWriteEvent(this.doRead);
            ll.WriteEvent += new TGIS_ReadWriteEvent(this.doWrite);
            ll.Params.Labels.Field = "NAME";
            ll.Params.Area.Color = TGIS_Color.Green;
            GIS.Add(ll);
            GIS.FullExtent();
        }

        // do decoding with incrementing XOR value
        private void doRead(object _sender, TGIS_ReadWriteEventArgs _e)
        {
            for (int i = 0; i < _e.Count; i++)
                _e.Buffer[i] = (byte)(_e.Buffer[i] ^ ((_e.Pos + i) % 256));
        }

        // do encoding with incrementing XOR value
        private void doWrite(object _sender, TGIS_ReadWriteEventArgs _e)
        {
            for (int i = 0; i < _e.Count; i++)
                _e.Buffer[i] = (byte)(_e.Buffer[i] ^ ((_e.Pos + i) % 256));
        }
    }
}
