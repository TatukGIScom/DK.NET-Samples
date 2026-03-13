Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports TatukGIS.NDK

Namespace SplitbyArc
    ''' <summary>
    ''' Summary description for WinForm.
    ''' </summary>
    Public Class WinForm
        Inherits System.Windows.Forms.Form
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing
        Private statusBar1 As System.Windows.Forms.StatusStrip
        Private statusBarPanel1 As System.Windows.Forms.ToolStripStatusLabel
        Private paLeft As System.Windows.Forms.Panel
        Private WithEvents btnLine As System.Windows.Forms.Button
        Private WithEvents btnSplit As System.Windows.Forms.Button
        Private gboxResult As System.Windows.Forms.GroupBox
        Private lbInfo As System.Windows.Forms.Label
        Private WithEvents GIS As TatukGIS.NDK.WinForms.TGIS_ViewerWnd
        Private layerObj As TGIS_LayerVector 'layer for new shapes
        Private shpPolygon As TGIS_ShapePolygon 'shape for split
        Private shpArc As TGIS_ShapeArc 'shape for line
        Private layerPolygon As TGIS_LayerVector
        Private layerArc As TGIS_LayerVector

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WinForm))
            Me.statusBar1 = New System.Windows.Forms.StatusStrip()
            Me.statusBarPanel1 = New System.Windows.Forms.ToolStripStatusLabel()
            Me.paLeft = New System.Windows.Forms.Panel()
            Me.gboxResult = New System.Windows.Forms.GroupBox()
            Me.lbInfo = New System.Windows.Forms.Label()
            Me.btnSplit = New System.Windows.Forms.Button()
            Me.btnLine = New System.Windows.Forms.Button()
            Me.GIS = New TatukGIS.NDK.WinForms.TGIS_ViewerWnd()
            
            Me.paLeft.SuspendLayout()
            Me.gboxResult.SuspendLayout()
            Me.SuspendLayout()
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 447)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Items.AddRange(New System.Windows.Forms.ToolStripStatusLabel() {Me.statusBarPanel1})
            
            Me.statusBar1.Size = New System.Drawing.Size(592, 19)
            Me.statusBar1.TabIndex = 0
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = True
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Text = "Click mouse to add line points."
            Me.statusBarPanel1.Width = 575
            '
            'paLeft
            '
            Me.paLeft.Controls.Add(Me.gboxResult)
            Me.paLeft.Controls.Add(Me.btnSplit)
            Me.paLeft.Controls.Add(Me.btnLine)
            Me.paLeft.Dock = System.Windows.Forms.DockStyle.Left
            Me.paLeft.Location = New System.Drawing.Point(0, 0)
            Me.paLeft.Name = "paLeft"
            Me.paLeft.Size = New System.Drawing.Size(185, 447)
            Me.paLeft.TabIndex = 1
            '
            'gboxResult
            '
            Me.gboxResult.Controls.Add(Me.lbInfo)
            Me.gboxResult.Location = New System.Drawing.Point(8, 104)
            Me.gboxResult.Name = "gboxResult"
            Me.gboxResult.Size = New System.Drawing.Size(169, 49)
            Me.gboxResult.TabIndex = 2
            Me.gboxResult.TabStop = False
            Me.gboxResult.Text = " Shapes after split : "
            '
            'lbInfo
            '
            Me.lbInfo.Location = New System.Drawing.Point(8, 24)
            Me.lbInfo.Name = "lbInfo"
            Me.lbInfo.Size = New System.Drawing.Size(40, 13)
            Me.lbInfo.TabIndex = 0
            Me.lbInfo.Text = "..."
            '
            'btnSplit
            '
            Me.btnSplit.Enabled = False
            Me.btnSplit.Location = New System.Drawing.Point(8, 64)
            Me.btnSplit.Name = "btnSplit"
            Me.btnSplit.Size = New System.Drawing.Size(129, 25)
            Me.btnSplit.TabIndex = 1
            Me.btnSplit.Text = "Split shape"
            '
            'btnLine
            '
            Me.btnLine.Location = New System.Drawing.Point(8, 24)
            Me.btnLine.Name = "btnLine"
            Me.btnLine.Size = New System.Drawing.Size(129, 25)
            Me.btnLine.TabIndex = 0
            Me.btnLine.Text = "New line / Create line"
            '
            'GIS
            '
            Me.GIS.Dock = System.Windows.Forms.DockStyle.Fill
            Me.GIS.Location = New System.Drawing.Point(185, 0)
            Me.GIS.Mode = TatukGIS.NDK.TGIS_ViewerMode.Edit
            Me.GIS.Name = "GIS"
            Me.GIS.SelectionColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
            Me.GIS.SelectionTransparency = 100
            Me.GIS.Size = New System.Drawing.Size(407, 447)
            Me.GIS.TabIndex = 2
            Me.GIS.UseRTree = False
            '
            'WinForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(592, 466)
            Me.Controls.Add(Me.GIS)
            Me.Controls.Add(Me.paLeft)
            Me.Controls.Add(Me.statusBar1)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Location = New System.Drawing.Point(200, 120)
            Me.Name = "WinForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "TatukGIS Samples - Split by Arc"
            
            Me.paLeft.ResumeLayout(False)
            Me.gboxResult.ResumeLayout(False)
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
            GIS.Lock()
            GIS.Open(TGIS_Utils.GisSamplesDataDirDownload() & "\Samples\Topology\topology3.shp")

            layerPolygon = (CType(GIS.Items(0), TGIS_LayerVector))
            shpPolygon = CType(layerPolygon.GetShape(1).MakeEditable(), TGIS_ShapePolygon)
            If shpPolygon Is Nothing Then
                Return
            End If

            layerArc = New TGIS_LayerVector() 'create layer for line
            layerArc.Params.Line.Color = TGIS_Color.Red
            layerArc.Params.Line.Width = 25
            If layerArc Is Nothing Then
                Return
            End If
            GIS.Add(layerArc)

            shpArc = CType((CType(GIS.Items(1), TGIS_LayerVector)).CreateShape(TGIS_ShapeType.Arc), TGIS_ShapeArc)
            If shpArc Is Nothing Then
                Return
            End If
            shpArc.AddPart()

            layerObj = New TGIS_LayerVector() 'create layer for new shapes - after split
            layerObj.Name = "Splits"
            GIS.Add(layerObj)

            GIS.Unlock()
            GIS.FullExtent()
            GIS.RestrictedExtent = GIS.Extent
        End Sub

        Private Sub btnLine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLine.Click
            layerObj.RevertAll() 'clear layer with new polygons
            shpArc.Reset() 'clear line
            shpArc.AddPart() 'initiate for new points
            lbInfo.Text = "..."
            GIS.InvalidateWholeMap()
            btnSplit.Enabled = False
        End Sub

        Private Sub btnSplit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSplit.Click
            Dim n As Integer
            Dim shape_list As ArrayList
            Dim topology_obj As TGIS_Topology
            Dim rnd As Random

            layerObj.RevertAll()
            topology_obj = New TGIS_Topology()
            shape_list = topology_obj.SplitByArc(shpPolygon, shpArc, True)
            If Not shape_list Is Nothing Then
                lbInfo.Text = shape_list.Count.ToString()
                rnd = New Random()
                n = 0
                Do While n < shape_list.Count
                    CType(shape_list(n), TGIS_Shape).Params.Area.Color = TGIS_Color.FromRGB(rnd.Next(256), rnd.Next(256), rnd.Next(256))
                    layerObj.AddShape(CType(shape_list(n), TGIS_Shape))
                    n += 1
                Loop
                shape_list = Nothing
            End If
            GIS.InvalidateWholeMap()
        End Sub

        Private Sub GIS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseDown
            Dim ptg As TGIS_Point

            'add point to line
            If GIS.IsEmpty Then
                Return
            End If

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            shpArc.Lock(TGIS_Lock.Extent)
            shpArc.AddPoint(ptg)
            shpArc.Unlock()
            GIS.InvalidateWholeMap()
            If shpArc.Intersect(shpPolygon) Then
                btnSplit.Enabled = True
            End If
        End Sub

        Private Sub GIS_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GIS.MouseUp
            Dim ptg As TGIS_Point

            'add point to line
            If GIS.IsEmpty Then
                Return
            End If

            ptg = GIS.ScreenToMap(New Point(e.X, e.Y))
            shpArc.Lock(TGIS_Lock.Extent)
            shpArc.AddPoint(ptg)
            shpArc.Unlock()
            GIS.InvalidateWholeMap()
            If shpArc.Intersect(shpPolygon) Then
                btnSplit.Enabled = True
            End If
        End Sub
    End Class
End Namespace
