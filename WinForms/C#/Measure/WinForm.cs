using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Measure
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
        private TGIS_LayerVector ll;
        private Boolean isLine;
        private Boolean isPolygon;
        private Panel panel1;
        private Panel panel2;
        private TGIS_ViewerWnd GIS;
        private TextBox tbArea;
        private TextBox tbLength;
        private Label lblArea;
        private Label lblLength;
        private Button btnLine;
        private Button btnPolygon;
        private Button btnClear;
        private TGIS_CSUnits unit;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbArea = new System.Windows.Forms.TextBox();
            this.tbLength = new System.Windows.Forms.TextBox();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.btnLine = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPolygon = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripLabel[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(686, 19);
            this.stripBar1.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Use left mouse button to measure";
            this.toolStripLabel1.Width = 669;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPolygon);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnLine);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 27);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tbArea);
            this.panel2.Controls.Add(this.tbLength);
            this.panel2.Controls.Add(this.lblArea);
            this.panel2.Controls.Add(this.lblLength);
            this.panel2.Location = new System.Drawing.Point(474, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 396);
            this.panel2.TabIndex = 1;
            // 
            // tbArea
            // 
            this.tbArea.Location = new System.Drawing.Point(6, 99);
            this.tbArea.Name = "tbArea";
            this.tbArea.Size = new System.Drawing.Size(191, 20);
            this.tbArea.TabIndex = 3;
            // 
            // tbLength
            // 
            this.tbLength.Location = new System.Drawing.Point(6, 32);
            this.tbLength.Name = "tbLength";
            this.tbLength.Size = new System.Drawing.Size(191, 20);
            this.tbLength.TabIndex = 2;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = true;
            this.lblArea.Location = new System.Drawing.Point(3, 83);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(32, 13);
            this.lblArea.TabIndex = 1;
            this.lblArea.Text = "Area:";
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(3, 16);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(43, 13);
            this.lblLength.TabIndex = 0;
            this.lblLength.Text = "Length:";
            // 
            // GIS
            // 
            this.GIS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraPosition = null;
            this.GIS.CursorForCameraRotation = null;
            this.GIS.CursorForCameraXY = null;
            this.GIS.CursorForCameraXYZ = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.CursorForSunPosition = null;
            this.GIS.Location = new System.Drawing.Point(15, 45);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(453, 396);
            this.GIS.TabIndex = 2;
            this.GIS.UseRTree = false;
            this.GIS.EditorChangeEvent += new System.EventHandler(this.GIS_EditorChangeEvent);
            this.GIS.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseClick);
            // 
            // btnLine
            // 
            this.btnLine.Location = new System.Drawing.Point(3, 0);
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(75, 23);
            this.btnLine.TabIndex = 5;
            this.btnLine.Text = "By line";
            this.btnLine.UseVisualStyleBackColor = true;
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(165, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPolygon
            // 
            this.btnPolygon.Location = new System.Drawing.Point(84, 0);
            this.btnPolygon.Name = "btnPolygon";
            this.btnPolygon.Size = new System.Drawing.Size(75, 23);
            this.btnPolygon.TabIndex = 7;
            this.btnPolygon.Text = "By polygon";
            this.btnPolygon.UseVisualStyleBackColor = true;
            this.btnPolygon.Click += new System.EventHandler(this.btnPolygon_Click);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(686, 466);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Measure";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
            GIS.Lock();
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\WorldDCW\world.shp");

            ll = new TGIS_LayerVector();
            ll.Params.Line.Color = TGIS_Color.Red;
            ll.Params.Line.Width = 25;
            ll.SetCSByEPSG( 4326 );

            GIS.Add(ll);
            GIS.RestrictedExtent = GIS.Extent;

            GIS.Unlock();

            unit = TGIS_Utils.CSUnitsList.ByEPSG( 904201 );

            isLine = false;
            isPolygon = false;

            GIS.Editor.EditingLinesStyle.PenWidth = 10;
            GIS.Editor.Mode = TGIS_EditorMode.AfterActivePoint;
        }
         

        private void btnPolygon_Click(object sender, EventArgs e)
        {
            GIS.Editor.DeleteShape();
            GIS.Editor.EndEdit();

            tbArea.Text = "";
            tbLength.Text = "";

            isPolygon = true;
            isLine = false;

            GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void btnLine_Click(object sender, EventArgs e)
        {
            GIS.Editor.DeleteShape();
            GIS.Editor.EndEdit();

            tbArea.Text = "";
            tbLength.Text = "";

            isPolygon = false;
            isLine = true;

            GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GIS.Editor.DeleteShape();
            GIS.Editor.EndEdit();

            tbArea.Text = "";
            tbLength.Text = "";

            GIS.Mode = TGIS_ViewerMode.Drag;
        }

        private void GIS_EditorChangeEvent(object sender, EventArgs e)
        {
            if (GIS.Editor.CurrentShape != null)
            {
                if (isLine)
                {
                    tbLength.Text = unit.AsLinear(((TGIS_Shape)GIS.Editor.CurrentShape).LengthCS(), true);
                }else if (isPolygon)
                {
                    tbLength.Text = unit.AsLinear(((TGIS_Shape)GIS.Editor.CurrentShape).LengthCS(), true);
                    tbArea.Text = unit.AsAreal(((TGIS_Shape)GIS.Editor.CurrentShape).AreaCS(), true, "%s²");
                }

            }
        }

        private void GIS_MouseClick(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;

            if (GIS.Mode == TGIS_ViewerMode.Edit)
                return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));

            if (isLine)
            {
                GIS.Editor.CreateShape(ll, ptg, TGIS_ShapeType.Arc);
            }else if (isPolygon)
            {
                GIS.Editor.CreateShape(ll, ptg, TGIS_ShapeType.Polygon);
            }

            GIS.Mode = TGIS_ViewerMode.Edit;
        }
    }
}
