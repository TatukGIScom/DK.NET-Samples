// =============================================================================
// This source code is a part of TatukGIS Developer Kernel.
// =============================================================================
//
// CustomPaint sample - demonstrates how to perform custom drawing on top of a
// TatukGIS map viewer using two complementary paint-hook mechanisms in C#.
//
// Key concepts:
//
//   PaintShapeEvent
//     A per-shape callback registered on a TGIS_LayerVector.  It fires once
//     for every shape in that layer during each repaint cycle and completely
//     replaces the layer's default rendering for those shapes.  The handler
//     obtains the shape's geographic position as screen pixels via
//     GIS.MapToScreen(shape.PointOnShape()), then uses TGIS_RendererAbstract
//     Canvas* methods to draw primitives (rectangles, ellipses, bitmaps, text)
//     directly on the renderer canvas.
//
//   PaintExtraEvent
//     A viewer-level callback that fires once after all layers have been
//     rendered.  It is the right place for overlays that must appear on top
//     of everything (watermarks, HUD elements, debug info).  This sample
//     shows how to access the renderer's native canvas handle to call
//     WinForms GDI+, SharpDX Direct2D, or SkiaSharp APIs.  Using the native
//     handle is NOT recommended for production because it ties code to a
//     specific backend; prefer TGIS_RendererAbstract helpers for portability.
//
//   Renderer switching
//     The cbRenderer combo box is populated from TGIS_Utils.RendererManager.Names.
//     Selecting an entry creates a new renderer instance and assigns it to
//     GIS.Renderer, demonstrating live backend switching.
//
// Layer layout:
//   One TGIS_LayerVector ("CustomPaint") with four invisible point shapes
//   at the four map quadrants.  A "type" string field selects the primitive:
//     "Rectangle" at (-25, 25)  -> CanvasDrawRectangle (red/yellow)
//     "Ellipse"   at ( 25, 25)  -> CanvasDrawEllipse   (black/red)
//     "Image1"    at (-25,-25)  -> CanvasDrawBitmap(TGIS_Bitmap from file)
//     "Image2"    at ( 25,-25)  -> CanvasDrawBitmap(int[] raw ARGB pixels)
//
// Compile with GIS_SKIA_SUPPORT defined to enable the SkiaSharp rendering path.
// =============================================================================

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
    /// Main application form for the CustomPaint sample.
    /// Hosts a TGIS_ViewerWnd and demonstrates per-shape and per-frame custom
    /// drawing using the TatukGIS abstract renderer API and native canvas handles.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;  // main GIS viewer
        private TGIS_LayerVector ll;    // the "CustomPaint" layer
        private TGIS_Bitmap bmp;        // bitmap loaded from police.bmp (used in Image1)
        private ComboBox cbRenderer;    // drop-down to switch the active renderer
        /// <summary>
        /// Raw ARGB pixel array (5x5) used for the Image2 drawing case.
        /// Format is 0xAARRGGBB per element (TGIS_BitmapFormat.ARGB).
        /// </summary>
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
            // PaintExtraEvent fires once per frame after all layers are drawn
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
            #if NET6_0_OR_GREATER
              ApplicationConfiguration.Initialize();
            #else
              Application.EnableVisualStyles();
              Application.SetCompatibleTextRenderingDefault(false);
            #endif
            Application.Run(new WinForm());
        }

        /// <summary>
        /// Initialises the raw ARGB pixel array used for the Image2 drawing case.
        ///
        /// The array represents a 5x5 pixel image laid out in row-major order:
        ///   Row 0 (px[0..4]):  all opaque red   (0xFFFF0000)
        ///   Diagonal column 2 pixels [7],[12],[17],[22]: opaque blue (0xFF0000FF)
        ///   All other pixels:  fully transparent (0x00000000)
        ///
        /// The pixel format is ARGB: the high byte is alpha (0xFF = opaque),
        /// followed by red, green, and blue bytes.
        /// </summary>
        private void initialize_pixels()
        {
            px = new int[25];

            // Row 0: five opaque red pixels
            px[0] = unchecked((int)0xFFFF0000);
            px[1] = unchecked((int)0xFFFF0000);
            px[2] = unchecked((int)0xFFFF0000);
            px[3] = unchecked((int)0xFFFF0000);
            px[4] = unchecked((int)0xFFFF0000);
            // Row 1: transparent, then blue at column 2
            px[5] = unchecked((int)0x00000000);
            px[6] = unchecked((int)0x00000000);
            px[7] = unchecked((int)0xFF0000FF); // opaque blue
            px[8] = unchecked((int)0x00000000);
            px[9] = unchecked((int)0x00000000);
            // Row 2
            px[10] = unchecked((int)0x00000000);
            px[11] = unchecked((int)0x00000000);
            px[12] = unchecked((int)0xFF0000FF);
            px[13] = unchecked((int)0x00000000);
            px[14] = unchecked((int)0x00000000);
            // Row 3
            px[15] = unchecked((int)0x00000000);
            px[16] = unchecked((int)0x00000000);
            px[17] = unchecked((int)0xFF0000FF);
            px[18] = unchecked((int)0x00000000);
            px[19] = unchecked((int)0x00000000);
            // Row 4
            px[20] = unchecked((int)0x00000000);
            px[21] = unchecked((int)0x00000000);
            px[22] = unchecked((int)0xFF0000FF);
            px[23] = unchecked((int)0x00000000);
            px[23] = unchecked((int)0x00000000); // note: index 23 assigned twice
        }

        /// <summary>
        /// WinForm_Load - builds the demo layer with four invisible point shapes
        /// and registers the PaintShapeEvent callback.
        ///
        /// Layer setup:
        ///   "CustomPaint" layer with Marker.Size=0 shapes at four quadrants.
        ///   PaintShapeEvent replaces default rendering; each shape is drawn
        ///   according to its "type" field value.
        ///
        /// Shapes:
        ///   "Rectangle" at (-25, 25)  -> NW quadrant
        ///   "Ellipse"   at ( 25, 25)  -> NE quadrant
        ///   "Image1"    at (-25,-25)  -> SW quadrant (renders police.bmp)
        ///   "Image2"    at ( 25,-25)  -> SE quadrant (renders raw pixel array)
        ///
        /// The renderer combo box is populated so the user can switch backends
        /// at runtime to verify that all drawing paths are backend-agnostic.
        /// </summary>
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            TGIS_Shape shp;

            initialize_pixels();

            ll = new TGIS_LayerVector();
            ll.Name = "CustomPaint";

            GIS.Add(ll);

            // Register the per-shape paint callback.
            // GIS_PaintShapeEvent will be called once per shape per repaint.
            ll.PaintShapeEvent += GIS_PaintShapeEvent;

            // Add the "type" attribute field that the paint handler reads.
            ll.AddField("type", TGIS_FieldType.String, 100, 0);
            ll.Extent = GIS.Extent;

            // --- Create the four demo shapes ---
            // Lock(Extent) prevents the layer from expanding its extent during
            // AddPoint; Unlock() releases the lock after construction.

            shp = ll.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(-25, 25));  // NW
            shp.Params.Marker.Size = 0;   // invisible; custom draw handles visuals
            shp.SetField("type", "Rectangle");
            shp.Unlock();

            shp = ll.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(25, 25));   // NE
            shp.Params.Marker.Size = 0;
            shp.SetField("type", "Ellipse");
            shp.Unlock();

            shp = ll.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(-25, -25)); // SW
            shp.Params.Marker.Size = 0;
            shp.SetField("type", "Image1");
            shp.Unlock();

            shp = ll.CreateShape(TGIS_ShapeType.Point);
            shp.Lock(TGIS_Lock.Extent);
            shp.AddPart();
            shp.AddPoint(TGIS_Utils.GisPoint(25, -25));  // SE
            shp.Params.Marker.Size = 0;
            shp.SetField("type", "Image2");
            shp.Unlock();

            // Set the layer's geographic extent to cover all four shapes
            ll.Extent = TGIS_Utils.GisExtent(-100, -100, 100, 100);

            // Load the file-based bitmap for the Image1 drawing case.
            // TGIS_Bitmap is TatukGIS's cross-renderer bitmap wrapper.
            bmp = new TGIS_Bitmap();
            bmp.LoadFromFile(TGIS_Utils.GisSamplesDataDirDownload() + @"Symbols\police.bmp");

            GIS.FullExtent();

            // Populate the renderer selector with all registered backends
            cbRenderer.Items.Clear();
            for ( int i = 0; i < TGIS_Utils.RendererManager.Names.Count; i++ )
            {
                cbRenderer.Items.Add(TGIS_Utils.RendererManager.Names[i]);
            }
            // Pre-select the currently active renderer
            cbRenderer.SelectedIndex = TGIS_Utils.RendererManager.Names.IndexOf(GIS.Renderer.GetType().Name);
        }

        /// <summary>
        /// PaintShapeEvent callback - called once per shape per repaint pass.
        ///
        /// Replaces the default rendering for every shape in the "CustomPaint"
        /// layer.  The shape's screen position is obtained via
        /// GIS.MapToScreen(shape.PointOnShape()), converting geographic map
        /// coordinates to pixel coordinates within the viewer control.
        ///
        /// The handler calls _e.Shape.Draw() to run any remaining default hooks
        /// (a no-op for Marker.Size=0 shapes, but good practice), then reads
        /// the "type" field and delegates to the appropriate Canvas* method.
        ///
        /// All coordinates are in screen pixels; the 20x20 size is fixed and
        /// does not scale with the map zoom level.
        ///
        /// "Rectangle" - red-bordered yellow rectangle + label 20px above.
        /// "Ellipse"   - black-bordered red ellipse   + label 20px above.
        /// "Image1"    - TGIS_Bitmap stretched to 20x20 + label 20px above.
        /// "Image2"    - raw int[] ARGB pixels (source offset (5,5)) to 20x20
        ///               + label 20px above.  The source offset skips the first
        ///               5 pixels (the solid red row) to start at the diagonal.
        /// </summary>
        private void GIS_PaintShapeEvent(object _sender, TGIS_ShapeEventArgs _e)
        {
            Point pt;                        // screen-pixel position of the shape
            TGIS_RendererAbstract rdr;       // abstract renderer for this layer

            // MapToScreen converts geographic coordinates to screen pixels.
            // PointOnShape() returns the representative point of the geometry.
            pt = GIS.MapToScreen(_e.Shape.PointOnShape());
            _e.Shape.Draw();
            // Obtain the abstract renderer from the shape's owning layer.
            rdr = (TGIS_RendererAbstract)_e.Shape.Layer.Renderer;

            // --- Select drawing primitive by "type" field ---

            if ((String)_e.Shape.GetField("type") == "Rectangle")
            {
                // Yellow-filled, red-bordered rectangle at the shape's screen position
                rdr.CanvasPen.Color = TGIS_Color.Red;
                rdr.CanvasBrush.Color = TGIS_Color.Yellow;
                rdr.CanvasDrawRectangle(new Rectangle(pt.X, pt.Y, 20, 20));
                // Draw label 20 pixels above the rectangle
                pt.Y = pt.Y - 20;
                rdr.CanvasDrawText(new Rectangle(pt.X, pt.Y, 50, 20), "Rectangle");
            }
            else if ((String)_e.Shape.GetField("type") == "Ellipse")
            {
                // Red-filled, black-bordered ellipse
                rdr.CanvasPen.Color = TGIS_Color.Black;
                rdr.CanvasBrush.Color = TGIS_Color.Red;
                rdr.CanvasDrawEllipse(pt.X, pt.Y, 20, 20);
                pt.Y = pt.Y - 20;
                rdr.CanvasDrawText(new Rectangle(pt.X, pt.Y, 50, 20), "Ellipse");
            }
            else if ((String)_e.Shape.GetField("type") == "Image1")
            {
                // Stretch the TGIS_Bitmap (police.bmp) into the 20x20 destination rect
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
                // Draw using a raw ARGB pixel array.
                // new Point(5, 5) = source offset within the 5-wide px array.
                // TGIS_BitmapFormat.ARGB = 0xAARRGGBB layout per int element.
                // TGIS_BitmapLinesOrder.Down = scanlines run top-to-bottom.
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

        /// <summary>
        /// PaintExtraEvent callback - fired once per frame after all layers
        /// have finished rendering.  This is the correct place for full-frame
        /// overlays such as watermarks or HUD elements.
        ///
        /// The handler demonstrates accessing the renderer's native canvas
        /// handle and calling platform-specific drawing APIs directly.  This
        /// approach is NOT recommended for portable code; use
        /// TGIS_RendererAbstract Canvas* methods instead.
        ///
        /// Supported backends:
        ///   TGIS_RendererWinForms  - System.Drawing.Graphics (GDI+)
        ///   TGIS_RendererSharpDX   - SharpDX.Direct2D1.RenderTarget
        ///   TGIS_RendererSkiaSharp - SkiaSharp.SKCanvas (GIS_SKIA_SUPPORT)
        /// </summary>
        private void GIS_PaintExtraEvent(object _sender, TGIS_RendererEventArgs _e)
        {
            Graphics cnvWinForms;         // GDI+ Graphics context (WinForms backend)
            Font fontWinForms;
            Brush brush;

            #if GIS_SKIA_SUPPORT
            SKCanvas cnvSkia;             // SkiaSharp canvas
            #endif

            SharpDX.Direct2D1.RenderTarget cnvSharpDX;  // SharpDX D2D render target
            SharpDX.Vector2 vector;
            SharpDX.DirectWrite.TextLayout txtl;
            SharpDX.DirectWrite.TextFormat txtf;
            SharpDX.DirectWrite.Factory factory;
            SharpDX.Direct2D1.Brush brushSharpDX;

            // --- WinForms GDI+ renderer path ---
            // CanvasNative() returns a System.Drawing.Graphics object when the
            // active renderer is TGIS_RendererWinForms.
            if (_e.Renderer is TGIS_RendererWinForms)
            {
                fontWinForms = new Font(this.Font, FontStyle.Regular);
                brush = new SolidBrush(Color.Blue);
                cnvWinForms = (Graphics)_e.Renderer.CanvasNative();
                cnvWinForms.DrawString("Hello from WinForms", fontWinForms, brush, new Point(50, 50));
            }
            else
            // --- SharpDX Direct2D renderer path ---
            // CanvasNative() returns a SharpDX.Direct2D1.RenderTarget.
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
            // --- SkiaSharp renderer path (requires GIS_SKIA_SUPPORT define) ---
            // CanvasNative() returns a SkiaSharp.SKCanvas.
            if (_e.Renderer is TGIS_RendererSkiaSharp)
            {
              cnvSkia = (SKCanvas)_e.Renderer.CanvasNative();
              cnvSkia.DrawText("Hello from Skia", new SKPoint(50, 50), new SKPaint());
            }
            else
            #endif
            {
                // Future or unrecognised renderer backends: no action required.
            }

        }

        /// <summary>
        /// Replaces the viewer's active rendering backend when the user selects
        /// a different entry in the combo box.
        ///
        /// RendererManager.CreateInstance constructs a fresh renderer by class
        /// name.  Assigning it to GIS.Renderer replaces the backend while
        /// keeping all layers intact.  ControlUpdateWholeMap forces an
        /// immediate full repaint with the new renderer so the change is
        /// visible without user interaction.
        /// </summary>
        private void cbRenderer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( cbRenderer.SelectedIndex >= 0 )
              GIS.Renderer = TGIS_Utils.RendererManager.CreateInstance(TGIS_Utils.RendererManager.Names[cbRenderer.SelectedIndex]);

            GIS.ControlUpdateWholeMap();
        }
    }
}
