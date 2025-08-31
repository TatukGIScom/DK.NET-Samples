using TatukGIS.NDK;
using TatukGIS.RTL;

namespace ViewerLite
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            GisLicense.Initialize();
            InitializeComponent();

            arrow.GIS_Viewer = GIS;
            legend.GIS_Viewer = GIS;
            scale.GIS_Viewer = GIS;
        }

        private void GISTapSimpleEvent(object sender, TGIS_TapEventArgs e)
        {
            TGIS_Shape shp;
            int x;
            int y;

            if (GIS.IsEmpty) return;

            x = (int)Math.Round(e.X);
            y = (int)Math.Round(e.Y);

            if (GIS.Mode != TGIS_ViewerMode.Select) return;

            // let's try to locate a selected shape on the map
            shp = (TGIS_Shape)GIS.Locate(
                    GIS.ScreenToMap(new System.Drawing.Point((int)Math.Round(e.X), (int)Math.Round(e.Y))),
                    5 / GIS.Zoom);

            if (shp == null) return;
            // if any found select it
            shp.IsSelected = !shp.IsSelected;
        }

        public void btnOpenClick(object sender, EventArgs args)
        {
            var path = TGIS_Utils.GisSamplesDataDirDownload();
            GIS.RotationAngle = 0;
            GIS.Open(path + "/World/Countries/Poland/DCW/poland.ttkproject");
            customizeButtonColors();
        }

        public void customizeButtonColors()
        {
            if (GIS.Mode == TGIS_ViewerMode.Select)
            {
                btnSelect.BackgroundColor = new Color(90, 90, 90);
                btnDrag.BackgroundColor = new Color(128, 128, 128);
                btnZoom.BackgroundColor = new Color(128, 128, 128);
                btnZoomEx.BackgroundColor = new Color(128, 128, 128);
            }
            else if (GIS.Mode == TGIS_ViewerMode.Drag)
            {
                btnSelect.BackgroundColor = new Color(128, 128, 128);
                btnDrag.BackgroundColor = new Color(90, 90, 90);
                btnZoom.BackgroundColor = new Color(128, 128, 128);
                btnZoomEx.BackgroundColor = new Color(128, 128, 128);
            }
            else if (GIS.Mode == TGIS_ViewerMode.Zoom)
            {
                btnSelect.BackgroundColor = new Color(128, 128, 128);
                btnDrag.BackgroundColor = new Color(128, 128, 128);
                btnZoom.BackgroundColor = new Color(90, 90, 90);
                btnZoomEx.BackgroundColor = new Color(128, 128, 128);
            }
            else if (GIS.Mode == TGIS_ViewerMode.ZoomEx)
            {
                btnSelect.BackgroundColor = new Color(128, 128, 128);
                btnDrag.BackgroundColor = new Color(128, 128, 128);
                btnZoom.BackgroundColor = new Color(128, 128, 128);
                btnZoomEx.BackgroundColor = new Color(90, 90, 90);
            }
            else
            {
                btnSelect.BackgroundColor = new Color(128, 128, 128);
                btnDrag.BackgroundColor = new Color(128, 128, 128);
                btnZoom.BackgroundColor = new Color(128, 128, 128);
                btnZoomEx.BackgroundColor = new Color(128, 128, 128);
            }
        }

        public void btnCloseClick(object sender, EventArgs args)
        {
            GIS.Close();
        }
        public void btnFullExtentClick(object sender, EventArgs args)
        {
            GIS.FullExtent();
        }
        public void btnCSClick(object sender, EventArgs args)
        {
            TGIS_CSGeographicCoordinateSystem wgs84 = TGIS_Utils.CSGeographicCoordinateSystemList.ByEPSG(4326);
            TGIS_CSProjectedCoordinateSystem mercator = TGIS_Utils.CSProjectedCoordinateSystemList.ByEPSG(3857);

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

        public void btnOpenDSClick(object sender, EventArgs args)
        {
            //if (GIS.IsEmpty) return;
        }
        public void btnSelectMode(object sender, EventArgs args)
        {
            GIS.Mode = TGIS_ViewerMode.Select;
            customizeButtonColors();
        }
        public void btnDragMode(object sender, EventArgs args)
        {
            GIS.Mode = TGIS_ViewerMode.Drag;
            customizeButtonColors();
        }
        public void btnZoomMode(object sender, EventArgs args)
        {
            GIS.Mode = TGIS_ViewerMode.Zoom;
            customizeButtonColors();
        }
        public void btnZoomExMode(object sender, EventArgs args)
        {
            GIS.Mode = TGIS_ViewerMode.ZoomEx;
            customizeButtonColors();
        }
        public void btnShowInfoClicked(object sender, EventArgs args)
        {

        }
    }
}