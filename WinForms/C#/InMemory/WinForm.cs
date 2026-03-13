using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace InMemory
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
        private System.Windows.Forms.StatusStrip stripBar1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;

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
            this.stripBar1 = new System.Windows.Forms.StatusStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            //((System.ComponentModel.ISupportInitialize)(this.toolStripLabel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Width = 575;
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 418);
            this.GIS.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 29);
            this.panel1.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(240, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Animate";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(160, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(80, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Add Lines";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(80, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Add Points";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create Layer";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - In memory Layers";
            //((System.ComponentModel.ISupportInitialize)(this.toolStripLabel1)).EndInit();
            this.panel1.ResumeLayout(false);
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

        private void button1_Click(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;

            // create a layer loading symbols for marker and line
            ll = new TGIS_LayerVector();
            ll.Params.Marker.Symbol = TGIS_Utils.SymbolList.Prepare(
                                        TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\2267.cgm"
                                      );
            ll.Params.Marker.SymbolRotate = Math.PI / 2;
            ll.Params.Marker.Size = -20;
            ll.Params.Line.Symbol = TGIS_Utils.SymbolList.Prepare(
                                        TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\1301.cgm"
                                    );
            ll.Params.Line.Width = -5;
            GIS.Add(ll);
            ll.Extent = TGIS_Utils.GisExtent(-180, -90, 180, 90);
            GIS.FullExtent();
            stripBar1.Items[0].Text = " Layer created.";
            button1.Enabled = false;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            int i;
            TGIS_Shape shp;
            Random rnd;

            if (GIS.IsEmpty)
            {
                MessageBox.Show("Create a layer first !", "In Memory");
                return;
            }

            // fill the viewer with points
            rnd = new Random();
            for (i = 0; i < 100; i++)
            {
                shp = ((TGIS_LayerVector)GIS.Items[0]).CreateShape(TGIS_ShapeType.Point);
                // in radians
                shp.Params.Marker.SymbolRotate = rnd.Next(360) * (Math.PI / 180);

                shp.Params.Marker.Color = TGIS_Color.FromRGB((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
                shp.Params.Marker.OutlineColor = shp.Params.Marker.Color;
                shp.Lock(TGIS_Lock.Extent);
                shp.AddPart();
                shp.AddPoint(new TGIS_Point(rnd.Next(360) - 180,
                                              rnd.Next(180) - 90));
                shp.Unlock();
            }
            GIS.InvalidateWholeMap();
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            int i;
            TGIS_Shape shp;
            Random rnd;

            if (GIS.IsEmpty)
            {
                MessageBox.Show("Create a layer first !", "In Memory");
                return;
            }

            // add lines
            shp = ((TGIS_LayerVector)GIS.Items[0]).CreateShape(TGIS_ShapeType.Arc);
            rnd = new Random();
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            for (i = 0; i < 20; i++)
            {
                shp.AddPoint(new TGIS_Point(rnd.Next(360) - 180,
                                              rnd.Next(180) - 90));
            }
            shp.Unlock();
            GIS.InvalidateWholeMap();
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            int i;
            TGIS_Shape shp;

            if (GIS.IsEmpty)
            {
                MessageBox.Show("Create a layer first !", "In Memory");
                return;
            }

            // create a ship and fly
            shp = ((TGIS_LayerVector)GIS.Items[0]).CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(new TGIS_Point(0, 0));

            shp.Params.Marker.Color = TGIS_Color.Blue;
            shp.Params.Marker.OutlineColor = TGIS_Color.Blue;
            shp.Params.Marker.Size = -20;

            shp.Unlock();
            shp.Invalidate();

            for (i = 0; i < 90; i++)
            {
                if (this.IsDisposed)
                    break;
                shp.SetPosition(new TGIS_Point(i * 2, i), null, 0);
                Thread.Sleep(10);
                Application.DoEvents();
            }
        }
    }
}
