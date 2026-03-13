using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using TatukGIS.NDK;
using TatukGIS.NDK.WinForms;
using TatukGIS.RTL;
using System.Diagnostics;

namespace ShapeOperations
{
    /// <summary>
    /// Summary description for WinForm.
    /// </summary>
    public class WinForm : System.Windows.Forms.Form
    {
        private Label lbHint;
        private TatukGIS.NDK.WinForms.TGIS_ViewerWnd GIS;
        private TGIS_LayerVector edtLayer;
        private TGIS_Shape currShape;
        private TGIS_Shape edtShape;
        private int prevX;
        private int prevY;
        private TGIS_Point prevPtg;
        private Boolean handleMouseMove;
        private RadioButton rbRotate;
        private RadioButton rbScale;
        private RadioButton rbMove;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            this.lbHint = new System.Windows.Forms.Label();
            this.GIS = new TatukGIS.NDK.WinForms.TGIS_ViewerWnd();
            this.rbRotate = new System.Windows.Forms.RadioButton();
            this.rbScale = new System.Windows.Forms.RadioButton();
            this.rbMove = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lbHint
            // 
            this.lbHint.AutoSize = true;
            this.lbHint.Location = new System.Drawing.Point(197, 9);
            this.lbHint.Name = "lbHint";
            this.lbHint.Size = new System.Drawing.Size(0, 13);
            this.lbHint.TabIndex = 3;
            // 
            // GIS
            // 
            this.GIS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GIS.CursorFor3DSelect = null;
            this.GIS.CursorForCameraZoom = null;
            this.GIS.Location = new System.Drawing.Point(-2, 33);
            this.GIS.Name = "GIS";
            this.GIS.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.GIS.Size = new System.Drawing.Size(786, 527);
            this.GIS.TabIndex = 4;
            this.GIS.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseMove);
            this.GIS.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GIS_MouseUp);
            // 
            // btnRotate
            // 
            this.rbRotate.AutoSize = true;
            this.rbRotate.Location = new System.Drawing.Point(12, 7);
            this.rbRotate.Name = "btnRotate";
            this.rbRotate.Size = new System.Drawing.Size(57, 17);
            this.rbRotate.TabIndex = 5;
            this.rbRotate.Text = "Rotate";
            this.rbRotate.UseVisualStyleBackColor = true;
            this.rbRotate.CheckedChanged += new System.EventHandler(this.btnRotate_CheckedChanged);
            // 
            // btnScale
            // 
            this.rbScale.AutoSize = true;
            this.rbScale.Location = new System.Drawing.Point(75, 7);
            this.rbScale.Name = "btnScale";
            this.rbScale.Size = new System.Drawing.Size(52, 17);
            this.rbScale.TabIndex = 6;
            this.rbScale.Text = "Scale";
            this.rbScale.UseVisualStyleBackColor = true;
            this.rbScale.CheckedChanged += new System.EventHandler(this.btnScale_CheckedChanged);
            // 
            // btnMove
            // 
            this.rbMove.AutoSize = true;
            this.rbMove.Location = new System.Drawing.Point(133, 7);
            this.rbMove.Name = "btnMove";
            this.rbMove.Size = new System.Drawing.Size(52, 17);
            this.rbMove.TabIndex = 7;
            this.rbMove.Text = "Move";
            this.rbMove.UseVisualStyleBackColor = true;
            this.rbMove.CheckedChanged += new System.EventHandler(this.btnMove_CheckedChanged);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.rbMove);
            this.Controls.Add(this.rbScale);
            this.Controls.Add(this.rbRotate);
            this.Controls.Add(this.GIS);
            this.Controls.Add(this.lbHint);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(200, 120);
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TatukGIS Samples - ShapeOperations";
            this.Load += new System.EventHandler(this.WinForm_Load);
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
            currShape = null;
            edtShape = null ;
            handleMouseMove = false;
            rbRotate.PerformClick();

            GIS.Lock();
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"Samples\3D\buildings.shp");

            edtLayer = new TGIS_LayerVector() ;
            edtLayer.CS =  GIS.CS ;
            edtLayer.CachedPaint = false ; // make tracking layer
            edtLayer.Params.Area.Pattern = TGIS_BrushStyle.Clear ;
            edtLayer.Params.Area.OutlineColor = TGIS_Color.Red ;
            GIS.Add( edtLayer );
            GIS.Unlock();
            
            GIS.Zoom = GIS.Zoom * 4;
        }

        private void TransformSelectedShape(TGIS_Shape _shp, double _xx, double _yx, double _xy, double _yy, double _dx, double _dy)
        {
            TGIS_Point centroid;

            if (_shp == null) return;

            centroid = _shp.Centroid();

            // transform
            // x' = x*xx + y*xy + dx
            // y' = x*yx + y*yy + dx
            // z' = z
            _shp.Transform(TGIS_Utils.GisPoint3DFrom2D(centroid),
                             _xx, _yx, 0,
                             _xy, _yy, 0,
                              0, 0, 1,
                             _dx, _dy, 0,
                             false
                            );
            GIS.InvalidateTopmost();
        }

        private void RotateSelectedShape(TGIS_Shape _shp, double _angle)
        {
            TransformSelectedShape(
              _shp,
               Math.Cos(_angle), Math.Sin(_angle),
              -Math.Sin(_angle), Math.Cos(_angle),
                       0, 0
            );
        }

        private void ScaleSelectedShape(TGIS_Shape _shp, double _x, double _y)
        {
            TransformSelectedShape(
              _shp,
               _x, 0,
                0, _y,
                0, 0
            );
        }

        private void TranslateSelectedShape(TGIS_Shape _shp, double _x, double _y)
        {
            TransformSelectedShape(
              _shp,
              1, 0,
              0, 1,
              _x, _y
            );
        }

        private void GIS_MouseMove(object sender, MouseEventArgs e)
        {
            TGIS_Point ptg;

            if (edtShape == null) return;

            if (handleMouseMove)
            {
                ptg = GIS.ScreenToMap(new Point(e.X, e.Y));

                if (rbRotate.Checked)
                    RotateSelectedShape(edtShape, ((Math.PI / 180) * ((e.X - prevX))));
                // Rotate by moving the mouse horizontally
                else if (rbScale.Checked)
                {
                    if ((prevX != 0) && (prevY != 0))
                        ScaleSelectedShape(edtShape, (double)e.X / prevX, (double)e.Y / prevY);
                }
                else if (rbMove.Checked)
                    TranslateSelectedShape(edtShape, (ptg.X - prevPtg.X), (ptg.Y - prevPtg.Y));

                prevPtg.X = ptg.X;
                prevPtg.Y = ptg.Y;
                prevX = e.X;
                prevY = e.Y;
            }
        }

        private void GIS_MouseUp(object sender, MouseEventArgs e)
        {
            TGIS_Shape shp;
            TGIS_Point ptg;

            lbHint.Text = "No selected shape. Select shape";

            if (currShape != null)
            {
                currShape.CopyGeometry( edtShape );
                edtLayer.RevertAll() ;
                currShape = null ;
                edtShape = null;
                GIS.InvalidateWholeMap() ;                
                handleMouseMove = false;
                return;
            }

            if (GIS.IsEmpty) return;
            if (GIS.IsEmpty) return;
            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            ptg = GIS.ScreenToMap(new Point(e.X, e.Y));
            shp = (TGIS_Shape)(GIS.Locate(ptg, 5 / GIS.Zoom));
            if (shp == null) return;

            currShape = shp.MakeEditable();
            edtShape = edtLayer.AddShape(currShape.CreateCopy() ) ;
  
            lbHint.Text = "Selected shape : " + currShape.Uid + ". Click to commit changes";

            prevPtg.X = ptg.X;
            prevPtg.Y = ptg.Y;
            prevX = e.X;
            prevY = e.Y;

            handleMouseMove = !handleMouseMove;
        }

        private void btnRotate_CheckedChanged(object sender, EventArgs e)
        {
            lbHint.Text = "Select shape to start rotating";
            if (currShape != null)
            {
                GIS.InvalidateTopmost();
                currShape = null;
                handleMouseMove = false;
                return;
            }

        }

        private void btnScale_CheckedChanged(object sender, EventArgs e)
        {
            lbHint.Text = "Select shape to start scaling";
            if (currShape != null)
            {
                GIS.InvalidateTopmost();
                currShape = null;
                handleMouseMove = false;
                return;
            }

        }

        private void btnMove_CheckedChanged(object sender, EventArgs e)
        {
            lbHint.Text = "Select shape to start moving";
            if (currShape != null)
            {
                GIS.InvalidateTopmost();
                currShape = null;
                handleMouseMove = false;
                return;
            }

        }
    }
}
