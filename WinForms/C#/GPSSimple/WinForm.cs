using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace GPSSimple
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
        private System.Windows.Forms.ComboBox cbxCom;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbxBaud;
        private TatukGIS.NDK.WinForms.TGIS_GpsNmea GPS;
        private System.Windows.Forms.TextBox textBox1;

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
            this.toolBar1 = new System.Windows.Forms.ToolStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxBaud = new System.Windows.Forms.ComboBox();
            this.cbxCom = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.GPS = new TatukGIS.NDK.WinForms.TGIS_GpsNmea();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.AutoSize = false;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowItemToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(594, 33);
            this.toolBar1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxBaud);
            this.panel1.Controls.Add(this.cbxCom);
            this.panel1.Controls.Add(this.toolBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 33);
            this.panel1.TabIndex = 1;
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
            this.cbxBaud.Location = new System.Drawing.Point(67, 6);
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
            this.cbxCom.Location = new System.Drawing.Point(0, 6);
            this.cbxCom.Name = "cbxCom";
            this.cbxCom.Size = new System.Drawing.Size(67, 21);
            this.cbxCom.TabIndex = 1;
            this.cbxCom.TabStop = false;
            this.cbxCom.SelectedIndexChanged += new System.EventHandler(this.cbxCom_SelectedIndexChanged);
            // 
            // GPS
            // 
            this.GPS.Active = false;
            this.GPS.BaudRate = 4800;
            this.GPS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GPS.Com = 1;
            this.GPS.Dock = System.Windows.Forms.DockStyle.Left;
            this.GPS.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GPS.Location = new System.Drawing.Point(0, 33);
            this.GPS.Name = "GPS";
            this.GPS.Size = new System.Drawing.Size(241, 435);
            this.GPS.TabIndex = 2;
            this.GPS.Text = " ";
            this.GPS.Timeout = 1000;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(241, 33);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(353, 435);
            this.textBox1.TabIndex = 3;
            this.textBox1.TabStop = false;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(594, 468);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.GPS);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - GPS Interface";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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

        private void cbxCom_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GPS.Com = cbxCom.SelectedIndex + 1;
            GPS.Active = true;
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            int i;

            cbxCom.SelectedIndex = GPS.Com - 1;

            for (i = 0; i < cbxBaud.Items.Count; i++)
            {
                if (Int32.Parse((string)cbxBaud.Items[i]) == GPS.BaudRate)
                {
                    cbxBaud.SelectedIndex = i;
                    break;
                }
            }
            GPS.Active = true;
        }

        private void cbxBaud_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            GPS.BaudRate = Int32.Parse((string)cbxBaud.Items[cbxBaud.SelectedIndex]);
            GPS.Active = true;
        }

        private void GPS_Position(object sender, System.EventArgs e)
        {
            string str;

            str = String.Format("{0} {1:F4} {2:F4}",
                                                     DateTime.Now.ToLocalTime().ToString(),
                                                     GPS.Longitude * (180 / Math.PI),
                                                     GPS.Latitude * (180 / Math.PI)
                                                 );
            textBox1.AppendText(str + "\n");
        }
    }
}
