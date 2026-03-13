using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TatukGIS.NDK;

namespace ShowOperations
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            currShape = null;
            edtShape = null;

            rbMove.IsChecked = true;
            rbMove_Click(this, new RoutedEventArgs());

            GIS.Lock();
            GIS.Open(TGIS_Utils.GisSamplesDataDir() + @"Samples\3D\buildings.shp");

            edtLayer = new TGIS_LayerVector();
            edtLayer.CS = GIS.CS;
            edtLayer.CachedPaint = false; // makes tracking layer
            edtLayer.Params.Area.Pattern = TGIS_BrushStyle.Clear;
            edtLayer.Params.Area.OutlineColor = TGIS_Color.Red;
            GIS.Add(edtLayer);
            GIS.Unlock();

            GIS.Zoom = GIS.Zoom * 4;

            mode = GIS.Mode;
            checkBox.IsChecked = GIS.IsManipulationEnabled;
            enableSelection();
        }
        private void rbRotate_Click(object sender, RoutedEventArgs e)
        {
            lbHint.Content = "Use long tap to select a shape to start rotating";
        }

        private void rbScale_Click(object sender, RoutedEventArgs e)
        {
            lbHint.Content = "Use long tap to select a shape to start scaling";
        }

        private void rbMove_Click(object sender, RoutedEventArgs e)
        {
            lbHint.Content = "Use long tap to select a shape to start moving";
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

        private void GIS_TapLongEvent(object _sender, TatukGIS.NDK.WPF.TGIS_TapRoutedEventArgs _e)
        {
            if (GIS.IsEmpty) return;
            if (GIS.Mode != TGIS_ViewerMode.Select) return;
            
            currShape = null;
            edtShape = null;

            // find the clicked shape 
            System.Drawing.Point pt = new System.Drawing.Point(Convert.ToInt32(_e.X), Convert.ToInt32(_e.Y));
            TGIS_Shape shp = (TGIS_Shape)GIS.Locate(GIS.ScreenToMap(pt), 5 / GIS.Zoom);

            if (shp == null) return;

            // this temporary setting makes the default mouse (or touch) behavior disabled
            changeViewerMode(TGIS_ViewerMode.UserDefined);

            // add a 'red' copied shape
            currShape = shp.MakeEditable();
            edtShape = edtLayer.AddShape(currShape.CreateCopy());

            lbHint.Content = "Selected shape : " + currShape.Uid;

            // remember the starting point
            prevPtg = GIS.ScreenToMap(pt);
            prevX = pt.X;
            prevY = pt.Y;

            // refresh the 'red' shape only
            GIS.InvalidateTopmost();
        }

        private void GIS_MouseMove(object sender, MouseEventArgs e)
        {
            // will be fired also for touches when IsManipulationEnabled = False
            // content similar to GIS_ManipulationDelta

            disableSelection(e.LeftButton == MouseButtonState.Pressed);

            // ProcessMessages will not be required 
            operateShape(e.MouseDevice.GetPosition(GIS), false);
        }

        private void GIS_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // will be fired also for touches when IsManipulationEnabled = False
            // content similar to GIS_ManipulationCompleted

            selectShape(e.MouseDevice.GetPosition(GIS));
            endOperatingShape();
            enableSelection();
        }

        private void GIS_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            // will be fired for touches when IsManipulationEnabled = True
            // content similar to GIS_MouseMove

            disableSelection(true);

            // ProcessMessages will be required 
            operateShape(e.ManipulationOrigin, true);
        }

        private void GIS_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            // will be fired for touches when IsManipulationEnabled = True
            // content similar to GIS_MouseUp

            selectShape(e.ManipulationOrigin);
            endOperatingShape();
            enableSelection();
        }

        private void selectShape(System.Windows.Point _pt)
        {
            if (GIS.IsEmpty) return;
            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            if (pointerMoved) return;

            TGIS_Point ptg;
            TGIS_Shape shp;

            // let's locate a shape after click
            ptg = GIS.ScreenToMap(new System.Drawing.Point(Convert.ToInt32(_pt.X), Convert.ToInt32(_pt.Y)));
            shp = (TGIS_Shape)(GIS.Locate(ptg, 5 / GIS.Zoom)); // 5 pixels precision
            if (shp == null) return;

            // set it as selected
            shp.IsSelected = !shp.IsSelected;
        }

        private void operateShape(System.Windows.Point _pt, bool _processMessages)
        {
            if (GIS.Mode != TGIS_ViewerMode.UserDefined) return;

            if (edtShape == null) return;

            TGIS_Point ptg;
            ptg = GIS.ScreenToMap(new System.Drawing.Point(Convert.ToInt32(_pt.X), Convert.ToInt32(_pt.Y)));

            if (rbRotate.IsChecked == true)
                // Rotate by moving the mouse horizontally
                RotateSelectedShape(edtShape, ((Math.PI / 180) * ((_pt.X - prevX))));
            else if (rbScale.IsChecked == true)
            {
                if ((prevX != 0) && (prevY != 0))
                    ScaleSelectedShape(edtShape, _pt.X / prevX, _pt.Y / prevY);
            }
            else if (rbMove.IsChecked == true)
                TranslateSelectedShape(edtShape, (ptg.X - prevPtg.X), (ptg.Y - prevPtg.Y));

            // refresh tracking layer
            GIS.InvalidateTopmost();
            if (_processMessages)
                GIS.ControlProcessMessages();

            prevPtg.X = ptg.X;
            prevPtg.Y = ptg.Y;
            prevX = _pt.X;
            prevY = _pt.Y;
        }

        private void endOperatingShape()
        {
            if (GIS.Mode != TGIS_ViewerMode.UserDefined) return;

            if (edtShape == null) return;

            lbHint.Content = "No selected shape. Select a shape";
            // copy the new geometry to the selected shape
            currShape.CopyGeometry(edtShape);
            // clear the 'red' layer
            edtLayer.RevertAll();
            currShape = null;
            edtShape = null;
            // restore the initial mode
            changeViewerMode(mode);
            // refresh all layers
            GIS.InvalidateWholeMap();
        }

        private void disableSelection(bool _pressed)
        {
            if (GIS.Mode == TGIS_ViewerMode.Select)
            {
                if (_pressed)
                    pointerMoved = true;
            }
        }

        private void enableSelection()
        {
            pointerMoved = false;
        }

        private void changeViewerMode(TGIS_ViewerMode _mode)
        {
            GIS.Mode = _mode;
            if (GIS.Mode == TGIS_ViewerMode.Select)
            {
                btnMode.Content = "Mode: Select";
            }
            else if (GIS.Mode == TGIS_ViewerMode.UserDefined)
            {
                btnMode.Content = "Mode: UserDefined";
            }
            checkBox.IsChecked = GIS.IsManipulationEnabled;
        }

        private TGIS_Shape currShape;
        private TGIS_Shape edtShape;
        private TGIS_LayerVector edtLayer;
        private double prevX;
        private double prevY;
        private TGIS_Point prevPtg;
        private TGIS_ViewerMode mode;
        private bool pointerMoved;
    }
}
