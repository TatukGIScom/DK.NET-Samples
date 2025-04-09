using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace TwoWindows
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
        private System.Windows.Forms.CheckBox checkBox1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS_ViewerWnd1;
        private System.Windows.Forms.Splitter splitter1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS_ViewerWnd2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private bool bSentinel=false;

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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.GIS_ViewerWnd1 = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.GIS_ViewerWnd2 = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(91, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(97, 25);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Keep Zoom";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 29);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GIS_ViewerWnd1
            // 
            this.GIS_ViewerWnd1.AutoStyle = true;
            this.GIS_ViewerWnd1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS_ViewerWnd1.Dock = System.Windows.Forms.DockStyle.Left;
            this.GIS_ViewerWnd1.Level = 28.140189979287609D;
            this.GIS_ViewerWnd1.Location = new System.Drawing.Point(0, 29);
            this.GIS_ViewerWnd1.Mode = TatukGIS.NDK.TGIS_ViewerMode.Select;
            this.GIS_ViewerWnd1.Name = "GIS_ViewerWnd1";
            this.GIS_ViewerWnd1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS_ViewerWnd1.Size = new System.Drawing.Size(249, 437);
            this.GIS_ViewerWnd1.TabIndex = 1;
            this.GIS_ViewerWnd1.VisibleExtentChangeEvent += new System.EventHandler(this.GIS_ViewerWnd1_VisibleExtentChangeEvent);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(249, 29);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(8, 437);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // GIS_ViewerWnd2
            // 
            this.GIS_ViewerWnd2.AutoStyle = true;
            this.GIS_ViewerWnd2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS_ViewerWnd2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS_ViewerWnd2.Level = 28.140189979287609D;
            this.GIS_ViewerWnd2.Location = new System.Drawing.Point(257, 29);
            this.GIS_ViewerWnd2.Mode = TatukGIS.NDK.TGIS_ViewerMode.Select;
            this.GIS_ViewerWnd2.Name = "GIS_ViewerWnd2";
            this.GIS_ViewerWnd2.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS_ViewerWnd2.Size = new System.Drawing.Size(335, 437);
            this.GIS_ViewerWnd2.TabIndex = 3;
            this.GIS_ViewerWnd2.VisibleExtentChangeEvent += new System.EventHandler(this.GIS_ViewerWnd2_VisibleExtentChangeEvent);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS_ViewerWnd2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.GIS_ViewerWnd1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Two Windows";
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
            // open the same project for two viewers
            GIS_ViewerWnd1.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\poland.ttkproject", true);
            GIS_ViewerWnd1.Zoom = GIS_ViewerWnd1.Zoom * 3;
            GIS_ViewerWnd1.Mode = TGIS_ViewerMode.Zoom;

            GIS_ViewerWnd2.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\Poland\DCW\poland.ttkproject", true);
            GIS_ViewerWnd2.Zoom = GIS_ViewerWnd2.Zoom * 4;
            GIS_ViewerWnd2.Mode = TGIS_ViewerMode.Zoom;
        }

        private void GIS_ViewerWnd1_VisibleExtentChangeEvent(object sender, EventArgs e)
        {
            if (bSentinel) // avoid circular calls
                return;
            bSentinel = true;

            GIS_ViewerWnd2.Lock();

            GIS_ViewerWnd2.Center = GIS_ViewerWnd1.Center;

            if (checkBox1.Checked)
                GIS_ViewerWnd2.Zoom = GIS_ViewerWnd1.Zoom;
            
            GIS_ViewerWnd2.Unlock();

            bSentinel = false;
        }

        private void GIS_ViewerWnd2_VisibleExtentChangeEvent(object sender, EventArgs e)
        {
            if (bSentinel) // avoid circular calls
                return;
            bSentinel = true;

            GIS_ViewerWnd1.Lock();

            GIS_ViewerWnd1.Center = GIS_ViewerWnd2.Center;

            if (checkBox1.Checked)
                GIS_ViewerWnd1.Zoom = GIS_ViewerWnd2.Zoom;

            GIS_ViewerWnd1.Unlock();

            bSentinel = false;
        }
    }
}
