using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace DynamicAggregation
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Panel pMenu;
        private TGIS_ViewerWnd GIS;
        private ComboBox cbxThreshhold;
        private Label lblThreshhold;
        private ComboBox cbxRadius;
        private Label lblRadius;
        private ComboBox cbxMethod;
        private Label lblMethod;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.pMenu = new System.Windows.Forms.Panel();
            this.cbxThreshhold = new System.Windows.Forms.ComboBox();
            this.lblThreshhold = new System.Windows.Forms.Label();
            this.cbxRadius = new System.Windows.Forms.ComboBox();
            this.lblRadius = new System.Windows.Forms.Label();
            this.cbxMethod = new System.Windows.Forms.ComboBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.pMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pMenu
            // 
            this.pMenu.Controls.Add(this.cbxThreshhold);
            this.pMenu.Controls.Add(this.lblThreshhold);
            this.pMenu.Controls.Add(this.cbxRadius);
            this.pMenu.Controls.Add(this.lblRadius);
            this.pMenu.Controls.Add(this.cbxMethod);
            this.pMenu.Controls.Add(this.lblMethod);
            this.pMenu.Location = new System.Drawing.Point(12, 12);
            this.pMenu.Name = "pMenu";
            this.pMenu.Size = new System.Drawing.Size(200, 442);
            this.pMenu.TabIndex = 0;
            // 
            // cbxThreshhold
            // 
            this.cbxThreshhold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxThreshhold.FormattingEnabled = true;
            this.cbxThreshhold.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "5",
            "10"});
            this.cbxThreshhold.Location = new System.Drawing.Point(3, 96);
            this.cbxThreshhold.Name = "cbxThreshhold";
            this.cbxThreshhold.Size = new System.Drawing.Size(194, 21);
            this.cbxThreshhold.TabIndex = 5;
            this.cbxThreshhold.SelectedIndexChanged += new System.EventHandler(this.cbxThreshhold_SelectedIndexChanged);
            // 
            // lblThreshhold
            // 
            this.lblThreshhold.AutoSize = true;
            this.lblThreshhold.Location = new System.Drawing.Point(0, 80);
            this.lblThreshhold.Name = "lblThreshhold";
            this.lblThreshhold.Size = new System.Drawing.Size(63, 13);
            this.lblThreshhold.TabIndex = 4;
            this.lblThreshhold.Text = "Threshhold:";
            // 
            // cbxRadius
            // 
            this.cbxRadius.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRadius.FormattingEnabled = true;
            this.cbxRadius.Items.AddRange(new object[] {
            "5 pt",
            "10 pt",
            "20 pt",
            "40 pt",
            "80 pt"});
            this.cbxRadius.Location = new System.Drawing.Point(3, 56);
            this.cbxRadius.Name = "cbxRadius";
            this.cbxRadius.Size = new System.Drawing.Size(194, 21);
            this.cbxRadius.TabIndex = 3;
            this.cbxRadius.SelectedIndexChanged += new System.EventHandler(this.cbxRadius_SelectedIndexChanged);
            // 
            // lblRadius
            // 
            this.lblRadius.AutoSize = true;
            this.lblRadius.Location = new System.Drawing.Point(0, 40);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(43, 13);
            this.lblRadius.TabIndex = 2;
            this.lblRadius.Text = "Radius:";
            // 
            // cbxMethod
            // 
            this.cbxMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMethod.FormattingEnabled = true;
            this.cbxMethod.Location = new System.Drawing.Point(3, 16);
            this.cbxMethod.Name = "cbxMethod";
            this.cbxMethod.Size = new System.Drawing.Size(194, 21);
            this.cbxMethod.TabIndex = 1;
            this.cbxMethod.SelectedIndexChanged += new System.EventHandler(this.cbxMethod_SelectedIndexChanged);
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Location = new System.Drawing.Point(0, 0);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(105, 13);
            this.lblMethod.TabIndex = 0;
            this.lblMethod.Text = "Aggregation method:";
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.Location = new System.Drawing.Point(218, 12);
            this.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Zoom;
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(751, 442);
            this.GIS.TabIndex = 1;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(981, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.pMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - DynamicAggregation";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.pMenu.ResumeLayout(false);
            this.pMenu.PerformLayout();
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
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\Aggregation\Aggregation.ttkproject");
            cbxMethod.Items.Add("Off");

            for( int i = 0; i < TGIS_DynamicAggregatorFactory.Names.Count; i++ )
                cbxMethod.Items.Add(TGIS_DynamicAggregatorFactory.Names[i]);

            cbxMethod.SelectedIndex = 0;
            cbxThreshhold.SelectedIndex = 1;
            
            cbxRadius.Enabled = false;
            cbxThreshhold.Enabled = false;
        }
        
        private void readDeafaultValues()
        {
            if (cbxMethod.SelectedItem.ToString().Equals("ShapeReduction"))
                cbxRadius.SelectedIndex = 0;
            else
                cbxRadius.SelectedIndex = 3;
        }

        private void changeAggregation()
        {
            String dyn_agg_name = cbxMethod.SelectedItem.ToString();
            TGIS_LayerVector lv = (TGIS_LayerVector)GIS.Get("cities");
            lv.Transparency = 70;

            if (dyn_agg_name.Equals("Off"))
            {
                cbxRadius.Enabled = false;
                cbxThreshhold.Enabled = false;
                lv.DynamicAggregator = null;
            }
            else
            {
                cbxRadius.Enabled = true;
                cbxThreshhold.Enabled = true;
                lv.DynamicAggregator = TGIS_DynamicAggregatorFactory.CreateInstance(dyn_agg_name, lv);
                lv.DynamicAggregator.RadiusAsText = "SIZE: " + cbxRadius.SelectedItem.ToString();
                lv.DynamicAggregator.Threshold = Int32.Parse(cbxThreshhold.SelectedItem.ToString());
            }

            GIS.InvalidateWholeMap();
        }

        private void cbxMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            readDeafaultValues();
            changeAggregation();
        }

        private void cbxRadius_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeAggregation();
        }

        private void cbxThreshhold_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeAggregation();
        }
    }
}
