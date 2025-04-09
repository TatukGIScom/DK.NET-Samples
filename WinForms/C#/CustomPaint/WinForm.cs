using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.NDK.SharpDX;
#if GIS_SKIA_SUPPORT
using SkiaSharp;
using TatukGIS.NDK.SkiaSharp;
#endif


// add GIS_SKIA_SUPPORT to the project,
// if you want to see native skia rendering
namespace CustomPaint
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
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TGIS_LayerVector ll;
        private TGIS_Bitmap bmp;
        private ComboBox cbRenderer;
        private int[] px;


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
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.cbRenderer = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.AutoStyle = true;
            this.GIS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Level = 28.140189979287609D;
            this.GIS.Location = new System.Drawing.Point(0, 41);
            this.GIS.Margin = new System.Windows.Forms.Padding(4);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(784, 520);
            this.GIS.TabIndex = 3;
            this.GIS.PaintExtraEvent += new TatukGIS.NDK.TGIS_RendererEvent(this.GIS_PaintExtraEvent);
            // 
            // cbRenderer
            // 
            this.cbRenderer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRenderer.FormattingEnabled = true;
            this.cbRenderer.Location = new System.Drawing.Point(13, 13);
            this.cbRenderer.Name = "cbRenderer";
            this.cbRenderer.Size = new System.Drawing.Size(186, 21);
            this.cbRenderer.TabIndex = 4;
            this.cbRenderer.SelectedIndexChanged += new System.EventHandler(this.cbRenderer_SelectedIndexChanged);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.cbRenderer);
            this.Controls.Add(this.GIS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - CustomPaint";
            this.Load += new System.EventHandler(this.WinForm_Load);
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

        private void initialize_pixels()
        {
            px = new int[25];

            px[0] = unchecked((int)0xFFFF0000);
            px[1] = unchecked((int)0xFFFF0000);
            px[2] = unchecked((int)0xFFFF0000);
            px[3] = unchecked((int)0xFFFF0000);
            px[4] = unchecked((int)0xFFFF0000);
            px[5] = unchecked((int)0x00000000);
            px[6] = unchecked((int)0x00000000);
            px[7] = unchecked((int)0xFF0000FF);
            px[8] = unchecked((int)0x00000000);
            px[9] = unchecked((int)0x00000000);
            px[10] = unchecked((int)0x00000000);
            px[11] = unchecked((int)0x00000000);
            px[12] = unchecked((int)0xFF0000FF);
            px[13] = unchecked((int)0x00000000);
            px[14] = unchecked((int)0x00000000);
            px[15] = unchecked((int)0x00000000);
            px[16] = unchecked((int)0x00000000);
            px[17] = unchecked((int)0xFF0000FF);
            px[18] = unchecked((int)0x00000000);
            px[19] = unchecked((int)0x00000000);
            px[20] = unchecked((int)0x00000000);
            px[21] = unchecked((int)0x00000000);
            px[22] = unchecked((int)0xFF0000FF);
            px[23] = unchecked((int)0x00000000);
            px[23] = unchecked((int)0x00000000);
        }

        private void WinForm_Load(object sender, System.EventArgs e)
        {
            TGIS_Shape shp;

            initialize_pixels();

            ll = new TGIS_LayerVector();
            ll.Name = "CustomPaint";

            GIS.Add(ll);

            ll.PaintShapeEvent += GIS_PaintShapeEvent;

            ll.AddField("type", TGIS_FieldType.String, 100, 0);
            ll.Extent = GIS.Extent;

            shp = ll.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(-25, 25));
            shp.Params.Marker.Size = 0;
            shp.SetField("type", "Rectangle");
            shp.Unlock();

            shp = ll.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(25, 25));
            shp.Params.Marker.Size = 0;
            shp.SetField("type", "Ellipse");
            shp.Unlock();

            shp = ll.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(-25, -25));
            shp.Params.Marker.Size = 0;
            shp.SetField("type", "Image1");
            shp.Unlock();

            shp = ll.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(25, -25));
            shp.Params.Marker.Size = 0;
            shp.SetField("type", "Image2");
            shp.Unlock();

            ll.Extent = TGIS_Utils.GisExtent(-100, -100, 100, 100);

            bmp = new TGIS_Bitmap();
            bmp.LoadFromFile(TGIS_Utils.GisSamplesDataDirDownload() + @"Symbols\police.bmp");

            GIS.FullExtent();

            cbRenderer.Items.Clear();
            for ( int i = 0; i < TGIS_Utils.RendererManager.Names.Count; i++ )
            {
                cbRenderer.Items.Add(TGIS_Utils.RendererManager.Names[i]);
            }
            cbRenderer.SelectedIndex = TGIS_Utils.RendererManager.Names.IndexOf(GIS.Renderer.GetType().Name);
        }

        private void GIS_PaintShapeEvent(object _sender, TGIS_ShapeEventArgs _e)
        {
            Point pt;
            TGIS_RendererAbstract rdr;

            pt = GIS.MapToScreen(_e.Shape.PointOnShape());
            _e.Shape.Draw();
            rdr = (TGIS_RendererAbstract)_e.Shape.Layer.Renderer;

            //Drawing with our renderer
            if ((String)_e.Shape.GetField("type") == "Rectangle")
            {
                rdr.CanvasPen.Color = TGIS_Color.Red;
                rdr.CanvasBrush.Color = TGIS_Color.Yellow;
                rdr.CanvasDrawRectangle(new Rectangle(pt.X, pt.Y, 20, 20));
                pt.Y = pt.Y - 20;
                rdr.CanvasDrawText(new Rectangle(pt.X, pt.Y, 50, 20), "Rectangle");
            }
            else if ((String)_e.Shape.GetField("type") == "Ellipse")
            {
                rdr.CanvasPen.Color = TGIS_Color.Black;
                rdr.CanvasBrush.Color = TGIS_Color.Red;
                rdr.CanvasDrawEllipse(pt.X, pt.Y, 20, 20);
                pt.Y = pt.Y - 20;
                rdr.CanvasDrawText(new Rectangle(pt.X, pt.Y, 50, 20), "Ellipse");
            }
            else if ((String)_e.Shape.GetField("type") == "Image1")
            {
                rdr.CanvasDrawBitmap(
                bmp,
                new Rectangle(
                  pt.X,
                  pt.Y,
                  20,
                  20
                ));
                pt.Y = pt.Y - 20;
                rdr.CanvasDrawText(new Rectangle(pt.X, pt.Y, 50, 20), "Image1");
            }
            else if ((String)_e.Shape.GetField("type") == "Image2")
            {
                rdr.CanvasDrawBitmap(
                  px,
                  new Point(5, 5),
                  new Rectangle(pt.X, pt.Y, 20, 20),
                  TGIS_BitmapFormat.ARGB,
                  TGIS_BitmapLinesOrder.Down
                );
                pt.Y = pt.Y - 20;
                rdr.CanvasDrawText(new Rectangle(pt.X, pt.Y, 50, 20), "Image2");
            }

        }

        private void GIS_PaintExtraEvent(object _sender, TGIS_RendererEventArgs _e)
        {
            Graphics cnvWinForms;
            Font fontWinForms;
            Brush brush;

            #if GIS_SKIA_SUPPORT
            SKCanvas cnvSkia;
            #endif

            SharpDX.Direct2D1.RenderTarget cnvSharpDX;
            SharpDX.Vector2 vector;
            SharpDX.DirectWrite.TextLayout txtl;
            SharpDX.DirectWrite.TextFormat txtf;
            SharpDX.DirectWrite.Factory factory;
            SharpDX.Direct2D1.Brush brushSharpDX;

            // drawing with native objects, not recommended
            if (_e.Renderer is TGIS_RendererWinForms)
            {
                fontWinForms = new Font(this.Font, FontStyle.Regular);
                brush = new SolidBrush(Color.Blue);
                cnvWinForms = (Graphics)_e.Renderer.CanvasNative();
                cnvWinForms.DrawString("Hello from WinForms", fontWinForms, brush, new Point(50, 50));
            }
            else
            if (_e.Renderer is TGIS_RendererSharpDX)
            {

                cnvSharpDX = (SharpDX.Direct2D1.RenderTarget)_e.Renderer.CanvasNative();

                vector = new SharpDX.Vector2(50, 50);
                factory = new SharpDX.DirectWrite.Factory(SharpDX.DirectWrite.FactoryType.Shared);
                brushSharpDX = new SharpDX.Direct2D1.SolidColorBrush(cnvSharpDX, SharpDX.Color4.Black);
                txtf = new SharpDX.DirectWrite.TextFormat(
                    factory,
                    "Arial",
                    SharpDX.DirectWrite.FontWeight.Regular,
                    0,
                   12
                );

                txtl = new SharpDX.DirectWrite.TextLayout(
                    factory,
                    "Hello from SharpDX",
                    txtf,
                    150,
                    50
                );

                cnvSharpDX.DrawTextLayout(
                    vector,
                    txtl,
                    brushSharpDX,
                    SharpDX.Direct2D1.DrawTextOptions.NoSnap
                    );
            }
            else
            #if GIS_SKIA_SUPPORT
            if (_e.Renderer is TGIS_RendererSkiaSharp)
            {
              cnvSkia = (SKCanvas)_e.Renderer.CanvasNative();
              cnvSkia.DrawText("Hello from Skia", new SKPoint(50, 50), new SKPaint());
            }
            else
            #endif
            {
                //for other renderers
            }

        }

        private void cbRenderer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( cbRenderer.SelectedIndex >= 0 )
              GIS.Renderer = TGIS_Utils.RendererManager.CreateInstance(TGIS_Utils.RendererManager.Names[cbRenderer.SelectedIndex]);

            GIS.ControlUpdateWholeMap();
        }
    }
}
