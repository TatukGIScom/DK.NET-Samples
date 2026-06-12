Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

' DataSet sample — demonstrates TGIS_DataSet as a live bridge between a GIS vector layer
' and a standard data grid.
'
' What the sample shows:
'   - Loading a vector shapefile (California Counties) into the GIS viewer
'   - Creating a TGIS_DataSet bound to the layer's attribute table
'   - Connecting the DataSet to a DataGridView for table display
'   - Bidirectional synchronization: grid rows ↔ map selection
'   - Clicking grid row auto-pans and zooms the viewer to the selected county
'   - Grid displays all shape attributes in editable columns
'   - Real-time map-to-grid and grid-to-map interaction
'   - CurrentCellChanged event handles selection updates
'
' Key TatukGIS API concepts shown here:
'   TGIS_ViewerWnd              - main visual map control
'   TGIS_LayerVector            - vector layer backing the data
'   TGIS_DataSet                - attribute table bridge (implements IDataSource)
'   DataGridView                - standard .NET data-aware grid control
'   GIS_DataSet.ActiveShape     - currently selected shape in grid
'   TGIS_Extent                 - geographic bounding box for zoom-to
'   Shape attributes            - grid columns bound to feature fields
'   GIS_UID field               - unique identifier for synchronization
Public Class Form1
    <STAThread>
    Shared Sub Main()
#If NET5_0_OR_GREATER Then
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2)
#End If
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        Application.Run(New Form1())
    End Sub

    Public Sub New()
        '
        ' Required for Windows Form Designer support
        '
        InitializeComponent()

        '
        ' TODO: Add any constructor code after InitializeComponent call
        '
    End Sub

    ''' <summary>Pans and zooms the map to the county corresponding to the newly selected grid row.</summary>
    Private Sub DataGridView1_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridView1.CurrentCellChanged
        If DataGridView1.CurrentRow Is Nothing Then
            Return
        End If
        GIS_DataSet.CurrentUid = Convert.ToInt32(DataGridView1.CurrentRow.Cells("GIS_UID").Value)
        If Not GIS_DataSet.ActiveShape Is Nothing Then
            GIS.Lock()
            GIS.VisibleExtent = GIS_DataSet.ActiveShape.Extent
            GIS.Zoom = GIS.Zoom * 0.8
            GIS.Unlock()
        End If
    End Sub

    ''' <summary>
    ''' Loads the California Counties shapefile, opens the DataSet on the layer, and binds it
    ''' to the data grid.
    ''' </summary>
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ll As TGIS_LayerVector

        GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "World\Countries\USA\States\California\tl_2008_06_county.shp")
        ll = CType(GIS.Items(0), TGIS_LayerVector)
        ll.Params.Labels.Field = "GIS_UID"
        GIS_DataSet.Open(ll, GIS.Extent)
        Me.DataGridView1.DataSource = Me.GIS_DataSet.Tables(0)
    End Sub
End Class
