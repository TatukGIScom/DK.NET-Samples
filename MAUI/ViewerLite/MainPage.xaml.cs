using TatukGIS.NDK;

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

        public void btnOpenClick(object sender, EventArgs args)
        {
            var path = TGIS_Utils.GisSamplesDataDirDownload();
            GIS.RotationAngle = 0;
            GIS.Open(path + "/World/Countries/Poland/DCW/poland.ttkproject");
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

        public void btnHighResClicked(object sender, EventArgs args)
        {
            //GIS.HiRes = !GIS.HiRes;
        }
        public void btnSelectMode(object sender, EventArgs args)
        {
            GIS.Mode = TGIS_ViewerMode.Select;
            btnSelect.BackgroundColor = new Color(90, 90, 90);
            btnDrag.BackgroundColor = new Color(128, 128, 128);
            btnZoom.BackgroundColor = new Color(128, 128, 128);
            btnZoomEx.BackgroundColor = new Color(128, 128, 128);
        }
        public void btnDragMode(object sender, EventArgs args)
        {
            GIS.Mode = TGIS_ViewerMode.Drag;
            btnSelect.BackgroundColor = new Color(128, 128, 128);
            btnDrag.BackgroundColor = new Color(90, 90, 90);
            btnZoom.BackgroundColor = new Color(128, 128, 128);
            btnZoomEx.BackgroundColor = new Color(128, 128, 128);
        }
        public void btnZoomMode(object sender, EventArgs args)
        {
            GIS.Mode = TGIS_ViewerMode.Zoom;
            btnSelect.BackgroundColor = new Color(128, 128, 128);
            btnDrag.BackgroundColor = new Color(128, 128, 128);
            btnZoom.BackgroundColor = new Color(90, 90, 90);
            btnZoomEx.BackgroundColor = new Color(128, 128, 128);
        }
        public void btnZoomExMode(object sender, EventArgs args)
        {
            GIS.Mode = TGIS_ViewerMode.ZoomEx;
            btnSelect.BackgroundColor = new Color(128, 128, 128);
            btnDrag.BackgroundColor = new Color(128, 128, 128);
            btnZoom.BackgroundColor = new Color(128, 128, 128);
            btnZoomEx.BackgroundColor = new Color(90, 90, 90);
        }
        public void btnShowInfoClicked(object sender, EventArgs args)
        {

        }
    }
}