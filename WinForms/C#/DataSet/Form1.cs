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
    /* DataSet sample — demonstrates TGIS_DataSet as a live bridge between a GIS vector layer
       and a standard data grid.

       What the sample shows:
         - Loading a vector shapefile (California Counties) into the GIS viewer
         - Creating a TGIS_DataSet bound to the layer's attribute table
         - Connecting the DataSet to a DataGridView for table display
         - Bidirectional synchronization: grid rows ↔ map selection
         - Clicking grid row auto-pans and zooms the viewer to the selected county
         - Grid displays all shape attributes in editable columns
         - Real-time map-to-grid and grid-to-map interaction
         - CurrentCellChanged event handles selection updates

       Key TatukGIS API concepts shown here:
         TGIS_ViewerWnd              - main visual map control
         TGIS_LayerVector            - vector layer backing the data
         TGIS_DataSet                - attribute table bridge (implements IDataSource)
         DataGridView                - standard .NET data-aware grid control
         GIS_DataSet.ActiveShape     - currently selected shape in grid
         TGIS_Extent                 - geographic bounding box for zoom-to
         Shape attributes            - grid columns bound to feature fields
         GIS_UID field               - unique identifier for synchronization
    */
    public partial class WinForm : Form
    {
        public WinForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the California Counties shapefile, opens the DataSet on the layer, and binds it
        /// to the data grid.
        /// </summary>
        private void WinForm_Load(object sender, EventArgs e)
        {
            TGIS_LayerVector ll;

            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() + @"World\Countries\USA\States\California\tl_2008_06_county.shp");
            ll = (TGIS_LayerVector)GIS.Items[0];
            ll.Params.Labels.Field = "GIS_UID";
            GIS_DataSet.Open((TGIS_LayerVector)GIS.Items[0], GIS.Extent);
            dataGrid1.DataSource = this.GIS_DataSet.Tables[0];
        }

        /// <summary>
        /// Pans and zooms the map to the county corresponding to the newly selected grid row.
        /// </summary>
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
