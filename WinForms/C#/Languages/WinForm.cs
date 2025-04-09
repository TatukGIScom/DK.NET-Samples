using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Languages
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
        private System.Windows.Forms.ComboBox comboBox1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;

        private const string TXT_ENGLISH  = "Welcome" ;
        private const string TXT_CHINESE  = "欢迎" ;
        private const string TXT_JAPANESE = "ようこそ" ;
        private const string TXT_HEBREW   = "ברוך הבא" ;
        private const string TXT_GREEK    = "Καλώς ήλθατε" ;
        private const string TXT_ARABIC   = "أهلا بك" ;


        private System.Windows.Forms.Panel panel1;

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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Items.AddRange(new object[] {
            "English",
            "Chinese",
            "Japanese",
            "Arabic",
            "Hebrew",
            "Greek"});
            this.comboBox1.Location = new System.Drawing.Point(0, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(145, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 437);
            this.GIS.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 29);
            this.panel1.TabIndex = 1;
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS DK Samples - Languages";
            this.Load += new System.EventHandler(this.WinForm_Load);
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

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_Shape shp;

            ll = new TGIS_LayerVector();
            ll.Name = "points";
            ll.Params.Labels.Position = TGIS_LabelPosition.UpLeft;
            ll.Params.Labels.Allocator = false;
            ll.PaintShapeLabelEvent += new TGIS_ShapeEvent(PaintShapeLabel2);

            GIS.Add(ll);
            ll.Extent = TGIS_Utils.GisExtent(-180, -90, 180, 90);

            shp = ll.CreateShape(TGIS_ShapeType.Point);
            shp.AddPart();
            shp.AddPoint(new TGIS_Point(-45, -45));

            ll = new TGIS_LayerVector();
            ll.Name = "lines";
            ll.AddField("name", TGIS_FieldType.String, 256, 0);
            ll.Params.Labels.Alignment = TGIS_LabelAlignment.Follow;
            ll.Params.Labels.Color = TGIS_Color.Black;
            ll.Params.Labels.Font.Size = 12;
            ll.Params.Labels.Font.Color = TGIS_Color.Black;
            ll.Params.Labels.Allocator = false;

            GIS.Add(ll);
            ll.Extent = TGIS_Utils.GisExtent(-180, -90, 180, 90);

            shp = ll.CreateShape(TGIS_ShapeType.Arc);
            shp.AddPart();
            shp.AddPoint(new TGIS_Point(-90, 90));
            shp.AddPoint(new TGIS_Point(180, -90));
            ll.PaintShapeLabelEvent += new TGIS_ShapeEvent(PaintShapeLabel);

            GIS.FullExtent();

            comboBox1.SelectedIndex = 0;
        }
        private void PaintShapeLabel(object sender, TGIS_ShapeEventArgs e)
        {
            e.Shape.DrawLabel();
        }
        private void PaintShapeLabel2(object sender, TGIS_ShapeEventArgs e)
        {
            e.Shape.DrawLabel();
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            TGIS_LayerVector ll;
            String txt;

            switch (comboBox1.SelectedIndex)
            {
                case 1:  // Chinese
                    txt = TXT_CHINESE;
                    break;
                case 2:  // Japanse
                    txt = TXT_JAPANESE;
                    break;
                case 3:  // Arabic
                    txt = TXT_ARABIC;
                    break;
                case 4:  // Hebrew
                    txt = TXT_HEBREW;
                    break;
                case 5:  // Greek
                    txt = TXT_GREEK;
                    break;
                default:  // English
                    txt = TXT_ENGLISH;
                    break;
            }

            ll = (TGIS_LayerVector)GIS.Get("points");
            ll.Params.Labels.Value = String.Format("{0} {1}", txt, 1);

            ll = (TGIS_LayerVector)GIS.Get("lines");
            ll.Params.Labels.Value = String.Format("{0} {1}", txt, 2);

            GIS.InvalidateWholeMap();
        }
    }
}
