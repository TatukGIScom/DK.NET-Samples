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

Namespace JoinAndRender
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
        Private WithEvents toolBar1 As System.Windows.Forms.ToolStrip
        Private imageList1 As System.Windows.Forms.ImageList
        Private btnFullExtent As System.Windows.Forms.ToolStripButton
        Private btnZoomIn As System.Windows.Forms.ToolStripButton
        Private btnZoomOut As System.Windows.Forms.ToolStripButton
        Private toolBarButton1 As System.Windows.Forms.ToolStripButton
        Private WithEvents cmbSize As System.Windows.Forms.ComboBox
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private panel3 As System.Windows.Forms.Panel
        Private toolBar2 As System.Windows.Forms.ToolStrip
        Private toolBarButton2 As System.Windows.Forms.ToolStripButton
        Private WithEvents panColorStart As System.Windows.Forms.Panel
        Private WithEvents panColorEnd As System.Windows.Forms.Panel
        Private panel4 As System.Windows.Forms.Panel
        Private toolBar3 As System.Windows.Forms.ToolStrip
        Private toolBarButton3 As System.Windows.Forms.ToolStripButton
        Private WithEvents scrTransparency As System.Windows.Forms.TrackBar
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private colorDialog1 As System.Windows.Forms.ColorDialog
        Private tgiS_ControlLegend1 As TatukGIS.NDK.WinForms.TGIS_ControlLegend
        Private GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private sqlConnection As System.Data.OleDb.OleDbConnection
        Private sqlCommand As System.Data.OleDb.OleDbCommand
        Private sqlDA As System.Data.OleDb.OleDbDataAdapter
        Private sqlDS As System.Data.DataSet

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
            '?this.ActiveControl = GIS ;
            Me.toolTip1.SetToolTip(Me.cmbSize, "Chart Size")
            Me.toolTip1.SetToolTip(Me.panColorStart, "Click to change start color")
            Me.toolTip1.SetToolTip(Me.panColorEnd, "Click to change end color")
            Me.toolTip1.SetToolTip(Me.panColorEnd, "Transparency")
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
            Dim TgiS_ControlLegendDialogOptions2 As TatukGIS.NDK.TGIS_ControlLegendDialogOptions = New TatukGIS.NDK.TGIS_ControlLegendDialogOptions()
            Me.panel1 = New System.Windows.Forms.Panel()
            Me.panel4 = New System.Windows.Forms.Panel()
            Me.scrTransparency = New System.Windows.Forms.TrackBar()
            Me.toolBar3 = New System.Windows.Forms.ToolStrip()
            Me.toolBarButton3 = New System.Windows.Forms.ToolStripButton()
            Me.panel3 = New System.Windows.Forms.Panel()
            Me.panColorEnd = New System.Windows.Forms.Panel()
            Me.panColorStart = New System.Windows.Forms.Panel()
            Me.toolBar2 = New System.Windows.Forms.ToolStrip()
            Me.toolBarButton2 = New System.Windows.Forms.ToolStripButton()
            Me.panel2 = New System.Windows.Forms.Panel()
            Me.cmbSize = New System.Windows.Forms.ComboBox()
            Me.toolBar1 = New System.Windows.Forms.ToolStrip()
            Me.btnFullExtent = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomIn = New System.Windows.Forms.ToolStripButton()
            Me.btnZoomOut = New System.Windows.Forms.ToolStripButton()
            Me.toolBarButton1 = New System.Windows.Forms.ToolStripButton()
            Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.colorDialog1 = New System.Windows.Forms.ColorDialog()
            Me.tgiS_ControlLegend1 = New TatukGIS.NDK.WinForms.TGIS_ControlLegend()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            Me.panel1.SuspendLayout()
            Me.panel4.SuspendLayout()
            CType(Me.scrTransparency, System.ComponentModel.ISupportInitialize).BeginInit()
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
            Me.panel4.Controls.Add(Me.scrTransparency)
            Me.panel4.Controls.Add(Me.toolBar3)
            Me.panel4.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panel4.Location = New System.Drawing.Point(318, 0)
            Me.panel4.Name = "panel4"
            Me.panel4.Size = New System.Drawing.Size(274, 24)
            Me.panel4.TabIndex = 2
            '
            'scrTransparency
            '
            Me.scrTransparency.Location = New System.Drawing.Point(6, 0)
            Me.scrTransparency.Maximum = 100
            Me.scrTransparency.Name = "scrTransparency"
            Me.scrTransparency.Size = New System.Drawing.Size(137, 45)
            Me.scrTransparency.TabIndex = 1
            Me.scrTransparency.TickFrequency = 25
            Me.scrTransparency.Value = 100
            '
            'toolBar3
            '
            
            Me.toolBar3.AutoSize = False
            Me.toolBar3.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.toolBarButton3})
            
            Me.toolBar3.Location = New System.Drawing.Point(0, 0)
            Me.toolBar3.Name = "toolBar3"
            Me.toolBar3.ShowItemToolTips = True
            Me.toolBar3.Size = New System.Drawing.Size(274, 24)
            Me.toolBar3.TabIndex = 0
            '
            'toolBarButton3
            '
            Me.toolBarButton3.Name = "toolBarButton3"
            '
            'panel3
            '
            Me.panel3.Controls.Add(Me.panColorEnd)
            Me.panel3.Controls.Add(Me.panColorStart)
            Me.panel3.Controls.Add(Me.toolBar2)
            Me.panel3.Dock = System.Windows.Forms.DockStyle.Left
            Me.panel3.Location = New System.Drawing.Point(224, 0)
            Me.panel3.Name = "panel3"
            Me.panel3.Size = New System.Drawing.Size(94, 24)
            Me.panel3.TabIndex = 1
            '
            'panColorEnd
            '
            Me.panColorEnd.BackColor = System.Drawing.Color.Navy
            Me.panColorEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.panColorEnd.Location = New System.Drawing.Point(50, 2)
            Me.panColorEnd.Name = "panColorEnd"
            Me.panColorEnd.Size = New System.Drawing.Size(42, 22)
            Me.panColorEnd.TabIndex = 2
            '
            'panColorStart
            '
            Me.panColorStart.BackColor = System.Drawing.Color.Aqua
            Me.panColorStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.panColorStart.Location = New System.Drawing.Point(6, 2)
            Me.panColorStart.Name = "panColorStart"
            Me.panColorStart.Size = New System.Drawing.Size(42, 22)
            Me.panColorStart.TabIndex = 1
            '
            'toolBar2
            '
            
            Me.toolBar2.AutoSize = False
            Me.toolBar2.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.toolBarButton2})
            
            Me.toolBar2.Location = New System.Drawing.Point(0, 0)
            Me.toolBar2.Name = "toolBar2"
            Me.toolBar2.ShowItemToolTips = True
            Me.toolBar2.Size = New System.Drawing.Size(94, 24)
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
            Me.panel2.Size = New System.Drawing.Size(224, 24)
            Me.panel2.TabIndex = 0
            '
            'cmbSize
            '
            Me.cmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cmbSize.Items.AddRange(New Object() {"pop2000", "under18", "asia", "black", "white", "hisp_lat", "male2000", "female2000"})
            Me.cmbSize.Location = New System.Drawing.Point(77, 2)
            Me.cmbSize.Name = "cmbSize"
            Me.cmbSize.Size = New System.Drawing.Size(145, 21)
            Me.cmbSize.TabIndex = 1
            Me.cmbSize.TabStop = False
            '
            'toolBar1
            '
            
            Me.toolBar1.AutoSize = False
            Me.toolBar1.Items.AddRange(New System.Windows.Forms.ToolStripButton() {Me.btnFullExtent, Me.btnZoomIn, Me.btnZoomOut, Me.toolBarButton1})
            
            Me.toolBar1.ImageList = Me.imageList1
            Me.toolBar1.Location = New System.Drawing.Point(0, 0)
            Me.toolBar1.Name = "toolBar1"
            Me.toolBar1.ShowItemToolTips = True
            Me.toolBar1.Size = New System.Drawing.Size(224, 24)
            Me.toolBar1.TabIndex = 0
            '
            'btnFullExtent
            '
            Me.btnFullExtent.ImageIndex = 0
            Me.btnFullExtent.Name = "btnFullExtent"
            Me.btnFullExtent.ToolTipText = "Full Extent"
            '
            'btnZoomIn
            '
            Me.btnZoomIn.ImageIndex = 1
            Me.btnZoomIn.Name = "btnZoomIn"
            Me.btnZoomIn.ToolTipText = "Zoom In"
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
            Me.statusBar1.Text = "Use the trackbar to change transparency"
            '
            'tgiS_ControlLegend1
            '
            TgiS_ControlLegendDialogOptions2.VectorWizardUniqueLimit = 256
            TgiS_ControlLegendDialogOptions2.VectorWizardUniqueSearchLimit = 16384
            Me.tgiS_ControlLegend1.DialogOptions = TgiS_ControlLegendDialogOptions2
            Me.tgiS_ControlLegend1.Dock = System.Windows.Forms.DockStyle.Left
            Me.tgiS_ControlLegend1.GIS_Group = Nothing
            Me.tgiS_ControlLegend1.GIS_Layer = Nothing
            Me.tgiS_ControlLegend1.GIS_Viewer = Me.GIS
            Me.tgiS_ControlLegend1.Location = New System.Drawing.Point(0, 24)
            Me.tgiS_ControlLegend1.Mode = TatukGIS.NDK.TGIS_ControlLegendMode.Layers
            Me.tgiS_ControlLegend1.Name = "tgiS_ControlLegend1"
            Me.tgiS_ControlLegend1.Options = CType(((((TatukGIS.NDK.TGIS_ControlLegendOption.AllowMove Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowActive) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowExpand) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowParams) _
            Or TatukGIS.NDK.TGIS_ControlLegendOption.AllowSelect), TatukGIS.NDK.TGIS_ControlLegendOption)
            Me.tgiS_ControlLegend1.ReverseOrder = True
            Me.tgiS_ControlLegend1.Size = New System.Drawing.Size(144, 423)
            Me.tgiS_ControlLegend1.TabIndex = 2
            '
            'GIS
            '
            Me.GIS.BackColor = System.Drawing.SystemColors.Control
            Me.GIS.Cursor = System.Windows.Forms.Cursors.Default
            Me.GIS.CursorFor3DSelect = Nothing
            Me.GIS.CursorForCameraZoom = Nothing
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(144, 24)
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(448, 423)
            Me.GIS.TabIndex = 3
            Me.GIS.UseRTree = False
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.tgiS_ControlLegend1)
            Me.Controls.Add(Me.statusBar1)
            Me.Controls.Add(Me.panel1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Join and Render"
            Me.panel1.ResumeLayout(False)
            Me.panel4.ResumeLayout(False)
            Me.panel4.PerformLayout()
            CType(Me.scrTransparency, System.ComponentModel.ISupportInitialize).EndInit()
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

            ' create ADO .NET connection object
            sqlConnection = New OleDbConnection()
            sqlConnection.ConnectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}Stats.mdb", TGIS_Utils.GisSamplesDataDirDownload() + "\World\Countries\USA\Statistical\")
            sqlConnection.Open()

            ' create a layer, set render and label params
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
            ll.Params.Render.StartSize = 350
            ll.Params.Render.EndSize = 1000

            GIS.Add(ll)
            GIS.FullExtent()

            cmbSize_SelectedIndexChanged(Me, e)
        End Sub

        Private Sub WinForm_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
            sqlConnection.Close()
        End Sub

        Private Sub cmbSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSize.SelectedIndexChanged
            Dim ll As TGIS_LayerVector
            Dim vsize As String
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

            ' render results
            ll.Params.Render.Expression = vsize
            ll.Params.Render.Zones = 10
            ll.Params.Render.MinVal = vmin
            ll.Params.Render.MaxVal = vmax
            ll.Params.Render.StartColor = TGIS_Color.FromARGB(panColorStart.BackColor.A, panColorStart.BackColor.R, panColorStart.BackColor.G, panColorStart.BackColor.B)
            ll.Params.Render.EndColor = TGIS_Color.FromARGB(panColorEnd.BackColor.A, panColorEnd.BackColor.R, panColorEnd.BackColor.G, panColorEnd.BackColor.B)
            ll.Params.Area.Color = TGIS_Color.RenderColor
            ll.Params.Area.ShowLegend = True

            GIS.InvalidateWholeMap()
        End Sub

        Private Sub toolBar1_ButtonClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles toolBar1.ItemClicked
            Select Case toolBar1.Items.IndexOf(e.ClickedItem)
                ' btnFullExt
                Case 0
                    GIS.FullExtent()
                ' btnZoomIn
                Case 1
                    ' change viewer zoom
                    GIS.Zoom = GIS.Zoom * 2
                ' btnZoomOut
                Case 2
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

        Private Sub panColorStart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles panColorStart.Click
            If colorDialog1.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
                Return
            End If

            panColorStart.BackColor = colorDialog1.Color
            cmbSize_SelectedIndexChanged(sender, e)
        End Sub

        Private Sub panColorEnd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles panColorEnd.Click
            If colorDialog1.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
                Return
            End If

            panColorEnd.BackColor = colorDialog1.Color
            cmbSize_SelectedIndexChanged(sender, e)
        End Sub

        Private Sub scrTransparency_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles scrTransparency.Scroll
            Dim ll As TGIS_LayerVector

            ll = CType(GIS.Items(0), TGIS_LayerVector)
            If ll Is Nothing Then
                Return
            End If

            ' change transparency
            ll.Transparency = scrTransparency.Value

            GIS.InvalidateWholeMap()
        End Sub
    End Class
End Namespace
