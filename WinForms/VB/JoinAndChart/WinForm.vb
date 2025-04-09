Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.Data.OleDb
Imports TatukGIS.NDK
Imports TatukGIS.NDK.WinForms

Namespace JoinAndChart
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer
        Private panel1 As System.Windows.Forms.Panel
        Private panel2 As System.Windows.Forms.Panel
        Private panel3 As System.Windows.Forms.Panel
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private btnZooIn As System.Windows.Forms.ToolStripButton
        Private btnZoomOut As System.Windows.Forms.ToolStripButton
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton
        Private imageList1 As System.Windows.Forms.ImageList
        Private toolBar2 As System.Windows.Forms.ToolStrip
        Private WithEvents cmbSize As System.Windows.Forms.ComboBox
        Private toolBarButton2 As System.Windows.Forms.ToolStripButton
        Private WithEvents cmbValues As System.Windows.Forms.ComboBox
        Private panel4 As System.Windows.Forms.Panel
        Private toolBar3 As System.Windows.Forms.ToolStrip
        Private toolBarButton3 As System.Windows.Forms.ToolStripButton
        Private WithEvents cmbStyle As System.Windows.Forms.ComboBox
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private toolTip2 As System.Windows.Forms.ToolTip
        Private toolTip3 As System.Windows.Forms.ToolTip
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private sqlConnection As System.Data.OleDb.OleDbConnection
        Private sqlCommand As System.Data.OleDb.OleDbCommand
        Private sqlDA As System.Data.OleDb.OleDbDataAdapter
        Private sqlDS As System.Data.DataSet
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            Me.ActiveControl = GIS
            Me.toolTip1.SetToolTip(Me.cmbSize, "Chart Size")
            Me.toolTip2.SetToolTip(Me.cmbValues, "Chart Values")
            Me.toolTip3.SetToolTip(Me.cmbStyle, "Chart Style")
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel4 = New System.Windows.Forms.Panel()
            Me.cmbStyle = New System.Windows.Forms.ComboBox()
            Me.toolBar3 = New System.Windows.Forms.ToolStrip()
            Me.toolBarButton3 = New System.Windows.Forms.ToolStripButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.cmbValues = New System.Windows.Forms.ComboBox()
            Me.toolBar2 = New System.Windows.Forms.ToolStrip()
            Me.toolBarButton2 = New System.Windows.Forms.ToolStripButton()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.cmbSize = New System.Windows.Forms.ComboBox()
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZooIn = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.toolTip2 = New System.Windows.Forms.ToolTip(Me.components)
            Me.toolTip3 = New System.Windows.Forms.ToolTip(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.panel4.SuspendLayout()
            Me.panel3.SuspendLayout()
            Me.panel2.SuspendLayout()
            Me.SuspendLayout()
            '
            'panel1
            '
            Me.panel1.Controls.Add(Me.panel4)
            Me.panel1.Controls.Add(Me.panel3)
            Me.panel1.Controls.Add(Me.panel2)
            Me.panel1.Dock = System.Windows.Forms.DockStyle.Top
            Me.panel1.Location = New System.Drawing.Point(0, 0)
            Me.panel1.Name = "panel1"
            Me.panel1.Size = New System.Drawing.Size(592, 24)
            Me.panel1.TabIndex = 0
            '
            'panel4
            '
            Me.panel4.Controls.Add(Me.cmbStyle)
            Me.panel4.Controls.Add(Me.toolBar3)
            Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel4.Location = New System.Drawing.Point(393, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(199, 24)
            Me.panel4.TabIndex = 2
            '
            'cmbStyle
            '
            Me.cmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbStyle.Items.AddRange(New Object() {"Pie", "Bar"})
            Me.cmbStyle.Location = New System.Drawing.Point(8, 2)
            Me.cmbStyle.Name = "cmbStyle"
            Me.cmbStyle.Size = New System.Drawing.Size(88, 21)
            Me.cmbStyle.TabIndex = 1
            '
            'toolBar3
            '
            
            Me.toolBar3.AutoSize = False
            Me.toolBar3.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.toolBarButton3})

            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowItemToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(199, 24)
            Me.toolBar3.TabIndex = 0
            '
            'toolBarButton3
            '
            Me.toolBarButton3.Name = "toolBarButton3"
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.cmbValues)
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(222, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(171, 24)
            Me.panel3.TabIndex = 1
            '
            'cmbValues
            '
            Me.cmbValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbValues.Items.AddRange(New Object() {"black:white", "pop2000:square_mil", "male2000:female2000"})
            Me.cmbValues.Location = New System.Drawing.Point(8, 2)
            Me.cmbValues.Name = "cmbValues"
            Me.cmbValues.Size = New System.Drawing.Size(163, 21)
            Me.cmbValues.TabIndex = 1
            '
            'toolBar2
            '
            
            Me.toolBar2.AutoSize = False
            Me.toolBar2.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.toolBarButton2})

            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowItemToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(171, 24)
            Me.toolBar2.TabIndex = 0
            '
            'toolBarButton2
            '
            Me.toolBarButton2.Name = "toolBarButton2"
            
            '
            'panel2
            '
            Me.panel2.Controls.Add(Me.cmbSize)
            Me.panel2.Controls.Add(Me.toolBar1)
            Me.panel2.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel2.Location = New System.Drawing.Point(0, 0)
            Me.panel2.Name = "panel2"
            Me.panel2.Size = New System.Drawing.Size(222, 24)
            Me.panel2.TabIndex = 0
            '
            'cmbSize
            '
            Me.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbSize.Items.AddRange(New Object() {"pop2000", "male2000", "female2000", "under18", "asia", "black", "white", "hisp_lat"})
            Me.cmbSize.Location = New System.Drawing.Point(77, 2)
            Me.cmbSize.Name = "cmbSize"
            Me.cmbSize.Size = New System.Drawing.Size(145, 21)
            Me.cmbSize.TabIndex = 1
            '
            'toolBar1
            '
            
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.btnZooIn, Me.btnZoomOut, Me.toolBarButton1})
            
            
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(222, 24)
            Me.toolBar1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'btnZooIn
            '
            Me.btnZooIn.ImageIndex = 1
            Me.btnZooIn.Name = "btnZooIn"
            Me.btnZooIn.ToolTipText = "Zoom In"
            '
            'btnZoomOut
            '
            Me.btnZoomOut.ImageIndex = 2
            Me.btnZoomOut.Name = "btnZoomOut"
            Me.btnZoomOut.ToolTipText = "Zoom Out"
            '
            'toolBarButton1
            '
            Me.toolBarButton1.Name = "toolBarButton1"
            
            '
            'imageList1
            '
            Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
            Me.imageList1.TransparentColor = System.Drawing.Color.Fuchsia
            Me.imageList1.Images.SetKeyName(0, "")
            Me.imageList1.Images.SetKeyName(1, "")
            Me.imageList1.Images.SetKeyName(2, "")
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 1
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.ControlLight
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(0, 24)
            Me.GIS.MinZoomSize = -5
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(592, 423)
            Me.GIS.TabIndex = 0
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Join and Chart"
            Me.panel1.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel3.ResumeLayout(False)
            Me.panel2.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(false)
            Application.Run(New WinForm())
        End Sub

        Private Sub WinForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim ll As TGIS_LayerSHP

            cmbSize.SelectedIndex = 0
            cmbValues.SelectedIndex = 0
            cmbStyle.SelectedIndex = 0

            ' create ADO .NET connection object
            sqlConnection = New OleDbConnection()
            sqlConnection.ConnectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}Stats.mdb", (TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\Statistical\"))
            sqlConnection.Open()

            ' use layer to display charts
            ll = New TGIS_LayerSHP()
            ll.Path = TGIS_Utils.GisSamplesDataDirDownload() & "\World\Countries\USA\States\California\tl_2008_06_county.shp"
            ll.Name = "tl_2008_06_county"

            ll.UseConfig = False
            ll.Params.Labels.Field = "name"
            ll.Params.Labels.Pattern = TGIS_BrushStyle.Clear
            ll.Params.Labels.OutlineWidth = 0
            ll.Params.Labels.FontColor = TGIS_Color.White
            ll.Params.Labels.Color = TGIS_Color.Black
            ll.Params.Labels.Position = TGIS_LabelPosition.MiddleCenter Or TGIS_LabelPosition.Flow

            GIS.Add(ll)
            GIS.FullExtent()

            cmb_SelectedIndexChanged(Me, e)
        End Sub

        Private Sub WinForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            sqlConnection.Close()
        End Sub

        Private Sub cmb_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStyle.SelectedIndexChanged, cmbValues.SelectedIndexChanged, cmbSize.SelectedIndexChanged
            Dim ll As TGIS_LayerVector
            Dim vsize As String
            Dim vvalues As String
            Dim vstyle As String
            Dim cstring As String
            Dim vmin As Double
            Dim vmax As Double

            If GIS.Items.Count = 0 Then
                Return
            End If
            ll = CType(GIS.Items(0), TGIS_LayerVector)
            If ll Is Nothing Then
                Return
            End If

            ' get params
            vsize = cmbSize.Items(cmbSize.SelectedIndex).ToString()
            vvalues = cmbValues.Items(cmbValues.SelectedIndex).ToString()
            vstyle = cmbStyle.Items(cmbStyle.SelectedIndex).ToString()

            ' find min, max values for shapes
            cstring = String.Format("SELECT min({0}) AS mini, max({1}) AS maxi FROM ce2000t", vsize, vsize)
            sqlCommand = New OleDbCommand(cstring, sqlConnection)
            sqlDA = New OleDbDataAdapter()
            sqlDA.SelectCommand = sqlCommand
            sqlDS = New DataSet()
            sqlDA.Fill(sqlDS)

            vmin = Double.Parse(sqlDS.Tables(0).Rows(0)("mini").ToString())
            vmax = Double.Parse(sqlDS.Tables(0).Rows(0)("maxi").ToString())

            ' let's load data to recordset
            cstring = "select * FROM ce2000t ORDER BY fips"
            sqlCommand = New OleDbCommand(cstring, sqlConnection)
            sqlDA = New OleDbDataAdapter()
            sqlDA.SelectCommand = sqlCommand
            sqlDS = New DataSet()
            sqlDA.Fill(sqlDS)

            ' connect layer with query results
            ll.JoinNET = sqlDS.Tables(0)

            ' set primary and foreign keys
            ll.JoinPrimary = "cntyidfp"
            ll.JoinForeign = "fips"

            ' Set the chart style: "Pie" or "Bar"
            ll.Params.Chart.Style = TGIS_Utils.ParamChart(vstyle, TGIS_ChartStyle.Pie)
            
            ' The chart size will be set by Render in the range of 350 to 1000
            ' depending on the value of the "vsize" field
            ll.Params.Chart.Size = TGIS_Utils.GIS_RENDER_SIZE()
            ll.Params.Render.StartSize = 350
            ll.Params.Render.EndSize = 1000
            ll.Params.Render.Expression = vsize
            
            ' The Renderer will create 10 zones to group field values,
            ' starting from "vmin" and edning with "vmax"
            ll.Params.Render.Zones = 10
            ll.Params.Render.MinVal = vmin
            ll.Params.Render.MaxVal = vmax

            ' For 'Bar' chart you can replace '0:0' by 'min:max' to set custom Y-axis limits.
            ' 'vvalues' contains list of values displayed on the chart.
            ' In this sample field names are used, e.g. 'male2000:female2000'.
            ' Values need to be divided by a colon ':'.
            ll.Params.Render.Chart = "0:0:" & vvalues

            ' If necessary, the chart can also be included in the legend
            ll.Params.Chart.Legend = ll.Params.Render.Chart
            ll.Params.Chart.ShowLegend = True
            
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                Case 0
                    ' btnFullExt
                    GIS.FullExtent()
                Case 1
                    ' btnZoomIn
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom * 2
                Case 2
                    ' btnZoomOut
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom / 2
            End Select
        End Sub

        Private Sub toolBar1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles toolBar1.MouseMove
            Dim p As Point = New Point(e.X, e.Y)

            If toolBar1.Items(0).Bounds.Contains(p) OrElse toolBar1.Items(1).Bounds.Contains(p) OrElse toolBar1.Items(2).Bounds.Contains(p) Then
                toolBar1.Cursor = Cursors.Hand
            Else
                toolBar1.Cursor = Cursors.Default
            End If
        End Sub
    End Class
End Namespace
