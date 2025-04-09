using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;

namespace DragLabel
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
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private const string LABEL_TEXT = "Ship ";
        private ToolStripButton toolStripButton1;
        private ToolStrip toolStrip1;
        private TGIS_Shape currShape;


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
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SuspendLayout();
            // 
            // stripBar1
            // 
            this.stripBar1.Location = new System.Drawing.Point(0, 447);
            this.stripBar1.Name = "stripBar1";
            this.stripBar1.Size = new System.Drawing.Size(592, 19);
            this.stripBar1.TabIndex = 0;
            // 
            // GIS
            // 
            this.GIS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GIS.Location = new System.Drawing.Point(0, 29);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(592, 418);
            this.GIS.TabIndex = 2;
            this.GIS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseDown);
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            this.GIS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseUp);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Text = "Animate";
            this.toolStripButton1.Click += toolStrip1_ButtonClick;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripButton[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = true;
            this.toolStrip1.Size = new System.Drawing.Size(592, 29);
            this.toolStrip1.TabIndex = 1;
            //this.toolStrip1.ButtonClick += new System.Windows.Forms.ToolStripButtonClickEventHandler(this.toolStrip1_ButtonClick);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(592, 466);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.stripBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - Draggable Labels";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);

        }

        private void ToolStripButton1_Click(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            TGIS_Point ptg;
            Random rnd;
            int i;

            // create real point layer
            ll = new TGIS_LayerVector();
            ll.Params.Marker.Symbol = TGIS_Utils.SymbolList.Prepare(
                                  TGIS_Utils.GisSamplesDataDirDownload() + @"\Symbols\2267.cgm"
                                );
            ll.Name = "realpoints";
            ll.CachedPaint = false;

            GIS.Add(ll);
            ll.AddField("name", TGIS_FieldType.String, 100, 0);
            ll.Extent = TGIS_Utils.GisExtent(-180, -180, 180, 180);

            // create display sidekick
            ll = new TGIS_LayerVector();
            ll.Name = "sidekicks";
            ll.Params.Marker.Size = 0;
            ll.Params.Labels.Position = TGIS_LabelPosition.MiddleCenter;

            GIS.Add(ll);
            ll.PaintShapeLabelEvent += new TGIS_ShapeEvent(doLabelPaint);
            ll.Params.Labels.Allocator = false;
            ll.CachedPaint = false;

            GIS.FullExtent();

            // add points
            rnd = new Random();
            for (i = 0; i < 20; i++)
            {
                ptg = new TGIS_Point(rnd.Next(360) - 180,
                                      rnd.Next(180) - 90
                                    );

                // add a real point
                shp = ((TGIS_LayerVector)GIS.Get("realpoints")).CreateShape(TGIS_ShapeType.Point, TGIS_DimensionType.XY);
                shp.Lock(TGIS_Lock.Extent);
                shp.AddPart();
                shp.AddPoint(ptg);
                shp.Params.Marker.SymbolRotate = shp.Uid;
                shp.Params.Marker.Size = 250;
                shp.Params.Marker.Color = TGIS_Color.FromRGB((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
                shp.Params.Marker.OutlineColor = shp.Params.Marker.Color;

                shp.SetField("name", String.Format(LABEL_TEXT + ": {0}", shp.Uid));
                shp.Unlock();

                // add sideckicks
                shp = ((TGIS_LayerVector)GIS.Get("sidekicks")).CreateShape(TGIS_ShapeType.Point);
                shp.Lock(TGIS_Lock.Extent);
                shp.AddPart();

                // with a small offset
                ptg.X = ptg.X + 15 / GIS.Zoom;
                ptg.Y = ptg.Y + 15 / GIS.Zoom;
                shp.AddPoint(ptg);
                shp.Unlock();
            }

            GIS.FullExtent();
        }

        private void toolStrip1_ButtonClick(object sender, System.EventArgs e)
        {
            int i;
            TGIS_Shape shp;

            shp = ((TGIS_LayerVector)GIS.Get("realpoints")).GetShape(5);
            for (i = 0; i < 90; i++)
            {
                if (this.IsDisposed)
                {
                    break;
                }
                synchroMove(shp, 2, 1);
                Thread.Sleep(50);
                Application.DoEvents();
            }

            //switch (toolStrip1.Buttons.IndexOf(e.Button))
            //{
            //    case 0:
            //        shp = ((TGIS_LayerVector)GIS.Get("realpoints")).GetShape(5);
            //        for (i = 0; i < 90; i++)
            //        {
            //            if (this.IsDisposed)
            //            {
            //                break;
            //            }
            //            synchroMove(shp, 2, 1);
            //            Thread.Sleep(50);
            //            Application.DoEvents();
            //        }
            //        break;
            //}
        }

        private void synchroMove(TGIS_Shape _shp, int _x, int _y)
        {
            TGIS_LayerVector ll;
            TGIS_Shape shp;
            TGIS_Point ptgA;
            TGIS_Point ptgB;
            TGIS_Extent ex;

            // move main shape
            ptgA = _shp.GetPoint(0, 0);
            ptgA.X = ptgA.X + _x;
            ptgA.Y = ptgA.Y + _y;
            _shp.SetPosition(ptgA, null, 0);

            // move "sidekick"
            ll = (TGIS_LayerVector)GIS.Get("sidekicks");
            shp = ll.GetShape(_shp.Uid);
            ptgB = shp.GetPoint(0, 0);
            ptgB.X = ptgB.X + _x;
            ptgB.Y = ptgB.Y + _y;
            shp.SetPosition(ptgB, null, 0);

            // aditional invalidation - we have now a starnge big
            // combo shape
            ex.XMin = Math.Min(ptgA.X, ptgB.X);
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y);
            ex.XMax = Math.Max(ptgA.X, ptgB.X);
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y);
        }

        private void doLabelPaint(object _sender, TGIS_ShapeEventArgs _e)
        {
            Point ptA, ptB;
            TGIS_LayerVector ll;
            TGIS_Shape shape = _e.Shape;
            TGIS_Shape shp;
            TGIS_RendererAbstract rnd;

            // draw line to real point
            ll = (TGIS_LayerVector)GIS.Get("realpoints");
            shp = ll.GetShape(shape.Uid);
            ptA = shape.Viewer.Ref.MapToScreen(shp.GetPoint(0, 0));
            ptB = shape.Viewer.Ref.MapToScreen(shape.GetPoint(0, 0));

            rnd = (TGIS_RendererAbstract)(GIS.Renderer);
            rnd.CanvasPen.Color = TGIS_Color.Blue;
            rnd.CanvasPen.Style = TGIS_PenStyle.Solid;
            rnd.CanvasPen.Width = 1;
            rnd.CanvasDrawLine(ptA.X, ptA.Y, ptB.X, ptB.Y);

            // draw label itself
            shape.Params.Labels.Value = shp.GetField("name").ToString();
            shape.DrawLabel();
        }

        private void GIS_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_Shape shp;

            if (GIS.IsEmpty) return;
            if (GIS.InPaint) return;

            // start editing of some shape from sidekicks
            ll = (TGIS_LayerVector)GIS.Get("sidekicks");
            shp = ll.Locate(GIS.ScreenToMap(new Point(e.X, e.Y)),
                             100 / GIS.Zoom
                           );
            currShape = shp;
            if (currShape == null) return;
            // we are not chnging the GIS.Mode to gisEdit because we want to control
            // editing on our own, so instead we will call MouseBegin, MouseMove and MouseEnd
            // "manually"
            GIS.Editor.MouseBegin(new Point(e.X, e.Y), true);
        }

        private void GIS_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TGIS_LayerVector ll;
            TGIS_Shape shp;
            TGIS_Point ptgA;
            TGIS_Point ptgB;
            TGIS_Extent ex;

            if (GIS.IsEmpty) return;
            if (currShape == null) return;
            // aditional invalidation - we have now a strange big
            // combo shape
            ptgA = currShape.GetPoint(0, 0);
            ll = (TGIS_LayerVector)GIS.Get("realpoints");
            shp = ll.GetShape(currShape.Uid);
            ptgB = shp.GetPoint(0, 0);
            ex.XMin = Math.Min(ptgA.X, ptgB.X);
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y);
            ex.XMax = Math.Max(ptgA.X, ptgB.X);
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y);

            ptgA = GIS.ScreenToMap(new Point(e.X, e.Y));
            if (TGIS_Utils.GisIsPointInsideExtent(ptgA, GIS.Extent))
                currShape.SetPosition(ptgA, null, 0);

            // aditional invalidation - we have now a starnge big
            // combo shape
            ptgA = currShape.GetPoint(0, 0);
            ll = (TGIS_LayerVector)GIS.Get("realpoints");
            shp = ll.GetShape(currShape.Uid);
            ptgB = shp.GetPoint(0, 0);
            ex.XMin = Math.Min(ptgA.X, ptgB.X);
            ex.YMin = Math.Min(ptgA.Y, ptgB.Y);
            ex.XMax = Math.Max(ptgA.X, ptgB.X);
            ex.YMax = Math.Max(ptgA.Y, ptgB.Y);
        }

        private void GIS_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (GIS.IsEmpty) return;
            currShape = null;
        }
    }
}
