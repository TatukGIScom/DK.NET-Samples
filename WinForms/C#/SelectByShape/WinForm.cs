using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace SelectByShape
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
        private System.Windows.Forms.TextBox textBox1;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private System.Drawing.Point oldPos;
        private System.Drawing.Point oldPos2;
        private double oldRadius;
        private Panel panel1;
        private CheckBox btnRect;
        private CheckBox btnCircle;
        private TGIS_LayerVector ll;

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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRect = new System.Windows.Forms.CheckBox();
            this.btnCircle = new System.Windows.Forms.CheckBox();
            this.GIS.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Text = "Use left mouse button to select by chosen shape";
            this.toolStripLabel1.Width = 575;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBox1.Location = new System.Drawing.Point(408, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(184, 447);
            this.textBox1.TabIndex = 1;
            // 
            // GIS
            // 
            this.GIS.Controls.Add(this.panel1);
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraPosition = null;
            this.GIS.CursorForCameraRotation = null;
            this.GIS.CursorForCameraXY = null;
            this.GIS.CursorForCameraXYZ = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.CursorForSunPosition = null;
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 0);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(408, 447);
            this.GIS.TabIndex = 2;
            this.GIS.UseRTree = false;
            this.GIS.PaintExtraEvent += new TatukGIS.NDK.TGIS_RendererEvent(this.GIS_PaintExtraEvent);
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            this.GIS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRect);
            this.panel1.Controls.Add(this.btnCircle);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 27);
            this.panel1.TabIndex = 0;
            // 
            // btnRect
            // 
            this.btnRect.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnRect.AutoSize = true;
            this.btnRect.Location = new System.Drawing.Point(65, 1);
            this.btnRect.Name = "btnRect";
            this.btnRect.Size = new System.Drawing.Size(76, 23);
            this.btnRect.TabIndex = 3;
            this.btnRect.Text = "By rectangle";
            this.btnRect.UseVisualStyleBackColor = true;
            this.btnRect.CheckedChanged += new System.EventHandler(this.btnRect_CheckedChanged);
            // 
            // btnCircle
            // 
            this.btnCircle.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnCircle.AutoSize = true;
            this.btnCircle.Location = new System.Drawing.Point(3, 1);
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(57, 23);
            this.btnCircle.TabIndex = 2;
            this.btnCircle.Text = "By circle";
            this.btnCircle.UseVisualStyleBackColor = true;
            this.btnCircle.CheckedChanged += new System.EventHandler(this.btnCircle_CheckedChanged);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Select by shape";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.GIS.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            GIS.Lock();
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\Counties.SHP");
            ll = new TGIS_LayerVector();
            ll.Params.Area.Color = TGIS_Color.Blue;
            ll.Transparency = 50;
            ll.Name = "Points";
            ll.CS = GIS.CS;
            GIS.Add(ll);

            ll = new TGIS_LayerVector();
            ll.Params.Area.Color = TGIS_Color.Blue;
            ll.Params.Area.OutlineColor = TGIS_Color.Blue;
            ll.Transparency = 60;
            ll.Name = "Buffers";
            ll.CS = GIS.CS;
            GIS.Add(ll);
            GIS.Unlock();

            btnRect.Checked = true;
        }

        private void GIS_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GIS.IsEmpty) return;

            if (e.Button == MouseButtons.Right)
            {
                GIS.Mode = TGIS_ViewerMode.Zoom;
                return;
            }
            oldPos = new Point(e.X, e.Y);
            oldPos2 = new Point(e.X, e.Y);
            oldRadius = 0;
        }

        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GIS.IsEmpty) return;

            if (GIS.Mode != TGIS_ViewerMode.Select) return;
            if (!(e.Button == MouseButtons.Left)) return;

            if (btnRect.Checked == true)
            {
                oldPos2 = new Point(e.X, e.Y);
            }

            if (btnCircle.Checked == true)
            {
                oldRadius = (int)Math.Round(Math.Sqrt(Math.Pow(oldPos.X - e.X, 2) + Math.Pow(oldPos.Y - e.Y, 2)));
            }

            GIS.Invalidate();
        }

        private void GIS_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_Topology tpl;
            TGIS_LayerVector ll;
            TGIS_Shape tmp;
            TGIS_Shape buf;
            TGIS_Point ptg = new TGIS_Point();
            TGIS_Point ptg1 = new TGIS_Point();
            TGIS_Point ptg2 = new TGIS_Point();
            double distance;

            if (GIS.IsEmpty) return;

            if (e.Button == MouseButtons.Right)
            {
                GIS.Mode = TGIS_ViewerMode.Select;
                return;
            }

            if (btnRect.Checked == true)
            {
                if ((oldPos2.X == oldPos.X) && (oldPos2.Y == oldPos.Y)) return;
            }
            if (btnCircle.Checked == true)
            {
                if (oldRadius == 0) return;
            }

            ll = (TGIS_LayerVector)GIS.Get("Points");
            ll.Lock();

            tmp = ll.CreateShape(TGIS_ShapeType.Point);

            if (btnCircle.Checked)
            {
                ptg = GIS.ScreenToMap(oldPos);
                tmp = ll.CreateShape(TGIS_ShapeType.Point);
                tmp.Params.Marker.Size = 0;
                tmp.Lock(TGIS_Lock.Extent);
                tmp.AddPart();
                tmp.AddPoint(ptg);
                tmp.Unlock();
                ll.Unlock();
                ptg1 = GIS.ScreenToMap(new Point(oldPos.X + (int)oldRadius, e.Y));
            }

            if (btnRect.Checked)
            {
                ptg = GIS.ScreenToMap(oldPos);
                tmp = ll.CreateShape(TGIS_ShapeType.Point);
                tmp.Params.Marker.Size = 0;
                tmp.Lock(TGIS_Lock.Extent);
                tmp.AddPart();
                tmp.AddPoint(ptg);
                tmp.Unlock();
                ptg = GIS.ScreenToMap(oldPos2);
                tmp.AddPoint(ptg);
                ll.Unlock();
                ptg1 = GIS.ScreenToMap(oldPos);
            }
            buf = ll.CreateShape(TGIS_ShapeType.Unknown);

            ll = (TGIS_LayerVector)GIS.Get("Buffers");
            ll.RevertShapes();


            if (btnCircle.Checked)
            {
                distance = ptg1.X - ptg.X;

                tpl = new TGIS_Topology();
                buf = tpl.MakeBuffer(tmp, distance, 32, true);
                buf = ll.AddShape(buf);

            }

            if (btnRect.Checked)
            {
                ptg2 = GIS.ScreenToMap(oldPos2);
                buf = ll.CreateShape(TGIS_ShapeType.Polygon);
                buf.AddPart();
                buf.AddPoint(ptg1);
                buf.AddPoint(TGIS_Utils.GisPoint(ptg1.X, ptg2.Y));
                buf.AddPoint(ptg2);
                buf.AddPoint(TGIS_Utils.GisPoint(ptg2.X, ptg1.Y));
            }

            ll = (TGIS_LayerVector)GIS.Items[0];
            if (ll == null)
            {
                GIS.InvalidateWholeMap();
                return;
            }

            ll.DeselectAll();

            textBox1.Clear();

            GIS.InvalidateWholeMap();
            GIS.Lock();
            tmp = ll.FindFirst(buf.Extent, "", buf, TGIS_Utils.GIS_RELATE_INTERSECT());
            while (tmp != null)
            {
                textBox1.AppendText(tmp.GetField("name").ToString() + "\r\n");
                tmp.IsSelected = true;
                tmp = ll.FindNext();
            }
            GIS.Unlock();
        }

        private void GIS_PaintExtraEvent(object _sender, TGIS_RendererEventArgs _e)
        {
            TGIS_RendererAbstract rdr;
            Random rnd;

            rnd = new Random();
            rdr = _e.Renderer;
            rdr.CanvasPen.Width = 1;
            rdr.CanvasPen.Color = TGIS_Color.FromBGR((uint)rnd.Next(0xFFFFFF));
            rdr.CanvasPen.Style = TGIS_PenStyle.Solid;
            rdr.CanvasBrush.Style = TGIS_BrushStyle.Clear;

            if (btnRect.Checked)
            {
                if ((oldPos.X == oldPos2.X) && (oldPos.Y == oldPos2.Y)) return;

                rdr.CanvasDrawRectangle(new Rectangle(oldPos.X, oldPos.Y, oldPos2.X - oldPos.X, oldPos2.Y - oldPos.Y));
            }
            if (btnCircle.Checked)
            {
                rdr.CanvasDrawEllipse(oldPos.X - (int)Math.Round(oldRadius), oldPos.Y - (int)Math.Round(oldRadius), (int)oldRadius * 2, (int)oldRadius * 2);
            }
        }

        private void btnRect_CheckedChanged(object sender, EventArgs e)
        {
            btnCircle.Checked = !btnRect.Checked;
        }

        private void btnCircle_CheckedChanged(object sender, EventArgs e)
        {
            btnRect.Checked = !btnCircle.Checked;
        }
    }
}
