using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TatukGIS.NDK;

namespace DataSet
{
    public partial class WinForm : Form
    {
        public WinForm()
        {
            InitializeComponent();
        }

        private void WinForm_Load(object sender, EventArgs e)
        {
            TGIS_LayerVector ll;

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"\World\Countries\USA\States\California\tl_2008_06_county.shp");
            ll = (TGIS_LayerVector)GIS.Items[0];
            ll.Params.Labels.Field = "GIS_UID";
            GIS_DataSet.Open((TGIS_LayerVector)GIS.Items[0], GIS.Extent);
            dataGrid1.DataSource = this.GIS_DataSet.Tables[0];
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGrid1.CurrentRow == null) return;
            GIS_DataSet.CurrentUid = Convert.ToInt32(dataGrid1.CurrentRow.Cells["GIS_UID"].Value);
            if (GIS_DataSet.ActiveShape != null)
            {
                GIS.Lock();
                GIS.VisibleExtent = GIS_DataSet.ActiveShape.Extent;
                GIS.Zoom = GIS.Zoom * 0.8;
                GIS.Unlock();
            }
        }
    }
}
