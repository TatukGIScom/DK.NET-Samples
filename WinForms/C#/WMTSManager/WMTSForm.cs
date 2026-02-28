using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;

namespace WMTSManager
{
    /// <summary>
    /// Summary description for WMTSForm.
    /// </summary>
    public class WMTSForm : System.Windows.Forms.Form
    {
        private Label lServers;
        private ComboBox cbxServers;
        private Button btnConnect;
        private Label lLayers;
        private ComboBox cbxLayers;
        private CheckBox cbInvertAxis;
        private Button btnAdd;
        private TGIS_Tokenizer tkn;
        private TGIS_ViewerWnd GIS;

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

        public WMTSForm()
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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(WMTSForm));
            lServers = new Label();
            cbxServers = new ComboBox();
            btnConnect = new Button();
            lLayers = new Label();
            cbxLayers = new ComboBox();
            cbInvertAxis = new CheckBox();
            btnAdd = new Button();
            SuspendLayout();
            // 
            // lServers
            // 
            lServers.AutoSize = true;
            lServers.Location = new Point(18, 14);
            lServers.Margin = new Padding(4, 0, 4, 0);
            lServers.Name = "lServers";
            lServers.Size = new Size(69, 25);
            lServers.TabIndex = 0;
            lServers.Text = "Servers";
            // 
            // cbxServers
            // 
            cbxServers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbxServers.FormattingEnabled = true;
            cbxServers.Items.AddRange(new object[] { "http://basemap.nationalmap.gov/arcgis/rest/services/USGSImageryOnly/MapServer/wmts", "http://garden.gis.vt.edu/arcgis/rest/services/VBMP2011/VBMP2011_Infrared_WGS/MapServer/WMTS/1.0.0/WMTSCapabilities.xml", "http://gis.oregonmetro.gov/services/wmts/1.0.0/WMTSGetCapabilities.xml", "http://maps.columbus.gov/arcgis/rest/services/Imagery/Imagery2013/MapServer/WMTS/1.0.0/WMTSCapabilities.xml", "https://mapy.geoportal.gov.pl/wss/service/PZGIK/ORTO/WMTS/StandardResolution" });
            cbxServers.Location = new Point(22, 38);
            cbxServers.Margin = new Padding(4, 4, 4, 4);
            cbxServers.Name = "cbxServers";
            cbxServers.Size = new Size(1104, 33);
            cbxServers.TabIndex = 1;
            // 
            // btnConnect
            // 
            btnConnect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConnect.Location = new Point(1137, 34);
            btnConnect.Margin = new Padding(4, 4, 4, 4);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(112, 34);
            btnConnect.TabIndex = 2;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // lLayers
            // 
            lLayers.AutoSize = true;
            lLayers.Location = new Point(18, 74);
            lLayers.Margin = new Padding(4, 0, 4, 0);
            lLayers.Name = "lLayers";
            lLayers.Size = new Size(61, 25);
            lLayers.TabIndex = 3;
            lLayers.Text = "Layers";
            // 
            // cbxLayers
            // 
            cbxLayers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbxLayers.FormattingEnabled = true;
            cbxLayers.Location = new Point(22, 98);
            cbxLayers.Margin = new Padding(4, 4, 4, 4);
            cbxLayers.Name = "cbxLayers";
            cbxLayers.Size = new Size(974, 33);
            cbxLayers.TabIndex = 4;
            // 
            // cbInvertAxis
            // 
            cbInvertAxis.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbInvertAxis.AutoSize = true;
            cbInvertAxis.Location = new Point(1002, 100);
            cbInvertAxis.Margin = new Padding(4, 4, 4, 4);
            cbInvertAxis.Name = "cbInvertAxis";
            cbInvertAxis.Size = new Size(117, 29);
            cbInvertAxis.TabIndex = 5;
            cbInvertAxis.Text = "Invert axis";
            cbInvertAxis.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.Location = new Point(1137, 94);
            btnAdd.Margin = new Padding(4, 4, 4, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(112, 34);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // WMTSForm
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1286, 165);
            Controls.Add(btnAdd);
            Controls.Add(cbInvertAxis);
            Controls.Add(cbxLayers);
            Controls.Add(lLayers);
            Controls.Add(btnConnect);
            Controls.Add(cbxServers);
            Controls.Add(lServers);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(200, 120);
            Margin = new Padding(4, 4, 4, 4);
            Name = "WMTSForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TatukGIS Samples";
            Load += WMTSForm_Load;
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Run()
        {
            Application.Run(new WMTSForm());
        }

        private void WMTSForm_Load(object sender, System.EventArgs e)
        {
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            TGIS_LayerWMTS wmts;
            TList<TGIS_LayerInfo> lst;

            wmts = new TGIS_LayerWMTS();
            wmts.Path = cbxServers.Text;
            try {
                lst = wmts.GetAvailableLayers();
                foreach (TGIS_LayerInfo li in lst)
                    cbxLayers.Items.Add(li.Name);
                if (cbxLayers.Items.Count > 0)
                    cbxLayers.SelectedIndex = 0;
            }
            catch ( EGIS_Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            TGIS_LayerWMTS wmts;
            TStrings layer;
            String str;
            char[] c;

            wmts = new TGIS_LayerWMTS();
            tkn = new TGIS_Tokenizer();

            str = cbxLayers.Text;
            c = new char[1];
            c[0] = ';';
            tkn.Execute(str, c);

            layer = tkn.Result;

            wmts.Path = "[TatukGIS Layer\n" +
                        "Storage=WMTS\n" +
                        "Layer=" + layer[0] + "\n" +
                        "Url=" + cbxServers.Text + "\n" +
                        "TileMatrixSet=" + layer[2] + "\n" +
                        "ImageFormat=" + layer[1] + "\n" +
                        "InvertAxis=" + cbInvertAxis.Checked.ToString() + "\n";

            GIS.Add(wmts);
            if (GIS.Items.Count == 1)
                GIS.FullExtent();
            else
                GIS.InvalidateWholeMap();

        }
    }
}
