using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;

namespace View3D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            btnTextures.Enabled = false;
            btnRoof.Enabled = false;
            btnWalls.Enabled = false;
            button4.Enabled = false;
            GIS_3D.GIS_Viewer = GIS;
            cbx3DMode.SelectedIndex = 0;
            cbx3DMode_TextChanged(this, new EventArgs());
            this.ActiveControl = GIS;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            GIS.Lock();
            try
            {
                if (GIS.View3D)
                    btn2D3D_Click(sender, e);
            
                GIS.Close();
                GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "Samples\\3D\\Building3D.ttkproject");
                cbx3DMode.SelectedIndex = 0;
            }
            finally
            {
                GIS.Unlock();
            }
        }

        private void btn2D3D_Click(object sender, EventArgs e)
        {
            if (GIS.IsEmpty) return;

            if (GIS.View3D && GIS.Viewer3D.IsBusy) return;
            GIS.View3D = !GIS.View3D;

            if (GIS.View3D)
            {
                btn2D3D.Text = "2D View";
                btnTextures.Enabled = true;
                btnRoof.Enabled = true;
                btnWalls.Enabled = true;
                button4.Enabled = true;
                GIS_3D.Enabled = true;
            }
            else
            {
                btn2D3D.Text = "3D View";
                btnTextures.Enabled = false;
                btnRoof.Enabled = false;
                btnWalls.Enabled = false;
                button4.Enabled = false;
                GIS_3D.Enabled = false;
            }
            cbx3DMode.SelectedIndex = 0;

        }

        private void cbx3DMode_TextChanged(object sender, EventArgs e)
        {
            if (!GIS.View3D)
                return;

            switch (cbx3DMode.SelectedIndex)
            {
                case 0:
                    GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.CameraPosition;
                    break;
                case 1:
                    GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.CameraXYZ;
                    break;
                case 2:
                    GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.CameraXY;
                    break;
                case 3:
                    GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.CameraRotation;
                    break;
                case 4:
                    GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.SunPosition;
                    break;
                case 5:
                    GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.Zoom;
                    break;
                case 6:
                    GIS.Viewer3D.Mode = TatukGIS.NDK.TGIS_Viewer3DMode.Select;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!GIS.View3D)
                GIS.FullExtent();
            else
                GIS.Viewer3D.ResetView();
        }

        private void btnRoof_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;

            lv = ((TGIS_LayerVector)GIS.Get("buildings"));
            if (lv == null) return;


            if (lv.Params.Area.Pattern == TGIS_BrushStyle.Clear)
            {
                btnRoof.Text = "Hide roof";
                lv.Params.Area.Pattern = TGIS_BrushStyle.Solid;
            }
            else
            {
                lv.Params.Area.Pattern = TGIS_BrushStyle.Clear;
                btnRoof.Text = "Show roof";
            };
            GIS.Viewer3D.UpdateWholeMap();
        }

        private void btnWalls_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;

            lv = ((TGIS_LayerVector)GIS.Get("buildings"));
            if (lv == null) return;


            if (lv.Params.Area.OutlinePattern == TGIS_BrushStyle.Clear)
            {
                btnWalls.Text = "Hide walls";
                lv.Params.Area.OutlinePattern = TGIS_BrushStyle.Solid;
            }
            else
            {
                lv.Params.Area.OutlinePattern = TGIS_BrushStyle.Clear;
                btnWalls.Text = "Show walls";
            };
            GIS.Viewer3D.UpdateWholeMap();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;
            TGIS_Shape shp;

            GIS.Lock();
            try
            {
                GIS.Close();

                lv = new TGIS_LayerVector();
                lv.Name = "volumetric_lines";
                GIS.Add(lv);
                shp = lv.CreateShape(TGIS_ShapeType.Arc, TGIS_DimensionType.XYZ) as TGIS_ShapeArc;
                shp.Params.Line.Color = TGIS_Color.Red;
                shp.Params.Line.Width = 450;
                shp.Params.Line.OutlinePattern = TGIS_BrushStyle.Clear;
                shp.Lock(TGIS_Lock.Projection);
                shp.AddPart();
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(-50, 50, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(50, 0, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(50, 50, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(50, 50, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(100, 50, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(150, 50, 0));
                shp.Unlock();

                shp = lv.CreateShape(TGIS_ShapeType.Arc, TGIS_DimensionType.XYZ) as TGIS_ShapeArc;
                shp.Params.Line.Color = TGIS_Color.Blue;
                shp.Params.Line.Width = 350;
                shp.Params.Line.OutlinePattern = TGIS_BrushStyle.Clear;
                shp.Lock(TGIS_Lock.Projection);
                shp.AddPart();
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(-50, 40, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, -10, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(60, -10, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(60, 40, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(60, 40, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(110, 40, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(160, 40, 0));
                shp.Unlock();

                shp = lv.CreateShape(TGIS_ShapeType.Arc, TGIS_DimensionType.XYZ) as TGIS_ShapeArc;
                shp.Params.Line.Color = TGIS_Color.Green;
                shp.Params.Line.Width = 250;
                shp.Params.Line.OutlinePattern = TGIS_BrushStyle.Clear;
                shp.Lock(TGIS_Lock.Projection);
                shp.AddPart();
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(-50, 30, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, -20, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(70, -20, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(70, 30, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(70, 30, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(120, 30, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(170, 30, 0));
                shp.Unlock();

                shp = lv.CreateShape(TGIS_ShapeType.Arc, TGIS_DimensionType.XYZ) as TGIS_ShapeArc;
                shp.Params.Line.Color = TGIS_Color.Yellow;
                shp.Params.Line.Width = 150;
                shp.Params.Line.OutlinePattern = TGIS_BrushStyle.Clear;
                shp.Lock(TGIS_Lock.Projection);
                shp.AddPart();
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(-50, 20, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, -30, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(80, -30, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(80, 20, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(80, 20, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(130, 20, 50));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(120, 30, 50));
                shp.Unlock();

                GIS.FullExtent();
                btn2D3D_Click(this, EventArgs.Empty);
                GIS.Viewer3D.ShowLights = true;
            }
            finally
            {
                GIS.Unlock();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;
            TGIS_Shape shp;

            GIS.Lock();
            try
            {
                GIS.Close();

                lv = new TGIS_LayerVector();
                lv.Name = "multipatch";
                lv.Params.Area.Color = TGIS_Color.Yellow;
                GIS.Add(lv);
                shp = lv.CreateShape(TGIS_ShapeType.MultiPatch, TGIS_DimensionType.XYZ) as TGIS_ShapeMultiPatch;
                shp.Lock(TGIS_Lock.Projection);

                shp.AddPart();
                shp.SetPartType(0, TGIS_PartType.TriangleFan);
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(5, 4, 10));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 5));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 0, 5));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 8, 5));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 8, 5));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 5));
                shp.AddPart();
                shp.SetPartType(1, TGIS_PartType.TriangleStrip);
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 0, 5));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 0, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 8, 5));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 8, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 8, 5));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 8, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 5));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 0));
                shp.AddPart();
                shp.SetPartType(2, TGIS_PartType.OuterRing);
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(4, 0, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(4, 0, 3));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(6, 0, 3));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(6, 0, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 0, 0));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(10, 0, 5));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 5));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(0, 0, 0));
                shp.AddPart();
                shp.SetPartType(3, TGIS_PartType.InnerRing);
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(1, 0, 2));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(1, 0, 4));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(3, 0, 4));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(3, 0, 2));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(1, 0, 2));
                shp.AddPart();
                shp.SetPartType(4, TGIS_PartType.InnerRing);
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(7, 0, 2));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(7, 0, 4));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(9, 0, 4));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(9, 0, 2));
                shp.AddPoint3D(TGIS_Utils.GisPoint3D(7, 0, 2));

                shp.Unlock();
                GIS.FullExtent();
                GIS.Zoom = GIS.Zoom / 2;
                btn2D3D_Click(this, EventArgs.Empty);
                GIS.Viewer3D.CameraPosition = TGIS_Utils.GisPoint3D(10 * (Math.PI / 180), 200 * (Math.PI / 180), 28);
                GIS.Viewer3D.ShowLights = true;
                GIS.Viewer3D.ShowVectorEdges = false;
            }
            finally
            {
                GIS.Unlock();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (GIS.View3D)
            {
                GIS.Viewer3D.LightVector = !GIS.Viewer3D.LightVector;
                GIS.Viewer3D.UpdateWholeMap();
            };
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!GIS.View3D) return;
            if (!GIS.Viewer3D.AdvNavigation)
            {
                GIS.Viewer3D.AdvNavigation = true;
                btnNavigation.Text = "Std. Navigation";
                GIS.Viewer3D.FastMode = true;
                btnRefresh.Text = "Unlock Refresh";
            }
            else
            {
                GIS.Viewer3D.AdvNavigation = false;
                btnNavigation.Text = "Adv. Navigation";
                GIS.Viewer3D.FastMode = false;
                btnRefresh.Text = "Lock Refresh";
            };

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!GIS.View3D) return;
            if (!GIS.Viewer3D.FastMode)
            {
                GIS.Viewer3D.FastMode = true;
                btnRefresh.Text = "Unlock Refresh";
            }
            else
            {
                GIS.Viewer3D.FastMode = false;
                btnRefresh.Text = "Lock Refresh";
            };
        }

        private void btnTextures_Click(object sender, EventArgs e)
        {
            TGIS_LayerVector lv;

            lv = ((TGIS_LayerVector)GIS.Get("buildings"));
            if (lv == null) return;

            if (lv.Params.Area.Bitmap==null)
            {
                TGIS_Bitmap bmp = new TGIS_Bitmap();
                try
                {
                    btnTextures.Text = "Hide Textures";
                    bmp.LoadFromBitmap((Bitmap)pictureBox2.Image, "");
                    lv.Params.Area.Bitmap=bmp;
                    bmp.LoadFromBitmap((Bitmap)pictureBox1.Image, "");
                    lv.Params.Area.OutlineBitmap = bmp;
                }
                finally
                {
                    bmp.Dispose();
                }
            }
            else
            {
                btnTextures.Text = "Show Textures";
                lv.Params.Area.Bitmap = null;
                lv.Params.Area.OutlineBitmap = null;
            };

            GIS.Viewer3D.UpdateWholeMap();
        }

        private void GIS_MouseDown(object sender, MouseEventArgs e)
        {

            TGIS_Shape shp;
            int prec;


            // if there is no layer or we are not in select mode, exit
            if (GIS.IsEmpty) return;
            if (GIS.IsEmpty) return;

            if (GIS.View3D)
            {
                if (GIS.Viewer3D.Mode == TGIS_Viewer3DMode.Select)
                {
                    prec = 20;
                    shp = (TGIS_Shape)(GIS.Locate(new Point(e.X, e.Y), prec));
                    if (shp != null)
                    {
                        shp.IsSelected = !shp.IsSelected;
                        GIS.Viewer3D.UpdateAllSelectedObjects();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GIS.MouseWheel += GIS_MouseWheel;

        }

        private void GIS_MouseWheel(object sender, MouseEventArgs e)
        {
            if (GIS.IsEmpty)
                return;

            if (GIS.View3D)
            {
                // allows MouseWheel works properly in ZoomMode
                GIS.Viewer3D.StoreMousePos(new Point(e.X, e.Y));

                TGIS_Point3D cam;
                cam = GIS.Viewer3D.CameraPosition;
                if (e.Delta < 0)
                    cam.Z = cam.Z * (1 + 0.05);
                else
                    cam.Z = cam.Z / (1 + 0.05);
                GIS.Viewer3D.CameraPosition = cam;
            }
            else {
                if (e.Delta < 0)
                    GIS.ZoomBy(2.0 / 3.0, e.X, e.Y - GIS.Top);
                else
                    GIS.ZoomBy(3.0 / 2.0, e.X, e.Y - GIS.Top);
            }
        }
    }
}
