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
using TatukGIS.NDK.WPF;

namespace ViewerLite
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            GIS_Legend.GIS_Viewer = GIS;
            GIS_Scale.GIS_Viewer = GIS;
            GIS_Arrow.GIS_Viewer = GIS;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + "\\World\\WorldDCW\\world.shp");
        }

        private void btnZoom_Click(object sender, RoutedEventArgs e)
        {
            GIS.Mode = TGIS_ViewerMode.Zoom;
        }

        private void btnZoomEx_Click(object sender, RoutedEventArgs e)
        {
            GIS.Mode = TGIS_ViewerMode.ZoomEx;
        }

        private void btnDrag_Click(object sender, RoutedEventArgs e)
        {
            GIS.Mode = TGIS_ViewerMode.Drag;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            GIS.Mode = TGIS_ViewerMode.Select;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            GIS.Close();
        }

        private void btnFullExtent_Click(object sender, RoutedEventArgs e)
        {
            GIS.FullExtent();
        }

        private void btnCS_Click(object sender, RoutedEventArgs e)
        {
            TGIS_CSGeographicCoordinateSystem wgs84 ;
            TGIS_CSProjectedCoordinateSystem mercator ;

            wgs84 = TGIS_Utils.CSGeographicCoordinateSystemList.ByEPSG(4326);
            mercator = TGIS_Utils.CSProjectedCoordinateSystemList.ByEPSG(3857);
            if (GIS.CS == wgs84)
            {
                GIS.CS = mercator;
            }
            else if (GIS.CS == mercator)
            {
                GIS.CS = wgs84;
            }
            GIS.FullExtent();        
        }

        private void GIS_TapSimpleEvent(object _sender, TGIS_TapRoutedEventArgs _e)
        {
            if (GIS.IsEmpty) return;
            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            System.Drawing.Point pt = new System.Drawing.Point(Convert.ToInt32(_e.X), Convert.ToInt32(_e.Y));
            // let's try to locate a selected shape on the map
            TGIS_Shape shp = (TGIS_Shape)GIS.Locate(GIS.ScreenToMap(pt), 5 / GIS.Zoom);

            if (shp == null) return;
            // if any found select it
            shp.IsSelected = !shp.IsSelected;
        }

        private void GIS_TapLongEvent(object _sender, TGIS_TapRoutedEventArgs _e)
        {
            if (GIS.IsEmpty) return;
            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            TGIS_LayerVector ll = (TGIS_LayerVector)GIS.Items[0];
            ll.DeselectAll();
            System.Drawing.Point pt = new System.Drawing.Point(Convert.ToInt32(_e.X), Convert.ToInt32(_e.Y));
            TGIS_Shape shp = (TGIS_Shape)GIS.Locate(GIS.ScreenToMap(pt), 5 / GIS.Zoom);

            if (shp == null) return;
            // if any found select it and show shape info
            shp.IsSelected = !shp.IsSelected;
            ShowInfo(shp);
        }

        private void ShowInfo(TGIS_Shape _shp)
        {

        }
    }
}
