using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace CGMViewer
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TatukGIS.NDK.TGIS_Shape shp;
        private System.Windows.Forms.ToolStripPanel toolStripPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox listBox1;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(WinForm));
            toolStripPanel1 = new ToolStripPanel();
            statusStrip1 = new StatusStrip();
            GIS = new TGIS_ViewerWnd();
            panel1 = new Panel();
            button1 = new Button();
            listBox1 = new ListBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Location = new Point(0, 444);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(592, 22);
            statusStrip1.TabIndex = 3;
            // 
            // toolStripPanel1
            // 
            toolStripPanel1.Location = new Point(0, 0);
            toolStripPanel1.Name = "toolStripPanel1";
            toolStripPanel1.Orientation = Orientation.Horizontal;
            toolStripPanel1.RowMargin = new Padding(3, 0, 0, 0);
            toolStripPanel1.Size = new Size(575, 0);
            // 
            // GIS
            // 
            GIS.AutoStyle = false;
            GIS.BackColor = Color.FromArgb(255, 255, 255);
            GIS.Dock = DockStyle.Fill;
            GIS.Level = 28.140189979287609D;
            GIS.Location = new Point(161, 29);
            GIS.Name = "GIS";
            GIS.SelectionColor = Color.FromArgb(255, 0, 0);
            GIS.Size = new Size(431, 415);
            GIS.TabIndex = 1;
            GIS.TiledPaint = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(592, 29);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Control;
            button1.Cursor = Cursors.Hand;
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(120, 22);
            button1.TabIndex = 0;
            button1.TabStop = false;
            button1.Text = "Rotate symbol";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Left;
            listBox1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            listBox1.IntegralHeight = false;
            listBox1.ItemHeight = 13;
            listBox1.Location = new Point(0, 29);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(161, 415);
            listBox1.TabIndex = 4;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // WinForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Window;
            ClientSize = new Size(592, 466);
            Controls.Add(GIS);
            Controls.Add(listBox1);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(200, 120);
            Name = "WinForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TatukGIS Samples - CGM Viewer";
            Load += WinForm_Load;
            Resize += WinForm_Resize;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

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
            DirectoryInfo dir;
            TGIS_LayerVector ll;

            // load list box
            dir = new DirectoryInfo(TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\");
            foreach (FileInfo fileItem in dir.GetFiles("*.cgm"))
            {
                listBox1.Items.Add(fileItem.Name);
            }

            // new layer as a grid
            ll = new TGIS_LayerVector();
            GIS.Add(ll);
            ll.Extent = TGIS_Utils.GisExtent(-90, -90, 90, 90);
            GIS.FullExtent();

            // add coordinate layout
            shp = ll.CreateShape(TGIS_ShapeType.Arc, TGIS_DimensionType.XY);
            shp.Params.Line.Width = 1;
            shp.AddPart();
            shp.AddPoint(new TGIS_Point(-90, 0));
            shp.AddPoint(new TGIS_Point(90, 0));

            shp = ll.CreateShape(TGIS_ShapeType.Arc, TGIS_DimensionType.XY);
            shp.Params.Line.Width = 1;
            shp.AddPart();
            shp.AddPoint(new TGIS_Point(0, -90));
            shp.AddPoint(new TGIS_Point(0, 90));

            shp = ll.CreateShape(TGIS_ShapeType.Point, TGIS_DimensionType.XY);
            shp.AddPart();
            shp.AddPoint(new TGIS_Point(0, 0));
        }

        private void WinForm_Resize(object sender, System.EventArgs e)
        {
            drawSymbol();
        }

        private void drawSymbol()
        {
            int w, h;
            if (shp == null) return;
            // create a symbol list
            shp.Params.Marker.Symbol = TGIS_Utils.SymbolList.Prepare(
                                         TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\" +
                                         listBox1.Items[listBox1.SelectedIndex]
                                       );

            // calculate symbol size
            if (shp.Params.Marker.Symbol != null)
            {
                shp.Params.Marker.Size = -Math.Min(GIS.Width, GIS.Height) * 2 / 3;

                // prepare to obtain computed width/height
                shp.Params.Marker.Symbol.Prepare(
                    GIS, -5,
                    TGIS_Color.Black,
                    TGIS_Color.Black,
                    0, 0,
                    TGIS_LabelPosition.MiddleCenter,
                    true
                );
                w = shp.Params.Marker.Symbol.Width;
                h = shp.Params.Marker.Symbol.Height;
                shp.Params.Marker.Symbol.Unprepare();

                if (h < w)
                    shp.Params.Marker.Size = shp.Params.Marker.Size * h / w;
            }
            else
                shp.Params.Marker.Size = 0;

            // set attributes
            shp.Params.Marker.Color = TGIS_Color.RenderColor;
            shp.Params.Marker.OutlineColor = TGIS_Color.RenderColor;
            GIS.InvalidateWholeMap();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            // rotate symbol
            shp.Params.Marker.SymbolRotate = shp.Params.Marker.SymbolRotate + Math.PI / 2;
            shp.Invalidate();
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //statusStrip1.Items[0].Text = TGIS_Utils.GisSamplesDataDirDownload() + listBox1.Items[listBox1.SelectedIndex];
            drawSymbol();
        }
    }
}
