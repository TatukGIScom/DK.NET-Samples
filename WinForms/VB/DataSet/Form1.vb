Imports System.Windows.Forms
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Public Class Form1
    <STAThread>
    Shared Sub Main()
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

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim ll As TGIS_LayerVector

        GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\tl_2008_06_county.shp")
        ll = CType(GIS.Items(0), TGIS_LayerVector)
        ll.Params.Labels.Field = "GIS_UID"
        GIS_DataSet.Open(ll, GIS.Extent)
        Me.DataGridView1.DataSource = Me.GIS_DataSet.Tables(0)
    End Sub
End Class
