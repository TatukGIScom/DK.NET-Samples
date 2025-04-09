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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WMTSForm));
            this.lServers = new System.Windows.Forms.Label();
            this.cbxServers = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lLayers = new System.Windows.Forms.Label();
            this.cbxLayers = new System.Windows.Forms.ComboBox();
            this.cbInvertAxis = new System.Windows.Forms.CheckBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lServers
            // 
            this.lServers.AutoSize = true;
            this.lServers.Location = new System.Drawing.Point(12, 9);
            this.lServers.Name = "lServers";
            this.lServers.Size = new System.Drawing.Size(43, 13);
            this.lServers.TabIndex = 0;
            this.lServers.Text = "Servers";
            // 
            // cbxServers
            // 
            this.cbxServers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxServers.FormattingEnabled = true;
            this.cbxServers.Items.AddRange(new object[] {
            "http://basemap.nationalmap.gov/arcgis/rest/services/USGSImageryOnly/MapServer/wmt" +
                "s",
            "http://garden.gis.vt.edu/arcgis/rest/services/VBMP2011/VBMP2011_Infrared_WGS/MapS" +
                "erver/WMTS/1.0.0/WMTSCapabilities.xml",
            "http://geodata.nationaalgeoregister.nl/tiles/service/wmts/bgtstandaard?VERSION=1." +
                "0.0&request=GetCapabilities",
            "http://geodata.nationaalgeoregister.nl/tiles/service/wmts/brtachtergrondkaart?REQ" +
                "UEST=getcapabilities&amp;VERSION=1.0.0",
            "http://gis.oregonmetro.gov/services/wmts/1.0.0/WMTSGetCapabilities.xml",
            "http://hazards.fema.gov/gis/nfhl/rest/services/MapSearch/MapSearch_DFIRM_Tiles/Ma" +
                "pServer/WMTS",
            "http://kortforsyningen.kms.dk/orto_foraar?SERVICE=WMTS&request=GetCapabilities",
            "http://kortforsyningen.kms.dk/orto_foraar?VERSION=1.0.0&LAYER=orto_foraar&request" +
                "=GetCapabilities&SERVICE=WMTS&login=qgistest&password=qgistestpw",
            "http://maps.columbus.gov/arcgis/rest/services/Imagery/Imagery2013/MapServer/WMTS/" +
                "1.0.0/WMTSCapabilities.xml",
            "http://maps.edc.uri.edu/arcgis/rest/services/Atlas_elevation/Hillshade/MapServer/" +
                "WMTS/1.0.0/WMTSCapabilities.xml",
            "http://maps.warwickshire.gov.uk/gs/gwc/service/wmts?REQUEST=GetCapabilities",
            "http://maps.wien.gv.at/wmts/1.0.0/WMTSCapabilities.xml?request=GetCapabilities",
            "http://opencache.statkart.no/gatekeeper/gk/gk.open_wmts?Version=1.0.0&service=wmt" +
                "s&request=getcapabilities",
            "http://s1-mdc.cloud.eaglegis.co.nz/arcgis/rest/services/Cache/TopographicMaps/Map" +
                "Server/WMTS",
            "http://sdi.provinz.bz.it/geoserver/gwc/service/wmts?REQUEST=getcapabilities",
            "http://suite.opengeo.org/geoserver/gwc/service/wmts/?request=GetCapabilities",
            "http://tileserver.maptiler.com/wmts",
            "http://tryitlive.arcgis.com/arcgis/rest/services/ImageryHybrid/MapServer/WMTS/1.0" +
                ".0/WMTSCapabilities.xml",
            "http://webgis.arpa.piemonte.it/ags101free/rest/services/topografia_dati_di_base/S" +
                "fumo_Europa_WM/MapServer/WMTS",
            "http://www.basemap.at/wmts/1.0.0/WMTSCapabilities.xml",
            "http://www.wien.gv.at/wmts/1.0.0/WMTSCapabilities.xml"});
            this.cbxServers.Location = new System.Drawing.Point(15, 25);
            this.cbxServers.Name = "cbxServers";
            this.cbxServers.Size = new System.Drawing.Size(737, 21);
            this.cbxServers.TabIndex = 1;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(758, 23);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lLayers
            // 
            this.lLayers.AutoSize = true;
            this.lLayers.Location = new System.Drawing.Point(12, 49);
            this.lLayers.Name = "lLayers";
            this.lLayers.Size = new System.Drawing.Size(38, 13);
            this.lLayers.TabIndex = 3;
            this.lLayers.Text = "Layers";
            // 
            // cbxLayers
            // 
            this.cbxLayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxLayers.FormattingEnabled = true;
            this.cbxLayers.Location = new System.Drawing.Point(15, 65);
            this.cbxLayers.Name = "cbxLayers";
            this.cbxLayers.Size = new System.Drawing.Size(651, 21);
            this.cbxLayers.TabIndex = 4;
            // 
            // cbInvertAxis
            // 
            this.cbInvertAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInvertAxis.AutoSize = true;
            this.cbInvertAxis.Location = new System.Drawing.Point(672, 67);
            this.cbInvertAxis.Name = "cbInvertAxis";
            this.cbInvertAxis.Size = new System.Drawing.Size(74, 17);
            this.cbInvertAxis.TabIndex = 5;
            this.cbInvertAxis.Text = "Invert axis";
            this.cbInvertAxis.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(758, 63);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // WMTSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(857, 110);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbInvertAxis);
            this.Controls.Add(this.cbxLayers);
            this.Controls.Add(this.lLayers);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cbxServers);
            this.Controls.Add(this.lServers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WMTSForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples";
            this.Load += new System.EventHandler(this.WMTSForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
