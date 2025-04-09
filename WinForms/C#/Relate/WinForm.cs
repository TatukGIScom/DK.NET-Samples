using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace Relate
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Relations;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ShapeA;
        private System.Windows.Forms.Label ShapeB;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TGIS_Shape shpA;
        private TGIS_Shape shpB;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ShapeB = new System.Windows.Forms.Label();
            this.ShapeA = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Relations = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 446);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 20);
            this.stripBar1.TabIndex = 0;
            this.stripBar1.Text = "Use left and right mouse button to select two shapes for relations";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 446);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ShapeB);
            this.groupBox2.Controls.Add(this.ShapeA);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(4, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(156, 55);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shapes";
            // 
            // ShapeB
            // 
            this.ShapeB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShapeB.ForeColor = System.Drawing.Color.Red;
            this.ShapeB.Location = new System.Drawing.Point(66, 32);
            this.ShapeB.Name = "ShapeB";
            this.ShapeB.Size = new System.Drawing.Size(67, 13);
            this.ShapeB.TabIndex = 3;
            this.ShapeB.Text = "Unselected";
            // 
            // ShapeA
            // 
            this.ShapeA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShapeA.ForeColor = System.Drawing.Color.Blue;
            this.ShapeA.Location = new System.Drawing.Point(66, 16);
            this.ShapeA.Name = "ShapeA";
            this.ShapeA.Size = new System.Drawing.Size(67, 13);
            this.ShapeA.TabIndex = 2;
            this.ShapeA.Text = "Unselected";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(8, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Shape B :";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shape A :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Relations);
            this.groupBox1.Location = new System.Drawing.Point(4, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 248);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Relations between A and B";
            // 
            // Relations
            // 
            this.Relations.BackColor = System.Drawing.SystemColors.Window;
            this.Relations.Location = new System.Drawing.Point(8, 16);
            this.Relations.Multiline = true;
            this.Relations.Name = "Relations";
            this.Relations.ReadOnly = true;
            this.Relations.Size = new System.Drawing.Size(140, 223);
            this.Relations.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(40, 312);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "CHECK";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(168, 0);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.SelectionTransparency = 0;
            this.GIS.Size = new System.Drawing.Size(424, 446);
            this.GIS.TabIndex = 2;
            this.GIS.UseRTree = false;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stripBar1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Sample - Shapes Relations ";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
            // open project
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\Samples\Topology\topology2.shp");

            // set style params
            ((TGIS_LayerVector)GIS.Items[0]).ParamsList.Add();
            ((TGIS_LayerVector)GIS.Items[0]).Params.Style = "selected";
            ((TGIS_LayerVector)GIS.Items[0]).Params.Area.OutlineWidth = 1;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if ((shpA == null) || (shpB == null)) return;
            Relations.Clear();

            // check all relations
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_EQUALITY()))
                Relations.AppendText("EQUALITY" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_DISJOINT()))
                Relations.AppendText("DISJOINT" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_INTERSECT()))
                Relations.AppendText("INTERSECT" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_INTERSECT1()))
                Relations.AppendText("INTERSECT1" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_INTERSECT2()))
                Relations.AppendText("INTERSECT2" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_INTERSECT3()))
                Relations.AppendText("INTERSECT3" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_WITHIN()))
                Relations.AppendText("WITHIN" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_CROSS()))
                Relations.AppendText("CROSS" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_CROSS_LINE()))
                Relations.AppendText("CROSS_LINE" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_TOUCH()))
                Relations.AppendText("TOUCH" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_TOUCH_INTERIOR()))
                Relations.AppendText("TOUCH_INTERIOR" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_CONTAINS()))
                Relations.AppendText("CONTAINS" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_OVERLAP()))
                Relations.AppendText("OVERLAP" + "\r\n");
            if (shpA.Relate(shpB, TGIS_Utils.GIS_RELATE_OVERLAP_LINE()))
                Relations.AppendText("OVERLAP_LINE" + "\r\n");
        }

        private void GIS_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Point ptg;
            TGIS_Shape shp;

            if (GIS.IsEmpty) return;
            if (GIS.IsEmpty) return;

            // let's locate a shape after click
            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            shp = (TGIS_Shape)GIS.Locate(ptg, 5 / GIS.Zoom); // 5 pixels precision
            if (shp == null) return;

            shp = shp.MakeEditable();

            if (e.Button == MouseButtons.Left)
            {
                // if selected shapeA, deselect it
                if (shpA != null)
                {
                    shpA.Params.Area.Color = TGIS_Color.Gray;
                    shpA.Params.Labels.Value = "";
                    shpA.Invalidate();
                    ShapeA.Text = "Unselected";
                }

                shpA = shp;
                shpA.Params.Area.Color = TGIS_Color.Blue;
                shpA.Params.Labels.Value = "Shape A";
                shpA.Params.Labels.Position = TGIS_LabelPosition.UpLeft;
                shpA.Invalidate();
                ShapeA.Text = "Selected";
            }

            if (e.Button == MouseButtons.Right)
            {
                // if selected shapeB, deselect it
                if (shpB != null)
                {
                    shpB.Params.Area.Color = TGIS_Color.Gray;
                    shpB.Params.Labels.Value = "";
                    shpB.Invalidate();
                    ShapeB.Text = "Unselected";
                }

                shpB = shp;
                shpB.Params.Area.Color = TGIS_Color.Red;
                shpB.Params.Labels.Value = "Shape B";
                shpB.Params.Labels.Position = TGIS_LabelPosition.UpLeft;
                shpB.Invalidate();
                ShapeB.Text = "Selected";
            }
        }
    }
}
